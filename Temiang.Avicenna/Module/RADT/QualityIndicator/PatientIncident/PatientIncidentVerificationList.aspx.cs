using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Collections.Generic;
using System.Web;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PatientIncidentVerificationList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e); // !!Jika tidak dipanggil, tampilan jadi tidak rapih
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.PatientIncidentVerification;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnit(cboServiceUnitID, false);
                StandardReference.InitializeIncludeSpace(cboSRIncidentGroup, AppEnum.StandardReference.IncidentGroup);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsPostBack) return;

            RestoreValueFromCookie();
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdOutstanding_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit) return;

            var grd = (RadGrid)source;
            var dataSource = PatientIncidentOutstandings;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }

        private DataTable PatientIncidentOutstandings
        {
            get
            {
                var isEmptyFilter = txtIncidentFromDate.IsEmpty && txtIncidentToDate.IsEmpty && string.IsNullOrEmpty(txtIncidentNo.Text) && string.IsNullOrEmpty(cboSRIncidentGroup.SelectedValue) &&
                    string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtReportedBy.Text);
                if (!ValidateSearch(isEmptyFilter, "Incident")) return null;

                var query = new PatientIncidentQuery("a");
                var qsu = new ServiceUnitQuery("b");
                var qreg = new RegistrationQuery("d");
                var qpat = new PatientQuery("e");
                var qrefgr = new AppStandardReferenceItemQuery("f");
                var sal = new AppStandardReferenceItemQuery("sal");
                var qrepBy = new AppUserQuery("rb");
                var rgn = new RiskGradingMtxQuery("rgn");
                var rg = new RiskGradingQuery("rg");

                query.InnerJoin(qsu).On(query.ServiceUnitIDInCharge == qsu.ServiceUnitID);
                query.LeftJoin(qreg).On(query.RegistrationNo == qreg.RegistrationNo);
                query.LeftJoin(qpat).On(qreg.PatientID == qpat.PatientID);
                query.LeftJoin(qrefgr).On(query.SRIncidentGroup == qrefgr.ItemID & qrefgr.StandardReferenceID == AppEnum.StandardReference.IncidentGroup.ToString());
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == qpat.SRSalutation);
                query.LeftJoin(qrepBy).On(qrepBy.UserID == query.ReportedByUserID);
                query.LeftJoin(rgn).On(query.SRClinicalImpact == rgn.SRClinicalImpact &&
                                        query.SRIncidentProbabilityFrequency == rgn.SRIncidentProbabilityFrequency &&
                                        query.SRIncidentFollowUp == rgn.SRIncidentFollowUp);
                query.LeftJoin(rg).On(rgn.RiskGradingID == rg.RiskGradingID);

                query.Where(query.IsApproved == true, 
                    query.Or(query.IsVerified.IsNull(), query.IsVerified == false), 
                    query.IsRiskManagement == false);

                query.OrderBy
                    (
                        query.IncidentDateTime.Descending
                    );

                query.Select(
                    query.PatientIncidentNo,
                            query.IncidentDateTime,
                            query.RegistrationNo,
                            qpat.MedicalNo,
                            query.PatientName,
                            sal.ItemName.As("SalutationName"),
                            query.ServiceUnitIDInCharge.As("ServiceUnitID"),
                            qsu.ServiceUnitName,

                            query.SRIncidentType,
                            query.SRIncidentGroup,
                            query.SRClinicalImpact,
                            query.SRIncidentFollowUp,
                            query.ReportingDateTime,
                            query.ReportedByUserID,
                            qrefgr.ItemName.As("IncidentGroupName"),
                            query.IsApproved,
                            query.IsVerified,

                            rg.RiskGradingName,
                            rg.RiskGradingColor
                    );

                if (!txtIncidentFromDate.IsEmpty && !txtIncidentToDate.IsEmpty)
                    query.Where(query.IncidentDateTime >= txtIncidentFromDate.SelectedDate,
                                query.IncidentDateTime < txtIncidentToDate.SelectedDate.Value.AddDays(1));
                //query.Where(query.IncidentDateTime.Between(txtIncidentFromDate.SelectedDate, txtIncidentToDate.SelectedDate));
                if (!string.IsNullOrEmpty(txtIncidentNo.Text))
                    query.Where(query.PatientIncidentNo == txtIncidentNo.Text);
                if (!string.IsNullOrEmpty(cboSRIncidentGroup.SelectedValue))
                    query.Where(query.SRIncidentGroup == cboSRIncidentGroup.SelectedValue);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitIDInCharge == cboServiceUnitID.SelectedValue);

                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = "%" + txtRegistrationNo.Text + "%";
                    query.Where(
                            query.Or(
                                string.Format("<a.RegistrationNo LIKE '{0}' OR >", searchReg),
                                string.Format("<e.MedicalNo LIKE '{0}' OR >", searchReg),
                                string.Format("<e.OldMedicalNo LIKE '{0}'>", searchReg)
                                )
                            );
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + txtPatientName.Text + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(a.FirstName + ' ' + a.MiddleName)) + ' ' + a.LastName) LIKE '{0}'>", searchPatient)
                          //string.Format("<a.PatientName LIKE '{0}'>", searchPatient)
                        );
                }
                if (txtReportedBy.Text != string.Empty)
                {
                    string searchTextContain = string.Format("%{0}%", txtReportedBy.Text);
                    query.Where(query.Or(query.ReportedByUserID.Like(searchTextContain),
                                         qrepBy.UserName.Like(searchTextContain)));
                }

                return query.LoadDataTable();
            }
        }

        protected void grdVerification_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit) return;

            var grd = (RadGrid)source;
            var dataSource = PatientIncidentVerifications;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }

        private DataTable PatientIncidentVerifications
        {
            get
            {
                var isEmptyFilter = txtIncidentFromDate.IsEmpty && txtIncidentToDate.IsEmpty && string.IsNullOrEmpty(txtIncidentNo.Text) && string.IsNullOrEmpty(cboSRIncidentGroup.SelectedValue) &&
                    string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtReportedBy.Text);
                if (!ValidateSearch(isEmptyFilter, "Incident")) return null;

                var query = new PatientIncidentQuery("a");
                var qsu = new ServiceUnitQuery("b");
                var qreg = new RegistrationQuery("d");
                var qpat = new PatientQuery("e");
                var qrefgr = new AppStandardReferenceItemQuery("f");
                var sal = new AppStandardReferenceItemQuery("sal");
                var qrepBy = new AppUserQuery("rb");
                var rgn = new RiskGradingMtxQuery("rgn");
                var rg = new RiskGradingQuery("rg");

                query.InnerJoin(qsu).On(query.ServiceUnitIDInCharge == qsu.ServiceUnitID);
                query.LeftJoin(qreg).On(query.RegistrationNo == qreg.RegistrationNo);
                query.LeftJoin(qpat).On(qreg.PatientID == qpat.PatientID);
                query.LeftJoin(qrefgr).On(query.SRIncidentGroup == qrefgr.ItemID & qrefgr.StandardReferenceID == AppEnum.StandardReference.IncidentGroup.ToString());
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == qpat.SRSalutation);
                query.LeftJoin(qrepBy).On(qrepBy.UserID == query.ReportedByUserID);
                query.LeftJoin(rgn).On(query.SRClinicalImpact == rgn.SRClinicalImpact &&
                                        query.SRIncidentProbabilityFrequency == rgn.SRIncidentProbabilityFrequency &&
                                        query.SRIncidentFollowUp == rgn.SRIncidentFollowUp);
                query.LeftJoin(rg).On(rgn.RiskGradingID == rg.RiskGradingID);

                query.Where(query.IsApproved == true, query.IsVerified == true, query.IsRiskManagement == false);

                query.OrderBy
                    (
                        query.IncidentDateTime.Descending
                    );

                query.Select(
                    query.PatientIncidentNo,
                            query.IncidentDateTime,
                            query.RegistrationNo,
                            qpat.MedicalNo,
                            query.PatientName,
                            sal.ItemName.As("SalutationName"),
                            query.ServiceUnitIDInCharge.As("ServiceUnitID"),
                            qsu.ServiceUnitName,

                            query.SRIncidentType,
                            query.SRIncidentGroup,
                            query.SRClinicalImpact,
                            query.SRIncidentFollowUp,
                            query.ReportingDateTime,
                            query.ReportedByUserID,
                            qrefgr.ItemName.As("IncidentGroupName"),
                            query.IsApproved,
                            query.IsVerified,

                            rg.RiskGradingName,
                            rg.RiskGradingColor
                    );

                if (!txtIncidentFromDate.IsEmpty && !txtIncidentToDate.IsEmpty)
                    query.Where(query.IncidentDateTime >= txtIncidentFromDate.SelectedDate,
                                query.IncidentDateTime < txtIncidentToDate.SelectedDate.Value.AddDays(1));
                //query.IncidentDateTime.Between(txtIncidentFromDate.SelectedDate, txtIncidentToDate.SelectedDate));
                if (!string.IsNullOrEmpty(txtIncidentNo.Text))
                    query.Where(query.PatientIncidentNo == txtIncidentNo.Text);
                if (!string.IsNullOrEmpty(cboSRIncidentGroup.SelectedValue))
                    query.Where(query.SRIncidentGroup == cboSRIncidentGroup.SelectedValue);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitIDInCharge == cboServiceUnitID.SelectedValue);
                
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = "%" + txtRegistrationNo.Text + "%";
                    query.Where(
                            query.Or(
                                string.Format("<a.RegistrationNo LIKE '{0}' OR >", searchReg),
                                string.Format("<e.MedicalNo LIKE '{0}' OR >", searchReg),
                                string.Format("<e.OldMedicalNo LIKE '{0}'>", searchReg)
                                )
                            );
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + txtPatientName.Text + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(a.FirstName + ' ' + a.MiddleName)) + ' ' + a.LastName) LIKE '{0}'>", searchPatient)
                          //string.Format("<a.PatientName LIKE '{0}'>", searchPatient)
                        );
                }
                if (txtReportedBy.Text != string.Empty)
                {
                    string searchTextContain = string.Format("%{0}%", txtReportedBy.Text);
                    query.Where(query.Or(query.ReportedByUserID.Like(searchTextContain),
                                         qrepBy.UserName.Like(searchTextContain)));
                }
                    
                return query.LoadDataTable();
            }
        }


        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdOutstanding.Rebind();
            grdVerification.Rebind();
        }

        public System.Drawing.Color GetColorOfGradingColor(object GradingColor)
        {
            System.Drawing.Color color = System.Drawing.Color.White;
            switch (GradingColor.ToString())
            {
                case "Blue":
                    {
                        color = System.Drawing.Color.Blue;
                        break;
                    }
                case "Green":
                    {
                        color = System.Drawing.Color.Green;
                        break;
                    }
                case "Yellow":
                    {
                        color = System.Drawing.Color.Yellow;
                        break;
                    }
                case "Red":
                    {
                        color = System.Drawing.Color.Red;
                        break;
                    }
            }

            return color;
        }
    }
}
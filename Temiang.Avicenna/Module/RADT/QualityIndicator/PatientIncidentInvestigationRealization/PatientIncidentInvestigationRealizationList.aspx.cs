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
    public partial class PatientIncidentInvestigationRealizationList : BasePage
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

            ProgramID = AppConstant.Program.PatientIncidentInvestigationRealization;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnit(cboServiceUnitID, false);
                txtInvestigationDate.SelectedDate = DateTime.Now;
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

        protected void grdInvestigation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit) return;

            var grd = (RadGrid)source;
            var dataSource = PatientIncidentInvestigations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable PatientIncidentInvestigations
        {
            get
            {
                var isEmptyFilter = txtIncidentFromDate.IsEmpty && txtIncidentToDate.IsEmpty && string.IsNullOrEmpty(txtIncidentNo.Text) && txtInvestigationDate.IsEmpty &&
                    string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Incident")) return null;

                var query = new PatientIncidentQuery("a");
                var qrelUnit = new PatientIncidentRelatedUnitQuery("b");
                var qsuRel = new ServiceUnitQuery("c");
                var qreg = new RegistrationQuery("d");
                var qpat = new PatientQuery("e");
                var qrefgr = new AppStandardReferenceItemQuery("f");
                var qsu = new ServiceUnitQuery("g");

                var rgn = new RiskGradingMtxQuery("rgn");
                var rg = new RiskGradingQuery("rg");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.InnerJoin(qrelUnit).On(query.PatientIncidentNo == qrelUnit.PatientIncidentNo);
                query.InnerJoin(qsuRel).On(qrelUnit.ServiceUnitID == qsuRel.ServiceUnitID);
                query.InnerJoin(qsu).On(query.ServiceUnitIDInCharge == qsu.ServiceUnitID);
                query.LeftJoin(qreg).On(query.RegistrationNo == qreg.RegistrationNo);
                query.LeftJoin(qpat).On(qreg.PatientID == qpat.PatientID);
                query.LeftJoin(qrefgr).On(query.SRIncidentGroup == qrefgr.ItemID &
                                          qrefgr.StandardReferenceID ==
                                          AppEnum.StandardReference.IncidentGroup.ToString());

                query.LeftJoin(rgn).On(query.SRClinicalImpact == rgn.SRClinicalImpact &&
                                        query.SRIncidentProbabilityFrequency == rgn.SRIncidentProbabilityFrequency &&
                                        query.SRIncidentFollowUp == rgn.SRIncidentFollowUp);
                query.LeftJoin(rg).On(rgn.RiskGradingID == rg.RiskGradingID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == qpat.SRSalutation);

                query.OrderBy(qrelUnit.InvestigationDateTime.Ascending);

                query.Select(
                            query.PatientIncidentNo,
                            query.IncidentDateTime,
                            query.RegistrationNo,
                            qpat.MedicalNo,
                            query.PatientName,
                            sal.ItemName.As("SalutationName"),
                            qrelUnit.ServiceUnitID,
                            qsuRel.ServiceUnitName.As("RelatedUnitName"),
                            qsu.ServiceUnitName,

                            query.SRIncidentType,
                            query.SRIncidentGroup,
                            query.SRClinicalImpact,
                            query.SRIncidentFollowUp,
                            query.ReportingDateTime,
                            query.ReportedByUserID,
                            qrefgr.ItemName.As("IncidentGroupName"),
                            qrelUnit.IsInvestigationApproved,

                            rg.RiskGradingName,
                            rg.RiskGradingColor
                    );
                query.Where(qrelUnit.IsInvestigationApproved == true, query.IsRiskManagement == false);

                if (!txtIncidentFromDate.IsEmpty && !txtIncidentToDate.IsEmpty)
                    query.Where(query.IncidentDateTime >= txtIncidentFromDate.SelectedDate,
                                query.IncidentDateTime < txtIncidentToDate.SelectedDate.Value.AddDays(1));
                    //query.Where(query.IncidentDateTime.Between(txtIncidentFromDate.SelectedDate, txtIncidentToDate.SelectedDate));
                if (!string.IsNullOrEmpty(txtIncidentNo.Text))
                    query.Where(query.PatientIncidentNo == txtIncidentNo.Text);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(qrelUnit.ServiceUnitID == cboServiceUnitID.SelectedValue);

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
                    var searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where(string.Format(@"<LTRIM(RTRIM(LTRIM(a.FirstName + ' ' + a.MiddleName)) + ' ' + a.LastName) LIKE '{0}'>", searchPatient));
                }
                if (!txtInvestigationDate.IsEmpty)
                    query.Where(qrelUnit.InvestigationDateTime.Date() == txtInvestigationDate.SelectedDate);

                return query.LoadDataTable();
            }
        }

        protected void grdInvestigation_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new PatientIncidentInvestigationQuery("a");
            query.Select(query);
            query.Where(
                query.PatientIncidentNo == e.DetailTableView.ParentItem.GetDataKeyValue("PatientIncidentNo").ToString(),
                query.ServiceUnitID == e.DetailTableView.ParentItem.GetDataKeyValue("ServiceUnitID").ToString());
            query.OrderBy(query.SeqNo.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();
            
            grdInvestigation.Rebind();
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

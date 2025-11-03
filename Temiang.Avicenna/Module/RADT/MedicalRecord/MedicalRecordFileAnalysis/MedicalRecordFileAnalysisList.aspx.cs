using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class MedicalRecordFileAnalysisList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.AnalysisDocument;

            if (!IsPostBack)
            {

                txtDate.SelectedDate = DateTime.Now.Date;

                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");
                query.Where(
                    query.SRRegistrationType.In(
                        AppConstant.RegistrationType.EmergencyPatient,
                        AppConstant.RegistrationType.InPatient,
                        AppConstant.RegistrationType.OutPatient,
                        AppConstant.RegistrationType.MedicalCheckUp
                        ),
                    query.IsActive == true
                    );
                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);
                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                cboFilterStatus.Items.Add(new RadComboBoxItem("", ""));
                cboFilterStatus.Items.Add(new RadComboBoxItem("Already Analyzed", "0"));
                cboFilterStatus.Items.Add(new RadComboBoxItem("Not Analyzed", "1"));
            }
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

        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = txtDate.IsEmpty && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboFilterStatus.SelectedValue) && 
                    string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtMedicalNo.Text) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Medical Record File")) return null;

                var qa = new MedicalRecordFileStatusQuery("a");
                var qb = new RegistrationQuery("b");
                var qc = new PatientQuery("c");
                var qe = new AppStandardReferenceItemQuery("e");
                var qf = new AppStandardReferenceItemQuery("f");
                var qg = new ParamedicQuery("g");
                var qh = new ServiceUnitQuery("h");
                var qad = new AnalysisDocumentQuery("i");
                var asr = new AppStandardReferenceItemQuery("j");

                qa.es.Top = AppSession.Parameter.MaxResultRecord;
                qa.Select
                    (
                        qa.RegistrationNo,
                        qb.RegistrationDate,
                        qb.RegistrationTime,
                        qb.RegistrationDateTime,
                        qb.DischargeDate,
                        qb.DischargeTime,
                        qb.DischargeDateTime,
                        qc.MedicalNo,
                        asr.ItemName.As("SalutationName"),
                        qc.PatientName,
                        qc.Sex,
                        qa.SRMedicalFileStatus,
                        qa.SRMedicalFileCategory,
                        qe.ItemName.As("MedicalFileCategory"),
                        qf.ItemName.As("MedicalFileStatus"),
                        qg.ParamedicName,
                        qh.ServiceUnitName,
                        qb.SRRegistrationType,
                        qb.IsConsul,
                        qad.Notes,
                        qa.ReceiptByUserID,
                        qa.FileInDate,
                        @"<CASE WHEN ISNULL(i.SRCompleteStatusRM, '') = '1' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsComplete>"                    
                    );

                qa.InnerJoin(qb).On(qa.RegistrationNo == qb.RegistrationNo);
                qa.InnerJoin(qc).On(qb.PatientID == qc.PatientID);
                qa.LeftJoin(qe).On
                        (
                            qa.SRMedicalFileCategory == qe.ItemID &
                            qe.StandardReferenceID == "MedicalFileCategory"
                        );
                qa.LeftJoin(qf).On
                        (
                            qa.SRMedicalFileStatus == qf.ItemID &
                            qf.StandardReferenceID == "MedicalFileStatus"
                        );
                qa.InnerJoin(qg).On(qb.ParamedicID == qg.ParamedicID);
                qa.InnerJoin(qh).On(qb.ServiceUnitID == qh.ServiceUnitID);
                qa.LeftJoin(qad).On(qa.RegistrationNo == qad.RegistrationNo);
                qa.LeftJoin(asr).On(asr.StandardReferenceID == "Title" & qc.SRSalutation == asr.ItemID);

                if (!txtDate.IsEmpty)
                    qa.Where(string.Format(@"<(b.SRRegistrationType = 'IPR' AND b.DischargeDate = '{0}') OR (b.SRRegistrationType <> 'IPR' AND b.RegistrationDate = '{0}') >", txtDate.SelectedDate));
                    
                if (txtMedicalNo.Text != string.Empty)
                {
                    string searchMedNo = Helper.EscapeQuery(txtMedicalNo.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        qa.Where(
                            qa.Or(
                                qc.MedicalNo == searchMedNo,
                                qc.OldMedicalNo == searchMedNo,
                                string.Format("< OR REPLACE(c.MedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo),
                                string.Format("< OR REPLACE(c.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo)
                                )
                            );
                    else
                        qa.Where(
                            qa.Or(
                                qc.MedicalNo == searchMedNo,
                                qc.OldMedicalNo == searchMedNo,
                                string.Format("< OR c.MedicalNo LIKE '%{0}%'>", searchMedNo),
                                string.Format("< OR c.OldMedicalNo LIKE '%{0}%'>", searchMedNo)
                                )
                            );
                }

                if (txtRegistrationNo.Text != string.Empty)
                    qa.Where(qa.RegistrationNo == txtRegistrationNo.Text);

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    qa.Where(qb.ServiceUnitID == cboServiceUnitID.SelectedValue);

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";

                    qa.Where
                        (
                          string.Format("<RTRIM(c.FirstName+' '+c.MiddleName)+' '+c.LastName LIKE '{0}'>", searchPatient)
                        );
                }
                if (!string.IsNullOrEmpty(cboFilterStatus.SelectedValue))
                {
                    qa.Where(cboFilterStatus.SelectedValue == "0"
                                 ? qad.RegistrationNo.IsNotNull()
                                 : qad.RegistrationNo.IsNull());
                }

                qa.Where(qa.FileInDate.IsNotNull());

                qa.OrderBy(qb.RegistrationDate.Descending, qb.RegistrationTime.Descending);

                DataTable tbl = qa.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    if (row["SRRegistrationType"].ToString() == AppConstant.RegistrationType.ClusterPatient &&
                        (bool)row["IsConsul"])
                        row.Delete();
                }

                tbl.AcceptChanges();

                return tbl;
            }
        }

        protected void grdRegisteredList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit) {
                grd.DataSource = new String[] { };
                return;
            }
            
            var dataSource = Registrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdRegisteredList.DataSource = Registrations;
            grdRegisteredList.CurrentPageIndex = 0;
            grdRegisteredList.Rebind();
        }
    }
}

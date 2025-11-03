using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class UpdateMrnPatientDetail : BasePageDialog
    {
        private string pId
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["pid"]) ? string.Empty : Request.QueryString["pid"];
            }
        }

        private string errorMsg;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.UpdateMrnPatient;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(pId);
                txtFromMedicalNo.Text = pat.MedicalNo;
                txtFromFirstName.Text = pat.FirstName;
                txtFromMiddleName.Text = pat.MiddleName;
                txtFromLastName.Text = pat.LastName;

                txtToMedicalNo.ReadOnly = AppSession.Parameter.IsReadonlyMedicalNoOnUpdateMrnPatient;
                txtToMedicalNo.Text = pat.MedicalNo;
                txtToFirstName.Text = pat.FirstName;
                txtToMiddleName.Text = pat.MiddleName;
                txtToLastName.Text = pat.LastName;
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            PatientUpdate();
            if (!string.IsNullOrEmpty(errorMsg))
            {
                ShowInformationHeader(errorMsg);
                return false;
            }

            return true;
        }


        private void PatientUpdate()
        {
            if (string.IsNullOrEmpty(txtToMedicalNo.Text) && string.IsNullOrEmpty(txtToFirstName.Text) && string.IsNullOrEmpty(txtToMiddleName.Text) && string.IsNullOrEmpty(txtToLastName.Text))
            {
                errorMsg = "No data change.";
                return;
            }

            if (txtFromMedicalNo.Text.Trim() == txtToMedicalNo.Text.Trim() && 
                txtFromFirstName.Text.Trim() == txtToFirstName.Text.Trim() &&
                txtFromMiddleName.Text.Trim() == txtToMiddleName.Text.Trim() && 
                txtFromLastName.Text.Trim() == txtToLastName.Text.Trim())
            {
                errorMsg = "No data change.";
                return;
            }

            if (!string.IsNullOrEmpty(txtFromMedicalNo.Text) && string.IsNullOrEmpty(txtToMedicalNo.Text))
            {
                errorMsg = "Medical No required.";
                return;
            }

            if (string.IsNullOrEmpty(txtToFirstName.Text))
            {
                errorMsg = "First Name required.";
                return;
            }

            var patientQuery = new PatientQuery();
            patientQuery.es.Top = 1;
            patientQuery.Select(patientQuery.PatientName, patientQuery.PatientID);
            patientQuery.Where(patientQuery.MedicalNo == txtToMedicalNo.Text);
            DataTable dtb = patientQuery.LoadDataTable();
            if (dtb.Rows.Count > 0 && !dtb.Rows[0]["PatientID"].Equals(pId))
            {
                errorMsg = "MRN: " + txtToMedicalNo.Text.Trim() + " has been used by another patient, please change to other No.";
                return;
            }

            using (var trans = new esTransactionScope())
            {
                var entity = new Patient();
                entity.LoadByPrimaryKey(pId);

                if (txtFromMedicalNo.Text.Trim() != txtToMedicalNo.Text.Trim())
                    entity.MedicalNo = txtToMedicalNo.Text;

                if (txtFromFirstName.Text.Trim() != txtToFirstName.Text.Trim() ||
                    txtFromMiddleName.Text.Trim() != txtToMiddleName.Text.Trim() ||
                    txtFromLastName.Text.Trim() != txtToLastName.Text.Trim())
                {
                    if (AppSession.Parameter.IsUppercasePatientID)
                    {
                        entity.FirstName = txtToFirstName.Text.ToUpper();
                        entity.MiddleName = txtToMiddleName.Text.ToUpper();
                        entity.LastName = txtToLastName.Text.ToUpper();
                    }
                    else
                    {
                        entity.FirstName = txtToFirstName.Text;
                        entity.MiddleName = txtToMiddleName.Text;
                        entity.LastName = txtToLastName.Text;
                    }
                }

                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                var hist = new PatientMRNNameHistory();
                hist.AddNew();
                hist.PatientID = pId;
                hist.UpdateDateTime = (new DateTime()).NowAtSqlServer();
                hist.UpdateByUserID = AppSession.UserLogin.UserID;
                hist.FromMedicalNo = txtFromMedicalNo.Text;
                hist.FromFirstName = txtFromFirstName.Text;
                hist.FromMiddleName = txtFromMiddleName.Text;
                hist.FromLastName = txtFromLastName.Text;
                hist.ToMedicalNo = txtToMedicalNo.Text;
                if (AppSession.Parameter.IsUppercasePatientID)
                {
                    hist.ToFirstName = txtToFirstName.Text.ToUpper();
                    hist.ToMiddleName = txtToMiddleName.Text.ToUpper();
                    hist.ToLastName = txtToLastName.Text.ToUpper();
                }
                else
                {
                    hist.ToFirstName = txtToFirstName.Text;
                    hist.ToMiddleName = txtToMiddleName.Text;
                    hist.ToLastName = txtToLastName.Text;
                }


                entity.Save();
                hist.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            errorMsg = string.Empty;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

    }
}
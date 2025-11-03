using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class MedicalRecordFileStatusDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "#";
            UrlPageList = "MedicalRecordFileStatusList.aspx";
            ProgramID = AppConstant.Program.MedicalFileStatus;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnit(cboServiceUnitID, false);
                StandardReference.InitializeIncludeSpace(cboSRMedicalFileCategory, AppEnum.StandardReference.MedicalFileCategory);
                StandardReference.InitializeIncludeSpace(cboSRMedicalFileStatus, AppEnum.StandardReference.MedicalFileStatus);
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new MedicalRecordFileStatus();
            if (!entity.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {

        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {

        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {

        }

        protected override void OnMenuNewClick()
        {
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new MedicalRecordFileStatus();
            if (entity.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new MedicalRecordFileStatus();
            if (entity.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new MedicalRecordFileStatus();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new MedicalRecordFileStatus();
            if (entity.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("RegistrationNo='{0}'", txtRegistrationNo.Text);
            auditLogFilter.TableName = "MedicalRecordFileStatus";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtRegistrationNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return true;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new MedicalRecordFileStatus();
            if (parameters.Length > 0)
            {
                String regNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(regNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtRegistrationNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var fs = (MedicalRecordFileStatus)entity;
            txtRegistrationNo.Text = fs.RegistrationNo;

            PopulatePatientInformation(fs.RegistrationNo);
            if (fs.FileInDate != null)
            {
                txtFileInDate.SelectedDate = fs.FileInDate.Value.Date;
                txtFileInTime.Text = fs.FileInDate.Value.ToString("HH:mm");
            }
            
            txtFileOutDate.SelectedDate = fs.FileOutDate.Value.Date;
            txtFileOutTime.Text = fs.FileOutDate.Value.ToString("HH:mm");
            cboSRMedicalFileCategory.SelectedValue = fs.SRMedicalFileCategory;
            cboSRMedicalFileStatus.SelectedValue = fs.SRMedicalFileStatus;
            txtRequestByUserID.Text = fs.RequestByUserID;
            txtReceiptByUserID.Text = fs.ReceiptByUserID;

            txtNotes.Text = fs.Notes;

            if (!string.IsNullOrEmpty(fs.ReceiptByUserID))
            {
                var usr = new AppUser();
                txtNameOfRecipient.Text = usr.LoadByPrimaryKey(fs.ReceiptByUserID) ? usr.UserName : string.Empty;
            }
            if (!string.IsNullOrEmpty(fs.RequestByUserID))
            {
                var usr = new AppUser();
                txtRequestName.Text = usr.LoadByPrimaryKey(fs.RequestByUserID) ? usr.UserName : string.Empty;
            }
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(MedicalRecordFileStatus entity)
        {
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.FileOutDate = txtFileOutDate.SelectedDate;
            entity.SRMedicalFileCategory = cboSRMedicalFileCategory.SelectedValue;
            entity.SRMedicalFileStatus = cboSRMedicalFileStatus.SelectedValue;
            entity.Notes = txtNotes.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(MedicalRecordFileStatus entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new MedicalRecordFileStatusQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.RegistrationNo > txtRegistrationNo.Text
                    );
                que.OrderBy(que.RegistrationNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.RegistrationNo < txtRegistrationNo.Text
                    );
                que.OrderBy(que.RegistrationNo.Descending);
            }

            var entity = new MedicalRecordFileStatus();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        private void PopulatePatientInformation(string regNo)
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(regNo))
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;

                txtAddress.Text = pat.StreetName + " " + pat.City.Trim() + " " + pat.County.Trim();

                string ageYear = Helper.GetAgeInYear(pat.DateOfBirth.Value).ToString();
                string ageMonth = Helper.GetAgeInMonth(pat.DateOfBirth.Value).ToString();
                string ageDay = Helper.GetAgeInDay(pat.DateOfBirth.Value).ToString();

                if (ageYear == "0")
                {
                    if (ageMonth == "0")
                        txtAge.Text = ageDay + " d";
                    else
                        txtAge.Text = ageMonth + " m";
                }
                else
                    txtAge.Text = ageYear + " y";


                if (pat.Sex == "M")
                    txtGender.Text = "Male";
                else
                    txtGender.Text = "Female";

                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);

                txtPhysicianName.Text = par.ParamedicName;

                cboServiceUnitID.SelectedValue = reg.ServiceUnitID;
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtAge.Text = string.Empty;
                txtGender.Text = string.Empty;
                txtPhysicianName.Text = string.Empty;
                cboServiceUnitID.SelectedValue = string.Empty;
                cboServiceUnitID.Text = string.Empty;
            }
        }
    }
}

using System;
using System.IO;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class PatientDocumentEntry : BasePageDialogEntry
    {
        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var entity = new PatientDocument();

            entity.LoadByPrimaryKey(PatientDocumentID);
            OnPopulateEntryControl(entity);
        }

        protected void OnPopulateEntryControl(esEntity entity)
        {
            var ent = (PatientDocument)entity;
            txtDocumentName.Text = ent.DocumentName;
            txtFileAttachName.Text = ent.FileAttachName;
            txtDocumentDate.SelectedDate = ent.DocumentDate;
            txtNotes.Text = ent.Notes;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {
            var ent = new PatientDocument();
            ent.RegistrationNo = RegistrationNo;
            ent.PatientID = PatientID;

            OnPopulateEntryControl(ent);

            txtDocumentDate.SelectedDate = (new DateTime()).NowAtSqlServer();
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new PatientDocument();
            entity = new PatientDocument();
            entity.AddNew();
            SetEntityValue(entity);
            var newID = SaveEntity(entity);

            // Upload file
            UploadFile(newID);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new PatientDocument();
            if (entity.LoadByPrimaryKey(PatientDocumentID))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
            // Upload file
            UploadFile(PatientDocumentID);
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
        }


        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new PatientDocument();
            if (entity.LoadByPrimaryKey(PatientDocumentID))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {

        }
        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        protected override void OnMenuRejournalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }
        public override string OnGetScriptToolBarSaveClicking()
        {
            return string.Empty;
        }
        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return true;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion


        protected int PatientDocumentID
        {
            get { return Convert.ToInt32(Request.QueryString["pdid"]); }
        }

        #region Page Event & Initialize

        private CpoeTypeEnum CpoeType
        {
            get
            {
                switch (this.Request.QueryString["rt"])
                {
                    case "IPR":
                        return CpoeTypeEnum.InPatient;
                    //case "EMR":
                    //    return CpoeTypeEnum.Emergency;
                    default:
                        return CpoeTypeEnum.Outpatient;
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicHealthRecord;

            // Program Fiture
            IsSingleRecordMode = true; // Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;
            // -------------------

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #endregion


        private void SetEntityValue(PatientDocument entity)
        {
            entity.PatientID = PatientID;
            entity.RegistrationNo = RegistrationNo;
            entity.DocumentName = txtDocumentName.Text;
            entity.DocumentDate = txtDocumentDate.SelectedDate;
            entity.Notes = txtNotes.Text;
            if (entity.es.IsAdded)
                entity.FileAttachName = string.Empty;
        }

        private long SaveEntity(PatientDocument entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
            return entity.PatientDocumentID ?? 0;
        }


        private void UploadFile(long pdid)
        {
            if (uplFileTemplate.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile validFile in uplFileTemplate.UploadedFiles)
                {
                    var fileName = string.Format("{0:000000000000000}_{1}", pdid, validFile.GetName());
                    var entity = new PatientDocument();
                    if (entity.LoadByPrimaryKey(pdid))
                    {
                        entity.FileAttachName = fileName;
                        entity.Save();
                    }

                    //var targetFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", entity.PatientID.Trim());

                    //if (!System.IO.Directory.Exists(targetFolder))
                    //    System.IO.Directory.CreateDirectory(targetFolder);

                    var targetFolderOld = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", entity.PatientID.Trim());
                    var targetFolderYearly = "";
                    if (!string.IsNullOrEmpty(entity.DocumentFolderYearly))
                        targetFolderYearly = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocumentYearly", entity.DocumentFolderYearly, entity.PatientID.Trim());

                    var targetFolder = targetFolderOld;
                    if (!System.IO.Directory.Exists(targetFolder))
                    {
                        // jika old blm ada brarti pakai yearly
                        targetFolder = string.IsNullOrEmpty(targetFolderYearly) ? targetFolderOld : targetFolderYearly;
                    }

                    if (!System.IO.Directory.Exists(targetFolder))
                        System.IO.Directory.CreateDirectory(targetFolder);

                    validFile.SaveAs(Path.Combine(targetFolder, fileName), true);
                    break;
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class PatientDocumentHistEnt : BasePageDialogHistEntry
    {

        #region Query String Properties
        private string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        private string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }

        protected long PatientDocumentID
        {
            get
            {
                if (!IsPostBack)
                    hdnPdId.Value = Request.QueryString["pdid"];
                if (string.IsNullOrEmpty(hdnPdId.Value))
                    hdnPdId.Value = "0";
                return Convert.ToInt32(hdnPdId.Value);
            }
            set { hdnPdId.Value = value.ToString(); }
        }
        #endregion

        #region Document History
        private DataTable PatientDocumentDataTable()
        {
            var query = new PatientDocumentQuery("a");
            query.Where(query.PatientID == PatientID, query.Or(query.IsDeleted.IsNull(), query.IsDeleted == false));

            if (chkIsCurrentReg.Checked == true)
                query.Where(query.RegistrationNo == RegistrationNo);

            if (!string.IsNullOrWhiteSpace(txtSearchDocumentName.Text))
                query.Where(query.DocumentName.Like("%" + txtSearchDocumentName.Text.Trim() + "%"));

            query.OrderBy(query.DocumentDate.Descending);

            var dtb = query.LoadDataTable();

            // Replace empty smallimage
            var imgHelper = new ImageHelper();
            var pdfImage = imgHelper.LoadImageToArray(string.Format("{0}\\Images\\pdf100.png", Server.MapPath("~")));
            var dicomImage = imgHelper.LoadImageToArray(string.Format("{0}\\Images\\dicom.png", Server.MapPath("~")));
            foreach (DataRow row in dtb.Rows)
            {
                var fileName = row["FileAttachName"].ToString().ToLower();
                if (fileName.Contains(".pdf"))
                {
                    row["SmallImage"] = pdfImage;
                }
                else if (fileName.Contains(".dcm"))
                {
                    row["SmallImage"] = dicomImage;
                }
            }

            return dtb;
        }

        protected void grdDocument_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdDocument.DataSource = PatientDocumentDataTable();
        }
        protected void grdDocument_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;
            PatientDocumentID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["PatientDocumentID"]);

            if (e.CommandName == "View")
            {
                OnPopulateEntryControl(new ValidateArgs());
                RefreshMenuStatus();
            }
        }
        #endregion


        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var entity = new PatientDocument();
            if (entity.LoadByPrimaryKey(PatientDocumentID))
                OnPopulateEntryControl(entity);
            else
                OnPopulateEntryControl(new PatientDocument());
        }

        protected void OnPopulateEntryControl(esEntity entity)
        {
            var pd = (PatientDocument)entity;
            txtDocumentName.Text = pd.DocumentName;
            txtFileAttachName.Text = pd.FileAttachName;
            txtDocumentDate.SelectedDate = pd.DocumentDate;
            txtNotes.Text = pd.Notes;
            PatientDocumentID = pd.PatientDocumentID ?? 0;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            grdDocument.Columns[0].Display = newVal == AppEnum.DataMode.Read;
            uplFileTemplate.Enabled = newVal == AppEnum.DataMode.New;
            btnBathUpload.Enabled = newVal == AppEnum.DataMode.Read;
        }
        protected override void OnMenuNewClick()
        {
            PatientDocumentID = 0;

            var ent = new PatientDocument();
            ent.RegistrationNo = RegistrationNo;
            ent.PatientID = PatientID;

            OnPopulateEntryControl(ent);

            txtDocumentDate.SelectedDate = (new DateTime()).NowAtSqlServer();
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new PatientDocument();
            SetEntityValue(entity);
            var newID = SaveEntity(entity);
            PatientDocumentID = newID;

            // Upload file
            var fullPathFileName = UploadFile(entity);

            // Save thumbnail
            if (!string.IsNullOrWhiteSpace(fullPathFileName))
            {
                if (entity.OriFileName.Contains(".jpg") || entity.OriFileName.Contains(".jpg") || entity.OriFileName.Contains(".png"))
                {
                    var imgHelper = new ImageHelper();
                    var imgArr = imgHelper.LoadImageToArray(fullPathFileName);
                    var smallImg = imgHelper.ResizeImage(imgArr, new Size(100, 100), true, InterpolationMode.Low);

                    if (entity.OriFileName.Contains(".jpg") || entity.OriFileName.Contains(".jpg"))
                        entity.SmallImage = imgHelper.ConvertImageToByteArray(smallImg, ImageFormat.Jpeg);
                    else if (entity.OriFileName.Contains(".png"))
                        entity.SmallImage = imgHelper.ConvertImageToByteArray(smallImg, ImageFormat.Png);
                }
                entity.Save();
            }
            else
            {
                // Fail Upload
                // Delete 
                entity.MarkAsDeleted();
                entity.Save();
            }

            grdDocument.Rebind();
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            // Edit hanya untuk update keterangan saja, jika edit filenya harus lewat delete lalu tambah
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
            grdDocument.Rebind();
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
            var ent = new PatientDocument();
            if (ent.LoadByPrimaryKey(PatientDocumentID))
            {
                //// Delete File
                //var filePath = (ent.IsUpload ?? false) ?
                //System.IO.Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", ent.PatientID.Trim(), ent.FileAttachName)
                //: ent.OriPath;

                //if (!System.IO.File.Exists(filePath))
                //    System.IO.File.Delete(filePath);

                //ent.MarkAsDeleted();

                // Rename File
                var filePath = "";
                if (ent.IsUpload ?? false) {
                    //filePath = System.IO.Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", ent.PatientID.Trim(), ent.FileAttachName);
                    
                    var fileFolderOld = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", ent.PatientID.Trim());
                    var fileFolderYearly = "";
                    if (!string.IsNullOrEmpty(ent.DocumentFolderYearly))
                        fileFolderYearly = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocumentYearly", ent.DocumentFolderYearly, ent.PatientID.Trim());

                    var fileFolder = fileFolderOld;
                    if (!System.IO.Directory.Exists(fileFolder))
                    {
                        // jika old blm ada brarti pakai yearly
                        fileFolder = string.IsNullOrEmpty(fileFolderYearly) ? fileFolderOld : fileFolderYearly;
                    }

                    filePath = Path.Combine(fileFolder, ent.FileAttachName);
                }
                else { 
                    filePath = ent.OriPath; 
                }

                if (System.IO.File.Exists(filePath))
                {
                    var newFilePath = Path.Combine(System.IO.Path.GetDirectoryName(filePath), "DEL_" + System.IO.Path.GetFileName(filePath));

                    File.Move(filePath, newFilePath);
                }

                ent.IsDeleted = true;
                ent.Save();
                PatientDocumentID = 0;
                grdDocument.Rebind();
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
            return !string.IsNullOrEmpty(hdnPdId.Value) && hdnPdId.Value != "0";
        }

        public override bool OnGetStatusMenuDelete()
        {
            return !string.IsNullOrEmpty(hdnPdId.Value) && hdnPdId.Value != "0";
        }

        public override bool? OnGetStatusMenuApproval()
        {
            // Disable diset menggunakan ToolBar.ApprovalEnabled = false 
            // Jika fungsi ini return false malah akan memunculkan image Stamp
            return true;
        }

        public override bool OnGetStatusMenuVoid()
        {
            // Disable diset menggunakan ToolBar.VoidEnabled = false 
            // Jika fungsi ini return false malah akan memunculkan image Void
            return true;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion




        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Menu hardcode
            ToolBar.ApprovalEnabled = false;
            ToolBar.VoidEnabled = false;
            ToolBar.PrintEnabled = false;

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "File Attachment of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }

            PaneEntry.Width = 500;
            btnBathUpload.Enabled = IsUserAddAble;
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
            {
                entity.IsUpload = true;
                entity.FileAttachName = string.Empty;
            }
        }

        private long SaveEntity(PatientDocument entity)
        {
            entity.Save();
            return entity.PatientDocumentID ?? 0;
        }

        private string UploadFile(PatientDocument entity)
        {
            if (uplFileTemplate.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile validFile in uplFileTemplate.UploadedFiles)
                {
                    var fileName = string.Format("{0:000000000000000}_{1}", entity.PatientDocumentID, validFile.GetName());
                    entity.FileAttachName = fileName;
                    entity.OriFileName = validFile.GetName();

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

                    var pathFile = Path.Combine(targetFolder, fileName);
                    validFile.SaveAs(pathFile, true);
                    return pathFile;
                }
            }

            return string.Empty;
        }

        protected void OnCheckedChanged(object sender, EventArgs e)
        {
            grdDocument.Rebind();
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdDocument.Rebind();
        }
    }
}

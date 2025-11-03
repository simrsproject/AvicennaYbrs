using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class PatientDocumentWebCam : BasePageDialogEntry
    {
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

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["progid"]))
            {
                ProgramID = Request.QueryString["progid"];
            }
            else
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Menu hardcode
            IsSingleRecordMode = true;

            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "File Attachment of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                    hdnMedicalNo.Value = pat.MedicalNo;
                }
            }
        }

        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var pd = new PatientDocument();
            pd.LoadByPrimaryKey(PatientDocumentID);
            PopulateEntryControl(pd);
        }
        protected void PopulateEntryControl(esEntity entity)
        {
            var pd = (PatientDocument)entity;
            txtDocumentName.Text = pd.DocumentName;
            txtDocumentDate.SelectedDate = pd.DocumentDate;
            txtNotes.Text = pd.Notes;
            txtReferenceNo.Text = pd.ReferenceNo;
            PatientDocumentID = pd.PatientDocumentID ?? 0;
        }
        protected override void OnMenuNewClick()
        {
            PatientDocumentID = 0;

            var ent = new PatientDocument();
            ent.RegistrationNo = RegistrationNo;
            ent.PatientID = PatientID;

            PopulateEntryControl(ent);

            if (!string.IsNullOrWhiteSpace(Request.QueryString["surgno"]))
                txtReferenceNo.Text = Request.QueryString["surgno"].ToString();

            txtDocumentDate.SelectedDate = (new DateTime()).NowAtSqlServer();
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new PatientDocument();
            SetEntityValue(entity);
            var newID = SaveEntity(entity);
            PatientDocumentID = newID;

            // Save thumbnail & Image File
            if (!string.IsNullOrWhiteSpace(hdnImgData.Value))
            {
                // Contoh data 
                //  - dari JCrop  -> data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD...
                //  - dari CropIt -> data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAA...

                var dataImage = (new ImageHelper()).ConvertBase64StringToImage(hdnImgData.Value.Split(',')[1]);

                // Save File
                var fileName = string.Format("{0:000000000000000}_{1}_{2}.jpg", entity.PatientDocumentID, DateTime.Now.ToString("yyMMdd"), hdnMedicalNo.Value);
                entity.FileAttachName = fileName;

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

                // Resize, Compres and Save File -> 140KB
                var width = AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamWidth).ToInt();
                var heigth = AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamHeight).ToInt();

                var imgHelper = new ImageHelper();
                var img = imgHelper.ResizeImage(dataImage, new Size(width, heigth), true, InterpolationMode.High);
                var compression = AppParameter.GetParameterValue(AppParameter.ParameterItem.PatientDocumentScanCompression).ToInt();
                imgHelper.Compress(img, pathFile, compression);

                // Save Thumbnail
                var smallImg = imgHelper.ResizeImage(dataImage, new Size(100, 100), true, InterpolationMode.Low);
                entity.SmallImage = imgHelper.CompressImageToArray(smallImg, 20); // 1KB from 14KB 
                entity.Save();
            }
            else
            {
                // Fail Upload
                // Delete 
                entity.MarkAsDeleted();
                entity.Save();
            }

        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            rowUploadFile.Visible = newVal == (AppEnum.DataMode.New);
        }
        protected override void OnMenuEditClick()
        {
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
        }

        private void SetEntityValue(PatientDocument entity)
        {
            entity.PatientID = PatientID;
            entity.RegistrationNo = RegistrationNo;
            entity.DocumentName = txtDocumentName.Text;
            entity.DocumentDate = txtDocumentDate.SelectedDate;
            entity.Notes = txtNotes.Text;
            entity.ReferenceNo = txtReferenceNo.Text;

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

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var ent = new PatientDocument();
            if (ent.LoadByPrimaryKey(PatientDocumentID))
            {
                // Rename File
                //var filePath = (ent.IsUpload ?? false) ?
                //System.IO.Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", ent.PatientID.Trim(), ent.FileAttachName)
                //: ent.OriPath;

                var filePath = "";
                if (ent.IsUpload ?? false)
                {
                    //filePath = System.IO.Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", ent.PatientID.Trim(), ent.FileAttachName);

                    var fileFolderOld = System.IO.Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", ent.PatientID.Trim());
                    var fileFolderYearly = "";
                    if (!string.IsNullOrEmpty(ent.DocumentFolderYearly))
                        fileFolderYearly = System.IO.Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocumentYearly", ent.DocumentFolderYearly, ent.PatientID.Trim());

                    var fileFolder = fileFolderOld;
                    if (!System.IO.Directory.Exists(fileFolder))
                    {
                        // jika old blm ada brarti pakai yearly
                        fileFolder = string.IsNullOrEmpty(fileFolderYearly) ? fileFolderOld : fileFolderYearly;
                    }

                    filePath = System.IO.Path.Combine(fileFolder, ent.FileAttachName);
                }
                else
                {
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
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

    }
}


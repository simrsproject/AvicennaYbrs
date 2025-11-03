using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.Module.HR.KEHRS.Document
{
    public partial class DocumentUpload : BasePageDialogEntry
    {
        protected long DocumentID
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
            ProgramID = AppConstant.Program.KEHRS_SafetyCultureIncidentReportsVerification;

            // Menu hardcode
            IsSingleRecordMode = true;

            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;

            if (!IsPostBack)
            {
                var rpt = new EmployeeSafetyCultureIncidentReports();
                if (rpt.LoadByPrimaryKey(Request.QueryString["pid"].ToString()))
                {
                    this.Title = "File Attachment of : " + rpt.TransactionNo + " [" + rpt.ReportDescription + "]";
                }
            }
        }


        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var pd = new EmployeeSafetyCultureIncidentReportsDocument();
            pd.LoadByPrimaryKey(DocumentID);
            PopulateEntryControl(pd);
        }
        protected void PopulateEntryControl(esEntity entity)
        {
            var pd = (EmployeeSafetyCultureIncidentReportsDocument)entity;
            txtDocumentName.Text = pd.DocumentName;
            txtFileAttachName.Text = pd.FileAttachName;
            txtDocumentDate.SelectedDate = pd.DocumentDate;
            txtNotes.Text = pd.Notes;
            DocumentID = pd.DocumentID ?? 0;
        }
        protected override void OnMenuNewClick()
        {
            DocumentID = 0;

            var ent = new EmployeeSafetyCultureIncidentReportsDocument();
            ent.TransactionNo = Request.QueryString["pid"].ToString();

            PopulateEntryControl(ent);

            txtDocumentDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            if (!string.IsNullOrEmpty(Request.QueryString["note"]))
                txtDocumentName.Text = Request.QueryString["note"].ToString();
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new EmployeeSafetyCultureIncidentReportsDocument();
            SetEntityValue(entity);
            var newID = SaveEntity(entity);
            DocumentID = newID;

            // Upload file
            var fullPathFileName = UploadFile(entity);

            // Save thumbnail
            if (!string.IsNullOrWhiteSpace(fullPathFileName))
            {
                if (entity.OriFileName.Contains(".jpg") || entity.OriFileName.Contains(".jpeg") || entity.OriFileName.Contains(".png"))
                {
                    var imgHelper = new ImageHelper();
                    var imgArr = imgHelper.LoadImageToArray(fullPathFileName);
                    var smallImg = imgHelper.ResizeImage(imgArr, new Size(100, 100), true, InterpolationMode.Low);

                    if (entity.OriFileName.Contains(".jpg") || entity.OriFileName.Contains(".jpeg"))
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
            var entity = new EmployeeSafetyCultureIncidentReportsDocument();
            if (entity.LoadByPrimaryKey(DocumentID))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        private void SetEntityValue(EmployeeSafetyCultureIncidentReportsDocument entity)
        {
            entity.TransactionNo = Request.QueryString["pid"].ToString();
            entity.DocumentName = txtDocumentName.Text;
            entity.DocumentDate = txtDocumentDate.SelectedDate;
            entity.Notes = txtNotes.Text;

            if (entity.es.IsAdded)
            {
                entity.IsUpload = true;
                entity.FileAttachName = string.Empty;
            }
        }

        private long SaveEntity(EmployeeSafetyCultureIncidentReportsDocument entity)
        {
            entity.Save();
            return entity.DocumentID ?? 0;
        }

        private string UploadFile(EmployeeSafetyCultureIncidentReportsDocument entity)
        {
            if (uplFileTemplate.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile validFile in uplFileTemplate.UploadedFiles)
                {
                    var fileName = string.Format("{0:000000000000000}_{1}", entity.DocumentID, validFile.GetName());
                    entity.FileAttachName = fileName;
                    entity.OriFileName = validFile.GetName();

                    var targetFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "KehrsReportsDocument", entity.TransactionNo.Trim()); if (!System.IO.Directory.Exists(targetFolder))
                        System.IO.Directory.CreateDirectory(targetFolder);

                    var pathFile = Path.Combine(targetFolder, fileName);
                    validFile.SaveAs(pathFile, true);
                    return pathFile;
                }
            }

            return string.Empty;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var ent = new EmployeeSafetyCultureIncidentReportsDocument();
            if (ent.LoadByPrimaryKey(DocumentID))
            {
                // Rename File
                var filePath = (ent.IsUpload ?? false) ?
                System.IO.Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "KehrsReportsDocument", ent.TransactionNo.ToString(), ent.FileAttachName)
                : ent.OriPath;

                if (System.IO.File.Exists(filePath))
                {
                    var newFilePath = Path.Combine(System.IO.Path.GetDirectoryName(filePath), "DEL_" + System.IO.Path.GetFileName(filePath));

                    File.Move(filePath, newFilePath);
                }

                ent.IsDeleted = true;
                ent.Save();
                DocumentID = 0;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }
    }
}
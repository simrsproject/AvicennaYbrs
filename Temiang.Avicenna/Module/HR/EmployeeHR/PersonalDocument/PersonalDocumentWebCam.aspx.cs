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

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalDocumentWebCam : BasePageDialogEntry
    {
        protected long PersonalDocumentID
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
            ProgramID = Request.QueryString["pageId"].ToString() == "log" ? AppConstant.Program.EmployeeLogbook : (Request.QueryString["pageId"].ToString() == "epi" ? AppConstant.Program.PersonalInfo : AppConstant.Program.EmployeeWorkingInfo);

            // Menu hardcode
            IsSingleRecordMode = true;

            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;

            if (!IsPostBack)
            {
                var emps = new VwEmployeeTable();
                emps.Query.Where(emps.Query.PersonID == Request.QueryString["pid"].ToInt());
                if (emps.Query.Load())
                {
                    this.Title = "File Attachment of : " + emps.EmployeeName + " [" + emps.EmployeeNumber + "]";
                    hdnEmployeeNo.Value = emps.EmployeeNumber;
                }
            }
        }

        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var pd = new PersonalDocument();
            pd.LoadByPrimaryKey(PersonalDocumentID);
            PopulateEntryControl(pd);
        }
        protected void PopulateEntryControl(esEntity entity)
        {
            var pd = (PersonalDocument)entity;
            txtDocumentName.Text = pd.DocumentName;
            txtDocumentDate.SelectedDate = pd.DocumentDate;
            txtNotes.Text = pd.Notes;
            PersonalDocumentID = pd.PersonalDocumentID ?? 0;
        }
        protected override void OnMenuNewClick()
        {
            PersonalDocumentID = 0;

            var ent = new PersonalDocument();
            ent.PersonID = Request.QueryString["pid"].ToInt();

            PopulateEntryControl(ent);

            txtDocumentDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            if (!string.IsNullOrEmpty(Request.QueryString["note"]))
                txtDocumentName.Text = Request.QueryString["note"].ToString();
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new PersonalDocument();
            SetEntityValue(entity);
            var newID = SaveEntity(entity);
            PersonalDocumentID = newID;

            // Save thumbnail & Image File
            if (!string.IsNullOrWhiteSpace(hdnImgData.Value))
            {
                // Contoh data 
                //  - dari JCrop  -> data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD...
                //  - dari CropIt -> data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAA...

                var dataImage = (new ImageHelper()).ConvertBase64StringToImage(hdnImgData.Value.Split(',')[1]);

                // Save File
                var fileName = string.Format("{0:000000000000000}_{1}.jpg", entity.PersonalDocumentID, hdnEmployeeNo.Value);
                entity.FileAttachName = fileName;

                var targetFolder = Path.Combine(AppSession.Parameter.EmployeeDocumentFolder, "PersonalDocument", entity.PersonID.ToString().Trim());
                if (!System.IO.Directory.Exists(targetFolder))
                    System.IO.Directory.CreateDirectory(targetFolder);

                var pathFile = Path.Combine(targetFolder, fileName);

                // Resize, Compres and Save File -> 140KB
                var width = AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamWidth).ToInt();
                var heigth = AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamHeight).ToInt();
                var imgHelper = new ImageHelper();
                var img = imgHelper.ResizeImage(dataImage, new Size(width, heigth), true, InterpolationMode.High);
                imgHelper.Compress(img, pathFile, 200);

                // Save Thumbnail
                var smallImg = imgHelper.ResizeImage(dataImage, new Size(100, 100), true, InterpolationMode.Low);
                entity.SmallImage = imgHelper.CompressImageToArray(smallImg,20); // 1KB from 14KB 
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
            var entity = new PersonalDocument();
            if (entity.LoadByPrimaryKey(PersonalDocumentID))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        private void SetEntityValue(PersonalDocument entity)
        {
            entity.PersonID = Request.QueryString["pid"].ToInt();
            entity.DocumentName = txtDocumentName.Text;
            entity.DocumentDate = txtDocumentDate.SelectedDate;
            entity.Notes = txtNotes.Text;
            entity.DocumentCode = Request.QueryString["dc"].ToString();
            entity.RefferenceID = Request.QueryString["rid"].ToInt();

            if (entity.es.IsAdded)
            {
                entity.IsUpload = true;
                entity.FileAttachName = string.Empty;
            }
        }

        private long SaveEntity(PersonalDocument entity)
        {
            entity.Save();
            return entity.PersonalDocumentID ?? 0;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var ent = new PersonalDocument();
            if (ent.LoadByPrimaryKey(PersonalDocumentID))
            {
                // Rename File
                var filePath = (ent.IsUpload ?? false) ?
                System.IO.Path.Combine(AppSession.Parameter.EmployeeDocumentFolder, "PersonalDocument", ent.PersonID.ToString(), ent.FileAttachName)
                : ent.OriPath;

                if (System.IO.File.Exists(filePath))
                {
                    var newFilePath = Path.Combine(System.IO.Path.GetDirectoryName(filePath), "DEL_" + System.IO.Path.GetFileName(filePath));

                    File.Move(filePath, newFilePath);
                }

                ent.IsDeleted = true;
                ent.Save();
                PersonalDocumentID = 0;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }
    }
}
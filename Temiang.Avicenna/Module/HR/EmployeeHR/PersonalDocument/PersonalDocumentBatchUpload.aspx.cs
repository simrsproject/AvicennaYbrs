using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalDocumentBatchUpload : BasePageDialog
    {
        private string QsPageId
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["pageId"]) ? string.Empty : Request.QueryString["pageId"];
            }
        }

        private DataTable UploadResult
        {
            get
            {
                if (ViewState["ur"] == null)
                {
                    var dtb = new DataTable();
                    dtb.Columns.Add("FileName", typeof(string));
                    dtb.Columns.Add("PersonID", typeof(int));
                    dtb.Columns.Add("EmployeeNo", typeof(string));
                    dtb.Columns.Add("EmployeeName", typeof(string));
                    dtb.Columns.Add("FileDate", typeof(DateTime));
                    dtb.Columns.Add("ErrorMessage", typeof(string));
                    ViewState["ur"] = dtb;
                }
                return (DataTable)ViewState["ur"];
            }
            set
            {
                ViewState["ur"] = value;
            }
        }
        public int PersonalDocumentID
        {
            get
            {
                return Request.QueryString["id"].ToInt();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            switch (QsPageId)
            {
                case "epi":
                    ProgramID = AppConstant.Program.PersonalInfo;
                    break;
                case "ewi":
                    ProgramID = AppConstant.Program.EmployeeWorkingInfo;
                    break;

                case "gen":
                    ProgramID = AppConstant.Program.EmployeeLogbook;
                    break;
                case "c01":
                    ProgramID = AppConstant.Program.EmployeeLogbookMedicalCommitte;
                    break;
                case "c02":
                    ProgramID = AppConstant.Program.EmployeeLogbookNursingCommitte;
                    break;
                case "c03":
                    ProgramID = AppConstant.Program.EmployeeLogbookKtkl;
                    break;

                default:
                    ProgramID = AppConstant.Program.EmployeeWorkingInfo;
                    break;
            }

            if (!IsPostBack)
                this.Title = "Employee Document Batch Upload";
        }

        protected override void OnButtonOkClicked(ValidateArgs args)
        {
            UploadFile();
            ButtonOk.Visible = false;
            ButtonCancel.Text = "Close";
            args.MessageText = "Please check upload result";
            args.IsCancel = true;

            pnlUpload.Visible = false;
            pnlResult.Visible = true;
            grdResult.DataSource = UploadResult;
            grdResult.DataBind();
        }

        private string UploadFile()
        {
            var dtbResult = UploadResult;
            string notUploaded = string.Empty;
            if (uplFile.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile validFile in uplFile.UploadedFiles)
                {
                    var rowResult = dtbResult.NewRow();
                    var fileName = validFile.GetName();

                    rowResult["FileName"] = validFile.FileName;

                    if (!fileName.Contains('_'))
                    {
                        rowResult["ErrorMessage"] = "File name not valid";
                        dtbResult.Rows.Add(rowResult);
                        continue;
                    }

                    var fileNames = fileName.Split('.')[0].Split('_');

                    if (fileNames.Length < 2)
                    {
                        rowResult["ErrorMessage"] = "File name not valid";
                        dtbResult.Rows.Add(rowResult);
                        continue;
                    }

                    // Cek file sudah diupload
                    var qr = new PersonalDocumentQuery();
                    var entity = new PersonalDocument();
                    qr.Where(qr.OriFileName == fileName, qr.Or(qr.IsDeleted.IsNull(), qr.IsDeleted == false));
                    qr.es.Top = 1;

                    if (entity.Load(qr))
                    {
                        rowResult["ErrorMessage"] = "File has registered";
                        dtbResult.Rows.Add(rowResult);
                        continue;
                    }

                    rowResult["FileDate"] = DateTime.Now;

                    var empNo = fileNames[0];

                    var personal = new VwEmployeeTable();
                    personal.Query.Where(personal.Query.EmployeeNumber == empNo);
                    if (!personal.Query.Load())
                    {
                        rowResult["ErrorMessage"] = string.Format("Employee No: {0} not found", empNo);
                        dtbResult.Rows.Add(rowResult);
                        continue;
                    }

                    rowResult["EmployeeNo"] = personal.EmployeeNumber;
                    rowResult["EmployeeName"] = personal.EmployeeName;

                    entity = new PersonalDocument();
                    entity.PersonID = personal.PersonID;
                    entity.DocumentName = fileNames.Length > 2 ? fileNames[2] : fileName;
                    entity.DocumentDate = DateTime.Now;
                    entity.Notes = string.Empty;
                    entity.FileAttachName = string.Empty;
                    entity.OriFileName = fileName;
                    entity.IsUpload = true; // Belum bisa diterapkan jika tidak diupload krn belum dapat cara mendapatkan path file sumbernya
                    entity.DocumentCode = "00";
                    entity.RefferenceID = 0;
                    // Save untuk mendapatkan identity (PatientDocumentID)
                    entity.Save();

                    // Create Thumbnail
                    var oriFileName = entity.OriFileName.ToLower();

                    if (oriFileName.Contains(".jpg") || oriFileName.Contains(".jpg") || oriFileName.Contains(".png"))
                    {

                        // when we cast the stream, we need to dispose in order to be able to manipulate the file
                        // otherwise, "The file is being used from another process" error will appear
                        using (var filestream = validFile.InputStream as System.IO.FileStream)
                        {
                            var imgByteArr = new byte[filestream.Length];
                            //Read data from the file stream and put into the byte array
                            filestream.Read(imgByteArr, 0, Convert.ToInt32(filestream.Length));
                            var imgHelper = new ImageHelper();
                            var smallImg = imgHelper.ResizeImage(imgByteArr, new Size(100, 100), true, InterpolationMode.Low);

                            if (entity.OriFileName.Contains(".jpg") || entity.OriFileName.Contains(".jpg"))
                                entity.SmallImage = imgHelper.ConvertImageToByteArray(smallImg, ImageFormat.Jpeg);
                            else if (entity.OriFileName.Contains(".png"))
                                entity.SmallImage = imgHelper.ConvertImageToByteArray(smallImg, ImageFormat.Png);

                            filestream.Close();
                        }
                    }

                    // Save File Name
                    fileName = string.Format("{0:000000000000000}_{1}", entity.PersonalDocumentID,
                        validFile.GetName());
                    entity.FileAttachName = fileName;

                    // Save File
                    var targetFolder = Path.Combine(AppSession.Parameter.EmployeeDocumentFolder, "PersonalDocument", entity.PersonID.ToString().Trim());

                    // If access denied read -> 
                    // https://www.codeproject.com/Articles/19830/How-to-Access-Network-Files-using-asp-net
                    if (!System.IO.Directory.Exists(targetFolder))
                        System.IO.Directory.CreateDirectory(targetFolder);

                    var fullPathFileName = Path.Combine(targetFolder, fileName);
                    validFile.SaveAs(fullPathFileName, true);

                    entity.Save();
                    dtbResult.Rows.Add(rowResult);
                }

                UploadResult = dtbResult;
            }

            return string.Empty;
        }
    }
}
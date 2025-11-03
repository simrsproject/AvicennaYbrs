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

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class PatientDocumentBatchUpload : BasePageDialog
    {
        private DataTable UploadResult
        {
            get
            {
                if (ViewState["ur"] == null)
                {
                    var dtb = new DataTable();
                    dtb.Columns.Add("FileName", typeof(string));
                    dtb.Columns.Add("PatientName", typeof(string));
                    dtb.Columns.Add("MedicalNo", typeof(string));
                    dtb.Columns.Add("RegistrationNo", typeof(string));
                    dtb.Columns.Add("RegistrationDate", typeof(DateTime));
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
        public int PatientDocumentID
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
            if (!string.IsNullOrWhiteSpace(Request.QueryString["progid"]))
            {
                ProgramID = Request.QueryString["progid"];
            }
            else
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;
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

                    if (validFile.ContentLength == 0)// || validFile.InputStream == null || validFile.InputStream.Length == 0)
                    {
                        rowResult["FileName"] = validFile.FileName;
                        rowResult["ErrorMessage"] = "Uploaded file is empty or corrupt";
                        dtbResult.Rows.Add(rowResult);
                        continue;
                    }

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
                    var qr = new PatientDocumentQuery();
                    var entity = new PatientDocument();
                    qr.Where(qr.OriFileName == fileName, qr.Or(qr.IsDeleted.IsNull(), qr.IsDeleted == false));
                    qr.es.Top = 1;

                    if (entity.Load(qr))
                    {
                        rowResult["ErrorMessage"] = "File has registered";
                        dtbResult.Rows.Add(rowResult);
                        continue;
                    }

                    // nama file -> Tanggal_MRN_NoUrut -> 010313_2007001961_01
                    var dateInfo = fileNames[0];
                    var docDate = DateTime.Today;
                    if (dateInfo.Length == 6)
                    {
                        // Format bawaan RS Immanuel
                        docDate = new DateTime(2000 + dateInfo.Substring(4, 2).ToInt(),
                            dateInfo.Substring(2, 2).ToInt(), dateInfo.Substring(0, 2).ToInt());
                    }
                    else if (dateInfo.Length == 8)
                    {
                        docDate = new DateTime(dateInfo.Substring(4, 4).ToInt(),
                            dateInfo.Substring(2, 2).ToInt(), dateInfo.Substring(0, 2).ToInt());
                    }
                    else
                    {
                        rowResult["ErrorMessage"] = "Date section in file name not valid ";
                        dtbResult.Rows.Add(rowResult);
                        continue;
                    }
                    rowResult["FileDate"] = docDate;

                    var medNo = fileNames[1];

                    var patient = new Patient();
                    if (chkIsUseOldMedicalNo.Checked)
                    {
                        patient.Query.Where(patient.Query.MedicalNo == medNo);
                        patient.Query.es.Top = 1;
                        if (!patient.Query.Load())
                        {
                            rowResult["ErrorMessage"] = string.Format("Old Medical Record No: {0} not found", medNo);
                            dtbResult.Rows.Add(rowResult);
                            continue;
                        }
                    }
                    else
                    if (!patient.LoadByMedicalNo(medNo))
                    {
                        rowResult["ErrorMessage"] = string.Format("Medical Record No: {0} not found", medNo);
                        dtbResult.Rows.Add(rowResult);
                        continue;
                    }

                    rowResult["MedicalNo"] = patient.MedicalNo;
                    rowResult["PatientName"] = patient.PatientName;

                    // Find Last Reg
                    var lastRegNo = string.Empty;
                    var reg = new Registration();
                    reg.Query.Where(reg.Query.PatientID == patient.PatientID, reg.Query.RegistrationDate <= docDate);
                    reg.Query.OrderBy(reg.Query.RegistrationDate.Descending, reg.Query.RegistrationTime.Descending);
                    reg.Query.es.Top = 1;

                    if (reg.Query.Load())
                    {
                        lastRegNo = reg.RegistrationNo;
                        rowResult["RegistrationNo"] = reg.RegistrationNo;
                        rowResult["RegistrationDate"] = reg.RegistrationDate;
                    }

                    //var lastReg = Patient.Last.Registration(patient.PatientID);
                    //var lastRegNo = lastReg == null ? string.Empty : lastReg.RegistrationNo;

                    entity = new PatientDocument();
                    entity.PatientID = patient.PatientID;
                    entity.RegistrationNo = lastRegNo;
                    entity.DocumentName = fileNames.Length > 3 ? fileNames[3] : fileName;
                    entity.DocumentDate = docDate;
                    entity.Notes = string.Empty;
                    entity.FileAttachName = string.Empty;
                    entity.OriFileName = fileName;
                    entity.IsUpload = true; // Belum bisa diterapkan jika tidak diupload krn belum dapat cara mendapatkan path file sumbernya
                    // Save untuk mendapatkan identity (PatientDocumentID)
                    entity.Save();

                    // Create Thumbnail
                    var oriFileName = entity.OriFileName.ToLower();

                    if (oriFileName.Contains(".jpg") || oriFileName.Contains(".jpg") || oriFileName.Contains(".png"))
                    {
                        try
                        {
                            // when we cast the stream, we need to dispose in order to be able to manipulate the file
                            // otherwise, "The file is being used from another process" error will appear
                            using (var filestream = validFile.InputStream as System.IO.FileStream)
                            {
                                var imgByteArr = new byte[filestream.Length];
                                //Read data from the file stream and put into the byte array
                                filestream.Read(imgByteArr, 0, Convert.ToInt32(filestream.Length));

                                //var smallImg = ImageHelper.ResizeImage(imgByteArr, new Size(100, 100), true, InterpolationMode.Low);
                                var smallImg = ResizeImage(imgByteArr, new Size(100, 100), true, InterpolationMode.Low);

                                if (entity.OriFileName.Contains(".jpg") || entity.OriFileName.Contains(".jpg"))
                                {
                                    //entity.SmallImage = ImageHelper.ConvertImageToByteArray(smallImg, ImageFormat.Jpeg);
                                    entity.SmallImage = ConvertImageToByteArray(smallImg, ImageFormat.Jpeg);
                                }
                                else if (entity.OriFileName.Contains(".png"))
                                {  //entity.SmallImage = ImageHelper.ConvertImageToByteArray(smallImg, ImageFormat.Png);
                                    entity.SmallImage = ConvertImageToByteArray(smallImg, ImageFormat.Png);
                                }

                                filestream.Close();
                            }
                        }
                        catch (Exception ex) { 
                            // error bisa terjadi kalau image gak valid atau corrupt, skip saja tidak usah bikin SmallImage
                        }
                    }

                    // Save File Name
                    fileName = string.Format("{0:000000000000000}_{1}", entity.PatientDocumentID,
                        validFile.GetName());
                    entity.FileAttachName = fileName;

                    //// Save File
                    //var targetFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", entity.PatientID.Trim());

                    //// If access denied read -> 
                    //// https://www.codeproject.com/Articles/19830/How-to-Access-Network-Files-using-asp-net
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

                    var fullPathFileName = Path.Combine(targetFolder, fileName);
                    validFile.SaveAs(fullPathFileName, true);

                    entity.Save();
                    dtbResult.Rows.Add(rowResult);
                }

                UploadResult = dtbResult;
            }

            return string.Empty;
        }

        #region Resize Image
        // Coba menggunakan non static method untuk testing apakah tidak terjadi lagi out off memory
        private byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert,
                               System.Drawing.Imaging.ImageFormat formatOfImage)
        {
            byte[] ret;
            try
            {
                using (var ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    ret = ms.ToArray();
                }
            }
            catch (Exception) { throw; }
            return ret;
        }
        private System.Drawing.Image ConvertByteArrayToImage(byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            var returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }
        private System.Drawing.Image ResizeImage(byte[] image, Size size, bool preserveAspectRatio, InterpolationMode interpolationMode)
        {
            return ResizeImage(ConvertByteArrayToImage(image), size, preserveAspectRatio, interpolationMode);
        }
        private System.Drawing.Image ResizeImage(System.Drawing.Image image, Size size, bool preserveAspectRatio, InterpolationMode interpolationMode)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            var newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = interpolationMode;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }
        #endregion

    }
}
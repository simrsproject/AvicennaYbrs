using System;
using System.IO;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Web.Services;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web.UI;

namespace Temiang.Avicenna.Module.RADT
{
    [System.Web.Script.Services.ScriptService]
    public partial class WebCamCaptureDocument : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public virtual string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        [WebMethod()]
        public static string SaveToFile(string data)
        {
            //data = data.Replace("imgBase64=data:image/png;base64,", String.Empty);
            data = data.Replace("data:image/png;base64,", String.Empty);

            var arr = data.Split('|');
            string imgBase64 = arr[0];
            string regNo = arr[1];
            string dfId = arr[2];
            var docExistCount = 0;
            try
            {
                var datas = Convert.FromBase64String(imgBase64);
                var df = new DocumentFiles();
                df.LoadByPrimaryKey(dfId.ToInt());

                string regType = null;
                string guarantorID = null;
                string programCategory = "005"; // Dokumen Penunjang
                var filePath = Reports.ReportViewer.GuarantorDocumentFilePath(regNo,string.Empty, df.DocumentNumber.Trim(), "jpg", ref regType, ref guarantorID, string.Empty);
                var path = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                // Resize, COmpres and Save File
                var width = AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamWidth).ToInt();
                var heigth = AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamHeight).ToInt();

                var imgHelper = new ImageHelper();
                var img = imgHelper.ResizeImage(imgHelper.ConvertByteArrayToImage(datas), new Size(width, heigth), true, InterpolationMode.High);
                var compression = AppParameter.GetParameterValue(AppParameter.ParameterItem.PatientDocumentScanCompression).ToInt();
                imgHelper.Compress(img, filePath, compression);

                // Save Checklist
                var rdcl = new BusinessObject.RegistrationDocumentCheckList();
                if (!rdcl.LoadByPrimaryKey(regNo, dfId.ToInt())) rdcl = new BusinessObject.RegistrationDocumentCheckList();

                rdcl.RegistrationNo = regNo;
                rdcl.DocumentFilesID = dfId.ToInt();
                rdcl.LastUpdateByUserID = AppSession.UserLogin.UserID;
                rdcl.LastUpdateDateTime = DateTime.Now;
                rdcl.FileName = filePath;
                rdcl.Save();

                // Jumlah document yg dianggap sudah ada
                var rdclColl = new RegistrationDocumentCheckListCollection();
                rdclColl.Query.Where(rdclColl.Query.RegistrationNo == regNo);
                rdclColl.LoadAll();
                docExistCount = rdclColl.Count;

                var regInfoCount = new RegistrationInfoSumary();
                if (!regInfoCount.LoadByPrimaryKey(regNo))
                {
                    regInfoCount.AddNew();
                    regInfoCount.RegistrationNo = regNo;
                    regInfoCount.NoteCount = 0;
                    regInfoCount.NoteMedicalCount = 0;
                }
                regInfoCount.DocumentCheckListCount = docExistCount;
                regInfoCount.Save();
            }
            catch (Exception ex)
            {
                //throw;
            }
            return docExistCount.ToString();
        }
    }
}
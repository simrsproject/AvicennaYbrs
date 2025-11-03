using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    /// <summary>
    /// Summary description for Localist
    /// </summary>
    public class ImageHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            byte[] img = new byte[1];

            var mode = string.IsNullOrEmpty(context.Request.QueryString["mode"]) ? "empdoc" : context.Request.QueryString["mode"];

            if (mode == "empdoc")
            {
                var ent = new PersonalDocument();
                if (ent.LoadByPrimaryKey(context.Request.QueryString["id"].ToInt()))
                {
                    var filePath = (ent.IsUpload ?? false) ?
                    System.IO.Path.Combine(AppSession.Parameter.EmployeeDocumentFolder, "PersonalDocument", ent.PersonID.ToString(), ent.FileAttachName)
                    : ent.OriPath;
                    img = System.IO.File.ReadAllBytes(filePath);
                }
                else
                {
                    // Blank Imgae
                    var bitmap = new Bitmap(1, 1);
                    img = (new ImageHelper()).ToByteArray(bitmap, ImageFormat.Png);
                }
            }
            //else
            //{
            //    var ent = new PatientDocument();
            //    if (ent.LoadByPrimaryKey(context.Request.QueryString["id"].ToInt()))
            //    {
            //        var filePath = (ent.IsUpload ?? false) ?
            //        System.IO.Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", ent.PatientID.Trim(), ent.FileAttachName)
            //        : ent.OriPath;
            //        img = System.IO.File.ReadAllBytes(filePath);
            //    }
            //    else
            //    {
            //        // Blank Imgae
            //        var bitmap = new Bitmap(1, 1);
            //        img = (new ImageHelper()).ToByteArray(bitmap, ImageFormat.Png);
            //    }
            //}
            context.Response.ContentType = "image/png";
            context.Response.OutputStream.Write(img, 0, img.Length);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
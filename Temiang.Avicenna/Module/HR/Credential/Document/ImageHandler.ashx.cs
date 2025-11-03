using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Credential.Document
{
    /// <summary>
    /// Summary description for Localist
    /// </summary>
    public class ImageHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            byte[] img = new byte[1];

            var ent = new CredentialProcessDocumentUpload();
            if (ent.LoadByPrimaryKey(context.Request.QueryString["id"].ToInt()))
            {
                var filePath = (ent.IsUpload ?? false) ?
                System.IO.Path.Combine(AppSession.Parameter.EmployeeDocumentFolder, "CredentialingDocument", ent.TransactionNo.ToString(), ent.FileAttachName)
                : ent.OriPath;
                img = System.IO.File.ReadAllBytes(filePath);
            }
            else
            {
                // Blank Imgae
                var bitmap = new Bitmap(1, 1);
                img = (new ImageHelper()).ToByteArray(bitmap,ImageFormat.Png);
            }
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
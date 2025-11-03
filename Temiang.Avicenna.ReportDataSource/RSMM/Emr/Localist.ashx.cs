using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportDataSource.RSMM.Emr
{
    /// <summary>
    /// Summary description for Localist
    /// </summary>
    public class Localist : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string rimid = context.Request.QueryString["rimid"]; 
            string bdid = context.Request.QueryString["bdid"]; 
            byte[] img = new byte[1];
            var bd = new RegistrationInfoMedicBodyDiagram();
            //bd.LoadByPrimaryKey("RIM/2019/12/10/0002", "PLOP");
            if (bd.LoadByPrimaryKey(rimid, bdid))
            {
                img = bd.BodyImage;
            }
            else
            {
                // Load BodyImage
                var bdm = new BodyDiagram();
                if (bdm.LoadByPrimaryKey(bdid))
                {
                    img = bdm.BodyImage;
                }
                else
                {
                    // Blank Imgae
                    var bitmap = new Bitmap(1, 1);
                    var imgHelper = new ImageHelper();
                    img = imgHelper.ToByteArray( bitmap,ImageFormat.Png);
                }
            }


            context.Response.ContentType = "image/png";
            context.Response.OutputStream.Write(img,0, img.Length);
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
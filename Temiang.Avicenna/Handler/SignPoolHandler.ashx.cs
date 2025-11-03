using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Handler
{
    /// <summary>
    /// Summary description for SignPoolHandler
    /// </summary>
    public class SignPoolHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            byte[] img = SignImage(context);
            context.Response.ContentType = "image/png";
            context.Response.OutputStream.Write(img, 0, img.Length);
        }

        private static byte[] SignImage(HttpContext context)
        {
            var signid = context.Request.QueryString["signid"].ToInt();

            byte[] img = new byte[1];
            var sign = new SignPool();
            if (sign.LoadByPrimaryKey(signid))
            {
                return sign.SignImg;
            }

            return img;
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
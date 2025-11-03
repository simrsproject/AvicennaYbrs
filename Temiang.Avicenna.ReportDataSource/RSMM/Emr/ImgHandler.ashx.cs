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
    public class ImgHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            byte[] img = new byte[1];

            string type = context.Request.QueryString["type"];
            switch (type)
            {
                case "localist":
                    img = Localist(context);
                    break;
                case "mdsppasign":
                    img = MedicalDischargeSummaryPpaSign(context);
                    break;
                case "mdspatientsign":
                    img = MedicalDischargeSummaryPatientSign(context);
                    break;
                default:
                    break;
            }

            context.Response.ContentType = "image/png";
            context.Response.OutputStream.Write(img, 0, img.Length);
        }

        private static byte[] Localist(HttpContext context)
        {
            string rimid = context.Request.QueryString["rimid"];
            string bdid = context.Request.QueryString["bdid"];

            byte[] img;
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
                    img = BlankImage();
                }
            }

            return img;
        }

        private static byte[] MedicalDischargeSummaryPpaSign(HttpContext context)
        {
            string regno = context.Request.QueryString["regno"];
            string csmix = context.Request.QueryString["csmix"];

            byte[] img;
            var mds = new MedicalDischargeSummary();
            if ("1".Equals(csmix))
                mds.Query.es.QuerySource = "MedicalDischargeSummaryCmx";

            if (mds.LoadByPrimaryKey(regno) && mds.PpaSign != null)
            {
                img = mds.PpaSign;
            }
            else
            {
                img = BlankImage();
            }

            return img;
        }
        private static byte[] MedicalDischargeSummaryPatientSign(HttpContext context)
        {
            string regno = context.Request.QueryString["regno"];
            string csmix = context.Request.QueryString["csmix"];

            byte[] img;
            var mds = new MedicalDischargeSummary();
            if ("1".Equals(csmix))
                mds.Query.es.QuerySource = "MedicalDischargeSummaryCmx";

            if (mds.LoadByPrimaryKey(regno) && mds.PatientSign != null)
            {
                img = mds.PatientSign;
            }
            else
            {
                img = BlankImage();
            }

            return img;
        }

        private static byte[] BlankImage()
        {
            // Blank Imgae
            var bitmap = new Bitmap(1, 1);
            var imgHelper = new ImageHelper();
            return imgHelper.ToByteArray(bitmap,ImageFormat.Png);

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
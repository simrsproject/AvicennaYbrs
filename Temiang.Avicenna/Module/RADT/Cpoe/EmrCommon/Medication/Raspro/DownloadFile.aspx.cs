using System;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.Services;
using System.IO;
using System.Net;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    [System.Web.Script.Services.ScriptService]

    public partial class DownloadFile : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Request.Form["id"];
            switch (id)
            {
                case "ppab":
                    DownloadPpab();
                    break;
                default:
                    break;
            }


        }

        private void DownloadPpab()
        {
            var urlFile = string.Format("{0}/App_Document/PPRA/PPAB.pdf", System.Configuration.ConfigurationManager.AppSettings.Get("ReportUrlLocation"));
            try
            {
                using (WebClient client = new WebClient())
                {
                    using (Stream ms = new MemoryStream(client.DownloadData(urlFile)))
                    {
                        MemoryStream download = new MemoryStream();
                        download.SetLength(ms.Length);
                        ms.Read(download.GetBuffer(), 0, (int)ms.Length);

                        var response = Response;
                        response.ContentType = "application/octet-stream";
                        response.AddHeader("Content-Disposition", "attachment; filename=\"PPAB.pdf\"");

                        // Write the file to the Response  
                        const int bufferLength = 10000;
                        byte[] buffer = new Byte[bufferLength];
                        int length = 0;
                        try
                        {
                            do
                            {
                                if (response.IsClientConnected)
                                {
                                    length = download.Read(buffer, 0, bufferLength);
                                    response.OutputStream.Write(buffer, 0, length);
                                    buffer = new Byte[bufferLength];
                                }
                                else
                                {
                                    length = -1;
                                }
                            }
                            while (length > 0);
                            response.Flush();
                            response.End();
                        }
                        finally
                        {
                            if (download != null)
                                download.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //nothing
            }
        }

    }
}

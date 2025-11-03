using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Xml;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for ePajak
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class ePajak : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string LoadPajak(string url)
        {
            var doc = new XmlDocument();
            doc.Load(url);

            using (TextReader sr = new StringReader(doc.InnerXml))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Common.Pajak.Metadata.resValidateFakturPm));
                Common.Pajak.Metadata.resValidateFakturPm response = (Common.Pajak.Metadata.resValidateFakturPm)serializer.Deserialize(sr);
                return response.nomorFaktur.ToString();
            }
        }

        public static XmlDocument Load(string url)
        {
            //url = "http://svc.efaktur.pajak.go.id/validasi/faktur/313593246407000/0031919942258/3031300D060960864801650304020105000420A550971B91246DF8198B45C330F0D70771F5C924F7CEB1498416DF6468F246A4";
            var doc = new XmlDocument();
            doc.Load(url);
            return doc;
        }

    }
}

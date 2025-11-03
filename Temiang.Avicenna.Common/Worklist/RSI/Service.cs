using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Common.Worklist.RSI
{
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    public class Service
    {
        public Json.Response.Root CreateJsonOrder(Json.Order.Root root)
        {
            string fileAndPath = ConfigurationManager.AppSettings["DocumentFolder"] + "order.json";
            if (File.Exists(fileAndPath)) File.Delete(fileAndPath);
            FileStream fs = new FileStream(fileAndPath, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.Write(JsonConvert.SerializeObject(root));
            sw.Flush();
            sw.Close();

            return SendJsonOrder(root);
        }

        public Json.Response.Root SendJsonOrder(Json.Order.Root root)
        {
            var webrequest = (HttpWebRequest)WebRequest.Create("http://10.15.10.1:8086/api/index.php");
            webrequest.Method = "POST";
            webrequest.ContentType = "application/json";

            byte[] formData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(root));
            webrequest.ContentLength = formData.Length;

            using (var post = webrequest.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            var response = webrequest.GetResponse() as HttpWebResponse;
            //if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));
            var sr = new StreamReader(response.GetResponseStream());
            return JsonConvert.DeserializeObject<Json.Response.Root>(sr.ReadToEnd());
        }

        private static Json.Result.Root _risPacsResults;
        public Json.Result.Hasil GetJsonOrder(Json.Order.Root root, bool isReload)
        {
            if (isReload || _risPacsResults == null)
            {
                var webrequest = (HttpWebRequest) WebRequest.Create("http://10.15.10.1:8086/api/index.php");
                webrequest.Method = "GET";
                webrequest.ContentType = "application/json";

                var response = webrequest.GetResponse() as HttpWebResponse;
                //if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));
                var sr = new StreamReader(response.GetResponseStream());
                _risPacsResults = JsonConvert.DeserializeObject<Json.Result.Root>(sr.ReadToEnd());
            }
            return _risPacsResults.data.hasil.Where(l => l.uid == root.uid).SingleOrDefault();
        }

        public string[] GetSpsLastCode()
        {
            var value = new string[2];

            var webrequest = (HttpWebRequest)WebRequest.Create("http://10.15.10.1:8086/api/mwl_item.php");
            webrequest.Method = "GET";

            var response = webrequest.GetResponse() as HttpWebResponse;
            if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));
            var sr = new StreamReader(response.GetResponseStream());
            var coll = JsonConvert.DeserializeObject<Json.Sps.Root>(sr.ReadToEnd());
            var hasil = coll.data.hasil;
            var entity = hasil.OrderByDescending(h => h.pk).Take(1).SingleOrDefault();
            if (entity != null)
            {
                var sps = entity.sps_id.Split('-');
                var spsNo = string.Empty;
                foreach (var c in sps[1].ToCharArray())
                {
                    if (!int.TryParse(c.ToString(), out int number)) continue;
                    spsNo += number.ToString();
                }
                spsNo = (spsNo.ToInt() + 1).ToString();
                value.SetValue(string.Format("{0}-xx{1}", sps[0], spsNo), 0);

                var rpi = entity.req_proc_id.Split('-');
                var rpiNo = string.Empty;
                foreach (var c in rpi[1].ToCharArray())
                {
                    if (!int.TryParse(c.ToString(), out int number)) continue;
                    rpiNo += number.ToString();
                }
                rpiNo = (rpiNo.ToInt() + 1).ToString();
                value.SetValue(string.Format("{0}-00{1}", rpi[0], rpiNo), 1);
            }
            return value;
        }

        public void CreateXmlWorklist(Xml.dataset ds)
        {
            string fileAndPath = ConfigurationManager.AppSettings["DocumentFolder"] + "worklist.xml";

            using (StringWriter textWriter = new Utf8StringWriter())
            {
                var serializer = new XmlSerializer(ds.GetType());
                serializer.Serialize(textWriter, ds);

                if (File.Exists(fileAndPath)) File.Delete(fileAndPath);
                FileStream fs = new FileStream(fileAndPath, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.Write(textWriter.ToString());
                sw.Flush();
                sw.Close();
            }

            //fileAndPath = ConfigurationManager.AppSettings["DocumentFolder"] + "intiwid_ris_pacs.bat";
            //if (!File.Exists(fileAndPath)) return;

            //System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("C:\\Windows\\System32\\cmd.exe");
            //psi.UseShellExecute = false;
            //psi.RedirectStandardOutput = true;
            //psi.RedirectStandardInput = true;
            //psi.RedirectStandardError = true;
            //psi.WorkingDirectory = ConfigurationManager.AppSettings["DocumentFolder"];

            //// Start the process
            //System.Diagnostics.Process proc = System.Diagnostics.Process.Start(psi);

            //// Open the batch file for reading
            //System.IO.StreamReader strm = System.IO.File.OpenText(fileAndPath);

            //// Attach the output for reading
            //System.IO.StreamReader sOut = proc.StandardOutput;

            //// Attach the in for writing
            //System.IO.StreamWriter sIn = proc.StandardInput;

            //// Write each line of the batch file to standard input
            //while (strm.Peek() != -1)
            //{
            //    sIn.WriteLine(strm.ReadLine());
            //}
            //strm.Close();

            //// Exit CMD.EXE
            //string stEchoFmt = "{0} run successfully. Exiting";

            //sIn.WriteLine(string.Format(stEchoFmt, fileAndPath));
            //sIn.WriteLine("EXIT");

            //// Close the process
            //proc.Close();

            //// Read the sOut to a string.
            //string results = sOut.ReadToEnd().Trim();

            //// Close the io Streams;
            //sIn.Close();
            //sOut.Close();
        }
    }
}

using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Common.Worklist.RSMMP
{
    public class Service
    {
        public Json.Order.Response.Root SendOrder(Json.Order.Request.Root root)
        {
            var user = Login(new Json.Login.Request.Root()
            {
                SiteId = 1,
                Code = "AVIAT",
                Key = "aviat"
            });
            if (user == null) return null;

            var webrequest = (HttpWebRequest)WebRequest.Create("http://192.168.110.200/empirisapi/exam/create");
            webrequest.Method = "POST";
            webrequest.ContentType = "application/json";
            webrequest.Headers.Add("Authorization", user.Response.FullToken);

            byte[] formData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(root));
            webrequest.ContentLength = formData.Length;

            using (var post = webrequest.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            var response = webrequest.GetResponse() as HttpWebResponse;
            if (response.StatusCode != HttpStatusCode.OK) return null;
            var sr = new StreamReader(response.GetResponseStream());
            return JsonConvert.DeserializeObject<Json.Order.Response.Root>(sr.ReadToEnd());
        }

        private Json.Login.Response.Root Login(Json.Login.Request.Root root)
        {
            var webrequest = (HttpWebRequest)WebRequest.Create("http://192.168.110.200/empirisapi/user/login");
            webrequest.Method = "POST";
            webrequest.ContentType = "application/json";

            byte[] formData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(root));
            webrequest.ContentLength = formData.Length;

            using (var post = webrequest.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            var response = webrequest.GetResponse() as HttpWebResponse;
            if (response.StatusCode != HttpStatusCode.OK) return null;
            var sr = new StreamReader(response.GetResponseStream());
            return JsonConvert.DeserializeObject<Json.Login.Response.Root>(sr.ReadToEnd());
        }

        public ImageStatus GetImageStatus(string transactionNo, string sequenceNo, string result)
        {
            if (string.IsNullOrEmpty(result)) return new ImageStatus()
            {
                Status = false,
                PacsStudyUid = result
            };

            var webrequest = (HttpWebRequest)WebRequest.Create($"http://192.168.110.200/empirispluginapi/exam/GetImageStatus/1/{transactionNo}@{sequenceNo}/{result}");
            webrequest.Method = "GET";
            webrequest.ContentType = "application/json";

            var response = webrequest.GetResponse() as HttpWebResponse;
            if (response.StatusCode != HttpStatusCode.OK) return new ImageStatus() { Status = false, PacsStudyUid = result };

            var sr = new StreamReader(response.GetResponseStream());
            var status = JsonConvert.DeserializeObject<Json.ImageStatus.Response.Root>(sr.ReadToEnd());
            if (status.StatusCode == (int)HttpStatusCode.OK)
            {
                var tci = new TransChargesItem();
                if (!tci.LoadByPrimaryKey(transactionNo, sequenceNo)) return new ImageStatus() { Status = false, PacsStudyUid = result };
                if (tci.ResultValue != status.PacsStudyUid)
                {
                    tci.ResultValue = status.PacsStudyUid;
                    tci.Save();
                }
                return new ImageStatus()
                {
                    Status = true,
                    PacsStudyUid = status.PacsStudyUid
                }; ;
            }
            return new ImageStatus()
            {
                Status = false,
                PacsStudyUid = result
            };
        }

        public class ImageStatus
        {
            public bool Status { get; set; }
            public string PacsStudyUid { get; set; }
        }
    }
}

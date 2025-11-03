using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.Worklist.RSCDR
{
    public class Service
    {
        public string PostOrder(Json.Order.Root root)
        {
            var webrequest = (HttpWebRequest)WebRequest.Create("http://121.121.121.4:10110/pacs/putOrder/");
            webrequest.Method = "POST";
            webrequest.ContentType = "application/json; charset=UTF-8";

            byte[] formData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(root));
            webrequest.ContentLength = formData.Length;

            using (var post = webrequest.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            try
            {
                var response = webrequest.GetResponse() as HttpWebResponse;
                if (response.StatusCode != HttpStatusCode.OK) return $"{(int)response.StatusCode} : {response.StatusDescription}";
                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
            catch (Exception e)
            {
                return $"{e.Message}, {e.Source}, {e.StackTrace}, {e.InnerException}";
            }
        }
    }
}

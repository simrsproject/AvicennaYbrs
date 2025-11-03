using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.Disdukcapil
{
    public class Service
    {
        public Response.Root GetPatientByNik(string nik)
        {
            var param = string.Concat(new string[]
                {
                    "nik=",nik,
                    "&user_id=",Helper.DukcapilUserID,
                    "&password=",Helper.DukcapilPassword,
                    "&host=",Helper.DukcapilHostIP
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Helper.PopulateWebRequest("dukcapil_get_nik", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Response.Root>(sr.ReadToEnd());
            }
        }

        public ResponseTarakan.Root GetPatientByNikTarakan(string nik)
        {
            var param = string.Concat(new string[]
            {
                "nik=",nik
            });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Inacbg.Helper.PopulateWebRequest("dukcapil_get_nik_tarakan", Inacbg.Helper.WebRequestMethod.POST, Inacbg.Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<ResponseTarakan.Root>(sr.ReadToEnd());
            }
        }
    }
}
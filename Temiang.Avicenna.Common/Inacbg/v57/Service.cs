using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.Inacbg.v57
{
    public class Service
    {
        public Sitb.Validate.Response.Root SetValidateSitb(string nosep, string nomor_register_sitb)
        {
            var param = string.Concat(new string[]
                {
                    "nomor_sep=",nosep,
                    "&nomor_register_sitb=", nomor_register_sitb
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Helper.PopulateWebRequest("set_sitb_validate", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Sitb.Validate.Response.Root>(sr.ReadToEnd());
            }
        }

        public Sitb.Invalidate.Response.Root SetInvalidateSitb(string nosep)
        {
            var param = string.Concat(new string[]
                {
                    "nomor_sep=",nosep
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Helper.PopulateWebRequest("set_sitb_invalidate", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Sitb.Invalidate.Response.Root>(sr.ReadToEnd());
            }
        }
    }
}
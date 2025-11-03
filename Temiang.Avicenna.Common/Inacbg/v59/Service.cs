using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.Inacbg.v59
{
    public class Service
    {
        public InaGroupper.Procedure.Root SearchProsedurInaGroupper(Inacbg.v51.Procedure.Search.Data tsep)
        {
            var param = string.Concat(new string[]
            {
                "keyword=",tsep.keyword
            });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Inacbg.Helper.PopulateWebRequest("get_procedure_inagroupper", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                var str = sr.ReadToEnd();
                var data = JsonConvert.DeserializeObject<InaGroupper.Procedure.Root>(str);
                if (data.Response.Count == 0)
                {
                    return new InaGroupper.Procedure.Root()
                    {
                        Metadata = new InaGroupper.Procedure.Metadata()
                        {
                            Code = "201",
                            Message = "Data tidak ditemukan"
                        }
                    };
                }
                else return JsonConvert.DeserializeObject<InaGroupper.Procedure.Root>(str);
            }
        }

        public InaGroupper.Diagnose.Root SearchDiagnoseInaGroupper(Inacbg.v51.Procedure.Search.Data tsep)
        {
            var param = string.Concat(new string[]
            {
                "keyword=",tsep.keyword
            });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Inacbg.Helper.PopulateWebRequest("get_diagnose_inagroupper", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                var str = sr.ReadToEnd();
                var data = JsonConvert.DeserializeObject<InaGroupper.Diagnose.Root>(str);
                if (data.Response.Count == 0)
                {
                    return new InaGroupper.Diagnose.Root()
                    {
                        Metadata = new InaGroupper.Diagnose.Metadata()
                        {
                            Code = "201",
                            Message = "Data tidak ditemukan"
                        }
                    };
                }
                else return JsonConvert.DeserializeObject<InaGroupper.Diagnose.Root>(str);
            }
        }
    }
}

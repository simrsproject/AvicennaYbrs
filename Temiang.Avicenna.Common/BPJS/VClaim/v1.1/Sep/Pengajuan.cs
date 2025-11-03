using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.BPJS.VClaim.v11.Sep
{
    public class Pengajuan
    {
        public class Request
        {
            public class TSep
            {
                [JsonProperty("noKartu")]
                public string NoKartu;

                [JsonProperty("tglSep")]
                public string TglSep;

                [JsonProperty("jnsPelayanan")]
                public string JnsPelayanan;

                [JsonProperty("jnsPengajuan")]
                public string JnsPengajuan;

                [JsonProperty("keterangan")]
                public string Keterangan;

                [JsonProperty("user")]
                public string User;
            }

            public class TRequest
            {
                [JsonProperty("t_sep")]
                public TSep TSep;
            }

            public class Root
            {
                [JsonProperty("request")]
                public TRequest Request;
            }
        }

        public class Response
        {
            public class Metadata
            {
                public bool IsValid { get { return Code == "200"; } }

                [JsonProperty("code")]
                public string Code;

                [JsonProperty("message")]
                public string Message;
            }

            public class Root
            {
                [JsonProperty("metadata")]
                public Metadata Metadata;

                [JsonProperty("response")]
                public string Response;
            }
        }
    }

    public class Approval
    {
        public class Request
        {
            public class TSep
            {
                [JsonProperty("noKartu")]
                public string NoKartu;

                [JsonProperty("tglSep")]
                public string TglSep;

                [JsonProperty("jnsPelayanan")]
                public string JnsPelayanan;

                [JsonProperty("jnsPengajuan")]
                public string JnsPengajuan;

                [JsonProperty("keterangan")]
                public string Keterangan;

                [JsonProperty("user")]
                public string User;
            }

            public class TRequest
            {
                [JsonProperty("t_sep")]
                public TSep TSep;
            }

            public class Root
            {
                [JsonProperty("request")]
                public TRequest Request;
            }
        }
    }
}

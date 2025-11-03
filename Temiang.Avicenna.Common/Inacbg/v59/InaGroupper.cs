using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.Inacbg.v59
{
    public class InaGroupper
    {
        public class Procedure
        {
            public class Datum
            {
                [JsonProperty("description")]
                public string Description;

                [JsonProperty("code")]
                public string Code;

                [JsonProperty("validcode")]
                public string Validcode;

                [JsonProperty("im")]
                public string Im;
            }

            public class Metadata : Inacbg.Metadata
            {

            }

            public class Response
            {
                [JsonProperty("count")]
                public int? Count;

                [JsonProperty("data")]
                public List<Datum> Data;
            }

            public class Root
            {
                [JsonProperty("metadata")]
                public Metadata Metadata;

                [JsonProperty("response")]
                public Response Response;
            }
        }

        public class Diagnose
        {
            public class Datum
            {
                [JsonProperty("description")]
                public string Description;

                [JsonProperty("code")]
                public string Code;

                [JsonProperty("validcode")]
                public string Validcode;

                [JsonProperty("accpdx")]
                public string Accpdx;

                [JsonProperty("code_asterisk")]
                public string CodeAsterisk;

                [JsonProperty("asterisk")]
                public string Asterisk;

                [JsonProperty("im")]
                public string Im;
            }

            public class Metadata : Inacbg.Metadata
            {

            }

            public class Response
            {
                [JsonProperty("count")]
                public int? Count;

                [JsonProperty("data")]
                public List<Datum> Data;
            }

            public class Root
            {
                [JsonProperty("metadata")]
                public Metadata Metadata;

                [JsonProperty("response")]
                public Response Response;
            }
        }
    }
}

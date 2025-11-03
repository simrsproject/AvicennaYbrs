using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.BPJS.Apotek.Obat
{
    public class NonRacikan
    {
        public class Request
        {
            public class Root
            {
                [JsonProperty("NOSJP")]
                public string Nosjp;

                [JsonProperty("NORESEP")]
                public string Noresep;

                [JsonProperty("KDOBT")]
                public string Kdobt;

                [JsonProperty("NMOBAT")]
                public string Nmobat;

                [JsonProperty("SIGNA1OBT")]
                public string Signa1obt;

                [JsonProperty("SIGNA2OBT")]
                public string Signa2obt;

                [JsonProperty("JMLOBT")]
                public string Jmlobt;

                [JsonProperty("JHO")]
                public string Jho;

                [JsonProperty("CatKhsObt")]
                public string Catkhsobt;
            }
        }

        public class Response : Metadata
        {
            
        }
    }

    public class Racikan
    {
        public class Request
        {
            public class Root
            {
                [JsonProperty("NOSJP")]
                public string Nosjp;

                [JsonProperty("NORESEP")]
                public string Noresep;

                [JsonProperty("JNSROBT")]
                public string Jnsrobt;

                [JsonProperty("KDOBT")]
                public string Kdobt;

                [JsonProperty("NMOBAT")]
                public string Nmobat;

                [JsonProperty("SIGNA1OBT")]
                public string Signa1obt;

                [JsonProperty("SIGNA2OBT")]
                public string Signa2obt;

                [JsonProperty("PERMINTAAN")]
                public string Permintaan;

                [JsonProperty("JMLOBT")]
                public string Jmlobt;

                [JsonProperty("JHO")]
                public string Jho;

                [JsonProperty("CatKhsObt")]
                public string Catkhsobt;
            }
        }

        public class Response : Metadata
        {

        }
    }
}

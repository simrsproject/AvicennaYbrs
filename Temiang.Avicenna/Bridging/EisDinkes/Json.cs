using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.EisDinkes.Json
{
    public class DataKunjunganPerPoli
    {
        public class Request
        {
            public class Date
            {
                [JsonProperty("start")]
                public string Start;

                [JsonProperty("end")]
                public string End;
            }

            public class Root
            {
                [JsonProperty("date")]
                public Date Date;
            }
        }

        public class Response
        {
            public class Kunjungan
            {
                [JsonProperty("kode_poli")]
                public string KodePoli;

                [JsonProperty("total")]
                public int Total;
            }

            public class Data
            {
                [JsonProperty("faskes")]
                public string Faskes;

                [JsonProperty("kunjungan")]
                public List<Kunjungan> Kunjungan;
            }

            public class Root
            {
                [JsonProperty("code")]
                public int Code;

                [JsonProperty("messages")]
                public string Messages;

                [JsonProperty("data")]
                public Data Data;
            }
        }
    }

    public class DataTop10Diagnosa
    {
        public class Request
        {
            public class Date
            {
                [JsonProperty("start")]
                public string Start;

                [JsonProperty("end")]
                public string End;
            }

            public class Root
            {
                [JsonProperty("date")]
                public Date Date;
            }
        }

        public class Response
        {
            public class DiagnosaIgd
            {
                [JsonProperty("kode_diagnosa")]
                public string KodeDiagnosa;

                [JsonProperty("total")]
                public int Total;
            }

            public class DiagnosaRalan
            {
                [JsonProperty("kode_diagnosa")]
                public string KodeDiagnosa;

                [JsonProperty("total")]
                public int Total;
            }

            public class DiagnosaRanap
            {
                [JsonProperty("kode_diagnosa")]
                public string KodeDiagnosa;

                [JsonProperty("total")]
                public int Total;
            }

            public class Diagnosa
            {
                [JsonProperty("diagnosa_igd")]
                public List<DiagnosaIgd> DiagnosaIgd;

                [JsonProperty("diagnosa_ralan")]
                public List<DiagnosaRalan> DiagnosaRalan;

                [JsonProperty("diagnosa_ranap")]
                public List<DiagnosaRanap> DiagnosaRanap;
            }

            public class Data
            {
                [JsonProperty("faskes")]
                public string Faskes;

                [JsonProperty("diagnosa")]
                public Diagnosa Diagnosa;
            }

            public class Root
            {
                [JsonProperty("code")]
                public int Code;

                [JsonProperty("messages")]
                public string Messages;

                [JsonProperty("data")]
                public Data Data;
            }
        }
    }

}
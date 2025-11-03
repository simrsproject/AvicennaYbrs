using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.Inacbg.v510
{
    public class Procedure
    {
        // === SET ===
        public class Data
        {
            [JsonProperty("nomor_sep")]
            public string nomor_sep { get; set; }

            // contoh: "88.01#90.090+2#90.090"
            [JsonProperty("procedure")]
            public string procedure { get; set; }
        }

        public class Response
        {
            public class Metadata : Inacbg.Metadata
            {
                [JsonProperty("error_no")]
                public string ErrorNo { get; set; }
            }

            public class Result
            {
                [JsonProperty("metadata")]
                public Metadata Metadata { get; set; }
            }
        }

        // === GET ===
        public class Get
        {
            public class Data
            {
                [JsonProperty("nomor_sep")]
                public string nomor_sep { get; set; }
            }

            public class Response
            {
                public class Result
                {
                    [JsonProperty("metadata")]
                    public Inacbg.Metadata Metadata { get; set; }

                    [JsonProperty("data")]
                    public Payload Data { get; set; }
                }

                public class Payload
                {
                    // contoh: "88.01#90.090+2#90.090"
                    [JsonProperty("string")]
                    public string String { get; set; }

                    [JsonProperty("expanded")]
                    public List<ExpandedItem> Expanded { get; set; }
                }

                public class ExpandedItem
                {
                    [JsonProperty("code")]
                    public string Code { get; set; }

                    [JsonProperty("display")]
                    public string Display { get; set; }

                    // jumlah kemunculan (qty) pada token "+N"
                    [JsonProperty("multiply")]
                    public int? Multiply { get; set; }

                    [JsonProperty("no")]
                    public string No { get; set; }

                    [JsonProperty("validcode")]
                    public string ValidCode { get; set; }

                    [JsonProperty("metadata")]
                    public Inacbg.Metadata Metadata { get; set; }
                }
            }
        }
    }
}

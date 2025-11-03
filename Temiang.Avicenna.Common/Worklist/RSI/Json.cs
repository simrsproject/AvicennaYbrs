using Newtonsoft.Json;
using System.Collections.Generic;

namespace Temiang.Avicenna.Common.Worklist.RSI
{
    public class Json
    {
        public class Order
        {
            public class Root
            {
                [JsonProperty("uid")]
                public string uid;
                [JsonProperty("acc")]
                public string acc;
                [JsonProperty("patientid")]
                public string patientid;
                [JsonProperty("mrn")]
                public string mrn;
                [JsonProperty("name")]
                public string name;
                [JsonProperty("address")]
                public string address;
                [JsonProperty("sex")]
                public string sex;
                [JsonProperty("birth_date")]
                public string birth_date;
                [JsonProperty("weight")]
                public string weight;
                [JsonProperty("name_dep")]
                public string name_dep;
                [JsonProperty("xray_type_code")]
                public string xray_type_code;
                [JsonProperty("typename")]
                public string typename;
                [JsonProperty("prosedur")]
                public string prosedur;
                [JsonProperty("dokterid")]
                public string dokterid;
                [JsonProperty("named")]
                public string named;
                [JsonProperty("dokradid")]
                public string dokradid;
                [JsonProperty("dokrad_name")]
                public string dokrad_name;
                [JsonProperty("create_time")]
                public string create_time;
                [JsonProperty("schedule_date")]
                public string schedule_date;
                [JsonProperty("schedule_time")]
                public string schedule_time;
                [JsonProperty("priority")]
                public string priority;
                [JsonProperty("pat_state")]
                public string pat_state;
                [JsonProperty("spc_needs")]
                public string spc_needs;
                [JsonProperty("payment")]
                public string payment;
                [JsonProperty("arrive_date")]
                public string arrive_date;
                [JsonProperty("arrive_time")]
                public string arrive_time;
            }
        }

        public class Sps
        {
            public class Hasil
            {
                [JsonProperty("pk")]
                public int pk;
                [JsonProperty("sps_id")]
                public string sps_id;
                [JsonProperty("req_proc_id")]
                public string req_proc_id;
            }

            public class Data
            {
                [JsonProperty("status")]
                public bool status;
                [JsonProperty("hasil")]
                public List<Hasil> hasil;
            }

            public class Root
            {
                [JsonProperty("data")]
                public Data data;
            }
        }

        public class Result
        {
            public class Hasil
            {
                [JsonProperty("uid")]
                public string uid { get; set; }
                [JsonProperty("acc")]
                public string acc { get; set; }
                [JsonProperty("patientid")]
                public string patientid { get; set; }
                [JsonProperty("mrn")]
                public string mrn { get; set; }
                [JsonProperty("name")]
                public string name { get; set; }
                [JsonProperty("address")]
                public string address { get; set; }
                [JsonProperty("sex")]
                public string sex { get; set; }
                [JsonProperty("birth_date")]
                public string birth_date { get; set; }
                [JsonProperty("weight")]
                public string weight { get; set; }
                [JsonProperty("name_dep")]
                public string name_dep { get; set; }
                [JsonProperty("xray_type_code")]
                public string xray_type_code { get; set; }
                [JsonProperty("typename")]
                public string typename { get; set; }
                [JsonProperty("prosedur")]
                public string prosedur { get; set; }
                [JsonProperty("dokterid")]
                public string dokterid { get; set; }
                [JsonProperty("named")]
                public string named { get; set; }
                [JsonProperty("dokradid")]
                public string dokradid { get; set; }
                [JsonProperty("dokrad_name")]
                public string dokrad_name { get; set; }
                [JsonProperty("create_time")]
                public string create_time { get; set; }
                [JsonProperty("schedule_date")]
                public string schedule_date { get; set; }
                [JsonProperty("schedule_time")]
                public string schedule_time { get; set; }
                [JsonProperty("priority")]
                public string priority { get; set; }
                [JsonProperty("pat_state")]
                public string pat_state { get; set; }
                [JsonProperty("spc_needs")]
                public string spc_needs { get; set; }
                [JsonProperty("payment")]
                public string payment { get; set; }
                [JsonProperty("arrive_date")]
                public string arrive_date { get; set; }
                [JsonProperty("arrive_time")]
                public string arrive_time { get; set; }
                [JsonProperty("status")]
                public string status { get; set; }
            }

            public class Data
            {
                [JsonProperty("status")]
                public bool status { get; set; }
                [JsonProperty("hasil")]
                public IList<Hasil> hasil { get; set; }
            }

            public class Root
            {
                [JsonProperty("data")]
                public Data data { get; set; }
            }
        }

        public class Response
        {
            public class Data
            {
                [JsonProperty("status")]
                public bool status;
                [JsonProperty("hasil")]
                public string hasil;
            }

            public class Root
            {
                [JsonProperty("data")]
                public Data data;
            }
        }
    }
}

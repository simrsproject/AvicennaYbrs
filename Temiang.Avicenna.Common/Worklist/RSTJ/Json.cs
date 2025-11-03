using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.Worklist.RSTJ
{
    public class Json
    {
        public class Request
        {
            public class Root
            {
                [JsonProperty("norm")]
                public string Norm;

                [JsonProperty("nama")]
                public string Nama;

                [JsonProperty("alamat")]
                public string Alamat;

                [JsonProperty("kota")]
                public string Kota;

                [JsonProperty("tgllahir")]
                public string Tgllahir;

                [JsonProperty("nohap")]
                public string Nohap;

                [JsonProperty("kelamin")]
                public string Kelamin;

                [JsonProperty("drpeminta")]
                public string Drpeminta;

                [JsonProperty("asalpasien")]
                public string Asalpasien;

                [JsonProperty("layanan")]
                public string Layanan;

                [JsonProperty("notagihan")]
                public string Notagihan;

                [JsonProperty("statusbayar")]
                public string Statusbayar;

                [JsonProperty("notransaksi")]
                public string NoTransaksi;

                [JsonProperty("asuransi")]
                public string Asuransi;

                [JsonProperty("dokterradiologi")]
                public string DokterRadiologi;

                [JsonProperty("dokterid")]
                public string DokterId;
            }
        }

        public class Response
        {
            public class Root
            {
                [JsonProperty("status")]
                public string Status;

                [JsonProperty("message")]
                public string Message;

                [JsonProperty("link")]
                public string Link;
            }
        }

        public class ExpertiseResponse
        {
            [JsonProperty("status")]
            public string Status;

            [JsonProperty("filepdf")]
            public string FilePdf;

            [JsonProperty("bacaan")]
            public string Bacaan;

            [JsonProperty("rmnumber")]
            public string RmNumber;

            [JsonProperty("accnumber")]
            public string AccNumber;

            [JsonProperty("trxnumber")]
            public string TrxNumber;

            [JsonProperty("doctor")]
            public string Doctor;
        }

        public class PdfResultResponse
        {
            [JsonProperty("tglekspertise")]
            public string Tglekspertise;

            [JsonProperty("tgldaftar")]
            public string Tgldaftar;

            [JsonProperty("admin")]
            public string Admin;

            [JsonProperty("tglverifikasiadmin")]
            public object Tglverifikasiadmin;

            [JsonProperty("updated_at")]
            public DateTime UpdatedAt;

            [JsonProperty("status")]
            public string Status;

            [JsonProperty("filepdf")]
            public string Filepdf;

            [JsonProperty("bacaan")]
            public string Bacaan;

            [JsonProperty("rmnumber")]
            public string Rmnumber;

            [JsonProperty("accnumber")]
            public string Accnumber;

            [JsonProperty("trxnumber")]
            public string Trxnumber;

            [JsonProperty("doctor")]
            public string Doctor;
        }
    }
}

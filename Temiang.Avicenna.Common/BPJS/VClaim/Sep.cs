using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Temiang.Avicenna.Common.BPJS.VClaim
{
    public class Sep
    {
        public class Insert
        {
            public class Rujukan
            {
                public string asalRujukan { get; set; }
                public string tglRujukan { get; set; }
                public string noRujukan { get; set; }
                public string ppkRujukan { get; set; }
            }

            public class Poli
            {
                public string tujuan { get; set; }
                public string eksekutif { get; set; }
            }

            public class Cob
            {
                public string cob { get; set; }
            }

            public class Jaminan
            {
                public string lakaLantas { get; set; }
                public string penjamin { get; set; }
                public string lokasiLaka { get; set; }
            }

            public class TSep
            {
                public string noKartu { get; set; }
                public string tglSep { get; set; }
                public string ppkPelayanan { get; set; }
                public string jnsPelayanan { get; set; }
                public string klsRawat { get; set; }
                public string noMR { get; set; }
                public Rujukan rujukan { get; set; }
                public string catatan { get; set; }
                public string diagAwal { get; set; }
                public Poli poli { get; set; }
                public Cob cob { get; set; }
                public Jaminan jaminan { get; set; }
                public string noTelp { get; set; }
                public string user { get; set; }
            }

            public class Request
            {
                public TSep t_sep { get; set; }
            }

            public class RootObject
            {
                public Request request { get; set; }
            }

            public class Feedback
            {

                public class Peserta
                {

                    [JsonProperty("asuransi")]
                    public string Asuransi { get; set; }

                    [JsonProperty("hakKelas")]
                    public string HakKelas { get; set; }

                    [JsonProperty("jnsPeserta")]
                    public string JnsPeserta { get; set; }

                    [JsonProperty("kelamin")]
                    public string Kelamin { get; set; }

                    [JsonProperty("nama")]
                    public string Nama { get; set; }

                    [JsonProperty("noKartu")]
                    public string NoKartu { get; set; }

                    [JsonProperty("noMr")]
                    public string NoMr { get; set; }

                    [JsonProperty("tglLahir")]
                    public string TglLahir { get; set; }
                }

                public class Sep
                {

                    [JsonProperty("catatan")]
                    public string Catatan { get; set; }

                    [JsonProperty("diagnosa")]
                    public string Diagnosa { get; set; }

                    [JsonProperty("jnsPelayanan")]
                    public string JnsPelayanan { get; set; }

                    [JsonProperty("kelasRawat")]
                    public string KelasRawat { get; set; }

                    [JsonProperty("noSep")]
                    public string NoSep { get; set; }

                    [JsonProperty("penjamin")]
                    public string Penjamin { get; set; }

                    [JsonProperty("peserta")]
                    public Peserta Peserta { get; set; }

                    [JsonProperty("poli")]
                    public string Poli { get; set; }

                    [JsonProperty("poliEksekutif")]
                    public string PoliEksekutif { get; set; }

                    [JsonProperty("tglSep")]
                    public string TglSep { get; set; }
                }

                public class Response
                {

                    [JsonProperty("sep")]
                    public Sep Sep { get; set; }
                }

                public class Result : Metadata
                {

                    [JsonProperty("metaData")]
                    public Metadata MetaData { get; set; }

                    [JsonProperty("response")]
                    public Response Response { get; set; }
                }
            }
        }

        public class Update
        {
            public class Rujukan
            {
                public string asalRujukan { get; set; }
                public string tglRujukan { get; set; }
                public string noRujukan { get; set; }
                public string ppkRujukan { get; set; }
            }

            public class Poli
            {
                public string eksekutif { get; set; }
            }

            public class Cob
            {
                public string cob { get; set; }
            }

            public class Jaminan
            {
                public string lakaLantas { get; set; }
                public string penjamin { get; set; }
                public string lokasiLaka { get; set; }
            }

            public class TSep
            {
                public string noSep { get; set; }
                public string klsRawat { get; set; }
                public string noMR { get; set; }
                public Rujukan rujukan { get; set; }
                public string catatan { get; set; }
                public string diagAwal { get; set; }
                public Poli poli { get; set; }
                public Cob cob { get; set; }
                public Jaminan jaminan { get; set; }
                public string noTelp { get; set; }
                public string user { get; set; }
            }

            public class Request
            {
                public TSep t_sep { get; set; }
            }

            public class RootObject
            {
                public Request request { get; set; }
            }

            public class Feedback : Metadata
            {
                [JsonProperty("metaData")]
                public Metadata MetaData { get; set; }

                [JsonProperty("response")]
                public string Response { get; set; }
            }
        }

        public class Delete
        {
            public class TSep
            {
                public string noSep { get; set; }
                public string user { get; set; }
            }

            public class Request
            {
                public TSep t_sep { get; set; }
            }

            public class RootObject
            {
                public Request request { get; set; }
            }

            public class Feedback : Metadata
            {
                [JsonProperty("metaData")]
                public Metadata MetaData { get; set; }

                [JsonProperty("response")]
                public string Response { get; set; }
            }
        }

        public class Search
        {

            public class Peserta
            {

                [JsonProperty("asuransi")]
                public object Asuransi { get; set; }

                [JsonProperty("hakKelas")]
                public string HakKelas { get; set; }

                [JsonProperty("jnsPeserta")]
                public string JnsPeserta { get; set; }

                [JsonProperty("kelamin")]
                public string Kelamin { get; set; }

                [JsonProperty("nama")]
                public string Nama { get; set; }

                [JsonProperty("noKartu")]
                public string NoKartu { get; set; }

                [JsonProperty("noMr")]
                public string NoMr { get; set; }

                [JsonProperty("tglLahir")]
                public string TglLahir { get; set; }
            }

            public class Data
            {

                [JsonProperty("catatan")]
                public string Catatan { get; set; }

                [JsonProperty("diagnosa")]
                public string Diagnosa { get; set; }

                [JsonProperty("jnsPelayanan")]
                public string JnsPelayanan { get; set; }

                [JsonProperty("kelasRawat")]
                public string KelasRawat { get; set; }

                [JsonProperty("noSep")]
                public string NoSep { get; set; }

                [JsonProperty("penjamin")]
                public object Penjamin { get; set; }

                [JsonProperty("peserta")]
                public Peserta Peserta { get; set; }

                [JsonProperty("poli")]
                public string Poli { get; set; }

                [JsonProperty("poliEksekutif")]
                public string PoliEksekutif { get; set; }

                [JsonProperty("tglSep")]
                public string TglSep { get; set; }
            }

            public class Feedback : Metadata
            {

                [JsonProperty("metaData")]
                public Metadata MetaData { get; set; }

                [JsonProperty("response")]
                public Data Response { get; set; }
            }
        }

        public class Approve
        {
            public class TSep
            {
                public string noKartu { get; set; }
                public string tglSep { get; set; }
                public string jnsPelayanan { get; set; }
                public string keterangan { get; set; }
                public string user { get; set; }
            }

            public class Request
            {
                public TSep t_sep { get; set; }
            }

            public class RootObject
            {
                public Request request { get; set; }
            }

            public class Feedback : Metadata
            {

                [JsonProperty("metaData")]
                public Metadata MetaData { get; set; }

                [JsonProperty("response")]
                public string Response { get; set; }
            }
        }

        public class UpdateTglPulang
        {
            public class TSep
            {
                public string noSep { get; set; }
                public string tglPulang { get; set; }
                public string user { get; set; }
            }

            public class Request
            {
                public TSep t_sep { get; set; }
            }

            public class RootObject
            {
                public Request request { get; set; }
            }

            public class Feedback : Metadata
            {

                [JsonProperty("metadata")]
                public Metadata Metadata { get; set; }

                [JsonProperty("response")]
                public string Response { get; set; }
            }
        }
    }
}

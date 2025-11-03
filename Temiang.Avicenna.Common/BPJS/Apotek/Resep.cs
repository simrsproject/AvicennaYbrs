using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.BPJS.Apotek.Resep
{
    public class MetaData
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public bool IsValid => Code == "200";
    }

    public class SimpanResep
    {
        public class Request
        {
            [JsonProperty("TGLSJP")]
            public string TGLSJP { get; set; }

            [JsonProperty("REFASALSJP")]
            public string REFASALSJP { get; set; }

            [JsonProperty("POLIRSP")]
            public string POLIRSP { get; set; }

            [JsonProperty("KDJNSOBAT")]
            public string KDJNSOBAT { get; set; }

            [JsonProperty("NORESEP")]
            public string NORESEP { get; set; }

            [JsonProperty("IDUSERSJP")]
            public string IDUSERSJP { get; set; }

            [JsonProperty("TGLRSP")]
            public string TGLRSP { get; set; }

            [JsonProperty("TGLPELRSP")]
            public string TGLPELRSP { get; set; }

            [JsonProperty("KdDokter")]
            public string KdDokter { get; set; }

            [JsonProperty("iterasi")]
            public string Iterasi { get; set; }
        }

        public class Response
        {
            [JsonProperty("noSep_Kunjungan")]
            public string NoSepKunjungan { get; set; }

            [JsonProperty("noKartu")]
            public string NoKartu { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }

            [JsonProperty("faskesAsal")]
            public string FaskesAsal { get; set; }

            [JsonProperty("noApotik")]
            public string NoApotik { get; set; }

            [JsonProperty("noResep")]
            public string NoResep { get; set; }

            [JsonProperty("tglResep")]
            public string TglResep { get; set; }

            [JsonProperty("kdJnsObat")]
            public string KdJnsObat { get; set; }

            [JsonProperty("byTagRsp")]
            public string ByTagRsp { get; set; }

            [JsonProperty("byVerRsp")]
            public string ByVerRsp { get; set; }

            [JsonProperty("tglEntry")]
            public string TglEntry { get; set; }
        }

        public class Root
        {
            [JsonProperty("Request")]
            public Request RequestData { get; set; }

            [JsonProperty("Response")]
            public Response ResponseData { get; set; }

            [JsonProperty("metaData")]
            public MetaData MetaData { get; set; }
        }
    }

    public class HapusResep
    {
        public class Request
        {
            [JsonProperty("nosjp")]
            public string NOSJP;

            [JsonProperty("refasalsjp")]
            public string REFASALSJP;

            [JsonProperty("noresep")]
            public string NORESEP;

            public class Root
            {
                public Request Request;
            }
        }

        public class Root
        {
            [JsonProperty("Request")]
            public Request Request;

            [JsonProperty("Response")]
            public Response Response;
        }

        public class Response : Metadata
        {

        }
    }

    public class DaftarResep
    {
        public class Request
        {
            public class Root
            {
                [JsonProperty("kdppk")]
                public string KdPPK;

                [JsonProperty("KdJnsObat")]
                public string KdJnsObat;

                [JsonProperty("JnsTgl")]
                public string JnsTgl;

                [JsonProperty("TglMulai")]
                public string TglMulai;

                [JsonProperty("TglAkhir")]
                public string TglAkhir;
            }
        }

        public class Response
        {
            public class Resep
            {
                [JsonProperty("NORESEP")]
                public string NoResep;

                [JsonProperty("NOAPOTIK")]
                public string NoApotik;

                [JsonProperty("NOSEP_KUNJUNGAN")]
                public string NoSep_Kunjungan;

                [JsonProperty("NOKARTU")]
                public string NoKartu;

                [JsonProperty("NAMA")]
                public string Nama;

                [JsonProperty("TGLENTRY")]
                public string TglEntry;

                [JsonProperty("TGLRESEP")]
                public string TglResep;

                [JsonProperty("TGLPELRSP")]
                public string TglPelResep;

                [JsonProperty("BYTAGRSP")]
                public string ByTagRsp;

                [JsonProperty("BYVERRSP")]
                public string ByVerRsp;

                [JsonProperty("KDJNSOBAT")]
                public string KdJnsObat;

                [JsonProperty("FASKESASAL")]
                public string FaskesAsal;
            }

            public class TResponse
            {
                [JsonProperty("list")]
                public List<Resep> List;
            }

            public class Root
            {
                [JsonProperty("response")]
                public List<Resep> Response;

                [JsonProperty("metadata")]
                public Metadata Metadata;
            }

        }

        public class Root
        {
            [JsonProperty("response")]
            public Response Response;

            [JsonProperty("metaData")]
            public Metadata MetaData;
        }
    }
}

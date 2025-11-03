using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.BPJS.Apotek.PelayananObat
{
    public class HapusPelayananObat
    {
        public class Request
        {
            public class Root
            {
                [JsonProperty("nosepapotek")]
                public string Nosepapotek;

                [JsonProperty("noresep")]
                public string Noresep;

                [JsonProperty("kodeobat")]
                public string Kodeobat;

                [JsonProperty("tipeobat")]
                public string Tipeobat;
            }
        }

        public class Response : Metadata
        {

        }
    }

    public class DaftarPelayananObat : Metadata
    {
        public class detailsep
        {
            [JsonProperty("noSepApotek")]
            public string NoSepApotek;

            [JsonProperty("noSepAsal")]
            public string NoSepAsal;

            [JsonProperty("noresep")]
            public string Noresep;

            [JsonProperty("nokartu")]
            public string Nokartu;

            [JsonProperty("nmpst")]
            public string Nmpst;

            [JsonProperty("kdjnsobat")]
            public string Kdjnsobat;

            [JsonProperty("nmjnsobat")]
            public string Nmjnsobat;

            [JsonProperty("tglpelayanan")]
            public string Tglpelayanan;

            public class listobat
            {
                [JsonProperty("kodeobat")]
                public string Kodeobat;

                [JsonProperty("namaobat")]
                public string Namaobat;

                [JsonProperty("tipeobat")]
                public string Tipeobat;

                [JsonProperty("signa1")]
                public string Signa1;

                [JsonProperty("signa2")]
                public string Signa2;

                [JsonProperty("hari")]
                public string Hari;

                [JsonProperty("permintaan")]
                public string Permintaan;

                [JsonProperty("jumlah")]
                public string Jumlah;

                [JsonProperty("harga")]
                public string Harga;
            }
        }
        public class Response
        {
            [JsonProperty("list")]
            public List<detailsep> List;
        }

        public class Root
        {
            [JsonProperty("response")]
            public Response Response;

            [JsonProperty("metaData")]
            public Metadata MetaData;
        }
    }

    public class RiwayatPelayananObat
    {
        public class History
        {
            [JsonProperty("nosjp")]
            public string Nosjp { get; set; }

            [JsonProperty("tglpelayanan")]
            public string Tglpelayanan { get; set; }

            [JsonProperty("noresep")]
            public string Noresep { get; set; }

            [JsonProperty("kodeobat")]
            public string Kodeobat { get; set; }

            [JsonProperty("namaobat")]
            public string Namaobat { get; set; }

            [JsonProperty("jmlobat")]
            public string Jmlobat { get; set; }
        }

        public class ListItem
        {
            [JsonProperty("nokartu")]
            public string Nokartu { get; set; }

            [JsonProperty("namapeserta")]
            public string Namapeserta { get; set; }

            [JsonProperty("tgllhr")]
            public string Tgllhr { get; set; }

            [JsonProperty("history")]
            public List<History> Histories { get; set; }
        }

        public class Response
        {
            [JsonProperty("list")]
            public ListItem List { get; set; }
        }

        public class Root
        {
            [JsonProperty("response")]
            public Response Response { get; set; }

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }
        }
    }

}

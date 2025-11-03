using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Temiang.Avicenna.Common.BPJS.VClaim.Referensi
{

    public class Diagnosa : Metadata
    {

        public class Data
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class Response
        {

            [JsonProperty("diagnosa")]
            public Data[] Diagnosa { get; set; }
        }

        public class Result
        {

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }

            [JsonProperty("response")]
            public Response Response { get; set; }
        }
    }

    public class Poli : Metadata
    {

        public class Data
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class Response
        {

            [JsonProperty("poli")]
            public Data[] Poli { get; set; }
        }

        public class Result
        {

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }

            [JsonProperty("response")]
            public Response Response { get; set; }
        }
    }

    public class Faskes : Metadata
    {

        public class Faske
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class Response
        {

            [JsonProperty("faskes")]
            public Faske[] Faskes { get; set; }
        }

        public class Result
        {

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }

            [JsonProperty("response")]
            public Response Response { get; set; }
        }
    }

    public class Procedure : Metadata
    {

        public class Data
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class Response
        {

            [JsonProperty("procedure")]
            public Data[] Procedure { get; set; }
        }

        public class Result
        {

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }

            [JsonProperty("response")]
            public Response Response { get; set; }
        }
    }

    public class KelasRawat : Metadata
    {

        public class List
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class Response
        {

            [JsonProperty("list")]
            public List[] List { get; set; }
        }

        public class Result
        {

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }

            [JsonProperty("response")]
            public Response Response { get; set; }
        }
    }

    public class Dokter : Metadata
    {

        public class List
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class Response
        {

            [JsonProperty("list")]
            public List[] List { get; set; }
        }

        public class Result
        {

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }

            [JsonProperty("response")]
            public Response Response { get; set; }
        }
    }

    public class Spesialistik : Metadata
    {

        public class List
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class Response
        {

            [JsonProperty("list")]
            public List[] List { get; set; }
        }

        public class Result
        {

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }

            [JsonProperty("response")]
            public Response Response { get; set; }
        }
    }

    public class RuangRawat : Metadata
    {

        public class List
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class Response
        {

            [JsonProperty("list")]
            public List[] List { get; set; }
        }

        public class Result
        {

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }

            [JsonProperty("response")]
            public Response Response { get; set; }
        }
    }

    public class CaraKeluar : Metadata
    {

        public class List
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class Response
        {

            [JsonProperty("list")]
            public List[] List { get; set; }
        }

        public class Result
        {

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }

            [JsonProperty("response")]
            public Response Response { get; set; }
        }
    }

    public class PascaPulang : Metadata
    {

        public class List
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class Response
        {

            [JsonProperty("list")]
            public List[] List { get; set; }
        }

        public class Result
        {

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }

            [JsonProperty("response")]
            public Response Response { get; set; }
        }
    }
}

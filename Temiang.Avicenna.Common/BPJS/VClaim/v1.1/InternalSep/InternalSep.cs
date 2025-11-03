using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.BPJS.VClaim.v11.InternalSep
{
    public class MetaData
    {
        public bool IsValid { get { return Code == "200"; } }

        [JsonProperty("code")]
        public string Code;

        [JsonProperty("message")]
        public string Message;
    }

    public class Select
    {
        public class List
        {
            [JsonProperty("tujuanrujuk")]
            public string Tujuanrujuk;

            [JsonProperty("nmtujuanrujuk")]
            public string Nmtujuanrujuk;

            [JsonProperty("nmpoliasal")]
            public string Nmpoliasal;

            [JsonProperty("tglrujukinternal")]
            public string Tglrujukinternal;

            [JsonProperty("nosep")]
            public string Nosep;

            [JsonProperty("nosepref")]
            public string Nosepref;

            [JsonProperty("ppkpelsep")]
            public string Ppkpelsep;

            [JsonProperty("nokapst")]
            public string Nokapst;

            [JsonProperty("tglsep")]
            public string Tglsep;

            [JsonProperty("nosurat")]
            public string Nosurat;

            [JsonProperty("flaginternal")]
            public string Flaginternal;

            [JsonProperty("kdpoliasal")]
            public string Kdpoliasal;

            [JsonProperty("kdpolituj")]
            public string Kdpolituj;

            [JsonProperty("kdpenunjang")]
            public string Kdpenunjang;

            [JsonProperty("nmpenunjang")]
            public string Nmpenunjang;

            [JsonProperty("diagppk")]
            public string Diagppk;

            [JsonProperty("kddokter")]
            public string Kddokter;

            [JsonProperty("nmdokter")]
            public string Nmdokter;

            [JsonProperty("flagprosedur")]
            public string Flagprosedur;

            [JsonProperty("opsikonsul")]
            public string Opsikonsul;

            [JsonProperty("flagsep")]
            public string Flagsep;

            [JsonProperty("fuser")]
            public string Fuser;

            [JsonProperty("fdate")]
            public string Fdate;

            [JsonProperty("nmdiag")]
            public string Nmdiag;
        }

        public class Response
        {
            [JsonProperty("count")]
            public string Count;

            [JsonProperty("list")]
            public List<List> List;
        }

        public class Root
        {
            [JsonProperty("metaData")]
            public MetaData MetaData;

            [JsonProperty("response")]
            public Response Response;
        }
    }

    public class Delete
    {
        public class Request
        {
            public class TSep
            {
                [JsonProperty("noSep")]
                public string NoSep;

                [JsonProperty("noSurat")]
                public string NoSurat;

                [JsonProperty("tglRujukanInternal")]
                public string TglRujukanInternal;

                [JsonProperty("kdPoliTuj")]
                public string KdPoliTuj;

                [JsonProperty("user")]
                public string User;
            }

            public class TRequest
            {
                [JsonProperty("t_sep")]
                public TSep TSep;
            }

            public class Root
            {
                [JsonProperty("request")]
                public TRequest Request;
            }
        }

        public class Response
        {
            public class Root
            {
                [JsonProperty("metaData")]
                public MetaData MetaData;

                [JsonProperty("response")]
                public string Response;
            }
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiang.Avicenna.Bridging.Antrol.Farmasi
{
    public class AmbilAntrean
    {
        public class Request
        {
            public class Root
            {
                [JsonProperty("kodebooking")]
                public string kodebooking;
            }
        }

        public class Response
        {

        }
    }
}
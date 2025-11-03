using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.PCare.Common
{
    public class MetaData
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        public string MessageDescription
        {
            get
            {
                switch (Code)
                {
                    case "200":
                        return "Retrieve data success.";
                    case "201":
                        return "Add data success.";
                    case "204":
                        return "Can't find data with that criteria in PCare service.";
                    default:
                        return string.Format("Bridging problem with code: {0} {1}", Code, Message);
                }
            }
        }
    }
}

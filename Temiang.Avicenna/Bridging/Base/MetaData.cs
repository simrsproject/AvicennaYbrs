using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.Base
{
    public class MetaData
    {
        public MetaData()
        {
            Code = "200";
            Message = "OK";
        }
        public MetaData(string code, string message)
        {
            Code = code;
            Message = message;
        }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        //public string MessageDescription
        //{
        //    get
        //    {
        //        switch (Code)
        //        {
        //            case "200":
        //                return "Retrieve data success.";
        //            case "201":
        //                return "Add data success.";
        //            case "204":
        //                return "Can't find data with that criteria in PCare service.";
        //            case "-1":
        //                return "Authorization has been denied for this request.";
        //            default:
        //                return string.Format("Bridging problem with code: {0} {1}", Code, Message);
        //        }
        //    }
        //}
    }
}

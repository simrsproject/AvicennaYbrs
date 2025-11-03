using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Temiang.Avicenna.Bridging.Antrean.ParameterClass
{
    public class TokenParam
    {
        //{
        //    "username": "admin",
        //    "password": "123456"
        //}
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }

}
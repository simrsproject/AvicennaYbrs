using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.MedicationPost;

namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Address
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("district")]
        public string District { get; set; }

        [JsonProperty("extension")]
        public List<ExtensionOrgItem> Extension { get; set; }

        [JsonProperty("line")]
        public List<string> Line { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("use")]
        public string Use { get; set; }
    }

    public class ExtensionOrgItem
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("valueCode")]
        public string ValueCode { get; set; }
    }

    public class Contact
    {
        [JsonProperty("purpose")]
        public Purpose Purpose { get; set; }

        [JsonProperty("telecom")]
        public List<Telecom> Telecom { get; set; }
    }

    public class AddressExtension
    {
        [JsonProperty("extension")]
        public List<AddressExtensionItem> Extension { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class AddressExtensionItem
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("valueCode")]
        public string ValueCode { get; set; }
    }

    public class Purpose
    {
        [JsonProperty("coding")]
        public List<Coding> Coding { get; set; }
    }

    public class OrganizationResponse : BaseResponse
    {
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("address")]
        public List<Address> Address { get; set; }

        [JsonProperty("contact")]
        public List<Contact> Contact { get; set; }

        [JsonProperty("identifier")]
        public List<Identifier> Identifier { get; set; }


        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public List<Code> Type { get; set; }
    }

    public class Telecom
    {
        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("use")]
        public string Use { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}

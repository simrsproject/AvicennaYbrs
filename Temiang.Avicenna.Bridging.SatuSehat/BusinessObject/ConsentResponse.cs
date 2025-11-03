using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.ConsentResponse
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Category
    {
        [JsonProperty("coding")]
        public List<Coding> Coding { get; set; }
    }

    public class Coding
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("system")]
        public string System { get; set; }
    }

    public class Organization
    {
        [JsonProperty("reference")]
        public string Reference { get; set; }
    }

    public class Patient
    {
        [JsonProperty("reference")]
        public string Reference { get; set; }
    }

    public class Period
    {
        [JsonProperty("start")]
        public DateTime Start { get; set; }
    }

    public class PolicyRule
    {
        [JsonProperty("coding")]
        public List<Coding> Coding { get; set; }
    }

    public class Provision
    {
        [JsonProperty("period")]
        public Period Period { get; set; }
    }

    public class Root : BaseResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("scope")]
        public Scope Scope { get; set; }

        [JsonProperty("category")]
        public List<Category> Category { get; set; }

        [JsonProperty("patient")]
        public Patient Patient { get; set; }

        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }

        [JsonProperty("organization")]
        public List<Organization> Organization { get; set; }

        [JsonProperty("policyRule")]
        public PolicyRule PolicyRule { get; set; }

        [JsonProperty("provision")]
        public Provision Provision { get; set; }
    }

    public class Scope
    {
        [JsonProperty("coding")]
        public List<Coding> Coding { get; set; }
    }




}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.MedicationPost
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Code
    {
        [JsonProperty("coding")]
        public List<Coding> Coding { get; set; }
    }

    public class Coding
    {
        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }
    }

    public class Denominator
    {
        [JsonProperty("value")]
        public int? Value { get; set; }

        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }

    public class Extension
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("valueCodeableConcept")]
        public ValueCodeableConcept ValueCodeableConcept { get; set; }
    }

    public class Form
    {
        [JsonProperty("coding")]
        public List<Coding> Coding { get; set; }
    }

    public class Identifier
    {
        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("use")]
        public string Use { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Ingredient
    {
        [JsonProperty("itemCodeableConcept")]
        public ItemCodeableConcept ItemCodeableConcept { get; set; }

        [JsonProperty("isActive")]
        public bool? IsActive { get; set; }

        [JsonProperty("strength")]
        public Strength Strength { get; set; }
    }

    public class ItemCodeableConcept
    {
        [JsonProperty("coding")]
        public List<Coding> Coding { get; set; }
    }

    public class Manufacturer
    {
        [JsonProperty("reference")]
        public string Reference { get; set; }
    }

    public class Meta
    {
        [JsonProperty("profile")]
        public List<string> Profile { get; set; }
    }

    public class Numerator
    {
        [JsonProperty("value")]
        public int? Value { get; set; }

        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }

    public class Root
    {
        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("identifier")]
        public List<Identifier> Identifier { get; set; }

        [JsonProperty("code")]
        public Code Code { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("manufacturer")]
        public Manufacturer Manufacturer { get; set; }

        [JsonProperty("form")]
        public Form Form { get; set; }

        [JsonProperty("ingredient")]
        public List<Ingredient> Ingredient { get; set; }

        [JsonProperty("extension")]
        public List<Extension> Extension { get; set; }
    }

    public class Strength
    {
        [JsonProperty("numerator")]
        public Numerator Numerator { get; set; }

        [JsonProperty("denominator")]
        public Denominator Denominator { get; set; }
    }

    public class ValueCodeableConcept
    {
        [JsonProperty("coding")]
        public List<Coding> Coding { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.MedicationResponse
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Code
    {
        [JsonProperty("coding", NullValueHandling = NullValueHandling.Ignore)]
        public List<Coding> Coding { get; set; }
    }

    public class Coding
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty("display", NullValueHandling = NullValueHandling.Ignore)]
        public string Display { get; set; }

        [JsonProperty("system", NullValueHandling = NullValueHandling.Ignore)]
        public string System { get; set; }
    }

    public class Denominator
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty("system", NullValueHandling = NullValueHandling.Ignore)]
        public string System { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public int Value { get; set; }
    }

    public class Extension
    {
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }

        [JsonProperty("valueCodeableConcept", NullValueHandling = NullValueHandling.Ignore)]
        public ValueCodeableConcept ValueCodeableConcept { get; set; }
    }

    public class Form
    {
        [JsonProperty("coding", NullValueHandling = NullValueHandling.Ignore)]
        public List<Coding> Coding { get; set; }
    }

    public class Identifier
    {
        [JsonProperty("system", NullValueHandling = NullValueHandling.Ignore)]
        public string System { get; set; }

        [JsonProperty("use", NullValueHandling = NullValueHandling.Ignore)]
        public string Use { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }

    public class Ingredient
    {
        [JsonProperty("isActive", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsActive { get; set; }

        [JsonProperty("itemCodeableConcept", NullValueHandling = NullValueHandling.Ignore)]
        public ItemCodeableConcept ItemCodeableConcept { get; set; }

        [JsonProperty("strength", NullValueHandling = NullValueHandling.Ignore)]
        public Strength Strength { get; set; }
    }

    public class ItemCodeableConcept
    {
        [JsonProperty("coding", NullValueHandling = NullValueHandling.Ignore)]
        public List<Coding> Coding { get; set; }
    }

    public class Manufacturer
    {
        [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
        public string Reference { get; set; }
    }

    public class Meta
    {
        [JsonProperty("lastUpdated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("profile", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Profile { get; set; }

        [JsonProperty("versionId", NullValueHandling = NullValueHandling.Ignore)]
        public string VersionId { get; set; }
    }

    public class Numerator
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty("system", NullValueHandling = NullValueHandling.Ignore)]
        public string System { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public int Value { get; set; }
    }

    public class Root : BaseResponse
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public Code Code { get; set; }

        [JsonProperty("extension", NullValueHandling = NullValueHandling.Ignore)]
        public List<Extension> Extension { get; set; }

        [JsonProperty("form", NullValueHandling = NullValueHandling.Ignore)]
        public Form Form { get; set; }

        [JsonProperty("identifier", NullValueHandling = NullValueHandling.Ignore)]
        public List<Identifier> Identifier { get; set; }

        [JsonProperty("ingredient", NullValueHandling = NullValueHandling.Ignore)]
        public List<Ingredient> Ingredient { get; set; }

        [JsonProperty("manufacturer", NullValueHandling = NullValueHandling.Ignore)]
        public Manufacturer Manufacturer { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
    }

    public class Strength
    {
        [JsonProperty("denominator", NullValueHandling = NullValueHandling.Ignore)]
        public Denominator Denominator { get; set; }

        [JsonProperty("numerator", NullValueHandling = NullValueHandling.Ignore)]
        public Numerator Numerator { get; set; }
    }

    public class ValueCodeableConcept
    {
        [JsonProperty("coding", NullValueHandling = NullValueHandling.Ignore)]
        public List<Coding> Coding { get; set; }
    }



}

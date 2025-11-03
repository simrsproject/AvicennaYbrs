using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.MedicationDispenseResponse
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Actor
    {
        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }
    }

    public class AuthorizingPrescription
    {
        [JsonProperty("reference")]
        public string Reference { get; set; }
    }

    public class Category
    {
        [JsonProperty("coding")]
        public List<Coding> Coding { get; set; }
    }

    public class Coding
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("system")]
        public string System { get; set; }
    }

    public class Context
    {
        [JsonProperty("reference")]
        public string Reference { get; set; }
    }

    public class DaysSupply
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }

    public class DosageInstruction
    {
        [JsonProperty("doseAndRate")]
        public List<DoseAndRate> DoseAndRate { get; set; }

        [JsonProperty("sequence")]
        public int Sequence { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("timing")]
        public Timing Timing { get; set; }
    }

    public class DoseAndRate
    {
        [JsonProperty("doseQuantity")]
        public DoseQuantity DoseQuantity { get; set; }

        [JsonProperty("type")]
        public Type Type { get; set; }
    }

    public class DoseQuantity
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
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

    public class Location
    {
        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }
    }

    public class MedicationReference
    {
        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }
    }

    public class Meta
    {
        [JsonProperty("lastUpdated")]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("versionId")]
        public string VersionId { get; set; }
    }

    public class Performer
    {
        [JsonProperty("actor")]
        public Actor Actor { get; set; }
    }

    public class Quantity
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }

    public class Repeat
    {
        [JsonProperty("frequency")]
        public int Frequency { get; set; }

        [JsonProperty("period")]
        public int Period { get; set; }

        [JsonProperty("periodUnit")]
        public string PeriodUnit { get; set; }
    }

    public class Root: BaseResponse
    {
        [JsonProperty("authorizingPrescription")]
        public List<AuthorizingPrescription> AuthorizingPrescription { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("context")]
        public Context Context { get; set; }

        [JsonProperty("daysSupply")]
        public DaysSupply DaysSupply { get; set; }

        [JsonProperty("dosageInstruction")]
        public List<DosageInstruction> DosageInstruction { get; set; }

        [JsonProperty("identifier")]
        public List<Identifier> Identifier { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("medicationReference")]
        public MedicationReference MedicationReference { get; set; }


        [JsonProperty("performer")]
        public List<Performer> Performer { get; set; }

        [JsonProperty("quantity")]
        public Quantity Quantity { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("whenHandedOver")]
        public DateTime WhenHandedOver { get; set; }

        [JsonProperty("whenPrepared")]
        public DateTime WhenPrepared { get; set; }
    }

    public class Subject
    {
        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }
    }

    public class Timing
    {
        [JsonProperty("repeat")]
        public Repeat Repeat { get; set; }
    }

    public class Type
    {
        [JsonProperty("coding")]
        public List<Coding> Coding { get; set; }
    }


}

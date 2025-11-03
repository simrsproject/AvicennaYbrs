using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.MedicationRequestResponse
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AdditionalInstruction
    {
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
    }

    public class Category
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

    public class CourseOfTherapyType
    {
        [JsonProperty("coding", NullValueHandling = NullValueHandling.Ignore)]
        public List<Coding> Coding { get; set; }
    }

    public class DispenseInterval
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty("system", NullValueHandling = NullValueHandling.Ignore)]
        public string System { get; set; }

        [JsonProperty("unit", NullValueHandling = NullValueHandling.Ignore)]
        public string Unit { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public int Value { get; set; }
    }

    public class DispenseRequest
    {
        [JsonProperty("dispenseInterval", NullValueHandling = NullValueHandling.Ignore)]
        public DispenseInterval DispenseInterval { get; set; }

        [JsonProperty("expectedSupplyDuration", NullValueHandling = NullValueHandling.Ignore)]
        public ExpectedSupplyDuration ExpectedSupplyDuration { get; set; }

        [JsonProperty("numberOfRepeatsAllowed", NullValueHandling = NullValueHandling.Ignore)]
        public int NumberOfRepeatsAllowed { get; set; }

        [JsonProperty("performer", NullValueHandling = NullValueHandling.Ignore)]
        public Performer Performer { get; set; }

        [JsonProperty("quantity", NullValueHandling = NullValueHandling.Ignore)]
        public Quantity Quantity { get; set; }

        [JsonProperty("validityPeriod", NullValueHandling = NullValueHandling.Ignore)]
        public ValidityPeriod ValidityPeriod { get; set; }
    }

    public class DosageInstruction
    {
        [JsonProperty("additionalInstruction", NullValueHandling = NullValueHandling.Ignore)]
        public List<AdditionalInstruction> AdditionalInstruction { get; set; }

        [JsonProperty("doseAndRate", NullValueHandling = NullValueHandling.Ignore)]
        public List<DoseAndRate> DoseAndRate { get; set; }

        [JsonProperty("patientInstruction", NullValueHandling = NullValueHandling.Ignore)]
        public string PatientInstruction { get; set; }

        [JsonProperty("route", NullValueHandling = NullValueHandling.Ignore)]
        public Route Route { get; set; }

        [JsonProperty("sequence", NullValueHandling = NullValueHandling.Ignore)]
        public int Sequence { get; set; }

        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("timing", NullValueHandling = NullValueHandling.Ignore)]
        public Timing Timing { get; set; }
    }

    public class DoseAndRate
    {
        [JsonProperty("doseQuantity", NullValueHandling = NullValueHandling.Ignore)]
        public DoseQuantity DoseQuantity { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public Type Type { get; set; }
    }

    public class DoseQuantity
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty("system", NullValueHandling = NullValueHandling.Ignore)]
        public string System { get; set; }

        [JsonProperty("unit", NullValueHandling = NullValueHandling.Ignore)]
        public string Unit { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public int Value { get; set; }
    }

    public class Encounter
    {
        [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
        public string Reference { get; set; }
    }

    public class ExpectedSupplyDuration
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty("system", NullValueHandling = NullValueHandling.Ignore)]
        public string System { get; set; }

        [JsonProperty("unit", NullValueHandling = NullValueHandling.Ignore)]
        public string Unit { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public int Value { get; set; }
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

    public class MedicationReference
    {
        [JsonProperty("display", NullValueHandling = NullValueHandling.Ignore)]
        public string Display { get; set; }

        [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
        public string Reference { get; set; }
    }

    public class Meta
    {
        [JsonProperty("lastUpdated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("versionId", NullValueHandling = NullValueHandling.Ignore)]
        public string VersionId { get; set; }
    }

    public class Performer
    {
        [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
        public string Reference { get; set; }
    }

    public class Quantity
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty("system", NullValueHandling = NullValueHandling.Ignore)]
        public string System { get; set; }

        [JsonProperty("unit", NullValueHandling = NullValueHandling.Ignore)]
        public string Unit { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public int Value { get; set; }
    }

    public class ReasonCode
    {
        [JsonProperty("coding", NullValueHandling = NullValueHandling.Ignore)]
        public List<Coding> Coding { get; set; }
    }

    public class Repeat
    {
        [JsonProperty("frequency", NullValueHandling = NullValueHandling.Ignore)]
        public int Frequency { get; set; }

        [JsonProperty("period", NullValueHandling = NullValueHandling.Ignore)]
        public int Period { get; set; }

        [JsonProperty("periodUnit", NullValueHandling = NullValueHandling.Ignore)]
        public string PeriodUnit { get; set; }
    }

    public class Requester
    {
        [JsonProperty("display", NullValueHandling = NullValueHandling.Ignore)]
        public string Display { get; set; }

        [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
        public string Reference { get; set; }
    }

    public class Root: BaseResponse
    {
        [JsonProperty("authoredOn", NullValueHandling = NullValueHandling.Ignore)]
        public string AuthoredOn { get; set; }

        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public List<Category> Category { get; set; }

        [JsonProperty("courseOfTherapyType", NullValueHandling = NullValueHandling.Ignore)]
        public CourseOfTherapyType CourseOfTherapyType { get; set; }

        [JsonProperty("dispenseRequest", NullValueHandling = NullValueHandling.Ignore)]
        public DispenseRequest DispenseRequest { get; set; }

        [JsonProperty("dosageInstruction", NullValueHandling = NullValueHandling.Ignore)]
        public List<DosageInstruction> DosageInstruction { get; set; }

        [JsonProperty("encounter", NullValueHandling = NullValueHandling.Ignore)]
        public Encounter Encounter { get; set; }


        [JsonProperty("identifier", NullValueHandling = NullValueHandling.Ignore)]
        public List<Identifier> Identifier { get; set; }

        [JsonProperty("intent", NullValueHandling = NullValueHandling.Ignore)]
        public string Intent { get; set; }

        [JsonProperty("medicationReference", NullValueHandling = NullValueHandling.Ignore)]
        public MedicationReference MedicationReference { get; set; }

        [JsonProperty("priority", NullValueHandling = NullValueHandling.Ignore)]
        public string Priority { get; set; }

        [JsonProperty("reasonCode", NullValueHandling = NullValueHandling.Ignore)]
        public List<ReasonCode> ReasonCode { get; set; }

        [JsonProperty("requester", NullValueHandling = NullValueHandling.Ignore)]
        public Requester Requester { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
    }

    public class Route
    {
        [JsonProperty("coding", NullValueHandling = NullValueHandling.Ignore)]
        public List<Coding> Coding { get; set; }
    }

    public class Subject
    {
        [JsonProperty("display", NullValueHandling = NullValueHandling.Ignore)]
        public string Display { get; set; }

        [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
        public string Reference { get; set; }
    }

    public class Timing
    {
        [JsonProperty("repeat", NullValueHandling = NullValueHandling.Ignore)]
        public Repeat Repeat { get; set; }
    }

    public class Type
    {
        [JsonProperty("coding", NullValueHandling = NullValueHandling.Ignore)]
        public List<Coding> Coding { get; set; }
    }

    public class ValidityPeriod
    {
        [JsonProperty("end", NullValueHandling = NullValueHandling.Ignore)]
        public string End { get; set; }

        [JsonProperty("start", NullValueHandling = NullValueHandling.Ignore)]
        public string Start { get; set; }
    }



}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject
{
    public class MedicationRequestPost
    {
        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("identifier")]
        public List<Identifier> Identifier { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("intent")]
        public string Intent { get; set; }

        [JsonProperty("category")]
        public List<Category> Category { get; set; }

        [JsonProperty("priority")]
        public string Priority { get; set; }

        [JsonProperty("medicationReference")]
        public RefAndDisplay MedicationReference { get; set; }

        [JsonProperty("subject")]
        public RefAndDisplay Subject { get; set; }

        [JsonProperty("encounter")]
        public RefAndDisplay Encounter { get; set; }

        [JsonProperty("authoredOn")]
        public string AuthoredOn { get; set; }

        [JsonProperty("requester")]
        public Requester Requester { get; set; }

        [JsonProperty("reasonCode")]
        public List<ReasonCode> ReasonCode { get; set; }

        [JsonProperty("courseOfTherapyType")]
        public Code CourseOfTherapyType { get; set; }

        [JsonProperty("dosageInstruction")]
        public List<DosageInstruction> DosageInstruction { get; set; }

        [JsonProperty("dispenseRequest")]
        public DispenseRequest DispenseRequest { get; set; }
    }

    public class AdditionalInstruction
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }


    public class DispenseInterval
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }

    public class DispenseRequest
    {
        [JsonProperty("dispenseInterval")]
        public DispenseInterval DispenseInterval { get; set; }

        [JsonProperty("validityPeriod")]
        public ValidityPeriod ValidityPeriod { get; set; }

        [JsonProperty("numberOfRepeatsAllowed")]
        public int NumberOfRepeatsAllowed { get; set; }

        [JsonProperty("quantity")]
        public Quantity Quantity { get; set; }

        [JsonProperty("expectedSupplyDuration")]
        public ExpectedSupplyDuration ExpectedSupplyDuration { get; set; }

        [JsonProperty("performer")]
        public Performer Performer { get; set; }
    }

    public class DosageInstruction
    {
        [JsonProperty("sequence")]
        public int Sequence { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("additionalInstruction")]
        public List<AdditionalInstruction> AdditionalInstruction { get; set; }

        [JsonProperty("patientInstruction")]
        public string PatientInstruction { get; set; }

        [JsonProperty("timing")]
        public Timing Timing { get; set; }

        [JsonProperty("route")]
        public Route Route { get; set; }

        [JsonProperty("doseAndRate")]
        public List<DoseAndRate> DoseAndRate { get; set; }
    }

    public class DoseAndRate
    {
        [JsonProperty("type")]
        public Code Type { get; set; }

        [JsonProperty("doseQuantity")]
        public DoseQuantity DoseQuantity { get; set; }
    }

    public class DoseQuantity
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }


    public class ExpectedSupplyDuration
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }


    public class Quantity
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
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

    public class Requester
    {
        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }
    }


    public class Route
    {
        [JsonProperty("coding")]
        public List<Coding> Coding { get; set; }
    }



    public class Timing
    {
        [JsonProperty("repeat")]
        public Repeat Repeat { get; set; }
    }



    public class ValidityPeriod
    {
        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }
    }



}

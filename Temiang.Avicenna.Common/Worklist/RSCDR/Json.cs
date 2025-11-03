using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.Worklist.RSCDR
{
    public class Json
    {
        public class Order
        {
            public class JOrder
            {
                [JsonProperty("patient")]
                public Patient Patient { get; set; }

                [JsonProperty("order")]
                public JOrder2 Order2 { get; set; }
            }

            public class JOrder2
            {
                [JsonProperty("id")]
                public string Id { get; set; }

                [JsonProperty("serviceCode")]
                public string ServiceCode { get; set; }

                [JsonProperty("serviceName")]
                public string ServiceName { get; set; }

                [JsonProperty("status")]
                public string Status { get; set; }

                [JsonProperty("orderDate")]
                public string OrderDate { get; set; }

                [JsonProperty("doctor")]
                public string Doctor { get; set; }

                [JsonProperty("modality")]
                public string Modality { get; set; }

                [JsonProperty("clinicalDiagnosis")]
                public string ClinicalDiagnosis { get; set; }
            }

            public class Patient
            {
                [JsonProperty("id")]
                public string Id { get; set; }

                [JsonProperty("first_name")]
                public string FirstName { get; set; }

                [JsonProperty("middle_name")]
                public string MiddleName { get; set; }

                [JsonProperty("last_name")]
                public string LastName { get; set; }

                [JsonProperty("sex")]
                public string Sex { get; set; }

                [JsonProperty("birthDate")]
                public string BirthDate { get; set; }

                [JsonProperty("phone")]
                public string Phone { get; set; }

                [JsonProperty("address")]
                public string Address { get; set; }

                [JsonProperty("height")]
                public string Height { get; set; }

                [JsonProperty("weight")]
                public string Weight { get; set; }

                [JsonProperty("priority")]
                public string Priority { get; set; }

                [JsonProperty("department")]
                public string Department { get; set; }
            }

            public class Root
            {
                [JsonProperty("Order")]
                public JOrder Order { get; set; }
            }
        }

        public class Report
        {
            public class Order
            {
                [JsonProperty("id")]
                public string Id;

                [JsonProperty("serviceCode")]
                public string ServiceCode;

                [JsonProperty("serviceName")]
                public string ServiceName;

                [JsonProperty("status")]
                public string Status;

                [JsonProperty("orderDate")]
                public string OrderDate;

                [JsonProperty("doctor")]
                public string Doctor;

                [JsonProperty("modality")]
                public string Modality;
            }

            public class Patient
            {
                [JsonProperty("id")]
                public string Id;

                [JsonProperty("name")]
                public string Name;

                [JsonProperty("sex")]
                public string Sex;

                [JsonProperty("birthDate")]
                public string BirthDate;

                [JsonProperty("phone")]
                public string Phone;

                [JsonProperty("address")]
                public string Address;

                [JsonProperty("height")]
                public string Height;

                [JsonProperty("weight")]
                public string Weight;

                [JsonProperty("priority")]
                public string Priority;

                [JsonProperty("department")]
                public string Department;
            }

            public class JReport
            {
                [JsonProperty("patient")]
                public Patient Patient;

                [JsonProperty("order")]
                public Order Order;

                [JsonProperty("report")]
                public Report Report;
            }

            public class Report2
            {
                [JsonProperty("description")]
                public string Description;

                [JsonProperty("reportDate")]
                public string ReportDate;

                [JsonProperty("doctorID")]
                public string DoctorID;

                [JsonProperty("doctorName")]
                public string DoctorName;

                [JsonProperty("link")]
                public string Link;
            }

            public class Root
            {
                [JsonProperty("Report")]
                public JReport Report;
            }
        }
    }
}

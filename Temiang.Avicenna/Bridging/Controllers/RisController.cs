using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Bridging.Controllers
{
    public class RisController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("ris/rscdrupdate")]
        public HttpResponseMessage RSCDRUpdate(RSCDRRequest.Root requestData)
        {
            if (requestData == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    metadata = new Antrol.StatusAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = $"null"
                    }
                });

            var log = new WebServiceAPILog
            {
                DateRequest = DateTime.Now,
                IPAddress = string.Empty,
                UrlAddress = "PACS_RSCDR",
                Params = JsonConvert.SerializeObject(requestData),
                Response = string.Empty,
                Totalms = 0
            };
            log.Save();

            var tc = new TransChargesQuery("a");
            var tci = new TransChargesItemQuery("b");
            var tcic = new TransChargesItemCompQuery("c");

            tc.Select(tc.TransactionNo, tci.ItemID, tcic.ParamedicID);
            tc.InnerJoin(tci).On(tc.TransactionNo == tci.TransactionNo);
            tc.InnerJoin(tcic).On(tci.TransactionNo == tcic.TransactionNo && tci.SequenceNo == tcic.SequenceNo && tcic.TariffComponentID == "01");
            tc.Where($"<a.TransactionNo + '-' + cast(cast(b.SequenceNo as int) as varchar(max)) = '{requestData.Report.Order.Id}'>");

            var tbl = tc.LoadDataTable();
            if (tbl.Rows.Count > 0)
            {
                var result = new TestResult();
                if (!result.LoadByPrimaryKey(tbl.Rows[0]["TransactionNo"].ToString(), tbl.Rows[0]["ItemID"].ToString())) result = new TestResult();
                result.TransactionNo = tbl.Rows[0]["TransactionNo"].ToString();
                result.ItemID = tbl.Rows[0]["ItemID"].ToString();
                result.ParamedicID = tbl.Rows[0]["ParamedicID"].ToString();
                result.TestResultDateTime = DateTime.Now;
                result.TestResult = requestData.Report.Expertise.Description;
                result.LastUpdateByUserID = "WEBSERVICE";
                result.LastUpdateDateTime = DateTime.Now;
                result.Save();

                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    metadata = new Antrol.StatusAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.OK,
                        Message = $"No transaksi {requestData.Report.Order.Id} berhasil di update"
                    }
                });
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, new
            {
                metadata = new Antrol.StatusAntrean.Response.Metadata()
                {
                    Code = (int)HttpStatusCode.NotFound,
                    Message = $"No transaksi {requestData.Report.Order.Id} tidak ditemukan"
                }
            });
        }

        public class RSCDRRequest
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

            public class Report
            {
                [JsonProperty("patient")]
                public Patient Patient;

                [JsonProperty("order")]
                public Order Order;

                [JsonProperty("report")]
                public Expertise Expertise;
            }

            public class Expertise
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
                public Report Report;
            }
        }
    }
}
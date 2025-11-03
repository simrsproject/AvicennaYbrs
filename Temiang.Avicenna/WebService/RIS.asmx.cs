using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for RIS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class RIS : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void UpdateExpertiseRSTJ(string notransaksi, string expertise)
        {
            var log = new WebServiceAPILog
            {
                DateRequest = DateTime.Now,
                IPAddress = string.Empty,
                UrlAddress = "UpdateExpertiseRSTJ",
                Params = JsonConvert.SerializeObject(new 
                { 
                    notransaksi,
                    expertise
                }),
                Response = string.Empty,
                Totalms = 0
            };
            log.Save();

            var json = new JsonRetWS();

            //var request = JsonConvert.DeserializeObject<RequestRSTJ>();
            //if (request == null)
            //{
            //    Context.Response.Write(json.JSonRetFormatted("Request parameter is null", false, "400"));
            //}

            if (string.IsNullOrWhiteSpace(notransaksi) || string.IsNullOrWhiteSpace(expertise))
            {
                Context.Response.Write(json.JSonRetFormatted("Request parameter is null", false, "400"));
            }

            var ids = notransaksi.Split('-');

            var tc = new TransChargesQuery("a");
            var tci = new TransChargesItemQuery("b");
            var tcic = new TransChargesItemCompQuery("c");

            tc.Select(tc.TransactionNo, tci.ItemID, tcic.ParamedicID);
            tc.InnerJoin(tci).On(tc.TransactionNo == tci.TransactionNo);
            tc.InnerJoin(tcic).On(tci.TransactionNo == tcic.TransactionNo && tci.SequenceNo == tcic.SequenceNo && tcic.TariffComponentID == "01");
            //tc.Where(tc.TransactionNo == string.Concat(ids[0], "-", ids[1]), tci.SequenceNo == ids[2]);
            tc.Where($"<replace(a.TransactionNo, '-', '') + cast(cast(b.SequenceNo as int) as varchar(max)) = '{notransaksi}'>");

            var tbl = tc.LoadDataTable();
            if (tbl.Rows.Count == 0)
            {
                Context.Response.Write(json.JSonRetFormatted("No data is available", false, "400"));
            }

            var result = new TestResult();
            if (!result.LoadByPrimaryKey(string.Concat(ids[0], "-", ids[1]), tbl.Rows[0]["ItemID"].ToString()))
            {
                result = new TestResult();
                result.TransactionNo = tbl.Rows[0]["TransactionNo"].ToString();
                result.ItemID = tbl.Rows[0]["ItemID"].ToString();
                result.ParamedicID = tbl.Rows[0]["ParamedicID"].ToString();
                result.TestResultDateTime = DateTime.Now;
                result.TestResult = expertise;
            }
            else result.TestResult = expertise;
            result.LastUpdateByUserID = "WEBSERVICE";
            result.LastUpdateDateTime = DateTime.Now;
            result.Save();

            Context.Response.Write(json.JSonRetFormatted("Expertise updated", true, string.Empty));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void UpdateExpertiseRSMMP(string notransaksi, string studyUid, string expertise)
        {
            var json = new JsonRetWS();

            //var request = JsonConvert.DeserializeObject<RequestRSTJ>();
            //if (request == null)
            //{
            //    Context.Response.Write(json.JSonRetFormatted("Request parameter is null", false, "400"));
            //}

            if (string.IsNullOrWhiteSpace(notransaksi) || string.IsNullOrWhiteSpace(expertise))
            {
                Context.Response.Write(json.JSonRetFormatted("Request parameter is null", false, "400"));
            }
            else
            {
                var ids = notransaksi.Split('#');

                var tc = new TransChargesQuery("a");
                var tci = new TransChargesItemQuery("b");
                var tcic = new TransChargesItemCompQuery("c");

                tc.Select(tc.TransactionNo, tci.ItemID, "< ISNULL(c.ParamedicID, b.ParamedicID) AS ParamedicID>");
                tc.InnerJoin(tci).On(tc.TransactionNo == tci.TransactionNo);
                tc.LeftJoin(tcic).On(tci.TransactionNo == tcic.TransactionNo && tci.SequenceNo == tcic.SequenceNo && tcic.TariffComponentID == "01");
                tc.Where(tc.TransactionNo == ids[0], tci.SequenceNo == ids[1]);

                var tbl = tc.LoadDataTable();
                if (tbl.Rows.Count == 0)
                {
                    Context.Response.Write(json.JSonRetFormatted("No data is available", false, "404"));
                }
                else
                {
                    var tsi = new TransChargesItem();
                    if (tsi.LoadByPrimaryKey(ids[0], ids[1]))
                    {
                        tsi.ResultValue = studyUid;
                        tsi.Save();
                    }

                    var result = new TestResult();
                    if (!result.LoadByPrimaryKey(string.Concat(ids[0], "-", ids[1]), tbl.Rows[0]["ItemID"].ToString()))
                    {
                        result = new TestResult();
                        result.TransactionNo = tbl.Rows[0]["TransactionNo"].ToString();
                        result.ItemID = tbl.Rows[0]["ItemID"].ToString();
                        result.ParamedicID = tbl.Rows[0]["ParamedicID"].ToString();
                        result.TestResultDateTime = DateTime.Now;
                        result.TestResult = expertise;
                    }
                    else result.TestResult = expertise;
                    result.LastUpdateByUserID = "WEBSERVICE";
                    result.LastUpdateDateTime = DateTime.Now;
                    result.Save();

                    Context.Response.Write(json.JSonRetFormatted("Expertise updated", true, string.Empty));
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void UpdateStudyUidRSMMP(string notransaksi, string studyUid)
        {
            var json = new JsonRetWS();

            //var request = JsonConvert.DeserializeObject<RequestRSTJ>();
            //if (request == null)
            //{
            //    Context.Response.Write(json.JSonRetFormatted("Request parameter is null", false, "400"));
            //}

            if (string.IsNullOrWhiteSpace(notransaksi) || string.IsNullOrWhiteSpace(studyUid))
            {
                Context.Response.Write(json.JSonRetFormatted("Request parameter is null", false, "400"));
            }
            else
            {
                var ids = notransaksi.Split('#');

                //var tc = new TransChargesQuery("a");
                //var tci = new TransChargesItemQuery("b");
                //var tcic = new TransChargesItemCompQuery("c");

                //tc.Select(tc.TransactionNo, tci.ItemID, tcic.ParamedicID);
                //tc.InnerJoin(tci).On(tc.TransactionNo == tci.TransactionNo);
                //tc.InnerJoin(tcic).On(tci.TransactionNo == tcic.TransactionNo && tci.SequenceNo == tcic.SequenceNo && tcic.TariffComponentID == "01");
                //tc.Where(tc.TransactionNo == ids[0], tci.SequenceNo == ids[1]);

                //var tbl = tc.LoadDataTable();

                var tsi = new TransChargesItem();
                if (!tsi.LoadByPrimaryKey(ids[0], ids[1]))
                {
                    Context.Response.Write(json.JSonRetFormatted("No data is available", false, "404"));
                }
                else
                {
                    tsi.ResultValue = studyUid;
                    tsi.Save();

                    Context.Response.Write(json.JSonRetFormatted("Study Uid updated", true, string.Empty));
                }
            }
        }

        public class RequestRSTJ
        {
            [JsonProperty("notransaksi")]
            public string NoTransaksi { get; set; }

            [JsonProperty("expertise")]
            public string Expertise { get; set; }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void UpdateExpertiseRSTS()
        {
            var success = new List<string>();
            var fail = new List<string>();
            foreach (DataRow item in new TransChargesItemCollection().GetRadiologyExamRsts().Rows) 
            {
                var svc = new Common.Worklist.RSTS.Service();
                if (svc.Read(new Common.Worklist.RSTS.Data() { ordercode = $"{item["TransactionNo"]}_{item["SequenceNo"]}" })) success.Add($"{item["TransactionNo"]}_{item["SequenceNo"]}");
                else fail.Add($"{item["TransactionNo"]}_{item["SequenceNo"]}");
            }
            var js = new System.Web.Script.Serialization.JavaScriptSerializer
            {
                MaxJsonLength = 2147483644
            };
            Context.Response.Write(js.Serialize(new
            {
                success,
                fail
            }));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object LoadStudyUidRSMMP(string transactionNo, string sequenceNo, string studyUid)
        {
            var svc = new Common.Worklist.RSMMP.Service();
            var status = svc.GetImageStatus(transactionNo, sequenceNo, studyUid);
            return status;
        }
    }
}

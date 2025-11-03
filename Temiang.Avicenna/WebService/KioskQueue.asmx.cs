using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Configuration;
using System.Xml.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using fastJSON;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for KioskQueue
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class KioskQueue : V0.BaseDataService //JsonRetWS //System.Web.Services.WebService
    {
        public class KioskQueueLast
        {
            public string kioskQueueType;
            public string last;
            public string next;
            public int count;
        }
        public class KioskCounter
        {
            public string KioskQueueNo;
            public string ProcessByUserID;
            public string LastCounterName;
            public int DataCounter;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetAjaxCounter()
        {
            Context.Response.Write(JSonRetFormatted(AppSession.Parameter.AjaxCounter));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void QueueClear(string jSonKioskQueueType, string UserID)
        {
            var oJson = Helper.JsonStrToArray(jSonKioskQueueType);
            var KioskQueueType = (oJson["kode"] as List<object>);

            var kQueColl = new KioskQueueCollection();
            kQueColl.Query.Where(kQueColl.Query.SRKioskQueueStatus == "01",
                kQueColl.Query.KioskQueueDate.Date() < DateTime.Now.Date,
                kQueColl.Query.KioskQueueCode.In(KioskQueueType.Select(x => x.ToString()).ToArray()));

            var i = 0;
            if (kQueColl.LoadAll())
            {
                foreach (var k in kQueColl)
                {
                    k.SRKioskQueueStatus = "03";
                    i++;
                }
            }
            kQueColl.Save();

            // clean the idle last 1 hour
            kQueColl = new KioskQueueCollection();
            kQueColl.Query.Where(kQueColl.Query.SRKioskQueueStatus == "02",
                "<DATEDIFF(hour, ProcessDateTime, GETDATE()) > 2>",
                kQueColl.Query.KioskQueueCode.In(KioskQueueType.Select(x => x.ToString()).ToArray()));

            if (kQueColl.LoadAll())
            {
                foreach (var k in kQueColl)
                {
                    k.SRKioskQueueStatus = "03";
                    i++;
                }
            }
            kQueColl.Save();

            Context.Response.Write(JSonRetFormatted(i));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetQueueOutstandingList(string[] KioskQueueType)
        {
            var kQueColl = new KioskQueueCollection();
            if (KioskQueueType.Length > 0)
            {
                kQueColl.Query.Where(kQueColl.Query.KioskQueueCode.In(KioskQueueType));
            }
            else
            {
                kQueColl.Query.Where(kQueColl.Query.KioskQueueCode == string.Empty);
            }
            kQueColl.Query.Where(kQueColl.Query.SRKioskQueueStatus == "01");

            kQueColl.LoadAll();

            return JSonRetFormatted(kQueColl);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetLastQueue(string jSonKioskQueueType, string UserID)
        {
            var oJson = Helper.JsonStrToArray(jSonKioskQueueType);
            var KioskQueueType = (oJson["kode"] as List<object>);
            GetLastQueue(KioskQueueType, UserID, string.Empty);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetLastQueueByRefIDWithCounterUpdate(string RefID, string LastData)
        {
            GetLastQueueByRefID_do(RefID, LastData);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetLastQueueByRefID(string RefID)
        {
            GetLastQueueByRefID_do(RefID, string.Empty);
        }
        private void GetLastQueueByRefID_do(string RefID, string LastData)
        {
            var refids = RefID.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var apstdi = new AppStandardReferenceItemCollection();
            apstdi.LoadByStandardReferenceID("KioskQueueType", 0);

            var KioskQueueType = apstdi.Where(x => refids.Contains(x.ReferenceID) || refids.Count() == 0)
                .Select(x => x.ItemID).ToList();

            // child
            var stdChld = new AppStandardReferenceQuery("a");
            var stdiChld = new AppStandardReferenceItemQuery("b");
            stdChld.InnerJoin(stdiChld).On(stdChld.StandardReferenceID == stdiChld.StandardReferenceID)
                .Where(stdChld.StandardReferenceGroup == "KioskQueueType",
                    stdChld.IsActive == true, stdiChld.IsActive == true
                ).Select(
                    stdiChld.ItemID, stdiChld.ItemName, stdiChld.ReferenceID
                );
            if (refids.Count() > 0)
            {
                stdChld.Where(stdiChld.ReferenceID.In(KioskQueueType));
            }
            var tbl = stdChld.LoadDataTable();

            var KioskQueueType2 = KioskQueueType.Select(x => x as object).Union(
                    tbl.AsEnumerable().Select(y => y["ItemID"])
                ).ToList();

            GetLastQueue(KioskQueueType2, string.Empty, LastData);
        }

        private void GetLastQueue(List<object> KioskQueueType, string UserID, string PrevJsonData)
        {
            List<object> que = new List<object>();
            if (KioskQueueType.Count() > 0)
            {
                var qq = (new KioskQueueCollection()).GetFirstLastData(KioskQueueType.Select(x => x.ToString()).ToArray());
                foreach (var kO in KioskQueueType)
                {
                    var k = kO.ToString();
                    string Next = string.Empty, Last = string.Empty;
                    int Count = 0;
                    var r1st = qq.AsEnumerable().Where(d => d["KioskQueueCode"].ToString() == k && System.Convert.ToInt32(d["oasc"]) == 1).FirstOrDefault();
                    if (r1st != null)
                    {
                        Next = r1st["KioskQueueNo"].ToString();

                        var rLast = qq.AsEnumerable().Where(d => d["KioskQueueCode"].ToString() == k && System.Convert.ToInt32(d["odesc"]) == 1).FirstOrDefault();
                        Last = rLast["KioskQueueNo"].ToString();

                        Count = System.Convert.ToInt32(rLast["oasc"]);
                    }

                    var o = new KioskQueueLast();
                    o.kioskQueueType = k; o.next = Next; o.last = Last; o.count = Count;
                    que.Add(o);
                }

                List<object> ret = new List<object>();
                ret.Add(que);

                // prev data
                List<object> prevQue = new List<object>();
                if (!string.IsNullOrEmpty(PrevJsonData))
                {
                    prevQue = JsonStrToArrayList(PrevJsonData);
                }

                var kColl = new KioskQueueCollection();
                kColl.Query.Where(kColl.Query.KioskQueueCode.In(KioskQueueType), kColl.Query.SRKioskQueueStatus == "02")
                    .OrderBy(kColl.Query.KioskQueueID.Descending);
                if (!string.IsNullOrEmpty(UserID))
                {
                    kColl.Query.Where(kColl.Query.ProcessByUserID == UserID);
                }

                if (kColl.LoadAll())
                {
                    var uColl = new AppUserCollection();
                    uColl.Query.Where(uColl.Query.UserID.In(kColl.Select(x => x.ProcessByUserID)));
                    uColl.LoadAll();

                    var xxx = kColl.Select(r => new KioskCounter()
                    {
                        KioskQueueNo = r.KioskQueueNo,
                        ProcessByUserID = r.ProcessByUserID,
                        LastCounterName = uColl.Where(x => x.UserID == r.ProcessByUserID).Select(x => x.LastCounterName).FirstOrDefault(), //r["LastCounterName"].ToString(),
                        DataCounter = (r.Recall ?? false) ? 0 : (prevQue.Where(p =>
                            (p as System.Collections.Generic.Dictionary<string, object>)["KioskQueueNo"].ToString() == r.KioskQueueNo)
                            .Select(p => System.Convert.ToInt32((p as System.Collections.Generic.Dictionary<string, object>)["DataCounter"]) + 1)).FirstOrDefault()
                    }).ToArray();
                    ret.Add(xxx);

                    if (prevQue.Any())
                    {
                        // update recalled
                        foreach (var k in kColl)
                        {
                            if (k.Recall ?? false) k.Recall = false;
                        }
                        kColl.Save();
                    }
                }
                else
                {
                    ret.Add(new List<object>() { new KioskCounter(){
                        KioskQueueNo = "0",
                        ProcessByUserID = UserID,
                        LastCounterName = string.Empty,
                        DataCounter = 0
                    }});
                }

                //ret.Add(new { currentNo= curr }); // string.Format("{{ \"currentNo\":\"{0}\" }}", curr));

                Context.Response.Write(JSonRetFormatted(ret));
            }
            else
            {
                Context.Response.Write(JSonRetFormatted("Empty KioskQueueType Selection", false));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NextQueue(string jSonKioskQueueType, string CurrentNo, string UserID)
        {
            var oJson = Helper.JsonStrToArray(jSonKioskQueueType);
            var KioskQueueType = (oJson["kode"] as List<object>);

            var kQueColl = new KioskQueueCollection();
            kQueColl.Query.Where(kQueColl.Query.SRKioskQueueStatus == "02", kQueColl.Query.ProcessByUserID == UserID);
            if (kQueColl.LoadAll())
            {
                foreach (var k in kQueColl)
                {
                    k.SRKioskQueueStatus = "03";
                }
            }
            // booking next
            var kQueNextColl = new KioskQueueCollection();
            kQueNextColl.Query.Where(kQueNextColl.Query.KioskQueueCode.In(KioskQueueType),
                kQueNextColl.Query.SRKioskQueueStatus == "01")
                .OrderBy(kQueNextColl.Query.KioskQueueID.Ascending);
            kQueNextColl.Query.es.Top = 1;
            if (kQueNextColl.LoadAll())
            {
                var kQueNext = kQueNextColl.First();
                kQueNext.SRKioskQueueStatus = "02";
                kQueNext.ProcessByUserID = UserID;
                kQueNext.ProcessDateTime = (new DateTime()).NowAtSqlServer();
                kQueColl.AttachEntity(kQueNext);
            }

            kQueColl.Save();

            GetLastQueue(jSonKioskQueueType, UserID);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RecallQueue(string CurrentNo, string UserID)
        {
            var kQueColl = new KioskQueueCollection();
            kQueColl.Query.Where(
                kQueColl.Query.KioskQueueNo == CurrentNo,
                kQueColl.Query.SRKioskQueueStatus == "02",
                kQueColl.Query.ProcessByUserID == UserID);
            if (kQueColl.LoadAll())
            {
                foreach (var k in kQueColl)
                {
                    k.Recall = true;
                }
            }

            kQueColl.Save();

            Context.Response.Write(JSonRetFormatted("Done"));
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void StopQueue(string UserID)
        {
            StopQueueCaller(UserID);

            Context.Response.Write(JSonRetFormatted("Stopped"));
        }

        public static void StopQueueCaller(string UserID)
        {
            var kQueColl = new KioskQueueCollection();
            kQueColl.Query.Where(kQueColl.Query.SRKioskQueueStatus == "02", kQueColl.Query.ProcessByUserID == UserID);
            if (kQueColl.LoadAll())
            {
                foreach (var k in kQueColl)
                {
                    k.SRKioskQueueStatus = "03";
                }
            }

            kQueColl.Save();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetQueueCode(string RefID)
        {
            var refids = RefID.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var apstdi = new AppStandardReferenceItemCollection();
            apstdi.LoadByStandardReferenceID("KioskQueueType", 0);

            var stdiColl = new AppStandardReferenceItemCollection();
            stdiColl.LoadByStdRefGroup("KioskQueueType");

            var ret = apstdi.Where(x => RefID.Contains(x.ReferenceID) || refids.Count() == 0)
                .Select(x => new
                {
                    ItemID = x.ItemID,
                    ItemName = HtmlTagHelper.Validate2(x.ItemName).Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)[0],
                    ItemNameEn = HtmlTagHelper.Validate2(x.ItemName).Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).Last(),
                    RefID = x.ReferenceID,
                    CustomField = x.CustomField,
                    ChildCount = (stdiColl.Where(y => y.ReferenceID == x.ItemID).Count())
                })
                .OrderBy(x => x.RefID).ThenBy(x => x.ItemID);

            Context.Response.Write(JSonRetFormatted(ret));
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetQueueCodeChild(string RefID)
        {
            var stdiColl = new AppStandardReferenceItemCollection();
            stdiColl.LoadByStdRefGroup("KioskQueueType", RefID);

            var ret = stdiColl.Select(x => new
            {
                ItemID = x.ItemID,
                ItemName = HtmlTagHelper.Validate2(x.ItemName).Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)[0],
                ItemNameEn = HtmlTagHelper.Validate2(x.ItemName).Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).Last(),
                RefID = x.ReferenceID,
                CustomField = x.CustomField,
                ChildCount = 0
            })
                .OrderBy(x => x.RefID);

            Context.Response.Write(JSonRetFormatted(ret));
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetQueueCodeUnionChild(string RefID)
        {
            var refids = RefID.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var apstdi = new AppStandardReferenceItemCollection();
            apstdi.LoadByStandardReferenceID("KioskQueueType", 0);

            var stdChld = new AppStandardReferenceQuery("a");
            var stdiChld = new AppStandardReferenceItemQuery("b");
            stdChld.InnerJoin(stdiChld).On(stdChld.StandardReferenceID == stdiChld.StandardReferenceID)
                .Where(stdChld.StandardReferenceGroup == "KioskQueueType",
                    stdChld.IsActive == true, stdiChld.IsActive == true
                ).Select(
                    stdiChld.ItemID, stdiChld.ItemName, stdiChld.ReferenceID
                );
            var tbl = stdChld.LoadDataTable();

            var ret = apstdi.Where(x => RefID.Contains(x.ReferenceID) || refids.Count() == 0)
                .Select(x => new
                {
                    ItemID = x.ItemID,
                    ItemName = HtmlTagHelper.Validate2(x.ItemName),
                    RefID = x.ReferenceID,
                    CustomField = x.CustomField,
                    ChildCount = (tbl.AsEnumerable().Where(y => y["ReferenceID"].ToString() == x.ItemID).Count())
                })
                .OrderBy(x => x.RefID);

            // 
            stdChld = new AppStandardReferenceQuery("a");
            stdiChld = new AppStandardReferenceItemQuery("b");
            var stdRef = new AppStandardReferenceItemQuery("c");

            stdChld.InnerJoin(stdiChld).On(stdChld.StandardReferenceID == stdiChld.StandardReferenceID)
                .InnerJoin(stdRef).On(stdRef.ItemID == stdiChld.ReferenceID && stdRef.StandardReferenceID == "KioskQueueType")
                .Where(stdChld.StandardReferenceGroup == "KioskQueueType",
                    stdChld.IsActive == true, stdiChld.IsActive == true
                ).Select(
                    stdiChld.ItemID, stdiChld.ItemName, stdiChld.ReferenceID, stdiChld.CustomField
                );
            if (refids.Count() > 0)
            {
                stdChld.Where(stdRef.ReferenceID.In(refids));
            }
            tbl = stdChld.LoadDataTable();

            var ret2 = tbl.AsEnumerable().Select(x => new
            {
                ItemID = x["ItemID"].ToString(),
                ItemName = HtmlTagHelper.Validate2(x["ItemName"].ToString()),
                RefID = x["ReferenceID"].ToString(),
                CustomField = x["CustomField"].ToString(),
                ChildCount = 0
            })
                .OrderBy(x => x.RefID);

            var retU = ret.Union(ret2).Where(r => r.ChildCount == 0).ToList();

            Context.Response.Write(JSonRetFormatted(retU));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void QueueAdd(string code)
        {
            var ret = QueueAdd((new DateTime()).NowAtSqlServer(), code, "WebService", true, false);

            Context.Response.Write(JSonRetFormatted(ret.KioskQueueNo));
        }
        public static BusinessObject.KioskQueue QueueAdd(DateTime xDate, string code, string UserID, bool IsAutoPrint, bool isAutoStatusSkipped)
        {
            // command goes here
            // var xDate = (new DateTime()).NowAtSqlServer();
            AppAutoNumberLast AutoNoQueue = Helper.GetNewAutoNumber(xDate, AppEnum.AutoNumber.KioskQueueNo, code, UserID);
            var lastno = AutoNoQueue.LastCompleteNumber.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

            var kno = new BusinessObject.KioskQueue();
            kno.AddNew();
            kno.KioskQueueNo = string.Join("-", lastno[0], lastno[3]);
            kno.KioskQueueCode = code;
            kno.KioskQueueDate = xDate.Date;
            kno.SRKioskQueueStatus = isAutoStatusSkipped ? "04" : "01"; /*booked*/
            kno.CreateByUserID = UserID;// "sci";
            kno.CreateDateTime = xDate;
            kno.LastUpdateByUserID = UserID; //"sci";
            kno.LastUpdateDateTime = xDate;

            //using (var trans = new esTransactionScope())
            {
                AutoNoQueue.Save();
                kno.Save();

                //trans.Complete();
            }

            if (IsAutoPrint)
            {
                var pSlip = new PrintJobParameterCollection();
                pSlip.AddNew("p_KioskQueueNo", kno.KioskQueueNo, null, null);
                PrintManager.CreatePrintJob(AppSession.Parameter.KioskQueueSlipRpt, pSlip, UserID,
                    HttpContext.Current.Request.UserHostName);
            }

            return kno;

            //#region AutoPrint
            //var sfi = new AppStandardReferenceItem();
            //if (!sfi.LoadByPrimaryKey("KioskQueueType", code))
            //{
            //    // cari di anaknya
            //    var sfiColl = new AppStandardReferenceItemCollection();
            //    var sfiQ = new AppStandardReferenceItemQuery("sfi");
            //    var sfQ = new AppStandardReferenceQuery("sf");
            //    sfiQ.InnerJoin(sfQ).On(sfiQ.StandardReferenceID == sfQ.StandardReferenceID)
            //        .Where(sfQ.StandardReferenceGroup == "KioskQueueType", sfiQ.ItemID == code);
            //    if (sfiColl.Load(sfiQ))
            //    {
            //        sfi = sfiColl.FirstOrDefault();
            //    }
            //}
            //if (!string.IsNullOrEmpty(sfi.ItemID) && !string.IsNullOrEmpty(sfi.CustomField2))
            //{
            //    var pSlip = new PrintJobParameterCollection();
            //    pSlip.AddNew("p_KioskQueueNo", kno.KioskQueueNo, null, null);
            //    //PrintManager.CreatePrintJob(AppSession.Parameter.KioskQueueSlipRpt, pSlip, UserID,
            //    //    HttpContext.Current.Request.UserHostName);
            //    PrintManager.CreatePrintJob(sfi.CustomField2, pSlip, UserID,
            //        HttpContext.Current.Request.UserHostName);
            //}
            //#endregion
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SaveCounterName(string CounterName, string UserID)
        {
            var u = new AppUser();
            if (u.LoadByPrimaryKey(UserID))
            {
                u.LastCounterName = CounterName;
                u.Save();
                Context.Response.Write(JSonRetFormatted(u.UserID));
            }
            else
            {
                Context.Response.Write(JSonRetFormatted("Data not found", false));
            }
        }

        // prescription
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PrescriptionGetQueue(string IpAddress, string ServiceUnitID, bool IsComplete)
        {
            var dtb = (new TransPrescriptionCollection())
                .GetQueueWithPaging(IpAddress, ServiceUnitID, IsComplete);

            Context.Response.Write(JSonRetFormatted(ConvertDataTabletoObject(dtb)));
        }

        // prescription
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PrescriptionGetQueue3Col(string IpAddress, string ServiceUnitID, int iProgress)
        {
            var dtb = (new TransPrescriptionCollection())
                .GetQueue3ColWithPaging(IpAddress, ServiceUnitID, iProgress);

            Context.Response.Write(JSonRetFormatted(ConvertDataTabletoObject(dtb)));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PrescriptionGetQueue6Col(string IpAddress, string ServiceUnitID, int iProgress)
        {
            var dtb = (new TransPrescriptionCollection())
                .GetQueue6ColWithPaging(IpAddress, ServiceUnitID, iProgress);

            Context.Response.Write(JSonRetFormatted(ConvertDataTabletoObject(dtb)));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PrescriptionGetQueueByCode(string IpAddress, string ServiceUnitID, string QueueCode)
        {
            var dtb = (new TransPrescriptionCollection())
                .GetQueueByCodeWithPaging(IpAddress, ServiceUnitID, QueueCode);
            Context.Response.Write(JSonRetFormatted(ConvertDataTabletoObject(dtb)));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PrescriptionGetOneByKeyword(string Keyword)
        {
            var tpq = new TransPrescriptionQuery("tpq");
            var rq = new RegistrationQuery("rq");
            var pq = new PatientQuery("pq");
            var prq = new ParamedicQuery("prq");
            var suq = new ServiceUnitQuery("suq");

            tpq.InnerJoin(rq).On(tpq.RegistrationNo == rq.RegistrationNo)
                .InnerJoin(pq).On(rq.PatientID == pq.PatientID)
                .InnerJoin(prq).On(tpq.ParamedicID == prq.ParamedicID)
                .InnerJoin(suq).On(tpq.FromServiceUnitID == suq.ServiceUnitID)
                .Where(tpq.KioskQueueNo == Keyword)
                .Select(
                    tpq.PrescriptionNo, tpq.PrescriptionDate,
                    tpq.RegistrationNo,
                    "<RTRIM(RTRIM(pq.FirstName + ' ' + pq.MiddleName) + ' ' + pq.Lastname) as PatientName >",
                    pq.DateOfBirth, prq.ParamedicName, suq.ServiceUnitName,
                    @"<CASE WHEN (tpq.DeliverDateTime IS NOT NULL) THEN 4 ELSE (
                        CASE WHEN(tpq.CompleteDateTime IS NOT NULL) THEN 3 ELSE(
                            CASE WHEN(tpq.IsProceedByPharmacist = 1) THEN 2 ELSE(
                                CASE WHEN(tpq.ApprovalDateTime IS NOT NULL) THEN 1 ELSE 0 END
                            ) END
                        ) END
                    ) END as Status >"
                );
            var dtb = tpq.LoadDataTable();
            if (dtb.Rows.Count == 0)
            {
                tpq = new TransPrescriptionQuery("tpq");
                tpq.InnerJoin(rq).On(tpq.RegistrationNo == rq.RegistrationNo)
                    .InnerJoin(pq).On(rq.PatientID == pq.PatientID)
                    .InnerJoin(prq).On(tpq.ParamedicID == prq.ParamedicID)
                    .InnerJoin(suq).On(tpq.FromServiceUnitID == suq.ServiceUnitID)
                    .Where(tpq.PrescriptionNo == Keyword)
                    .OrderBy(tpq.PrescriptionDate.Descending)
                    .Select(
                        tpq.PrescriptionNo, tpq.PrescriptionDate,
                        tpq.RegistrationNo,
                        "<RTRIM(RTRIM(pq.FirstName + ' ' + pq.MiddleName) + ' ' + pq.Lastname) as PatientName >",
                        pq.DateOfBirth, prq.ParamedicName, suq.ServiceUnitName,
                        @"<CASE WHEN (tpq.DeliverDateTime IS NOT NULL) THEN 4 ELSE (
                            CASE WHEN(tpq.CompleteDateTime IS NOT NULL) THEN 3 ELSE(
                                CASE WHEN(tpq.IsProceedByPharmacist = 1) THEN 2 ELSE(
                                    CASE WHEN(tpq.ApprovalDateTime IS NOT NULL) THEN 1 ELSE 0 END
                                ) END
                            ) END
                        ) END as Status >"
                    );

                dtb = tpq.LoadDataTable();
                if (dtb.Rows.Count == 0)
                {

                }
            }
            if (dtb.Rows.Count > 0)
            {
                Context.Response.Write(JSonRetFormatted(ConvertDataRowtoObject(dtb.Rows[0])));
            }
            else
            {
                Context.Response.Write(JSonRetFormatted("Data not found", false));
            }
        }

        #region Bed Monitoring
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BedMonitoringGetList(string IpAddress)
        {
            var dtb = (new BedCollection()).GetBedInformationSummaryMF(IpAddress);
            Context.Response.Write(JSonRetFormatted(ConvertDataTabletoObject(dtb)));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BedMonitoringGetListV2(string IpAddress)
        {
            var dtb = (new BedCollection()).GetBedInformationSummaryMF_Pandemi(IpAddress);
            Context.Response.Write(JSonRetFormatted(ConvertDataTabletoObject(dtb)));
        }
        #endregion

        #region Operating theater
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void OperatingTheaterGetList(string IpAddress)
        {
            var dtb = (new ServiceUnitBookingCollection()).GetQueueOutstanding(IpAddress);
            Context.Response.Write(JSonRetFormatted(ConvertDataTabletoObject(dtb)));
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void OperatingTheaterGetList2(string IpAddress)
        {
            var dtb = (new ServiceUnitBookingCollection()).GetQueueOutstandingV2(IpAddress);
            Context.Response.Write(JSonRetFormatted(ConvertDataTabletoObject(dtb)));
        }
        #endregion

        #region PHR
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PhrSave()
        {
            DateTime DateServer = (new DateTime()).NowAtSqlServer();

            var qfid = HttpContext.Current.Request.QueryString["qfid"];
            if (string.IsNullOrEmpty(qfid))
            {
                Context.Response.Write(JSonRetFormatted("Invalid Question Form", false));
                return;
            }

            var qf = new QuestionForm();
            if (!qf.LoadByPrimaryKey(qfid))
            {
                Context.Response.Write(JSonRetFormatted("Question Form Not Found", false));
                return;
            }

            var keys = HttpContext.Current.Request.QueryString.AllKeys;
            var questions = new QuestionCollection();
            if (!questions.LoadByFormID(qfid))
            {
                Context.Response.Write(JSonRetFormatted("No Questions Found in Form", false));
                return;
            }

            var cboType = (new string[] { "cbo", "cbt", "cb2" });

            var qas = questions.Where(q => cboType.Contains(q.SRAnswerType.ToLower()))
                .Select(q => q.QuestionAnswerSelectionID).ToList().Distinct().ToList();
            qas.Remove(string.Empty);

            var qaslColl = new QuestionAnswerSelectionLineCollection();
            if (qas.Count > 0)
            {
                qaslColl.Query.Where(qaslColl.Query.QuestionAnswerSelectionID.In(qas));
                qaslColl.LoadAll();
            }

            // validasi question
            // validasi mandatory
            var oColl = questions.Where(x => (x.IsMandatory ?? true) &&
            !(new string[] { "lbl" }).Contains(x.SRAnswerType.ToLower()) &&
            (!keys.Contains(x.QuestionID) || (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[x.QuestionID]))));
            if (oColl.Any())
            {
                Context.Response.Write(JSonRetFormatted(new { ErrorType = "FieldValidation", Keys = (oColl.Select(x => x.QuestionID)) }, false));
                return;
            }

            var phrlColl = new PatientHealthRecordLineCollection();
            var phr = new PatientHealthRecord();
            var isNew = true;
            var transno = HttpContext.Current.Request.QueryString["transno"];
            if (!string.IsNullOrEmpty(transno))
            {
                isNew = !phr.LoadByPrimaryKey(transno, string.Empty, qfid);
                if (!isNew)
                {
                    phrlColl.Query.Where(phrlColl.Query.TransactionNo == transno,
                        phrlColl.Query.QuestionFormID == qfid);
                    phrlColl.LoadAll();
                }
            }

            foreach (var aKey in HttpContext.Current.Request.QueryString.AllKeys.Where(x =>
            (questions.Select(y => y.QuestionID)).Contains(
                x.Split((new string[] { "_" }), StringSplitOptions.RemoveEmptyEntries)[0]))
                .Select(x => x.Split((new string[] { "_" }), StringSplitOptions.RemoveEmptyEntries)))
            {
                var q = questions.Where(z => z.QuestionID == aKey[0]).FirstOrDefault();
                if (q == null) continue;

                var phrl = phrlColl.Where(x => x.QuestionID == aKey[0]).FirstOrDefault();
                if (phrl == null)
                {
                    phrl = phrlColl.AddNew();
                    phrl.AddNew();
                }
                phrl.RegistrationNo = string.Empty;
                phrl.QuestionFormID = qf.QuestionFormID;
                phrl.QuestionGroupID = q.QuestionGroupID;
                phrl.QuestionID = q.QuestionID;
                phrl.QuestionAnswerPrefix = q.AnswerPrefix;
                phrl.QuestionAnswerSuffix = q.AnswerSuffix;

                if (aKey.Length == 2)
                {
                    phrl.QuestionAnswerText2 = HttpContext.Current.Request.QueryString[string.Join("_", aKey)];
                }
                else
                {
                    if (cboType.Contains(q.SRAnswerType.ToLower()))
                    {
                        var qasl = qaslColl.Where(x => x.QuestionAnswerSelectionID == q.QuestionAnswerSelectionID &&
                        x.QuestionAnswerSelectionLineID == HttpContext.Current.Request.QueryString[aKey[0]])
                        .FirstOrDefault();
                        if (qasl != null)
                        {
                            phrl.QuestionAnswerSelectionLineID = qasl.QuestionAnswerSelectionLineID;
                            phrl.QuestionAnswerText = qasl.QuestionAnswerSelectionLineText;
                        }
                    }
                    else
                    {
                        phrl.QuestionAnswerSelectionLineID = "";
                        phrl.QuestionAnswerText = HttpContext.Current.Request.QueryString[aKey[0]];
                    }

                    phrl.QuestionAnswerNum = 0;
                    if (q.SRAnswerType.ToLower() == "num")
                    {
                        phrl.QuestionAnswerNum = System.Convert.ToDecimal(phrl.QuestionAnswerText);
                    }
                }

                phrl.LastUpdateByUserID = "WebService";
                phrl.LastUpdateDateTime = DateTime.Now;
            }

            if (!phrlColl.Any())
            {
                Context.Response.Write(JSonRetFormatted("Detail can not be empty", false));
                return;
            }

            if (isNew)
            {
                phr.AddNew();
                phr.RegistrationNo = string.Empty;
                phr.QuestionFormID = qf.QuestionFormID;
                phr.RecordDate = DateServer.Date;
                phr.RecordTime = DateServer.ToString("HH:mm");
                phr.EmployeeID = "";
                phr.IsComplete = true;
                phr.LastUpdateDateTime = DateServer;
                phr.ExaminerID = "WebService";
                phr.CreateByUserID = "WebService";
                phr.CreateDateTime = DateServer;
                phr.ServiceUnitID = string.Empty;
                phr.ReferenceNo = string.Empty;
            }

            using (var trans = new esTransactionScope())
            {
                if (isNew)
                {
                    var _autoNumber = Helper.GetNewAutoNumber(DateServer, AppEnum.AutoNumber.PatientHealthRecord, "", "WebService");
                    phr.TransactionNo = _autoNumber.LastCompleteNumber;
                    _autoNumber.Save();
                }

                foreach (var phrl in phrlColl)
                {
                    if (phrl.es.IsAdded)
                    {
                        phrl.TransactionNo = phr.TransactionNo;
                    }
                }

                phr.Save();
                phrlColl.Save();

                trans.Complete();
            }

            Context.Response.Write(JSonRetFormatted(phr.TransactionNo));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PhrGetList()
        {
            var req = new jQueryDatatableRequest();
            req.RetrieveQueryString();

            var dateFrom = ValidateDate("datefrom", HttpContext.Current.Request.Params["datefrom"]);
            var dateTo = ValidateDate("dateto", HttpContext.Current.Request.Params["dateto"]);

            var oPhr = new PatientHealthRecordCollection();
            var iCount = oPhr.GetCountByDate(dateFrom, dateTo, req.searchKey);
            var dtb = oPhr.GetByDate(dateFrom, dateTo, req.start, req.limit,
                req.GetColumnName(req.orderColumn), req.orderDir, req.searchKey);

            var ret = new jQueryDatatableReturn
            {
                status = "success",
                draw = req.draw,
                recordsTotal = iCount,
                recordsFiltered = iCount,
                data = ConvertDataTabletoObject(dtb)
            };

            //return ret.Serialize();
            Context.Response.Write(ret.Serialize());
        }
        #endregion

        #region Registration Emergency
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationEmergency(string IpAddress, string ServiceUnitID)
        {
            var dtb = (new RegistrationCollection())
                .GetRegistrationDisplayEmergency(IpAddress, ServiceUnitID);
            Context.Response.Write(JSonRetFormatted(ConvertDataTabletoObject(dtb)));
        }
        #endregion



        #region MedicalRecord File Status
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicalRecordFileStatusGetList(string IpAddress, string QueueCode)
        {
            var dtb = (new MedicalRecordFileStatusCollection()).MedicalRecordFileStatusGetList(IpAddress, QueueCode);
            Context.Response.Write(JSonRetFormatted(ConvertDataTabletoObject(dtb)));
        }
        #endregion


        #region PoliklinikQue
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PoliklinikList(string IpAddress, string ServiceUnitID)
        {
            var dtb = (new RegistrationCollection()).PoliklinikList(IpAddress, ServiceUnitID);
            Context.Response.Write(JSonRetFormatted(ConvertDataTabletoObject(dtb)));
        }
        #endregion

    }
}
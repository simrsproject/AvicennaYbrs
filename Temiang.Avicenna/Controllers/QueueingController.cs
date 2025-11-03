using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Module.Charges.Cashier;
using DevExpress.XtraPrinting.Native.LayoutAdjustment;
using System.Data;
using DevExpress.Utils.Serializing;
using Temiang.Avicenna.Module.Reports.OptionControl;

namespace Temiang.Avicenna.Controllers
{
    public class UserQueueingSettings
    {
        public string[] qLocation { get; set; }
        public string[] qGroup { get; set; }
        public string[] qSu { get; set; }
        public string[] qType { get; set; }
        public string counterCode { get; set; }
    }
    public class CustomQueueing
    {
        public int Id { get; set; }
        public string SRQueueingLocation { get; set; }
        public string SRQueueingGroup { get; set; }
        public string SRQueueingType { get; set; }
        public string ServiceUnitID { get; set; }
        public string ServiceUnitName { get; set; }
        public string ServiceUnitShortName { get; set; }
        public string ParamedicID { get; set; }
        public string ParamedicName { get; set; }
        public string FormattedNo { get; set; }
        public string SRKioskQueueStatus { get; set; }
        public string CounterCode { get; set; }
        public List<CustomAudioInfo> AudioInfo;
    }
    public class CustomAudioInfo
    {
        public int No { get; set; }
        public string Name { get; set; }
        public string Audio { get; set; }
    }

    public class QueueData
    {
        public string FormattedNo { get; set; }
        public string CounterCode { get; set; }
    }

    public class QueueingController : BaseController
    {
        private void SetProgramID()
        {
            ProgramID = AppConstant.Program.QueueList;
        }
        private void InitLocalPage()
        {

        }
        // GET: Pos
        public ActionResult Index()
        {
            SetProgramID();
            if (!InitStartPage()) return RedirectToAction("Login");
            InitLocalPage();
            return View();
        }

        public ActionResult HomePage()
        {
            InitAjaxPage();

            var stdRefQLoc = new AppStandardReferenceItemCollection();
            stdRefQLoc.LoadByStandardReferenceID("QueueingLocation", 0);
            ViewData["StdRef_QLoc"] = stdRefQLoc;

            var stdRefQGroup = new AppStandardReferenceItemCollection();
            stdRefQGroup.LoadByStandardReferenceID("QueueingGroup", 0);
            ViewData["StdRef_QGroup"] = stdRefQGroup;

            var stdRefQType = new AppStandardReferenceItemCollection();
            stdRefQType.LoadByStandardReferenceID("QueueingType", 0);
            ViewData["StdRef_QType"] = stdRefQType;

            // daftar counter yang memungkinkan
            var ctrList = new Dictionary<string, string>();
            var soColl = new QueueingSoundCollection();
            soColl.Query.Where(soColl.Query.IsServiceCounter == true);
            soColl.LoadAll();
            foreach (var so in soColl)
            {
                ctrList.Add(so.SoundID.Value.ToString(), so.Name);
            }
            ViewData["ctrList"] = ctrList;

            var suColl = new ServiceUnitCollection();
            suColl.Query.Where(suColl.Query.IsActive == true, suColl.Query.QueueCode.Coalesce("''") != "");
            suColl.LoadAll();
            //foreach (var su in suColl)
            //{
            //    ctrList.Add(su.ServiceUnitID, su.ServiceUnitName);
            //}
            ViewData["suList"] = suColl;

            var oSet = new UserQueueingSettings();
            var uSet = new AppUserSettings();
            if (uSet.LoadByPrimaryKey(AppSession.UserLogin.UserID))
            {
                if (!string.IsNullOrEmpty(uSet.QueueingCounterSetting))
                    oSet = Newtonsoft.Json.JsonConvert.DeserializeObject<UserQueueingSettings>(uSet.QueueingCounterSetting);
            }
            if (oSet.qLocation == null) oSet.qLocation = new string[] { };
            if (oSet.qGroup == null) oSet.qGroup = new string[] { };
            if (oSet.qSu == null) oSet.qSu = new string[] { };
            if (oSet.qType == null) oSet.qType = new string[] { };
            ViewData["oSet"] = oSet;

            // current queueing
            var queColl = new AppointmentQueueingCollection();
            queColl.Query.Where(queColl.Query.SRKioskQueueStatus == "02", queColl.Query.ProcessByUserID == AppSession.UserLogin.UserID)
                .OrderBy(queColl.Query.Id.Descending);
            queColl.Query.es.Top = 1;
            if (queColl.LoadAll())
            {
                ViewData["currentQue"] = QueueingToObject(queColl.First());
            }
            else
            {
                ViewData["currentQue"] = QueueingToObject(new AppointmentQueueing());
            }

            return View();
        }

        public ActionResult GetData()
        {
            var req = new jQueryDatatableRequest();
            req.RetrieveQueryString();

            var lLoc = Request.Form.GetValues("qLoc[]") is null ? new List<string>() : Request.Form.GetValues("qLoc[]").ToList();
            var lGroup = Request.Form.GetValues("qGroup[]") is null ? new List<string>() : Request.Form.GetValues("qGroup[]").ToList();
            var lSu = Request.Form.GetValues("qSu[]") is null ? new List<string>() : Request.Form.GetValues("qSu[]").ToList();
            var lType = Request.Form.GetValues("qType[]") is null ? new List<string>() : Request.Form.GetValues("qType[]").ToList();

            if (lLoc.Count == 0) lLoc.Add("xxx"); // fake it to get empty result
            if (lGroup.Count == 0) lGroup.Add("xxx");
            if (lSu.Count == 0) lSu.Add("xxx");
            if (lType.Count == 0) lType.Add("xxx");


            var qQue = new AppointmentQueueingQuery("qQue");
            var qLoc = new AppStandardReferenceItemQuery("qLoc");
            var qGroup = new AppStandardReferenceItemQuery("qGroup");
            var qType = new AppStandardReferenceItemQuery("qType");
            var qSu = new ServiceUnitQuery("qSu");
            var qPar = new ParamedicQuery("aPar");

            // count
            qQue.InnerJoin(qSu).On(qQue.ServiceUnitID == qSu.ServiceUnitID)
                .LeftJoin(qLoc).On(qLoc.StandardReferenceID == "QueueingLocation" && qLoc.ItemID == qQue.SRQueueingLocation)
                .LeftJoin(qGroup).On(qGroup.StandardReferenceID == "QueueingGroup" && qGroup.ItemID == qQue.SRQueueingGroup)
                .LeftJoin(qType).On(qType.StandardReferenceID == "QueueingType" && qType.ItemID == qQue.SRQueueingType)
                .LeftJoin(qPar).On(qPar.ParamedicID == qQue.ParamedicID)
                .Where(
                   qQue.QueueingDate == DateTime.Now.Date,
                   qQue.SRQueueingLocation.In(lLoc),
                   //qQue.SRQueueingGroup.In(lGroup),
                   //qSu.ServiceUnitID.In(lSu),
                   qQue.Or(
                       qQue.And(
                           qQue.SRQueueingGroup != "02",
                           qQue.SRQueueingGroup.In(lGroup)
                       ),
                       qQue.And(
                           qQue.SRQueueingGroup == "02",
                           qSu.ServiceUnitID.In(lSu)
                       )
                   ),
                   qQue.SRQueueingType.In(lType),
                   qQue.SRKioskQueueStatus == "01"
                ).GroupBy(
                   qQue.SRQueueingLocation,
                   qQue.SRQueueingGroup,
                   qQue.SRQueueingType,
                   qQue.ServiceUnitID,
                   qQue.ParamedicID
                ).Select(
                    qQue.Id.Count()
                );

            if (!string.IsNullOrEmpty(req.searchKey))
            {
                qQue.Where(qQue.Or(
                    qLoc.ItemName.Like(string.Format("%{0}%", req.searchKey)),
                    qGroup.ItemName.Like(string.Format("%{0}%", req.searchKey)),
                    qType.ItemName.Like(string.Format("%{0}%", req.searchKey)),
                    qSu.ServiceUnitName.Like(string.Format("%{0}%", req.searchKey))
                ));
            }

            var iCount = System.Convert.ToInt32(qQue.LoadDataTable().Rows.Count);
            // end count

            qQue = new AppointmentQueueingQuery("qQue");
            qLoc = new AppStandardReferenceItemQuery("qLoc");
            qGroup = new AppStandardReferenceItemQuery("qGroup");
            qType = new AppStandardReferenceItemQuery("qType");
            qSu = new ServiceUnitQuery("qSu");
            qPar = new ParamedicQuery("aPar");

            qQue.InnerJoin(qSu).On(qQue.ServiceUnitID == qSu.ServiceUnitID)
                .LeftJoin(qLoc).On(qLoc.StandardReferenceID == "QueueingLocation" && qLoc.ItemID == qQue.SRQueueingLocation)
                .LeftJoin(qGroup).On(qGroup.StandardReferenceID == "QueueingGroup" && qGroup.ItemID == qQue.SRQueueingGroup)
                .LeftJoin(qType).On(qType.StandardReferenceID == "QueueingType" && qType.ItemID == qQue.SRQueueingType)
                .LeftJoin(qPar).On(qPar.ParamedicID == qQue.ParamedicID)
                .Where(
                   qQue.QueueingDate == DateTime.Now.Date,
                   qQue.SRQueueingLocation.In(lLoc),
                   //qQue.SRQueueingGroup.In(lGroup),
                   //qSu.ServiceUnitID.In(lSu),
                   qQue.Or(
                       qQue.And(
                           qQue.SRQueueingGroup != "02",
                           qQue.SRQueueingGroup.In(lGroup)
                       ),
                       qQue.And(
                           qQue.SRQueueingGroup == "02",
                           qSu.ServiceUnitID.In(lSu)
                       )
                   ),
                   qQue.SRQueueingType.In(lType)//,
                                                //qQue.SRKioskQueueStatus == "01"
                ).GroupBy(
                    qQue.SRQueueingLocation,
                    qQue.SRQueueingGroup,
                    qQue.SRQueueingType,
                    qQue.ServiceUnitID,
                    qQue.ParamedicID,
                   qLoc.ItemName,
                   qGroup.ItemName,
                   qType.ItemName,
                   qSu.ServiceUnitName,
                   qPar.ParamedicName
                )
                //.Having("<SUM(CASE qQue.SRKioskQueueStatus WHEN '01' THEN 1 ELSE 0 END) > 0>") <-- doesnt work as expected
                .Select(
                    qQue.SRQueueingLocation,
                    qQue.SRQueueingGroup,
                    qQue.SRQueueingType,
                    qQue.ServiceUnitID,
                    qQue.ParamedicID,
                    qLoc.ItemName.As("QueueingLocationName"),
                    qGroup.ItemName.As("QueueingGroupName"),
                    qType.ItemName.As("QueueingTypeName"),
                    qSu.ServiceUnitName,
                   qPar.ParamedicName,
                    qQue.Id.Count().As("Total"),
                    "<SUM(CASE qQue.SRKioskQueueStatus WHEN '01' THEN 1 ELSE 0 END) DataCount>",
                    "<(SELECT TOP 1 a.FormattedNo FROM [AppointmentQueueing] a WHERE a.SRKioskQueueStatus <> '01' AND a.SRQueueingLocation = qQue.SRQueueingLocation AND a.SRQueueingGroup = qQue.SRQueueingGroup AND a.SRQueueingType = qQue.SRQueueingType AND a.ServiceUnitID = qQue.ServiceUnitID ORDER BY a.ProcessDateTime DESC) CurrentNo>"
                );

            if (!string.IsNullOrEmpty(req.searchKey))
            {
                qQue.Where(qQue.Or(
                    qLoc.ItemName.Like(string.Format("%{0}%", req.searchKey)),
                    qGroup.ItemName.Like(string.Format("%{0}%", req.searchKey)),
                    qType.ItemName.Like(string.Format("%{0}%", req.searchKey)),
                    qSu.ServiceUnitName.Like(string.Format("%{0}%", req.searchKey)))
                );
            }

            qQue.es.PageSize = req.limit;
            qQue.es.PageNumber = System.Convert.ToInt32(req.start / req.limit) + 1;

            switch (req.GetColumnName(req.orderColumn))
            {
                case "QueueingLocationName":
                    {
                        if (req.orderDir == "asc")
                            qQue.OrderBy(qLoc.ItemName.Ascending);
                        else
                            qQue.OrderBy(qLoc.ItemName.Descending);
                        break;
                    }
                case "QueueingGroupName":
                    {
                        if (req.orderDir == "asc")
                            qQue.OrderBy(qGroup.ItemName.Ascending);
                        else
                            qQue.OrderBy(qGroup.ItemName.Descending);
                        break;
                    }
                case "QueueingTypeName":
                    {
                        if (req.orderDir == "asc")
                            qQue.OrderBy(qType.ItemName.Ascending);
                        else
                            qQue.OrderBy(qType.ItemName.Descending);
                        break;
                    }
                case "ServiceUnitName":
                    {
                        if (req.orderDir == "asc")
                            qQue.OrderBy(qSu.ServiceUnitName.Ascending);
                        else
                            qQue.OrderBy(qSu.ServiceUnitName.Descending);
                        break;
                    }
                case "ParamedicName":
                    {
                        if (req.orderDir == "asc")
                            qQue.OrderBy(qPar.ParamedicName.Ascending);
                        else
                            qQue.OrderBy(qPar.ParamedicName.Descending);
                        break;
                    }
            }

            DataTable dtb = qQue.LoadDataTable();
            foreach (System.Data.DataRow row in dtb.Rows)
            {
                if ((Int32)row["DataCount"] == 0)
                {
                    row.Delete();
                }
            }
            dtb.AcceptChanges();

            //var dt = ConvertDataTabletoJSON(dtb);

            var ret = new jQueryDatatableReturn
            {
                status = "success",
                draw = req.draw,
                recordsTotal = iCount,
                recordsFiltered = iCount,
                data = ConvertDataTabletoObject(dtb)
            };

            return Json(ret);
        }

        public JsonResult SaveSettings(string[] qLocation, string[] qGroup, string[] qSu, string[] qType, string counterCode, string userId)
        {
            try
            {
                var oSet = new UserQueueingSettings()
                {
                    qLocation = qLocation,
                    qGroup = qGroup,
                    qSu = qSu,
                    qType = qType,
                    counterCode = counterCode
                };

                var uSet = new AppUserSettings();
                if (!uSet.LoadByPrimaryKey(AppSession.UserLogin.UserID))
                {
                    uSet = new AppUserSettings();
                    uSet.UserID = AppSession.UserLogin.UserID;
                }
                uSet.QueueingCounterSetting = Newtonsoft.Json.JsonConvert.SerializeObject(oSet);
                uSet.Save();

                return Json(JSonRetFormatted(oSet));
            }
            catch (Exception e)
            {
                return Json(JSonRetFormatted(e.Message, false));
            }
        }

        private AppointmentQueueingCollection GetAndMarkPrevAsDone(string userId)
        {
            var queColl = new AppointmentQueueingCollection();
            queColl.Query.Where(queColl.Query.SRKioskQueueStatus == "02", queColl.Query.ProcessByUserID == userId);
            if (queColl.LoadAll())
            {
                foreach (var k in queColl)
                {
                    k.SRKioskQueueStatus = "03";
                    k.LastUpdateByUserID = userId;
                    k.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
            return queColl;
        }

        private string GetCounterName(string CounterID)
        {
            if (Helper.IsNumeric(CounterID))
            {
                var qsColl = new QueueingSoundCollection();
                qsColl.Query.Where(qsColl.Query.SoundID == CounterID);
                if (qsColl.LoadAll())
                {
                    return qsColl.First().Name;
                }
                else
                {
                    return "Undef";
                }
            }
            else
            {
                var su = new ServiceUnit();
                if (su.LoadByPrimaryKey(CounterID))
                {
                    return su.ServiceUnitID;
                }
                else
                {
                    return "Undef";
                }
            }
        }

        public JsonResult NextByRow(string qLocation, string qGroup, string qType, string suId, string parId, string counterCode, string userId)
        {
            var queColl = GetAndMarkPrevAsDone(userId);
            // booking next
            var queNextColl = new AppointmentQueueingCollection();
            queNextColl.Query.Where(
                queNextColl.Query.SRQueueingLocation == qLocation,
                queNextColl.Query.SRQueueingGroup == qGroup,
                queNextColl.Query.SRQueueingType == qType,
                queNextColl.Query.ServiceUnitID == suId,
                queNextColl.Query.ParamedicID == parId,
                queNextColl.Query.SRKioskQueueStatus == "01")
                .OrderBy(queNextColl.Query.Id.Ascending);
            queNextColl.Query.es.Top = 1;

            var queNext = new AppointmentQueueing();
            if (queNextColl.LoadAll())
            {
                queNext = queNextColl.First();
                queNext.SRKioskQueueStatus = "02";
                queNext.ProcessByUserID = userId;
                queNext.ProcessDateTime = (new DateTime()).NowAtSqlServer();
                queNext.Recall = true;
                queNext.LastUpdateByUserID = userId;
                queNext.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                queNext.CounterCode = GetCounterName(counterCode);
                queColl.AttachEntity(queNext);
            }

            try
            {
                queColl.Save();
                return Json(JSonRetFormatted(QueueingToObject(queNext)));
            }
            catch (Exception e)
            {
                return Json(JSonRetFormatted(e.Message, false));
            }
        }
        public JsonResult Next(string[] qLocation, string[] qGroup, string[] qSu, string[] qType, string counterCode, string userId)
        {
            if (qLocation == null) qLocation = new string[] { "xxx" };
            if (qGroup == null) qGroup = new string[] { "xxx" };
            if (qSu == null) qSu = new string[] { "xxx" };
            if (qType == null) qType = new string[] { "xxx" };

            var queColl = GetAndMarkPrevAsDone(userId);
            // booking next
            var queNextColl = new AppointmentQueueingCollection();
            queNextColl.Query.Where(
                queNextColl.Query.SRQueueingLocation.In(qLocation),
                //queNextColl.Query.SRQueueingGroup.In(qGroup),
                //queNextColl.Query.ServiceUnitID.In(qSu),
                queNextColl.Query.Or(
                    queNextColl.Query.And(
                        queNextColl.Query.SRQueueingGroup != "02",
                        queNextColl.Query.SRQueueingGroup.In(qGroup)
                    ),
                    queNextColl.Query.And(
                        queNextColl.Query.SRQueueingGroup == "02",
                        queNextColl.Query.ServiceUnitID.In(qSu)
                    )
                ),
                queNextColl.Query.SRQueueingType.In(qType),
                queNextColl.Query.SRKioskQueueStatus == "01")
                .OrderBy(queNextColl.Query.Id.Ascending);
            queNextColl.Query.es.Top = 1;

            var queNext = new AppointmentQueueing();
            if (queNextColl.LoadAll())
            {
                queNext = queNextColl.First();
                queNext.SRKioskQueueStatus = "02";
                queNext.ProcessByUserID = userId;
                queNext.ProcessDateTime = (new DateTime()).NowAtSqlServer();
                queNext.Recall = true;
                queNext.LastUpdateByUserID = userId;
                queNext.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                queNext.CounterCode = GetCounterName(counterCode);
                queColl.AttachEntity(queNext);
            }

            try
            {
                queColl.Save();
                return Json(JSonRetFormatted(QueueingToObject(queNext)));
            }
            catch (Exception e)
            {
                return Json(JSonRetFormatted(e.Message, false));
            }
        }

        public JsonResult Recall(string currentNo, string userId)
        {
            var queColl = new AppointmentQueueingCollection();
            queColl.Query.Where(
                queColl.Query.FormattedNo == currentNo,
                queColl.Query.SRKioskQueueStatus == "02",
                queColl.Query.ProcessByUserID == userId);
            if (queColl.LoadAll())
            {
                foreach (var k in queColl)
                {
                    k.Recall = true;
                    k.LastUpdateByUserID = userId;
                    k.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            try
            {
                queColl.Save();
                return Json(JSonRetFormatted(QueueingToObject(queColl.First())));
            }
            catch (Exception e)
            {
                return Json(JSonRetFormatted(e.Message, false));
            }
        }

        public JsonResult Stop(string userId)
        {
            var queColl = GetAndMarkPrevAsDone(userId);

            try
            {
                queColl.Save();
                if (queColl.Count > 0)
                {
                    return Json(JSonRetFormatted(QueueingToObject(queColl.First())));
                }
                else
                {
                    return Json(JSonRetFormatted(QueueingToObject(new AppointmentQueueing())));
                }
            }
            catch (Exception e)
            {
                return Json(JSonRetFormatted(e.Message, false));
            }
        }

        public JsonResult Clear(string[] qLocation, string[] qGroup, string[] qSu, string[] qType, string counterCode, string userId)
        {
            if (qLocation == null)
            {
                return Json(JSonRetFormatted("No queueing location is selected", false));
            }
            //if (qGroup == null)
            //{
            //    return Json(JSonRetFormatted("No queueing group is selected", false));
            //}
            //if (qSu == null)
            //{
            //    return Json(JSonRetFormatted("No service unit is selected", false));
            //}
            if (qType == null)
            {
                return Json(JSonRetFormatted("No queueing type is selected", false));
            }

            var queColl = new AppointmentQueueingCollection();
            queColl.Query.Where(queColl.Query.SRKioskQueueStatus == "01",
            queColl.Query.QueueingDate < DateTime.Now.Date,
            queColl.Query.SRQueueingLocation.In(qLocation),
            //queColl.Query.SRQueueingGroup.In(qGroup),
            //queColl.Query.ServiceUnitID.In(qSu),
            queColl.Query.Or(
                queColl.Query.And(
                    queColl.Query.SRQueueingGroup != "02",
                    queColl.Query.SRQueueingGroup.In(qGroup)
                ),
                queColl.Query.And(
                    queColl.Query.SRQueueingGroup == "02",
                    queColl.Query.ServiceUnitID.In(qSu)
                )
            ),
            queColl.Query.SRQueueingType.In(qType),
            queColl.Query.SRKioskQueueStatus == "01");

            var i = 0;
            if (queColl.LoadAll())
            {
                var dNow = (new DateTime()).NowAtSqlServer();
                foreach (var k in queColl)
                {
                    k.SRKioskQueueStatus = "03";
                    k.LastUpdateByUserID = userId;
                    k.LastUpdateDateTime = dNow;
                    i++;
                }
            }
            try
            {
                queColl.Save();
                return Json(JSonRetFormatted(i));
            }
            catch (Exception e)
            {
                return Json(JSonRetFormatted(e.Message, false));
            }

            //// clean the idle last 1 hour
            //kQueColl = new KioskQueueCollection();
            //kQueColl.Query.Where(kQueColl.Query.SRKioskQueueStatus == "02",
            //    "<DATEDIFF(hour, ProcessDateTime, GETDATE()) > 2>",
            //    kQueColl.Query.KioskQueueCode.In(KioskQueueType.Select(x => x.ToString()).ToArray()));

            //if (kQueColl.LoadAll())
            //{
            //    foreach (var k in kQueColl)
            //    {
            //        k.SRKioskQueueStatus = "03";
            //        i++;
            //    }
            //}
            //kQueColl.Save();
        }

        private CustomQueueing QueueingToObject(AppointmentQueueing que)
        {
            if (que.Id.HasValue)
            {
                var su = new ServiceUnit();
                su.LoadByPrimaryKey(que.ServiceUnitID);

                var par = new Paramedic();
                par.LoadByPrimaryKey(que.ParamedicID);

                return new CustomQueueing()
                {
                    Id = que.Id.Value,
                    SRQueueingLocation = que.SRQueueingLocation,
                    SRQueueingGroup = que.SRQueueingGroup,
                    SRQueueingType = que.SRQueueingType,
                    ServiceUnitID = que.ServiceUnitID,
                    ServiceUnitName = su.ServiceUnitName,
                    ParamedicID = que.ParamedicID,
                    ParamedicName = par.ParamedicName,
                    FormattedNo = que.FormattedNo,
                    SRKioskQueueStatus = que.SRKioskQueueStatus,
                    CounterCode = que.CounterCode
                };
            }
            else
            {
                return new CustomQueueing()
                {
                    Id = 0,
                    SRQueueingLocation = "",
                    SRQueueingGroup = "",
                    SRQueueingType = "",
                    ServiceUnitID = "",
                    ServiceUnitName = "",
                    ParamedicID = "",
                    ParamedicName = "",
                    FormattedNo = "",
                    SRKioskQueueStatus = "",
                    CounterCode = ""
                };
            }
        }
        private CustomQueueing QueueingToObjectWithAudioInfo(AppointmentQueueing que, AppStandardReferenceItemCollection stdGroup)
        {
            var queRet = QueueingToObject(que);

            var std = stdGroup.Where(s => s.ItemID == que.SRQueueingGroup).FirstOrDefault();
            if (std != null)
            {
                if (!string.IsNullOrEmpty(std.CustomField))
                {
                    var dict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, string>>(std.CustomField);

                    var audioColl = new QueueingSoundCollection();
                    audioColl.LoadAll();

                    queRet.AudioInfo = new List<CustomAudioInfo>();
                    int i = 0;
                    foreach (var kp in dict)
                    {
                        if (kp.Value == "@queueingno")
                        {
                            // code
                            string code = (que.FormattedNo.Split('-')[0]).ToLower();
                            foreach (char c in code)
                            {
                                queRet.AudioInfo.Add(new CustomAudioInfo() { No = i, Name = c.ToString(), Audio = GetAudioFile(audioColl, c.ToString()) });
                                i++;
                            }
                            // number
                            int no = Convert.ToInt32(que.FormattedNo.Split('-')[1]);
                            var nArray = NumberToArrayAudio(no);
                            foreach (var str in nArray)
                            {
                                queRet.AudioInfo.Add(new CustomAudioInfo() { No = i, Name = str, Audio = GetAudioFile(audioColl, str) });
                                i++;
                            }
                        }
                        else if (kp.Value == "@counterno")
                        {
                            // number
                            int no = Convert.ToInt32(queRet.CounterCode);
                            var nArray = NumberToArrayAudio(no);
                            foreach (var str in nArray)
                            {
                                queRet.AudioInfo.Add(new CustomAudioInfo() { No = i, Name = str, Audio = GetAudioFile(audioColl, str) });
                                i++;
                            }
                        }
                        else if (kp.Value == "@serviceunit")
                        {
                            var su = new ServiceUnit();
                            if (su.LoadByPrimaryKey(queRet.ServiceUnitID))
                            {
                                if (!string.IsNullOrEmpty(su.SoundFilePath))
                                {
                                    queRet.AudioInfo.Add(new CustomAudioInfo() { No = i, Name = su.ServiceUnitID.Replace(".", ""), Audio = su.SoundFilePath });
                                    i++;
                                }
                            }
                        }
                        else
                        {
                            queRet.AudioInfo.Add(new CustomAudioInfo() { No = i, Name = kp.Value, Audio = GetAudioFile(audioColl, kp.Value) });
                            i++;
                        }
                    }
                }
            }
            return queRet;
        }
        private List<string> NumberToArrayAudio(int num)
        {
            if (num == 0)
            {
                return new List<string>();
            }
            if (num < 12)
            {
                var huruf = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "sepuluh", "sebelas" };
                return new List<string> { huruf[num] };
            }
            else if (num < 20)
            {
                var tmp = NumberToArrayAudio(num - 10);
                tmp.Add("belas");
                return tmp;
            }
            else if (num < 100)
            {
                var tmp = NumberToArrayAudio(Convert.ToInt32(num / 10));
                tmp.Add("puluh");
                tmp.AddRange(NumberToArrayAudio(num % 10));
                return tmp;
            }
            else if (num < 200)
            {
                var tmp = new List<string> { "seratus" };
                tmp.AddRange(NumberToArrayAudio(num - 100));
                return tmp;
            }
            else if (num < 1000)
            {
                var tmp = NumberToArrayAudio(Convert.ToInt32(num / 100));
                tmp.Add("ratus");
                tmp.AddRange(NumberToArrayAudio(num % 100));
                return tmp;
            }
            else if (num < 2000)
            {
                var tmp = new List<string> { "seribu" };
                tmp.AddRange(NumberToArrayAudio(num - 1000));
                return tmp;
            }
            else if (num < 1000000)
            {
                var tmp = NumberToArrayAudio(Convert.ToInt32(num / 1000));
                tmp.Add("ribu");
                tmp.AddRange(NumberToArrayAudio(num % 1000));
                return tmp;
            }
            return new List<string>();
        }
        private string GetAudioFile(QueueingSoundCollection aColl, string name)
        {
            var audio = aColl.Where(a => a.Name == name).FirstOrDefault();
            if (audio != null)
            {
                return audio.FilePath;
            }
            else
            {
                return string.Empty;
            }
        }

        #region Running Text
        
        public ActionResult RunningTextGetData()
        {
            var req = new jQueryDatatableRequest();
            req.RetrieveQueryString();

            var lLoc = Request.Form.GetValues("qLoc[]") is null ? new List<string>() : Request.Form.GetValues("qLoc[]").ToList();
            var lGroup = Request.Form.GetValues("qGroup[]") is null ? new List<string>() : Request.Form.GetValues("qGroup[]").ToList();
            var lSu = Request.Form.GetValues("qSu[]") is null ? new List<string>() : Request.Form.GetValues("qSu[]").ToList();
            var lType = Request.Form.GetValues("qType[]") is null ? new List<string>() : Request.Form.GetValues("qType[]").ToList();

            if (lLoc.Count == 0) lLoc.Add("xxx"); // fake it to get empty result
            if (lGroup.Count == 0) lGroup.Add("xxx");
            if (lSu.Count == 0) lSu.Add("xxx");
            if (lType.Count == 0) lType.Add("xxx");


            var qRt = new QueueingRunningTextQuery("qRt");
            var qSu = new ServiceUnitQuery("qSu");

            // count
            qRt.InnerJoin(qSu).On(qRt.ServiceUnitID == qSu.ServiceUnitID)
                //.Where(
                //   qSu.ServiceUnitID.In(lSu)
                //)
                .Select(
                    qRt.RunningTextID.Count()
                );

            if (!string.IsNullOrEmpty(req.searchKey))
            {
                qRt.Where(qRt.Or(
                    qRt.RunningText.Like(string.Format("%{0}%", req.searchKey)),
                    qSu.ServiceUnitName.Like(string.Format("%{0}%", req.searchKey))
                ));
            }

            var iCount = System.Convert.ToInt32(qRt.LoadDataTable().Rows.Count);
            // end count

            qRt = new QueueingRunningTextQuery("qRt");
            qSu = new ServiceUnitQuery("qSu");

            qRt.InnerJoin(qSu).On(qRt.ServiceUnitID == qSu.ServiceUnitID)
                //.Where(
                //   qSu.ServiceUnitID.In(lSu)
                //)
                //.GroupBy(
                //    qRt.RunningTextID,
                //    qRt.ServiceUnitID,
                //    qRt.RunningText,
                //    qSu.ServiceUnitName
                //)
                .OrderBy(
                    qRt.RunningTextID.Ascending
                )
                .Select(
                    qRt.RunningTextID,
                    qRt.ServiceUnitID,
                    qRt.RunningText,
                    qSu.ServiceUnitName
                );

            if (!string.IsNullOrEmpty(req.searchKey))
            {
                qRt.Where(qRt.Or(
                    qRt.RunningText.Like(string.Format("%{0}%", req.searchKey)),
                    qSu.ServiceUnitName.Like(string.Format("%{0}%", req.searchKey))
                ));
            }

            qRt.es.PageSize = req.limit;
            qRt.es.PageNumber = System.Convert.ToInt32(req.start / req.limit) + 1;

            switch (req.GetColumnName(req.orderColumn))
            {
                case "ServiceUnitName":
                    {
                        if (req.orderDir == "asc")
                            qRt.OrderBy(qSu.ServiceUnitName.Ascending);
                        else
                            qRt.OrderBy(qSu.ServiceUnitName.Descending);
                        break;
                    }
                case "RunningText":
                    {
                        if (req.orderDir == "asc")
                            qRt.OrderBy(qRt.RunningText.Ascending);
                        else
                            qRt.OrderBy(qRt.RunningText.Descending);
                        break;
                    }
            }

            DataTable dtb = qRt.LoadDataTable();
            //var dt = ConvertDataTabletoJSON(dtb);

            var ret = new jQueryDatatableReturn
            {
                status = "success",
                draw = req.draw,
                recordsTotal = iCount,
                recordsFiltered = iCount,
                data = ConvertDataTabletoObject(dtb)
            };

            return Json(ret);
        }
        public ActionResult RunningTextEditor(int RunningTextID)
        {
            InitAjaxPage();

            var rt = new QueueingRunningText();
            if (RunningTextID != 0)
            {
                rt.LoadByPrimaryKey(RunningTextID);
            }
            else
            {
                rt.RunningTextID = 0;
            }

            ViewBag.rt = rt;

            var suColl = new ServiceUnitCollection();
            var suq = new ServiceUnitQuery("su");
            var usuq = new AppUserServiceUnitQuery("usu");
            suq.InnerJoin(usuq).On(suq.ServiceUnitID == usuq.ServiceUnitID)
                .Where(
                    suq.IsActive == true,
                    suq.QueueCode.Coalesce("''") != "",
                    usuq.UserID == AppSession.UserLogin.UserID);
            suColl.Load(suq);

            ViewData["suColl"] = suColl;

            return View();
        }
        public JsonResult RunningTextSave(int RunningTextID, string ServiceUnitID, string RunningText)
        {
            if(string.IsNullOrEmpty(ServiceUnitID)) return Json(JSonRetFormatted("Service Unit Required", false));
            if (string.IsNullOrEmpty(RunningText)) return Json(JSonRetFormatted("RunningText Required", false));

            var rn = new QueueingRunningText();
            if (RunningTextID != 0)
            {
                rn.LoadByPrimaryKey(RunningTextID);
            }
            else {
                rn.CreateByUserID = AppSession.UserLogin.UserID;
                rn.CreateDateTime = DateTime.Now;
            }

            try
            {
                rn.ServiceUnitID = ServiceUnitID;
                rn.RunningText = RunningText;
                rn.LastUpdateByUserID = AppSession.UserLogin.UserID;
                rn.LastUpdateDateTime = DateTime.Now;

                rn.Save();
                return Json(JSonRetFormatted(rn.RunningTextID));
            }
            catch (Exception e)
            {
                return Json(JSonRetFormatted(e.Message, false));
            }
        }

        public JsonResult RunningTextDelete(int RunningTextID)
        {
            var rn = new QueueingRunningText();
            if (!rn.LoadByPrimaryKey(RunningTextID))
            {
                return Json(JSonRetFormatted("Running Text Not Found", false));
            }
            else
            {
                try
                {
                    rn.MarkAsDeleted();
                    rn.Save();
                    return Json(JSonRetFormatted("Success"));
                }
                catch (Exception e)
                {
                    return Json(JSonRetFormatted(e.Message, false));
                }
            }
        }

        public ActionResult RunningTextGetDataByLocation(string qLocation)
        {
            var qRt = new QueueingRunningTextQuery("qRt");
            var qSu = new ServiceUnitQuery("qSu");

            qRt.InnerJoin(qSu).On(qRt.ServiceUnitID == qSu.ServiceUnitID)
                .Where(qSu.SrqueueinglocationPoli == qLocation)
                .OrderBy(
                    qRt.RunningTextID.Ascending
                )
                .Select(
                    qRt.RunningTextID,
                    qRt.ServiceUnitID,
                    qRt.RunningText,
                    qSu.ServiceUnitName
                );

            var rtColl = new QueueingRunningTextCollection();
            rtColl.Load(qRt);
            // tambahkan yang dari appparameter jika ada isinya
            if (!string.IsNullOrEmpty(AppSession.Parameter.QueueDisplayScrollingText)) {
                var rt = rtColl.AddNew();
                rt.RunningTextID = 0;
                rt.RunningText = AppSession.Parameter.QueueDisplayScrollingText;
            }
            // tambahkan dari cuti dokter
            var pld = new ParamedicLeaveDateQuery("pld");
            var pl = new ParamedicLeaveQuery("pl");
            var p = new ParamedicQuery("p");
            var sup = new ServiceUnitParamedicQuery("sup");
            var su = new ServiceUnitQuery("su");

            pld.InnerJoin(pl).On(pld.TransactionNo == pl.TransactionNo)
                .InnerJoin(p).On(pl.ParamedicID == p.ParamedicID)
                .InnerJoin(sup).On(p.ParamedicID == sup.ParamedicID)
                .InnerJoin(su).On(sup.ServiceUnitID == su.ServiceUnitID)
                .Where(
                    pld.LeaveDate == DateTime.Now.Date,
                    su.SrqueueinglocationPoli == qLocation
                ).Select(
                    p.ParamedicID,
                    p.ParamedicName,
                    pl.StartDate,
                    pl.EndDate
                );

            var dtLeave = pld.LoadDataTable();
            foreach (System.Data.DataRow dr in dtLeave.Rows) {
                var rt = rtColl.AddNew();
                rt.RunningTextID = rtColl.Count + 1;
                rt.RunningText = String.Format("{0} cuti {1}", 
                    dr["ParamedicName"].ToString(),
                    ((dr["StartDate"] as DateTime?).Value == (dr["EndDate"] as DateTime?).Value ? 
                    (string.Format("hari ini, tgl {0}", (dr["StartDate"] as DateTime?).Value.ToString("dd-MM-yyyy"))):
                    (string.Format("dari tgl {0} sd tgl {1}", 
                        (dr["StartDate"] as DateTime?).Value.ToString("dd-MM-yyyy"),
                        (dr["EndDate"] as DateTime?).Value.ToString("dd-MM-yyyy"))))
                    );
            }

            if (rtColl.Count > 0)
            {
                return Json(JSonRetFormatted(rtColl.Select(x => 
                new { 
                    RunningTextID = x.RunningTextID, 
                    RunningText = x.RunningText,
                    TextLength = x.RunningText.Length
                })));
            }
            else {
                return Json(JSonRetFormatted("No Data", false));
            }
        }
        #endregion

        #region Display versi Avicenna
        public ActionResult DisplayReg(string qLocation, string qCounter)
        {
            InitConstanta();

            ViewData["audioPathURL"] = AppSession.Parameter.SoundFolderURL;// "http://localhost/audio"; //AppSession.Parameter.SoundFolder;
            var audioColl = new QueueingSoundCollection();
            audioColl.LoadAll();
            foreach (var au in audioColl)
            {
                au.FilePath = "QueueingSound/" + au.FilePath;
            }

            var suColl = new ServiceUnitCollection();
            suColl.Query.Where(suColl.Query.SoundFilePath.Coalesce("''") != "");
            if (suColl.LoadAll())
            {
                foreach (var su in suColl)
                {
                    var aSu = audioColl.AddNew();
                    aSu.Name = su.ServiceUnitID.Replace(".", "");
                    aSu.FilePath = "ServiceUnit/" + su.SoundFilePath;
                }
            }

            var lastQueCall = new AppointmentQueueingCollection();
            lastQueCall.Query
                .Where(lastQueCall.Query.SRKioskQueueStatus == "02" && lastQueCall.Query.QueueingDate == DateTime.Now.Date)
                .Select(lastQueCall.Query.FormattedNo, lastQueCall.Query.CounterCode)
                .es.Top = 1;

            var lastQue = lastQueCall.LoadAll() ? lastQueCall.FirstOrDefault() : null;

            var latestQueuePerCounter = new List<QueueData>();

            var lastQueConterCall = new AppointmentQueueingCollection();
            lastQueConterCall.Query.Where(lastQueConterCall.Query.QueueingDate == DateTime.Now.Date
                                    && (lastQueConterCall.Query.SRKioskQueueStatus == "02" || lastQueConterCall.Query.SRKioskQueueStatus == "03"));

            if (lastQueConterCall.LoadAll())
            {
                var groupedQueCalls = lastQueConterCall
                    .GroupBy(q => q.CounterCode)
                    .Select(g => new QueueData
                    {
                        FormattedNo = g.OrderByDescending(q => q.ProcessDateTime).FirstOrDefault()?.FormattedNo,
                        CounterCode = g.Key
                    });

                foreach (var item in groupedQueCalls)
                {
                    latestQueuePerCounter.Add(item);
                }
            }

            ViewData["lastFormatedNo"] = lastQue?.FormattedNo ?? "A000";

            ViewData["lastCounter"] = lastQue?.CounterCode ?? "00";

            ViewData["LatestQueuePerCounter"] = latestQueuePerCounter;

            ViewData["audioColl"] = audioColl;

            ViewData["qCounter"] = qCounter;

            ViewData["qLocation"] = qLocation;

            return View("DisplayReg");
        }


        public ActionResult DisplayPoli(string qLocation, string qCounter)
        {
            InitConstanta();

            ViewData["audioPathURL"] = AppSession.Parameter.SoundFolderURL;// "http://localhost/audio"; //AppSession.Parameter.SoundFolder;
            var audioColl = new QueueingSoundCollection();
            audioColl.LoadAll();
            foreach (var au in audioColl)
            {
                au.FilePath = "QueueingSound/" + au.FilePath;
            }

            var suColl = new ServiceUnitCollection();
            suColl.Query.Where(suColl.Query.SoundFilePath.Coalesce("''") != "");
            if (suColl.LoadAll())
            {
                foreach (var su in suColl)
                {
                    var aSu = audioColl.AddNew();
                    aSu.Name = su.ServiceUnitID.Replace(".", "");
                    aSu.FilePath = "ServiceUnit/" + su.SoundFilePath;
                }
            }

            ViewData["audioColl"] = audioColl;

            ViewData["qLocation"] = qLocation;

            ViewData["qCounter"] = qCounter;

            return View("DisplayPoli");
        }

        public ActionResult DisplayPoliByPhysician(string qLocation, string qPhysician)
        {
            InitConstanta();

            ViewData["audioPathURL"] = AppSession.Parameter.SoundFolderURL;// "http://localhost/audio"; //AppSession.Parameter.SoundFolder;
            var audioColl = new QueueingSoundCollection();
            audioColl.LoadAll();
            foreach (var au in audioColl)
            {
                au.FilePath = "QueueingSound/" + au.FilePath;
            }

            var suColl = new ServiceUnitCollection();
            suColl.Query.Where(suColl.Query.SoundFilePath.Coalesce("''") != "");
            if (suColl.LoadAll())
            {
                foreach (var su in suColl)
                {
                    var aSu = audioColl.AddNew();
                    aSu.Name = su.ServiceUnitID.Replace(".", "");
                    aSu.FilePath = "ServiceUnit/" + su.SoundFilePath;
                }
            }

            ViewData["audioColl"] = audioColl;

            ViewData["qLocation"] = qLocation;

            ViewData["qPhysician"] = qPhysician;

            return View("DisplayPoliByPhysician");
        }
        #endregion

        #region Display RS Cideres
        public ActionResult DisplayRegRsCideres()
        {
            InitConstanta();

            ViewData["audioPathURL"] = AppSession.Parameter.SoundFolderURL;// "http://localhost/audio"; //AppSession.Parameter.SoundFolder;
            var audioColl = new QueueingSoundCollection();
            audioColl.LoadAll();
            foreach (var au in audioColl)
            {
                au.FilePath = "QueueingSound/" + au.FilePath;
            }

            var suColl = new ServiceUnitCollection();
            suColl.Query.Where(suColl.Query.SoundFilePath.Coalesce("''") != "");
            if (suColl.LoadAll())
            {
                foreach (var su in suColl)
                {
                    var aSu = audioColl.AddNew();
                    aSu.Name = su.ServiceUnitID.Replace(".", "");
                    aSu.FilePath = "ServiceUnit/" + su.SoundFilePath;
                }
            }

            ViewData["audioColl"] = audioColl;

            return View("Rscideres/DisplayReg");
        }

        public ActionResult DisplayPoliRsCideres(string qLocation)
        {
            InitConstanta();

            ViewData["audioPathURL"] = AppSession.Parameter.SoundFolderURL;// "http://localhost/audio"; //AppSession.Parameter.SoundFolder;
            var audioColl = new QueueingSoundCollection();
            audioColl.LoadAll();
            foreach (var au in audioColl)
            {
                au.FilePath = "QueueingSound/" + au.FilePath;
            }

            var suColl = new ServiceUnitCollection();
            suColl.Query.Where(suColl.Query.SoundFilePath.Coalesce("''") != "");
            if (suColl.LoadAll())
            {
                foreach (var su in suColl)
                {
                    var aSu = audioColl.AddNew();
                    aSu.Name = su.ServiceUnitID.Replace(".", "");
                    aSu.FilePath = "ServiceUnit/" + su.SoundFilePath;
                }
            }

            ViewData["audioColl"] = audioColl;

            ViewData["qLocation"] = qLocation;

            return View("Rscideres/DisplayPoli");
        }

        public ActionResult Display(string qLocation)
        {
            InitConstanta();

            ViewData["audioPathURL"] = AppSession.Parameter.SoundFolderURL;// "http://localhost/audio"; //AppSession.Parameter.SoundFolder;
            var audioColl = new QueueingSoundCollection();
            audioColl.LoadAll();
            foreach (var au in audioColl)
            {
                au.FilePath = "QueueingSound/" + au.FilePath;
            }

            var suColl = new ServiceUnitCollection();
            suColl.Query.Where(suColl.Query.SoundFilePath.Coalesce("''") != "");
            if (suColl.LoadAll())
            {
                foreach (var su in suColl)
                {
                    var aSu = audioColl.AddNew();
                    aSu.Name = su.ServiceUnitID.Replace(".", "");
                    aSu.FilePath = "ServiceUnit/" + su.SoundFilePath;
                }
            }

            ViewData["audioColl"] = audioColl;

            ViewData["qLocation"] = qLocation;

            return View();
        }

        public JsonResult GetQueueing(string qLocation)
        {
            var queColl = new AppointmentQueueingCollection();
            queColl.Query.Where(queColl.Query.SRKioskQueueStatus == "02", queColl.Query.SRQueueingLocation == qLocation, queColl.Query.Recall == true);
            if (queColl.LoadAll())
            {
                var suColl = new ServiceUnitCollection();
                suColl.LoadAll();
                var pColl = new ParamedicCollection();
                pColl.LoadAll();
                var dDate = (new DateTime()).NowAtSqlServer();
                foreach (var que in queColl)
                {
                    que.Recall = false;
                    que.LastUpdateByUserID = "caller";
                    que.LastUpdateDateTime = dDate;
                }
                try
                {
                    queColl.Save();

                    var lQue = new List<CustomQueueing>();
                    var stdGroup = new AppStandardReferenceItemCollection();
                    stdGroup.LoadByStandardReferenceID("QueueingGroup");
                    stdGroup.LoadAll();

                    foreach (var que in queColl)
                    {
                        var Que = QueueingToObjectWithAudioInfo(que, stdGroup);
                        var serviceUnit = suColl.FirstOrDefault(su => su.ServiceUnitID == que.ServiceUnitID);
                        var Paramedic = pColl.FirstOrDefault(p => p.ParamedicID == que.ParamedicID);
                        if (serviceUnit != null)
                            Que.ServiceUnitShortName = serviceUnit.ShortName;
                        if (Paramedic != null)
                            Que.ParamedicName = Paramedic.ParamedicName;
                        lQue.Add(Que);
                    }
                    return Json(JSonRetFormatted(lQue));
                }
                catch (Exception e)
                {
                    return Json(JSonRetFormatted(e.Message, false));
                }
            }
            else
            {
                return Json(JSonRetFormatted(null));
            }
        }

        public JsonResult GetQueueingByPhysician(string qLocation, string qPhysician)
        {
            var queColl = new AppointmentQueueingCollection();
            queColl.Query.Where(queColl.Query.SRKioskQueueStatus == "02", queColl.Query.SRQueueingLocation == qLocation, queColl.Query.ParamedicID == qPhysician, queColl.Query.Recall == true);
            if (queColl.LoadAll())
            {
                var dDate = (new DateTime()).NowAtSqlServer();
                foreach (var que in queColl)
                {
                    que.Recall = false;
                    que.LastUpdateByUserID = "caller";
                    que.LastUpdateDateTime = dDate;
                }
                try
                {
                    queColl.Save();

                    var lQue = new List<CustomQueueing>();
                    var stdGroup = new AppStandardReferenceItemCollection();
                    stdGroup.LoadByStandardReferenceID("QueueingGroup");
                    stdGroup.LoadAll();

                    foreach (var que in queColl)
                    {
                        lQue.Add(QueueingToObjectWithAudioInfo(que, stdGroup));
                    }
                    return Json(JSonRetFormatted(lQue));
                }
                catch (Exception e)
                {
                    return Json(JSonRetFormatted(e.Message, false));
                }
            }
            else
            {
                return Json(JSonRetFormatted(null));
            }
        }
        #endregion

        #region Pengambilan Antrian
        public ActionResult GetQueue(string qLocation)
        {
            InitConstanta();
            ViewData["qLocation"] = qLocation;
            ViewData["addjustStartTime"] = AppSession.Parameter.ValueForTakingQueueBeforeStartTime;
            return View();
        }
        #endregion
    }
}
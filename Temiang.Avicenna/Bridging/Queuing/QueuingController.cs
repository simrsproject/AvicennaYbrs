using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Runtime.Serialization;
using System.Net.Http.Formatting;
using Temiang.Avicenna.WebService.V1_1;
using Temiang.Dal.Interfaces;
using System.Web;

namespace Temiang.Avicenna.Bridging.Queuing
{
    public class QueuingController : BaseController
    {
        #region Pengambilan Atrian
        #region static
        private static object PopulateQuota(DateTime dDate)
        {
            dDate = dDate.Date;// (new DateTime()).NowAtSqlServer();
            int dayLen = 0;

            // find paramedic
            var qsup = new ServiceUnitParamedicQuery("sup");
            var qsu = new ServiceUnitQuery("su");
            var qp = new ParamedicQuery("p");
            var qpsd = new ParamedicScheduleDateQuery("psd");


            qsup.InnerJoin(qsu).On(qsup.ServiceUnitID == qsu.ServiceUnitID)
                .InnerJoin(qp).On(qp.ParamedicID == qsup.ParamedicID)
                .InnerJoin(qpsd).On(qp.ParamedicID == qpsd.ParamedicID && qsup.ServiceUnitID == qpsd.ServiceUnitID)
                .Where(
                    qsu.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
                    qsu.IsUsingJobOrder == false,
                    qsu.IsActive == true,
                    qp.IsActive == true,
                    qpsd.ScheduleDate.Between(dDate.Date, dDate.AddDays(dayLen).Date))
                .Select(qsup);

            List<object> lQuota = new List<object>();
            var supColl = new ServiceUnitParamedicCollection();
            if (supColl.Load(qsup)) {
                var parColl = new ParamedicCollection();
                parColl.Query.Where(parColl.Query.ParamedicID.In(supColl.Select(s => s.ParamedicID)));
                parColl.LoadAll();

                var suColl = new ServiceUnitCollection();
                suColl.Query.Where(suColl.Query.ServiceUnitID.In(supColl.Select(s => s.ServiceUnitID)));
                suColl.LoadAll();

                foreach (var sup in supColl) {
                    var dtbSlotTime = AppointmentNRegistrationDataService
                        .AppointmentSlotTime("", sup.ServiceUnitID, sup.ParamedicID, dDate.Date, dDate.Date);

                    var aQuota = new {
                        Paramedic = parColl.Where(p => p.ParamedicID == sup.ParamedicID).Select(p => new { p.ParamedicID, p.ParamedicName}).First(),
                        ServiceUnit = suColl.Where(s => s.ServiceUnitID == sup.ServiceUnitID).Select(s => new { s.ServiceUnitID, s.ServiceUnitName }).First(),
                        QuotaTotal = dtbSlotTime.Rows.Count,
                        QuotaAvailable = dtbSlotTime.AsEnumerable().Where(r => string.IsNullOrEmpty(r["AppointmentStatus"].ToString())).Count()
                    };
                    lQuota.Add(aQuota);
                }
            }

            return lQuota;
        }
        #endregion

        [HttpGet]
        //[Route("api/queuing/getquota/{date}")]
        public HttpResponseMessage GetQuota(string accesskey, string date)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(accesskey);
                var dateS = ValidateDate("date", date);

                var lQuota = PopulateQuota(dateS);

                return WriteResponseAndLog(log,HttpStatusCode.OK, CreateRetObjOK(lQuota));
            }
            catch (Exception ex)
            {
                return WriteResponseAndLog(log, HttpStatusCode.Created, CreateRetObjERR(GetErrorCode(ex.Message), GetErrorMessage(ex.Message)));
            }
        }

        [HttpGet]
        //[Route("api/queuing/GetQueueBeenCalled/{date}")]
        public HttpResponseMessage GetQueueBeenCalled(string accesskey, string date1)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(accesskey);
                var dateS = ValidateDate("date", date1);

                var queColl = new AppointmentQueueingCollection();
                var dtb = queColl.GetLatestCalled(dateS);

                return WriteResponseAndLog(log, HttpStatusCode.OK, CreateRetObjOK(Temiang.Avicenna.Controllers.BaseController.ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                return WriteResponseAndLog(log, HttpStatusCode.Created, CreateRetObjERR(GetErrorCode(ex.Message), GetErrorMessage(ex.Message)));
            }
        }

        [HttpGet]
        //[Route("api/queuing/getquota/{date}")]
        public HttpResponseMessage SetQueue(string accesskey, string date, string serviceUnitId, string paramedicId)
        {
            var log = LogAdd();
            try
            {
                var UserID = ValidateAccessKey(accesskey);
                var dateS = ValidateDate("date", date);
                ValidateStr("paramedicId", paramedicId);
                ValidateStr("serviceUnitId", serviceUnitId);

                // validate service unit
                var su = ValidateServiceUnit(serviceUnitId);
                var par = ValidateParamedic(paramedicId);

                int queNumber = 0;

                //SetUserLoginSession(UserID);
                // insert as appointment
                var que = AppointmentSetQueueBooking(su, par, dateS, "01", queNumber, UserID);

                return WriteResponseAndLog(log, HttpStatusCode.OK, CreateRetObjOK(que));
            }
            catch (Exception ex)
            {
                return WriteResponseAndLog(log, HttpStatusCode.Created, CreateRetObjERR(GetErrorCode(ex.Message), GetErrorMessage(ex.Message)));
            }
        }

        [HttpGet]
        //[Route("api/queuing/getquota/{date}")]
        public HttpResponseMessage SetQueue(string accesskey, string date, string serviceUnitId, string paramedicId, int queNumber)
        {
            var log = LogAdd();
            try
            {
                var UserID = ValidateAccessKey(accesskey);
                var dateS = ValidateDate("date", date);
                ValidateStr("paramedicId", paramedicId);
                ValidateStr("serviceUnitId", serviceUnitId);

                // validate service unit
                var su = ValidateServiceUnit(serviceUnitId);
                var par = ValidateParamedic(paramedicId);

                if (queNumber < 1) {
                    throw new Exception(
                    ErrFieldInvalidValue.Replace(GetErrorMessage(ErrFieldInvalidValue),
                    string.Format("Invalid value for {0}", "queNumber")));
                }

                //SetUserLoginSession(UserID);
                // insert as appointment
                var que = AppointmentSetQueueBooking(su, par, dateS, "01", queNumber, UserID);

                return WriteResponseAndLog(log, HttpStatusCode.OK, CreateRetObjOK(que));
            }
            catch (Exception ex)
            {
                return WriteResponseAndLog(log, HttpStatusCode.Created, CreateRetObjERR(GetErrorCode(ex.Message), GetErrorMessage(ex.Message)));
            }
        }

        [HttpGet]
        //[Route("api/queuing/getquota/{date}")]
        public HttpResponseMessage SetQueueByType(string accesskey, string date, string serviceUnitId, string paramedicId, string type)
        {
            var log = LogAdd();
            try
            {
                var UserID = ValidateAccessKey(accesskey);
                var dateS = ValidateDate("date", date);
                ValidateStr("paramedicId", paramedicId);
                ValidateStr("serviceUnitId", serviceUnitId);
                ValidateStr("type", type);

                var su = ValidateServiceUnit(serviceUnitId);
                var par = ValidateParamedic(paramedicId);
                // validate type
                ValidateQueueingType(type);

                int queNumber = 0;

                //SetUserLoginSession(UserID);
                // insert as appointment
                var que = AppointmentSetQueueBooking(su, par, dateS, type, queNumber, UserID);

                return WriteResponseAndLog(log, HttpStatusCode.OK, CreateRetObjOK(que));
            }
            catch (Exception ex)
            {
                return WriteResponseAndLog(log, HttpStatusCode.Created, CreateRetObjERR(GetErrorCode(ex.Message), GetErrorMessage(ex.Message)));
            }
        }
        [HttpGet]
        //[Route("api/queuing/getquota/{date}")]
        public HttpResponseMessage SetQueueByType(string accesskey, string date, string serviceUnitId, string paramedicId, string type, int queNumber)
        {
            var log = LogAdd();
            try
            {
                var UserID = ValidateAccessKey(accesskey);
                var dateS = ValidateDate("date", date);
                ValidateStr("paramedicId", paramedicId);
                ValidateStr("serviceUnitId", serviceUnitId);
                ValidateStr("type", type);

                var su = ValidateServiceUnit(serviceUnitId);
                var par = ValidateParamedic(paramedicId);
                // validate type
                ValidateQueueingType(type);

                //SetUserLoginSession(UserID);
                // insert as appointment
                var que = AppointmentSetQueueBooking(su, par, dateS, type, queNumber, UserID);

                return WriteResponseAndLog(log, HttpStatusCode.OK, CreateRetObjOK(que));
            }
            catch (Exception ex)
            {
                return WriteResponseAndLog(log, HttpStatusCode.Created, CreateRetObjERR(GetErrorCode(ex.Message), GetErrorMessage(ex.Message)));
            }
        }

        private static object AppointmentSetQueueBooking(ServiceUnit su, Paramedic par,
            DateTime AppointmentDate, string type, int QueNumber, string UserID)
        {
            var psdColl = new ParamedicScheduleDateCollection();
            psdColl.Query.Where(
                psdColl.Query.ServiceUnitID == su.ServiceUnitID,
                psdColl.Query.ParamedicID == par.ParamedicID,
                psdColl.Query.ScheduleDate == AppointmentDate);
            if (psdColl.LoadAll())
            {
                if (psdColl.Count > 1)
                    throw new Exception(ErrDataMultipleScheFound.Replace(GetErrorMessage(ErrDataMultipleScheFound),
                        string.Format("Multiple schedule for service unit {0}, paramedic {1}, date {2}",
                        su.ServiceUnitID, par.ParamedicID, AppointmentDate)));
                var psd = psdColl.First();

                var ps = new ParamedicSchedule();
                ps.Query.Where(
                    ps.Query.ServiceUnitID == psd.ServiceUnitID,
                    ps.Query.ParamedicID == psd.ParamedicID,
                    ps.Query.PeriodYear == psd.PeriodYear
                );
                if (!ps.Load(ps.Query))
                    throw new Exception(ErrDataScheNotFound.Replace(GetErrorMessage(ErrDataScheNotFound),
                        string.Format("Related schedule not found for service unit {0}, paramedic {1}, date {2}",
                    su.ServiceUnitID, par.ParamedicID, AppointmentDate)));

                // validasi dokter cuti
                string valMsg = RegistrationWS.ValidatePhycisianOnLeave(par.ParamedicID, (new DateTime()).NowAtSqlServer().Date, "en");
                if (!string.IsNullOrEmpty(valMsg))
                {
                    throw new Exception(ErrDataApptSlotNotFound.Replace(GetErrorMessage(ErrDataApptSlotNotFound),
                            string.Format("Selected paramedic is on leave, paramedic {0}, date {1}",
                        par.ParamedicID, AppointmentDate)));
                }

                //// validasi nomor antrian sudah diambil atau blm
                //var apptColl = new AppointmentCollection();
                //apptColl.Query.Where(apptColl.Query.AppointmentDate == AppointmentDate,
                //    apptColl.Query.ServiceUnitID == ServiceUnitID,
                //    apptColl.Query.ParamedicID == ParamedicID,
                //    apptColl.Query.AppointmentQue == QueNumber);
                //if (apptColl.LoadAll()) {
                //    throw new Exception(ErrDataApptConflict.Replace(GetErrorMessage(ErrDataApptConflict),
                //        string.Format("Queue number {0} has been taken", QueNumber.ToString())));
                //}

                //if (QueNumber == 0) {
                //    var apptColl = new AppointmentCollection();
                //    apptColl.Query.Where(
                //        apptColl.Query.AppointmentDate == AppointmentDate,
                //        apptColl.Query.ServiceUnitID == su.ServiceUnitID,
                //        apptColl.Query.ParamedicID == par.ParamedicID,
                //        apptColl.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                //    ).OrderBy(apptColl.Query.AppointmentQue.Descending);
                //    apptColl.Query.es.Top = 1;
                //    if (!apptColl.LoadAll())
                //    {
                //        QueNumber = 1;
                //    }
                //    else {
                //        QueNumber = apptColl.First().AppointmentQue.Value + 1;
                //    }
                //}

                BusinessObject.Appointment apt = new BusinessObject.Appointment();
                apt.ServiceUnitID = su.ServiceUnitID;
                apt.ParamedicID = par.ParamedicID;
                apt.PatientID = null;
                apt.AppointmentDate = AppointmentDate.Date;

                apt.VisitTypeID = "VT001";
                apt.VisitDuration = Convert.ToByte(ps.ExamDuration ?? 0);
                apt.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusBooked; // Queue Booking
                apt.SRAppoinmentType = null;
                apt.FirstName = "";
                apt.MiddleName = "";
                apt.LastName = "";
                apt.DateOfBirth = null;
                apt.GuarantorID = null;
                apt.Notes = "";

                apt.StreetName = "";
                apt.District = "";
                apt.City = "";
                apt.County = "";
                apt.State = "";
                apt.ZipCode = null;
                apt.PhoneNo = "";
                apt.Email = "";
                apt.FaxNo = string.Empty;
                apt.BirthPlace = null;
                apt.Sex = null;
                apt.Ssn = null;
                apt.MobilePhoneNo = "";

                // bpjs
                apt.GuarantorCardNo = null;
                apt.ReferenceNumber = null;
                apt.ReferenceType = null;

                var dtbSlots = AppointmentWS.AppointmentSlotTime("", su.ServiceUnitID, par.ParamedicID, AppointmentDate, AppointmentDate);

                DataRow slot = null;
                if (QueNumber == 0)
                {
                    slot = dtbSlots.AsEnumerable().Where(r => string.IsNullOrEmpty(r["AppointmentNo"].ToString()))
                        .OrderBy(r => r["AppointmentTime"].ToString()).FirstOrDefault();
                    if (slot == null) {
                        throw new Exception(ErrDataApptSlotNotFound.Replace(GetErrorMessage(ErrDataApptSlotNotFound), string.Format("Queue slot is full")));
                    }
                }
                else {
                    var slots = dtbSlots.AsEnumerable().Where(r => System.Convert.ToInt32(r["AppointmentQue"]) == QueNumber);

                    switch (slots.Count())
                    {
                        case 0:
                            {
                                throw new Exception(ErrDataApptSlotNotFound.Replace(GetErrorMessage(ErrDataApptSlotNotFound), string.Format("Queue slot is full")));
                                break;
                            }
                        case 1:
                            {
                                slot = slots.First();
                                if (!string.IsNullOrEmpty(slot["AppointmentStatus"].ToString()))
                                {
                                    throw new Exception(ErrDataApptConflict.Replace(GetErrorMessage(ErrDataApptConflict),
                                        string.Format("Queue number {0} has been taken", QueNumber.ToString())));
                                }
                                break;
                            }
                        default:
                            {
                                throw new Exception(ErrDataMultipleApptSlotFound.Replace(GetErrorMessage(ErrDataMultipleApptSlotFound),
                                                   string.Format("Duplicate queue slot {0}", QueNumber)));
                                break;
                            }
                    }
                }
                
                apt.AppointmentTime = slot["AppointmentTime"].ToString();
                apt.AppointmentQue = System.Convert.ToInt32(slot["AppointmentQue"]); //QueNumber;

                apt.SRAppoinmentType = "QRS";

                var aptQue = new AppointmentQueueing();
                using (esTransactionScope trans = new esTransactionScope())
                {
                    var cDate = (new DateTime()).NowAtSqlServer();

                    AppAutoNumberLast _autoNumber;
                    _autoNumber = Helper.GetNewAutoNumber(apt.AppointmentDate.Value, AppEnum.AutoNumber.AppointmentNo, "", UserID);
                    apt.AppointmentNo = _autoNumber.LastCompleteNumber;
                    _autoNumber.Save();

                    apt.LastCreateByUserID = UserID;
                    apt.LastCreateDateTime = cDate;

                    //Last Update Status
                    apt.LastUpdateByUserID = UserID;
                    apt.LastUpdateDateTime = cDate;

                    apt.Save();

                    aptQue.SetQueForReg(apt, type, su, UserID, true); 
                    aptQue.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                var pSlip = new PrintJobParameterCollection();
                pSlip.AddNew("p_KioskQueueNo", aptQue.FormattedNo, null, null);
                PrintManager.CreatePrintJob(AppSession.Parameter.KioskQueueSlipRpt, pSlip, UserID,
                    HttpContext.Current.Request.UserHostName);

                return new { QueueingNo = apt.AppointmentQue.Value, QueueingCode = aptQue.FormattedNo, AppoitmentNo = aptQue.AppointmentNo };
            }
            else
            {
                throw new Exception(ErrDataScheNotFound.Replace(GetErrorMessage(ErrDataScheNotFound),
                    string.Format("Related schedule not found for service unit {0}, paramedic {1}, date {2}",
                        su.ServiceUnitName, par.ParamedicName, AppointmentDate)));
            }
        }

        [HttpGet]
        //[Route("api/queuing/GetListQueueRegistrationBeenCalled/{date}")]
        public HttpResponseMessage GetListQueueRegistrationBeenCalled(string accesskey, string date2)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(accesskey);
                var date = ValidateDate("date", date2);

                var queColl = new AppointmentQueueingCollection();
                var dtb = queColl.GetLastCalledRegistration(date);

                return WriteResponseAndLog(log, HttpStatusCode.OK, CreateRetObjOK(Temiang.Avicenna.Controllers.BaseController.ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                return WriteResponseAndLog(log, HttpStatusCode.Created, CreateRetObjERR(GetErrorCode(ex.Message), GetErrorMessage(ex.Message)));
            }
        }

        [HttpGet]
        //[Route("api/queuing/GetListQueuePolyclinicBeenCalled/{date}")]
        public HttpResponseMessage GetListQueuePolyclinicBeenCalled(string accesskey, string date3)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(accesskey);
                var date = ValidateDate("date", date3);

                var queColl = new AppointmentQueueingCollection();
                var dtb = queColl.GetLastCalledPolyclinic(date);

                return WriteResponseAndLog(log, HttpStatusCode.OK, CreateRetObjOK(Temiang.Avicenna.Controllers.BaseController.ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                return WriteResponseAndLog(log, HttpStatusCode.Created, CreateRetObjERR(GetErrorCode(ex.Message), GetErrorMessage(ex.Message)));
            }
        }

        [HttpGet]
        //[Route("api/queuing/GetListQueuePrefix/{date}")]
        public HttpResponseMessage GetListQueuePrefix(string accesskey, string date1, string prefix)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(accesskey);
                var date = ValidateDate("date", date1);
                ValidateStr("prefix", prefix);

                var queColl = new AppointmentQueueingCollection();
                var dtb = queColl.GetListQueuePrefix(date, prefix);

                return WriteResponseAndLog(log, HttpStatusCode.OK, CreateRetObjOK(Temiang.Avicenna.Controllers.BaseController.ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                return WriteResponseAndLog(log, HttpStatusCode.Created, CreateRetObjERR(GetErrorCode(ex.Message), GetErrorMessage(ex.Message)));
            }
        }

        [HttpGet]
        //[Route("api/queuing/GetListQueueBeingServedByPrefix/{date}")]
        public HttpResponseMessage GetListQueueBeingServedByPrefix(string accesskey, string date2, string prefix)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(accesskey);
                var date = ValidateDate("date", date2);
                ValidateStr("prefix", prefix);

                var queColl = new AppointmentQueueingCollection();
                var dtb = queColl.GetLastCalledBeeingServed(date, prefix);

                return WriteResponseAndLog(log, HttpStatusCode.OK, CreateRetObjOK(Temiang.Avicenna.Controllers.BaseController.ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                return WriteResponseAndLog(log, HttpStatusCode.Created, CreateRetObjERR(GetErrorCode(ex.Message), GetErrorMessage(ex.Message)));
            }
        }
        #endregion

        #region Pemanggilan Antrian
        #endregion
    }
}
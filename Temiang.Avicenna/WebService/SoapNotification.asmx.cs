using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for SoapNotification
    /// </summary>
    [ScriptService]
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SoapNotification : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public string WidthAndHeightScreenClient(string width, string height)
        {
            //AppSession.ScreenClient.Width = width.ToInt();
            //AppSession.ScreenClient.Height = height.ToInt();
            return string.Empty;
        }

        [WebMethod]
        public string GetTotalSoap(object context)
        {
            IDictionary<string, object> contextDictionary = (IDictionary<string, object>)context;
            string[] value = ((string)contextDictionary["Value"]).Split(';');

            string response = "<b>&nbsp;&nbsp;" + value[1] + "</b>" +
                string.Format("<hr />&nbsp;&nbsp;<a href=\"#\" onclick=\"OnSoapReload('{0}'); return false;\">You have {1} patient with no assesment.</a>", value[0], value[2]);
            return response;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTotalSoapOutstanding(string date, string serviceUnitID, string paramedicID)
        {
            try
            {
                return TransCharges(Convert.ToDateTime(date), serviceUnitID, paramedicID).ToString();
            }
            catch
            {
                return "0";
            }
        }

        private int TransCharges(DateTime date, string serviceUnitID, string paramedicID)
        {
            var dtb = TransChargesOutPatient(date, serviceUnitID, paramedicID);

            var rooms = new ServiceRoomCollection();
            rooms.Query.Where(
                rooms.Query.IsOperatingRoom == true,
                rooms.Query.IsActive == true
                );
            rooms.LoadAll();

            var r = (from i in rooms
                     where i.ServiceUnitID == serviceUnitID && i.IsOperatingRoom == true
                     select i.ServiceUnitID).Distinct().SingleOrDefault();

            if (r != null)
            {
                var tab = dtb.AsEnumerable().Where(t => t.Field<string>("RoomName") == null);
                foreach (var row in tab)
                {
                    row.Delete();
                }

                dtb.AcceptChanges();
            }
            else
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (rooms.Select(x => x.ServiceUnitID).Distinct().SingleOrDefault(i => i == row["ServiceUnitID"].ToString()) != null)
                    {
                        var booking = new ServiceUnitBookingQuery();
                        booking.Where(booking.ServiceUnitID == row["ServiceUnitID"].ToString() &&
                            booking.RegistrationNo == row["RegistrationNo"].ToString());

                        var book = new ServiceUnitBooking();
                        if (!book.Load(booking))
                            row.Delete();
                    }
                }

                dtb.AcceptChanges();

                foreach (var row in dtb.Rows.Cast<DataRow>().Where(row => rooms.Select(x => x.ServiceUnitID).Distinct().Contains(row["ServiceUnitID"].ToString()) && row["QueNo"].ToString() == "0"))
                {
                    row.Delete();
                }

                dtb.AcceptChanges();
            }

            int count = 0;
            foreach (DataRow row in dtb.Rows)
            {
                var soapColl = new EpisodeSOAPECollection();
                soapColl.Query.Where(soapColl.Query.RegistrationNo == row["RegistrationNo"].ToString(),
                                     soapColl.Query.ParamedicID == row["ParamedicID"].ToString(),
                                     soapColl.Query.IsVoid == false);
                soapColl.LoadAll();

                if (soapColl.Count == 0) count++;

            }

            return count;
        }

        private DataTable TransChargesOutPatient(DateTime date, string serviceUnitID, string paramedicID)
        {
            var unit = new ServiceUnitQuery("b");
            var room = new ServiceRoomQuery("c");
            var medic = new ParamedicQuery("d");
            var query = new RegistrationQuery("e");
            var patient = new PatientQuery("f");
            var grr = new GuarantorQuery("g");
            //var mb = new MergeBillingQuery("mb");
            var rmb = new RegistrationQuery("rmb");
            var pmb = new ParamedicQuery("pmb");
            var sumInfo = new RegistrationInfoSumaryQuery("h");

            query.es.Top = 100;

            query.Select
                (
                    room.RoomName,
                    query.RegistrationDate,
                    query.RegistrationQue.As("QueNo"),
                    unit.ServiceUnitID,
                    query.ParamedicID,
                    medic.ParamedicName,
                    query.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    patient.Sex,
                    grr.GuarantorName,
                    query.PatientID,
                    query.IsConsul,
                    query.SRRegistrationType,
                    query.RoomID,
                    "<CAST(0 AS BIT) AS IsEpisodeSOAP>",
                    "<CAST (0 AS BIT) AS IsDiagnosis>",
                    pmb.ParamedicName.As("ReferFrom"),
                    "<'' AS ReferTo>",
                    query.RegistrationTime,
                    "<'' AS SRTriage>",
                    query.RegistrationQue,
                    query.IsConfirmedAttendance,
                    query.IsNewPatient,
                    sumInfo.NoteCount
                );

            query.LeftJoin(room).On(query.RoomID == room.RoomID);
            query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
            query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
            query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
            //query.InnerJoin(mb).On(query.RegistrationNo == mb.RegistrationNo);
            //query.LeftJoin(rmb).On(mb.FromRegistrationNo == rmb.RegistrationNo);
            query.LeftJoin(rmb).On(query.FromRegistrationNo == rmb.RegistrationNo);
            query.LeftJoin(pmb).On(rmb.ParamedicID == pmb.ParamedicID);
            query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo & sumInfo.NoteCount > 0);

            query.Where(
                query.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
                query.IsVoid == false,
                query.IsFromDispensary == false
            );
            if (!string.IsNullOrEmpty(serviceUnitID))
            {
                var rooms = new ServiceRoomCollection();
                rooms.Query.Where(
                    rooms.Query.IsOperatingRoom == true,
                    rooms.Query.IsActive == true
                    );
                rooms.LoadAll();

                var r = (rooms.Where(i => i.ServiceUnitID == serviceUnitID && i.IsOperatingRoom == true)
                              .Select(i => i.ServiceUnitID)).Distinct().SingleOrDefault();

                if (r != null)
                {
                    var booking = new ServiceUnitBookingQuery("x");

                    query.InnerJoin(booking).On(query.RegistrationNo == booking.RegistrationNo);
                    query.InnerJoin(unit).On(booking.ServiceUnitID == unit.ServiceUnitID);
                    query.Where(booking.IsApproved == true);
                    query.OrderBy(booking.BookingDateTimeFrom.Ascending);
                }
                else
                {
                    query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                    query.Where(query.ServiceUnitID == serviceUnitID);
                }
            }
            else
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
            if (!string.IsNullOrEmpty(paramedicID))
                query.Where(query.ParamedicID == paramedicID);

            query.Where(query.RegistrationDate == date);


            var group = new esQueryItem(query, "Group", esSystemType.String);
            group = unit.ServiceUnitName;

            query.Select(group.As("Group"));

            query.OrderBy
                (
                    query.ParamedicID.Ascending,
                    query.RegistrationDate.Descending,
                    query.RegistrationTime.Ascending,
                    query.RegistrationNo.Descending,
                    query.RegistrationQue.Ascending
                );

            DataTable dtb = query.LoadDataTable();

            //foreach (DataRow row in dtb.Rows)
            //{
            //    var referTo = string.Empty;
            //    var mbcoll = new MergeBillingCollection();
            //    mbcoll.Query.Where(mbcoll.Query.FromRegistrationNo == row["RegistrationNo"].ToString());
            //    mbcoll.LoadAll();
            //    foreach (var c in mbcoll)
            //    {
            //        var r = new Registration();
            //        r.LoadByPrimaryKey(c.RegistrationNo);
            //        if (r.IsVoid == false)
            //        {
            //            var p = new Paramedic();
            //            p.LoadByPrimaryKey(r.ParamedicID);
            //            referTo += p.ParamedicName + ";";
            //        }
            //    }

            //    if (referTo != string.Empty)
            //        referTo = referTo.Remove(referTo.Length - 1);
            //    row["ReferTo"] = referTo;

            //var phr = new PatientHealthRecordLineCollection();
            //phr.Query.Where(phr.Query.RegistrationNo == row["RegistrationNo"].ToString(),
            //                phr.Query.QuestionFormID == "PHYEXAM");

            //var phrC = new PatientHealthRecordLineCollection();
            //var phr = new PatientHealthRecordLineQuery("phr");
            //var qf = new QuestionFormQuery("qf");
            //phr.InnerJoin(qf).On(phr.QuestionFormID == qf.QuestionFormID)
            //    .Where(phr.RegistrationNo == row["RegistrationNo"].ToString(),
            //                qf.IsVSignForm == true);

            //phrC.Load(phr);
            //if (phrC.Count > 0)
            //{
            //    row["SRTriage"] = "99";
            //}
            //else {
            //    var phrColl = new PatientHealthRecordLineCollection();
            //    phrColl.Query.Where(phrColl.Query.RegistrationNo == row["RegistrationNo"].ToString(),
            //                    phrColl.Query.QuestionFormID == "PHYEXAM");
            //    phrColl.LoadAll();
            //    if (phrColl.Count > 0)
            //    {
            //        row["SRTriage"] = "99";
            //    }
            //}

            //}
            //dtb.AcceptChanges();

            return dtb;
        }
    }
}

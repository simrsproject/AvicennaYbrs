using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using Newtonsoft.Json;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppointmentLokadokCollection
    {
        public bool LoadGridDataSource(DateTime Date, string ParamedicID)
        {
            long fromUtDate = ConvertToUnixTime(Date);
            long toUtDate = ConvertToUnixTime(Date.AddDays(1));

            this.ClearData();

            var Appt = new AppointmentLokadokQuery("a");
            var Pat = new PatientQuery("b");
            var Par = new ParamedicQuery("c");
            var Reg = new RegistrationQuery("d");
            Appt.LeftJoin(Pat).On(Appt.PId.Equal(Pat.MedicalNo) && Pat.MedicalNo != string.Empty)
                .LeftJoin(Par).On(Appt.DocId.Equal(Par.ParamedicID))
                .LeftJoin(Reg).On(Appt.RegistrationNo.Equal(Reg.RegistrationNo) && Reg.IsVoid.Equal(false))
                .Where(Appt.StartDate >= fromUtDate, Appt.StartDate < toUtDate/*, Appt.Confirmed.Equal("1")*/)
                .Select(
                    Appt,
                    "<ISNULL(d.RegistrationNo,'') refToRegistration_RegistrationNo>",
                    "<ISNULL(d.RegistrationQue,'') refToRegistration_RegistrationQue>",
                    //Pat.FirstName.As("refToPatient_FirstName"),
                    "<ISNULL(b.FirstName, a.p_name) refToPatient_FirstName>",
                    Pat.MiddleName.As("refToPatient_MiddleName"),
                    Pat.LastName.As("refToPatient_LastName"),
                    Par.ParamedicName.As("refToParamedic_ParamedicName"),
                    "<CAST(CASE ISNULL(b.PatientID,'') WHEN '' THEN 1 ELSE 0 END as BIT) refToPatient_PatientNotFound>"
                ).OrderBy(Appt.StartDate.Ascending, Appt.PId.Ascending);
            if (!string.IsNullOrEmpty(ParamedicID)) {
                Appt.Where(Appt.DocId.Equal(ParamedicID));
            }
            return this.Load(Appt);
        }

        public bool LoadByIdAppointment(long[] ids) {
            this.ClearData();
            if (ids.Count() == 0) return false;

            var Appt = new AppointmentLokadokQuery("a");
            Appt.Where(Appt.ApptId.In(ids));
            return this.Load(Appt);
        }

        /// <summary>
        /// Convert a date time object to Unix time representation.
        /// </summary>
        /// <param name="datetime">The datetime object to convert to Unix time stamp.</param>
        /// <returns>Returns a numerical representation (Unix time) of the DateTime object.</returns>
        public static long ConvertToUnixTime(DateTime datetime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
            //sTime = sTime.ToLocalTime();
            return (long)(datetime - sTime).TotalSeconds;
        }

        public static long ConvertToUnixTimeMillis(DateTime d)
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
            return (long)(d - UnixEpoch).TotalMilliseconds;
        }

        /// <summary>
        /// Convert Unix time value to a DateTime object.
        /// </summary>
        /// <param name="unixtime">The Unix time stamp you want to convert to DateTime.</param>
        /// <returns>Returns a DateTime object that represents value of the Unix time.</returns>
        public static DateTime UnixTimeToDateTime(long unixtime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            sTime = sTime.ToLocalTime();
            return sTime.AddSeconds(unixtime);
        }
    }
}

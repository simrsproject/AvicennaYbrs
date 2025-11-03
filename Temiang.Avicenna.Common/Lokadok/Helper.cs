using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Common.Lokadok
{
    public class Helper
    {
        #region Setting
        private static string GetBaseUrl()
        {
            return ConfigurationManager.AppSettings["LokadokServiceUrlLocation"];
        }
        private static string GetSubDomain()
        {
            return ConfigurationManager.AppSettings["LokadokServiceSubDomain"];
        }
        private static string GetPassKey()
        {
            return ConfigurationManager.AppSettings["LokadokServicePassKey"];
        }
        private static string GetPlace()
        {
            return ConfigurationManager.AppSettings["LokadokServicePlace"];
        }
        #endregion

        #region Add Patient
        public static bool AddPatient(string medicalno, string name, string mobile, DateTime dob, string gender)
        {
            var pid = medicalno;
            var pfname = name;
            string pmobile = mobile;
            long pdob = AppointmentLokadokCollection.ConvertToUnixTimeMillis(dob);
            string pgender = MFtoLP(gender);

            try
            {
                string sUrl = string.Format("{0}set/add_patient?id={1}&passkey={2}&pid={3}&pfname={4}&pmobile={5}&pdob={6}&pgender={7}", 
                    GetBaseUrl(), GetSubDomain(), GetPassKey(), pid, pfname, pmobile, pdob, pgender);

                //return false;

                var sResponse = Common.Helper.WebRequestGet(sUrl);
                return sResponse.Trim().Equals("0");
            }
            catch (Exception e) {
                return false;
            }
        }

        private static string MFtoLP(string gender)
        {
            return gender.ToUpper().Equals("M") ? "L" : "P";
        }
        #endregion

        public class ApptLokadok {
            public string appt_id;
            public long start_date;
            public long end_date;
            public string place;
            public string p_id;
            public string p_name;
            public string p_dob;
            public string p_gender;
            public string p_mobile;
            public string p_insurance;
            public string p_ins_id;
            public string p_ins_number;
            public string lkd_pid;
            public string doc_id;
            public string doctor;
            public string doc_spc;
            public string poly_id;
            public string notes;
            public string reason_visit;
            public string confirmed;
            public string checked_in;
            public int new_patient;
            public string booking_code;
            public string status;
        }

        public static int[] UpdateFromLokadokTest1(AppointmentLokadokCollection coll, DateTime Date)
        {
            //try
            //{
            //string sResponse = "[{\"appt_id\":\"2\",\"start_date\":1490877600,\"end_date\":1490878200,\"place\":\"28127\",\"p_id\":\"09-12-77\",\"p_name\":\"Theo Daffin\",\"p_dob\":\"1443600000\",\"p_gender\":\"Pria\",\"p_mobile\":\"85720202020\",\"p_insurance\":null,\"p_ins_id\":\"0\",\"p_ins_number\":null,\"lkd_pid\":\"290825\",\"doc_id\":\"DS.0006\",\"doctor\":\"Aditya Suryansyah, Spa\",\"doc_spc\":\"Anak\",\"poly_id\":null,\"notes\":\"Imunisasi\",\"reason_visit\":\"12 Bln Cek Kesehatan Bayi\",\"confirmed\":\"1\",\"checked_in\":\"0\",\"new_patient\":0,\"booking_code\":\"DNM2VSIG\",\"status\":\"BOOK\"},{\"appt_id\":\"3\",\"start_date\":1490878800,\"end_date\":1490879400,\"place\":\"28127\",\"p_id\":\"09-12-75\",\"p_name\":\"Randi Satria Eka Putra\",\"p_dob\":\"1184400000\",\"p_gender\":\"Pria\",\"p_mobile\":\"85966656018\",\"p_insurance\":null,\"p_ins_id\":\"0\",\"p_ins_number\":null,\"lkd_pid\":\"290826\",\"doc_id\":\"DS.0006\",\"doctor\":\"Aditya Suryansyah, Spa\",\"doc_spc\":\"Anak\",\"poly_id\":null,\"notes\":\"AMpek\",\"reason_visit\":\"Konsultasi Anak\",\"confirmed\":\"1\",\"checked_in\":\"0\",\"new_patient\":0,\"booking_code\":\"UV96SV8H\",\"status\":\"BOOK\"},{\"appt_id\":\"4\",\"start_date\":1490879400,\"end_date\":1490880000,\"place\":\"28127\",\"p_id\":\"09-12-76\",\"p_name\":\"Vino Bastian\",\"p_dob\":\"1357200000\",\"p_gender\":\"Pria\",\"p_mobile\":\"\",\"p_insurance\":null,\"p_ins_id\":\"0\",\"p_ins_number\":null,\"lkd_pid\":\"290827\",\"doc_id\":\"DS.0006\",\"doctor\":\"Aditya Suryansyah, Spa\",\"doc_spc\":\"Anak\",\"poly_id\":null,\"notes\":\"Bengek\",\"reason_visit\":\"Konsultasi Anak\",\"confirmed\":\"1\",\"checked_in\":\"0\",\"new_patient\":0,\"booking_code\":\"CVMVAN8W\",\"status\":\"BOOK\"}]";

            string sUrl = string.Format("{0}get/appointment?id={1}&passkey={2}{3}&start={4}&end={5}&type=JSON&unread=0",
                GetBaseUrl(), GetSubDomain(), GetPassKey(),
                GetPlace().Equals(string.Empty) ? "" : ("&place=" + GetPlace()),
                Date.ToString("yyyyMMdd"), Date.ToString("yyyyMMdd"));
            var sResponse = Common.Helper.WebRequestGet(sUrl);

            if (sResponse.Equals("8")) return new int[]{0,0};

            sResponse = sResponse.Replace("null", "\"\"");

            //var appts = JsonConvert.DeserializeObject<ApptLokadok[]>(sResponse);

            // to deserialize a string to an object
            //var appts = fastJSON.JSON.Instance.ToObject(sResponse);
            var appts = fastJSON.JSON.ToObject<ApptLokadok[]>(sResponse);

            // to deserialize a string to an object
            //var newobj = fastJSON.JSON.Instance.ToObject(sResponse);

            // refresh data
            //coll.LoadGridDataSource(Date);
            coll.LoadByIdAppointment(appts.Select(x => Int64.Parse(x.appt_id)).ToArray());

            var iCountNew = 0;
            var iCountUpdate = 0;
            foreach (var appt in appts)
            {
                var _appt = coll.Where(x => x.ApptId.Equals(Int64.Parse(appt.appt_id))).FirstOrDefault();
                if (_appt == null)
                {
                    _appt = coll.AddNew();
                    _appt.ApptId = Int64.Parse(appt.appt_id);
                    iCountNew++;
                }
                else
                {
                    // udpate aja lah kalau belum registrasi
                    if (!string.IsNullOrEmpty(_appt.RegistrationNo)) continue;
                }
                _appt.StartDate = appt.start_date;
                _appt.EndDate = appt.end_date;
                _appt.Place = Int32.Parse(appt.place);
                _appt.PId = appt.p_id;
                _appt.PName = appt.p_name;

                _appt.PDob = 0;
                try
                {
                    _appt.PDob = Int64.Parse(appt.p_dob);
                }catch{
                    
                }
                _appt.PGender = appt.p_gender;
                _appt.PMobile = appt.p_mobile;
                _appt.PInsurance = appt.p_insurance;
                _appt.PInsId = appt.p_ins_id;
                _appt.PInsNumber = appt.p_ins_number;
                _appt.LkdPid = appt.lkd_pid;
                _appt.DocId = appt.doc_id;
                _appt.Doctor = appt.doctor;
                _appt.DocSpc = appt.doc_spc;
                _appt.PolyId = appt.poly_id;
                _appt.Notes = appt.notes;
                _appt.ReasonVisit = appt.reason_visit;
                _appt.Confirmed = appt.confirmed;
                _appt.CheckedIn = appt.checked_in;
                _appt.NewPatient = appt.new_patient == 1;
                _appt.BookingCode = appt.booking_code;
                _appt.Status = appt.status;
                iCountUpdate++;
            }

            coll.Save();
            return new int[]{iCountNew, iCountUpdate};
            //}
            //catch (Exception e)
            //{
            //    return -1;
            //}
        }


        #region Appointment
        /// <summary>
        /// Get Latest Data From Lokadok
        /// </summary>
        /// <param name="Date"></param>
        /// <returns>-1: failed, 0: data is uptodate, n: n new data</returns>
        public static int UpdateFromLokadok(AppointmentLokadokCollection coll, DateTime Date)
        {
            //try
            //{
                //string sResponse = "[{\"appt_id\":\"2\",\"start_date\":1490877600,\"end_date\":1490878200,\"place\":\"28127\",\"p_id\":\"09-12-77\",\"p_name\":\"Theo Daffin\",\"p_dob\":\"1443600000\",\"p_gender\":\"Pria\",\"p_mobile\":\"85720202020\",\"p_insurance\":null,\"p_ins_id\":\"0\",\"p_ins_number\":null,\"lkd_pid\":\"290825\",\"doc_id\":\"DS.0006\",\"doctor\":\"Aditya Suryansyah, Spa\",\"doc_spc\":\"Anak\",\"poly_id\":null,\"notes\":\"Imunisasi\",\"reason_visit\":\"12 Bln Cek Kesehatan Bayi\",\"confirmed\":\"1\",\"checked_in\":\"0\",\"new_patient\":0,\"booking_code\":\"DNM2VSIG\",\"status\":\"BOOK\"},{\"appt_id\":\"3\",\"start_date\":1490878800,\"end_date\":1490879400,\"place\":\"28127\",\"p_id\":\"09-12-75\",\"p_name\":\"Randi Satria Eka Putra\",\"p_dob\":\"1184400000\",\"p_gender\":\"Pria\",\"p_mobile\":\"85966656018\",\"p_insurance\":null,\"p_ins_id\":\"0\",\"p_ins_number\":null,\"lkd_pid\":\"290826\",\"doc_id\":\"DS.0006\",\"doctor\":\"Aditya Suryansyah, Spa\",\"doc_spc\":\"Anak\",\"poly_id\":null,\"notes\":\"AMpek\",\"reason_visit\":\"Konsultasi Anak\",\"confirmed\":\"1\",\"checked_in\":\"0\",\"new_patient\":0,\"booking_code\":\"UV96SV8H\",\"status\":\"BOOK\"},{\"appt_id\":\"4\",\"start_date\":1490879400,\"end_date\":1490880000,\"place\":\"28127\",\"p_id\":\"09-12-76\",\"p_name\":\"Vino Bastian\",\"p_dob\":\"1357200000\",\"p_gender\":\"Pria\",\"p_mobile\":\"\",\"p_insurance\":null,\"p_ins_id\":\"0\",\"p_ins_number\":null,\"lkd_pid\":\"290827\",\"doc_id\":\"DS.0006\",\"doctor\":\"Aditya Suryansyah, Spa\",\"doc_spc\":\"Anak\",\"poly_id\":null,\"notes\":\"Bengek\",\"reason_visit\":\"Konsultasi Anak\",\"confirmed\":\"1\",\"checked_in\":\"0\",\"new_patient\":0,\"booking_code\":\"CVMVAN8W\",\"status\":\"BOOK\"}]";

            string sUrl = string.Format("{0}get/appointment?id={1}&passkey={2}{3}&start={4}&end={5}&type=JSON&unread=0",
                GetBaseUrl(), GetSubDomain(), GetPassKey(),
                GetPlace().Equals(string.Empty) ? "" : ("&place=" + GetPlace()),
                Date.ToString("yyyyMMdd"), Date.ToString("yyyyMMdd"));
            var sResponse = Common.Helper.WebRequestGet(sUrl);

                if (sResponse.Equals("8")) return 0;

                sResponse = sResponse.Replace("_", "");
                var appts = JsonConvert.DeserializeObject<AppointmentLokadok[]>(sResponse);

                // to deserialize a string to an object
                //var newobj = fastJSON.JSON.Instance.ToObject(sResponse);

                // refresh data
                //coll.LoadGridDataSource(Date);
                coll.LoadByIdAppointment(appts.Select(x => x.ApptId.Value).ToArray());

                var newAppt = appts.Where(x => !(coll.Where(y => (y.BookingCode == null ? string.Empty : y.BookingCode) != string.Empty).Select(y => y.ApptId)).Contains(x.ApptId));
                var iCount = newAppt.Count();
                foreach (var appt in newAppt)
                {
                    var oldApp = coll.Where(x => x.ApptId.Equals(appt.ApptId)).FirstOrDefault();
                    if (oldApp == null)
                    {
                        appt.AddNew();
                        coll.AttachEntity(appt);
                    }
                    else {
                        oldApp.StartDate = appt.StartDate;
                        oldApp.EndDate = appt.EndDate;
                        oldApp.Place = appt.Place;
                        oldApp.PId = appt.PId;
                        oldApp.PName = appt.PName;
                        oldApp.PDob = appt.PDob;
                        oldApp.PGender = appt.PGender;
                        oldApp.PMobile = appt.PMobile;
                        oldApp.PInsurance = appt.PInsurance;
                        oldApp.PInsId = appt.PInsId;
                        oldApp.PInsNumber = appt.PInsNumber;
                        oldApp.LkdPid = appt.LkdPid;
                        oldApp.DocId = appt.DocId;
                        oldApp.Doctor = appt.Doctor;
                        oldApp.DocSpc = appt.DocSpc;
                        oldApp.PolyId = appt.PolyId;
                        oldApp.Notes = appt.Notes;
                        oldApp.ReasonVisit = appt.ReasonVisit;
                        oldApp.Confirmed = appt.Confirmed;
                        oldApp.CheckedIn = appt.CheckedIn;
                        oldApp.NewPatient = appt.NewPatient;
                        oldApp.BookingCode = appt.BookingCode;
                        oldApp.Status = appt.Status;
                        oldApp.RegistrationNo = appt.RegistrationNo;
                    }
                }

                coll.Save();
                return iCount;
            //}
            //catch (Exception e)
            //{
            //    return -1;
            //}
        }

        public static AppointmentLokadok GetByRegistrationNoSender(string RegistrationNoSender) {
            var lCol = new AppointmentLokadokCollection();
            lCol.Query.Where(lCol.Query.RegistrationNoSender.Equal(RegistrationNoSender));
            if (lCol.LoadAll())
            {
                return lCol.First();
            }
            else {
                return null;
            }
        }

        /// <summary>
        /// Send appointment to lokadok
        /// </summary>
        /// <param name="ApptDate"></param>
        /// <param name="ApptTime">Format hh:mm</param>
        /// <param name="MedicalNo"></param>
        /// <param name="ParamedicID"></param>
        /// <param name="Reason">2 = Konsultasi Umum; 3 = Follow Up / Kontrol</param>
        /// <param name="notes">Catatan bagi staff admisi</param>
        /// <returns>Sukses => Integer Positif dari ID pertemuan, Gagal => TRANS_FAIL (-204)</returns>
        public static int SendAppt(DateTime ApptDate, string ApptTime, string MedicalNo, string ParamedicID,
            int Reason, string notes, string RegistrationNoSender) {
            try
            {
                string sUrl = string.Format(
                    "{0}set/insert_appt?id={1}&passkey={2}{3}&date={4}&time={5}&pid={6}&doc_id={7}&reason={8}&notes={9}",
                    GetBaseUrl(), GetSubDomain(), GetPassKey(),
                    GetPlace().Equals(string.Empty) ? "" : ("&place=" + GetPlace()),
                    ApptDate.ToString("yyyyMMdd"),
                    string.IsNullOrEmpty(ApptTime) ? "auto" : ApptTime,
                    MedicalNo, ParamedicID, Reason.ToString(), System.Web.HttpUtility.UrlEncode(notes));
                var sResponse = Common.Helper.WebRequestGet(sUrl);

                int ret = System.Convert.ToInt32(sResponse.Trim());
                // kalau berhasil langsung save aja ke tabel 
                if (ret > 0) { 
                    //
                    var app = new AppointmentLokadok();
                    app.AddNew();
                    app.ApptId = ret;
                    app.StartDate = AppointmentLokadokCollection.ConvertToUnixTime(ApptDate);
                    app.PId = MedicalNo;
                    app.DocId = ParamedicID;
                    app.Notes = notes;
                    app.Confirmed = "0";
                    app.BookingCode = string.Empty;
                    app.RegistrationNoSender = RegistrationNoSender;
                    app.Save();
                }

                return ret;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        #endregion
    }
}

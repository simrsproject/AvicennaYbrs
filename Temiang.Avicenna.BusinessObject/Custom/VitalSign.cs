using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class VitalSign
    {
        public enum VitalSignEnum
        {
            BodyWeight,
            BodyHeight,
            BodyMassIndex,
            BloodPressure,
            BloodPressureSistolic,
            BloodPressureDiastolic,
            HeartRate,
            HeadCircumference,
            RespiratoryRate,
            Temperature,
            PainScale,
            SpO2,
            Djj,
            Nutrition
        }

        private static DeceasedInfo _deceasedInfo;
        private class DeceasedInfo
        {
            public DeceasedInfo(string registrationNo, DateTime? deceasedDateTime, bool? isAlive)
            {
                RegistrationNo = registrationNo;
                DeceasedDateTime = deceasedDateTime;
                IsDeceased = deceasedDateTime != null || !(isAlive ?? true); // DeceasedDateTime bisa kosong untuk misal kasus pasien lama yg tidak diketahui tgl meninggalnya
            }
            public string RegistrationNo { get; set; }
            public DateTime? DeceasedDateTime { get; set; }
            public bool IsDeceased { get; set; }
        }

        private static void RefreshDeceasedInfo(string registrationNo)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);
            var pat = new Patient();
            if (pat.LoadByPrimaryKey(reg.PatientID))
            {
                _deceasedInfo = new DeceasedInfo(registrationNo, pat.DeceasedDateTime, pat.IsAlive);
            }
        }

        #region registrationNo & referFromRegistrationNo
        public static double LastVitalSignValue(string registrationNo, string referFromRegistrationNo, VitalSignEnum vitalSignItem, DateTime lastDateTime)
        {
            return LastVitalSignValue(registrationNo, referFromRegistrationNo, GetVitalSignID(vitalSignItem), lastDateTime);
        }
        private static double LastVitalSignValue(string registrationNo, string referFromRegistrationNo, string vitalSignID, DateTime lastDateTime)
        {
            var vitalSignValue = LastVitalSign(registrationNo, referFromRegistrationNo, vitalSignID, lastDateTime);
            return vitalSignValue.Value;
        }

        public static VitalSignItem LastVitalSignItem(string registrationNo, string referFromRegistrationNo, VitalSignEnum vitalSignItem, DateTime lastDateTime)
        {
            return LastVitalSign(registrationNo, referFromRegistrationNo, GetVitalSignID(vitalSignItem), lastDateTime);
        }

        private static VitalSignItem LastVitalSign(string registrationNo, string referFromRegistrationNo, string vitalSignID, DateTime lastDateTime)
        {

            var phrl = new PatientHealthRecordLineQuery("phrl");
            var quest = new QuestionQuery("q");
            phrl.InnerJoin(quest).On(phrl.QuestionID == quest.QuestionID);

            var vital = new VitalSignQuery("v");
            phrl.InnerJoin(vital).On(quest.VitalSignID == vital.VitalSignID);

            var phr = new PatientHealthRecordQuery("phr");
            phrl.InnerJoin(phr).On(phrl.TransactionNo == phr.TransactionNo);

            if (!String.IsNullOrEmpty(referFromRegistrationNo))
                phrl.Where(phrl.Or(phr.RegistrationNo == registrationNo, phr.RegistrationNo == referFromRegistrationNo));  // Query cost optimal checked by Handono 231205, cost phr:phrl -> 3:97
            else
                phrl.Where(phr.RegistrationNo == registrationNo);

            // Abaikan filter tanggal jika waktunya hampir bersamaan dengan waktu sekarang  
            // lastDateTime untuk keperluan display di asesmen (kondisi vital sign saat pembuatan asesmen)
            if (DateTime.Now > lastDateTime.AddSeconds(10))
            {
                // RecordDate tidak seragam ...ada yg include time ada yg tidak
                // Ambil semua record di tanggal sebelumnya
                // ditambah record ditanggal dipilih dgn batasan time (untuk RecordDate yg include time)
                // ditambah record ditanggal dipilih dgn batasan RecordTime

                phrl.Where(phr.Or(phr.RecordDate <= lastDateTime.Date.AddDays(-1),
                    phr.And(phr.RecordDate > lastDateTime.Date, phr.RecordDate <= lastDateTime),
                    phr.And(phr.RecordDate == lastDateTime.Date, phr.RecordTime <= lastDateTime.ToString("HH:mm"))));
            }

            phrl.es.Top = 1;
            phrl.Select(phrl.QuestionAnswerNum, vital.VitalSignUnit, vital.NumDecimalDigits, phr.RecordDate, phr.RecordTime, phrl.LastUpdateByUserID);

            // Ambil semua value termasuk 0 pada vitalsign terakhir untuk pasien meninggal (Handono 231004 by req: RSI)
            if (_deceasedInfo == null || _deceasedInfo.RegistrationNo != registrationNo)
                RefreshDeceasedInfo(registrationNo);

            if (!(_deceasedInfo.IsDeceased && (_deceasedInfo.DeceasedDateTime == null || lastDateTime >= _deceasedInfo.DeceasedDateTime)))
                phrl.Where(phrl.QuestionAnswerNum > 0);

            phrl.Where(vital.VitalSignID == vitalSignID);
            phrl.OrderBy(phr.RecordDate.Descending, phrl.TransactionNo.Descending);
            var dtb = phrl.LoadDataTable();
            if (dtb != null && dtb.Rows.Count > 0)
            {
                var row = dtb.Rows[0];
                return new VitalSignItem()
                {
                    Value = row["QuestionAnswerNum"].ToDouble(),
                    Unit = row["VitalSignUnit"].ToString(),
                    ValueDecimalDigits = row["NumDecimalDigits"].ToInt(),
                    RecordDate = Convert.ToDateTime(row["RecordDate"]),
                    RecordTime = row["RecordTime"].ToString(),
                    ByUserID = row["LastUpdateByUserID"].ToString()
                };
            }
            return new VitalSignItem();
        }

        public class VitalSignItem
        {
            public double Value { get; set; }
            public string Unit { get; set; }
            public int ValueDecimalDigits { get; set; }
            public DateTime RecordDate { get; set; }
            public string RecordTime { get; set; }
            public DateTime RecordDateTime
            {
                get
                {
                    var recordTimes = RecordTime.Split(':');
                    return new DateTime(RecordDate.Year, RecordDate.Month, RecordDate.Day, Convert.ToInt16(recordTimes[0]), Convert.ToInt16(recordTimes[1]), 0);
                }
            }
            public string ByUserID { get; set; }
        }

        public static string LastVitalSignWithUnit(string registrationNo, string referFromRegistrationNo, VitalSignEnum vitalSignItem, DateTime lastDateTime)
        {
            var vitalSignID = GetVitalSignID(vitalSignItem);

            if (vitalSignItem == VitalSignEnum.BloodPressure)
            {
                var sist = LastVitalSign(registrationNo, referFromRegistrationNo, GetVitalSignID(VitalSignEnum.BloodPressureSistolic), lastDateTime);
                var diast = LastVitalSign(registrationNo, referFromRegistrationNo, GetVitalSignID(VitalSignEnum.BloodPressureDiastolic), lastDateTime);

                if (sist.Value == 0)
                    return String.Empty;

                var format = string.Concat("{0:N", sist.ValueDecimalDigits, "} / {1:N", diast.ValueDecimalDigits, "} {2}");
                return string.Format(format, sist.Value, diast.Value, sist.Unit);
            }

            var vitVal = LastVitalSign(registrationNo, referFromRegistrationNo, vitalSignID, lastDateTime);
            var format2 = string.Concat("{0:N", vitVal.ValueDecimalDigits, "} {1}");
            return string.Format(format2, vitVal.Value, vitVal.Unit);
        }

        //public static DataTable VitalSignLastValue(string registrationNo, string referFromRegistrationNo, bool isWithEwsLevel = false, DateTime lastDateTime )
        public static DataTable VitalSignLastValue(string registrationNo, string referFromRegistrationNo, bool isWithEwsLevel, DateTime lastDateTime)
        {
            var dtb = LastVitalSignRecord(registrationNo, referFromRegistrationNo, lastDateTime);

            var dtbSelected = DistinctAndSetEws(registrationNo, isWithEwsLevel, dtb, lastDateTime);
            return dtbSelected;
        }

        private static DataTable DistinctAndSetEws(string registrationNo, bool isWithEwsLevel, DataTable dtb, DateTime lastDateTime)
        {

            // TODO: Belum ada ide cara ambil yg terakhir sementara ambil semuanya saja
            if (_deceasedInfo == null || _deceasedInfo.RegistrationNo != registrationNo)
                RefreshDeceasedInfo(registrationNo);

            // Ambil yg terakhir dan ada isinya
            // Ambil semua value termasuk 0 pada vitalsign terakhir untuk pasien meninggal (Handono 231004 by req: RSI)
            var dtbSelected = dtb.Clone();
            var id = string.Empty;
            foreach (DataRow row in dtb.Rows)
            {
                //if (!id.Equals(row["VitalSignID"]) && (!string.IsNullOrEmpty(row["QuestionAnswerText"].ToString()) ||
                //                                       ConvertToDecimal(row["QuestionAnswerNum"]) != 0))

                if (!id.Equals(row["VitalSignID"]))
                {
                    var valText = row["QuestionAnswerText"].ToString();
                    var valNum = ConvertToDecimal(row["QuestionAnswerNum"]);
                    if (!string.IsNullOrEmpty(valText) || valNum != 0
                        || (_deceasedInfo.IsDeceased && (_deceasedInfo.DeceasedDateTime == null || lastDateTime >= _deceasedInfo.DeceasedDateTime)))
                    {
                        id = row["VitalSignID"].ToString();

                        // Copy
                        dtbSelected.Rows.Add(row.ItemArray);
                    }
                }
            }


            // EWS & Formated
            var dateOfBirth = DateTime.Today;
            if (isWithEwsLevel)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(registrationNo);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                dateOfBirth = pat.DateOfBirth ?? DateTime.Today;
            }

            dtbSelected.Columns.Add("EwsLevelColor", typeof(System.String));
            dtbSelected.Columns.Add("QuestionAnswerFormatted", typeof(System.String));

            foreach (DataRow row in dtbSelected.Rows)
            {
                // EWS Level Color
                string ewsLevel = "white";
                if (isWithEwsLevel && ConvertToDecimal(row["QuestionAnswerNum"]) != 0)
                {
                    ewsLevel = VitalSign.EwsLevelColor(row["VitalSignID"].ToString(), dateOfBirth,
                        Convert.ToDateTime(row["RecordDate"]),
                        ConvertToDecimal(row["QuestionAnswerNum"]));
                }

                row["EwsLevelColor"] = ewsLevel;

                // QuestionAnswerFormatted
                row["QuestionAnswerFormatted"] = VitalSignValueFormated(row);
            }

            dtbSelected.AcceptChanges();
            return dtbSelected;
        }

        #endregion

        #region MergeRegistration / per Episode / Rawat Inap
        public static DataTable VitalSignLastValue(string registrationNo, List<string> mergeRegistrations, bool isWithEwsLevel, DateTime lastDateTime, bool isForCURB = false)
        {
            var dtb = LastVitalSignRecord(mergeRegistrations, lastDateTime, isForCURB);

            var dtbSelected = DistinctAndSetEws(registrationNo, isWithEwsLevel, dtb, lastDateTime);
            return dtbSelected;
        }

        public static double LastVitalSign(List<string> mergeRegistrations, VitalSignEnum vitalSignItem, DateTime lastDateTime)
        {
            return LastVitalSignValue(mergeRegistrations, GetVitalSignID(vitalSignItem), lastDateTime);
        }

        private static double LastVitalSignValue(List<string> mergeRegistrations, string vitalSignID, DateTime lastDateTime)
        {
            var dtb = LastVitalSignRecord(mergeRegistrations, vitalSignID, lastDateTime);
            if (dtb.Rows.Count > 0)
            {
                try
                {
                    var val = (dtb.Rows[0][0]);
                    return (double)((decimal)val);
                }
                catch
                {
                    return 0;
                }
            }

            return 0;
        }

        public static string LastVitalSignWithUnit(List<string> mergeRegistrations, VitalSignEnum vitalSignItem, DateTime lasDateTime)
        {
            var vitalSignID = GetVitalSignID(vitalSignItem);

            if (vitalSignItem == VitalSignEnum.BloodPressure)
            {
                var dtb1 = LastVitalSignRecord(mergeRegistrations, GetVitalSignID(VitalSignEnum.BloodPressureSistolic), lasDateTime);
                var dtb2 = LastVitalSignRecord(mergeRegistrations, GetVitalSignID(VitalSignEnum.BloodPressureDiastolic), lasDateTime);
                if (dtb1.Rows.Count > 0 && dtb2.Rows.Count > 0)
                {
                    try
                    {
                        var format = string.Concat("{0:N", dtb1.Rows[0][2].ToInt(), "} / {1:N", dtb2.Rows[0][2].ToInt(), "} {2}");
                        return string.Format(format, dtb1.Rows[0][0], dtb2.Rows[0][0], dtb1.Rows[0][1]);
                    }
                    catch
                    {
                        return string.Empty;
                    }
                }
            }
            else
            {

                var dtb = LastVitalSignRecord(mergeRegistrations, vitalSignID, lasDateTime);
                if (dtb.Rows.Count > 0)
                {
                    try
                    {
                        var format = string.Concat("{0:N", dtb.Rows[0][2].ToInt(), "} {1}");
                        return string.Format(format, dtb.Rows[0][0], dtb.Rows[0][1]);
                    }
                    catch
                    {
                        return string.Empty;
                    }
                }
            }

            return string.Empty;
        }

        private static DataTable LastVitalSignRecord(List<string> mergeRegistrations, string vitalSignID, DateTime lastDateTime)
        {
            var phrl = new PatientHealthRecordLineQuery("phrl");
            var quest = new QuestionQuery("q");
            phrl.InnerJoin(quest).On(phrl.QuestionID == quest.QuestionID);

            var vital = new VitalSignQuery("v");
            phrl.InnerJoin(vital).On(quest.VitalSignID == vital.VitalSignID);

            var phr = new PatientHealthRecordQuery("phr");
            phrl.InnerJoin(phr).On(phrl.TransactionNo == phr.TransactionNo & phrl.RegistrationNo == phr.RegistrationNo);

            phrl.Where(phrl.RegistrationNo.In(mergeRegistrations));

            phrl.es.Top = 1;

            phrl.Select(phrl.QuestionAnswerNum, vital.VitalSignUnit, vital.NumDecimalDigits);

            // Abaikan filter tanggal jika waktunya hampir bersamaan dengan waktu sekarang  
            // lastDateTime untuk keperluan display di asesmen (kondisi vital sign saat pembuatan asesmen)
            if (DateTime.Now > lastDateTime.AddSeconds(10))
            {
                // RecordDate tidak seragam ...ada yg include time ada yg tidak
                // Ambil semua record di tanggal sebelumnya
                // ditambah record ditanggal dipilih dgn batasan time (untuk RecordDate yg include time)
                // ditambah record ditanggal dipilih dgn batasan RecordTime

                phrl.Where(phr.Or(phr.RecordDate <= lastDateTime.Date.AddDays(-1),
                    phr.And(phr.RecordDate > lastDateTime.Date, phr.RecordDate <= lastDateTime),
                    phr.And(phr.RecordDate == lastDateTime.Date, phr.RecordTime <= lastDateTime.ToString("HH:mm"))));
            }

            // Ambil semua value termasuk 0 pada vitalsign terakhir untuk pasien meninggal (Handono 231004 by req: RSI)
            // TODO: Belum ada ide cara ambil yg terakhir sementara ambil semuanya saja
            var registrationNo = mergeRegistrations[0];
            if (_deceasedInfo == null || _deceasedInfo.RegistrationNo != registrationNo)
                RefreshDeceasedInfo(registrationNo);
            if (!(_deceasedInfo.IsDeceased && (_deceasedInfo.DeceasedDateTime == null || lastDateTime >= _deceasedInfo.DeceasedDateTime)))
                phrl.Where(phrl.QuestionAnswerNum > 0);

            phrl.Where(vital.VitalSignID == vitalSignID);
            phrl.OrderBy(phr.RecordDate.Descending, phrl.TransactionNo.Descending);
            var dtb = phrl.LoadDataTable();
            return dtb;
        }

        //public static DataTable VitalSignValue(string registrationNo, List<string> mergeRegistrations, bool isWithEwsLevel = false, bool isLast = true)
        public static DataTable VitalSignValue(string registrationNo, List<string> mergeRegistrations, bool isWithEwsLevel, DateTime lastDateTime, bool isForCURB = false)
        {
            var dtb = LastVitalSignRecord(mergeRegistrations, lastDateTime, isForCURB);

            // Ambil yg terakhir dan ada isinya
            var dtbSelected = dtb.Clone();
            var id = string.Empty;
            foreach (DataRow row in dtb.Rows)
            {
                if (!id.Equals(row["VitalSignID"]) && (!string.IsNullOrEmpty(row["QuestionAnswerText"].ToString()) || ConvertToDecimal(row["QuestionAnswerNum"]) != 0))
                {
                    id = row["VitalSignID"].ToString();
                    //Copy
                    dtbSelected.Rows.Add(row.ItemArray);
                }
            }


            // EWS & Formated
            var dateOfBirth = DateTime.Today;
            if (isWithEwsLevel)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(registrationNo);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                dateOfBirth = pat.DateOfBirth ?? DateTime.Today;
            }

            dtbSelected.Columns.Add("EwsLevelColor", typeof(System.String));
            dtbSelected.Columns.Add("QuestionAnswerFormatted", typeof(System.String));

            foreach (DataRow row in dtbSelected.Rows)
            {
                // EWS Level Color
                string ewsLevel = "white";
                if (isWithEwsLevel && ConvertToDecimal(row["QuestionAnswerNum"]) != 0)
                {
                    ewsLevel = VitalSign.EwsLevelColor(row["VitalSignID"].ToString(), dateOfBirth, Convert.ToDateTime(row["RecordDate"]),
                        ConvertToDecimal(row["QuestionAnswerNum"]));
                }

                row["EwsLevelColor"] = ewsLevel;

                // QuestionAnswerFormatted
                row["QuestionAnswerFormatted"] = VitalSignValueFormated(row);
            }
            dtbSelected.AcceptChanges();
            return dtbSelected;
        }

        #endregion

        public static string GetVitalSignID(VitalSignEnum vitalSignEnum)
        {
            string vitalSignID = string.Empty;
            switch (vitalSignEnum)
            {
                case VitalSignEnum.BodyHeight:
                    vitalSignID = "HEIGHT";
                    break;
                case VitalSignEnum.BodyWeight:
                    vitalSignID = "WEIGHT";
                    break;
                case VitalSignEnum.BloodPressure:
                    vitalSignID = "BP";
                    break;
                case VitalSignEnum.BloodPressureSistolic:
                    vitalSignID = "BP1";
                    break;
                case VitalSignEnum.BloodPressureDiastolic:
                    vitalSignID = "BP2";
                    break;
                case VitalSignEnum.HeartRate:
                    vitalSignID = "HR";
                    break;
                case VitalSignEnum.RespiratoryRate:
                    vitalSignID = "RESP";
                    break;
                case VitalSignEnum.Temperature:
                    vitalSignID = "TEMP";
                    break;
                case VitalSignEnum.PainScale:
                    vitalSignID = "PAINSC";
                    break;
                case VitalSignEnum.SpO2:
                    vitalSignID = "SpO2";
                    break;
                case VitalSignEnum.Nutrition:
                    vitalSignID = "GIZISTAT";
                    break;
            }

            return vitalSignID;
        }

        #region EWS
        public static string EwsLevelColor(string vitalSignID, DateTime birthDate, DateTime vitalSignDate, decimal vitalSignValue)
        {
            var level = EwsLevelValue(vitalSignID, birthDate, vitalSignDate, vitalSignValue);
            return EwsLevelColor(level);
        }

        public static string EwsLevelColor(int ewsLevel)
        {
            //TODO: (Handono) Ews Color bisa diseting di Appparameter supaya setiap client bisa berbeda (Request RSI 230107)
            switch (ewsLevel)
            {
                case 1:
                    return "lightblue";
                case 2:
                    return "yellow";
                case 3:
                    return "Coral";
                default:
                    return "white";
            }
        }

        public static int EwsLevelValue(string vitalSignID, DateTime birthDate, DateTime vitalSignDate, decimal vitalSignValue, string ewsType = "EWS")
        {
            int ageInDay = (vitalSignDate - birthDate).Days;
            var qr = new VitalSignEwsLevelQuery();
            qr.Where(qr.SREwsType == ewsType, qr.VitalSignID == vitalSignID, qr.StartAgeInDay <= ageInDay, qr.StartValue <= vitalSignValue);
            qr.es.Top = 1;
            qr.OrderBy(qr.StartAgeInDay.Descending, qr.StartValue.Descending);

            var ewsLevel = new VitalSignEwsLevel();
            if (ewsLevel.Load(qr))
                return ewsLevel.EwsLevel ?? 0;
            return 0;
        }

        public static string LastVitalSignNo(string patientID, DateTime dateOfBirth)
        {
            int ageInDay = (DateTime.Today - dateOfBirth).Days;
            var qrStartAgeInDay = new VitalSignEwsQuery("sa");
            qrStartAgeInDay.Select(qrStartAgeInDay.StartAgeInDay);
            qrStartAgeInDay.Where(qrStartAgeInDay.StartAgeInDay <= ageInDay);
            qrStartAgeInDay.es.Top = 1;
            qrStartAgeInDay.OrderBy(qrStartAgeInDay.StartAgeInDay.Descending);

            var qrEws = new VitalSignEwsQuery("ews");
            qrEws.Where(qrEws.StartAgeInDay == (qrStartAgeInDay));
            qrEws.Select(qrEws.VitalSignID);


            var phr = new PatientHealthRecordQuery("phr");
            var phrl = new PatientHealthRecordLineQuery("phrl");
            phr.InnerJoin(phrl).On(phr.TransactionNo == phrl.TransactionNo);

            var reg = new RegistrationQuery("r");
            phr.InnerJoin(reg).On(phr.RegistrationNo == reg.RegistrationNo);

            var quest = new QuestionQuery("q");
            phr.InnerJoin(quest).On(phrl.QuestionID == quest.QuestionID);

            phr.Where(reg.PatientID == patientID, quest.VitalSignID.In(qrEws));

            phr.Select(phr.TransactionNo);

            phr.OrderBy(phr.RecordDate.Descending, phr.RecordTime.Descending);
            phr.es.Top = 1;
            var dtb = phr.LoadDataTable();
            if (dtb != null && dtb.Rows != null && dtb.Rows.Count == 1)
            {
                return (dtb.Rows[0][0]).ToString();
            }

            return string.Empty;
        }
        public static DateTime LastVitalSignDate(string registrationNo, string referFromRegistrationNo, string questionFormID = "", bool isForValueNotZero=true)
        {
            var phrl = new PatientHealthRecordLineQuery("phrl");
            var quest = new QuestionQuery("q");
            phrl.InnerJoin(quest).On(phrl.QuestionID == quest.QuestionID);

            var vital = new VitalSignQuery("v");
            phrl.InnerJoin(vital).On(quest.VitalSignID == vital.VitalSignID);

            var phr = new PatientHealthRecordQuery("phr");
            phrl.InnerJoin(phr).On(phrl.TransactionNo == phr.TransactionNo & phrl.RegistrationNo == phr.RegistrationNo);

            if (!string.IsNullOrEmpty(referFromRegistrationNo))
                phrl.Where(phrl.Or(phrl.RegistrationNo == registrationNo, phrl.RegistrationNo == referFromRegistrationNo));
            else
                phrl.Where(phrl.RegistrationNo == registrationNo);

            if (!string.IsNullOrEmpty(questionFormID))
                phrl.Where(phrl.QuestionFormID == questionFormID);

            if (isForValueNotZero)
                phrl.Where(phrl.QuestionAnswerNum > 0);

            phrl.Where(vital.VitalSignInitial != "CURB");
            phrl.Select(phr.RecordDate);
            phrl.OrderBy(phr.RecordDate.Descending);
            phrl.es.Top = 1;
            var dtb = phrl.LoadDataTable();

            var lastVitalSignDate = new DateTime(1900, 1, 1);

            // Ambil yg terakhir dan ada isinya
            foreach (DataRow row in dtb.Rows)
            {
                lastVitalSignDate = Convert.ToDateTime(row["RecordDate"]);
            }

            return lastVitalSignDate;
        }

        public static DataTable EwsLatestPatientInHouseByParamedic(string ParamedicID)
        {
            return (new PatientHealthRecordLineCollection()).GetLatestVitalSignPatientInHouseByParamedicID(ParamedicID);
        }
        public static Dictionary<string, object> EwsLatestInHouse(DataTable Source, string PatientID, DateTime dateOfBirth)
        {
            int Status = 0;// status High atau Low
            string VitalSignInitial = "";
            string VitalSignName = "";
            decimal Value = -1;
            string VitalSignUnit = "";

            var rows = Source.AsEnumerable().Where(x => x["PatientID"].ToString() == PatientID);

            var topEwsLevel = 0;
            foreach (DataRow row in rows)
            {
                var nextEwsLevel = VitalSign.EwsLevelValue(row["VitalSignID"].ToString(), dateOfBirth,
                    Convert.ToDateTime(row["RecordDate"]),
                    ConvertToDecimal(row["QuestionAnswerNum"]));

                if (topEwsLevel < nextEwsLevel)
                {
                    topEwsLevel = nextEwsLevel;
                    Status = topEwsLevel;
                    VitalSignInitial = row["VitalSignInitial"].ToString();
                    VitalSignName = row["VitalSignName"].ToString();
                    Value = (Convert.ToDecimal(row["QuestionAnswerNum"] == DBNull.Value ? -1 : row["QuestionAnswerNum"]));
                    VitalSignUnit = string.IsNullOrEmpty(row["QuestionAnswerSuffix"].ToString()) ? string.Empty : row["QuestionAnswerSuffix"].ToString();
                }
            }

            var ret = new Dictionary<string, object>();
            ret.Add("Status", Status);
            ret.Add("VitalSignInitial", VitalSignInitial);
            ret.Add("VitalSignName", VitalSignName);
            ret.Add("Value", Value);
            ret.Add("VitalSignUnit", VitalSignUnit);

            return ret;
        }

        public static Dictionary<string, object> EwsLatest(string registrationNo, string referFromRegistrationNo, DateTime dateOfBirth)
        {
            int Status = 0;// status High atau Low
            string VitalSignName = "";
            decimal Value = -1;
            string VitalSignUnit = "";


            var dtb = LastVitalSignRecord(registrationNo, referFromRegistrationNo, DateTime.Now);

            // Ambil yg terakhir dan ada isinya
            //var dtbSelected = dtb.Clone();
            //var id = string.Empty;
            //foreach (DataRow row in dtb.Rows)
            //{
            //    if (!id.Equals(row["VitalSignID"]) && (!string.IsNullOrEmpty(row["QuestionAnswerText"].ToString()) ||
            //                                           ConvertToDecimal(row["QuestionAnswerNum"]) != 0))
            //    {
            //        id = row["VitalSignID"].ToString();
            //        //Copy
            //        dtbSelected.Rows.Add(row.ItemArray);
            //    }
            //}

            var dtbSelected = dtb.Clone();
            var rows = dtb.AsEnumerable()
                .GroupBy(r => r["VitalSignID"])
                .Select(g => g.OrderBy(x => ((DateTime)x["RecordDate"]).Date.Add(TimeSpan.Parse(x["RecordTime"].ToString()))).First());
            if (rows.Count() > 0) dtbSelected = rows.CopyToDataTable();

            var topEwsLevel = 0;
            foreach (DataRow row in dtbSelected.Rows)
            {
                var nextEwsLevel = VitalSign.EwsLevelValue(row["VitalSignID"].ToString(), dateOfBirth,
                    Convert.ToDateTime(row["RecordDate"]),
                    ConvertToDecimal(row["QuestionAnswerNum"]));

                if (topEwsLevel < nextEwsLevel)
                {
                    topEwsLevel = nextEwsLevel;
                    Status = topEwsLevel;
                    VitalSignName = row["VitalSignInitial"].ToString();
                    Value = (Convert.ToDecimal(row["QuestionAnswerNum"] == DBNull.Value ? -1 : row["QuestionAnswerNum"]));
                    VitalSignUnit = string.IsNullOrEmpty(row["QuestionAnswerSuffix"].ToString()) ? string.Empty : row["QuestionAnswerSuffix"].ToString();
                }
            }

            var ret = new Dictionary<string, object>();
            ret.Add("Status", Status);
            ret.Add("VitalSignName", VitalSignName);
            ret.Add("Value", Value);
            ret.Add("VitalSignUnit", VitalSignUnit);

            return ret;
        }

        public static int EwsLevelTopLevel(string registrationNo, string referFromRegistrationNo, DateTime dateOfBirth, ref string ewsTopLevelVitalSignValue, ref string ewsTopLevelColor, ref DateTime recordDate, DateTime lastDateTime)
        {
            var dtb = LastVitalSignRecord(registrationNo, referFromRegistrationNo, lastDateTime);

            // Ambil yg terakhir dan ada isinya
            var dtbSelected = dtb.Clone();
            var id = string.Empty;
            foreach (DataRow row in dtb.Rows)
            {
                if (!id.Equals(row["VitalSignID"]) && (!string.IsNullOrEmpty(row["QuestionAnswerText"].ToString()) ||
                                                       ConvertToDecimal(row["QuestionAnswerNum"]) != 0))
                {
                    id = row["VitalSignID"].ToString();
                    //Copy
                    dtbSelected.Rows.Add(row.ItemArray);
                }
            }

            var topEwsLevel = 0;
            foreach (DataRow row in dtbSelected.Rows)
            {
                var nextEwsLevel = VitalSign.EwsLevelValue(row["VitalSignID"].ToString(), dateOfBirth,
                    Convert.ToDateTime(row["RecordDate"]),
                    ConvertToDecimal(row["QuestionAnswerNum"]));

                if (topEwsLevel < nextEwsLevel)
                {
                    topEwsLevel = nextEwsLevel;

                    ewsTopLevelVitalSignValue = string.Format("{0}: {1}", row["VitalSignInitial"], VitalSignValueFormated(row));
                    ewsTopLevelColor = EwsLevelColor(topEwsLevel);
                    recordDate = Convert.ToDateTime(row["RecordDate"]);
                }

            }

            return topEwsLevel;
        }

        public static bool LastEwsTotalScore(string registrationNo, string referFromRegistrationNo, DateTime dateOfBirth, ref string ewsLastTotalScoreValue, ref string ewsLastTotalLevelColor, ref DateTime recordDate, DateTime lastDateTime,ref string statType)
        {
            bool isExist = false;
            var templateId = string.Empty;

            List<string> templates = new List<string>();
            var nd = new NursingDiagnosaTemplateCollection();
            nd.Query.Where(nd.Query.TemplateName.In("EWS", "PEWS", "MEOWS"), nd.Query.IsActive == true);
            if (nd.LoadAll())
            {
                foreach (var i in nd)
                {
                    templates.Add(i.TemplateID.ToString());
                }
            }

            //var phr = new PatientHealthRecord();
            //var phrQr = new PatientHealthRecordQuery("a");
            //var ndtd = new NursingDiagnosaTransDTQuery("b");
            //phrQr.InnerJoin(ndtd).On(phrQr.TransactionNo == ndtd.ReferenceToPhrNo);
            //phrQr.Where(phrQr.RegistrationNo == registrationNo, phrQr.QuestionFormID.In(templates), phrQr.Or(ndtd.IsDeleted.IsNull(), ndtd.IsDeleted == false));

            // Sepertinya cukup di headernya saja ceknya supaya lebih efisien querynya ...silahkan perbaiki jika salah (Handono 2024-10-17)
            var phr = new PatientHealthRecord();
            var phrQr = new PatientHealthRecordQuery("a");
            phrQr.Select(phrQr.QuestionFormID);
            phrQr.Where(phrQr.RegistrationNo == registrationNo, phrQr.QuestionFormID.In(templates));

            phrQr.OrderBy(phrQr.RecordDate.Descending);
            phrQr.es.Top = 1;
            if (phr.Load(phrQr))
                templateId = phr.QuestionFormID;

            if (nd.LoadAll())
            {
                foreach (var i in nd)
                {
                    if (templateId == i.TemplateID.ToString())
                    {
                        statType = i.TemplateName;
                        break;
                    }
                }
            }

            var dtb = LastVitalSignRecordByQuestionForm(registrationNo, referFromRegistrationNo, templateId, lastDateTime);

            if (dtb == null || dtb.Rows.Count == 0) return false;

            var dtbSelected = dtb.Clone();
            var id = string.Empty;
            foreach (DataRow row in dtb.Rows)
            {
                if (string.IsNullOrEmpty(row["VitalSignID"].ToString()))
                {
                    dtbSelected.Rows.Add(row.ItemArray);
                }
                else
                {
                    if (!id.Equals(row["VitalSignID"]))
                    {
                        id = row["VitalSignID"].ToString();
                        dtbSelected.Rows.Add(row.ItemArray);
                    }
                }
            }

            var totalScore = 0;
            var level = 0;

            var mostRecentDate = dtbSelected.AsEnumerable().Max(row => Convert.ToDateTime(row["RecordDate"]));
            var mostRecentRows = dtbSelected.AsEnumerable().Where(row => Convert.ToDateTime(row["RecordDate"]) == mostRecentDate);

            foreach (DataRow row in mostRecentRows)
            {
                isExist = true;
                var nextEwsLevel = 0;
                if (!"NUM".Equals(row["SRAnswerType"]))
                {
                    if (row["QuestionAnswerSelectionLineID"] != DBNull.Value)
                    {
                        var answerLine = new QuestionAnswerSelectionLine();
                        if (answerLine.LoadByPrimaryKey(row["QuestionID"].ToString(), row["QuestionAnswerSelectionLineID"].ToString()))
                        {
                            nextEwsLevel = answerLine.Score.ToInt();
                        }
                    }
                }
                else
                {
                    if (statType == "PEWS")
                    {
                        nextEwsLevel = row["QuestionAnswerNum"].ToInt();
                    } 
                    else
                    {
                        nextEwsLevel = VitalSign.EwsLevelValue(row["VitalSignID"].ToString(), dateOfBirth, Convert.ToDateTime(row["RecordDate"]), ConvertToDecimal(row["QuestionAnswerNum"]), statType);
                    }
                }

                totalScore += nextEwsLevel;

                if (totalScore == 0) 
                    level = 0; // White
                else if (totalScore < 5)
                    level = 1; // Blue
                else if (totalScore < 7)
                    level = 2; // Yellow
                else
                    level = 3; // Red

                ewsLastTotalScoreValue = totalScore.ToString();
                ewsLastTotalLevelColor = EwsLevelColor(level);
                recordDate = Convert.ToDateTime(row["RecordDate"]);
            }

            return isExist;
        }

        private class VitalSignItemValue
        {
            public int No { get; set; }
            public string TransactionNo { get; set; }
            public DateTime Time { get; set; }
            public string VitalSignID { get; set; }
            public string VitalSignID2 { get; set; }
            public int Level { get; set; }
            public int Level2 { get; set; }
            public Decimal Value { get; set; }
            public Decimal Value2 { get; set; }
            public int TotalScore { get; set; }
            public bool IsExistLevel3 { get; set; }
            public string ValueInString { get; set; }
            public string ValueInString2 { get; set; }

        }

        #endregion

        #region VitalSign Transaction Record
        private static string RemoveZeroDigits(decimal value)
        {
            return value == -1 ? "-" : Convert.ToString(value / 1.000000000000000000000000000000M);
        }
        private static decimal ConvertToDecimal(object input)
        {
            if (input == null || input == DBNull.Value) return 0;

            decimal returnValue = 0;
            try { returnValue = Convert.ToDecimal(input); }
            catch { returnValue = 0; }
            return returnValue;
        }



        private static string VitalSignValueFormated(DataRow row)
        {
            var answerText = row["QuestionAnswerText"].ToString();
            string questionAnswerFormatted;
            switch (row["SRAnswerType"].ToString())
            {
                case "CNM":
                    try
                    {
                        answerText = answerText.Split('|')[1];
                    }
                    catch
                    {
                    }


                    questionAnswerFormatted = string.Format("{0} {1}", answerText,
                        string.IsNullOrEmpty(row["QuestionAnswerSuffix"].ToString())
                            ? string.Empty
                            : row["QuestionAnswerSuffix"].ToString()
                    );
                    break;
                default:
                    questionAnswerFormatted = string.Format("{0} {1}",
                        string.IsNullOrEmpty(answerText)
                            ? RemoveZeroDigits(Convert.ToDecimal(row["QuestionAnswerNum"] == DBNull.Value
                                ? -1
                                : row["QuestionAnswerNum"]))
                            : (row["QuestionAnswerText"].ToString()),
                        string.IsNullOrEmpty(row["QuestionAnswerSuffix"].ToString())
                            ? string.Empty
                            : row["QuestionAnswerSuffix"].ToString()
                    );
                    break;
            }

            return questionAnswerFormatted;
        }

        public static DataTable LastVitalSignRecord(string registrationNo, string referFromRegistrationNo, DateTime lastDateTime)
        {
            // TODO: Tampilkan semua yg ada di EWS jika EWS ada recordnya
            var phrl = new PatientHealthRecordLineQuery("phrl");

            var quest = new QuestionQuery("q");
            phrl.InnerJoin(quest).On(phrl.QuestionID == quest.QuestionID);

            var vital = new VitalSignQuery("v");
            phrl.InnerJoin(vital).On(quest.VitalSignID == vital.VitalSignID);

            var phr = new PatientHealthRecordQuery("phr");
            phrl.InnerJoin(phr).On(phrl.TransactionNo == phr.TransactionNo);

            if (!string.IsNullOrEmpty(referFromRegistrationNo))
                phrl.Where(phr.Or(phr.RegistrationNo == registrationNo, phr.RegistrationNo == referFromRegistrationNo)); // Query cost optimal checked by Handono 231205, cost phr:phrl -> 3:97
            else
                phrl.Where(phr.RegistrationNo == registrationNo); // Query cost optimal checked by Handono 231205

            phrl.Where(vital.VitalSignInitial != "CURB");
            // Abaikan filter tanggal jika waktunya hampir bersamaan dengan waktu sekarang  
            // lastDateTime untuk keperluan display di asesmen (kondisi vital sign saat pembuatan asesmen)
            if (DateTime.Now > lastDateTime.AddSeconds(10))
            {
                // RecordDate tidak seragam ...ada yg include time ada yg tidak
                // Ambil semua record di tanggal sebelumnya
                // ditambah record ditanggal dipilih dgn batasan time (untuk RecordDate yg include time)
                // ditambah record ditanggal dipilih dgn batasan RecordTime

                phrl.Where(phr.Or(phr.RecordDate <= lastDateTime.Date.AddDays(-1),
                    phr.And(phr.RecordDate > lastDateTime.Date, phr.RecordDate <= lastDateTime),
                    phr.And(phr.RecordDate == lastDateTime.Date, phr.RecordTime <= lastDateTime.ToString("HH:mm"))));
            }

            //phrl.Where("<CAST(phr.RecordDate AS DATE)+ CAST(phr.RecordTime AS DATETIME) <= >");

            phrl.Select(quest.SRAnswerType, vital.VitalSignID, vital.VitalSignName, phrl.QuestionAnswerPrefix,
                phrl.QuestionAnswerSuffix,
                phrl.QuestionAnswerText, phrl.QuestionAnswerNum, phrl.QuestionAnswerText2, phr.RecordDate, phr.RecordTime, vital.VitalSignInitial);
            phrl.OrderBy(vital.SRVitalSignGroup.Ascending, vital.RowIndexInGroup.Ascending, vital.VitalSignName.Ascending, phr.RecordDate.Descending, phrl.TransactionNo.Descending);

            var dtb = phrl.LoadDataTable();
            return dtb;
        }

        public static DataTable LastVitalSignRecordByQuestionForm(string registrationNo, string referFromRegistrationNo, string questionFormId, DateTime lastDateTime)
        {
            // Migration to SP for optimizing query (Handono 2024010 - 17)

            //var phrl = new PatientHealthRecordLineQuery("phrl");

            //var quest = new QuestionQuery("q");
            //phrl.LeftJoin(quest).On(phrl.QuestionID == quest.QuestionID);

            //var vital = new VitalSignQuery("v");
            //phrl.LeftJoin(vital).On(quest.VitalSignID == vital.VitalSignID);

            //var phr = new PatientHealthRecordQuery("phr");
            //phrl.InnerJoin(phr).On(phrl.TransactionNo == phr.TransactionNo);

            //var nth = new NursingTransHDQuery("nth");
            ////phrl.InnerJoin(nth).On(phrl.RegistrationNo == nth.RegistrationNo);
            //phrl.InnerJoin(nth).On(phr.RegistrationNo == nth.RegistrationNo); //Optimize query memory grant (Handono 2024-10-17)

            //var ndtd = new NursingDiagnosaTransDTQuery("ndtd");
            ////phrl.InnerJoin(ndtd).On(nth.TransactionNo == ndtd.TransactionNo && phrl.TransactionNo == ndtd.ReferenceToPhrNo);
            //phrl.InnerJoin(ndtd).On(nth.TransactionNo == ndtd.TransactionNo && phr.TransactionNo == ndtd.ReferenceToPhrNo); //Optimize query memory grant (Handono 2024-10-17)

            //if (!string.IsNullOrEmpty(referFromRegistrationNo))
            //    phrl.Where(phr.Or(phr.RegistrationNo == registrationNo, phr.RegistrationNo == referFromRegistrationNo));
            //else
            //    phrl.Where(phr.RegistrationNo == registrationNo);

            //// Abaikan filter tanggal jika waktunya hampir bersamaan dengan waktu sekarang  
            //// lastDateTime untuk keperluan display di asesmen (kondisi vital sign saat pembuatan asesmen)
            //if (DateTime.Now > lastDateTime.AddSeconds(10))
            //{
            //    // RecordDate tidak seragam ...ada yg include time ada yg tidak
            //    // Ambil semua record di tanggal sebelumnya
            //    // ditambah record ditanggal dipilih dgn batasan time (untuk RecordDate yg include time)
            //    // ditambah record ditanggal dipilih dgn batasan RecordTime

            //    phrl.Where(phr.Or(phr.RecordDate <= lastDateTime.Date.AddDays(-1),
            //        phr.And(phr.RecordDate > lastDateTime.Date, phr.RecordDate <= lastDateTime),
            //        phr.And(phr.RecordDate == lastDateTime.Date, phr.RecordTime <= lastDateTime.ToString("HH:mm"))));
            //}

            //phrl.Where(phrl.QuestionFormID == questionFormId, phr.Or(ndtd.IsDeleted.IsNull(), ndtd.IsDeleted == false));

            //phrl.Select(quest.SRAnswerType, vital.VitalSignID, vital.VitalSignName, phrl.QuestionAnswerPrefix,
            //    phrl.QuestionAnswerSuffix,
            //    phrl.QuestionAnswerText, phrl.QuestionAnswerNum, phrl.QuestionAnswerText2, phr.RecordDate, phr.RecordTime, vital.VitalSignInitial, phrl.QuestionID, phrl.QuestionAnswerSelectionLineID);
            //phrl.OrderBy(vital.SRVitalSignGroup.Ascending, vital.RowIndexInGroup.Ascending, vital.VitalSignName.Ascending, phr.RecordDate.Descending, phrl.TransactionNo.Descending);

            //var dtb = phrl.LoadDataTable();

            var pars = new esParameters();
            pars.Add("p_RegistrationNo", registrationNo);
            pars.Add("p_FromRegistrationNo", referFromRegistrationNo);
            pars.Add("p_QuestionFormID", questionFormId);
            var dtb = BusinessObject.Common.Utils.LoadDataTableFromStoreProcedure("spHis_Emr_VitalSign_LastVitalSignRecordByQuestionForm", pars, 0);

            return dtb;
        }

        public static DataTable LastVitalSignRecord(List<string> mergeRegistrations, DateTime lastDateTime, bool isForCURB)
        {
            // TODO: Tampilkan semua yg ada di EWS jika EWS ada recordnya
            var phrl = new PatientHealthRecordLineQuery("phrl");
            var quest = new QuestionQuery("q");
            phrl.InnerJoin(quest).On(phrl.QuestionID == quest.QuestionID);

            var vital = new VitalSignQuery("v");
            phrl.InnerJoin(vital).On(quest.VitalSignID == vital.VitalSignID);

            var phr = new PatientHealthRecordQuery("phr");
            phrl.InnerJoin(phr).On(phrl.TransactionNo == phr.TransactionNo);

            phrl.Where(phr.RegistrationNo.In(mergeRegistrations));// Query cost optimal checked by Handono 231205, cost phr:phrl -> 3:97
            if (isForCURB && AppParameter.GetParameterValue(AppParameter.ParameterItem.IsShowCurb65ScoreInAssesmentAndMDS).Equals("Yes", StringComparison.OrdinalIgnoreCase))
            {
                var ndtd = new NursingDiagnosaTransDTQuery("ndtd");
                phrl.LeftJoin(ndtd).On(phr.TransactionNo == ndtd.ReferenceToPhrNo);
                phrl.Where(
                    vital.VitalSignInitial == "CURB" && (ndtd.IsDeleted.IsNull() || ndtd.IsDeleted == 0)
                );
            }
            else
                phrl.Where(vital.VitalSignInitial != "CURB");

            // Abaikan filter tanggal jika waktunya hampir bersamaan dengan waktu sekarang  
            // lastDateTime untuk keperluan display di asesmen (kondisi vital sign saat pembuatan asesmen)
            if (DateTime.Now > lastDateTime.AddSeconds(10))
            {
                // RecordDate tidak seragam ...ada yg include time ada yg tidak
                // Ambil semua record di tanggal sebelumnya
                // ditambah record ditanggal dipilih dgn batasan time (untuk RecordDate yg include time)
                // ditambah record ditanggal dipilih dgn batasan RecordTime

                phrl.Where(phr.Or(phr.RecordDate <= lastDateTime.Date,
                    phr.And(phr.RecordDate > lastDateTime.Date, phr.RecordDate <= lastDateTime),
                    phr.And(phr.RecordDate == lastDateTime.Date, phr.RecordTime <= lastDateTime.ToString("HH:mm"))));
            }

            phrl.Select(quest.SRAnswerType, vital.VitalSignID, vital.VitalSignName, phrl.QuestionAnswerPrefix,
                phrl.QuestionAnswerSuffix,
                phrl.QuestionAnswerText, phrl.QuestionAnswerNum, phrl.QuestionAnswerText2, phr.RecordDate, phr.RecordTime, vital.VitalSignInitial);

            if (isForCURB)
                phrl.OrderBy(quest.QuestionID.Ascending,phr.RecordDate.Descending, phrl.TransactionNo.Descending);
            else
                phrl.OrderBy(vital.SRVitalSignGroup.Ascending, vital.RowIndexInGroup.Ascending, vital.VitalSignName.Ascending, phr.RecordDate.Descending, phrl.TransactionNo.Descending);

            var dtb = phrl.LoadDataTable();
            return dtb;
        }

        public static DataTable VitalSignLastValue(string registrationNo, string referFromRegistrationNo, bool isWithEwsLevel)
        {
            var dtb = LoadOrderByTransactionNoDesc(registrationNo, referFromRegistrationNo);

            // Ambil yg terakhir dan ada isinya
            var dtbSelected = dtb.Clone();
            var id = string.Empty;
            foreach (DataRow row in dtb.Rows)
            {
                if (!id.Equals(row["VitalSignID"]) && (!string.IsNullOrEmpty(row["QuestionAnswerText"].ToString()) || ConvertToDecimal(row["QuestionAnswerNum"]) != 0))
                {
                    id = row["VitalSignID"].ToString();
                    //Copy
                    dtbSelected.Rows.Add(row.ItemArray);
                }
            }


            // EWS & Formated
            var dateOfBirth = DateTime.Today;
            if (isWithEwsLevel)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(registrationNo);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                dateOfBirth = pat.DateOfBirth ?? DateTime.Today;
            }

            dtbSelected.Columns.Add("EwsLevelColor", typeof(System.String));
            dtbSelected.Columns.Add("QuestionAnswerFormatted", typeof(System.String));

            foreach (DataRow row in dtbSelected.Rows)
            {
                // EWS Level Color
                string ewsLevel = "white";
                if (isWithEwsLevel && ConvertToDecimal(row["QuestionAnswerNum"]) != 0)
                {
                    ewsLevel = VitalSign.EwsLevelColor(row["VitalSignID"].ToString(), dateOfBirth, Convert.ToDateTime(row["RecordDate"]),
                        ConvertToDecimal(row["QuestionAnswerNum"]));
                }

                row["EwsLevelColor"] = ewsLevel;

                // QuestionAnswerFormatted
                row["QuestionAnswerFormatted"] = VitalSignValueFormated(row);
            }
            dtbSelected.AcceptChanges();
            return dtbSelected;
        }

        private static DataTable LoadOrderByTransactionNoDesc(string registrationNo, string referFromRegistrationNo)
        {
            var phrl = new PatientHealthRecordLineQuery("phrl");
            var quest = new QuestionQuery("q");
            phrl.InnerJoin(quest).On(phrl.QuestionID == quest.QuestionID);


            var vital = new VitalSignQuery("v");
            phrl.InnerJoin(vital).On(quest.VitalSignID == vital.VitalSignID);

            var phr = new PatientHealthRecordQuery("phr");
            phrl.InnerJoin(phr).On(phrl.TransactionNo == phr.TransactionNo);

            if (!string.IsNullOrEmpty(referFromRegistrationNo))
                phrl.Where(phrl.Or(phr.RegistrationNo == registrationNo, phr.RegistrationNo == referFromRegistrationNo)); // Query cost optimal checked by Handono 231205, cost phr:phrl -> 3:97
            else
                phrl.Where(phr.RegistrationNo == registrationNo);

            phrl.Where(vital.VitalSignInitial != "CURB");
            phrl.Select(quest.SRAnswerType, vital.VitalSignID, vital.VitalSignName, phrl.QuestionAnswerPrefix,
                phrl.QuestionAnswerSuffix,
                phrl.QuestionAnswerText, phrl.QuestionAnswerNum, phrl.QuestionAnswerText2, phr.RecordDate, phr.RecordTime, vital.VitalSignInitial);
            phrl.OrderBy(vital.SRVitalSignGroup.Ascending, vital.RowIndexInGroup.Ascending, phrl.TransactionNo.Descending);

            var dtb = phrl.LoadDataTable();
            return dtb;
        }

        #endregion
    }
}

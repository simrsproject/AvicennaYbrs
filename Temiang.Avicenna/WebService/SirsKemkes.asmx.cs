using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for SirsKemkes
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SirsKemkes : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void RekapPasienMasuk(string date)
        {
            string format = "yyyy-MM-dd";
            DateTime.TryParseExact(date, format, null, System.Globalization.DateTimeStyles.None, out var parsed);

            var reg = new RegistrationQuery("a");
            var patient = new PatientQuery("b");
            var asri = new AppStandardReferenceItemQuery("c");

            reg.Select(
                reg.SRRegistrationType,
                patient.Sex,
                "<ISNULL(a.SRCovidStatus, CAST(0 AS TINYINT)) AS SRCovidStatus>"
                );

            reg.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            reg.LeftJoin(asri).On(reg.SRCovidStatus == asri.ItemID && asri.StandardReferenceID == AppEnum.StandardReference.CovidStatus);
            reg.Where(reg.RegistrationDate.Date() == parsed.Date, reg.IsVoid == false);
            reg.Where("<ISNULL(a.SRCovidStatus, CAST(0 AS TINYINT)) > 0>");

            var table = reg.LoadDataTable();

            var svc = new Common.SirsKemkes.Service();
            var request = new Common.SirsKemkes.EntryDataPasien.Request.RekapPasienMasuk()
            {
                Tanggal = date,
                IgdSuspectL = table.AsEnumerable().Count(t => t.Field<string>("SRRegistrationType") == AppConstant.RegistrationType.EmergencyPatient && t.Field<string>("Sex") == "M" && t.Field<byte>("SRCovidStatus") == Convert.ToByte(1)).ToString(),
                IgdSuspectP = table.AsEnumerable().Count(t => t.Field<string>("SRRegistrationType") == AppConstant.RegistrationType.EmergencyPatient && t.Field<string>("Sex") == "F" && t.Field<byte>("SRCovidStatus") == Convert.ToByte(1)).ToString(),
                IgdConfirmL = table.AsEnumerable().Count(t => t.Field<string>("SRRegistrationType") == AppConstant.RegistrationType.EmergencyPatient && t.Field<string>("Sex") == "M" && t.Field<byte>("SRCovidStatus") == Convert.ToByte(2)).ToString(),
                IgdConfirmP = table.AsEnumerable().Count(t => t.Field<string>("SRRegistrationType") == AppConstant.RegistrationType.EmergencyPatient && t.Field<string>("Sex") == "F" && t.Field<byte>("SRCovidStatus") == Convert.ToByte(2)).ToString(),
                RjSuspectL = table.AsEnumerable().Count(t => t.Field<string>("SRRegistrationType") == AppConstant.RegistrationType.OutPatient && t.Field<string>("Sex") == "M" && t.Field<byte>("SRCovidStatus") == Convert.ToByte(1)).ToString(),
                RjSuspectP = table.AsEnumerable().Count(t => t.Field<string>("SRRegistrationType") == AppConstant.RegistrationType.OutPatient && t.Field<string>("Sex") == "F" && t.Field<byte>("SRCovidStatus") == Convert.ToByte(1)).ToString(),
                RjConfirmP = table.AsEnumerable().Count(t => t.Field<string>("SRRegistrationType") == AppConstant.RegistrationType.OutPatient && t.Field<string>("Sex") == "M" && t.Field<byte>("SRCovidStatus") == Convert.ToByte(2)).ToString(),
                RjConfirmL = table.AsEnumerable().Count(t => t.Field<string>("SRRegistrationType") == AppConstant.RegistrationType.OutPatient && t.Field<string>("Sex") == "F" && t.Field<byte>("SRCovidStatus") == Convert.ToByte(2)).ToString(),
                RiSuspectL = table.AsEnumerable().Count(t => t.Field<string>("SRRegistrationType") == AppConstant.RegistrationType.InPatient && t.Field<string>("Sex") == "M" && t.Field<byte>("SRCovidStatus") == Convert.ToByte(1)).ToString(),
                RiSuspectP = table.AsEnumerable().Count(t => t.Field<string>("SRRegistrationType") == AppConstant.RegistrationType.InPatient && t.Field<string>("Sex") == "F" && t.Field<byte>("SRCovidStatus") == Convert.ToByte(1)).ToString(),
                RiConfirmL = table.AsEnumerable().Count(t => t.Field<string>("SRRegistrationType") == AppConstant.RegistrationType.InPatient && t.Field<string>("Sex") == "M" && t.Field<byte>("SRCovidStatus") == Convert.ToByte(2)).ToString(),
                RiConfirmP = table.AsEnumerable().Count(t => t.Field<string>("SRRegistrationType") == AppConstant.RegistrationType.InPatient && t.Field<string>("Sex") == "F" && t.Field<byte>("SRCovidStatus") == Convert.ToByte(2)).ToString()
            };
            var response = svc.RekapPasienMasuk(request);

            var wsal = new WebServiceAPILog()
            {
                DateRequest = parsed,
                IPAddress = "",
                UrlAddress = "http://sirs.kemkes.go.id/fo/index.php/LapV2/PasienMasuk",
                Params = JsonConvert.SerializeObject(request),
                Response = JsonConvert.SerializeObject(response),
                Totalms = 0
            };
            wsal.Save();
        }

        [WebMethod]
        public void RekapPasienDirawatDenganKomorbid(string date)
        {
            string format = "yyyy-MM-dd";
            DateTime.TryParseExact(date, format, null, System.Globalization.DateTimeStyles.None, out var parsed);

            var reg = new RegistrationQuery("a");
            var patient = new PatientQuery("b");
            var asri = new AppStandardReferenceItemQuery("c");
            var su = new ServiceUnitQuery("d");
            var sr = new ServiceRoomQuery("e");

            reg.Select(
                reg.SRRegistrationType,
                patient.Sex,
                "<ISNULL(a.SRCovidStatus, CAST(0 AS TINYINT)) AS SRCovidStatus>",
                su.SRInpatientClassificationUnit,
                sr.IsIsolationRoom,
                sr.IsNegativePressureRoom,
                sr.IsPandemicRoom,
                sr.IsVentilator,
                reg.SRCovidComorbidStatus
                );

            reg.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            reg.LeftJoin(asri).On(reg.SRCovidStatus == asri.ItemID && asri.StandardReferenceID == AppEnum.StandardReference.CovidStatus);
            reg.InnerJoin(su).On(reg.ServiceUnitID == reg.ServiceUnitID);
            reg.InnerJoin(sr).On(reg.RoomID == reg.RoomID);
            reg.Where(reg.SRRegistrationType == AppConstant.RegistrationType.InPatient, reg.DischargeDate.IsNull(), reg.IsVoid == false);
            reg.Where("<ISNULL(a.SRCovidStatus, CAST(0 AS TINYINT)) > 0>");
            reg.Where("<ISNULL(a.SRCovidComorbidStatus, '') == '1'>");

            var table = reg.LoadDataTable();

            var svc = new Common.SirsKemkes.Service();
            var request = new Common.SirsKemkes.EntryDataPasien.Request.RekapPasienDirawatDenganKomorbid()
            {
                Tanggal = date,
                IcuDenganVentilatorSuspectL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                              t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                              t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                              !t.Field<bool>("IsNegativePressureRoom") &&
                                                                              t.Field<bool>("IsVentilator")).ToString(),
                IcuDenganVentilatorSuspectP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                              t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                              t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                              !t.Field<bool>("IsNegativePressureRoom") &&
                                                                              t.Field<bool>("IsVentilator")).ToString(),
                IcuDenganVentilatorConfirmL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                              t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                              t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                              !t.Field<bool>("IsNegativePressureRoom") &&
                                                                              t.Field<bool>("IsVentilator")).ToString(),
                IcuDenganVentilatorConfirmP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                              t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                              t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                              !t.Field<bool>("IsNegativePressureRoom") &&
                                                                              t.Field<bool>("IsVentilator")).ToString(),
                IcuTanpaVentilatorSuspectL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                             t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                             t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                             !t.Field<bool>("IsNegativePressureRoom") &&
                                                                             !t.Field<bool>("IsVentilator")).ToString(),
                IcuTanpaVentilatorSuspectP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                             t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                             t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                             !t.Field<bool>("IsNegativePressureRoom") &&
                                                                             !t.Field<bool>("IsVentilator")).ToString(),
                IcuTanpaVentilatorConfirmL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                             t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                             t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                             !t.Field<bool>("IsNegativePressureRoom") &&
                                                                             !t.Field<bool>("IsVentilator")).ToString(),
                IcuTanpaVentilatorConfirmP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                             t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                             t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                             !t.Field<bool>("IsNegativePressureRoom") &&
                                                                             !t.Field<bool>("IsVentilator")).ToString(),

                IcuTekananNegatifDenganVentilatorSuspectL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                                            t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                                            t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                                            t.Field<bool>("IsNegativePressureRoom") &&
                                                                                            t.Field<bool>("IsVentilator")).ToString(),
                IcuTekananNegatifDenganVentilatorSuspectP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                                            t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                                            t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                                            t.Field<bool>("IsNegativePressureRoom") &&
                                                                                            t.Field<bool>("IsVentilator")).ToString(),
                IcuTekananNegatifDenganVentilatorConfirmL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                                           t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                                           t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                                           t.Field<bool>("IsNegativePressureRoom") &&
                                                                                           t.Field<bool>("IsVentilator")).ToString(),
                IcuTekananNegatifDenganVentilatorConfirmP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                                           t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                                           t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                                           t.Field<bool>("IsNegativePressureRoom") &&
                                                                                           t.Field<bool>("IsVentilator")).ToString(),

                IcuTekananNegatifTanpaVentilatorSuspectL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                                           t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                                           t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                                           t.Field<bool>("IsNegativePressureRoom") &&
                                                                                           !t.Field<bool>("IsVentilator")).ToString(),
                IcuTekananNegatifTanpaVentilatorSuspectP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                                           t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                                           t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                                           t.Field<bool>("IsNegativePressureRoom") &&
                                                                                           !t.Field<bool>("IsVentilator")).ToString(),
                IcuTekananNegatifTanpaVentilatorConfirmL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                                           t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                                           t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                                           t.Field<bool>("IsNegativePressureRoom") &&
                                                                                           !t.Field<bool>("IsVentilator")).ToString(),
                IcuTekananNegatifTanpaVentilatorConfirmP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                                           t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                                           t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                                           t.Field<bool>("IsNegativePressureRoom") &&
                                                                                           !t.Field<bool>("IsVentilator")).ToString(),

                IsolasiTekananNegatifSuspectL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                                t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                                t.Field<string>("SRInpatientClassificationUnit") == "4" &&
                                                                                t.Field<bool>("IsNegativePressureRoom")).ToString(),
                IsolasiTekananNegatifSuspectP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                                t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                                t.Field<string>("SRInpatientClassificationUnit") == "4" &&
                                                                                t.Field<bool>("IsNegativePressureRoom")).ToString(),
                IsolasiTekananNegatifConfirmL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                                t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                                t.Field<string>("SRInpatientClassificationUnit") == "4" &&
                                                                                t.Field<bool>("IsNegativePressureRoom")).ToString(),
                IsolasiTekananNegatifConfirmP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                                t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                                t.Field<string>("SRInpatientClassificationUnit") == "4" &&
                                                                                t.Field<bool>("IsNegativePressureRoom")).ToString(),

                IsolasiTanpaTekananNegatifSuspectL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                                     t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                                     t.Field<string>("SRInpatientClassificationUnit") == "4" &&
                                                                                     !t.Field<bool>("IsNegativePressureRoom")).ToString(),
                IsolasiTanpaTekananNegatifSuspectP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                                     t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                                     t.Field<string>("SRInpatientClassificationUnit") == "4" &&
                                                                                     !t.Field<bool>("IsNegativePressureRoom")).ToString(),
                IsolasiTanpaTekananNegatifConfirmL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                                     t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                                     t.Field<string>("SRInpatientClassificationUnit") == "4" &&
                                                                                     !t.Field<bool>("IsNegativePressureRoom")).ToString(),
                IsolasiTanpaTekananNegatifConfirmP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                                     t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                                     t.Field<string>("SRInpatientClassificationUnit") == "4" &&
                                                                                     !t.Field<bool>("IsNegativePressureRoom")).ToString(),

                NicuKhususCovidSuspectL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                          t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                          t.Field<string>("SRInpatientClassificationUnit") == "2").ToString(),
                NicuKhususCovidSuspectP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                          t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                          t.Field<string>("SRInpatientClassificationUnit") == "2").ToString(),
                NicuKhususCovidConfirmL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                          t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                          t.Field<string>("SRInpatientClassificationUnit") == "2").ToString(),
                NicuKhususCovidConfirmP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                          t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                          t.Field<string>("SRInpatientClassificationUnit") == "2").ToString(),

                PicuKhususCovidSuspectL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                          t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                          t.Field<string>("SRInpatientClassificationUnit") == "3").ToString(),
                PicuKhususCovidSuspectP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                          t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                          t.Field<string>("SRInpatientClassificationUnit") == "3").ToString(),
                PicuKhususCovidConfirmL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                          t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                          t.Field<string>("SRInpatientClassificationUnit") == "3").ToString(),
                PicuKhususCovidConfirmP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                          t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                          t.Field<string>("SRInpatientClassificationUnit") == "3").ToString()
            };
            var response = svc.RekapPasienDirawatDenganKomorbid(request);

            var wsal = new WebServiceAPILog()
            {
                DateRequest = parsed,
                IPAddress = "",
                UrlAddress = "http://sirs.kemkes.go.id/fo/index.php/LapV2/PasienDirawatKomorbid",
                Params = JsonConvert.SerializeObject(request),
                Response = JsonConvert.SerializeObject(response),
                Totalms = 0
            };
            wsal.Save();
        }

        [WebMethod]
        public void RekapPasienDirawatTanpaKomorbid(string date)
        {
            string format = "yyyy-MM-dd";
            DateTime.TryParseExact(date, format, null, System.Globalization.DateTimeStyles.None, out var parsed);

            var reg = new RegistrationQuery("a");
            var patient = new PatientQuery("b");
            var asri = new AppStandardReferenceItemQuery("c");
            var su = new ServiceUnitQuery("d");
            var sr = new ServiceRoomQuery("e");

            reg.Select(
                reg.SRRegistrationType,
                patient.Sex,
                "<ISNULL(a.SRCovidStatus, CAST(0 AS TINYINT)) AS SRCovidStatus>",
                su.SRInpatientClassificationUnit,
                sr.IsIsolationRoom,
                sr.IsNegativePressureRoom,
                sr.IsPandemicRoom,
                sr.IsVentilator,
                reg.SRCovidComorbidStatus
                );

            reg.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            reg.LeftJoin(asri).On(reg.SRCovidStatus == asri.ItemID && asri.StandardReferenceID == AppEnum.StandardReference.CovidStatus);
            reg.InnerJoin(su).On(reg.ServiceUnitID == reg.ServiceUnitID);
            reg.InnerJoin(sr).On(reg.RoomID == reg.RoomID);
            reg.Where(reg.SRRegistrationType == AppConstant.RegistrationType.InPatient, reg.DischargeDate.IsNull(), reg.IsVoid == false);
            reg.Where("<ISNULL(a.SRCovidStatus, CAST(0 AS TINYINT)) > 0>");
            reg.Where("<ISNULL(a.SRCovidComorbidStatus, '') == '2'>");

            var table = reg.LoadDataTable();

            var svc = new Common.SirsKemkes.Service();
            var request = new Common.SirsKemkes.EntryDataPasien.Request.RekapPasienDirawatTanpaKomorbid()
            {
                Tanggal = date,
                IcuDenganVentilatorSuspectL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                              t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                              t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                              !t.Field<bool>("IsNegativePressureRoom") &&
                                                                              t.Field<bool>("IsVentilator")).ToString(),
                IcuDenganVentilatorSuspectP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                              t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                              t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                              !t.Field<bool>("IsNegativePressureRoom") &&
                                                                              t.Field<bool>("IsVentilator")).ToString(),
                IcuDenganVentilatorConfirmL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                              t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                              t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                              !t.Field<bool>("IsNegativePressureRoom") &&
                                                                              t.Field<bool>("IsVentilator")).ToString(),
                IcuDenganVentilatorConfirmP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                              t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                              t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                              !t.Field<bool>("IsNegativePressureRoom") &&
                                                                              t.Field<bool>("IsVentilator")).ToString(),
                IcuTanpaVentilatorSuspectL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                             t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                             t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                             !t.Field<bool>("IsNegativePressureRoom") &&
                                                                             !t.Field<bool>("IsVentilator")).ToString(),
                IcuTanpaVentilatorSuspectP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                             t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                             t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                             !t.Field<bool>("IsNegativePressureRoom") &&
                                                                             !t.Field<bool>("IsVentilator")).ToString(),
                IcuTanpaVentilatorConfirmL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                             t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                             t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                             !t.Field<bool>("IsNegativePressureRoom") &&
                                                                             !t.Field<bool>("IsVentilator")).ToString(),
                IcuTanpaVentilatorConfirmP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                             t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                             t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                             !t.Field<bool>("IsNegativePressureRoom") &&
                                                                             !t.Field<bool>("IsVentilator")).ToString(),

                IcuTekananNegatifDenganVentilatorSuspectL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                                            t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                                            t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                                            t.Field<bool>("IsNegativePressureRoom") &&
                                                                                            t.Field<bool>("IsVentilator")).ToString(),
                IcuTekananNegatifDenganVentilatorSuspectP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                                            t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                                            t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                                            t.Field<bool>("IsNegativePressureRoom") &&
                                                                                            t.Field<bool>("IsVentilator")).ToString(),
                IcuTekananNegatifDenganVentilatorConfirmL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                                           t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                                           t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                                           t.Field<bool>("IsNegativePressureRoom") &&
                                                                                           t.Field<bool>("IsVentilator")).ToString(),
                IcuTekananNegatifDenganVentilatorConfirmP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                                           t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                                           t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                                           t.Field<bool>("IsNegativePressureRoom") &&
                                                                                           t.Field<bool>("IsVentilator")).ToString(),

                IcuTekananNegatifTanpaVentilatorSuspectL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                                           t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                                           t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                                           t.Field<bool>("IsNegativePressureRoom") &&
                                                                                           !t.Field<bool>("IsVentilator")).ToString(),
                IcuTekananNegatifTanpaVentilatorSuspectP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                                           t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                                           t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                                           t.Field<bool>("IsNegativePressureRoom") &&
                                                                                           !t.Field<bool>("IsVentilator")).ToString(),
                IcuTekananNegatifTanpaVentilatorConfirmL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                                           t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                                           t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                                           t.Field<bool>("IsNegativePressureRoom") &&
                                                                                           !t.Field<bool>("IsVentilator")).ToString(),
                IcuTekananNegatifTanpaVentilatorConfirmP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                                           t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                                           t.Field<string>("SRInpatientClassificationUnit") == "1" &&
                                                                                           t.Field<bool>("IsNegativePressureRoom") &&
                                                                                           !t.Field<bool>("IsVentilator")).ToString(),

                IsolasiTekananNegatifSuspectL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                                t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                                t.Field<string>("SRInpatientClassificationUnit") == "4" &&
                                                                                t.Field<bool>("IsNegativePressureRoom")).ToString(),
                IsolasiTekananNegatifSuspectP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                                t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                                t.Field<string>("SRInpatientClassificationUnit") == "4" &&
                                                                                t.Field<bool>("IsNegativePressureRoom")).ToString(),
                IsolasiTekananNegatifConfirmL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                                t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                                t.Field<string>("SRInpatientClassificationUnit") == "4" &&
                                                                                t.Field<bool>("IsNegativePressureRoom")).ToString(),
                IsolasiTekananNegatifConfirmP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                                t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                                t.Field<string>("SRInpatientClassificationUnit") == "4" &&
                                                                                t.Field<bool>("IsNegativePressureRoom")).ToString(),

                IsolasiTanpaTekananNegatifSuspectL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                                     t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                                     t.Field<string>("SRInpatientClassificationUnit") == "4" &&
                                                                                     !t.Field<bool>("IsNegativePressureRoom")).ToString(),
                IsolasiTanpaTekananNegatifSuspectP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                                     t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                                     t.Field<string>("SRInpatientClassificationUnit") == "4" &&
                                                                                     !t.Field<bool>("IsNegativePressureRoom")).ToString(),
                IsolasiTanpaTekananNegatifConfirmL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                                     t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                                     t.Field<string>("SRInpatientClassificationUnit") == "4" &&
                                                                                     !t.Field<bool>("IsNegativePressureRoom")).ToString(),
                IsolasiTanpaTekananNegatifConfirmP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                                     t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                                     t.Field<string>("SRInpatientClassificationUnit") == "4" &&
                                                                                     !t.Field<bool>("IsNegativePressureRoom")).ToString(),

                NicuKhususCovidSuspectL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                          t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                          t.Field<string>("SRInpatientClassificationUnit") == "2").ToString(),
                NicuKhususCovidSuspectP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                          t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                          t.Field<string>("SRInpatientClassificationUnit") == "2").ToString(),
                NicuKhususCovidConfirmL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                          t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                          t.Field<string>("SRInpatientClassificationUnit") == "2").ToString(),
                NicuKhususCovidConfirmP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                          t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                          t.Field<string>("SRInpatientClassificationUnit") == "2").ToString(),

                PicuKhususCovidSuspectL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                          t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                          t.Field<string>("SRInpatientClassificationUnit") == "3").ToString(),
                PicuKhususCovidSuspectP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                          t.Field<byte>("SRCovidStatus") == Convert.ToByte(1) &&
                                                                          t.Field<string>("SRInpatientClassificationUnit") == "3").ToString(),
                PicuKhususCovidConfirmL = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "M" &&
                                                                          t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                          t.Field<string>("SRInpatientClassificationUnit") == "3").ToString(),
                PicuKhususCovidConfirmP = table.AsEnumerable().Count(t => t.Field<string>("Sex") == "F" &&
                                                                          t.Field<byte>("SRCovidStatus") == Convert.ToByte(2) &&
                                                                          t.Field<string>("SRInpatientClassificationUnit") == "3").ToString()
            };
            var response = svc.RekapPasienDirawatTanpaKomorbid(request);

            var wsal = new WebServiceAPILog()
            {
                DateRequest = parsed,
                IPAddress = "",
                UrlAddress = "http://sirs.kemkes.go.id/fo/index.php/LapV2/PasienDirawatTanpaKomorbid",
                Params = JsonConvert.SerializeObject(request),
                Response = JsonConvert.SerializeObject(response),
                Totalms = 0
            };
            wsal.Save();
        }

        [WebMethod]
        public void RekapPasienKeluar(string date)
        {
            string format = "yyyy-MM-dd";
            DateTime.TryParseExact(date, format, null, System.Globalization.DateTimeStyles.None, out var parsed);

            var reg = new RegistrationQuery("a");
            var patient = new PatientQuery("b");
            var asri = new AppStandardReferenceItemQuery("c");
            var su = new ServiceUnitQuery("d");
            var sr = new ServiceRoomQuery("e");

            reg.Select(
                reg.SRRegistrationType,
                patient.Sex,
                "<ISNULL(a.SRCovidStatus, CAST(0 AS TINYINT)) AS SRCovidStatus>",
                su.SRInpatientClassificationUnit,
                sr.IsIsolationRoom,
                sr.IsNegativePressureRoom,
                sr.IsPandemicRoom,
                sr.IsVentilator,
                reg.SRCovidComorbidStatus,
                reg.SRDischargeMethod,
                reg.SRDischargeCondition,
                reg.AgeInYear,
                reg.AgeInMonth,
                reg.AgeInDay
                );

            reg.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            reg.LeftJoin(asri).On(reg.SRCovidStatus == asri.ItemID && asri.StandardReferenceID == AppEnum.StandardReference.CovidStatus);
            reg.InnerJoin(su).On(reg.ServiceUnitID == reg.ServiceUnitID);
            reg.InnerJoin(sr).On(reg.RoomID == reg.RoomID);
            reg.Where(reg.SRRegistrationType == AppConstant.RegistrationType.InPatient, reg.DischargeDate.Date() == parsed.Date, reg.IsVoid == false);
            reg.Where("<ISNULL(a.SRCovidStatus, CAST(0 AS TINYINT)) > 0>");

            var table = reg.LoadDataTable();

            var svc = new Common.SirsKemkes.Service();
            var request = new Common.SirsKemkes.EntryDataPasien.Request.RekapPasienKeluar.Simpan()
            {
                Tanggal = date,
                Sembuh = table.AsEnumerable().Count(t => new string[] { "E01", "I01", "O01" }.Contains(t.Field<string>("SRDischargeCondition"))).ToString(),
                Discarded = "0",
                MeninggalKomorbid = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "1" &&
                                                                    new string[] { "E05", "E06", "E07", "I04", "I05", "O05", "E06", "E07", "E08" }.Contains(t.Field<string>("SRDischargeCondition"))).ToString(),
                MeninggalTanpaKomorbid = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "2" &&
                                                                         new string[] { "E05", "E06", "E07", "I04", "I05", "O05", "E06", "E07", "E08" }.Contains(t.Field<string>("SRDischargeCondition"))).ToString(),
                MeninggalProbPreKomorbid = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "1" &&
                                                                           new string[] { "E05", "E06", "E07", "I04", "I05", "O05", "E06", "E07", "E08" }.Contains(t.Field<string>("SRDischargeCondition")) &&
                                                                           t.Field<byte>("AgeInYear") == Convert.ToByte(0) &&
                                                                           t.Field<byte>("AgeInMonth") == Convert.ToByte(0) &&
                                                                           t.Field<byte>("AgeInDay") <= Convert.ToByte(6)).ToString(),
                MeninggalProbNeoKomorbid = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "1" &&
                                                                           new string[] { "E05", "E06", "E07", "I04", "I05", "O05", "E06", "E07", "E08" }.Contains(t.Field<string>("SRDischargeCondition")) &&
                                                                           t.Field<byte>("AgeInYear") == Convert.ToByte(0) &&
                                                                           t.Field<byte>("AgeInMonth") == Convert.ToByte(0) &&
                                                                           t.Field<byte>("AgeInDay") >= Convert.ToByte(7) &&
                                                                           t.Field<byte>("AgeInDay") <= Convert.ToByte(28)).ToString(),
                MeninggalProbBayiKomorbid = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "1" &&
                                                                            new string[] { "E05", "E06", "E07", "I04", "I05", "O05", "E06", "E07", "E08" }.Contains(t.Field<string>("SRDischargeCondition")) &&
                                                                            t.Field<byte>("AgeInYear") <= Convert.ToByte(1) &&
                                                                            t.Field<byte>("AgeInMonth") <= Convert.ToByte(12) &&
                                                                            t.Field<byte>("AgeInDay") >= Convert.ToByte(29)).ToString(),
                MeninggalProbBalitaKomorbid = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "1" &&
                                                                            new string[] { "E05", "E06", "E07", "I04", "I05", "O05", "E06", "E07", "E08" }.Contains(t.Field<string>("SRDischargeCondition")) &&
                                                                            t.Field<byte>("AgeInYear") >= Convert.ToByte(1) &&
                                                                            t.Field<byte>("AgeInYear") <= Convert.ToByte(4)).ToString(),
                MeninggalProbAnakKomorbid = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "1" &&
                                                                            new string[] { "E05", "E06", "E07", "I04", "I05", "O05", "E06", "E07", "E08" }.Contains(t.Field<string>("SRDischargeCondition")) &&
                                                                            t.Field<byte>("AgeInYear") >= Convert.ToByte(5) &&
                                                                            t.Field<byte>("AgeInYear") <= Convert.ToByte(18)).ToString(),
                MeninggalProbRemajaKomorbid = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "1" &&
                                                                            new string[] { "E05", "E06", "E07", "I04", "I05", "O05", "E06", "E07", "E08" }.Contains(t.Field<string>("SRDischargeCondition")) &&
                                                                            t.Field<byte>("AgeInYear") >= Convert.ToByte(19) &&
                                                                            t.Field<byte>("AgeInYear") <= Convert.ToByte(40)).ToString(),
                MeninggalProbDwsKomorbid = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "1" &&
                                                                           new string[] { "E05", "E06", "E07", "I04", "I05", "O05", "E06", "E07", "E08" }.Contains(t.Field<string>("SRDischargeCondition")) &&
                                                                           t.Field<byte>("AgeInYear") >= Convert.ToByte(41) &&
                                                                           t.Field<byte>("AgeInYear") <= Convert.ToByte(60)).ToString(),
                MeninggalProbLansiaKomorbid = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "1" &&
                                                                              new string[] { "E05", "E06", "E07", "I04", "I05", "O05", "E06", "E07", "E08" }.Contains(t.Field<string>("SRDischargeCondition")) &&
                                                                              t.Field<byte>("AgeInYear") > Convert.ToByte(60)).ToString(),

                MeninggalProbPreTanpaKomorbid = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "2" &&
                                                                                new string[] { "E05", "E06", "E07", "I04", "I05", "O05", "E06", "E07", "E08" }.Contains(t.Field<string>("SRDischargeCondition")) &&
                                                                                t.Field<byte>("AgeInYear") == Convert.ToByte(0) &&
                                                                                t.Field<byte>("AgeInMonth") == Convert.ToByte(0) &&
                                                                                t.Field<byte>("AgeInDay") <= Convert.ToByte(6)).ToString(),
                MeninggalProbNeoTanpaKomorbid = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "2" &&
                                                                                new string[] { "E05", "E06", "E07", "I04", "I05", "O05", "E06", "E07", "E08" }.Contains(t.Field<string>("SRDischargeCondition")) &&
                                                                                t.Field<byte>("AgeInYear") == Convert.ToByte(0) &&
                                                                                t.Field<byte>("AgeInMonth") == Convert.ToByte(0) &&
                                                                                t.Field<byte>("AgeInDay") >= Convert.ToByte(7) &&
                                                                                t.Field<byte>("AgeInDay") <= Convert.ToByte(28)).ToString(),
                MeninggalProbBayiTanpaKomorbid = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "2" &&
                                                                                 new string[] { "E05", "E06", "E07", "I04", "I05", "O05", "E06", "E07", "E08" }.Contains(t.Field<string>("SRDischargeCondition")) &&
                                                                                 t.Field<byte>("AgeInYear") <= Convert.ToByte(1) &&
                                                                                 t.Field<byte>("AgeInMonth") <= Convert.ToByte(12) &&
                                                                                 t.Field<byte>("AgeInDay") >= Convert.ToByte(29)).ToString(),
                MeninggalProbBalitaTanpaKomorbid = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "2" &&
                                                                                   new string[] { "E05", "E06", "E07", "I04", "I05", "O05", "E06", "E07", "E08" }.Contains(t.Field<string>("SRDischargeCondition")) &&
                                                                                   t.Field<byte>("AgeInYear") >= Convert.ToByte(1) &&
                                                                                   t.Field<byte>("AgeInYear") <= Convert.ToByte(4)).ToString(),
                MeninggalProbAnakTanpaKomorbid = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "2" &&
                                                                                 new string[] { "E05", "E06", "E07", "I04", "I05", "O05", "E06", "E07", "E08" }.Contains(t.Field<string>("SRDischargeCondition")) &&
                                                                                 t.Field<byte>("AgeInYear") >= Convert.ToByte(5) &&
                                                                                 t.Field<byte>("AgeInYear") <= Convert.ToByte(18)).ToString(),
                MeninggalProbRemajaTanpaKomorbid = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "2" &&
                                                                                   new string[] { "E05", "E06", "E07", "I04", "I05", "O05", "E06", "E07", "E08" }.Contains(t.Field<string>("SRDischargeCondition")) &&
                                                                                   t.Field<byte>("AgeInYear") >= Convert.ToByte(19) &&
                                                                                   t.Field<byte>("AgeInYear") <= Convert.ToByte(40)).ToString(),
                MeninggalProbDwsTanpaKomorbid = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "2" &&
                                                                                new string[] { "E05", "E06", "E07", "I04", "I05", "O05", "E06", "E07", "E08" }.Contains(t.Field<string>("SRDischargeCondition")) &&
                                                                                t.Field<byte>("AgeInYear") >= Convert.ToByte(41) &&
                                                                                t.Field<byte>("AgeInYear") <= Convert.ToByte(60)).ToString(),
                MeninggalProbLansiaTanpaKomorbid = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "2" &&
                                                                                   new string[] { "E05", "E06", "E07", "I04", "I05", "O05", "E06", "E07", "E08" }.Contains(t.Field<string>("SRDischargeCondition")) &&
                                                                                   t.Field<byte>("AgeInYear") > Convert.ToByte(60)).ToString(),

                MeninggalDiscardedKomorbid = "0",
                MeninggalDiscardedTanpaKomorbid = "0",
                Dirujuk = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "1" &&
                                                          new string[] { "E09", "E11", "E12", "I02" }.Contains(t.Field<string>("SRDischargeMethod"))).ToString(),
                Isman = "0",
                Aps = table.AsEnumerable().Count(t => t.Field<string>("SRCovidComorbidStatus") == "1" &&
                                                      new string[] { "E04", "E11", "I01", "I04", "O05" }.Contains(t.Field<string>("SRDischargeMethod"))).ToString(),
            };
            var response = svc.RekapPasienKeluar(request);

            var wsal = new WebServiceAPILog()
            {
                DateRequest = parsed,
                IPAddress = "",
                UrlAddress = "http://sirs.kemkes.go.id/fo/index.php/LapV2/PasienKeluar",
                Params = JsonConvert.SerializeObject(request),
                Response = JsonConvert.SerializeObject(response),
                Totalms = 0
            };
            wsal.Save();
        }
    }
}

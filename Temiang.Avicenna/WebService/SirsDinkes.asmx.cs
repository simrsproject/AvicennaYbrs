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
    /// Summary description for SirsDinkes
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SirsDinkes : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string KetersediaanBed(string date)
        {
            string format = "yyyy-MM-dd";
            DateTime.TryParseExact(date, format, null, System.Globalization.DateTimeStyles.None, out var parsed);

            //var reg = new RegistrationQuery("a");
            //var patient = new PatientQuery("b");
            //var asri = new AppStandardReferenceItemQuery("c");

            //reg.Select(
            //    reg.SRRegistrationType,
            //    patient.Sex,
            //    "<ISNULL(a.SRCovidStatus, CAST(0 AS TINYINT)) AS SRCovidStatus>"
            //    );

            //reg.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            //reg.LeftJoin(asri).On(reg.SRCovidStatus == asri.ItemID && asri.StandardReferenceID == AppEnum.StandardReference.CovidStatus);
            //reg.Where(reg.RegistrationDate.Date() == parsed.Date, reg.IsVoid == false);
            //reg.Where("<ISNULL(a.SRCovidStatus, CAST(0 AS TINYINT)) > 0>");

            //var table = reg.LoadDataTable();

            var unit = new ServiceUnitQuery("a");
            var room = new ServiceRoomQuery("b");
            var bed = new BedQuery("c");
            var reg = new RegistrationQuery("d");
            var cls = new ClassQuery("e");

            unit.Select(unit.ServiceUnitID, unit.SRInpatientClassificationUnit, room.IsPandemicRoom, room.IsVentilator, room.IsNegativePressureRoom, bed.BedID, cls.SRClassRL, reg.RegistrationNo);
            unit.InnerJoin(room).On(unit.ServiceUnitID == room.ServiceUnitID);
            unit.LeftJoin(bed).On(room.RoomID == bed.BedID);
            unit.LeftJoin(cls).On(bed.ClassID == cls.ClassID);
            unit.LeftJoin(reg).On(bed.RegistrationNo == reg.RegistrationNo && bed.BedID == reg.BedID);

            unit.Where(
                unit.Or(unit.SRRegistrationType.IsNotNull(), unit.SRRegistrationType != string.Empty),
                unit.Or(unit.SRInpatientClassificationUnit.IsNotNull(), unit.SRInpatientClassificationUnit != string.Empty),
                unit.IsActive == true,
                room.IsActive == true,
                bed.IsTemporary == false,
                bed.IsActive == true);

            var table = unit.LoadDataTable();
            var covid = table.AsEnumerable().Where(t => t.Field<bool>("IsPandemicRoom"));
            var nonCovid = table.AsEnumerable().Where(t => !t.Field<bool>("IsPandemicRoom"));


            var svc = new Common.SirsDinkes.Service();
            var request = new Common.SirsDinkes.KetersediaanBed.Request.Root()
            {
                Covid = new Common.SirsDinkes.KetersediaanBed.Request.Covid()
                {
                    Kapasitas = new Common.SirsDinkes.KetersediaanBed.Request.Kapasitas()
                    {
                        KapasitasIcuTekananNegatifDenganVentilatorCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "1" && c.Field<bool>("IsVentilator") && c.Field<bool>("IsNegativePressureRoom")),
                        KapasitasIcuTekananNegatifTanpaVentilatorCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "1" && !c.Field<bool>("IsVentilator") && c.Field<bool>("IsNegativePressureRoom")),
                        KapasitasIcuTanpaTekananNegatifDenganVentilatorCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "1" && c.Field<bool>("IsVentilator") && !c.Field<bool>("IsNegativePressureRoom")),
                        KapasitasIcuTanpaTekananNegatifTanpaVentilatorCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "1" && !c.Field<bool>("IsVentilator") && !c.Field<bool>("IsNegativePressureRoom")),
                        KapasitasIsolasiTekananNegatifCovidPria = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "4" && c.Field<bool>("IsNegativePressureRoom")),
                        KapasitasIsolasiTekananNegatifCovidWanita = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "4" && c.Field<bool>("IsNegativePressureRoom")),
                        KapasitasIsolasiTekananNegatifCovidAnak = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "4" && c.Field<bool>("IsNegativePressureRoom")),
                        KapasitasNicuCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "2"),
                        KapasitasPicuCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "3"),
                        KapasitasPerinaCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "5"),
                        KapasitasOkCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "6"),
                        KapasitasHdCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "7"),
                        KapasitasIgdCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "8")
                    },
                    Kosong = new Common.SirsDinkes.KetersediaanBed.Request.Kosong()
                    {
                        KosongIcuTekananNegatifDenganVentilatorCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "1" && c.Field<bool>("IsVentilator") && c.Field<bool>("IsNegativePressureRoom") && c.Field<string>("RegistrationNo") == null),
                        KosongIcuTekananNegatifTanpaVentilatorCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "1" && !c.Field<bool>("IsVentilator") && c.Field<bool>("IsNegativePressureRoom") && c.Field<string>("RegistrationNo") == null),
                        KosongIcuTanpaTekananNegatifDenganVentilatorCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "1" && c.Field<bool>("IsVentilator") && !c.Field<bool>("IsNegativePressureRoom") && c.Field<string>("RegistrationNo") == null),
                        KosongIcuTanpaTekananNegatifTanpaVentilatorCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "1" && !c.Field<bool>("IsVentilator") && !c.Field<bool>("IsNegativePressureRoom") && c.Field<string>("RegistrationNo") == null),
                        KosongIsolasiTekananNegatifCovidPria = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "4" && c.Field<bool>("IsNegativePressureRoom") && c.Field<string>("RegistrationNo") == null),
                        KosongIsolasiTekananNegatifCovidWanita = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "4" && c.Field<bool>("IsNegativePressureRoom") && c.Field<string>("RegistrationNo") == null),
                        KosongIsolasiTekananNegatifCovidAnak = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "4" && c.Field<bool>("IsNegativePressureRoom") && c.Field<string>("RegistrationNo") == null),
                        KosongNicuCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "2" && c.Field<string>("RegistrationNo") == null),
                        KosongPicuCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "3" && c.Field<string>("RegistrationNo") == null),
                        KosongPerinaCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "5" && c.Field<string>("RegistrationNo") == null),
                        KosongOkCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "6" && c.Field<string>("RegistrationNo") == null),
                        KosongHdCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "7" && c.Field<string>("RegistrationNo") == null),
                        KosongIgdCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "8" && c.Field<string>("RegistrationNo") == null)
                    }
                },
                NonCovid = new Common.SirsDinkes.KetersediaanBed.Request.NonCovid()
                {
                    Kapasitas = new Common.SirsDinkes.KetersediaanBed.Request.Kapasitas()
                    {
                        KapasitasVipNonCovid = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-001"),
                        KapasitasKelas1NonCovidPria = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-002"),
                        KapasitasKelas1NonCovidWanita = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-002"),
                        KapasitasKelas1NonCovidAnak = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-002"),
                        KapasitasKelas2NonCovidPria = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-003"),
                        KapasitasKelas2NonCovidWanita = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-003"),
                        KapasitasKelas2NonCovidAnak = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-003"),
                        KapasitasKelas3NonCovidPria = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-004"),
                        KapasitasKelas3NonCovidWanita = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-004"),
                        KapasitasKelas3NonCovidAnak = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-004"),
                        KapasitasHcuNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "9"),
                        KapasitasIccuNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "10"),
                        KapasitasIcuNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "1"),
                        KapasitasNicuNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "2"),
                        KapasitasPicuNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "3"),
                        KapasitasPerinaNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "5"),
                        KapasitasOkNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "6"),
                        KapasitasHdNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "7"),
                        KapasitasIsolasiNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "4"),
                        KapasitasIgdNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "8")
                    },
                    Kosong = new Common.SirsDinkes.KetersediaanBed.Request.Kosong()
                    {
                        KosongVipNonCovid = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-001" && c.Field<string>("RegistrationNo") == null),
                        KosongKelas1NonCovidPria = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-002" && c.Field<string>("RegistrationNo") == null),
                        KosongKelas1NonCovidWanita = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-002" && c.Field<string>("RegistrationNo") == null),
                        KosongKelas1NonCovidAnak = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-002" && c.Field<string>("RegistrationNo") == null),
                        KosongKelas2NonCovidPria = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-003" && c.Field<string>("RegistrationNo") == null),
                        KosongKelas2NonCovidWanita = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-003" && c.Field<string>("RegistrationNo") == null),
                        KosongKelas2NonCovidAnak = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-003" && c.Field<string>("RegistrationNo") == null),
                        KosongKelas3NonCovidPria = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-004" && c.Field<string>("RegistrationNo") == null),
                        KosongKelas3NonCovidWanita = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-004" && c.Field<string>("RegistrationNo") == null),
                        KosongKelas3NonCovidAnak = nonCovid.Count(c => c.Field<string>("SRClassRL") == "ClassRL-004" && c.Field<string>("RegistrationNo") == null),
                        KosongHcuNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "9" && c.Field<string>("RegistrationNo") == null),
                        KosongIccuNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "10" && c.Field<string>("RegistrationNo") == null),
                        KosongIcuNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "1" && c.Field<string>("RegistrationNo") == null),
                        KosongNicuNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "2" && c.Field<string>("RegistrationNo") == null),
                        KosongPicuNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "3" && c.Field<string>("RegistrationNo") == null),
                        KosongPerinaNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "5" && c.Field<string>("RegistrationNo") == null),
                        KosongOkNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "6" && c.Field<string>("RegistrationNo") == null),
                        KosongHdNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "7" && c.Field<string>("RegistrationNo") == null),
                        KosongIsolasiNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "4" && c.Field<string>("RegistrationNo") == null),
                        KosongIgdNonCovid = covid.Count(c => c.Field<string>("SRInpatientClassificationUnit") == "8" && c.Field<string>("RegistrationNo") == null)
                    }
                }
            };
            var response = svc.KetersediaanBed(request);

            var wsal = new WebServiceAPILog()
            {
                DateRequest = parsed,
                IPAddress = "",
                UrlAddress = "http://eis.dinkes.jakarta.go.id/apibedv2",
                Params = JsonConvert.SerializeObject(request),
                Response = JsonConvert.SerializeObject(response),
                Totalms = 0
            };
            wsal.Save();

            return response.Messages;
        }
    }
}
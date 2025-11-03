using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for SirsYankes
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class SirsYankes : System.Web.Services.WebService
    {
        [WebMethod]
        public string InsertDataKunjunganIrj()
        {
            var reg = new RegistrationQuery("a");
            var unit = new ServiceUnitQuery("b");
            var grr1 = new GuarantorQuery("c");
            var grr2 = new GuarantorQuery("d");

            reg.Select(
                unit.ServiceUnitName,
                grr1.GuarantorID.Count().As("JKN"),
                grr2.GuarantorID.Count().As("NonJKN")
                );
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            reg.LeftJoin(grr1).On(reg.GuarantorID == grr1.GuarantorID && grr1.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS);
            reg.LeftJoin(grr2).On(reg.GuarantorID == grr2.GuarantorID && grr2.SRGuarantorType != AppSession.Parameter.GuarantorTypeBPJS);
            reg.Where(
                reg.RegistrationDate == DateTime.Now.Date,
                reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
                reg.IsVoid == false,
                reg.IsConsul == false,
                reg.IsFromDispensary == false,
                reg.IsDirectPrescriptionReturn == false,
                reg.IsNonPatient == false
                );
            reg.GroupBy(reg.RegistrationDate, unit.ServiceUnitName);
            reg.OrderBy(reg.RegistrationDate.Ascending, unit.ServiceUnitName.Ascending);

            var table = reg.LoadDataTable();

            var list = new List<Common.SirsYankes.DataKunjungan.Json.Irj>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Common.SirsYankes.DataKunjungan.Json.Irj()
                {
                    KLINIK = row["ServiceUnitName"].ToString(),
                    JKN = row["JKN"].ToString(),
                    NONJKN = row["NonJKN"].ToString()
                });
            }

            if (list.Any())
            {
                var svc = new Common.SirsYankes.Service();
                var response = svc.InsertDataKunjunganIrj(list, DateTime.Now.Date);

                return response;
            }
            return string.Empty;
        }

        [WebMethod]
        public string InsertDataKunjunganIgd()
        {
            var reg = new RegistrationQuery("a");
            var grr1 = new GuarantorQuery("b");
            var grr2 = new GuarantorQuery("c");

            reg.Select(
                grr1.GuarantorID.Count().As("JKN"),
                grr2.GuarantorID.Count().As("NonJKN")
                );
            reg.LeftJoin(grr1).On(reg.GuarantorID == grr1.GuarantorID && grr1.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS);
            reg.LeftJoin(grr2).On(reg.GuarantorID == grr2.GuarantorID && grr2.SRGuarantorType != AppSession.Parameter.GuarantorTypeBPJS);
            reg.Where(
                reg.RegistrationDate == DateTime.Now.Date,
                reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient,
                reg.IsVoid == false,
                reg.IsConsul == false,
                reg.IsFromDispensary == false,
                reg.IsDirectPrescriptionReturn == false,
                reg.IsNonPatient == false
                );
            reg.GroupBy(reg.RegistrationDate);

            var table = reg.LoadDataTable();

            if (table.AsEnumerable().Any())
            {
                var svc = new Common.SirsYankes.Service();
                var response = svc.InsertDataKunjunganIgd(new Common.SirsYankes.DataKunjungan.Json.Igd()
                {
                    JKN = table.Rows[0]["JKN"].ToString(),
                    NONJKN = table.Rows[0]["NonJKN"].ToString()
                }, DateTime.Now.Date);

                return response;
            }
            return string.Empty;
        }

        [WebMethod]
        public string InsertDataKunjunganIri()
        {
            var reg = new RegistrationQuery("a");
            var cls = new ClassQuery("b");

            reg.Select(
                cls.ClassName,
                reg.RegistrationNo.Count().As("JLH")
                );
            reg.InnerJoin(cls).On(reg.ServiceUnitID == cls.ClassName);
            reg.Where(
                reg.RegistrationDate == DateTime.Now.Date,
                reg.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                reg.IsVoid == false,
                reg.IsConsul == false,
                reg.IsFromDispensary == false,
                reg.IsDirectPrescriptionReturn == false,
                reg.IsNonPatient == false
                );
            reg.GroupBy(reg.RegistrationDate, cls.ClassName);
            reg.OrderBy(reg.RegistrationDate.Ascending, cls.ClassName.Ascending);

            var table = reg.LoadDataTable();

            var list = new List<Common.SirsYankes.DataKunjungan.Json.Iri>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Common.SirsYankes.DataKunjungan.Json.Iri()
                {
                    CONTENT = row["ClassName"].ToString(),
                    JLH = row["JLH"].ToString()
                });
            }

            if (list.Any())
            {
                var svc = new Common.SirsYankes.Service();
                var response = svc.InsertDataKunjunganIri(list, DateTime.Now.Date);

                return response;
            }
            return string.Empty;
        }

        [WebMethod]
        public string InsertDiagnosaTerbesarIrj()
        {
            var reg = new RegistrationQuery("a");
            var ed = new EpisodeDiagnoseQuery("b");

            reg.Select(
                reg.RegistrationDate,
                ed.DiagnoseID,
                reg.RegistrationNo.Count().As("JUMLAHKASUS")
                ); ;
            reg.InnerJoin(ed).On(reg.RegistrationNo == ed.RegistrationNo && ed.IsVoid == false);
            reg.Where(
                reg.RegistrationDate.DatePart("month") == DateTime.Now.Date.Month,
                reg.RegistrationDate.DatePart("year") == DateTime.Now.Date.Year,
                reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
                reg.IsVoid == false,
                reg.IsConsul == false,
                reg.IsFromDispensary == false,
                reg.IsDirectPrescriptionReturn == false,
                reg.IsNonPatient == false
                );
            reg.GroupBy(reg.RegistrationDate, ed.DiagnoseID);
            reg.OrderBy(reg.RegistrationDate.Ascending);

            var table = reg.LoadDataTable();

            var list = new List<Common.SirsYankes.DiagnosaTerbesar.Json>();
            foreach (var date in table.AsEnumerable().Select(t => t.Field<DateTime>("RegistrationDate")).Distinct())
            {
                foreach (var row in table.AsEnumerable().Where(t => t.Field<DateTime>("RegistrationDate") == date).OrderByDescending(t => t.Field<int>("JUMLAHKASUS")).Take(10))
                {
                    list.Add(new Common.SirsYankes.DiagnosaTerbesar.Json()
                    {
                        IDDIAG = row["DiagnoseID"].ToString(),
                        JUMLAHKASUS = row["JUMLAHKASUS"].ToString(),
                        TANGGAL = row["RegistrationDate"].ToString()
                    });
                }
            }

            if (list.Any())
            {
                var svc = new Common.SirsYankes.Service();
                var response = svc.InsertDiagnosaTerbesarIrj(list, DateTime.Now.Date);

                return response;
            }
            return string.Empty;
        }

        [WebMethod]
        public string InsertDiagnosaTerbesarIri()
        {
            var reg = new RegistrationQuery("a");
            var ed = new EpisodeDiagnoseQuery("b");

            reg.Select(
                reg.RegistrationDate,
                ed.DiagnoseID,
                reg.RegistrationNo.Count().As("JUMLAHKASUS")
                ); ;
            reg.InnerJoin(ed).On(reg.RegistrationNo == ed.RegistrationNo && ed.IsVoid == false);
            reg.Where(
                reg.RegistrationDate.DatePart("month") == DateTime.Now.Date.Month,
                reg.RegistrationDate.DatePart("year") == DateTime.Now.Date.Year,
                reg.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                reg.IsVoid == false,
                reg.IsConsul == false,
                reg.IsFromDispensary == false,
                reg.IsDirectPrescriptionReturn == false,
                reg.IsNonPatient == false
                );
            reg.GroupBy(reg.RegistrationDate, ed.DiagnoseID);
            reg.OrderBy(reg.RegistrationDate.Ascending);

            var table = reg.LoadDataTable();

            var list = new List<Common.SirsYankes.DiagnosaTerbesar.Json>();
            foreach (var date in table.AsEnumerable().Select(t => t.Field<DateTime>("RegistrationDate")).Distinct())
            {
                foreach (var row in table.AsEnumerable().Where(t => t.Field<DateTime>("RegistrationDate") == date).OrderByDescending(t => t.Field<int>("JUMLAHKASUS")).Take(10))
                {
                    list.Add(new Common.SirsYankes.DiagnosaTerbesar.Json()
                    {
                        IDDIAG = row["DiagnoseID"].ToString(),
                        JUMLAHKASUS = row["JUMLAHKASUS"].ToString(),
                        TANGGAL = row["RegistrationDate"].ToString()
                    });
                }
            }

            if (list.Any())
            {
                var svc = new Common.SirsYankes.Service();
                var response = svc.InsertDiagnosaTerbesarIri(list, DateTime.Now.Date);

                return response;
            }
            return string.Empty;
        }

        [WebMethod]
        public string InsertBedMonitoring()
        {
            var bed = new BedQuery("a");
            var room = new ServiceRoomQuery("b");
            var unit = new ServiceUnitQuery("c");
            var reg = new RegistrationQuery("d");
            var patient = new PatientQuery("e");

            bed.Select(unit.ServiceUnitID, bed.BedID, bed.RegistrationNo, patient.Sex);
            bed.InnerJoin(room).On(bed.RoomID == room.RoomID && room.IsActive == 1);
            bed.InnerJoin(unit).On(room.ServiceUnitID == unit.ServiceUnitID && unit.SRRegistrationType == "IPR" && unit.IsActive == 1);
            bed.LeftJoin(reg).On(bed.RegistrationNo == reg.RegistrationNo);
            bed.LeftJoin(patient).On(reg.PatientID == patient.PatientID);

            bed.Where(bed.IsActive == true, bed.IsTemporary == true);

            var table = bed.LoadDataTable();

            var list = new List<Common.SirsYankes.BedMonitoring.Json>();
            foreach (var suid in table.AsEnumerable().Select(t => t.Field<string>("ServiceUnitID")).Distinct())
            {
                foreach (var row in table.AsEnumerable().Where(t => t.Field<string>("ServiceUnitID") == suid))
                {
                    list.Add(new Common.SirsYankes.BedMonitoring.Json()
                    {
                        KodeRuang = row["ServiceUnitID"].ToString(),
                        TipePasien = string.Empty,
                        TotalTt = table.AsEnumerable().Where(t => t.Field<string>("ServiceUnitID") == suid).Count().ToString(),
                        TerpakaiMale = 
                            table.AsEnumerable().Where(t => t.Field<string>("ServiceUnitID") == suid && !string.IsNullOrEmpty(t.Field<string>("RegistrationNo")) && t.Field<string>("Sex") == "M").Count().ToString(),
                        TerpakaiFemale = 
                            table.AsEnumerable().Where(t => t.Field<string>("ServiceUnitID") == suid && !string.IsNullOrEmpty(t.Field<string>("RegistrationNo")) && t.Field<string>("Sex") == "F").Count().ToString(),
                        KosongMale = (table.AsEnumerable().Where(t => t.Field<string>("ServiceUnitID") == suid).Count() - 
                            table.AsEnumerable().Where(t => t.Field<string>("ServiceUnitID") == suid && !string.IsNullOrEmpty(t.Field<string>("RegistrationNo")) && t.Field<string>("Sex") == "M").Count()).ToString(),
                        KosongFemale = (table.AsEnumerable().Where(t => t.Field<string>("ServiceUnitID") == suid).Count() -
                            table.AsEnumerable().Where(t => t.Field<string>("ServiceUnitID") == suid && !string.IsNullOrEmpty(t.Field<string>("RegistrationNo")) && t.Field<string>("Sex") == "F").Count()).ToString(),
                        Waiting = "0",
                        TglUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    });
                }
            }

            if (list.Any())
            {
                var svc = new Common.SirsYankes.Service();
                var response = svc.InsertBedMonitoring(list);

                return response;
            }
            return string.Empty;
        }

        [WebMethod]
        public string InsertIndikatorPelayanan()
        {
            var hd = new RlTxReportQuery("a");
            var dt = new RlTxReport12Query("b");


            hd.InnerJoin(dt).On(hd.RlTxReportNo == dt.RlTxReportNo);
            hd.Where(hd.PeriodYear == DateTime.Now.Year, hd.PeriodMonthStart == DateTime.Now.Month, hd.PeriodMonthEnd == DateTime.Now.Month);
            
            var table = hd.LoadDataTable();
            if (table.AsEnumerable().Any())
            {
                var list = new Common.SirsYankes.IndikatorPelayanan.Json() { BOR = table.Rows[0]["Bor"].ToString() };

                var svc = new Common.SirsYankes.Service();
                var response = svc.InsertIndikatorPelayanan(list, DateTime.Now.Date);

                return response;
            }
            return string.Empty;
        }
    }
}

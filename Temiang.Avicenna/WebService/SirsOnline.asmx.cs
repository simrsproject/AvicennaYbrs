using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Newtonsoft.Json;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for SirsOnline
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SirsOnline : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string DataKunjunganIgd(DateTime tanggal)
        {
            var reg = new RegistrationQuery("a");
            var unit = new ServiceUnitQuery("b");

            reg.Select(unit.ServiceUnitName, reg.GuarantorID);
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            reg.Where(reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient, reg.RegistrationDate.Date() == tanggal.Date, reg.IsConsul == false, reg.IsVoid == false);

            var table = reg.LoadDataTable();

            var grrID = AppSession.Parameter.GuarantorAskesID;

            var json = new List<Common.SirsOnline.DataKunjungan.Igd>();

            foreach (string klinik in table.AsEnumerable().Select(t => t.Field<string>("ServiceUnitName")).Distinct())
            {
                json.Add(new Common.SirsOnline.DataKunjungan.Igd
                {
                    JKN = table.AsEnumerable().Where(t => t.Field<string>("ServiceUnitName") == klinik && grrID.Contains(t.Field<string>("GuarantorID"))).Count(),
                    NONJKN = table.AsEnumerable().Where(t => t.Field<string>("ServiceUnitName") == klinik && !grrID.Contains(t.Field<string>("GuarantorID"))).Count()
                });
            }

            var svc = new Common.SirsOnline.Service();
            var response = svc.DataKunjunganIgd(json, tanggal.ToString("d-M-yyyy"));

            return response;
        }

        [WebMethod]
        public string DataKunjunganIrj(DateTime tanggal)
        {
            var reg = new RegistrationQuery("a");
            var unit = new ServiceUnitQuery("b");

            reg.Select(unit.ServiceUnitName, reg.GuarantorID);
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            reg.Where(reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient, reg.RegistrationDate.Date() == tanggal.Date, reg.IsConsul == false, reg.IsVoid == false);

            var table = reg.LoadDataTable();

            var grrID = AppSession.Parameter.GuarantorAskesID;

            var json = new List<Common.SirsOnline.DataKunjungan.Irj>();

            foreach (string klinik in table.AsEnumerable().Select(t => t.Field<string>("ServiceUnitName")).Distinct())
            {
                json.Add(new Common.SirsOnline.DataKunjungan.Irj
                {
                    KLINIK = klinik,
                    JKN = table.AsEnumerable().Where(t => t.Field<string>("ServiceUnitName") == klinik && grrID.Contains(t.Field<string>("GuarantorID"))).Count(),
                    NONJKN = table.AsEnumerable().Where(t => t.Field<string>("ServiceUnitName") == klinik && !grrID.Contains(t.Field<string>("GuarantorID"))).Count()
                });
            }

            var svc = new Common.SirsOnline.Service();
            var response = svc.DataKunjunganIrj(json, tanggal.ToString("d-M-yyyy"));

            return response;
        }

        [WebMethod]
        public string DataKunjunganIri(DateTime tanggal)
        {
            var reg = new RegistrationQuery("a");
            var kelas = new ClassQuery("b");

            reg.Select(kelas.ClassName, reg.RegistrationNo);
            reg.InnerJoin(kelas).On(reg.ChargeClassID == kelas.ClassID);
            reg.Where(reg.SRRegistrationType == AppConstant.RegistrationType.InPatient, reg.RegistrationDate.Date() == tanggal.Date, reg.IsConsul == false, reg.IsVoid == false);

            var table = reg.LoadDataTable();

            var json = new List<Common.SirsOnline.DataKunjungan.Iri>();

            foreach (string klinik in table.AsEnumerable().Select(t => t.Field<string>("ClassName")).Distinct())
            {
                json.Add(new Common.SirsOnline.DataKunjungan.Iri
                {
                    CONTENT = klinik,
                    JLH = table.AsEnumerable().Where(t => t.Field<string>("ClassName") == klinik).Count().ToString()
                });
            }

            var svc = new Common.SirsOnline.Service();
            var response = svc.DataKunjunganIri(json, tanggal.ToString("d-M-yyyy"));

            return response;
        }

        [WebMethod]
        public string DiagnosaTerbesar(bool isIri, DateTime tanggal)
        {
            var reg = new RegistrationQuery("a");
            var epi = new EpisodeDiagnoseQuery("b");
            var diag = new DiagnoseQuery("c");

            reg.es.Top = 10;
            reg.Select(reg.RegistrationDate, epi.DiagnoseID, epi.DiagnoseID.Count().As("Total"));
            reg.InnerJoin(epi).On(reg.RegistrationNo == epi.RegistrationNo && epi.SRDiagnoseType == AppSession.Parameter.DiagnoseTypeMain && epi.IsVoid == false);
            reg.InnerJoin(diag).On(epi.DiagnoseID == diag.DiagnoseID);
            if (isIri) reg.Where(reg.SRRegistrationType == AppConstant.RegistrationType.InPatient);
            else reg.Where(reg.SRRegistrationType.In(AppConstant.RegistrationType.EmergencyPatient, AppConstant.RegistrationType.InPatient));
            reg.Where(reg.RegistrationDate.DatePart("month") == tanggal.Date.Month, reg.RegistrationDate.DatePart("year") == tanggal.Date.Year, reg.IsConsul == false, reg.IsVoid == false, epi.DiagnoseID != string.Empty);
            reg.GroupBy(reg.RegistrationDate, epi.DiagnoseID);

            var table = reg.LoadDataTable();

            var json = new List<Common.SirsOnline.DiagnosaTerbesar>();

            foreach (DateTime date in table.AsEnumerable().Select(t => t.Field<DateTime>("RegistrationDate")).OrderBy(t => t).Distinct())
            {
                foreach (DataRow row in table.AsEnumerable().Where(t => t.Field<DateTime>("RegistrationDate") == date).OrderByDescending(t => t.Field<int>("Count")).Take(10))
                {
                    json.Add(new Common.SirsOnline.DiagnosaTerbesar
                    {
                        IDDIAG = row["DiagnoseID"].ToString(),
                        JUMLAHKASUS = Convert.ToInt32(row["Count"]),
                        TANGGAL = date.ToString("dd-MM-yyyy")
                    });
                }
            }

            var svc = new Common.SirsOnline.Service();
            var response = svc.DiagnosaTerbesar(json, isIri, tanggal.ToString("M-yyyy"));

            return response;
        }

        [WebMethod]
        public string IndikatorPelayanan(int bulan, int tahun)
        {
            var rlhd = new RlTxReportQuery("a");
            var rldt = new RlTxReport12Query("b");

            rlhd.es.Top = 1;
            rlhd.Select(rldt.Bor);
            rlhd.InnerJoin(rldt).On(rlhd.RlTxReportNo == rldt.RlTxReportNo);
            rlhd.Where(rlhd.PeriodMonthStart == bulan, rlhd.PeriodMonthEnd == bulan, rlhd.PeriodYear == tahun);
            rlhd.OrderBy(rlhd.LastUpdateDateTime.Descending);

            var table = rlhd.LoadDataTable();

            if (table != null && table.Rows.Count > 0)
            {
                var svc = new Common.SirsOnline.Service();
                var response = svc.IndikatorPelayanan(new Common.SirsOnline.IndikatorPelayanan { BOR = Convert.ToDouble(table.Rows[0]["Bor"]) }, $"{bulan}-{tahun}");

                return response;
            }
            else return "no data";
        }
    }
}

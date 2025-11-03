using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Text.RegularExpressions;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for Sep
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Sep : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetRencanaKontrol(string noRencanaKontrol)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetRencanaKontrolByNoSuratKontrol(noRencanaKontrol);
            Context.Response.Write(response);
        }

        [WebMethod]
        public string GetDetailPeserta(string nama, string nik, string nokartu, string nomr, string sex, string tgllahir, string umur, string jenis, string kelas, string status)
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Nama : {0}", nama));
            sb.AppendLine(string.Format("NIK : {0}", nik));
            sb.AppendLine(string.Format("No Kartu : {0}", nokartu));
            sb.AppendLine(string.Format("No MR : {0}", nomr));
            sb.AppendLine(string.Format("Jenis Kelamin : {0}", sex));
            sb.AppendLine(string.Format("Tanggal Lahir : {0}", tgllahir));
            sb.AppendLine(string.Format("Usia : {0}", umur));
            sb.AppendLine(string.Format("Jenis Peserta : {0}", jenis));
            sb.AppendLine(string.Format("Kelas Tanggungan : {0}", kelas));
            sb.AppendLine(string.Format("Status : {0}", status));

            return sb.ToString();
        }

        [WebMethod]
        public RadComboBoxItemData[] GetDiagnosa(object context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["filter"]).ToLower();

            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetDiagnosa(filter.Split('|')[0]);
            if (!response.MetaData.IsValid)
            {
                return new List<RadComboBoxItemData>()
                {
                    new RadComboBoxItemData()
                    {
                        Text = response.MetaData.Code + " - " + response.MetaData.Message,
                        Value = string.Empty
                    }
                }.ToArray();
            }

            var result = new List<RadComboBoxItemData>(response.Response.Diagnosa.Count());
            foreach (var data in response.Response.Diagnosa)
            {
                var item = new RadComboBoxItemData();
                item.Text = data.Nama;
                item.Value = data.Kode;
                result.Add(item);
            }
            return result.ToArray();
        }

        [WebMethod]
        public RadComboBoxItemData[] GetAsalRujukan(object context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["filter"]).ToLower();

            var count = 0;

            var svc = new Common.BPJS.VClaim.v11.Service();
            var faskes1 = svc.GetFaskes(filter.Split('|')[0], Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);
            count += faskes1.Response != null ? faskes1.Response.Faskes.Count() : 0;

            svc = new Common.BPJS.VClaim.v11.Service();
            var faskes2 = svc.GetFaskes(filter.Split('|')[0], Common.BPJS.VClaim.Enum.JenisFaskes.RS);
            count += faskes1.Response != null ? faskes1.Response.Faskes.Count() : 0;

            //if (!response.MetaData.IsValid) return null;
            var result = new List<RadComboBoxItemData>(count);

            if (faskes1.Response != null)
            {
                foreach (var data in faskes1.Response.Faskes)
                {
                    var item = new RadComboBoxItemData();
                    item.Text = data.Nama;
                    item.Value = data.Kode;
                    result.Add(item);
                }
            }

            if (faskes2.Response != null)
            {
                foreach (var data in faskes2.Response.Faskes)
                {
                    var item = new RadComboBoxItemData();
                    item.Text = data.Nama;
                    item.Value = data.Kode;
                    result.Add(item);
                }
            }

            return result.ToArray();
        }

        [WebMethod]
        public RadComboBoxItemData[] GetPasien(object context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["filter"]).ToLower();

            var patient = new PatientCollection();
            patient.Query.es.Top = 20;
            patient.Query.Where(
                patient.Query.Or(
                    string.Format("<RTRIM(FirstName+' '+MiddleName)+' '+LastName LIKE '%{0}%' OR >", filter.Split('|')[0]),
                    //patient.Query.MedicalNo.Like("%" + filter.Split('|')[0] + "%")
                    string.Format("<REPLACE(MedicalNo, '-', '') = '{0}'>", filter.Split('|')[0].Replace("-", string.Empty))
                    )
                );
            patient.Query.Load();

            var result = new List<RadComboBoxItemData>(patient.Count);
            foreach (var data in patient)
            {
                var item = new RadComboBoxItemData();
                item.Text = data.MedicalNo + " - " + data.PatientName;
                item.Value = data.MedicalNo + "|" + data.PatientID;
                result.Add(item);
            }
            return result.ToArray();
        }

        [WebMethod]
        public RadComboBoxItemData[] GetPoli(object context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["filter"]).ToLower();

            var poli = filter.Split('|')[0];
            var rujukan = filter.Split('|')[1];
            var jenis = filter.Split('|')[2];

            if (rujukan == "select...") rujukan = string.Empty;

            if (!string.IsNullOrWhiteSpace(rujukan))
            {
                var svc = new Common.BPJS.VClaim.v11.Service();
                var response = svc.GetDataJumlahSEPRujukan(jenis == "1" ? Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1 : Common.BPJS.VClaim.Enum.JenisFaskes.RS, rujukan);
                if (response.MetaData.IsValid && response.Response.JumlahSEP.ToInt() < 1)
                {
                    return new List<RadComboBoxItemData>()
                    {
                        new RadComboBoxItemData()
                        {
                            Text = "Rujukan pertama tidak bisa mengganti poli tujuan",
                            Value = string.Empty
                        }
                    }.ToArray();
                }
            }

            var sub = new ServiceUnitBridgingQuery("a");
            var su = new ServiceUnitQuery("b");

            sub.Select(sub.BridgingID, "<CASE WHEN a.BridgingName = '' THEN b.ServiceUnitName ELSE a.BridgingName END AS ServiceUnitName>");
            sub.InnerJoin(su).On(sub.ServiceUnitID == su.ServiceUnitID && sub.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString()));
            sub.Where(string.Format("<CASE WHEN a.BridgingName = '' THEN b.ServiceUnitName ELSE a.BridgingName END LIKE '%{0}%'>", poli));
            sub.Where(su.IsActive == true);
            var dtb = sub.LoadDataTable();

            var result = new List<RadComboBoxItemData>(dtb.Rows.Count);
            foreach (DataRow data in dtb.Rows)
            {
                var item = new RadComboBoxItemData();
                item.Text = data["BridgingID"].ToString() + " - " + data["ServiceUnitName"].ToString();
                item.Value = data["BridgingID"].ToString();
                result.Add(item);
            }
            return result.ToArray();
        }

        [WebMethod]
        public RadComboBoxItemData[] GetDPJP(object context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["filter"]).ToLower().Split('|');

            var sub = new ParamedicBridgingQuery("a");
            var su = new ParamedicQuery("b");
            //var sup = new ServiceUnitParamedicQuery("c");

            sub.Select(sub.BridgingID, "<CASE WHEN a.BridgingName = '' THEN b.ParamedicName ELSE a.BridgingName END AS ParamedicName>");
            sub.InnerJoin(su).On(sub.ParamedicID == su.ParamedicID && sub.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString()));
            //if (filter[2] == "2")
            //{
            //    var bridging = new ServiceUnitBridging();
            //    bridging.Query.Where(bridging.Query.BridgingID == filter[1]);
            //    bridging.Query.Load();

            //    sub.InnerJoin(sup).On(sub.ParamedicID == sup.ParamedicID && sup.ServiceUnitID == bridging.ServiceUnitID);
            //}
            sub.Where(string.Format("<CASE WHEN a.BridgingName = '' THEN b.ParamedicName ELSE a.BridgingName END LIKE '%{0}%'>", filter[0]));
            sub.Where(su.IsActive == true);
            var dtb = sub.LoadDataTable();

            var result = new List<RadComboBoxItemData>(dtb.Rows.Count);
            foreach (DataRow data in dtb.Rows)
            {
                var item = new RadComboBoxItemData();
                item.Text = (string.IsNullOrEmpty(data["BridgingID"].ToString()) ? string.Empty : data["BridgingID"].ToString() + " - ") + data["ParamedicName"].ToString();
                item.Value = data["BridgingID"].ToString();
                result.Add(item);
            }
            return result.ToArray();
        }

        [WebMethod]
        public RadComboBoxItemData[] GetDPJPByUnitMapping(object context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["filter"]).ToLower().Split('|');

            string format = "yyyy-MM-dd";
            DateTime.TryParseExact(filter[3], format, null, System.Globalization.DateTimeStyles.None, out var date);

            var sub = new ParamedicBridgingQuery("a");
            var su = new ParamedicQuery("b");
            var sup = new ServiceUnitParamedicQuery("c");

            sub.es.Distinct = true;
            sub.Select(sub.BridgingID, "<CASE WHEN a.BridgingName = '' THEN b.ParamedicName ELSE a.BridgingName END AS ParamedicName>", sub.ParamedicID);
            sub.InnerJoin(su).On(sub.ParamedicID == su.ParamedicID && sub.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString()));

            var units = new List<string>();
            if (filter[2] == "2")
            {
                if (!string.IsNullOrWhiteSpace(filter[1]))
                {
                    var bridging = new ServiceUnitBridgingCollection();
                    bridging.Query.Where(bridging.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString()), bridging.Query.BridgingID == filter[1]);
                    if (bridging.Query.Load())
                    {
                        units = bridging.Select(b => b.ServiceUnitID).ToList();

                        //sub.Select($"<'{bridging.BridgingID}' AS ServiceUnitBridging>");
                        sub.InnerJoin(sup).On(sub.ParamedicID == sup.ParamedicID && sup.ServiceUnitID.In(bridging.Select(b => b.ServiceUnitID)));
                    }
                }
            }

            sub.Where(string.Format("<CASE WHEN a.BridgingName = '' THEN b.ParamedicName ELSE a.BridgingName END LIKE '%{0}%'>", filter[0]));
            sub.Where(su.IsActive == true);
            var dtb = sub.LoadDataTable();

            var result = new List<RadComboBoxItemData>(dtb.Rows.Count);
            foreach (DataRow data in dtb.Rows)
            {
                var item = new RadComboBoxItemData();

                if (filter[2] == "2")
                {
                    if (units.Any())
                    {
                        foreach (var unit in units)
                        {
                            var sUnit = new ServiceUnit();
                            sUnit.LoadByPrimaryKey(unit);
                            if (sUnit.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                            {
                                // rehab medik dan hemodialisa exclude
                                //if (new string[] { "IRM", "HDL" }.Contains(data["ServiceUnitBridging"].ToString()))
                                //{
                                if (result.Any(r => r.Value == data["BridgingID"].ToString())) continue;
                                item = new RadComboBoxItemData();
                                item.Text = (string.IsNullOrEmpty(data["BridgingID"].ToString()) ? string.Empty : data["BridgingID"].ToString() + " - ") + data["ParamedicName"].ToString();
                                item.Value = data["BridgingID"].ToString();
                                result.Add(item);

                                //    continue;
                                //}

                                //var sch = new ParamedicScheduleDate();
                                //if (!sch.LoadByPrimaryKey(unit, data["ParamedicID"].ToString(), date.Year.ToString(), date.Date)) continue;
                            }
                        }
                    }
                }

                if (result.Any(r => r.Value == data["BridgingID"].ToString())) continue;
                item = new RadComboBoxItemData();
                item.Text = (string.IsNullOrEmpty(data["BridgingID"].ToString()) ? string.Empty : data["BridgingID"].ToString() + " - ") + data["ParamedicName"].ToString();
                item.Value = data["BridgingID"].ToString();
                result.Add(item);
            }
            return result.ToArray();
        }

        //[WebMethod]
        //public RadComboBoxItemData[] GetDPJP(object context)
        //{
        //    var contextDictionary = (IDictionary<string, object>)context;
        //    var filter = ((string)contextDictionary["filter"]).ToLower();

        //    var svc = new Common.BPJS.VClaim.v11.Service();
        //    var response = svc.GetDokterDpjp(Common.BPJS.VClaim.Enum.JenisPelayanan.Jalan, DateTime.Now.Date, "");
        //    //if (!response.MetaData.IsValid) return null;
        //    var result = new List<RadComboBoxItemData>(response.Response.List.Count());
        //    foreach (var data in response.Response.List)
        //    {
        //        var item = new RadComboBoxItemData();
        //        item.Text = data.Nama;
        //        item.Value = data.Kode;
        //        result.Add(item);
        //    }
        //    return result.ToArray();
        //}

        [WebMethod]
        public RadComboBoxItemData[] GetRegistrasiIGD(object context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["filter"]).ToLower().Split('|');

            var reg = new RegistrationQuery("a");
            var patient = new PatientQuery("b");
            var unit = new ServiceUnitQuery("c");

            reg.es.Top = 10;
            reg.Select(
                reg.RegistrationNo,
                reg.RegistrationDate,
                unit.ServiceUnitName
                );
            reg.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            reg.Where(patient.MedicalNo == filter[1]);
            reg.Where(string.Format("<a.RegistrationNo LIKE '%{0}%'>", filter[0]));
            reg.Where(
                reg.SRRegistrationType.In(AppConstant.RegistrationType.EmergencyPatient, AppConstant.RegistrationType.OutPatient, AppConstant.RegistrationType.InPatient),
                reg.IsVoid == false
                );
            reg.OrderBy(reg.RegistrationDate.Descending);

            var dtb = reg.LoadDataTable();

            reg = new RegistrationQuery("a");
            patient = new PatientQuery("b");
            unit = new ServiceUnitQuery("c");

            reg.es.Top = 10;
            reg.Select(
                reg.RegistrationNo,
                reg.RegistrationDate,
                unit.ServiceUnitName
                );
            reg.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            reg.Where(patient.MedicalNo == filter[1]);
            reg.Where(string.Format("<a.RegistrationNo LIKE '%{0}%'>", filter[0]));
            reg.Where(
                reg.SRRegistrationType.In(AppConstant.RegistrationType.InPatient),
                reg.IsNewBornInfant == true,
                reg.IsVoid == false
                );
            reg.OrderBy(reg.RegistrationDate.Descending);

            dtb.Merge(reg.LoadDataTable());

            var result = new List<RadComboBoxItemData>(dtb.Rows.Count);
            foreach (DataRow data in dtb.Rows)
            {
                var item = new RadComboBoxItemData();
                item.Text = data["RegistrationNo"].ToString() + " - " + Convert.ToDateTime(data["RegistrationDate"]).ToString("dd/MM/yyyy") + " - " + data["ServiceUnitName"].ToString();
                item.Value = data["RegistrationNo"].ToString();
                result.Add(item);
            }
            return result.ToArray();
        }

        [WebMethod]
        public RadComboBoxItemData[] GetPpkDirujuk(object context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["filter"]).ToLower();

            var count = 0;

            var svc = new Common.BPJS.VClaim.v11.Service();
            var faskes2 = svc.GetFaskes(filter, Common.BPJS.VClaim.Enum.JenisFaskes.RS);
            if (faskes2.MetaData.IsValid)
                count += faskes2.Response.Faskes.Count();

            svc = new Common.BPJS.VClaim.v11.Service();
            var faskes1 = svc.GetFaskes(filter, Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);
            if (faskes1.MetaData.IsValid)
                count += faskes1.Response.Faskes.Count();

            if (count == 0)
            {
                return new List<RadComboBoxItemData>()
                {
                    new RadComboBoxItemData()
                    {
                        Text = faskes1.MetaData.Code + " - " + faskes1.MetaData.Message,
                        Value = string.Empty
                    }
                }.ToArray();
            }

            var result = new List<RadComboBoxItemData>(count);

            if (faskes2.MetaData.IsValid)
                foreach (var data in faskes2.Response.Faskes)
                {
                    var item = new RadComboBoxItemData();
                    item.Text = data.Nama;
                    item.Value = data.Kode;
                    result.Add(item);
                }

            if (faskes1.MetaData.IsValid)
                foreach (var data in faskes1.Response.Faskes)
                {
                    var item = new RadComboBoxItemData();
                    item.Text = data.Nama;
                    item.Value = data.Kode;
                    result.Add(item);
                }

            return result.ToArray();
        }

        [WebMethod]
        public RadComboBoxItemData[] GetPoliDirujuk(object context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["filter"]).ToLower();

            var svc = new Common.BPJS.VClaim.v11.Service();
            var faskes2 = svc.GetPoli(filter);

            var result = new List<RadComboBoxItemData>(faskes2.Response.Poli.Count());
            foreach (var data in faskes2.Response.Poli)
            {
                var item = new RadComboBoxItemData();
                item.Text = data.Kode + " - " + data.Nama;
                item.Value = data.Kode;
                result.Add(item);
            }
            return result.ToArray();
        }

        [WebMethod]
        // jenisRujukan = "1" -> Faskes 1
        // jenisRujukan = "2" -> Faskes 2 (RS)
        public string GetDetailByNoRujukan(string nomorRujukan, string jenisRujukan)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetRujukan(true, nomorRujukan, jenisRujukan == "1" ? Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1 : Common.BPJS.VClaim.Enum.JenisFaskes.RS);
            return JsonConvert.SerializeObject(response);
        }

        public RadComboBoxItemData[] GetPoliKontrol(object context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["filter"]).ToLower();

            var svc = new Common.BPJS.VClaim.v11.Service();
            var faskes2 = svc.GetPoli(filter);

            var result = new List<RadComboBoxItemData>(faskes2.Response.Poli.Count());
            foreach (var data in faskes2.Response.Poli)
            {
                var item = new RadComboBoxItemData();
                item.Text = data.Kode + " - " + data.Nama;
                item.Value = data.Kode;
                result.Add(item);
            }
            return result.ToArray();
        }

        [WebMethod]
        public string UpdateDiagnose()
        {
            var diagnoses = new BusinessObject.DiagnoseCollection();
            diagnoses.LoadAll();

            foreach (var diagnose in diagnoses)
            {
                var svc = new Common.BPJS.VClaim.v11.Service();
                var response = svc.GetDiagnosa(diagnose.DiagnoseID);
                if (response.MetaData.IsValid)
                {
                    foreach (var diag in response.Response.Diagnosa)
                    {
                        diagnose.DiagnoseID = diag.Kode;
                        diagnose.DiagnoseName = diag.Nama;
                    }
                }
            }

            diagnoses.Save();

            return "ok";
        }

        [WebMethod]
        public RadComboBoxItemData[] GetObatPRB(object context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["filter"]).ToLower();

            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetObatPrb(filter);
            if (!response.MetaData.IsValid)
            {
                return new List<RadComboBoxItemData>()
                {
                    new RadComboBoxItemData()
                    {
                        Text = response.MetaData.Code + " - " + response.MetaData.Message,
                        Value = string.Empty
                    }
                }.ToArray();
            }
            var result = new List<RadComboBoxItemData>(response.Response.List.Count());
            foreach (var data in response.Response.List)
            {
                var item = new RadComboBoxItemData();
                item.Text = data.Nama;
                item.Value = data.Kode;
                result.Add(item);
            }
            return result.ToArray();
        }


        #region helper idrg
        private enum CodeSystem { Icd10, Icd9Proc }

        private static string GuessDotted(string input, CodeSystem system)
        {
            var t = NormalizeCode(input);
            if (string.IsNullOrEmpty(t)) return string.Empty;

            switch (system)
            {
                case CodeSystem.Icd10:
                    if (t.Length <= 3) return t; 
                    var head10 = t.Substring(0, 3);
                    var tail10 = t.Substring(3);
                    if (tail10.Length > 4) tail10 = tail10.Substring(0, 4);
                    return head10 + "." + tail10;

                case CodeSystem.Icd9Proc:
                    if (t.Length <= 2) return t;
                    var head9 = t.Substring(0, 2);
                    var tail9 = t.Substring(2);
                    if (tail9.Length > 4) tail9 = tail9.Substring(0, 4);
                    return head9 + "." + tail9;
            }
            return t;
        }

        private static void ApplySmartWhere(dynamic q, dynamic idField, dynamic nameField, string filter, CodeSystem system)
        {
            if (string.IsNullOrWhiteSpace(filter)) return;

            var fLike = "%" + filter + "%";
            var dotted = GuessDotted(filter, system);
            if (!string.IsNullOrEmpty(dotted))
            {
                var dLike = "%" + dotted + "%";
                q.Where(nameField.Like(fLike) || idField.Like(fLike) || idField.Like(dLike));
            }
            else
            {
                q.Where(nameField.Like(fLike) || idField.Like(fLike));
            }
        }

        private static string GetFilter(object context)
        {
            if (context is IDictionary<string, object> ctx &&
                ctx.TryGetValue("filter", out var f) &&
                f is string s)
            {
                return (s ?? string.Empty).Trim();
            }
            return string.Empty;
        }

        private static string NormalizeCode(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            return Regex.Replace(s, "[^A-Za-z0-9]", "").ToUpperInvariant();
        }

        private static bool MatchNameOrCode(string name, string code, string rawFilter)
        {
            if (string.IsNullOrEmpty(rawFilter)) return true;

            if (!string.IsNullOrEmpty(name) &&
                name.IndexOf(rawFilter, StringComparison.OrdinalIgnoreCase) >= 0)
                return true;

            // by code (normalize)
            var normCode = NormalizeCode(code);
            var normFilter = NormalizeCode(rawFilter);

            return !string.IsNullOrEmpty(normCode) &&
                   !string.IsNullOrEmpty(normFilter) &&
                   normCode.Contains(normFilter);
        }
        #endregion

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public RadComboBoxItemData[] GetDiagnosaIdrg(object context)
        {
            try
            {
                var filter = GetFilter(context);
                var coll = new DiagnoseCollection();

                coll.Query.es.Top = 300;
                ApplySmartWhere(coll.Query, coll.Query.DiagnoseID, coll.Query.DiagnoseName, filter, CodeSystem.Icd10);
                coll.Query.OrderBy(coll.Query.DiagnoseName.Ascending);
                coll.Query.Load();

                var items = coll
                    .Where(d => MatchNameOrCode(d.DiagnoseName, d.DiagnoseID, filter))
                    .Take(50)
                    .Select(d =>
                    {
                        bool isValid = d.ValidCode.GetValueOrDefault(false);
                        bool canPrimary = d.Accpdx.GetValueOrDefault(true);
                        return new RadComboBoxItemData
                        {
                            Text = $"{d.DiagnoseName} [{d.DiagnoseID}]{(isValid ? "" : " (invalid)")}{(canPrimary ? "" : " (no-primary)")}",
                            Value = d.DiagnoseID,
                            Attributes = new Dictionary<string, object>
                            {
                        { "VC",  isValid    ? "1" : "0" },
                        { "ACC", canPrimary ? "1" : "0" }
                            }
                        };
                    })
                    .ToList();

                if (items.Count == 0)
                    items.Add(new RadComboBoxItemData { Text = "Tidak ada data", Value = string.Empty });

                return items.ToArray();
            }
            catch (Exception ex)
            {
                return new[] { new RadComboBoxItemData { Text = "Error: " + ex.Message, Value = string.Empty } };
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public RadComboBoxItemData[] GetProcedureIdrg(object context)
        {
            try
            {
                var filter = GetFilter(context);
                var coll = new ProcedureCollection();

                coll.Query.es.Top = 300;
                ApplySmartWhere(coll.Query, coll.Query.ProcedureID, coll.Query.ProcedureName, filter, CodeSystem.Icd9Proc);
                coll.Query.OrderBy(coll.Query.ProcedureName.Ascending);
                coll.Query.Load();

                var items = coll
                    .Where(p => MatchNameOrCode(p.ProcedureName, p.ProcedureID, filter))
                    .Take(50)
                    .Select(p =>
                    {
                        bool isValid = false;
                        var prop = p.GetType().GetProperty("ValidCode");
                        if (prop != null) isValid = Convert.ToBoolean(prop.GetValue(p, null) ?? false);

                        return new RadComboBoxItemData
                        {
                            Text = $"{p.ProcedureName} [{p.ProcedureID}]{(isValid ? "" : " (invalid)")}",
                            Value = p.ProcedureID,
                            Attributes = new Dictionary<string, object> { { "VC", isValid ? "1" : "0" } }
                        };
                    })
                    .ToList();

                if (items.Count == 0)
                    items.Add(new RadComboBoxItemData { Text = "Tidak ada data", Value = string.Empty });

                return items.ToArray();
            }
            catch (Exception ex)
            {
                return new[] { new RadComboBoxItemData { Text = "Error: " + ex.Message, Value = string.Empty } };
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public RadComboBoxItemData[] GetDiagnosaIna(object context)
        {
            try
            {
                var filter = GetFilter(context);
                var coll = new DiagnoseCollection();

                coll.Query.es.Top = 300;
                ApplySmartWhere(coll.Query, coll.Query.DiagnoseID, coll.Query.DiagnoseName, filter, CodeSystem.Icd10);
                coll.Query.OrderBy(coll.Query.DiagnoseName.Ascending);
                coll.Query.Load();

                var items = coll
                    .Where(d => MatchNameOrCode(d.DiagnoseName, d.DiagnoseID, filter))
                    .Take(50)
                    .Select(d =>
                    {
                        bool isIM = false;
                        var pIM = d.GetType().GetProperty("IM");
                        if (pIM != null) isIM = Convert.ToBoolean(pIM.GetValue(d, null) ?? false);

                        bool canPrimary = true;
                        var pAcc = d.GetType().GetProperty("Accpdx");
                        if (pAcc != null) canPrimary = Convert.ToBoolean(pAcc.GetValue(d, null) ?? false);

                        return new RadComboBoxItemData
                        {
                            Text = $"{d.DiagnoseName}{(isIM ? " (IM)" : "")} [{d.DiagnoseID}]{(canPrimary ? "" : " (no-primary)")}",
                            Value = d.DiagnoseID,
                            Attributes = new Dictionary<string, object>
                            {
                        { "IM",  isIM       ? "1" : "0" },
                        { "ACC", canPrimary ? "1" : "0" }
                            }
                        };
                    })
                    .ToList();

                if (items.Count == 0)
                    items.Add(new RadComboBoxItemData { Text = "Tidak ada data", Value = string.Empty });

                return items.ToArray();
            }
            catch (Exception ex)
            {
                return new[] { new RadComboBoxItemData { Text = "Error: " + ex.Message, Value = string.Empty } };
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public RadComboBoxItemData[] GetProcedureIna(object context)
        {
            try
            {
                var filter = GetFilter(context);
                var coll = new ProcedureCollection();

                coll.Query.es.Top = 300;
                ApplySmartWhere(coll.Query, coll.Query.ProcedureID, coll.Query.ProcedureName, filter, CodeSystem.Icd9Proc);
                coll.Query.OrderBy(coll.Query.ProcedureName.Ascending);
                coll.Query.Load();

                var items = coll
                    .Where(p => MatchNameOrCode(p.ProcedureName, p.ProcedureID, filter))
                    .Take(50)
                    .Select(p =>
                    {
                        bool isIM = false;
                        var prop = p.GetType().GetProperty("IM");
                        if (prop != null) isIM = Convert.ToBoolean(prop.GetValue(p, null) ?? false);

                        return new RadComboBoxItemData
                        {
                            Text = $"{p.ProcedureName}{(isIM ? " (IM)" : "")} [{p.ProcedureID}]",
                            Value = p.ProcedureID,
                            Attributes = new Dictionary<string, object> { { "IM", isIM ? "1" : "0" } }
                        };
                    })
                    .ToList();

                if (items.Count == 0)
                    items.Add(new RadComboBoxItemData { Text = "Tidak ada data", Value = string.Empty });

                return items.ToArray();
            }
            catch (Exception ex)
            {
                return new[] { new RadComboBoxItemData { Text = "Error: " + ex.Message, Value = string.Empty } };
            }
        }

        [WebMethod]
        public RadComboBoxItemData[] GetDiagnosaEklaim(object context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["filter"]).ToLower();

            var svc = new Common.Inacbg.v51.Service();
            var diag = svc.Search(new Common.Inacbg.v51.Procedure.Search.Data() { keyword = filter }, true);
            if (!diag.Metadata.IsValid)
            {
                return new List<RadComboBoxItemData>()
                    {
                        new RadComboBoxItemData()
                        {
                            Text = diag.Metadata.Code + " - " + diag.Metadata.Message,
                            Value = string.Empty
                        }
                    }.ToArray();
            }
            else
            {
                var result = new List<RadComboBoxItemData>(diag.Response.Count);
                foreach (var data in diag.Response.Data)
                {
                    var icd = data;
                    string namaDiagnosa = string.Format("{0}-{1}", icd[1], icd[0]);

                    var item = new RadComboBoxItemData
                    {
                        Text = namaDiagnosa,
                        Value = icd[1]
                    };
                    result.Add(item);
                }
                return result.ToArray();
            }
        }

        [WebMethod]
        public RadComboBoxItemData[] GetProcedureEklaim(object context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["filter"]).ToLower();

            var svc = new Common.Inacbg.v51.Service();
            var diag = svc.Search(new Common.Inacbg.v51.Procedure.Search.Data() { keyword = filter }, false);
            if (!diag.Metadata.IsValid)
            {
                return new List<RadComboBoxItemData>()
                    {
                        new RadComboBoxItemData()
                        {
                            Text = diag.Metadata.Code + " - " + diag.Metadata.Message,
                            Value = string.Empty
                        }
                    }.ToArray();
            }
            else
            {
                var result = new List<RadComboBoxItemData>(diag.Response.Count);
                foreach (var data in diag.Response.Data)
                {
                    var icd = data;
                    string namaDiagnosa = string.Format("{0}-{1}", icd[1], icd[0]);

                    var item = new RadComboBoxItemData
                    {
                        Text = namaDiagnosa,
                        Value = icd[1]
                    };
                    result.Add(item);
                }
                return result.ToArray();
            }
        }

        //[WebMethod(EnableSession = true)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public void UploadBatchAntrol()
        //{
        //    var reg = new RegistrationQuery("a");
        //    var unit = new ServiceUnitQuery("b");
        //    var medic = new ParamedicQuery("c");

        //    reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
        //    reg.InnerJoin(medic).On(reg.ParamedicID == medic.ParamedicID);
        //    reg.Where(reg.RegistrationDate.Date() == DateTime.Now.Date, reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient.ToString(),
        //        reg.GuarantorID.In(AppSession.Parameter.GuarantorAskesID), reg.IsVoid == false, reg.IsConsul == false, reg.IsNonPatient == false, reg.IsFromDispensary == false);
        //}

        [WebMethod(EnableSession = true)]
        public string GenerateSkdp(string appointmentNo)
        {
            var table = string.IsNullOrWhiteSpace(appointmentNo) ? new BusinessObject.AppointmentCollection().BpjsOpenAppointment(AppEnum.BridgingType.BPJS.ToString(), AppSession.Parameter.GuarantorAskesID[0], AppSession.Parameter.AppointmentStatusOpen, DateTime.Now.Date) :
                new BusinessObject.AppointmentCollection().BpjsOpenAppointment(AppEnum.BridgingType.BPJS.ToString(), AppSession.Parameter.GuarantorAskesID[0], AppSession.Parameter.AppointmentStatusOpen, appointmentNo);
            foreach (DataRow row in table.Rows)
            {
                var svc = new Common.BPJS.VClaim.v11.Service();
                var param = new Common.BPJS.VClaim.v11.RencanaKontrol.Insert.Request.Root()
                {
                    Request = new Common.BPJS.VClaim.v11.RencanaKontrol.Insert.Request.TRequest()
                    {
                        NoSEP = row["NoSEP"].ToString(),
                        KodeDokter = row["pb"].ToString(),
                        PoliKontrol = row["sub"].ToString(),
                        TglRencanaKontrol = Convert.ToDateTime(row["AppointmentDate"]).ToString("yyyyMMdd"),
                        User = row["LastCreateByUserID"].ToString()
                    }
                };
                var response = svc.Insert(param);

                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = string.Empty,
                    UrlAddress = "RencanaKontrolService",
                    Params = JsonConvert.SerializeObject(param),
                    Response = JsonConvert.SerializeObject(response),
                    Totalms = 0
                };
                log.Save();

                if (!response.MetaData.IsValid) continue;

                var entity = new BusinessObject.Appointment();
                entity.LoadByPrimaryKey(row["AppointmentNo"].ToString());
                entity.ReferenceNumber = response.Response.NoSuratKontrol;
                entity.Save();

                // antrol
                var patient = new Patient();
                patient.LoadByPrimaryKey(entity.PatientID);

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(entity.ServiceUnitID);

                var sub = new ServiceUnitBridging();
                sub.Query.Where(sub.Query.ServiceUnitID == entity.ServiceUnitID &&
                                sub.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                sub.Query.Load();

                var exclude = new[] { "HDL", "IRM" };
                if (!exclude.Contains(sub.BridgingID.Split(';')[0]))
                {
                    var p = new Paramedic();
                    p.LoadByPrimaryKey(entity.ParamedicID);

                    var pb = new ParamedicBridging();
                    pb.Query.Where(pb.Query.ParamedicID == entity.ParamedicID &&
                                   pb.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                    pb.Query.Load();

                    var ps = new ParamedicSchedule();
                    ps.LoadByPrimaryKey(entity.ServiceUnitID, entity.ParamedicID,
                        entity.AppointmentDate.Value.Year.ToString());

                    var psd = new ParamedicScheduleDate();
                    psd.LoadByPrimaryKey(entity.ServiceUnitID, entity.ParamedicID,
                        entity.AppointmentDate.Value.Year.ToString(), entity.AppointmentDate.Value.Date);

                    var ot = new OperationalTime();
                    ot.LoadByPrimaryKey(psd.OperationalTimeID);

                    var jam = TimeSpan.ParseExact(entity.AppointmentTime, "hh\\:mm", null);
                    string waktu = string.Empty;

                    if (!string.IsNullOrWhiteSpace(ot.StartTime1) && !string.IsNullOrWhiteSpace(ot.EndTime1))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime1, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime1, "hh\\:mm", null);

                        if (jam >= ot1 && jam <= ot2)
                        {
                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime2, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime2, "hh\\:mm", null);

                        if (jam >= ot1 && jam <= ot2)
                        {
                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime3, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime3, "hh\\:mm", null);

                        if (jam >= ot1 && jam <= ot2)
                        {
                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime4, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime4, "hh\\:mm", null);

                        if (jam >= ot1 && jam <= ot2)
                        {
                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime5, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime5, "hh\\:mm", null);

                        if (jam >= ot1 && jam <= ot2)
                        {
                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                        }
                    }

                    var antreanDateTime = Convert.ToDateTime(entity.AppointmentDate.Value.ToString("yyyy-MM-dd") + ' ' +
                                                             entity.AppointmentTime + ":00");

                    var jam2 = waktu.Split('-');

                    var appt = new BusinessObject.AppointmentCollection();
                    appt.Query.Where(appt.Query.ServiceUnitID == entity.ServiceUnitID,
                        appt.Query.ParamedicID == entity.ParamedicID,
                        appt.Query.AppointmentDate.Date() == entity.AppointmentDate.Value.Date,
                        appt.Query.AppointmentTime >= jam2[0].Trim(),
                        appt.Query.AppointmentTime <= jam2[1].Trim(),
                        appt.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                    );
                    var apptAvailable = appt.Query.Load();

                    var antrol = new Common.BPJS.Antrian.Tambah.Request.Root()
                    {
                        Kodebooking = entity.AppointmentNo,
                        Jenispasien = AppSession.Parameter.GuarantorAskesID.Contains(entity.GuarantorID)
                            ? "JKN"
                            : "NON JKN",
                        Nomorkartu = entity.GuarantorCardNo,
                        Nik = entity.Ssn,
                        Nohp = string.IsNullOrWhiteSpace(entity.MobilePhoneNo) ? entity.PhoneNo : entity.MobilePhoneNo,
                        Kodepoli = sub.BridgingID.Split(';')[1],
                        Namapoli = su.ServiceUnitName,
                        Pasienbaru = 0,
                        Norm = patient.MedicalNo,
                        Tanggalperiksa = entity.AppointmentDate.Value.Date.ToString("yyyy-MM-dd"),
                        Kodedokter = pb.BridgingID.ToInt(),
                        Namadokter = p.ParamedicName,
                        Jampraktek = waktu,
                        Jeniskunjungan = 3,
                        Nomorreferensi = entity.ReferenceNumber,
                        Nomorantrean = $"{su.ShortName}{p.ParamedicInitial} - {(entity.AppointmentQue ?? 1)}",
                        Angkaantrean = entity.AppointmentQue ?? 1,
                        Estimasidilayani = Convert.ToInt64(antreanDateTime.AddHours(-7)
                            .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds),
                        Sisakuotajkn = (ps.QuotaBpjsOnline ?? 0) -
                                       appt.Count(a => a.GuarantorID == AppSession.Parameter.GuarantorAskesID[0]),
                        Kuotajkn = ps.QuotaBpjsOnline ?? 0,
                        Sisakuotanonjkn = (ps.QuotaOnline ?? 0) -
                                          appt.Count(a => a.GuarantorID != AppSession.Parameter.GuarantorAskesID[0]),
                        Kuotanonjkn = ps.QuotaOnline ?? 0,
                        Keterangan = "Peserta harap 30 menit lebih awal guna pencatatan administrasi"
                    };

                    var svc2 = new Common.BPJS.Antrian.Service();
                    var response2 = svc2.TambahAntrian(antrol);

                    var log2 = new WebServiceAPILog
                    {
                        DateRequest = DateTime.Now,
                        IPAddress = string.Empty,
                        UrlAddress = "RencanaKontrolService",
                        Params = JsonConvert.SerializeObject(antrol),
                        Response = JsonConvert.SerializeObject(response2),
                        Totalms = 0
                    };
                    log2.Save();
                }
            }

            return "success";
        }

        [WebMethod]
        public RadComboBoxItemData[] GetDiagnosaInaGroupperEklaim(object context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["filter"]).ToLower();

            var svc = new Common.Inacbg.v59.Service();
            var diag = svc.SearchDiagnoseInaGroupper(new Common.Inacbg.v51.Procedure.Search.Data() { keyword = filter });
            if (!diag.Metadata.IsValid)
            {
                return new List<RadComboBoxItemData>()
                    {
                        new RadComboBoxItemData()
                        {
                            Text = diag.Metadata.Code + " - " + diag.Metadata.Message,
                            Value = string.Empty
                        }
                    }.ToArray();
            }
            else
            {
                var result = new List<RadComboBoxItemData>(diag.Response.Count ?? 0);
                foreach (var data in diag.Response.Data)
                {
                    var icd = data;
                    string namaDiagnosa = string.Format("{0}-{1}", icd.Code, icd.Description);

                    var item = new RadComboBoxItemData
                    {
                        Text = namaDiagnosa,
                        Value = icd.Code
                    };
                    result.Add(item);
                }
                return result.ToArray();
            }
        }

        [WebMethod]
        public RadComboBoxItemData[] GetProcedureInaGroupperEklaim(object context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["filter"]).ToLower();

            var svc = new Common.Inacbg.v59.Service();
            var diag = svc.SearchProsedurInaGroupper(new Common.Inacbg.v51.Procedure.Search.Data() { keyword = filter });
            if (!diag.Metadata.IsValid)
            {
                return new List<RadComboBoxItemData>()
                    {
                        new RadComboBoxItemData()
                        {
                            Text = diag.Metadata.Code + " - " + diag.Metadata.Message,
                            Value = string.Empty
                        }
                    }.ToArray();
            }
            else
            {
                var result = new List<RadComboBoxItemData>(diag.Response.Count ?? 0);
                foreach (var data in diag.Response.Data)
                {
                    var icd = data;
                    string namaDiagnosa = string.Format("{0}-{1}", icd.Code, icd.Description);

                    var item = new RadComboBoxItemData
                    {
                        Text = namaDiagnosa,
                        Value = icd.Code
                    };
                    result.Add(item);
                }
                return result.ToArray();
            }
        }
    }
}

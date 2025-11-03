using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for Siranap
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Siranap : System.Web.Services.WebService
    {
        //[System.Web.Services.WebMethod]
        //public string Execute()
        //{
        //    var sb = new StringBuilder();
        //    esUtility es;
        //    DataTable dtb;

        //    //delete
        //    es = new esUtility();
        //    dtb = es.FillDataTable("dbo", "sp_siranap_id_monintoring");
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        var svc = new Temiang.Avicenna.Common.Siranap.Service();
        //        var response = svc.BedMonitoringDelete(row);
        //        if (response == null) continue;
        //        if (response.response != "1") sb.AppendLine("DELETE " + response.deskripsi);
        //    }

        //    //insert
        //    es = new esUtility();
        //    dtb = es.FillDataTable("dbo", "sp_siranap_bed_monintoring");
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        var svc = new Temiang.Avicenna.Common.Siranap.Service();
        //        var response = svc.BedMonitoringNew(row);
        //        if (response.response != "1") sb.AppendLine("INSERT " + response.deskripsi);
        //    }

        //    return sb.Length == 0 ? "success" : sb.ToString();
        //}

        //[System.Web.Services.WebMethod]
        //public string ExecuteString(string text)
        //{
        //    return text;
        //}

        //[System.Web.Services.WebMethod]
        //public string HelloWorld()
        //{
        //    return "HelloWorld";
        //}

        [System.Web.Services.WebMethod]
        public string InsertSiranapV21()
        {
            var table = new ServiceUnitCollection().SiranapV21();

            var json = table.AsEnumerable().Select(t => new Root
            {
                IdTt = t.Field<int>("id_tt").ToString(),
                Ruang = t.Field<string>("ruang").ToString(),
                JumlahRuang = t.Field<int>("jumlah_ruang").ToString(),
                Jumlah = t.Field<int>("jumlah").ToString(),
                Terpakai = t.Field<int>("terpakai").ToString(),
                TerpakaiSuspek = t.Field<int>("terpakai_suspek").ToString(),
                TerpakaiKonfirmasi = t.Field<int>("terpakai_konfirmasi").ToString(),
                Antrian = t.Field<int>("antrian").ToString(),
                Prepare = t.Field<int>("prepare").ToString(),
                PreparePlan = t.Field<int>("prepare_plan").ToString(),
                Covid = t.Field<int>("covid").ToString()
            }).ToList();

            var respose = string.Empty;

            foreach(var item in json)
            {
                var svc = new Common.Siranap.Service();
                respose += $"tt : {item.IdTt},{item.Ruang},{svc.SendV21(true, JsonConvert.SerializeObject(item))}, ";
            }

            return respose;
        }

        [System.Web.Services.WebMethod]
        public string UpdateSiranapV21()
        {
            var table = new ServiceUnitCollection().SiranapV21();

            var json = table.AsEnumerable().Select(t => new Root
            {
                IdTt = t.Field<int>("id_tt").ToString(),
                Ruang = t.Field<string>("ruang").ToString(),
                JumlahRuang = t.Field<int>("jumlah_ruang").ToString(),
                Jumlah = t.Field<int>("jumlah").ToString(),
                Terpakai = t.Field<int>("terpakai").ToString(),
                TerpakaiSuspek = t.Field<int>("terpakai_suspek").ToString(),
                TerpakaiKonfirmasi = t.Field<int>("terpakai_konfirmasi").ToString(),
                Antrian = t.Field<int>("antrian").ToString(),
                Prepare = t.Field<int>("prepare").ToString(),
                PreparePlan = t.Field<int>("prepare_plan").ToString(),
                Covid = t.Field<int>("covid").ToString()
            }).ToList();

            var respose = string.Empty;

            foreach (var item in json)
            {
                var svc = new Common.Siranap.Service();
                respose += $"tt : {item.IdTt},{item.Ruang},{svc.SendV21(false, JsonConvert.SerializeObject(item))}, ";
            }

            return respose;
        }

        [System.Web.Services.WebMethod]
        public string GetReferensiKelas()
        {
            var svc = new Common.Siranap.Service();
            return svc.ReferensiKelas();
        }

        [System.Web.Services.WebMethod]
        public string DeleteSiranapV21_idttt()
        {
            var svc = new Common.Siranap.Service();
            var list = svc.SelectV21();
            foreach (var item in list.Fasyankes)
            {
                if (string.IsNullOrWhiteSpace(item.IdTTt)) continue;
                svc = new Common.Siranap.Service();
                var response = svc.DeleteV21(new Common.Siranap.Json.Delete.Root()
                {
                    IdTTt = item.IdTTt
                });
            }
            return SelectSiranapV21();
        }

        [System.Web.Services.WebMethod]
        public string DeleteSiranapV21_idtt()
        {
            var svc = new Common.Siranap.Service();
            var list = svc.SelectV21();
            foreach (var item in list.Fasyankes)
            {
                if (string.IsNullOrWhiteSpace(item.IdTTt)) continue;
                svc = new Common.Siranap.Service();
                var response = svc.DeleteV21(new Common.Siranap.Json.Delete.Root()
                {
                    IdTt = item.IdTt
                });
            }
            return SelectSiranapV21();
        }

        [System.Web.Services.WebMethod]
        public string SelectSiranapV21()
        {
            var svc = new Common.Siranap.Service();
            return JsonConvert.SerializeObject(svc.SelectV21());
        }

        public class Root
        {
            [JsonProperty("id_tt")]
            public string IdTt;

            [JsonProperty("ruang")]
            public string Ruang;

            [JsonProperty("jumlah_ruang")]
            public string JumlahRuang;

            [JsonProperty("jumlah")]
            public string Jumlah;

            [JsonProperty("terpakai")]
            public string Terpakai;

            [JsonProperty("terpakai_suspek")]
            public string TerpakaiSuspek;

            [JsonProperty("terpakai_konfirmasi")]
            public string TerpakaiKonfirmasi;

            [JsonProperty("antrian")]
            public string Antrian;

            [JsonProperty("prepare")]
            public string Prepare;

            [JsonProperty("prepare_plan")]
            public string PreparePlan;

            [JsonProperty("covid")]
            public string Covid;
        }
    }
}
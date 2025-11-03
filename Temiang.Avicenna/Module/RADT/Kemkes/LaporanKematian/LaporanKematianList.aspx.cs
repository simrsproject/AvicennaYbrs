using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Kemkes
{
    public partial class LaporanKematianList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "LaporanKematianSearch.aspx";
            UrlPageDetail = "LaporanKematianDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.KemenkesLaporanKematian;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var svc = new Common.SirsKemkes.Service();
                var login = svc.PostLogin();

                ViewState["PenyebabKematianLangsung"] = null;
                svc = new Common.SirsKemkes.Service();
                var penyebab = svc.GetPenyebabKematianLangsung(login.Data.AccessToken, 1, 1000);
                if (penyebab.Data.Any()) ViewState["PenyebabKematianLangsung"] = penyebab.Data;

                ViewState["Komorbid"] = null;
                svc = new Common.SirsKemkes.Service();
                var komorbid = svc.GetKomorbid(login.Data.AccessToken, 1, 1000);
                if (komorbid.Data.Any()) ViewState["Komorbid"] = komorbid.Data;
            }
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue("Id").ToString();
            Page.Response.Redirect("LaporanKematianDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var svc = new Common.SirsKemkes.Service();
            var login = svc.PostLogin();

            svc = new Common.SirsKemkes.Service();
            var select = svc.GetSelect(login.Data.AccessToken, 1, 1000);

            var list = new List<Data>();
            foreach (var row in select.Data)
            {
                //var wsial = new WebServiceAPILog();
                //wsial.Query.Where(wsial.Query.IPAddress == "C" && wsial.Query.UrlAddress == ConfigurationManager.AppSettings["SirsKemkesLaporanKematianServiceUrlLocation"] && wsial.Query.Totalms == row.Id);
                //if (!wsial.Query.Load()) return;

                list.Add(new Data()
                {
                    Id = row.Id,
                    Nik = row.Nik,
                    TanggalMasuk = row.TanggalMasuk,
                    SaturasiOksigen = row.SaturasiOksigen,
                    TanggalKematian = row.TanggalKematian,
                    LokasiKematian = row.LokasiKematian,
                    PenyebabKematianLangsungId = (ViewState["PenyebabKematianLangsung"] as List<Common.SirsKemkes.PenyebabKematianLangsung.Json.Datum>).Single(s => s.Id == row.PenyebabKematianLangsungId).Description,
                    KasusKematian = row.KasusKematian,
                    StatusKomorbid = row.StatusKomorbid,
                    Komorbid = $"{GetKomorbid(1, row.Komorbid1)}, {GetKomorbid(2, row.Komorbid2)}, {GetKomorbid(3, row.Komorbid3)}, {GetKomorbid(4, row.Komorbid4)}",
                    StatusKehamilan = row.StatusKehamilan
                });
            }
            grdList.DataSource = list;
        }

        protected void grdList_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id"]);
            var svc = new Common.SirsKemkes.Service();
            var login = svc.PostLogin();

            svc = new Common.SirsKemkes.Service();
            var response = svc.PostDelete(login.Data.AccessToken, id);

            var wsal = new WebServiceAPILog()
            {
                DateRequest = DateTime.Now,
                IPAddress = "D",
                UrlAddress = ConfigurationManager.AppSettings["SirsKemkesLaporanKematianServiceUrlLocation"],
                Params = id.ToString(),
                Response = JsonConvert.SerializeObject(response),
                Totalms = id
            };
            wsal.Save();
        }

        private string GetKomorbid(int no, string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return id;
            var komorbid = (ViewState["Komorbid"] as List<Common.SirsKemkes.Komorbid.Json.Datum>).Single(s => s.Id == id);
            return $"{no}. {komorbid.Id}-{komorbid.Description}";
        }
    }

    public class Data
    {
        public int Id { get; set; }

        public string Nik { get; set; }

        public string TanggalMasuk { get; set; }

        public int SaturasiOksigen { get; set; }

        public string TanggalKematian { get; set; }

        public string LokasiKematian { get; set; }

        public string PenyebabKematianLangsungId { get; set; }

        public string KasusKematian { get; set; }

        public string StatusKomorbid { get; set; }

        public string Komorbid { get; set; }

        public string Komorbid1 { get; set; }

        public object Komorbid2 { get; set; }

        public object Komorbid3 { get; set; }

        public object Komorbid4 { get; set; }

        public string StatusKehamilan { get; set; }
    }
}
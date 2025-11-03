using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class RencanaKontrolList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "RencanaKontrolSearch.aspx";
            UrlPageDetail = "RencanaKontrolDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.BpjsRencanaKontrol;
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
            string id = dataItem.GetDataKeyValue("NoSuratKontrol").ToString();
            string nopeserta = dataItem.GetDataKeyValue("NoKartu").ToString();
            string namapeserta = dataItem.GetDataKeyValue("Nama").ToString();
            Page.Response.Redirect("RencanaKontrolDetail.aspx?md=" + mode + "&id=" + id + "&nopeserta=" + nopeserta + "&namapeserta=" + namapeserta, true);
        }

        private DataTable GridDataSource(List<Common.BPJS.VClaim.v11.RencanaKontrol.Select.ResponseSuratKontrolList.List> lists)
        {
            var dtb = new DataTable();

            dtb.Columns.Add("NoSuratKontrol", typeof(string));
            dtb.Columns.Add("TglRencanaKontrol", typeof(string));
            dtb.Columns.Add("TglTerbitKontrol", typeof(string));
            dtb.Columns.Add("NamaPoliTujuan", typeof(string));
            dtb.Columns.Add("NoSepAsalKontrol", typeof(string));
            dtb.Columns.Add("TglSep", typeof(string));
            dtb.Columns.Add("NoKartu", typeof(string));
            dtb.Columns.Add("Nama", typeof(string));
            dtb.Columns.Add("NamaDokter", typeof(string));
            dtb.Columns.Add("JenisSuratKontrol", typeof(string));

            foreach (var list in lists)
            {
                var row = dtb.NewRow();
                row[0] = list.NoSuratKontrol;
                row[1] = list.TglRencanaKontrol;
                row[2] = list.TglTerbitKontrol;
                row[3] = list.NamaPoliTujuan;
                row[4] = list.NoSepAsalKontrol;
                row[5] = list.TglSEP;
                row[6] = list.NoKartu;
                row[7] = list.Nama;
                row[8] = list.NamaDokter;
                row[9] = list.JnsKontrol == "1" ? "Rawat Inap" : "Rawat Jalan";
                dtb.Rows.Add(row);
            }

            dtb.AcceptChanges();

            return dtb;
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var noSuratKontrol = string.Empty;
            var tglAwal = DateTime.Now;
            var tglAkhir = DateTime.Now;
            var noPeserta = string.Empty;
            var filter = string.Empty;

            var query = Session[SessionNameForQuery];
            if (query != null)
            {
                var parameters = (List<string>)query;
                noSuratKontrol = parameters[0];
                if (!string.IsNullOrWhiteSpace(parameters[1])) tglAwal = Convert.ToDateTime(parameters[1]);
                if (!string.IsNullOrWhiteSpace(parameters[2])) tglAkhir = Convert.ToDateTime(parameters[2]);
                noPeserta = parameters[3];
                filter = parameters[4];
            }
            var svc = new Common.BPJS.VClaim.v11.Service();
            if (!string.IsNullOrWhiteSpace(noSuratKontrol))
            {
                var data = new List<Common.BPJS.VClaim.v11.RencanaKontrol.Select.ResponseSuratKontrolList.List>();
                var response = svc.GetRencanaKontrolByNoSuratKontrol(noSuratKontrol);
                if (response.MetaData.IsValid)
                {
                    data.Add(new Common.BPJS.VClaim.v11.RencanaKontrol.Select.ResponseSuratKontrolList.List()
                    {
                        NoSuratKontrol = response.Response.NoSuratKontrol,
                        TglRencanaKontrol = response.Response.TglRencanaKontrol,
                        TglTerbitKontrol = response.Response.TglTerbit,
                        NamaPoliTujuan = response.Response.NamaPoliTujuan,
                        NoSepAsalKontrol = response.Response.Sep.NoSep,
                        TglSEP = response.Response.Sep.TglSep,
                        NoKartu = response.Response.Sep.Peserta.NoKartu,
                        Nama = response.Response.Sep.Peserta.Nama,
                        NamaDokter = response.Response.NamaDokter,
                        JnsKontrol = response.Response.JnsKontrol
                    });
                    grdList.DataSource = GridDataSource(data);
                }
                else grdList.DataSource = GridDataSource(data);
            }
            else if (!string.IsNullOrWhiteSpace(noPeserta))
            {
                var bulan = tglAwal.ToString("MM");
                var tahun = tglAwal.Year.ToString();
                var response = svc.GetRencanaKontrolByNoPeserta(bulan, tahun, noPeserta, Common.BPJS.VClaim.Enum.FilterRencanaKontrol.TanggalRencanaKontrol);
                //var response = svc.GetRencanaKontrolByNoPeserta(DateTime.Now.ToString("MM"), DateTime.Now.Year.ToString(), noPeserta, Common.BPJS.VClaim.Enum.FilterRencanaKontrol.TanggalRencanaKontrol);
                if (response.MetaData.IsValid)
                {
                    grdList.DataSource = GridDataSource(response.Response.List);
                }
                else grdList.DataSource = new List<Common.BPJS.VClaim.v11.RencanaKontrol.Select.ResponseSuratKontrolList.List>();
            }
            else
            {
                var response = svc.GetDataRencanaKontrolByTanggal(tglAwal, tglAkhir, filter ==  "1" ? Common.BPJS.VClaim.Enum.FilterRencanaKontrol.TanggalEntry : Common.BPJS.VClaim.Enum.FilterRencanaKontrol.TanggalRencanaKontrol);
                if (response.MetaData.IsValid)
                {
                    grdList.DataSource = GridDataSource(response.Response.List);
                }
                else grdList.DataSource = new List<Common.BPJS.VClaim.v11.RencanaKontrol.Select.ResponseSuratKontrolList.List>();
            }
        }

        protected void grdList_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var noSuratKontrol = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["NoSuratKontrol"]);

            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.Delete(new Common.BPJS.VClaim.v11.RencanaKontrol.Delete.Request.Root()
            {
                Request = new Common.BPJS.VClaim.v11.RencanaKontrol.Delete.Request.TRequest()
                {
                    TSuratkontrol = new Common.BPJS.VClaim.v11.RencanaKontrol.Delete.Request.TSuratkontrol()
                    {
                        NoSuratKontrol = noSuratKontrol,
                        User = AppSession.UserLogin.UserID
                    }
                }
            });
            if (response.MetaData.IsValid)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "poli", string.Format("alert('Code : {0}, Message : {1}');", "000", "Hapus SKDP/SPRI berhasil"), true);

                grdList.Rebind();
            }
            else ScriptManager.RegisterStartupScript(this, GetType(), "poli", string.Format("alert('Code : {0}, Message : {1}');", response.MetaData.Code, response.MetaData.Message), true);
        }
    }
}
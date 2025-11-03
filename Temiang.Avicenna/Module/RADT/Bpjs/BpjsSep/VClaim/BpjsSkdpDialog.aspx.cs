using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs.BpjsSep.VClaim
{
    public partial class BpjsSkdpDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.BpjsSep;

            if (!IsPostBack)
            {
                for (int i = 0; i <= 11; i++)
                {
                    cboBulan.Items.Add(new Telerik.Web.UI.RadComboBoxItem(CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[i], i.ToString().Length == 1 ? "0" + (i + 1).ToString() : (i + 1).ToString()));
                }

                cboBulan.SelectedValue = DateTime.Now.Month.ToString().Length == 1 ? "0" + (DateTime.Now.Month).ToString() : (DateTime.Now.Month).ToString();

                txtTahun.Value = Convert.ToDouble(DateTime.Now.Year);
            }
        }

        protected void btnSearchSkdp_Click(object sender, ImageClickEventArgs e)
        {
            grdPatient.Rebind();
        }

        protected void grdPatient_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetRencanaKontrolByNoPeserta(cboBulan.SelectedValue, Convert.ToUInt32(txtTahun.Value).ToString(), Request.QueryString["noka"],
                rblFilter.SelectedValue == "1" ? Common.BPJS.VClaim.Enum.FilterRencanaKontrol.TanggalEntry : Common.BPJS.VClaim.Enum.FilterRencanaKontrol.TanggalRencanaKontrol);
            if (!response.MetaData.IsValid)
            {
                grdPatient.DataSource = new List<Common.BPJS.VClaim.v11.RencanaKontrol.Select.ResponseSuratKontrolList.List>();
            }
            else
            {
                grdPatient.DataSource = GridDataSource(response.Response.List);
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.mode = '" + "value!skdp!" + grdPatient.SelectedValue + "'";
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
            dtb.Columns.Add("TerbitSEP", typeof(string));

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
                row[9] = list.TerbitSEP;

                dtb.Rows.Add(row);
            }

            dtb.AcceptChanges();

            return dtb;
        }
    }
}
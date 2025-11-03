using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class BpjsCheckoutList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.BpjsCheckout;
        }

        private DataTable GetRiwayatTable
        {
            get
            {
                if (ViewState["riwayatTable"] != null) return ViewState["riwayatTable"] as DataTable;

                var table = new DataTable();
                table.Columns.Add("noSEP", typeof(string));
                table.Columns.Add("tglSEP", typeof(DateTime));
                table.Columns.Add("jnsPelayanan", typeof(string));
                table.Columns.Add("poliTujuan", typeof(string));
                table.Columns.Add("diagnosa", typeof(string));
                table.Columns.Add("biayaTagihan", typeof(decimal));
                table.Columns.Add("tglPulang", typeof(DateTime));

                ViewState["riwayatTable"] = table;

                return GetRiwayatTable;
            }
            set
            { ViewState["riwayatTable"] = value; }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = GetRiwayatTable;
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            var table = GetRiwayatTable;
            table.Rows.Clear();

            if (string.IsNullOrEmpty(txtNoKartu.Text)) return;

            //var service = new Temiang.Avicenna.Common.BPJS.v21.Service();
            //var riwayat = service.GetRiwayat(txtNoKartu.Text);
            //if (riwayat.Metadata.IsValid)
            //{
            //    if (riwayat.Response.Count.ToInt() == 0) return;


            //    foreach (var entity in riwayat.Response.List)
            //    {
            //        var row = table.NewRow();

            //        row[0] = entity.NoSEP;
            //        row[1] = Convert.ToDateTime(entity.TglSEP);
            //        row[2] = entity.JnsPelayanan;
            //        row[3] = entity.PoliTujuan.KdPoli + " - " + entity.PoliTujuan.NmPoli;
            //        row[4] = entity.Diagnosa.KodeDiagnosa + " - " + entity.Diagnosa.NamaDiagnosa;
            //        row[5] = Convert.ToDecimal(entity.BiayaTagihan);
            //        DateTime? date = null;
            //        row[6] = string.IsNullOrEmpty(entity.TglPulang) ? date : Convert.ToDateTime(entity.TglPulang);

            //        table.Rows.Add(row);
            //    }

            //    grdList.DataSource = table;
            //    grdList.DataBind();
            //}
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (sourceControl is RadGrid && eventArgument == "rebind") btnFilter_Click(null, null);
        }
    }
}

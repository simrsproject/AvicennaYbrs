using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs.Monitoring
{
    public partial class DataKunjungan : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.MonitoringDataKunjungan;

            if (!IsPostBack)
            {
                txtTglPelayanan.SelectedDate = DateTime.Now.Date;
            }
        }

        protected void btnFilterTglPelayanan_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void grdList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetDataKunjungan(txtTglPelayanan.SelectedDate ?? new DateTime(), cboPelayanan.SelectedValue == "1" ? Common.BPJS.VClaim.Enum.JenisPelayanan.Inap : Common.BPJS.VClaim.Enum.JenisPelayanan.Jalan);
            if (response.MetaData.IsValid) grdList.DataSource = response.Response.Sep.ToDataTable();
            else
            {
                grdList.DataSource = new List<Common.BPJS.VClaim.v11.Monitoring.Kunjungan.Sep>()
                {
                    new Common.BPJS.VClaim.v11.Monitoring.Kunjungan.Sep()
                    {
                        Nama = response.MetaData.Code + " - " + response.MetaData.Message
                    }
                }.ToDataTable();
            }
        }
    }
}
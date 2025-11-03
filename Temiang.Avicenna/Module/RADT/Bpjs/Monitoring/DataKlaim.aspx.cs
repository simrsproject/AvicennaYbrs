using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs.Monitoring
{
    public partial class DataKlaim : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.MonitoringDataKlaim;

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
            Common.BPJS.VClaim.Enum.StatusKlaim status = null;
            switch (cboStatusKlaim.SelectedValue)
            {
                case "1":
                    status = Common.BPJS.VClaim.Enum.StatusKlaim.Proses;
                    break;
                case "2":
                    status = Common.BPJS.VClaim.Enum.StatusKlaim.Pending;
                    break;
                case "3":
                    status = Common.BPJS.VClaim.Enum.StatusKlaim.Klaim;
                    break;
            }

            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetDataKlaim(txtTglPelayanan.SelectedDate ?? new DateTime(), cboPelayanan.SelectedValue == "1" ? Common.BPJS.VClaim.Enum.JenisPelayanan.Inap : Common.BPJS.VClaim.Enum.JenisPelayanan.Jalan, status);
            if (response.MetaData.IsValid) grdList.DataSource = response.Response.Klaim.ToDataTable();
            else
            {
                grdList.DataSource = new List<Common.BPJS.VClaim.v11.Monitoring.Klaim.Klaim>()
                {
                    new Common.BPJS.VClaim.v11.Monitoring.Klaim.Klaim()
                    {
                        NoSEP = response.MetaData.Code + " - " + response.MetaData.Message
                    }
                }.ToDataTable();
            }
        }
    }
}
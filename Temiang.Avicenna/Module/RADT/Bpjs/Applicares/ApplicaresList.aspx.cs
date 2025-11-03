using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class ApplicaresList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.BpjsApplicares;
        }

        protected void tmrUpdate_Tick(object sender, EventArgs e)
        {
            grdList.Rebind();
            grdMonitor.Rebind();
        }

        protected void grdMonitor_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var list = new BusinessObject.BedCollection();
            grdMonitor.DataSource = list.InpatientBedAvailability();
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var ws = new Temiang.Avicenna.WebService.Applicares();
            try
            {
                var list = fastJSON.JSON.ToObject<Common.BPJS.Applicare.KetersediaanKamarRS.KetersediaanKamar>(ws.ReadRuangan()).Response.List;

                grdList.DataSource = list;
            }
            catch
            {

            }
        }
    }
}

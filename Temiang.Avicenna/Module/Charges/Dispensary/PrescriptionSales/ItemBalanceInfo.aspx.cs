using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using DevExpress.Web;

namespace Temiang.Avicenna.Module.Charges.Dispensary.PrescriptionSales
{
    public partial class ItemBalanceInfo : BasePageDialog
    {
        public string ItemID {
            get {
                return Request.QueryString["itemid"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            ButtonOk.Visible = false;
            ButtonCancel.Text = "Close";
        }

        protected void grdBalance_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var ib = new ItemBalanceQuery("ib");
            var i = new ItemQuery("i");
            var loc = new LocationQuery("loc");
            var im = new ItemProductMedicQuery("im");

            ib.InnerJoin(i).On(ib.ItemID == i.ItemID)
                .InnerJoin(loc).On(ib.LocationID == loc.LocationID)
                .LeftJoin(im).On(i.ItemID == im.ItemID)
                .Where(ib.ItemID == ItemID, loc.IsActive == true)
                .Select(ib, i.ItemName, im.SRItemUnit, loc.LocationName);
            var dtb = ib.LoadDataTable();

            grdBalance.DataSource = dtb;
        }
    }
}

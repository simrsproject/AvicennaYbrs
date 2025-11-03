using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PrescriptionSalesEtiquetteDetailEdList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.tno = '" + grdList.SelectedValue + "'";
        }

        private DataTable ItemTransactionItemEds
        {
            get
            {
                var itemEdQ = new ItemTransactionItemEdQuery("a");
                var itQ = new ItemTransactionQuery("b");
                var vwQ = new VwItemProductMedicNonMedicQuery("c");
                itemEdQ.InnerJoin(itQ).On(itQ.TransactionNo == itemEdQ.TransactionNo);
                itemEdQ.InnerJoin(vwQ).On(vwQ.ItemID == itemEdQ.ItemID);
                itemEdQ.Select
                (
                    @"<a.BatchNumber + '|' + CONVERT(VARCHAR(10), a.ExpiredDate, 101) AS ListKey>",
                    itemEdQ.ExpiredDate,
                    itemEdQ.BatchNumber,
                    @"<SUM(a.Quantity * a.ConversionFactor) AS QuantityInBaseUnit>",
                    vwQ.SRItemUnit
                );
                itemEdQ.Where(itQ.TransactionCode == TransactionCode.PurchaseOrderReceive, itQ.IsApproved == true, itemEdQ.ItemID == Request.QueryString["itemId"],
                    itemEdQ.Or(itemEdQ.IsClosed.IsNull(), itemEdQ.IsClosed == false));
                itemEdQ.GroupBy(itemEdQ.ExpiredDate,
                    itemEdQ.BatchNumber, 
                    vwQ.SRItemUnit);

                itemEdQ.OrderBy(itemEdQ.ExpiredDate.Ascending);

                itemEdQ.es.WithNoLock = true;

                DataTable dtb = itemEdQ.LoadDataTable();

                return dtb;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ItemTransactionItemEds;
        }
    }
}
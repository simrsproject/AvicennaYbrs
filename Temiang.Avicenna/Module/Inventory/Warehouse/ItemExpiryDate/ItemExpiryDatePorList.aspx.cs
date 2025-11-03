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

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class ItemExpiryDatePorList : BasePageDialog
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
                    @"<a.TransactionNo + '|' + a.SequenceNo + '|' + a.BatchNumber + '|' + CONVERT(VARCHAR(10), a.ExpiredDate, 101) AS ListKey>",
                    itemEdQ.TransactionNo,
                    itemEdQ.SequenceNo,
                    itemEdQ.ExpiredDate,
                    itemEdQ.BatchNumber,
                    @"<a.Quantity * a.ConversionFactor AS QuantityInBaseUnit>",
                    vwQ.SRItemUnit
                );
                itemEdQ.Where(itQ.TransactionCode == TransactionCode.PurchaseOrderReceive, itQ.IsApproved == true, itemEdQ.ItemID == Request.QueryString["itemId"],
                    itemEdQ.Or(itemEdQ.IsClosed.IsNull(), itemEdQ.IsClosed == false));

                itemEdQ.OrderBy(itemEdQ.ExpiredDate.Ascending, itemEdQ.TransactionNo.Ascending);

                itemEdQ.es.WithNoLock = true;

                DataTable dtb = itemEdQ.LoadDataTable();

                //foreach (DataRow row in dtb.Rows)
                //{
                //    var asset = new AssetCollection();
                //    asset.Query.Where(asset.Query.PurchaseOrderNumber == row["TransactionNo"], asset.Query.ItemID == row["ItemID"]);
                //    asset.LoadAll();

                //    if ((Convert.ToInt64(row["Quantity"])) - asset.Count == 0)
                //        row.Delete();
                //    else
                //    {
                //        row["ListGroup"] = row["TransactionNo"] + " [Remining Qty: " + string.Format("{0:n0}", (Convert.ToInt64(row["QtyPor"])) - (Convert.ToInt64(row["QtyAsset"]))) + "]";
                //        row["RemainingQty"] = (Convert.ToInt64(row["Quantity"])) - asset.Count;
                //    }
                //}
                //dtb.AcceptChanges();

                return dtb;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ItemTransactionItemEds;
        }
    }
}
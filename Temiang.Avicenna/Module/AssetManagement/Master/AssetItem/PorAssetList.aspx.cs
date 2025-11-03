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

namespace Temiang.Avicenna.Module.AssetManagement.Master
{
    public partial class PorAssetList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFromDate.SelectedDate = (DateTime.Today).AddMonths(-1).AddDays(1);
                txtToDate.SelectedDate = DateTime.Today;
                //txtFromDate.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.tno = '" + grdList.SelectedValue + "'";
        }

        private DataTable PurchaseOrderReceiveds
        {
            get
            {
                var query = new ItemTransactionQuery("a");
                var qsupp = new SupplierQuery("b");
                var qunit = new ServiceUnitQuery("c");
                var qitemType = new AppStandardReferenceItemQuery("d");
                var qtransItem = new ItemTransactionItemQuery("e");
                var qitem = new ItemQuery("i");

                query.InnerJoin(qsupp).On(query.BusinessPartnerID == qsupp.SupplierID);
                query.InnerJoin(qunit).On(query.ToServiceUnitID == qunit.ServiceUnitID);
                query.InnerJoin(qitemType).On(query.SRItemType == qitemType.ItemID &&
                                              qitemType.StandardReferenceID == AppEnum.StandardReference.ItemType);
                query.InnerJoin(qtransItem).On(query.TransactionNo == qtransItem.TransactionNo);
                query.LeftJoin(qitem).On(qitem.ItemID == qtransItem.ItemID);

                query.Where(query.TransactionCode == TransactionCode.PurchaseOrderReceive, 
                    query.Or(query.IsAssets == true, qitem.IsAsset == true),
                    query.IsApproved == true);

                if (string.IsNullOrEmpty(txtPoReceivedNo.Text))
                {
                    if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
                        query.Where(query.TransactionDate.Between(txtFromDate.SelectedDate, txtToDate.SelectedDate));
                    if (!string.IsNullOrEmpty(cboSupplier.SelectedValue))
                        query.Where(query.BusinessPartnerID == cboSupplier.SelectedValue);
                }
                else
                    query.Where(query.TransactionNo == txtPoReceivedNo.Text);

                query.Select
                    (
                        @"<'' AS ListGroup>",
                        query.TransactionNo,
                        query.TransactionDate,
                        qsupp.SupplierName,
                        qunit.ServiceUnitName,
                        qitemType.ItemName.As("ItemType"),
                        qtransItem.ItemID,
                        qtransItem.Description.As("ItemName"),
                        @"<SUM(e.Quantity * e.ConversionFactor) AS Quantity>",
                        @"<0 AS RemainingQty>",
                        @"<a.TransactionNo + '|' + e.ItemID AS ListKey>",
                        @"<ISNULL((SELECT SUM(iti.Quantity*iti.ConversionFactor) AS Qty FROM ItemTransactionItem iti WHERE iti.TransactionNo = a.TransactionNo), 0) AS QtyPor>",
                        @"<ISNULL((SELECT COUNT(x.AssetID) AS Qty FROM Asset x WHERE x.PurchaseOrderNumber = a.TransactionNo), 0) AS QtyAsset>"
                    );
                query.GroupBy(
                        query.TransactionNo,
                        query.TransactionDate,
                        qsupp.SupplierName,
                        qunit.ServiceUnitName,
                        qitemType.ItemName,
                        qtransItem.ItemID,
                        qtransItem.Description);

                query.OrderBy(query.TransactionNo.Ascending, qtransItem.Description.Ascending);

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var asset = new AssetCollection();
                    asset.Query.Where(asset.Query.PurchaseOrderNumber == row["TransactionNo"], asset.Query.ItemID == row["ItemID"]);
                    asset.LoadAll();

                    if ((Convert.ToInt64(row["Quantity"])) - asset.Count == 0)
                        row.Delete();
                    else
                    {
                        row["ListGroup"] = row["TransactionNo"] + " [Remining Qty: " + string.Format("{0:n0}", (Convert.ToInt64(row["QtyPor"])) - (Convert.ToInt64(row["QtyAsset"]))) + "]";
                        row["RemainingQty"] = (Convert.ToInt64(row["Quantity"])) - asset.Count;
                    }
                }
                dtb.AcceptChanges();

                return dtb;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PurchaseOrderReceiveds;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void cboSupplier_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SupplierQuery();
            query.Select(query.SupplierID, query.SupplierName);
            query.es.Top = 20;
            query.Where
                (
                    query.SupplierName.Like(searchTextContain),
                    query.IsActive == true
                );
            query.OrderBy(query.SupplierName.Ascending);

            cboSupplier.DataSource = query.LoadDataTable();
            cboSupplier.DataBind();
        }

        protected void cboSupplier_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SupplierName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SupplierID"].ToString();
        }
    }
}

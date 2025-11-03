using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class ConsignmentTransferList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ConsignmentTransferSearch.aspx";
            UrlPageDetail = "ConsignmentTransferDetail.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.ConsignmentTransfer;
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
            string id = dataItem.GetDataKeyValue(ItemTransactionMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("ConsignmentTransferDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                grdList.DataSource = ItemTransactions;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string transNo = dataItem.GetDataKeyValue("TransactionNo").ToString();
            //Load record
            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");
            query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
            query.Where(query.TransactionNo == transNo);
            query.OrderBy(query.ItemID.Ascending);

            query.Select
                (
                    query.ItemID,
                    query.SRItemUnit,
                    query.Quantity,
                    query.SequenceNo,
                    query.IsClosed,
                    query.Price,
                    iq.ItemName.As("ItemName")
                );

            DataTable dtb = query.LoadDataTable();
            //Apply
            e.DetailTableView.DataSource = dtb;
        }

        private DataTable ItemTransactions
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                ItemTransactionQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (ItemTransactionQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new ItemTransactionQuery("a");
                    var itype = new AppStandardReferenceItemQuery("d");
                    var supp = new SupplierQuery("s");
                    var floc = new LocationQuery("floc");
                    var tsu = new ServiceUnitQuery("c");
                    var tloc = new LocationQuery("tloc");

                    query.LeftJoin(itype).On(itype.ItemID == query.SRItemType && itype.StandardReferenceID == "ItemType");
                    query.InnerJoin(supp).On(query.BusinessPartnerID == supp.SupplierID);
                    query.InnerJoin(floc).On(query.FromLocationID == floc.LocationID);
                    query.InnerJoin(tsu).On(tsu.ServiceUnitID == query.ToServiceUnitID);
                    query.InnerJoin(tloc).On(query.ToLocationID == tloc.LocationID);

                    query.Select(
                           query.TransactionNo,
                           query.TransactionDate,
                           itype.ItemName.As("ItemType"),
                           supp.SupplierName,
                           floc.LocationName.As("FromLocationName"),
                           tsu.ServiceUnitName.As("ToServiceUnitName"),
                           tloc.LocationName.As("ToLocationName"),
                           query.Notes,
                           query.IsApproved,
                           query.IsVoid
                       );

                    query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.ConsignmentTransfer);

                    query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}

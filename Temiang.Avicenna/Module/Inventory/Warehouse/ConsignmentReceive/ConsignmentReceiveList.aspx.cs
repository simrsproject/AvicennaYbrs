using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class ConsignmentReceiveList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ConsignmentReceiveSearch.aspx";
            UrlPageDetail = "ConsignmentReceiveDetail.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.ConsignmentReceive;
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
            string url = string.Format("ConsignmentReceiveDetail.aspx?md={0}&id={1}", mode, id);
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
                    var qryserviceunit = new ServiceUnitQuery("c");
                    query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.ToServiceUnitID);

                    var itemtype = new AppStandardReferenceItemQuery("d");
                    query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");

                    var supplierQuery = new SupplierQuery("s");
                    query.InnerJoin(supplierQuery).On(query.BusinessPartnerID == supplierQuery.SupplierID);

                    query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.ConsignmentReceive);

                    query.Select(
                           query.TransactionNo,
                           query.TransactionDate,
                           query.ReferenceNo,
                           qryserviceunit.ServiceUnitName,
                           itemtype.ItemName.As("ItemType"),
                           query.IsApproved,
                           supplierQuery.SupplierName,
                           query.Notes,
                           query.IsVoid
                       );

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
using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Charges.Dispensary
{
    public partial class SalesReturnList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "SalesReturnSearch.aspx";
            UrlPageDetail = "SalesReturnDetail.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.SalesReturn;
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
            string url = string.Format("SalesReturnDetail.aspx?md={0}&id={1}", mode, id);
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
            ItemTransactionItemQuery query = new ItemTransactionItemQuery("a");
            ItemQuery iq = new ItemQuery("b");
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
                    iq.ItemName.As("ItemName"),
                    query.Price,
                    query.Discount1Percentage,
                    query.Discount2Percentage,
                    query.Discount
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
                    ServiceUnitQuery qryserviceunit = new ServiceUnitQuery("c");
                    CustomerQuery cust = new CustomerQuery("b");

                    AppStandardReferenceItemQuery itemtype = new AppStandardReferenceItemQuery("d");
                    query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.ToServiceUnitID);
                    query.LeftJoin(cust).On(query.CustomerID == cust.CustomerID);
                    query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
                    query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.SalesReturn);

                    query.Select(
                           query.TransactionNo,
                           query.TransactionDate,
                           query.ReferenceNo,
                           qryserviceunit.ServiceUnitName.As("TServiceUnitID"),
                           cust.CustomerName,
                           itemtype.ItemName,
                           query.IsApproved,
                           query.ReferenceNo,
                           query.Notes,
                           query.IsVoid
                       );
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.OrderBy(query.TransactionDate.Descending);
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}
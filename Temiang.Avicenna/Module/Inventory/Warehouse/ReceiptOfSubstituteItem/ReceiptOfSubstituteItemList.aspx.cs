using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class ReceiptOfSubstituteItemList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ReceiptOfSubstituteItemSearch.aspx";
            UrlPageDetail = "ReceiptOfSubstituteItemDetail.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.ReceiptOfSubstituteItem;
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
            string url = string.Format("ReceiptOfSubstituteItemDetail.aspx?md={0}&id={1}", mode, id);
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
                    var sup = new SupplierQuery("b");
                    var itemtype = new AppStandardReferenceItemQuery("d");
                    var qusr = new AppUserServiceUnitQuery("u");

                    query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.ToServiceUnitID);
                    query.LeftJoin(sup).On(query.BusinessPartnerID == sup.SupplierID);
                    query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
                    query.InnerJoin(qusr).On(query.ToServiceUnitID == qusr.ServiceUnitID &&
                                    qusr.UserID == AppSession.UserLogin.UserID);

                    query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.ReceiptOfSubstitute);

                    query.Select(
                           query.TransactionNo,
                           query.TransactionDate,
                           query.ReferenceNo,
                           qryserviceunit.ServiceUnitName.As("TServiceUnitID"),
                           sup.SupplierName,
                           itemtype.ItemName,
                           query.IsApproved,
                           query.ReferenceNo,
                           query.Notes,
                           query.IsVoid
                       );
                    query.OrderBy(query.ToServiceUnitID.Ascending, query.TransactionDate.Descending, query.TransactionNo.Descending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }
    }
}

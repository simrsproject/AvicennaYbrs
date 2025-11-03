using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Charges.Dispensary
{
    public partial class SalesList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "SalesSearch.aspx";
            UrlPageDetail = "SalesDetail.aspx";

            this.WindowSearch.Height = 400;
			ProgramID = AppConstant.Program.Sales ;
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
            string url = string.Format("SalesDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = ItemTransactions;
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

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        private DataTable ItemTransactions
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ItemTransactionQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ItemTransactionQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ItemTransactionQuery("a");
                    var qryCustomer= new CustomerQuery("c");
                    var itemtype = new AppStandardReferenceItemQuery("d");
                    var user = new AppUserServiceUnitQuery("e");

                    query.InnerJoin(qryCustomer).On(qryCustomer.CustomerID== query.CustomerID);
                    query.InnerJoin(user).On(query.FromServiceUnitID == user.ServiceUnitID &&
                                             user.UserID == AppSession.UserLogin.UserID);
                    query.LeftJoin(itemtype).On
                        (
                            itemtype.ItemID == query.SRItemType && 
                            itemtype.StandardReferenceID == AppEnum.StandardReference.ItemType.ToString()
                        );
                    query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.Sales);
                    query.OrderBy(query.TransactionDate.Descending);

                    query.Select
                        (
                           query.TransactionNo,
                           query.TransactionDate,
                           qryCustomer.CustomerName,
                           itemtype.ItemName,
                           query.IsApproved,
                           query.Notes,
                           query.IsVoid
                       );
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();

                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
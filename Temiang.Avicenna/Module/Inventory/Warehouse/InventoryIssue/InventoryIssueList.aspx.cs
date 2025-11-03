using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class InventoryIssueList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "InventoryIssueSearch.aspx";
            UrlPageDetail = "InventoryIssueDetail.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.InventoryIssue;
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
            string url = string.Format("InventoryIssueDetail.aspx?md={0}&id={1}&rod=", mode, id);
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
                    var qryserviceunit = new ServiceUnitQuery("c");
                    var itemtype = new AppStandardReferenceItemQuery("d");
                    var user = new AppUserServiceUnitQuery("e");
                    var tounit = new ServiceUnitQuery("f");
                    var floc = new LocationQuery("g");

                    query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                    query.InnerJoin(user).On(query.FromServiceUnitID == user.ServiceUnitID &&
                                             user.UserID == AppSession.UserLogin.UserID);
                    query.LeftJoin(itemtype).On
                        (
                            itemtype.ItemID == query.SRItemType &&
                            itemtype.StandardReferenceID == AppEnum.StandardReference.ItemType.ToString()
                        );
                    query.LeftJoin(tounit).On(query.ToServiceUnitID == tounit.ServiceUnitID);
                    query.InnerJoin(floc).On(query.FromLocationID == floc.LocationID);
                    query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.InventoryIssueOut);
                    query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);

                    query.Select
                        (
                            query.TransactionNo,
                            query.TransactionDate,
                            qryserviceunit.ServiceUnitName,
                            floc.LocationName,
                            tounit.ServiceUnitName.As("ToUnit"),
                            itemtype.ItemName,
                            query.IsApproved,
                            query.Notes,
                            query.IsVoid,
                            query.ApprovedByUserID,
                            query.ApprovedDate
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
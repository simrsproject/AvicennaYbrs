using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class DistributionConfirmList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "DistributionConfirmSearch.aspx";
            UrlPageDetail = "DistributionConfirmDetail.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.DistributionConfirm;
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
            string url = string.Format("DistributionConfirmDetail.aspx?md={0}&id={1}", mode, id);
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
                    var refq = new ItemTransactionQuery("f");
                    var refunitq = new ServiceUnitQuery("g");

                    query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                    query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
                    query.InnerJoin(user).On(user.ServiceUnitID == query.FromServiceUnitID);
                    query.InnerJoin(refq).On(query.ReferenceNo == refq.TransactionNo);
                    query.InnerJoin(refunitq).On(refq.FromServiceUnitID == refunitq.ServiceUnitID);

                    query.Where
                        (
                            query.TransactionCode == BusinessObject.Reference.TransactionCode.DistributionConfirm,
                            user.UserID == AppSession.UserLogin.UserID
                        );

                    query.Select(
                           query.TransactionNo,
                           query.TransactionDate,
                           query.ReferenceNo,
                           qryserviceunit.ServiceUnitName.As("FServiceUnitID"),
                           refunitq.ServiceUnitName.As("FromUnit"),
                           itemtype.ItemName,
                           query.IsApproved,
                           query.Notes,
                           query.IsVoid,
                           query.LastUpdateDateTime,
                           query.LastUpdateByUserID
                       );
                    query.OrderBy(query.FromServiceUnitID.Ascending, query.TransactionDate.Descending, query.TransactionNo.Descending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}
using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class InventoryIssueRequestList : BasePageList
    {
        private string FormType
        {
            get
            {
                return (string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"]);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //UrlPageSearch = "InventoryIssueRequestSearch.aspx?type=" + FormType;
            UrlPageSearch = "InventoryIssueRequestSearch.aspx" + (string.IsNullOrEmpty(FormType) ? "" : "?type=" + FormType);
            UrlPageDetail = "InventoryIssueRequestDetail.aspx?type=" + FormType;

            ProgramID = FormType == "PurchaseCat-002" ? AppConstant.Program.InventoryIssueRequest2 : AppConstant.Program.InventoryIssueRequest;    

            this.WindowSearch.Height = 400;
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", FormType);
            return script;
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
            string url = string.Format("InventoryIssueRequestDetail.aspx?md={0}&id={1}&rod=&type={2}", mode, id, FormType);
            
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
                    query.ConversionFactor,
                    query.Quantity,
                    query.SequenceNo,
                    query.IsClosed,
                    iq.ItemName.As("ItemName"),
                    @"<(ISNULL((SELECT SUM(iti.Quantity * iti.ConversionFactor) AS Qty 
FROM ItemTransactionItem AS iti
INNER JOIN ItemTransaction AS it ON it.TransactionNo = iti.TransactionNo
WHERE it.TransactionCode = '082' AND it.IsApproved = 1 AND iti.ReferenceNo = a.TransactionNo AND iti.ReferenceSequenceNo = a.SequenceNo), 0))/ a.ConversionFactor AS QtyFinished>"
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

                    query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                    query.InnerJoin(user).On(query.FromServiceUnitID == user.ServiceUnitID &&
                                             user.UserID == AppSession.UserLogin.UserID);
                    query.LeftJoin(itemtype).On
                        (
                            itemtype.ItemID == query.SRItemType &&
                            itemtype.StandardReferenceID == AppEnum.StandardReference.ItemType.ToString()
                        );
                    query.LeftJoin(tounit).On(query.ToServiceUnitID == tounit.ServiceUnitID);
                    query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.InventoryIssueRequestOut);
                    if (!string.IsNullOrEmpty(FormType))
                        query.Where(query.SRPurchaseCategorization == FormType);
                    
                    query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);

                    query.Select
                        (
                            query.TransactionNo,
                            query.TransactionDate,
                            qryserviceunit.ServiceUnitName,
                            tounit.ServiceUnitName.As("ToUnit"),
                            itemtype.ItemName,
                            query.IsApproved,
                            query.Notes,
                            query.IsVoid,
                            //@"<CASE WHEN (SELECT COUNT(*) FROM ItemTransaction AS it WHERE it.TransactionNo = a.ReferenceNo AND it.IsApproved = 1) > 0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsClosed>",
                            query.IsClosed,
                            query.ApprovedDate,
                            query.ApprovedByUserID,
                            query.LastUpdateByUserID,
                            query.LastUpdateDateTime
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
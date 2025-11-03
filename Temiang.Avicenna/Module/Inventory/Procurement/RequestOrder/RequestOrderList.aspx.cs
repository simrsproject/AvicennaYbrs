using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Procurement
{
    public partial class RequestOrderList : BasePageList
    {
        private string getPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["itype"]) ? string.Empty : Request.QueryString["itype"];
            }
        }

        protected bool IsShowStock
        {
            get { return AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPOWithStockInfo); }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["md"]))
            {
                // Redirect to entry page
                // Program entry RequestOrder ada beberapa macam shg jika dipanggil dari program lain 
                // hanya bisa memakai url listnya yg disimpan di AppProgram yg kemudian harus di redirect
                Response.Redirect(string.Format("PurchaseOrderDetail.aspx?{0}", Request.QueryString.ToString()));
                return;
            }

            UrlPageSearch = "RequestOrderSearch.aspx?itype=" + getPageID;
            UrlPageDetail = "RequestOrderDetail.aspx?itype=" + getPageID;

            ProgramID = getPageID == "a" ? AppConstant.Program.RequestOrderAsset : AppConstant.Program.RequestOrder;

            this.WindowSearch.Height = 400;
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", getPageID);
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

        //private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        //{
        //    string id = dataItem.GetDataKeyValue(ItemTransactionMetadata.ColumnNames.TransactionNo).ToString();
        //    string url = string.Format("RequestOrderDetail.aspx?md={0}&id={1}&rod=&itype={2}", mode, id, getPageID);
        //    Page.Response.Redirect(url, true);
        //}

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(ItemTransactionMetadata.ColumnNames.TransactionNo).ToString();
            Page.Response.Redirect("RequestOrderDetail.aspx?md=" + mode + "&id=" + id + "&rod=&itype=" + getPageID, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = ItemTransactions;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            ProcurementUtils.HideColumnStockAndPriceInfo(e.DetailTableView);

            //Load record
            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    query.SRItemUnit,
                    //query.Quantity,
                    @"<ISNULL((SELECT SUM(iti.Quantity*iti.ConversionFactor) AS QuantityFinishInBaseUnit 
                        FROM ItemTransactionItem iti
                        INNER JOIN ItemTransaction it ON it.TransactionNo = iti.TransactionNo AND it.TransactionCode = '037' AND it.IsVoid = 0
                        WHERE iti.ReferenceNo = a.TransactionNo AND iti.ReferenceSequenceNo = a.SequenceNo 
                        GROUP BY iti.ReferenceNo, iti.ReferenceSequenceNo
                    ), 0) / a.ConversionFactor AS QuantityFinishInBaseUnit>",
                    @"<ISNULL((SELECT SUM(iti.QuantityFinishInBaseUnit) AS QuantityFinishInBaseUnit 
                        FROM ItemTransactionItem iti
                        INNER JOIN ItemTransaction it ON it.TransactionNo = iti.TransactionNo AND it.TransactionCode = '037' AND it.IsVoid = 0
                        WHERE iti.ReferenceNo = a.TransactionNo AND iti.ReferenceSequenceNo = a.SequenceNo
                        GROUP BY iti.ReferenceNo, iti.ReferenceSequenceNo
                    ), 0) / a.ConversionFactor AS QuantityReceived>",
                    query.ConversionFactor,
                    query.IsClosed,
                    query.Description,
                    iq.ItemName.As("ItemName"),
                    query.Price,
                    query.Discount1Percentage,
                    query.Discount2Percentage,
                    query.Discount
                );

            if (AppSession.Parameter.IsUsingApprovalPurchaseRequest)
            {
                query.Select(
                    @"<CASE WHEN a.RequestQty IS NULL THEN a.Quantity ELSE a.RequestQty END AS Quantity>",
                    @"<CASE WHEN a.RequestQty IS NULL THEN 0 ELSE a.Quantity END AS QtyApproved>");
            }
            else
            {
                query.Select(query.Quantity, query.Quantity.As("QtyApproved"));
            }

            // Base Unit
            var ipnmq = new ItemProductNonMedicQuery("i2");
            var ikq = new ItemKitchenQuery("i2");
            var ipmq = new ItemProductMedicQuery("i2");
            var itemType = e.DetailTableView.ParentItem.GetDataKeyValue("SRItemType").ToString();

            switch (itemType)
            {
                case ItemType.NonMedical:
                    query.LeftJoin(ipnmq).On(query.ItemID == ipnmq.ItemID);
                    break;
                case ItemType.Kitchen:
                    query.LeftJoin(ikq).On(query.ItemID == ikq.ItemID);
                    break;
                default:
                    query.LeftJoin(ipmq).On(query.ItemID == ipmq.ItemID);
                    break;
            }

            // Balance Min Max
            var locationID = e.DetailTableView.ParentItem.GetDataKeyValue("FromLocationID").ToString();
            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorByStockGroup))
            {
                var stockGroup = string.Empty;
                var ibbsgq = new ItemBalanceByStockGroupQuery("c");
                var loc = new Location();
                loc.LoadByPrimaryKey(locationID);
                if (!string.IsNullOrEmpty(loc.SRStockGroup))
                    stockGroup = loc.SRStockGroup;
                query.LeftJoin(ibbsgq).On(query.ItemID == ibbsgq.ItemID && ibbsgq.SRStockGroup == stockGroup);


                var ibq = new ItemBalanceQuery("bl");
                query.LeftJoin(ibq).On(query.ItemID == ibq.ItemID && ibq.LocationID == locationID);


                query.Select(@"<CONVERT(decimal(10,2),COALESCE(c.Balance,0)) AS BalanceSG>",
                    @"<CONVERT(decimal(10,2),COALESCE(bl.Balance,0)) AS Balance>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Minimum,0)) AS Minimum>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Maximum,0)) AS Maximum>",
                    @"<i2.SRItemUnit AS SRMasterBaseUnit>"
                    );
            }
            else
            {
                locationID = ProcurementUtils.LocationIdByItemType(itemType);
                var ibq = new ItemBalanceQuery("c");
                if (string.IsNullOrEmpty(locationID))
                    locationID = "ABCD_EFG";
                query.LeftJoin(ibq).On(query.ItemID == ibq.ItemID && ibq.LocationID == locationID);

                query.Select(@"<CONVERT(decimal(10,2),0) AS BalanceSG>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Balance,0)) AS Balance>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Minimum,0)) AS Minimum>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Maximum,0)) AS Maximum>",
                    @"<i2.SRItemUnit AS SRMasterBaseUnit>"
                    );
            }
            // Sub Query BalanceTotal
            var itemBalTot = new ItemBalanceQuery("ibt");
            itemBalTot.Select((itemBalTot.Balance.Sum().As("BalanceTotal")));
            itemBalTot.Where(itemBalTot.ItemID == query.ItemID);
            query.Select(itemBalTot.Select().As("BalanceTotal"));

            query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
            var trNo = e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString();
            query.Where(query.TransactionNo == trNo);
            query.OrderBy(query.ItemID.Ascending);

            //Apply
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
                    var qryserviceunit = new ServiceUnitQuery("b");
                    var qryserviceunitto = new ServiceUnitQuery("c");
                    var itemtype = new AppStandardReferenceItemQuery("d");
                    var user = new AppUserServiceUnitQuery("e");
                    var costunit = new ServiceUnitQuery("cu");

                    query.Select
                        (
                            query.TransactionNo,
                            query.TransactionDate,
                            qryserviceunit.ServiceUnitName.As("FServiceUnitID"),
                            qryserviceunitto.ServiceUnitName.As("TServiceUnitID"),
                            costunit.ServiceUnitName.As("CostUnit"),
                            itemtype.ItemName,
                            query.IsInventoryItem,
                            query.IsApproved,
                            query.IsClosed,
                            query.Notes,
                            query.IsVoid,
                            query.FromLocationID,
                            query.SRItemType
                        );
                    if (getPageID == "a")
                    {
                        query.Select("<'RequestOrderDetail.aspx?md=view&id=' + a.TransactionNo + '&cons=0&itype=a' AS PrUrl>");
                        query.Where(query.IsAssets == true);
                    }
                    else
                        query.Select("<'RequestOrderDetail.aspx?md=view&id=' + a.TransactionNo + '&cons=0&itype=' AS PrUrl>");

                    query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                    query.InnerJoin(qryserviceunitto).On(qryserviceunitto.ServiceUnitID == query.ToServiceUnitID);
                    query.InnerJoin(costunit).On(costunit.ServiceUnitID == query.ServiceUnitCostID);
                    query.LeftJoin(itemtype).On
                        (
                            itemtype.ItemID == query.SRItemType &&
                            itemtype.StandardReferenceID == "ItemType"
                        );
                    query.InnerJoin(user).On(user.ServiceUnitID == query.FromServiceUnitID & user.UserID == AppSession.UserLogin.UserID);

                    query.Where
                        (
                            query.TransactionCode == TransactionCode.PurchaseRequest,
                            query.Or(query.IsConsignmentAlreadyReceived.IsNull(), query.IsConsignmentAlreadyReceived == false)
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
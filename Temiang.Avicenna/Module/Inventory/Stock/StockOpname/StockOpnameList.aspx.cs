using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Inventory.Stock
{
    public partial class StockOpnameList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "StockOpnameSearch.aspx";
            //if (AppSession.Parameter.HealthcareInitial == "RSCH")
            //    UrlPageDetail = "../StockOpnameDetail.aspx";
            //else
            UrlPageDetail = "StockOpnameDetail.aspx";
            UrlPageDetailImport = "openWinImport();";

            ProgramID = AppConstant.Program.StockOpname;

            WindowSearch.Height = 400;
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"openWinStockOpnameAdd('{0}'); args.set_cancel(true);", AppSession.Parameter.IsStockOpnamePerGroupItem);
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
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ItemTransactions;
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
                    var unit = new ServiceUnitQuery("b");
                    var type = new AppStandardReferenceItemQuery("d");
                    var user = new AppUserServiceUnitQuery("e");
                    var Location = new LocationQuery("f");

                    query.Select
                        (
                            query.TransactionNo,
                            query.TransactionDate,
                            unit.ServiceUnitName.As("FromServiceUnitID"),
                            type.ItemName.As("SRItemType"),
                            Location.LocationName.As("FromLocationID"),
                            query.Notes,
                            "<CAST((CASE WHEN (SELECT COUNT(TransactionNo) FROM ItemStockOpnameApproval WHERE TransactionNo = a.TransactionNo AND IsApproved = 0) = 0 THEN 1 ELSE 0 END) AS BIT) AS IsApproved>",
                            query.IsVoid
                        );
                    query.InnerJoin(unit).On(query.FromServiceUnitID == unit.ServiceUnitID);
                    query.InnerJoin(type).On
                        (
                            query.SRItemType == type.ItemID &
                            type.StandardReferenceID == AppEnum.StandardReference.ItemType
                        );
                    query.InnerJoin(user).On(user.ServiceUnitID == query.FromServiceUnitID);
                    query.InnerJoin(Location).On(Location.LocationID == query.FromLocationID);
                    query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.StockTaking,
                                user.UserID == AppSession.UserLogin.UserID);
                    query.OrderBy(query.TransactionNo.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                
                return dtb;
            }
        }

        private DataTable GetDataGridDataTable(string transactionNo)
        {
            esDynamicQuery query = GetQueryDetail(transactionNo);
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        private esDynamicQuery GetQueryDetail(string transactionNo)
        {
            var query = new ItemTransactionItemQuery("a");
            var item = new ItemQuery("b");
            //var prevBal = new ItemStockOpnamePrevBalanceQuery("c");
            var medic = new ItemProductMedicQuery("d");
            var nonmedic = new ItemProductNonMedicQuery("e");
            //var std = new AppStandardReferenceItemQuery("f");
            //var ib = new ItemBalanceQuery("g");
            var kitchen = new ItemKitchenQuery("h");

            var trans = new ItemTransactionQuery("i");
            //var unit = new ServiceUnitQuery("j");
            //var loc = new LocationQuery("k");
            var app = new ItemStockOpnameApprovalQuery("l");

            query.Select
                (
                //unit.ServiceUnitName,
                //loc.LocationName,
                //trans.TransactionDate,
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    item.ItemName.As("ItemName"),
                //prevBal.Quantity.As("PrevQty"),
                    query.Quantity.As("ActualQty"),
                    query.SRItemUnit//,
                //std.ItemName.As("ItemBinName")
                );
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.InnerJoin(trans).On(query.TransactionNo == trans.TransactionNo);
            query.LeftJoin(app).On(query.TransactionNo == app.TransactionNo && query.PageNo == app.PageNo);
            //query.InnerJoin(unit).On(trans.FromServiceUnitID == unit.ServiceUnitID);
            //query.InnerJoin(loc).On(trans.FromLocationID == loc.LocationID && unit.LocationID == loc.LocationID);

            //query.InnerJoin(ib).On(ib.LocationID == trans.FromLocationID && ib.ItemID == item.ItemID);
            //query.LeftJoin(std).On(ib.SRItemBin == std.ItemID && std.StandardReferenceID == AppEnum.StandardReference.ItemBin);

            if (trans.SRItemType == BusinessObject.Reference.ItemType.Medical)
            {
                query.Select("<CAST((CASE WHEN d.IsInventoryItem = 1 THEN 1 ELSE 0 END) AS INT) AS IsInventoryItem>");
                query.InnerJoin(medic).On(item.ItemID == medic.ItemID);

            }
            else if (trans.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
            {
                query.Select("<CAST((CASE WHEN e.IsInventoryItem = 1 THEN 1 ELSE 0 END) AS INT) AS IsInventoryItem>");
                query.InnerJoin(nonmedic).On(item.ItemID == nonmedic.ItemID);
            }
            else if (trans.SRItemType == BusinessObject.Reference.ItemType.Kitchen)
            {
                query.Select("<CAST((CASE WHEN h.IsInventoryItem = 1 THEN 1 ELSE 0 END) AS INT) AS IsInventoryItem>");
                query.InnerJoin(kitchen).On(item.ItemID == kitchen.ItemID);
            }

            //query.LeftJoin(prevBal).On(query.TransactionNo == prevBal.TransactionNo & query.ItemID == prevBal.ItemID);
            query.Where(query.TransactionNo == transactionNo, app.IsApproved.Coalesce("0") == false);
            query.OrderBy(query.TransactionNo.Ascending, query.PageNo.Ascending, query.SequenceNo.Ascending);
            return query;
        }

        public override void OnMenuExportToExcelClick(ValidateArgs args)
        {
            try
            {
                var items = grdList.MasterTableView.GetSelectedItems()[0];
                var table = GetDataGridDataTable(items.GetDataKeyValue("TransactionNo").ToString());
                if (table.Rows.Count > 0)
                {
                    var t = new ItemTransaction();
                    t.LoadByPrimaryKey(items.GetDataKeyValue("TransactionNo").ToString());

                    var u = new ServiceUnit();
                    u.LoadByPrimaryKey(t.FromServiceUnitID);

                    Common.CreateExcelFile.CreateExcelDocument(table, t.TransactionNo.Trim().Replace('/', '_') + ".xls", this.Response);
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                throw new Exception(error);
            }
            args.IsCancel = true;
        }
    }
}


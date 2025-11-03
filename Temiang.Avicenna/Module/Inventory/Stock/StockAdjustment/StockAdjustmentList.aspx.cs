using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Stock
{
    public partial class StockAdjustmentList : BasePageList
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
            UrlPageSearch = "StockAdjustmentSearch.aspx" + (string.IsNullOrEmpty(FormType) ? "" : "?type=" + FormType);
            UrlPageDetail = "StockAdjustmentDetail.aspx?type=" + FormType;

            ProgramID = FormType == "p" ? AppConstant.Program.StockAdjustmentPlus : AppConstant.Program.StockAdjustment; 

            WindowSearch.Height = 400;
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
            //string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            string url = string.Format("StockAdjustmentDetail.aspx?md={0}&id={1}&type={2}", mode, id, FormType);
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
                    var adjustment = new AppStandardReferenceItemQuery("e");
                    var loc = new LocationQuery("f");
                    var usr = new AppUserServiceUnitQuery("u");

                    query.Select
                        (
                            query.TransactionNo,
                            query.TransactionDate,
                            unit.ServiceUnitName.As("FromServiceUnitID"),
                            loc.LocationName.As("FromLocationID"),
                            type.ItemName.As("SRItemType"),
                            adjustment.ItemName.As("SRAdjustmentType"),
                            query.Notes,
                            query.IsApproved,
                            query.IsVoid
                        );
                    query.InnerJoin(unit).On(query.FromServiceUnitID == unit.ServiceUnitID);
                    query.InnerJoin(loc).On(query.FromLocationID == loc.LocationID);
                    query.InnerJoin(type).On
                        (
                            query.SRItemType == type.ItemID &
                            type.StandardReferenceID == "ItemType"
                        );
                    query.InnerJoin(adjustment).On
                        (
                            query.SRAdjustmentType == adjustment.ItemID &
                            adjustment.StandardReferenceID == "AdjustmentType"
                        );
                    query.InnerJoin(usr).On(query.FromServiceUnitID == usr.ServiceUnitID &
                                    usr.UserID == AppSession.UserLogin.UserID);

                    query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.StockAdjustment);
                    if (!string.IsNullOrEmpty(FormType))
                    {
                        var iti = new ItemTransactionItemQuery("iti");
                        query.InnerJoin(iti).On(query.TransactionNo == iti.TransactionNo);
                        if (FormType == "p")
                            query.Where(iti.Quantity > 0);
                        else query.Where(iti.Quantity < 0);
                        query.GroupBy(
                            query.TransactionNo,
                            query.TransactionDate,
                            unit.ServiceUnitName,
                            loc.LocationName,
                            type.ItemName,
                            adjustment.ItemName,
                            query.Notes,
                            query.IsApproved,
                            query.IsVoid);
                    }
                    
                    query.OrderBy(query.TransactionNo.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;

                return dtb;
            }
        }
    }
}


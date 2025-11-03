using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LinenItemsExterminationList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "LinenItemsExterminationSearch.aspx";
            UrlPageDetail = "LinenItemsExterminationDetail.aspx";

            ProgramID = AppConstant.Program.LinenItemsExtermination;

            WindowSearch.Height = 400;
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
                    var loc = new LocationQuery("e");
                    var usr = new AppUserServiceUnitQuery("u");

                    query.Select
                        (
                            query.TransactionNo,
                            query.TransactionDate,
                            unit.ServiceUnitName.As("FromServiceUnitID"),
                            loc.LocationName,
                            type.ItemName.As("SRItemType"),
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
                    query.InnerJoin(usr).On(query.FromServiceUnitID == usr.ServiceUnitID &
                                    usr.UserID == AppSession.UserLogin.UserID);

                    query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.LinenItemsExtermination);
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
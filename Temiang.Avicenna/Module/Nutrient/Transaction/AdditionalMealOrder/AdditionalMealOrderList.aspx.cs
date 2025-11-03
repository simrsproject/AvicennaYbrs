using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class AdditionalMealOrderList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "AdditionalMealOrderSearch.aspx";
            UrlPageDetail = "AdditionalMealOrderDetail.aspx";

            ProgramID = AppConstant.Program.ExtraMealOrder;

            this.WindowSearch.Height = 400;
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
            string id = dataItem.GetDataKeyValue(AddMealOrderMetadata.ColumnNames.OrderNo).ToString();
            Page.Response.Redirect("AdditionalMealOrderDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = AddMealOrders;
        }

        private DataTable AddMealOrders
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                AddMealOrderQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (AddMealOrderQuery)Session[SessionNameForQuery];
                else
                {
                    query = new AddMealOrderQuery("a");
                    var menu = new MenuQuery("b");
                    var set = new AppStandardReferenceItemQuery("c");
                    query.InnerJoin(menu).On(query.MenuID == menu.MenuID);
                    query.InnerJoin(set).On(query.SRMealSet == set.ItemID &&
                                            set.StandardReferenceID == AppEnum.StandardReference.MealSet);
                    query.Select
                        (
                            query.OrderNo,
                            query.OrderDate,
                            query.EffectiveDate,
                            query.MenuID,
                            menu.MenuName,
                            set.ItemName.As("MealSet"),
                            query.Qty,
                            query.Notes,
                            query.IsApproved,
                            query.IsVoid
                        );
                    query.OrderBy(query.OrderNo.Descending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}

using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class EventMealOrderList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "EventMealOrderSearch.aspx";
            UrlPageDetail = "EventMealOrderDetail.aspx";

            ProgramID = AppConstant.Program.EventMealOrder;
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
            string id = dataItem.GetDataKeyValue(EventMealOrderMetadata.ColumnNames.OrderNo).ToString();
            Page.Response.Redirect("EventMealOrderDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EventMealOrders;
        }

        private DataTable EventMealOrders
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                EventMealOrderQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (EventMealOrderQuery)Session[SessionNameForQuery];
                else
                {
                    query = new EventMealOrderQuery("a");
                    query.Select
                        (
                            query.OrderNo,
                            query.OrderDate,
                            query.EventDate,
                            query.EventTime,
                            query.EventName,
                            query.Pic,
                            query.NoOfParticipant,
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

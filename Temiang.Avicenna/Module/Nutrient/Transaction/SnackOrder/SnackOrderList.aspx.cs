using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class SnackOrderList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "SnackOrderSearch.aspx";
            UrlPageDetail = "SnackOrderDetail.aspx";

            ProgramID = AppConstant.Program.SnackOrder;
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
            string id = dataItem.GetDataKeyValue(SnackOrderMetadata.ColumnNames.SnackOrderNo).ToString();
            Page.Response.Redirect("SnackOrderDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = SnackOrders;
        }

        private DataTable SnackOrders
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                SnackOrderQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (SnackOrderQuery)Session[SessionNameForQuery];
                else
                {
                    query = new SnackOrderQuery("a");
                    var unit = new ServiceUnitQuery("b");
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Select
                        (
                            query.SnackOrderNo,
                            query.SnackOrderDate,
                            query.SnackOrderForDate,
                            unit.ServiceUnitName,
                            query.IsApproved,
                            query.IsVoid
                        );
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                    query.OrderBy(query.SnackOrderNo.Descending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string transNo = dataItem.GetDataKeyValue("SnackOrderNo").ToString();

            //Load record
            var query = new SnackOrderItemQuery("a");
            var snack = new SnackQuery("b");

            query.InnerJoin(snack).On(query.SnackID == snack.SnackID);
            query.OrderBy(query.SnackID.Ascending);

            query.Select(
                query,
                snack.SnackName
                );
            query.Where(query.SnackOrderNo == transNo);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}

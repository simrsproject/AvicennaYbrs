using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class MarginList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageSearch = "MarginSearch.aspx";
            UrlPageDetail = "MarginDetail.aspx";

            ProgramID = AppConstant.Program.MARGIN;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
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
            string id = dataItem.GetDataKeyValue(ItemProductMarginMetadata.ColumnNames.MarginID).ToString();
            Page.Response.Redirect("MarginDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = ItemProductMargins;
        }

        private DataTable ItemProductMargins
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ItemProductMarginQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ItemProductMarginQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ItemProductMarginQuery("a");
                    query.Select(
                                    query.MarginID,
                                    query.MarginName,
                                    query.IsActive
                                );
                    query.OrderBy(query.MarginID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "MarginName", "MarginID");
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
            string id = dataItem.GetDataKeyValue("MarginID").ToString();

            //Load record
            ItemProductMarginValueCollection items = new ItemProductMarginValueCollection();
            items.QueryReset();
            items.Query.Where(items.Query.MarginID == id);
            items.LoadAll();

            //Apply
            e.DetailTableView.DataSource = items;
        }
    }
}
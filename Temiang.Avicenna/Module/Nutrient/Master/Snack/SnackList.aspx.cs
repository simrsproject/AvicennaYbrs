using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class SnackList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "SnackSearch.aspx";
            UrlPageDetail = "SnackDetail.aspx";

            ProgramID = AppConstant.Program.Snack;

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
            string id = dataItem.GetDataKeyValue(SnackMetadata.ColumnNames.SnackID).ToString();
            Page.Response.Redirect("SnackDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Snacks;
        }

        private DataTable Snacks
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                SnackQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (SnackQuery)Session[SessionNameForQuery];
                else
                {
                    query = new SnackQuery();
                    query.Select
                        (
                            query.SnackID,
                            query.SnackName,
                            query.IsActive
                        );
                    query.OrderBy(query.SnackID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "SnackName", "SnackID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}

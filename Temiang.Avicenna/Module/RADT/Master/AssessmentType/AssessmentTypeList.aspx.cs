using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class AssessmentTypeBodyDiagramList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "AssessmentTypeSearch.aspx";
            UrlPageDetail = "AssessmentTypeDetail.aspx";

            ProgramID = AppConstant.Program.AsessmentType;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
        }

        public override void OnMenuEditClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(AppStandardReferenceItemMetadata.ColumnNames.ItemID).ToString();
            Page.Response.Redirect(UrlPageDetail + "?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = AssessmentTypeBodyDiagrams;
        }

        private DataTable AssessmentTypeBodyDiagrams
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                AppStandardReferenceItemQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (AppStandardReferenceItemQuery)Session[SessionNameForQuery];
                else
                {
                    query = new AppStandardReferenceItemQuery("a");
                    query.Where(query.StandardReferenceID == "AssessmentType");
                    query.Select(query.ItemID, query.ItemName, query.IsActive);
                    query.OrderBy(query.ItemName.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "ItemName", "ItemID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}


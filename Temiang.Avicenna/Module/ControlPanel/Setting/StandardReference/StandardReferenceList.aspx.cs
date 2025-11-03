using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ControlPanel.Setting
{
    public partial class StandardReferenceList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageSearch = "StandardReferenceSearch.aspx";
            UrlPageDetail = "StandardReferenceDetail.aspx";

            WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.StandardReference;

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
            string id = dataItem.GetDataKeyValue(AppStandardReferenceMetadata.ColumnNames.StandardReferenceID).ToString();
            Page.Response.Redirect("StandardReferenceDetail.aspx?md=" + mode + "&id=" + id, true);
        }
        private DataTable AppStandardReferences
        {
            get
            {
                object obj = Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                AppStandardReferenceQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (AppStandardReferenceQuery)Session[SessionNameForQuery];
                else
                {
                    query = new AppStandardReferenceQuery();
                    //Quick Search
                    ApplyQuickSearch(query);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                grdList.DataSource = AppStandardReferences;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string id = dataItem.GetDataKeyValue("StandardReferenceID").ToString();
            //Load record
            AppStandardReferenceItemQuery query = new AppStandardReferenceItemQuery();
            query.Where(query.StandardReferenceID == id);
            DataTable dtb = query.LoadDataTable();
            //Apply
            e.DetailTableView.DataSource = dtb;
        }
    }
}
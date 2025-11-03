using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.ControlPanel.Admin
{
    public partial class UserGroupList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageSearch = "UserGroupSearch.aspx";
            UrlPageDetail = "UserGroupDetail.aspx";

            ProgramID = AppConstant.Program.UserGroup;

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
            string id = dataItem.GetDataKeyValue(AppUserGroupMetadata.ColumnNames.UserGroupID).ToString();
            Page.Response.Redirect("UserGroupDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = AppUserGroups;
        }

        private DataTable AppUserGroups
        {
            get
            {
                object obj = Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                AppUserGroupQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (AppUserGroupQuery)Session[SessionNameForQuery];
                else
                {
                    query = new AppUserGroupQuery();
                    //Quick Search
                    ApplyQuickSearch(query);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
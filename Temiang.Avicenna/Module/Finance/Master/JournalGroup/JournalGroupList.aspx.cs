using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class JournalGroupList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "JournalGroupSearch.aspx";
            UrlPageDetail = "JournalGroupDetail.aspx";

            WindowSearch.Height = 200;

            ProgramID = AppConstant.Program.JournalModuleGroup;

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

        private void RedirectToPageDetail(Telerik.Web.UI.GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(JournalGroupMetadata.ColumnNames.JournalGroupID).ToString();
            Page.Response.Redirect("JournalGroupDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = JournalGroups;
        }

        private DataTable JournalGroups
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                JournalGroupQuery query;
                if (Session[SessionNameForQuery] != null) query = (JournalGroupQuery)Session[SessionNameForQuery];
                else
                {
                    query = new JournalGroupQuery("a");
                    query.OrderBy(query.JournalGroupID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();

                Session[SessionNameForList] = dtb;
                
                return dtb;
            }
        }
    }
}
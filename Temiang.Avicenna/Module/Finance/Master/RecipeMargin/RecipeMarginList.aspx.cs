using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class RecipeMarginList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "RecipeMarginSearch.aspx";
            UrlPageDetail = "RecipeMarginDetail.aspx";

            WindowSearch.Height = 170;

            ProgramID = AppConstant.Program.RecipeMargin;
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
            string id = dataItem.GetDataKeyValue(RecipeMarginValueMetadata.ColumnNames.CounterID).ToString();
            Page.Response.Redirect("RecipeMarginDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = RecipeMargins;
        }

        private DataTable RecipeMargins
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                RecipeMarginValueQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (RecipeMarginValueQuery)Session[SessionNameForQuery];
                else
                {
                    query = new RecipeMarginValueQuery();
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

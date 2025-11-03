using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ContributoryFactorsClassificationFrameworkList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "ContributoryFactorsClassificationFrameworkSearch.aspx";
            UrlPageDetail = "ContributoryFactorsClassificationFrameworkDetail.aspx";

            ProgramID = AppConstant.Program.ContributoryFactorsClassificationFramework;

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
            string id = dataItem.GetDataKeyValue(ContributoryFactorsClassificationFrameworkMetadata.ColumnNames.FactorID).ToString();
            Page.Response.Redirect("ContributoryFactorsClassificationFrameworkDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ContributoryFactorsClassificationFrameworks;
        }

        private DataTable ContributoryFactorsClassificationFrameworks
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ContributoryFactorsClassificationFrameworkQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ContributoryFactorsClassificationFrameworkQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ContributoryFactorsClassificationFrameworkQuery();
                    query.OrderBy(query.FactorID.Ascending);

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

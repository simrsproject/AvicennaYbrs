using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master.AcctParameter
{
    public partial class AcctParameterList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "AcctParameterSearch.aspx";
            UrlPageDetail = "AcctParameterDetail.aspx";

            ProgramID = AppConstant.Program.AccountingParameter;

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
            string id = dataItem.GetDataKeyValue(AppParameterMetadata.ColumnNames.ParameterID).ToString();
            Page.Response.Redirect("AcctParameterDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = GetDataSource;
        }

        private DataTable GetDataSource
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                AppParameterQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (AppParameterQuery)Session[SessionNameForQuery];
                else
                {
                    query = new AppParameterQuery();
                    //Quick Search
                    ApplyQuickSearch(query);
                }
                query.Where(
                                query.IsUsedBySystem == false,
                                query.Or(query.ParameterID.Like("coa%"), query.ParameterID.Like("acc%"))
                           );
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}

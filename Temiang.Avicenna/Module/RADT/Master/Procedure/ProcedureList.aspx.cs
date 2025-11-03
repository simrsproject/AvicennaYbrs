using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ProcedureList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageSearch = "ProcedureSearch.aspx";
            UrlPageDetail = "ProcedureDetail.aspx";

            ProgramID = AppConstant.Program.Procedure;
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
            string id = dataItem.GetDataKeyValue(ProcedureMetadata.ColumnNames.ProcedureID).ToString();
            Page.Response.Redirect("ProcedureDetail.aspx?md=" + mode + "&id=" + id, true);
        }
        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Procedure;
        }

        private DataTable Procedure
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                ProcedureQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ProcedureQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ProcedureQuery();
                    query.OrderBy(query.ProcedureID.Ascending);
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


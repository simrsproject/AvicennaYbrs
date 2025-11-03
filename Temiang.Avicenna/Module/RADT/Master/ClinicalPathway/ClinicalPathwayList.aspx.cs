using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ClinicalPathwayList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ClinicalPathway;

            UrlPageSearch = "ClinicalPathwaySearch.aspx";
            UrlPageDetail = "ClinicalPathwayDetail.aspx";

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
            string id = dataItem.GetDataKeyValue(PathwayMetadata.ColumnNames.PathwayID).ToString();
            Page.Response.Redirect("ClinicalPathwayDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Pathways;
        }

        private DataTable Pathways
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null) return ((DataTable)(obj));

                PathwayQuery query;
                if (Session[SessionNameForQuery] != null) query = (PathwayQuery)Session[SessionNameForQuery];
                else
                {
                    query = new PathwayQuery("a");
                    query.OrderBy(query.StartingDate.Ascending, query.PathwayID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new PathwayDiagnoseItemQuery("a");
            var diag = new DiagnoseQuery("b");

            query.Select(query.PathwayID, diag.DiagnoseID, diag.DiagnoseName);
            query.InnerJoin(diag).On(query.DiagnoseID == diag.DiagnoseID);
            query.Where(query.PathwayID == e.DetailTableView.ParentItem.GetDataKeyValue("PathwayID").ToString());

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}

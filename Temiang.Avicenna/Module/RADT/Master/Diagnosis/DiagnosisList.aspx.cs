using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class DiagnosisList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "DiagnosisSearch.aspx";
            UrlPageDetail = "DiagnosisDetail.aspx";

            ProgramID = AppConstant.Program.Diagnosis;

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
            string id = dataItem.GetDataKeyValue(DiagnoseMetadata.ColumnNames.DiagnoseID).ToString();
            id = id.Replace(" ", "s").Replace("+", "p").Replace(",", "c");
            Page.Response.Redirect("DiagnosisDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Diagnoses;
        }

        private DataTable Diagnoses
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                DiagnoseQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (DiagnoseQuery)Session[SessionNameForQuery];
                else
                {
                    query = new DiagnoseQuery("a");
                    var dtdq = new DtdQuery("b");
                    query.InnerJoin(dtdq).On(query.DtdNo == dtdq.DtdNo);
                    query.Select(query.DiagnoseID, query.DiagnoseName, query.DtdNo, dtdq.DtdName, query.IsChronicDisease,
                                 query.IsDisease, query.IsActive);
                    query.OrderBy(query.DiagnoseID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "DiagnoseName", "DiagnoseID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}

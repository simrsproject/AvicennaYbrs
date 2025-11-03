using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.TrainingHR.Master.AssessmentCriteria
{
    public partial class AssessmentCriteriaList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "AssessmentCriteriaSearch.aspx";
            UrlPageDetail = "AssessmentCriteriaDetail.aspx";

            ProgramID = AppConstant.Program.EmployeeTrainingAssessmentCriteria;

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
            string id = dataItem.GetDataKeyValue(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.AssessmentCriteriaID).ToString();
            Page.Response.Redirect("AssessmentCriteriaDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeeTrainingAssessmentCriterias;
        }

        private DataTable EmployeeTrainingAssessmentCriterias
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                EmployeeTrainingAssessmentCriteriaQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (EmployeeTrainingAssessmentCriteriaQuery)Session[SessionNameForQuery];
                else
                {
                    query = new EmployeeTrainingAssessmentCriteriaQuery("a");
                    query.Select(query);
                    query.OrderBy(query.MinValue.Descending);

                    //Quick Search
                    ApplyQuickSearch(query, "AssessmentCriteriaName", "AssessmentCriteriaID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
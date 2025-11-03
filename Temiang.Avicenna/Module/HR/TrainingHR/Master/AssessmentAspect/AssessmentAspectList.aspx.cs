using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.TrainingHR.Master.AssessmentAspect
{
    public partial class AssessmentAspectList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "AssessmentAspectSearch.aspx";
            UrlPageDetail = "AssessmentAspectDetail.aspx";

            ProgramID = AppConstant.Program.EmployeeTrainingAssessmentAspect;

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
            string id = dataItem.GetDataKeyValue(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.AssessmentAspectID).ToString();
            Page.Response.Redirect("AssessmentAspectDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = TrainingAssessmentAspects;
        }

        private DataTable TrainingAssessmentAspects
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                EmployeeTrainingAssessmentAspectQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (EmployeeTrainingAssessmentAspectQuery)Session[SessionNameForQuery];
                else
                {
                    query = new EmployeeTrainingAssessmentAspectQuery("a");
                    query.Select(query);
                    query.OrderBy(query.AssessmentAspectID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "AssessmentAspectName", "AssessmentAspectID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
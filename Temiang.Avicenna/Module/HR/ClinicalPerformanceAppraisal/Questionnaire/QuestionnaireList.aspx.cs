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

namespace Temiang.Avicenna.Module.HR.CPA
{
    public partial class QuestionnaireList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ClinicalPerformanceAppraisalQuestionnaire;

            UrlPageSearch = "QuestionnaireSearch.aspx";
            UrlPageDetail = "QuestionnaireDetail.aspx";

            WindowSearch.Height = 400;

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
            string id = dataItem.GetDataKeyValue(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.QuestionnaireID).ToString();
            Page.Response.Redirect("QuestionnaireDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ClinicalPerformanceAppraisalQuestionnaires;
        }

        private DataTable ClinicalPerformanceAppraisalQuestionnaires
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                ClinicalPerformanceAppraisalQuestionnaireQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (ClinicalPerformanceAppraisalQuestionnaireQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new ClinicalPerformanceAppraisalQuestionnaireQuery("a");
                    query.Select(query);
                    query.OrderBy(query.QuestionnaireCode.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "QuestionnaireCode", "QuestionnaireName");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
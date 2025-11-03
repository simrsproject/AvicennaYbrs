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

namespace Temiang.Avicenna.Module.HR.Appraisal
{
    public partial class QuestionerList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AppraisalQuestioner;

            UrlPageSearch = "QuestionerSearch.aspx";
            UrlPageDetail = "QuestionerDetail.aspx";

            WindowSearch.Height = 400;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;

            if (!IsPostBack)
            {
            }
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
            string id = dataItem.GetDataKeyValue(AppraisalQuestionMetadata.ColumnNames.QuestionerID).ToString();
            Page.Response.Redirect("QuestionerDetail.aspx?md=" + mode + "&id=" + id, true);
        }
        
        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = AppraisalQuestions;
        }

        private DataTable AppraisalQuestions
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                AppraisalQuestionQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (AppraisalQuestionQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new AppraisalQuestionQuery("a");
                    var type = new AppStandardReferenceItemQuery("b");
                    query.Select(query, type.ItemName.As("AppraisalTypeName"));
                    query.LeftJoin(type).On(type.StandardReferenceID == AppEnum.StandardReference.AppraisalType && type.ItemID == query.SRAppraisalType);
                    query.OrderBy(query.QuestionerID.Ascending, query.PeriodYear.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "QuestionerName", "PeriodYear");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
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

namespace Temiang.Avicenna.Module.HR.Credential.Questionnaire
{
    public partial class QuestionnaireList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CredentialQuestionnaire;

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
            string id = dataItem.GetDataKeyValue(CredentialQuestionnaireMetadata.ColumnNames.QuestionnaireID).ToString();
            Page.Response.Redirect("QuestionnaireDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = CredentialQuestionnaires;
        }

        private DataTable CredentialQuestionnaires
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                CredentialQuestionnaireQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (CredentialQuestionnaireQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new CredentialQuestionnaireQuery("a");
                    var profession = new AppStandardReferenceItemQuery("b");
                    var area = new AppStandardReferenceItemQuery("c");
                    var level = new AppStandardReferenceItemQuery("d");
                    query.Select(query, profession.ItemName.As("ProfessionGroupName"), area.ItemName.As("ClinicalWorkAreaName"), level.ItemName.As("ClinicalAuthorityLevelName"));
                    query.InnerJoin(profession).On(profession.StandardReferenceID == AppEnum.StandardReference.ProfessionGroup && profession.ItemID == query.SRProfessionGroup);
                    query.InnerJoin(area).On(area.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea && area.ItemID == query.SRClinicalWorkArea);
                    query.InnerJoin(level).On(level.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel && level.ItemID == query.SRClinicalAuthorityLevel);
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
using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.CPA
{
    public partial class ScoresheetList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 400;

            UrlPageSearch = "ScoresheetSearch.aspx";
            UrlPageDetail = "ScoresheetDetail.aspx";

            ProgramID = AppConstant.Program.ClinicalPerformanceAppraisalScoresheet;

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
            string id = dataItem.GetDataKeyValue(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ScoresheetNo).ToString();
            string url = string.Format("ScoresheetDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ClinicalPerformanceAppraisalQuestionnaireScoresheets;
        }

        private DataTable ClinicalPerformanceAppraisalQuestionnaireScoresheets
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery("a");
                    var personal = new PersonalInfoQuery("b");
                    var evaluator = new PersonalInfoQuery("c");
                    var profession = new AppStandardReferenceItemQuery("d");
                    var area = new AppStandardReferenceItemQuery("e");
                    var level = new AppStandardReferenceItemQuery("f");

                    query.InnerJoin(personal).On(personal.PersonID == query.PersonID);
                    query.InnerJoin(evaluator).On(evaluator.PersonID == query.EvaluatorID);
                    query.LeftJoin(profession).On(profession.StandardReferenceID == AppEnum.StandardReference.ProfessionGroup.ToString() & profession.ItemID == query.SRProfessionGroup);
                    query.LeftJoin(area).On(area.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea.ToString() & area.ItemID == query.SRClinicalWorkArea);
                    query.LeftJoin(level).On(level.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel.ToString() & level.ItemID == query.SRClinicalAuthorityLevel);
                    query.Where(query.EvaluatorID == AppSession.UserLogin.PersonID.ToInt());
                    query.OrderBy
                        (
                            query.ScoringDate.Descending
                        );

                    query.Select(
                        query.ScoresheetNo,
                        query.ScoringDate,
                        evaluator.EmployeeName.As("EvaluatorName"),
                        query.PersonID,
                        personal.EmployeeNumber,
                        personal.EmployeeName,
                        profession.ItemName.As("ProfessionGroupName"),
                        area.ItemName.As("ClinicalWorkAreaName"),
                        level.ItemName.As("ClinicalAuthorityLevelName"),
                        query.IsApproved,
                        query.IsVoid
                        );
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
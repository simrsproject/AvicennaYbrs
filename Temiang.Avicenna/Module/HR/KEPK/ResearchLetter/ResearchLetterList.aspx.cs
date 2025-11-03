using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.KEPK
{
    public partial class ResearchLetterList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ResearchLetterSearch.aspx";
            UrlPageDetail = "ResearchLetterDetail.aspx";

            ProgramID = AppConstant.Program.KEPK_ResearchLetter;

            WindowSearch.Height = 400;
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
            string id = dataItem.GetDataKeyValue(ResearchLetterMetadata.ColumnNames.LetterID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", "ResearchLetterDetail.aspx", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ResearchLetters;
        }

        private DataTable ResearchLetters
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                ResearchLetterQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (ResearchLetterQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new ResearchLetterQuery("a");
                    var desicion = new AppStandardReferenceItemQuery("b");
                    var institution = new AppStandardReferenceItemQuery("c");
                    var faculty = new AppStandardReferenceItemQuery("d");
                    var majors = new AppStandardReferenceItemQuery("e");
                    var degree = new AppStandardReferenceItemQuery("f");
                    var reviewer = new AppStandardReferenceItemQuery("g");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                        query.LetterID,
                        query.ResearcherName,
                        query.LetterNo,
                        query.LetterDate,
                        query.Subject,
                        query.SRResearchDecision,
                        desicion.ItemName.As("ResearchDecisionName"),
                        query.Attachment,
                        query.SRResearchInstitution,
                        institution.ItemName.As("ResearchInstitutionName"),
                        query.SRResearchFaculty,
                        faculty.ItemName.As("ResearchFacultyName"),
                        query.SRResearchMajors,
                        majors.ItemName.As("ResearchMajorsName"),
                        query.SREducationDegree,
                        degree.ItemName.As("EducationDegreeName"),
                        query.IsUpload,
                        query.ReviewTime,
                        query.SRResearchReviewerName,
                        reviewer.ItemName.As("ResearchReviewerName"),
                        query.IsVoid
                        );

                    query.LeftJoin(desicion).On(desicion.StandardReferenceID == AppEnum.StandardReference.ResearchDecision && desicion.ItemID == query.SRResearchDecision);
                    query.LeftJoin(institution).On(institution.StandardReferenceID == AppEnum.StandardReference.ResearchInstitution && institution.ItemID == query.SRResearchInstitution);
                    query.LeftJoin(faculty).On(faculty.StandardReferenceID == AppEnum.StandardReference.ResearchFaculty && faculty.ItemID == query.SRResearchFaculty);
                    query.LeftJoin(majors).On(majors.StandardReferenceID == AppEnum.StandardReference.ResearchMajors && majors.ItemID == query.SRResearchMajors);
                    query.LeftJoin(degree).On(degree.StandardReferenceID == AppEnum.StandardReference.EducationDegree && degree.ItemID == query.SREducationDegree);
                    query.LeftJoin(reviewer).On(reviewer.StandardReferenceID == AppEnum.StandardReference.ResearchReviewerName && reviewer.ItemID == query.SRResearchReviewerName);

                    query.OrderBy(query.LetterID.Descending);
                }

                DataTable dtb = query.LoadDataTable();

                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
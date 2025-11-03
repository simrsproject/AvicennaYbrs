using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.KEPK
{
    public partial class ResearchLetterSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.KEPK_ResearchLetter;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRResearchDecision, AppEnum.StandardReference.ResearchDecision);
                StandardReference.InitializeIncludeSpace(cboSRResearchInstitution, AppEnum.StandardReference.ResearchInstitution);
                StandardReference.InitializeIncludeSpace(cboSRResearchFaculty, AppEnum.StandardReference.ResearchFaculty);
                StandardReference.InitializeIncludeSpace(cboSRResearchReviewerName, AppEnum.StandardReference.ResearchReviewerName);
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ResearchLetterQuery("a");
            var desicion = new AppStandardReferenceItemQuery("b");
            var institution = new AppStandardReferenceItemQuery("c");
            var faculty = new AppStandardReferenceItemQuery("d");
            var majors = new AppStandardReferenceItemQuery("e");
            var degree = new AppStandardReferenceItemQuery("f");
            var reviewer = new AppStandardReferenceItemQuery("g");

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

            bool isMax = true;

            if (!txtLetterDateFrom.IsEmpty && !txtLetterDateTo.IsEmpty)
            {
                query.Where(query.LetterDate >= txtLetterDateFrom.SelectedDate, query.LetterDate <= txtLetterDateTo.SelectedDate);
            }
            if (!string.IsNullOrEmpty(txtResearcherName.Text))
            {
                if (cboFilterResearcherName.SelectedIndex == 1)
                    query.Where(query.ResearcherName == txtResearcherName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtResearcherName.Text);
                    query.Where(query.ResearcherName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtLetterNo.Text))
            {
                if (cboFilterLetterNo.SelectedIndex == 1)
                    query.Where(query.LetterNo == txtLetterNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtLetterNo.Text);
                    query.Where(query.LetterNo.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSRResearchDecision.SelectedValue))
            {
                query.Where(query.SRResearchDecision == cboSRResearchDecision.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboSRResearchInstitution.SelectedValue))
            {
                query.Where(query.SRResearchInstitution == cboSRResearchInstitution.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboSRResearchFaculty.SelectedValue))
            {
                query.Where(query.SRResearchFaculty == cboSRResearchFaculty.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboSRResearchReviewerName.SelectedValue))
            {
                query.Where(query.SRResearchReviewerName == cboSRResearchReviewerName.SelectedValue);
            }

            if (isMax)
                query.es.Top = AppSession.Parameter.MaxResultRecord;

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
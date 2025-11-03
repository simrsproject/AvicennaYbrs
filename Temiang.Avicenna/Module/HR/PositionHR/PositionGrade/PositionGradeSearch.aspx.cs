using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionGradeSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PositionGrade; //TODO: Isi ProgramID

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSREmploymentType, AppEnum.StandardReference.EmploymentType);
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new PositionGradeQuery("a");
            var et = new AppStandardReferenceItemQuery("b");

            query.LeftJoin(et).On(et.StandardReferenceID == "EmploymentType" && et.ItemID == query.SREmploymentType);

            query.Select(
                                    query.PositionGradeID,
                                    query.PositionGradeCode,
                                    query.PositionGradeName,
                                    query.Interval,
                                    query.Ranking,
                                    query.RankName,
                                    et.ItemName.As("EmploymentTypeName"),
                                    query.LastUpdateDateTime,
                                    query.LastUpdateByUserID
                                );
            if (!string.IsNullOrEmpty(txtPositionGradeCode.Text))
            {
                if (cboFilterPositionGradeCode.SelectedIndex == 1)
                    query.Where(query.PositionGradeCode == txtPositionGradeCode.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPositionGradeCode.Text);
                    query.Where(query.PositionGradeCode.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtPositionGradeName.Text))
            {
                if (cboFilterPositionGradeName.SelectedIndex == 1)
                    query.Where(query.PositionGradeName == txtPositionGradeName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPositionGradeName.Text);
                    query.Where(query.PositionGradeName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSREmploymentType.SelectedValue))
                query.Where(query.SREmploymentType == cboSREmploymentType.SelectedValue);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}

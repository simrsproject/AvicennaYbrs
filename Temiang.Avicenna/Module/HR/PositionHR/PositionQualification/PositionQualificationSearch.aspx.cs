using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionQualificationSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PositionQualification; //TODO: Isi ProgramID
            
            if (!IsPostBack)
            {
                trPositionGradeName.Visible = false;
                trPositionLevelName.Visible = false;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }

        public override bool OnButtonOkClicked()
        {
            var query = new PositionQuery("a");
            var PsLevel = new PositionLevelQuery("d");
            var PsGrade = new PositionGradeQuery("c");

            query.LeftJoin(PsLevel).On(query.PositionLevelID == PsLevel.PositionLevelID);
            query.LeftJoin(PsGrade).On(query.PositionGradeID == PsGrade.PositionGradeID);
            query.Select
                (
                    query.PositionID,
                            query.PositionCode,
                            query.PositionName,
                            query.Summary,
                            query.PositionGradeID,
                            PsGrade.PositionGradeName,
                            query.PositionLevelID,
                            PsLevel.PositionLevelName,
                            query.ValidFrom,
                            query.ValidTo,
                            query.LastUpdateDateTime,
                            query.LastUpdateByUserID
                );

            if (!string.IsNullOrEmpty(txtPositionCode.Text))
            {
                if (cboFilterPositionCode.SelectedIndex == 1)
                    query.Where(query.PositionCode == txtPositionCode.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPositionCode.Text);
                    query.Where(query.PositionCode.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtPositionName.Text))
            {
                if (cboFilterPositionName.SelectedIndex == 1)
                    query.Where(query.PositionName == txtPositionName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPositionName.Text);
                    query.Where(query.PositionName.Like(searchTextContain));
                }
            }

            if (!string.IsNullOrEmpty(txtPositionGradeName.Text))
            {
                if (cboFilterPositionGradeName.SelectedIndex == 1)
                    query.Where(PsGrade.PositionGradeName == txtPositionGradeName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPositionGradeName.Text);
                    query.Where(PsGrade.PositionGradeName.Like(searchTextContain));
                }
            }

            if (!string.IsNullOrEmpty(txtPositionLevelName.Text))
            {
                if (cboFilterPositionLevelName.SelectedIndex == 1)
                    query.Where(PsLevel.PositionLevelName == txtPositionLevelName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPositionLevelName.Text);
                    query.Where(PsLevel.PositionLevelName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}

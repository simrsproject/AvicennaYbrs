using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionLevelSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PositionLevel; //TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var query = new PositionLevelQuery();
            if (!string.IsNullOrEmpty(txtPositionLevelCode.Text))
            {
                if (cboFilterPositionLevelCode.SelectedIndex == 1)
                    query.Where(query.PositionLevelCode == txtPositionLevelCode.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPositionLevelCode.Text);
                    query.Where(query.PositionLevelCode.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtPositionLevelName.Text))
            {
                if (cboFilterPositionLevelName.SelectedIndex == 1)
                    query.Where(query.PositionLevelName == txtPositionLevelName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPositionLevelName.Text);
                    query.Where(query.PositionLevelName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}

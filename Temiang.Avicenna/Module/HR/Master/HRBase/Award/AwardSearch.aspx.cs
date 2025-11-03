using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Master
{
    public partial class AwardSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Award; //TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var query = new AwardQuery();
            if (!string.IsNullOrEmpty(txtAwardCode.Text))
            {
                if (cboFilterAwardCode.SelectedIndex == 1)
                    query.Where(query.AwardCode == txtAwardCode.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtAwardCode.Text);
                    query.Where(query.AwardCode.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(txtAwardName.Text))
            {
                if (cboFilterAwardName.SelectedIndex == 1)
                    query.Where(query.AwardName == txtAwardName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtAwardName.Text);
                    query.Where(query.AwardName.Like(searchText));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}

using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ConsumeMethodSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ConsumeMethod;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ConsumeMethodQuery();
            if (!string.IsNullOrEmpty(txtSRConsumeMethodName.Text))
            {
                if (cboFilterSRConsumeMethodName.SelectedIndex == 1)
                    query.Where(query.SRConsumeMethodName == txtSRConsumeMethodName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtSRConsumeMethodName.Text);
                    query.Where(query.SRConsumeMethodName.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(txtTimeSequence.Text))
            {
                if (cboFilterSRConsumeMethodName.SelectedIndex == 1)
                    query.Where(query.TimeSequence == txtTimeSequence.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtTimeSequence.Text);
                    query.Where(query.TimeSequence.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(txtSygnaText.Text))
            {
                if (cboFilterSRConsumeMethodName.SelectedIndex == 1)
                    query.Where(query.SygnaText == txtSygnaText.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtSygnaText.Text);
                    query.Where(query.SygnaText.Like(searchText));
                }
            }
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}

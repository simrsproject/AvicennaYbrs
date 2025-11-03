using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.ControlPanel.Setting
{
    public partial class AppAutoNumberSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.AutoNumbering;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new AppAutoNumberQuery();
            if (!string.IsNullOrEmpty(txtSRAutoNumber.Text))
            {
                if (cboFilterSRAutoNumber.SelectedIndex == 1)
                    query.Where(query.SRAutoNumber == txtSRAutoNumber.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtSRAutoNumber.Text);
                    query.Where(query.SRAutoNumber.Like(searchText));
                }
            }
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}

using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Cssd.Master
{
    public partial class MachineSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.CssdMachine;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new CssdMachineQuery();
            if (!string.IsNullOrEmpty(txtMachineName.Text))
            {
                if (cboFilterMachineName.SelectedIndex == 1)
                    query.Where(query.MachineName == txtMachineName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtMachineName.Text);
                    query.Where(query.MachineName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.MachineID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}

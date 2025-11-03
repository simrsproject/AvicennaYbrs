using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Laundry.Master.WashingMachine
{
    public partial class WashingMachineSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.WashingMachine;

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new LaundryWashingMachineQuery("a");
            query.Select(
                        query.MachineID,
                        query.MachineName,
                        query.StartUsingDate,
                        query.Volume,
                        query.Notes,
                        query.IsActive);
            var isEsTop = true;

            if (!string.IsNullOrEmpty(txtMachineID.Text))
            {
                if (cboFilterMachineID.SelectedIndex == 1)
                    query.Where(query.MachineID == txtMachineID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtMachineID.Text);
                    query.Where(query.MachineID.Like(searchTextContain));
                }
                isEsTop = false;
            }
            if (!string.IsNullOrEmpty(txtMachineName.Text))
            {
                if (cboFilterMachineName.SelectedIndex == 1)
                    query.Where(query.MachineName == txtMachineName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtMachineName.Text);
                    query.Where(query.MachineName.Like(searchTextContain));
                }
                isEsTop = false;
            }
            query.OrderBy(query.MachineID.Ascending);

            if (isEsTop)
                query.es.Top = AppSession.Parameter.MaxResultRecord;

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();
            return true;
        }
    }
}
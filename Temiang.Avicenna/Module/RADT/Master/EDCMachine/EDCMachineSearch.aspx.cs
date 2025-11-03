using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class EDCMachineSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.EdcMachine;

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSRCardProvider, AppEnum.StandardReference.CardProvider);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
            
        }
        public override bool OnButtonOkClicked()
        {
            var query = new EDCMachineQuery("a");
            var qRef = new AppStandardReferenceItemQuery("b");
            query.InnerJoin(qRef).On(
                query.SRCardProvider == qRef.ItemID & qRef.StandardReferenceID == "CardProvider");
            query.Select(query.EDCMachineID, query.EDCMachineName, qRef.ItemName.As("CardProviderName"));

            if (!string.IsNullOrEmpty(txtEDCMachineID.Text))
            {
                if (cboFilterEDCMachineID.SelectedIndex == 1)
                    query.Where(query.EDCMachineID == txtEDCMachineID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEDCMachineID.Text);
                    query.Where(query.EDCMachineID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtEDCMachineName.Text))
            {
                if (cboFilterEDCMachineName.SelectedIndex == 1)
                    query.Where(query.EDCMachineName == txtEDCMachineName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEDCMachineName.Text);
                    query.Where(query.EDCMachineName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSRCardProvider.SelectedValue))
                query.Where(query.SRCardProvider == cboSRCardProvider.SelectedValue);

            query.OrderBy(query.EDCMachineID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}

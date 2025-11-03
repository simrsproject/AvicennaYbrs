using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.CashManagement.CashEntryV2
{
    public partial class CashEntrySearch : BasePageDialog
    {
        public class SearchValue
        {
            public string BankId;
            public string BankName;
            public string ModuleCode;
            public string ModuleName;
            public string TransactionType;
            public string DocumentNumber;
            public DateTime? TransactionDate;
            public DateTime? TransactionDateTo;
            public string Status;
            public string JournalNumber;
            public string Description;
            public string DescriptionDetail;
            public double? Amount;
            public string RangeFilter;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            var bankColl = new BankCollection();
            bankColl.Query.Where(bankColl.Query.IsActive == true);
            bankColl.LoadAll();
            cboBankName.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
            foreach (var bank in bankColl)
            {
                cboBankName.Items.Add(new Telerik.Web.UI.RadComboBoxItem(bank.BankName, bank.BankID));
            }

            ProgramID = AppConstant.Program.CASH_ENTRY;
            if (Session[SessionNameForQuery] != null)
            {
                var sv = (SearchValue)Session[SessionNameForQuery];
                cboBankName.SelectedValue = sv.BankId;
                cboModuleName.SelectedValue = sv.ModuleCode;
                cboTransactionType.SelectedValue = sv.TransactionType;
                txtDocumentNumber.Text = sv.DocumentNumber;
                txtTransactionDate.SelectedDate = sv.TransactionDate;
                txtTransactionDateTo.SelectedDate = sv.TransactionDateTo;
                cboPostingStatus.SelectedValue = sv.Status;
                txtJournalNumber.Text = sv.JournalNumber;
                txtDescription.Text = sv.Description;
                txtAmount.Value = sv.Amount;
                rbRangeFilter.SelectedValue = sv.RangeFilter;
            }
            else
            {
                rbRangeFilter.SelectedValue = AppSession.Parameter.JournalEntrySearchRangeFilter;
            }
        }

        public override bool OnButtonOkClicked()
        {
            var sv = new SearchValue
            {
                BankId = cboBankName.SelectedValue,
                ModuleName = (cboModuleName.SelectedValue == "-1" ? string.Empty : cboModuleName.Text),
                ModuleCode = cboModuleName.SelectedValue,
                TransactionType = cboTransactionType.SelectedValue,
                DocumentNumber = txtDocumentNumber.Text,
                TransactionDate = txtTransactionDate.SelectedDate,
                TransactionDateTo = txtTransactionDateTo.SelectedDate,
                Status = cboPostingStatus.SelectedValue,
                JournalNumber = txtJournalNumber.Text,
                Description = txtDescription.Text,
                DescriptionDetail = txtDescriptioinDetail.Text,
                Amount = txtAmount.Value,
                RangeFilter = rbRangeFilter.SelectedValue

            };

            Session[SessionNameForQuery] = sv;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
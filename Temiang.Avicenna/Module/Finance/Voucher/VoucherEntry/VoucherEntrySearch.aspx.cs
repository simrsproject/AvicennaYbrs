using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry
{
    public partial class VoucherEntrySearch : BasePageDialog
    {
        public class SearchValue
        {
            public string JournalType;
            public string VoucherCode;
            public string VoucherNumber;
            public DateTime? TransactionDate;
            public DateTime? TransactionDateTo;
            public string Status;
            public string Description;
            public string RegistrationNo;
            public string ReferenceNo;
            public string RangeFilter;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VOUCHER_MEMORIAL;
            if (Session[SessionNameForQuery] != null)
            {
                VoucherEntrySearch.SearchValue sv = (VoucherEntrySearch.SearchValue)Session[SessionNameForQuery];
                cboJournalType.SelectedValue = sv.JournalType;
                txtVoucherCode.Text = sv.VoucherCode;
                txtVoucherNumber.Text = sv.VoucherNumber;
                txtVoucherDate.SelectedDate = sv.TransactionDate;
                txtVoucherDateTo.SelectedDate = sv.TransactionDateTo;
                cboPostingStatus.SelectedValue = sv.Status;
                txtDescription.Text = sv.Description;
                txtRegistrationNo.Text = sv.RegistrationNo;
                txtReferenceNo.Text = sv.ReferenceNo;
                rbRangeFilter.SelectedValue = sv.RangeFilter;
            }
            else
            {
                rbRangeFilter.SelectedValue = AppSession.Parameter.JournalEntrySearchRangeFilter;
            }

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboJournalType, AppEnum.StandardReference.JournalType);
            }
        }

        public override bool OnButtonOkClicked()
        {
            SearchValue sv = new SearchValue();

            sv.JournalType = cboJournalType.SelectedValue;
            sv.VoucherCode = txtVoucherCode.Text;
            sv.VoucherNumber = txtVoucherNumber.Text;
            sv.TransactionDate = txtVoucherDate.SelectedDate;
            sv.TransactionDateTo = txtVoucherDateTo.SelectedDate;
            sv.Status = cboPostingStatus.SelectedValue;
            sv.Description = txtDescription.Text;
            sv.RegistrationNo = txtRegistrationNo.Text;
            sv.ReferenceNo = txtReferenceNo.Text;
            sv.RangeFilter = rbRangeFilter.SelectedValue;
            Session[SessionNameForQuery] = sv;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry
{
    public partial class VoucherReprocessingDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VOUCHER_MEMORIAL;
            Title="Re-Balance Journal";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

            }

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
            {
                {
                    RadComboBoxItem item = (RadComboBoxItem)cboJournalType.FindItemByText("Patient Receivable");
                    item.Visible = false;
                }
 
            }
        }

        public override bool OnButtonOkClicked()
        {
            bool retVal = true;

            if (!txtDateStart.SelectedDate.HasValue) return false;
            if (!txtDateEnd.SelectedDate.HasValue) return false;
            if (cboJournalType.SelectedValue == string.Empty) return false;

            //var journals = new JournalTransactionsCollection();
            //var jq = new JournalTransactionsQuery("a");
            //var jdq = new JournalTransactionDetailsQuery("b");
            //jq.InnerJoin(jdq).On(jdq.JournalId == jq.JournalId);
            //jq.Select(jq.JournalId, jq.RefferenceNumber, jq.JournalType, jq.JournalCode, jq.TransactionDate, jq.IsPosted);
            //jq.GroupBy(jq.JournalId, jq.RefferenceNumber, jq.JournalType, jq.JournalCode, jq.TransactionDate, jq.IsPosted);
            //jq.Where(
            //    jq.TransactionDate.Between(txtDateStart.SelectedDate, txtDateEnd.SelectedDate),
            //    jq.JournalType.Equal(cboJournalType.SelectedValue),
            //    jq.IsPosted.Equal(false)
            //);
            //jq.Having("<SUM(b.Debit) <> SUM(b.Credit) OR SUM(b.Debit) + SUM(b.Credit) = 0>");

            var journals = new JournalTransactionsCollection();
            var jq = new JournalTransactionsQuery("a");

            jq.Select(jq.JournalId, jq.RefferenceNumber, jq.JournalType, jq.JournalCode, jq.TransactionDate, jq.IsPosted);
            jq.Where(
                jq.TransactionDate.Date().Between(txtDateStart.SelectedDate, txtDateEnd.SelectedDate),
                jq.JournalType.Equal(cboJournalType.SelectedValue),
                jq.IsVoid.Equal(false)
            ).OrderBy(jq.JournalId.Ascending);

            if (!chkIncludePosted.Checked)
                jq.Where(jq.IsPosted.Equal(false));

            if (journals.Load(jq)) {
                foreach (var j in journals) {
                    VoucherRejournalDialog.ReJournal(j.JournalId.Value, j.RefferenceNumber, j.JournalType, j.JournalIdRefference.HasValue);
                }
            }

            VoucherEntrySearch.SearchValue sv = new VoucherEntrySearch.SearchValue();

            sv.JournalType = (cboJournalType.SelectedValue == "35a") ? "35" : cboJournalType.SelectedValue;
            sv.Status = "0";

            Session[SessionNameForQuery] = sv;
            Session.Remove(SessionNameForList); //reset

            return retVal;
        }
    }
}

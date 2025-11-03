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

namespace Temiang.Avicenna.Module.Finance.CashManagement.CashEntryV2
{
    public partial class CashEntryRebalanceDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CASH_ENTRY;
            Title="Re-Balance Cash Entry";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                var bColl = new BankCollection();
                bColl.Query.Where(bColl.Query.IsActive == true);
                bColl.LoadAll();
                //cboBank.Items.Clear();
                cboBank.Items.Add(new RadComboBoxItem("", ""));
                foreach (var b in bColl) {
                    cboBank.Items.Add(new RadComboBoxItem(b.BankName, b.BankID));
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            bool retVal = true;

            if (!txtDateStart.SelectedDate.HasValue) return false;
            if (!txtDateEnd.SelectedDate.HasValue) return false;
            
            var cColl = new CashTransactionCollection();
            var ctq = new CashTransactionQuery("ct");
            var ctdq = new CashTransactionDetailQuery("ctd");

            ctq.InnerJoin(ctdq).On(ctq.TransactionId == ctdq.TransactionId)
                .Select(ctq.TransactionId, ctq.JournalId)
                .Where(
                    ctq.TransactionDate.Date().Between(txtDateStart.SelectedDate, txtDateEnd.SelectedDate),
                    ctq.IsVoid.Equal(false),
                    ctq.IsPosted.Equal(true) // hanya boleh rebalance transaksi yang sudah approve
                ).OrderBy(ctq.TransactionId.Ascending);
            ctq.es.Distinct = true;

            if (!string.IsNullOrEmpty(cboBank.SelectedValue))
                ctq.Where(ctq.BankId.Equal(cboBank.SelectedValue));

            if (cColl.Load(ctq)) {
                foreach (var ct in cColl) {
                    CashTransaction.PostingUpdateBalance(ct.TransactionId.Value, ct.JournalId.Value);
                }
            }

            //VoucherEntrySearch.SearchValue sv = new VoucherEntrySearch.SearchValue();

            //sv.JournalType = (cboJournalType.SelectedValue == "35a") ? "35" : cboJournalType.SelectedValue;
            //sv.Status = "0";

            //Session[SessionNameForQuery] = sv;
            //Session.Remove(SessionNameForList); //reset

            return retVal;
        }
    }
}

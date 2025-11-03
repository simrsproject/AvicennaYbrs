using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class ClosingBalanceDialog : BasePageDialog
    {
        private string errorMsg;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CashierClosingBalance;

            if (!IsPostBack)
            {
                ViewState["result" + Request.UserHostName] = string.Empty;

                StandardReference.InitializeIncludeSpace(cboSRShift, AppEnum.StandardReference.Shift);
                StandardReference.InitializeIncludeSpace(cboSRCashierCounter, AppEnum.StandardReference.CashierCounter);

                var cm = new CashManagement();
                cm.LoadByPrimaryKey(Request.QueryString["cmno"]);
                txtTransactionNo.Text = cm.TransactionNo;
                txtOpeningDate.SelectedDate = cm.OpeningDate.Value.Date;
                txtOpeningTime.Text = cm.OpeningDate.Value.ToString("HH:mm");
                cboSRShift.SelectedValue = cm.SRShift;
                cboSRCashierCounter.SelectedValue = cm.SRCashierCounter;
                txtOpeningBalance.Value = Convert.ToDouble(cm.OpeningBalance);

                Title = "Closing for " + Request.QueryString["cmno"];
            }
        }

        private void Closing(string cmno)
        {
            var cm = new CashManagement();
            cm.LoadByPrimaryKey(cmno);

            //sering ada double journal untuk void
            if (cm.ClosingDate != null)
            {
                errorMsg = "Closing failed. This transaction has been closed.";
                return;
            }

            using (var trans = new esTransactionScope())
            {
                cm.CashPayment = CalculateCashPayment(cmno);
                cm.CashAmount = Convert.ToDecimal(txtCashAmount.Value);
                cm.ClosingBalance = (cm.CashAmount ?? 0) - (cm.CashPayment ?? 0);
                cm.ClosingDate = (new DateTime()).NowAtSqlServer();
                cm.ClosingByUserID = AppSession.UserLogin.UserID;
                cm.LastUpdateByUserID = AppSession.UserLogin.UserID;
                cm.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                cm.Save();

                var cmc = new CashManagementCashierCollection();
                cmc.Query.Where(cmc.Query.TransactionNo == cmno);
                cmc.LoadAll();
                foreach (var c in cmc)
                {
                    var usr = new AppUser();
                    usr.LoadByPrimaryKey(c.CashierUserID);
                    usr.CashManagementNo = string.Empty;
                    usr.Save();
                }

                trans.Complete();
            }
            errorMsg = string.Empty;
        }

        private decimal CalculateCashPayment(string cmno)
        {
            var dt = new TransPaymentItemQuery("a");
            var hd = new TransPaymentQuery("b");
            dt.InnerJoin(hd).On(dt.PaymentNo == hd.PaymentNo);
            dt.Where(hd.TransactionCode != "019", hd.IsApproved == true, hd.CashManagementNo == cmno,
                     dt.SRPaymentType == AppSession.Parameter.PaymentTypePayment,
                     dt.SRPaymentMethod == AppSession.Parameter.PaymentMethodCash);

            var coll = new TransPaymentItemCollection();
            coll.Load(dt);

            decimal? totalPayment = coll.Aggregate<TransPaymentItem, decimal?>(0, (current, c) => current + c.Amount);

            dt = new TransPaymentItemQuery("a");
            hd = new TransPaymentQuery("b");
            dt.InnerJoin(hd).On(dt.PaymentNo == hd.PaymentNo);
            dt.Where(hd.TransactionCode == "019", hd.IsApproved == true, hd.CashManagementNo == cmno,
                     dt.SRPaymentType == AppSession.Parameter.PaymentTypePayment,
                     dt.SRPaymentMethod == AppSession.Parameter.PaymentMethodCash);

            coll = new TransPaymentItemCollection();
            coll.Load(dt);

            decimal? totalReturDp = coll.Aggregate<TransPaymentItem, decimal?>(0, (current, c) => current + c.Amount);

            var dtp = new TransPaymentPatientItemQuery("a");
            var hdp = new TransPaymentPatientQuery("b");
            dtp.InnerJoin(hdp).On(dtp.PaymentNo == hdp.PaymentNo);
            dtp.Where(hdp.TransactionCode == "018", hdp.IsApproved == true, hdp.CashManagementNo == cmno,
                     dtp.SRPaymentType == AppSession.Parameter.PaymentTypePayment,
                     dtp.SRPaymentMethod == AppSession.Parameter.PaymentMethodCash);

            var collp = new TransPaymentPatientItemCollection();
            collp.Load(dtp);

            decimal? totalPatientPayment = collp.Aggregate<TransPaymentPatientItem, decimal?>(0, (current, c) => current + c.Amount);

            dtp = new TransPaymentPatientItemQuery("a");
            hdp = new TransPaymentPatientQuery("b");
            dtp.InnerJoin(hdp).On(dtp.PaymentNo == hdp.PaymentNo);
            dtp.Where(hdp.TransactionCode == "019", hdp.IsApproved == true, hdp.CashManagementNo == cmno,
                     dtp.SRPaymentType == AppSession.Parameter.PaymentTypePayment,
                     dtp.SRPaymentMethod == AppSession.Parameter.PaymentMethodCash);

            collp = new TransPaymentPatientItemCollection();
            collp.Load(dtp);

            decimal? totalPatientRetur = collp.Aggregate<TransPaymentPatientItem, decimal?>(0, (current, c) => current + c.Amount);

            return (totalPayment ?? 0) - (totalReturDp ?? 0) + (totalPatientPayment ?? 0) - (totalPatientRetur ?? 0);
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            Closing(Request.QueryString["cmno"]);
            if (!string.IsNullOrEmpty(errorMsg))
            {
                ShowInformationHeader(errorMsg);
                return false;
            }

            return true;
        }
    }
}

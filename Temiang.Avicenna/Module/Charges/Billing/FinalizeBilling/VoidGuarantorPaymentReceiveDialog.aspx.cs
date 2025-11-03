using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class VoidGuarantorPaymentReceiveDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var py = new TransPayment();
                py.LoadByPrimaryKey(Request.QueryString["payno"]);

                var guar = new Guarantor();
                guar.LoadByPrimaryKey(py.GuarantorID);

                Title = "Void Payment Receive for " + guar.GuarantorName;
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            //invoice
            var invcoll = new InvoicesItemCollection();
            var invit = new InvoicesItemQuery("a");
            var inv = new InvoicesQuery("b");
            invit.InnerJoin(inv).On(invit.InvoiceNo == inv.InvoiceNo && inv.IsInvoicePayment == false &&
                                    inv.IsVoid == false);
            invit.Where(invit.PaymentNo == Request.QueryString["payno"]);
            invcoll.Load(invit);

            bool allowVoid = invcoll.Count <= 0;

            if (!allowVoid)
            {
                ShowInformationHeader("This transaction has been proceed to Invoice. If you still want to void this data, please void Invoice first.");
                return false;
            }

            // cek sudah tarik ke jasmed (jasmed by dischargedate) atau belum 
            var msg = ParamedicFeeTransChargesItemCompByDischargeDate.IsParamedicFeeVerified(Request.QueryString["payno"], true);
            if (!string.IsNullOrEmpty(msg)) {
                ShowInformationHeader(msg);
                return false;
            }

            var etp = new TransPayment();
            etp.LoadByPrimaryKey(Request.QueryString["payno"]);

            // cek sudah pernah void atau belum. kalau gak dicek jurnalnya void bisa jadi double
            if (etp.IsVoid ?? false) {
                ShowInformationHeader(AppConstant.Message.RecordHasVoided);
                return false;
            }

            etp.Notes = txtNotes.Text;
            etp.IsApproved = false;
            etp.IsVoid = true;
            etp.LastUpdateByUserID = AppSession.UserLogin.UserID;
            etp.LastUpdateDateTime = DateTime.Now;

            etp.VoidByUserID = AppSession.UserLogin.UserID;
            etp.VoidDate = DateTime.Now;

            var collibguar = new TransPaymentItemIntermBillGuarantorCollection();
            collibguar.Query.Where(collibguar.Query.PaymentNo == etp.PaymentNo);
            collibguar.LoadAll();

            foreach (var item in collibguar)
            {
                item.IsPaymentProceed = false;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }

            var collib = new TransPaymentItemIntermBillCollection();
            collib.Query.Where(collib.Query.PaymentNo == etp.PaymentNo);
            collib.LoadAll();

            foreach (var item in collib)
            {
                item.IsPaymentProceed = false;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }

            var colltpio = new TransPaymentItemOrderCollection();
            colltpio.Query.Where(colltpio.Query.PaymentNo == etp.PaymentNo);
            colltpio.LoadAll();
            foreach (var item in colltpio)
            {
                item.IsPaymentProceed = false;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now; 
            }

            var collbuffer = new CostCalculationBufferCollection();
            collbuffer.Query.Where(collbuffer.Query.PaymentNo == etp.PaymentNo);
            collbuffer.LoadAll();

            foreach (var item in collbuffer)
            {
                item.PaymentNo = null;
            }

            var collac = new AskesCovered2Collection();
            collac.Query.Where(collac.Query.PaymentNo == etp.PaymentNo);
            collac.LoadAll();

            foreach (var item in collac)
            {
                item.PaymentNo = null;
                item.IsPaid = false;
            }

            var colltpi = new TransPaymentItemCollection();
            colltpi.Query.Where(colltpi.Query.PaymentNo == etp.PaymentNo);
            colltpi.LoadAll();

            var total = colltpi.Sum(detail => (decimal)detail.Amount);

            var reg = new Registration();
            reg.LoadByPrimaryKey(etp.RegistrationNo);
            reg.RemainingAmount += total;

            var collDP = new TransPaymentCollection();
            collDP.Query.Where(collDP.Query.PaymentReferenceNo.Equal(etp.PaymentNo));
            collDP.LoadAll();
            foreach (var dp in collDP) {
                dp.PaymentReferenceNo = string.Empty;
                dp.ReceiptIsReturned = null;
                dp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                dp.LastUpdateDateTime = DateTime.Now;
            }

            // unset payment jasmed
            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            feeColl.UnSetPayment(etp, AppSession.UserLogin.UserID);

            using (esTransactionScope trans = new esTransactionScope())
            {
                etp.Save();
                reg.Save();
                collib.Save();
                collibguar.Save();
                colltpio.Save();
                collbuffer.Save();
                collac.Save();
                collDP.Save();
                feeColl.Save();

                if (AppSession.Parameter.IsUsingIntermBill)
                {

                    int? journalId = JournalTransactions.AddNewPaymentCorrectionJournalCashBased(BusinessObject.JournalType.ARCreating, etp, reg, colltpi, "TP", etp.PaymentNo, AppSession.UserLogin.UserID, 0);
                }
                else
                {

                    int? journalId = JournalTransactions.AddNewPaymentCorrectionJournal(BusinessObject.JournalType.ARCreating, etp, reg, colltpi, "TP", AppSession.UserLogin.UserID, 0);
                }

                //if (AppSession.Parameter.IsPhysicianFeeArCreateOnArReceipt == "Yes")
                //{
                //    int? x = ParamedicFeeTransChargesItemCompSettled.DeleteSettled(etp, false);
                //}

                #region Guarantor Deposit Balance

                colltpi = new TransPaymentItemCollection();
                colltpi.Query.Where(colltpi.Query.PaymentNo == etp.PaymentNo, colltpi.Query.SRPaymentType == AppSession.Parameter.PaymentTypeSaldoAR);
                colltpi.LoadAll();
                if (colltpi.Count > 0)
                {
                    total = colltpi.Sum(detail => (decimal)detail.Amount);

                    var balance = new GuarantorDepositBalance();
                    var movement = new GuarantorDepositMovement();
                    GuarantorDepositBalance.PrepareGuarantorDepositBalances(etp.PaymentNo, etp.GuarantorID,
                                                                            "A/R Process (Void)", AppSession.UserLogin.UserID,
                                                                            total,
                                                                            0,
                                                                            ref balance, ref movement);
                    balance.Save();
                    movement.Save();
                }

                #endregion

                trans.Complete();
            }

            // checkout otomatis,
            Helper.RegistrationOpenClose.SetDischargeDate(reg);

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'voidpayment'";
        }
    }
}

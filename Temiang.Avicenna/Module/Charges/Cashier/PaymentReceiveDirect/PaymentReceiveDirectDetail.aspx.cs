using System;
using System.Linq;
using System.Text;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PaymentReceiveDirectDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;
        private bool _isPowerUser;
        private string CashManagementNo
        {
            get
            {
                string cmno = string.IsNullOrEmpty(Request.QueryString["cmno"])
                                  ? string.Empty
                                  : Request.QueryString["cmno"];
                return cmno;
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "#";

            UrlPageList = string.IsNullOrEmpty(Request.QueryString["prescno"])
                              ? "PaymentReceiveDirectList.aspx"
                              : "../../Dispensary/PrescriptionSales/RSCH/PrescriptionSalesPosList.aspx?type=sales&rt=opr";

            if (AppSession.Parameter.IsNeedVoidReasonOnPaymentReceive)
            {
                IsUsingBeforeUnapprovalValidation = true;
            }

            ProgramID = AppConstant.Program.PaymentReceiveDirect;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                TransPaymentItems = null;
                TransPaymentItemOrders = null;

                trInitial.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH";

                if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSSA" && string.IsNullOrEmpty(Request.QueryString["prescno"]))
                {
                    var cstext1 = new StringBuilder();
                    cstext1.Append("<script type=text/javascript> __doPostBack('ctl00$ContentPlaceHolder1$grdTransPaymentItem$ctl00$ctl02$ctl00$AddNewRecordButton','') </script>");

                    Page.ClientScript.RegisterStartupScript(GetType(), "OpenAddNewRecordGrid", cstext1.ToString());
                }

                txtPaymentDate.DateInput.ReadOnly = !AppSession.Parameter.IsPaymentReceiveAllowBackdated;
                txtPaymentDate.DatePopupButton.Enabled = !txtPaymentDate.DateInput.ReadOnly;
                txtPaymentTime.ReadOnly = txtPaymentDate.DateInput.ReadOnly;
            }

            _isPowerUser = this.IsPowerUser || AppSession.Parameter.IsBypassCashierAuthorization;

            WindowSearch.Height = 300;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdTransPaymentItem, grdTransPaymentItem);
            ajax.AddAjaxSetting(grdTransPaymentItem, btnOrderItem);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            var hd = new TransPayment();
            if (hd.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                hd.PrintNumber++;
                if (!hd.IsPrinted ?? false)
                    hd.IsPrinted = true;
                hd.LastPrintedDateTime = (new DateTime()).NowAtSqlServer();
                hd.LastPrintedByUserID = AppSession.UserLogin.UserID;
                hd.Save();
            }

            if (AppSession.Parameter.IsUsedPrintSlipLogForPaymentReceipt)
                PrintSlipLog.InsertUpdate(programID, "PaymentNo", txtPaymentNo.Text, AppSession.UserLogin.UserID);

            switch (programID)
            {
                case AppConstant.Report.PaymentReceiptSlip:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                    printJobParameters.AddNew("PaymentTypeDiscount", AppSession.Parameter.PaymentTypeDiscount);

                    break;
                default:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;
            }
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new TransPayment();
            if (entity.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                if (entity.CreatedBy != AppSession.UserLogin.UserID && !_isPowerUser)
                {
                    var usr = new AppUser();
                    usr.LoadByPrimaryKey(entity.CreatedBy);

                    args.MessageText =
                        "You don't have authorization to edit this transaction. This data belong to: " +
                        usr.UserName + ". Please contact your supervision.";
                    args.IsCancel = true;
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new TransPayment());

            txtPaymentNo.Text = GetNewPaymentNo();

            PopulateTransPaymentItemGrid();

            txtPaymentDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtPaymentTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
            txtRegistrationNo.Text = Request.QueryString["regno"];

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            txtServiceUnitID.Text = reg.ServiceUnitID;
            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(txtServiceUnitID.Text);
            lblServiceUnitName.Text = unit.ServiceUnitName;

            var patient = new Patient();
            patient.LoadByPrimaryKey(reg.PatientID);

            txtMedicalNo.Text = patient.MedicalNo;
            txtPatientName.Text = patient.PatientName;
            txtPrintReceiptAsName.Text = patient.PatientName;
            var std = new AppStandardReferenceItem();
            txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;

            txtGuarantorID.Text = reg.GuarantorID;
            var guarantor = new Guarantor();
            guarantor.LoadByPrimaryKey(txtGuarantorID.Text);
            lblGuarantorName.Text = guarantor.GuarantorName;

            txtOrderAmount.Value = 0;

            if (!string.IsNullOrEmpty(Request.QueryString["prescno"]))
            {
                var tpioColl = new TransPaymentItemOrderCollection();
                tpioColl.Query.Where(tpioColl.Query.TransactionNo == Request.QueryString["prescno"],
                                     tpioColl.Query.IsPaymentReturned == false);
                tpioColl.LoadAll();
                if (tpioColl.Count == 0)
                {
                    decimal amount = 0;
                    //-- payment item order
                    var order = (TransPaymentItemOrderCollection)Session["PaymentReceiveDirect:collTransPaymentItemOrder" + Request.UserHostName];

                    var tpiColl = new TransPrescriptionItemCollection();
                    tpiColl.Query.Where(tpiColl.Query.PrescriptionNo == Request.QueryString["prescno"]);
                    tpiColl.LoadAll();

                    foreach (var tpi in tpiColl)
                    {
                        TransPaymentItemOrder entity = FindTransPaymentItemOrder(tpi.PrescriptionNo, tpi.SequenceNo);
                        if (entity == null)
                            entity = order.AddNew();

                        entity.TransactionNo = tpi.PrescriptionNo;
                        entity.SequenceNo = tpi.SequenceNo;
                        entity.PaymentNo = txtPaymentNo.Text;

                        var phd = new TransPrescription();
                        phd.LoadByPrimaryKey(entity.TransactionNo);
                        entity.TransactionDate = phd.PrescriptionDate;
                        string psu = phd.ServiceUnitID;
                        string regno = phd.RegistrationNo;

                        var su = new ServiceUnit();
                        su.LoadByPrimaryKey(psu);
                        entity.ServiceUnitName = su.ServiceUnitName;

                        entity.ItemID = string.IsNullOrEmpty(tpi.ItemInterventionID) ? tpi.ItemID : tpi.ItemInterventionID;
                        entity.Qty = tpi.ResultQty;
                        if (tpi.ResultQty == 0)
                            entity.Price = 0;
                        else
                            entity.Price = tpi.LineAmount / tpi.ResultQty;
                        entity.Total = tpi.LineAmount;

                        var cc = new CostCalculation();
                        if (cc.LoadByPrimaryKey(regno, entity.TransactionNo, entity.SequenceNo))
                        {
                            if (entity.Qty == 0)
                                entity.Price = 0;
                            else
                                entity.Price = cc.PatientAmount / entity.Qty;
                            entity.Total = cc.PatientAmount;
                        }

                        var i = new Item();
                        i.LoadByPrimaryKey(entity.ItemID);
                        entity.ItemName = i.ItemName;

                        amount += Convert.ToDecimal(entity.Total);
                    }

                    //--payment item
                    var payment = (TransPaymentItemCollection)Session["PaymentReceiveDirect:collTransPaymentItem" + Request.UserHostName];

                    string seq;
                    if (!payment.HasData)
                        seq = "001";
                    else
                    {
                        int seqNo = int.Parse(payment[payment.Count - 1].SequenceNo) + 1;
                        seq = string.Format("{0:000}", seqNo);
                    }

                    TransPaymentItem entitypy = payment.AddNew();

                    entitypy.SequenceNo = seq;
                    entitypy.SRPaymentType = AppSession.Parameter.PaymentTypePayment;

                    var type = new PaymentType();
                    type.LoadByPrimaryKey(entitypy.SRPaymentType);
                    entitypy.PaymentTypeName = type.PaymentTypeName;

                    entitypy.SRPaymentMethod = AppSession.Parameter.PaymentMethodCash;

                    var meth = new PaymentMethod();
                    meth.LoadByPrimaryKey(entitypy.SRPaymentType, entitypy.SRPaymentMethod);
                    entitypy.PaymentMethodName = meth.PaymentMethodName;
                    entitypy.Amount = amount;
                    entitypy.AmountReceived = 0;
                    entitypy.Change = 0;
                    entitypy.RoundingAmount = 0;
                    entitypy.Balance = 0;
                    entitypy.BankID = string.Empty;

                    txtOrderAmount.Value = Convert.ToDouble(amount);
                    grdOrderItem.Rebind();
                    grdTransPaymentItem.Rebind();

                    SetNotesValue(Request.QueryString["prescno"]);
                }
            }
        
            ViewState["IsVoid"] = false;
            ViewState["IsApproved"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            if (AppSession.Parameter.IsUsingValidationUserAccessOnPaymentReceive)
            {
                if (!Helper.IsValidUserAuthorization(ProgramID, AppConstant.UserAccessType.Delete))
                {
                    args.MessageText = "You don't have authorization to delete this transaction.";
                    args.IsCancel = true;
                    return;
                }
            }

            var entity = new TransPayment();
            if (entity.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                TransPaymentItems.MarkAllAsDeleted();
                TransPaymentItems.Save();

                TransPaymentItemOrders.MarkAllAsDeleted();
                TransPaymentItemOrders.Save();

                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var regStatusMsg = Registration.GetRegStatusVoidOrClose(txtRegistrationNo.Text);
            if (regStatusMsg != string.Empty)
            {
                args.MessageText = regStatusMsg;
                args.IsCancel = true;
                return;
            }

            if (AppSession.Parameter.IsUsingCashManagement && string.IsNullOrEmpty(CashManagementNo))
            {
                args.MessageText = "Cashier checkin required.";
                args.IsCancel = true;
                return;
            }

            if (TransPaymentItems.Count == 0)
            {
                args.MessageText = "Payment detail is not defined.";
                args.IsCancel = true;
                return;
            }

            if (TransPaymentItemOrders.Count == 0)
            {
                args.MessageText = "Transaction Item detail is not defined.";
                args.IsCancel = true;
                return;
            }

            decimal totPayment =
                TransPaymentItems.Sum(item => Convert.ToDecimal(item.Amount) - Convert.ToDecimal(item.RoundingAmount));

            double diff = Convert.ToDouble(totPayment) - Convert.ToDouble(txtOrderAmount.Value);
            double excess = Math.Abs(AppSession.Parameter.ExcessPaymentAmount);

            if (diff < (-1) * excess)
            {
                args.MessageText = "Total payment amount can't be less than total transaction.";
                args.IsCancel = true;
                return;
            }

            if (!AppSession.Parameter.IsAllowExcessPaymentAmountPlus && diff > excess)
            {
                args.MessageText = "Total payment amount can't be more than total transaction.";
                args.IsCancel = true;
                return;
            }

            var entity = new TransPayment();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var regStatusMsg = Registration.GetRegStatusVoidOrClose(txtRegistrationNo.Text);
            if (regStatusMsg != string.Empty)
            {
                args.MessageText = regStatusMsg;
                args.IsCancel = true;
                return;
            }

            if (TransPaymentItems.Count == 0)
            {
                args.MessageText = "Payment detail is not defined.";
                args.IsCancel = true;
                return;
            }

            if (TransPaymentItemOrders.Count == 0)
            {
                args.MessageText = "Transaction Item detail is not defined.";
                args.IsCancel = true;
                return;
            }

            decimal totPayment =
                TransPaymentItems.Sum(item => Convert.ToDecimal(item.Amount) - Convert.ToDecimal(item.RoundingAmount));

            double diff = Convert.ToDouble(totPayment) - Convert.ToDouble(txtOrderAmount.Value);
            double excess = Math.Abs(AppSession.Parameter.ExcessPaymentAmount);

            if (diff < (-1) * excess)
            {
                args.MessageText = "Total payment amount can't be less than total transaction.";
                args.IsCancel = true;
                return;
            }

            if (!AppSession.Parameter.IsAllowExcessPaymentAmountPlus && diff > excess)
            {
                args.MessageText = "Total payment amount can't be more than total transaction.";
                args.IsCancel = true;
                return;
            }

            var entity = new TransPayment();
            if (entity.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("PaymentNo='{0}'", txtPaymentNo.Text.Trim());
            auditLogFilter.TableName = "TransPayment";
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var regStatusMsg = Registration.GetRegStatusVoidOrClose(txtRegistrationNo.Text);
            if (regStatusMsg != string.Empty)
            {
                args.MessageText = regStatusMsg;
                args.IsCancel = true;
                return;
            }

            var entity = new TransPayment();
            if (!entity.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }
            if (TransPaymentItems.Count == 0)
            {
                args.MessageText = "Payment detail is not defined";
                args.IsCancel = true;
                return;
            }

            decimal totPayment =
                TransPaymentItems.Sum(item => Convert.ToDecimal(item.Amount) - Convert.ToDecimal(item.RoundingAmount));

            double diff = Convert.ToDouble(totPayment) - Convert.ToDouble(txtOrderAmount.Value);
            double excess = Math.Abs(AppSession.Parameter.ExcessPaymentAmount);

            if (diff < (-1) * excess)
            {
                args.MessageText = "Total payment amount can't be less than total transaction.";
                args.IsCancel = true;
                return;
            }

            if (!AppSession.Parameter.IsAllowExcessPaymentAmountPlus && diff > excess)
            {
                args.MessageText = "Total payment amount can't be more than total transaction.";
                args.IsCancel = true;
                return;
            }

            if (entity.CreatedBy != AppSession.UserLogin.UserID && !_isPowerUser)
            {
                var usr = new AppUser();
                usr.LoadByPrimaryKey(entity.CreatedBy);

                args.MessageText = "You don't have authorization to approved this transaction. This data belong to: " +
                                   usr.UserName + ". Please contact your supervision.";
                args.IsCancel = true;
                return;
            }

            var closingperiod = entity.PaymentDate.Value.Date;
            var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod);
            if (isClosingPeriod)
            {
                args.MessageText = "Financial statements for period: " +
                                   string.Format("{0:MMMM-yyyy}", closingperiod) +
                                   " have been closed. Please contact the authorities.";
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, true, string.Empty);
        }

        public override bool OnGetStatusMenuUnApprovalEnabled()
        {
            return !PostingStatus.IsUnApproveDisabledIfPerClosed(txtPaymentDate.SelectedDate.Value);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var regStatusMsg = Registration.GetRegStatusVoidOrClose(txtRegistrationNo.Text);
            if (regStatusMsg != string.Empty)
            {
                args.MessageText = regStatusMsg;
                args.IsCancel = true;
                return;
            }

            if (AppSession.Parameter.IsUsingValidationUserAccessOnPaymentReceive)
            {
                if (!Helper.IsValidUserAuthorization(ProgramID, AppConstant.UserAccessType.UnApproved))
                {
                    args.MessageText = "You don't have authorization to Un-Approved this transaction.";
                    args.IsCancel = true;
                    return;
                }
            }

            var reason = args.ReasonText;

            var entity = new TransPayment();
            if (!entity.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            //sering ada double journal untuk void
            if (entity.IsVoid == true)
            {
                args.MessageText = "Un-approval failed. Payment has been voided";
                args.IsCancel = true;
                return;
            }

            // validate finance verification
            var tpiColl = new TransPaymentItemCollection();
            tpiColl.Query.Where(tpiColl.Query.PaymentNo == entity.PaymentNo, tpiColl.Query.CashTransactionReconcileId > 0);
            if (tpiColl.LoadAll())
            {
                args.MessageText = "Payment is already verified by finance.";
                args.IsCancel = true;
                return;
            }

            if (entity.CreatedBy != AppSession.UserLogin.UserID && !_isPowerUser)
            {
                var usr = new AppUser();
                usr.LoadByPrimaryKey(entity.CreatedBy);
                args.MessageText = "You don't have authorization to Un-Approved this transaction. This data belong to: " + usr.UserName + ". Please contact your supervision.";
                args.IsCancel = true;
                return;
            }
            if ((new DateTime()).NowAtSqlServer() > entity.LastUpdateDateTime.Value.AddHours(AppSession.Parameter.TimeLimitForVoidPayment) && !_isPowerUser)
            {
                args.MessageText = "You don't have authorization to Un-Approved this transaction. Time limit already exceeded.";
                args.IsCancel = true;
                return;
            }

            // cek sudah masuk jasmed atau belum
            var msg = ParamedicFeeTransChargesItemCompByDischargeDate.IsParamedicFeeVerified(txtPaymentNo.Text, true);
            if (!string.IsNullOrEmpty(msg))
            {
                ShowInformationHeader(msg);
                args.IsCancel = true;
                return;
            }

            var closingperiod = (new DateTime()).NowAtSqlServer();
            var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod);
            if (isClosingPeriod)
            {
                args.MessageText = "Financial statements for period: " +
                                   string.Format("{0:MMMM-yyyy}", closingperiod) +
                                   " have been closed. Please contact the authorities.";
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, false, reason);
        }

        private void SetApproval(TransPayment entity, bool isApprove, string voidReason)
        {
            entity.IsApproved = isApprove;
            entity.IsVoid = !isApprove;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            if (isApprove)
            {
                entity.ApproveByUserID = AppSession.UserLogin.UserID;
                entity.ApproveDate = (new DateTime()).NowAtSqlServer();
            }
            else {
                entity.VoidByUserID = AppSession.UserLogin.UserID;
                entity.VoidDate = (new DateTime()).NowAtSqlServer();
                entity.VoidReason = voidReason;
            }

            foreach (TransPaymentItemOrder item in TransPaymentItemOrders)
            {
                item.IsPaymentProceed = isApprove;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                TransPaymentItemOrders.Save();

                /* Automatic Journal Testing Start */
                if (isApprove)
                {
                    /* Automatic Journal Testing Start */
                    if (entity.TransactionCode == TransactionCode.PaymentReturn)
                    {
                        //if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "Yes")
                        //{
                        //    int? journalId = JournalTransactions.AddNewPaymentReturnCashBasedJournal(BusinessObject.JournalType.PaymentReturn, entity, reg, this.TransPaymentItems, "TP", AppSession.UserLogin.UserID);
                        //}
                        //else
                        //{

                        //    int? journalId = JournalTransactions.AddNewPaymentReturnJournal(BusinessObject.JournalType.PaymentReturn, entity, reg, this.TransPaymentItems, "TP", AppSession.UserLogin.UserID, 0);

                        //}

                        //function ini utk payment return dari pembelian resep
                        int? journalId =
                            JournalTransactions.AddNewPaymentReturnJournal(BusinessObject.JournalType.PaymentReturn,
                                entity, reg, this.TransPaymentItems, "TP",
                                AppSession.UserLogin.UserID, 0);
                    }
                    else if (entity.TransactionCode == TransactionCode.Payment)
                    {
                        //if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "Yes")
                        //{
                        //    int? journalId =
                        //        JournalTransactions.AddNewPaymentCashBasedJournal(BusinessObject.JournalType.Payment,
                        //                                                          entity, reg, this.TransPaymentItems,
                        //                                                          "TP", entity.PaymentNo,
                        //                                                          entity.CreatedBy, 0);
                        //}
                        //else
                        //{
                        //    int? journalId = JournalTransactions.AddNewPaymentJournal(BusinessObject.JournalType.Payment, entity, reg, this.TransPaymentItems, "TP", AppSession.UserLogin.UserID, 0);
                        //}

                        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "Yes")
                        {
                            int? journalId = JournalTransactions.AddNewPaymentCashBasedJournal(BusinessObject.JournalType.Payment,
                                entity, reg, this.TransPaymentItems,
                                "TP", entity.PaymentNo, AppSession.UserLogin.UserID, 0);
                        }
                        else
                        {
                            var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                            if (type.Contains(reg.SRRegistrationType))
                            {

                                int? journalId = JournalTransactions.AddNewPaymentCashBasedJournalTemporary(BusinessObject.JournalType.Payment,
                                    entity, reg, this.TransPaymentItems,
                                    "TP", entity.PaymentNo, AppSession.UserLogin.UserID, 0);
                            }
                            else
                            {
                                int? journalId = JournalTransactions.AddNewPaymentCashBasedJournal(BusinessObject.JournalType.Payment,
                                                                                                   entity, reg,
                                                                                                   this.TransPaymentItems,
                                                                                                   "TP", entity.PaymentNo,
                                                                                                   AppSession.UserLogin.UserID, 0);
                            }
                        }
                   }
                    /* Automatic Journal Testing End */

                    //if (AppSession.Parameter.IsPhysicianFeeBasedOnPayment == "Yes")
                    //{
                    //    int? x = ParamedicFeeTransChargesItemCompSettled.AddNewSettled(entity, TransPaymentItemOrders,
                    //                                           AppSession.UserLogin.UserID);
                    //}
                }
                else
                {
                    var collibguar = new TransPaymentItemIntermBillGuarantorCollection();
                    collibguar.Query.Where(collibguar.Query.PaymentNo == entity.PaymentNo);
                    collibguar.LoadAll();

                    foreach (var item in collibguar)
                    {
                        item.IsPaymentProceed = false;
                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    }

                    var collib = new TransPaymentItemIntermBillCollection();
                    collib.Query.Where(collib.Query.PaymentNo == entity.PaymentNo);
                    collib.LoadAll();

                    foreach (var item in collib)
                    {
                        item.IsPaymentProceed = false;
                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    }

                    var collbuffer = new CostCalculationBufferCollection();
                    collbuffer.Query.Where(collbuffer.Query.PaymentNo == entity.PaymentNo);
                    collbuffer.LoadAll();

                    foreach (var item in collbuffer)
                    {
                        item.PaymentNo = null;
                    }

                    var collac = new AskesCovered2Collection();
                    collac.Query.Where(collac.Query.PaymentNo == entity.PaymentNo);
                    collac.LoadAll();

                    foreach (var item in collac)
                    {
                        item.PaymentNo = null;
                        item.IsPaid = false;
                    }

                    collib.Save();
                    collibguar.Save();
                    collbuffer.Save();
                    collac.Save();

                    if (entity.TransactionCode == TransactionCode.PaymentReturn)
                    {
                        //if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "Yes")
                        //{
                        //    int? journalId = JournalTransactions.AddNewPaymentReturnCorrectionJournalCashBased(BusinessObject.JournalType.PaymentReturn, entity, reg, this.TransPaymentItems, "TP", AppSession.UserLogin.UserID);
                        //}
                        //else
                        //{
                        //    int? journalId = JournalTransactions.AddNewPaymentReturnCorrectionJournal(BusinessObject.JournalType.PaymentReturn, entity, reg, this.TransPaymentItems, "TP", AppSession.UserLogin.UserID, 0);
                        //}

                        int? journalId =
                         JournalTransactions.AddNewPaymentReturnCorrectionJournal(BusinessObject.JournalType.Payment,
                                                                                  entity, reg, this.TransPaymentItems,
                                                                                  "TP", entity.CreatedBy, 0);

                     }
                    else if (entity.TransactionCode == TransactionCode.Payment)
                    {
                        //if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "Yes")
                        //{
                        //    int? journalId = JournalTransactions.AddNewPaymentCorrectionJournalCashBased(BusinessObject.JournalType.Payment, entity, reg, this.TransPaymentItems, "TP", entity.PaymentNo, AppSession.UserLogin.UserID, 0);
                        //}
                        //else
                        //{
                        //    int? journalId = JournalTransactions.AddNewPaymentCorrectionJournal(BusinessObject.JournalType.Payment, entity, reg, this.TransPaymentItems, "TP", AppSession.UserLogin.UserID, 0);
                        //}

                        if (AppSession.Parameter.IsUsingIntermBill)
                        {
                            int? journalId = JournalTransactions.AddNewPaymentCorrectionJournalCashBased(BusinessObject.JournalType.Payment, entity, reg, this.TransPaymentItems, "TP", entity.PaymentNo, AppSession.UserLogin.UserID, 0);
                        }
                        else
                        {
                            int? journalId =
                                JournalTransactions.AddNewPaymentCorrectionJournal(BusinessObject.JournalType.Payment, entity,
                                                                                   reg, this.TransPaymentItems, "TP",
                                                                                   entity.CreatedBy, 0);
                        }
                     }

                    //if (AppSession.Parameter.IsPhysicianFeeBasedOnPayment == "Yes")
                    //{
                    //    int? x = ParamedicFeeTransChargesItemCompSettled.DeleteSettled(entity, false);
                    //}
                }

                // update informasi payment jasmed
                var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                if (isApprove)
                {
                    feeColl.RecalculateForFeeByPlafonGuarantor(entity, TransPaymentItems, AppSession.UserLogin.UserID);
                    feeColl.SetPayment(entity, 1, AppSession.UserLogin.UserID);
                }
                else {
                    feeColl.UnSetPayment(entity, AppSession.UserLogin.UserID);
                }
                feeColl.Save();


                if (AppSession.Parameter.IsJobOrderRealizationNeedConfirm)
                {
                    if (isApprove)
                    {
                        foreach (var item in TransPaymentItemOrders)
                        {
                            var tci = new TransChargesItem();
                            if (tci.LoadByPrimaryKey(item.TransactionNo, item.SequenceNo))
                            {
                                tci.IsPaymentConfirmed = true;
                                tci.PaymentConfirmedBy = entity.PrintReceiptAsName;
                                tci.PaymentConfirmedDateTime = (new DateTime()).NowAtSqlServer().Date;
                                tci.LastPaymentConfirmedByUserID = AppSession.UserLogin.UserID;
                                tci.LastPaymentConfirmedDateTime = (new DateTime()).NowAtSqlServer();
                                tci.Save();
                            }
                        }
                    }
                    else
                    {
                        foreach (var item in TransPaymentItemOrders)
                        {
                            var tci = new TransChargesItem();
                            if (tci.LoadByPrimaryKey(item.TransactionNo, item.SequenceNo))
                            {
                                tci.IsPaymentConfirmed = false;
                                tci.PaymentConfirmedBy = string.Empty;
                                tci.str.PaymentConfirmedDateTime = string.Empty;
                                tci.LastPaymentConfirmedByUserID = AppSession.UserLogin.UserID;
                                tci.LastPaymentConfirmedDateTime = (new DateTime()).NowAtSqlServer();
                                tci.Save();
                            }
                        }
                    }
                }

                #region Membership - Update Reward Point

                var totPatientPayment = TransPaymentItems.Where(item => item.SRPaymentType == AppSession.Parameter.PaymentTypePayment).Sum(item => (item.Amount ?? 0));

                if (isApprove && reg.MembershipDetailID == -1)
                {
                    reg.MembershipDetailID = Registration.GetMembershipDetailId(reg.PatientID, reg.RegistrationDate.Value.Date);
                    if (reg.MembershipDetailID != -1)
                        reg.Save();
                }
                if (reg.MembershipDetailID != -1)
                {
                    var div = AppSession.Parameter.MultipleForRewardPoints;
                    var x = BusinessObject.MembershipDetail.UpdateRewardPoints(Convert.ToInt64(reg.MembershipDetailID), totPatientPayment, div, isApprove, AppSession.UserLogin.UserID);
                }
                if (!string.IsNullOrEmpty(reg.MembershipNo))
                {
                    var div = AppSession.Parameter.MultipleForRewardPointsForEmployee;
                    var x = BusinessObject.MembershipDetail.UpdateEmployeeRewardPoints(reg.MembershipNo, reg.RegistrationNo, totPatientPayment, div, isApprove, AppSession.UserLogin.UserID);
                }
                #endregion

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            if (AppSession.Parameter.IsUsingValidationUserAccessOnPaymentReceive)
            {
                if (!Helper.IsValidUserAuthorization(ProgramID, AppConstant.UserAccessType.Void))
                {
                    args.MessageText = "You don't have authorization to void this transaction.";
                    args.IsCancel = true;
                    return;
                }
            }

            var entity = new TransPayment();
            if (!entity.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            if (entity.CreatedBy != AppSession.UserLogin.UserID && !_isPowerUser)
            {
                var usr = new AppUser();
                usr.LoadByPrimaryKey(entity.CreatedBy);

                args.MessageText = "You don't have authorization to void this transaction. This data belong to: " +
                                   usr.UserName + ". Please contact your supervision.";
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            //var entity = new TransPayment();
            //if (!entity.LoadByPrimaryKey(txtPaymentNo.Text))
            //{
            //    args.MessageText = AppConstant.Message.RecordNotExist;
            //    args.IsCancel = true;
            //    return;
            //}

            //SetVoid(entity, false);
        }

        private void SetVoid(TransPayment entity, bool isVoid)
        {
            entity.IsVoid = isVoid;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            TransPaymentItemOrders.MarkAllAsDeleted();

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                TransPaymentItemOrders.Save();

                trans.Complete();
            }
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemTransPaymentItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new TransPayment();
            if (parameters.Length > 0)
            {
                String paymentNo = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(paymentNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtPaymentNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            using (new esTransactionScope())
            {
                var transPayment = (TransPayment)entity;
                txtPaymentNo.Text = transPayment.PaymentNo;
                txtRegistrationNo.Text = transPayment.RegistrationNo;

                var registration = new Registration();
                registration.LoadByPrimaryKey(txtRegistrationNo.Text);
                txtServiceUnitID.Text = registration.ServiceUnitID;
                var patient = new Patient();
                if (patient.LoadByPrimaryKey(registration.str.PatientID))
                {
                    txtMedicalNo.Text = patient.MedicalNo;
                    txtPatientName.Text = patient.PatientName;
                    var std = new AppStandardReferenceItem();
                    txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
                }
                else
                {
                    txtMedicalNo.Text = string.Empty;
                    txtPatientName.Text = string.Empty;
                    txtSalutation.Text = string.Empty;
                }
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(txtServiceUnitID.Text);
                lblServiceUnitName.Text = unit.ServiceUnitName;

                txtPrintReceiptAsName.Text = transPayment.PrintReceiptAsName;
                txtGuarantorID.Text = registration.GuarantorID;
                var guarantor = new Guarantor();
                guarantor.LoadByPrimaryKey(txtGuarantorID.Text);
                lblGuarantorName.Text = guarantor.GuarantorName;

                txtPaymentDate.SelectedDate = transPayment.PaymentDate;
                txtPaymentTime.Text = transPayment.PaymentTime;

                ViewState["IsVoid"] = transPayment.IsVoid ?? false;
                ViewState["IsApproved"] = transPayment.IsApproved ?? false;

                txtNotes.Text = transPayment.Notes;
                txtInitial.Text = transPayment.Initial;
                txtOrderAmount.Value = Convert.ToDouble(transPayment.TotalPaymentAmount);

                PopulateTransPaymentItemGrid();
                PopulateTransPaymentItemOrder();

                //CalculateOrderAmount();
            }
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(TransPayment entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtPaymentNo.Text = GetNewPaymentNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.PaymentNo = txtPaymentNo.Text;
            entity.TransactionCode = TransactionCode.Payment; // (decimal)txtOrderAmount.Value >= 0 ? TransactionCode.Payment : TransactionCode.PaymentReturn;
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.PaymentDate = txtPaymentDate.SelectedDate;
            entity.PaymentTime = txtPaymentTime.Text;
            entity.PrintReceiptAsName = txtPrintReceiptAsName.Text;
            entity.PaymentReferenceNo = string.Empty; //(decimal)txtOrderAmount.Value >= 0 ? string.Empty : txtPaymentNo.Text;
            entity.TotalPaymentAmount = (decimal)txtOrderAmount.Value;
            entity.RemainingAmount = 0;
            entity.PrintNumber = 0;
            entity.IsVoid = false;
            entity.IsApproved = false;
            entity.Notes = txtNotes.Text;
            entity.Initial = txtInitial.Text;
            entity.GuarantorID = AppSession.Parameter.SelfGuarantor; //txtGuarantorID.Text;
            entity.IsToGuarantor = false;

            if (entity.es.IsAdded)
            {
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entity.CashManagementNo = CashManagementNo;
            }

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (TransPaymentItem item in TransPaymentItems)
            {
                item.PaymentNo = entity.PaymentNo;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            //string paymentReffNo = string.Empty;
            foreach (var item in TransPaymentItemOrders)
            {
                item.PaymentNo = entity.PaymentNo;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }

                //var tx = new TransPrescription();
                //if (tx.LoadByPrimaryKey(item.TransactionNo))
                //{
                //    var py = new TransPaymentItemOrderCollection();
                //    py.Query.Where(py.Query.TransactionNo == tx.ReferenceNo && py.Query.SequenceNo == item.SequenceNo &&
                //                   py.Query.IsPaymentProceed == true && py.Query.IsPaymentReturned == false);
                //    py.LoadAll();

                //    if (py.Count > 0)
                //    {
                //        foreach (var x in py)
                //        {
                //            paymentReffNo = x.PaymentNo;
                //        }
                //    }
                //}
            }

            //if ((decimal)txtOrderAmount.Value < 0 && !(string.IsNullOrEmpty(paymentReffNo)))
            //    entity.PaymentReferenceNo = paymentReffNo;
        }

        private void SaveEntity(TransPayment entity)
        {
            using (var trans = new esTransactionScope())
            {
                //autonumber has been saved on SetEntity
                //if (DataModeCurrent == DataMode.New)
                //    _autoNumber.Save();

                entity.Save();
                TransPaymentItems.Save();
                TransPaymentItemOrders.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new TransPaymentQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                    que.PaymentNo > txtPaymentNo.Text,
                    que.Or(que.TransactionCode == TransactionCode.Payment, que.TransactionCode == TransactionCode.PaymentReturn)
                    );
                que.OrderBy(que.PaymentNo.Ascending);
            }
            else
            {
                que.Where
                    (
                    que.PaymentNo < txtPaymentNo.Text,
                    que.Or(que.TransactionCode == TransactionCode.Payment, que.TransactionCode == TransactionCode.PaymentReturn)
                    );
                que.OrderBy(que.PaymentNo.Descending);
            }

            var entity = new TransPayment();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        private string GetNewPaymentNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer(), AppEnum.AutoNumber.PaymentNo);
            return _autoNumber.LastCompleteNumber;
        }

        #endregion

        #region Record Detail Method Function TransPaymentItem

        private TransPaymentItemCollection TransPaymentItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["PaymentReceiveDirect:collTransPaymentItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((TransPaymentItemCollection)(obj));
                    }
                }

                var coll = new TransPaymentItemCollection();
                var query = new TransPaymentItemQuery("a");
                var srQuery = new PaymentMethodQuery("b");
                var srQuery2 = new PaymentTypeQuery("c");

                query.Select
                    (
                    query,
                    srQuery2.PaymentTypeName.As("refToAppStandardReferenceItem_PaymentType"),
                    srQuery.PaymentMethodName.As("refToAppStandardReferenceItem_PaymentMethod")
                    //,
                    //"<CASE WHEN a.AmountReceived > 0 THEN a.AmountReceived - a.Amount ELSE 0 END AS 'refToTransPaymentItem_Change'>" 
                    );
                query.InnerJoin(srQuery2).On(query.SRPaymentType == srQuery2.SRPaymentTypeID);
                query.LeftJoin(srQuery).On
                   (
                       srQuery2.SRPaymentTypeID == srQuery.SRPaymentTypeID &
                       query.SRPaymentMethod == srQuery.SRPaymentMethodID
                   );
                query.Where(query.PaymentNo == txtPaymentNo.Text);
                query.OrderBy(query.SequenceNo.Ascending);

                coll.Load(query);

                Session["PaymentReceiveDirect:collTransPaymentItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["PaymentReceiveDirect:collTransPaymentItem" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemTransPaymentItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdTransPaymentItem.Columns[0].Visible = isVisible;
            grdTransPaymentItem.Columns[grdTransPaymentItem.Columns.Count - 1].Visible = isVisible;

            grdTransPaymentItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdTransPaymentItem.Rebind();

            grdOrderItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            if (IsPostBack)
                grdOrderItem.Rebind();

            btnOrderItem.Enabled = isVisible;
        }

        private void PopulateTransPaymentItemGrid()
        {
            //Display Data Detail
            TransPaymentItems = null; //Reset Record Detail
            grdTransPaymentItem.DataSource = TransPaymentItems; //Requery
            grdTransPaymentItem.MasterTableView.IsItemInserted = false;
            grdTransPaymentItem.MasterTableView.ClearEditItems();
            grdTransPaymentItem.DataBind();
        }

        protected void grdTransPaymentItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransPaymentItem.DataSource = TransPaymentItems;
        }

        protected void grdTransPaymentItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            TransPaymentItem entity = FindTransPaymentItem((string)editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                                                  [TransPaymentItemMetadata.ColumnNames.SequenceNo]);

            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdTransPaymentItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            String sequenceNo = item.OwnerTableView.DataKeyValues[item.ItemIndex][TransPaymentItemMetadata.ColumnNames.SequenceNo].ToString();
            TransPaymentItem entity = FindTransPaymentItem(sequenceNo);

            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdTransPaymentItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            TransPaymentItem entity = TransPaymentItems.AddNew();

            SetEntityValue(entity, e);

            foreach (var item in TransPaymentItems)
            {
                if (item.IsFromDownPayment == false && item.SequenceNo.ToInt() < entity.SequenceNo.ToInt())
                {
                    item.RoundingAmount = 0;
                }
            }

            //Stay in insert mode
            e.Canceled = true;
            grdTransPaymentItem.Rebind();
        }

        private TransPaymentItem FindTransPaymentItem(String sequenceNo)
        {
            TransPaymentItemCollection coll = TransPaymentItems;
            return coll.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        private void SetEntityValue(TransPaymentItem entity, GridCommandEventArgs e)
        {
            var userControl = (PaymentReceiveDirectItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.PaymentNo = txtPaymentNo.Text;
                entity.SequenceNo = userControl.SequenceNo;
                entity.SRPaymentType = userControl.SRPaymentType;
                entity.PaymentTypeName = userControl.PaymentTypeName;
                entity.SRPaymentMethod = userControl.SRPaymentMethod;
                entity.PaymentMethodName = userControl.PaymentMethodName;
                entity.str.SRCardProvider = userControl.SRCardProvider;
                entity.str.SRCardType = userControl.SRCardType;
                entity.str.EDCMachineID = userControl.EDCMachineID;
                entity.CardHolderName = userControl.CardHolderName;
                entity.CardFeeAmount = userControl.CardFeeAmount;
                entity.BankID = userControl.BankID;
                entity.Amount = userControl.Amount;
                entity.RoundingAmount = Math.Round(userControl.RoundingAmount, 2);
                entity.AmountReceived = userControl.AmountReceived;
                entity.Change = userControl.Change;

                entity.IsFromDownPayment = false;
                entity.CardNo = userControl.CardNo;
            }
        }

        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"] && !(bool)ViewState["IsVoid"];
        }

        #region Record Detail Methode Function TransPaymentItemOrder

        private TransPaymentItemOrderCollection TransPaymentItemOrders
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["PaymentReceiveDirect:collTransPaymentItemOrder" + Request.UserHostName];
                    if (obj != null)
                        return ((TransPaymentItemOrderCollection)(obj));
                }

                var coll = new TransPaymentItemOrderCollection();

                var query = new TransPaymentItemOrderQuery("a");
                var header = new VwTransactionQuery("b");
                var item = new ItemQuery("c");
                var su = new ServiceUnitQuery("d");

                query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(su).On(header.ServiceUnitID == su.ServiceUnitID);
                query.Where(query.PaymentNo == txtPaymentNo.Text);

                //var total = new esQueryItem(query, "Total", esSystemType.Decimal);
                //total = query.Qty * query.Price;

                query.Select
                    (
                        query,
                        item.ItemName.As("refToItem_ItemName"),
                        su.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"),
                        header.TransactionDate.As("refTransCharges_TransactionDate"),
                        @"<ISNULL(a.Total, (a.Qty*a.Price)) AS 'refToTransPaymentItemOrder_Total'>"
                        //total.As("refToTransPaymentItemOrder_Total")
                    );

                coll.Load(query);

                Session["PaymentReceiveDirect:collTransPaymentItemOrder" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["PaymentReceiveDirect:collTransPaymentItemOrder" + Request.UserHostName] = value; }
        }

        private void PopulateTransPaymentItemOrder()
        {
            //Display Data Detail
            TransPaymentItemOrders = null; //Reset Record Detail
            grdOrderItem.DataSource = TransPaymentItemOrders;
            grdOrderItem.MasterTableView.IsItemInserted = false;
            grdOrderItem.MasterTableView.ClearEditItems();
            grdOrderItem.DataBind();
        }

        protected void grdOrderItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdOrderItem.DataSource = TransPaymentItemOrders;
        }

        private void CalculateOrderAmount()
        {
            decimal? total = 0;

            if (TransPaymentItemOrders.Count > 0)
            {
                //total = TransPaymentItemOrders.Aggregate(total, (current, item) => current + (item.Qty * item.Price));
                total = TransPaymentItemOrders.Aggregate(total, (current, item) => current + (item.Total ?? (item.Qty * item.Price)));
            }
            txtOrderAmount.Value = Convert.ToDouble(total);
        }

        private void SetNotesValue(string transNo)
        {
            var tp = new TransPrescription();
            if (tp.LoadByPrimaryKey(transNo))
            {
                txtNotes.Text = txtOrderAmount.Value > 0
                                    ? "PEMBAYARAN RESEP " + " (" + transNo + ")"
                                    : "RETUR RESEP " + " (" + transNo + ")";
            }
            else
            {
                var tc = new TransCharges();
                if (tc.LoadByPrimaryKey(transNo))
                {
                    var su = new ServiceUnit();
                    if (su.LoadByPrimaryKey(tc.ToServiceUnitID))
                        txtNotes.Text = "PEMBAYARAN PELAYANAN " + su.ServiceUnitName.ToUpper() + " (" + transNo + ")";
                }
            }
        }

        #endregion

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument.Contains("rebind"))
            {
                var val = eventArgument.Split('|');

                if ((sourceControl as RadGrid).ID == grdOrderItem.ID)
                {
                    CalculateOrderAmount();
                    grdOrderItem.Rebind();
                    grdTransPaymentItem.Rebind();
                    SetNotesValue(val[1]);
                }

            }
            if (eventArgument == "clearlist")
            {
                TransPaymentItemOrders.MarkAllAsDeleted();
                grdOrderItem.Rebind();
                CalculateOrderAmount();

                TransPaymentItems.MarkAllAsDeleted();
                grdTransPaymentItem.Rebind();
            }

            btnOrderItem.Enabled = !(TransPaymentItems.Count > 0);
        }

        private TransPaymentItemOrder FindTransPaymentItemOrder(string transNo, string seqNo)
        {
            var order = (TransPaymentItemOrderCollection)Session["PaymentReceiveDirect:collTransPaymentItemOrder" + Request.UserHostName];
            foreach (TransPaymentItemOrder item in order)
            {
                if (item.TransactionNo == transNo && item.SequenceNo == seqNo)
                    return item;
            }

            return null;
        }
    }
}

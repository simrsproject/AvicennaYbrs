using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class GuarantorPaymentReceiveExcludeDiscount : BasePageDialog
    {
        private AppAutoNumberLast _autoNumber;

        private bool ExcludeDiscount()
        {
            return AppSession.Parameter.IsBillVerifARGuarantorExclDisc;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["src"]) && Request.QueryString["src"] == "eklaim") ProgramID = AppConstant.Program.InacbgProcess;
            else ProgramID = AppConstant.Program.VerificationFinalizeBilling;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRDiscountReason, AppEnum.StandardReference.DiscountReason);

                txtPaymentDate.SelectedDate = (new DateTime()).NowAtSqlServer().Date;
                txtPaymentTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

                txtRegistrationNo.Text = Request.QueryString["regNo"];

                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);

                txtRegistrationDate.SelectedDate = reg.RegistrationDate;
                txtRegistrationTime.Text = reg.RegistrationTime;

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;

                decimal? total = 0, plavon2 = 0, totalPatIns = 0;
                var guarType = string.Empty;

                chkArIsVerified.Visible = false;

                var guar = new Guarantor();
                guar.LoadByPrimaryKey(Request.QueryString["guarId"]);

                trBillRounding.Visible = guar.RoundingTransaction > 0;

                if (Request.QueryString["guarIdBuff"] == "")
                {
                    if (string.IsNullOrEmpty(Request.QueryString["seqNo"]))
                    {
                        if (reg.PlavonAmount > 0)
                        {
                            var coll = new TransPaymentItemCollection();
                            var tpi = new TransPaymentItemQuery("a");
                            var tp = new TransPaymentQuery("b");
                            var rcob = new RegistrationGuarantorQuery("rcob");
                            tpi.InnerJoin(tp).On(tpi.PaymentNo == tp.PaymentNo)
                                .LeftJoin(rcob).On(tp.RegistrationNo == rcob.RegistrationNo & tp.GuarantorID == rcob.GuarantorID) /*exclude yang COB supaya tidak potong plafon utama*/
                                .Where(tp.IsApproved == true,
                                    tpi.SRPaymentType == AppSession.Parameter.PaymentTypeCorporateAR,
                                    tp.RegistrationNo == txtRegistrationNo.Text, 
                                    rcob.GuarantorID.IsNull());
                            coll.Load(tpi);
                            if (coll.Count > 0)
                            {
                                decimal paymentAmt = coll.Sum(c => (c.Amount ?? 0));
                                total = (reg.PlavonAmount - paymentAmt) < 0 ? 0 : (reg.PlavonAmount - paymentAmt);
                            }
                            else
                            {
                                total = reg.PlavonAmount;
                                plavon2 = reg.PlavonAmount2;
                            }
                        }
                        else
                        {
                            if (AppSession.Parameter.IsUsingIntermBill)
                            {
                                var collection = IntermBillGuarantors;

                                total = collection.Aggregate(total, (current, item) => current + (item.GuarantorAmount ?? 0) + (item.GuarantorAdministrationAmount ?? 0) - (item.DiscAdmGuarantor ?? 0));

                            }
                            else
                            {
                                var collection = CostCalculations;

                                total = collection.Aggregate(total, (current, item) => current + (item.GuarantorAmount ?? 0));
                                total += reg.GuarantorAdm;
                            }
                        }
                        //
                        if (guar.SRPhysicianFeeCategory == "02")
                        {
                            /*
                             SRPhysicianFeeCategory == "02" --> jasa medis by percentage of AR
                             */
                            chkArIsVerified.Visible = true;
                            if (!string.IsNullOrEmpty(Request.QueryString["src"])) chkArIsVerified.Checked = true; // untuk dari e-klaim checkbox default tercentang
                        }

                        // total transaksi
                        if (AppSession.Parameter.IsUsingIntermBill)
                        {
                            var collection = IntermBillGuarantors;

                            totalPatIns = collection.Aggregate(totalPatIns, (current, item) =>
                                current + (item.GuarantorAmount ?? 0) + (item.GuarantorAdministrationAmount ?? 0) - (item.DiscAdmGuarantor ?? 0) +
                                (item.PatientAmount ?? 0) + (item.AdministrationAmount ?? 0) - (item.DiscAdmPatient ?? 0));

                            // kurangi pembayaran yang sudah pernah ada dan dikurangi plafon COB
                            var paid = Helper.Payment.GetTotalPaymentByIntermbillIncludePlafonCOB(reg.RegistrationNo, collection.Select(x => x.IntermBillNo).ToArray(), true, true, reg.PlavonAmount.Value);

                            totalPatIns -= paid;
                        }
                        else
                        {
                            var collection = CostCalculations;

                            totalPatIns = collection.Aggregate(totalPatIns, (current, item) => current + (item.GuarantorAmount ?? 0));
                            total += reg.GuarantorAdm;
                        }
                    }
                    //else
                    //{
                    //    guar.LoadByPrimaryKey(AppSession.Parameter.GuarantorAskesID);
                    //    var ac = new AskesCovered2();
                    //    if (ac.LoadByPrimaryKey(Request.QueryString["regNo"], Request.QueryString["seqNo"]))
                    //        total = (ac.RoomAmount * ac.RoomDays) + (ac.IccuAmount * ac.IccuDays) + (ac.HcuAmount * ac.HcuDays) +
                    //                ac.SurgeryAmount + ac.MedicalSupportAmount + ac.HaemodialiseAmount + ac.CtScanAmount;
                    //}
                    txtGuarantorName.Text = guar.GuarantorName;
                    guarType = guar.SRGuarantorType;
                }
                else
                {
                    var guarIdBuff = new Guarantor();
                    guarIdBuff.LoadByPrimaryKey(Request.QueryString["guarIdBuff"]);
                    txtGuarantorName.Text = guarIdBuff.GuarantorName;
                    guarType = guarIdBuff.SRGuarantorType;

                    if (reg.PlavonAmount > 0) total = reg.PlavonAmount;
                    else
                    {
                        var collection = CostCalculationBuffers;

                        if (collection.Count > 0)
                        {
                            total = collection.Aggregate(total, (current, item) => current + (item.GuarantorAmount ?? 0));
                        }
                    }
                }

                txtTotalPaymentAmount.Value = Convert.ToDouble(total);
                if ((reg.PlavonAmount > 0) && (guar.IsExcessPlafonToDiscount ?? false) && totalPatIns > 0) // selisih masukin diskon, lihat settingan guarantor
                {
                    if ((plavon2 ?? 0) > 0)
                    {
                        txtDiscountAmount.Value = ((Convert.ToDouble(totalPatIns) - Convert.ToDouble(plavon2)) < 0) ? 0 : Convert.ToDouble(totalPatIns) - Convert.ToDouble(plavon2);
                    }
                    else
                    {
                        txtDiscountAmount.Value = ((Convert.ToDouble(totalPatIns) - Convert.ToDouble(total)) < 0) ? 0 : Convert.ToDouble(totalPatIns) - Convert.ToDouble(total);
                    }
                    if (!ExcludeDiscount())
                    {
                        txtTotalPaymentAmount.Value = txtTotalPaymentAmount.Value + txtDiscountAmount.Value;
                    }

                    bool isBridging = false;
                    var selisihPat = Temiang.Avicenna.Module.Charges.Cashier.PaymentReceiveDetail
                        .GetSelisihPasienBPJS(
                            reg, ref isBridging,
                            IntermBillGuarantors.Sum(ib => (ib.PatientAmount ?? 0) + (ib.GuarantorAmount ?? 0))
                        );
                    if (isBridging && (selisihPat > 0)) {
                        //var plafon = txtTotalPaymentAmount.Value - txtDiscountAmount.Value;
                        //if ((txtDiscountAmount.Value ?? 0) > Convert.ToDouble(selisihPat))
                        //{
                        //    txtDiscountAmount.Value -= Convert.ToDouble(selisihPat);
                        //}
                        //else {
                        //    txtDiscountAmount.Value = 0;
                        //}
                        //txtTotalPaymentAmount.Value = txtDiscountAmount.Value + plafon;

                        // COB
                        decimal cobPlafon = 0;
                        var rgColl = new RegistrationGuarantorCollection();
                        rgColl.Query.Where(rgColl.Query.RegistrationNo == reg.RegistrationNo);
                        if (rgColl.LoadAll()) {
                            cobPlafon = rgColl.Sum(rg => rg.PlafondAmount ?? 0);
                        }

                        var tBill = IntermBillGuarantors.Sum(ib => (ib.GuarantorAmount ?? 0) + (ib.GuarantorAdministrationAmount ?? 0) - (ib.DiscAdmGuarantor ?? 0) +
                                (ib.PatientAmount ?? 0) + (ib.AdministrationAmount ?? 0) - (ib.DiscAdmPatient ?? 0));

                        tBill -= cobPlafon;

                        var plafon = reg.PlavonAmount ?? 0;
                        var disc = tBill - plafon - selisihPat;
                        if (disc < 0) disc = 0;
                        txtDiscountAmount.Value = Convert.ToDouble(disc);
                        var gPay = plafon + disc;
                        txtTotalPaymentAmount.Value = Convert.ToDouble(gPay);
                    }
                }
                else
                {
                    txtDiscountAmount.Value = 0;
                }

                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH" && guarType == AppSession.Parameter.GuarantorTypeDiscount) txtDiscountAmount.ReadOnly = true;

                if (!string.IsNullOrEmpty(Request.QueryString["src"]) && txtDiscountAmount.Value > 0)
                    cboSRDiscountReason.SelectedValue = AppSession.Parameter.DiscountReasonSelisihKlaimBpjs; //"DiscountReason-011"; // untuk dari e-klaim default selisih ke kode : DiscountReason-011, nama : Selisih Klaim BPJS 
                if (guar.RoundingTransaction > 0)
                {
                    txtTotalPaymentAmount.Value = Convert.ToDouble(Helper.BillRounding(total ?? 0, guar.RoundingTransaction ?? 0, guar.IsUsingRoundingDown ?? false));
                    txtRoundingAmount.Value = txtTotalPaymentAmount.Value - Convert.ToDouble(total);
                }
                else txtRoundingAmount.Value = 0;
            }
        }

        private string[] MergeRegistrationList()
        {
            if (ViewState["BillingVerification:MergeRegistration" + Request.UserHostName] == null)
                ViewState["BillingVerification:MergeRegistration" + Request.UserHostName] = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regNo"]);

            return (string[])ViewState["BillingVerification:MergeRegistration" + Request.UserHostName];
        }

        private string GetNewPaymentNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer(), AppEnum.AutoNumber.PaymentNo);
            return _autoNumber.LastCompleteNumber;
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            decimal discAmt = Convert.ToDecimal(txtDiscountAmount.Value);
            if (chkIsDiscountInPercent.Checked)
                discAmt = Convert.ToDecimal(txtTotalPaymentAmount.Value) * (Convert.ToDecimal(txtDiscountAmount.Value) / 100);

            //if (txtDiscountAmount.Value != 0 && string.IsNullOrEmpty(cboSRDiscountReason.SelectedValue))
            if (discAmt != 0 && string.IsNullOrEmpty(cboSRDiscountReason.SelectedValue))
            {
                ShowInformationHeader("Discount Reason required.");
                return false;
            }

            if (!ExcludeDiscount())
            {
                //if (txtDiscountAmount.Value != 0 && txtDiscountAmount.Value > txtTotalPaymentAmount.Value)
                if (discAmt != 0 && discAmt > Convert.ToDecimal(txtTotalPaymentAmount.Value))
                {
                    ShowInformationHeader(string.Format("Total discount amount ({0}) can't be greater then Receipt amount.", string.Format("{0:0,0.00}", discAmt)));
                    return false;
                }
            }

            var collection = IntermBillGuarantors;
            if (collection.Count == 0)
            {
                ShowInformationHeader("Corporate A/R can't be process. There is no Interm Bill available.");
                return false;
            }

            var closingperiod = txtPaymentDate.SelectedDate;
            var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod.Value);
            if (isClosingPeriod)
            {
                ShowInformationHeader("Financial statements for period: " +
                                   string.Format("{0:MMMM-yyyy}", closingperiod) +
                                   " have been closed. Please contact the authorities.");
                return false;
            }

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            string guarId = Request.QueryString["seqNo"] == "" ? (Request.QueryString["guarIdBuff"] == ""
                                     ? Request.QueryString["guarId"]
                                     : Request.QueryString["guarIdBuff"]) : AppSession.Parameter.GuarantorAskesID[0];

            if (string.IsNullOrEmpty(guarId))
                guarId = reg.GuarantorID;
            var guar = new Guarantor();
            guar.LoadByPrimaryKey(guarId);

            var printReceiptAsName = guar.GuarantorName;
            var srPaymentType = string.IsNullOrEmpty(guar.SRPaymentType) ? AppSession.Parameter.PaymentTypeCorporateAR : guar.SRPaymentType;

            if (srPaymentType == AppSession.Parameter.PaymentTypeSaldoAR && !AppSession.Parameter.IsAllowGuarantorDepositBalanceMinus)
            {
                var msg = string.Empty;
                //cek deposit balance
                var b = new GuarantorDepositBalance();
                if (b.LoadByPrimaryKey(guarId))
                {
                    //if (b.BalanceAmount < (Convert.ToDecimal(txtTotalPaymentAmount.Value) - Convert.ToDecimal(txtDiscountAmount.Value)))
                    if (b.BalanceAmount < (Convert.ToDecimal(txtTotalPaymentAmount.Value) - discAmt))
                        msg = "There is insufficient Deposit Balance for Guarantor : " + printReceiptAsName;
                }
                else
                    msg = "There is no Deposit Balance for Guarantor : " + printReceiptAsName;

                if (msg != string.Empty)
                {
                    ShowInformationHeader(msg);
                    return false;
                }
            }

            //---TransPayment
            var entity = new TransPayment();
            entity.AddNew();
            entity.TransactionCode = TransactionCode.Payment;
            entity.PaymentNo = GetNewPaymentNo();
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.PaymentDate = txtPaymentDate.SelectedDate;
            entity.PaymentTime = txtPaymentTime.TextWithLiterals;
            entity.PrintReceiptAsName = txtGuarantorName.Text;
            //entity.TotalPaymentAmount = (ExcludeDiscount() && reg.PlavonAmount > 0) ?
            //    (Convert.ToDecimal(txtTotalPaymentAmount.Value) + Convert.ToDecimal(txtDiscountAmount.Value)) :
            //    Convert.ToDecimal(txtTotalPaymentAmount.Value);
            entity.TotalPaymentAmount = (ExcludeDiscount() && reg.PlavonAmount > 0) ?
                (Convert.ToDecimal(txtTotalPaymentAmount.Value) + discAmt) :
                Convert.ToDecimal(txtTotalPaymentAmount.Value);
            entity.RemainingAmount = 0;
            entity.PrintNumber = 0;
            entity.PaymentReceiptNo = string.Empty;
            entity.CreatedBy = AppSession.UserLogin.UserID;
            entity.IsVoid = false;
            entity.IsApproved = true;
            entity.Notes = string.Empty;

            entity.GuarantorID = guarId;
            entity.PrintReceiptAsName = printReceiptAsName;
            entity.IsToGuarantor = true;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.ApproveByUserID = AppSession.UserLogin.UserID;
            entity.ApproveDate = (new DateTime()).NowAtSqlServer();
            entity.Notes = txtNotes.Text;

            if (guar.SRPhysicianFeeCategory == "02")
            {
                entity.IsGuarantorVerified = chkArIsVerified.Checked;
            }

            //---TransPaymentItem
            var collPaymentItem = new TransPaymentItemCollection();
            var pi = collPaymentItem.AddNew();
            pi.PaymentNo = entity.PaymentNo;
            pi.SequenceNo = "001";
            pi.SRPaymentType = srPaymentType;

            var type = new PaymentType();
            pi.PaymentTypeName = type.LoadByPrimaryKey(pi.SRPaymentType) ? type.PaymentTypeName : string.Empty;

            pi.SRPaymentMethod = string.Empty;
            pi.PaymentMethodName = string.Empty;
            //pi.Amount = (ExcludeDiscount() && reg.PlavonAmount > 0) ?
            //    Convert.ToDecimal(txtTotalPaymentAmount.Value) :
            //    (Convert.ToDecimal(txtTotalPaymentAmount.Value) - Convert.ToDecimal(txtDiscountAmount.Value));
            pi.Amount = (ExcludeDiscount() && reg.PlavonAmount > 0) ?
                Convert.ToDecimal(txtTotalPaymentAmount.Value) :
                (Convert.ToDecimal(txtTotalPaymentAmount.Value) - discAmt);
            //pi.RoundingAmount = 0;
            pi.RoundingAmount = Convert.ToDecimal(txtRoundingAmount.Value);
            pi.Balance = 0;
            pi.IsFromDownPayment = false;
            pi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            pi.LastUpdateByUserID = AppSession.UserLogin.UserID;

            //if (txtDiscountAmount.Value > 0)
            if (discAmt > 0)
            {
                var pi2 = collPaymentItem.AddNew();
                pi2.PaymentNo = entity.PaymentNo;
                pi2.SequenceNo = "002";
                pi2.SRPaymentType = AppSession.Parameter.PaymentTypeDiscount;

                type = new PaymentType();
                type.LoadByPrimaryKey(pi.SRPaymentType);
                pi2.PaymentTypeName = type.PaymentTypeName;

                pi2.SRPaymentMethod = string.Empty;
                pi2.PaymentMethodName = string.Empty;
                pi2.SRDiscountReason = cboSRDiscountReason.SelectedValue;
                pi2.Amount = discAmt;//Convert.ToDecimal(txtDiscountAmount.Value);
                pi2.RoundingAmount = 0;
                pi2.Balance = 0;
                pi2.IsFromDownPayment = false;
                pi2.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                pi2.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
            else
            {
                //tambahan spesifik untuk rs grha mm2100, masih beta
                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitial) == "GRHA")
                {
                    if (txtDiscountAmount.Value == 0)
                    {
                        pi.SRDiscountReason = cboSRDiscountReason.SelectedValue;
                    }
                }
            }

            if (CostCalculationBuffers.Count > 0)
            {
                foreach (CostCalculationBuffer item in CostCalculationBuffers)
                {
                    item.PaymentNo = entity.PaymentNo;
                }
            }

            //---TransPaymentItemIntermBillGuarantor -> untuk cost calculation yang ada interm bill
            var collPaymentIntermBill = new TransPaymentItemIntermBillGuarantorCollection();
            foreach (IntermBill item in IntermBillGuarantors)
            {
                var pib = collPaymentIntermBill.AddNew();
                pib.PaymentNo = entity.PaymentNo;
                pib.IntermBillNo = item.IntermBillNo;
                pib.IsPaymentProceed = true;
                pib.IsPaymentReturned = false;
                pib.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                pib.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }

            //reg.RemainingAmount -= (decimal)(txtTotalPaymentAmount.Value + txtDiscountAmount.Value);
            reg.RemainingAmount -= (decimal)(txtTotalPaymentAmount.Value) + discAmt;

            var acColl = new AskesCovered2Collection();
            acColl.Query.Where(acColl.Query.RegistrationNo == Request.QueryString["regNo"],
                               acColl.Query.SeqNo == Request.QueryString["seqNo"]);
            acColl.LoadAll();
            foreach (var ac in acColl)
            {
                ac.IsPaid = true;
                ac.PaymentNo = entity.PaymentNo;
            }

            #region Close Registration
            if ((reg.SRRegistrationType == AppConstant.RegistrationType.InPatient && AppSession.Parameter.IsAutoClosedRegIpOnPayment) ||
                (reg.SRRegistrationType != AppConstant.RegistrationType.InPatient && AppSession.Parameter.IsAutoClosedRegOpOnPayment))
            {
                string[] regno = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);

                //var isClosed = Helper.RegistrationOpenClose.GetStatusClosed(reg, regno, 0, (decimal)(txtTotalPaymentAmount.Value + txtDiscountAmount.Value));
                var isClosed = Helper.RegistrationOpenClose.GetStatusClosed(reg, regno, 0, (decimal)(txtTotalPaymentAmount.Value) + discAmt);
                if (isClosed)
                {
                    var isAutoClosedRegOnPaymentWithHoldTx = AppSession.Parameter.IsAutoClosedRegOnPaymentWithHoldTx;

                    var coll = new MergeBillingCollection();
                    coll.Query.Where(coll.Query.FromRegistrationNo == entity.RegistrationNo);
                    coll.LoadAll();

                    var regs = new string[coll.Count + 1];
                    var idx = 1;

                    regs.SetValue(entity.RegistrationNo, 0);

                    foreach (var bill in coll)
                    {
                        regs.SetValue(bill.RegistrationNo, idx);
                        idx++;
                    }

                    var regis = new RegistrationCollection();
                    regis.Query.Where(regis.Query.RegistrationNo.In(regs));
                    regis.LoadAll();

                    var historys = new RegistrationCloseOpenHistoryCollection();

                    foreach (var re in regis)
                    {
                        var hist = historys.AddNew();
                        hist.RegistrationNo = re.RegistrationNo;

                        if (!isAutoClosedRegOnPaymentWithHoldTx)
                        {
                            re.IsClosed = true;

                            hist.StatusId = "C";
                        }
                        else
                        {
                            re.IsHoldTransactionEntry = true;
                            re.IsHoldTransactionEntryByUserID = AppSession.UserLogin.UserID;

                            hist.StatusId = "H";
                        }
                        re.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        re.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        hist.IsTrue = true;
                        hist.Notes = "Verification & Finalize Billing >> Guarantor Receipt (Corporate A/R)";
                        hist.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        hist.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }

                    var ques = new ServiceUnitQueCollection();
                    ques.Query.Where(ques.Query.RegistrationNo.In(regs));
                    ques.LoadAll();

                    foreach (var que in ques)
                    {
                        if (!isAutoClosedRegOnPaymentWithHoldTx)
                            que.IsClosed = true;

                        que.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        que.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    }

                    using (var trans = new esTransactionScope())
                    {
                        regis.Save();
                        historys.Save();

                        if (ques.Count > 0)
                            ques.Save();

                        trans.Complete();
                    }

                    var ques2 = new ServiceUnitQueCollection();
                    ques2.Query.Where(ques2.Query.RegistrationNo == entity.RegistrationNo);
                    ques2.LoadAll();

                    foreach (var que in ques2)
                    {
                        if (!isAutoClosedRegOnPaymentWithHoldTx)
                            que.IsClosed = true;
                        que.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        que.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    }

                    using (var trans = new esTransactionScope())
                    {
                        if (ques2.Count > 0)
                            ques2.Save();

                        trans.Complete();
                    }

                    if (!isAutoClosedRegOnPaymentWithHoldTx)
                        reg.IsClosed = true;
                    else
                    {
                        reg.IsHoldTransactionEntry = true;
                        reg.IsHoldTransactionEntryByUserID = AppSession.UserLogin.UserID;
                    }
                }
            }
            #endregion

            using (var trans = new esTransactionScope())
            {
                reg.Save();
                entity.Save();
                collPaymentItem.Save();
                if (Request.QueryString["guarIdBuff"] == "")
                    collPaymentIntermBill.Save();
                else
                    CostCalculationBuffers.Save();
                if (acColl != null)
                    acColl.Save();

                _autoNumber.Save();

                //if (AppSession.Parameter.IsPhysicianFeeArCreateOnArReceipt == "Yes")
                //{
                //    int? x = ParamedicFeeTransChargesItemCompSettled.AddNewSettled(entity, collPaymentIntermBill, AppSession.UserLogin.UserID);
                //}

                #region Guarantor Deposit Balance

                if (srPaymentType == AppSession.Parameter.PaymentTypeSaldoAR)
                {
                    var balance = new GuarantorDepositBalance();
                    var movement = new GuarantorDepositMovement();
                    //GuarantorDepositBalance.PrepareGuarantorDepositBalances(entity.PaymentNo, entity.GuarantorID,
                    //                                                        "A/R Process", AppSession.UserLogin.UserID,
                    //                                                        0,
                    //                                                        (Convert.ToDecimal(txtTotalPaymentAmount.Value) -
                    //                                                         Convert.ToDecimal(txtDiscountAmount.Value)),
                    //                                                        ref balance, ref movement);
                    GuarantorDepositBalance.PrepareGuarantorDepositBalances(entity.PaymentNo, entity.GuarantorID,
                                                                            "A/R Process", AppSession.UserLogin.UserID,
                                                                            0,
                                                                            (Convert.ToDecimal(txtTotalPaymentAmount.Value) -
                                                                             discAmt),
                                                                            ref balance, ref movement);
                    balance.Save();
                    movement.Save();
                }

                #endregion

                // update informasi payment jasmed
                var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                feeColl.RecalculateForFeeByPlafonGuarantor(entity, collPaymentItem, AppSession.UserLogin.UserID);
                feeColl.SetPayment(entity, collPaymentItem, 0, AppSession.UserLogin.UserID);
                feeColl.Save();

                // rekal untuk prorata ???
                var ba = new BillingAdjustment(entity.RegistrationNo, true);
                var msg = ba.CalculateAndSaveProrata_NoTransactionScope(AppSession.UserLogin.UserID);
                if (!string.IsNullOrEmpty(msg)) {
                    ShowInformationHeader(msg);
                    return false;
                }

                trans.Complete();
            }

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "Yes")
            {
                int? journalId = JournalTransactions.AddNewPaymentCashBasedJournal(BusinessObject.JournalType.ARCreating,
                    entity, reg, collPaymentItem,
                    "TP", entity.PaymentNo, AppSession.UserLogin.UserID, 0);
            }
            else
            {
                var rtype = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                if (rtype.Contains(reg.SRRegistrationType))
                {

                    int? journalId = JournalTransactions.AddNewPaymentCashBasedJournalTemporary(BusinessObject.JournalType.ARCreating,
                        entity, reg, collPaymentItem,
                        "TP", entity.PaymentNo, AppSession.UserLogin.UserID, 0);
                }
                else
                {
                    int? journalId = JournalTransactions.AddNewPaymentCashBasedJournal(BusinessObject.JournalType.ARCreating,
entity, reg, collPaymentItem,
"TP", entity.PaymentNo, AppSession.UserLogin.UserID, 0);
                }
            }

            // checkout otomatis,
            Helper.RegistrationOpenClose.SetDischargeDate(reg);

            return true;
        }

        private IntermBillCollection IntermBillGuarantors
        {
            get
            {
                var obj = ViewState["VerificationBilling:IntermBillGuarantors" + Request.UserHostName];
                if (obj != null)
                    return ((IntermBillCollection)(obj));

                var registrationNoList = MergeRegistrationList();

                var collection = new IntermBillCollection();

                var query = new IntermBillQuery("a");
                var payib = new TransPaymentItemIntermBillGuarantorQuery("b");
                var cc = new CostCalculationQuery("c");

                query.Select(query);
                query.es.Distinct = true;
                query.LeftJoin(payib).On(
                    query.IntermBillNo == payib.IntermBillNo &&
                    payib.IsPaymentProceed == true &&
                    payib.IsPaymentReturned == false
                    );
                query.InnerJoin(cc).On(query.IntermBillNo == cc.IntermBillNo);
                query.Where(
                    query.RegistrationNo.In(registrationNoList),
                    payib.PaymentNo.IsNull(),
                    query.IsVoid == false
                    );

                collection.Load(query);

                ViewState["VerificationBilling:IntermBillGuarantors" + Request.UserHostName] = collection;

                return collection;
            }
            set { ViewState["IntermBillGuarantors" + Request.UserHostName] = value; }
        }

        private CostCalculationBufferCollection CostCalculationBuffers
        {
            get
            {
                var obj = ViewState["CostCalculationBuffers" + Request.UserHostName];
                if (obj != null)
                    return ((CostCalculationBufferCollection)(obj));

                var registrationNoList = MergeRegistrationList();

                var collection = new CostCalculationBufferCollection();

                var query = new CostCalculationBufferQuery("a");
                query.Select(query);

                query.Where(query.RegistrationNo.In(registrationNoList), query.PaymentNo.IsNull(),
                            query.GuarantorID == Request.QueryString["guarIdBuff"]);

                collection.Load(query);

                ViewState["CostCalculationBuffers" + Request.UserHostName] = collection;

                return collection;
            }
            set { ViewState["CostCalculationBuffers" + Request.UserHostName] = value; }
        }

        private CostCalculationCollection CostCalculations
        {
            get
            {
                var obj = ViewState["VerificationBilling:CostCalculations" + Request.UserHostName];
                if (obj != null)
                    return ((CostCalculationCollection)(obj));

                var registrationNoList = MergeRegistrationList();

                var collection = new CostCalculationCollection();
                var query = new CostCalculationQuery("a");
                var item = new ItemQuery("b");
                var unit = new ServiceUnitQuery("c");
                var view = new VwTransactionQuery("d");
                var pay = new TransPaymentItemOrderQuery("e");
                var txi = new TransChargesItemQuery("y");
                var cls = new ClassQuery("cls");

                query.Select(
                        query,
                        view.TransactionDate.As("refToTransaction_TransactionDate"),
                        unit.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"),
                        @"<CASE WHEN ISNULL(y.ParamedicCollectionName, '') = '' THEN b.ItemName ELSE b.ItemName + ' (' + y.ParamedicCollectionName + ')' END AS 'refToItem_ItemName'>",
                        view.ReferenceNo.As("refToTransaction_ReferenceNo"),
                        cls.ClassName.As("refToClass_ClassName")
                    );

                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(view).On(
                        query.TransactionNo == view.TransactionNo &&
                        view.RegistrationNo.In(registrationNoList)
                    );
                query.LeftJoin(cls).On(view.ClassID == cls.ClassID);
                query.InnerJoin(unit).On(view.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(pay).On(
                    query.TransactionNo == pay.TransactionNo &&
                    query.SequenceNo == pay.SequenceNo &&
                    pay.IsPaymentProceed == true &&
                    pay.IsPaymentReturned == false
                    );
                query.LeftJoin(txi).On(query.TransactionNo == txi.TransactionNo & query.SequenceNo == txi.SequenceNo);

                query.Where(
                    query.RegistrationNo.In(registrationNoList),
                    pay.PaymentNo.IsNull(),
                    query.IntermBillNo.IsNull(),
                    view.PackageReferenceNo == string.Empty
                    );

                query.OrderBy(
                    unit.ServiceUnitName.Ascending,
                    view.OrderDate.Ascending,
                    view.OrderTransNo.Ascending,
                    query.SequenceNo.Ascending,
                    query.TransactionNo.Ascending
                    );

                collection.Load(query);

                ViewState["VerificationBilling:CostCalculations" + Request.UserHostName] = collection;

                return collection;
            }
            set { ViewState["VerificationBilling:CostCalculations" + Request.UserHostName] = value; }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'payment'";
        }
    }
}

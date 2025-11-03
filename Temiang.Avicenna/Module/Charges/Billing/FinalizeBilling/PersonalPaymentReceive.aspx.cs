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
    public partial class PersonalPaymentReceive : BasePageDialog
    {
        private AppAutoNumberLast _autoNumber;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VerificationFinalizeBilling;
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
                txtGuarantorName.Text = pat.PatientName;

                decimal? total = 0;

                #region -new-
                decimal selisih = 0;
                bool isBridging = false;
                /*pengecekan status bridging u/ billing, apakah mengikuti aturan bpjs atau selisih tetap dibayar pasien*/
                //if (AppSession.Parameter.IsBridgingBillingBpjs)
                var isBridgingBillingBpjs = AppParameter.IsYes(AppParameter.ParameterItem.IsBridgingBillingBpjs);
                if (isBridgingBillingBpjs)
                {
                    if (Helper.IsInacbgIntegration)
                    {
                        var ncc = new NccInacbg();
                        ncc.Query.es.Top = 1;
                        ncc.Query.Where(ncc.Query.RegistrationNo.In(txtRegistrationNo.Text));
                        if (ncc.Query.Load())
                        {
                            selisih = ncc.AddPaymentAmt ?? 0;
                            isBridging = true;
                        }
                    }

                    if (!isBridging || selisih == 0)
                    {
                        var bridging = new GuarantorBridging();
                        bridging.Query.Where(bridging.Query.GuarantorID == reg.GuarantorID,
                                             bridging.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(),
                                                                              AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(),
                                                                              AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
                        if (bridging.Query.Load())
                        {
                            isBridging = true;
                            if (reg.CoverageClassID != reg.ChargeClassID || reg.GuarantorID == AppSession.Parameter.SelfGuarantor)
                            {
                                var cov = new RegistrationCoverageDetail();
                                cov.Query.Select(cov.Query.CalculatedAmount.Sum());
                                cov.Query.Where(cov.Query.RegistrationNo == reg.RegistrationNo);
                                if (cov.Query.Load())
                                {
                                    selisih = cov.CalculatedAmount ?? 0;
                                    //if (AppSession.Parameter.IsBridgingBillingBpjsWithCostSharing)
                                    var isBridgingBillingBpjsWithCostSharing = AppParameter.IsYes(AppParameter.ParameterItem.IsBridgingBillingBpjsWithCostSharing);
                                    if (isBridgingBillingBpjsWithCostSharing)
                                    {
                                        //1. cek selisih plafond (75% dari kelas 1)
                                        //2. cek total tagihan - plafond
                                        //3. ambil nilai paling kecil

                                        var class1 = new Class();
                                        class1.LoadByPrimaryKey(reg.CoverageClassID);

                                        var asri1 = new AppStandardReferenceItem();
                                        asri1.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class1.SRClassRL);

                                        if (asri1.Note == "2") // Kelas 1
                                        {
                                            var class2 = new Class();
                                            class2.LoadByPrimaryKey(reg.ChargeClassID);

                                            var asri2 = new AppStandardReferenceItem();
                                            asri2.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class2.SRClassRL);

                                            if (new[] { "0", "1" }.Contains(asri2.Note)) // Kelas VIP, VVIP dll, diatas kelas 1 yg dihitung 75% coverage untuk selisih
                                            {
                                                var cob = new RegistrationGuarantorCollection();
                                                var cobq = new RegistrationGuarantorQuery("a");
                                                var gq = new GuarantorQuery("b");
                                                cobq.InnerJoin(gq).On(gq.GuarantorID == cobq.GuarantorID);
                                                cobq.Where(cobq.RegistrationNo == txtRegistrationNo.Text);
                                                cob.Load(cobq);

                                                decimal cobPlafond = cob.Sum(c => (c.PlafondAmount ?? 0));
                                                var plafon = (reg.PlavonAmount ?? 0) + cobPlafond;

                                                var collection = IntermBillPatients;
                                                var tpatientAmt = collection.Aggregate(total,
                                                                                     (current, item) =>
                                                                                     current + (item.PatientAmount ?? 0) +
                                                                                     (item.AdministrationAmount ?? 0) - (item.DiscAdmPatient ?? 0));

                                                if (selisih > (tpatientAmt ?? 0))
                                                    selisih = (tpatientAmt ?? 0);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if ((reg.PlavonAmount2 ?? 0) > 0)
                                    {
                                        var class1 = new Class();
                                        class1.LoadByPrimaryKey(reg.CoverageClassID);

                                        var asri1 = new AppStandardReferenceItem();
                                        asri1.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class1.SRClassRL);

                                        var class2 = new Class();
                                        class2.LoadByPrimaryKey(reg.ChargeClassID);

                                        var asri2 = new AppStandardReferenceItem();
                                        asri2.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class2.SRClassRL);

                                        if (asri2.Note.ToInt() < asri1.Note.ToInt())
                                            selisih = (reg.PlavonAmount2 ?? 0) - (reg.PlavonAmount ?? 0);
                                    }
                                }
                            }
                        }
                    }
                }
                if (selisih > 0)
                {
                    total = selisih;
                }
                else
                {
                    if (AppSession.Parameter.IsUsingIntermBill)
                    {
                        var collection = IntermBillPatients;
                        if (isBridging)
                        {
                            total = collection.Aggregate(total,
                                                             (current, item) =>
                                                             current + (item.PatientAmount ?? 0) +
                                                             (item.AdministrationAmount ?? 0) - (item.DiscAdmPatient ?? 0));

                            if (reg.CoverageClassID != reg.ChargeClassID || reg.GuarantorID == AppSession.Parameter.SelfGuarantor)
                            {
                                if ((reg.PlavonAmount2 ?? 0) > 0)
                                    total = (((reg.PlavonAmount2 ?? 0) == 0) ? total : selisih);
                                else
                                    total = (selisih > 0 ? selisih : total);
                            }
                        }
                        else
                        {
                            var cob = new RegistrationGuarantorCollection();
                            //cob.Query.Where(cob.Query.RegistrationNo == txtRegistrationNo.Text);
                            //cob.LoadAll();
                            var cobq = new RegistrationGuarantorQuery("a");
                            var gq = new GuarantorQuery("b");
                            cobq.InnerJoin(gq).On(gq.GuarantorID == cobq.GuarantorID);
                            cobq.Where(cobq.RegistrationNo == txtRegistrationNo.Text);
                            cob.Load(cobq);
                            decimal cobPlafond = cob.Sum(c => (c.PlafondAmount ?? 0));

                            if (reg.PlavonAmount > 0)
                            {
                                total =
                                    (collection.Aggregate(total,
                                                          (current, item) =>
                                                          current + (item.PatientAmount ?? 0) + (item.GuarantorAmount ?? 0) +
                                                          (item.AdministrationAmount ?? 0) +
                                                          (item.GuarantorAdministrationAmount ?? 0) - (item.DiscAdmPatient ?? 0) -
                                                          (item.DiscAdmGuarantor ?? 0))) - (reg.PlavonAmount + cobPlafond);
                            }
                            else
                                total = collection.Aggregate(total,
                                                             (current, item) =>
                                                             current + (item.PatientAmount ?? 0) +
                                                             (item.AdministrationAmount ?? 0) - (item.DiscAdmPatient ?? 0));
                        }
                    }
                    else
                    {
                        var collection = CostCalculations;
                        if (isBridging)
                        {
                            total = (collection.Aggregate(total, (current, item) => current + (item.PatientAmount ?? 0))) +
                                        reg.PatientAdm;

                            if (reg.CoverageClassID != reg.ChargeClassID || reg.GuarantorID == AppSession.Parameter.SelfGuarantor)
                            {
                                if ((reg.PlavonAmount2 ?? 0) > 0)
                                    total = (((reg.PlavonAmount2 ?? 0) == 0) ? total : selisih);
                                else
                                    total = (selisih > 0 ? selisih : total);
                            }
                        }
                        else
                        {
                            if (reg.PlavonAmount > 0)
                            {
                                var cob = new RegistrationGuarantorCollection();
                                //cob.Query.Where(cob.Query.RegistrationNo == txtRegistrationNo.Text);
                                //cob.LoadAll();
                                var cobq = new RegistrationGuarantorQuery("a");
                                var gq = new GuarantorQuery("b");
                                cobq.InnerJoin(gq).On(gq.GuarantorID == cobq.GuarantorID);
                                cobq.Where(cobq.RegistrationNo == txtRegistrationNo.Text);
                                cob.Load(cobq);
                                decimal cobPlafond = cob.Sum(c => (c.PlafondAmount ?? 0));

                                total =
                                    (collection.Aggregate(total,
                                                          (current, item) =>
                                                          current + (item.PatientAmount ?? 0) + (item.GuarantorAmount))) -
                                    reg.PlavonAmount + cobPlafond + reg.AdministrationAmount;

                            }
                            else
                                total = (collection.Aggregate(total, (current, item) => current + (item.PatientAmount ?? 0))) +
                                        reg.PatientAdm;
                        }
                    }
                }
                #endregion

                #region -old-
                //if (AppSession.Parameter.IsUsingIntermBill)
                //{
                //    var collection = IntermBillPatients;

                //    decimal selisih = 0;
                //    bool isBridging = false;

                //    var bridging = new GuarantorBridging();
                //    bridging.Query.Where(bridging.Query.GuarantorID == reg.GuarantorID,
                //                         bridging.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(),
                //                                                          AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(),
                //                                                          AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
                //    if (bridging.Query.Load())
                //    {
                //        isBridging = true;
                //        if (reg.CoverageClassID != reg.ChargeClassID || reg.GuarantorID == AppSession.Parameter.SelfGuarantor)
                //        {
                //            var cov = new RegistrationCoverageDetail();
                //            cov.Query.Select(cov.Query.CalculatedAmount.Sum());
                //            cov.Query.Where(cov.Query.RegistrationNo == reg.RegistrationNo);
                //            if (cov.Query.Load()) selisih = cov.CalculatedAmount ?? 0;
                //            else
                //            {
                //                if ((reg.PlavonAmount2 ?? 0) > 0)
                //                {
                //                    var class1 = new Class();
                //                    class1.LoadByPrimaryKey(reg.CoverageClassID);

                //                    var asri1 = new AppStandardReferenceItem();
                //                    asri1.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class1.SRClassRL);

                //                    var class2 = new Class();
                //                    class2.LoadByPrimaryKey(reg.ChargeClassID);

                //                    var asri2 = new AppStandardReferenceItem();
                //                    asri2.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class2.SRClassRL);

                //                    if (asri2.Note.ToInt() < asri1.Note.ToInt()) selisih = (reg.PlavonAmount2 ?? 0) - (reg.PlavonAmount ?? 0);
                //                }
                //            }
                //        }

                //        total = collection.Aggregate(total,
                //                                         (current, item) =>
                //                                         current + (item.PatientAmount ?? 0) +
                //                                         (item.AdministrationAmount ?? 0) - (item.DiscAdmPatient ?? 0));

                //        if (reg.CoverageClassID != reg.ChargeClassID || reg.GuarantorID == AppSession.Parameter.SelfGuarantor)
                //        {
                //            if ((reg.PlavonAmount2 ?? 0) > 0)
                //                total = (((reg.PlavonAmount2 ?? 0) == 0) ? total : selisih);
                //            else
                //                total = (selisih > 0 ? selisih : total);
                //        }
                //    }
                //    else
                //    {
                //        var cob = new RegistrationGuarantorCollection();
                //        cob.Query.Where(cob.Query.RegistrationNo == txtRegistrationNo.Text);
                //        cob.LoadAll();
                //        decimal cobPlafond = cob.Sum(c => (c.PlafondAmount ?? 0));

                //        if (reg.PlavonAmount > 0)
                //        {
                //            total =
                //                (collection.Aggregate(total,
                //                                      (current, item) =>
                //                                      current + (item.PatientAmount ?? 0) + (item.GuarantorAmount ?? 0) +
                //                                      (item.AdministrationAmount ?? 0) +
                //                                      (item.GuarantorAdministrationAmount ?? 0) - (item.DiscAdmPatient ?? 0) -
                //                                      (item.DiscAdmGuarantor ?? 0))) - (reg.PlavonAmount + cobPlafond);
                //        }
                //        else
                //            total = collection.Aggregate(total,
                //                                         (current, item) =>
                //                                         current + (item.PatientAmount ?? 0) +
                //                                         (item.AdministrationAmount ?? 0) - (item.DiscAdmPatient ?? 0));
                //    }
                //}
                //else
                //{
                //    var collection = CostCalculations;

                //    if (reg.PlavonAmount > 0)
                //    {
                //        var cob = new RegistrationGuarantorCollection();
                //        cob.Query.Where(cob.Query.RegistrationNo == txtRegistrationNo.Text);
                //        cob.LoadAll();
                //        decimal cobPlafond = cob.Sum(c => (c.PlafondAmount ?? 0));

                //        total =
                //            (collection.Aggregate(total,
                //                                  (current, item) =>
                //                                  current + (item.PatientAmount ?? 0) + (item.GuarantorAmount))) -
                //            reg.PlavonAmount + cobPlafond + reg.AdministrationAmount;

                //    }
                //    else
                //        total = (collection.Aggregate(total, (current, item) => current + (item.PatientAmount ?? 0))) +
                //                reg.PatientAdm;
                //}

                #endregion

                txtTotalAmount.Value = Convert.ToDouble(total);

                // Get DownPayment
                string[] regs = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);
                DataTable dtDP = Helper.Payment.GetDownPaymentOutstanding(regs, null, null, reg.SRRegistrationType);
                var sumDP = dtDP.AsEnumerable().Sum(x => x.Field<decimal>("Amount"));

                txtDownPayment.Value = Convert.ToDouble(sumDP);

                var ar = txtTotalAmount.Value - txtDownPayment.Value;
                txtPersonalAR.Value = (ar >= 0) ? ar : 0;
                txtDiscountAmount.Value = 0;

                var guar = new Guarantor();
                if (guar.LoadByPrimaryKey(reg.GuarantorID) && guar.RoundingTransaction > 0)
                {
                    trBillRounding.Visible = true;
                    decimal _total = Convert.ToDecimal(txtPersonalAR.Value);
                    txtPersonalAR.Value = Convert.ToDouble(Helper.BillRounding(_total, guar.RoundingTransaction ?? 0, guar.IsUsingRoundingDown ?? false));
                    txtRoundingAmount.Value = txtPersonalAR.Value - Convert.ToDouble(_total);
                }
                else
                {
                    trBillRounding.Visible = false;
                    txtRoundingAmount.Value = 0;
                }
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

            var collection = IntermBillPatients;
            if (collection.Count == 0)
            {
                ShowInformationHeader("Personal A/R can't be process. There is no Interm Bill available.");
                return false;
            }

            bool isAllowZeroPayment = false;
            
            if (txtTotalAmount.Value == 0)
            {
                var regs = Helper.MergeBilling.GetFullMergeRegistration(txtRegistrationNo.Text);
                //var TotalAll = Helper.CostCalculation.GetBillingTotalAllTransactionInclAdm(regs, true);
                string[] ibn = collection.Select(m => m.IntermBillNo).ToArray();
                var TotalAll = Helper.CostCalculation.GetBillingTotalAllTransactionIntermbillInclAdm(ibn, true);

                if (TotalAll == 0)
                {
                    // closing 0 tetap harus bisa dilakukan untuk kasus ada transaksi tapi didiskon 100%
                    // karena pencatatan di akunting tetap harus dilakukan

                    // jika sudah ada PM 0 rupiah tidak boleh create lagi
                    if (Helper.Payment.IsPaymentZeroExist(regs)) //(Helper.Payment.IsPaymentExist(regs))
                    {
                        ShowInformationHeader("Previous zero payment exist, zero payment can not be done");
                        return false;
                    }
                    else
                    {
                        isAllowZeroPayment = true;
                    }
                }
                //else
                //{
                //    ShowInformationHeader("Total Amount must be greater than 0.");
                //    return false;
                //}
            }
            
            if (txtPersonalAR.Value == 0 && !isAllowZeroPayment)
            {
                ShowInformationHeader("Personal A/R must be greater than 0.");
                return false;
            }

            if (txtDiscountAmount.Value != 0 && txtDiscountAmount.Value > 0 && string.IsNullOrEmpty(cboSRDiscountReason.SelectedValue))
            {
                ShowInformationHeader("Discount Reason required.");
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

            //---TransPayment
            var entity = new TransPayment();
            entity.AddNew();
            entity.TransactionCode = TransactionCode.Payment;
            entity.PaymentNo = GetNewPaymentNo();
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.PaymentDate = txtPaymentDate.SelectedDate;
            entity.PaymentTime = txtPaymentTime.TextWithLiterals;
            entity.PrintReceiptAsName = txtGuarantorName.Text;
            entity.TotalPaymentAmount = (decimal)txtPersonalAR.Value + (decimal)txtDownPayment.Value +
                (decimal)txtDiscountAmount.Value; //(decimal)txtTotalPaymentAmount.Value;
            entity.RemainingAmount = 0;
            entity.PrintNumber = 0;
            entity.PaymentReceiptNo = string.Empty;
            entity.CreatedBy = AppSession.UserLogin.UserID;
            entity.IsVoid = false;
            entity.IsApproved = true;
            entity.Notes = string.Empty;

            entity.GuarantorID = AppSession.Parameter.SelfGuarantor;
            entity.IsToGuarantor = false;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.ApproveByUserID = AppSession.UserLogin.UserID;
            entity.ApproveDate = (new DateTime()).NowAtSqlServer();
            entity.Notes = txtNotes.Text;


            int seqno = 1;
            var collPaymentItem = new TransPaymentItemCollection();

            var dpColl = new TransPaymentCollection();

            if (txtDownPayment.Value > 0)
            {
                var piDP = collPaymentItem.AddNew();
                piDP.PaymentNo = entity.PaymentNo;
                piDP.SequenceNo = seqno.ToString().PadLeft(3, '0');
                piDP.SRPaymentType = AppSession.Parameter.PaymentTypePayment;
                piDP.PaymentTypeName = string.Empty;
                piDP.SRPaymentMethod = string.Empty;
                piDP.PaymentMethodName = string.Empty;
                piDP.Amount = (decimal)txtDownPayment.Value;
                piDP.RoundingAmount = 0;
                piDP.Balance = 0;
                piDP.IsFromDownPayment = true;
                piDP.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                piDP.LastUpdateByUserID = AppSession.UserLogin.UserID;
                seqno++;

                string[] regs = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);
                DataTable dtDP = Helper.Payment.GetDownPaymentOutstanding(regs, null, null, reg.SRRegistrationType);
                string[] noDP = dtDP.AsEnumerable().Select(x => x.Field<string>("PaymentNo")).ToArray();
                dpColl.Query.Where(dpColl.Query.PaymentNo.In(noDP));
                dpColl.LoadAll();
                // update ke downpayment
            }
            var pi = collPaymentItem.AddNew();
            //---TransPaymentItem
            pi.PaymentNo = entity.PaymentNo;
            pi.SequenceNo = seqno.ToString().PadLeft(3, '0');
            pi.SRPaymentType = AppSession.Parameter.PaymentTypePersonalAR;

            var type = new PaymentType();
            type.LoadByPrimaryKey(pi.SRPaymentType);
            pi.PaymentTypeName = type.PaymentTypeName;

            pi.SRPaymentMethod = string.Empty;
            pi.PaymentMethodName = string.Empty;
            pi.Amount = (decimal)txtPersonalAR.Value;//(decimal)txtTotalPaymentAmount.Value;
            pi.RoundingAmount = (decimal)txtRoundingAmount.Value;
            pi.Balance = 0;
            pi.IsFromDownPayment = false;
            pi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            pi.LastUpdateByUserID = AppSession.UserLogin.UserID;
            seqno++;

            if (txtDiscountAmount.Value > 0)
            {
                var pDisc = collPaymentItem.AddNew();
                //---TransPaymentItem
                pDisc.PaymentNo = entity.PaymentNo;
                pDisc.SequenceNo = seqno.ToString().PadLeft(3, '0');
                pDisc.SRPaymentType = AppSession.Parameter.PaymentTypeDiscount;

                var typeDisc = new PaymentType();
                typeDisc.LoadByPrimaryKey(pDisc.SRPaymentType);
                pDisc.PaymentTypeName = typeDisc.PaymentTypeName;

                pDisc.SRPaymentMethod = string.Empty;
                pDisc.PaymentMethodName = string.Empty;
                pDisc.Amount = (decimal)txtDiscountAmount.Value;//(decimal)txtTotalPaymentAmount.Value;
                pDisc.SRDiscountReason = cboSRDiscountReason.SelectedValue;
                pDisc.RoundingAmount = 0;
                pDisc.Balance = 0;
                pDisc.IsFromDownPayment = false;
                pDisc.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                pDisc.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
            var collPaymentIntermBill = new TransPaymentItemIntermBillCollection();
            //var collIntermBill = IntermBillPatients.Where(coll => (coll.PatientAmount + coll.GuarantorAmount) > 0);
            foreach (IntermBill item in IntermBillPatients)
            {
                var pib = collPaymentIntermBill.AddNew();
                pib.PaymentNo = entity.PaymentNo;
                pib.IntermBillNo = item.IntermBillNo;
                pib.IsPaymentProceed = true;
                pib.IsPaymentReturned = false;
                pib.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                pib.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }

            reg.RemainingAmount -= (decimal)txtPersonalAR.Value + (decimal)txtDownPayment.Value +
                (decimal)txtDiscountAmount.Value;//(decimal)txtTotalPaymentAmount.Value;

            #region Close Registration
            if ((reg.SRRegistrationType == AppConstant.RegistrationType.InPatient && AppSession.Parameter.IsAutoClosedRegIpOnPayment) ||
                (reg.SRRegistrationType != AppConstant.RegistrationType.InPatient && AppSession.Parameter.IsAutoClosedRegOpOnPayment))
            {
                string[] regno = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);

                var isClosed = Helper.RegistrationOpenClose.GetStatusClosed(reg, regno,
                    (decimal)txtPersonalAR.Value + (decimal)txtDownPayment.Value +
                    (decimal)txtDiscountAmount.Value/*(decimal)txtTotalPaymentAmount.Value*/, 0);
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
                        hist.Notes = "Verification & Finalize Billing >> Personal A/R";
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
                collPaymentIntermBill.Save();

                #region Auto Blacklist
                if (AppSession.Parameter.IsAutoBlacklistOnPersonalAr)
                {
                    var pat = new Patient();
                    pat.LoadByPrimaryKey(reg.PatientID);
                    pat.IsBlackList = true;
                    pat.Save();

                    var hist = new PatientBlackListHistory();
                    hist.AddNew();
                    hist.PatientID = reg.PatientID;
                    hist.IsBlackList = true;
                    hist.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    hist.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    hist.Notes = "Personal A/R with no. " + entity.PaymentNo;
                    hist.Save();
                }
                #endregion

                if (dpColl.HasData)
                {
                    foreach (var dp in dpColl)
                    {
                        dp.PaymentReferenceNo = entity.PaymentNo;
                    }
                    dpColl.Save();
                }

                _autoNumber.Save();

                //if (AppSession.Parameter.IsPhysicianFeeArCreateOnArReceipt == "Yes")
                //{
                //    int? x = ParamedicFeeTransChargesItemCompSettled.AddNewSettled(entity, collPaymentIntermBill, AppSession.UserLogin.UserID);
                //}

                // update informasi payment jasmed
                var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                feeColl.RecalculateForFeeByPlafonGuarantor(entity, collPaymentItem, AppSession.UserLogin.UserID);
                feeColl.SetPayment(entity, collPaymentItem, 2, AppSession.UserLogin.UserID);
                feeColl.Save();

                trans.Complete();
            }


            int? journalId = JournalTransactions.AddNewPaymentCashBasedJournal(BusinessObject.JournalType.ARCreating,
                                                                                entity, reg, collPaymentItem,
                                                                                "TP", entity.PaymentNo, AppSession.UserLogin.UserID, 0);


            // checkout otomatis,
            Helper.RegistrationOpenClose.SetDischargeDate(reg);

            return true;
        }

        private IntermBillCollection IntermBillPatients
        {
            get
            {
                var obj = ViewState["VerificationBilling:IntermBillPatients" + Request.UserHostName];
                if (obj != null)
                    return ((IntermBillCollection)(obj));

                var registrationNoList = MergeRegistrationList();

                var collection = new IntermBillCollection();

                var query = new IntermBillQuery("a");
                var payib = new TransPaymentItemIntermBillQuery("b");
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

                ViewState["VerificationBilling:IntermBillPatients" + Request.UserHostName] = collection;

                return collection;
            }
            set { ViewState["VerificationBilling:IntermBillPatients" + Request.UserHostName] = value; }
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
            return "oWnd.argument = 'paymentPersonalAr'";
        }

        protected void txtPersonalAR_TextChanged(object sender, EventArgs e)
        {
            var discVal = txtTotalAmount.Value - txtDownPayment.Value - txtPersonalAR.Value;
            txtDiscountAmount.Value = (discVal >= 0) ? discVal : 0;
        }
    }
}

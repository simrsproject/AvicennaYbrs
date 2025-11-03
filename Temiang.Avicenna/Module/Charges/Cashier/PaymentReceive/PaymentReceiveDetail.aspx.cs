using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Text;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class PaymentReceiveDetail : BasePageDetail
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
            UrlPageSearch = "PaymentReceiveSearch.aspx?fp=dt";

            if (AppSession.Parameter.IsNeedVoidReasonOnPaymentReceive)
            {
                IsUsingBeforeUnapprovalValidation = true;
            }

            if (string.IsNullOrEmpty(Request.QueryString["from"]))
            {
                switch (Request.QueryString["pc"])
                {
                    case "no":
                        ProgramID = string.IsNullOrEmpty(Request.QueryString["utype"]) ? AppConstant.Program.PaymentReceive : AppConstant.Program.PaymentReceiveCashier;
                        UrlPageList = string.IsNullOrEmpty(Request.QueryString["utype"]) ? "PaymentReceiveRegistrationList.aspx?pc=no" : "PaymentReceiveCashierList.aspx?pc=no";
                        break;
                    case "yes":
                        ProgramID = AppConstant.Program.PaymentReceiveLinkToPettyCash;
                        UrlPageList = "PaymentReceiveRegistrationList.aspx?pc=yes";
                        break;
                    case "":
                        ProgramID = AppConstant.Program.PaymentReceivePatientActive;
                        UrlPageList = "PaymentReceivePatientActiveList.aspx?pc=";
                        break;
                }
            }
            else
            {
                ProgramID = AppConstant.Program.PaymentReceive;
                UrlPageList = "../../Billing/FinalizeBilling/FinalizeBillingVerification.aspx?regNo=" + Request.QueryString["regno"] + "&regType=" + Request.QueryString["rtype"] + "&md=new&from=1";
            }

            //StandardReference Initialize
            if (!IsPostBack)
            {
                (Helper.FindControlRecursive(Page, "fw_tbarData") as RadToolBar).Items[10].Enabled = false;

                TransPaymentItems = null;
                Session["PaymentReceive:collTransPaymentReference" + Request.QueryString["regno"]] = null;

                trPromotion.Visible = (AppSession.Parameter.IsUsingPromotion);
                StandardReference.InitializeIncludeSpace(cboPromotion, AppEnum.StandardReference.Promotion);

                if (!AppSession.Parameter.IsUsingIntermBill)
                {
                    tabStrip.Tabs[2].Visible = false;
                    tabStrip.Tabs[3].Visible = false;
                    btnIntermBill.Visible = false;
                    btnIntermBillGuarantor.Visible = false;
                }

                trInitial.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH";

                //if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSSA")
                //{
                //var cstext1 = new StringBuilder();
                //cstext1.Append("<script type=text/javascript> __doPostBack('ctl00$ContentPlaceHolder1$grdTransPaymentItem$ctl00$ctl02$ctl00$AddNewRecordButton','') </script>");

                //Page.ClientScript.RegisterStartupScript(GetType(), "OpenAddNewRecordGrid", cstext1.ToString());
                //}

                txtPaymentDate.DateInput.ReadOnly = !AppSession.Parameter.IsPaymentReceiveAllowBackdated;
                txtPaymentDate.DatePopupButton.Enabled = !txtPaymentDate.DateInput.ReadOnly;
                txtPaymentTime.ReadOnly = txtPaymentDate.DateInput.ReadOnly;
            }

            _isPowerUser = this.IsPowerUser || AppSession.Parameter.IsBypassCashierAuthorization;

            WindowSearch.Height = 300;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdTransPaymentItem, txtTotalPaymentAmountPatient);
            ajax.AddAjaxSetting(grdTransPaymentItem, txtRemainingAmountPatient);
            ajax.AddAjaxSetting(grdTransPaymentItem, txtTotalPaymentAmountGuarantor);
            ajax.AddAjaxSetting(grdTransPaymentItem, txtRemainingAmountGuarantor);
            ajax.AddAjaxSetting(grdTransPaymentItem, txtDownPaymentAmount);
            ajax.AddAjaxSetting(grdTransPaymentItem, txtDiscountAmount);
            ajax.AddAjaxSetting(grdTransPaymentItem, txtRoundingAmount);
            ajax.AddAjaxSetting(rblToGuarantor, btnIntermBill);
            ajax.AddAjaxSetting(rblToGuarantor, btnIntermBillGuarantor);

            ajax.AddAjaxSetting(grdTransPaymentItem, grdTransPaymentItem);
            ajax.AddAjaxSetting(rblToGuarantor, rblToGuarantor);
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
                case AppConstant.Report.PaymentReturnReceipt:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;
                case AppConstant.Report.RSSA_PaymentRRtInPatientP:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                    printJobParameters.AddNew("RegistrationNo", txtRegistrationNo.Text);
                    printJobParameters.AddNew("GuarantorAskesID", string.Empty);

                    break;
                case AppConstant.Report.RSSA_PaymentRRtInPatientG:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                    printJobParameters.AddNew("RegistrationNo", txtRegistrationNo.Text);
                    printJobParameters.AddNew("GuarantorAskesID", string.Empty);//AppSession.Parameter.GuarantorAskesID);

                    break;
                case AppConstant.Report.PaymentReceiptDetailOutPatient:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;
                case AppConstant.Report.PaymentReceiptDetailOutPatient2:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                    printJobParameters.AddNew("PaymentTypeDiscount", AppSession.Parameter.PaymentTypeDiscount);

                    break;
                case AppConstant.Report.PaymentReceiptPrescDetail:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;
                case AppConstant.Report.PaymentReceiveReceiptNoDP:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;
                case AppConstant.Report.PaymentReceiptDetail:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;
                case AppConstant.Report.RSSA_Slip_Mandiri:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;
                case AppConstant.Report.RSSA_Slip_Kalbar:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;
                case AppConstant.Report.RSSA_PaymentReceiveReceipt:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                    break;

                case AppConstant.Report.PaymentReceiptGlobal:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                    printJobParameters.AddNew("DownPayment", null,
                                              Helper.Payment.GetTotalDownPaymentOnly(
                                                  Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text)),
                                              null);
                    printJobParameters.AddNew("ParamedicTariffComponentID",
                                              AppSession.Parameter.ParamedicTariffComponentID);
                    printJobParameters.AddNew("RegistrationNo", txtRegistrationNo.Text);
                    var jobParameter = printJobParameters.AddNew();
                    jobParameter.Name = "RegistrationNoList";
                    jobParameter.ValueString = string.Empty;

                    var merge = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);
                    foreach (var str in merge)
                    {
                        jobParameter.ValueString += str + ",";
                    }
                    jobParameter.ValueString = jobParameter.ValueString.Substring(0, jobParameter.ValueString.Length - 1);

                    break;
                case AppConstant.Report.SpectaclePrescriptionSlip:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);

                    break;
                case AppConstant.Report.StatementOfDebt:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;
                case AppConstant.Report.PaymentReceiptSlip2:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

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
                if (entity.CreatedBy != AppSession.UserLogin.UserID && !_isPowerUser && string.IsNullOrEmpty(Request.QueryString["utype"]))
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

            hdnPatientId.Value = reg.PatientID;
            txtMedicalNo.Text = patient.MedicalNo;
            var std = new AppStandardReferenceItem();
            txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
            txtPatientName.Text = patient.PatientName;
            txtPrintReceiptAsName.Text = patient.PatientName;

            txtGuarantorID.Text = reg.GuarantorID;
            var guarantor = new Guarantor();
            guarantor.LoadByPrimaryKey(txtGuarantorID.Text);
            lblGuarantorName.Text = guarantor.GuarantorName;

            txtMemberID.Text = patient.str.MemberID;
            var member = new Guarantor();
            if (member.LoadByPrimaryKey(txtMemberID.Text))
                lblMemberName.Text = member.GuarantorName;

            //var gQ = new GuarantorQuery();
            //gQ.Select(gQ.GuarantorID, gQ.GuarantorName);
            //gQ.Where(gQ.GuarantorID == reg.GuarantorID);
            //var dtbG = gQ.LoadDataTable();
            //cboGuarantorID.DataSource = dtbG;
            //cboGuarantorID.DataBind();
            //cboGuarantorID.SelectedValue = reg.GuarantorID;
            cboGuarantorID.Items.Add(new RadComboBoxItem(reg.GuarantorID + " - " + guarantor.GuarantorName, reg.GuarantorID));
            cboGuarantorID.SelectedValue = reg.GuarantorID;

            rblToGuarantor.SelectedIndex = 1;

            if (AppSession.Parameter.IsUsingIntermBill)
            {
                btnIntermBill.Visible = rblToGuarantor.SelectedIndex == 1;
                btnIntermBillGuarantor.Visible = rblToGuarantor.SelectedIndex == 0;
            }

            string[] patientParam = new string[2];
            var regno = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);
            patientParam.SetValue(AppSession.Parameter.PaymentTypePayment, 0);
            patientParam.SetValue(AppSession.Parameter.PaymentTypePersonalAR, 1);

            var discPatient = (double)Helper.Payment.GetPaymentDiscount(regno, false);
            var discGuarantor = (double)Helper.Payment.GetPaymentDiscount(regno, true);

            decimal tpayment = Helper.Payment.GetTotalPayment(regno, true, patientParam);
            decimal treturn = Helper.Payment.GetTotalPayment(regno, false);
            txtTotalPaymentAmountPatient.Value = (double)tpayment + (double)treturn;
            txtTotalPaymentAmountGuarantor.Value = (double)Helper.Payment.GetTotalPayment(regno, true, AppSession.Parameter.PaymentTypeCorporateAR) + (double)Helper.Payment.GetTotalPayment(regno, true, AppSession.Parameter.PaymentTypeSaldoAR);


            var cob = new RegistrationGuarantorCollection();
            var cobq = new RegistrationGuarantorQuery("a");
            var gq = new GuarantorQuery("b");
            cobq.InnerJoin(gq).On(gq.GuarantorID == cobq.GuarantorID);
            cobq.Where(cobq.RegistrationNo == txtRegistrationNo.Text);
            cob.Load(cobq);
            decimal cobPlafond = cob.Sum(c => (c.PlafondAmount ?? 0));

            decimal tpatient, tguarantor;
            Helper.CostCalculation.GetBillingTotal(regno, reg.SRBussinesMethod, (reg.PlavonAmount ?? 0) + cobPlafond, out tpatient, out tguarantor, guarantor, reg.IsGlobalPlafond ?? false);

            txtRemainingAmountGuarantor.Value = (double)tguarantor - txtTotalPaymentAmountGuarantor.Value - discGuarantor;

            double ExcessGuar = 0;
            if (txtRemainingAmountGuarantor.Value < 0)
            {
                ExcessGuar = txtRemainingAmountGuarantor.Value ?? 0;
                txtRemainingAmountGuarantor.Value = 0;
            }

            txtPlafonAmount.Value = (double)((reg.PlavonAmount ?? 0) + cobPlafond);
            txtAdminCal.Value = (double)(reg.AdministrationAmount ?? 0);
            txtPatientAdm.Value = (double)(reg.PatientAdm ?? 0);
            txtGuarantorAdm.Value = (double)(reg.GuarantorAdm ?? 0);
            txtDownPaymentAmount.Value = (double)(Helper.Payment.GetTotalDownPayment(regno) - Helper.Payment.GetTotalDownPaymentReturn(regno));
            btnDownPayment.Enabled = (txtDownPaymentAmount.Value > 0 && rblToGuarantor.SelectedIndex == 1);
            txtDiscountAmount.Value = (double)Helper.Payment.GetTotalPaymentDiscount(regno);
            txtPaymentDiscountPatient.Value = discPatient;
            txtPaymentDiscountGuarantor.Value = discGuarantor;
            txtRemainingAmountPatient.Value = (double)tpatient - txtTotalPaymentAmountPatient.Value - discPatient;

            //selisih kelas untuk bpjs
            //decimal selisih = 0;
            //bool isBridging = false;
            //if (AppSession.Parameter.IsBridgingBillingBpjs)
            //{
            //    if (Helper.IsInacbgIntegration)
            //    {
            //        var ncc = new NccInacbg();
            //        ncc.Query.es.Top = 1;
            //        ncc.Query.Where(ncc.Query.RegistrationNo.In(regno));
            //        if (ncc.Query.Load())
            //        {
            //            selisih = ncc.AddPaymentAmt ?? 0;
            //            isBridging = true;
            //        }
            //    }

            //    if (!isBridging || selisih == 0)
            //    {
            //        var bridging = new GuarantorBridging();
            //        bridging.Query.Where(bridging.Query.GuarantorID == reg.GuarantorID,
            //                             bridging.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(), AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(), AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
            //        if (bridging.Query.Load())
            //        {
            //            isBridging = true;
            //            if (reg.CoverageClassID != reg.ChargeClassID || reg.GuarantorID == AppSession.Parameter.SelfGuarantor)
            //            {
            //                var cov = new RegistrationCoverageDetail();
            //                cov.Query.Select(cov.Query.CalculatedAmount.Sum());
            //                cov.Query.Where(cov.Query.RegistrationNo == reg.RegistrationNo);
            //                if (cov.Query.Load())
            //                {
            //                    selisih = cov.CalculatedAmount ?? 0;
            //                    if (AppSession.Parameter.IsBridgingBillingBpjsWithCostSharing)
            //                    {
            //                        //1. cek selisih plafond (75% dari kelas 1)
            //                        //2. cek total tagihan - plafond
            //                        //3. ambil nilai paling kecil

            //                        var class1 = new Class();
            //                        class1.LoadByPrimaryKey(reg.CoverageClassID);

            //                        var asri1 = new AppStandardReferenceItem();
            //                        asri1.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class1.SRClassRL);

            //                        if (asri1.Note == "2") // Kelas 1
            //                        {
            //                            var class2 = new Class();
            //                            class2.LoadByPrimaryKey(reg.ChargeClassID);

            //                            var asri2 = new AppStandardReferenceItem();
            //                            asri2.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class2.SRClassRL);

            //                            if (new[] { "0", "1" }.Contains(asri2.Note)) // Kelas VIP, VVIP dll, diatas kelas 1 yg dihitung 75% coverage untuk selisih
            //                            {
            //                                var plafon = (decimal)txtPlafonAmount.Value;

            //                                var totalTx = Helper.CostCalculation.GetBillingTotalAllTransactionInclAdm(regno, true);

            //                                if (selisih > (totalTx - plafon))
            //                                    selisih = (totalTx - plafon);
            //                            }
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    if ((reg.PlavonAmount2 ?? 0) > 0)
            //                    {
            //                        var class1 = new Class();
            //                        class1.LoadByPrimaryKey(reg.CoverageClassID);

            //                        var asri1 = new AppStandardReferenceItem();
            //                        asri1.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class1.SRClassRL);

            //                        var class2 = new Class();
            //                        class2.LoadByPrimaryKey(reg.ChargeClassID);

            //                        var asri2 = new AppStandardReferenceItem();
            //                        asri2.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class2.SRClassRL);

            //                        if (asri2.Note.ToInt() < asri1.Note.ToInt()) selisih = (reg.PlavonAmount2 ?? 0) - (reg.PlavonAmount ?? 0);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            bool isBridging = false;
            decimal? totalTx = 0;
            totalTx = TransPaymentItemIntermBills.Where(item => item.PaymentNo != string.Empty).Aggregate(totalTx, (current, item) => current + (item.PatientAmount + item.GuarantorAmount));
            decimal selisih = GetSelisihPasienBPJS(reg, ref isBridging, (totalTx ?? 0));

            if (selisih > 0)
                txtRemainingAmountPatient.Value = (double)selisih - txtTotalPaymentAmountPatient.Value - discPatient;

            if (ExcessGuar < 0)
            {
                txtRemainingAmountPatient.Value += ExcessGuar;
            }

            txtRoundingAmount.Value = 0D;

            txtTransPatientAmount.Value = 0;
            txtTransGuarantorAmount.Value = 0;

            if (AppSession.Parameter.IsPaymentShowTransactionListForAllRegType)
            {
                var ccColl = new CostCalculationCollection();
                ccColl.GetCostCalculationsByRegWithMergeBilling(txtRegistrationNo.Text, true);
                btnOrderItem.Enabled = ccColl.Count > 0;

                if (!btnOrderItem.Enabled)
                {
                    var registrationNoList = MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);

                    var dtb = Cashier.DetailItemList.TransactionChargesItemOrders(registrationNoList);
                    btnOrderItem.Enabled = (dtb.Rows.Count > 0);
                    if (!btnOrderItem.Enabled)
                    {
                        dtb = Cashier.DetailItemList.TransactionChargesItemPackages(registrationNoList);
                        btnOrderItem.Enabled = (dtb.Rows.Count > 0);
                    }

                }
            }
            else
            {
                if (reg.SRRegistrationType != AppConstant.RegistrationType.InPatient && !isBridging) btnOrderItem.Enabled = rblToGuarantor.SelectedIndex == 1;
                else btnOrderItem.Enabled = false;
            }

            lblRegistrationInfo2.Text = RegistrationInfoSumary.GetDocumentCheckListCountRemains(reg.RegistrationNo);

            ViewState["IsVoid"] = false;
            ViewState["IsApproved"] = false;

            PopulateTransPaymentItemOrderGrid();
            PopulateTransPaymentItemIntermBillGrid();
            PopulateTransPaymentItemIntermBillGuarantorGrid();
            pnlInfo.Visible = GetStatusOutstandingTransaction();
            //if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH" && reg.SRRegistrationType == AppConstant.RegistrationType.InPatient) pnlInfo2.Visible = GetStatusOutstandingIntermBill();
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

                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
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

            if (AppSession.Parameter.IsUsingPromotion)
            {
                if (cboPromotion.SelectedIndex < 0)
                {
                    args.MessageText = "Promotion has not been chosen.";
                    args.IsCancel = true;
                    return;
                }
            }

            if (TransPaymentItems.Count == 0)
            {
                args.MessageText = "Detail payment is required.";
                args.IsCancel = true;
                return;
            }

            //if (txtTransPatientAmount.Value > 0)
            //{
            //    decimal? totpayment = TransPaymentItems.Where(item => item.SRPaymentType != AppSession.Parameter.PaymentTypeCorporateAR && item.SRPaymentType != AppSession.Parameter.PaymentTypeSaldoAR).Aggregate<TransPaymentItem, decimal?>(0, (current, item) => current + item.Amount);

            //    if (totpayment < Convert.ToDecimal(txtTransPatientAmount.Value))
            //    {
            //        args.MessageText = "Total payment amount can't be less than total transaction.";
            //        args.IsCancel = true;
            //        return;
            //    }
            //}

            //double totPayment =
            //    TransPaymentItems.Sum(item => Convert.ToDouble(item.Amount) - Convert.ToDouble(item.RoundingAmount)) +
            //    0.1;

            //if (rblToGuarantor.SelectedIndex == 1)
            //{
            //    if (totPayment < txtTransPatientAmount.Value)
            //    {
            //        args.MessageText = "Total payment amount can't be less than total transaction.";
            //        args.IsCancel = true;
            //        return;
            //    }
            //}
            //else
            //{
            //    if (totPayment < txtTransGuarantorAmount.Value)
            //    {
            //        args.MessageText = "Total payment amount can't be less then total transaction.";
            //        args.IsCancel = true;
            //        return;
            //    }
            //}

            decimal totPayment =
                TransPaymentItems.Sum(item => Convert.ToDecimal(item.Amount) - Convert.ToDecimal(item.RoundingAmount));

            double diff = 0.00;
            double excess = Math.Abs(AppSession.Parameter.ExcessPaymentAmount);

            if (rblToGuarantor.SelectedIndex == 1)
                diff = Convert.ToDouble(totPayment) - Convert.ToDouble(txtTransPatientAmount.Value);
            else
                diff = Convert.ToDouble(totPayment) - Convert.ToDouble(txtTransGuarantorAmount.Value);

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

            var ar = TransPaymentItems.Any(item => item.SRPaymentType == AppSession.Parameter.PaymentTypeCorporateAR || item.SRPaymentType == AppSession.Parameter.PaymentTypeSaldoAR);

            if (txtDownPaymentAmount.Value > 0 && !ar)
            {
                var hasDownPayment = TransPaymentItems.Any(item => item.IsFromDownPayment ?? false);

                if (!hasDownPayment)
                {
                    string[] regno = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);
                    var r = new Registration();
                    r.LoadByPrimaryKey(txtRegistrationNo.Text);

                    decimal totalDp = 0;
                    DataTable dtb = Helper.Payment.GetDownPaymentOutstanding(regno, TransPaymentItemOrders, TransPaymentItemIntermBills, r.SRRegistrationType);
                    foreach (DataRow row in dtb.Rows)
                    {
                        totalDp += Convert.ToDecimal(row["Amount"]);
                    }

                    if (totalDp > 0)
                    {
                        args.MessageText = "The patient has outstanding down payment.";
                        args.IsCancel = true;
                        return;
                    }
                }
            }

            if (AppSession.Parameter.IsUsingIntermBill)
            {
                if (rblToGuarantor.SelectedIndex == 1)
                {
                    if (TransPaymentItemOrders.Count == 0 && TransPaymentItemIntermBills.Count == 0)
                    {
                        args.MessageText = "Detail item transaction or Interim Bill is required.";
                        args.IsCancel = true;
                        return;
                    }

                    if (TransPaymentItemOrders.Count > 0 && TransPaymentItemIntermBills.Count > 0)
                    {
                        args.MessageText = "Item transaction and Interim Bill both have details. Please select one and clear the other.";
                        args.IsCancel = true;
                        return;
                    }

                    foreach (var item in TransPaymentItemOrders)
                    {
                        var dt = new TransPaymentItemOrderQuery("a");
                        var hd = new TransPaymentQuery("b");
                        dt.InnerJoin(hd).On(hd.PaymentNo == dt.PaymentNo);
                        dt.Where(dt.TransactionNo == item.TransactionNo, dt.SequenceNo == item.SequenceNo, dt.IsPaymentReturned == false, hd.IsVoid == false);
                        dt.Select(dt.PaymentNo);
                        DataTable dtb = dt.LoadDataTable();
                        if (dtb.Rows.Count > 0)
                        {
                            args.MessageText = "Item: " + item.ItemName + " has been proceed. Please recheck your previous payment transaction for this patient.";
                            args.IsCancel = true;
                            return;
                        }
                    }

                    foreach (var item in TransPaymentItemIntermBills)
                    {
                        var ib = new IntermBill();
                        if (ib.LoadByPrimaryKey(item.IntermBillNo) && ib.IsVoid == true)
                        {
                            args.MessageText = "Interim Bill Patient: " + item.IntermBillNo + " has been void.";
                            args.IsCancel = true;
                            return;
                        }

                        var cc = new CostCalculationCollection();
                        cc.Query.Where(cc.Query.IntermBillNo == item.IntermBillNo);
                        cc.LoadAll();
                        if (cc.Count == 0)
                        {
                            args.MessageText = "Interim Bill Patient: " + item.IntermBillNo + " has no detail item.";
                            args.IsCancel = true;
                            return;
                        }

                        var dt = new TransPaymentItemIntermBillQuery("a");
                        var hd = new TransPaymentQuery("b");
                        dt.InnerJoin(hd).On(hd.PaymentNo == dt.PaymentNo);
                        dt.Where(dt.IntermBillNo == item.IntermBillNo, dt.IsPaymentReturned == false, hd.IsVoid == false);
                        dt.Select(dt.PaymentNo);
                        DataTable dtb = dt.LoadDataTable();
                        if (dtb.Rows.Count > 0)
                        {
                            args.MessageText = "Interim Bill Patient: " + item.IntermBillNo + " has been proceed with Payment No. " + dtb.Rows[0]["PaymentNo"].ToString();
                            args.IsCancel = true;
                            return;
                        }
                    }
                }
                else
                {
                    if (TransPaymentItemIntermBillGuarantors.Count == 0)
                    {
                        args.MessageText = "Detail item Interim Bill guarantor is required.";
                        args.IsCancel = true;
                        return;
                    }

                    foreach (var item in TransPaymentItemIntermBillGuarantors)
                    {
                        var ib = new IntermBill();
                        if (ib.LoadByPrimaryKey(item.IntermBillNo) && ib.IsVoid == true)
                        {
                            args.MessageText = "Interim Bill Guarantor: " + item.IntermBillNo + " has been void.";
                            args.IsCancel = true;
                            return;
                        }

                        var cc = new CostCalculationCollection();
                        cc.Query.Where(cc.Query.IntermBillNo == item.IntermBillNo);
                        cc.LoadAll();
                        if (cc.Count == 0)
                        {
                            args.MessageText = "Interim Bill Guarantor: " + item.IntermBillNo + " has no detail item.";
                            args.IsCancel = true;
                            return;
                        }

                        var dt = new TransPaymentItemIntermBillGuarantorQuery("a");
                        var hd = new TransPaymentQuery("b");
                        dt.InnerJoin(hd).On(hd.PaymentNo == dt.PaymentNo);
                        dt.Where(dt.IntermBillNo == item.IntermBillNo, dt.IsPaymentReturned == false, hd.IsVoid == false);
                        dt.Select(dt.PaymentNo);
                        DataTable dtb = dt.LoadDataTable();
                        if (dtb.Rows.Count > 0)
                        {
                            args.MessageText = "Interim Bill Patient: " + item.IntermBillNo + " has been proceed with Payment No. " + dtb.Rows[0]["PaymentNo"].ToString();
                            args.IsCancel = true;
                            return;
                        }
                    }
                }
            }
            else
            {
                var isToGuarantor = false;
                foreach (var item in TransPaymentItems.Where(item => item.SRPaymentType == AppSession.Parameter.PaymentTypeCorporateAR || item.SRPaymentType == AppSession.Parameter.PaymentTypePersonalAR))
                {
                    isToGuarantor = true;
                }
                rblToGuarantor.SelectedIndex = isToGuarantor == false ? 1 : 0;
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
                args.MessageText = "Detail payment is required.";
                args.IsCancel = true;
                return;
            }

            //double totPayment =
            //    TransPaymentItems.Sum(item => Convert.ToDouble(item.Amount) - Convert.ToDouble(item.RoundingAmount)) +
            //    0.1;
            //if (rblToGuarantor.SelectedIndex == 1)
            //{
            //    if (totPayment < txtTransPatientAmount.Value)
            //    {
            //        args.MessageText = "Total payment amount can't be less then total transaction.";
            //        args.IsCancel = true;
            //        return;
            //    }
            //}
            //else
            //{
            //    if (totPayment < txtTransGuarantorAmount.Value)
            //    {
            //        args.MessageText = "Total payment amount can't be less then total transaction.";
            //        args.IsCancel = true;
            //        return;
            //    }
            //}

            decimal totPayment =
                TransPaymentItems.Sum(item => Convert.ToDecimal(item.Amount) - Convert.ToDecimal(item.RoundingAmount));

            double diff = 0.00;
            double excess = Math.Abs(AppSession.Parameter.ExcessPaymentAmount);

            if (rblToGuarantor.SelectedIndex == 1)
                diff = Convert.ToDouble(totPayment) - Convert.ToDouble(txtTransPatientAmount.Value);
            else
                diff = Convert.ToDouble(totPayment) - Convert.ToDouble(txtTransGuarantorAmount.Value);

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

            if (AppSession.Parameter.IsUsingIntermBill)
            {
                if (rblToGuarantor.SelectedIndex == 1)
                {
                    if (TransPaymentItemOrders.Count == 0 && TransPaymentItemIntermBills.Count == 0)
                    {
                        args.MessageText = "Detail item transaction or Interim Bill is required.";
                        args.IsCancel = true;
                        return;
                    }

                    if (TransPaymentItemOrders.Count > 0 && TransPaymentItemIntermBills.Count > 0)
                    {
                        args.MessageText = "Item transaction and Interim Bill both have details. Please select one and clear the other.";
                        args.IsCancel = true;
                        return;
                    }

                    foreach (var item in TransPaymentItemIntermBills)
                    {
                        var ib = new IntermBill();
                        if (ib.LoadByPrimaryKey(item.IntermBillNo) && ib.IsVoid == true)
                        {
                            args.MessageText = "Interim Bill Patient: " + item.IntermBillNo + " has been void.";
                            args.IsCancel = true;
                            return;
                        }

                        var cc = new CostCalculationCollection();
                        cc.Query.Where(cc.Query.IntermBillNo == item.IntermBillNo);
                        cc.LoadAll();
                        if (cc.Count == 0)
                        {
                            args.MessageText = "Interim Bill Patient: " + item.IntermBillNo + " has no detail item.";
                            args.IsCancel = true;
                            return;
                        }
                    }
                }
                else
                {
                    if (TransPaymentItemIntermBillGuarantors.Count == 0)
                    {
                        args.MessageText = "Detail item Interim Bill guarantor is required.";
                        args.IsCancel = true;
                        return;
                    }

                    foreach (var item in TransPaymentItemIntermBillGuarantors)
                    {
                        var ib = new IntermBill();
                        if (ib.LoadByPrimaryKey(item.IntermBillNo) && ib.IsVoid == true)
                        {
                            args.MessageText = "Interim Bill Guarantor: " + item.IntermBillNo + " has been void.";
                            args.IsCancel = true;
                            return;
                        }

                        var cc = new CostCalculationCollection();
                        cc.Query.Where(cc.Query.IntermBillNo == item.IntermBillNo);
                        cc.LoadAll();
                        if (cc.Count == 0)
                        {
                            args.MessageText = "Interim Bill Guarantor: " + item.IntermBillNo + " has no detail item.";
                            args.IsCancel = true;
                            return;
                        }
                    }
                }
            }
            else
            {
                var isToGuarantor = false;
                foreach (var item in TransPaymentItems.Where(item => item.SRPaymentType == AppSession.Parameter.PaymentTypeCorporateAR || item.SRPaymentType == AppSession.Parameter.PaymentTypePersonalAR))
                {
                    isToGuarantor = true;
                }
                rblToGuarantor.SelectedIndex = isToGuarantor == false ? 1 : 0;
            }

            var entity = new TransPayment();
            if (entity.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                if (entity.IsVoid == true)
                    args.MessageText = AppConstant.Message.RecordHasVoided;
                else
                {
                    SetEntityValue(entity);
                    SaveEntity(entity);
                }
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
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

            var tpiColl = new TransPaymentItemCollection();
            tpiColl.Query.Where(tpiColl.Query.PaymentNo == txtPaymentNo.Text);
            tpiColl.LoadAll();
            if (tpiColl.Count == 0)
            {
                args.MessageText = "Detail payment is required.";
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

            if (entity.CreatedBy != AppSession.UserLogin.UserID && !_isPowerUser && string.IsNullOrEmpty(Request.QueryString["utype"]))
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

            //double totPayment =
            //    TransPaymentItems.Sum(item => Convert.ToDouble(item.Amount) - Convert.ToDouble(item.RoundingAmount)) +
            //    0.1;

            //if (rblToGuarantor.SelectedIndex == 1)
            //{
            //    if (totPayment < txtTransPatientAmount.Value)
            //    {
            //        args.MessageText = "Total payment amount can't be less than total transaction.";
            //        args.IsCancel = true;
            //        return;
            //    }
            //}
            //else
            //{
            //    if (totPayment < txtTransGuarantorAmount.Value)
            //    {
            //        args.MessageText = "Total payment amount can't be less then total transaction.";
            //        args.IsCancel = true;
            //        return;
            //    }
            //}

            decimal totPayment =
                 TransPaymentItems.Sum(item => Convert.ToDecimal(item.Amount) - Convert.ToDecimal(item.RoundingAmount));

            double diff = 0.00;
            double excess = Math.Abs(AppSession.Parameter.ExcessPaymentAmount);

            if (rblToGuarantor.SelectedIndex == 1)
                diff = Convert.ToDouble(totPayment) - Convert.ToDouble(txtTransPatientAmount.Value);
            else
                diff = Convert.ToDouble(totPayment) - Convert.ToDouble(txtTransGuarantorAmount.Value);

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

            // SetApproval(entity, true);
            Helper.Payment.SetApproval(entity, TransPaymentItems, TransPaymentItemOrders, TransPaymentItemIntermBills,
                TransPaymentItemIntermBillGuarantors, true, txtRemainingAmountPatient.Value ?? 0, txtRemainingAmountGuarantor.Value ?? 0, "Payment Received");
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
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }
            var ret = new TransPaymentCollection();
            ret.Query.Where(ret.Query.PaymentReferenceNo == txtPaymentNo.Text, ret.Query.IsVoid == false,
                            ret.Query.TransactionCode == TransactionCode.PaymentReturn);
            ret.LoadAll();
            if (ret.Count > 0)
            {
                args.MessageText = "Payment is already returned. Un-Approved is not allowed.";
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

            var item = new InvoicesItemQuery("a");
            var inv = new InvoicesQuery("b");

            item.InnerJoin(inv).On(item.InvoiceNo == inv.InvoiceNo);
            item.Where
                (
                    item.PaymentNo == txtPaymentNo.Text,
                    //inv.IsApproved == true
                    inv.IsVoid == false
                );

            var invoice = new InvoicesItemCollection();
            invoice.Load(item);
            if (invoice.Count > 0)
            {
                args.MessageText = "Data is already proceed to invoice. Un-Approved is not allowed.";
                args.IsCancel = true;
                return;
            }

            // cek sudah masuk jasmed atau belum
            var msg = ParamedicFeeTransChargesItemCompByDischargeDate.IsParamedicFeeVerified(txtPaymentNo.Text, true);
            if (!string.IsNullOrEmpty(msg))
            {
                args.MessageText = msg;
                args.IsCancel = true;
                return;

                //ShowInformationHeader(msg);
                //args.IsCancel = true;
                //return;
            }

            if (entity.CreatedBy != AppSession.UserLogin.UserID && !_isPowerUser)
            {
                var usr = new AppUser();
                usr.LoadByPrimaryKey(entity.CreatedBy);
                args.MessageText =
                    "You don't have authorization to Un-Approved this transaction. This data belong to: " +
                    usr.UserName + ". Please contact your supervision.";
                args.IsCancel = true;
                return;
            }
            if ((new DateTime()).NowAtSqlServer() > entity.LastUpdateDateTime.Value.AddHours(AppSession.Parameter.TimeLimitForVoidPayment) && !_isPowerUser)
            {
                args.MessageText =
                    "You don't have authorization to Un-Approved this transaction. Time limit already exceeded. Please contact your supervision.";
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

            SetUnApproval(entity, reason);
        }

        //private void SetApproval(TransPayment entity, bool isApprove)
        //{
        //    entity.IsApproved = isApprove;
        //    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //    entity.ApproveByUserID = AppSession.UserLogin.UserID;
        //    entity.ApproveDate = (new DateTime()).NowAtSqlServer();

        //    //registration
        //    var reg = new Registration();
        //    reg.LoadByPrimaryKey(entity.RegistrationNo);

        //    var total = TransPaymentItems.Sum(detail => (decimal)detail.Amount);

        //    if (isApprove)
        //        reg.RemainingAmount -= total;
        //    else
        //        reg.RemainingAmount += total;

        //    if ((reg.SRRegistrationType == AppConstant.RegistrationType.InPatient && AppSession.Parameter.IsAutoClosedRegIpOnPayment) ||
        //        (reg.SRRegistrationType != AppConstant.RegistrationType.InPatient && AppSession.Parameter.IsAutoClosedRegOpOnPayment))
        //    {
        //        if (txtRemainingAmountPatient.Value == 0 & txtRemainingAmountGuarantor.Value == 0)
        //        {
        //            var isAutoClosedRegOnPaymentWithHoldTx = AppSession.Parameter.IsAutoClosedRegOnPaymentWithHoldTx;

        //            var coll = new MergeBillingCollection();
        //            coll.Query.Where(coll.Query.FromRegistrationNo == entity.RegistrationNo);
        //            coll.LoadAll();

        //            var regs = new string[coll.Count + 1];
        //            var idx = 1;

        //            regs.SetValue(entity.RegistrationNo, 0);

        //            foreach (var bill in coll)
        //            {
        //                regs.SetValue(bill.RegistrationNo, idx);
        //                idx++;
        //            }

        //            var regis = new RegistrationCollection();
        //            regis.Query.Where(regis.Query.RegistrationNo.In(regs));
        //            regis.LoadAll();

        //            foreach (var re in regis)
        //            {
        //                if (!isAutoClosedRegOnPaymentWithHoldTx)
        //                    re.IsClosed = !((txtRemainingAmountPatient.Value + txtRemainingAmountGuarantor.Value) > 0) && isApprove;
        //                else
        //                {
        //                    re.IsHoldTransactionEntry = !((txtRemainingAmountPatient.Value + txtRemainingAmountGuarantor.Value) > 0) && isApprove;
        //                    re.IsHoldTransactionEntryByUserID = AppSession.UserLogin.UserID;
        //                }

        //                re.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //                re.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //            }

        //            var ques = new ServiceUnitQueCollection();
        //            ques.Query.Where(ques.Query.RegistrationNo.In(regs));
        //            ques.LoadAll();

        //            foreach (var que in ques)
        //            {
        //                if (!isAutoClosedRegOnPaymentWithHoldTx)
        //                    que.IsClosed = !((txtRemainingAmountPatient.Value + txtRemainingAmountGuarantor.Value) > 0) && isApprove;

        //                que.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //                que.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //            }

        //            using (var trans = new esTransactionScope())
        //            {
        //                regis.Save();

        //                if (ques.Count > 0)
        //                    ques.Save();

        //                trans.Complete();
        //            }

        //            var ques2 = new ServiceUnitQueCollection();
        //            ques2.Query.Where(ques2.Query.RegistrationNo == entity.RegistrationNo);
        //            ques2.LoadAll();

        //            foreach (var que in ques2)
        //            {
        //                if (!isAutoClosedRegOnPaymentWithHoldTx)
        //                    que.IsClosed = !((txtRemainingAmountPatient.Value + txtRemainingAmountGuarantor.Value) > 0) && isApprove;

        //                que.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //                que.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //            }

        //            using (var trans = new esTransactionScope())
        //            {
        //                if (ques2.Count > 0)
        //                    ques2.Save();

        //                trans.Complete();
        //            }

        //            if (!isAutoClosedRegOnPaymentWithHoldTx)
        //                reg.IsClosed = !((txtRemainingAmountPatient.Value + txtRemainingAmountGuarantor.Value) > 0) && isApprove;
        //            else
        //            {
        //                reg.IsHoldTransactionEntry = !((txtRemainingAmountPatient.Value + txtRemainingAmountGuarantor.Value) > 0) && isApprove;
        //                reg.IsHoldTransactionEntryByUserID = AppSession.UserLogin.UserID;
        //            }
        //        }
        //    }

        //    foreach (var item in TransPaymentItemOrders)
        //    {
        //        item.IsPaymentProceed = isApprove;
        //        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //        item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //    }

        //    foreach (var item in TransPaymentItemIntermBills)
        //    {
        //        item.IsPaymentProceed = isApprove;
        //        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //        item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //    }

        //    foreach (var item in TransPaymentItemIntermBillGuarantors)
        //    {
        //        item.IsPaymentProceed = isApprove;
        //        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //        item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //    }

        //    bool isAllowCheckout = TransPaymentItemIntermBills.Count > 0;

        //    using (var trans = new esTransactionScope())
        //    {
        //        entity.Save();

        //        if (AppSession.Parameter.IsNeedAllowCheckoutConfirmedForDischarge && isAllowCheckout)
        //        {
        //            reg.IsAllowPatientCheckOut = true;
        //            reg.AllowPatientCheckOutByUserID = AppSession.UserLogin.UserID;
        //            reg.AllowPatientCheckOutDateTime = (new DateTime()).NowAtSqlServer();
        //        }

        //        reg.Save();
        //        TransPaymentItemOrders.Save();
        //        TransPaymentItemIntermBills.Save();
        //        TransPaymentItemIntermBillGuarantors.Save();

        //        var package =
        //            (TransPaymentItems.Where(p => p.SRPaymentMethod == AppSession.Parameter.PaymentMethodPackageBalance))
        //                .SingleOrDefault();
        //        if (package != null)
        //        {
        //            var pat = new Patient();
        //            pat.LoadByPrimaryKey(reg.PatientID);

        //            if (pat.PackageBalance != null)
        //            {
        //                if (pat.PackageBalance > 0)
        //                {
        //                    if (isApprove)
        //                    {
        //                        if (package.IsPackageClosed ?? false)
        //                            pat.PackageBalance -= (package.Amount + package.Balance);
        //                        else
        //                            pat.PackageBalance -= package.Amount;
        //                    }
        //                    else
        //                    {
        //                        if (package.IsPackageClosed ?? false)
        //                            pat.PackageBalance += (package.Amount + package.Balance);
        //                        else
        //                            pat.PackageBalance += package.Amount;
        //                    }

        //                    pat.Save();
        //                }
        //            }
        //        }

        //        #region Guarantor Deposit Balance

        //        var tpicoll = new TransPaymentItemCollection();
        //        tpicoll.Query.Where(tpicoll.Query.PaymentNo == entity.PaymentNo,
        //                            tpicoll.Query.SRPaymentType == AppSession.Parameter.PaymentTypeSaldoAR);
        //        tpicoll.LoadAll();
        //        if (tpicoll.Count > 0)
        //        {
        //            decimal totAmount = tpicoll.Sum(item => item.Amount ?? 0);

        //            var balance = new GuarantorDepositBalance();
        //            var movement = new GuarantorDepositMovement();
        //            GuarantorDepositBalance.PrepareGuarantorDepositBalances(entity.PaymentNo, entity.GuarantorID,
        //                                                                    "A/R Process", AppSession.UserLogin.UserID,
        //                                                                    0,
        //                                                                    totAmount,
        //                                                                    ref balance, ref movement);
        //            balance.Save();
        //            movement.Save();
        //        }

        //        #endregion

        //        #region Membership - Update Reward Point
        //        var totPatientPayment = TransPaymentItems.Where(item => item.SRPaymentType == AppSession.Parameter.PaymentTypePayment).Sum(item => (item.Amount ?? 0));
        //        if (reg.MembershipDetailID == -1)
        //        {
        //            reg.MembershipDetailID = Registration.GetMembershipDetailId(reg.PatientID, reg.RegistrationDate.Value.Date);
        //            if (reg.MembershipDetailID != -1)
        //                reg.Save();
        //        }
        //        if (reg.MembershipDetailID != -1)
        //        {
        //            var div = AppSession.Parameter.MultipleForRewardPoints;
        //            var x = BusinessObject.MembershipDetail.UpdateRewardPoints(Convert.ToInt64(reg.MembershipDetailID), totPatientPayment, div, true, AppSession.UserLogin.UserID);
        //        }
        //        if (!string.IsNullOrEmpty(reg.MembershipNo))
        //        {
        //            var div = AppSession.Parameter.MultipleForRewardPointsForEmployee;
        //            var x = BusinessObject.MembershipDetail.UpdateEmployeeRewardPoints(reg.MembershipNo, reg.RegistrationNo, totPatientPayment, div, true, AppSession.UserLogin.UserID);
        //        }
        //        #endregion

        //        // rekal untuk prorata ???
        //        var ba = new BillingAdjustment(entity.RegistrationNo, true);
        //        var msg = ba.CalculateAndSaveProrata_NoTransactionScope(AppSession.UserLogin.UserID);
        //        if (!string.IsNullOrEmpty(msg))
        //        {
        //            //ShowInformationHeader(msg);
        //            //return false;
        //            throw new Exception(msg);
        //        }

        //        // update informasi payment jasmed
        //        var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
        //        if (isApprove)
        //        {
        //            feeColl.RecalculateForFeeByPlafonGuarantor(entity, TransPaymentItems, AppSession.UserLogin.UserID);
        //            feeColl.SetPayment(entity, 0, AppSession.UserLogin.UserID);
        //        }
        //        else
        //        {
        //            feeColl.UnSetPayment(entity, AppSession.UserLogin.UserID);
        //        }
        //        feeColl.Save();

        //        if (AppSession.Parameter.IsJobOrderRealizationNeedConfirm)
        //        {
        //            if (isApprove)
        //            {
        //                foreach (var item in TransPaymentItemOrders)
        //                {
        //                    var tci = new TransChargesItem();
        //                    if (tci.LoadByPrimaryKey(item.TransactionNo, item.SequenceNo))
        //                    {
        //                        tci.IsPaymentConfirmed = true;
        //                        tci.PaymentConfirmedBy = entity.PrintReceiptAsName;
        //                        tci.PaymentConfirmedDateTime = (new DateTime()).NowAtSqlServer().Date;
        //                        tci.LastPaymentConfirmedByUserID = AppSession.UserLogin.UserID;
        //                        tci.LastPaymentConfirmedDateTime = (new DateTime()).NowAtSqlServer();
        //                        tci.Save();
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                foreach (var item in TransPaymentItemOrders)
        //                {
        //                    var tci = new TransChargesItem();
        //                    if (tci.LoadByPrimaryKey(item.TransactionNo, item.SequenceNo))
        //                    {
        //                        tci.IsPaymentConfirmed = false;
        //                        tci.PaymentConfirmedBy = string.Empty;
        //                        tci.str.PaymentConfirmedDateTime = string.Empty;
        //                        tci.LastPaymentConfirmedByUserID = AppSession.UserLogin.UserID;
        //                        tci.LastPaymentConfirmedDateTime = (new DateTime()).NowAtSqlServer();
        //                        tci.Save();
        //                    }
        //                }
        //            }
        //        }

        //        //Commit if success, Rollback if failed
        //        trans.Complete();
        //    }

        //    // Create Journal Accounting
        //    CreateJournalAccounting(entity, this.TransPaymentItems, isApprove, reg);

        //    // checkout otomatis,
        //    Helper.RegistrationOpenClose.SetDischargeDate(reg);
        //}

        //public static void CreateJournalAccounting(TransPayment entity, TransPaymentItemCollection tpiColl, 
        //    bool isApprove, Registration reg)
        //{
        //    /* Automatic Journal Testing Start */
        //    if (isApprove)
        //    {
        //        /* Automatic Journal Testing Start */
        //        if (entity.TransactionCode == TransactionCode.PaymentReturn)
        //        {
        //            //function ini utk payment return dari pembelian resep
        //            int? journalId =
        //                JournalTransactions.AddNewPaymentReturnJournal(BusinessObject.JournalType.PaymentReturn,
        //                    entity, reg, tpiColl, "TP",
        //                    AppSession.UserLogin.UserID, 0);
        //        }
        //        else if (entity.TransactionCode == TransactionCode.Payment)
        //        {
        //            var x = (from tpi in tpiColl select tpi.SRPaymentType).Distinct();
        //            string journalType = BusinessObject.JournalType.Payment;
        //            if (x.Contains(AppSession.Parameter.PaymentTypeCorporateAR))
        //            {
        //                journalType = BusinessObject.JournalType.ARCreating;
        //            }
        //            else if (x.Contains(AppSession.Parameter.PaymentTypePersonalAR))
        //            {
        //                journalType = BusinessObject.JournalType.ARCreating;
        //            }
        //            else if (x.Contains(AppSession.Parameter.PaymentTypeSaldoAR))
        //            {
        //                //journalType = BusinessObject.JournalType.ARCreating;
        //            }

        //            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "Yes")
        //            {
        //                int? journalId = JournalTransactions.AddNewPaymentCashBasedJournal(journalType,
        //                    entity, reg, tpiColl,
        //                    "TP", entity.PaymentNo, AppSession.UserLogin.UserID, 0);
        //            }
        //            else
        //            {
        //                var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
        //                if (type.Contains(reg.SRRegistrationType))
        //                {

        //                    int? journalId = JournalTransactions.AddNewPaymentCashBasedJournalTemporary(journalType,
        //                        entity, reg, tpiColl,
        //                        "TP", entity.PaymentNo, AppSession.UserLogin.UserID, 0);
        //                }
        //                else
        //                {
        //                    int? journalId = JournalTransactions.AddNewPaymentCashBasedJournal(journalType,
        //                                                                                       entity, reg,
        //                                                                                       tpiColl,
        //                                                                                       "TP", entity.PaymentNo,
        //                                                                                       AppSession.UserLogin.
        //                                                                                           UserID, 0);
        //                }
        //            }
        //        }
        //        /* Automatic Journal Testing End */

        //        //if (AppSession.Parameter.IsPhysicianFeeBasedOnPayment == "Yes")
        //        //{
        //        //    int? x = ParamedicFeeTransChargesItemCompSettled.AddNewSettled(entity, TransPaymentItemOrders,
        //        //                                                               TransPaymentItemIntermBills,
        //        //                                                               AppSession.UserLogin.UserID);
        //        //}

        //        //if (AppSession.Parameter.IsPhysicianFeeArCreateOnArReceipt == "Yes")
        //        //{
        //        //    int? x = ParamedicFeeTransChargesItemCompSettled.AddNewSettled(entity, TransPaymentItemIntermBills,
        //        //                                                               AppSession.UserLogin.UserID);

        //        //    int? y = ParamedicFeeTransChargesItemCompSettled.AddNewSettled(entity, TransPaymentItemIntermBillGuarantors,
        //        //                                                               AppSession.UserLogin.UserID);
        //        //}
        //    }
        //    else
        //    {
        //        if (entity.TransactionCode == TransactionCode.PaymentReturn)
        //        {
        //            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "Yes")
        //            {
        //                //function ini utk payment return dari pembelian resep
        //                int? journalId =
        //                    JournalTransactions.AddNewPaymentReturnCorrectionJournal(
        //                        BusinessObject.JournalType.PaymentReturn, entity, reg, tpiColl, "TP",
        //                        AppSession.UserLogin.UserID, 0);
        //            }
        //            else
        //            {
        //                //var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
        //                //if (type.Contains(reg.SRRegistrationType))
        //                //{

        //                //}
        //                //else
        //                {
        //                    int? journalId =
        //                        JournalTransactions.AddNewPaymentReturnCorrectionJournal(
        //                            BusinessObject.JournalType.PaymentReturn, entity, reg, tpiColl, "TP",
        //                            AppSession.UserLogin.UserID, 0);
        //                }
        //            }
        //        }
        //        else if (entity.TransactionCode == TransactionCode.Payment)
        //        {
        //            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "Yes")
        //            {
        //                int? journalId =
        //                    JournalTransactions.AddNewPaymentCorrectionJournal(BusinessObject.JournalType.Payment,
        //                        entity, reg, tpiColl, "TP",
        //                        AppSession.UserLogin.UserID, 0);
        //            }
        //            else
        //            {
        //                var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
        //                if (type.Contains(reg.SRRegistrationType))
        //                {
        //                    int? journalId =
        //                        JournalTransactions.AddNewPaymentCorrectionJournalTemporary(BusinessObject.JournalType.Payment,
        //                            entity, reg, tpiColl, "TP",
        //                            AppSession.UserLogin.UserID, 0);
        //                }
        //                else
        //                {
        //                    int? journalId =
        //                        JournalTransactions.AddNewPaymentCorrectionJournal(BusinessObject.JournalType.Payment,
        //                            entity, reg, tpiColl, "TP",
        //                            AppSession.UserLogin.UserID, 0);
        //                }
        //            }
        //        }

        //        //if (AppSession.Parameter.IsPhysicianFeeBasedOnPayment == "Yes" || AppSession.Parameter.IsPhysicianFeeArCreateOnArReceipt == "Yes")
        //        //{
        //        //    int? x = ParamedicFeeTransChargesItemCompSettled.DeleteSettled(entity, false);
        //        //}
        //    }
        //}

        private void SetUnApproval(TransPayment entity, string voidReason)
        {
            entity.IsApproved = false;
            entity.IsVoid = true;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.VoidByUserID = AppSession.UserLogin.UserID;
            entity.VoidDate = (new DateTime()).NowAtSqlServer();
            entity.VoidReason = voidReason;

            //registration
            var reg = new Registration();
            reg.LoadByPrimaryKey(entity.RegistrationNo);

            reg.RemainingAmount += TransPaymentItems.Sum(detail => detail.Amount ?? 0);

            bool isClosedBefore = reg.IsClosed ?? false;
            reg.IsClosed = false;

            var coll = new TransPaymentCollection();
            //coll.Query.Where(coll.Query.PaymentReferenceNo == txtPaymentNo.Text);
            string searchTextContain = string.Format("%{0}%", txtPaymentNo.Text);
            coll.Query.Where(coll.Query.PaymentReferenceNo.Like(searchTextContain));
            coll.LoadAll();

            foreach (var tp in coll)
            {
                var newPaymentReferenceNo = tp.PaymentReferenceNo.Replace("|" + txtPaymentNo.Text, "");
                newPaymentReferenceNo = newPaymentReferenceNo.Replace(txtPaymentNo.Text, "");

                tp.PaymentReferenceNo = newPaymentReferenceNo; //string.Empty;
                tp.ReceiptIsReturned = null;
                tp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                tp.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in TransPaymentItemOrders)
            {
                item.IsPaymentProceed = false;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in TransPaymentItemIntermBills)
            {
                item.IsPaymentProceed = false;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in TransPaymentItemIntermBillGuarantors)
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

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                reg.Save();
                TransPaymentItemOrders.Save();
                TransPaymentItemIntermBills.Save();
                TransPaymentItemIntermBillGuarantors.Save();
                collac.Save();
                collbuffer.Save();

                if (coll.Count > 0)
                    coll.Save();

                if (isClosedBefore)
                {
                    var hist = new RegistrationCloseOpenHistory();
                    hist.AddNew();
                    hist.RegistrationNo = entity.RegistrationNo;
                    hist.StatusId = "C";
                    hist.IsTrue = false;
                    hist.Notes = "Payment Received >> UnApproval";
                    hist.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    hist.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    hist.Save();
                }

                /* Automatic Journal Testing Start */
                if (entity.TransactionCode == TransactionCode.PaymentReturn)
                {
                    int? journalId =
                        JournalTransactions.AddNewPaymentReturnCorrectionJournal(BusinessObject.JournalType.Payment,
                                                                                 entity, reg, this.TransPaymentItems,
                                                                                 "TP", entity.CreatedBy, 0);
                }
                else if (entity.TransactionCode == TransactionCode.Payment)
                {
                    var x = (from tpi in TransPaymentItems select tpi.SRPaymentType).Distinct();
                    string journalType = BusinessObject.JournalType.Payment;
                    if (x.Contains(AppSession.Parameter.PaymentTypeCorporateAR))
                    {
                        journalType = BusinessObject.JournalType.ARCreating;
                    }
                    else if (x.Contains(AppSession.Parameter.PaymentTypePersonalAR))
                    {
                        journalType = BusinessObject.JournalType.ARCreating;
                    }
                    else if (x.Contains(AppSession.Parameter.PaymentTypeSaldoAR))
                    {
                        //journalType = BusinessObject.JournalType.ARCreating;
                    }

                    if (AppSession.Parameter.IsUsingIntermBill)
                    {
                        //int? journalId =
                        //    JournalTransactions.AddNewPaymentCashBasedVoidJournal(BusinessObject.JournalType.Payment, entity,
                        //                                                                              reg, this.TransPaymentItems, "TP",
                        //                                                                              entity.CreatedBy);
                        int? journalId = JournalTransactions.AddNewPaymentCorrectionJournalCashBased(journalType, entity, reg, this.TransPaymentItems, "TP", entity.PaymentNo, AppSession.UserLogin.UserID, 0);
                    }
                    else
                    {
                        int? journalId =
                            JournalTransactions.AddNewPaymentCorrectionJournal(journalType, entity,
                                                                               reg, this.TransPaymentItems, "TP",
                                                                               entity.CreatedBy, 0);
                    }
                }

                /* Automatic Journal Testing End */

                //if (AppSession.Parameter.IsPhysicianFeeBasedOnPayment == "Yes")
                //{
                //    int? x = ParamedicFeeTransChargesItemCompSettled.DeleteSettled(entity, false);
                //}

                #region Guarantor Deposit Balance

                var tpicoll = new TransPaymentItemCollection();
                tpicoll.Query.Where(tpicoll.Query.PaymentNo == entity.PaymentNo,
                                    tpicoll.Query.SRPaymentType == AppSession.Parameter.PaymentTypeSaldoAR);
                tpicoll.LoadAll();
                if (tpicoll.Count > 0)
                {
                    decimal totAmount = tpicoll.Sum(item => item.Amount ?? 0);

                    var balance = new GuarantorDepositBalance();
                    var movement = new GuarantorDepositMovement();
                    GuarantorDepositBalance.PrepareGuarantorDepositBalances(entity.PaymentNo, entity.GuarantorID,
                                                                            "A/R Process (Void)", AppSession.UserLogin.UserID,
                                                                            totAmount,
                                                                            0,
                                                                            ref balance, ref movement);
                    balance.Save();
                    movement.Save();
                }

                #endregion

                #region Membership - Update Reward Point
                var totPatientPayment = TransPaymentItems.Where(item => item.SRPaymentType == AppSession.Parameter.PaymentTypePayment).Sum(item => (item.Amount ?? 0));
                if (reg.MembershipDetailID != -1)
                {
                    var div = AppSession.Parameter.MultipleForRewardPoints;
                    var x = BusinessObject.MembershipDetail.UpdateRewardPoints(Convert.ToInt64(reg.MembershipDetailID), totPatientPayment, div, false, AppSession.UserLogin.UserID);
                }
                if (!string.IsNullOrEmpty(reg.MembershipNo))
                {
                    var div = AppSession.Parameter.MultipleForRewardPointsForEmployee;
                    var x = BusinessObject.MembershipDetail.UpdateEmployeeRewardPoints(reg.MembershipNo, reg.RegistrationNo, totPatientPayment, div, false, AppSession.UserLogin.UserID);
                }
                #endregion

                // unset payment jasmed
                var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                feeColl.UnSetPayment(entity, AppSession.UserLogin.UserID);
                feeColl.Save();

                if (AppSession.Parameter.IsJobOrderRealizationNeedConfirm)
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

                // update qty realization TransPaymentItemVisite
                #region TransPaymentItemVisite
                var visitList = (TransPaymentItems.Where(p => p.VisiteDownPaymentNotes != null & p.VisiteDownPaymentNotes != string.Empty));
                if (visitList.Any() && visitList.Count() > 0)
                {
                    foreach (var v in visitList)
                    {
                        var val = v.VisiteDownPaymentNotes.Split(';');
                        var valLength = val.Length;
                        for (int i = 0; i < valLength; i++)
                        {
                            var val2 = val[i].Split('|');

                            var paymentNo = val2[0];
                            var itemId = val2[1];

                            var itemVisitQ = new TransPaymentItemVisiteQuery();
                            itemVisitQ.Where(itemVisitQ.PaymentNo == paymentNo, itemVisitQ.ItemID == itemId);

                            var itemVisit = new TransPaymentItemVisite();
                            if (itemVisit.Load(itemVisitQ) && itemVisit != null)
                            {
                                itemVisit.RealizationQty -= 1;
                                itemVisit.Save();
                            }
                        }
                    }
                }
                
                var visiteRealizations = new TransPaymentItemVisiteRealizationCollection();
                visiteRealizations.Query.Where(visiteRealizations.Query.PaymentReferenceNo == entity.PaymentNo);
                visiteRealizations.MarkAllAsDeleted();
                visiteRealizations.Save();

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

            var invoice = new InvoicesItemCollection();

            var item = new InvoicesItemQuery("a");
            var inv = new InvoicesQuery("b");

            item.InnerJoin(inv).On(inv.InvoiceNo == item.InvoiceNo);
            item.Where
                (
                    item.PaymentNo == txtPaymentNo.Text,
                    //inv.IsApproved == 1,
                    inv.IsVoid == 0
                );

            invoice.Load(item);

            if (invoice.Count > 0)
            {
                args.MessageText = "Data is already processed to invoice.";
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            var entity = new TransPayment();
            if (!entity.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (entity.CreatedBy != AppSession.UserLogin.UserID && _isPowerUser)
            {
                var usr = new AppUser();
                usr.LoadByPrimaryKey(entity.CreatedBy);

                args.MessageText = "You don't have authorization to un-void this transaction. This data belong to: " +
                                   usr.UserName + ". Please contact your supervision.";
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(esTransPayment entity, bool isVoid)
        {
            entity.IsVoid = isVoid;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            var coll = new TransPaymentCollection();
            if (isVoid)
            {
                //coll.Query.Where(coll.Query.PaymentReferenceNo == txtPaymentNo.Text);
                string searchTextContain = string.Format("%{0}%", txtPaymentNo.Text);
                coll.Query.Where(coll.Query.PaymentReferenceNo.Like(searchTextContain));
                coll.LoadAll();

                foreach (var tp in coll)
                {
                    var newPaymentReferenceNo = tp.PaymentReferenceNo.Replace("|" + txtPaymentNo.Text, "");
                    newPaymentReferenceNo = newPaymentReferenceNo.Replace(txtPaymentNo.Text, "");

                    tp.PaymentReferenceNo = newPaymentReferenceNo; //string.Empty;
                    tp.ReceiptIsReturned = null;
                    tp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    tp.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            var reg = new Registration();
            reg.LoadByPrimaryKey(entity.RegistrationNo);

            TransPaymentItemOrders.MarkAllAsDeleted();
            TransPaymentItemIntermBills.MarkAllAsDeleted();
            TransPaymentItemIntermBillGuarantors.MarkAllAsDeleted();

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                TransPaymentItemOrders.Save();
                TransPaymentItemIntermBills.Save();
                TransPaymentItemIntermBillGuarantors.Save();

                if (coll.Count > 0)
                    coll.Save();

                trans.Complete();
            }

            // checkout otomatis,
            Helper.RegistrationOpenClose.SetDischargeDate(reg);
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
                var paymentNo = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(paymentNo);
            }
            else
                entity.LoadByPrimaryKey(txtPaymentNo.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var transPayment = (TransPayment)entity;
            txtPaymentNo.Text = transPayment.PaymentNo;
            txtRegistrationNo.Text = transPayment.RegistrationNo;
            if (transPayment.SRPromotion == null) cboPromotion.SelectedIndex = -1;
            else cboPromotion.SelectedValue = transPayment.SRPromotion;

            var registration = new Registration();
            registration.LoadByPrimaryKey(txtRegistrationNo.Text);
            txtServiceUnitID.Text = registration.ServiceUnitID;
            hdnPatientId.Value = registration.PatientID;
            var patient = new Patient();
            if (patient.LoadByPrimaryKey(registration.PatientID))
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

            txtMemberID.Text = patient.str.MemberID;
            var member = new Guarantor();
            if (member.LoadByPrimaryKey(txtMemberID.Text)) lblMemberName.Text = guarantor.GuarantorName;

            txtPaymentDate.SelectedDate = transPayment.PaymentDate;
            txtPaymentTime.Text = transPayment.PaymentTime;

            ViewState["IsVoid"] = transPayment.IsVoid ?? false;
            ViewState["IsApproved"] = transPayment.IsApproved ?? false;

            txtNotes.Text = transPayment.Notes;
            txtInitial.Text = transPayment.Initial;

            string[] patientParam = new string[2], regno = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);
            patientParam.SetValue(AppSession.Parameter.PaymentTypePayment, 0);
            patientParam.SetValue(AppSession.Parameter.PaymentTypePersonalAR, 1);

            decimal treturn = Helper.Payment.GetTotalPayment(regno, false);
            decimal tPatientPaymentAmt = Helper.Payment.GetTotalPayment(regno, true, patientParam);
            decimal tGuarantorPaymentAmt = Helper.Payment.GetTotalPayment(regno, true, AppSession.Parameter.PaymentTypeCorporateAR);
            decimal tGuarantorSaldoAmt = Helper.Payment.GetTotalPayment(regno, true, AppSession.Parameter.PaymentTypeSaldoAR);

            var discPatient = (double)Helper.Payment.GetPaymentDiscount(regno, false);
            var discGuarantor = (double)Helper.Payment.GetPaymentDiscount(regno, true);

            txtTotalPaymentAmountPatient.Value = (double)(tPatientPaymentAmt + treturn);
            txtTotalPaymentAmountGuarantor.Value = (double)tGuarantorPaymentAmt + (double)tGuarantorSaldoAmt;

            var cob = new RegistrationGuarantorCollection();
            var cobq = new RegistrationGuarantorQuery("a");
            var gq = new GuarantorQuery("b");
            cobq.InnerJoin(gq).On(gq.GuarantorID == cobq.GuarantorID);
            cobq.Where(cobq.RegistrationNo == txtRegistrationNo.Text);
            cob.Load(cobq);
            decimal cobPlafond = cob.Sum(c => (c.PlafondAmount ?? 0));

            decimal tpatient, tguarantor;
            Helper.CostCalculation.GetBillingTotal(regno, registration.SRBussinesMethod, (registration.PlavonAmount ?? 0) + cobPlafond, out tpatient, out tguarantor, guarantor, registration.IsGlobalPlafond ?? false);

            var trounding = Helper.Payment.GetTotalRoundingPayment(txtPaymentNo.Text);

            txtRemainingAmountGuarantor.Value = (double)tguarantor - txtTotalPaymentAmountGuarantor.Value - discGuarantor;
            txtPlafonAmount.Value = (double)((registration.PlavonAmount ?? 0) + cobPlafond);
            txtDownPaymentAmount.Value = (double)(Helper.Payment.GetTotalDownPayment(regno) - Helper.Payment.GetTotalDownPaymentReturn(regno));
            if (btnDownPayment.Enabled)
                btnDownPayment.Enabled = (txtDownPaymentAmount.Value > 0);
            txtDiscountAmount.Value = (double)Helper.Payment.GetTotalPaymentDiscount(regno);
            txtPaymentDiscountPatient.Value = discPatient;
            txtPaymentDiscountGuarantor.Value = discGuarantor;
            //txtRemainingAmountPatient.Value = (double)tpatient - txtTotalPaymentAmountPatient.Value - discPatient + (double)trounding;

            //decimal selisih = 0;
            //bool isBridging = false;

            #region - pengecekan status bridging u/ billing, apakah mengikuti aturan bpjs atau selisih tetap dibayar pasien
            ///*pengecekan status bridging u/ billing, apakah mengikuti aturan bpjs atau selisih tetap dibayar pasien*/
            //if (AppSession.Parameter.IsBridgingBillingBpjs)
            //{
            //    //selisih kelas untuk bpjs
            //    if (Helper.IsInacbgIntegration)
            //    {
            //        var ncc = new NccInacbg();
            //        ncc.Query.es.Top = 1;
            //        ncc.Query.Where(ncc.Query.RegistrationNo.In(regno));
            //        if (ncc.Query.Load())
            //        {
            //            selisih = ncc.AddPaymentAmt ?? 0;
            //            isBridging = true;
            //        }
            //    }

            //    if (!isBridging || selisih == 0)
            //    {
            //        var bridging = new GuarantorBridging();
            //        bridging.Query.Where(bridging.Query.GuarantorID == registration.GuarantorID,
            //                             bridging.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(), AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(), AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
            //        if (bridging.Query.Load())
            //        {
            //            isBridging = true;
            //            if (registration.CoverageClassID != registration.ChargeClassID || registration.GuarantorID == AppSession.Parameter.SelfGuarantor)
            //            {
            //                isBridging = true;
            //                if (registration.CoverageClassID != registration.ChargeClassID || registration.GuarantorID == AppSession.Parameter.SelfGuarantor)
            //                {
            //                    var cov = new RegistrationCoverageDetail();
            //                    cov.Query.Select(cov.Query.CalculatedAmount.Sum());
            //                    cov.Query.Where(cov.Query.RegistrationNo == registration.RegistrationNo);
            //                    if (cov.Query.Load())
            //                    {
            //                        selisih = cov.CalculatedAmount ?? 0;

            //                        if (AppSession.Parameter.IsBridgingBillingBpjsWithCostSharing)
            //                        {
            //                            //1. cek selisih plafond (75% dari kelas 1)
            //                            //2. cek total tagihan - plafond
            //                            //3. ambil nilai paling kecil

            //                            var class1 = new Class();
            //                            class1.LoadByPrimaryKey(registration.CoverageClassID);

            //                            var asri1 = new AppStandardReferenceItem();
            //                            asri1.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class1.SRClassRL);

            //                            if (asri1.Note == "2") // Kelas 1
            //                            {
            //                                var class2 = new Class();
            //                                class2.LoadByPrimaryKey(registration.ChargeClassID);

            //                                var asri2 = new AppStandardReferenceItem();
            //                                asri2.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class2.SRClassRL);

            //                                if (new[] { "0", "1" }.Contains(asri2.Note)) // Kelas VIP, VVIP dll, diatas kelas 1 yg dihitung 75% coverage untuk selisih
            //                                {
            //                                    var plafon = (decimal)txtPlafonAmount.Value;

            //                                    decimal? totalTx = 0;
            //                                    totalTx = TransPaymentItemIntermBills.Where(item => item.PaymentNo != string.Empty).Aggregate(totalTx, (current, item) => current + (item.PatientAmount + item.GuarantorAmount));
            //                                    var admAmt = (registration.AdministrationAmount ?? 0);

            //                                    if (selisih > (totalTx ?? 0) + admAmt - plafon)
            //                                        selisih = (totalTx ?? 0) + admAmt - plafon;
            //                                }
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if ((registration.PlavonAmount2 ?? 0) > 0)
            //                        {
            //                            var class1 = new Class();
            //                            class1.LoadByPrimaryKey(registration.CoverageClassID);

            //                            var asri1 = new AppStandardReferenceItem();
            //                            asri1.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class1.SRClassRL);

            //                            var class2 = new Class();
            //                            class2.LoadByPrimaryKey(registration.ChargeClassID);

            //                            var asri2 = new AppStandardReferenceItem();
            //                            asri2.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class2.SRClassRL);

            //                            if (asri2.Note.ToInt() < asri1.Note.ToInt())
            //                                selisih = (registration.PlavonAmount2 ?? 0) - (registration.PlavonAmount ?? 0);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion

            bool isBridging = false;
            decimal? totalTx = 0;
            totalTx = TransPaymentItemIntermBills.Where(item => item.PaymentNo != string.Empty).Aggregate(totalTx, (current, item) => current + (item.PatientAmount + item.GuarantorAmount));
            decimal selisih = GetSelisihPasienBPJS(registration, ref isBridging, (totalTx ?? 0));

            if (selisih > 0)
                tpatient = (decimal)selisih;
            
            txtRemainingAmountPatient.Value = (double)tpatient - txtTotalPaymentAmountPatient.Value - discPatient + (double)trounding;

            txtAdminCal.Value = (double)(registration.AdministrationAmount ?? 0);
            txtPatientAdm.Value = (double)(registration.PatientAdm ?? 0);
            txtGuarantorAdm.Value = (double)(registration.GuarantorAdm ?? 0);
            txtRoundingAmount.Value = (double)trounding;

            if ((double)tpatient < 0) txtRemainingAmountPatient.Value = (double)tpatient;

            if (!string.IsNullOrEmpty(transPayment.GuarantorID))
            {
                var g = new Guarantor();
                g.LoadByPrimaryKey(transPayment.GuarantorID);
                cboGuarantorID.SelectedValue = g.GuarantorID;
                cboGuarantorID.Text = g.GuarantorID + " - " + g.GuarantorName;
            }
            else
            {
                cboGuarantorID.Items.Clear();
                cboGuarantorID.Text = string.Empty;
            }

            rblToGuarantor.SelectedIndex = (transPayment.IsToGuarantor ?? false) ? 0 : 1;

            if (AppSession.Parameter.IsUsingIntermBill)
            {
                btnIntermBill.Visible = rblToGuarantor.SelectedIndex == 1;
                btnIntermBillGuarantor.Visible = rblToGuarantor.SelectedIndex == 0;
            }

            PopulateTransPaymentItemGrid();
            PopulateTransPaymentItemOrder();

            if (transPayment.IsApproved == false && transPayment.IsVoid == false)
            {
                foreach (var item in TransPaymentItems)
                {
                    if (item.SRPaymentType != AppSession.Parameter.PaymentTypeCorporateAR && item.SRPaymentType != AppSession.Parameter.PaymentTypeSaldoAR)
                    {
                        txtRemainingAmountPatient.Value -= Convert.ToDouble(item.Amount);
                        txtTotalPaymentAmountPatient.Value += Convert.ToDouble(item.Amount);
                    }
                    else
                    {
                        txtRemainingAmountGuarantor.Value -= Convert.ToDouble(item.Amount);
                        txtTotalPaymentAmountGuarantor.Value += Convert.ToDouble(item.Amount);
                    }
                }
            }

            txtTransPatientAmount.Value = 0;
            txtTransGuarantorAmount.Value = 0;

            lblRegistrationInfo2.Text = RegistrationInfoSumary.GetDocumentCheckListCountRemains(txtRegistrationNo.Text);

            CalculateOrderAmount();
            CalculateIntermBillAmount();
            CalculateIntermBillGuarantorAmount();
            //CalculateDownPaymentAmount();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(esTransPayment entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtPaymentNo.Text = GetNewPaymentNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            //var reg = new Registration();
            //reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            //entity.TransactionCode = reg.isDirectPrescriptionReturn == false
            //                             ? TransactionCode.Payment
            //                             : TransactionCode.PaymentReturn;
            entity.TransactionCode = TransactionCode.Payment;
            entity.PaymentNo = txtPaymentNo.Text;
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.PaymentDate = txtPaymentDate.SelectedDate;
            entity.PaymentTime = txtPaymentTime.Text;
            entity.PrintReceiptAsName = txtPrintReceiptAsName.Text;
            entity.PaymentReferenceNo = string.Empty;
            entity.TotalPaymentAmount = (decimal)txtTotalPaymentAmountPatient.Value +
                                        (decimal)txtTotalPaymentAmountGuarantor.Value;
            entity.RemainingAmount = (decimal)txtRemainingAmountPatient.Value +
                                     (decimal)txtRemainingAmountGuarantor.Value;
            entity.PrintNumber = 0;
            entity.PaymentReceiptNo = string.Empty;

            entity.IsVoid = false;
            entity.IsApproved = false;
            entity.Notes = txtNotes.Text;
            entity.Initial = txtInitial.Text;
            entity.IsToGuarantor = rblToGuarantor.SelectedIndex == 0;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            if (entity.es.IsAdded)
            {
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entity.CashManagementNo = CashManagementNo;
            }

            if (AppSession.Parameter.IsUsingPromotion)
            {
                entity.SRPromotion = cboPromotion.SelectedValue;
            }

            //var guarantorId = string.Empty;
            foreach (var item in TransPaymentItems)
            {
                item.PaymentNo = entity.PaymentNo;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                //guarantorId = item.SRPaymentType == AppSession.Parameter.PaymentTypeCorporateAR ||
                //              item.SRPaymentType == AppSession.Parameter.PaymentTypeSaldoAR
                //                  ? cboGuarantorID.SelectedValue
                //                  : AppSession.Parameter.SelfGuarantor;
            }
            //entity.GuarantorID = guarantorId;
            entity.GuarantorID = entity.IsToGuarantor == true ? cboGuarantorID.SelectedValue : AppSession.Parameter.SelfGuarantor;

            foreach (var item in TransPaymentItemOrders)
            {
                item.PaymentNo = entity.PaymentNo;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in TransPaymentItemIntermBills)
            {
                item.PaymentNo = entity.PaymentNo;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in TransPaymentItemIntermBillGuarantors)
            {
                item.PaymentNo = entity.PaymentNo;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
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
                TransPaymentItemIntermBills.Save();
                TransPaymentItemIntermBillGuarantors.Save();

                var refno = (string)Session["PaymentReceive:collTransPaymentReference" + Request.QueryString["regno"]];
                if (!string.IsNullOrEmpty(refno))
                {
                    var val = refno.Split('|');

                    var refnos = val[0].Split(',');
                    var refnos2 = val[1].Split(',');

                    if (refnos.Any())
                    {
                        foreach (var array in refnos)
                        {
                            var dp = new TransPayment();
                            if (dp.LoadByPrimaryKey(array))
                            {
                                if (dp.PaymentReferenceNo == string.Empty)
                                    dp.PaymentReferenceNo = entity.PaymentNo;
                                else
                                {
                                    if (!dp.PaymentReferenceNo.Contains(entity.PaymentNo))
                                        dp.PaymentReferenceNo += ("|" + entity.PaymentNo);
                                }
                                    
                                dp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                dp.LastUpdateDateTime = (new DateTime()).NowAtSqlServer().Date;
                                if (dp.RegistrationNo == "" && dp.IsVisiteDownPayment == false)
                                    dp.RegistrationNo = entity.RegistrationNo;

                                dp.Save();
                            }
                        }
                        if (refnos2.Any())
                        {
                            foreach (var array in refnos2)
                            {
                                var dp = new TransPayment();
                                if (dp.LoadByPrimaryKey(array))
                                {
                                    dp.ReceiptIsReturned = true;

                                    dp.Save();
                                }
                            }
                        }
                    }
                }

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
                que.Where(
                    que.PaymentNo > txtPaymentNo.Text,
                    que.TransactionCode == TransactionCode.Payment
                    //que.Or(que.TransactionCode == TransactionCode.Payment,
                    //       que.TransactionCode == TransactionCode.PaymentReturn)
                    );
                que.OrderBy(que.PaymentNo.Ascending);
            }
            else
            {
                que.Where(
                    que.PaymentNo < txtPaymentNo.Text,
                    que.TransactionCode == TransactionCode.Payment
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
                var obj = Session["PaymentReceive:collTransPaymentItem" + Request.QueryString["regno"]];
                if (obj != null)
                    return ((TransPaymentItemCollection)(obj));

                var coll = new TransPaymentItemCollection();
                var query = new TransPaymentItemQuery("a");
                var srQuery = new PaymentMethodQuery("b");
                var srQuery2 = new PaymentTypeQuery("c");

                query.Select
                    (
                        query,
                        srQuery2.PaymentTypeName.As("refToAppStandardReferenceItem_PaymentType"),
                        srQuery.PaymentMethodName.As("refToAppStandardReferenceItem_PaymentMethod"),
                        "<0 AS [refToTransPaymentItem_IsDownPayment]>",
                        "<CASE WHEN a.AmountReceived > 0 THEN a.AmountReceived - a.Amount ELSE 0 END AS [refToTransPaymentItem_Change]>"
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

                Session["PaymentReceive:collTransPaymentItem" + Request.QueryString["regno"]] = coll;
                return coll;
            }
            set { Session["PaymentReceive:collTransPaymentItem" + Request.QueryString["regno"]] = value; }
        }

        private void RefreshCommandItemTransPaymentItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            var isVisible = (newVal != AppEnum.DataMode.Read);
            grdTransPaymentItem.Columns[0].Visible = isVisible;
            grdTransPaymentItem.Columns[grdTransPaymentItem.Columns.Count - 1].Visible = isVisible;

            grdTransPaymentItem.Columns.FindByUniqueName("IsBackOfficeReturn").Visible = AppSession.Parameter.IsAllowPaymentReturnFromCashEntry;

            grdTransPaymentItem.MasterTableView.CommandItemDisplay = isVisible
                                                                         ? GridCommandItemDisplay.Top
                                                                         : GridCommandItemDisplay.None;

            //if (newVal != AppEnum.DataMode.Read) {
            //    grdTransPaymentItem.MasterTableView.InsertItem();
            //    //grdTransPaymentItem.Rebind();
            //}
            //Perbaharui tampilan dan data
            //if (IsPostBack)
            grdTransPaymentItem.Rebind();

            grdOrderItem.MasterTableView.CommandItemDisplay =
                string.IsNullOrEmpty(Request.QueryString["utype"]) && isVisible
                    ? GridCommandItemDisplay.Top
                    : GridCommandItemDisplay.None;
            if (IsPostBack)
                grdOrderItem.Rebind();

            grdIntermBill.MasterTableView.CommandItemDisplay =
                string.IsNullOrEmpty(Request.QueryString["utype"]) && isVisible
                    ? GridCommandItemDisplay.Top
                    : GridCommandItemDisplay.None;
            if (IsPostBack)
                grdIntermBill.Rebind();

            grdIntermBillGuarantor.MasterTableView.CommandItemDisplay =
                string.IsNullOrEmpty(Request.QueryString["utype"]) && isVisible
                    ? GridCommandItemDisplay.Top
                    : GridCommandItemDisplay.None;
            if (IsPostBack)
                grdIntermBillGuarantor.Rebind();

            btnSummary.Enabled = isVisible;

            bool isBridging = false;
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                /*pengecekan status bridging u/ billing, apakah mengikuti aturan bpjs atau selisih tetap dibayar pasien*/
                //if (AppSession.Parameter.IsBridgingBillingBpjs)
                var isBridgingBillingBpjs = AppParameter.IsYes(AppParameter.ParameterItem.IsBridgingBillingBpjs);
                if (isBridgingBillingBpjs)
                {
                    var bridging = new GuarantorBridging();
                    bridging.Query.Where(bridging.Query.GuarantorID == reg.GuarantorID,
                                         bridging.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(),
                                                                          AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(),
                                                                          AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
                    if (bridging.Query.Load())
                    {
                        isBridging = true;
                    }
                }
                if (reg.SRRegistrationType != AppConstant.RegistrationType.InPatient && !isBridging) btnOrderItem.Enabled = rblToGuarantor.SelectedIndex == 1 && isVisible;
                else btnOrderItem.Enabled = false;
            }
            else btnOrderItem.Enabled = isVisible;

            if (AppSession.Parameter.IsUsingIntermBill)
            {
                //btnIntermBill.Enabled = rblToGuarantor.SelectedIndex == 1 && txtRemainingAmountPatient.Value <= 0 ? false : isVisible;
                //btnIntermBillGuarantor.Enabled = rblToGuarantor.SelectedIndex == 0 && txtRemainingAmountGuarantor.Value <= 0 ? false : isVisible;
                btnIntermBill.Enabled = isVisible;
                btnIntermBillGuarantor.Enabled = isVisible;
            }

            btnDownPayment.Enabled = isVisible;
        }

        private void PopulateTransPaymentItemGrid()
        {
            //Display Data Detail
            TransPaymentItems = null; //Reset Record Detail
            grdTransPaymentItem.DataSource = TransPaymentItems; //Requery
            grdTransPaymentItem.MasterTableView.IsItemInserted = DataModeCurrent == AppEnum.DataMode.New;
            grdTransPaymentItem.MasterTableView.ClearEditItems();
            grdTransPaymentItem.DataBind();
        }

        protected void chkIsBackOfficeReturn_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            if (chk != null)
            {
                var seqNo = ((GridDataItem)chk.Parent.Parent).GetDataKeyValue("SequenceNo");
                var payNo = ((GridDataItem)chk.Parent.Parent).GetDataKeyValue("PaymentNo");

                //var payItem = TransPaymentItems.Where(a => a.PaymentNo == payNo && a.SequenceNo == seqNo).FirstOrDefault();
                //if(payItem != null){
                //    payItem.IsBackOfficeReturn = chk.Checked;
                //}

                foreach (var n in TransPaymentItems)
                {
                    if (Equals(n.PaymentNo, payNo) && n.SequenceNo.Equals(seqNo)) n.IsBackOfficeReturn = chk.Checked;
                }
            }
        }

        protected void grdTransPaymentItem_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                grdTransPaymentItem.MasterTableView.IsItemInserted = true;
                grdTransPaymentItem.Rebind();
            }
        }

        protected void grdTransPaymentItem_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var chk = (CheckBox)e.Item.FindControl("chkIsBackOfficeReturn");
                if (chk != null)
                {
                    chk.Checked = ((TransPaymentItem)e.Item.DataItem).IsBackOfficeReturn ?? false;
                    chk.Enabled = (DataModeCurrent != AppEnum.DataMode.Read) &&
                        ((((TransPaymentItem)e.Item.DataItem).Balance ?? 0) > 0);
                }
            }
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

            var entity = FindTransPaymentItem((string)editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                                                           [TransPaymentItemMetadata.ColumnNames.SequenceNo]);
            if (entity != null)
            {
                decimal prevAmount = 0;
                SetEntityValue(entity, e, out prevAmount);

                if (entity.SRPaymentType == AppSession.Parameter.PaymentTypeCorporateAR || entity.SRPaymentType == AppSession.Parameter.PaymentTypeSaldoAR)
                {
                    if ((double)prevAmount > (double)entity.Amount)
                    {
                        txtTotalPaymentAmountGuarantor.Value = (txtTotalPaymentAmountGuarantor.Value ?? 0) -
                                                               (double)entity.Amount;
                        txtRemainingAmountGuarantor.Value = (double)prevAmount - (double)entity.Amount;
                    }
                    else
                    {
                        txtTotalPaymentAmountGuarantor.Value = ((txtTotalPaymentAmountGuarantor.Value ?? 0) +
                                                                (double)entity.Amount) -
                                                               ((double)entity.Amount - (double)prevAmount);
                        txtRemainingAmountGuarantor.Value = ((double)entity.Amount - (double)prevAmount) -
                                                            (txtRemainingAmountGuarantor.Value ?? 0);
                    }
                }
                else if (entity.SRPaymentType == AppSession.Parameter.PaymentTypePayment ||
                         entity.SRPaymentType == AppSession.Parameter.PaymentTypePersonalAR)
                {
                    if (entity.SRPaymentType == AppSession.Parameter.PaymentTypePersonalAR)
                    {
                        if ((double)prevAmount > (double)entity.Amount)
                        {
                            txtTotalPaymentAmountPatient.Value = (txtTotalPaymentAmountPatient.Value ?? 0) -
                                                                 (double)entity.Amount;
                            txtRemainingAmountPatient.Value = (double)prevAmount - (double)entity.Amount;
                        }
                        else
                        {
                            txtTotalPaymentAmountPatient.Value = ((txtTotalPaymentAmountPatient.Value ?? 0) +
                                                                  (double)entity.Amount) -
                                                                 ((double)entity.Amount - (double)prevAmount);
                            txtRemainingAmountPatient.Value = ((double)entity.Amount - (double)prevAmount);
                        }
                    }
                    else
                    {
                        if (entity.SRPaymentMethod == AppSession.Parameter.PaymentMethodCash)
                        {
                            if ((double)prevAmount > (double)entity.Amount)
                            {
                                txtTotalPaymentAmountPatient.Value = (txtTotalPaymentAmountPatient.Value ?? 0) - (double)entity.Amount;
                                //txtRemainingAmountPatient.Value = (double)prevAmount - (double)entity.Amount + (double)entity.RoundingAmount;
                                txtRemainingAmountPatient.Value = (double)prevAmount - (double)entity.Amount - (double)entity.RoundingAmount;
                                txtRoundingAmount.Value += (double)entity.RoundingAmount;
                            }
                            else
                            {
                                txtTotalPaymentAmountPatient.Value = ((txtTotalPaymentAmountPatient.Value ?? 0) + (double)entity.Amount) - ((double)entity.Amount - (double)prevAmount);
                                txtRemainingAmountPatient.Value = ((double)entity.Amount - (double)prevAmount) - (txtRemainingAmountPatient.Value ?? 0);
                                txtRoundingAmount.Value = 0;
                            }
                        }
                        else
                        {
                            if ((double)prevAmount > (double)entity.Amount)
                            {
                                txtTotalPaymentAmountPatient.Value = (txtTotalPaymentAmountPatient.Value ?? 0) -
                                                                     (double)entity.Amount;
                                txtRemainingAmountPatient.Value = (double)prevAmount - (double)entity.Amount;
                            }
                            else
                            {
                                txtTotalPaymentAmountPatient.Value = ((txtTotalPaymentAmountPatient.Value ?? 0) +
                                                                      (double)entity.Amount) -
                                                                     ((double)entity.Amount - (double)prevAmount);
                                txtRemainingAmountPatient.Value = ((double)entity.Amount - (double)prevAmount);
                            }
                        }
                    }
                }
                else if (entity.SRPaymentType == AppSession.Parameter.PaymentTypeDiscount)
                {
                    if ((double)prevAmount > (double)entity.Amount)
                    {
                        txtDiscountAmount.Value = (txtDiscountAmount.Value ?? 0) - (double)entity.Amount;
                        txtRemainingAmountPatient.Value = (double)prevAmount - (double)entity.Amount;
                        if (rblToGuarantor.SelectedIndex == 0)
                        {
                            txtPaymentDiscountGuarantor.Value = (txtPaymentDiscountGuarantor.Value ?? 0) - (double)entity.Amount;
                        }
                        else
                            txtPaymentDiscountPatient.Value = (txtPaymentDiscountPatient.Value ?? 0) - (double)entity.Amount;
                    }
                    else
                    {
                        txtDiscountAmount.Value = ((txtDiscountAmount.Value ?? 0) + (double)entity.Amount) -
                                                  ((double)entity.Amount - (double)prevAmount);
                        txtRemainingAmountPatient.Value = ((double)entity.Amount - (double)prevAmount) -
                                                          (txtRemainingAmountPatient.Value ?? 0);

                        if (rblToGuarantor.SelectedIndex == 0)
                        {
                            txtPaymentDiscountGuarantor.Value = ((txtPaymentDiscountGuarantor.Value ?? 0) +
                                                                 (double)entity.Amount) -
                                                                ((double)entity.Amount - (double)prevAmount);
                        }
                        else
                            txtPaymentDiscountPatient.Value = ((txtPaymentDiscountPatient.Value ?? 0) -
                                                               (double)entity.Amount + (double)entity.Amount) -
                                                              ((double)entity.Amount - (double)prevAmount);

                    }
                }
            }
        }

        protected void grdTransPaymentItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            var sequenceNo =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][TransPaymentItemMetadata.ColumnNames.SequenceNo]);
            var entity = FindTransPaymentItem(sequenceNo);

            if (entity != null)
            {
                if (entity.SRPaymentType == AppSession.Parameter.PaymentTypePayment)
                {
                    txtTotalPaymentAmountPatient.Value -= (double)entity.Amount;
                    if (entity.SRPaymentMethod == AppSession.Parameter.PaymentMethodCash)
                        txtRemainingAmountPatient.Value += ((double)entity.Amount - (double)entity.RoundingAmount);
                    else
                        txtRemainingAmountPatient.Value += (double)entity.Amount;
                    txtRoundingAmount.Value -= (double)entity.RoundingAmount;
                }
                else if (entity.SRPaymentType == AppSession.Parameter.PaymentTypePersonalAR)
                {
                    txtTotalPaymentAmountPatient.Value -= (double)entity.Amount;
                    txtRemainingAmountPatient.Value += (double)entity.Amount;
                }
                else if (entity.SRPaymentType == AppSession.Parameter.PaymentTypeDiscount)
                {
                    txtDiscountAmount.Value -= (double)entity.Amount;
                    txtRemainingAmountPatient.Value += (double)entity.Amount;
                    if (rblToGuarantor.SelectedIndex == 0)
                    {
                        txtPaymentDiscountGuarantor.Value -= (double)entity.Amount;
                    }
                    else
                        txtPaymentDiscountPatient.Value -= (double)entity.Amount;
                }
                else
                {
                    txtTotalPaymentAmountGuarantor.Value -= (double)entity.Amount;
                    txtRemainingAmountGuarantor.Value += (double)entity.Amount;
                }

                entity.MarkAsDeleted();
            }
        }

        protected void grdTransPaymentItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = TransPaymentItems.AddNew();

            decimal prevAmount;
            SetEntityValue(entity, e, out prevAmount);

            if (entity.SRPaymentType == AppSession.Parameter.PaymentTypeCorporateAR || entity.SRPaymentType == AppSession.Parameter.PaymentTypeSaldoAR)
            {
                txtTotalPaymentAmountGuarantor.Value += (double)entity.Amount;
                txtRemainingAmountGuarantor.Value -= (double)entity.Amount;
            }
            else if (entity.SRPaymentType == AppSession.Parameter.PaymentTypePayment ||
                     entity.SRPaymentType == AppSession.Parameter.PaymentTypePersonalAR)
            {
                txtTotalPaymentAmountPatient.Value += (double)entity.Amount;
                //txtRemainingAmountPatient.Value -= ((double)entity.Amount + (double)entity.RoundingAmount);
                txtRemainingAmountPatient.Value -= ((double)entity.Amount - (double)entity.RoundingAmount);
                txtRoundingAmount.Value += (double)entity.RoundingAmount;
            }
            else if (entity.SRPaymentType == AppSession.Parameter.PaymentTypeDiscount)
            {
                txtDiscountAmount.Value += (double)entity.Amount;
                txtRemainingAmountPatient.Value -= (double)entity.Amount;
                if (rblToGuarantor.SelectedIndex == 0)
                {
                    txtPaymentDiscountGuarantor.Value += (double)entity.Amount;
                }
                else
                    txtPaymentDiscountPatient.Value += (double)entity.Amount;
            }

            double rounding = 0;
            foreach (var item in TransPaymentItems)
            {
                if (item.IsFromDownPayment == false && item.SequenceNo.ToInt() < entity.SequenceNo.ToInt() && item.RoundingAmount != 0)
                {
                    rounding += (double)item.RoundingAmount;
                    item.RoundingAmount = 0;
                }
            }
            if (rounding != 0)
            {
                txtRemainingAmountPatient.Value -= rounding;
                txtRoundingAmount.Value -= rounding;
            }

            //Stay in insert mode
            e.Canceled = true;
            grdTransPaymentItem.Rebind();
        }

        private TransPaymentItem FindTransPaymentItem(String sequenceNo)
        {
            var coll = TransPaymentItems;
            return coll.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        private void SetEntityValue(TransPaymentItem entity, GridCommandEventArgs e, out Decimal prevAmount)
        {
            var userControl = (ItemPaymentReceive)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
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
                entity.str.SRDiscountReason = userControl.SRDiscountReason;
                entity.str.EDCMachineID = userControl.EDCMachineID;
                entity.CardHolderName = userControl.CardHolderName;
                entity.CardFeeAmount = userControl.CardFeeAmount;
                entity.BankID = userControl.BankID;
                entity.Amount = userControl.Amount;
                entity.IsPackageClosed = userControl.IsClosed;
                entity.CardNo = userControl.CardNo;
                entity.RoundingAmount = Math.Round(userControl.RoundingAmount, 2);
                entity.AmountReceived = userControl.AmountReceived;
                entity.Change = userControl.Change;

                if (entity.es.IsAdded)
                    entity.IsFromDownPayment = entity.SRPaymentMethod == AppSession.Parameter.PaymentMethodPackageBalance;

                if (entity.IsPackageClosed ?? false)
                {
                    var pat = new Patient();
                    pat.LoadByMedicalNo(txtMedicalNo.Text);

                    if (pat.PackageBalance != null)
                        if (pat.PackageBalance > 0)
                            entity.Balance = pat.PackageBalance - entity.Amount;
                }

                prevAmount = userControl.PrevoiusAmount;
            }
            else
                prevAmount = 0;
        }

        #endregion

        #region Record Detail Methode Function TransPaymentItemOrder

        private TransPaymentItemOrderCollection TransPaymentItemOrders
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["PaymentReceive:collTransPaymentItemOrder" + Request.QueryString["regno"]];
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

                Session["PaymentReceive:collTransPaymentItemOrder" + Request.QueryString["regno"]] = coll;

                return coll;
            }
            set { Session["PaymentReceive:collTransPaymentItemOrder" + Request.QueryString["regno"]] = value; }
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
                //total = TransPaymentItemOrders.Where(item => item.PaymentNo != string.Empty)
                //                              .Aggregate(total, (current, item) => current + (item.Qty * item.Price));
                total = TransPaymentItemOrders.Where(item => item.PaymentNo != string.Empty)
                                                  .Aggregate(total, (current, item) => current + (item.Total ?? (item.Qty * item.Price)));
            }

            txtTransPatientAmount.Value = Convert.ToDouble(total);
        }

        private void CalculateDownPaymentAmount()
        {
            decimal? total = 0;
            if (TransPaymentItems.Count > 0)
            {
                total = TransPaymentItems.Where(item => item.PaymentNo != string.Empty && item.IsFromDownPayment == true)
                                                  .Aggregate(total, (current, item) => current + ((item.Amount ?? 0) + (item.Balance ?? 0)));
            }
            if (total > 0)
                txtDownPaymentAmount.Value = Convert.ToDouble(total);
        }

        #endregion

        #region Record Detail Methode Function TransPaymentItemIntermBill

        private TransPaymentItemIntermBillCollection TransPaymentItemIntermBills
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["PaymentReceive:collTransPaymentItemIntermBill" + Request.QueryString["regno"]];
                    if (obj != null)
                        return ((TransPaymentItemIntermBillCollection)(obj));
                }

                var coll = new TransPaymentItemIntermBillCollection();

                var query = new TransPaymentItemIntermBillQuery("a");
                var ib = new IntermBillQuery("b");
                query.InnerJoin(ib).On(query.IntermBillNo == ib.IntermBillNo);
                query.Where(query.PaymentNo == txtPaymentNo.Text);

                query.Select
                    (
                        query,
                        ib.RegistrationNo.As("refIntermBill_RegistrationNo"),
                        ib.IntermBillDate.As("refIntermBill_IntermBillDate"),
                        ib.StartDate.As("refIntermBill_StartDate"),
                        ib.EndDate.As("refIntermBill_EndDate"),
                        (ib.PatientAmount + ib.AdministrationAmount - ib.DiscAdmPatient).As("refToIntermBill_PatientAmount"),
                        (ib.GuarantorAmount + ib.GuarantorAdministrationAmount - ib.DiscAdmGuarantor).As("refToIntermBill_GuarantorAmount"),
                        (ib.PatientAmount + ib.AdministrationAmount - ib.DiscAdmPatient + ib.GuarantorAmount + ib.GuarantorAdministrationAmount - ib.DiscAdmGuarantor).As("refToIntermBill_TotalAmount"),
                        ib.AskesCoveredSeqNo.As("refToIntermBill_AskesCoveredSeqNo")
                    );

                coll.Load(query);

                Session["PaymentReceive:collTransPaymentItemIntermBill" + Request.QueryString["regno"]] = coll;
                return coll;
            }
            set { Session["PaymentReceive:collTransPaymentItemIntermBill" + Request.QueryString["regno"]] = value; }
        }

        protected void grdIntermBill_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdIntermBill.DataSource = TransPaymentItemIntermBills;
        }

        public static decimal GetSelisihPasienBPJS(Registration reg, ref bool isBridging, decimal totalTx) {
            decimal selisih = 0;

            if (!AppSession.Parameter.GuarantorAskesID.Contains(reg.GuarantorID))
            {
                isBridging = false;
                return 0;
            }

            /*pengecekan status bridging u/ billing, apakah mengikuti aturan bpjs atau selisih tetap dibayar pasien*/
            //if (AppSession.Parameter.IsBridgingBillingBpjs)
            var isBridgingBillingBpjs = AppParameter.IsYes(AppParameter.ParameterItem.IsBridgingBillingBpjs);
            if (isBridgingBillingBpjs)
            {
                if (Helper.IsInacbgIntegration)
                {
                    var ncc = new NccInacbg();
                    ncc.Query.es.Top = 1;
                    ncc.Query.Where(ncc.Query.RegistrationNo.In(reg.RegistrationNo));
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
                                            cobq.Where(cobq.RegistrationNo == reg.RegistrationNo);
                                            cob.Load(cobq);

                                            decimal cobPlafond = cob.Sum(c => (c.PlafondAmount ?? 0));
                                            var plafon = (reg.PlavonAmount ?? 0) + cobPlafond;

                                            var admAmt = (reg.AdministrationAmount ?? 0);

                                            if (selisih > totalTx + admAmt - plafon)
                                                selisih = totalTx + admAmt - plafon;
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

            return selisih;
        }
        private void CalculateIntermBillAmount()
        {
            decimal? total = 0;

            var registration = new Registration();
            registration.LoadByPrimaryKey(txtRegistrationNo.Text);

            bool isBridging = false;
            decimal? totalTx = 0;
            totalTx = TransPaymentItemIntermBills.Where(item => item.PaymentNo != string.Empty).Aggregate(totalTx, (current, item) => current + (item.PatientAmount + item.GuarantorAmount));
            decimal selisih = GetSelisihPasienBPJS(registration, ref isBridging, (totalTx ?? 0));
            
            if (selisih > 0)
            {
                txtTransPatientAmount.Value = Convert.ToDouble(selisih);
                return;
            }

            if (TransPaymentItemIntermBills.Count > 0)
            {
                if (txtPlafonAmount.Value > 0)
                {
                    decimal? plafondAmount = 0;
                    foreach (var item in TransPaymentItemIntermBills)
                    {
                        total += item.TotalAmount;

                        var ib = new IntermBill();
                        if (ib.LoadByPrimaryKey(item.IntermBillNo))
                        {
                            var ac = new AskesCovered2();
                            if (ac.LoadByPrimaryKey(txtRegistrationNo.Text, ib.AskesCoveredSeqNo))
                            {
                                plafondAmount += ((ac.RoomAmount * ac.RoomDays) + (ac.IccuAmount * ac.IccuDays) +
                                                (ac.HcuAmount * ac.HcuDays) + ac.SurgeryAmount + ac.MedicalSupportAmount +
                                                ac.CtScanAmount + ac.HaemodialiseAmount);
                            }
                        }
                    }
                    if (isBridging)//(AppSession.Parameter.GuarantorAskesID.Contains(cboGuarantorID.SelectedValue))
                    {
                        txtPlafonAmount.Value = plafondAmount > 0 ? Convert.ToDouble(plafondAmount) : txtPlafonAmount.Value;
                        txtTransPatientAmount.Value = txtRemainingAmountPatient.Value;
                    }
                    else
                    {
                        var paid = Helper.Payment.GetTotalPaymentByIntermbill(TransPaymentItemIntermBills.Select(x => x.IntermBillNo).ToArray(), true, false, System.Convert.ToDecimal(txtPlafonAmount.Value));

                        txtTransPatientAmount.Value = Convert.ToDouble(Math.Round(
                            Convert.ToDecimal(total) - Convert.ToDecimal(txtPlafonAmount.Value), 2) - paid);
                    }
                }
                else
                {
                    total = TransPaymentItemIntermBills.Where(item => item.PaymentNo != string.Empty).Aggregate(total, (current, item) => current + (item.PatientAmount));
                    txtTransPatientAmount.Value = Convert.ToDouble(total);
                }
            }
        }

        #endregion

        #region Record Detail Methode Function TransPaymentItemIntermBillGuarantor

        private TransPaymentItemIntermBillGuarantorCollection TransPaymentItemIntermBillGuarantors
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["PaymentReceive:collTransPaymentItemIntermBillGuarantor" + Request.QueryString["regno"]];
                    if (obj != null)
                        return ((TransPaymentItemIntermBillGuarantorCollection)(obj));
                }

                var coll = new TransPaymentItemIntermBillGuarantorCollection();

                var query = new TransPaymentItemIntermBillGuarantorQuery("a");
                var ib = new IntermBillQuery("b");
                query.InnerJoin(ib).On(query.IntermBillNo == ib.IntermBillNo);
                query.Where(query.PaymentNo == txtPaymentNo.Text);

                query.Select
                    (
                        query,
                        ib.RegistrationNo.As("refIntermBill_RegistrationNo"),
                        ib.IntermBillDate.As("refIntermBill_IntermBillDate"),
                        ib.StartDate.As("refIntermBill_StartDate"),
                        ib.EndDate.As("refIntermBill_EndDate"),
                        (ib.PatientAmount + ib.AdministrationAmount - ib.DiscAdmPatient).As("refToIntermBill_PatientAmount"),
                        (ib.GuarantorAmount + ib.GuarantorAdministrationAmount - ib.DiscAdmGuarantor).As("refToIntermBill_GuarantorAmount")
                    );

                coll.Load(query);

                Session["PaymentReceive:collTransPaymentItemIntermBillGuarantor" + Request.QueryString["regno"]] = coll;
                return coll;
            }
            set { Session["PaymentReceive:collTransPaymentItemIntermBillGuarantor" + Request.QueryString["regno"]] = value; }
        }

        protected void grdIntermBillGuarantor_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdIntermBillGuarantor.DataSource = TransPaymentItemIntermBillGuarantors;
        }

        private void CalculateIntermBillGuarantorAmount()
        {
            decimal? total = 0;

            if (txtPlafonAmount.Value > 0)
                total = Convert.ToDecimal(txtPlafonAmount.Value);
            else
            {
                if (TransPaymentItemIntermBillGuarantors.Count > 0)
                {
                    total = TransPaymentItemIntermBillGuarantors.Where(item => item.PaymentNo != string.Empty)
                                                       .Aggregate(total,
                                                                  (current, item) =>
                                                                  current + (item.GuarantorAmount));
                }

            }
            txtTransGuarantorAmount.Value = Convert.ToDouble(total);
        }

        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (!string.IsNullOrEmpty(Request.QueryString["utype"]))
            {
                ToolBarMenuAdd.Enabled = false;

                btnSummary.Visible = false;
                btnOrderItem.Visible = false;
                btnIntermBill.Visible = false;
                btnIntermBillGuarantor.Visible = false;
                btnDownPayment.Visible = false;
            }

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"] && !(bool)ViewState["IsVoid"];
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler source, string argument)
        {
            base.RaisePostBackEvent(source, argument);

            if (string.IsNullOrEmpty(argument) || !(source is RadGrid))
                return;

            if (argument.Contains("rebind|"))  //if (argument == "rebind") <-- salah, tidak sesuai dg yg dikirim dari form sebelumnya
            {
                var val = argument.Split('|');

                grdTransPaymentItem.Rebind();

                foreach (var item in TransPaymentItems.Where(item => item.SequenceNo == val[1]))
                {
                    if (item.SRPaymentType != AppSession.Parameter.PaymentTypeCorporateAR && item.SRPaymentType != AppSession.Parameter.PaymentTypeSaldoAR)
                    {
                        txtTotalPaymentAmountPatient.Value += (double)item.Amount;
                        txtRemainingAmountPatient.Value -= (double)item.Amount - (double)(item.RoundingAmount ?? 0);
                        txtRoundingAmount.Value += (double)(item.RoundingAmount ?? 0);
                        txtDownPaymentAmount.Value = (double)item.Amount;
                    }
                    else
                    {
                        txtTotalPaymentAmountGuarantor.Value += (double)item.Amount;
                        txtRemainingAmountGuarantor.Value -= (double)item.Amount;
                    }

                    break;
                }
            }
            if (argument == "refresh")
            {
                var registration = new Registration();
                registration.LoadByPrimaryKey(txtRegistrationNo.Text);

                var guarantor = new Guarantor();
                guarantor.LoadByPrimaryKey(string.IsNullOrEmpty(txtMemberID.Text)
                                               ? txtGuarantorID.Text
                                               : txtMemberID.Text);

                string[] patientParam = new string[2],
                         regno = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);
                patientParam.SetValue(AppSession.Parameter.PaymentTypePayment, 0);
                patientParam.SetValue(AppSession.Parameter.PaymentTypePersonalAR, 1);

                var treturn = Helper.Payment.GetTotalPayment(regno, false);
                txtTotalPaymentAmountPatient.Value = (double)Helper.Payment.GetTotalPayment(regno, true, patientParam) + (double)treturn;
                txtTotalPaymentAmountGuarantor.Value =
                    (double)Helper.Payment.GetTotalPayment(regno, true, AppSession.Parameter.PaymentTypeCorporateAR) +
                    (double)Helper.Payment.GetTotalPayment(regno, true, AppSession.Parameter.PaymentTypeSaldoAR);

                var cob = new RegistrationGuarantorCollection();
                //cob.Query.Where(cob.Query.RegistrationNo == txtRegistrationNo.Text);
                //cob.LoadAll();
                var cobq = new RegistrationGuarantorQuery("a");
                var gq = new GuarantorQuery("b");
                cobq.InnerJoin(gq).On(gq.GuarantorID == cobq.GuarantorID);
                cobq.Where(cobq.RegistrationNo == txtRegistrationNo.Text);
                cob.Load(cobq);
                decimal cobPlafond = cob.Sum(c => (c.PlafondAmount ?? 0));

                decimal tpatient, tguarantor;
                Helper.CostCalculation.GetBillingTotal(regno, registration.SRBussinesMethod, (registration.PlavonAmount ?? 0) + cobPlafond, out tpatient,
                                                       out tguarantor, guarantor, registration.IsGlobalPlafond ?? false);

                var trounding = Helper.Payment.GetTotalRoundingPayment(txtPaymentNo.Text);

                var discPatient = (double)Helper.Payment.GetPaymentDiscount(regno, false);
                var discGuarantor = (double)Helper.Payment.GetPaymentDiscount(regno, true);

                txtRemainingAmountGuarantor.Value = (double)tguarantor - txtTotalPaymentAmountGuarantor.Value - discGuarantor;

                txtPlafonAmount.Value = (double)((registration.PlavonAmount ?? 0) + cobPlafond);
                txtDownPaymentAmount.Value = (double)(Helper.Payment.GetTotalDownPayment(regno) - Helper.Payment.GetTotalDownPaymentReturn(regno));
                if (btnDownPayment.Enabled)
                    btnDownPayment.Enabled = (txtDownPaymentAmount.Value > 0);

                txtDiscountAmount.Value = (double)Helper.Payment.GetTotalPaymentDiscount(regno);

                txtPaymentDiscountGuarantor.Value = discGuarantor;
                txtPaymentDiscountPatient.Value = discPatient;

                tpatient = (tpatient - (decimal)txtTotalPaymentAmountPatient.Value) - (decimal)(discPatient);

                txtRemainingAmountPatient.Value = (double)tpatient + (double)trounding;
                txtRoundingAmount.Value = (double)trounding;
            }
            if (argument == "rebindo")
            {
                if ((source as RadGrid).ID == grdOrderItem.ID)
                {
                    foreach (var item in TransPaymentItemOrders.Where(item => item.PaymentNo == string.Empty))
                    {
                        item.MarkAsDeleted();
                    }

                    CalculateOrderAmount();
                    grdOrderItem.Rebind();
                }
            }

            if (argument == "clearlist")
            {
                TransPaymentItemOrders.MarkAllAsDeleted();
                grdOrderItem.Rebind();
                CalculateOrderAmount();

                TransPaymentItems.MarkAllAsDeleted();
                grdTransPaymentItem.Rebind();
            }

            if (argument == "rebindib")
            {
                if ((source as RadGrid).ID == grdIntermBill.ID)
                {
                    foreach (var item in TransPaymentItemIntermBills.Where(item => item.PaymentNo == string.Empty))
                    {
                        item.MarkAsDeleted();
                    }

                    CalculateIntermBillAmount();
                    grdIntermBill.Rebind();
                }
            }

            if (argument.Contains("rebindibg")) //if (argument == "rebindibg") <-- salah, tidak sesuai dg yg dikirim dari form sebelumnya
            {
                if ((source as RadGrid).ID == grdIntermBillGuarantor.ID)
                {
                    foreach (
                        var item in TransPaymentItemIntermBillGuarantors.Where(item => item.PaymentNo == string.Empty))
                    {
                        item.MarkAsDeleted();
                    }

                    CalculateIntermBillGuarantorAmount();
                    grdIntermBillGuarantor.Rebind();
                }
            }

            if (argument == "clearlistib")
            {
                TransPaymentItemIntermBills.MarkAllAsDeleted();
                grdIntermBill.Rebind();
                CalculateIntermBillAmount();

                TransPaymentItems.MarkAllAsDeleted();
                grdTransPaymentItem.Rebind();
            }

            if (argument == "clearlistibg")
            {
                TransPaymentItemIntermBillGuarantors.MarkAllAsDeleted();
                grdIntermBillGuarantor.Rebind();
                CalculateIntermBillGuarantorAmount();

                TransPaymentItems.MarkAllAsDeleted();
                grdTransPaymentItem.Rebind();
            }
        }

        private void PopulateTransPaymentItemOrderGrid()
        {
            //Display Data Detail
            TransPaymentItemOrders = null; //Reset Record Detail
            grdOrderItem.DataSource = TransPaymentItemOrders; //Requery
            grdOrderItem.MasterTableView.IsItemInserted = false;
            grdOrderItem.MasterTableView.ClearEditItems();
            grdOrderItem.DataBind();
        }

        private void PopulateTransPaymentItemIntermBillGrid()
        {
            //Display Data Detail
            TransPaymentItemIntermBills = null; //Reset Record Detail
            grdIntermBill.DataSource = TransPaymentItemIntermBills; //Requery
            grdIntermBill.MasterTableView.IsItemInserted = false;
            grdIntermBill.MasterTableView.ClearEditItems();
            grdIntermBill.DataBind();
        }

        private void PopulateTransPaymentItemIntermBillGuarantorGrid()
        {
            //Display Data Detail
            TransPaymentItemIntermBillGuarantors = null; //Reset Record Detail
            grdIntermBillGuarantor.DataSource = TransPaymentItemIntermBillGuarantors; //Requery
            grdIntermBillGuarantor.MasterTableView.IsItemInserted = false;
            grdIntermBillGuarantor.MasterTableView.ClearEditItems();
            grdIntermBillGuarantor.DataBind();
        }

        public bool GetStatusOutstandingTransaction()
        {
            var query = new TransChargesItemQuery("a");
            var header = new TransChargesQuery("c");
            var pay = new TransPaymentItemOrderQuery("g");

            query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo && header.IsOrder == true &&
                                       query.IsOrderRealization == false && query.IsBillProceed == false);
            query.InnerJoin(pay).On(
                query.TransactionNo == pay.TransactionNo &&
                query.SequenceNo == pay.SequenceNo &&
                pay.IsPaymentProceed == true &&
                pay.IsPaymentReturned == false
                );
            query.Where(
                header.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text)),
                header.IsVoid == false,
                query.IsVoid == false,
                query.Or(query.ParentNo == string.Empty, query.ParentNo.IsNull())
                );

            DataTable tbl = query.LoadDataTable();

            return tbl.Rows.Count > 0;
        }

        public bool GetStatusOutstandingIntermBill()
        {
            var query = new CostCalculationQuery("a");
            var charges = new TransChargesQuery("b");
            var chargesDt = new TransChargesItemQuery("c");
            var pay = new TransPaymentItemOrderQuery("d");
            query.InnerJoin(charges).On(query.RegistrationNo == charges.RegistrationNo &&
                                        query.TransactionNo == charges.TransactionNo && charges.IsBillProceed == true);
            query.InnerJoin(chargesDt).On(query.TransactionNo == chargesDt.TransactionNo &&
                                          query.SequenceNo == chargesDt.SequenceNo);
            query.LeftJoin(pay).On(
                query.TransactionNo == pay.TransactionNo &&
                query.SequenceNo == pay.SequenceNo &&
                pay.IsPaymentProceed == true &&
                pay.IsPaymentReturned == false
                );
            query.Where(
                query.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text)),
                pay.PaymentNo.IsNull(), query.IntermBillNo.IsNull()
                );

            DataTable tbl = query.LoadDataTable();

            query = new CostCalculationQuery("a");
            var presc = new TransPrescriptionQuery("b");
            var prescDt = new TransPrescriptionItemQuery("c");
            pay = new TransPaymentItemOrderQuery("d");
            query.InnerJoin(presc).On(query.RegistrationNo == presc.RegistrationNo &&
                                      query.TransactionNo == presc.PrescriptionNo && presc.IsApproval == true &&
                                      presc.IsVoid == false);
            query.InnerJoin(prescDt).On(query.TransactionNo == prescDt.PrescriptionNo &&
                                        query.SequenceNo == prescDt.SequenceNo);
            query.LeftJoin(pay).On(
                query.TransactionNo == pay.TransactionNo &&
                query.SequenceNo == pay.SequenceNo &&
                pay.IsPaymentProceed == true &&
                pay.IsPaymentReturned == false
                );
            query.Where(
                query.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text)),
                pay.PaymentNo.IsNull(), query.IntermBillNo.IsNull()
                );

            DataTable tbl2 = query.LoadDataTable();
            tbl.Merge(tbl2);

            return tbl.Rows.Count > 0;
        }

        #region ComboBox GuarantorID

        protected void cboGuarantorID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery();
            query.Select(query.GuarantorID, query.GuarantorName);
            query.Where
                (
                    query.Or
                        (
                            query.GuarantorID.Like(searchTextContain),
                            query.GuarantorName.Like(searchTextContain)
                        )
                );
            query.es.Top = 20;

            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        #endregion

        protected void rblToGuarantor_OnTextChanged(object sender, EventArgs e)
        {
            if (AppSession.Parameter.IsUsingIntermBill)
            {
                btnIntermBill.Visible = rblToGuarantor.SelectedIndex == 1;
                btnIntermBillGuarantor.Visible = rblToGuarantor.SelectedIndex == 0;
            }
        }
        
    }
}
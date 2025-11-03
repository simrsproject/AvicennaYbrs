using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PaymentReceiveReturnDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber, _autoNumberPettyCash;
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

            if (AppSession.Parameter.IsNeedVoidReasonOnPaymentReceive)
            {
                IsUsingBeforeUnapprovalValidation = true;
            }

            switch (Request.QueryString["pc"])
            {
                case "no":
                    UrlPageList = string.IsNullOrEmpty(Request.QueryString["utype"]) ? "PaymentReceiveReturnList.aspx?pc=no" : "PaymentReceiveReturnCashierList.aspx?pc=no";
                    ProgramID = string.IsNullOrEmpty(Request.QueryString["utype"]) ? AppConstant.Program.PaymentReceiveReturn : AppConstant.Program.PaymentReceiveReturnCashier;
                    break;
                case "yes":
                    UrlPageList = "PaymentReceiveReturnList.aspx?pc=yes";
                    ProgramID = AppConstant.Program.PaymentReceiveReturnLinkToPettyCash;
                    break;
            }

            //StandardReference Initialize
            if (!IsPostBack)
            {
                if (!AppSession.Parameter.IsUsingIntermBill)
                {
                    rblPaymentDetail.Enabled = false;
                    tabStrip.Tabs[2].Visible = false;//tab Interm Bill
                }
                
                TransPaymentItems = null;
                TransPaymentItemOrders = null;
                TransPaymentItemIntermBills = null;

                trInitial.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH";

                rblPaymentDetail.SelectedIndex = 0;
                txtReferenceNo.Visible = rblPaymentDetail.SelectedIndex == 0;
                txtReferenceNoIB.Visible = rblPaymentDetail.SelectedIndex == 1;

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
            ajax.AddAjaxSetting(grdTransPaymentItem, txtReferenceNo);
            ajax.AddAjaxSetting(grdTransPaymentItem, txtReferenceNoIB);
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

         
            
            printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
            printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
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

                    args.MessageText = "You don't have authorization to edit this transaction. This data belong to: " +
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

            ViewState["IsVoid"] = false;
            ViewState["IsApproved"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new TransPayment();
            if (entity.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                TransPaymentItems.MarkAllAsDeleted();
                TransPaymentItems.Save();

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

            if (string.IsNullOrEmpty(txtReferenceNo.Text) && string.IsNullOrEmpty(txtReferenceNoIB.Text))
            {
                args.MessageText = "Reference No is required.";
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
            if (TransPaymentItems.Count == 0)
            {
                args.MessageText = "Payment detail is not defined.";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(txtReferenceNo.Text) && string.IsNullOrEmpty(txtReferenceNoIB.Text))
            {
                args.MessageText = "Reference No is required.";
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

            if (string.IsNullOrEmpty(entity.PaymentReferenceNo))
            {
                args.MessageText = "Reference No is required.";
                args.IsCancel = true;
                return;
            }

            string valMsg = DoubleReturnValidation(entity);
            if (!valMsg.Equals(string.Empty))
            {
                args.MessageText = valMsg;
                args.IsCancel = true;
                return;
            }

            // cek sudah masuk jasmed atau belum
            valMsg = ParamedicFeeTransChargesItemCompByDischargeDate.IsParamedicFeeVerified(entity.PaymentReferenceNo, true);
            if (!string.IsNullOrEmpty(valMsg))
            {
                args.MessageText = valMsg;
                args.IsCancel = true;
                return;
            }

            DateTime closingperiod = (new DateTime()).NowAtSqlServer();
            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "Yes")
            {
                //var tpRef = new TransPayment();
                //if (tpRef.LoadByPrimaryKey(entity.PaymentReferenceNo))
                //    closingperiod = tpRef.PaymentDate ?? closingperiod;
                closingperiod = entity.PaymentDate ?? closingperiod;
            }

            var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod);
            if (isClosingPeriod)
            {
                args.MessageText = "Financial statements for period: " +
                                   string.Format("{0:MMMM-yyyy}", closingperiod) +
                                   " have been closed. Please contact the authorities.";
                args.IsCancel = true;
                return;
            }

            var msg = SetApproval(entity, true, string.Empty);
            if (!string.IsNullOrEmpty(msg)) {
                args.MessageText = msg;
                args.IsCancel = true;
                return;
            }
        }
        public override bool OnGetStatusMenuUnApprovalEnabled()
        {
            return !PostingStatus.IsUnApproveDisabledIfPerClosed(txtPaymentDate.SelectedDate.Value);
        }
        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
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

            foreach (TransPaymentItemIntermBill item in TransPaymentItemIntermBills)
            {
                var ib = new IntermBill();
                if (ib.LoadByPrimaryKey(item.IntermBillNo) & ib.IsVoid == true)
                {
                    args.MessageText = "IntermBill No: " + item.IntermBillNo + " is already Void, this transaction can't be Un-Approved.";
                    args.IsCancel = true;
                    return;
                }
            }

            DateTime closingperiod = (new DateTime()).NowAtSqlServer();
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

        private string SetApproval(TransPayment entity, bool isApprove, string voidReason)
        {
            var pfpColl = new ParamedicFeeTransPaymentCollection();
            pfpColl.Query.Where(pfpColl.Query.PaymentRefNo == entity.PaymentReferenceNo);
            pfpColl.LoadAll();

            entity.IsApproved = isApprove;
            entity.IsVoid = !isApprove;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            if (isApprove)
            {
                entity.ApproveByUserID = AppSession.UserLogin.UserID;
                entity.ApproveDate = (new DateTime()).NowAtSqlServer();

                // kalau sudah dibayar ke dokter feenya, maka gak boleh retur payment
                if (pfpColl.Where(a => !string.IsNullOrEmpty(a.PaymentGroupNo) && a.IsVoid == false).Any()) {
                    return "Related physician fee have already been paid, this payment can not be returned";
                }

                // tidak diperlukan lagi karena sudah include di fee.UnSetPayment dibawah
                //foreach (var pfp in pfpColl) {
                //    pfp.IsVoid = true;
                //    pfp.VoidByUserID = AppSession.UserLogin.UserID;
                //    pfp.VoidDateTime = entity.ApproveDate;
                //    pfp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                //    pfp.LastUpdateDateTime = entity.ApproveDate;
                //}
            }
            else
            {
                entity.VoidByUserID = AppSession.UserLogin.UserID;
                entity.VoidDate = (new DateTime()).NowAtSqlServer();
                entity.VoidReason = voidReason;

                // tidak diperlukan lagi karena sudah include di fee.SetPayment dibawah
                //foreach (var pfp in pfpColl)
                //{
                //    pfp.IsVoid = false;
                //    pfp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                //    pfp.LastUpdateDateTime = entity.VoidDate;
                //}
            }

            foreach (TransPaymentItemOrder item in TransPaymentItemOrders)
            {
                item.IsPaymentReturned = isApprove;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (TransPaymentItemIntermBill item in TransPaymentItemIntermBills)
            {
                item.IsPaymentReturned = isApprove;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            var registration = new Registration();
            registration.LoadByPrimaryKey(txtRegistrationNo.Text);

            var errMsg = new ErrorMessage();// untuk log, cari approve payment return tidak update paid jasmed
            string eMsg = string.Format("App: {0}, PaymentReturnNo: {1} PaymentRef",Common.ApplicationSettings.ApplicationInfo.Default, entity.PaymentNo);

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                TransPaymentItemOrders.Save();
                TransPaymentItemIntermBills.Save();

                if (AppSession.Parameter.IsJobOrderRealizationNeedConfirm)
                {
                    if (isApprove)
                    {
                        eMsg += ", Payment Return Approve";
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
                    else
                    {
                        eMsg += ", Payment Return Unapprove";
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
                }

                #region Membership - Update Reward Point
                var totPatientPayment = TransPaymentItems.Where(item => item.SRPaymentType == AppSession.Parameter.PaymentTypePayment).Sum(item => (item.Amount ?? 0));
                if (registration.MembershipDetailID != -1)
                {
                    var div = AppSession.Parameter.MultipleForRewardPoints;
                    var x = BusinessObject.MembershipDetail.UpdateRewardPoints(Convert.ToInt64(registration.MembershipDetailID), totPatientPayment, div, !isApprove, AppSession.UserLogin.UserID);
                }
                if (!string.IsNullOrEmpty(registration.MembershipNo))
                {
                    var div = AppSession.Parameter.MultipleForRewardPointsForEmployee;
                    var x = BusinessObject.MembershipDetail.UpdateEmployeeRewardPoints(registration.MembershipNo, registration.RegistrationNo, totPatientPayment, div, !isApprove, AppSession.UserLogin.UserID);
                }
                #endregion

                #region Jasmed
                TransPayment tpRef = new TransPayment();
                tpRef.LoadByPrimaryKey(entity.PaymentReferenceNo);
                eMsg = eMsg.Replace("PaymentRef", "PaymentRef " + tpRef.PaymentNo);

                if (isApprove)
                {
                    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    feeColl.UnSetPayment(tpRef, AppSession.UserLogin.UserID);
                    feeColl.Save();

                    eMsg += ", " + string.Join(",", feeColl.Select(f => f.TransactionNo).Distinct().ToArray());

                    //pfpColl.Save();
                }
                else {
                    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    feeColl.SetPayment(tpRef, 0, AppSession.UserLogin.UserID);
                    feeColl.Save();

                    eMsg += ", " + string.Join(",", feeColl.Select(f => f.TransactionNo).Distinct().ToArray());

                    //pfpColl.Save();
                }

                #endregion

                //return String.Empty; // jangan commit dulu, lagi debug

                errMsg.Message = eMsg;
                errMsg.CreatedBy = AppSession.UserLogin.UserID;
                errMsg.CreatedDateTime = DateTime.Now;
                errMsg.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            /* Automatic Journal Testing Start */
            if (isApprove)
            {
                // function ini utk retur payment biasa
                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "Yes")
                {
                    int? journalId =
                        JournalTransactions.AddNewPaymentCorrectionJournalCashBased(
                            BusinessObject.JournalType.PaymentReturn, entity, registration, this.TransPaymentItems,
                            "TP", entity.PaymentReferenceNo, AppSession.UserLogin.UserID, 0);
                }
                else
                {
                    int? journalId =
                        JournalTransactions.AddNewPaymentCorrectionJournal(
                            BusinessObject.JournalType.PaymentReturn, entity, registration, this.TransPaymentItems,
                            "TP", AppSession.UserLogin.UserID, 0);
                }

                //if (AppSession.Parameter.IsPhysicianFeeBasedOnPayment == "Yes")
                //{
                //    int? x = ParamedicFeeTransChargesItemCompSettled.AddReturnSettled(entity, AppSession.UserLogin.UserID);
                //}
            }
            else
            {
                // function ini utk retur payment biasa
                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "Yes")
                {
                    int? journalId =
                        JournalTransactions.AddNewPaymentCorrectionJournalVoidCashBased(
                        entity, "TP", AppSession.UserLogin.UserID, 0);
                }
                else
                {

                    int? journalId =
                        JournalTransactions.AddNewPaymentJournal(BusinessObject.JournalType.PaymentReturn, entity,
                                                                 registration, this.TransPaymentItems, "TP",
                                                                 AppSession.UserLogin.UserID, 0);
                }
                //if (AppSession.Parameter.IsPhysicianFeeBasedOnPayment == "Yes")
                //{
                //    int? x = ParamedicFeeTransChargesItemCompSettled.DeleteSettled(entity, true);
                //}
            }
            /* Automatic Journal Testing End */

            return string.Empty;
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
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
            var entity = new TransPayment();
            if (!entity.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (entity.CreatedBy != AppSession.UserLogin.UserID && !_isPowerUser)
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

        private void SetVoid(TransPayment entity, bool isVoid)
        {
            entity.IsVoid = isVoid;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            //TransPaymentItemOrders.MarkAllAsDeleted();

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                //TransPaymentItemOrders.Save();

                trans.Complete();
            }
        }

        private static string DoubleReturnValidation(TransPayment entity)
        {
            var returned = new TransPaymentQuery();
            returned.Where(returned.PaymentNo != entity.PaymentNo, returned.PaymentReferenceNo == entity.PaymentReferenceNo, returned.IsApproved == true, returned.TransactionCode == "017");
            returned.Select(returned.PaymentNo);
            DataTable dtb = returned.LoadDataTable();
            if (dtb.Rows.Count > 0)
                return "Payment Receive with no# " + entity.PaymentReferenceNo + " has been returned with no# " + dtb.Rows[0]["PaymentNo"].ToString();

            return string.Empty;
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
                txtReferenceNo.Text = transPayment.PaymentReferenceNo;
                txtReferenceNoIB.Text = transPayment.PaymentReferenceNo;

                PopulateTransPaymentItemGrid();
                PopulateTransPaymentItemOrder();
                PopulateTransPaymentItemIntermBill();

                CalculateOrderAmount();
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
            entity.TransactionCode = TransactionCode.PaymentReturn;
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.PaymentDate = txtPaymentDate.SelectedDate;
            entity.PaymentTime = txtPaymentTime.Text;
            entity.PrintReceiptAsName = txtPrintReceiptAsName.Text;
            entity.PaymentReferenceNo = txtReferenceNo.Text;
            entity.TotalPaymentAmount = 0;
            entity.RemainingAmount = 0;
            entity.PrintNumber = 0;
            entity.IsVoid = false;
            entity.IsApproved = false;
            entity.Notes = txtNotes.Text;
            entity.Initial = txtInitial.Text;

            var pReff = new TransPayment();
            entity.GuarantorID = pReff.LoadByPrimaryKey(txtReferenceNo.Text) ? pReff.GuarantorID : txtGuarantorID.Text;

            entity.IsVisiteDownPayment = pReff.IsVisiteDownPayment ?? false;

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
                    que.TransactionCode == TransactionCode.PaymentReturn
                    );
                que.OrderBy(que.PaymentNo.Ascending);
            }
            else
            {
                que.Where
                    (
                    que.PaymentNo < txtPaymentNo.Text,
                    que.TransactionCode == TransactionCode.PaymentReturn
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
                    object obj = Session["PaymentReceiveReturnItems" + Request.UserHostName];
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
                    srQuery.PaymentMethodName.As("refToAppStandardReferenceItem_PaymentMethod"),
                    query.Amount
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

                Session["PaymentReceiveReturnItems" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["PaymentReceiveReturnItems" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemTransPaymentItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = false;//(newVal != DataMode.Read);
            grdTransPaymentItem.Columns[0].Visible = isVisible;
            grdTransPaymentItem.Columns[grdTransPaymentItem.Columns.Count - 1].Visible = isVisible;

            grdTransPaymentItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdTransPaymentItem.Rebind();
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

            //Stay in insert mode
            e.Canceled = true;
            grdTransPaymentItem.Rebind();
        }

        private TransPaymentItem FindTransPaymentItem(String sequenceNo)
        {
            TransPaymentItemCollection coll = TransPaymentItems;
            TransPaymentItem retEntity = null;
            foreach (TransPaymentItem rec in coll)
            {
                if (rec.SequenceNo.Equals(sequenceNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(TransPaymentItem entity, GridCommandEventArgs e)
        {
            var userControl = (PaymentReceiveReturnItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
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
                entity.IsFromDownPayment = false;
                entity.CardNo = userControl.CardNo;
            }
        }

        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (!string.IsNullOrEmpty(Request.QueryString["utype"]))
            {
                ToolBarMenuAdd.Enabled = false;

                rblPaymentDetail.Enabled = false;
                txtReferenceNo.ReadOnly = true;
                txtReferenceNoIB.ReadOnly = true;
            }

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"];
        }

        #region Record Detail Methode Function TransPaymentItemOrder

        private TransPaymentItemOrderCollection TransPaymentItemOrders
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["PaymentReceiveReturnItemOrders" + Request.UserHostName];
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
                query.Where(query.PaymentNo == txtReferenceNo.Text);

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

                Session["PaymentReceiveReturnItemOrders" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["PaymentReceiveReturnItemOrders" + Request.UserHostName] = value; }
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

        private void PopulateTransPaymentItemIntermBill()
        {
            //Display Data Detail
            TransPaymentItemIntermBills = null; //Reset Record Detail
            grdIntermBill.DataSource = TransPaymentItemIntermBills;
            grdIntermBill.MasterTableView.IsItemInserted = false;
            grdIntermBill.MasterTableView.ClearEditItems();
            grdIntermBill.DataBind();
        }

        protected void grdOrderItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdOrderItem.DataSource = TransPaymentItemOrders;
        }

        #endregion

        #region Record Detail Methode Function TransPaymentItemIntermBill

        private TransPaymentItemIntermBillCollection TransPaymentItemIntermBills
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["PaymentReceiveReturnItemIntermBills" + Request.UserHostName];
                    if (obj != null)
                        return ((TransPaymentItemIntermBillCollection)(obj));
                }

                var coll = new TransPaymentItemIntermBillCollection();

                var query = new TransPaymentItemIntermBillQuery("a");
                var ib = new IntermBillQuery("b");
                query.InnerJoin(ib).On(query.IntermBillNo == ib.IntermBillNo);
                query.Where(query.PaymentNo == txtReferenceNo.Text);

                query.Select
                    (
                        query,
                        ib.RegistrationNo.As("refIntermBill_RegistrationNo"),
                        ib.IntermBillDate.As("refIntermBill_IntermBillDate"),
                        ib.StartDate.As("refIntermBill_StartDate"),
                        ib.EndDate.As("refIntermBill_EndDate"),
                        ib.PatientAmount.As("refToIntermBill_PatientAmount")
                    );

                coll.Load(query);

                Session["PaymentReceiveReturnItemIntermBills" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["PaymentReceiveReturnItemIntermBills" + Request.UserHostName] = value; }
        }

        protected void grdIntermBill_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdIntermBill.DataSource = TransPaymentItemIntermBills;
        }

        #endregion

        private void CalculateOrderAmount()
        {
            decimal? total = 0;
            decimal? total2 = 0;

            total = TransPaymentItems.Aggregate(total, (current, item) => current - (item.Amount));

            //if (TransPaymentItemOrders.Any())
            //    total = TransPaymentItemOrders.Aggregate(total, (current, item) => current - (item.Qty * item.Price));
            //if (TransPaymentItemIntermBills.Any())
            //    total2 += TransPaymentItemIntermBills.Where(item => item.PaymentNo != string.Empty).Aggregate(total2, (current, item) => current + (item.PatientAmount));

            txtOrderAmount.Value = Convert.ToDouble(total + total2);
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument.Contains("rebind"))
            {
                var val = eventArgument.Split('|');

                grdTransPaymentItem.Rebind();
                grdOrderItem.Rebind();
                txtReferenceNo.Text = val[1];
                txtReferenceNoIB.Text = val[1];
                CalculateOrderAmount();
                txtReferenceNo.Enabled = !(TransPaymentItems.Count > 0);
                txtReferenceNo.ShowButton = !(TransPaymentItems.Count > 0);
                txtReferenceNoIB.Enabled = !(TransPaymentItems.Count > 0);
                txtReferenceNoIB.ShowButton = !(TransPaymentItems.Count > 0);
            }
            if (eventArgument.Contains("intermbill"))
            {
                var val = eventArgument.Split('|');

                grdTransPaymentItem.Rebind();
                grdIntermBill.Rebind();
                txtReferenceNo.Text = val[1];
                txtReferenceNoIB.Text = val[1];
                CalculateOrderAmount();
                txtReferenceNo.Enabled = !(TransPaymentItems.Count > 0);
                txtReferenceNo.ShowButton = !(TransPaymentItems.Count > 0);
                txtReferenceNoIB.Enabled = !(TransPaymentItems.Count > 0);
                txtReferenceNoIB.ShowButton = !(TransPaymentItems.Count > 0);
            }
        }

        protected void rblPaymentDetail_OnTextChanged(object sender, EventArgs e)
        {
            txtReferenceNo.Visible = rblPaymentDetail.SelectedIndex == 0;
            txtReferenceNoIB.Visible = rblPaymentDetail.SelectedIndex == 1;
        }
    }
}

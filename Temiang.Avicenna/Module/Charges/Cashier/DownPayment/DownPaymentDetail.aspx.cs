using System;
using System.Data;
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
    public partial class DownPaymentDetail : BasePageDetail
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
            WindowSearch.Height = 300;

            if (AppSession.Parameter.IsNeedVoidReasonOnPaymentReceive)
            {
                IsUsingBeforeUnapprovalValidation = true;
            }

            if (string.IsNullOrEmpty(Request.QueryString["utype"]))
            {
                UrlPageList = "DownPaymentList.aspx";
                ProgramID = AppConstant.Program.DownPayment;
            }
            else
            {
                UrlPageList = "DownPaymentCashierList.aspx";
                ProgramID = AppConstant.Program.DownPaymentCashier;
            }

            Session["ProgramID"] = ProgramID;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                TransPaymentItems = null;

                trInitial.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH";

                // bikin error javascript, gak bisa klik menu utama
                //var cstext1 = new StringBuilder();
                //cstext1.Append("<script type=text/javascript> __doPostBack('ctl00$ContentPlaceHolder1$grdTransPaymentItem$ctl00$ctl02$ctl00$AddNewRecordButton','') </script>");
                //Page.ClientScript.RegisterStartupScript(GetType(), "OpenAddNewRecordGrid", cstext1.ToString());


                //db:20230724 --> dipindah ke OnLoadComplete()
                //btnOrderItem.Enabled = !string.IsNullOrEmpty(Request.QueryString["regno"]);

                if (string.IsNullOrEmpty(Request.QueryString["regno"])) {
                    rfvServiceUnit.Enabled = false;
                    rfvRegistrationNo.Enabled = false;
                }

                txtPaymentDate.DateInput.ReadOnly = !AppSession.Parameter.IsPaymentReceiveAllowBackdated;
                txtPaymentDate.DatePopupButton.Enabled = !txtPaymentDate.DateInput.ReadOnly;
                txtPaymentTime.ReadOnly = txtPaymentDate.DateInput.ReadOnly;
            }

            _isPowerUser = this.IsPowerUser || AppSession.Parameter.IsBypassCashierAuthorization;

            AjaxManager.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(AjaxManager_AjaxRequest);
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdTransPaymentItem, grdTransPaymentItem);
         
            if (string.IsNullOrEmpty(Request.QueryString["regno"]))
            {
                ajax.AddAjaxSetting(grdVisiteItem, grdVisiteItem);
                ajax.AddAjaxSetting(grdVisiteItem, txtOrderAmount);

                ajax.AddAjaxSetting(AjaxManager, grdVisiteItem);
                ajax.AddAjaxSetting(AjaxManager, txtOrderAmount);
            }
        }

        private void AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            PopulateFromPickList();
        }

        private void PopulateFromPickList()
        {
            Session.Remove("DownPaymentItemVisite:Selection");

            object obj = Session["DownPaymentItemVisite:ItemSelected"];
            if (obj == null) return;

            //delete previouse item
            if (TransPaymentItemVisites.Count > 0)
                TransPaymentItemVisites.MarkAllAsDeleted();

            DataTable dtbSelectedItem = (DataTable)obj;
            if (dtbSelectedItem.Rows.Count == 0) return;

            var guarantorID = AppSession.Parameter.SelfGuarantor;
            int i = 0;
            foreach (DataRow row in dtbSelectedItem.Rows)
            {
                if (Convert.ToDecimal(row["Qty"]) < 1) continue;
                i++;

                TransPaymentItemVisite entity = TransPaymentItemVisites.AddNew();

                entity.ServiceUnitID = row["ServiceUnitID"].ToString();
                entity.ServiceUnitName = row["ServiceUnitName"].ToString();
                entity.ItemID = row["ItemID"].ToString();
                entity.ItemName = row["ItemName"].ToString();
                entity.VisiteQty = Convert.ToInt32(row["Qty"]);

                var date = txtPaymentDate.SelectedDate;
                ItemTariff tariff = (Helper.Tariff.GetItemTariff(date.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.OutPatientClassID, AppSession.Parameter.OutPatientClassID, entity.ItemID, guarantorID, false, AppConstant.RegistrationType.OutPatient) ??
                    Helper.Tariff.GetItemTariff(date.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, AppSession.Parameter.OutPatientClassID, entity.ItemID, guarantorID, false, AppConstant.RegistrationType.OutPatient));

                if (tariff != null)
                    entity.Price = Convert.ToDecimal(tariff.Price);
                else
                    entity.Price = 0;
                entity.Discount = 0;
            }
            grdVisiteItem.DataSource = TransPaymentItemVisites;
            grdVisiteItem.DataBind();

            CalculateTransPaymentItemVisite();

            //Remove session
            Session.Remove("DownPaymentItemVisite:ItemSelected");
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
                case AppConstant.Report.DownPaymentReceiptSlip:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;

                case AppConstant.Report.DownPaymentReceiptDetailOutPatient:
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
            if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                txtServiceUnitID.Text = reg.ServiceUnitID;
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(txtServiceUnitID.Text);
                lblServiceUnitName.Text = unit.ServiceUnitName;

                txtGuarantorID.Text = reg.GuarantorID;
                var guarantor = new Guarantor();
                guarantor.LoadByPrimaryKey(txtGuarantorID.Text);
                lblGuarantorName.Text = guarantor.GuarantorName;

                var cl = new Class();
                cl.LoadByPrimaryKey(reg.ClassID);
                txtNotes.Text = "PEMBAYARAN DEPOSIT " + lblServiceUnitName.Text.ToUpper() + " (" + cl.ClassName.ToUpper() + ")";
            }
            else {

            }

            var patient = new Patient();
            patient.LoadByPrimaryKey(reg.PatientID ?? Request.QueryString["patid"]);

            txtMedicalNo.Text = patient.MedicalNo;
            txtPatientName.Text = patient.PatientName;
            txtPrintReceiptAsName.Text = patient.PatientName;
            var std = new AppStandardReferenceItem();
            txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;

            txtOrderAmount.Value = 0;
            
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
                args.MessageText = "Down payment detail is not defined.";
                args.IsCancel = true;
                return;
            }

            if (txtOrderAmount.Value > 0)
            {
                decimal totPayment = TransPaymentItems.Sum(item => Convert.ToDecimal(item.Amount));
                if (totPayment != Convert.ToDecimal(txtOrderAmount.Value))
                {
                    args.MessageText = "Total down payment amount can't be more/less than total transaction.";
                    args.IsCancel = true;
                    return;
                }
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
                args.MessageText = "Down payment detail is not defined.";
                args.IsCancel = true;
                return;
            }

            if (txtOrderAmount.Value > 0)
            {
                decimal totPayment = TransPaymentItems.Sum(item => Convert.ToDecimal(item.Amount));
                if (totPayment != Convert.ToDecimal(txtOrderAmount.Value))
                {
                    args.MessageText = "Total down payment amount can't be more/less than total transaction.";
                    args.IsCancel = true;
                    return;
                }
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

            if (TransPaymentItems.Count == 0)
            {
                args.MessageText = "Down payment detail is not defined.";
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

            var coll = new TransPaymentCollection();
            coll.Query.Where(coll.Query.PaymentNo == entity.PaymentReferenceNo, coll.Query.IsApproved == true);
            coll.LoadAll();
            if (coll.Count > 0)
            {
                args.MessageText = "Can't be Unapproved, this Down Payment has already used for payment";
                args.IsCancel = true;
                return;
            }

            var tpiq = new TransPaymentItemQuery("a");
            var tpq = new TransPaymentQuery("b");
            tpiq.InnerJoin(tpq).On(tpiq.PaymentNo == tpq.PaymentNo &&
                                   tpq.TransactionCode == TransactionCode.DownPaymentReturn && tpq.IsVoid == false);
            tpiq.Where(tpiq.ReferenceNo == entity.PaymentNo);
            DataTable tpidtb = tpiq.LoadDataTable();
            if (tpidtb.Rows.Count > 0)
            {
                args.MessageText = "Can't be Unapproved, this Down Payment has been returned";
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
            else
            {
                entity.VoidByUserID = AppSession.UserLogin.UserID;
                entity.VoidDate = (new DateTime()).NowAtSqlServer();
                entity.VoidReason = voidReason;
            }

            if (!isApprove)
            {
                TransPaymentItemOrders.MarkAllAsDeleted();
                TransPaymentItemVisites.MarkAllAsDeleted();
                TransPaymentItemVisites.Save();
            }

            var registration = new Registration();
            registration.LoadByPrimaryKey(txtRegistrationNo.Text);

            using (var trans = new esTransactionScope())
            {
                entity.Save();

                foreach (var item in TransPaymentItems)
                {
                    if (string.IsNullOrEmpty(item.ReferenceNo) && string.IsNullOrEmpty(item.ReferenceSequenceNo)) continue;
                    var deposit = new TransPaymentPatient();
                    if (deposit.LoadByPrimaryKey(item.ReferenceNo))
                    {
                        if (isApprove) deposit.ReferenceNo = entity.PaymentNo;
                        else deposit.str.ReferenceNo = string.Empty;
                        deposit.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        deposit.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        deposit.Save();
                    }
                }

                TransPaymentItemOrders.Save();

                if (chkIsVisiteDownPayment.Checked && (TransPaymentItemVisites.Count == 0))
                {
                    var pat = new Patient();
                    pat.LoadByPrimaryKey(registration.PatientID);
                    pat.PackageBalance = isApprove ? (pat.PackageBalance ?? 0) + TransPaymentItems.Sum(s => s.Amount)
                                                   : (pat.PackageBalance ?? 0) - TransPaymentItems.Sum(s => s.Amount);
                    pat.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    pat.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    pat.Save();
                }

                /* Automatic Journal Testing Start */
                if (isApprove)
                {
                    int? journalId = JournalTransactions.AddNewPaymentJournal(BusinessObject.JournalType.DownPayment, entity, registration, this.TransPaymentItems, "DP", entity.CreatedBy, 0);
                    /* Automatic Journal Testing End */
                }
                else
                {
                    int? journalId = JournalTransactions.AddNewPaymentCorrectionJournal(BusinessObject.JournalType.DownPayment, entity, registration, this.TransPaymentItems, "DP", entity.CreatedBy, 0);
                }

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
            var tpiv = new TransPaymentItemVisiteCollection();
            if (entity.IsVisiteDownPayment == true)
            {
                tpiv.Query.Where(tpiv.Query.PaymentNo == entity.PaymentNo);
                tpiv.Query.Load();
                tpiv.MarkAllAsDeleted();

            }

            entity.IsVoid = isVoid;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (var trans = new esTransactionScope())
            {
                tpiv.Save();

                foreach (var item in TransPaymentItems)
                {
                    if (string.IsNullOrEmpty(item.ReferenceNo) && string.IsNullOrEmpty(item.ReferenceSequenceNo)) continue;
                    var deposit = new TransPaymentPatient();
                    if (deposit.LoadByPrimaryKey(item.ReferenceNo))
                    {
                        deposit.str.ReferenceNo = string.Empty;
                        deposit.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        deposit.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        deposit.Save();
                    }
                }

                entity.Save();

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
            //RefreshCommandItemTransPaymentItemVisite(oldVal, newVal);
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
                if (registration.LoadByPrimaryKey(txtRegistrationNo.Text))
                {
                    txtServiceUnitID.Text = registration.ServiceUnitID;
                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(txtServiceUnitID.Text);
                    lblServiceUnitName.Text = unit.ServiceUnitName;

                    txtGuarantorID.Text = registration.GuarantorID;
                    var guarantor = new Guarantor();
                    guarantor.LoadByPrimaryKey(txtGuarantorID.Text);
                    lblGuarantorName.Text = guarantor.GuarantorName;
                }
                else {
                    txtServiceUnitID.Text = string.Empty;
                    lblServiceUnitName.Text = string.Empty;
                    txtGuarantorID.Text = string.Empty;
                    lblGuarantorName.Text = string.Empty;
                }
                
                var patient = new Patient();
                //if (patient.LoadByPrimaryKey((!string.IsNullOrEmpty(transPayment.PatientID) ? transPayment.PatientID : (registration.PatientID ?? Request.QueryString["patid"]))))
                if (patient.LoadByPrimaryKey((!string.IsNullOrEmpty(transPayment.PatientID) ? transPayment.PatientID : Request.QueryString["patid"])))
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
                

                txtPrintReceiptAsName.Text = transPayment.PrintReceiptAsName;

                txtPaymentDate.SelectedDate = transPayment.PaymentDate;
                txtPaymentTime.Text = transPayment.PaymentTime;

                ViewState["IsVoid"] = transPayment.IsVoid ?? false;
                ViewState["IsApproved"] = transPayment.IsApproved ?? false;

                txtNotes.Text = transPayment.Notes;
                txtInitial.Text = transPayment.Initial;

                chkIsVisiteDownPayment.Checked = transPayment.IsVisiteDownPayment ?? false;
                tabStrip.Tabs[1].Enabled = chkIsVisiteDownPayment.Checked;

                PopulateTransPaymentItemGrid();
                PopulateTransPaymentItemVisiteGrid();
                PopulateTransPaymentItemOrder();

                if (!chkIsVisiteDownPayment.Checked)
                    CalculateOrderAmount();
                else
                    CalculateTransPaymentItemVisite();
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

                entity.PaymentNo = txtPaymentNo.Text;
                entity.TransactionCode = TransactionCode.DownPayment;
                entity.RegistrationNo = txtRegistrationNo.Text;
                entity.PaymentDate = txtPaymentDate.SelectedDate;
                entity.PaymentTime = txtPaymentTime.Text;
                entity.PrintReceiptAsName = txtPrintReceiptAsName.Text;
                entity.TotalPaymentAmount = 0;
                entity.RemainingAmount = 0;
                entity.PrintNumber = 0;
                entity.IsVoid = false;
                entity.IsApproved = false;
                entity.PatientID = Request.QueryString["patid"];
            }
            
            entity.Notes = txtNotes.Text;
            entity.Initial = txtInitial.Text;
            entity.IsVisiteDownPayment = chkIsVisiteDownPayment.Checked;
            entity.GuarantorID = txtGuarantorID.Text;
           
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

            foreach (var item in TransPaymentItemVisites)
            {
                item.PaymentNo = entity.PaymentNo;
                item.PatientID = entity.PatientID;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            foreach (var item in TransPaymentItemOrders)
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

                if (!(entity.IsVisiteDownPayment ?? false))
                    TransPaymentItemVisites.MarkAllAsDeleted();

                TransPaymentItemVisites.Save();
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
                    que.TransactionCode == TransactionCode.DownPayment
                    );
                que.OrderBy(que.PaymentNo.Ascending);
            }
            else
            {
                que.Where
                    (
                    que.PaymentNo < txtPaymentNo.Text,
                    que.TransactionCode == TransactionCode.DownPayment
                    );
                que.OrderBy(que.PaymentNo.Descending);
            }

            var entity = new TransPayment();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        private string GetNewPaymentNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer(), AppEnum.AutoNumber.DownPaymentNo);
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
                    object obj = Session["DownPayment:collTransPaymentItem" + Request.UserHostName];
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

                Session["DownPayment:collTransPaymentItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["DownPayment:collTransPaymentItem" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemTransPaymentItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdTransPaymentItem.Columns[0].Visible = isVisible;
            grdTransPaymentItem.Columns[grdTransPaymentItem.Columns.Count - 1].Visible = isVisible;

            grdVisiteItem.Columns[0].Visible = isVisible;
            grdVisiteItem.Columns[grdVisiteItem.Columns.Count - 1].Visible = isVisible;

            grdTransPaymentItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            grdVisiteItem.MasterTableView.CommandItemDisplay = string.IsNullOrEmpty(Request.QueryString["utype"]) && isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            grdOrderItem.MasterTableView.CommandItemDisplay = string.IsNullOrEmpty(Request.QueryString["utype"]) && isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            if (isVisible)
            {
                grdTransPaymentItem.DataSource = null;
                grdTransPaymentItem.Rebind();

                grdVisiteItem.DataSource = null;
                grdVisiteItem.Rebind();

                grdOrderItem.DataSource = null;
                grdOrderItem.Rebind();
            }

            //db:20230724 --> transaction list u/ dp dg noreg
            //btnOrderItem.Enabled = !string.IsNullOrEmpty(Request.QueryString["regno"]) && isVisible;
            btnOrderItem.Visible = !string.IsNullOrEmpty(Request.QueryString["regno"]) && isVisible;
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
            return coll.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        private void SetEntityValue(TransPaymentItem entity, GridCommandEventArgs e)
        {
            var userControl = (ItemDownPayment)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
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

            //db: 20230724
            //- flag visit down payment hanya u/ dp tanpa noreg
            //- transaction list hanya u/ dp dg noreg --> ini tujuannya nanti sebenarnya buat cetakan dp per tindakan (peninggalan rssa)

            if (!string.IsNullOrEmpty(Request.QueryString["utype"])) // --> menu cashier
            {
                ToolBarMenuAdd.Enabled = false;

                chkIsVisiteDownPayment.Enabled = false;
                btnOrderItem.Visible = false;
            }
            else 
                btnOrderItem.Visible = !string.IsNullOrEmpty(Request.QueryString["regno"]);

            trVisiteDownPayment.Visible = string.IsNullOrEmpty(Request.QueryString["regno"]);
            tabStrip.Tabs[1].Visible = string.IsNullOrEmpty(Request.QueryString["regno"]);
            tabStrip.Tabs[2].Visible = !string.IsNullOrEmpty(Request.QueryString["regno"]);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"];
        }

        #region Record Detail Methode Fucntion TransPaymentItemVisites
        private TransPaymentItemVisiteCollection TransPaymentItemVisites
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["DownPayment:TransPaymentItemVisite" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((TransPaymentItemVisiteCollection)(obj));
                    }
                }

                var coll = new TransPaymentItemVisiteCollection();

                var query = new TransPaymentItemVisiteQuery("a");
                var item = new ItemQuery("b");
                var unit = new ServiceUnitQuery("c");

                query.Select(
                    query,
                    item.ItemName.As("refToItem_ItemName"),
                    unit.ServiceUnitName.As("refToServiceUnit_ServiceUnitName")
                    );
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.Where(query.PaymentNo == txtPaymentNo.Text);

                coll.Load(query);

                Session["DownPayment:TransPaymentItemVisite" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["DownPayment:TransPaymentItemVisite" + Request.UserHostName] = value; }
        }

        //private void RefreshCommandItemTransPaymentItemVisite(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        //{
        //    //Toogle grid command
        //    bool isVisible = (newVal != AppEnum.DataMode.Read) && string.IsNullOrEmpty(Request.QueryString["utype"]);
        //    grdVisiteItem.Columns[0].Visible = isVisible;
        //    grdVisiteItem.Columns[grdVisiteItem.Columns.Count - 1].Visible = isVisible;

        //    grdVisiteItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

        //    if (oldVal != AppEnum.DataMode.Read)
        //        TransPaymentItemVisites = null;

        //    //Perbaharui tampilan dan data
        //    if (IsPostBack)
        //        grdVisiteItem.Rebind();
        //}

        private void PopulateTransPaymentItemVisiteGrid()
        {
            //Display Data Detail
            TransPaymentItemVisites = null; //Reset Record Detail
            grdVisiteItem.DataSource = TransPaymentItemVisites; //Requery
            grdVisiteItem.MasterTableView.IsItemInserted = false;
            grdVisiteItem.MasterTableView.ClearEditItems();
            grdVisiteItem.DataBind();
        }

        protected void chkIsVisiteDownPayment_CheckedChanged(object sender, EventArgs e)
        {
            tabStrip.Tabs[1].Enabled = chkIsVisiteDownPayment.Checked;
            if (!tabStrip.Tabs[1].Enabled)
            {
                tabStrip.Tabs[0].Selected = true;
                multiPage.SelectedIndex = 0;
            }
            else
            {
                tabStrip.Tabs[1].Selected = true;
                multiPage.SelectedIndex = 1;
            }
        }

        protected void grdVisiteItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdVisiteItem.DataSource = TransPaymentItemVisites;
        }

        protected void grdVisiteItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            var entity = FindTransPaymentItemVisite((string)editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                                                  [TransPaymentItemVisiteMetadata.ColumnNames.ItemID]);

            if (entity != null)
                SetEntityValue(entity, e);

            CalculateTransPaymentItemVisite();
        }

        protected void grdVisiteItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            String itemID = item.OwnerTableView.DataKeyValues[item.ItemIndex][TransPaymentItemVisiteMetadata.ColumnNames.ItemID].ToString();
            var entity = FindTransPaymentItemVisite(itemID);

            if (entity != null)
                entity.MarkAsDeleted();

            CalculateTransPaymentItemVisite();
        }

        protected void grdVisiteItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = TransPaymentItemVisites.AddNew();

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdVisiteItem.Rebind();

            CalculateTransPaymentItemVisite();
        }

        private TransPaymentItemVisite FindTransPaymentItemVisite(String itemID)
        {
            TransPaymentItemVisiteCollection coll = TransPaymentItemVisites;
            return coll.FirstOrDefault(rec => rec.ItemID.Equals(itemID));
        }

        private void SetEntityValue(TransPaymentItemVisite entity, GridCommandEventArgs e)
        {
            var userControl = (ItemDownPaymentVisite)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ServiceUnitID = userControl.ServiceUnitID;
                entity.ServiceUnitName = userControl.ServiceUnitName;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.VisiteQty = userControl.Qty;
                entity.Price = userControl.Price;
                entity.Discount = userControl.Discount;
            }
        }

        private void CalculateTransPaymentItemVisite()
        {
            if (TransPaymentItemVisites.Count > 0)
            {
                decimal? total = 0;
                
                foreach (TransPaymentItemVisite item in TransPaymentItemVisites)
                {
                    total += (item.VisiteQty * (item.Price - (item.Price * item.Discount / 100)));
                }

                txtOrderAmount.Value = Convert.ToDouble(total);
            }
        }

        #endregion

        #region Record Detail Methode Function TransPaymentItemOrder
        private TransPaymentItemOrderCollection TransPaymentItemOrders
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["DownPayment:TransPaymentItemOrder" + Request.UserHostName];
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

                var total = new esQueryItem(query, "Total", esSystemType.Decimal);
                total = query.Qty * query.Price;

                query.Select
                    (
                        query,
                        item.ItemName.As("refToItem_ItemName"),
                        su.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"),
                        header.TransactionDate.As("refTransCharges_TransactionDate"),
                        total.As("refToTransPaymentItemOrder_Total")
                    );

                coll.Load(query);

                Session["DownPayment:TransPaymentItemOrder" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["DownPayment:TransPaymentItemOrder" + Request.UserHostName] = value; }
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
                foreach (TransPaymentItemOrder item in TransPaymentItemOrders)
                {
                    total += (item.Qty * item.Price);
                }
            }
            txtOrderAmount.Value = Convert.ToDouble(total);
        }

        private void SetNotesValue(string transNo)
        {
            var tp = new TransPrescription();
            if (tp.LoadByPrimaryKey(transNo))
                txtNotes.Text = "PEMBAYARAN DEPOSIT RESEP " + " (" + transNo + ")";
            else
            {
                var tc = new TransCharges();
                if (tc.LoadByPrimaryKey(transNo))
                {
                    var su = new ServiceUnit();
                    if (su.LoadByPrimaryKey(tc.ToServiceUnitID))
                        txtNotes.Text = "PEMBAYARAN DEPOSIT PELAYANAN " + su.ServiceUnitName.ToUpper() + " (" + transNo + ")";
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

                grdOrderItem.Rebind();
                CalculateOrderAmount();
                SetNotesValue(val[1]);
            }
            else if (eventArgument == "clearlist")
            {
                TransPaymentItemOrders.MarkAllAsDeleted();
                grdOrderItem.Rebind();
                CalculateOrderAmount();

                TransPaymentItems.MarkAllAsDeleted();
                grdTransPaymentItem.Rebind();

                txtNotes.Text = string.Empty;
            }
            else if (eventArgument == "deposit") grdTransPaymentItem.Rebind();
        }
    }
}

using System;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Collections.Generic;
using System.Linq;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class PaymentReturnDetail : BasePageDetail
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
            UrlPageSearch = "PaymentReturnSearch.aspx?fp=dt";
            UrlPageList = string.IsNullOrEmpty(Request.QueryString["utype"]) ? "PaymentReturnRegistrationList.aspx" : "PaymentReturnCashierList.aspx";

            if (AppSession.Parameter.IsNeedVoidReasonOnPaymentReceive)
            {
                IsUsingBeforeUnapprovalValidation = true;
            }

            ProgramID = string.IsNullOrEmpty(Request.QueryString["utype"]) ? AppConstant.Program.PaymentReturn : AppConstant.Program.PaymentReturnCashier;

            if (!IsPostBack)
            {
                TransPaymentItems = null;
                trInitial.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH";

                if (AppSession.Parameter.IsDpReturnUsingChecklist)
                    btnImportDownPayment.Visible = false;
                else
                    btnDownPayment.Visible = false;

                if (string.IsNullOrEmpty(Request.QueryString["RegistrationNo"]))
                {
                    rfvRegistrationNo.Enabled = false;
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
                case AppConstant.Report.DownPaymentReturnReceiptSlip:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("PaymentTypeDiscount", AppSession.Parameter.PaymentTypeDiscount);
                    printJobParameters.AddNew("SelfGuarantor", AppSession.Parameter.SelfGuarantor);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                    printJobParameters.AddNew("ReportTitle", "PAYMENT RETURN");

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
                                       usr.UserName;
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
            txtPaymentDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtPaymentTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
            txtRegistrationNo.Text = Request.QueryString["regno"];

            Registration reg = new Registration();
            if (reg.LoadByPrimaryKey(txtRegistrationNo.Text)) {
                txtServiceUnitID.Text = reg.ServiceUnitID;
                ServiceUnit unit = new ServiceUnit();
                unit.LoadByPrimaryKey(txtServiceUnitID.Text);
                lblServiceUnitName.Text = unit.ServiceUnitName;

                txtGuarantorID.Text = reg.GuarantorID;
                var grr = new Guarantor();
                grr.LoadByPrimaryKey(txtGuarantorID.Text);
                lblGuarantorName.Text = grr.GuarantorName;
            }

            Patient patient = new Patient();
            patient.LoadByPrimaryKey(reg.PatientID ?? Request.QueryString["patid"]);

            txtMedicalNo.Text = patient.MedicalNo;
            txtPatientName.Text = patient.PatientName;
            txtPrintReceiptAsName.Text = patient.PatientName;
            var std = new AppStandardReferenceItem();
            txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.str.SRSalutation) ? std.ItemName : string.Empty;

            

            //string[] patientParam = new string[2], mrg = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"]);
            //patientParam.SetValue(AppSession.Parameter.PaymentTypePayment, 0);
            //patientParam.SetValue(AppSession.Parameter.PaymentTypePersonalAR, 1);

            ViewState["IsVoid"] = false;
            ViewState["IsApproved"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            TransPayment entity = new TransPayment();
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

            TransPayment entity = new TransPayment();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            TransPayment entity = new TransPayment();
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
            TransPayment entity = new TransPayment();
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

            if (TransPaymentItems.Count == 0)
            {
                args.MessageText = "Payment detail is not defined";
                args.IsCancel = true;
                return;
            }

            // cek DP void atau tidak
            var dpiColl = new TransPaymentItemCollection();
            var dpiQ = new TransPaymentItemQuery("dpi");
            var dpQ = new TransPaymentQuery("dp");
            dpiQ.InnerJoin(dpQ).On(dpiQ.PaymentNo.Equal(dpQ.PaymentNo))
                .Where(dpiQ.PaymentNo.In(TransPaymentItems.Select(x => x.ReferenceNo).Distinct()), 
                dpQ.IsApproved.Equal(true));
            if (!dpiColl.Load(dpiQ)) {
                args.MessageText = "Down Payment reference can not be found or has been deleted";
                args.IsCancel = true;
                return;
            }
            var sumReturn = TransPaymentItems.Sum(x => x.Amount);
            var sumReceived = dpiColl.Sum(x => x.Amount);
            if (sumReturn != sumReceived)
            {
                args.MessageText = "Invalid return amount";
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

            entity.IsApproved = true;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.ApproveByUserID = AppSession.UserLogin.UserID;
            entity.ApproveDate = (new DateTime()).NowAtSqlServer();

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                if (entity.TransactionCode == TransactionCode.DownPaymentReturn)
                {
                    int? journalId =
                        JournalTransactions.AddNewDownPaymentReturnJournal(BusinessObject.JournalType.DownPaymentReturn,
                                                                           entity, reg, this.TransPaymentItems, "DR",
                                                                           AppSession.UserLogin.UserID, 0);
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }
        public override bool OnGetStatusMenuUnApprovalEnabled()
        {
            return !PostingStatus.IsUnApproveDisabledIfPerClosed(txtPaymentDate.SelectedDate.Value);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var reason = args.ReasonText;

            TransPayment entity = new TransPayment();
            if (!entity.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
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

                args.MessageText = "You don't have authorization to un-approved this transaction. This data belong to: " +
                                   usr.UserName + ". Please contact your supervision.";
                args.IsCancel = true;
                return;
            }

            if ((new DateTime()).NowAtSqlServer() > entity.LastUpdateDateTime.Value.AddHours(AppSession.Parameter.TimeLimitForVoidPayment) && !_isPowerUser)
            {
                args.MessageText = "You don't have authorization to Un-Approved this transaction. Time limit already exceeded.";
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

            entity.IsApproved = false;
            entity.IsVoid = true;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.VoidByUserID = AppSession.UserLogin.UserID;
            entity.VoidDate = (new DateTime()).NowAtSqlServer();
            entity.VoidReason = reason;

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

              
                    int? journalId =
                        JournalTransactions.AddNewDownPaymentReturnCorrectionJournal(BusinessObject.JournalType.DownPaymentReturn,
                                                                           entity, reg, this.TransPaymentItems, "DR",
                                                                           AppSession.UserLogin.UserID, 0);
               
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            TransPayment entity = new TransPayment();
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

            entity.IsVoid = true;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                trans.Complete();
            }
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtPaymentNo.Text != string.Empty;
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
            TransPayment entity = new TransPayment();
            if (parameters.Length > 0)
            {
                String paymentNo = (String)parameters[0];

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
            TransPayment transPayment = (TransPayment)entity;
            txtPaymentNo.Text = transPayment.PaymentNo;
            txtPaymentDate.SelectedDate = transPayment.PaymentDate;
            txtPaymentTime.Text = transPayment.PaymentTime;

            txtRegistrationNo.Text = transPayment.RegistrationNo;

            Registration registration = new Registration();
            if (registration.LoadByPrimaryKey(txtRegistrationNo.Text)) {
                txtServiceUnitID.Text = registration.ServiceUnitID;
                ServiceUnit unit = new ServiceUnit();
                unit.LoadByPrimaryKey(txtServiceUnitID.Text);
                lblServiceUnitName.Text = unit.ServiceUnitName;

                txtGuarantorID.Text = registration.GuarantorID;
                Guarantor grr = new Guarantor();
                grr.LoadByPrimaryKey(txtGuarantorID.Text);
                lblGuarantorName.Text = grr.GuarantorName;
            }

            Patient pat = new Patient();
            if (pat.LoadByPrimaryKey((registration.PatientID ?? (transPayment.PatientID ?? Request.QueryString["patid"])))) {
                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.str.SRSalutation) ? std.ItemName : string.Empty;
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtSalutation.Text = string.Empty;
            }


            //string[] patientParam = new string[2], reg = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"]);
            //patientParam.SetValue(AppSession.Parameter.PaymentTypePayment, 0);
            //patientParam.SetValue(AppSession.Parameter.PaymentTypePersonalAR, 1);

            ViewState["IsVoid"] = transPayment.IsVoid ?? false;
            ViewState["IsApproved"] = transPayment.IsApproved ?? false;

            txtNotes.Text = transPayment.Notes;
            txtInitial.Text = transPayment.Initial;

            //Display Data Detail
            PopulateTransPaymentItemGrid();
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
            entity.TransactionCode = BusinessObject.Reference.TransactionCode.DownPaymentReturn;
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.ReferenceNo = string.Empty;
            entity.PaymentDate = txtPaymentDate.SelectedDate;
            entity.PaymentTime = txtPaymentTime.Text;

            decimal total = 0;
            foreach (TransPaymentItem payment in TransPaymentItems)
            {
                total += (payment.Amount ?? 0);
            }
            entity.TotalPaymentAmount = total;

            entity.RemainingAmount = 0;
            entity.PrintNumber = (byte)0;
            entity.PaymentReceiptNo = string.Empty;
            entity.IsPrinted = false;
            entity.IsVoid = false;
            entity.IsApproved = false;
            entity.Notes = txtNotes.Text;
            entity.Initial = txtInitial.Text;
            entity.GuarantorID = txtGuarantorID.Text;
            entity.PrintReceiptAsName = txtPrintReceiptAsName.Text;
            entity.PatientID = Request.QueryString["patid"];

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
            using (esTransactionScope trans = new esTransactionScope())
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
            TransPaymentQuery que = new TransPaymentQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.PaymentNo > txtPaymentNo.Text,
                        que.TransactionCode == BusinessObject.Reference.TransactionCode.PaymentReturn
                    );
                que.OrderBy(que.PaymentNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.PaymentNo < txtPaymentNo.Text,
                        que.TransactionCode == BusinessObject.Reference.TransactionCode.PaymentReturn
                    );
                que.OrderBy(que.PaymentNo.Descending);
            }
            TransPayment entity = new TransPayment();
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

        private void RefreshCommandItemTransPaymentItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdTransPaymentItem.Columns[0].Visible = isVisible; //false;
            grdTransPaymentItem.Columns[grdTransPaymentItem.Columns.Count - 1].Visible = isVisible && string.IsNullOrEmpty(Request.QueryString["utype"]);

            //Perbaharui tampilan dan data
            grdTransPaymentItem.Rebind();

            btnImportDownPayment.Enabled = isVisible && string.IsNullOrEmpty(Request.QueryString["utype"]);
            btnDownPayment.Enabled = isVisible && string.IsNullOrEmpty(Request.QueryString["utype"]);
        }

        private TransPaymentItemCollection TransPaymentItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["DownPaymentReturnItems" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((TransPaymentItemCollection)(obj));
                    }
                }

                var coll = new TransPaymentItemCollection();
                var query = new TransPaymentItemQuery("a");
                var srQuery = new PaymentMethodQuery("b");
                var srQuery2 = new PaymentTypeQuery("c");
                var bankQuery = new BankQuery("d");

                query.Select
                    (
                        query,
                        srQuery2.PaymentTypeName.As("refToAppStandardReferenceItem_PaymentType"),
                        srQuery.PaymentMethodName.As("refToAppStandardReferenceItem_PaymentMethod"),
                        bankQuery.BankName.As("refToBank_BankName"),
                        query.Amount
                    );
                query.InnerJoin(srQuery2).On(query.SRPaymentType == srQuery2.SRPaymentTypeID);
                query.LeftJoin(srQuery).On
                   (
                       srQuery2.SRPaymentTypeID == srQuery.SRPaymentTypeID &
                       query.SRPaymentMethod == srQuery.SRPaymentMethodID
                   );
                query.LeftJoin(bankQuery).On(bankQuery.BankID == query.BankID);
                query.Where(query.PaymentNo == txtPaymentNo.Text);
                query.OrderBy(query.SequenceNo.Ascending);

                coll.Load(query);

                Session["DownPaymentReturnItems" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["DownPaymentReturnItems" + Request.UserHostName] = value; }
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
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][TransPaymentItemMetadata.ColumnNames.SequenceNo]);

            TransPaymentItem entity = FindTransPaymentItem(sequenceNo);
            Double amount = (double)entity.Amount;
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdTransPaymentItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransPaymentItemMetadata.ColumnNames.SequenceNo]);
            TransPaymentItem entity = FindTransPaymentItem(sequenceNo);
            Double amount = (double)entity.Amount;

            if (entity != null)
            {
                entity.MarkAsDeleted();
            }
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
            ItemPaymentReturn userControl = (ItemPaymentReturn)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
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
                entity.BankName = userControl.BankName;
                entity.ReferenceNo = userControl.ReferenceNo;
                entity.Amount = userControl.Amount;
                entity.IsFromDownPayment = false;
            }
        }

        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (!string.IsNullOrEmpty(Request.QueryString["utype"]))
            {
                ToolBarMenuAdd.Enabled = false;

                btnImportDownPayment.Visible = false;
                btnDownPayment.Visible = false;
            }

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"];
        }

        protected void btnImportDownPayment_Click(object sender, EventArgs e)
        {
            var payment = new TransPaymentQuery("a");
            var item = new TransPaymentItemQuery("b");

            item.Select(item);
            item.InnerJoin(payment).On(payment.PaymentNo == item.PaymentNo);
            item.Where
                (
                    payment.TransactionCode == BusinessObject.Reference.TransactionCode.DownPayment,
                    payment.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text)),
                    payment.IsApproved == true,
                    payment.PaymentReferenceNo == string.Empty
                );
            item.es.Distinct = true;
            
            var itemColl = new TransPaymentItemCollection();
            itemColl.Load(item);

            int idx = 0;
            foreach (var entity in itemColl)
            {
                var itemRets = new TransPaymentItemCollection();
                var itemRet = new TransPaymentItemQuery("a");
                var ret = new TransPaymentQuery("b");
                itemRet.InnerJoin(ret).On(itemRet.PaymentNo == ret.PaymentNo && ret.IsVoid == false &&
                                          ret.TransactionCode == TransactionCode.DownPaymentReturn);
                itemRet.Where(itemRet.ReferenceNo == entity.PaymentNo);
                itemRet.Select(itemRet);
                itemRets.Load(itemRet);
               
                if (itemRets.Count == 0)
                {
                    idx++;

                    var tpi = TransPaymentItems.AddNew();
                    tpi.SequenceNo = string.Format("{0:000}", idx);

                    tpi.SRPaymentType = entity.SRPaymentType;
                    var pt = new AppStandardReferenceItem();
                    pt.LoadByPrimaryKey(AppEnum.StandardReference.PaymentType.ToString(), tpi.SRPaymentType);
                    tpi.PaymentTypeName = pt.ItemName;

                    tpi.SRPaymentMethod = AppSession.Parameter.PaymentMethodCash;
                    var pm = new AppStandardReferenceItem();
                    pm.LoadByPrimaryKey(AppEnum.StandardReference.PaymentMethod.ToString(), tpi.SRPaymentMethod);
                    tpi.PaymentMethodName = pm.ItemName;

                    tpi.SRCardProvider = string.Empty; //entity.SRCardProvider;
                    tpi.SRCardType = string.Empty; //entity.SRCardType;
                    tpi.SRDiscountReason = string.Empty; //entity.SRDiscountReason;
                    tpi.EDCMachineID = string.Empty; //entity.EDCMachineID;
                    tpi.CardHolderName = string.Empty; //entity.CardHolderName;
                    tpi.CardFeeAmount = 0; //entity.CardFeeAmount;
                    tpi.BankID = string.Empty; //entity.BankID;
                    tpi.BankName = string.Empty;
                    tpi.ReferenceNo = entity.PaymentNo;
                    tpi.Amount = entity.Amount;
                    tpi.Balance = entity.Balance;
                    tpi.IsFromDownPayment = entity.IsFromDownPayment;
                }
            }

            grdTransPaymentItem.Rebind();

            btnImportDownPayment.Enabled = false;
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler source, string argument)
        {
            base.RaisePostBackEvent(source, argument);

            if (string.IsNullOrEmpty(argument) || !(source is RadGrid))
                return;

            if (argument.Contains("rebind"))
            {
                grdTransPaymentItem.Rebind();
            }
        }
    }
}

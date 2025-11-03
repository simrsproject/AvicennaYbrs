using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using DevExpress.XtraBars.Ribbon.Drawing;

namespace Temiang.Avicenna.Module.Finance.CashManagement.CashEntryV2
{
    public partial class CashEntryDetail : BasePageDetail
    {
        private bool _isPowerUser;
        protected int TransactionId
        {
            get
            {
                var tmpVal = lblJournalId.Text; //Request.QueryString["ivd"];
                int ret;
                int.TryParse(tmpVal, out ret);
                return ret;
            }
            set
            {
                lblJournalId.Text = value.ToString();
            }
        }

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            ToolBarMenuSearch.Enabled = false;
            //UrlPageSearch = "VoucherEntrySearch.aspx";

            if (Request.QueryString["source"] == "ce")
            {
                ProgramID = AppConstant.Program.CASH_ENTRY;
                UrlPageList = "CashEntryList.aspx";
                UrlPageSearch = "CashEntrySearch.aspx";
            }
            else if (Request.QueryString["source"] == "re")
            {
                ProgramID = AppConstant.Program.CASH_ENTRY;
                UrlPageList = "../Reconcile/ReconcileDetail.aspx?md=view&bankid=" + Request.QueryString["bankid"];
                UrlPageSearch = "CashEntrySearch.aspx";
            }
            else if (Request.QueryString["source"] == "reV2")
            {
                ProgramID = AppConstant.Program.CASH_ENTRY;
                UrlPageList = "../ReconcileV2/ReconcileDetail.aspx?md=view&bankid=" + Request.QueryString["bankid"];
                UrlPageSearch = "CashEntrySearch.aspx";
            }
            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                StandardReference.Initialize(cboTransactionType, AppEnum.StandardReference.CashTransType);
                InitCurrency();
                //   InitPaymentType(false);
                //  InitPaymentMethod();

                trBudgettingCode.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB";
                trFromTo.Visible = AppSession.Parameter.IsCashEntryShowReceivedFromPaidTo;

                lblFromTo.Text = (cboNormalBalance.SelectedValue == "D") ? "Received From" : "Paid To";

                pnlBKU.Visible = AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingBKUModule) == "Yes";
            }


            //   Tambah otorisasi poower user 
            _isPowerUser = this.IsPowerUser;

            if (!_isPowerUser)
            {
                txtTransactionDate.Enabled = false;
            }




            Helper.SetupComboBox(txtBankAccount);
            Helper.SetupGrid(grdVoucherEntryItem);


            grdVoucherEntryItem.SortCommand += grdVoucherEntryItem_SortCommand;
            grdVoucherEntryItem.ItemCommand += grdVoucherEntryItem_ItemCommand;
            grdVoucherEntryItem.ItemDataBound += grdVoucherEntryItem_ItemDataBound;

            txtBankAccount.ItemsRequested += txtBankAccount_ItemsRequested;
            txtBankAccount.ItemDataBound += txtBankAccount_ItemDataBound;
            txtBankAccount.TextChanged += txtBankAccount_TextChanged;
            txtBankAccount.SelectedIndexChanged += txtBankAccount_SelectedIndexChanged;

            cboPaymentType.AutoPostBack = true;
            cboPaymentType.TextChanged += cboPaymentType_TextChanged;
        }

        void cboPaymentType_TextChanged(object sender, EventArgs e)
        {
            InitPaymentMethod();
        }

        void grdVoucherEntryItem_ItemDataBound(object sender, GridItemEventArgs e)
        {
        }

        void grdVoucherEntryItem_ItemCommand(object source, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "InitInsert":
                    Validate();
                    if (!IsValid)
                    {
                        e.Canceled = true;
                        return;
                    }
                    Session["CashTransaction::Description"] = txtDescription.Text;
                    break;
            }
        }

        void txtBankAccount_TextChanged(object sender, EventArgs e)
        {
            txtCurrencyRate.Text = "1";
            cboCurrency.ClearSelection();

            var code = txtBankAccount.Text;
            var entity = Bank.Get(code);
            if (entity == null)
                return;

            cboCurrency.SelectedValue = entity.CurrencyCode;
            var currency = new CurrencyRate();
            currency.Query.Where(currency.Query.CurrencyID == entity.CurrencyCode);
            if (currency.Query.Load())
            {
                txtCurrencyRate.Text = currency.CurrencyRate.ToString();
            }
        }

        void txtBankAccount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            //e.Item.Attributes["IsAutoNumber"] = ((JournalCodes)e.Item.DataItem).IsAutoNumber.Value ? "1" : "0";
        }

        void txtBankAccount_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var box = o as RadComboBox;
            if (box == null)
                return;

            var val = e.Text;
            if (val.Length != 0)
            {
                var coll = Bank.GetLike(val);

                box.Items.Clear();
                box.DataSource = coll;
                box.DataBind();
            }
        }

        void grdVoucherEntryItem_SortCommand(object source, GridSortCommandEventArgs e)
        {
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                var sortExpr = new GridSortExpression { FieldName = e.SortExpression, SortOrder = e.NewSortOrder };

                grdVoucherEntryItem.MasterTableView.SortExpressions.Clear();
                grdVoucherEntryItem.MasterTableView.SortExpressions.AddSortExpression(sortExpr);

                grdVoucherEntryItem.Rebind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                var q = Request.QueryString["ivd"];
                var val = 0;
                if (int.TryParse(q, out val))
                    TransactionId = val;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        private void InitCurrency()
        {
            var coll = new CurrencyRateCollection();
            coll.LoadAll();

            cboCurrency.DataSource = coll;
            cboCurrency.DataTextField = CurrencyRateMetadata.ColumnNames.CurrencyName;
            cboCurrency.DataValueField = CurrencyRateMetadata.ColumnNames.CurrencyID;
            cboCurrency.DataBind();
        }

        private void InitPaymentType()
        {
            cboPaymentType.DataSource = null;
            cboPaymentType.DataBind();

            var coll = new PaymentTypeCollection();

            if (cboModuleName.SelectedIndex == 0 || cboModuleName.SelectedIndex == 2)
                coll.Query.Where(coll.Query.IsApPayment == true);
            else if (cboModuleName.SelectedIndex == 1)
                coll.Query.Where(coll.Query.IsArPayment == true);

            coll.LoadAll();

            cboPaymentType.DataSource = coll;
            cboPaymentType.DataTextField = PaymentTypeMetadata.ColumnNames.PaymentTypeName;
            cboPaymentType.DataValueField = PaymentTypeMetadata.ColumnNames.SRPaymentTypeID;
            cboPaymentType.DataBind();
        }

        private void InitPaymentMethod()
        {
            cboPaymentMethod.DataSource = null;
            cboPaymentMethod.DataBind();

            var query = new PaymentMethodQuery();
            query.Where(query.SRPaymentTypeID == cboPaymentType.SelectedValue);

            var coll = new PaymentMethodCollection();
            coll.Load(query);

            cboPaymentMethod.DataSource = coll;
            cboPaymentMethod.DataTextField = PaymentMethodMetadata.ColumnNames.PaymentMethodName;
            cboPaymentMethod.DataValueField = PaymentMethodMetadata.ColumnNames.SRPaymentMethodID;
            cboPaymentMethod.DataBind();
        }

        #endregion

        #region Toolbar Menu Event
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            if (TransactionId == 0)
            {
                args.MessageText = AppConstant.Message.RecordCanNotEdited;
                args.IsCancel = true;
                return;
            }

            var entity = CashTransaction.Get(TransactionId);
            if (entity == null)
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (entity.IsPosted.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved + AppConstant.Message.RecordCanNotEdited;
                args.IsCancel = true;
                return;
            }

            //if (entity.IsVoid.Value)
            //{
            //    args.MessageText = AppConstant.Message.RecordHasVoided + AppConstant.Message.RecordCanNotEdited;
            //    args.IsCancel = true;
            //    return;
            //}
        }

        protected override void OnMenuNewClick()
        {
            TransactionId = 0;

            EmptyingRefference();

            var coll = CashTransactionDetails;

            txtTransactionDate.SelectedDate = DateTime.Now;
            Session.Remove("CashTransaction::Description");

            var ct = new CashTransaction();
            if (Request.QueryString["source"] == "re")
            {
                ct.BankId = Request.QueryString["bankid"];
                ct.IsCleared = true;
            }

            OnPopulateEntryControl(ct);

            txtBankAccount.Text = string.Empty;
            txtBankAccount.SelectedValue = string.Empty;
            cboModuleName.Text = string.Empty;
            cboModuleName.SelectedValue = string.Empty;
            cboTransactionType.Text = string.Empty;
            cboTransactionType.SelectedValue = string.Empty;
            cboNormalBalance.Text = string.Empty;
            cboNormalBalance.SelectedValue = string.Empty;

            ShowBtnRefference();
            cboReferenceType.Text = string.Empty;
            cboReferenceType.SelectedValue = string.Empty;

            grdVoucherEntryItem.Rebind();
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = CashTransaction.Get(TransactionId);
            if (entity != null)
            {
                if (entity.IsPosted.Value)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved + AppConstant.Message.RecordCanNotDeleted;
                    args.IsCancel = true;
                    return;
                }

                entity.MarkAsDeleted();

                var coll = new CashTransactionDetailCollection();
                coll.Query.Where(coll.Query.TransactionId == TransactionId);
                coll.LoadAll();
                coll.MarkAllAsDeleted();

                using (var trans = new esTransactionScope())
                {
                    coll.Save();
                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var bank = Bank.Get(txtBankAccount.SelectedValue);
            if (bank == null)
            {
                args.MessageText = "Invalid Bank Account";
                args.IsCancel = true;
                return;
            }

            if (!Helper.ValidatePeriode(txtTransactionDate.SelectedDate.Value))
            {
                args.MessageText = AppConstant.Message.TRANSACTION_DATE_HAS_CLOSED;
                args.IsCancel = true;
                return;
            }

            CashTransaction entity = null;
            if (TransactionId == 0)
            {
                entity = new CashTransaction();
                entity.AddNew();
            }
            else
            {
                entity = CashTransaction.Get(TransactionId);
                if (entity.IsPosted.Value)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return;
                }
            }

            if (pnlBKU.Visible && string.IsNullOrEmpty(cboBkuAccount.SelectedValue))
            {
                args.MessageText = "BKU Account required";
                args.IsCancel = true;
                return;
            }


            SetEntityValue(entity);
            if (!SaveEntity(entity))
            {
                args.MessageText = "Unable to create new transaction please try again.";
                args.IsCancel = true;
                return;
            }

            GenerateGrid();
            // close edit form in grid
            grdVoucherEntryItem.MasterTableView.IsItemInserted = false;
        }

        protected override void OnMenuEditClick()
        {
            var totalCount = CashTransactionDetail.TotalCount(TransactionId);
            if (totalCount == 0)
            {
                cboNormalBalance.Enabled = true;
                txtBankAccount.Enabled = true;
            }

            var coll = CashTransactionDetails;
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = CashTransaction.Get(TransactionId);
            if (entity == null)
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            else
            {
                if (entity.IsPosted.Value)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return;
                }

                if (!Helper.ValidatePeriode(txtTransactionDate.SelectedDate.Value))
                {
                    args.MessageText = AppConstant.Message.TRANSACTION_DATE_HAS_CLOSED;
                    args.IsCancel = true;
                    return;
                }
            }
            if (pnlBKU.Visible && string.IsNullOrEmpty(cboBkuAccount.SelectedValue))
            {
                args.MessageText = "BKU Account required";
                args.IsCancel = true;
                return;
            }

            SetEntityValue(entity);
            using (var scope = new esTransactionScope())
            {
                ////CashTransaction.Save(entity);
                //if (hfIdent.Value.Equals("3"))
                //{
                //    // save link to payment AR
                //    entity.PaymentARNoRef = lblRef1.Text;
                //}
                //if (hfIdent.Value.Equals("4"))
                //{
                //    // save link to payment AP
                //    entity.PaymentAPNoRef = lblRef1.Text;
                //}

                //if (hfIdent.Value.Equals("3")) {
                //    entity.PaymentARNoRef = lblRef1.Text;
                //}
                //if (hfIdent.Value.Equals("4")) {
                //    entity.PaymentAPNoRef = lblRef1.Text;
                //}
                entity.Save();

                var PaymentNo = string.Empty;
                var SequenceNo = string.Empty;


                if (hfIdent.Value.Equals("2"))
                {
                    //// save link to payment receive
                    //PaymentNo = lblRef1.Text;
                    //SequenceNo = hfSequenceNo.Value;
                    var tpiColl = new TransPaymentItemCollection();
                    //CashTransaction.SetLinkToPaymentReceive(tpiColl, entity);
                    //tpiColl.Save();

                    //tpiColl.Query.Where(tpiColl.Query.CashTransactionReconcileId == entity.TransactionId);
                    //if(tpiColl.LoadAll()){
                    //    foreach (var tpi in tpiColl)
                    //    {
                    //        tpi.CashTransactionReconcileId = null;
                    //    }
                    //    tpiColl.Save();
                    //}

                    //var newTpiColl = ParseTPI(lblRef1.Text);
                    //foreach (var tpi in newTpiColl) {
                    //    tpi.CashTransactionReconcileId = entity.TransactionId.Value;
                    //}
                    //newTpiColl.Save();
                }
                else if (hfIdent.Value.Equals("3"))
                {
                    // save link to ar receive
                    PaymentNo = lblRef1.Text;
                    SequenceNo = hfSequenceNo.Value;
                    var arPayColl = new InvoicesCollection();
                    CashTransaction.SetLinkToPaymentARReceive(arPayColl, entity, PaymentNo);
                    arPayColl.Save();
                }
                else if (hfIdent.Value.Equals("4"))
                {
                    // save link to ap receive
                    PaymentNo = lblRef1.Text;
                    SequenceNo = hfSequenceNo.Value;
                    var apPayColl = new InvoiceSupplierCollection();
                    CashTransaction.SetLinkToPaymentAPReceive(apPayColl, entity, PaymentNo);
                    apPayColl.Save();
                }
                else if (hfIdent.Value.Equals("6"))
                {
                    // save link to ap receive
                    PaymentNo = lblRef1.Text;
                    SequenceNo = hfSequenceNo.Value;

                    CashTransaction.SetLinkToReturnPO(entity, PaymentNo);
                }
                else if (hfIdent.Value.Equals("7"))
                {
                    // save link to ap receive
                    PaymentNo = lblRef1.Text;
                    SequenceNo = hfSequenceNo.Value;

                    CashTransaction.SetLinkToReturnPO(entity, PaymentNo);
                }
                else if (hfIdent.Value.Equals("5"))
                {
                    // save link to ap receive
                    PaymentNo = lblRef1.Text;
                    SequenceNo = hfSequenceNo.Value;

                    CashTransaction.SetLinkToReturnPO(entity, PaymentNo);
                }

                scope.Complete();
            }


            grdVoucherEntryItem.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            // close edit form in grid
            grdVoucherEntryItem.MasterTableView.ClearEditItems();
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionId='{0}'", TransactionId);
            auditLogFilter.TableName = "CashTransaction";
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var bank = Bank.Get(txtBankAccount.SelectedValue);
            if (bank == null)
            {
                args.MessageText = "Invalid Bank Account";
                args.IsCancel = true;
                return;
            }
            if (string.IsNullOrEmpty(bank.JournalCode))
            {
                args.MessageText = string.Format("Invalid Journal Code Of Bank Account Of {0}", bank.BankName);
                args.IsCancel = true;
                return;
            }

            var jCode = JournalCodes.GetOrCreateAutoJournalCode(
                JournalCodes.GetJournalCodeForCashEntry(cboNormalBalance.SelectedValue, bank.JournalCode),
                txtTransactionDate.SelectedDate.Value);
            //var jSeq = JournalCodes.GenerateAndIncrementAutoNumber(jc);

            //var journalCode = JournalCodes.Get(txtJournalCode.Text);
            if (string.IsNullOrEmpty(jCode))
            {
                args.MessageText = "Invalid Journal Code";
                args.IsCancel = true;
                return;
            }

            if (TransactionId == 0 && (true == (args.IsCancel = true)))
                return;

            var entity = CashTransaction.Get(TransactionId);
            if (entity == null)
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            else
            {
                if (entity.IsPosted.Value)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return;
                }

                if (!Helper.ValidatePeriode(entity.TransactionDate.Value))
                {
                    args.MessageText = AppConstant.Message.TRANSACTION_DATE_HAS_CLOSED;
                    args.IsCancel = true;
                    return;
                }
            }


            string trNumber = string.Empty;
            int ret = CashTransaction.MarkStatusAsPosting(TransactionId, AppSession.UserLogin.UserID, jCode);
            if (ret != 0)
            {
                args.MessageText = "Approval failed for this transaction. Please check again your transaction.";
                args.IsCancel = true;
            }

            // jurnal bku
            BkuJournalTransactions.AddBkuJournalByCashTransactions(TransactionId, AppSession.UserLogin.UserID);

            if (pnlBKU.Visible)
            {
                using (var trans = new esTransactionScope())
                {
                    var pts = new PaymentTypeCollection();
                    pts.Query.Where(pts.Query.IsCashierFrontOffice == true);
                    pts.Query.Load();

                    var pms = new PaymentMethodCollection();
                    pms.Query.Where(pms.Query.SRPaymentTypeID.In(pts.Select(p => p.SRPaymentTypeID)));
                    pms.Query.Load();

                    var coas = new int[pms.Count];
                    int index = 0;
                    foreach (var pm in pms)
                    {
                        coas.SetValue(pm.ChartOfAccountID, index);
                        index++;
                    }

                    entity = CashTransaction.Get(TransactionId);

                    var bkus = new BkuTransactionCollection();
                    foreach (var detail in CashTransactionDetail.GetAllForTransactions(TransactionId))
                    {
                        var jhd = new JournalTransactions();
                        jhd.Query.Where(jhd.Query.RefferenceNumber == (detail.ReferenceNo ?? string.Empty), jhd.Query.IsPosted == true);
                        if (!jhd.Query.Load()) continue;

                        var jdts = new JournalTransactionDetailsCollection();
                        jdts.Query.Where(jdts.Query.JournalId == jhd.JournalId);
                        jdts.Query.Load();

                        var coa = new ChartOfAccounts();
                        coa.LoadByPrimaryKey(jdts.Where(j => coas.Contains(j.ChartOfAccountId ?? 0) && j.DocumentNumber == detail.ReferenceNo).Single().ChartOfAccountId ?? 0);

                        var bku = bkus.AddNew();
                        bku.RekeningID = coa.ChartOfAccountId;
                        bku.UnitID = 0;
                        bku.Debit = detail.Amount;
                        bku.Credit = 0;
                        bku.Uraian = string.Empty;
                        bku.PaymentReferenceNo = entity.JournalNumber;

                        foreach (var jdt in jdts.Where(j => j.DocumentNumber != detail.ReferenceNo))
                        {
                            bku = bkus.AddNew();
                            bku.RekeningID = cboBkuAccount.SelectedValue.ToInt();
                            bku.UnitID = jdt.SubLedgerId;
                            bku.Debit = jdt.Debit;
                            bku.Credit = jdt.Credit;
                            bku.Uraian = jdt.Description;
                            bku.PaymentReferenceNo = entity.JournalNumber;
                            //bku.InvoiceReferenceNo = detail.InvoiceReferenceNo;
                            bku.TransactionReferenceNo = jhd.RefferenceNumber;
                        }
                    }
                    bkus.Save();

                    foreach (var bku in (from b in bkus
                                         where b.RekeningID == cboBkuAccount.SelectedValue.ToInt()
                                         group b by b.RekeningID into g
                                         select new { RekeningID = g.Key, Debit = g.Sum(d => d.Debit), Credit = g.Sum(c => c.Credit) }))
                    {
                        var bal = new BkuTransactionBalances();
                        bal.Query.Where(bal.Query.RekeningID == bku.RekeningID, bal.Query.Date == DateTime.Now.Date);
                        if (!bal.Query.Load()) bal = new BkuTransactionBalances();
                        bal.RekeningID = bku.RekeningID;
                        bal.Date = DateTime.Now.Date;
                        bal.InitialBalance = 0;
                        bal.DebitAmount = (bal.DebitAmount ?? 0) + bku.Debit;
                        bal.CreditAmount = (bal.CreditAmount ?? 0) + bku.Credit;
                        bal.FinalBalance = bal.DebitAmount - bal.CreditAmount;
                        bal.Save();
                    }

                    trans.Complete();
                }
            }

        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = CashTransaction.Get(TransactionId);
            if (entity == null)
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            else
            {
                if (!entity.IsPosted.Value)
                {
                    args.MessageText = AppConstant.Message.RecordHasNotApproved;
                    args.IsCancel = true;
                    return;
                }

                if (entity.IsVoid.Value)
                {
                    args.MessageText = AppConstant.Message.RecordHasVoided;
                    args.IsCancel = true;
                    return;
                }

                if (entity.IsAutoCashEntry ?? false)
                {
                    args.MessageText = "Auto cash entry can not be unapproved";
                    args.IsCancel = true;
                    return;
                }

                if (!Helper.ValidatePeriode(entity.TransactionDate.Value))
                {
                    args.MessageText = AppConstant.Message.TRANSACTION_DATE_HAS_CLOSED;
                    args.IsCancel = true;
                    return;
                }
            }

            string trNumber = string.Empty;
            var bank = Bank.Get(txtBankAccount.SelectedValue);
            var code = JournalCodes.GetJournalCodeForCashEntry(cboNormalBalance.SelectedValue, bank.JournalCode);
            int ret = CashTransaction.MarkStatusAsUnPostingRev2(TransactionId, AppSession.UserLogin.UserID, code);
            if (ret != 0)
            {
                args.MessageText = "UnApproval failed for this transaction. Please check again your transaction.";
                args.IsCancel = true;
            }

            if (pnlBKU.Visible)
            {
                var bt = new BkuTransactionCollection();
                bt.Query.Where(bt.Query.PaymentReferenceNo == entity.JournalNumber);
                if (bt.Query.Load())
                {
                    foreach (var bku in (from b in bt
                                         where b.RekeningID == cboBkuAccount.SelectedValue.ToInt()
                                         group b by b.RekeningID into g
                                         select new { RekeningID = g.Key, Debit = g.Sum(d => d.Debit), Credit = g.Sum(c => c.Credit) }))
                    {
                        var bal = new BkuTransactionBalances();
                        bal.Query.Where(bal.Query.RekeningID == bku.RekeningID, bal.Query.Date == DateTime.Now.Date);
                        if (!bal.Query.Load()) bal = new BkuTransactionBalances();
                        bal.RekeningID = bku.RekeningID;
                        bal.Date = DateTime.Now.Date;
                        bal.InitialBalance = 0;
                        bal.DebitAmount = (bal.DebitAmount ?? 0) - bku.Debit;
                        bal.CreditAmount = (bal.CreditAmount ?? 0) - bku.Credit;
                        bal.FinalBalance = bal.DebitAmount - bal.CreditAmount;
                        bal.Save();
                    }

                    bt.MarkAllAsDeleted();
                    bt.Save();
                }
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            if (TransactionId == 0 && (true == (args.IsCancel = true)))
                return;

            var entity = CashTransaction.Get(TransactionId);
            if (entity == null)
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (entity.IsVoid.Value || entity.IsPosted.Value)
            {
                args.MessageText = "Unable to void this transaction.";
                args.IsCancel = true;
                return;
            }

            entity.IsVoid = true;
            entity.VoidDate = DateTime.Now.Date;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;

            using (var scope = new esTransactionScope())
            {
                entity.Save();

                //var tpiColl = new TransPaymentItemCollection();
                //CashTransaction.SetLinkToPaymentReceive(tpiColl, entity, string.Empty, string.Empty);
                //tpiColl.Save();
                var detail = new CashTransactionDetailCollection();
                if (detail.LoadByTransactionId(entity.TransactionId.Value))
                {
                    foreach (var d in detail)
                    {
                        var tp = d.SetUnLinkToPaymentReceive();
                        if (tp != null) tp.Save();

                        var tpRet = d.SetUnLinkToPaymentReceiveReturn();
                        if (tpRet != null) tpRet.Save();

                    }
                }

                var tpARColl = new InvoicesCollection();
                CashTransaction.SetLinkToPaymentARReceive(tpARColl, entity, string.Empty);
                tpARColl.Save();

                var tpAPColl = new InvoiceSupplierCollection();
                CashTransaction.SetLinkToPaymentAPReceive(tpAPColl, entity, string.Empty);
                tpAPColl.Save();

                // save changes to database
                scope.Complete();
            }


        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            if (TransactionId == 0 && (true == (args.IsCancel = true)))
                return;

            CashTransaction entity = CashTransaction.Get(TransactionId);
            if (entity == null)
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (!entity.IsVoid.Value || entity.IsPosted.Value)
            {
                args.MessageText = "Unable to unvoid this transaction.";
                args.IsCancel = true;
                return;
            }

            entity.IsVoid = false;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;
            entity.Save();
        }
        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuUnApprovalEnabled()
        {
            return !PostingStatus.IsUnApproveDisabledIfPerClosed(txtTransactionDate.SelectedDate.Value);
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //if (oldVal == DataMode.New && TransactionId == 0)
            //    Response.Redirect("CashEntryList.aspx");

            RefreshCommandItemGrid(oldVal, newVal);

            txtBankAccount.Enabled = cboNormalBalance.Enabled = cboModuleName.Enabled = (newVal == AppEnum.DataMode.New);

            if (Request.QueryString["source"] == "re")
            {
                txtBankAccount.Enabled = false;
                chkIsCleared.Enabled = false;
            }

            ShowBtnRefference();
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new CashTransaction();
            if (parameters.Length > 0)
            {
                string id = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity = CashTransaction.Get(Convert.ToInt32(id));
            }
            else
            {
                entity = CashTransaction.Get(TransactionId);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var e = (CashTransaction)entity;

            if (!string.IsNullOrEmpty(e.BankId))
            {
                var bankAccount = Bank.Get(e.BankId);

                txtBankAccount.SelectedValue = e.BankId;
                //txtBankAccount.SelectedItem.Text = string.Format("{0} - {1}", bankAccount.BankName.Trim(), bankAccount.NoRek.Trim());
                txtBankAccount.Text = string.Format("{0} - {1}", bankAccount.BankName, bankAccount.NoRek);
            }

            cboModuleName.SelectedValue = e.Module ?? cboModuleName.SelectedValue;
            cboTransactionType.SelectedValue = e.TransactionType ?? cboTransactionType.SelectedValue;
            cboNormalBalance.SelectedValue = e.NormalBalance ?? cboNormalBalance.SelectedValue;
            cboCurrency.SelectedValue = e.CurrencyCode;
            txtCurrencyRate.Value = Convert.ToDouble(e.CurrencyRate ?? 1);
            // e.CurrencyRate.HasValue ? e.CurrencyRate.Value.ToString() : "1";
            txtChequeNumber.Text = e.ChequeNumber;
            txtDocumentNumber.Text = e.DocumentNumber;
            lblJournalNumber.Text = e.JournalNumber;
            txtTransactionDate.SelectedDate = e.TransactionDate.HasValue ? e.TransactionDate.Value : DateTime.Now;

            InitPaymentType();
            //cboPaymentType.Items.Clear();
            //PaymentTypeCollection ptColl = new PaymentTypeCollection();
            //if (cboModuleName.SelectedValue == "GL" || cboModuleName.SelectedValue == "A/P")
            //    ptColl.Query.Where(ptColl.Query.IsApPayment==true);
            //else if (cboModuleName.SelectedValue == "A/R")
            //    ptColl.Query.Where(ptColl.Query.IsArPayment == true);
            //ptColl.LoadAll();
            //foreach (PaymentType pt in ptColl)
            //{
            //    cboPaymentType.Items.Add(new RadComboBoxItem(pt.PaymentTypeName,pt.SRPaymentTypeID));
            //}
            cboPaymentType.SelectedValue = e.PaymentType;

            //method
            InitPaymentMethod();
            //cboPaymentMethod.Items.Clear();
            //cboPaymentMethod.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            //PaymentMethodCollection pmColl = new PaymentMethodCollection();
            //pmColl.Query.Where(pmColl.Query.SRPaymentTypeID == cboPaymentType.SelectedValue);
            //pmColl.LoadAll();

            //foreach (PaymentMethod pm in pmColl)
            //{
            //    cboPaymentMethod.Items.Add(new RadComboBoxItem(pm.PaymentMethodName, pm.SRPaymentMethodID));
            //}

            cboPaymentMethod.SelectedValue = e.PaymentMethod;
            txtDescription.Text = e.Description;
            chkIsCleared.Checked = e.IsCleared ?? false;

            txtDueDate.SelectedDate = e.DueDate;

            chkIsApproved.Checked = e.IsPosted ?? false;
            chkIsVoid.Checked = e.IsVoid ?? false;
            lblJournalNumber.Text = e.JournalNumber;

            txtFromTo.Text = e.ReceivedFromOrPaidTo ?? string.Empty;

            EmptyingRefference();

            switch (e.RefferenceIdentification)
            {
                case "1":
                    {
                        hfDetailIdRef.Value = e.DetailIdRef.HasValue ? e.DetailIdRef.Value.ToString() : "";
                        LoadDetailRef(e.DetailIdRef.Value);
                        break;
                    }
                case "2":
                    {
                        LoadPaymentReceive(e.PaymentNo);
                        break;
                    }
                case "3":
                    {
                        LoadPaymentAR(e.PaymentNo);
                        break;
                    }
                case "4":
                    {
                        LoadPaymentAP(e.PaymentNo);
                        break;
                    }
                case "5":
                    {
                        LoadReturnPO(e.PaymentNo, "5");
                        break;
                    }
                case "6":
                    {
                        LoadReturnPO(e.PaymentNo, "6");
                        break;
                    }
                case "7":
                    {
                        LoadReturnPO(e.PaymentNo, "7");
                        break;
                    }
            }

            if (pnlBKU.Visible)
            {
                var coa = new ChartOfAccountsQuery();
                coa.es.Top = 20;
                coa.Where(coa.ChartOfAccountId == (e.BkuAccountID ?? 0));
                coa.OrderBy(coa.ChartOfAccountId.Ascending);
                coa.Select(coa.ChartOfAccountId, coa.ChartOfAccountCode, coa.ChartOfAccountName);

                var dt = coa.LoadDataTable();
                var nr = dt.NewRow();
                nr["ChartOfAccountId"] = 0;
                nr["ChartOfAccountCode"] = "";
                nr["ChartOfAccountName"] = "";
                dt.Rows.InsertAt(nr, 0);

                cboBkuAccount.DataSource = dt;
                cboBkuAccount.DataBind();
                cboBkuAccount.SelectedValue = e.BkuAccountID.ToString();
            }

            OnGetStatusMenuApproval();

            OnGetStatusMenuVoid();

            GenerateGrid();
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !chkIsVoid.Checked;
        }

        public override bool OnGetStatusMenuEdit()
        {
            return true;
            //return !chkIsApproved.Checked;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }
        //public override bool OnGetStatusMenuUnApproval()
        //{
        //    return chkIsApproved.Checked || chkIsVoid.Checked;
        //}
        public override bool OnGetStatusMenuDelete()
        {
            return !chkIsApproved.Checked;
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            switch (programID)
            {
                case AppConstant.Report.CashTransactionVoucher:
                    printJobParameters.AddNew("pEntityId", TransactionId.ToString());
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                    break;

                default:
                    printJobParameters.AddNew("pEntityId", TransactionId.ToString());
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                    break;
            }
        }
        #endregion

        #region Private Method Standard
        private void SetEntityValue(CashTransaction entity)
        {
            string bankId = txtBankAccount.SelectedValue;
            //if (entity.es.RowState == System.Data.DataRowState.Added)
            //    bankId = txtBankAccount.SelectedValue;
            //else
            //    bankId = entity.BankId;

            var bankAccount = Bank.Get(bankId);

            entity.PostingId = 0;
            entity.BankId = bankId;
            entity.ChartOfAccountId = bankAccount.ChartOfAccountId;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.TransactionType = cboTransactionType.SelectedItem == null ? string.Empty : cboTransactionType.SelectedItem.Value;
            entity.PaymentType = cboPaymentType.SelectedItem == null ? string.Empty : cboPaymentType.SelectedItem.Value;
            entity.PaymentMethod = cboPaymentMethod.SelectedItem == null ? string.Empty : cboPaymentMethod.SelectedItem.Value;
            entity.NormalBalance = cboNormalBalance.SelectedValue;
            entity.Module = cboModuleName.SelectedValue;
            entity.CurrencyCode = cboCurrency.SelectedValue;
            entity.CurrencyRate = decimal.Parse(txtCurrencyRate.Text);
            entity.IsPosted = false;
            entity.IsCleared = chkIsCleared.Checked;
            entity.IsVoid = false;
            entity.ChequeNumber = txtChequeNumber.Text;
            entity.DocumentNumber = txtDocumentNumber.Text;
            entity.Description = txtDescription.Text;
            entity.DueDate = txtDueDate.SelectedDate;
            entity.ReceivedFromOrPaidTo = txtFromTo.Text;

            entity.VoidDate = new DateTime(1900, 1, 1);

            if (hfIdent.Value.Equals("1") && !hfDetailIdRef.Value.Equals(string.Empty))
            {
                entity.DetailIdRef = hfDetailIdRef.Value.ToInt();
            }

            //entity.JournalNumber = lblJournalNumber.Text;

            if (entity.es.RowState == System.Data.DataRowState.Added)
            {
                entity.JournalId = 0;
                entity.DateCreated = DateTime.Now;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
            else
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            if (pnlBKU.Visible) entity.BkuAccountID = cboBkuAccount.SelectedValue.ToInt();
        }

        private bool SaveEntity(CashTransaction entity)
        {
            //if (hfIdent.Value.Equals("3")) entity.PaymentARNoRef = lblRef1.Text;
            //if (hfIdent.Value.Equals("4")) entity.PaymentAPNoRef = lblRef1.Text;

            var PaymentNo = string.Empty;
            var SequenceNo = string.Empty;
            var PaymentInvoiceAR = string.Empty;
            var PaymentInvoiceAP = string.Empty;
            var ItemTransaction = string.Empty;

            if (hfIdent.Value.Equals("2"))
            {
                PaymentNo = lblRef1.Text;
                SequenceNo = hfSequenceNo.Value;
            }
            else if (hfIdent.Value.Equals("8"))
            {
                PaymentNo = lblRef1.Text;
                SequenceNo = hfSequenceNo.Value;
            }
            else if (hfIdent.Value.Equals("3"))
            {
                PaymentInvoiceAR = lblRef1.Text;
            }
            else if (hfIdent.Value.Equals("4"))
            {
                PaymentInvoiceAP = lblRef1.Text;
            }
            else
            {
                ItemTransaction = lblRef1.Text;
            }

            var ret = CashTransaction.AddNew(entity, PaymentNo, SequenceNo, PaymentInvoiceAR, PaymentInvoiceAP, ItemTransaction);
            TransactionId = ret; // to reset the screen
            return ret > 0;
        }

        private void MoveRecord(bool isNextRecord)
        {
            var q = new CashTransactionQuery();
            q.es.Top = 1;
            if (isNextRecord)
            {
                q.Where(q.TransactionId > TransactionId);
                q.OrderBy(q.TransactionId.Ascending);
            }
            else
            {
                q.Where(q.TransactionId < TransactionId);
                q.OrderBy(q.TransactionId.Descending);
            }

            var entity = new CashTransaction();
            if (entity.Load(q))
            {
                TransactionId = entity.TransactionId.Value;
                OnPopulateEntryControl(entity);
                grdVoucherEntryItem.Rebind();
            }
        }

        protected void cboModuleName_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            InitPaymentType();
            InitPaymentMethod();
        }

        protected void cboNormalBalance_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ShowBtnRefference();
            lblFromTo.Text = (cboNormalBalance.SelectedValue == "D") ? "Received From" : "Paid To";
        }

        protected void txtBankAccount_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //ShowBtnRefference();

            if (string.IsNullOrEmpty(e.Value)) return;
            var bank = new Bank();
            bank.LoadByPrimaryKey(e.Value);

            var coa1 = new ChartOfAccounts();
            coa1.LoadByPrimaryKey(bank.ChartOfAccountId ?? 0);

            if (pnlBKU.Visible)
            {
                var coa = new ChartOfAccountsQuery();
                coa.es.Top = 20;
                coa.Where(coa.ChartOfAccountId == (coa1.BkuAccountID ?? 0));
                coa.OrderBy(coa.ChartOfAccountId.Ascending);
                cboBkuAccount.DataSource = coa.LoadDataTable();
                cboBkuAccount.DataBind();
                cboBkuAccount.SelectedValue = coa1.BkuAccountID.ToString();
            }
        }

        private void ShowBtnRefference()
        {
            btnCrossRefference.Visible = false;
            btnRefferencePaymentReceive.Visible = false;
            btnRefferencePaymentAR.Visible = false;
            btnRefferencePaymentAP.Visible = false;
            btnDownPayment.Visible = false;

            cboReferenceType.Items.Clear();
            cboReferenceType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));


            if (DataModeCurrent != AppEnum.DataMode.Read)
            {
                if (cboNormalBalance.SelectedValue.Equals("K"))
                {
                    var code = txtBankAccount.SelectedValue;
                    var entity = Bank.Get(code);
                    if (entity == null) return;
                    if (entity.IsCrossRefference ?? false)
                    {
                        //btnCrossRefference.Visible = true;
                        cboReferenceType.Items.Add(new RadComboBoxItem("Cross Reference Cash Entry", "01"));
                        return;
                    }

                    //btnRefferencePaymentAP.Visible = true;
                    cboReferenceType.Items.Add(new RadComboBoxItem("Reference To AP Payment", "04"));

                    //btnDownPayment.Visible = true;
                    cboReferenceType.Items.Add(new RadComboBoxItem("Reference To Purchase Order (Down Payment)", "05"));

                    cboReferenceType.Items.Add(new RadComboBoxItem("Reference To Purchase Order Receive", "07"));

                    if (AppSession.Parameter.IsAllowPaymentReturnFromCashEntry)
                    {
                        cboReferenceType.Items.Add(new RadComboBoxItem("Reference To Payment Return (Patient)", "08"));
                    }

                    if (AppSession.Parameter.acc_IsAutoJournalPayroll)
                    {
                        cboReferenceType.Items.Add(new RadComboBoxItem("Reference To Payroll", "09"));
                    }
                }
                else
                {
                    //btnRefferencePaymentReceive.Visible = true;
                    cboReferenceType.Items.Add(new RadComboBoxItem("Reference To Patient Receive", "02"));

                    //btnRefferencePaymentAR.Visible = true;
                    cboReferenceType.Items.Add(new RadComboBoxItem("Reference To AR Payment", "03"));

                    cboReferenceType.Items.Add(new RadComboBoxItem("Reference To Purchase Order Return (Cash)", "06"));
                }
            }
        }
        #endregion

        #region Record Detail Method Function
        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdVoucherEntryItem.Columns[0].Visible = isVisible;
            grdVoucherEntryItem.Columns[grdVoucherEntryItem.Columns.Count - 1].Visible = isVisible;
            grdVoucherEntryItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            if (!isVisible)
            {
                foreach (var item in grdVoucherEntryItem.MasterTableView.GetItems())
                {
                    item.Visible = false;
                }
            }
            grdVoucherEntryItem.Rebind();
        }

        protected void GenerateGrid()
        {
            if (!IsPostBack)
            {
                var sortExpr = new GridSortExpression
                {
                    FieldName = JournalTransactionDetailsMetadata.ColumnNames.DetailId,
                    SortOrder = GridSortOrder.Ascending
                };

                grdVoucherEntryItem.MasterTableView.SortExpressions.AddSortExpression(sortExpr);
                grdVoucherEntryItem.MasterTableView.SortExpressions.AllowNaturalSort = false;
            }

            var sb = new System.Text.StringBuilder();
            foreach (GridSortExpression e in grdVoucherEntryItem.MasterTableView.SortExpressions)
            {
                sb.AppendFormat("{0}^{1}", grdVoucherEntryItem.MasterTableView.SortExpressions[0].FieldName, grdVoucherEntryItem.MasterTableView.SortExpressions[0].SortOrder);
                sb.Append(",");
            }

            var totalCount = CashTransactionDetail.TotalCount(TransactionId);
            grdVoucherEntryItem.VirtualItemCount = totalCount;

            var en = CashTransactionDetail.GetAllForTransactionWithPaging(TransactionId, grdVoucherEntryItem.CurrentPageIndex, grdVoucherEntryItem.PageSize);
            var items = en.Select(e => new GridItem(e)).ToList();

            grdVoucherEntryItem.DataSource = items;

            if (totalCount == 0)
            {
                txtBankAccount.Enabled = true;
                cboModuleName.Enabled = true;
                cboNormalBalance.Enabled = true;
            }
        }

        protected void grdVoucherEntryItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            GenerateGrid();
        }

        protected void grdVoucherEntryItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var ctl = (CashEntryItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (ctl == null) return;

            if (!IsEnableEdit(TransactionId))
            {
                e.Canceled = true;
                return;
            }

            var entity = CashTransactionDetail.Get(TransactionId, ctl.DetailId);
            if (entity != null)
            {
                var rate = Decimal.Parse(txtCurrencyRate.Text);
                if (rate == 0)
                    rate = 1;

                decimal debitAmount = 0;
                decimal creditAmount = 0;

                //if (cboNormalBalance.SelectedValue == "K")
                //    debitAmount = rate * ctl.Amount;
                //else
                //    creditAmount = rate*ctl.Amount;
                if (cboNormalBalance.SelectedValue == "K")
                {
                    if (ctl.Amount > 0)
                        debitAmount = rate * ctl.Amount;
                    else
                        creditAmount = rate * ctl.Amount * -1;

                }
                else
                {
                    if (ctl.Amount > 0)
                        creditAmount = rate * ctl.Amount;
                    else
                        debitAmount = rate * ctl.Amount * -1;
                }

                entity.ListID = ctl.CashListId;
                entity.Amount = ctl.Amount;
                entity.Debit = debitAmount;
                entity.Credit = creditAmount;
                entity.Description = ctl.Description;
                entity.SubLedgerId = ctl.SubLedgerId;
                entity.IsParentRefference = ctl.IsParentRefference;
                entity.Save();
            }
            GenerateGrid();

            CalculateCrossRefBalance();
        }

        private void CalculateCrossRefBalance()
        {
            if (hfIdent.Value == "1")
            {
                decimal initBalance = System.Convert.ToDecimal(hfRef3.Value);
                decimal transAmount = 0;
                foreach (GridDataItem r in grdVoucherEntryItem.MasterTableView.Items)
                {
                    transAmount += System.Convert.ToDecimal(r["Amount"].Text);
                }

                lblRef3.Text = (initBalance - transAmount).ToString("0,###.##");
            }
        }

        protected void grdVoucherEntryItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            if (!IsEnableEdit(TransactionId))
            {
                e.Canceled = true;
                return;
            }

            int id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][CashTransactionDetailMetadata.ColumnNames.DetailId]);
            var entity = CashTransactionDetail.Get(TransactionId, id);
            if (entity != null)
            {
                var tpi = entity.SetUnLinkToPaymentReceive();
                var tpiRet = entity.SetUnLinkToPaymentReceiveReturn();
                entity.MarkAsDeleted();

                if (tpi != null) tpi.Save();
                if (tpiRet != null) tpiRet.Save();
                entity.Save();
            }
            GenerateGrid();

            CalculateCrossRefBalance();
        }

        protected void grdVoucherEntryItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                e.Canceled = true;
                return;
            }

            var ctl = (CashEntryItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (ctl == null) return;

            int accId = validateChartOfAccount(ctl);
            if (accId == 0)
            {
                e.Canceled = true;
                return;
            }

            bool newlyCreated = false;
            if (TransactionId == 0)
            {
                bool isValid = false;

                var journalCode = Bank.Get(txtBankAccount.SelectedValue);
                isValid = (journalCode != null);
                if (!isValid)
                {
                    e.Canceled = true;
                    return;
                }

                if (!Helper.ValidatePeriode(txtTransactionDate.SelectedDate.Value))
                {
                    e.Canceled = true;
                    return;
                }

                if (cboNormalBalance.SelectedValue == "")
                {
                    e.Canceled = true;
                    return;
                }

                var entity = new CashTransaction();
                entity.AddNew();
                SetEntityValue(entity);
                if (!SaveEntity(entity))
                {
                    e.Canceled = true;
                    return;
                }
                OnPopulateEntryControl(entity);
                OnDataModeChanged(AppEnum.DataMode.New, AppEnum.DataMode.Edit);

                newlyCreated = true;
            }

            if (!newlyCreated && !IsEnableEdit(TransactionId))
            {
                e.Canceled = true;
                return;
            }

            var rate = Decimal.Parse(txtCurrencyRate.Text);
            if (rate == 0)
                rate = 1;

            decimal debitAmount = 0;
            decimal creditAmount = 0;

            if (cboNormalBalance.SelectedValue == "K")
            {
                if (ctl.Amount > 0)
                    debitAmount = rate * ctl.Amount;
                else
                    creditAmount = rate * ctl.Amount * -1;

            }
            else
            {
                if (ctl.Amount > 0)
                    creditAmount = rate * ctl.Amount;
                else
                    debitAmount = rate * ctl.Amount * -1;
            }


            var detail = new CashTransactionDetail
            {
                TransactionId = this.TransactionId,
                ChartOfAccountId = accId,
                Debit = debitAmount,
                Credit = creditAmount,
                Amount = ctl.Amount,
                Description = ctl.Description.Trim(),
                SubLedgerId = ctl.SubLedgerId,
                CostCenterId = 0,
                ListID = ctl.CashListId,
                IsParentRefference = ctl.IsParentRefference
            };

            detail.AddNew();
            detail.Save();

            e.Canceled = true;
            GenerateGrid();
            grdVoucherEntryItem.Rebind();

            CalculateCrossRefBalance();
        }

        private int validateChartOfAccount(CashEntryItemDetail ctl)
        {
            try
            {
                ChartOfAccounts entity = null;
                // try to extract selected value from radcombo box first
                int selectedValue = ctl.ChartOfAccountId;
                if (selectedValue != 0)
                    entity = ChartOfAccounts.Get(selectedValue);
                else
                    entity = ChartOfAccounts.Get(ctl.ChartOfAccountCode);

                if (entity != null && ((entity.IsDetail.Value) /*&& (entity.AccountLevel == 4)*/))
                    return entity.ChartOfAccountId.Value;
            }
            catch
            {
            }

            return 0;
        }

        private bool IsEnableEdit(int journalId)
        {
            if (journalId > 0)
            {
                var entity = CashTransaction.Get(journalId);
                return (Helper.ValidatePeriode(entity.TransactionDate.Value) && !entity.IsPosted.Value);
            }
            return false;
        }
        #endregion

        private class GridItem
        {
            private CashTransactionDetail entity;

            public GridItem(CashTransactionDetail entity)
            {
                this.entity = entity;
            }

            public int DetailId
            {
                get { return entity.DetailId.Value; }
            }
            public string ListId
            {
                get { return entity.ListID; }
            }
            public int ChartOfAccountId
            {
                get { return entity.ChartOfAccountId.Value; }
            }
            public string ChartOfAccountCode
            {
                get { return entity.ChartOfAccountCode; }
            }
            public string ChartOfAccountName
            {
                get { return entity.ChartOfAccountName; }
            }

            public int SubLedgerId
            {
                get { return entity.SubLedgerId.Value; }
            }

            public string SubLedgerName
            {
                get { return entity.SubLedgerName; }
            }
            public decimal Amount
            {
                get { return entity.Amount.Value; }
            }
            public string Description
            {
                get { return entity.Description; }
            }
            public bool IsParentRefference
            {
                get { return entity.IsParentRefference ?? false; }
            }
        }

        private CashTransactionDetailCollection CashTransactionDetails
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCashTransactionDetail"];
                    if (obj != null) return ((CashTransactionDetailCollection)(obj));
                }

                var coll = new CashTransactionDetailCollection();
                coll.Query.Where(coll.Query.TransactionId == TransactionId);
                coll.Query.Load();

                Session["collCashTransactionDetail"] = coll;

                return coll;
            }
            set { Session["collCashTransactionDetail"] = value; }
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (sourceControl is RadGrid && eventArgument == "rebind")
            {
                if (TransactionId == 0)
                {
                    var entity = new CashTransaction();
                    entity.AddNew();
                    SetEntityValue(entity);
                    SaveEntity(entity);

                    OnPopulateEntryControl(entity);
                    OnDataModeChanged(AppEnum.DataMode.New, AppEnum.DataMode.Edit);
                }

                foreach (var c in CashTransactionDetails)
                {
                    c.TransactionId = TransactionId;
                }

                CashTransactionDetails.Save();

                GenerateGrid();
                grdVoucherEntryItem.Rebind();
            }
            else if (sourceControl is RadGrid)
            {
                // load ref
                var param = eventArgument.Split('|');
                if (param[0] == "rebind")
                {
                    if (param.Count() >= 4)
                    {
                        if (param[3].Equals("PaymentReceive"))
                        {
                            var PaymentNo = param[1];
                            //var SequenceNo = param[2];
                            LoadPaymentReceive(PaymentNo);
                        }
                        else if (param[3].Equals("PaymentAR"))
                        {
                            var PaymentNoAR = param[1];
                            var SequenceNo = param[2]; // empty
                            LoadPaymentAR(PaymentNoAR);
                        }
                        else if (param[3].Equals("PaymentAP"))
                        {
                            var PaymentNoAP = param[1];
                            var SequenceNo = param[2]; // empty
                            LoadPaymentAP(PaymentNoAP);
                        }
                        else if (param[3].Equals("ReturnPO"))
                        {
                            var ReturnNo = param[1];
                            LoadReturnPO(ReturnNo, "6");
                        }
                        else if (param[3].Equals("ReceivePO"))
                        {
                            var ReturnNo = param[1];
                            LoadReturnPO(ReturnNo, "7");
                        }
                        else if (param[3].Equals("PO"))
                        {
                            var ReturnNo = param[1];
                            LoadReturnPO(ReturnNo, "5");
                        }
                        if (param[3].Equals("PaymentReceiveReturn"))
                        {
                            var PaymentNo = param[1];
                            //var SequenceNo = param[2];
                            LoadPaymentReceiveReturn(PaymentNo);
                        }
                        if (param[3].Equals("Payroll"))
                        {
                            var periodCode = param[1];
                            LoadPayroll(periodCode);
                        }
                    }
                    else
                    {
                        if (param[1] != "undefined")
                        {
                            // cash transaction detail id
                            int ctdId = System.Convert.ToInt32(param[1]);
                            LoadDetailRef(ctdId);
                        }
                    }
                }
            }
        }

        private bool LoadDetailRef(int CashTransactionDetailID)
        {
            var ctd = new CashTransactionDetail();
            if (ctd.LoadByPrimaryKey(CashTransactionDetailID))
            {
                hfDetailIdRef.Value = CashTransactionDetailID.ToString();
                hfIdent.Value = "1";

                lblRef2.Text = ctd.Description;
                var ct = new CashTransaction();
                if (ct.LoadByPrimaryKey(ctd.TransactionId.Value))
                {
                    lblRef1.Text = ct.TransactionDate.Value.ToString("dd/MM/yyyy");
                }

                lblRef3.Text = (ctd.Debit.Value - ctd.Credit.Value).ToString("0,###.##");
                hfRef3.Value = ctd.Debit.Value.ToString();
                // realization
                var realisasiColl = new CashTransactionDetailCollection();
                var dttbl = realisasiColl.GetCashTransactionRealizationDetail(CashTransactionDetailID);
                if (dttbl.Rows.Count > 0)
                {
                    lblRef3.Text = ((decimal)dttbl.Rows[dttbl.Rows.Count - 1]["Balance"]).ToString("0,###.##");
                    hfRef3.Value = ((decimal)dttbl.Rows[dttbl.Rows.Count - 1]["Balance"]).ToString();
                }

                return true;
            }
            return false;
        }

        private bool LoadPaymentReceive_(string PaymentNo, string SequenceNo)
        {
            if (string.IsNullOrEmpty(PaymentNo) && string.IsNullOrEmpty(SequenceNo)) return false;

            var tp = new TransPayment();
            if (tp.LoadByPrimaryKey(PaymentNo))
            {
                var tpi = new TransPaymentItem();
                if (tpi.LoadByPrimaryKey(PaymentNo, SequenceNo))
                {
                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(tp.RegistrationNo))
                    {
                        var pat = new Patient();
                        if (pat.LoadByPrimaryKey(reg.PatientID))
                        {
                            lblRef1.Text = tp.PaymentNo;
                            hfSequenceNo.Value = tpi.SequenceNo;

                            lblRef2.Text = pat.PatientName;
                            lblRef3.Text = tpi.Amount.Value.ToString("0,###.##");
                            hfIdent.Value = "2";

                            if (!string.IsNullOrEmpty(tpi.SRCardProvider))
                            {
                                var cp = new AppStandardReferenceItem();
                                cp.Query.Where(cp.Query.StandardReferenceID.Equal("CardProvider"), cp.Query.ItemID.Equal(tpi.SRCardProvider));
                                if (cp.Load(cp.Query))
                                {
                                    lblRef2.Text += ", Card Provider:" + cp.ItemName;
                                }
                            }

                            if (!string.IsNullOrEmpty(tpi.SRCardType))
                            {
                                var ct = new AppStandardReferenceItem();
                                ct.Query.Where(ct.Query.StandardReferenceID.Equal("CardType"), ct.Query.ItemID.Equal(tpi.SRCardType));
                                if (ct.Load(ct.Query))
                                {
                                    lblRef2.Text += ", Card Type:" + ct.ItemName;
                                }
                            }
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        private TransPaymentItemCollection ParseTPI(string PaymentNoSequenceNo)
        {
            var PayNoSeqNos = PaymentNoSequenceNo.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries);

            var PayNos = PayNoSeqNos.Select(x => x.Split('^')[0]);

            var tpColl = new TransPaymentItemCollection();
            tpColl.Query.Where(tpColl.Query.PaymentNo.In(PayNos));
            if (tpColl.LoadAll())
            {
                var tpToDel = tpColl.Where(x => !PayNoSeqNos.Contains(x.PaymentNo + "^" + x.SequenceNo));
                foreach (var ttd in tpToDel)
                {
                    tpColl.DetachEntity(ttd);
                }
            }
            return tpColl;
        }

        private bool LoadPaymentReceive(string PaymentNoSequenceNo)
        {
            if (txtDescription.Text.Trim() == string.Empty) txtDescription.Text = "-";
            Page.Validate();
            if (!Page.IsValid)
            {
                return false;
            }
            var journalCode = Bank.Get(txtBankAccount.SelectedValue);
            var isValid = (journalCode != null);
            if (!isValid)
            {
                return false;
            }

            if (!Helper.ValidatePeriode(txtTransactionDate.SelectedDate.Value))
            {
                return false;
            }

            if (string.IsNullOrEmpty(PaymentNoSequenceNo)) return false;
            var tpiColl = ParseTPI(PaymentNoSequenceNo);
            if (tpiColl.Count > 0)
            {

                lblRef1.Text = string.Empty;
                hfSequenceNo.Value = PaymentNoSequenceNo;

                lblRef3.Text = tpiColl.Sum(x => ((x.IsFromDownPayment ?? false) ? (-x.Balance.Value) : (!string.IsNullOrEmpty(x.ReferenceNo) ? -x.Amount.Value : x.Amount.Value))).ToString("N2");
                hfIdent.Value = "2";

                if (txtDescription.Text == string.Empty) txtDescription.Text = "-";

                // default fee account
                var coaColl = new ChartOfAccountsCollection();
                var defaultFeeCOA = JournalTransactions.GetCachedAccountFromParam("coa_PaymentCardFeeAmt");

                foreach (var tpi in tpiColl)
                {
                    if (tpi.CashTransactionReconcileId != null) continue;

                    var tp = new TransPayment();
                    tp.LoadByPrimaryKey(tpi.PaymentNo);

                    var cisi = tpi.GetChartOfAccountIdSubLedgerIdFromPayment(tp.TransactionCode);

                    var pat = new Patient();

                    var patq = new PatientQuery("pat");
                    var regq = new RegistrationQuery("reg");
                    patq.InnerJoin(regq).On(patq.PatientID == regq.PatientID)
                        .Where(regq.RegistrationNo == tp.RegistrationNo)
                        .Select(patq);
                    var patColl = new PatientCollection();
                    if (patColl.Load(patq))
                    {
                        pat = patColl.First();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(tp.PatientID))
                        {
                            pat.LoadByPrimaryKey(tp.PatientID);
                        }
                    }

                    bool newlyCreated = false;
                    if (TransactionId == 0)
                    {
                        var entity = new CashTransaction();
                        entity.AddNew();

                        // set
                        string bankId = string.Empty;
                        if (entity.es.RowState == System.Data.DataRowState.Added)
                            bankId = txtBankAccount.SelectedValue;
                        else
                            bankId = entity.BankId;

                        var bankAccount = Bank.Get(bankId);

                        entity.PostingId = 0;
                        entity.BankId = txtBankAccount.SelectedValue;
                        entity.ChartOfAccountId = bankAccount.ChartOfAccountId;
                        entity.TransactionDate = txtTransactionDate.SelectedDate;
                        entity.TransactionType = cboTransactionType.SelectedItem == null ? string.Empty : cboTransactionType.SelectedItem.Value;
                        entity.PaymentType = cboPaymentType.SelectedItem == null ? string.Empty : cboPaymentType.SelectedItem.Value;
                        entity.PaymentMethod = cboPaymentMethod.SelectedItem == null ? string.Empty : cboPaymentMethod.SelectedItem.Value;
                        entity.NormalBalance = cboNormalBalance.SelectedValue;
                        entity.Module = cboModuleName.SelectedValue;
                        entity.CurrencyCode = cboCurrency.SelectedValue;
                        entity.CurrencyRate = decimal.Parse(txtCurrencyRate.Text);
                        entity.IsPosted = false;
                        entity.IsCleared = chkIsCleared.Checked;
                        entity.IsVoid = false;
                        entity.ChequeNumber = txtChequeNumber.Text;
                        entity.DocumentNumber = txtDocumentNumber.Text;
                        entity.Description = txtDescription.Text;
                        entity.DueDate = txtDueDate.SelectedDate;
                        entity.ReceivedFromOrPaidTo = txtFromTo.Text;

                        entity.VoidDate = new DateTime(1900, 1, 1);

                        if (hfIdent.Value.Equals("1") && !hfDetailIdRef.Value.Equals(string.Empty))
                        {
                            entity.DetailIdRef = hfDetailIdRef.Value.ToInt();
                        }

                        //entity.JournalNumber = lblJournalNumber.Text;

                        if (entity.es.RowState == System.Data.DataRowState.Added)
                        {
                            entity.JournalId = 0;
                            entity.DateCreated = DateTime.Now;
                            entity.LastUpdateDateTime = DateTime.Now;
                            entity.CreatedBy = AppSession.UserLogin.UserID;
                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        }
                        else
                        {
                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            entity.LastUpdateDateTime = DateTime.Now;
                        }
                        if (pnlBKU.Visible) entity.BkuAccountID = cboBkuAccount.SelectedValue.ToInt();

                        // set

                        if (!SaveEntity(entity))
                        {
                            return false;
                        }
                        OnPopulateEntryControl(entity);
                        OnDataModeChanged(AppEnum.DataMode.New, AppEnum.DataMode.Edit);

                        newlyCreated = true;
                    }

                    if (!newlyCreated && !IsEnableEdit(TransactionId))
                    {
                        return false;
                    }

                    var rate = Decimal.Parse(txtCurrencyRate.Text);
                    if (rate == 0)
                        rate = 1;

                    decimal debitAmount = 0;
                    decimal creditAmount = 0;

                    if (cboNormalBalance.SelectedValue == "K")
                    {
                        if (tpi.IsFromDownPayment ?? false)
                        {
                            if (tpi.Balance > 0)
                                creditAmount = rate * tpi.Balance.Value;
                            else
                                debitAmount = rate * tpi.Balance.Value * -1;
                        }
                        else
                        {
                            if (tpi.Amount > 0)
                                debitAmount = rate * tpi.Amount.Value;
                            else
                                creditAmount = rate * tpi.Amount.Value * -1;
                        }
                    }
                    else
                    {
                        if (tpi.IsFromDownPayment ?? false)
                        {
                            if (tpi.Balance > 0)
                                debitAmount = rate * tpi.Balance.Value;
                            else
                                creditAmount = rate * tpi.Balance.Value * -1;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(tpi.ReferenceNo))
                            {
                                if (tpi.Amount > 0)
                                    debitAmount = rate * tpi.Amount.Value;

                                else
                                    creditAmount = rate * tpi.Amount.Value * -1;
                            }
                            else
                            {
                                if (tpi.Amount > 0)
                                    creditAmount = rate * tpi.Amount.Value;
                                else
                                    debitAmount = rate * tpi.Amount.Value * -1;
                            }
                        }
                    }


                    var detail = new CashTransactionDetail
                    {
                        TransactionId = this.TransactionId,
                        ChartOfAccountId = cisi[0],
                        Debit = debitAmount,
                        Credit = creditAmount,
                        Amount = creditAmount - debitAmount,
                        Description = string.Format("{0}PayNo#:{1}, RegNo#:{2}, Name:{3}",
                            txtDescription.Text == "-" ? "" : (AppSession.Parameter.HealthcareInitialAppsVersion == "RSMM" ? "" : (txtDescription.Text + ". ")),
                            tpi.PaymentNo,
                            tp.RegistrationNo,
                            pat.PatientName),
                        SubLedgerId = cisi[1],
                        CostCenterId = 0,
                        ListID = null,
                        IsParentRefference = false,
                        ReferenceNo = tpi.PaymentNo
                    };
                    detail.AddNew();
                    detail.Save();

                    detail.SetLinkToPaymentReceive(tpi);

                    // fee yang ditanggung rs
                    if (!string.IsNullOrEmpty(tpi.EDCMachineID) && !string.IsNullOrEmpty(tpi.SRCardType))
                    {
                        var feeCoa = defaultFeeCOA;
                        var feeSL = 0;
                        var edct = new EDCMachineTariff();
                        if (edct.LoadByPrimaryKey(tpi.EDCMachineID, tpi.SRCardType))
                        {
                            if (!(edct.IsChargedToPatient ?? false) && ((edct.EDCMachineTariff ?? 0) > 0))
                            {
                                if ((edct.ChartOfAccountId ?? 0) > 0)
                                {
                                    JournalTransactions.ValidateCOA(edct.ChartOfAccountId.Value, coaColl, string.Format("EDCMachine {0} CardType {1}", edct.EDCMachineID, edct.SRCardType));
                                    feeCoa = edct.ChartOfAccountId.Value;
                                    feeSL = edct.SubledgerID ?? 0;
                                }
                                else
                                {
                                    JournalTransactions.ValidateCOA(feeCoa, coaColl, "AppParameter:coa_PaymentCardFeeAmt");
                                }

                                decimal feeAmt = edct.EDCMachineTariff.Value / 100 * tpi.Amount.Value;

                                var detailFee = new CashTransactionDetail
                                {
                                    TransactionId = this.TransactionId,
                                    ChartOfAccountId = feeCoa,
                                    Debit = feeAmt >= 0 ? feeAmt : 0,
                                    Credit = feeAmt < 0 ? (feeAmt * -1) : 0,
                                    Amount = -feeAmt,
                                    Description = string.Format("{0}PayNo#:{1}, RegNo#:{2}", txtDescription.Text == "-" ? "" : (txtDescription.Text + ". Card Fee "), tpi.PaymentNo, tp.RegistrationNo),
                                    SubLedgerId = feeSL,
                                    CostCenterId = 0,
                                    ListID = null,
                                    IsParentRefference = false
                                };
                                detailFee.AddNew();
                                detailFee.Save();

                            }
                        }
                    }

                    GenerateGrid();
                    grdVoucherEntryItem.Rebind();

                    CalculateCrossRefBalance();
                }
                tpiColl.Save();

                // reload ref


                return true;
            }

            return false;
        }

        private bool LoadPaymentReceiveReturn(string PaymentNoSequenceNo)
        {
            if (txtDescription.Text.Trim() == string.Empty) txtDescription.Text = "-";
            Page.Validate();
            if (!Page.IsValid)
            {
                return false;
            }
            var journalCode = Bank.Get(txtBankAccount.SelectedValue);
            var isValid = (journalCode != null);
            if (!isValid)
            {
                return false;
            }

            if (!Helper.ValidatePeriode(txtTransactionDate.SelectedDate.Value))
            {
                return false;
            }

            if (string.IsNullOrEmpty(PaymentNoSequenceNo)) return false;
            var tpiColl = ParseTPI(PaymentNoSequenceNo);
            if (tpiColl.Count > 0)
            {

                lblRef1.Text = string.Empty;
                hfSequenceNo.Value = PaymentNoSequenceNo;

                lblRef3.Text = tpiColl.Sum(x => x.Balance ?? 0).ToString("N2");
                hfIdent.Value = "8";

                if (txtDescription.Text == string.Empty) txtDescription.Text = "-";

                // default fee account
                var coaColl = new ChartOfAccountsCollection();
                var coaReturnPayable = JournalTransactions.GetCachedAccountFromParam("coa_DownPaymentReturnPayable");

                foreach (var tpi in tpiColl)
                {
                    if (tpi.BackOfficeReturnTransactionId != null) continue;

                    var tp = new TransPayment();
                    tp.LoadByPrimaryKey(tpi.PaymentNo);

                    bool newlyCreated = false;
                    if (TransactionId == 0)
                    {
                        var entity = new CashTransaction();
                        entity.AddNew();

                        // set
                        string bankId = string.Empty;
                        if (entity.es.RowState == System.Data.DataRowState.Added)
                            bankId = txtBankAccount.SelectedValue;
                        else
                            bankId = entity.BankId;

                        var bankAccount = Bank.Get(bankId);

                        entity.PostingId = 0;
                        entity.BankId = txtBankAccount.SelectedValue;
                        entity.ChartOfAccountId = bankAccount.ChartOfAccountId;
                        entity.TransactionDate = txtTransactionDate.SelectedDate;
                        entity.TransactionType = cboTransactionType.SelectedItem == null ? string.Empty : cboTransactionType.SelectedItem.Value;
                        entity.PaymentType = cboPaymentType.SelectedItem == null ? string.Empty : cboPaymentType.SelectedItem.Value;
                        entity.PaymentMethod = cboPaymentMethod.SelectedItem == null ? string.Empty : cboPaymentMethod.SelectedItem.Value;
                        entity.NormalBalance = cboNormalBalance.SelectedValue;
                        entity.Module = cboModuleName.SelectedValue;
                        entity.CurrencyCode = cboCurrency.SelectedValue;
                        entity.CurrencyRate = decimal.Parse(txtCurrencyRate.Text);
                        entity.IsPosted = false;
                        entity.IsCleared = chkIsCleared.Checked;
                        entity.IsVoid = false;
                        entity.ChequeNumber = txtChequeNumber.Text;
                        entity.DocumentNumber = txtDocumentNumber.Text;
                        entity.Description = txtDescription.Text;
                        entity.DueDate = txtDueDate.SelectedDate;
                        entity.ReceivedFromOrPaidTo = txtFromTo.Text;

                        entity.VoidDate = new DateTime(1900, 1, 1);

                        if (hfIdent.Value.Equals("1") && !hfDetailIdRef.Value.Equals(string.Empty))
                        {
                            entity.DetailIdRef = hfDetailIdRef.Value.ToInt();
                        }

                        //entity.JournalNumber = lblJournalNumber.Text;

                        if (entity.es.RowState == System.Data.DataRowState.Added)
                        {
                            entity.JournalId = 0;
                            entity.DateCreated = DateTime.Now;
                            entity.LastUpdateDateTime = DateTime.Now;
                            entity.CreatedBy = AppSession.UserLogin.UserID;
                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        }
                        else
                        {
                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            entity.LastUpdateDateTime = DateTime.Now;
                        }
                        // set
                        if (pnlBKU.Visible) entity.BkuAccountID = cboBkuAccount.SelectedValue.ToInt();

                        if (!SaveEntity(entity))
                        {
                            return false;
                        }
                        OnPopulateEntryControl(entity);
                        OnDataModeChanged(AppEnum.DataMode.New, AppEnum.DataMode.Edit);

                        newlyCreated = true;
                    }

                    if (!newlyCreated && !IsEnableEdit(TransactionId))
                    {
                        return false;
                    }

                    var rate = Decimal.Parse(txtCurrencyRate.Text);
                    if (rate == 0)
                        rate = 1;

                    decimal debitAmount = 0;
                    decimal creditAmount = 0;

                    if (cboNormalBalance.SelectedValue == "K")
                    {
                        if (tpi.Amount > 0)
                            debitAmount = rate * tpi.Amount.Value;
                        else
                            creditAmount = rate * tpi.Amount.Value * -1;

                    }
                    else
                    {
                        if (tpi.Amount > 0)
                            creditAmount = rate * tpi.Amount.Value;
                        else
                            debitAmount = rate * tpi.Amount.Value * -1;
                    }


                    var detail = new CashTransactionDetail
                    {
                        TransactionId = this.TransactionId,
                        ChartOfAccountId = coaReturnPayable,
                        Debit = debitAmount,
                        Credit = creditAmount,
                        Amount = tpi.Balance,
                        Description = string.Format("{0}PayNo#:{1}, RegNo#:{2}", txtDescription.Text == "Down Payment Return " ? "" : (txtDescription.Text + ". "), tpi.PaymentNo, tp.RegistrationNo),
                        SubLedgerId = 0,
                        CostCenterId = 0,
                        ListID = null,
                        IsParentRefference = false,
                        ReferenceNo = tpi.PaymentNo
                    };
                    detail.AddNew();
                    detail.Save();

                    detail.SetLinkToPaymentReceiveReturn(tpi);

                    GenerateGrid();
                    grdVoucherEntryItem.Rebind();

                    CalculateCrossRefBalance();
                }
                tpiColl.Save();

                // reload ref


                return true;
            }

            return false;
        }


        private bool LoadPaymentAR(string PaymentNoAR)
        {
            if (string.IsNullOrEmpty(PaymentNoAR)) return false;

            var i = new Invoices();
            if (i.LoadByPrimaryKey(PaymentNoAR))
            {
                var ii = new InvoicesItemCollection();
                ii.Query.Where(ii.Query.InvoiceNo.Equal(i.InvoiceNo));
                if (ii.LoadAll())
                {
                    lblRef1.Text = i.InvoiceNo;
                    hfSequenceNo.Value = string.Empty;

                    var g = new Guarantor();
                    if (g.LoadByPrimaryKey(i.GuarantorID))
                    {
                        lblRef2.Text = g.GuarantorName;

                        decimal Amount = 0;
                        foreach (var ii_ in ii)
                        {
                            Amount += ii_.PaymentAmount.Value +
                                ii_.BankCost.Value + ii_.OtherAmount.Value;
                        }

                        lblRef3.Text = Amount.ToString("0,###.##");
                        hfIdent.Value = "3";
                    }
                    return true;
                }
            }
            return false;
        }
        private bool LoadPaymentAP(string PaymentNoAP)
        {
            if (string.IsNullOrEmpty(PaymentNoAP)) return false;

            var i = new InvoiceSupplier();
            if (i.LoadByPrimaryKey(PaymentNoAP))
            {
                var ii = new InvoiceSupplierItemCollection();
                ii.Query.Where(ii.Query.InvoiceNo.Equal(i.InvoiceNo));
                if (ii.LoadAll())
                {
                    lblRef1.Text = i.InvoiceNo;
                    hfSequenceNo.Value = string.Empty;

                    var s = new Supplier();
                    if (s.LoadByPrimaryKey(i.SupplierID))
                    {
                        lblRef2.Text = s.SupplierName;

                        decimal Amount = 0;
                        foreach (var ii_ in ii)
                        {
                            Amount += (ii_.PaymentAmount ?? 0) +
                                (ii_.OtherDeduction ?? 0);
                        }

                        lblRef3.Text = Amount.ToString("0,###.##");
                        hfIdent.Value = "4";
                    }
                    return true;
                }
            }
            return false;
        }

        private bool LoadReturnPO(string ReturnNo, string value)
        {
            if (string.IsNullOrEmpty(ReturnNo)) return false;

            var i = new ItemTransaction();
            if (i.LoadByPrimaryKey(ReturnNo))
            {
                lblRef1.Text = ReturnNo;
                //hfRef1.Value = string.Empty;

                hfSequenceNo.Value = string.Empty;

                var s = new Supplier();
                if (s.LoadByPrimaryKey(i.BusinessPartnerID)) lblRef2.Text = s.SupplierName;
                //hfRef2.Value = string.Empty;

                //ChargesAmount,TaxAmount,PPH22Amount,PPH23Amount,StampAmount,OtherDeduction,DownPaymentAmount,PphAmount
                //{0}+{1}+{2}+{3}+{4}-{5}-{6}-{7}

                decimal Amount = i.ChargesAmount ?? 0 + i.TaxAmount ?? 0 + i.Pph22 ?? 0 + i.Pph23 ?? 0 + i.StampAmount ?? 0 - i.AdvanceAmount ?? 0 - i.PphAmount ?? 0;

                lblRef3.Text = Amount.ToString("0,###.##");
                //hfRef3.Value = string.Empty;

                //hfDetailIdRef.Value = string.Empty;
                hfIdent.Value = value;

                return true;
            }
            return false;
        }

        //private bool LoadReceivePO(string ReturnNo)
        //{
        //    if (string.IsNullOrEmpty(ReturnNo)) return false;

        //    var i = new ItemTransaction();
        //    if (i.LoadByPrimaryKey(ReturnNo))
        //    {
        //        lblRef1.Text = ReturnNo;
        //        //hfRef1.Value = string.Empty;

        //        hfSequenceNo.Value = string.Empty;

        //        var s = new Supplier();
        //        if (s.LoadByPrimaryKey(i.BusinessPartnerID)) lblRef2.Text = s.SupplierName;
        //        //hfRef2.Value = string.Empty;

        //        //ChargesAmount,TaxAmount,PPH22Amount,PPH23Amount,StampAmount,OtherDeduction,DownPaymentAmount,PphAmount
        //        //{0}+{1}+{2}+{3}+{4}-{5}-{6}-{7}

        //        decimal Amount = i.ChargesAmount ?? 0 + i.TaxAmount ?? 0 + i.Pph22 ?? 0 + i.Pph23 ?? 0 + i.StampAmount ?? 0 - i.AdvanceAmount ?? 0 - i.PphAmount ?? 0;

        //        lblRef3.Text = Amount.ToString("0,###.##");
        //        //hfRef3.Value = string.Empty;

        //        //hfDetailIdRef.Value = string.Empty;
        //        hfIdent.Value = "6";

        //        return true;
        //    }
        //    return false;
        //}


        private bool LoadPayroll(string payrollPeriodCode)
        {
            if (txtDescription.Text.Trim() == string.Empty) txtDescription.Text = "-";
            Page.Validate();
            if (!Page.IsValid)
            {
                return false;
            }
            var journalCode = Bank.Get(txtBankAccount.SelectedValue);
            var isValid = (journalCode != null);
            if (!isValid)
            {
                return false;
            }

            if (!Helper.ValidatePeriode(txtTransactionDate.SelectedDate.Value))
            {
                return false;
            }

            if (string.IsNullOrEmpty(payrollPeriodCode)) return false;

            object obj = Session["CashEntryPayroll:ItemSelected" + Request.UserHostName];
            if (obj == null) 
                return false;

            //payrollPeriodCode = payrollPeriodCode.Replace("PYR", "");

            var jtq = new JournalTransactionsQuery();
            jtq.Where(jtq.JournalType == JournalType.Payroll.ToString(), jtq.RefferenceNumber == "PYR" + payrollPeriodCode, jtq.IsPosted == true);
            jtq.OrderBy(jtq.JournalId.Descending);
            jtq.es.Top = 1;
            jtq.es.WithNoLock = true;
            var jtdtb = jtq.LoadDataTable();

            if (jtdtb.Rows.Count > 0)
            {
                var jt = new JournalTransactions();
                jt.Load(jtq);

                bool newlyCreated = false;
                if (TransactionId == 0)
                {
                    var entity = new CashTransaction();
                    entity.AddNew();

                    // set
                    string bankId = string.Empty;
                    if (entity.es.RowState == System.Data.DataRowState.Added)
                        bankId = txtBankAccount.SelectedValue;
                    else
                        bankId = entity.BankId;

                    var bankAccount = Bank.Get(bankId);

                    entity.PostingId = 0;
                    entity.BankId = txtBankAccount.SelectedValue;
                    entity.ChartOfAccountId = bankAccount.ChartOfAccountId;
                    entity.TransactionDate = txtTransactionDate.SelectedDate;
                    entity.TransactionType = cboTransactionType.SelectedItem == null ? string.Empty : cboTransactionType.SelectedItem.Value;
                    entity.PaymentType = cboPaymentType.SelectedItem == null ? string.Empty : cboPaymentType.SelectedItem.Value;
                    entity.PaymentMethod = cboPaymentMethod.SelectedItem == null ? string.Empty : cboPaymentMethod.SelectedItem.Value;
                    entity.NormalBalance = cboNormalBalance.SelectedValue;
                    entity.Module = cboModuleName.SelectedValue;
                    entity.CurrencyCode = cboCurrency.SelectedValue;
                    entity.CurrencyRate = decimal.Parse(txtCurrencyRate.Text);
                    entity.IsPosted = false;
                    entity.IsCleared = chkIsCleared.Checked;
                    entity.IsVoid = false;
                    entity.ChequeNumber = txtChequeNumber.Text;
                    entity.DocumentNumber = "PYR" + payrollPeriodCode;
                    entity.Description = txtDescription.Text;
                    entity.DueDate = txtDueDate.SelectedDate;
                    entity.ReceivedFromOrPaidTo = txtFromTo.Text;
                    entity.VoidDate = new DateTime(1900, 1, 1);

                    if (entity.es.RowState == System.Data.DataRowState.Added)
                    {
                        entity.JournalId = jt.JournalId;
                        entity.DateCreated = DateTime.Now;
                        entity.LastUpdateDateTime = DateTime.Now;
                        entity.CreatedBy = AppSession.UserLogin.UserID;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                    else
                    {
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = DateTime.Now;
                    }
                    if (pnlBKU.Visible) entity.BkuAccountID = cboBkuAccount.SelectedValue.ToInt();

                    // set

                    if (!SaveEntity(entity))
                    {
                        return false;
                    }
                    OnPopulateEntryControl(entity);
                    OnDataModeChanged(AppEnum.DataMode.New, AppEnum.DataMode.Edit);

                    newlyCreated = true;
                }

                if (!newlyCreated && !IsEnableEdit(TransactionId))
                {
                    return false;
                }

                decimal debitAmount = 0;
                decimal creditAmount = 0;

                DataTable jtdDt = (DataTable)obj;
                
                if (jtdDt.Rows.Count > 0)
                {
                    foreach (DataRow jtd in jtdDt.Rows)
                    {
                        if (Convert.ToBoolean(jtd["IsSelect"]))
                        {
                            if (cboNormalBalance.SelectedValue == "K")
                                debitAmount = Convert.ToDecimal(jtd["Amount"]);
                            else
                                creditAmount = Convert.ToDecimal(jtd["Amount"]);

                            var detail = new CashTransactionDetail
                            {
                                TransactionId = this.TransactionId,
                                ChartOfAccountId = jtd["ChartOfAccountId"].ToInt(),
                                Debit = debitAmount,
                                Credit = creditAmount,
                                Amount = debitAmount - creditAmount,
                                Description = jtd["Description"].ToString(),
                                SubLedgerId = 0,
                                CostCenterId = 0,
                                ListID = null,
                                IsParentRefference = false,
                                ReferenceNo = "PYR" + payrollPeriodCode
                            };
                            detail.AddNew();
                            detail.Save();
                        }
                    }
                }

                GenerateGrid();
                grdVoucherEntryItem.Rebind();

                //CalculateCrossRefBalance();

                return true;
            }

            return false;
        }

        private void EmptyingRefference()
        {
            lblRef1.Text = string.Empty;
            hfRef1.Value = string.Empty;
            hfSequenceNo.Value = string.Empty;
            lblRef2.Text = string.Empty;
            hfRef2.Value = string.Empty;
            lblRef3.Text = string.Empty;
            hfRef3.Value = string.Empty;
            hfDetailIdRef.Value = string.Empty;
            hfIdent.Value = string.Empty;
        }

        protected void cboBkuAccount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboBkuAccount_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var coa = new ChartOfAccountsQuery();
            coa.es.Top = 20;
            coa.Where(coa.Or(coa.ChartOfAccountCode.Like(searchText), coa.ChartOfAccountName.Like(searchText)), coa.IsDetail == true, coa.IsActive == true);
            coa.OrderBy(coa.ChartOfAccountId.Ascending);
            cboBkuAccount.DataSource = coa.LoadDataTable();
            cboBkuAccount.DataBind();
        }
    }
}
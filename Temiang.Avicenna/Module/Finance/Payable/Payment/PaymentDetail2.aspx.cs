using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Core;
using System.Linq;

namespace Temiang.Avicenna.Module.Finance.Payable
{
    public partial class PaymentDetail2 : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PaymentSearch.aspx?pg=0";
            UrlPageList = "PaymentList2.aspx?pg=" + Request.QueryString["pg"];

            ProgramID = AppConstant.Program.AP_PAYMENT;

            if (!IsPostBack)
            {
                ViewState["IsApproved"] = false;

                var coll = new PaymentMethodCollection();
                coll.Query.Where(coll.Query.SRPaymentTypeID == AppSession.Parameter.PaymentTypeBackOfficePayment);
                coll.Query.OrderBy(coll.Query.SRPaymentMethodID.Ascending);
                coll.LoadAll();
                cboSRInvoicePayment.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var row in coll)
                {
                    cboSRInvoicePayment.Items.Add(new RadComboBoxItem(row.PaymentMethodName, row.SRPaymentMethodID));
                }

                pnlBKU.Visible = AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingBKUModule) == "Yes";
            }
        }

        //protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        //{
        //    ajax.AddAjaxSetting(grdItem, grdItem);
        //    ajax.AddAjaxSetting(grdItem, cboSupplierID);
        //    ajax.AddAjaxSetting(grdItem, cboSRInvoicePayment);
        //    //ajax.AddAjaxSetting(grdItem, pnlBank);
        //    ajax.AddAjaxSetting(grdItem, trBankId);
        //    ajax.AddAjaxSetting(grdItem, trBankAccNo);
        //    ajax.AddAjaxSetting(grdItem, cboBankID);
        //    ajax.AddAjaxSetting(grdItem, cboBankAccountNo);

        //    //ajax.AddAjaxSetting(cboSRInvoicePayment, pnlBank);
        //    ajax.AddAjaxSetting(cboSRInvoicePayment, trBankId);
        //    ajax.AddAjaxSetting(cboSRInvoicePayment, trBankAccNo);

        //    ajax.AddAjaxSetting(AjaxManager, trBankId);
        //    ajax.AddAjaxSetting(AjaxManager, trBankAccNo);
        //    ajax.AddAjaxSetting(AjaxManager, cboBankID);
        //    ajax.AddAjaxSetting(AjaxManager, cboBankAccountNo);
        //}

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new InvoiceSupplier();
            if (entity.LoadByPrimaryKey(txtInvoicePaymentNo.Text))
            {
                if (entity.IsApproved != null && entity.IsApproved.Value)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuEditClick()
        {
            cboSupplierID.Enabled = !(InvoiceSupplierItems.Count > 0);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            string x = (new InvoiceSupplier()).PaymentApproved(txtInvoicePaymentNo.Text, InvoiceSupplierItems, AppSession.UserLogin.UserID);
            if (!string.IsNullOrEmpty(x))
            {
                args.MessageText = x;
                args.IsCancel = true;
                return;
            }

            if (pnlBKU.Visible)
            {
                using (var trans = new esTransactionScope())
                {
                    var grr = new Supplier();
                    grr.LoadByPrimaryKey(cboSupplierID.SelectedValue);

                    var bkus = new BkuTransactionCollection();
                    foreach (var detail in InvoiceSupplierItems)
                    {
                        var jhd = new JournalTransactions();
                        jhd.Query.Where(jhd.Query.RefferenceNumber == detail.TransactionNo, jhd.Query.IsPosted == true);
                        if (!jhd.Query.Load()) continue;

                        var jdts = new JournalTransactionDetailsCollection();
                        jdts.Query.Where(jdts.Query.JournalId == jhd.JournalId);
                        jdts.Query.Load();

                        var bank = new Bank();
                        bank.LoadByPrimaryKey(cboBankID.SelectedValue);

                        var coa = new ChartOfAccounts();
                        coa.LoadByPrimaryKey(bank.ChartOfAccountId ?? 0);

                        var bku = bkus.AddNew();
                        bku.RekeningID = coa.ChartOfAccountId;
                        bku.UnitID = 0;
                        bku.Debit = 0;
                        bku.Credit = detail.PaymentAmount;
                        bku.Uraian = string.Empty;
                        bku.PaymentReferenceNo = txtInvoicePaymentNo.Text;

                        foreach (var jdt in jdts.Where(j => j.ChartOfAccountId != grr.ChartOfAccountIdAPTemporary))
                        {
                            bku = bkus.AddNew();
                            bku.RekeningID = jdt.ChartOfAccountId;
                            bku.UnitID = jdt.SubLedgerId;
                            bku.Debit = jdt.Debit;
                            bku.Credit = jdt.Credit;
                            bku.Uraian = jdt.Description;
                            bku.PaymentReferenceNo = txtInvoicePaymentNo.Text;
                            bku.InvoiceReferenceNo = detail.InvoiceReferenceNo;
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

        public override bool OnGetStatusMenuUnApprovalEnabled()
        {
            return !PostingStatus.IsUnApproveDisabledIfPerClosed(txtPaymentDate.SelectedDate.Value);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            string ret = (new InvoiceSupplier()).PaymentUnApproved(txtInvoicePaymentNo.Text, InvoiceSupplierItems, AppSession.UserLogin.UserID);
            if (!string.IsNullOrEmpty(ret))
            {
                args.MessageText = ret;
                args.IsCancel = true;
                return;
            }

            if (pnlBKU.Visible)
            {
                var bt = new BkuTransactionCollection();
                bt.Query.Where(bt.Query.PaymentReferenceNo == txtInvoicePaymentNo.Text);
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
            (new InvoiceSupplier()).PaymentVoid(txtInvoicePaymentNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuNewClick()
        {

            cboSupplierID.DataSource = null;
            cboSupplierID.DataBind();
            cboSupplierID.SelectedValue = string.Empty;
            cboSupplierID.Text = string.Empty;

            cboBankID.DataSource = null;
            cboBankID.DataBind();
            cboBankID.SelectedValue = string.Empty;
            cboBankID.Text = string.Empty;

            cboBankAccountNo.Items.Clear();
            cboBankAccountNo.Text = string.Empty;

            OnPopulateEntryControl(new InvoiceSupplier());

            PopulateNewInvoiceNo();
            txtPaymentDate.SelectedDate = DateTime.Now.Date;
            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        private void PopulateNewInvoiceNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;

            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date,
                AppSession.Parameter.IsInvoicePaymentSeparatedNo ?
                AppEnum.AutoNumber.InvoicePaymentAPNo : AppEnum.AutoNumber.InvoiceAPNo);
            txtInvoicePaymentNo.Text = _autoNumber.LastCompleteNumber;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            PopulateNewInvoiceNo();

            if (pnlBKU.Visible && string.IsNullOrEmpty(cboBkuAccount.SelectedValue))
            {
                args.MessageText = "BKU Account required.";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboSRInvoicePayment.SelectedValue))
            {
                args.MessageText = "Invoice Payment required.";
                args.IsCancel = true;
                return;
            }

            var entity = new InvoiceSupplier();
            if (entity.LoadByPrimaryKey(txtInvoicePaymentNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new InvoiceSupplier();
            entity.AddNew();
            SetEntityValue(entity);
            if (InvoiceSupplierItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (pnlBKU.Visible && string.IsNullOrEmpty(cboBkuAccount.SelectedValue))
            {
                args.MessageText = "BKU Account required";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboSRInvoicePayment.SelectedValue))
            {
                args.MessageText = "Invoice Payment required.";
                args.IsCancel = true;
                return;
            }

            var entity = new InvoiceSupplier();
            if (entity.LoadByPrimaryKey(txtInvoicePaymentNo.Text))
            {
                SetEntityValue(entity);
                if (InvoiceSupplierItems.Count == 0)
                {
                    args.MessageText = AppConstant.Message.RecordDetailEmpty;
                    args.IsCancel = true;
                    return;
                }
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
            auditLogFilter.PrimaryKeyData = string.Format("InvoiceNo='{0}'", txtInvoicePaymentNo.Text.Trim());
            auditLogFilter.TableName = "InvoiceSupplier";
        }

        public override bool OnGetStatusMenuEdit()
        {
            return txtInvoicePaymentNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new InvoiceSupplier();
            if (parameters.Length > 0)
            {
                String invoiceNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(invoiceNo);
            }
            else
                entity.LoadByPrimaryKey(txtInvoicePaymentNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var invoiceSupplier = (InvoiceSupplier)entity;
            txtInvoicePaymentNo.Text = invoiceSupplier.InvoiceNo;

            if (!string.IsNullOrEmpty(invoiceSupplier.SupplierID))
            {
                var query = new SupplierQuery();
                query.Select(
                    query.SupplierID,
                    query.SupplierName);
                query.Where(query.SupplierID == invoiceSupplier.str.SupplierID);
                DataTable dtbg = query.LoadDataTable();
                cboSupplierID.DataSource = dtbg;
                cboSupplierID.DataBind();
                cboSupplierID.SelectedValue = invoiceSupplier.SupplierID;
                cboSupplierID.Text = dtbg.Rows[0]["SupplierName"].ToString();
            }
            else
            {
                cboSupplierID.Items.Clear();
                cboSupplierID.Text = string.Empty;
            }


            txtPaymentDate.SelectedDate = invoiceSupplier.InvoiceDate;
            txtNotes.Text = invoiceSupplier.InvoiceNotes;
            cboSRInvoicePayment.SelectedValue = invoiceSupplier.SRInvoicePayment;
            pnlBank.Visible = invoiceSupplier.SRInvoicePayment != AppSession.Parameter.InvoicePaymentCash;
            
            if (!string.IsNullOrEmpty(invoiceSupplier.BankID))
            {
                var query = new BankQuery();
                query.Select(
                    query.BankID,
                    query.BankName);
                query.Where(query.BankID == invoiceSupplier.str.BankID);
                DataTable dtbb = query.LoadDataTable();
                cboBankID.DataSource = dtbb;
                cboBankID.DataBind();
                cboBankID.SelectedValue = invoiceSupplier.BankID;
                cboBankID.Text = dtbb.Rows[0]["BankName"].ToString();
            }
            else
            {
                cboBankID.Items.Clear();
                cboBankID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(invoiceSupplier.BankAccountNo))
            {
                //var query = new SupplierBankQuery();
                //query.Select(
                //    query.BankAccountNo,
                //    query.BankName);
                //query.Where(query.SupplierID == invoiceSupplier.SupplierID, query.BankAccountNo == invoiceSupplier.str.BankAccountNo);
                //DataTable dtbb = query.LoadDataTable();
                //cboBankAccountNo.DataSource = dtbb;
                //cboBankAccountNo.DataBind();

                cboBankAccountNo_ItemsRequested(cboBankAccountNo, new RadComboBoxItemsRequestedEventArgs() { Text = "" });

                cboBankAccountNo.SelectedValue = invoiceSupplier.BankAccountNo;
                //cboBankAccountNo.Text = dtbb.Rows[0]["BankAccountNo"].ToString() + " - " +
                //                        dtbb.Rows[0]["BankName"].ToString();
            }
            else
            {
                cboBankAccountNo.Items.Clear();
                cboBankAccountNo.Text = string.Empty;
            }

            if (pnlBKU.Visible)
            {
                var coa = new ChartOfAccountsQuery();
                coa.es.Top = 20;
                coa.Where(coa.ChartOfAccountId == (invoiceSupplier.BkuAccountID ?? 0));
                coa.OrderBy(coa.ChartOfAccountId.Ascending);
                cboBkuAccount.DataSource = coa.LoadDataTable();
                cboBkuAccount.DataBind();
                cboBkuAccount.SelectedValue = invoiceSupplier.BkuAccountID.ToString();
            }

            ViewState["IsApproved"] = invoiceSupplier.IsApproved ?? false;
            ViewState["IsVoid"] = invoiceSupplier.IsVoid ?? false;

            PopulateInvoiceSupplierItemGrid();
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new InvoiceSupplierQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(
                    que.InvoiceNo > txtInvoicePaymentNo.Text,
                    que.IsInvoicePayment == true, que.IsWriteOff == false, que.IsAddPayment.IsNull()
                    );
                que.OrderBy(que.InvoiceNo.Ascending);
            }
            else
            {
                que.Where(
                    que.InvoiceNo < txtInvoicePaymentNo.Text,
                    que.IsInvoicePayment == true, que.IsWriteOff == false, que.IsAddPayment.IsNull()
                    );
                que.OrderBy(que.InvoiceNo.Descending);
            }
            var entity = new InvoiceSupplier();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = InvoiceSupplierItems;
        }

        private InvoiceSupplierItemCollection InvoiceSupplierItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["InvoiceSupplierItemsPayment" + Request.UserHostName];
                    if (obj != null)
                        return ((InvoiceSupplierItemCollection)(obj));
                }

                var coll = new InvoiceSupplierItemCollection();
                var query = new InvoiceSupplierItemQuery("a");
                var por = new ItemTransactionQuery("por");

                query.LeftJoin(por).On(query.TransactionNo == por.TransactionNo)
                    .Select(
                        query,
                        (query.VerifyAmount - query.PaymentAmount.Coalesce("'0'")).As("refToInvoiceSupplierItem_BalanceAmount"),
                        por.InvoiceNo.As("refToItemTransaction_InvoiceSupplierNo")
                    );

                query.Where(query.InvoiceNo == txtInvoicePaymentNo.Text);
                query.OrderBy(query.TransactionNo.Ascending);

                coll.Load(query);
                Session["InvoiceSupplierItemsPayment" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["InvoiceSupplierItemsPayment" + Request.UserHostName] = value; }
        }

        protected void cboSupplierID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SupplierQuery();

            query.Select(
                query.SupplierID,
                query.SupplierName
                );

            query.Where(
                query.Or(
                    query.SupplierID.Like(searchTextContain),
                    query.SupplierName.Like(searchTextContain)
                    )
                );

            query.es.Top = 20;
            cboSupplierID.DataSource = query.LoadDataTable();
            cboSupplierID.DataBind();
        }

        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SupplierName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SupplierID"].ToString();
        }

        protected void cboBankID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["BankName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["BankID"].ToString();
        }

        protected void cboBankID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new BankQuery();
            query.es.Top = 20;
            query.Select(
                query.BankID,
                query.BankName
                );
            query.Where(
                query.IsApPayment == true,
                query.BankName.Like(searchTextContain), query.IsActive == true
                );
            query.OrderBy(query.BankName.Ascending);
            cboBankID.DataSource = query.LoadDataTable();
            cboBankID.DataBind();
        }

        protected void cboBankAccountNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["BankAccountNo"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["BankName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["BankAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["BankAccountNo"].ToString();
        }

        protected void cboBankAccountNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SupplierBankQuery();
            query.es.Top = 10;
            query.Select(
                query.BankAccountNo,
                query.BankName,
                query.BankAccountName
                );
            query.Where(
                query.SupplierID == cboSupplierID.SelectedValue,
                query.Or(query.BankAccountNo.Like(searchTextContain),
                         query.BankName.Like(searchTextContain),
                         query.BankAccountName.Like(searchTextContain)), query.IsActive == true);
            query.OrderBy(query.BankAccountNo.Ascending);
            cboBankAccountNo.DataSource = query.LoadDataTable();
            cboBankAccountNo.DataBind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid) || string.IsNullOrEmpty(eventArgument))
                return;

            switch (((RadGrid)sourceControl).ID)
            {
                case "grdItem":
                    var firstSelectedInvNo = eventArgument;
                    var inv = new InvoiceSupplier();
                    if (inv.LoadByPrimaryKey(firstSelectedInvNo))
                    {
                        if (!string.IsNullOrEmpty(inv.SRInvoicePayment))
                        {
                            cboSRInvoicePayment.SelectedValue = inv.SRInvoicePayment;
                            pnlBank.Visible = cboSRInvoicePayment.SelectedValue != AppSession.Parameter.InvoicePaymentCash;

                            if (!string.IsNullOrEmpty(inv.BankID))
                            {
                                var query = new BankQuery();
                                query.Select(
                                    query.BankID,
                                    query.BankName);
                                query.Where(query.BankID == inv.str.BankID);
                                DataTable dtbb = query.LoadDataTable();
                                cboBankID.SelectedValue = string.Empty;
                                cboBankID.DataSource = dtbb;
                                cboBankID.DataBind();
                                //cboBankID.SelectedValue = inv.BankID;
                                //cboBankID.Text = dtbb.Rows[0]["BankName"].ToString();
                                ComboBox.SelectedValue(cboBankID, inv.BankID);
                            }
                            else
                            {
                                cboBankID.Items.Clear();
                                cboBankID.SelectedValue = string.Empty;
                                cboBankID.Text = string.Empty;
                            }
                        }

                        if (!string.IsNullOrEmpty(inv.BankAccountNo))
                        {
                            //var query = new SupplierBankQuery();
                            //query.Select(
                            //    query.BankAccountNo,
                            //    query.BankName,
                            //    query.BankAccountName);
                            //query.Where(query.SupplierID == inv.str.SupplierID, query.BankAccountNo == inv.str.BankAccountNo);
                            //DataTable dtbb = query.LoadDataTable();
                            //cboBankAccountNo.DataSource = dtbb;
                            //cboBankAccountNo.DataBind();

                            cboBankAccountNo_ItemsRequested(cboBankAccountNo, new RadComboBoxItemsRequestedEventArgs() { Text = "" });

                            cboBankAccountNo.SelectedValue = inv.BankAccountNo;
                            //cboBankAccountNo.Text = dtbb.Rows[0]["BankAccountNo"].ToString() + " - " + dtbb.Rows[0]["BankName"].ToString() + " - " + dtbb.Rows[0]["BankAccountName"].ToString();
                        }
                        else
                        {
                            cboBankAccountNo.Items.Clear();
                            cboBankAccountNo.Text = string.Empty;
                        }

                        cboSupplierID.Enabled = !(InvoiceSupplierItems.Count > 0);
                    }

                    grdItem.Rebind();
                    
                    break;
            }
        }

        protected void cboSRInvoicePayment_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            pnlBank.Visible = e.Value != AppSession.Parameter.InvoicePaymentCash;
            pnlBank.Visible = e.Value != AppSession.Parameter.InvoicePaymentDiscount;

            if (!pnlBank.Visible) {
                cboBankID.SelectedValue = "";
            }
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            String invoiceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][InvoiceSupplierItemMetadata.ColumnNames.InvoiceNo]);
            String transNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][InvoiceSupplierItemMetadata.ColumnNames.TransactionNo]);

            InvoiceSupplierItem entity = InvoiceSupplierItems.FindByPrimaryKey(invoiceNo, transNo);
            if (entity != null)
                entity.MarkAsDeleted();

            cboSupplierID.Enabled = !(InvoiceSupplierItems.Count > 0);
        }

        private void RefreshCommandItemInvoiceSupplierItem(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = (newVal != AppEnum.DataMode.Read);

            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdItem.Rebind();

        }

        private void PopulateInvoiceSupplierItemGrid()
        {
            //Display Data Detail
            InvoiceSupplierItems = null; //Reset Record Detail
            grdItem.DataSource = InvoiceSupplierItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemInvoiceSupplierItem(oldVal, newVal);
        }

        private void SetEntityValue(InvoiceSupplier entity)
        {
            if (entity.es.IsAdded)
                PopulateNewInvoiceNo();
            entity.InvoiceNo = txtInvoicePaymentNo.Text;

            entity.SRInvoicePayment = cboSRInvoicePayment.SelectedValue;
            entity.InvoiceDate = txtPaymentDate.SelectedDate.Value;
            entity.str.InvoiceDueDate = string.Empty;
            entity.InvoiceTOP = 0;
            entity.SupplierID = cboSupplierID.SelectedValue;
            entity.BankID = cboBankID.SelectedValue;
            entity.BankAccountNo = cboBankAccountNo.SelectedValue;
            entity.InvoiceNotes = txtNotes.Text;

            entity.SRPayableStatus = AppSession.Parameter.PayableStatusPaid;
            entity.IsInvoicePayment = true;
            entity.InvoiceReferenceNo = string.Empty;

            if (pnlBKU.Visible) entity.BkuAccountID = cboBkuAccount.SelectedValue.ToInt();

            foreach (InvoiceSupplierItem item in InvoiceSupplierItems)
            {
                item.InvoiceNo = txtInvoicePaymentNo.Text;

                // Set for first selected InvoiceNo
                if (string.IsNullOrEmpty(entity.InvoiceReferenceNo))
                    entity.InvoiceReferenceNo = item.InvoiceReferenceNo;
            }
        }

        private void SaveEntity(InvoiceSupplier entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                InvoiceSupplierItems.Save();

                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            switch (programID)
            {
                case AppConstant.Report.CashTransactionVoucher:
                    var ct = new CashTransaction();
                    ct.Query.Where(ct.Query.DocumentNumber == txtInvoicePaymentNo.Text,
                        ct.Query.IsVoid == false, ct.Query.IsPosted == true);
                    if (ct.Load(ct.Query))
                    {
                        printJobParameters.AddNew("pEntityId", ct.TransactionId.ToString());
                        printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                    }
                    else
                    {
                        printJobParameters.AddNew("pEntityId", "xxxxx");
                        printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                    }
                    break;

                default:
                    printJobParameters.AddNew("p_invoiceNo", txtInvoicePaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                    break;

            }
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = InvoiceSupplierItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();

            cboSupplierID.Enabled = !(InvoiceSupplierItems.Count > 0);
        }

        private void SetEntityValue(InvoiceSupplierItem entity, GridCommandEventArgs e)
        {
            var userControl = (PaymentDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.InvoiceNo = userControl.InvoiceReferenceNo;
                entity.TransactionNo = userControl.SequenceNo;
                entity.TransactionDate = txtPaymentDate.SelectedDate;
                entity.VerifyAmount = 0;
                entity.PaymentAmount = userControl.PaymentAmount;
                entity.Notes = userControl.Notes;
                //entity.AccountID,
                entity.Amount = 0;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                //entity.VoucherID,
                //entity.AgingDate,
                //entity.SRInvoicePayment,
                //entity.BankID,
                //entity.BankAccountNo,
                //entity.VerifyDate,
                //entity.VerifyByUserID,
                //entity.PaymentDate,
                //entity.PaymentByUserID,
                //entity.IsPaymentApproved,
                //entity.PaymentApprovedDate,
                //entity.PaymentApprovedByUserID,
                //entity.PPnAmount,
                //entity.PPh22Amount,
                //entity.PPh23Amount,
                entity.CurrencyID = userControl.CurrencyID;

                var cur = new CurrencyRate();
                cur.LoadByPrimaryKey(entity.CurrencyID);
                entity.CurrencyRate = cur.CurrencyRate;

                //entity.StampAmount,
                //entity.InvoiceReferenceNo,
                //entity.InvoiceSN,
                //entity.TaxInvoiceDate,
                //entity.OtherDeduction
                entity.SRItemType = userControl.SRItemType;

                entity.ChartOfAccountId = userControl.ChartOfAccountId;
                entity.ChartOfAccountCode = userControl.ChartOfAccountCodeText.Split('-')[0].Trim();
                entity.ChartOfAccountName = userControl.ChartOfAccountCodeText.Split('-')[1].Trim();
                entity.SubLedgerId = userControl.SubLedgerId;

                //if (!string.IsNullOrEmpty(userControl.ChartOfAccountID)){
                //    entity.ChartOfAccountId = int.Parse(userControl.ChartOfAccountID);
                //}
                //if (!string.IsNullOrEmpty(userControl.SubledgerID))
                //{
                //    entity.SubLedgerId = int.Parse(userControl.SubledgerID);
                //}
            }
        }

        protected void cboBkuAccount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboBkuAccount_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var coa = new ChartOfAccountsQuery();
            coa.es.Top = 20;
            coa.Where(coa.Or(coa.ChartOfAccountCode.Like(searchTextContain), coa.ChartOfAccountName.Like(searchTextContain)), coa.IsDetail == true, coa.IsActive == true);
            coa.OrderBy(coa.ChartOfAccountId.Ascending);
            cboBkuAccount.DataSource = coa.LoadDataTable();
            cboBkuAccount.DataBind();
        }
    }
}

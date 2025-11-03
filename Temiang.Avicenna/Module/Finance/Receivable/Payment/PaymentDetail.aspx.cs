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

namespace Temiang.Avicenna.Module.Finance.Receivable
{
    public partial class PaymentDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PaymentSearch.aspx?pg=0";
            UrlPageList = "PaymentList.aspx?pg=" + Request.QueryString["pg"];

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.AR_PAYMENT;

            if (!IsPostBack)
            {
                cboSRPaymentType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                var ptColl = new PaymentTypeCollection();
                ptColl.Query.Where(ptColl.Query.IsArPayment == true);
                ptColl.LoadAll();


                foreach (PaymentType pt in ptColl)
                {
                    cboSRPaymentType.Items.Add(new RadComboBoxItem(pt.PaymentTypeName, pt.SRPaymentTypeID));
                }

                StandardReference.InitializeIncludeSpace(cboSRCardProvider, AppEnum.StandardReference.CardProvider);
                StandardReference.InitializeIncludeSpace(cboSRCardType, AppEnum.StandardReference.CardType);
                PopulateEDCMachine();

                StandardReference.InitializeIncludeSpace(cboSRDiscountReason, AppEnum.StandardReference.DiscountReason);

                ViewState["IsApproved"] = false;

                pnlBKU.Visible = AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingBKUModule) == "Yes";
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItem, grdItem);
            ajax.AddAjaxSetting(grdItem, cboGuarantorID);
            ajax.AddAjaxSetting(grdItem, lblRelatedBankInquiryAmount);
            ajax.AddAjaxSetting(grdItem, lblRelatedBankInquiryDesc);
            ajax.AddAjaxSetting(grdItem, hfRelatedBankInquiryID);

            ajax.AddAjaxSetting(cboGuarantorID, hdnGuarantorId);
        }

        protected void lbDeleteAll_OnClick(object sender, EventArgs e)
        {
            InvoicesItems.MarkAllAsDeleted();
            grdItem.Rebind();
            cboGuarantorID.Enabled = true;
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new Invoices();
            if (entity.LoadByPrimaryKey(txtInvoicePaymentNo.Text))
            {
                if (entity.IsPaymentApproved != null && entity.IsPaymentApproved.Value)
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
            cboGuarantorID.Enabled = !(InvoicesItems.Count > 0);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            string x = (new Invoices()).PaymentApproved(txtInvoicePaymentNo.Text, InvoicesItems, AppSession.UserLogin.UserID);
            if (!string.IsNullOrEmpty(x))
            {
                args.MessageText = x;
                args.IsCancel = true;
                return;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "ProporsiJasmed", string.Format("rowRecalFeePercByAjax('{0}')", txtInvoicePaymentNo.Text), true);

            if (pnlBKU.Visible)
            {
                using (var trans = new esTransactionScope())
                {
                    var grr = new Guarantor();
                    grr.LoadByPrimaryKey(cboGuarantorID.SelectedValue);

                    var bkus = new BkuTransactionCollection();
                    foreach (var detail in InvoicesItems)
                    {
                        var jhd = new JournalTransactions();
                        jhd.Query.Where(jhd.Query.RefferenceNumber == detail.PaymentNo);//, jhd.Query.IsPosted == true);
                        if (!jhd.Query.Load()) continue;

                        var jdts = new JournalTransactionDetailsCollection();
                        jdts.Query.Where(jdts.Query.JournalId == jhd.JournalId);
                        jdts.Query.Load();

                        var coa = new ChartOfAccounts();
                        coa.LoadByPrimaryKey(jdts.Where(j => j.ChartOfAccountId == ((grr.ChartOfAccountIdTemporary ?? 0) == 0 ? grr.ChartOfAccountId : grr.ChartOfAccountIdTemporary) && j.DocumentNumber == detail.PaymentNo).Single().ChartOfAccountId ?? 0);

                        var bku = bkus.AddNew();
                        bku.RekeningID = coa.ChartOfAccountId;
                        bku.UnitID = 0;
                        bku.Debit = detail.PaymentAmount;
                        bku.Credit = 0;
                        bku.Uraian = string.Empty;
                        bku.PaymentReferenceNo = txtInvoicePaymentNo.Text;

                        foreach (var jdt in jdts.Where(j => j.DocumentNumber != detail.PaymentNo))
                        {
                            bku = bkus.AddNew();
                            bku.RekeningID = cboBkuAccount.SelectedValue.ToInt();
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

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            string ret = (new Invoices()).PaymentUnApproved(txtInvoicePaymentNo.Text, InvoicesItems, AppSession.UserLogin.UserID);
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
            (new Invoices()).PaymentVoid(txtInvoicePaymentNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuNewClick()
        {

            cboGuarantorID.DataSource = null;
            cboGuarantorID.DataBind();
            cboGuarantorID.SelectedValue = string.Empty;
            cboGuarantorID.Text = string.Empty;

            cboBankID.DataSource = null;
            cboBankID.DataBind();
            cboBankID.SelectedValue = string.Empty;
            cboBankID.Text = string.Empty;

            OnPopulateEntryControl(new Invoices());

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
                AppEnum.AutoNumber.InvoicePaymentARNo : AppEnum.AutoNumber.InvoiceARNo);
            txtInvoicePaymentNo.Text = _autoNumber.LastCompleteNumber;
        }

        private string CustomValidation()
        {
            if (string.IsNullOrEmpty(cboSRPaymentType.SelectedValue))
            {
                return "Payment Type is required";
            }

            if (cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypeDiscount)
            {
                if (string.IsNullOrEmpty(cboSRDiscountReason.SelectedValue))
                {
                    return "Discount reason is required";
                }
            }

            //// validate payment method
            //var pm = new PaymentMethodCollection();
            //pm.Query.Where(pm.Query.SRPaymentTypeID == cboSRPaymentMethod.SelectedValue);
            //if (pm.LoadAll())
            //{
            if (cboSRPaymentType.SelectedValue != AppSession.Parameter.PaymentTypeDiscount)
            {
                if (string.IsNullOrEmpty(cboSRPaymentMethod.SelectedValue))
                {
                    return "Payment Method is required";
                }
            }
            //}

            if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodTransfer)
            {
                if (string.IsNullOrEmpty(cboBankID.SelectedValue))
                {
                    return "Bank is required";
                }
            }

            if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCreditCard ||
                cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodDebitCard)
            {
                if (string.IsNullOrEmpty(cboSRCardProvider.SelectedValue))
                {
                    return "Card Provider is required";
                }

                if (string.IsNullOrEmpty(cboSRCardType.SelectedValue))
                {
                    return "Card Type is required";
                }

                if (string.IsNullOrEmpty(cboEDCMachineID.SelectedValue))
                {
                    return "EDC Machine is required";
                }
            }

            return string.Empty;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (pnlBKU.Visible && string.IsNullOrEmpty(cboBkuAccount.SelectedValue))
            {
                args.MessageText = "BKU Account required";
                args.IsCancel = true;
                return;
            }

            PopulateNewInvoiceNo();

            var entity = new Invoices();

            if (entity.LoadByPrimaryKey(txtInvoicePaymentNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            string cv = CustomValidation();
            if (!string.IsNullOrEmpty(cv))
            {
                args.MessageText = cv;
                args.IsCancel = true;
                return;
            }

            entity = new Invoices();
            entity.AddNew();
            SetEntityValue(entity);
            if (InvoicesItems.Count == 0)
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

            var entity = new Invoices();
            if (entity.LoadByPrimaryKey(txtInvoicePaymentNo.Text))
            {
                string cv = CustomValidation();
                if (!string.IsNullOrEmpty(cv))
                {
                    args.MessageText = cv;
                    args.IsCancel = true;
                    return;
                }

                SetEntityValue(entity);
                if (InvoicesItems.Count == 0)
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

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            //switch (programID)
            //{
            //    case AppConstant.Report.ARPaymentSlip:
            printJobParameters.AddNew("p_invoiceNo", txtInvoicePaymentNo.Text);
            //        break;

            //}
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new Invoices();
            if (entity.LoadByPrimaryKey(txtInvoicePaymentNo.Text))
            {
                entity.MarkAsDeleted();
                InvoicesItems.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    InvoicesItems.Save();
                    entity.Save();

                    trans.Complete();
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("InvoiceNo='{0}'", txtInvoicePaymentNo.Text.Trim());
            auditLogFilter.TableName = "Invoices";
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
            var entity = new Invoices();
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
            var invoices = (Invoices)entity;
            txtInvoicePaymentNo.Text = invoices.InvoiceNo;

            if (!string.IsNullOrEmpty(invoices.GuarantorID))
            {
                var query = new GuarantorQuery("a");
                var query2 = new GuarantorQuery("b");

                query.Select(
                    query2.GuarantorID,
                    query2.GuarantorName,
                    (query2.StreetName + " " + query2.City + " " + query2.ZipCode).Trim().As("Address")
                    );

                query.InnerJoin(query2).On(query.GuarantorHeaderID == query2.GuarantorID);
                query.Where(query.GuarantorID == invoices.str.GuarantorID);
                query.es.Distinct = true;

                DataTable dtbg = query.LoadDataTable();
                cboGuarantorID.DataSource = dtbg;
                cboGuarantorID.DataBind();
                cboGuarantorID.SelectedValue = invoices.GuarantorID;
                cboGuarantorID.Text = dtbg.Rows[0]["GuarantorName"].ToString();
            }
            else
            {
                cboGuarantorID.Items.Clear();
                cboGuarantorID.Text = string.Empty;
            }

            txtNotes.Text = invoices.InvoiceNotes;

            txtPaymentDate.SelectedDate = invoices.PaymentDate;
            cboSRPaymentType.SelectedValue = invoices.SRPaymentType;

            //method
            cboSRPaymentMethod.Items.Clear();
            cboSRPaymentMethod.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            var pmColl = new PaymentMethodCollection();
            pmColl.Query.Where(pmColl.Query.SRPaymentTypeID == cboSRPaymentType.SelectedValue);
            pmColl.LoadAll();

            foreach (PaymentMethod pm in pmColl)
            {
                cboSRPaymentMethod.Items.Add(new RadComboBoxItem(pm.PaymentMethodName, pm.SRPaymentMethodID));
            }

            if (cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypeDiscount)
            {
                pnlDiscountReason.Visible = true;
                pnlPaymentMethod.Visible = false;
                pnlBank.Visible = false;
                pnlCardProvider.Visible = false;
                pnlCardDetail.Visible = false;
                pnlRounding.Visible = false;
            }
            else
                pnlPaymentMethod.Visible = true;

            cboSRPaymentMethod.SelectedValue = invoices.SRPaymentMethod;
            SetVisiblePanel();

            cboSRDiscountReason.SelectedValue = invoices.SRDiscountReason;
            cboSRCardProvider.SelectedValue = invoices.SRCardProvider;
            cboSRCardType.SelectedValue = invoices.SRCardType;

            //edc
            cboEDCMachineID.Items.Clear();
            var edc = new EDCMachineQuery();
            edc.Where
                (
                    edc.SRCardProvider == cboSRCardProvider.SelectedValue,
                    edc.IsActive == true
                );

            DataTable dtb = edc.LoadDataTable();
            cboEDCMachineID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            for (int i = 0; i < dtb.Rows.Count; i++)
            {
                cboEDCMachineID.Items.Add(new RadComboBoxItem((string)dtb.Rows[i][EDCMachineMetadata.ColumnNames.EDCMachineName],
                    (string)dtb.Rows[i][EDCMachineMetadata.ColumnNames.EDCMachineID]));
            }
            cboEDCMachineID.SelectedValue = invoices.EDCMachineID;

            txtCardHolderName.Text = invoices.CardHolderName;

            if (!string.IsNullOrEmpty(invoices.BankID))
            {
                var bank = new BankQuery("a");
                bank.Select(
                    bank.BankID,
                    bank.BankName
                    );
                bank.Where(bank.BankID == invoices.str.BankID);
                DataTable dtbb = bank.LoadDataTable();
                cboBankID.DataSource = dtbb;
                cboBankID.DataBind();
                cboBankID.SelectedValue = invoices.BankID;
                cboBankID.Text = dtbb.Rows[0]["BankName"].ToString();
            }
            else
            {
                cboBankID.Items.Clear();
                cboBankID.Text = string.Empty;
            }

            //cboBankID.SelectedValue = invoices.BankID;
            txtBankAccountNo.Text = invoices.BankAccountNo;
            txtPrintReceiptAsName.Text = invoices.PrintReceiptAsName;
            txtRoundingAR.Text = Convert.ToString(invoices.RoundingAmount);

            if (pnlBKU.Visible)
            {
                var coa = new ChartOfAccountsQuery();
                coa.es.Top = 20;
                coa.Where(coa.ChartOfAccountId == (invoices.BkuAccountID ?? 0));
                coa.OrderBy(coa.ChartOfAccountId.Ascending);
                cboBkuAccount.DataSource = coa.LoadDataTable();
                cboBkuAccount.DataBind();
                cboBkuAccount.SelectedValue = invoices.BkuAccountID.ToString();
            }

            ViewState["IsApproved"] = invoices.IsApproved ?? false;
            ViewState["IsVoid"] = invoices.IsVoid ?? false;

            PopulateInvoicesItemGrid();

            if (!string.IsNullOrEmpty(txtInvoicePaymentNo.Text)) {
                var bid = new BankInquiryDetail();
                if (bid.LoadByRelatedTransactionNo(txtInvoicePaymentNo.Text)) {
                    hfRelatedBankInquiryID.Value = bid.TransactionID.ToString();
                    LoadRelatedInquiry();
                }
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new InvoicesQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.InvoiceNo > txtInvoicePaymentNo.Text, que.IsInvoicePayment == true,
                          que.IsWriteOff == false, que.IsAddPayment.IsNull());
                que.OrderBy(que.InvoiceNo.Ascending);
            }
            else
            {
                que.Where(que.InvoiceNo < txtInvoicePaymentNo.Text, que.IsInvoicePayment == true,
                          que.IsWriteOff == false, que.IsAddPayment.IsNull());
                que.OrderBy(que.InvoiceNo.Descending);
            }
            Invoices entity = new Invoices();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = InvoicesItems;
        }

        private InvoicesItemCollection InvoicesItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["InvoicesItemsPayment" + Request.UserHostName];
                    if (obj != null)
                        return ((InvoicesItemCollection)(obj));
                }

                var coll = new InvoicesItemCollection();
                var query = new InvoicesItemQuery("a");
                var patQ = new PatientQuery("b");
                var drQ = new AppStandardReferenceItemQuery("c");
                var tpQ = new TransPaymentQuery("d");

                query.Select(
                    query,
                    (query.VerifyAmount - (query.PaymentAmount.Coalesce("'0'") + query.OtherAmount.Coalesce("'0'") + query.BankCost.Coalesce("'0'"))).As("refToInvoicesItem_BalanceAmount"),
                    patQ.MedicalNo.As("refPatientID_MedicalNo"),
                    drQ.ItemName.As("refToAppStandardReferenceItem_DiscountReason"),
                    tpQ.GuarantorID.As("refToInvoices_GuarantorID")
                    );

                query.LeftJoin(patQ).On(query.PatientID == patQ.PatientID);
                query.LeftJoin(drQ).On(query.SRDiscountReason == drQ.ItemID &&
                                       drQ.StandardReferenceID == AppEnum.StandardReference.DiscountReason.ToString());
                query.LeftJoin(tpQ).On(tpQ.PaymentNo == query.PaymentNo);
                query.Where(query.InvoiceNo == txtInvoicePaymentNo.Text);
                query.OrderBy(query.PaymentNo.Ascending);

                DataTable dtb = query.LoadDataTable();

                coll.Load(query);
                Session["InvoicesItemsPayment" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["InvoicesItemsPayment" + Request.UserHostName] = value; }
        }

        protected void cboSRPaymentType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRPaymentMethod.Items.Clear();
            cboSRPaymentMethod.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            var pmColl = new PaymentMethodCollection();
            pmColl.Query.Where(pmColl.Query.SRPaymentTypeID == e.Value); //, pmColl.Query.SRPaymentMethodID != "PaymentMethod-001");
            pmColl.LoadAll();

            foreach (PaymentMethod pm in pmColl)
            {
                cboSRPaymentMethod.Items.Add(new RadComboBoxItem(pm.PaymentMethodName, pm.SRPaymentMethodID));
            }

            if (e.Value == AppSession.Parameter.PaymentTypeDiscount)
            {
                pnlDiscountReason.Visible = true;
                pnlPaymentMethod.Visible = false;
                pnlBank.Visible = false;
                pnlCardProvider.Visible = false;
                pnlCardDetail.Visible = false;
                pnlRounding.Visible = false;
            }
            else
            {
                pnlPaymentMethod.Visible = true;
                SetVisiblePanel();
            }

            ResetValue(true);
        }

        private void ResetValue(bool includePaymentMethod)
        {
            if (includePaymentMethod)
                cboSRPaymentMethod.SelectedValue = string.Empty;

            cboSRDiscountReason.SelectedValue = string.Empty;
            cboBankID.SelectedValue = string.Empty;

            cboSRCardProvider.SelectedValue = string.Empty;
            cboSRCardType.SelectedValue = string.Empty;
            cboEDCMachineID.SelectedValue = string.Empty;
            txtCardHolderName.Text = string.Empty;
            txtBankAccountNo.Text = string.Empty;
            txtRoundingAR.Text = string.Empty;
        }

        protected void cboSRPaymentMethod_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetVisiblePanel();
            ResetValue(false);
        }

        protected void cboSRCardProvider_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateEDCMachine();
        }

        protected void cboEDCMachineID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            EDCMachineTariff entity = Helper.EDCMachineTariff.GetEDCMachineTariff(cboEDCMachineID.SelectedValue, cboSRCardType.SelectedValue);
        }

        private void PopulateEDCMachine()
        {
            if (cboSRCardProvider.SelectedIndex == -1)
                cboEDCMachineID.Items.Clear();
            else
            {
                cboEDCMachineID.Items.Clear();
                var query = new EDCMachineQuery();
                query.Where
                    (
                        query.SRCardProvider == cboSRCardProvider.SelectedValue,
                        query.IsActive == true
                    );

                DataTable edc = query.LoadDataTable();
                cboEDCMachineID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                for (int i = 0; i < edc.Rows.Count; i++)
                {
                    cboEDCMachineID.Items.Add(new RadComboBoxItem((string)edc.Rows[i][EDCMachineMetadata.ColumnNames.EDCMachineName],
                        (string)edc.Rows[i][EDCMachineMetadata.ColumnNames.EDCMachineID]));
                }
            }
        }

        private void SetVisiblePanel()
        {
            if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCash ||
                cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodBiaya)
            {
                pnlDiscountReason.Visible = false;
                pnlBank.Visible = false;
                pnlCardProvider.Visible = false;
                pnlCardDetail.Visible = false;
                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingRoundingPaymentAR) == "Yes")
                {
                    pnlRounding.Visible = true;
                }
                else
                    pnlRounding.Visible = false;
            }
            //else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCash &&
            //         AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingRoundingPaymentAR) == "Yes")
            //{
            //    pnlDiscountReason.Visible = false;
            //    pnlBank.Visible = false;
            //    pnlCardProvider.Visible = false;
            //    pnlCardDetail.Visible = false;
            //    pnlRounding.Visible = true;
            //}
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCreditCard)
            {
                pnlDiscountReason.Visible = false;
                pnlBank.Visible = false;
                pnlCardDetail.Visible = true;
                pnlCardProvider.Visible = true;
                pnlRounding.Visible = false;
            }
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodDebitCard)
            {
                pnlDiscountReason.Visible = false;
                pnlBank.Visible = false;
                pnlCardDetail.Visible = true;
                pnlCardProvider.Visible = true;
                pnlRounding.Visible = false;
            }
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodTransfer)
            {
                pnlDiscountReason.Visible = false;
                pnlBank.Visible = true;
                pnlCardProvider.Visible = false;
                pnlCardDetail.Visible = false;
                pnlRounding.Visible = false;
            }
            else
            {
                pnlDiscountReason.Visible = true;
                pnlBank.Visible = true;
                pnlCardProvider.Visible = true;
                pnlCardDetail.Visible = true;
                pnlRounding.Visible = false;
            }
        }

        //private void ResetValue(bool includePaymentMethod)
        //{
        //    if (includePaymentMethod)
        //        cboSRPaymentMethod.SelectedValue = string.Empty;

        //    cboBankID.SelectedValue = string.Empty;
        //    cboSRCardProvider.SelectedValue = string.Empty;
        //    cboSRCardType.SelectedValue = string.Empty;
        //    cboEDCMachineID.SelectedValue = string.Empty;
        //    txtCardHolderName.Text = string.Empty;
        //    txtBankAccountNo.Text = string.Empty;
        //}

        protected void cboGuarantorID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery("a");
            var query2 = new GuarantorQuery("b");

            query.InnerJoin(query2).On(query.GuarantorHeaderID == query2.GuarantorID);

            query.Select(
                query2.GuarantorID,
                query2.GuarantorName,
                (query2.StreetName + " " + query2.City + " " + query2.ZipCode).Trim().As("Address")
                );

            query.Where(
                query.Or(
                    query2.GuarantorID.Like(searchTextContain),
                    query2.GuarantorName.Like(searchTextContain)
                    )
                );

            query.es.Distinct = true;
            query.es.Top = 20;
            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void cboGuarantorID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            hdnGuarantorId.Value = cboGuarantorID.SelectedValue;
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
                query.IsArPayment == true,
                query.BankName.Like(searchTextContain), query.IsActive == true
                );
            query.OrderBy(query.BankName.Ascending);
            cboBankID.DataSource = query.LoadDataTable();
            cboBankID.DataBind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid) || string.IsNullOrEmpty(eventArgument))
                return;

            switch (((RadGrid)sourceControl).ID)
            {
                case "grdItem":
                    grdItem.Rebind();
                    cboGuarantorID.Enabled = !(InvoicesItems.Count > 0);

                    if (Session["BankInquiryTransactionID"] != null) {
                        hfRelatedBankInquiryID.Value = Session["BankInquiryTransactionID"].ToString();
                        LoadRelatedInquiry();
                    }

                    break;
            }
        }

        private void LoadRelatedInquiry() {
            lblRelatedBankInquiryAmount.Text = string.Empty;
            lblRelatedBankInquiryDesc.Text = string.Empty;

            if (!string.IsNullOrEmpty(hfRelatedBankInquiryID.Value)) {
                var bid = new BankInquiryDetail();
                if (bid.LoadByPrimaryKey(System.Convert.ToInt32(hfRelatedBankInquiryID.Value)))
                {
                    lblRelatedBankInquiryAmount.Text = ((bid.Credit ?? 0) - (bid.Debit ?? 0)).ToString("N2");
                    lblRelatedBankInquiryDesc.Text = bid.Description;
                }
            }
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            String invoiceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][InvoicesItemMetadata.ColumnNames.InvoiceNo]);
            String paymentNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][InvoicesItemMetadata.ColumnNames.PaymentNo]);

            InvoicesItem entity = InvoicesItems.FindByPrimaryKey(invoiceNo, paymentNo);
            if (entity != null)
                entity.MarkAsDeleted();

            cboGuarantorID.Enabled = !(InvoicesItems.Count > 0);
        }

        private void RefreshCommandItemInvoicesItem(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = (newVal != AppEnum.DataMode.Read);

            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                InvoicesItems = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdItem.Rebind();
        }

        private void PopulateInvoicesItemGrid()
        {
            //Display Data Detail
            InvoicesItems = null; //Reset Record Detail
            grdItem.DataSource = InvoicesItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemInvoicesItem(oldVal, newVal);
        }

        private void SetEntityValue(Invoices entity)
        {
            if (entity.es.IsAdded)
                PopulateNewInvoiceNo();
            entity.InvoiceNo = txtInvoicePaymentNo.Text;

            entity.InvoiceDate = txtPaymentDate.SelectedDate.Value;
            entity.str.InvoiceDueDate = string.Empty;
            entity.InvoiceTOP = 0;
            entity.GuarantorID = cboGuarantorID.SelectedValue;
            entity.BankID = cboBankID.SelectedValue;
            entity.BankAccountNo = txtBankAccountNo.Text;
            entity.InvoiceNotes = txtNotes.Text;
            entity.PaymentDate = txtPaymentDate.SelectedDate.Value;
            entity.SRReceivableStatus = AppSession.Parameter.ReceivableStatusPaid;
            entity.SRPaymentType = cboSRPaymentType.SelectedValue;
            entity.SRPaymentMethod = cboSRPaymentMethod.SelectedValue;
            entity.SRCardType = cboSRCardType.SelectedValue;
            entity.SRDiscountReason = cboSRDiscountReason.SelectedValue;
            entity.SRCardProvider = cboSRCardProvider.SelectedValue;
            entity.EDCMachineID = cboEDCMachineID.SelectedValue;
            entity.CardHolderName = txtCardHolderName.Text;
            entity.IsInvoicePayment = true;
            entity.InvoiceReferenceNo = string.Empty;
            entity.PrintReceiptAsName = txtPrintReceiptAsName.Text;
            entity.RoundingAmount = Convert.ToDecimal(txtRoundingAR.Value);
            if (pnlBKU.Visible) entity.BkuAccountID = cboBkuAccount.SelectedValue.ToInt();

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Update Detil
            foreach (InvoicesItem item in InvoicesItems)
            {
                item.InvoiceNo = txtInvoicePaymentNo.Text;

                // Set for first selected InvoiceNo
                if (string.IsNullOrEmpty(entity.InvoiceReferenceNo))
                {
                    entity.InvoiceReferenceNo = item.InvoiceReferenceNo;

                    var en = new Invoices();
                    en.LoadByPrimaryKey(entity.InvoiceReferenceNo);
                    entity.SRReceivableType = en.SRReceivableType;
                }

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(Invoices entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                InvoicesItems.Save();

                if (!string.IsNullOrEmpty(hfRelatedBankInquiryID.Value))
                {
                    var bid = new BankInquiryDetail();
                    if (bid.LoadByPrimaryKey(System.Convert.ToInt32(hfRelatedBankInquiryID.Value)))
                    {
                        bid.RelatedTransactionNo = entity.InvoiceNo;
                    }
                    bid.Save();
                }

                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
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

        protected void cboBankID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
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
    }
}

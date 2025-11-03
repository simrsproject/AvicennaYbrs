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

namespace Temiang.Avicenna.Module.Finance.Receivable.Customer
{
    public partial class PaymentDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PaymentSearch.aspx";
            UrlPageList = "PaymentList.aspx";

            ProgramID = AppConstant.Program.AR_CUSTOMER_PAYMENT;

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
            }
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new InvoiceCustomer();
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

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            string x = (new InvoiceCustomer()).PaymentApproved(txtInvoicePaymentNo.Text, InvoiceCustomerItems, AppSession.UserLogin.UserID);
            if (!string.IsNullOrEmpty(x))
            {
                args.MessageText = x;
                args.IsCancel = true;
                return;
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            string ret = (new InvoiceCustomer()).PaymentUnApproved(txtInvoicePaymentNo.Text, InvoiceCustomerItems, AppSession.UserLogin.UserID);
            if (!string.IsNullOrEmpty(ret))
            {
                args.MessageText = ret;
                args.IsCancel = true;
                return;
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            (new InvoiceCustomer()).PaymentVoid(txtInvoicePaymentNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new InvoiceCustomer());

            cboCustomerID.DataSource = null;
            cboCustomerID.DataBind();
            cboCustomerID.SelectedValue = string.Empty;
            cboCustomerID.Text = string.Empty;

            cboSRPaymentType.SelectedValue = string.Empty;
            cboSRPaymentType.Text = string.Empty;

            cboSRPaymentMethod.SelectedValue = string.Empty;
            cboSRPaymentMethod.Text = string.Empty;

            cboSRDiscountReason.SelectedValue = string.Empty;
            cboSRDiscountReason.Text = string.Empty;

            cboSRCardProvider.SelectedValue = string.Empty;
            cboSRCardProvider.Text = string.Empty;

            cboSRCardType.SelectedValue = string.Empty;
            cboSRCardType.Text = string.Empty;

            cboEDCMachineID.SelectedValue = string.Empty;
            cboEDCMachineID.Text = string.Empty;

            cboBankID.DataSource = null;
            cboBankID.DataBind();
            cboBankID.SelectedValue = string.Empty;
            cboBankID.Text = string.Empty;

            PopulateNewInvoiceNo();
            txtInvoiceNo.ShowButton = true;
            txtPaymentDate.SelectedDate = DateTime.Now.Date;
            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        private void PopulateNewInvoiceNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;

            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.InvoiceARCustomerNo);
            txtInvoicePaymentNo.Text = _autoNumber.LastCompleteNumber;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            PopulateNewInvoiceNo();
            var entity = new InvoiceCustomer();
            if (entity.LoadByPrimaryKey(txtInvoicePaymentNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboSRPaymentType.SelectedValue))
            {
                args.MessageText = "Payment Type is required";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboSRPaymentMethod.SelectedValue))
            {
                args.MessageText = "Payment Method is required";
                args.IsCancel = true;
                return;
            }

            if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodTransfer)
            {
                if (string.IsNullOrEmpty(cboBankID.SelectedValue))
                {
                    args.MessageText = "Bank is required";
                    args.IsCancel = true;
                    return;
                }
            }

            if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCreditCard ||
                cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodDebitCard)
            {
                if (string.IsNullOrEmpty(cboSRCardProvider.SelectedValue))
                {
                    args.MessageText = "Card Provider is required";
                    args.IsCancel = true;
                    return;
                }

                if (string.IsNullOrEmpty(cboSRCardType.SelectedValue))
                {
                    args.MessageText = "Card Type is required";
                    args.IsCancel = true;
                    return;
                }

                if (string.IsNullOrEmpty(cboEDCMachineID.SelectedValue))
                {
                    args.MessageText = "EDC Machine is required";
                    args.IsCancel = true;
                    return;
                }
            }

            entity = new InvoiceCustomer();
            entity.AddNew();
            SetEntityValue(entity);
            if (InvoiceCustomerItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new InvoiceCustomer();
            if (entity.LoadByPrimaryKey(txtInvoicePaymentNo.Text))
            {
                if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodTransfer)
                {
                    if (string.IsNullOrEmpty(cboBankID.SelectedValue))
                    {
                        args.MessageText = "Bank is required";
                        args.IsCancel = true;
                        return;
                    }
                }

                if (string.IsNullOrEmpty(cboSRPaymentType.SelectedValue))
                {
                    args.MessageText = "Payment Type is required";
                    args.IsCancel = true;
                    return;
                }

                if (string.IsNullOrEmpty(cboSRPaymentMethod.SelectedValue))
                {
                    args.MessageText = "Payment Method is required";
                    args.IsCancel = true;
                    return;
                }

                if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCreditCard ||
                    cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodDebitCard)
                {
                    if (string.IsNullOrEmpty(cboSRCardProvider.SelectedValue))
                    {
                        args.MessageText = "Card Provider is required";
                        args.IsCancel = false;
                        return;
                    }

                    if (string.IsNullOrEmpty(cboSRCardType.SelectedValue))
                    {
                        args.MessageText = "Card Type is required";
                        args.IsCancel = false;
                        return;
                    }

                    if (string.IsNullOrEmpty(cboEDCMachineID.SelectedValue))
                    {
                        args.MessageText = "EDC Machine is required";
                        args.IsCancel = false;
                        return;
                    }
                }

                SetEntityValue(entity);
                if (InvoiceCustomerItems.Count == 0)
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

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID,
                                               PrintJobParameterCollection printJobParameters)
        {
            //switch (programID)
            //{
            //    case AppConstant.Report.ARPaymentSlip:
            printJobParameters.AddNew("p_invoiceNo", txtInvoicePaymentNo.Text);
            //        break;

            //}
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
            var entity = new InvoiceCustomer();
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
            var invoices = (InvoiceCustomer)entity;
            txtInvoicePaymentNo.Text = invoices.InvoiceNo;

            if (!string.IsNullOrEmpty(invoices.CustomerID))
            {
                var query = new CustomerQuery("a");
                
                query.Select(
                    query.CustomerID,
                    query.CustomerName,
                    (query.StreetName + " " + query.City + " " + query.ZipCode).Trim().As("Address")
                    );

                query.Where(query.CustomerID == invoices.str.CustomerID);
                query.es.Distinct = true;

                DataTable dtbg = query.LoadDataTable();
                cboCustomerID.DataSource = dtbg;
                cboCustomerID.DataBind();
                cboCustomerID.SelectedValue = invoices.CustomerID;
                cboCustomerID.Text = dtbg.Rows[0]["CustomerName"].ToString();
            }
            else
            {
                cboCustomerID.Items.Clear();
                cboCustomerID.Text = string.Empty;
            }

            txtInvoiceNo.Text = invoices.InvoiceReferenceNo;
            txtInvoiceDate.SelectedDate = invoices.InvoiceDate;
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
            }
            else
            {
                pnlPaymentMethod.Visible = true;
                pnlDiscountReason.Visible = false;
            }

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

            ViewState["IsApproved"] = invoices.IsApproved ?? false;
            ViewState["IsVoid"] = invoices.IsVoid ?? false;

            PopulateInvoicesItemGrid();
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new InvoiceCustomerQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.InvoiceNo > txtInvoicePaymentNo.Text, que.IsInvoicePayment == true);
                que.OrderBy(que.InvoiceNo.Ascending);
            }
            else
            {
                que.Where(que.InvoiceNo < txtInvoicePaymentNo.Text, que.IsInvoicePayment == true);
                que.OrderBy(que.InvoiceNo.Descending);
            }
            InvoiceCustomer entity = new InvoiceCustomer();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = InvoiceCustomerItems;
        }

        private InvoiceCustomerItemCollection InvoiceCustomerItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["InvoiceCustomerItemPayments" + Request.UserHostName];
                    if (obj != null)
                        return ((InvoiceCustomerItemCollection)(obj));
                }

                var coll = new InvoiceCustomerItemCollection();
                var query = new InvoiceCustomerItemQuery("a");

                query.Select(
                    query,
                    (query.VerifyAmount - (query.PaymentAmount.Coalesce("'0'") + query.OtherAmount.Coalesce("'0'") + query.BankCost.Coalesce("'0'"))).As("refToInvoicesItem_BalanceAmount")
                    );

                query.Where(query.InvoiceNo == txtInvoicePaymentNo.Text);
                query.OrderBy(query.TransactionNo.Ascending);

                coll.Load(query);
                Session["InvoiceCustomerItemPayments" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["InvoiceCustomerItemPayments" + Request.UserHostName] = value; }
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
            }
            else
            {
                pnlPaymentMethod.Visible = true;
                pnlDiscountReason.Visible = false;
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
                pnlBank.Visible = false;
                pnlCardProvider.Visible = false;
                pnlCardDetail.Visible = false;
            }
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCreditCard)
            {
                pnlBank.Visible = false;
                pnlCardDetail.Visible = true;
                pnlCardProvider.Visible = true;
            }
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodDebitCard)
            {
                pnlBank.Visible = false;
                pnlCardDetail.Visible = true;
                pnlCardProvider.Visible = true;
            }
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodTransfer)
            {
                pnlBank.Visible = true;
                pnlCardProvider.Visible = false;
                pnlCardDetail.Visible = false;
            }
            else
            {
                pnlBank.Visible = true;
                pnlCardProvider.Visible = true;
                pnlCardDetail.Visible = true;
            }
        }

        protected void cboCustomerID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new CustomerQuery("a");
            
            query.Select(
                query.CustomerID,
                query.CustomerName,
                (query.StreetName + " " + query.City + " " + query.ZipCode).Trim().As("Address")
                );

            query.Where(
                query.Or(
                    query.CustomerID.Like(searchTextContain),
                    query.CustomerName.Like(searchTextContain)
                    )
                );

            query.es.Distinct = true;
            query.es.Top = 20;
            cboCustomerID.DataSource = query.LoadDataTable();
            cboCustomerID.DataBind();
        }

        protected void cboCustomerID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["CustomerName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["CustomerID"].ToString();
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
                    txtInvoiceNo.Text = eventArgument;
                    var inv = new InvoiceCustomer();
                    if (inv.LoadByPrimaryKey(txtInvoiceNo.Text))
                        txtInvoiceDate.SelectedDate = inv.InvoiceDate;

                    grdItem.Rebind();
                    break;
            }
        }

        protected void txtInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            var inv = new InvoiceCustomer();
            txtInvoiceDate.SelectedDate = inv.LoadByPrimaryKey(txtInvoiceNo.Text) ? inv.InvoiceDate : null;
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            String invoiceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][InvoiceCustomerItemMetadata.ColumnNames.InvoiceNo]);
            String transactionNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][InvoiceCustomerItemMetadata.ColumnNames.TransactionNo]);
            String invoiceReferenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][InvoiceCustomerItemMetadata.ColumnNames.InvoiceReferenceNo]);

            InvoiceCustomerItem entity = InvoiceCustomerItems.FindByPrimaryKey(invoiceNo, transactionNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        private void RefreshCommandItemInvoicesItem(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = (newVal != AppEnum.DataMode.Read);
        }

        private void PopulateInvoicesItemGrid()
        {
            //Display Data Detail
            InvoiceCustomerItems = null; //Reset Record Detail
            grdItem.DataSource = InvoiceCustomerItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemInvoicesItem(oldVal, newVal);
        }

        private void SetEntityValue(InvoiceCustomer entity)
        {
            if (entity.es.IsAdded)
                PopulateNewInvoiceNo();
            entity.InvoiceNo = txtInvoicePaymentNo.Text;

            var en = new InvoiceCustomer();
            en.LoadByPrimaryKey(txtInvoiceNo.Text);

            entity.SRReceivableType = en.SRReceivableType;
            entity.InvoiceDate = txtPaymentDate.SelectedDate.Value;
            entity.str.InvoiceDueDate = string.Empty;
            entity.InvoiceTOP = 0;
            entity.CustomerID = cboCustomerID.SelectedValue;
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
            entity.InvoiceReferenceNo = txtInvoiceNo.Text;
            entity.IsVoid = false;
            entity.IsApproved = false;
            
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Update Detil
            foreach (InvoiceCustomerItem item in InvoiceCustomerItems)
            {
                item.InvoiceNo = txtInvoicePaymentNo.Text;
                item.InvoiceReferenceNo = txtInvoiceNo.Text;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(InvoiceCustomer entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                InvoiceCustomerItems.Save();

                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }
    }
}

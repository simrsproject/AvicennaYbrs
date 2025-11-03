using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Charges.Dispensary
{
    public partial class SalesDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New)
                return;
            if (cboFromServiceUnitID.SelectedValue == string.Empty)
            {
                cboFromServiceUnitID.Text = string.Empty;
                return;
            }

            ServiceUnit serv = new ServiceUnit();
            if (serv.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue))
            {
                _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, BusinessObject.Reference.TransactionCode.Sales, serv.DepartmentID);
                txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "SalesSearch.aspx";
            UrlPageList = "SalesList.aspx";

            ProgramID = AppConstant.Program.Sales;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                ViewState["ReferenceNo" + Request.UserHostName] = string.Empty;

                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
                ComboBox.SelectedValue(cboSRItemType, BusinessObject.Reference.ItemType.Medical);

                StandardReference.InitializeIncludeSpace(cboSRPaymentType, AppEnum.StandardReference.STBPaymentType);
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromServiceUnitID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, txtTransactionNo);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboCustomerID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboSRItemType);

            ajax.AddAjaxSetting(grdItemTransactionItem, grdItemTransactionItem);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboFromServiceUnitID);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboSRItemType);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboCustomerID);
            ajax.AddAjaxSetting(grdItemTransactionItem, txtChargesAmount);
            ajax.AddAjaxSetting(grdItemTransactionItem, txtTaxAmount);
            ajax.AddAjaxSetting(grdItemTransactionItem, txtTotal);

            ajax.AddAjaxSetting(cboCustomerID, txtSalesMarginPercentage);

            //ajax.AddAjaxSetting(chkIsUseTax, txtTaxPercentage);
            //ajax.AddAjaxSetting(chkIsUseTax, txtTaxAmount);
            //ajax.AddAjaxSetting(chkIsUseTax, txtTotal);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
            string fromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.Sales, true);
            cboFromServiceUnitID.SelectedValue = fromServiceUnitID;

            cboSRItemType.Enabled = ItemTransactionItems.Count == 0;
            cboFromServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboCustomerID.Enabled = cboSRItemType.Enabled;
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            ItemTransaction entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            string result = ItemTransaction.IsItemMinusProcess(txtTransactionNo.Text, ItemTransactionItems);
            if (result != string.Empty)
            {
                args.MessageText = result;
                args.IsCancel = true;
                return;
            }
            using (var trans = new esTransactionScope())
            {
                var entity = new ItemTransaction();
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
                var loc = new Location();
                if (loc.LoadByPrimaryKey(entity.FromLocationID) && loc.IsHoldForTransaction == true)
                {
                    args.MessageText = "Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.";
                    args.IsCancel = true;
                    return;
                }


                entity.IsApproved = true;
                entity.ApprovedDate = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                entity.Save();

                // stock calculation
                var chargesBalances = new ItemBalanceCollection();
                var chargesDetailBalances = new ItemBalanceDetailCollection();
                var chargesMovements = new ItemMovementCollection();
                var itemBalanceDetailEds = new ItemBalanceDetailEdCollection();

                string itemNoStock;
                var itemTransactionItems = ItemTransactionItems;

                ItemBalance.PrepareItemBalances(entity, itemTransactionItems, entity.FromServiceUnitID, entity.FromLocationID, AppSession.UserLogin.UserID,
                   ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, ref itemBalanceDetailEds, out itemNoStock, AppSession.Parameter.IsEnabledStockWithEdControl);

                if (!string.IsNullOrEmpty(itemNoStock))
                {
                    if (itemNoStock.Length >= 5 && itemNoStock.Substring(0, 5) == "Zero|")
                        args.MessageText = "Zero cost price of item : " + itemNoStock.Replace("Zero|", "");
                    else
                        args.MessageText = "Insufficient balance of item : " + itemNoStock;

                    args.IsCancel = true;
                    return;
                }

                itemTransactionItems.Save();

                if (chargesBalances != null)
                    chargesBalances.Save();
                if (chargesDetailBalances != null)
                    chargesDetailBalances.Save();
                if (itemBalanceDetailEds != null)
                    itemBalanceDetailEds.Save();
                if (chargesMovements != null)
                    chargesMovements.Save();

                //Commit if success, Rollback if failed
                if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsAutoJournalSales))
                {
                    DateTime jDate = entity.TransactionDate.Value.Date;

                    var isClosingPeriod = PostingStatus.IsPeriodeClosed(jDate);
                    if (isClosingPeriod)
                    {
                        args.MessageText = "Financial statements for period: " + string.Format("{0:MMMM-yyyy}", jDate) + " have been closed. Please contact the authorities.";
                        args.IsCancel = true;
                        return;
                    }
                        
                    var journalId = JournalTransactions.AddNewSalesJournal(entity, AppSession.UserLogin.UserID, 0);
                }
                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            (new ItemTransaction()).Void(txtTransactionNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            (new ItemTransaction()).UnVoid(txtTransactionNo.Text, AppSession.UserLogin.UserID);
        }

        private bool IsApprovedOrVoid(ItemTransaction entity, ValidateArgs args)
        {
            if (entity.IsApproved.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ItemTransaction());

            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();// DateTime.Now;
            ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.Sales, true);
            PopulateNewTransactionNo();
            cboSRPaymentType.SelectedValue = string.Empty;
            cboSRPaymentType.Text = string.Empty;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            ItemTransaction entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            PopulateNewTransactionNo();
            // save autonumber immediately to decrease time gap between create and save
            _autoNumber.Save();

            ItemTransaction entity = new ItemTransaction();

            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new ItemTransaction();
            entity.AddNew();
            SetEntityValue(entity);
            if (ItemTransactionItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            ItemTransaction entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                if (ItemTransactionItems.Count == 0)
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "ItemTransaction";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);

            //if (programID == "SLP.02.0110") //Sales To Customer Slip
            //{
            //    printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
            //}
            //else
            //{
            //    args.IsCancel = true;
            //    args.MessageText = "Report not defined, please contact IT support";
            //}
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtTransactionNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !chkIsVoid.Checked;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ItemTransaction entity = new ItemTransaction();
            if (parameters.Length > 0)
            {
                String transactionNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transactionNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            ItemTransaction itemTransaction = (ItemTransaction)entity;
            txtTransactionNo.Text = itemTransaction.TransactionNo;
            txtTransactionDate.SelectedDate = itemTransaction.TransactionDate;
            ComboBox.PopulateWithOneServiceUnit(cboFromServiceUnitID, itemTransaction.FromServiceUnitID ?? string.Empty);
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, BusinessObject.Reference.TransactionCode.Sales);

            if (!string.IsNullOrEmpty(itemTransaction.CustomerID))
            {
                var cust = new CustomerQuery();
                cust.Where(cust.CustomerID == itemTransaction.CustomerID);
                cboCustomerID.DataSource = cust.LoadDataTable();
                cboCustomerID.DataBind();
                cboCustomerID.SelectedValue = itemTransaction.CustomerID;
            }
            else
            {
                cboCustomerID.Items.Clear();
                cboCustomerID.SelectedValue = string.Empty;
                cboCustomerID.Text = string.Empty;
            }
            
            cboSRItemType.SelectedValue = itemTransaction.SRItemType;
            chkIsVoid.Checked = itemTransaction.IsVoid ?? false;
            chkIsApproved.Checked = itemTransaction.IsApproved ?? false;
            txtNotes.Text = itemTransaction.Notes;
            txtChargesAmount.Value = Convert.ToDouble(itemTransaction.ChargesAmount);
            txtTaxInvoiceNo.Text = itemTransaction.TaxInvoiceNo;
            txtTaxTransactionDate.SelectedDate = itemTransaction.TaxInvoiceDate;
            cboSRPaymentType.SelectedValue = itemTransaction.SRPaymentType;
            txtOrderNo.Text = itemTransaction.InvoiceNo;
            txtSalesMarginPercentage.Value = Convert.ToDouble(itemTransaction.SalesMarginPercentage ?? 0);

            //IsTaxable -> 1. Exclude Tax, 0. Include Tax, 2. No Tax
            chkIsUseTax.Checked = itemTransaction.IsTaxable == 0;
            txtTaxPercentage.Value = Convert.ToDouble(itemTransaction.TaxPercentage);
            txtTaxAmount.Value = Convert.ToDouble(itemTransaction.TaxAmount);

            CalculateTotal();

            //Display Data Detail
            PopulateGridDetail();
        }
        #endregion

        #region Private Method Standard

        private void SetEntityValue(ItemTransaction entity)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionCode = BusinessObject.Reference.TransactionCode.Sales;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.ReferenceNo = ViewState["ReferenceNo" + Request.UserHostName] == null ? string.Empty : ViewState["ReferenceNo" + Request.UserHostName].ToString();
            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;

            var su = new ServiceUnit();
            su.LoadByPrimaryKey(entity.FromServiceUnitID);
            entity.FromLocationID = su.GetMainLocationId();

            entity.CustomerID = cboCustomerID.SelectedValue;
            entity.ChargesAmount = Convert.ToDecimal(txtChargesAmount.Value);
            entity.SRPaymentType = cboSRPaymentType.SelectedValue;
            entity.SRItemType = cboSRItemType.SelectedValue;
            entity.Notes = txtNotes.Text;
            entity.TaxInvoiceDate = txtTaxTransactionDate.SelectedDate;
            entity.TaxInvoiceNo = txtTaxInvoiceNo.Text;
            entity.InvoiceNo = txtOrderNo.Text;
            entity.IsNonMasterOrder = false;
            entity.IsInventoryItem = true;

            entity.TaxPercentage = Convert.ToDecimal(txtTaxPercentage.Value);
            entity.TaxAmount = Convert.ToDecimal(txtTaxAmount.Value);

            //IsTaxable -> 1. Exclude Tax, 0. Include Tax, 2. No Tax
            entity.IsTaxable = Convert.ToInt16(chkIsUseTax.Checked ? 0 : 2);

            entity.SalesMarginPercentage = Convert.ToDecimal(txtSalesMarginPercentage.Value ?? 0);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
            }

            //Update Detil
            foreach (ItemTransactionItem item in ItemTransactionItems)
            {
                item.TransactionNo = txtTransactionNo.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                }
            }
        }

        private void SaveEntity(ItemTransaction entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ItemTransactionItems.Save();

                ////autonumber has been saved on SetEntity
                //if (DataModeCurrent == DataMode.New)
                //    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ItemTransactionQuery("a");
            var qusr = new AppUserServiceUnitQuery("u");
            que.InnerJoin(qusr).On(que.FromServiceUnitID == qusr.ServiceUnitID &&
                                         qusr.UserID == AppSession.UserLogin.UserID);

            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.TransactionNo > txtTransactionNo.Text &&
                        que.TransactionCode == BusinessObject.Reference.TransactionCode.Sales
                    );
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.TransactionNo < txtTransactionNo.Text &&
                        que.TransactionCode == BusinessObject.Reference.TransactionCode.Sales
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }

            ItemTransaction entity = new ItemTransaction();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function

        private ItemTransactionItemCollection ItemTransactionItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["Sales:collItemTransactionItem" + Request.UserHostName];
                    if (obj != null)
                        return ((ItemTransactionItemCollection)(obj));
                }

                var coll = new ItemTransactionItemCollection();
                var query = new ItemTransactionItemQuery("a");
                var iq = new ItemQuery("b");

                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.ItemID.Ascending);

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName")
                    );

                coll.Load(query);
                Session["Sales:collItemTransactionItem" + Request.UserHostName] = coll;

                return coll;
            }
            set { Session["Sales:collItemTransactionItem" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemTransactionItem.Columns[0].Visible = isVisible;
            grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 2].Visible = isVisible;

            grdItemTransactionItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                ItemTransactionItems = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdItemTransactionItem.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            ItemTransactionItems = null; //Reset Record Detail
            grdItemTransactionItem.DataSource = ItemTransactionItems;
            grdItemTransactionItem.MasterTableView.IsItemInserted = false;
            grdItemTransactionItem.MasterTableView.ClearEditItems();
            grdItemTransactionItem.DataBind();
        }

        protected void grdItemTransactionItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemTransactionItem.DataSource = ItemTransactionItems;
        }

        protected void grdItemTransactionItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            String sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                [ItemTransactionItemMetadata.ColumnNames.SequenceNo]);

            ItemTransactionItem entity = FindItemTransactionItem(sequenceNo);
            if (entity != null)
                SetEntityValue(entity, e);

            CalculateDetailTransaction();
        }

        private ItemTransactionItem FindItemTransactionItem(String sequenceNo)
        {
            ItemTransactionItemCollection coll = ItemTransactionItems;
            ItemTransactionItem retEntity = null;
            foreach (ItemTransactionItem rec in coll)
            {
                if (rec.SequenceNo.Equals(sequenceNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItemTransactionItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]
                [ItemTransactionItemMetadata.ColumnNames.SequenceNo]);

            ItemTransactionItem entity = FindItemTransactionItem(sequenceNo);
            if (entity != null)
                entity.MarkAsDeleted();

            cboFromServiceUnitID.Enabled = !(ItemTransactionItems.Count > 0);
            cboSRItemType.Enabled = !(ItemTransactionItems.Count > 0);
            cboCustomerID.Enabled = cboSRItemType.Enabled;
            CalculateDetailTransaction();
        }

        protected void grdItemTransactionItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemTransactionItem entity = ItemTransactionItems.AddNew();
            SetEntityValue(entity, e);

            //grid not close first
            e.Canceled = true;
            grdItemTransactionItem.Rebind();

            CalculateDetailTransaction();
        }

        private void SetEntityValue(ItemTransactionItem entity, GridCommandEventArgs e)
        {
            SalesItemDetail userControl = (SalesItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.SequenceNo = userControl.SequenceNo;
                entity.Quantity = userControl.Quantity;
                entity.ItemName = userControl.ItemName;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.Discount1Percentage = userControl.Discount1Percentage;
                entity.Discount2Percentage = userControl.Discount2Percentage;
                entity.Discount = userControl.DiscountAmount;
                entity.IsDiscountInPercent = userControl.IsDiscountInPercent;
                entity.ConversionFactor = userControl.ConversionFactor;
                entity.Price = userControl.Price;
                entity.Description = userControl.ItemName;

                if (entity.IsDiscountInPercent == true)
                {
                    entity.Discount1Percentage = userControl.Discount1Percentage;
                    entity.Discount2Percentage = userControl.Discount2Percentage;
                    decimal disc1 = Convert.ToDecimal(entity.Price * entity.Discount1Percentage / 100);
                    decimal disc2 = Convert.ToDecimal((entity.Price - disc1) * entity.Discount2Percentage / 100);
                    entity.Discount = disc1 + disc2;
                }
                else
                {
                    entity.Discount1Percentage = 0;
                    entity.Discount2Percentage = 0;
                    entity.Discount = userControl.DiscountAmount;
                }

                if (cboSRItemType.SelectedValue == BusinessObject.Reference.ItemType.Medical)
                {
                    ItemProductMedic med = new ItemProductMedic();
                    med.LoadByPrimaryKey(entity.ItemID);
                    entity.CostPrice = med.CostPrice; // cost price tetap avg walaupun price penjualannya bdasarkan harga POR terakhir

                }
                else if (cboSRItemType.SelectedValue == BusinessObject.Reference.ItemType.NonMedical)
                {
                    ItemProductNonMedic nonMed = new ItemProductNonMedic();
                    nonMed.LoadByPrimaryKey(entity.ItemID);
                    entity.CostPrice = nonMed.CostPrice;

                }
                else if (cboSRItemType.SelectedValue == BusinessObject.Reference.ItemType.NonMedical)
                {
                    ItemKitchen kc = new ItemKitchen();
                    kc.LoadByPrimaryKey(entity.ItemID);
                    entity.CostPrice = kc.CostPrice;

                }
                entity.PriceInCurrency = entity.Price;
                entity.DiscountInCurrency = entity.Discount;
            }
        }

        #endregion

        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateNewTransactionNo();
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, BusinessObject.Reference.TransactionCode.Sales);
        }

        protected void cboCustomerID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.CustomerItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboCustomerID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.CustomerItemDataBound(e);
        }

        protected void cboCustomerID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtSalesMarginPercentage.Value = 0;
            var cust = new Customer();
            if (cust.LoadByPrimaryKey(e.Value))
                txtSalesMarginPercentage.Value = (double) (cust.SalesMarginPercentage ?? 0);

        }
        protected void txtTaxPercentage_TextChanged(object sender, EventArgs e)
        {
            CalculateTax();
        }

        //private void CalculateDetailTransaction()
        //{
        //    if (ItemTransactionItems.Count > 0)
        //    {
        //        decimal? totaltransaction = 0;
        //        decimal? totaldiscitem = 0;

        //        foreach (ItemTransactionItem item in ItemTransactionItems)
        //        {
        //            if (!Convert.ToBoolean(item.IsBonusItem))
        //            {
        //                totaltransaction += (item.Price * item.Quantity);
        //                totaldiscitem += (item.Discount * item.Quantity);
        //            }
        //        }

        //        txtTransactionAmount.Value = Convert.ToDouble(totaltransaction - totaldiscitem);


        //    }
        //}

        private void CalculateDetailTransaction()
        {
            decimal? totaltransaction = 0;
            decimal? totaldiscitem = 0; if (ItemTransactionItems.Count > 0)
            {
                foreach (ItemTransactionItem item in ItemTransactionItems)
                {
                    if (!Convert.ToBoolean(item.IsBonusItem))
                    {
                        totaltransaction += (item.Price * item.Quantity);
                        totaldiscitem += (item.Discount * item.Quantity);
                    }
                }
            }
            txtChargesAmount.Value = Convert.ToDouble(totaltransaction - totaldiscitem);
            CalculateTax();
        }

        private void CalculateTax()
        {
            if (txtTaxPercentage.Value == 0)
                txtTaxAmount.Value = 0.00;
            else
                txtTaxAmount.Value = ((txtChargesAmount.Value * txtTaxPercentage.Value) / Convert.ToDouble(100));

            CalculateTotal();
        }

        private void CalculateTotal()
        {
            txtTotal.Value = txtChargesAmount.Value + txtTaxAmount.Value;
        }

        protected void chkIsUseTax_CheckedChanged(object sender, EventArgs e)
        {

            if (chkIsUseTax.Checked == true)
                txtTaxPercentage.Value = AppSession.Parameter.TaxPercentage;
            else
                txtTaxPercentage.Value = 0;

            CalculateTax();
        }
    }
}
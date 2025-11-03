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

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class ConsignmentTransferDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New)
                return;

            if (string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                return;

            var serv = new ServiceUnit();
            if (serv.LoadByPrimaryKey(cboToServiceUnitID.SelectedValue))
            {
                _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, TransactionCode.ConsignmentTransfer, serv.DepartmentID);
                txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
            }
        }
        
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ConsignmentTransferSearch.aspx";
            UrlPageList = "ConsignmentTransferList.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.ConsignmentTransfer;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItemTransactionItem, grdItemTransactionItem);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboSRItemType);

            ajax.AddAjaxSetting(cboToServiceUnitID, cboToServiceUnitID);
            ajax.AddAjaxSetting(cboToServiceUnitID, txtTransactionNo);
            ajax.AddAjaxSetting(cboToServiceUnitID, cboToLocationID);
            ajax.AddAjaxSetting(cboToServiceUnitID, cboSRItemType);

            ajax.AddAjaxSetting(cboSupplierID, cboSupplierID);
            ajax.AddAjaxSetting(cboSupplierID, cboFromLocationID);

            ajax.AddAjaxSetting(AjaxManager, grdItemTransactionItem);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.ConsignmentTransfer, false);

            cboSRItemType.Enabled = (ItemTransactionItems.Count == 0);
            cboToServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboToLocationID.Enabled = cboSRItemType.Enabled;
            grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = false; 
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new ItemTransaction();
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

        private bool IsApprovedOrVoid(ItemTransaction entity, ValidateArgs args)
        {
            if (entity.IsApproved != null && entity.IsApproved.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid != null && entity.IsVoid.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var c = ItemTransactionItems;
            if (c.Count == 0)
            {
                args.MessageText = "Data can't be approved because detail is empty. Please check back your data.";
                args.IsCancel = true;
                return;
            }

            if (AppSession.Parameter.IsEnabledStockWithEdControl)
            {
                var msg = string.Empty;
                foreach (var item in c)
                {
                    if (item.IsControlExpired)
                    {
                        decimal qty = (item.Quantity ?? 0) * (item.ConversionFactor ?? 0);
                        var ed = new ItemTransactionItemEdCollection();
                        ed.Query.Where(ed.Query.TransactionNo == item.TransactionNo,
                                       ed.Query.SequenceNo == item.SequenceNo);
                        ed.LoadAll();
                        decimal qtyDt = ed.Sum(i => (i.Quantity ?? 0) * (i.ConversionFactor ?? 0));

                        if (qty != qtyDt)
                        {
                            if (msg == string.Empty)
                                msg = item.ItemID;
                            else
                                msg += ", " + item.ItemID;
                        }
                    }
                    else
                    {
                        DateTime defDate = DateTime.Parse("1/1/2999");
                        var ed = new ItemTransactionItemEd();
                        if (!ed.LoadByPrimaryKey(txtTransactionNo.Text, item.SequenceNo, defDate, "-N/A-"))
                            ed.AddNew();
                        ed.TransactionNo = txtTransactionNo.Text;
                        ed.SequenceNo = item.SequenceNo;
                        ed.ExpiredDate = defDate;
                        ed.BatchNumber = "-N/A-";
                        ed.ItemID = item.ItemID;
                        ed.Quantity = item.Quantity;
                        ed.SRItemUnit = item.SRItemUnit;
                        ed.ConversionFactor = item.ConversionFactor;
                        ed.QuantityFinishInBaseUnit = 0;
                        ed.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        ed.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        ed.IsClosed = false;
                        ed.ClosedDateTime = null;
                        ed.ClosedByUserID = null;
                        ed.ReferenceNo = string.Empty;
                        ed.ReferenceSequenceNo = string.Empty;
                        ed.Save();
                    }
                }
                if (msg != string.Empty)
                {
                    args.MessageText = "Data can't be approved. Quantity detail Expiry Date for item: " + msg + " does not match the total quantity transfer.";
                    args.IsCancel = true;
                    return;
                }
            }

            var it = new ItemTransaction();
            if (it.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (it.IsApproved ?? false)
                {
                    args.MessageText = "This transaction already approved";
                    args.IsCancel = true;
                    return;
                }

                if (string.IsNullOrEmpty(it.FromLocationID))
                {
                    args.MessageText = "Location required.";
                    args.IsCancel = true;
                    return;
                }

                if (string.IsNullOrEmpty(it.ToLocationID))
                {
                    args.MessageText = "To Location required.";
                    args.IsCancel = true;
                    return;
                }
            }

            ApproveConsignmentTransfer(args, it);
            if (args.IsCancel) return;
        }

        public static bool ApproveConsignmentTransfer(ValidateArgs args, ItemTransaction it)
        {
            using (var trans = new esTransactionScope())
            {
                var loc = new Location();
                if (loc.LoadByPrimaryKey(it.FromLocationID) && loc.IsHoldForTransaction == true)
                {
                    args.MessageText = "Location: " + loc.LocationName +
                                       " in Hold For Transaction status. Transaction is not allowed.";
                    args.IsCancel = true;
                    return false;
                }

                loc = new Location();
                if (loc.LoadByPrimaryKey(it.ToLocationID) && loc.IsHoldForTransaction == true)
                {
                    args.MessageText = "Location: " + loc.LocationName +
                                       " in Hold For Transaction status. Transaction is not allowed.";
                    args.IsCancel = true;
                    return false;
                }

                bool valid = true;

                string itemId = string.Empty;

                var detailItems = new ItemTransactionItemCollection();
                detailItems.Query.Where(detailItems.Query.TransactionNo == it.TransactionNo);
                detailItems.LoadAll();

                foreach (var dt in detailItems)
                {
                    var ipm = new ItemProductMedic();
                    if (ipm.LoadByPrimaryKey(dt.ItemID))
                    {
                        if (!(ipm.IsInventoryItem ?? false)) continue;
                    }
                    else
                    {
                        var ipnm = new ItemProductNonMedic();
                        ipnm.LoadByPrimaryKey(dt.ItemID);
                        if (!(ipnm.IsInventoryItem ?? false)) continue;
                    }

                    var bal = new ItemBalance();
                    if (bal.LoadByPrimaryKey(it.FromLocationID, dt.ItemID))
                    {
                        if ((bal.Balance - bal.Booking) <= 0)
                        {
                            valid = false;
                            itemId += dt.ItemID + ", ";
                        }
                    }
                    else
                    {
                        valid = false;
                        itemId += dt.ItemID + ", ";
                    }
                }

                if (!valid)
                {
                    args.MessageText = itemId + "has no balance available";
                    args.IsCancel = true;
                    return false;
                }


                it.IsApproved = true;
                it.ApprovedDate = (new DateTime()).NowAtSqlServer(); //DateTime.Now.Date;
                it.ApprovedByUserID = AppSession.UserLogin.UserID;
                it.LastUpdateDateTime = (new DateTime()).NowAtSqlServer(); //DateTime.Now;
                it.LastUpdateByUserID = AppSession.UserLogin.UserID;
                it.Save();

                var chargesBalances = new ItemBalanceCollection();
                var chargesDetailBalances = new ItemBalanceDetailCollection();
                var chargesMovements = new ItemMovementCollection();
                var itemHistory = new ItemTransactionItemHistoryCollection();

                string itemNoStock;

                ItemBalance.PrepareItemBalancesForConsignmentTransfer(detailItems, it.FromServiceUnitID,
                                                                      it.FromLocationID, AppSession.UserLogin.UserID,
                                                                      ref chargesBalances, ref chargesDetailBalances,
                                                                      ref chargesMovements, ref itemHistory, AppSession.Parameter.IsEnabledStockWithEdControl,
                                                                      out itemNoStock);

                if (!string.IsNullOrEmpty(itemNoStock))
                {
                    if (itemNoStock.Length >= 5 && itemNoStock.Substring(0, 5) == "Zero|")
                        args.MessageText = "Zero cost price of item : " + itemNoStock.Replace("Zero|", "");
                    else
                        args.MessageText = "Insufficient balance of item : " + itemNoStock;

                    args.IsCancel = true;
                    return false;
                }

                detailItems.Save();

                if (chargesBalances != null)
                    chargesBalances.Save();
                if (chargesDetailBalances != null)
                    chargesDetailBalances.Save();
                if (chargesMovements != null)
                    chargesMovements.Save();
                if (itemHistory != null)
                    itemHistory.Save();

                /*Add stock to location*/
                chargesBalances = new ItemBalanceCollection();
                chargesDetailBalances = new ItemBalanceDetailCollection();
                var chargesDetailBalanceEds = new ItemBalanceDetailEdCollection();
                chargesMovements = new ItemMovementCollection();

                ItemBalance.PrepareItemBalancesForAutoConsignmentTransfer(detailItems, it, AppSession.UserLogin.UserID,
                                                                          ref chargesBalances, ref chargesDetailBalances,
                                                                          ref chargesMovements, ref chargesDetailBalanceEds, 
                                                                          AppSession.Parameter.IsEnabledStockWithEdControl);

                if (chargesBalances != null)
                    chargesBalances.Save();
                if (chargesDetailBalances != null)
                    chargesDetailBalances.Save();
                if (chargesDetailBalanceEds != null)
                    chargesDetailBalanceEds.Save();
                if (chargesMovements != null)
                    chargesMovements.Save();

                var app = new AppParameter();
                app.LoadByPrimaryKey("acc_IsAutoJournalStockAdjustment");
                if (app.ParameterValue == "Yes")
                {
                    var isClosingPeriod = PostingStatus.IsPeriodeClosed(it.TransactionDate.Value);
                    if (isClosingPeriod)
                    {
                        args.MessageText = "Financial statements for period: " +
                                           string.Format("{0:MMMM-yyyy}", it.TransactionDate.Value) +
                                           " have been closed. Please contact the authorities.";
                        args.IsCancel = true;
                        return false;
                    }

                    /* Automatic Journal Testing Start */

                    int? journalId = JournalTransactions.AddNewInventoryStockAdjustmentJournal(it, AppSession.UserLogin.UserID, 0, "SA", 0, true);

                    /* Automatic Journal Testing End */
                }

                trans.Complete();
            }

            return true;
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            (new ItemTransaction()).Void(txtTransactionNo.Text, AppSession.UserLogin.UserID);
            grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = false;
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            (new ItemTransaction()).UnVoid(txtTransactionNo.Text, AppSession.UserLogin.UserID);
            grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = AppSession.Parameter.IsEnabledStockWithEdControl;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ItemTransaction());
            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.ConsignmentTransfer, false);
            cboToLocationID.Text = string.Empty;
            cboFromLocationID.Text = string.Empty;
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();// DateTime.Now;
            PopulateNewTransactionNo();
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
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
            if (string.IsNullOrEmpty(cboSupplierID.SelectedValue) || string.IsNullOrEmpty(cboSupplierID.Text))
            {
                args.MessageText = AppConstant.Message.SelectValidSupplier;
                args.IsCancel = true;
                return;
            }
            PopulateNewTransactionNo();
            // save autonumber immediately to decrease time gap between create and save
            _autoNumber.Save();

            var entity = new ItemTransaction();
            SetEntityValue(entity);
            if (ItemTransactionItems.Where(x => x.TransactionNo == txtTransactionNo.Text).Count() == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboSupplierID.SelectedValue) || string.IsNullOrEmpty(cboSupplierID.Text))
            {
                args.MessageText = AppConstant.Message.SelectValidSupplier;
                args.IsCancel = true;
                return;
            }

            var entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                if (ItemTransactionItems.Where(x => x.TransactionNo == txtTransactionNo.Text).Count() == 0)
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
            var entity = new ItemTransaction();
            if (parameters.Length > 0)
            {
                var transactionNo = parameters[0];
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
            var itemTransaction = (ItemTransaction)entity;
            txtTransactionNo.Text = itemTransaction.TransactionNo;
            txtTransactionDate.SelectedDate = itemTransaction.TransactionDate;
            ComboBox.PopulateWithOneSupplier(cboSupplierID, itemTransaction.BusinessPartnerID ?? string.Empty);
            ComboBox.PopulateWithOneLocation(cboFromLocationID, itemTransaction.FromLocationID ?? string.Empty);
            ComboBox.PopulateWithOneServiceUnit(cboToServiceUnitID, itemTransaction.ToServiceUnitID ?? string.Empty);
            ComboBox.PopulateWithOneLocation(cboToLocationID, itemTransaction.ToLocationID ?? string.Empty);
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboToServiceUnitID.SelectedValue, TransactionCode.ConsignmentTransfer);
            cboSRItemType.SelectedValue = itemTransaction.SRItemType;
            chkIsVoid.Checked = itemTransaction.IsVoid ?? false;
            chkIsApproved.Checked = itemTransaction.IsApproved ?? false;
            txtNotes.Text = itemTransaction.Notes;

            //Display Data Detail
            PopulateGridDetail();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ItemTransaction entity)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionCode = TransactionCode.ConsignmentTransfer;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.BusinessPartnerID = cboSupplierID.SelectedValue;
            entity.FromServiceUnitID = cboToServiceUnitID.SelectedValue;
            entity.FromLocationID = cboFromLocationID.SelectedValue;
            entity.ToServiceUnitID = cboToServiceUnitID.SelectedValue;
            entity.ToLocationID = cboToLocationID.SelectedValue;
            entity.SRItemType = cboSRItemType.SelectedValue;
            entity.Notes = txtNotes.Text;
            entity.ChargesAmount = 0;
            entity.TaxAmount = 0;
            entity.TaxPercentage = 0;
            entity.IsTaxable = 0;
            entity.CurrencyID = AppSession.Parameter.CurrencyRupiahID;
            entity.CurrencyRate = 1;
            entity.IsInventoryItem = true;
            entity.IsNonMasterOrder = false;
            entity.IsAssets = false;
            entity.IsConsignment = true;
            entity.IsConsignmentAlreadyReceived = true;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;

            //Update Detil
            foreach (var item in ItemTransactionItems)
            {
                if (item.es.IsAdded)
                {
                    item.TransactionNo = txtTransactionNo.Text;
                }
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
            }
        }

        private void SaveEntity(ItemTransaction entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                ItemTransactionItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ItemTransactionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TransactionNo > txtTransactionNo.Text && que.TransactionCode == TransactionCode.ConsignmentTransfer);
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < txtTransactionNo.Text && que.TransactionCode == TransactionCode.ConsignmentTransfer);
                que.OrderBy(que.TransactionNo.Descending);
            }
            var entity = new ItemTransaction();
            entity.Load(que);
            OnPopulateEntryControl(entity);

            if (AppSession.Parameter.IsEnabledStockWithEdControl)
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = !chkIsApproved.Checked && !chkIsVoid.Checked; // ed ico
            else grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = false;
        }

        #endregion

        #region Record Detail Method Function

        private ItemTransactionItemCollection ItemTransactionItems
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["ConsignmentTransferItems" + Request.UserHostName];
                    if (obj != null)
                        return ((ItemTransactionItemCollection)(obj));
                }

                var coll = new ItemTransactionItemCollection();

                var query = new ItemTransactionItemQuery("a");
                var iq = new ItemQuery("b");
                var vwip = new VwItemProductMedicNonMedicQuery("vw");

                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                query.InnerJoin(vwip).On(vwip.ItemID == query.ItemID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.ItemID.Ascending);

                query.Select(
                    query,
                    iq.ItemName.As("refToItem_ItemName"), 
                    vwip.IsControlExpired.As("refToItemProduct_IsControlExpired"),
                    @"<CASE WHEN a.Quantity * a.ConversionFactor > ISNULL((SELECT SUM(itie.Quantity * itie.ConversionFactor)
                        FROM ItemTransactionItemEd AS itie 
                        WHERE itie.TransactionNo = a.TransactionNo AND itie.SequenceNo = a.SequenceNo), 0) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS 'refToItemProduct_IsNotCompleteED'>"
                    );

                coll.Load(query);
                Session["ConsignmentTransferItems" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["ConsignmentTransferItems" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            var isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemTransactionItem.Columns[0].Visible = isVisible;
            grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 1].Visible = isVisible;

            if (AppSession.Parameter.IsEnabledStockWithEdControl)
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = !isVisible && !chkIsApproved.Checked; // ed ico; 
            else
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = false; 

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
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            var sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemTransactionItemMetadata.ColumnNames.SequenceNo]);
            var entity = FindItemTransactionItem(sequenceNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        private ItemTransactionItem FindItemTransactionItem(String sequenceNo)
        {
            return ItemTransactionItems.Where(x => x.SequenceNo == sequenceNo &&
                (x.TransactionNo ?? string.Empty) == (x.es.IsAdded ? string.Empty : txtTransactionNo.Text)).First();
        }

        protected void grdItemTransactionItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemTransactionItemMetadata.ColumnNames.SequenceNo]);
            var entity = FindItemTransactionItem(sequenceNo);
            if (entity != null)
                entity.MarkAsDeleted();

            cboSRItemType.Enabled = !(ItemTransactionItems.Count > 0);
        }

        protected void grdItemTransactionItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ItemTransactionItems.AddNew();
            SetEntityValue(entity, e);

            //grid not close first
            e.Canceled = true;
            grdItemTransactionItem.Rebind();
        }

        private void SetEntityValue(ItemTransactionItem entity, GridCommandEventArgs e)
        {
            var userControl = (ConsignmentTransferItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.SequenceNo = userControl.SequenceNo;
                entity.Quantity = userControl.Quantity;
                entity.ItemName = userControl.ItemName;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ConversionFactor = 1; //Transaksi Consignment Receive selalu dalam Base Unit

                if (cboSRItemType.SelectedValue == ItemType.Medical)
                {
                    var i = new ItemProductMedic();
                    i.LoadByPrimaryKey(entity.ItemID);
                    entity.Price = i.PriceInBaseUnit;
                    entity.Discount1Percentage = i.PurchaseDiscount1;
                    entity.Discount2Percentage = i.PurchaseDiscount2;
                    entity.IsControlExpired = i.IsControlExpired ?? false;
                }
                else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                {
                    var i = new ItemProductNonMedic();
                    i.LoadByPrimaryKey(entity.ItemID);
                    entity.Price = i.PriceInBaseUnit;
                    entity.Discount1Percentage = i.PurchaseDiscount1;
                    entity.Discount2Percentage = i.PurchaseDiscount2;
                    entity.IsControlExpired = i.IsControlExpired ?? false;
                }
                else if (cboSRItemType.SelectedValue == ItemType.Kitchen)
                {
                    var i = new ItemKitchen();
                    i.LoadByPrimaryKey(entity.ItemID);
                    entity.Price = i.PriceInBaseUnit;
                    entity.Discount1Percentage = i.PurchaseDiscount1;
                    entity.Discount2Percentage = i.PurchaseDiscount2;
                    entity.IsControlExpired = i.IsControlExpired ?? false;
                }

                var suppItem = new SupplierItem();
                if (suppItem.LoadByPrimaryKey(cboSupplierID.SelectedValue, entity.ItemID))
                {
                    entity.Price = suppItem.PriceInPurchaseUnit/suppItem.ConversionFactor;
                    entity.Discount1Percentage = suppItem.PurchaseDiscount1;
                    entity.Discount2Percentage = suppItem.PurchaseDiscount2;
                }
                
                entity.PriceInCurrency = entity.Price;
                entity.Discount = (entity.Price * entity.Discount1Percentage / 100) +
                                  ((entity.Price - (entity.Price * entity.Discount1Percentage / 100)) *
                                   entity.Discount2Percentage / 100);
                entity.DiscountInCurrency = entity.Discount;
                entity.IsBonusItem = false;
                entity.IsNotCompleteED = true;
            }
        }

        #endregion

        #region ComboBox SupplierID

        protected void cboSupplierID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSupplierID((RadComboBox)sender, e.Text);
        }

        private static void PopulateCboSupplierID(BaseDataBoundControl comboBox, string textSearch)
        {
            string searchText = string.Format("%{0}%", textSearch);
            var query = new SupplierQuery();

            query.Select(
                query.SupplierID,
                query.SupplierName,
                (query.StreetName + " " + query.City + " " + query.ZipCode).Trim().As("Address")
                );
            query.Where(
                query.Or(
                    query.SupplierID == textSearch,
                    query.SupplierName.Like(searchText)
                    ), query.IsActive == true
                );

            query.es.Top = 20;

            comboBox.DataSource = query.LoadDataTable();
            comboBox.DataBind();
        }

        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SupplierName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SupplierID"].ToString();
        }

        protected void cboSupplierID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComboBox.PopulateWithSupplierForLocation(cboFromLocationID, cboSupplierID.SelectedValue);
            cboFromLocationID.SelectedIndex = cboFromLocationID.Items.Count == 2 ? 1 : 0;
        }

        #endregion

        protected void cboToServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateNewTransactionNo();
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboToServiceUnitID.SelectedValue, TransactionCode.ConsignmentTransfer);
            ComboBox.PopulateWithServiceUnitForLocation(cboToLocationID, e.Value);
            cboToLocationID.SelectedIndex = 1;
        }
    }
}

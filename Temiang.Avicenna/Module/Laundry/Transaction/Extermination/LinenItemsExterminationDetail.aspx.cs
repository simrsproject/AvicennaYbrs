using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LinenItemsExterminationDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New)
                return;
            if (cboFromServiceUnitID.SelectedValue == string.Empty)
                return;

            var serv = new ServiceUnit();
            if (serv.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue))
            {
                _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, BusinessObject.Reference.TransactionCode.LinenItemsExtermination, serv.DepartmentID);
                txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "LinenItemsExterminationSearch.aspx";
            UrlPageList = "LinenItemsExterminationList.aspx";

            ProgramID = AppConstant.Program.LinenItemsExtermination;

            WindowSearch.Height = 400;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRAdjustmentType, AppEnum.StandardReference.LinenExterminationReason);
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromServiceUnitID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, txtTransactionNo);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromLocationID);

            ajax.AddAjaxSetting(grdItemTransactionItem, grdItemTransactionItem);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboFromServiceUnitID);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
            string fromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.LinenItemsExtermination, true);
            cboFromServiceUnitID.SelectedValue = fromServiceUnitID;

            cboFromLocationID.Enabled = ItemTransactionItems.Count == 0;
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

            var loc = new Location();
            if (loc.LoadByPrimaryKey(cboFromLocationID.SelectedValue) && loc.IsHoldForTransaction == true)
            {
                args.MessageText = "Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.";
                args.IsCancel = true;
                return;
            }

            using (var trans = new esTransactionScope())
            {
                var entity = new ItemTransaction();
                entity.LoadByPrimaryKey(txtTransactionNo.Text);

                entity.IsApproved = true;
                entity.ApprovedDate = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                entity.Save();

                // stock calculation
                var chargesBalances = new ItemBalanceCollection();
                var chargesDetailBalances = new ItemBalanceDetailCollection();
                var itemBalanceDetailEds = new ItemBalanceDetailEdCollection();
                var chargesMovements = new ItemMovementCollection();

                string itemNoStock;
                var itemTransactionItems = ItemTransactionItems;

                ItemBalance.PrepareItemBalancesForDestructionOfExpiredItems(entity, itemTransactionItems,
                                                                            cboFromServiceUnitID.SelectedValue,
                                                                            cboFromLocationID.SelectedValue,
                                                                            AppSession.UserLogin.UserID,
                                                                            ref chargesBalances,
                                                                            ref chargesDetailBalances,
                                                                            ref chargesMovements, ref itemBalanceDetailEds, AppSession.Parameter.IsEnabledStockWithEdControl,
                                                                            out itemNoStock);

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

                trans.Complete();

                var app = new AppParameter();
                app.LoadByPrimaryKey("acc_IsAutoJournalInvIssue");
                if (app.ParameterValue == "Yes")
                {
                    var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.TransactionDate.Value);
                    if (isClosingPeriod)
                    {
                        args.MessageText = "Financial statements for period: " +
                                           string.Format("{0:MMMM-yyyy}", entity.TransactionDate.Value) +
                                           " have been closed. Please contact the authorities.";
                        args.IsCancel = true;
                        return;
                    }

                    /* Automatic Journal Testing Start */

                    int? journalId = JournalTransactions.AddNewInventoryIssueJournal(entity, AppSession.UserLogin.UserID, 0);

                    /* Automatic Journal Testing End */
                }
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
            ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.LinenItemsExtermination, true);
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();// DateTime.Now;
            PopulateNewTransactionNo();
            cboFromServiceUnitID.Text = string.Empty;
            cboFromLocationID.Items.Clear();
            cboFromLocationID.Text = string.Empty;
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
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (ItemTransactionItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            PopulateNewTransactionNo();
            // save autonumber immediately to decrease time gap between create and save
            _autoNumber.Save();

            var entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new ItemTransaction();
            entity.AddNew();
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
            if (ItemTransactionItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
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
            var itemTransaction = (ItemTransaction)entity;
            txtTransactionNo.Text = itemTransaction.TransactionNo;
            txtTransactionDate.SelectedDate = itemTransaction.TransactionDate;
            ComboBox.PopulateWithOneServiceUnit(cboFromServiceUnitID, itemTransaction.FromServiceUnitID ?? string.Empty);
            if (!string.IsNullOrEmpty(itemTransaction.FromServiceUnitID))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboFromLocationID, itemTransaction.FromServiceUnitID);
                if (!string.IsNullOrEmpty(itemTransaction.FromLocationID))
                    cboFromLocationID.SelectedValue = itemTransaction.FromLocationID;
                else
                    cboFromLocationID.SelectedIndex = 1;
            }
            cboSRAdjustmentType.SelectedValue = itemTransaction.SRAdjustmentType;
            txtNotes.Text = itemTransaction.Notes;

            chkIsVoid.Checked = itemTransaction.IsVoid ?? false;
            chkIsApproved.Checked = itemTransaction.IsApproved ?? false;

            //Display Data Detail
            PopulateGridDetail();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ItemTransaction entity)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionCode = BusinessObject.Reference.TransactionCode.LinenItemsExtermination;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.ReferenceNo = string.Empty;
            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            entity.FromLocationID = cboFromLocationID.SelectedValue;
            entity.SRItemType = BusinessObject.Reference.ItemType.NonMedical;
            entity.SRAdjustmentType = cboSRAdjustmentType.SelectedValue;
            entity.Notes = txtNotes.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
            }

            //Update Detil
            foreach (ItemTransactionItem item in ItemTransactionItems)
            {
                if (item.es.IsAdded)
                {
                    item.TransactionNo = txtTransactionNo.Text;
                }
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

                //autonumber has been saved on SetEntity
                //if (DataModeCurrent == AppEnum.DataMode.New)
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
                        que.TransactionCode == BusinessObject.Reference.TransactionCode.LinenItemsExtermination
                    );
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.TransactionNo < txtTransactionNo.Text &&
                        que.TransactionCode == BusinessObject.Reference.TransactionCode.LinenItemsExtermination
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new ItemTransaction();
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
                    object obj = Session["collLinenDestructionItems" + Request.UserHostName];
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
                Session["collLinenDestructionItems" + Request.UserHostName] = coll;

                return coll;
            }
            set { Session["collLinenDestructionItems" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemTransactionItem.Columns[0].Visible = isVisible;
            grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 1].Visible = isVisible;

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
        }

        private ItemTransactionItem FindItemTransactionItem(String sequenceNo)
        {
            return ItemTransactionItems.Where(x => x.SequenceNo == sequenceNo &&
                (x.TransactionNo ?? string.Empty) == (x.es.IsAdded ? string.Empty : txtTransactionNo.Text)).First();

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
            cboFromLocationID.Enabled = !(ItemTransactionItems.Count > 0);
        }

        protected void grdItemTransactionItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemTransactionItem entity = ItemTransactionItems.AddNew();
            SetEntityValue(entity, e);

            //grid not close first
            e.Canceled = true;
            grdItemTransactionItem.Rebind();
        }

        private void SetEntityValue(ItemTransactionItem entity, GridCommandEventArgs e)
        {
            var userControl = (LinenItemsExterminationDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.SequenceNo = userControl.SequenceNo;
                entity.Quantity = userControl.Quantity;
                entity.ItemName = userControl.ItemName;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ConversionFactor = 1; //Transaksi inventory out selalu dalam Base Unit

                var nonMed = new ItemProductNonMedic();
                nonMed.LoadByPrimaryKey(entity.ItemID);
                entity.CostPrice = nonMed.CostPrice;
                entity.Price = nonMed.PriceInBasedUnitWVat;
            }
        }

        #endregion

        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateNewTransactionNo();
            ComboBox.PopulateWithServiceUnitForLocation(cboFromLocationID, e.Value);
            cboFromLocationID.SelectedIndex = 1;
        }
    }
}
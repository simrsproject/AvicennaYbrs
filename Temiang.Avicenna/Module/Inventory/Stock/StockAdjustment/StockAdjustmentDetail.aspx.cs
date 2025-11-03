using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Stock
{
    public partial class StockAdjustmentDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "StockAdjustmentSearch.aspx?type=" + FormType;
            UrlPageList = "StockAdjustmentList.aspx?type=" + FormType;

            ProgramID = FormType == "p" ? AppConstant.Program.StockAdjustmentPlus : AppConstant.Program.StockAdjustment;

            WindowSearch.Height = 400;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.StockAdjustment, true);
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
                StandardReference.InitializeIncludeSpace(cboSRAdjustmentType, AppEnum.StandardReference.AdjustmentType);

                grdItemTransactionItem.Columns.FindByUniqueName("ExpiredDate").Visible = AppSession.Parameter.IsEnabledStockWithEdControl;
                grdItemTransactionItem.Columns.FindByUniqueName("BatchNumber").Visible = AppSession.Parameter.IsEnabledStockWithEdControl;

                ItemTransactionItems = null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItemTransactionItem, grdItemTransactionItem);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboSRItemType);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboFromServiceUnitID);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboFromLocationID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromServiceUnitID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromLocationID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboSRItemType);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new ItemTransaction();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
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
            if (ItemTransactionItems.Count() == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            string itemZeroCostPrice;
            ItemTransaction.UpdateCostPrice(ItemTransactionItems, out itemZeroCostPrice);
            if (!string.IsNullOrEmpty(itemZeroCostPrice))
            {
                args.MessageText = "Zero cost price of item : " + itemZeroCostPrice;
                args.IsCancel = true;
                return;
            }

            var loc = new Location();
            if (loc.LoadByPrimaryKey(entity.FromLocationID) && loc.IsHoldForTransaction == true)
            {
                args.MessageText = "Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.";
                args.IsCancel = true;
                return;
            }

            AppParameter app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalStockAdjustment");
            if (app.ParameterValue == "Yes")
            {
                var closingperiod = entity.TransactionDate.Value.Date;
                var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod);
                if (isClosingPeriod)
                {
                    args.MessageText = "Financial statements for period: " +
                                       string.Format("{0:MMMM-yyyy}", closingperiod) +
                                       " have been closed. Please contact the authorities.";
                    args.IsCancel = true;
                    return;
                }
            }

            try
            {
                SetApproval(entity, true);
            }
            catch (Exception ex) {
                args.MessageText = ex.Message;
                args.IsCancel = true;
                return;
            }

            if (FormType == "p" && AppSession.Parameter.IsAutoPrintStockAdjustmentReceipt)
            {
                var printJobParameters = new PrintJobParameterCollection();
                printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);

                PrintManager.CreatePrintJob(AppSession.Parameter.ProgramIdPrintStockAdjustmentReceipt, printJobParameters);
            }
        } 

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new ItemTransaction();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
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
            var loc = new Location();
            if (loc.LoadByPrimaryKey(entity.FromLocationID) && loc.IsHoldForTransaction == true)
            {
                args.MessageText = "Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.";
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, false);
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new ItemTransaction();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
            }

            SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            var entity = new ItemTransaction();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }

            SetVoid(entity, false);
       }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ItemTransaction());

            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer().Date;
            cboFromServiceUnitID.SelectedValue = string.Empty;
            cboFromServiceUnitID.Text = string.Empty;
            cboFromLocationID.Items.Clear();
            cboFromLocationID.Text = string.Empty;

            _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, AppEnum.AutoNumber.StockAdjustment);
            txtTransactionNo.Text = _autoNumber.LastCompleteNumber;

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
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
            if (string.IsNullOrEmpty(cboSRAdjustmentType.SelectedValue))
            {
                args.MessageText = "Invalid Adjustment Type";
                args.IsCancel = true;
                return;
            }
            if (ItemTransactionItems.Where(x => x.TransactionNo == txtTransactionNo.Text).Count() == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, AppEnum.AutoNumber.StockAdjustment);
            txtTransactionNo.Text = _autoNumber.LastCompleteNumber;

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
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboSRAdjustmentType.SelectedValue))
            {
                args.MessageText = "Invalid Adjustment Type";
                args.IsCancel = true;
                return;
            }

            var entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (ItemTransactionItems.Where(x => x.TransactionNo == txtTransactionNo.Text).Count() == 0)
                {
                    args.MessageText = AppConstant.Message.RecordDetailEmpty;
                    args.IsCancel = true;
                    return;
                }

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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "ItemTransaction";
        }

        protected override void OnMenuEditClick()
        {

            if (!(OnGetStatusMenuApproval()??true))
            {
                DataModeCurrent = AppEnum.DataMode.Read;
                RefreshMenuStatus();
            }

            cboFromServiceUnitID.Enabled = (ItemTransactionItems.Count == 0);
            cboFromLocationID.Enabled = (ItemTransactionItems.Count == 0);
            cboSRItemType.Enabled = (ItemTransactionItems.Count == 0);
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
            return !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemItemTransactionItem(newVal);
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

            ComboBox.SelectedValue(cboFromServiceUnitID, itemTransaction.FromServiceUnitID);

            if (!string.IsNullOrEmpty(itemTransaction.FromServiceUnitID))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboFromLocationID, itemTransaction.FromServiceUnitID);
                if (!string.IsNullOrEmpty(itemTransaction.FromLocationID))
                {
                    ComboBox.SelectedValue(cboFromLocationID,itemTransaction.FromLocationID);
                }
                else
                    cboFromLocationID.SelectedIndex = 1;
            }
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue,
                                                                   TransactionCode.StockAdjustment);
            //cboSRItemType.SelectedValue = itemTransaction.SRItemType; // Tidak berubah dilayarnya jika kosong nilainya (Handono)
            ComboBox.SelectedValue(cboSRItemType, itemTransaction.SRItemType);

            ComboBox.SelectedValue(cboSRAdjustmentType,itemTransaction.SRAdjustmentType);
            txtNotes.Text = itemTransaction.Notes;

            ViewState["IsApproved"] = itemTransaction.IsApproved ?? false;
            ViewState["IsVoid"] = itemTransaction.IsVoid ?? false;

            //Display Data Detail
            PopulateItemTransactionItemGrid();
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuDelete.Enabled = !(bool)ViewState["IsApproved"] && !(bool)ViewState["IsVoid"];
        }
        #endregion

        #region Private Method Standard

        private void SetEntityValue(esItemTransaction entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, AppEnum.AutoNumber.StockAdjustment);
                txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionCode = BusinessObject.Reference.TransactionCode.StockAdjustment;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            entity.FromLocationID = cboFromLocationID.SelectedValue;

            entity.SRItemType = cboSRItemType.SelectedValue;
            entity.SRAdjustmentType = cboSRAdjustmentType.SelectedValue;
            entity.Notes = txtNotes.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            //Detail
            foreach (var detail in ItemTransactionItems)
            {
                detail.TransactionNo = entity.TransactionNo;
                detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                detail.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(esEntity entity)
        {
            using (var trans = new esTransactionScope())
            {
                //autonumber has been saved on SetEntity
                //if (entity.es.IsAdded)
                //    _autoNumber.Save();

                entity.Save();
                ItemTransactionItems.Save();

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
                        que.TransactionNo > txtTransactionNo.Text,
                        que.TransactionCode == BusinessObject.Reference.TransactionCode.StockAdjustment
                    );
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.TransactionNo < txtTransactionNo.Text,
                        que.TransactionCode == BusinessObject.Reference.TransactionCode.StockAdjustment
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }
            if (!string.IsNullOrEmpty(FormType))
            {
                var iti = new ItemTransactionItemQuery("iti");
                que.InnerJoin(iti).On(que.TransactionNo == iti.TransactionNo);
                if (FormType == "p")
                    que.Where(iti.Quantity > 0);
                else que.Where(iti.Quantity < 0);
            }

            var entity = new ItemTransaction();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function ItemTransactionItem

        private void RefreshCommandItemItemTransactionItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            var isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemTransactionItem.Columns[0].Visible = isVisible;
            grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 1].Visible = isVisible;

            grdItemTransactionItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdItemTransactionItem.Rebind();
        }

        private ItemTransactionItemCollection ItemTransactionItems
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["StockAdjustmentItems" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((ItemTransactionItemCollection)(obj));
                    }
                }

                var coll = new ItemTransactionItemCollection();

                var query = new ItemTransactionItemQuery("a");
                var item = new ItemQuery("b");
                query.Select
                    (
                        query,
                        item.ItemName.As("refToItem_ItemName")
                    );
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);

                if (cboSRItemType.SelectedValue == ItemType.Medical)
                {
                    var ipq = new ItemProductMedicQuery("c");
                    query.InnerJoin(ipq).On(query.ItemID == ipq.ItemID);
                    query.Select(@"<ISNULL(c.IsControlExpired, 0) AS 'refToItemProduct_IsControlExpired'>");
                }
                else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                {
                    var ipq = new ItemProductNonMedicQuery("c");
                    query.InnerJoin(ipq).On(query.ItemID == ipq.ItemID);
                    query.Select(@"<ISNULL(c.IsControlExpired, 0) AS 'refToItemProduct_IsControlExpired'>");
                }
                else
                {
                    var ipq = new ItemKitchenQuery("c");
                    query.InnerJoin(ipq).On(query.ItemID == ipq.ItemID);
                    query.Select(@"<ISNULL(c.IsControlExpired, 0) AS 'refToItemProduct_IsControlExpired'>");
                }

                query.OrderBy(query.SequenceNo.Ascending);
                coll.Load(query);

                Session["StockAdjustmentItems" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["StockAdjustmentItems" + Request.UserHostName] = value; }
        }

        private void PopulateItemTransactionItemGrid()
        {
            //Display Data Detail
            ItemTransactionItems = null; //Reset Record Detail
            grdItemTransactionItem.DataSource = ItemTransactionItems; //Requery
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

        protected void grdItemTransactionItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            var sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemTransactionItemMetadata.ColumnNames.SequenceNo]);
            var entity = FindItemTransactionItem(sequenceNo);
            if (entity != null)
                entity.MarkAsDeleted();

            if (ItemTransactionItems.Count == 0)
            {
                cboFromServiceUnitID.Enabled = true;
                cboFromLocationID.Enabled = true;
                cboSRItemType.Enabled = true;
            }
        }

        protected void grdItemTransactionItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ItemTransactionItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemTransactionItem.Rebind();
        }

        private ItemTransactionItem FindItemTransactionItem(String sequenceNo)
        {
            var coll = ItemTransactionItems;
            return coll.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        private void SetEntityValue(ItemTransactionItem entity, GridCommandEventArgs e)
        {
            var userControl = (StockAdjustmentItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = txtTransactionNo.Text;
                entity.SequenceNo = userControl.SequenceNo;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Quantity = userControl.Quantity;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ConversionFactor = 1; //Transaksi inventory out selalu dalam Base Unit
                if (cboSRItemType.SelectedValue == ItemType.Medical)
                {
                    var med = new ItemProductMedic();
                    med.LoadByPrimaryKey(userControl.ItemID);
                    entity.CostPrice = med.CostPrice;
                    entity.Price = med.PriceInBasedUnitWVat;
                    entity.IsControlExpired = med.IsControlExpired ?? false;
                }
                else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                {
                    var nonMed = new ItemProductNonMedic();
                    nonMed.LoadByPrimaryKey(userControl.ItemID);
                    entity.CostPrice = nonMed.CostPrice;
                    entity.Price = nonMed.PriceInBasedUnitWVat;
                    entity.IsControlExpired = nonMed.IsControlExpired ?? false;
                }
                else if (cboSRItemType.SelectedValue == ItemType.Kitchen)
                {
                    var kitchen = new ItemKitchen();
                    kitchen.LoadByPrimaryKey(userControl.ItemID);
                    entity.CostPrice = kitchen.CostPrice;
                    entity.Price = kitchen.PriceInBasedUnitWVat;
                    entity.IsControlExpired = kitchen.IsControlExpired ?? false;
                }
                entity.BatchNumber = userControl.BatchNumber;
                entity.ExpiredDate = userControl.ExpiredDate;
                if (entity.ExpiredDate == Convert.ToDateTime("1/1/2999"))
                {
                    entity.BatchNumber = "-N/A-";
                    entity.ExpiredDate = null;
                }
            }
        }

        #endregion

        private void SetApproval(ItemTransaction entity, bool isApproval)
        {
            //header
            entity.IsApproved = isApproval;
            entity.ApprovedByUserID = AppSession.UserLogin.UserID;
            entity.ApprovedDate = (new DateTime()).NowAtSqlServer();
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (var trans = new esTransactionScope())
            {
                entity.Save();

                // stock calculation
                var chargesBalances = new ItemBalanceCollection();
                var chargesDetailBalances = new ItemBalanceDetailCollection();
                var chargesMovements = new ItemMovementCollection();
                var chargesDetailEdBalances = new ItemBalanceDetailEdCollection();

                var itemTransactionItems = ItemTransactionItems;

                if (!AppSession.Parameter.IsEnabledStockWithEdControl)
                {
                    ItemBalance.PrepareItemBalancesForAdjustment(itemTransactionItems, entity.FromServiceUnitID, entity.FromLocationID,
                        AppSession.UserLogin.UserID, ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, ref chargesDetailEdBalances);
                }
                else
                {
                    ItemBalance.PrepareItemBalancesForAdjustmentWithEdControl(itemTransactionItems, entity.FromServiceUnitID, entity.FromLocationID,
                        AppSession.UserLogin.UserID, ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, ref chargesDetailEdBalances);
                }

                itemTransactionItems.Save();

                if (chargesBalances != null)
                    chargesBalances.Save();
                if (chargesDetailBalances != null)
                    chargesDetailBalances.Save();
                if (chargesDetailEdBalances != null)
                    chargesDetailEdBalances.Save();
                if (chargesMovements != null)
                    chargesMovements.Save();

                //Commit if success, Rollback if failed

                AppParameter app = new AppParameter();
                app.LoadByPrimaryKey("acc_IsAutoJournalStockAdjustment");
                if (app.ParameterValue == "Yes")
                {
                    /* Automatic Journal Testing Start */

                    int? journalId = JournalTransactions.AddNewInventoryStockAdjustmentJournal(entity, AppSession.UserLogin.UserID, 0, "SA", 0, false);

                    /* Automatic Journal Testing End */
                }

                trans.Complete();
            }
        }

        private void SetVoid(ItemTransaction entity, bool isVoid)
        {
            //header
            entity.IsVoid = isVoid;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (var trans = new esTransactionScope())
            {
                entity.Save();


                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComboBox.PopulateWithServiceUnitForLocation(cboFromLocationID, e.Value);
            cboFromLocationID.SelectedIndex = 1;
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, TransactionCode.StockAdjustment);
        }
    }
}

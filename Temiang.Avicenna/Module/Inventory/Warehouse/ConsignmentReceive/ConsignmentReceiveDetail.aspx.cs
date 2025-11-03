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
    public partial class ConsignmentReceiveDetail : BasePageDetail
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
                _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, TransactionCode.ConsignmentReceive, serv.DepartmentID);
                txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
            }
        }

        protected void cboToServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateNewTransactionNo();
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboToServiceUnitID.SelectedValue, TransactionCode.ConsignmentReceive);
        }

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
                    ),query.IsActive == true
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
            ComboBox.PopulateWithSupplierForLocation(cboToLocationID, cboSupplierID.SelectedValue);
            cboToLocationID.SelectedIndex = cboToLocationID.Items.Count == 2 ? 1 : 0;
        }

        #endregion

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ConsignmentReceiveSearch.aspx";
            UrlPageList = "ConsignmentReceiveList.aspx";

            ProgramID = AppConstant.Program.ConsignmentReceive;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItemTransactionItem, grdItemTransactionItem);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboSRItemType);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboSupplierID);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboToLocationID);

            ajax.AddAjaxSetting(cboToServiceUnitID, cboToServiceUnitID);
            ajax.AddAjaxSetting(cboToServiceUnitID, txtTransactionNo);
            ajax.AddAjaxSetting(cboToServiceUnitID, cboSRItemType);

            ajax.AddAjaxSetting(cboSupplierID, cboSupplierID);
            ajax.AddAjaxSetting(cboSupplierID, cboToLocationID);
            
            ajax.AddAjaxSetting(AjaxManager, grdItemTransactionItem);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.ConsignmentReceive, true, cboToServiceUnitID.SelectedValue, string.Empty);

            cboSRItemType.Enabled = (ItemTransactionItems.Count == 0);
            cboToServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboSupplierID.Enabled = cboSRItemType.Enabled;
            cboToLocationID.Enabled = cboSRItemType.Enabled;
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
                foreach (var item in c)
                {
                    DateTime defDate = DateTime.Parse("1/1/2999");
                    var ed = new ItemTransactionItemEd();
                    if (!ed.LoadByPrimaryKey(txtTransactionNo.Text, item.SequenceNo, defDate, ""))
                        ed.AddNew();
                    ed.TransactionNo = txtTransactionNo.Text;
                    ed.SequenceNo = item.SequenceNo;
                    ed.ExpiredDate = defDate;
                    ed.BatchNumber = "";
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

            (new ItemTransaction()).Approve(txtTransactionNo.Text, ItemTransactionItems, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            (new ItemTransaction()).Void(txtTransactionNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            (new ItemTransaction()).UnVoid(txtTransactionNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ItemTransaction());
            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.ConsignmentReceive, true);
            cboToLocationID.Text = string.Empty;
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

            if (string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue) || string.IsNullOrEmpty(cboToServiceUnitID.Text))
            {
                args.MessageText = "Service Unit required.";
                args.IsCancel = true;
                return;
            }
            var unit = new ServiceUnit();
            if (!unit.LoadByPrimaryKey(cboToServiceUnitID.SelectedValue))
            {
                args.MessageText = "Invalid Service Unit.";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboToLocationID.SelectedValue) || string.IsNullOrEmpty(cboToLocationID.Text))
            {
                args.MessageText = "Location required.";
                args.IsCancel = true;
                return;
            }
            var loc = new Location();
            if (!loc.LoadByPrimaryKey(cboToLocationID.SelectedValue))
            {
                args.MessageText = "Invalid Location.";
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

            if (string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue) || string.IsNullOrEmpty(cboToServiceUnitID.Text))
            {
                args.MessageText = "Service Unit required.";
                args.IsCancel = true;
                return;
            }
            var unit = new ServiceUnit();
            if (!unit.LoadByPrimaryKey(cboToServiceUnitID.SelectedValue))
            {
                args.MessageText = "Invalid Service Unit.";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboToLocationID.SelectedValue) || string.IsNullOrEmpty(cboToLocationID.Text))
            {
                args.MessageText = "Location required.";
                args.IsCancel = true;
                return;
            }
            var loc = new Location();
            if (!loc.LoadByPrimaryKey(cboToLocationID.SelectedValue))
            {
                args.MessageText = "Invalid Location.";
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
            ComboBox.PopulateWithOneServiceUnit(cboToServiceUnitID, itemTransaction.ToServiceUnitID ?? string.Empty);
            ComboBox.PopulateWithOneSupplier(cboSupplierID, itemTransaction.BusinessPartnerID ?? string.Empty);
            ComboBox.PopulateWithOneLocation(cboToLocationID, itemTransaction.ToLocationID ?? string.Empty);
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboToServiceUnitID.SelectedValue, TransactionCode.ConsignmentReceive);
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
            entity.TransactionCode = TransactionCode.ConsignmentReceive;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.ToServiceUnitID = cboToServiceUnitID.SelectedValue;
            entity.ToLocationID = cboToLocationID.SelectedValue;
            entity.BusinessPartnerID = cboSupplierID.SelectedValue;
            entity.SRItemType = cboSRItemType.SelectedValue;
            entity.Notes = txtNotes.Text;
            entity.ChargesAmount = 0;
            entity.TaxAmount = 0;
            entity.TaxPercentage = 0;
            entity.IsTaxable = 0;
            entity.CurrencyID = AppSession.Parameter.CurrencyRupiahID;
            entity.CurrencyRate = 1;
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
                que.Where(que.TransactionNo > txtTransactionNo.Text && que.TransactionCode == TransactionCode.ConsignmentReceive);
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < txtTransactionNo.Text && que.TransactionCode == TransactionCode.ConsignmentReceive);
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
                    var obj = Session["ConsignmentReceiveItems" + Request.UserHostName];
                    if (obj != null)
                        return ((ItemTransactionItemCollection)(obj));
                }

                var coll = new ItemTransactionItemCollection();

                var query = new ItemTransactionItemQuery("a");
                var iq = new ItemQuery("b");

                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.ItemID.Ascending);

                query.Select(
                    query,
                    iq.ItemName.As("refToItem_ItemName")
                    );

                coll.Load(query);
                Session["ConsignmentReceiveItems" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["ConsignmentReceiveItems" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            var isVisible = (newVal != AppEnum.DataMode.Read);
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
            
            //var coll = ItemTransactionItems;
            //return coll.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
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
            cboSupplierID.Enabled = cboSRItemType.Enabled;
            cboToLocationID.Enabled = cboSRItemType.Enabled;
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
            var userControl = (ConsignmentReceiveItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.SequenceNo = userControl.SequenceNo;
                entity.Quantity = userControl.Quantity;
                entity.ItemName = userControl.ItemName;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.Price = 0;
                entity.PriceInCurrency = 0;
                entity.ConversionFactor = 1; //Transaksi Consignment Receive selalu dalam Base Unit
                entity.Discount = 0;
                entity.DiscountInCurrency = 0;
            }
        }

        #endregion
    }
}
using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class DistributionConfirmDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;
        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;
            if (cboFromServiceUnitID.SelectedValue == string.Empty) return;
            ServiceUnit serv = new ServiceUnit();
            if (serv.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue))
            {
                _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, BusinessObject.Reference.TransactionCode.DistributionConfirm, serv.DepartmentID);
                txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
            }
        }

        private void PopulateFromSelectedPurchaseOrder()
        {
            object obj = Session["DistributionConfirmItemSelected" + Request.UserHostName];
            if (obj == null) return;

            //delete previouse item
            if (ItemTransactionItems.Count > 0)
                ItemTransactionItems.MarkAllAsDeleted();

            DataTable dtbSelectedItem = (DataTable) obj;
            if (dtbSelectedItem.Rows.Count > 0)
            {
                txtReferenceNo.Text = dtbSelectedItem.Rows[0]["TransactionNo"].ToString();

                var itRef = new ItemTransaction();
                if (itRef.LoadByPrimaryKey(txtReferenceNo.Text))
                {
                    cboFromServiceUnitID.SelectedValue = itRef.ToServiceUnitID;
                    cboFromLocationID.SelectedValue = itRef.ToLocationID;
                }
            }

            string seqNo;

            int i = 0;
            foreach (DataRow row in dtbSelectedItem.Rows)
            {
                if (Convert.ToDecimal(row["QtyInput"]) <= 0) continue;
                i++;
                seqNo = string.Format("{0:000}", i);
                ItemTransactionItem entity = ItemTransactionItems.AddNew();

                entity.ItemID = row["ItemID"].ToString();
                entity.SequenceNo = seqNo;
                entity.ReferenceNo = row["TransactionNo"].ToString();
                entity.ReferenceSequenceNo = row["SequenceNo"].ToString();
                entity.ItemName = row["ItemName"].ToString();
                entity.Quantity = Convert.ToDecimal(row["QtyInput"]);
                entity.SRItemUnit = row["SRItemUnit"].ToString();
                entity.ConversionFactor = Convert.ToDecimal(row["ConversionFactor"]);
                entity.CostPrice = Convert.ToDecimal(row["CostPrice"]);
                entity.Price = Convert.ToDecimal(row["price"]);
                entity.PriceInCurrency = Convert.ToDecimal(row["PriceInCurrency"]);
            }
            grdItemTransactionItem.DataSource = ItemTransactionItems;
            grdItemTransactionItem.DataBind();

            cboSRItemType.Enabled = !(ItemTransactionItems.Count > 0);
            cboFromServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboFromLocationID.Enabled = cboSRItemType.Enabled;

            //Remove session
            Session.Remove("DistributionConfirmItemSelected" + Request.UserHostName);
        }

        protected void btnResetItem_Click(object sender, EventArgs e)
        {
            if (txtReferenceNo.Text != string.Empty)
            {
                txtReferenceNo.Text = string.Empty;
                if (ItemTransactionItems.Count > 0)
                    ItemTransactionItems.MarkAllAsDeleted();
                grdItemTransactionItem.DataSource = ItemTransactionItems;
                grdItemTransactionItem.DataBind();
            }

            cboSRItemType.Enabled = !(ItemTransactionItems.Count > 0);
            cboFromServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboFromLocationID.Enabled = cboSRItemType.Enabled;
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "DistributionConfirmSearch.aspx";
            UrlPageList = "DistributionConfirmList.aspx";

            ProgramID = AppConstant.Program.DistributionConfirm;
            this.WindowSearch.Height = 400;

            //StandardReference Initialize
            if (!IsPostBack)
            {
            }

            //Add Event for Request Order Selection
            AjaxManager.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(AjaxManager_AjaxRequest);
        }

        private void AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            PopulateFromSelectedPurchaseOrder();
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

            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromLocationID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboSRItemType);
            ajax.AddAjaxSetting(cboFromServiceUnitID, txtTransactionNo);

            //PurchaseReturn Request Selection
            ajax.AddAjaxSetting(AjaxManager, txtReferenceNo);
            ajax.AddAjaxSetting(AjaxManager, grdItemTransactionItem);
            ajax.AddAjaxSetting(AjaxManager, cboSRItemType);
            ajax.AddAjaxSetting(AjaxManager, cboFromServiceUnitID);
            ajax.AddAjaxSetting(AjaxManager, cboFromLocationID);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
            string serviceUnitID = cboFromServiceUnitID.SelectedValue;
            ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.DistributionConfirm, true);
            cboFromServiceUnitID.SelectedValue = serviceUnitID;

            cboSRItemType.Enabled = ItemTransactionItems.Count == 0;
            cboFromServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboFromLocationID.Enabled = cboSRItemType.Enabled;
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
        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("TransactionNo", txtTransactionNo.Text);
        }
        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var loc = new Location();
            if (loc.LoadByPrimaryKey(cboFromLocationID.SelectedValue) && loc.IsHoldForTransaction == true)
            {
                args.MessageText = "Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.";
                args.IsCancel = true;
                return;
            }

            var entity = new ItemTransaction();
            using (var trans = new esTransactionScope())
            {
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
                entity.IsApproved = true;
                entity.ApprovedDate = (new DateTime()).NowAtSqlServer();// DateTime.Now.Date;
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.Save();

                var reference = new ItemTransaction();
                var referenceItems = new ItemTransactionItemCollection();
                var chargesBalances = new ItemBalanceCollection();
                var chargesMovements = new ItemMovementCollection();
                var chargesDetailBalances = new ItemBalanceDetailCollection();
                var itemBalanceDetailEds = new ItemBalanceDetailEdCollection();

                ItemTransactionItems.Save();

                ItemBalance.PrepareItemBalancesForDistribution(ItemTransactionItems, entity, AppSession.UserLogin.UserID,
                    ref reference, ref referenceItems, ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, ref itemBalanceDetailEds, AppSession.Parameter.IsEnabledStockWithEdControl);

                ItemTransactionItems.Save();

                if (reference != null)
                    reference.Save();
                if (referenceItems != null)
                    referenceItems.Save();
                if (chargesBalances != null)
                    chargesBalances.Save();
                if (chargesDetailBalances != null)
                    chargesDetailBalances.Save();
                if (itemBalanceDetailEds != null)
                    itemBalanceDetailEds.Save();
                if (chargesMovements != null)
                    chargesMovements.Save();

                trans.Complete();
            }

            Finance.Voucher.VoucherEntry.VoucherEntryDetail.JournalDistribution(0, entity.TransactionNo);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var im = new ItemMovementCollection();
            im.Query.Where(im.Query.TransactionNo == txtTransactionNo.Text);
            im.LoadAll();
            if (im.Count > 0)
            {
                args.MessageText = "Unapproved is not allowed. The data is already in stock.";
                args.IsCancel = true;
                return;
            }

            var entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                entity.IsApproved = false;
                entity.ApprovedDate = (new DateTime()).NowAtSqlServer();
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.Save();
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

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ItemTransaction());
            ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.DistributionConfirm, true);
            cboFromServiceUnitID.SelectedValue = string.Empty;
            cboFromServiceUnitID.Text = string.Empty;
            cboFromLocationID.Items.Clear();
            cboFromLocationID.Text = string.Empty;
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();// DateTime.Now;
            PopulateNewTransactionNo();
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
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboFromLocationID.SelectedValue))
            {
                args.MessageText = "Location required.";
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

            var refq = new ItemTransactionQuery();
            refq.Where(refq.ReferenceNo == txtReferenceNo.Text, refq.IsVoid == false);
            refq.es.Top = 1;
            if (refq.LoadDataTable().Rows.Count > 0)
            {
                var r = new ItemTransaction();
                r.Load(refq);
                args.MessageText = "Reference No. : " + txtReferenceNo.Text + " already proceed with Transaction No. : " + r.TransactionNo;
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
            if (string.IsNullOrEmpty(cboFromLocationID.SelectedValue))
            {
                args.MessageText = "Location required.";
                args.IsCancel = true;
                return;
            }

            ItemTransaction entity = new ItemTransaction();
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
            btnGetPickList.Enabled = newVal != AppEnum.DataMode.Read;
            btnResetItem.Enabled = newVal != AppEnum.DataMode.Read;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ItemTransaction entity = new ItemTransaction();
            if (parameters.Length > 0)
            {
                String transactionNo = (String) parameters[0];

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
            var itemTransaction = (ItemTransaction) entity;
            txtTransactionNo.Text = itemTransaction.TransactionNo;
            txtTransactionDate.SelectedDate = itemTransaction.TransactionDate;
            txtReferenceNo.Text = itemTransaction.ReferenceNo;
            ComboBox.PopulateWithOneServiceUnit(cboFromServiceUnitID, itemTransaction.FromServiceUnitID ?? string.Empty);
            if (!string.IsNullOrEmpty(itemTransaction.FromServiceUnitID))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboFromLocationID, itemTransaction.FromServiceUnitID);
                if (!string.IsNullOrEmpty(itemTransaction.FromLocationID))
                    cboFromLocationID.SelectedValue = itemTransaction.FromLocationID;
                else if (cboFromLocationID.Items.Count > 1)
                    cboFromLocationID.SelectedIndex = 1;
                else 
                {
                    cboFromLocationID.SelectedValue = string.Empty;
                    cboFromLocationID.Text = string.Empty;
                }
            }
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, BusinessObject.Reference.TransactionCode.DistributionConfirm);
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
            entity.TransactionCode = BusinessObject.Reference.TransactionCode.DistributionConfirm;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.ReferenceNo = txtReferenceNo.Text;

            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            entity.FromLocationID = cboFromLocationID.SelectedValue;

            entity.SRItemType = cboSRItemType.SelectedValue;
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

                ////autonumber has been saved on SetEntity
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
                que.Where(que.TransactionNo > txtTransactionNo.Text &&
                          que.TransactionCode == BusinessObject.Reference.TransactionCode.DistributionConfirm);
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < txtTransactionNo.Text &&
                          que.TransactionCode == BusinessObject.Reference.TransactionCode.DistributionConfirm);
                que.OrderBy(que.TransactionNo.Descending);
            }
            ItemTransaction entity = new ItemTransaction();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }



        #endregion

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

        #region Record Detail Method Function

        private ItemTransactionItemCollection ItemTransactionItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["DistributionConfirmItems" + Request.UserHostName];
                    if (obj != null)
                        return ((ItemTransactionItemCollection) (obj));
                }

                ItemTransactionItemCollection coll = new ItemTransactionItemCollection();
                ItemTransactionItemQuery query = new ItemTransactionItemQuery("a");

                ItemQuery iq = new ItemQuery("b");

                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy
                    (
                    query.ItemID.Ascending
                    );

                query.Select
                    (
                    query.TransactionNo,
                    query.ItemID,
                    query.SRItemUnit,
                    query.Quantity,
                    query.SequenceNo,
                    query.ReferenceNo,
                    query.ReferenceSequenceNo,
                    query.QuantityFinishInBaseUnit,
                    query.PageNo,
                    query.ConversionFactor,
                    query.CostPrice,
                    query.Price,
                    query.PriceInCurrency,
                    query.Discount1Percentage,
                    query.Discount2Percentage,
                    query.BatchNumber,
                    query.ExpiredDate,
                    query.IsPackage,
                    query.IsBonusItem,
                    query.IsClosed,
                    query.LastUpdateByUserID,
                    query.LastUpdateDateTime,
                    iq.ItemName.As("refToItem_ItemName")
                    );

                coll.Load(query);
                Session["DistributionConfirmItems" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["DistributionConfirmItems" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemTransactionItem.Columns[0].Visible = false;// isVisible;
            grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 1].Visible = false;// isVisible;

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
            if (editedItem == null) return;

            String sequenceNo =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ItemTransactionItemMetadata.ColumnNames.SequenceNo]);
            ItemTransactionItem entity = FindItemTransactionItem(sequenceNo);
            if (entity != null)
                SetEntityValue(entity, e);

        }

        private ItemTransactionItem FindItemTransactionItem(String sequenceNo)
        {
            return ItemTransactionItems.Where(x => x.SequenceNo == sequenceNo &&
                (x.TransactionNo ?? string.Empty) == (x.es.IsAdded ? string.Empty : txtTransactionNo.Text)).First();
            
            //ItemTransactionItemCollection coll = ItemTransactionItems;
            //ItemTransactionItem retEntity = null;
            //foreach (ItemTransactionItem rec in coll)
            //{
            //    if (rec.SequenceNo.Equals(sequenceNo))
            //    {
            //        retEntity = rec;
            //        break;
            //    }
            //}
            //return retEntity;
        }

        protected void grdItemTransactionItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String sequenceNo =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemTransactionItemMetadata.ColumnNames.SequenceNo
                        ]);
            ItemTransactionItem entity = FindItemTransactionItem(sequenceNo);
            if (entity != null)
            {
                if (txtReferenceNo.Text.Length > 0)
                {
                    if (Convert.ToBoolean(entity.IsBonusItem))
                    {
                        entity.MarkAsDeleted();
                    }
                }
                else
                {
                    entity.MarkAsDeleted();
                }
            }

            cboSRItemType.Enabled = !(ItemTransactionItems.Count > 0);
            cboFromServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboFromLocationID.Enabled = cboSRItemType.Enabled;
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
            DistributionConfirmItemDetail userControl =
                (DistributionConfirmItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.Quantity = userControl.Quantity;
            }
        }

        #endregion
        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateNewTransactionNo();
            ComboBox.PopulateWithServiceUnitForLocation(cboFromLocationID, e.Value);
            if (cboFromLocationID.Items.Count > 1)
                cboFromLocationID.SelectedIndex = 1;
            else
            {
                cboFromLocationID.SelectedValue = string.Empty;
                cboFromLocationID.Text = string.Empty;
            }
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, BusinessObject.Reference.TransactionCode.DistributionConfirm);
        }
        protected void cboBusinessPartnerID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.SupplierItemsRequested((RadComboBox)sender, e.Text);
        }
    }
}
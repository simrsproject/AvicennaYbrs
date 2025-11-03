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
    public partial class ConsignmentReturnDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;
        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;
            if (cboFromServiceUnitID.SelectedValue == string.Empty) return;
            ServiceUnit serv = new ServiceUnit();
            if (serv.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue))
            {
                _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, BusinessObject.Reference.TransactionCode.ConsignmentReturn, serv.DepartmentID);
                txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
            }
        }

        private void PopulateFromSelectedConsignment()
        {
            object obj = Session["ConsignmentReturnItemSelected" + Request.UserHostName];
            if (obj == null) return;

            //delete previouse item
            if (ItemTransactionItems.Count > 0)
                ItemTransactionItems.MarkAllAsDeleted();

            DataTable dtbSelectedItem = (DataTable)obj;
            if (dtbSelectedItem.Rows.Count > 0)
            {
                txtReferenceNo.Text = dtbSelectedItem.Rows[0]["TransactionNo"].ToString();
            }

            string seqNo;
            string parCostType = AppParameter.GetParameterValue(AppParameter.ParameterItem.ItemProductCostPriceType);

            int i = 0;
            foreach (DataRow row in dtbSelectedItem.Rows)
            {
                if (Convert.ToDecimal(row["QtyInput"]) < 1)
                    continue;

                i++;
                seqNo = string.Format("{0:000}", i);
                ItemTransactionItem entity = ItemTransactionItems.AddNew();

                entity.ItemID = row["ItemID"].ToString();
                entity.SequenceNo = seqNo;
                entity.ReferenceNo = row["TransactionNo"].ToString();
                entity.ReferenceSequenceNo = row["SequenceNo"].ToString();
                entity.Description = row["ItemName"].ToString();
                entity.Quantity = Convert.ToDecimal(row["QtyInput"]);
                entity.SRItemUnit = row["SRItemUnit"].ToString();
                entity.ConversionFactor = 1;
                entity.Price = Convert.ToDecimal(row["Price"]);
                entity.Discount1Percentage = Convert.ToDecimal(row["Discount1Percentage"]);
                entity.Discount2Percentage = Convert.ToDecimal(row["Discount2Percentage"]);
                entity.Discount = Convert.ToDecimal(row["Discount"]);
                entity.IsDiscountInPercent = Convert.ToBoolean(row["IsDiscountInPercent"]);
                entity.PriceInCurrency = Convert.ToDecimal(row["PriceInCurrency"]);
                entity.DiscountInCurrency = Convert.ToDecimal(row["DiscountInCurrency"]);


                var ent = new ItemTransaction();
                ent.LoadByPrimaryKey(row["TransactionNo"].ToString());

                if (cboSRItemType.SelectedValue == BusinessObject.Reference.ItemType.Medical)
                {
                    var med = new ItemProductMedic();
                    if (med.LoadByPrimaryKey(entity.ItemID))
                    {
                        if (parCostType == "AVG")
                            entity.CostPrice = med.CostPrice;
                        else
                        {
                            entity.CostPrice = (((decimal)row["Price"] - (decimal)row["Discount"]) * (1 + (ent.TaxPercentage / 100)));
                        }
                    }
                    else
                        entity.CostPrice = entity.Price;
                }
                else if (cboSRItemType.SelectedValue == BusinessObject.Reference.ItemType.NonMedical)
                {
                    var nonMed = new ItemProductNonMedic();
                    if (nonMed.LoadByPrimaryKey(entity.ItemID))
                    {
                        if (parCostType == "AVG")
                            entity.CostPrice = nonMed.CostPrice;
                        else
                        {
                            entity.CostPrice = (((decimal)row["Price"] - (decimal)row["Discount"]) * (1 + (ent.TaxPercentage / 100)));
                        }
                    }
                    else
                        entity.CostPrice = entity.Price;
                }
            }
            grdItemTransactionItem.DataSource = ItemTransactionItems;
            grdItemTransactionItem.DataBind();

            cboSRItemType.Enabled = !(ItemTransactionItems.Count > 0);

            //Remove session
            Session.Remove("ConsignmentReturnItemSelected" + Request.UserHostName);
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
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ConsignmentReturnSearch.aspx";
            UrlPageList = "ConsignmentReturnList.aspx";

            ProgramID = AppConstant.Program.ConsignmentReturn;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                var supp = new SupplierCollection();
                supp.Query.Where(supp.Query.IsActive == true);
                supp.LoadAll();
                cboBusinessPartnerID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var entity in supp)
                {
                    cboBusinessPartnerID.Items.Add(new RadComboBoxItem(entity.SupplierName, entity.SupplierID));
                }

                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
                ComboBox.SelectedValue(cboSRItemType, BusinessObject.Reference.ItemType.Medical);
            }

            //Add Event for Request Order Selection
            AjaxManager.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(AjaxManager_AjaxRequest);
        }

        private void AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            PopulateFromSelectedConsignment();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItemTransactionItem, grdItemTransactionItem);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboSRItemType);

            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromServiceUnitID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, txtTransactionNo);

            ajax.AddAjaxSetting(cboBusinessPartnerID, cboBusinessPartnerID);
            ajax.AddAjaxSetting(cboBusinessPartnerID, cboFromLocationID);

            //PurchaseReturn Request Selection
            ajax.AddAjaxSetting(AjaxManager, txtReferenceNo);
            ajax.AddAjaxSetting(AjaxManager, grdItemTransactionItem);
            ajax.AddAjaxSetting(AjaxManager, cboSRItemType);

        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
            //string serviceUnitID = cboFromServiceUnitID.SelectedValue;
            ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.ConsignmentReturn, true);
            //cboFromServiceUnitID.SelectedValue = serviceUnitID;

            cboSRItemType.Enabled = ItemTransactionItems.Count == 0;
            cboFromServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboFromLocationID.Enabled = cboSRItemType.Enabled;
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

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
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
            cboBusinessPartnerID.Text = string.Empty;
            ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.ConsignmentReturn, true);
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
            //Check Supplier entry
            if (!IsSupplierExist(args)) return;

            if (string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue) || string.IsNullOrEmpty(cboFromServiceUnitID.Text))
            {
                args.MessageText = "Service Unit required.";
                args.IsCancel = true;
                return;
            }
            var unit = new ServiceUnit();
            if (!unit.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue))
            {
                args.MessageText = "Invalid Service Unit.";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboFromLocationID.SelectedValue) || string.IsNullOrEmpty(cboFromLocationID.Text))
            {
                args.MessageText = "Location required.";
                args.IsCancel = true;
                return;
            }
            var loc = new Location();
            if (!loc.LoadByPrimaryKey(cboFromLocationID.SelectedValue))
            {
                args.MessageText = "Invalid Location.";
                args.IsCancel = true;
                return;
            }

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
            if (ItemTransactionItems.Where(x => x.TransactionNo == txtTransactionNo.Text).Count() == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }
            SaveEntity(entity);
        }

        private bool IsSupplierExist(ValidateArgs args)
        {
            SupplierQuery query = new SupplierQuery();
            query.es.Top = 1;
            query.Where(query.SupplierName == cboBusinessPartnerID.Text);
            Supplier item = new Supplier();
            if (!item.Load(query))
            {
                args.IsCancel = true;
                args.MessageText = "Selected supplier not valid, please select exist supplier";
                return false;
            }
            return true;
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            //Check Supplier entry
            if (!IsSupplierExist(args)) return;

            if (string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue) || string.IsNullOrEmpty(cboFromServiceUnitID.Text))
            {
                args.MessageText = "Service Unit required.";
                args.IsCancel = true;
                return;
            }
            var unit = new ServiceUnit();
            if (!unit.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue))
            {
                args.MessageText = "Invalid Service Unit.";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboFromLocationID.SelectedValue) || string.IsNullOrEmpty(cboFromLocationID.Text))
            {
                args.MessageText = "Location required.";
                args.IsCancel = true;
                return;
            }
            var loc = new Location();
            if (!loc.LoadByPrimaryKey(cboFromLocationID.SelectedValue))
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
            txtReferenceNo.Text = itemTransaction.ReferenceNo;
            ComboBox.PopulateWithOneServiceUnit(cboFromServiceUnitID, itemTransaction.FromServiceUnitID ?? string.Empty);
            cboSRItemType.SelectedValue = itemTransaction.SRItemType;
            chkIsVoid.Checked = itemTransaction.IsVoid ?? false;
            chkIsApproved.Checked = itemTransaction.IsApproved ?? false;
            txtNotes.Text = itemTransaction.Notes;
            cboBusinessPartnerID.SelectedValue = itemTransaction.BusinessPartnerID;
            if (!string.IsNullOrEmpty(itemTransaction.BusinessPartnerID))
            {
                ComboBox.PopulateWithSupplierForLocation(cboFromLocationID, cboBusinessPartnerID.SelectedValue);
                if (!string.IsNullOrEmpty(itemTransaction.FromLocationID))
                    cboFromLocationID.SelectedValue = itemTransaction.FromLocationID;
                else
                    cboFromLocationID.SelectedIndex = 1;
            }

            //Display Data Detail
            PopulateGridDetail();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ItemTransaction entity)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionCode = BusinessObject.Reference.TransactionCode.ConsignmentReturn;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.ReferenceNo = txtReferenceNo.Text;
            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            entity.FromLocationID = cboFromLocationID.SelectedValue;
            entity.BusinessPartnerID = cboBusinessPartnerID.SelectedValue;

            entity.SRItemType = cboSRItemType.SelectedValue;
            entity.Notes = txtNotes.Text;

            var refs = new ItemTransaction();
            refs.LoadByPrimaryKey(txtReferenceNo.Text);
            entity.IsNonMasterOrder = refs.IsNonMasterOrder;
            entity.SRPurchaseOrderType = refs.SRPurchaseOrderType;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
            }

            decimal? totalcharge = 0;
            decimal? totaltax = 0;

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
                totalcharge += ((item.Price * item.Quantity) - (item.Discount * item.Quantity));
            }

            totaltax = refs.TaxPercentage == 0 ? 0 : ((totalcharge.Value * refs.TaxPercentage) / Convert.ToDecimal(100));

            entity.ChargesAmount = (-1) * totalcharge;
            entity.TaxAmount = (-1) * totaltax;
        }

        private void SaveEntity(ItemTransaction entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ItemTransactionItems.Save();

                //autonumber has been saved on SetEntity
                //if (DataModeCurrent == DataMode.New)
                //    _autoNumber.Save();

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
                que.Where(que.TransactionNo > txtTransactionNo.Text &&
                          que.TransactionCode == BusinessObject.Reference.TransactionCode.ConsignmentReturn);
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < txtTransactionNo.Text &&
                          que.TransactionCode == BusinessObject.Reference.TransactionCode.ConsignmentReturn);
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
                    object obj = Session["ConsignmentReturnItems" + Request.UserHostName];
                    if (obj != null)
                        return ((ItemTransactionItemCollection)(obj));
                }

                var coll = new ItemTransactionItemCollection();
                var query = new ItemTransactionItemQuery("a");

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.ItemID.Ascending);

                coll.Load(query);
                Session["ConsignmentReturnItems" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["ConsignmentReturnItems" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemTransactionItem.Columns[0].Visible = isVisible;
            grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 1].Visible = isVisible;

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
                entity.MarkAsDeleted();

            cboSRItemType.Enabled = !(ItemTransactionItems.Count > 0);
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
            var userControl = (ConsignmentReturnItemDetail) e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.Quantity = userControl.Quantity;
            }
        }

        #endregion

        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateNewTransactionNo();
        }

        protected void cboBusinessPartnerID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new SupplierQuery("a");
            query.Where(
                query.Or(query.SupplierID == e.Text, query.SupplierName.Like(searchText))
                );
            query.Select(query.SupplierID, query.SupplierName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboBusinessPartnerID.DataSource = dtb;
            cboBusinessPartnerID.DataBind();
        }

        protected void cboBusinessPartnerID_OnItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.SupplierItemDataBound(e);
        }

        protected void cboSupplierID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComboBox.PopulateWithSupplierForLocation(cboFromLocationID, cboBusinessPartnerID.SelectedValue);
            cboFromLocationID.SelectedIndex = cboFromLocationID.Items.Count == 2 ? 1 : 0;
        }

    }
}

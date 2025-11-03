using System;
using System.Linq;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class InventoryIssueRequestDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New)
                return;
            if (cboFromServiceUnitID.SelectedValue == string.Empty)
                return;

            var serv = new ServiceUnit();
            if (serv.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue))
            {
                _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, TransactionCode.InventoryIssueRequestOut, serv.DepartmentID);
                txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "InventoryIssueRequestSearch.aspx?type=" + FormType;
            UrlPageList = string.IsNullOrEmpty(Request.QueryString["rod"])
                              ? "InventoryIssueRequestList.aspx?spc=" + Request.QueryString["spc"] + "&type=" + FormType
                              : "../ItemRequestMaintenance/ItemRequestMaintenanceList.aspx?su=" + Request.QueryString["su"] +
                                "&it=" + Request.QueryString["it"];

            ProgramID = FormType == "PurchaseCat-002" ? AppConstant.Program.InventoryIssueRequest2 : AppConstant.Program.InventoryIssueRequest;

            this.WindowSearch.Height = 400;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                ViewState["ReferenceNo" + Request.UserHostName] = string.Empty;

                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);

                ComboBox.SelectedValue(cboSRItemType, ItemType.Medical);
            }

            AjaxManager.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(AjaxManager_AjaxRequest);
        }

        private void AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            PopulateFromPickList();
        }

        private void PopulateFromPickList()
        {
            Session.Remove("InventoryIssueRequest:Selection");

            object obj = Session["InventoryIssueRequest:ItemSelected"];
            if (obj == null) return;

            //delete previouse item
            if (ItemTransactionItems.Count > 0)
                ItemTransactionItems.MarkAllAsDeleted();

            DataTable dtbSelectedItem = (DataTable)obj;
            if (dtbSelectedItem.Rows.Count == 0) return;
            int i = 0;
            foreach (DataRow row in dtbSelectedItem.Rows)
            {
                if (Convert.ToDecimal(row["QtyInput"]) < 1) continue;
                i++;

                ItemTransactionItem entity = ItemTransactionItems.AddNew();
                entity.ItemID = row["ItemID"].ToString();
                entity.SequenceNo = string.Format("{0:000}", i);
                entity.Quantity = Convert.ToDecimal(row["QtyInput"]);
                entity.ItemName = row["ItemName"].ToString();
                entity.SRItemUnit = row["SRItemUnit"].ToString();
                entity.ConversionFactor = 1;
                if (cboSRItemType.SelectedValue == ItemType.Medical)
                {
                    ItemProductMedic med = new ItemProductMedic();
                    med.LoadByPrimaryKey(entity.ItemID);
                    entity.CostPrice = med.CostPrice;
                    entity.Price = med.PriceInBasedUnitWVat;
                    entity.IsControlExpired = med.IsControlExpired ?? false;
                }
                else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                {
                    ItemProductNonMedic nonMed = new ItemProductNonMedic();
                    nonMed.LoadByPrimaryKey(entity.ItemID);
                    entity.CostPrice = nonMed.CostPrice;
                    entity.Price = nonMed.PriceInBasedUnitWVat;
                    entity.IsControlExpired = nonMed.IsControlExpired ?? false;
                }
                else
                {
                    var kitchen = new ItemKitchen();
                    kitchen.LoadByPrimaryKey(entity.ItemID);
                    entity.CostPrice = kitchen.CostPrice;
                    entity.Price = kitchen.PriceInBasedUnitWVat;
                    entity.IsControlExpired = kitchen.IsControlExpired ?? false;
                }
            }
            grdItemTransactionItem.DataSource = ItemTransactionItems;
            grdItemTransactionItem.DataBind();

            //Remove session
            Session.Remove("InventoryIssueRequest:ItemSelected");
            cboSRItemType.Enabled = ItemTransactionItems.Count == 0;
            cboFromServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboToServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboToLocationID.Enabled = cboSRItemType.Enabled;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboFromServiceUnitID, txtTransactionNo);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromServiceUnitID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboSRItemType);

            ajax.AddAjaxSetting(cboSRItemType, cboToServiceUnitID);
            ajax.AddAjaxSetting(cboSRItemType, cboToLocationID);

            ajax.AddAjaxSetting(cboToServiceUnitID, cboToServiceUnitID);
            ajax.AddAjaxSetting(cboToServiceUnitID, cboToLocationID);
            
            ajax.AddAjaxSetting(grdItemTransactionItem, grdItemTransactionItem);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboFromServiceUnitID);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboToServiceUnitID);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboToLocationID);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboSRItemType);

            ajax.AddAjaxSetting(AjaxManager, grdItemTransactionItem);
            ajax.AddAjaxSetting(AjaxManager, cboFromServiceUnitID);
            ajax.AddAjaxSetting(AjaxManager, cboToServiceUnitID);
            ajax.AddAjaxSetting(AjaxManager, cboToLocationID);
            ajax.AddAjaxSetting(AjaxManager, cboSRItemType);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
            string fromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            
            ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, TransactionCode.InventoryIssueRequestOut, true);
            cboFromServiceUnitID.SelectedValue = fromServiceUnitID;

            string toServiceUnitID = cboToServiceUnitID.SelectedValue;
            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.InventoryIssueOutForOtherUnit, false, string.Empty, cboSRItemType.SelectedValue);

            cboToServiceUnitID.SelectedValue = toServiceUnitID;

            cboSRItemType.Enabled = ItemTransactionItems.Count == 0;
            cboFromServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboToServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboToLocationID.Enabled = cboSRItemType.Enabled;
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
            if (ItemTransactionItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
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

                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            if (!IsProceed(args)) return;

            using (var trans = new esTransactionScope())
            {
                var entity = new ItemTransaction();
                entity.LoadByPrimaryKey(txtTransactionNo.Text);

                entity.IsApproved = false;
                entity.ApprovedDate = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                entity.Save();

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

        private bool IsProceed(ValidateArgs args)
        {
            var itQ = new ItemTransactionQuery();
            itQ.Where(itQ.ReferenceNo == txtTransactionNo.Text, itQ.IsVoid == false);
            DataTable dtb = itQ.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                args.IsCancel = true;
                args.MessageText = "This transaction can't be canceled, this data has been proceed to another process";
                return false;
            }
            return true;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ItemTransaction());
            ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, TransactionCode.InventoryIssueRequestOut, true);
            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.InventoryIssueOutForOtherUnit, false);
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();// DateTime.Now;
            PopulateNewTransactionNo();
            cboFromServiceUnitID.Text = string.Empty;
            cboToServiceUnitID.Text = string.Empty;
            cboToLocationID.Items.Clear();
            cboToLocationID.Text = string.Empty;
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
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, TransactionCode.InventoryIssueRequestOut);

            ComboBox.PopulateWithOneServiceUnit(cboToServiceUnitID, itemTransaction.ToServiceUnitID ?? string.Empty);
            if (!string.IsNullOrEmpty(itemTransaction.ToServiceUnitID))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboToLocationID, itemTransaction.ToServiceUnitID);
                if (!string.IsNullOrEmpty(itemTransaction.ToLocationID))
                    cboToLocationID.SelectedValue = itemTransaction.ToLocationID;
                else
                    cboToLocationID.SelectedIndex = 1;
            }
            
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
            entity.TransactionCode = TransactionCode.InventoryIssueRequestOut;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.ReferenceNo = ViewState["ReferenceNo" + Request.UserHostName] == null ? string.Empty : ViewState["ReferenceNo" + Request.UserHostName].ToString();
            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            entity.ToServiceUnitID = cboToServiceUnitID.SelectedValue;
            entity.ToLocationID = cboToLocationID.SelectedValue;
            entity.SRItemType = cboSRItemType.SelectedValue;
            entity.Notes = txtNotes.Text;
            if (!string.IsNullOrEmpty(FormType))
                entity.SRPurchaseCategorization = FormType;

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
                        que.TransactionCode == TransactionCode.InventoryIssueRequestOut
                    );
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.TransactionNo < txtTransactionNo.Text &&
                        que.TransactionCode == TransactionCode.InventoryIssueRequestOut
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }
            if (!string.IsNullOrEmpty(FormType))
                que.Where(que.SRPurchaseCategorization == FormType);

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
                    object obj = Session["InventoryIssueRequest:collItemTransactionItem" + Request.UserHostName];
                    if (obj != null)
                        return ((ItemTransactionItemCollection)(obj));
                }

                ItemTransactionItemCollection coll = new ItemTransactionItemCollection();
                ItemTransactionItemQuery query = new ItemTransactionItemQuery("a");

                ItemQuery iq = new ItemQuery("b");

                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                
                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.ItemID.Ascending);

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName")
                    );

                if (cboSRItemType.SelectedValue == ItemType.Medical)
                {
                    var ipq = new ItemProductMedicQuery("f");
                    query.LeftJoin(ipq).On(query.ItemID == ipq.ItemID);
                    query.Select(@"<ISNULL(f.IsControlExpired, 0) AS 'refToItemProduct_IsControlExpired'>");
                }
                else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                {
                    var ipq = new ItemProductNonMedicQuery("f");
                    query.LeftJoin(ipq).On(query.ItemID == ipq.ItemID);
                    query.Select(@"<ISNULL(f.IsControlExpired, 0) AS 'refToItemProduct_IsControlExpired'>");
                }
                else
                {
                    var ipq = new ItemKitchenQuery("f");
                    query.LeftJoin(ipq).On(query.ItemID == ipq.ItemID);
                    query.Select(@"<ISNULL(f.IsControlExpired, 0) AS 'refToItemProduct_IsControlExpired'>");
                    
                }


                coll.Load(query);
                Session["InventoryIssueRequest:collItemTransactionItem" + Request.UserHostName] = coll;

                return coll;
            }
            set { Session["InventoryIssueRequest:collItemTransactionItem" + Request.UserHostName] = value; }
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

            String sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]
                [ItemTransactionItemMetadata.ColumnNames.SequenceNo]);

            ItemTransactionItem entity = FindItemTransactionItem(sequenceNo);
            if (entity != null)
                entity.MarkAsDeleted();

            cboFromServiceUnitID.Enabled = !(ItemTransactionItems.Count > 0);
            cboToServiceUnitID.Enabled = !(ItemTransactionItems.Count > 0);
            cboToLocationID.Enabled = !(ItemTransactionItems.Count > 0);
            cboSRItemType.Enabled = !(ItemTransactionItems.Count > 0);
        }

        protected void grdItemTransactionItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            #region Generate SequenceNo
            // jika insert maka bikin sequenceno disini saja
            // kasus RSUI sering error duplikat sequence pada saat insert tapi timeout ajax client
            // makanya generate sequence dipindah kemari
            InventoryIssueRequestItemDetail userControl = (InventoryIssueRequestItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (ItemTransactionItems.Count == 0)
                userControl.SequenceNo = "001";
            else
            {
                int seqNo = 0;
                foreach (ItemTransactionItem item in ItemTransactionItems)
                {
                    if (int.Parse(item.SequenceNo) > seqNo)
                        seqNo = int.Parse(item.SequenceNo);
                }
                userControl.SequenceNo = string.Format("{0:000}", seqNo + 1);
            }
            // end of bikin sequenceno
            #endregion

            ItemTransactionItem entity = ItemTransactionItems.AddNew();
            SetEntityValue(entity, e);

            //grid not close first
            e.Canceled = true;
            grdItemTransactionItem.Rebind();
        }

        private void SetEntityValue(ItemTransactionItem entity, GridCommandEventArgs e)
        {
            var userControl = (InventoryIssueRequestItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.SequenceNo = userControl.SequenceNo;
                entity.Quantity = userControl.Quantity;
                entity.ItemName = userControl.ItemName;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ConversionFactor = userControl.ConversionFactor; 
                if (cboSRItemType.SelectedValue == ItemType.Medical)
                {
                    ItemProductMedic med = new ItemProductMedic();
                    med.LoadByPrimaryKey(entity.ItemID);
                    entity.CostPrice = med.CostPrice;
                    entity.Price = med.PriceInBasedUnitWVat;
                    entity.IsControlExpired = med.IsControlExpired ?? false;
                }
                else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                {
                    ItemProductNonMedic nonMed = new ItemProductNonMedic();
                    nonMed.LoadByPrimaryKey(entity.ItemID);
                    entity.CostPrice = nonMed.CostPrice;
                    entity.Price = nonMed.PriceInBasedUnitWVat;
                    entity.IsControlExpired = nonMed.IsControlExpired ?? false;
                }
                else
                {
                    var kitchen = new ItemKitchen();
                    kitchen.LoadByPrimaryKey(entity.ItemID);
                    entity.CostPrice = kitchen.CostPrice;
                    entity.Price = kitchen.PriceInBasedUnitWVat;
                    entity.IsControlExpired = kitchen.IsControlExpired ?? false;
                }
            }
        }

        #endregion

        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateNewTransactionNo();
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, e.Value, TransactionCode.InventoryIssueRequestOut);
            cboSRItemType.SelectedValue = string.Empty;
            cboSRItemType.Text = string.Empty;
        }

        protected void cboSRItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.InventoryIssueOutForOtherUnit, false, string.Empty, e.Value);
        }

        protected void cboToServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComboBox.PopulateWithServiceUnitForLocation(cboToLocationID, e.Value);
            cboToLocationID.SelectedIndex = 1;
        }
    }
}
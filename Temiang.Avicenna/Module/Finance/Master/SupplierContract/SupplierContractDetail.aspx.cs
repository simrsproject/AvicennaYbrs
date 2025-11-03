using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class SupplierContractDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumberLast;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "SupplierContractSearch.aspx";
            UrlPageList = "SupplierContractList.aspx";

            ProgramID = AppConstant.Program.SupplierContract;

            if (!IsPostBack)
            {
                SupplierContractItems = null;
                SupplierContractItemNonMedics = null;
                SupplierContractItemKitchens = null;
            }
            
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItem, grdItem);
            ajax.AddAjaxSetting(grdItemNonMedic, grdItemNonMedic);
            ajax.AddAjaxSetting(grdItemKitchen, grdItemKitchen);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            if (txtTransactionNo.Text.Trim() == string.Empty)
            {
                args.MessageText = AppConstant.Message.RecordCanNotEdited;
                args.IsCancel = true;
                return;
            }

            var entity = new SupplierContract();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new SupplierContract());
            txtTranasctionDate.SelectedDate = DateTime.Now;
            cboSupplierID.Text = string.Empty;
            cboSupplierID.SelectedValue = string.Empty;
            txtContractAmount.Value = 0;
            txtPurchaseAmount.Value = 0;
            txtDiscountAmount.Value = 0;
            chkIsActive.Checked = true;

            PopulateNewRequestNo();
        }

        private void PopulateNewRequestNo()
        {
            _autoNumberLast = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.SupplierContract);
            txtTransactionNo.Text = _autoNumberLast.LastCompleteNumber;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new SupplierContract();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                entity.MarkAsDeleted();

                var coll = new SupplierContractItemCollection();
                string transno = txtTransactionNo.Text;
                coll.Query.Where(coll.Query.TransactionNo == transno);
                coll.LoadAll();
                coll.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    entity.Save();
                    coll.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            PopulateNewRequestNo();
            var entity = new SupplierContract();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new SupplierContract();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
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
            auditLogFilter.TableName = "SupplierContract";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(newVal);
            RefreshCommandItemNonMedicGrid(newVal);
            RefreshCommandItemKitchenGrid(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new SupplierContract();
            if (parameters.Length > 0)
            {
                String transno = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transno);
            }
            else
            {
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var sc = (SupplierContract) entity;
            txtTransactionNo.Text = sc.TransactionNo;
            txtTranasctionDate.SelectedDate = sc.TransactionDate;
            if (!string.IsNullOrEmpty(sc.SupplierID))
                ComboBox.SupplierItemsRequested(cboSupplierID, sc.SupplierID);
            
            txtContractNumber.Text = sc.ContractNo;
            txtContractStart.SelectedDate = sc.ContractStart;
            txtContractEnd.SelectedDate = sc.ContractEnd;
            txtContractSummary.Text = sc.ContractSummary;
            txtContractAmount.Value = Convert.ToDouble(sc.ContractAmount);
            txtPurchaseAmount.Value = Convert.ToDouble(sc.PurchaseAmount);
            txtDiscountAmount.Value = Convert.ToDouble(sc.DiscountAmount);
            chkIsActive.Checked = sc.IsActive ?? false;

            //Display Data Detail
            PopulateGridDetail();
            PopulateGridNonMedicDetail();
            PopulateGridDetailKitchen();
        }

        public override bool OnGetStatusMenuEdit()
        {
            return txtTransactionNo.Text != string.Empty;
        }
        #endregion

        #region Private Method Standard

        private void SetEntityValue(esSupplierContract entity)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = DateTime.Now; //txtTariffRequestDate.SelectedDate;
            entity.SupplierID = cboSupplierID.SelectedValue;
            entity.ContractNo = txtContractNumber.Text;
            entity.ContractStart = txtContractStart.SelectedDate;
            entity.ContractEnd = txtContractEnd.SelectedDate;
            entity.ContractSummary = txtContractSummary.Text;
            entity.ContractAmount = Convert.ToDecimal(txtContractAmount.Value);
            entity.IsActive = chkIsActive.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Detail Item
            var coll = SupplierContractItems;
            foreach (SupplierContractItem item in coll)
            {
                item.TransactionNo = txtTransactionNo.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }

            var coll2 = SupplierContractItemNonMedics;
            foreach (SupplierContractItem item in coll2)
            {
                item.TransactionNo = txtTransactionNo.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }

            var coll3 = SupplierContractItemKitchens;
            foreach (SupplierContractItem item in coll3)
            {
                item.TransactionNo = txtTransactionNo.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(esEntity entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                SupplierContractItems.Save();
                SupplierContractItemNonMedics.Save();
                SupplierContractItemKitchens.Save();

                //AutoNumberLast
                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumberLast.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new SupplierContractQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TransactionNo > txtTransactionNo.Text);
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < txtTransactionNo.Text);
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new SupplierContract();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function
        private SupplierContractItemCollection SupplierContractItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collSupplierContractItem"];
                    if (obj != null)
                        return ((SupplierContractItemCollection)(obj));
                }

                var coll = new SupplierContractItemCollection();
                var query = new SupplierContractItemQuery("a");

                var iq = new ItemQuery("b");
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);

                var prodmedQ = new ItemProductMedicQuery("p");
                query.InnerJoin(prodmedQ).On(query.ItemID == prodmedQ.ItemID);

                query.Where(query.TransactionNo == txtTransactionNo.Text);

                query.Select
                    (
                        query.TransactionNo,
                        query.ItemID,
                        iq.ItemName.As("refToItem_ItemName"),
                        query.PurchaseDiscount1,
                        query.PurchaseDiscount2,
                        prodmedQ.SRPurchaseUnit,
                        query.PriceInPurchaseUnit,
                        query.IsActive,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID
                    );

                query.OrderBy(query.ItemID.Ascending);

                coll.Load(query);

                Session["collSupplierContractItem"] = coll;
                return coll;
            }
            set { Session["collSupplierContractItem"] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            SupplierContractItems = null; //Reset Record Detail
            grdItem.DataSource = SupplierContractItems;
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = SupplierContractItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            String itemID =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][SupplierContractItemMetadata.ColumnNames.ItemID]);
            var entity = FindItemGrid(itemID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemID =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][SupplierContractItemMetadata.ColumnNames.ItemID]);
            var entity = FindItemGrid(itemID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = SupplierContractItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private SupplierContractItem FindItemGrid(string itemID)
        {
            var coll = SupplierContractItems;
            SupplierContractItem retval = null;
            foreach (SupplierContractItem rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        private void SetEntityValue(SupplierContractItem entity, GridCommandEventArgs e)
        {
            var userControl = (SupplierContractItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.SRPurchaseUnit = userControl.SRPurchaseUnit;
                entity.PriceInPurchaseUnit = userControl.PriceInPurchaseUnit;
                entity.PurchaseDiscount1 = userControl.PurchaseDiscount1;
                entity.PurchaseDiscount2 = userControl.PurchaseDiscount2;
                entity.IsActive = userControl.IsActive;
            }
        }
        #endregion

        #region Record Detail Non Medic Method Function
        private SupplierContractItemCollection SupplierContractItemNonMedics
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collSupplierContractItemNonMedic"];
                    if (obj != null)
                        return ((SupplierContractItemCollection)(obj));
                }

                var coll = new SupplierContractItemCollection();
                var query = new SupplierContractItemQuery("a");

                var iq = new ItemQuery("b");
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);

                var prodmedQ = new ItemProductNonMedicQuery("p");
                query.InnerJoin(prodmedQ).On(query.ItemID == prodmedQ.ItemID);

                query.Where(query.TransactionNo == txtTransactionNo.Text);

                query.Select
                    (
                        query.TransactionNo,
                        query.ItemID,
                        iq.ItemName.As("refToItem_ItemName"),
                        query.PurchaseDiscount1,
                        query.PurchaseDiscount2,
                        prodmedQ.SRPurchaseUnit,
                        query.PriceInPurchaseUnit,
                        query.IsActive,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID
                    );

                query.OrderBy(query.ItemID.Ascending);

                coll.Load(query);

                Session["collSupplierContractItemNonMedic"] = coll;
                return coll;
            }
            set { Session["collSupplierContractItemNonMedic"] = value; }
        }

        private void RefreshCommandItemNonMedicGrid(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemNonMedic.Columns[0].Visible = isVisible;
            grdItemNonMedic.Columns[grdItemNonMedic.Columns.Count - 1].Visible = isVisible;

            grdItemNonMedic.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdItemNonMedic.Rebind();
        }

        private void PopulateGridNonMedicDetail()
        {
            //Display Data Detail
            SupplierContractItemNonMedics = null; //Reset Record Detail
            grdItemNonMedic.DataSource = SupplierContractItemNonMedics;
            grdItemNonMedic.MasterTableView.IsItemInserted = false;
            grdItemNonMedic.MasterTableView.ClearEditItems();
            grdItemNonMedic.DataBind();
        }

        protected void grdItemNonMedic_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemNonMedic.DataSource = SupplierContractItemNonMedics;
        }

        protected void grdItemNonMedic_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            String itemID =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][SupplierContractItemMetadata.ColumnNames.ItemID]);
            var entity = FindItemNonMedicGrid(itemID);
            if (entity != null)
                SetEntityNonMedicValue(entity, e);
        }

        protected void grdItemNonMedic_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemID =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][SupplierContractItemMetadata.ColumnNames.ItemID]);
            var entity = FindItemNonMedicGrid(itemID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItemNonMedic_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = SupplierContractItemNonMedics.AddNew();
            SetEntityNonMedicValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemNonMedic.Rebind();
        }

        private SupplierContractItem FindItemNonMedicGrid(string itemID)
        {
            var coll = SupplierContractItemNonMedics;
            SupplierContractItem retval = null;
            foreach (SupplierContractItem rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        private void SetEntityNonMedicValue(SupplierContractItem entity, GridCommandEventArgs e)
        {
            var userControl = (SupplierContractItemNonMedicDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.SRPurchaseUnit = userControl.SRPurchaseUnit;
                entity.PriceInPurchaseUnit = userControl.PriceInPurchaseUnit;
                entity.PurchaseDiscount1 = userControl.PurchaseDiscount1;
                entity.PurchaseDiscount2 = userControl.PurchaseDiscount2;
                entity.IsActive = userControl.IsActive;
            }
        }
        #endregion

        #region Record Detail Kitchen Method Function
        private SupplierContractItemCollection SupplierContractItemKitchens
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collSupplierContractItemKitchen"];
                    if (obj != null)
                        return ((SupplierContractItemCollection)(obj));
                }

                var coll = new SupplierContractItemCollection();
                var query = new SupplierContractItemQuery("a");

                var iq = new ItemQuery("b");
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);

                var prodmedQ = new ItemKitchenQuery("p");
                query.InnerJoin(prodmedQ).On(query.ItemID == prodmedQ.ItemID);

                query.Where(query.TransactionNo == txtTransactionNo.Text);

                query.Select
                    (
                        query.TransactionNo,
                        query.ItemID,
                        iq.ItemName.As("refToItem_ItemName"),
                        query.PurchaseDiscount1,
                        query.PurchaseDiscount2,
                        prodmedQ.SRPurchaseUnit,
                        query.PriceInPurchaseUnit,
                        query.IsActive,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID
                    );

                query.OrderBy(query.ItemID.Ascending);

                coll.Load(query);

                Session["collSupplierContractItemKitchen"] = coll;
                return coll;
            }
            set { Session["collSupplierContractItemKitchen"] = value; }
        }

        private void RefreshCommandItemKitchenGrid(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemKitchen.Columns[0].Visible = isVisible;
            grdItemKitchen.Columns[grdItemKitchen.Columns.Count - 1].Visible = isVisible;

            grdItemKitchen.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdItemKitchen.Rebind();
        }

        private void PopulateGridDetailKitchen()
        {
            //Display Data Detail
            SupplierContractItemKitchens = null; //Reset Record Detail
            grdItemKitchen.DataSource = SupplierContractItemKitchens;
            grdItemKitchen.MasterTableView.IsItemInserted = false;
            grdItemKitchen.MasterTableView.ClearEditItems();
            grdItemKitchen.DataBind();
        }

        protected void grdItemKitchen_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemKitchen.DataSource = SupplierContractItemKitchens;
        }

        protected void grdItemKitchen_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            String itemID =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][SupplierContractItemMetadata.ColumnNames.ItemID]);
            var entity = FindItemKitchenGrid(itemID);
            if (entity != null)
                SetEntityKitchenValue(entity, e);
        }

        protected void grdItemKitchen_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemID =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][SupplierContractItemMetadata.ColumnNames.ItemID]);
            var entity = FindItemKitchenGrid(itemID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItemKitchen_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = SupplierContractItemKitchens.AddNew();
            SetEntityKitchenValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemKitchen.Rebind();
        }

        private SupplierContractItem FindItemKitchenGrid(string itemID)
        {
            var coll = SupplierContractItemKitchens;
            SupplierContractItem retval = null;
            foreach (SupplierContractItem rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        private void SetEntityKitchenValue(SupplierContractItem entity, GridCommandEventArgs e)
        {
            var userControl = (SupplierContractItemKitchenDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.SRPurchaseUnit = userControl.SRPurchaseUnit;
                entity.PriceInPurchaseUnit = userControl.PriceInPurchaseUnit;
                entity.PurchaseDiscount1 = userControl.PurchaseDiscount1;
                entity.PurchaseDiscount2 = userControl.PurchaseDiscount2;
                entity.IsActive = userControl.IsActive;
            }
        }
        #endregion

        #region Combobox
        protected void cboSupplierID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.SupplierItemsRequested((RadComboBox)o, e.Text);
        }

        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.SupplierItemDataBound(e);
        }
        #endregion
    }
}

using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Finance.Tariff
{
    public partial class ItemProductTariffRequestDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumberLast;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ItemProductTariffRequestSearch.aspx";
            UrlPageList = "ItemProductTariffRequestList.aspx";

            ProgramID = AppConstant.Program.ITEM_PRODUCT_TARIFF_REQUEST;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRTariffType, AppEnum.StandardReference.TariffType);
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);

                //Class
                var coll = new ClassCollection();
                coll.Query.Where(coll.Query.IsActive == true, coll.Query.IsTariffClass == true);
                coll.LoadAll();

                cboClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Class c in coll)
                {
                    cboClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (DataModeCurrent != AppEnum.DataMode.Read)
            {
                //Item type bisa dirubah bila item detail belum dipilih
                cboSRItemType.Enabled = ItemTariffRequestItems.Count == 0;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItemTariffRequestItem, grdItemTariffRequestItem);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            if (txtTariffRequestNo.Text.Trim() == string.Empty)
            {
                args.MessageText = AppConstant.Message.RecordCanNotEdited;
                args.IsCancel = true;
                return;
            }

            var entity = new ItemTariffRequest();
            if (!entity.LoadByPrimaryKey(txtTariffRequestNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (entity.IsApproved != null && entity.IsApproved.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved + AppConstant.Message.RecordCanNotEdited;
                args.IsCancel = true;
                return;
            }
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ItemTariffRequest());
            txtTariffRequestDate.SelectedDate = DateTime.Now;
            txtStartingDate.SelectedDate = DateTime.Now;
            cboSRTariffType.SelectedValue = AppSession.Parameter.DefaultTariffType;
            cboClassID.SelectedValue = AppSession.Parameter.DefaultTariffClass;
            if (AppSession.Parameter.IsDisableClassOnRequestChangeItemProduct)
            {
                cboClassID.Enabled = false;
            }

            PopulateNewRequestNo();
        }

        private void PopulateNewRequestNo()
        {
            _autoNumberLast = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.TariffRequestNo);
            txtTariffRequestNo.Text = _autoNumberLast.LastCompleteNumber;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ItemTariffRequest();
            if (entity.LoadByPrimaryKey(txtTariffRequestNo.Text))
            {
                entity.MarkAsDeleted();

                var coll = new ItemTariffRequestItemCollection();
                string tariffRequestNo = txtTariffRequestNo.Text;
                coll.Query.Where(coll.Query.TariffRequestNo == tariffRequestNo);
                coll.LoadAll();
                coll.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    coll.Save();
                    entity.Save();

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
            var entity = new ItemTariffRequest();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ItemTariffRequest();
            if (entity.LoadByPrimaryKey(txtTariffRequestNo.Text))
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
            (new ItemTariffRequest()).Approv(txtTariffRequestNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new ItemTariffRequest();
            if (!entity.LoadByPrimaryKey(txtTariffRequestNo.Text))
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
            entity.IsVoid = true;
            entity.VoidDate = DateTime.Now.Date;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                trans.Complete();
            }
            txtVoidDate.Text = entity.VoidDate.Value.ToString(AppConstant.DisplayFormat.Date);
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
            auditLogFilter.PrimaryKeyData = string.Format("TariffRequestNo='{0}'", txtTariffRequestNo.Text.Trim());
            auditLogFilter.TableName = "ItemTariffRequest";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ItemTariffRequest();
            if (parameters.Length > 0)
            {
                String tariffRequestNo = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(tariffRequestNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtTariffRequestNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var itemTariffRequest = (ItemTariffRequest)entity;
            txtTariffRequestNo.Text = itemTariffRequest.TariffRequestNo;
            txtTariffRequestDate.SelectedDate = itemTariffRequest.TariffRequestDate;
            cboSRTariffType.SelectedValue = itemTariffRequest.SRTariffType;
            cboSRItemType.SelectedValue = itemTariffRequest.SRItemType;
            cboClassID.SelectedValue = itemTariffRequest.ClassID;
            txtStartingDate.SelectedDate = itemTariffRequest.StartingDate;
            chkIsApproved.Checked = itemTariffRequest.IsApproved ?? false;
            txtApprovedDate.Text = itemTariffRequest.ApprovedDate == null
                                       ? string.Empty
                                       : itemTariffRequest.ApprovedDate.Value.ToString(AppConstant.DisplayFormat.Date);

            chkIsVoid.Checked = itemTariffRequest.IsVoid ?? false;
            txtVoidDate.Text = itemTariffRequest.VoidDate == null
                                       ? string.Empty
                                       : itemTariffRequest.VoidDate.Value.ToString(AppConstant.DisplayFormat.Date);

            txtNotes.Text = itemTariffRequest.Notes;

            //Display Data Detail
            PopulateGridDetail();
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }
        public override bool OnGetStatusMenuVoid()
        {
            return !chkIsVoid.Checked;
        }
        public override bool OnGetStatusMenuEdit()
        {
            return txtTariffRequestNo.Text != string.Empty;
        }
        #endregion

        #region Private Method Standard

        private void SetEntityValue(esItemTariffRequest entity)
        {
            entity.TariffRequestNo = txtTariffRequestNo.Text;
            entity.TariffRequestDate = DateTime.Now;// txtTariffRequestDate.SelectedDate;
            entity.SRTariffType = cboSRTariffType.SelectedValue;
            entity.SRItemType = cboSRItemType.SelectedValue;
            entity.ClassID = cboClassID.SelectedValue;
            entity.StartingDate = txtStartingDate.SelectedDate;
            entity.Notes = txtNotes.Text;
            entity.IsApproved = false;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Detail Item
            ItemTariffRequestItemCollection coll = ItemTariffRequestItems;
            foreach (ItemTariffRequestItem item in coll)
            {
                item.TariffRequestNo = txtTariffRequestNo.Text;
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
                ItemTariffRequestItems.Save();

                //AutoNumberLast
                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumberLast.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ItemTariffRequestQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TariffRequestNo > txtTariffRequestNo.Text);
                que.OrderBy(que.TariffRequestNo.Ascending);
            }
            else
            {
                que.Where(que.TariffRequestNo < txtTariffRequestNo.Text);
                que.OrderBy(que.TariffRequestNo.Descending);
            }
            var entity = new ItemTariffRequest();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function

        private ItemTariffRequestItemCollection ItemTariffRequestItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemTariffRequestItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((ItemTariffRequestItemCollection)(obj));
                    }
                }

                var coll = new ItemTariffRequestItemCollection();
                var query = new ItemTariffRequestItemQuery("a");
                var itemQuery = new ItemQuery("b");
                query.InnerJoin(itemQuery).On(query.ItemID == itemQuery.ItemID);
                query.Select(query.TariffRequestNo,
                             query.ItemID,
                             query.Price,
                             query.PriceInBaseUnit,
                             query.PriceInBaseUnitWVat,
                             query.PriceInPurchaseUnit,
                             query.CostPrice,
                             query.DiscPercentage,
                             query.LastUpdateDateTime,
                             query.LastUpdateByUserID,
                             itemQuery.ItemName.As("refToItem_ItemName"));
                string tariffRequestNo = txtTariffRequestNo.Text;

                query.Where(query.TariffRequestNo == tariffRequestNo);
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collItemTariffRequestItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collItemTariffRequestItem" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemTariffRequestItem.Columns[0].Visible = isVisible;
            grdItemTariffRequestItem.Columns[grdItemTariffRequestItem.Columns.Count - 1].Visible = isVisible;

            grdItemTariffRequestItem.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            grdItemTariffRequestItem.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            ItemTariffRequestItems = null; //Reset Record Detail
            grdItemTariffRequestItem.DataSource = ItemTariffRequestItems;
            grdItemTariffRequestItem.MasterTableView.IsItemInserted = false;
            grdItemTariffRequestItem.MasterTableView.ClearEditItems();
            grdItemTariffRequestItem.DataBind();
        }

        protected void grdItemTariffRequestItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemTariffRequestItem.DataSource = ItemTariffRequestItems;
        }

        protected void grdItemTariffRequestItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemID =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ItemTariffRequestItemMetadata.ColumnNames.ItemID]);
            ItemTariffRequestItem entity = FindItemTariffRequestItem(itemID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItemTariffRequestItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemID =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemTariffRequestItemMetadata.ColumnNames.ItemID]);
            ItemTariffRequestItem entity = FindItemTariffRequestItem(itemID);
            if (entity != null)
                entity.MarkAsDeleted();

        }

        protected void grdItemTariffRequestItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemTariffRequestItem entity = ItemTariffRequestItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemTariffRequestItem.Rebind();
        }

        private ItemTariffRequestItem FindItemTariffRequestItem(String itemID)
        {
            ItemTariffRequestItemCollection coll = ItemTariffRequestItems;
            ItemTariffRequestItem retEntity = null;
            foreach (ItemTariffRequestItem rec in coll)
            {
                if (!rec.ItemID.Equals(itemID)) continue;
                retEntity = rec;
                break;
            }
            return retEntity;
        }

        private void SetEntityValue(ItemTariffRequestItem entity, GridCommandEventArgs e)
        {
            var userControl = (ItemProductTariffRequestItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl == null)
                return;
            entity.ItemID = userControl.ItemID;
            entity.ItemName = userControl.ItemName;
            entity.Price = userControl.PriceInBasedUnitWVat;
            entity.PriceInBaseUnit = userControl.PriceInBaseUnit;
            entity.PriceInBaseUnitWVat = userControl.PriceInBasedUnitWVat;
            entity.PriceInPurchaseUnit = userControl.PriceInPurchaseUnit;
            entity.CostPrice = userControl.CostPrice;
            entity.DiscPercentage = userControl.DiscPercentage;
        }

        #endregion
    }
}
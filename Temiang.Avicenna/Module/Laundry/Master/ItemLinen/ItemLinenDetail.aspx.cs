using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Laundry.Master
{
    public partial class ItemLinenDetail : BasePageDetail
    {
        private void SetEntityValue(ItemLinen entity)
        {
            //Item
            entity.ItemID = txtItemID.Text;
            entity.ItemName = txtItemName.Text;
            entity.Notes = txtNotes.Text;
            entity.IsActive = chkIsActive.Checked;
            
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Detail
            ItemLinenItemCollection coll = ItemLinenItems;
            foreach (ItemLinenItem item in coll)
            {
                item.ItemID = txtItemID.Text;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ItemLinenQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ItemID > txtItemID.Text);
                que.OrderBy(que.ItemID.Ascending);
            }
            else
            {
                que.Where(que.ItemID < txtItemID.Text);
                que.OrderBy(que.ItemID.Descending);
            }

            var entity = new ItemLinen();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ItemLinen();
            if (parameters.Length > 0)
            {
                String itemID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(itemID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtItemID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var item = (ItemLinen)entity;
            txtItemID.Text = item.ItemID;
            txtItemName.Text = item.ItemName;
            txtNotes.Text = item.Notes;
            chkIsActive.Checked = item.IsActive ?? false;

            PopulateItemGrid();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ItemLinen());
            chkIsActive.Checked = true;
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtItemID.ReadOnly = (newVal != AppEnum.DataMode.New);
            RefreshCommandItemGrid(newVal);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            // Url Search & List
            UrlPageSearch = "ItemLinenSearch.aspx";
            UrlPageList = "ItemLinenList.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.ItemLinen;

            if (!IsPostBack)
            {
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ItemLinen();

            if (entity.LoadByPrimaryKey(txtItemID.Text))
            {
                // cek apakah master item sudah ada transaksi
                var cek = new LaundryReceivedItemInfectiousCollection();
                cek.Query.Where(cek.Query.ItemID == txtItemID.Text);
                cek.LoadAll();
                if (cek.Count > 0)
                {
                    args.MessageText = "Item already used in transaction.";
                    args.IsCancel = true;
                    return;
                }

                entity.MarkAsDeleted();

                string itemID = txtItemID.Text;
                var itemLinenItemCollection = new ItemLinenItemCollection();
                itemLinenItemCollection.Query.Where(itemLinenItemCollection.Query.ItemID == itemID);
                itemLinenItemCollection.LoadAll();
                itemLinenItemCollection.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    itemLinenItemCollection.Save();
                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }

            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new ItemLinen();
            if (entity.LoadByPrimaryKey(txtItemID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new ItemLinen();
            entity.AddNew();
            
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(ItemLinen entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ItemLinenItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ItemLinen();
            if (entity.LoadByPrimaryKey(txtItemID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
        }

        #endregion

        #region Record Detail Method Function
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

        private ItemLinenItemCollection ItemLinenItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemLinenItem"];
                    if (obj != null)
                    {
                        return ((ItemLinenItemCollection)(obj));
                    }
                }

                var coll = new ItemLinenItemCollection();
                var query = new ItemLinenItemQuery("a");
                var item = new ItemQuery("b");

                string itemID = txtItemID.Text;
                query.Where(query.ItemID == itemID);
                query.Select(query.SelectAllExcept(), item.ItemName.As("refToItem_ItemName"));
                query.InnerJoin(item).On(query.ItemDetailID == item.ItemID);
                coll.Load(query);

                Session["collItemLinenItem"] = coll;
                return coll;
            }
            set { Session["collItemLinenItem"] = value; }
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            ItemLinenItems = null; //Reset Record Detail
            grdItem.DataSource = ItemLinenItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = ItemLinenItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemLinenItemMetadata.ColumnNames.ItemDetailID]);
            ItemLinenItem entity = FindItem(itemID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemLinenItemMetadata.ColumnNames.ItemDetailID]);
            ItemLinenItem entity = FindItem(itemID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemLinenItem entity = ItemLinenItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private ItemLinenItem FindItem(String itemID)
        {
            ItemLinenItemCollection coll = ItemLinenItems;
            ItemLinenItem retEntity = null;
            foreach (ItemLinenItem rec in coll)
            {
                if (rec.ItemDetailID.Equals(itemID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        
        private void SetEntityValue(ItemLinenItem entity, GridCommandEventArgs e)
        {
            var userControl = (ItemLinenItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemDetailID = userControl.ItemDetailID;
                entity.ItemDetailName = userControl.ItemDetailName;
                entity.Qty = userControl.Qty ?? 0;
                entity.QtyDetail = userControl.QtyDetail ?? 0;
                entity.SRItemUnit = userControl.SRItemUnit;
            }
        }
        #endregion
    }
}
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ProductionFormulaDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ProductionFormulaSearch.aspx?fp=dt";
            UrlPageList = "ProductionFormulaList.aspx";

            ProgramID = AppConstant.Program.ProductionFormula;

            //StandardReference Initialize
            if (!IsPostBack)
                ProductionFormulaItems = null;
            
            WindowSearch.Height = 300;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItem, grdItem);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ProductionFormula());
            chkIsActive.Checked = true;
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new ProductionFormula();
            if (!(entity.LoadByPrimaryKey(txtFormulaID.Text)))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ProductionFormula();
            if (entity.LoadByPrimaryKey(txtFormulaID.Text))
            {
                entity.MarkAsDeleted();

                var detailColl = new ProductionFormulaItemCollection();
                var detailQ = new ProductionFormulaItemQuery();
                detailQ.Where(detailQ.FormulaID == txtFormulaID.Text);
                detailColl.Load(detailQ);

                if (detailColl.Count > 0)
                    detailColl.MarkAllAsDeleted();

                using (var trans = new esTransactionScope())
                {
                    detailColl.Save();
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
            if (ProductionFormulaItems.Count == 0)
            {
                args.MessageText = "Detail item is required.";
                args.IsCancel = true;
                return;
            }

            var item = new Item();
            if (!item.LoadByPrimaryKey(cboItemID.SelectedValue))
            {
                args.MessageText = "Item Production is required.";
                args.IsCancel = true;
                return;
            }

            var entity = new ProductionFormula();
            if (entity.LoadByPrimaryKey(txtFormulaID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new ProductionFormula();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var item = new Item();
            if (!item.LoadByPrimaryKey(cboItemID.SelectedValue))
            {
                args.MessageText = "Item Production is required.";
                args.IsCancel = true;
                return;
            }

            var entity = new ProductionFormula();
            if (entity.LoadByPrimaryKey(txtFormulaID.Text))
            {
                SetEntityValue(entity);
                if (ProductionFormulaItems.Count == 0)
                {
                    args.MessageText = AppConstant.Message.RecordDetailEmpty;
                    args.IsCancel = true;
                    return;
                }
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtFormulaID.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItemProductionFormulaItem(newVal);
            RefreshCommandItemProductionFormulaOtherItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ProductionFormula();
            if (parameters.Length > 0)
            {
                String formulaId = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(formulaId);
            }
            else
            {
                entity.LoadByPrimaryKey(txtFormulaID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var pf = (ProductionFormula)entity;

            txtFormulaID.Text = pf.FormulaID;
            txtFormulaName.Text = pf.FormulaName;
            cboItemID.SelectedValue = pf.ItemID;
            txtQty.Value = Convert.ToDouble(pf.Qty);

            if (!string.IsNullOrEmpty(pf.ItemID))
            {
                var query = new ItemQuery();
                query.Select(query.ItemID, query.ItemName);
                query.Where(query.ItemID == pf.ItemID);

                DataTable dtb = query.LoadDataTable();
                cboItemID.DataSource = dtb;
                cboItemID.DataBind();
                cboItemID.SelectedValue = pf.ItemID;
                cboItemID.Text = dtb.Rows[0]["ItemName"] + " (" + dtb.Rows[0]["ItemID"] + ")";
            }
            else
            {
                cboItemID.Items.Clear();
                cboItemID.Text = string.Empty;
            }

            GetItemUnit(pf.ItemID);
            chkIsCostInPercentage.Checked = pf.IsCostInPercentage ?? false;
            txtCostAmount.Value = Convert.ToDouble(pf.CostAmount);
            txtNotes.Text = pf.Notes;
            chkIsActive.Checked = pf.IsActive ?? false;

            PopulateProductionFormulaItemGrid();
            PopulateProductionFormulaOtherItemGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ProductionFormula entity)
        {
            entity.FormulaID = txtFormulaID.Text;
            entity.FormulaName = txtFormulaName.Text;
            entity.ItemID = cboItemID.SelectedValue;
            entity.Qty = Convert.ToDecimal(txtQty.Value);
            entity.IsCostInPercentage = chkIsCostInPercentage.Checked;
            entity.CostAmount = Convert.ToDecimal(txtCostAmount.Value);
            entity.Notes = txtNotes.Text;
            entity.IsActive = chkIsActive.Checked;
            
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            foreach (ProductionFormulaItem item in ProductionFormulaItems)
            {
                item.FormulaID = txtFormulaID.Text;

                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }

            foreach (ProductionFormulaOtherItem item in ProductionFormulaOtherItems)
            {
                item.FormulaID = txtFormulaID.Text;

                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(ProductionFormula entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                ProductionFormulaItems.Save();
                ProductionFormulaOtherItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ProductionFormulaQuery("a");

            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                    que.FormulaID > txtFormulaID.Text
                    );
                que.OrderBy(que.FormulaID.Ascending);
            }
            else
            {
                que.Where
                    (
                    que.FormulaID < txtFormulaID.Text
                    );
                que.OrderBy(que.FormulaID.Descending);
            }

            var entity = new ProductionFormula();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }
        #endregion

        #region Record Detail Method Function ProductionFormulaItem

        private ProductionFormulaItemCollection ProductionFormulaItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collProductionFormulaItem"];
                    if (obj != null)
                    {
                        return ((ProductionFormulaItemCollection)(obj));
                    }
                }

                var coll = new ProductionFormulaItemCollection();
                var query = new ProductionFormulaItemQuery("a");
                var iQuery = new ItemQuery("b");
                var srQuery = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query,
                        iQuery.ItemName.As("refToItem_ItemName"),
                        srQuery.ItemName.As("refToAppStandardReferenceItem_ItemUnit")
                    );
                query.InnerJoin(iQuery).On(query.ItemID == iQuery.ItemID);

                query.InnerJoin(srQuery).On(query.SRItemUnit == srQuery.ItemID &&
                                            srQuery.StandardReferenceID == "ItemUnit");

                query.Where(query.FormulaID == txtFormulaID.Text);
                query.OrderBy(query.ItemID.Ascending);

                coll.Load(query);

                Session["collProductionFormulaItem"] = coll;
                return coll;
            }
            set { Session["collProductionFormulaItem"] = value; }
        }

        private void RefreshCommandItemProductionFormulaItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible
                                                                         ? GridCommandItemDisplay.Top
                                                                         : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateProductionFormulaItemGrid()
        {
            //Display Data Detail
            ProductionFormulaItems = null; //Reset Record Detail
            grdItem.DataSource = ProductionFormulaItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = ProductionFormulaItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            String formulaId =
                            Convert.ToString((string)editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                [ProductionFormulaItemMetadata.ColumnNames.FormulaID]);
            String itemId =
                Convert.ToString((string)editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                [ProductionFormulaItemMetadata.ColumnNames.ItemID]);
            ProductionFormulaItem entity = FindProductionFormulaItem(formulaId, itemId);

            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            String formulaId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ProductionFormulaItemMetadata.ColumnNames.FormulaID]);
            String itemId =
                            Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ProductionFormulaItemMetadata.ColumnNames.ItemID]);
            ProductionFormulaItem entity = FindProductionFormulaItem(formulaId, itemId);

            if (entity != null)
            {
                entity.MarkAsDeleted();
            }
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ProductionFormulaItem entity = ProductionFormulaItems.AddNew();

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private ProductionFormulaItem FindProductionFormulaItem(String formulaId, String itemId)
        {
            ProductionFormulaItemCollection coll = ProductionFormulaItems;
            ProductionFormulaItem retEntity = null;
            foreach (ProductionFormulaItem rec in coll)
            {
                if (rec.FormulaID.Equals(formulaId) && rec.ItemID.Equals(itemId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(ProductionFormulaItem entity, GridCommandEventArgs e)
        {
            var userControl = (ProductionFormulaDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.FormulaID = txtFormulaID.Text;
                entity.ItemID = userControl.ItemID;
                entity.Qty = userControl.Qty;
                entity.SRItemUnit = userControl.SRItemUnit;

                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);
                entity.ItemName = item.ItemName;

                var asri = new AppStandardReferenceItem();
                asri.LoadByPrimaryKey("ItemUnit", entity.SRItemUnit);
                entity.ItemUnit = asri.ItemName;
                entity.IsConsumables = userControl.IsConsumables;
            }
        }

        #endregion

        #region Record Detail Method Function ProductionFormulaOtherItem

        private ProductionFormulaOtherItemCollection ProductionFormulaOtherItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collProductionFormulaOtherItem"];
                    if (obj != null)
                    {
                        return ((ProductionFormulaOtherItemCollection)(obj));
                    }
                }

                var coll = new ProductionFormulaOtherItemCollection();
                var query = new ProductionFormulaOtherItemQuery("a");
                var iQuery = new ItemQuery("b");
                var srQuery = new AppStandardReferenceItemQuery("c");
                var vw = new VwItemProductMedicNonMedicQuery("d");
                query.Select
                    (
                        query,
                        iQuery.ItemName.As("refToItem_ItemName"),
                        srQuery.ItemName.As("refToAppStandardReferenceItem_ItemUnit")
                    );
                query.InnerJoin(iQuery).On(query.ItemID == iQuery.ItemID);
                query.InnerJoin(vw).On(vw.ItemID == query.ItemID);
                query.InnerJoin(srQuery).On(vw.SRItemUnit == srQuery.ItemID &&
                                            srQuery.StandardReferenceID == "ItemUnit");

                query.Where(query.FormulaID == txtFormulaID.Text);
                query.OrderBy(query.ItemID.Ascending);

                coll.Load(query);

                Session["collProductionFormulaOtherItem"] = coll;
                return coll;
            }
            set { Session["collProductionFormulaOtherItem"] = value; }
        }

        private void RefreshCommandItemProductionFormulaOtherItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdOtherProducts.Columns[0].Visible = isVisible;
            grdOtherProducts.Columns[grdOtherProducts.Columns.Count - 1].Visible = isVisible;

            grdOtherProducts.MasterTableView.CommandItemDisplay = isVisible
                                                                         ? GridCommandItemDisplay.Top
                                                                         : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdOtherProducts.Rebind();
        }

        private void PopulateProductionFormulaOtherItemGrid()
        {
            //Display Data Detail
            ProductionFormulaOtherItems = null; //Reset Record Detail
            grdOtherProducts.DataSource = ProductionFormulaOtherItems; //Requery
            grdOtherProducts.MasterTableView.IsItemInserted = false;
            grdOtherProducts.MasterTableView.ClearEditItems();
            grdOtherProducts.DataBind();
        }

        protected void grdOtherProducts_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdOtherProducts.DataSource = ProductionFormulaOtherItems;
        }

        protected void grdOtherProducts_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            String formulaId =
                Convert.ToString((string)editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ProductionFormulaOtherItemMetadata.ColumnNames.FormulaID]);
            String itemId =
                Convert.ToString((string)editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ProductionFormulaOtherItemMetadata.ColumnNames.ItemID]);
            ProductionFormulaOtherItem entity = FindProductionFormulaOtherItem(formulaId, itemId);

            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdOtherProducts_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            String formulaId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ProductionFormulaOtherItemMetadata.ColumnNames.FormulaID]);
            String itemId =
                            Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ProductionFormulaOtherItemMetadata.ColumnNames.ItemID]);
            ProductionFormulaOtherItem entity = FindProductionFormulaOtherItem(formulaId, itemId);

            if (entity != null)
            {
                entity.MarkAsDeleted();
            }
        }

        protected void grdOtherProducts_InsertCommand(object source, GridCommandEventArgs e)
        {
            ProductionFormulaOtherItem entity = ProductionFormulaOtherItems.AddNew();

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdOtherProducts.Rebind();
        }

        private ProductionFormulaOtherItem FindProductionFormulaOtherItem(String formulaId, String itemId)
        {
            ProductionFormulaOtherItemCollection coll = ProductionFormulaOtherItems;
            ProductionFormulaOtherItem retEntity = null;
            foreach (ProductionFormulaOtherItem rec in coll)
            {
                if (rec.FormulaID.Equals(formulaId) && rec.ItemID.Equals(itemId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(ProductionFormulaOtherItem entity, GridCommandEventArgs e)
        {
            var userControl = (ProductionFormulaOtherDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.FormulaID = txtFormulaID.Text;
                entity.ItemID = userControl.ItemID;
                entity.Qty = userControl.Qty;
                
                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);
                entity.ItemName = item.ItemName;

                var asri = new AppStandardReferenceItem();
                asri.LoadByPrimaryKey("ItemUnit", userControl.SRItemUnit);
                entity.ItemUnit = asri.ItemName;
            }
        }

        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
        } 

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler source, string argument)
        {
            base.RaisePostBackEvent(source, argument);

            if (string.IsNullOrEmpty(argument) || !(source is RadGrid))
                return;
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.Where
                (
                    query.Or
                        (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                        ),
                    query.IsActive == true, query.IsItemProduction == true
                );

            cboItemID.DataSource = query.LoadDataTable();
            cboItemID.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"] + " (" + (((DataRowView)e.Item.DataItem)["ItemID"] + ")");
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value == string.Empty)
            {
                txtSRItemUnit.Text = string.Empty;
                return;
            }

            GetItemUnit(e.Value);
        }

        protected  void GetItemUnit(string itemId)
        {
            if (!string.IsNullOrEmpty(itemId))
            {
                string unit = string.Empty;
                var item = new Item();
                if (item.LoadByPrimaryKey(itemId))
                {
                    if (item.SRItemType == ItemType.Medical)
                    {
                        var ipm = new ItemProductMedic();
                        ipm.LoadByPrimaryKey(itemId);
                        unit = ipm.SRItemUnit;
                    }
                    else if (item.SRItemType == ItemType.NonMedical)
                    {
                        var ipnm = new ItemProductNonMedic();
                        ipnm.LoadByPrimaryKey(itemId);
                        unit = ipnm.SRItemUnit;
                    }
                    else
                    {
                        var ik = new ItemKitchen();
                        ik.LoadByPrimaryKey(itemId);
                        unit = ik.SRItemUnit;
                    }
                }
                txtSRItemUnit.Text = unit;
            }
        }
    }
}

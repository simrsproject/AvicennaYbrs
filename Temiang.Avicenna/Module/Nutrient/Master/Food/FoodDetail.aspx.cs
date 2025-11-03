using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class FoodDetail : BasePageDetail
    {
        private string FormType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            if (FormType == "sales")
            {
                UrlPageSearch = "FoodSearch.aspx?type=sales";
                UrlPageList = "FoodList.aspx?type=sales";
                ProgramID = AppConstant.Program.FoodCafetaria;
            }
            else
            {
                UrlPageSearch = "FoodSearch.aspx";
                UrlPageList = "FoodList.aspx";
                ProgramID = AppConstant.Program.Food;
            }

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRItemUnit, AppEnum.StandardReference.ItemUnit);
                StandardReference.InitializeIncludeSpace(cboSRFoodGroup1, AppEnum.StandardReference.FoodGroup1);
                StandardReference.InitializeIncludeSpace(cboSRFoodGroup2, AppEnum.StandardReference.FoodGroup2, FormType == "sales" ? "U" : "P");

                rfvSRFoodGroup2.Visible = AppSession.Parameter.IsFoodSelectedByType || FormType == "sales";
                trIsPackage.Visible = !AppSession.Parameter.IsFoodSelectedByType && FormType != "sales";
                trIsForSpecialCondition.Visible = FormType != "sales";
                trIsIsSalesAvailable.Visible = FormType == "sales";
                chkIsSalesAvailable.Enabled = false;
                trItemID.Visible = FormType == "sales";
                rfvItemID.Visible = FormType == "sales";
                tabStrip.Tabs[1].Visible = !AppSession.Parameter.IsFoodSelectedByType && FormType == "sales";
            }
        }

        private void SetEntityValue(Food entity)
        {
            entity.FoodID = txtFoodID.Text;
            entity.FoodName = txtFoodName.Text;
            entity.Notes = txtNotes.Text;
            entity.Weight = Convert.ToDecimal(txtWeight.Value);
            entity.SRItemUnit = cboSRItemUnit.SelectedValue;
            entity.QtyPortion = Convert.ToDecimal(txtQtyPortion.Value);
            entity.SRFoodGroup1 = cboSRFoodGroup1.SelectedValue;
            entity.SRFoodGroup2 = cboSRFoodGroup2.SelectedValue;
            entity.IsForSpecialCondition = chkIsForSpecialCondition.Checked;
            entity.IsSalesAvailable = chkIsSalesAvailable.Checked;
            entity.IsPackage = chkIsPackage.Checked;
            entity.IsActive = chkIsActive.Checked;
            entity.ItemID = cboItemID.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Update Key
            FoodItemCollection coll = FoodItems;
            foreach (FoodItem item in coll)
            {
                item.FoodID = txtFoodID.Text;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }

            FoodPackageCollection coll2 = FoodPackages;
            foreach (FoodPackage item in coll2)
            {
                item.FoodID = txtFoodID.Text;

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
            var que = new FoodQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.FoodID > txtFoodID.Text);
                if (FormType == "sales")
                    que.Where(que.IsSalesAvailable == true);
                else 
                    que.Where(que.IsSalesAvailable == false);
                que.OrderBy(que.FoodID.Ascending);
            }
            else
            {
                que.Where(que.FoodID < txtFoodID.Text);
                if (FormType == "sales")
                    que.Where(que.IsSalesAvailable == true);
                else
                    que.Where(que.IsSalesAvailable == false);
                que.OrderBy(que.FoodID.Descending);
            }
            var entity = new Food();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Food();
            if (parameters.Length > 0)
            {
                String foodId = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(foodId);
            }
            else
                entity.LoadByPrimaryKey(txtFoodID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var food = (Food)entity;
            txtFoodID.Text = food.FoodID;
            txtFoodName.Text = food.FoodName;
            txtNotes.Text = food.Notes;
            txtWeight.Value = Convert.ToDouble(food.Weight);
            cboSRItemUnit.SelectedValue = food.SRItemUnit;
            txtQtyPortion.Value = Convert.ToDouble(food.QtyPortion);
            cboSRFoodGroup1.SelectedValue = food.SRFoodGroup1;
            cboSRFoodGroup2.SelectedValue = food.SRFoodGroup2;
            chkIsForSpecialCondition.Checked = food.IsForSpecialCondition ?? false;
            chkIsSalesAvailable.Checked = food.IsSalesAvailable ?? false;
            chkIsPackage.Checked = food.IsPackage ?? false;
            chkIsActive.Checked = food.IsActive ?? false;
            if (!string.IsNullOrEmpty(food.ItemID))
            {
                var iq = new ItemQuery();
                iq.Where(iq.ItemID == food.ItemID);
                cboItemID.DataSource = iq.LoadDataTable();
                cboItemID.DataBind();
                cboItemID.SelectedValue = food.ItemID;
            }
            else
            {
                cboItemID.Items.Clear();
                cboItemID.Text = string.Empty;
            }

            //Display Data Detail
            PopulateGridDetail();
            PopulateGridPackageDetail();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Food());
            if (AppSession.Parameter.IsCreateFoodIdAutomatic)
                txtFoodID.ReadOnly = true;
            cboSRItemUnit.Text = string.Empty;
            cboSRFoodGroup1.Text = string.Empty;
            cboSRFoodGroup2.Text = string.Empty;
            chkIsSalesAvailable.Checked = FormType == "sales";
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

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = "FoodID='" + txtFoodID.Text.Trim() + "'";
            auditLogFilter.TableName = "Food";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtFoodID.ReadOnly = (newVal != AppEnum.DataMode.New);
            RefreshCommandItemGrid(oldVal, newVal);
            RefreshCommandPackageGrid(oldVal, newVal);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new Food();
            if (entity.LoadByPrimaryKey(txtFoodID.Text))
            {
                var menuColl = new MenuItemFoodCollection();
                menuColl.Query.Where(menuColl.Query.FoodID == txtFoodID.Text);
                menuColl.LoadAll();
                if (menuColl.Count > 0)
                {
                    args.MessageText = AppConstant.Message.RecordHasUsed;
                    args.IsCancel = true;
                    return;
                }

                entity.MarkAsDeleted();

                var foodItems = new FoodItemCollection();
                foodItems.Query.Where(foodItems.Query.FoodID == txtFoodID.Text);
                foodItems.MarkAllAsDeleted();

                var foodPackages = new FoodPackageCollection();
                foodPackages.Query.Where(foodPackages.Query.FoodID == txtFoodID.Text);
                foodPackages.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    foodItems.Save();
                    foodPackages.Save();

                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (AppSession.Parameter.IsCreateFoodIdAutomatic)
            {
                txtFoodID.Text = Helper.GetFoodID(txtFoodName.Text.ToUpper());
            }
            else if (string.IsNullOrEmpty(txtFoodID.Text))
            {
                args.MessageText = "Food ID required.";
                args.IsCancel = true;
                return;
            }

            var f = new Food();
            if (f.LoadByPrimaryKey(txtFoodID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            var entity = new Food();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(Food entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                FoodItems.Save();
                FoodPackages.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new Food();
            if (entity.LoadByPrimaryKey(txtFoodID.Text))
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

        private FoodItemCollection FoodItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collFoodItem"];
                    if (obj != null)
                        return ((FoodItemCollection)(obj));
                }

                var coll = new FoodItemCollection();
                var query = new FoodItemQuery("a");
                var iQ = new ItemQuery("b");

                string foodId = txtFoodID.Text;
                query.Select
                    (
                        query,
                        iQ.ItemName.As("refToItem_ItemName")
                    );
                query.InnerJoin(iQ).On(query.ItemID == iQ.ItemID);
                query.Where(query.FoodID == foodId);
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collFoodItem"] = coll;
                return coll;
            }
            set { Session["collFoodItem"] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdFoodItem.Columns[0].Visible = isVisible;
            grdFoodItem.Columns[grdFoodItem.Columns.Count - 1].Visible = isVisible;

            grdFoodItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                FoodItems = null;

            //Perbaharui tampilan dan data
            grdFoodItem.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            FoodItems = null; //Reset Record Detail
            grdFoodItem.DataSource = FoodItems;
            grdFoodItem.DataBind();
        }

        protected void grdFoodItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdFoodItem.DataSource = FoodItems;
        }

        protected void grdFoodItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            String itemId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][FoodItemMetadata.ColumnNames.ItemID]);
            FoodItem entity = FindItemGrid(itemId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdFoodItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String itemId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][FoodItemMetadata.ColumnNames.ItemID]);
            FoodItem entity = FindItemGrid(itemId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdFoodItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            FoodItem entity = FoodItems.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdFoodItem.Rebind();
        }

        private void SetEntityValue(FoodItem entity, GridCommandEventArgs e)
        {
            var userControl = (FoodItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.FoodID= txtFoodID.Text;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Qty = userControl.Qty;
                entity.SRItemUnit = userControl.SRItemUnit;
            }
        }

        private FoodItem FindItemGrid(string itemId)
        {
            FoodItemCollection coll = FoodItems;
            FoodItem retval = null;
            foreach (FoodItem rec in coll)
            {
                if (rec.ItemID.Equals(itemId))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        #endregion

        #region Record Detail Method Function - Food Package

        private FoodPackageCollection FoodPackages
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collFoodPackage"];
                    if (obj != null)
                        return ((FoodPackageCollection)(obj));
                }

                var coll = new FoodPackageCollection();
                var query = new FoodPackageQuery("a");
                var fQ = new FoodQuery("b");

                string foodId = txtFoodID.Text;
                query.Select
                    (
                        query,
                        fQ.FoodName.As("refToFood_FoodName")
                    );
                query.InnerJoin(fQ).On(query.FoodDetailID == fQ.FoodID);
                query.Where(query.FoodID == foodId);
                query.OrderBy(query.FoodDetailID.Ascending);
                coll.Load(query);

                Session["collFoodPackage"] = coll;
                return coll;
            }
            set { Session["collFoodPackage"] = value; }
        }

        private void RefreshCommandPackageGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdFoodPackage.Columns[grdFoodPackage.Columns.Count - 1].Visible = isVisible;

            grdFoodPackage.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                FoodPackages = null;

            //Perbaharui tampilan dan data
            grdFoodPackage.Rebind();
        }

        private void PopulateGridPackageDetail()
        {
            //Display Data Detail
            FoodPackages = null; //Reset Record Detail
            grdFoodPackage.DataSource = FoodPackages;
            grdFoodPackage.DataBind();
        }

        protected void grdFoodPackage_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdFoodPackage.DataSource = FoodPackages;
        }

        protected void grdFoodPackage_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String foodId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][FoodPackageMetadata.ColumnNames.FoodDetailID]);
            FoodPackage entity = FindPackageGrid(foodId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdFoodPackage_InsertCommand(object source, GridCommandEventArgs e)
        {
            FoodPackage entity = FoodPackages.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdFoodPackage.Rebind();
        }

        private void SetEntityValue(FoodPackage entity, GridCommandEventArgs e)
        {
            var userControl = (FoodPackageDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.FoodID = txtFoodID.Text;
                entity.FoodDetailID = userControl.FoodDetailID;
                entity.FoodDetailName = userControl.FoodDetailName;
            }
        }

        private FoodPackage FindPackageGrid(string foodId)
        {
            FoodPackageCollection coll = FoodPackages;
            FoodPackage retval = null;
            foreach (FoodPackage rec in coll)
            {
                if (rec.FoodDetailID.Equals(foodId))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        #endregion

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery();
            query.Select(query.ItemID, query.ItemName);
            query.Where
            (query.SRItemType == ItemType.Service, query.IsActive == true,
                query.Or
                (
                    query.ItemID.Like(searchTextContain),
                    query.ItemName.Like(searchTextContain)
                )
            );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboItemID.DataSource = dtb;
            cboItemID.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }
    }
}

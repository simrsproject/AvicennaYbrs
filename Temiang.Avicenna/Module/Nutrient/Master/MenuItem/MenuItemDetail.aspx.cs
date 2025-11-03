using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;


namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class MenuItemDetail : BasePageDetail
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "MenuItemSearch.aspx?ext=" + Request.QueryString["ext"];
            UrlPageList = "MenuItemList.aspx?ext=" + Request.QueryString["ext"];

            ProgramID = Request.QueryString["ext"] == "0" ? AppConstant.Program.MenuItem : AppConstant.Program.MenuExtraItem;
            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                var mvcoll = new MenuVersionCollection();
                mvcoll.Query.Where(mvcoll.Query.IsActive == true);
                if (Request.QueryString["ext"] == "0")
                    mvcoll.Query.Where(mvcoll.Query.IsExtra == false);
                else
                    mvcoll.Query.Where(mvcoll.Query.IsExtra == true);

                mvcoll.Query.OrderBy(mvcoll.Query.VersionID.Descending);
                mvcoll.LoadAll();

                cboVersionID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var item in mvcoll)
                {
                    cboVersionID.Items.Add(new RadComboBoxItem(item.VersionName, item.VersionID));
                }

                ComboBox.PopulateWithInpatientClassTariff(cboClassID);
                StandardReference.Initialize(cboSRMealSet, AppEnum.StandardReference.MealSet, true);
            }
        }

        private void SetEntityValue(MenuItem entity)
        {
            entity.MenuItemID = txtMenuItemID.Text;
            entity.MenuItemName = txtMenuItemName.Text;
            entity.MenuID = cboMenuID.SelectedValue;
            entity.VersionID = cboVersionID.SelectedValue;
            entity.SeqNo = cboSeqNo.SelectedValue;
            entity.ClassID = cboClassID.SelectedValue;
            entity.Notes = txtNotes.Text;
            entity.IsActive = chkIsActive.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Update Key
            MenuItemFoodCollection coll = MenuItemFoods;
            foreach (MenuItemFood item in coll)
            {
                item.MenuItemID = txtMenuItemID.Text;

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
            var que = new MenuItemQuery("a");
            var menu = new MenuQuery("b");
            que.InnerJoin(menu).On(que.MenuID == menu.MenuID);
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.MenuItemID > txtMenuItemID.Text);
                que.OrderBy(que.MenuItemID.Ascending);
            }
            else
            {
                que.Where(que.MenuItemID < txtMenuItemID.Text);
                que.OrderBy(que.MenuItemID.Descending);
            }
            if (Request.QueryString["ext"] == "0")
                que.Where(menu.IsExtra == false);
            else
                que.Where(menu.IsExtra == true);
            
            var entity = new MenuItem();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected void cboMenuID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new MenuQuery("a");
            query.Where(query.Or(query.MenuID == e.Text, query.MenuName.Like(searchTextContain)));
            query.Where(query.IsActive == true);
            if (Request.QueryString["ext"] == "0")
                query.Where(query.IsExtra == false);
            else
                query.Where(query.IsExtra == true);

            query.Select(query.MenuID, query.MenuName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboMenuID.DataSource = dtb;
            cboMenuID.DataBind();
        }

        protected void cboMenuID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["MenuName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["MenuID"].ToString();
        }

        protected void cboSRMealSet_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtSRMealSet.Text = e.Value;
            MenuItemFoods = null;
            grdMenuItemFood.Rebind();
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new MenuItem();
            if (parameters.Length > 0)
            {
                String menuItemId = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(menuItemId);
            }
            else
                entity.LoadByPrimaryKey(txtMenuItemID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var menuItem = (MenuItem)entity;
            txtMenuItemID.Text = menuItem.MenuItemID;
            txtMenuItemName.Text = menuItem.MenuItemName;
            if (!string.IsNullOrEmpty(menuItem.MenuID))
            {
                var menuq = new MenuQuery();
                menuq.Where(menuq.MenuID == menuItem.MenuID);
                cboMenuID.DataSource = menuq.LoadDataTable();
                cboMenuID.DataBind();
                cboMenuID.SelectedValue = menuItem.MenuID;
            }
            else
            {
                cboMenuID.Items.Clear();
                cboMenuID.Text = string.Empty;
            }
            
            cboVersionID.SelectedValue = menuItem.VersionID;
            if (!string.IsNullOrEmpty(menuItem.VersionID))
            {
                ComboBox.PopulateMenuVersionSeqNoList(cboSeqNo, menuItem.VersionID);
                cboSeqNo.SelectedValue = menuItem.SeqNo;
            }
            else
            {
                cboSeqNo.Items.Clear();
                cboSeqNo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboSeqNo.SelectedValue = string.Empty;
                cboSeqNo.Text = string.Empty;
            }
            
            cboClassID.SelectedValue = menuItem.ClassID;
            if (string.IsNullOrEmpty(txtSRMealSet.Text))
                cboSRMealSet.SelectedValue = BusinessObject.Reference.MealSet.Breakfast;
            txtNotes.Text = menuItem.Notes;
            chkIsActive.Checked = menuItem.IsActive ?? false;

            //Display Data Detail
            PopulateGridDetail();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdMenuItemFood, grdMenuItemFood);
            ajax.AddAjaxSetting(cboVersionID, cboVersionID);
            ajax.AddAjaxSetting(cboVersionID, cboSeqNo);
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new MenuItem());
            cboMenuID.Text = string.Empty;
            cboVersionID.SelectedValue = string.Empty;
            cboVersionID.Text = string.Empty;
            cboClassID.SelectedValue = string.Empty;
            cboClassID.Text = string.Empty;
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
            auditLogFilter.PrimaryKeyData = "MenuItemID='" + txtMenuItemID.Text.Trim() + "'";
            auditLogFilter.TableName = "MenuItem";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtMenuItemID.ReadOnly = true;
            txtMenuItemName.ReadOnly = true;
            cboMenuID.Enabled = (newVal == AppEnum.DataMode.New);
            cboVersionID.Enabled = (newVal == AppEnum.DataMode.New);
            cboSeqNo.Enabled = (newVal == AppEnum.DataMode.New);
            cboClassID.Enabled = (newVal == AppEnum.DataMode.New);
            cboSRMealSet.Enabled = newVal == AppEnum.DataMode.Read;
            btnCopyMenuItem.Visible = (newVal == AppEnum.DataMode.Read) && Request.QueryString["ext"] == "0";

            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new MenuItem();
            if (entity.LoadByPrimaryKey(txtMenuItemID.Text))
            {
                entity.MarkAsDeleted();

                var coll = new MenuItemFoodCollection();
                coll.Query.Where(coll.Query.MenuItemID == txtMenuItemID.Text);
                coll.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    MenuItemFoods.Save();
                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            txtMenuItemID.Text = cboMenuID.SelectedValue + "/" + cboVersionID.SelectedValue + "/" +
                                 cboSeqNo.SelectedValue + "/" + cboClassID.SelectedValue;
            txtMenuItemName.Text = cboMenuID.Text + " HARI KE-" + Convert.ToInt16(cboSeqNo.SelectedValue).ToString();
            var e = new MenuItem();
            if (e.LoadByPrimaryKey(txtMenuItemID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            var mv = new MenuVersion();
            mv.LoadByPrimaryKey(cboVersionID.SelectedValue);
            int cycle = Convert.ToInt32(mv.Cycle) + ((mv.IsPlusOneRule ?? false) ? 1 : 0);
            if (Convert.ToInt32(cboSeqNo.SelectedValue) > cycle)
            {
                args.MessageText = "Seq No can't be greater than " + cycle.ToString();
                args.IsCancel = true;
                return;
            }

            var entity = new MenuItem();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(MenuItem entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                MenuItemFoods.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new MenuItem();
            if (entity.LoadByPrimaryKey(txtMenuItemID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion

        #region Record Detail Method Function

        private MenuItemFoodCollection MenuItemFoods
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collMenuItemFood"];
                    if (obj != null)
                        return ((MenuItemFoodCollection)(obj));
                }

                var coll = new MenuItemFoodCollection();
                var query = new MenuItemFoodQuery("a");
                var fQ = new FoodQuery("b");
                var stdQ = new AppStandardReferenceItemQuery("c");
                var std2Q = new AppStandardReferenceItemQuery("d");
                var std3Q = new AppStandardReferenceItemQuery("e");

                string menuItemId = txtMenuItemID.Text;
                string srMealSet = cboSRMealSet.SelectedValue;
                query.Select
                    (
                        query,
                        fQ.FoodName.As("refToFood_FoodName"),
                        stdQ.ItemName.As("refToFood_FoodGroup"),
                        std2Q.ItemName.As("refToFood_FoodGroup2"),
                        std3Q.ItemName.As("refToStdRef_MenuItemFoodGroup")
                    );
                query.InnerJoin(fQ).On(query.FoodID == fQ.FoodID);
                query.InnerJoin(stdQ).On(fQ.SRFoodGroup1 == stdQ.ItemID &
                                         stdQ.StandardReferenceID == AppEnum.StandardReference.FoodGroup1);
                query.LeftJoin(std2Q).On(fQ.SRFoodGroup2 == std2Q.ItemID &
                                        std2Q.StandardReferenceID == AppEnum.StandardReference.FoodGroup2);
                query.LeftJoin(std3Q).On(query.SRMenuItemFoodGroup == std3Q.ItemID &
                                        std3Q.StandardReferenceID == AppEnum.StandardReference.MenuItemFoodGroup);
                query.Where(query.MenuItemID == menuItemId, query.SRMealSet == srMealSet);
                query.OrderBy(fQ.SRFoodGroup1.Ascending, query.FoodID.Ascending);
                coll.Load(query);

                Session["collMenuItemFood"] = coll;
                return coll;
            }
            set { Session["collMenuItemFood"] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdMenuItemFood.Columns[0].Visible = isVisible;
            grdMenuItemFood.Columns[grdMenuItemFood.Columns.Count - 1].Visible = isVisible;

            grdMenuItemFood.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            grdMenuItemFood.Columns.FindByUniqueName("MenuItemFoodGroup").Visible = AppSession.Parameter.IsFoodSelectedByMenuItemFoodGroup;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                MenuItemFoods = null;

            //Perbaharui tampilan dan data
            grdMenuItemFood.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            MenuItemFoods = null; //Reset Record Detail
            grdMenuItemFood.DataSource = MenuItemFoods;
            grdMenuItemFood.DataBind();
        }

        protected void grdMenuItemFood_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdMenuItemFood.DataSource = MenuItemFoods;
        }

        protected void grdMenuItemFood_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            String foodId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][MenuItemFoodMetadata.ColumnNames.FoodID]);
            MenuItemFood entity = FindItemGrid(foodId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdMenuItemFood_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String foodId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][MenuItemFoodMetadata.ColumnNames.FoodID]);
            MenuItemFood entity = FindItemGrid(foodId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdMenuItemFood_InsertCommand(object source, GridCommandEventArgs e)
        {
            MenuItemFood entity = MenuItemFoods.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdMenuItemFood.Rebind();
        }

        private void SetEntityValue(MenuItemFood entity, GridCommandEventArgs e)
        {
            var userControl = (MenuItemFoodDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.MenuItemID = txtMenuItemID.Text;
                entity.SRMealSet = cboSRMealSet.SelectedValue;
                entity.FoodID = userControl.FoodID;
                entity.FoodName = userControl.FoodName;

                var food = new Food();
                food.LoadByPrimaryKey(entity.FoodID);

                var std = new AppStandardReferenceItem();
                std.LoadByPrimaryKey(AppEnum.StandardReference.FoodGroup1.ToString(), food.SRFoodGroup1);
                entity.FoodGroup = std.ItemName;
                
                std = new AppStandardReferenceItem();
                std.LoadByPrimaryKey(AppEnum.StandardReference.FoodGroup2.ToString(), food.SRFoodGroup2);
                entity.FoodType = std.ItemName;

                entity.SRMenuItemFoodGroup = userControl.SRMenuItemFoodGroup;
                entity.MenuItemFoodGroup = userControl.MenuItemFoodGroup;
                entity.IsOptional = userControl.IsOptional;
                entity.IsStandard = userControl.IsStandard;
            }
        }

        private MenuItemFood FindItemGrid(string foodId)
        {
            MenuItemFoodCollection coll = MenuItemFoods;
            MenuItemFood retval = null;
            foreach (MenuItemFood rec in coll)
            {
                if (rec.FoodID.Equals(foodId))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        #endregion

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler source, string argument)
        {
            base.RaisePostBackEvent(source, argument);

            if (string.IsNullOrEmpty(argument) || !(source is RadGrid))
                return;

            if (argument == "rebind")
            {
                Session["collMenuItemFood"] = null;
                grdMenuItemFood.Rebind();
            }
        }

        protected void cboVersionID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComboBox.PopulateMenuVersionSeqNoList(cboSeqNo, e.Value);
        }
    }
}

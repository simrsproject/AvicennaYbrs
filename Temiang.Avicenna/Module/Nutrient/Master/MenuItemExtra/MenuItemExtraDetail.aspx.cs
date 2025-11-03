using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class MenuItemExtraDetail : BasePageDetail
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "MenuItemExtraSearch.aspx";
            UrlPageList = "MenuItemExtraList.aspx";

            ProgramID = AppConstant.Program.MenuExtraItemFood;
            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRMealSet, AppEnum.StandardReference.MealSet);
            }
        }

        private void SetEntityValue(BusinessObject.MenuItemExtra entity)
        {
            entity.SeqNo = txtSeqNo.Text;
            entity.MenuID = cboMenuID.SelectedValue;
            entity.StartingDate = txtStartingDate.SelectedDate;
            entity.SRMealSet = cboSRMealSet.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Update Key
            foreach (MenuItemExtraFood item in MenuItemExtraFoods)
            {
                item.SeqNo = txtSeqNo.Text;

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
            var que = new MenuItemExtraQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.SeqNo > txtSeqNo.Text);
                que.OrderBy(que.SeqNo.Ascending);
            }
            else
            {
                que.Where(que.SeqNo < txtSeqNo.Text);
                que.OrderBy(que.SeqNo.Descending);
            }

            var entity = new MenuItemExtra();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected void cboMenuID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new MenuQuery("a");
            query.Where(query.Or(query.MenuID == e.Text, query.MenuName.Like(searchTextContain)));
            query.Where(query.IsActive == true, query.IsExtra == true);
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
            MenuItemExtraFoods = null;
            grdMenuItemFood.Rebind();
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new MenuItemExtra();
            if (parameters.Length > 0)
            {
                String id = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id);
            }
            else
                entity.LoadByPrimaryKey(txtSeqNo.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var ext = (MenuItemExtra)entity;
            txtSeqNo.Text = ext.SeqNo;
            if (!string.IsNullOrEmpty(ext.MenuID))
            {
                var menuq = new MenuQuery();
                menuq.Where(menuq.MenuID == ext.MenuID);
                cboMenuID.DataSource = menuq.LoadDataTable();
                cboMenuID.DataBind();
                cboMenuID.SelectedValue = ext.MenuID;
            }
            else
            {
                cboMenuID.Items.Clear();
                cboMenuID.Text = string.Empty;
            }
            txtStartingDate.SelectedDate = ext.StartingDate;
            cboSRMealSet.SelectedValue = ext.SRMealSet;

            //Display Data Detail
            PopulateGridDetail();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdMenuItemFood, grdMenuItemFood);
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new MenuItemExtra());

            var coll = new MenuItemExtraCollection();
            coll.LoadAll();
            if (coll.Count == 0)
                txtSeqNo.Text = "0001";
            else
            {
                int seqNo = 0;
                foreach (MenuItemExtra item in coll)
                {
                    if (int.Parse(item.SeqNo) > seqNo)
                        seqNo = int.Parse(item.SeqNo);
                }
                txtSeqNo.Text = string.Format("{0:0000}", seqNo + 1);
            }

            cboMenuID.Text = string.Empty;
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
            auditLogFilter.PrimaryKeyData = "SeqNo='" + txtSeqNo.Text.Trim() + "'";
            auditLogFilter.TableName = "MenuItemExtra";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtSeqNo.Enabled = false;
            cboMenuID.Enabled = (newVal == AppEnum.DataMode.New);
            txtStartingDate.Enabled = (newVal == AppEnum.DataMode.New);
            cboSRMealSet.Enabled = (newVal == AppEnum.DataMode.New);

            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new MenuItemExtra();
            if (entity.LoadByPrimaryKey(txtSeqNo.Text))
            {
                entity.MarkAsDeleted();

                var coll = new MenuItemExtraFoodCollection();
                coll.Query.Where(coll.Query.SeqNo == txtSeqNo.Text);
                coll.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    MenuItemExtraFoods.Save();
                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var coll = new MenuItemExtraCollection();
            coll.LoadAll();
            if (coll.Count == 0)
                txtSeqNo.Text = "0001";
            else
            {
                int seqNo = 0;
                foreach (MenuItemExtra item in coll)
                {
                    if (int.Parse(item.SeqNo) > seqNo)
                        seqNo = int.Parse(item.SeqNo);
                }
                txtSeqNo.Text = string.Format("{0:0000}", seqNo + 1);
            }

            var e = new MenuItemExtra();
            if (e.LoadByPrimaryKey(txtSeqNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            var entity = new MenuItemExtra();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(MenuItemExtra entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                MenuItemExtraFoods.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new MenuItemExtra();
            if (entity.LoadByPrimaryKey(txtSeqNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion

        #region Record Detail Method Function

        private MenuItemExtraFoodCollection MenuItemExtraFoods
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collMenuItemExtraFood"];
                    if (obj != null)
                        return ((MenuItemExtraFoodCollection)(obj));
                }

                var coll = new MenuItemExtraFoodCollection();
                var query = new MenuItemExtraFoodQuery("a");
                var fQ = new FoodQuery("b");
                var stdQ = new AppStandardReferenceItemQuery("c");
                var std2Q = new AppStandardReferenceItemQuery("d");

                query.Select
                    (
                        query,
                        fQ.FoodName.As("refToFood_FoodName"),
                        stdQ.ItemName.As("refToFood_FoodGroup"),
                        std2Q.ItemName.As("refToAppStandardReferenceItem_ItemName")
                    );
                query.InnerJoin(fQ).On(query.FoodID == fQ.FoodID);
                query.InnerJoin(stdQ).On(fQ.SRFoodGroup1 == stdQ.ItemID &
                                         stdQ.StandardReferenceID == AppEnum.StandardReference.FoodGroup1);
                query.InnerJoin(std2Q).On(query.SRDayName == std2Q.ItemID &
                                         std2Q.StandardReferenceID == AppEnum.StandardReference.DayName);
                query.Where(query.SeqNo == txtSeqNo.Text);
                query.OrderBy(query.SRDayName.Ascending, fQ.SRFoodGroup1.Ascending, query.FoodID.Ascending);
                coll.Load(query);

                Session["collMenuItemExtraFood"] = coll;
                return coll;
            }
            set { Session["collMenuItemExtraFood"] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdMenuItemFood.Columns[0].Visible = isVisible;
            grdMenuItemFood.Columns[grdMenuItemFood.Columns.Count - 1].Visible = isVisible;

            grdMenuItemFood.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                MenuItemExtraFoods = null;

            //Perbaharui tampilan dan data
            grdMenuItemFood.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            MenuItemExtraFoods = null; //Reset Record Detail
            grdMenuItemFood.DataSource = MenuItemExtraFoods;
            grdMenuItemFood.DataBind();
        }

        protected void grdMenuItemFood_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdMenuItemFood.DataSource = MenuItemExtraFoods;
        }

        protected void grdMenuItemFood_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            String srDayName = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][MenuItemExtraFoodMetadata.ColumnNames.SRDayName]);
            String foodId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][MenuItemExtraFoodMetadata.ColumnNames.FoodID]);
            MenuItemExtraFood entity = FindItemGrid(srDayName, foodId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdMenuItemFood_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String srDayName = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][MenuItemExtraFoodMetadata.ColumnNames.SRDayName]);
            String foodId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][MenuItemExtraFoodMetadata.ColumnNames.FoodID]);
            MenuItemExtraFood entity = FindItemGrid(srDayName, foodId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdMenuItemFood_InsertCommand(object source, GridCommandEventArgs e)
        {
            MenuItemExtraFood entity = MenuItemExtraFoods.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdMenuItemFood.Rebind();
        }

        private void SetEntityValue(MenuItemExtraFood entity, GridCommandEventArgs e)
        {
            var userControl = (MenuItemExtraFoodDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRDayName = userControl.SRDayName;
                entity.DayName = userControl.DayName;
                entity.FoodID = userControl.FoodID;
                entity.FoodName = userControl.FoodName;

                var food = new Food();
                food.LoadByPrimaryKey(entity.FoodID);

                var std = new AppStandardReferenceItem();
                std.LoadByPrimaryKey(AppEnum.StandardReference.FoodGroup1.ToString(), food.SRFoodGroup1);

                entity.FoodGroup = std.ItemName;
            }
        }

        private MenuItemExtraFood FindItemGrid(string srDayName, string foodId)
        {
            MenuItemExtraFoodCollection coll = MenuItemExtraFoods;
            MenuItemExtraFood retval = null;
            foreach (MenuItemExtraFood rec in coll)
            {
                if (rec.SRDayName.Equals(srDayName) && rec.FoodID.Equals(foodId))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        #endregion
    }
}

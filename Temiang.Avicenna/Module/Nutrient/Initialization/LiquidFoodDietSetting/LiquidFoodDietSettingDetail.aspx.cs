using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class LiquidFoodDietSettingDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "LiquidFoodDietSettingSearch.aspx";
            UrlPageList = "LiquidFoodDietSettingList.aspx";

            ProgramID = AppConstant.Program.LiquidFoodDietSetting;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {

        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Diet());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new LiquidFoodDiet();
            if (!entity.LoadByPrimaryKey(txtDietID.Text))
                entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
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
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Diet();
            if (parameters.Length > 0)
            {
                String id = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id);
            }
            else
            {
                entity.LoadByPrimaryKey(txtDietID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var d = (Diet)entity;
            txtDietID.Text = d.DietID;
            txtDietName.Text = d.DietName;

            var lfd = new LiquidFoodDiet();
            if (lfd.LoadByPrimaryKey(txtDietID.Text))
            {
                ComboBox.LiquidFoodsRequested(cboFoodID, lfd.FoodID);
                cboFoodID.SelectedValue = lfd.FoodID;

                ComboBox.LiquidFoodsRequested(cboChildrenFoodID, lfd.ChildrenFoodID);
                cboChildrenFoodID.SelectedValue = lfd.ChildrenFoodID;
            }
            else
            {
                cboFoodID.Items.Clear();
                cboFoodID.SelectedValue = string.Empty;
                cboFoodID.Text = string.Empty;

                cboChildrenFoodID.Items.Clear();
                cboChildrenFoodID.SelectedValue = string.Empty;
                cboChildrenFoodID.Text = string.Empty;
            }

            PopulateItemGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(LiquidFoodDiet entity)
        {
            entity.DietID = txtDietID.Text;
            entity.FoodID = cboFoodID.SelectedValue;
            entity.ChildrenFoodID = cboChildrenFoodID.SelectedValue;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;

            foreach (var item in LiquidFoodDietTimes)
            {
                item.DietID = txtDietID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(LiquidFoodDiet entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                LiquidFoodDietTimes.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new DietQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.DietID > txtDietID.Text);
                que.OrderBy(que.DietID.Ascending);
            }
            else
            {
                que.Where(que.DietID < txtDietID.Text);
                que.OrderBy(que.DietID.Descending);
            }

            var entity = new Diet();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of Liquid Food Diet Time
        private LiquidFoodDietTimeCollection LiquidFoodDietTimes
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLiquidFoodDietTime" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((LiquidFoodDietTimeCollection)(obj));
                    }
                }

                var coll = new LiquidFoodDietTimeCollection();
                var query = new LiquidFoodDietTimeQuery("a");
                var foodQ = new FoodQuery("b");
                var food2Q = new FoodQuery("c");
                query.Select
                    (
                        query,
                        foodQ.FoodName.As("refToFood_FoodName"),
                        food2Q.FoodName.As("refToFood_ChildrenFoodName")
                    );
                query.InnerJoin(foodQ).On(query.FoodID == foodQ.FoodID);
                query.InnerJoin(food2Q).On(query.ChildrenFoodID == food2Q.FoodID);
                query.Where(query.DietID == txtDietID.Text);
                coll.Load(query);

                Session["collLiquidFoodDietTime" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collLiquidFoodDietTime" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 2].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            LiquidFoodDietTimes = null; //Reset Record Detail
            grdItem.DataSource = LiquidFoodDietTimes; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private LiquidFoodDietTime FindTime(String time)
        {
            LiquidFoodDietTimeCollection coll = LiquidFoodDietTimes;
            LiquidFoodDietTime retEntity = null;
            foreach (LiquidFoodDietTime rec in coll)
            {
                if (rec.Time.Equals(time))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = LiquidFoodDietTimes;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String time =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][LiquidFoodDietTimeMetadata.ColumnNames.Time]);
            LiquidFoodDietTime entity = FindTime(time);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String time =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LiquidFoodDietTimeMetadata.ColumnNames.Time]);
            LiquidFoodDietTime entity = FindTime(time);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            LiquidFoodDietTime entity = LiquidFoodDietTimes.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(LiquidFoodDietTime entity, GridCommandEventArgs e)
        {
            var userControl = (LiquidFoodDietSettingItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.Time = userControl.Time;
                entity.FoodID = userControl.FoodID;
                entity.FoodName = userControl.FoodName;
                entity.ChildrenFoodID = userControl.ChildrenFoodID;
                entity.ChildrenFoodName = userControl.ChildrenFoodName;
            }
        }
        #endregion

        protected void cboFoodID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.LiquidFoodsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboFoodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["FoodName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["FoodID"].ToString();
        }
    }
}

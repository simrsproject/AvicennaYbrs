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
    public partial class LiquidFoodSettingDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "LiquidFoodSettingSearch.aspx";
            UrlPageList = "LiquidFoodSettingList.aspx";

            ProgramID = AppConstant.Program.LiquidFoodSetting;
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
            OnPopulateEntryControl(new AppStandardReference());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //var entity = new AppStandardReference();
            //if (entity.LoadByPrimaryKey(txtStandardReferenceID.Text))
            //{
            //    entity.MarkAsDeleted();
            //    SaveEntity();
            //}
            //else
            //{
            //    args.MessageText = AppConstant.Message.RecordNotExist;
            //}
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            SetEntityValue();
            SaveEntity();
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            SetEntityValue();
            SaveEntity();
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
            //auditLogFilter.PrimaryKeyData = string.Format("StandardReferenceID='{0}'", txtStandardReferenceID.Text.Trim());
            //auditLogFilter.TableName = "AppStandardReference";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AppStandardReference();
            if (parameters.Length > 0)
            {
                String itemId = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(itemId);
            }
            else
            {
                entity.LoadByPrimaryKey(txtStandardReferenceID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var std = (AppStandardReference)entity;
            txtStandardReferenceID.Text = std.StandardReferenceID;
            txtStandardReferenceName.Text = std.StandardReferenceName;

            PopulateItemGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue()
        {
            foreach (var item in AppStandardReferenceItems)
            {
                item.StandardReferenceID = txtStandardReferenceID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity()
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                AppStandardReferenceItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AppStandardReferenceQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.StandardReferenceID > txtStandardReferenceID.Text,
                          que.StandardReferenceID.In("LQ-Unit", "LQ-Class"));
                que.OrderBy(que.StandardReferenceID.Ascending);
            }
            else
            {
                que.Where(que.StandardReferenceID < txtStandardReferenceID.Text,
                          que.StandardReferenceID.In("LQ-Unit", "LQ-Class"));
                que.OrderBy(que.StandardReferenceID.Descending);
            }

            var entity = new AppStandardReference();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of AppStandardReference - Liquid Food
        private AppStandardReferenceItemCollection AppStandardReferenceItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLiquidFoodSetting"];
                    if (obj != null)
                    {
                        return ((AppStandardReferenceItemCollection)(obj));
                    }
                }

                var coll = new AppStandardReferenceItemCollection();
                var query = new AppStandardReferenceItemQuery("a");
                query.Select
                    (
                        query
                    );
                query.Where(query.StandardReferenceID == txtStandardReferenceID.Text);
                coll.Load(query);

                Session["collLiquidFoodSetting"] = coll;
                return coll;
            }
            set
            {
                Session["collLiquidFoodSetting"] = value;
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
            AppStandardReferenceItems = null; //Reset Record Detail
            grdItem.DataSource = AppStandardReferenceItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private AppStandardReferenceItem FindItem(String itemId)
        {
            AppStandardReferenceItemCollection coll = AppStandardReferenceItems;
            AppStandardReferenceItem retEntity = null;
            foreach (AppStandardReferenceItem rec in coll)
            {
                if (rec.ItemID.Equals(itemId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = AppStandardReferenceItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][AppStandardReferenceItemMetadata.ColumnNames.ItemID]);
            AppStandardReferenceItem entity = FindItem(itemId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String itemId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][AppStandardReferenceItemMetadata.ColumnNames.ItemID]);
            AppStandardReferenceItem entity = FindItem(itemId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            AppStandardReferenceItem entity = AppStandardReferenceItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(AppStandardReferenceItem entity, GridCommandEventArgs e)
        {
            var userControl = (LiquidFoodSettingItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Note = userControl.Note;
                entity.ReferenceID = userControl.ReferenceID;
                entity.ReferenceName = userControl.ReferenceName;
                entity.IsActive = userControl.IsActive;
                entity.IsUsedBySystem = userControl.IsUsedBySystem;
            }
        }
        #endregion
    }
}

using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.HR.Master.HRBase.RL4
{
    public partial class RL4Detail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "RL4Search.aspx";
            UrlPageList = "RL4List.aspx";

            ProgramID = AppConstant.Program.RL4; //TODO: Isi ProgramID

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AppStandardReferenceItem());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new AppStandardReferenceItem();
            if (!entity.LoadByPrimaryKey(AppEnum.StandardReference.RL4Type.ToString(), txtRL4TypeID.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            var professionTypeColl = new AppStandardReferenceItemCollection();
            professionTypeColl.Query.Where(professionTypeColl.Query.StandardReferenceID == AppEnum.StandardReference.RL4ProfessionType.ToString(), professionTypeColl.Query.ReferenceID == txtRL4TypeID.Text);
            professionTypeColl.LoadAll();

            var eduLevelColl = new AppStandardReferenceItemCollection();
            eduLevelColl.Query.Where(eduLevelColl.Query.StandardReferenceID == AppEnum.StandardReference.RL4EducationLevel.ToString(), eduLevelColl.Query.CustomField == txtRL4TypeID.Text);
            eduLevelColl.LoadAll();

            var eduMajorColl = new AppStandardReferenceItemCollection();
            eduMajorColl.Query.Where(eduMajorColl.Query.StandardReferenceID == AppEnum.StandardReference.RL4EducationMajor.ToString(), eduMajorColl.Query.CustomField == txtRL4TypeID.Text);
            eduMajorColl.LoadAll();

            using (var trans = new esTransactionScope())
            {
                eduMajorColl.MarkAllAsDeleted();
                eduMajorColl.Save();

                eduLevelColl.MarkAllAsDeleted();
                eduLevelColl.Save();

                professionTypeColl.MarkAllAsDeleted();
                professionTypeColl.Save();

                entity.MarkAsDeleted();
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.RL4Type.ToString(), txtRL4TypeID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new AppStandardReferenceItem();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.RL4Type.ToString(), txtRL4TypeID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
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
            //auditLogFilter.PrimaryKeyData = string.Format("ItemID='{0}'", txtTypeOfLaborID.Text.Trim());
            //auditLogFilter.TableName = "AppStandardReferenceItem";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtRL4TypeID.ReadOnly = (newVal != AppEnum.DataMode.New);
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AppStandardReferenceItem();
            if (parameters.Length > 0)
            {
                var itemId = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(AppEnum.StandardReference.RL4Type.ToString(), itemId);
            }
            else
                entity.LoadByPrimaryKey(AppEnum.StandardReference.RL4Type.ToString(), txtRL4TypeID.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var rl4Type = (AppStandardReferenceItem)entity;

            txtRL4TypeID.Text = rl4Type.ItemID;
            txtRL4TypeName.Text = rl4Type.ItemName;

            PopulateItemGrid();
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
        }
        #endregion

        #region Private Method Standard

        private void SetEntityValue(AppStandardReferenceItem entity)
        {
            entity.StandardReferenceID = AppEnum.StandardReference.RL4Type.ToString();
            entity.ItemID = txtRL4TypeID.Text;
            entity.ItemName = txtRL4TypeName.Text;
            entity.IsActive = true;
            entity.IsUsedBySystem = true;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in RL4ProfessionTypes)
            {
                item.ReferenceID = txtRL4TypeID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(AppStandardReferenceItem entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                RL4ProfessionTypes.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AppStandardReferenceItemQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.StandardReferenceID == AppEnum.StandardReference.RL4Type.ToString(),
                        que.ItemID > txtRL4TypeID.Text
                    );
                que.OrderBy(que.ItemID.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.StandardReferenceID == AppEnum.StandardReference.RL4Type.ToString(),
                        que.ItemID < txtRL4TypeID.Text
                    );
                que.OrderBy(que.ItemID.Descending);
            }

            var entity = new AppStandardReferenceItem();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of RL4ProfessionType

        private AppStandardReferenceItemCollection RL4ProfessionTypes
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collRL4ProfessionType"];
                    if (obj != null)
                    {
                        return ((AppStandardReferenceItemCollection)(obj));
                    }
                }

                var coll = new AppStandardReferenceItemCollection();
                var query = new AppStandardReferenceItemQuery("a");

                query.Select(query);

                query.Where(query.StandardReferenceID == AppEnum.StandardReference.RL4ProfessionType.ToString(), query.ReferenceID == txtRL4TypeID.Text);
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collRL4ProfessionType"] = coll;
                return coll;
            }
            set
            {
                Session["collRL4ProfessionType"] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[1].Visible = !isVisible;
            grdItem.Columns[2].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible
                                                             ? GridCommandItemDisplay.Top
                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            RL4ProfessionTypes = null; //Reset Record Detail
            grdItem.DataSource = RL4ProfessionTypes; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private AppStandardReferenceItem FindItem(String itemId)
        {
            AppStandardReferenceItemCollection coll = RL4ProfessionTypes;
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
            grdItem.DataSource = RL4ProfessionTypes;
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
            AppStandardReferenceItem entity = RL4ProfessionTypes.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(AppStandardReferenceItem entity, GridCommandEventArgs e)
        {
            var userControl = (RL4ProfessionTypeItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.StandardReferenceID = AppEnum.StandardReference.RL4ProfessionType.ToString();
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.ReferenceID = txtRL4TypeID.Text;
                entity.IsActive = true;
                entity.IsUsedBySystem = true;
            }
        }

        #endregion
    }
}
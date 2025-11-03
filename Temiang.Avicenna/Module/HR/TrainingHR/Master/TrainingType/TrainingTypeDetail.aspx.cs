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

namespace Temiang.Avicenna.Module.HR.TrainingHR.Master.TrainingType
{
    public partial class TrainingTypeDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "TrainingTypeSearch.aspx";
            UrlPageList = "TrainingTypeList.aspx";

            ProgramID = AppConstant.Program.EmployeeTrainingType; //TODO: Isi ProgramID

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

            var coll = new AppStandardReferenceItemCollection();
            coll.Query.Where(coll.Query.StandardReferenceID == AppEnum.StandardReference.ActivityType.ToString());
            coll.LoadAll();
            if (coll.Count == 0)
            {
                txtItemID.Text = "01";
            }
            else
            {
                var itemIdMax = (coll.OrderByDescending(c => c.ItemID).Select(c => c.ItemID.Substring(0, 2))).Take(1);
                int itemId = int.Parse(itemIdMax.Single()) + 1;
                txtItemID.Text = string.Format("{0:00}", itemId);
            }

            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.ActivityType.ToString(), txtItemID.Text))
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
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.ActivityType.ToString(), txtItemID.Text))
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
            txtItemID.ReadOnly = true;//!(newVal == AppEnum.DataMode.New);

            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AppStandardReferenceItem();
            if (parameters.Length > 0)
            {
                var itemId = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(AppEnum.StandardReference.ActivityType.ToString(), itemId);
            }
            else
                entity.LoadByPrimaryKey(AppEnum.StandardReference.ActivityType.ToString(), txtItemID.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var type = (AppStandardReferenceItem)entity;

            txtItemID.Text = type.ItemID;
            txtItemName.Text = type.ItemName;
            chkIsActive.Checked = type.IsActive ?? false;

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
            entity.StandardReferenceID = AppEnum.StandardReference.ActivityType.ToString();
            entity.ItemID = txtItemID.Text;
            entity.ItemName = txtItemName.Text;
            entity.IsActive = chkIsActive.Checked;
            entity.IsUsedBySystem = true;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var i in TrainigSubTypes)
            {
                i.LastUpdateByUserID = AppSession.UserLogin.UserID;
                i.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(AppStandardReferenceItem entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                TrainigSubTypes.Save();

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
                        que.StandardReferenceID == AppEnum.StandardReference.ActivityType.ToString(),
                        que.ItemID > txtItemID.Text
                    );
                que.OrderBy(que.ItemID.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.StandardReferenceID == AppEnum.StandardReference.ActivityType.ToString(),
                        que.ItemID < txtItemID.Text
                    );
                que.OrderBy(que.ItemID.Descending);
            }

            var entity = new AppStandardReferenceItem();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of ClinicalAuthorityLevel

        private AppStandardReferenceItemCollection TrainigSubTypes
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collTrainigSubType"];
                    if (obj != null)
                    {
                        return ((AppStandardReferenceItemCollection)(obj));
                    }
                }

                var coll = new AppStandardReferenceItemCollection();
                var query = new AppStandardReferenceItemQuery("a");

                query.Select(query);

                query.Where(query.StandardReferenceID == AppEnum.StandardReference.ActivitySubType.ToString(), query.ReferenceID == txtItemID.Text);
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collTrainigSubType"] = coll;
                return coll;
            }
            set
            {
                Session["collTrainigSubType"] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
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

        private void PopulateItemGrid()
        {
            //Display Data Detail
            TrainigSubTypes = null; //Reset Record Detail
            grdItem.DataSource = TrainigSubTypes; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private AppStandardReferenceItem FindItem(String itemId)
        {
            AppStandardReferenceItemCollection coll = TrainigSubTypes;
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
            grdItem.DataSource = TrainigSubTypes;
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
            AppStandardReferenceItem entity = TrainigSubTypes.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(AppStandardReferenceItem entity, GridCommandEventArgs e)
        {
            var userControl = (TrainingSubTypeDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.StandardReferenceID = AppEnum.StandardReference.ActivitySubType.ToString();
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.ReferenceID = txtItemID.Text;
                entity.IsActive = userControl.IsActive;
                entity.IsUsedBySystem = false;
            }
        }

        #endregion
    }
}
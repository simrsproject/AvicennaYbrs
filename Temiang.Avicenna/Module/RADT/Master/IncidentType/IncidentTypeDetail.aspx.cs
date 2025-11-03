using System;
using System.Collections.Generic;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Linq;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class IncidentTypeDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "IncidentTypeSearch.aspx";
            UrlPageList = "IncidentTypeList.aspx";

            ProgramID = AppConstant.Program.IncidentType;

            if (!IsPostBack)
            {
            }
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
            OnPopulateEntryControl(new AppStandardReferenceItem());
            
            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.IncidentType.ToString(), txtItemID.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.IncidentType.ToString(), txtItemID.Text))
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
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.IncidentType.ToString(), txtItemID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
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
            //auditLogFilter.PrimaryKeyData = string.Format("ItemIDID='{0}'", txtItemID.Text.Trim());
            //auditLogFilter.TableName = "AppStandardReferenceItem";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemIncidentType(newVal);
            txtItemID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AppStandardReferenceItem();
            if (parameters.Length > 0)
            {
                String itemId = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(AppEnum.StandardReference.IncidentType.ToString(), itemId);
            }
            else
            {
                entity.LoadByPrimaryKey(AppEnum.StandardReference.IncidentType.ToString(), txtItemID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var std = (AppStandardReferenceItem)entity;
            txtItemID.Text = std.ItemID;
            txtItemName.Text = std.ItemName;
            txtNotes.Text = std.Note;
            chkIsActive.Checked = std.IsActive ?? false;
            
            PopulateIncidentTypeGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(AppStandardReferenceItem entity)
        {
            entity.StandardReferenceID = AppEnum.StandardReference.IncidentType.ToString();
            entity.ItemID = txtItemID.Text;
            entity.ItemName = txtItemName.Text;
            entity.Note = txtNotes.Text;
            entity.IsActive = chkIsActive.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            foreach (var item in IncidentTypes)
            {
                item.SRIncidentType = txtItemID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(AppStandardReferenceItem entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                IncidentTypes.Save();
                
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
                que.Where(que.ItemID > txtItemID.Text, que.StandardReferenceID == AppEnum.StandardReference.IncidentType.ToString());
                que.OrderBy(que.ItemID.Ascending);
            }
            else
            {
                que.Where(que.ItemID < txtItemID.Text, que.StandardReferenceID == AppEnum.StandardReference.IncidentType.ToString());
                que.OrderBy(que.ItemID.Descending);
            }

            var entity = new AppStandardReferenceItem();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of Incident Type Component
        private IncidentTypeCollection IncidentTypes
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collIncidentType"];
                    if (obj != null)
                    {
                        return ((IncidentTypeCollection)(obj));
                    }
                }

                var coll = new IncidentTypeCollection();
                var query = new IncidentTypeQuery("a");
                query.Select
                    (
                        query
                    );
                query.Where(query.SRIncidentType == txtItemID.Text);
                coll.Load(query);

                Session["collIncidentType"] = coll;
                return coll;
            }
            set
            {
                Session["collIncidentType"] = value;
            }
        }

        private void RefreshCommandItemIncidentType(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdIncidentType.Columns[0].Visible = isVisible;
            grdIncidentType.Columns[grdIncidentType.Columns.Count - 1].Visible = isVisible;
            grdIncidentType.Columns[grdIncidentType.Columns.Count - 2].Visible = isVisible;

            grdIncidentType.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdIncidentType.Rebind();
        }

        private void PopulateIncidentTypeGrid()
        {
            //Display Data Detail
            IncidentTypes = null; //Reset Record Detail
            grdIncidentType.DataSource = IncidentTypes; //Requery
            grdIncidentType.MasterTableView.IsItemInserted = false;
            grdIncidentType.MasterTableView.ClearEditItems();
            grdIncidentType.DataBind();
        }

        private IncidentType FindIncidentType(String compId)
        {
            IncidentTypeCollection coll = IncidentTypes;
            IncidentType retEntity = null;
            foreach (IncidentType rec in coll)
            {
                if (rec.ComponentID.Equals(compId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdIncidentType_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdIncidentType.DataSource = IncidentTypes;
        }

        protected void grdIncidentType_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String compId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][IncidentTypeMetadata.ColumnNames.ComponentID]);
            IncidentType entity = FindIncidentType(compId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdIncidentType_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String compId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][IncidentTypeMetadata.ColumnNames.ComponentID]);
            IncidentType entity = FindIncidentType(compId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdIncidentType_InsertCommand(object source, GridCommandEventArgs e)
        {
            IncidentType entity = IncidentTypes.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdIncidentType.Rebind();
        }

        private void SetEntityValue(IncidentType entity, GridCommandEventArgs e)
        {
            var userControl = (IncidentTypeComponentDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ComponentID = userControl.ComponentID;
                entity.ComponentName = userControl.ComponentName;
            }
        }
        #endregion
    }
}

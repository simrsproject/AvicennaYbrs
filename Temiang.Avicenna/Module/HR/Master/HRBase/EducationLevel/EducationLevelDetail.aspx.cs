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

namespace Temiang.Avicenna.Module.HR.Master.HRBase
{
    public partial class EducationLevelDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "EducationLevelSearch.aspx";
            UrlPageList = "EducationLevelList.aspx";

            ProgramID = AppConstant.Program.EducationLevel; //TODO: Isi ProgramID

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                var query = new RlMasterReportItemQuery("a");
                query.Where(query.RlMasterReportID == Convert.ToInt32(3), query.IsActive == true);
                query.Where(@"<RIGHT(a.RlMasterReportItemNo, 2) = '00'>");

                DataTable dtb = query.LoadDataTable();

                cboRlReportID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (DataRow row in dtb.Rows)
                {
                    cboRlReportID.Items.Add(new RadComboBoxItem(row["RlMasterReportItemCode"].ToString() + " - " + row["RlMasterReportItemName"].ToString(), row["RlMasterReportItemID"].ToString()));
                }
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            //ToolBarMenuSearch.Visible = false;
            //ToolBarMenuAdd.Visible = false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AppStandardReferenceItem());
            cboRlReportID.SelectedValue = string.Empty;
            cboRlReportID.Text = string.Empty;
            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.FieldLabor.ToString(), txtTypeOfLaborID.Text))
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
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.FieldLabor.ToString(), txtTypeOfLaborID.Text))
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
            RefreshCommandItem(newVal);
            txtTypeOfLaborID.ReadOnly = (newVal != AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AppStandardReferenceItem();
            if (parameters.Length > 0)
            {
                var itemId = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(AppEnum.StandardReference.FieldLabor.ToString(), itemId);
            }
            else
                entity.LoadByPrimaryKey(AppEnum.StandardReference.FieldLabor.ToString(), txtTypeOfLaborID.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var labor = (AppStandardReferenceItem)entity;

            txtTypeOfLaborID.Text = labor.ItemID;
            txtTypeOfLaborName.Text = labor.ItemName;
            cboRlReportID.SelectedValue = labor.ReferenceID;
            chkIsActive.Checked = labor.IsActive ?? false;

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
            entity.StandardReferenceID = AppEnum.StandardReference.FieldLabor.ToString();
            entity.ItemID = txtTypeOfLaborID.Text;
            entity.ItemName = txtTypeOfLaborName.Text;
            entity.ReferenceID = cboRlReportID.SelectedValue;
            entity.IsActive = chkIsActive.Checked;
            entity.IsUsedBySystem = true;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in EducationLevels)
            {
                item.Note = txtTypeOfLaborID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(AppStandardReferenceItem entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                EducationLevels.Save();

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
                        que.StandardReferenceID == AppEnum.StandardReference.FieldLabor.ToString(),
                        que.ItemID > txtTypeOfLaborID.Text
                    );
                que.OrderBy(que.ItemID.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.StandardReferenceID == AppEnum.StandardReference.FieldLabor.ToString(),
                        que.ItemID < txtTypeOfLaborID.Text
                    );
                que.OrderBy(que.ItemID.Descending);
            }

            var entity = new AppStandardReferenceItem();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of SterileItemsReceived

        private AppStandardReferenceItemCollection EducationLevels
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEducationLevel"];
                    if (obj != null)
                    {
                        return ((AppStandardReferenceItemCollection)(obj));
                    }
                }

                var coll = new AppStandardReferenceItemCollection();
                var query = new AppStandardReferenceItemQuery("a");
                var rlq = new RlMasterReportItemQuery("b");
                var cfq = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query,
                        @"<b.RlMasterReportItemNo + ' - ' + b.RlMasterReportItemName AS 'refTo_ReferenceName'>",
                        cfq.ItemName.As("refTo_CustomFieldName")
                    );
                query.LeftJoin(rlq).On(query.ReferenceID == rlq.RlMasterReportItemID);
                query.LeftJoin(cfq).On(cfq.StandardReferenceID == AppEnum.StandardReference.EducationGroup.ToString() && query.CustomField == cfq.ItemID);

                query.Where(query.StandardReferenceID == AppEnum.StandardReference.EducationLevel.ToString(), query.Note == txtTypeOfLaborID.Text);
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collEducationLevel"] = coll;
                return coll;
            }
            set
            {
                Session["collEducationLevel"] = value;
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
            EducationLevels = null; //Reset Record Detail
            grdItem.DataSource = EducationLevels; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private AppStandardReferenceItem FindItem(String itemId)
        {
            AppStandardReferenceItemCollection coll = EducationLevels;
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
            grdItem.DataSource = EducationLevels;
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
            AppStandardReferenceItem entity = EducationLevels.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(AppStandardReferenceItem entity, GridCommandEventArgs e)
        {
            var userControl = (EducationLevelItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.StandardReferenceID = AppEnum.StandardReference.EducationLevel.ToString();
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.ReferenceID = userControl.ReferenceID;
                entity.CustomField = userControl.CustomField;

                var rl = new RlMasterReportItem();
                if (rl.LoadByPrimaryKey(entity.ReferenceID.ToInt()))
                    entity.ReferenceName = rl.RlMasterReportItemNo + " - " + rl.RlMasterReportItemName;
                else
                    entity.ReferenceName = string.Empty;

                var cf = new AppStandardReferenceItem();
                if (cf.LoadByPrimaryKey(AppEnum.StandardReference.EducationGroup.ToString(), entity.CustomField))
                    entity.CustomFieldName = cf.ItemName;
                else 
                    entity.CustomFieldName = string.Empty;

                entity.IsActive = userControl.IsActive;
                entity.IsUsedBySystem = true;
            }
        }

        #endregion
    }
}
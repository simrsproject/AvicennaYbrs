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
    public partial class RiskFactorsDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.RiskFactors;

            // Url Search & List
            UrlPageSearch = "RiskFactorsSearch.aspx";
            UrlPageList = "RiskFactorsList.aspx";

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
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.RiskFactorsType.ToString(), txtItemID.Text))
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
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.RiskFactorsType.ToString(), txtItemID.Text))
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
            var entity = new AppStandardReferenceItem();
            if (parameters.Length > 0)
            {
                String itemId = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(AppEnum.StandardReference.RiskFactorsType.ToString(), itemId);
            }
            else
            {
                entity.LoadByPrimaryKey(AppEnum.StandardReference.RiskFactorsType.ToString(), txtItemID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var std = (AppStandardReferenceItem)entity;
            txtItemID.Text = std.ItemID;
            txtItemName.Text = std.ItemName;
            chkIsActive.Checked = std.IsActive ?? false;

            PopulateItemGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(AppStandardReferenceItem entity)
        {
            entity.StandardReferenceID = AppEnum.StandardReference.RiskFactorsType.ToString();
            entity.ItemID = txtItemID.Text;
            entity.ItemName = txtItemName.Text;
            entity.Note = string.Empty;
            entity.ReferenceID = string.Empty;
            entity.IsUsedBySystem = true;
            entity.IsActive = chkIsActive.Checked;
            entity.LastUpdateDateTime = DateTime.Now;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

            foreach (var item in RiskFactorss)
            {
                item.SRRiskFactorsType = txtItemID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(AppStandardReferenceItem entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                RiskFactorss.Save();

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
                que.Where(que.ItemID > txtItemID.Text,
                          que.StandardReferenceID == AppEnum.StandardReference.RiskFactorsType);
                que.OrderBy(que.StandardReferenceID.Ascending);
            }
            else
            {
                que.Where(que.ItemID < txtItemID.Text,
                          que.StandardReferenceID == AppEnum.StandardReference.RiskFactorsType);
                que.OrderBy(que.StandardReferenceID.Descending);
            }

            var entity = new AppStandardReferenceItem();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of Incident Type Component
        private RiskFactorsCollection RiskFactorss
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collRiskFactors"];
                    if (obj != null)
                    {
                        return ((RiskFactorsCollection)(obj));
                    }
                }

                var coll = new RiskFactorsCollection();
                var query = new RiskFactorsQuery("a");
                query.Select
                    (
                        query
                    );
                query.Where(query.SRRiskFactorsType == txtItemID.Text);
                coll.Load(query);

                Session["collRiskFactors"] = coll;
                return coll;
            }
            set
            {
                Session["collRiskFactors"] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            RiskFactorss = null; //Reset Record Detail
            grdItem.DataSource = RiskFactorss; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private RiskFactors FindItem(String id)
        {
            RiskFactorsCollection coll = RiskFactorss;
            RiskFactors retEntity = null;
            foreach (RiskFactors rec in coll)
            {
                if (rec.RiskFactorsID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = RiskFactorss;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String id =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][RiskFactorsMetadata.ColumnNames.RiskFactorsID]);
            RiskFactors entity = FindItem(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String id =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][RiskFactorsMetadata.ColumnNames.RiskFactorsID]);
            RiskFactors entity = FindItem(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            RiskFactors entity = RiskFactorss.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(RiskFactors entity, GridCommandEventArgs e)
        {
            var userControl = (RiskFactorsItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RiskFactorsID = userControl.RiskFactorsID;
                entity.RiskFactorsName = userControl.RiskFactorsName;
            }
        }
        #endregion
    }
}

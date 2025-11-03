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
    public partial class ContributoryFactorsClassificationFrameworkDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ContributoryFactorsClassificationFrameworkSearch.aspx";
            UrlPageList = "ContributoryFactorsClassificationFrameworkList.aspx";

            ProgramID = AppConstant.Program.ContributoryFactorsClassificationFramework;

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
            OnPopulateEntryControl(new ContributoryFactorsClassificationFramework());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ContributoryFactorsClassificationFramework();
            if (entity.LoadByPrimaryKey(txtFactorID.Text))
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
            var entity = new ContributoryFactorsClassificationFramework();
            if (entity.LoadByPrimaryKey(txtFactorID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new ContributoryFactorsClassificationFramework();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ContributoryFactorsClassificationFramework();
            if (entity.LoadByPrimaryKey(txtFactorID.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("FactorID='{0}'", txtFactorID.Text.Trim());
            auditLogFilter.TableName = "ContributoryFactorsClassificationFramework";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemIncidentType(newVal);
            txtFactorID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ContributoryFactorsClassificationFramework();
            if (parameters.Length > 0)
            {
                String factorId = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(factorId);
            }
            else
            {
                entity.LoadByPrimaryKey(txtFactorID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var factor = (ContributoryFactorsClassificationFramework)entity;
            txtFactorID.Text = factor.FactorID;
            txtFactorName.Text = factor.FactorName;

            PopulateDetailGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ContributoryFactorsClassificationFramework entity)
        {
            entity.FactorID = txtFactorID.Text;
            entity.FactorName = txtFactorName.Text;
            
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            foreach (var item in ContributoryFactorsClassificationFrameworkItems)
            {
                item.FactorID = txtFactorID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(ContributoryFactorsClassificationFramework entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ContributoryFactorsClassificationFrameworkItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ContributoryFactorsClassificationFrameworkQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.FactorID > txtFactorID.Text);
                que.OrderBy(que.FactorID.Ascending);
            }
            else
            {
                que.Where(que.FactorID < txtFactorID.Text);
                que.OrderBy(que.FactorID.Descending);
            }

            var entity = new ContributoryFactorsClassificationFramework();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of Incident Type Component
        private ContributoryFactorsClassificationFrameworkItemCollection ContributoryFactorsClassificationFrameworkItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collContributoryFactorsClassificationFrameworkItem"];
                    if (obj != null)
                    {
                        return ((ContributoryFactorsClassificationFrameworkItemCollection)(obj));
                    }
                }

                var coll = new ContributoryFactorsClassificationFrameworkItemCollection();
                var query = new ContributoryFactorsClassificationFrameworkItemQuery("a");
                query.Select
                    (
                        query
                    );
                query.Where(query.FactorID == txtFactorID.Text);
                coll.Load(query);

                Session["collContributoryFactorsClassificationFrameworkItem"] = coll;
                return coll;
            }
            set
            {
                Session["collContributoryFactorsClassificationFrameworkItem"] = value;
            }
        }

        private void RefreshCommandItemIncidentType(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdDetail.Columns[0].Visible = isVisible;
            grdDetail.Columns[grdDetail.Columns.Count - 1].Visible = isVisible;
            grdDetail.Columns[grdDetail.Columns.Count - 2].Visible = isVisible;

            grdDetail.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdDetail.Rebind();
        }

        private void PopulateDetailGrid()
        {
            //Display Data Detail
            ContributoryFactorsClassificationFrameworkItems = null; //Reset Record Detail
            grdDetail.DataSource = ContributoryFactorsClassificationFrameworkItems; //Requery
            grdDetail.MasterTableView.IsItemInserted = false;
            grdDetail.MasterTableView.ClearEditItems();
            grdDetail.DataBind();
        }

        private ContributoryFactorsClassificationFrameworkItem FindItem(String factorItemId)
        {
            ContributoryFactorsClassificationFrameworkItemCollection coll = ContributoryFactorsClassificationFrameworkItems;
            ContributoryFactorsClassificationFrameworkItem retEntity = null;
            foreach (ContributoryFactorsClassificationFrameworkItem rec in coll)
            {
                if (rec.FactorItemID.Equals(factorItemId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = ContributoryFactorsClassificationFrameworkItems;
        }

        protected void grdDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String factorItemId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.FactorItemID]);
            ContributoryFactorsClassificationFrameworkItem entity = FindItem(factorItemId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdDetail_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String factorItemId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.FactorItemID]);
            ContributoryFactorsClassificationFrameworkItem entity = FindItem(factorItemId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdDetail_InsertCommand(object source, GridCommandEventArgs e)
        {
            ContributoryFactorsClassificationFrameworkItem entity = ContributoryFactorsClassificationFrameworkItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdDetail.Rebind();
        }

        private void SetEntityValue(ContributoryFactorsClassificationFrameworkItem entity, GridCommandEventArgs e)
        {
            var userControl = (ContributoryFactorsClassificationFrameworkItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.FactorItemID = userControl.FactorItemID;
                entity.FactorItemName = userControl.FactorItemName;
            }
        }
        #endregion
    }
}

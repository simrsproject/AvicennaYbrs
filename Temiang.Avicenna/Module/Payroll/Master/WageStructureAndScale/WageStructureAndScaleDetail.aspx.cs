using System;
using System.Data;

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class WageStructureAndScaleDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "WageStructureAndScaleSearch.aspx";
            UrlPageList = "WageStructureAndScaleList.aspx";

            ProgramID = AppConstant.Program.WageStructureAndScale;
            this.WindowSearch.Height = 400;
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
            var entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.WageStructureAndScaleType.ToString(), txtItemID.Text))
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
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.WageStructureAndScaleType.ToString(), txtItemID.Text))
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
            //auditLogFilter.PrimaryKeyData = string.Format("ItemID='{0}'", txtItemID.Text.Trim());
            //auditLogFilter.TableName = "AppStandardReferenceItem";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtItemID.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AppStandardReferenceItem();
            if (parameters.Length > 0)
            {
                string id = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(AppEnum.StandardReference.WageStructureAndScaleType.ToString(), id);
            }
            else
            {
                entity.LoadByPrimaryKey(AppEnum.StandardReference.WageStructureAndScaleType.ToString(), txtItemID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var wss = (AppStandardReferenceItem)entity;
            txtItemID.Text = wss.ItemID;
            txtItemName.Text = wss.ItemName;
            txtNotes.Text = wss.Note;
            txtNumericValue.Value = Convert.ToDouble(wss.NumericValue);

            PopulateItemGrid();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(AppStandardReferenceItem entity)
        {
            entity.StandardReferenceID = AppEnum.StandardReference.WageStructureAndScaleType.ToString();
            entity.ItemID = txtItemID.Text;
            entity.ItemName = txtItemName.Text;
            entity.Note = txtNotes.Text;
            entity.NumericValue = Convert.ToDecimal(txtNumericValue.Value);

            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            foreach (AppStandardReferenceItem detil in WageStructureAndScaleTypeItems)
            {
                detil.ReferenceID = txtItemID.Text;

                if (detil.es.IsAdded || detil.es.IsModified)
                {
                    detil.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    detil.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(AppStandardReferenceItem entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                WageStructureAndScaleTypeItems.Save();

                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AppStandardReferenceItemQuery();
            que.es.Top = 1;
            if (isNextRecord)
            {
                que.Where(que.StandardReferenceID == AppEnum.StandardReference.WageStructureAndScaleType.ToString(), que.ItemID > txtItemID.Text);
                que.OrderBy(que.ItemID.Ascending);
            }
            else
            {
                que.Where(que.StandardReferenceID == AppEnum.StandardReference.WageStructureAndScaleType.ToString(), que.ItemID < txtItemID.Text);
                que.OrderBy(que.ItemID.Descending);
            }
            var entity = new AppStandardReferenceItem();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged


        #endregion

        #region ComboBox Function

        #endregion ComboBox Function

        #region Record Detail Method Function WageStructureAndScaleItem

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            grdItem.Rebind();
        }

        private AppStandardReferenceItemCollection WageStructureAndScaleTypeItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collWageStructureAndScaleTypeItem"];
                    if (obj != null)
                    {
                        return ((AppStandardReferenceItemCollection)(obj));
                    }
                }

                var coll = new AppStandardReferenceItemCollection();
                var query = new AppStandardReferenceItemQuery("a");

                query.Select(query);

                query.Where(query.StandardReferenceID == AppEnum.StandardReference.WageStructureAndScaleItem.ToString(), query.ReferenceID == txtItemID.Text);
                query.OrderBy(query.ItemID.Ascending);

                coll.Load(query);
                Session["collWageStructureAndScaleTypeItem"] = coll;
                return coll;
            }
            set { Session["collWageStructureAndScaleTypeItem"] = value; }
        }

        private void PopulateItemGrid()
        {
            WageStructureAndScaleTypeItems = null;
            grdItem.DataSource = WageStructureAndScaleTypeItems;
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = WageStructureAndScaleTypeItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            string id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][AppStandardReferenceItemMetadata.ColumnNames.ItemID]);
            AppStandardReferenceItem entity = FindItem(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            string id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][AppStandardReferenceItemMetadata.ColumnNames.ItemID]);
            AppStandardReferenceItem entity = FindItem(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            AppStandardReferenceItem entity = WageStructureAndScaleTypeItems.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdItem.Rebind();
        }

        private AppStandardReferenceItem FindItem(string id)
        {
            var coll = WageStructureAndScaleTypeItems;
            AppStandardReferenceItem retEntity = null;
            foreach (AppStandardReferenceItem rec in coll)
            {
                if (rec.ItemID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(AppStandardReferenceItem entity, GridCommandEventArgs e)
        {
            var userControl = (WageStructureAndScaleItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.StandardReferenceID = AppEnum.StandardReference.WageStructureAndScaleItem.ToString();
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.ReferenceID = txtItemID.Text;
                entity.IsActive = true;
            }
        }

        #endregion
    }
}
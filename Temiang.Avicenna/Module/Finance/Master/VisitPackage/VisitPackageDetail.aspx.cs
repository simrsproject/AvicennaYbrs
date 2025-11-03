using System;
using System.Collections.Generic;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Linq;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class VisitPackageDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VisitPackage;

            // Url Search & List
            UrlPageSearch = "VisitPackageSearch.aspx";
            UrlPageList = "VisitPackageList.aspx";

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
            OnPopulateEntryControl(new VisitPackage());
            txtVisitPackageID.ReadOnly = true;
            txtVisitPackageID.Text = Helper.GetVisitPackageID();
            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                args.MessageText = "Invalid Service Unit.";
                args.IsCancel = true;
                return;
            }

            txtVisitPackageID.Text = Helper.GetVisitPackageID();

            var entity = new VisitPackage();
            if (entity.LoadByPrimaryKey(txtVisitPackageID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new VisitPackage();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                args.MessageText = "Invalid Service Unit.";
                args.IsCancel = true;
                return;
            }

            var entity = new VisitPackage();
            if (entity.LoadByPrimaryKey(txtVisitPackageID.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("VisitPackageID='{0}'", txtVisitPackageID.Text.Trim());
            auditLogFilter.TableName = "VisitPackage";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtVisitPackageID.ReadOnly = true;
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new VisitPackage();
            if (parameters.Length > 0)
            {
                String id = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id);
            }
            else
            {
                entity.LoadByPrimaryKey(txtVisitPackageID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var vp = (VisitPackage)entity;
            txtVisitPackageID.Text = vp.VisitPackageID;
            txtVisitPackageName.Text = vp.VisitPackageName;
            if (!string.IsNullOrEmpty(vp.ServiceUnitID))
            {
                var query = new ServiceUnitQuery();
                query.Where(query.ServiceUnitID == vp.ServiceUnitID);

                cboServiceUnitID.DataSource = query.LoadDataTable();
                cboServiceUnitID.DataBind();
                cboServiceUnitID.SelectedValue = vp.ServiceUnitID;
            }
            else
            {
                cboServiceUnitID.Items.Clear();
                cboServiceUnitID.SelectedValue = string.Empty;
                cboServiceUnitID.Text = string.Empty;
            }
            chkIsActive.Checked = vp.IsActive ?? false;

            PopulateItemGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(VisitPackage entity)
        {
            entity.VisitPackageID = txtVisitPackageID.Text;
            entity.VisitPackageName = txtVisitPackageName.Text;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.IsActive = chkIsActive.Checked;
            entity.LastUpdateDateTime = DateTime.Now;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

            foreach (var item in VisitPackageItems)
            {
                item.VisitPackageID = txtVisitPackageID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(VisitPackage entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                VisitPackageItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new VisitPackageQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.VisitPackageID > txtVisitPackageID.Text);
                que.OrderBy(que.VisitPackageID.Ascending);
            }
            else
            {
                que.Where(que.VisitPackageID < txtVisitPackageID.Text);
                que.OrderBy(que.VisitPackageID.Descending);
            }

            var entity = new VisitPackage();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of VisitPackageItem
        private VisitPackageItemCollection VisitPackageItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collVisitPackageItem"];
                    if (obj != null)
                    {
                        return ((VisitPackageItemCollection)(obj));
                    }
                }

                var coll = new VisitPackageItemCollection();
                var query = new VisitPackageItemQuery("a");
                var itemq = new ItemQuery("b");
                query.InnerJoin(itemq).On(itemq.ItemID == query.ItemID);
                query.Select
                    (
                        query, 
                        itemq.ItemName.As("refToItem_ItemName")
                    );
                query.Where(query.VisitPackageID == txtVisitPackageID.Text);
                coll.Load(query);

                Session["collVisitPackageItem"] = coll;
                return coll;
            }
            set
            {
                Session["collVisitPackageItem"] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;
            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            VisitPackageItems = null; //Reset Record Detail
            grdItem.DataSource = VisitPackageItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private VisitPackageItem FindItem(String id)
        {
            VisitPackageItemCollection coll = VisitPackageItems;
            VisitPackageItem retEntity = null;
            foreach (VisitPackageItem rec in coll)
            {
                if (rec.ItemID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = VisitPackageItems;
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String id =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][VisitPackageItemMetadata.ColumnNames.ItemID]);
            VisitPackageItem entity = FindItem(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][VisitPackageItemMetadata.ColumnNames.ItemID]);
            VisitPackageItem entity = FindItem(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            VisitPackageItem entity = VisitPackageItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(VisitPackageItem entity, GridCommandEventArgs e)
        {
            var userControl = (VisitPackageItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Qty = userControl.Qty;
            }
        }
        #endregion

        #region Combobox
        protected void cboServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery("a");
            var tcode = new ServiceUnitTransactionCodeQuery("b");

            query.Select(query.ServiceUnitID, query.ServiceUnitName);

            query.InnerJoin(tcode).On(tcode.ServiceUnitID == query.ServiceUnitID);
            query.Where(
                query.IsActive == true,
                query.ServiceUnitName.Like(searchTextContain), 
                tcode.SRTransactionCode.In("001", "003"));

            query.OrderBy(query.ServiceUnitName.Ascending);
            query.es.Top = 20;
            query.es.Distinct = true;

            cboServiceUnitID.DataSource = query.LoadDataTable();
            cboServiceUnitID.DataBind();
        }
        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }
        #endregion
    }
}
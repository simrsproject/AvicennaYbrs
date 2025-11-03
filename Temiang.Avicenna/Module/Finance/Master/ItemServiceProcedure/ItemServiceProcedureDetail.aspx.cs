using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ItemServiceProcedureDetail : BasePageDetail
    {
        #region Page Event
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ItemServiceProcedureSearch.aspx";
            UrlPageList = "ItemServiceProcedureList.aspx";

            ProgramID = AppConstant.Program.ItemServiceProcedure;

            if (!IsPostBack)
            {
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        private void SetEntityValue(AppStandardReferenceItem entity)
        {
            entity.StandardReferenceID = AppEnum.StandardReference.Procedure.ToString();
            entity.ItemID = txtItemID.Text;
            entity.ItemName = txtItemName.Text;
            entity.IsActive = chkIsActive.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            ItemServiceProcedureCollection collProc = ItemServiceProcedures;
            foreach (ItemServiceProcedure proc in collProc)
            {
                proc.SRProcedure = txtItemID.Text;

                //Last Update Status
                if (proc.es.IsAdded || proc.es.IsModified)
                {
                    proc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    proc.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AppStandardReferenceItemQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.StandardReferenceID == AppEnum.StandardReference.Procedure.ToString(), que.ItemID > txtItemID.Text);
                que.OrderBy(que.ItemID.Ascending);
            }
            else
            {
                que.Where(que.StandardReferenceID == AppEnum.StandardReference.Procedure.ToString(), que.ItemID < txtItemID.Text);
                que.OrderBy(que.ItemID.Descending);
            }
            AppStandardReferenceItem entity = new AppStandardReferenceItem();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            AppStandardReferenceItem entity = new AppStandardReferenceItem();
            if (parameters.Length > 0)
            {
                String id = (String)parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(AppEnum.StandardReference.Procedure.ToString(), id);
            }
            else
                entity.LoadByPrimaryKey(AppEnum.StandardReference.Procedure.ToString(), txtItemID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            AppStandardReferenceItem proc = (AppStandardReferenceItem)entity;
            txtItemID.Text = proc.ItemID;
            txtItemName.Text = proc.ItemName;
            chkIsActive.Checked = proc.IsActive ?? false;

            //Display Data Detail
            PopulateGridDetail();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AppStandardReferenceItem());
            chkIsActive.Checked = true;
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

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtItemID.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            AppStandardReferenceItem entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.Procedure.ToString(), txtItemID.Text))
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

        private void SaveEntity(AppStandardReferenceItem entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ItemServiceProcedures.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            AppStandardReferenceItem entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.Procedure.ToString(), txtItemID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion

        #region Record Detail Method Function

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private ItemServiceProcedureCollection ItemServiceProcedures
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemServiceProcedure"];
                    if (obj != null)
                        return ((ItemServiceProcedureCollection)(obj));
                }

                ItemServiceProcedureCollection coll = new ItemServiceProcedureCollection();
                ItemServiceProcedureQuery query = new ItemServiceProcedureQuery("a");
                ItemQuery itmq = new ItemQuery("b");

                query.Select(query, itmq.ItemName.As("refToItem_ItemName"));
                query.InnerJoin(itmq).On(itmq.ItemID == query.ItemID);

                query.Where(query.SRProcedure == txtItemID.Text);
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collItemServiceProcedure"] = coll;
                return coll;
            }
            set
            {
                Session["collItemServiceProcedure"] = value;
            }
        }

        private void PopulateGridDetail()
        {
            ItemServiceProcedures = null;
            grdItem.DataSource = ItemServiceProcedures;
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = ItemServiceProcedures;
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemServiceProcedureMetadata.ColumnNames.ItemID]);
            ItemServiceProcedure entity = FindItem(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemServiceProcedure entity = ItemServiceProcedures.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private ItemServiceProcedure FindItem(String id)
        {
            ItemServiceProcedureCollection coll = ItemServiceProcedures;
            ItemServiceProcedure retEntity = null;
            foreach (ItemServiceProcedure rec in coll)
            {
                if (rec.ItemID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(ItemServiceProcedure entity, GridCommandEventArgs e)
        {
            var userControl = (ItemServiceProcedureItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRProcedure = txtItemID.Text;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                
                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        #endregion
    }
}
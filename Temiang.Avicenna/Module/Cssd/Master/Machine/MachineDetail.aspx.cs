using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Cssd.Master
{
    public partial class MachineDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "MachineSearch.aspx";
            UrlPageList = "MachineList.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.CssdMachine;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdCssdMachineItem, grdCssdMachineItem);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new CssdMachine());
            txtStartUsingDate.SelectedDate = DateTime.Now;
            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new CssdMachine();
            if (entity.LoadByPrimaryKey(txtMachineID.Text))
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
            var entity = new CssdMachine();
            if (entity.LoadByPrimaryKey(txtMachineID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new CssdMachine();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new CssdMachine();
            if (entity.LoadByPrimaryKey(txtMachineID.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("MachineID='{0}'", txtMachineID.Text.Trim());
            auditLogFilter.TableName = "CssdMachine";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtMachineID.Enabled = (newVal == AppEnum.DataMode.New);

            RefreshCommandItemMachine(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new CssdMachine();
            if (parameters.Length > 0)
            {
                String id = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id);
            }
            else
            {
                entity.LoadByPrimaryKey(txtMachineID.Text);
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var m = (CssdMachine)entity;
            txtMachineID.Text = m.MachineID;
            txtMachineName.Text = m.MachineName;
            txtStartUsingDate.SelectedDate = m.StartUsingDate;
            txtVolume.Value = Convert.ToDouble(m.Volume);
            txtNotes.Text = m.Notes;
            chkIsActive.Checked = m.IsActive ?? false;

            PopulateGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(CssdMachine entity)
        {
            entity.MachineID = txtMachineID.Text;
            entity.MachineName = txtMachineName.Text;
            entity.StartUsingDate = txtStartUsingDate.SelectedDate;
            entity.Volume = Convert.ToDecimal(txtVolume.Value);
            entity.Notes = txtNotes.Text;
            entity.IsActive = chkIsActive.Checked;
           
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            foreach (CssdMachineItem item in CssdMachineItems)
            {
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(CssdMachine entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                CssdMachineItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new CssdMachineQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.MachineID > txtMachineID.Text);
                que.OrderBy(que.MachineID.Ascending);
            }
            else
            {
                que.Where(que.MachineID < txtMachineID.Text);
                que.OrderBy(que.MachineID.Descending);
            }
            var entity = new CssdMachine();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged
        #endregion

        #region Record Detail Method Function Machine

        private void RefreshCommandItemMachine(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdCssdMachineItem.Columns[0].Visible = isVisible;
            grdCssdMachineItem.Columns[grdCssdMachineItem.Columns.Count - 1].Visible = isVisible;

            grdCssdMachineItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdCssdMachineItem.Rebind();
        }

        private CssdMachineItemCollection CssdMachineItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCssdMachineItem"];
                    if (obj != null)
                    {
                        return ((CssdMachineItemCollection)(obj));
                    }
                }

                var coll = new CssdMachineItemCollection();
                var query = new CssdMachineItemQuery("a");
                var stdQuery = new AppStandardReferenceItemQuery("b");

                query.Select
                    (
                        query,
                        stdQuery.ItemName.As("refToAppStandardReferenceItem_CssdProcessType")
                    );
                query.InnerJoin(stdQuery).On(query.SRCssdProcessType == stdQuery.ItemID &&
                                             stdQuery.StandardReferenceID == AppEnum.StandardReference.CssdProcessType);
                query.Where(query.MachineID == txtMachineID.Text);
                query.OrderBy
                    (
                        query.SRCssdProcessType.Ascending
                    );
                coll.Load(query);

                Session["collCssdMachineItem"] = coll;
                return coll;
            }
            set { Session["collCssdMachineItem"] = value; }
        }

        private void PopulateGrid()
        {
            //Display Data Detail
            CssdMachineItems = null; //Reset Record Detail
            grdCssdMachineItem.DataSource = CssdMachineItems; //Requery
            grdCssdMachineItem.MasterTableView.IsItemInserted = false;
            grdCssdMachineItem.MasterTableView.ClearEditItems();
            grdCssdMachineItem.DataBind();
        }

        protected void grdCssdMachineItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdCssdMachineItem.DataSource = CssdMachineItems;
        }

        protected void grdCssdMachineItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CssdMachineItemMetadata.ColumnNames.SRCssdProcessType]);

            CssdMachineItem entity = FindItem(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdCssdMachineItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CssdMachineItemMetadata.ColumnNames.SRCssdProcessType]);

            CssdMachineItem entity = FindItem(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdCssdMachineItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            CssdMachineItem entity = CssdMachineItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdCssdMachineItem.Rebind();
        }

        private CssdMachineItem FindItem(String id)
        {
            CssdMachineItemCollection coll = CssdMachineItems;
            return coll.FirstOrDefault(rec => rec.SRCssdProcessType.Equals(id));
        }

        private void SetEntityValue(CssdMachineItem entity, GridCommandEventArgs e)
        {
            var userControl = (MachineItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.MachineID = txtMachineID.Text;
                entity.SRCssdProcessType = userControl.SRCssdProcessType;
                entity.CssdProcessType = userControl.CssdProcessType;
                entity.Duration = Convert.ToInt16(userControl.Duration);
            }
        }

        #endregion
    }
}

using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Laundry.Master.WashingMachine
{
    public partial class WashingMachineDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "WashingMachineSearch.aspx";
            UrlPageList = "WashingMachineList.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.WashingMachine;
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
            OnPopulateEntryControl(new LaundryWashingMachine());
            txtStartUsingDate.SelectedDate = DateTime.Now;
            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //cek
            var process = new LaunderedProcessCollection();
            process.Query.Where(process.Query.MachineID == txtMachineID.Text);
            process.LoadAll();
            if (process.Count > 0)
            {
                args.MessageText = "Machine ID already used.";
                args.IsCancel = true;
                return;
            }

            var entity = new LaundryWashingMachine();
            if (entity.LoadByPrimaryKey(txtMachineID.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new LaundryWashingMachine();
            if (entity.LoadByPrimaryKey(txtMachineID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new LaundryWashingMachine();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new LaundryWashingMachine();
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
            auditLogFilter.TableName = "LaundryWashingMachine";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtMachineID.ReadOnly = !(newVal == AppEnum.DataMode.New);
            RefreshCommandItemGrid(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new LaundryWashingMachine();
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
            var m = (LaundryWashingMachine)entity;
            txtMachineID.Text = m.MachineID;
            txtMachineName.Text = m.MachineName;
            txtStartUsingDate.SelectedDate = m.StartUsingDate;
            txtVolume.Value = Convert.ToDouble(m.Volume);
            txtNotes.Text = m.Notes;
            chkIsActive.Checked = m.IsActive ?? false;

            PopulateItemGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(LaundryWashingMachine entity)
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

            foreach (LaundryWashingMachineProgram item in LaundryWashingMachinePrograms)
            {
                item.MachineID = txtMachineID.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
        }

        private void SaveEntity(LaundryWashingMachine entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                LaundryWashingMachinePrograms.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new LaundryWashingMachineQuery();
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
            var entity = new LaundryWashingMachine();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged
        #endregion

        #region Record Detail Method Function - LaundryWashingMachineProgram
        private void RefreshCommandItemGrid(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private LaundryWashingMachineProgramCollection LaundryWashingMachinePrograms
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLaundryWashingMachineProgram" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((LaundryWashingMachineProgramCollection)(obj));
                    }
                }

                var coll = new LaundryWashingMachineProgramCollection();
                var query = new LaundryWashingMachineProgramQuery("a");
                var unit = new AppStandardReferenceItemQuery("b");

                query.Where(query.MachineID == txtMachineID.Text);
                query.Select(
                    query.SelectAllExcept(), 
                    unit.ItemName.As("refToAppStdRef_LaundryProgramName")
                    );
                query.InnerJoin(unit).On(unit.StandardReferenceID == "LaundryProgram" && unit.ItemID == query.SRLaundryProgram);
                coll.Load(query);

                Session["collLaundryWashingMachineProgram" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collLaundryWashingMachineProgram" + Request.UserHostName] = value; }
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            LaundryWashingMachinePrograms = null; //Reset Record Detail
            grdItem.DataSource = LaundryWashingMachinePrograms; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = LaundryWashingMachinePrograms;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][LaundryWashingMachineProgramMetadata.ColumnNames.SRLaundryProgram]);
            LaundryWashingMachineProgram entity = FindItem(itemId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LaundryWashingMachineProgramMetadata.ColumnNames.SRLaundryProgram]);
            LaundryWashingMachineProgram entity = FindItem(itemId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            LaundryWashingMachineProgram entity = LaundryWashingMachinePrograms.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private LaundryWashingMachineProgram FindItem(String itemId)
        {
            var coll = LaundryWashingMachinePrograms;
            LaundryWashingMachineProgram retEntity = null;
            foreach (LaundryWashingMachineProgram rec in coll)
            {
                if (rec.SRLaundryProgram.Equals(itemId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(LaundryWashingMachineProgram entity, GridCommandEventArgs e)
        {
            var userControl = (WashingMachineProgramDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRLaundryProgram = userControl.SRLaundryProgram;
                entity.LaundryProgramName = userControl.LaundryProgramName;
            }
        }
        #endregion

    }
}
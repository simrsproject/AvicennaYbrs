using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Laundry.Master
{
    public partial class WashingProgramTypeDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "WashingProgramTypeSearch.aspx";
            UrlPageList = "WashingProgramTypeList.aspx";

            ProgramID = AppConstant.Program.LaundryWashingProgramType;

            WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRLaundryProcessType, AppEnum.StandardReference.LaundryProcessType);
                StandardReference.InitializeIncludeSpace(cboSRLaundryProgram, AppEnum.StandardReference.LaundryProgram);
                StandardReference.InitializeIncludeSpace(cboSRLaundryType, AppEnum.StandardReference.LaundryType);
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new LaundryWashingProgramType());
            txtLaundryProgramTypeID.Text = "0";

            cboSRLaundryProcessType.SelectedValue = string.Empty;
            cboSRLaundryProcessType.Text = string.Empty;
            cboSRLaundryProcessType.SelectedValue = string.Empty;
            cboSRLaundryProcessType.Text = string.Empty;
            cboSRLaundryType.SelectedValue = string.Empty;
            cboSRLaundryType.Text = string.Empty;
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new LaundryWashingProgramType();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtLaundryProgramTypeID.Text)))
            {
                entity.MarkAsDeleted();
                LaundryWashingProgramTypeItemConsumptions.MarkAllAsDeleted();

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
            var coll = new LaundryWashingProgramTypeCollection();
            coll.Query.Where(coll.Query.SRLaundryProcessType == cboSRLaundryProcessType.SelectedValue,
                coll.Query.SRLaundryProgram == cboSRLaundryProgram.SelectedValue,
                coll.Query.SRLaundryType == cboSRLaundryType.SelectedValue);
            coll.LoadAll();

            if (coll.Count == 0)
            {
                var entity = new LaundryWashingProgramType();

                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new LaundryWashingProgramType();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtLaundryProgramTypeID.Text)))
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

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("LaundryProgramTypeID='{0}'", txtLaundryProgramTypeID.Text);
            auditLogFilter.TableName = "LaundryWashingProgramType";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
        }


        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            cboSRLaundryProcessType.Enabled = (newVal == AppEnum.DataMode.New);
            cboSRLaundryProgram.Enabled = (newVal == AppEnum.DataMode.New);
            cboSRLaundryType.Enabled = (newVal == AppEnum.DataMode.New);

            RefreshCommandItemGrid(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new LaundryWashingProgramType();
            if (parameters.Length > 0)
            {
                string id = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(id));

                txtLaundryProgramTypeID.Text = entity.LaundryProgramTypeID.ToString();
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtLaundryProgramTypeID.Text));
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var e = (LaundryWashingProgramType)entity;

            txtLaundryProgramTypeID.Text = e.LaundryProgramTypeID.ToString();
            cboSRLaundryProcessType.SelectedValue = e.SRLaundryProcessType;
            cboSRLaundryProgram.SelectedValue = e.SRLaundryProgram;
            cboSRLaundryType.SelectedValue = e.SRLaundryType;
            txtWeight.Value = Convert.ToDouble(e.Weight);

            PopulateItemGrid();
        }

        private void SetEntityValue(LaundryWashingProgramType entity)
        {
            if (entity.es.IsModified)
                entity.LaundryProgramTypeID = Convert.ToInt32(txtLaundryProgramTypeID.Text);
            entity.SRLaundryProcessType = cboSRLaundryProcessType.SelectedValue;
            entity.SRLaundryProgram = cboSRLaundryProgram.SelectedValue;
            entity.SRLaundryType = cboSRLaundryType.SelectedValue;
            entity.Weight = Convert.ToDecimal(txtWeight.Value);

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (LaundryWashingProgramTypeItemConsumption item in LaundryWashingProgramTypeItemConsumptions)
            {
                item.SRLaundryProcessType = cboSRLaundryProcessType.SelectedValue;
                item.SRLaundryProgram = cboSRLaundryProgram.SelectedValue;
                item.SRLaundryType = cboSRLaundryType.SelectedValue;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
        }

        private void SaveEntity(LaundryWashingProgramType entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                LaundryWashingProgramTypeItemConsumptions.Save();
                try
                {
                    txtLaundryProgramTypeID.Text = entity.LaundryProgramTypeID.ToString();
                }
                catch
                { }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new LaundryWashingProgramTypeQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.LaundryProgramTypeID > Convert.ToInt32(txtLaundryProgramTypeID.Text));
                que.OrderBy(que.LaundryProgramTypeID.Ascending);
            }
            else
            {
                que.Where(que.LaundryProgramTypeID < Convert.ToInt32(txtLaundryProgramTypeID.Text));
                que.OrderBy(que.LaundryProgramTypeID.Descending);
            }
            var entity = new LaundryWashingProgramType();
            entity.Load(que);

            txtLaundryProgramTypeID.Text = entity.LaundryProgramTypeID.ToString();

            OnPopulateEntryControl(entity);
        }

        #region Record Detail Method Function - LaundryWashingProgramTypeItemConsumption
        private void RefreshCommandItemGrid(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemConsumption.Columns[0].Visible = isVisible;
            grdItemConsumption.Columns[grdItemConsumption.Columns.Count - 1].Visible = isVisible;

            grdItemConsumption.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdItemConsumption.Rebind();
        }

        private LaundryWashingProgramTypeItemConsumptionCollection LaundryWashingProgramTypeItemConsumptions
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLaundryWashingProgramTypeItemConsumption" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((LaundryWashingProgramTypeItemConsumptionCollection)(obj));
                    }
                }

                var coll = new LaundryWashingProgramTypeItemConsumptionCollection();
                var query = new LaundryWashingProgramTypeItemConsumptionQuery("a");
                var item = new ItemQuery("b");
                var unit = new AppStandardReferenceItemQuery("c");

                query.Where(
                    query.SRLaundryProcessType == cboSRLaundryProcessType.SelectedValue, 
                    query.SRLaundryProgram == cboSRLaundryProgram.SelectedValue, 
                    query.SRLaundryType == cboSRLaundryType.SelectedValue
                    );
                query.Select(
                    query.SelectAllExcept(),
                    item.ItemName.As("refToItem_ItemName"),
                    unit.ItemName.As("refToAppStandardReferenceItem_ItemUnit")
                    );
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(unit).On(unit.StandardReferenceID == "ItemUnit" && unit.ItemID == query.SRItemUnit);
                coll.Load(query);

                Session["collLaundryWashingProgramTypeItemConsumption" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collLaundryWashingProgramTypeItemConsumption" + Request.UserHostName] = value; }
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            LaundryWashingProgramTypeItemConsumptions = null; //Reset Record Detail
            grdItemConsumption.DataSource = LaundryWashingProgramTypeItemConsumptions; //Requery
            grdItemConsumption.MasterTableView.IsItemInserted = false;
            grdItemConsumption.MasterTableView.ClearEditItems();
            grdItemConsumption.DataBind();
        }

        protected void grdItemConsumption_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemConsumption.DataSource = LaundryWashingProgramTypeItemConsumptions;
        }

        protected void grdItemConsumption_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.ItemID]);
            LaundryWashingProgramTypeItemConsumption entity = FindItemNonInfectious(itemId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItemConsumption_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.ItemID]);
            LaundryWashingProgramTypeItemConsumption entity = FindItemNonInfectious(itemId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItemConsumption_InsertCommand(object source, GridCommandEventArgs e)
        {
            LaundryWashingProgramTypeItemConsumption entity = LaundryWashingProgramTypeItemConsumptions.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemConsumption.Rebind();
        }

        private LaundryWashingProgramTypeItemConsumption FindItemNonInfectious(String itemId)
        {
            var coll = LaundryWashingProgramTypeItemConsumptions;
            LaundryWashingProgramTypeItemConsumption retEntity = null;
            foreach (LaundryWashingProgramTypeItemConsumption rec in coll)
            {
                if (rec.ItemID.Equals(itemId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(LaundryWashingProgramTypeItemConsumption entity, GridCommandEventArgs e)
        {
            var userControl = (WashingProgramTypeItemConsumption)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Qty = userControl.Qty;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ItemUnitName = userControl.ItemUnitName;
            }
        }
        #endregion
    }
}
using System;
using System.Linq;
using System.Data;
using System.Web.UI;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.Module.Payroll.PayrollInfo;

namespace Temiang.Avicenna.Module.Payroll.TaxRegulation
{
    public partial class TERMonthlyDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "TERMonthlySearch.aspx";
            UrlPageList = "TERMonthlyList.aspx";

            ProgramID = AppConstant.Program.TERMonthly; //TODO: Isi ProgramID
            txtTERMonthlyID.Text = "0";

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRTaxStatus, AppEnum.StandardReference.TaxStatus, "TAX");
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
            OnPopulateEntryControl(new TERMonthly());

            txtTERMonthlyID.Text = "0";
            txtValidFrom.SelectedDate = DateTime.Now;
            cboSRTaxStatus.SelectedValue = string.Empty;
            cboSRTaxStatus.Text = string.Empty;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new TERMonthly();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtTERMonthlyID.Text)))
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
            var entity = new TERMonthly();

            entity.Query.es.Top = 1;
            entity.Query.Where(entity.Query.TERMonthlyID != txtTERMonthlyID.Value.ToInt(), 
                entity.Query.ValidFrom == txtValidFrom.SelectedDate,
                entity.Query.SRTaxStatus == cboSRTaxStatus.SelectedValue);
            if (entity.Query.Load())
            {
                args.MessageText = "TER Monthly already exist";
                args.IsCancel = true;
                return;
            }

            entity = new TERMonthly();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new TERMonthly();
            entity.Query.es.Top = 1;
            entity.Query.Where(entity.Query.TERMonthlyID != txtTERMonthlyID.Value.ToInt(),
                 entity.Query.ValidFrom == txtValidFrom.SelectedDate,
                 entity.Query.SRTaxStatus == cboSRTaxStatus.SelectedValue);
            if (entity.Query.Load())
            {
                args.MessageText = "TER Monthly already exist";
                args.IsCancel = true;
                return;
            }

            entity = new TERMonthly();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtTERMonthlyID.Text)))
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
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("TERMonthlyID='{0}'", txtTERMonthlyID.Text.Trim());
            auditLogFilter.TableName = "TERMonthly";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtTERMonthlyID.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            TERMonthly entity = new TERMonthly();
            if (parameters.Length > 0)
            {
                string id = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(id));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtTERMonthlyID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            TERMonthly ter = (TERMonthly)entity;
            txtTERMonthlyID.Value = Convert.ToDouble(ter.TERMonthlyID);
            txtValidFrom.SelectedDate = ter.ValidFrom;
            if (!string.IsNullOrEmpty(ter.SRTaxStatus))
                cboSRTaxStatus.SelectedValue = ter.SRTaxStatus;
            else
            {
                cboSRTaxStatus.SelectedValue = string.Empty;
                cboSRTaxStatus.Text = string.Empty;
            }

            //Display Data Detail
            PopulateGrid();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(TERMonthly entity)
        {
            entity.ValidFrom = txtValidFrom.SelectedDate;
            entity.SRTaxStatus = cboSRTaxStatus.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(TERMonthly entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                foreach (TERMonthlyItem item in TERMonthlyItems)
                {
                    item.TERMonthlyID = entity.TERMonthlyID;

                    if (item.es.IsAdded || item.es.IsModified)
                    {
                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        item.LastUpdateDateTime = DateTime.Now;
                    }
                }

                TERMonthlyItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();

                txtTERMonthlyID.Text = entity.TERMonthlyID.ToString();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new TERMonthlyQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TERMonthlyID > txtTERMonthlyID.Text.ToInt());
                que.OrderBy(que.TERMonthlyID.Ascending);
            }
            else
            {
                que.Where(que.TERMonthlyID < txtTERMonthlyID.Text.ToInt());
                que.OrderBy(que.TERMonthlyID.Descending);
            }
            var entity = new TERMonthly();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Record Detail Method Function TERMonthlyItem
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

        private TERMonthlyItemCollection TERMonthlyItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collTERMonthlyItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((TERMonthlyItemCollection)(obj));
                    }
                }

                var coll = new TERMonthlyItemCollection();

                var query = new TERMonthlyItemQuery("a");

                query.Select
                    (
                       query
                    );

                query.Where(query.TERMonthlyID == txtTERMonthlyID.Text.ToInt());
                query.OrderBy(query.LowerLimit.Ascending); 

                coll.Load(query);
                Session["collTERMonthlyItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collTERMonthlyItem" + Request.UserHostName] = value; }
        }

        private void PopulateGrid()
        {
            //Display Data Detail
            TERMonthlyItems = null; //Reset Record Detail
            grdItem.DataSource = TERMonthlyItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = TERMonthlyItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][TERMonthlyItemMetadata.ColumnNames.TERMonthlyItemID]);
            TERMonthlyItem entity = FindItem(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][TERMonthlyItemMetadata.ColumnNames.TERMonthlyItemID]);
            TERMonthlyItem entity = FindItem(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            TERMonthlyItem entity = TERMonthlyItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }
        private TERMonthlyItem FindItem(Int32 id)
        {
            TERMonthlyItemCollection coll = TERMonthlyItems;
            TERMonthlyItem retEntity = null;
            foreach (TERMonthlyItem rec in coll)
            {
                if (rec.TERMonthlyItemID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(TERMonthlyItem entity, GridCommandEventArgs e)
        {
            var userControl = (TERMonthlyItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.LowerLimit = userControl.LowerLimit;
                entity.UpperLimit = userControl.UpperLimit;
                entity.TaxRate = userControl.TaxRate;
            }
        }

        #endregion
    }
}
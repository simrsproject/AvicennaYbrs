using System;
using System.Data;

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class WageStructureAndScalePointsDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "WageStructureAndScalePointsSearch.aspx";
            UrlPageList = "WageStructureAndScalePointsList.aspx";

            ProgramID = AppConstant.Program.WageStructureAndScalePoints;
            this.WindowSearch.Height = 400;

            txtWageStructureAndScaleID.Text = "-1";

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSRWageStructureAndScaleType, AppEnum.StandardReference.WageStructureAndScaleType, false);
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
            OnPopulateEntryControl(new WageStructureAndScale());

            txtWageStructureAndScaleID.Text = "0";
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new WageStructureAndScale();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtWageStructureAndScaleID.Text)))
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
            var entity = new WageStructureAndScale();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new WageStructureAndScale();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtWageStructureAndScaleID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("WageStructureAndScaleID='{0}'", txtWageStructureAndScaleID.Text.Trim());
            auditLogFilter.TableName = "WageStructureAndScale";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtWageStructureAndScaleID.Enabled = (newVal == AppEnum.DataMode.New);
            cboSRWageStructureAndScaleType.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new WageStructureAndScale();
            if (parameters.Length > 0)
            {
                string WageStructureAndScaleId = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(WageStructureAndScaleId));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtWageStructureAndScaleID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var wss = (WageStructureAndScale)entity;
            txtWageStructureAndScaleID.Value = Convert.ToDouble(wss.WageStructureAndScaleID);
            cboSRWageStructureAndScaleType.SelectedValue = wss.SRWageStructureAndScaleType;
            txtWageStructureAndScaleCode.Text = wss.WageStructureAndScaleCode;
            txtWageStructureAndScaleName.Text = wss.WageStructureAndScaleName;

            PopulateItemGrid();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(WageStructureAndScale entity)
        {
            entity.SRWageStructureAndScaleType = cboSRWageStructureAndScaleType.SelectedValue;
            entity.WageStructureAndScaleCode = txtWageStructureAndScaleCode.Text;
            entity.WageStructureAndScaleName = txtWageStructureAndScaleName.Text;

            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(WageStructureAndScale entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                foreach (WageStructureAndScaleItem detil in WageStructureAndScaleItems)
                {
                    detil.WageStructureAndScaleID = entity.WageStructureAndScaleID;

                    if (detil.es.IsAdded || detil.es.IsModified)
                    {
                        detil.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        detil.LastUpdateDateTime = DateTime.Now;
                    }
                }

                WageStructureAndScaleItems.Save();

                trans.Complete();

                txtWageStructureAndScaleID.Text = entity.WageStructureAndScaleID.ToString();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new WageStructureAndScaleQuery();
            que.es.Top = 1;
            if (isNextRecord)
            {
                que.Where(que.SRWageStructureAndScaleType == cboSRWageStructureAndScaleType.SelectedValue, que.WageStructureAndScaleCode > txtWageStructureAndScaleCode.Text);
                que.OrderBy(que.WageStructureAndScaleCode.Ascending);
            }
            else
            {
                que.Where(que.SRWageStructureAndScaleType == cboSRWageStructureAndScaleType.SelectedValue, que.WageStructureAndScaleCode < txtWageStructureAndScaleCode.Text);
                que.OrderBy(que.WageStructureAndScaleCode.Descending);
            }
            var entity = new WageStructureAndScale();
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

        private WageStructureAndScaleItemCollection WageStructureAndScaleItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collWageStructureAndScaleItem"];
                    if (obj != null)
                    {
                        return ((WageStructureAndScaleItemCollection)(obj));
                    }
                }

                var coll = new WageStructureAndScaleItemCollection();
                var query = new WageStructureAndScaleItemQuery("a");
                var std = new AppStandardReferenceItemQuery("b");
                query.InnerJoin(std).On(std.StandardReferenceID == AppEnum.StandardReference.WageStructureAndScaleItem.ToString() && std.ItemID == query.SRWageStructureAndScaleItem);

                query.Select(
                    query, 
                    std.ItemName.As("refToStdRefItem_WageStructureAndScaleItem"));

                query.Where(query.WageStructureAndScaleID == txtWageStructureAndScaleID.Text);
                query.OrderBy(query.SRWageStructureAndScaleItem.Ascending);

                coll.Load(query);
                Session["collWageStructureAndScaleItem"] = coll;
                return coll;
            }
            set { Session["collWageStructureAndScaleItem"] = value; }
        }

        private void PopulateItemGrid()
        {
            WageStructureAndScaleItems = null;
            grdItem.DataSource = WageStructureAndScaleItems;
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = WageStructureAndScaleItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][WageStructureAndScaleItemMetadata.ColumnNames.WageStructureAndScaleItemID]);
            WageStructureAndScaleItem entity = FindItem(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][WageStructureAndScaleItemMetadata.ColumnNames.WageStructureAndScaleItemID]);
            WageStructureAndScaleItem entity = FindItem(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            WageStructureAndScaleItem entity = WageStructureAndScaleItems.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdItem.Rebind();
        }

        private WageStructureAndScaleItem FindItem(Int64 id)
        {
            var coll = WageStructureAndScaleItems;
            WageStructureAndScaleItem retEntity = null;
            foreach (WageStructureAndScaleItem rec in coll)
            {
                if (rec.WageStructureAndScaleItemID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(WageStructureAndScaleItem entity, GridCommandEventArgs e)
        {
            var userControl = (WageStructureAndScalePointsItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.WageStructureAndScaleItemID = userControl.WageStructureAndScaleItemID;
                entity.SRWageStructureAndScaleItem = userControl.SRWageStructureAndScaleItem;
                entity.WageStructureAndScaleItemName = userControl.WageStructureAndScaleItemName;
                entity.Points = userControl.Points;
            }
        }

        #endregion
    }
}
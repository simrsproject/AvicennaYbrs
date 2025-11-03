using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ItemConditionRuleDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ItemConditionRuleSearch.aspx";
            UrlPageList = "ItemConditionRuleList.aspx";

            ProgramID = AppConstant.Program.ItemConditionRule;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                tabStrip.Tabs[1].Visible = AppSession.Parameter.IsPromoPackageActivated;
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
            OnPopulateEntryControl(new ItemConditionRule());
            chkIsValueInPercent.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new ItemConditionRule();
            if (entity.LoadByPrimaryKey(txtItemID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            var itemConditionRuleServiceUnits = new ItemConditionRuleServiceUnitCollection();
            entity = new ItemConditionRule();
            entity.AddNew();
            SetEntityValue(entity, itemConditionRuleServiceUnits);
            SaveEntity(entity, itemConditionRuleServiceUnits);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ItemConditionRule();
            if (entity.LoadByPrimaryKey(txtItemID.Text))
            {
                var itemConditionRuleServiceUnits = new ItemConditionRuleServiceUnitCollection();
                SetEntityValue(entity, itemConditionRuleServiceUnits);
                SaveEntity(entity, itemConditionRuleServiceUnits);
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

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("ItemConditionRuleID='{0}'", txtItemID.Text.Trim());
            auditLogFilter.TableName = "ItemConditionRule";
        }
        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtItemID.ReadOnly = (newVal != AppEnum.DataMode.New);

            RefreshCommandItemGrid(newVal);

            grdServiceUnit.Columns[0].Visible = (newVal != AppEnum.DataMode.Read);
            RefreshGridServiceUnit();
            
            //Refresh Selection Check
            switch (newVal)
            {
                case AppEnum.DataMode.New:
                    foreach (GridDataItem dataItem in grdServiceUnit.MasterTableView.Items)
                        dataItem.Selected = false;

                    break;
                case AppEnum.DataMode.Edit:
                    foreach (GridDataItem dataItem in grdServiceUnit.MasterTableView.Items)
                        dataItem.Selected = (bool)dataItem.GetDataKeyValue("IsSelect");

                    break;
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ItemConditionRule();
            if (parameters.Length > 0)
            {
                String itemId = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(itemId);
            }
            else
            {
                entity.LoadByPrimaryKey(txtItemID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var rule = (ItemConditionRule)entity;
            txtItemID.Text = rule.ItemConditionRuleID;
            txtItemName.Text = rule.ItemConditionRuleName;
            txtStartingDate.SelectedDate = rule.StartingDate;
            txtEndingDate.SelectedDate = rule.EndingDate;
            rblSRItemConditionRuleType.SelectedIndex = rule.SRItemConditionRuleType == "MRG" ? 0 : 1;
            txtAmountValue.Value = Convert.ToDouble(rule.AmountValue);
            chkIsValueInPercent.Checked = rule.IsValueInPercent ?? false;

            PopulateItemConditionRuleItemGrid();
            //Refresh Detail
            if (IsPostBack)
            {
                RefreshGridServiceUnit();
            }
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(ItemConditionRule entity, ItemConditionRuleServiceUnitCollection itemConditionRuleServiceUnits)
        {
            entity.ItemConditionRuleID = txtItemID.Text;
            entity.ItemConditionRuleName = txtItemName.Text;
            entity.StartingDate = txtStartingDate.SelectedDate;
            entity.EndingDate = txtEndingDate.SelectedDate;
            entity.SRItemConditionRuleType = rblSRItemConditionRuleType.SelectedIndex == 0 ? "MRG" : "DISC";
            entity.AmountValue = Convert.ToDecimal(txtAmountValue.Value);
            entity.IsValueInPercent = chkIsValueInPercent.Checked;
            
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            foreach (var item in ItemConditionRuleItems)
            {
                item.ItemConditionRuleID = txtItemID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }

            itemConditionRuleServiceUnits.Query.Where(itemConditionRuleServiceUnits.Query.ItemConditionRuleID == txtItemID.Text);
            itemConditionRuleServiceUnits.LoadAll();

            foreach (GridDataItem dataItem in grdServiceUnit.MasterTableView.Items)
            {
                ItemConditionRuleServiceUnit item;
                string serviceUnitId = dataItem.GetDataKeyValue("ServiceUnitID").ToString();
                item = itemConditionRuleServiceUnits.FindByPrimaryKey(txtItemID.Text, serviceUnitId);
                if (dataItem.Selected)
                {
                    if (item == null)
                    {
                        item = itemConditionRuleServiceUnits.AddNew();
                        item.ItemConditionRuleID = txtItemID.Text;
                        item.ServiceUnitID = serviceUnitId;
                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        item.LastUpdateDateTime = DateTime.Now;
                    }
                }
                else
                    if (item != null)
                        item.MarkAsDeleted();
            }
        }

        private void SaveEntity(ItemConditionRule entity, ItemConditionRuleServiceUnitCollection itemConditionRuleServiceUnits)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                itemConditionRuleServiceUnits.Save();
                ItemConditionRuleItems.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ItemConditionRuleQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ItemConditionRuleID > txtItemID.Text);
                que.OrderBy(que.ItemConditionRuleID.Ascending);
            }
            else
            {
                que.Where(que.ItemConditionRuleID < txtItemID.Text);
                que.OrderBy(que.ItemConditionRuleID.Descending);
            }
            var entity = new ItemConditionRule();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged
        
        #endregion

        #region Record Detail Method Function

        #region ItemConditionRuleServiceUnit
        protected void grdServiceUnit_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdServiceUnit.DataSource = DetailServiceUnit;
        }

        private DataTable DetailServiceUnit
        {
            get
            {
                object obj = this.Session["ItemConditionRuleServiceUnit"];
                if (obj != null)
                    return ((DataTable)(obj));

                var coll = new ItemConditionRuleServiceUnitCollection();
                DataTable dtb = DataModeCurrent == AppEnum.DataMode.Read
                                    ? coll.GetInnerJoinWRule(txtItemID.Text)
                                    : coll.GetFullJoinWRule(txtItemID.Text);

                Session["ItemConditionRuleServiceUnit"] = dtb;
                return dtb;
            }
        }

        private void RefreshGridServiceUnit()
        {
            Session["ItemConditionRuleServiceUnit"] = null;
            grdServiceUnit.Rebind();
        }
        #endregion

        #region ItemConditionRuleItem
        private ItemConditionRuleItemCollection ItemConditionRuleItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemConditionRuleItem"];
                    if (obj != null)
                    {
                        return ((ItemConditionRuleItemCollection)(obj));
                    }
                }

                var coll = new ItemConditionRuleItemCollection();
                var query = new ItemConditionRuleItemQuery("a");
                var item = new ItemQuery("b");

                query.Select(query, item.ItemName.As("refToItem_ItemName"));
                query.InnerJoin(item).On(item.ItemID == query.ItemID);
                query.Where(query.ItemConditionRuleID == txtItemID.Text);
                coll.Load(query);

                Session["collItemConditionRuleItem"] = coll;
                return coll;
            }
            set { Session["collItemConditionRuleItem"] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemConditionRuleItemGrid()
        {
            //Display Data Detail
            ItemConditionRuleItems = null; //Reset Record Detail
            grdItem.DataSource = ItemConditionRuleItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = ItemConditionRuleItems;
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemConditionRuleItemMetadata.ColumnNames.ItemID]);
            ItemConditionRuleItem entity = FindItem(itemId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemConditionRuleItem entity = ItemConditionRuleItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private ItemConditionRuleItem FindItem(String itemId)
        {
            ItemConditionRuleItemCollection coll = ItemConditionRuleItems;
            ItemConditionRuleItem retEntity = null;
            foreach (ItemConditionRuleItem rec in coll)
            {
                if (rec.ItemID.Equals(itemId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(ItemConditionRuleItem entity, GridCommandEventArgs e)
        {
            var userControl = (ItemConditionRuleItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
            }
        }
        #endregion

        #endregion
    }
}

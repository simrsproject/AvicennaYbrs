using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;

namespace Temiang.Avicenna.Module.AssetManagement.Master
{
    public partial class AssetGroupDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "AssetGroupSearch.aspx";
            UrlPageList = "AssetGroupList.aspx";

            ProgramID = AppConstant.Program.ASSET_GROUP;

            if (!IsPostBack)
            {
                
            }
        }

        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AssetGroup());
            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var assets = new AssetCollection();
            assets.Query.Where(assets.Query.AssetGroupID == txtAssetGroupId.Text);
            assets.LoadAll();
            if (assets.Count > 0)
            {
                args.MessageText = AppConstant.Message.RecordUsedByOther;
                args.IsCancel = true;
                return;
            }

            var entity = new AssetGroup();
            entity.LoadByPrimaryKey(this.txtAssetGroupId.Text);
            entity.MarkAsDeleted();

            AssetSubGroups.MarkAllAsDeleted();

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                AssetSubGroups.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new AssetGroup();
            if (entity.LoadByPrimaryKey(txtAssetGroupId.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            int chartOfAccountIdForAsset = 0;
            int.TryParse(cboChartOfAccountIdForAsset.SelectedValue, out chartOfAccountIdForAsset);
            int chartOfAccountIdForDepreciation = 0;
            int.TryParse(cboChartOfAccountIdForDepreciation.SelectedValue, out chartOfAccountIdForDepreciation);
            int chartOfAccountIdForCost = 0;
            int.TryParse(cboChartOfAccountIdForCost.SelectedValue, out chartOfAccountIdForCost);
            int chartOfAccountIdForCostDestruction = 0;
            int.TryParse(cboChartOfAccountIdForCostDestruction.SelectedValue, out chartOfAccountIdForCostDestruction);

            //if (chartOfAccountIdForAsset == 0)
            //{
            //    args.MessageText = "Please choose Chart Of Account (Asset) for this Asset Group";
            //    args.IsCancel = true;
            //    return;
            //}

            if (chartOfAccountIdForDepreciation == 0)
            {
                args.MessageText = "Please choose Chart Of Account (Depreciation) for this Asset Group";
                args.IsCancel = true;
                return;
            }

            if (chartOfAccountIdForCost == 0)
            {
                args.MessageText = "Please choose Chart Of Account (Cost of Depreciation) for this Asset Group";
                args.IsCancel = true;
                return;
            }

            if (chartOfAccountIdForCostDestruction == 0)
            {
                args.MessageText = "Please choose Chart Of Account (Cost of Destruction) for this Asset Group";
                args.IsCancel = true;
                return;
            }

            entity = new AssetGroup();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new AssetGroup();
            if (entity.LoadByPrimaryKey(txtAssetGroupId.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
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
            auditLogFilter.PrimaryKeyData = string.Format("AssetGroupId='{0}'", txtAssetGroupId.Text.Trim());
            auditLogFilter.TableName = "AssetGroup";
        }
        #endregion

        #region Private Method Standard

        private void SetEntityValue(AssetGroup entity)
        {
            entity.GroupName = this.txtGroupName.Text;
            entity.Description = this.txtDescription.Text;
            entity.Initial = txtInitial.Text;
            entity.IsActive = chkIsActive.Checked;

            int chartOfAccountIdForAsset = 0;
            int subLedgerIdForAsset = 0;
            int.TryParse(cboChartOfAccountIdForAsset.SelectedValue, out chartOfAccountIdForAsset);
            int.TryParse(cboSubledgerIdForAsset.SelectedValue, out subLedgerIdForAsset);

            int chartOfAccountIdForDepreciation = 0;
            int subLedgerIdForDepreciation = 0;
            int.TryParse(cboChartOfAccountIdForDepreciation.SelectedValue, out chartOfAccountIdForDepreciation);
            int.TryParse(cboSubledgerIdForDepreciation.SelectedValue, out subLedgerIdForDepreciation);

            int chartOfAccountIdForCost = 0;
            int subLedgerIdForCost = 0;
            int.TryParse(cboChartOfAccountIdForCost.SelectedValue, out chartOfAccountIdForCost);
            int.TryParse(cboSubledgerIdForCost.SelectedValue, out subLedgerIdForCost);

            int chartOfAccountIdForCostDestruction = 0;
            int subLedgerIdForCostDestruction = 0;
            int.TryParse(cboChartOfAccountIdForCostDestruction.SelectedValue, out chartOfAccountIdForCostDestruction);
            int.TryParse(cboSubledgerIdForCostDestruction.SelectedValue, out subLedgerIdForCostDestruction);

            entity.AssetAccountId = chartOfAccountIdForAsset;
            entity.AssetSubLedgerId = subLedgerIdForAsset;
            entity.AssetAccumulationAccountId = chartOfAccountIdForDepreciation;
            entity.AssetAccumulationSubLedgerId = subLedgerIdForDepreciation;
            entity.AssetCostAccountId = chartOfAccountIdForCost;
            entity.AssetCostSubLedgerId = subLedgerIdForCost;
            entity.AssetCostDestructionAccountId = chartOfAccountIdForCostDestruction;
            entity.AssetCostDestructionSubLedgerId = subLedgerIdForCostDestruction;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.AssetGroupId = this.txtAssetGroupId.Text;
                entity.LastUpdateByUserId = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in AssetSubGroups)
            {
                item.AssetGroupId = txtAssetGroupId.Text;
            }
        }

        private void SaveEntity(AssetGroup entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                AssetSubGroups.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AssetGroupQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.AssetGroupId > this.txtAssetGroupId.Text);
                que.OrderBy(que.AssetGroupId.Ascending);
            }
            else
            {
                que.Where(que.AssetGroupId < this.txtAssetGroupId.Text);
                que.OrderBy(que.AssetGroupId.Descending);
            }

            var entity = new AssetGroup();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            this.txtAssetGroupId.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AssetGroup();
            if (parameters.Length > 0)
            {
                var id = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id);
            }
            else
            {
                entity.LoadByPrimaryKey(this.txtAssetGroupId.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var e = (AssetGroup)entity;
            this.txtAssetGroupId.Text = e.AssetGroupId;
            this.txtGroupName.Text = e.GroupName;
            this.txtDescription.Text = e.Description;
            txtInitial.Text = e.Initial;
            this.chkIsActive.Checked = e.IsActive ?? false;
            
            SelectComboBox(e.AssetAccountId, e.AssetSubLedgerId, this.cboChartOfAccountIdForAsset, this.cboSubledgerIdForAsset);
            SelectComboBox(e.AssetAccumulationAccountId, e.AssetAccumulationSubLedgerId, this.cboChartOfAccountIdForDepreciation, this.cboSubledgerIdForDepreciation);
            SelectComboBox(e.AssetCostAccountId, e.AssetCostSubLedgerId, this.cboChartOfAccountIdForCost, this.cboSubledgerIdForCost);
            SelectComboBox(e.AssetCostDestructionAccountId, e.AssetCostDestructionSubLedgerId, this.cboChartOfAccountIdForCostDestruction, this.cboSubledgerIdForCostDestruction);

            PopulateItemGrid();
        }
        #endregion

        #region Record Detail Method Function of AssetSubGroup

        private AssetSubGroupCollection AssetSubGroups
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collAssetSubGroup"];
                    if (obj != null)
                    {
                        return ((AssetSubGroupCollection)(obj));
                    }
                }

                var coll = new AssetSubGroupCollection();
                var query = new AssetSubGroupQuery("a");

                query.Select
                    (
                        query
                    );
                query.Where(query.AssetGroupId == txtAssetGroupId.Text);
                query.OrderBy(query.AssetSubGroupId.Ascending);
                coll.Load(query);

                Session["collAssetSubGroup"] = coll;
                return coll;
            }
            set
            {
                Session["collAssetSubGroup"] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            AssetSubGroups = null; //Reset Record Detail
            grdItem.DataSource = AssetSubGroups; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private AssetSubGroup FindItem(String id)
        {
            AssetSubGroupCollection coll = AssetSubGroups;
            AssetSubGroup retEntity = null;
            foreach (AssetSubGroup rec in coll)
            {
                if (rec.AssetSubGroupId.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = AssetSubGroups;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String id =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][AssetSubGroupMetadata.ColumnNames.AssetSubGroupId]);
            AssetSubGroup entity = FindItem(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String id =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][AssetSubGroupMetadata.ColumnNames.AssetSubGroupId]);
            AssetSubGroup entity = FindItem(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            AssetSubGroup entity = AssetSubGroups.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(AssetSubGroup entity, GridCommandEventArgs e)
        {
            var userControl = (AssetSubGroupDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.AssetSubGroupId = userControl.AssetSubGroupId;
                entity.AssetSubGroupName = userControl.AssetSubGroupName;
                entity.Initial = userControl.Initial;
                entity.IsActive = userControl.IsActive;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        #endregion

        #region ComboBox
        private void SelectComboBox(int? chartOfAccountId, int? subLedgerId, RadComboBox cboChartOfAccountId, RadComboBox cboSubledgerId)
        {
            if (chartOfAccountId.HasValue && chartOfAccountId > 0)
            {
                var coaQuery = new ChartOfAccountsQuery();
                coaQuery.Select(coaQuery.ChartOfAccountId, coaQuery.ChartOfAccountCode, coaQuery.ChartOfAccountName);
                coaQuery.Where(coaQuery.ChartOfAccountId == chartOfAccountId);
                
                var coaTable = coaQuery.LoadDataTable();
                cboChartOfAccountId.DataSource = coaTable;
                cboChartOfAccountId.DataBind();
                cboChartOfAccountId.SelectedValue = chartOfAccountId.ToString();

                if (subLedgerId.HasValue && subLedgerId > 0)
                {
                    var ledgerQuery = new SubLedgersQuery();
                    ledgerQuery.Select(ledgerQuery.SubLedgerId, ledgerQuery.SubLedgerName, ledgerQuery.Description);
                    ledgerQuery.Where(ledgerQuery.SubLedgerId == subLedgerId);
                    
                    var ledgerTable = ledgerQuery.LoadDataTable();
                    cboSubledgerId.DataSource = ledgerTable;
                    cboSubledgerId.DataBind();
                    cboSubledgerId.SelectedValue = subLedgerId.ToString();
                }
                else
                {
                    cboSubledgerId.Items.Clear();
                    cboSubledgerId.Text = string.Empty;
                }
            }
            else
            {
                cboChartOfAccountId.Items.Clear();
                cboSubledgerId.Items.Clear();
                cboChartOfAccountId.Text = string.Empty;
                cboSubledgerId.Text = string.Empty;
            }
        }

        #region Assets
        protected void cboChartOfAccountIdForAsset_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchText),
                                query.ChartOfAccountName.Like(searchText)
                            ), query.IsDetail == true, query.IsActive == true
                        );
            query.es.Top = 20;
            var tbl = query.LoadDataTable();
            cboChartOfAccountIdForAsset.DataSource = tbl;
            cboChartOfAccountIdForAsset.DataBind();
        }

        protected void cboChartOfAccountIdForAsset_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + @" - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        protected void cboSubledgerIdForAsset_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupId;
            if (cboChartOfAccountIdForAsset.SelectedValue == string.Empty)
            {
                groupId = 0;
            }
            else
            {
                var coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdForAsset.SelectedValue));
                groupId = coa.SubLedgerId ?? 0;
            }
            string searchText = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupId);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchText),
                                query.Description.Like(searchText)
                            )
                        );
            query.es.Top = 20;
            var tbl = query.LoadDataTable();
            cboSubledgerIdForAsset.DataSource = tbl;
            cboSubledgerIdForAsset.DataBind();
        }

        protected void cboSubledgerIdForAsset_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + @" - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        protected void cboChartOfAccountIdForAsset_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdForAsset.Items.Clear();
            cboSubledgerIdForAsset.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdForAsset.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdForAsset.Items.Clear();
                cboChartOfAccountIdForAsset.Text = string.Empty;
                return;
            }
        }
        #endregion

        #region Depreciation
        protected void cboChartOfAccountIdForDepreciation_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchText),
                                query.ChartOfAccountName.Like(searchText)
                            ), query.IsDetail == true, query.IsActive == true
                        );
            query.es.Top = 20;
            var tbl = query.LoadDataTable();
            cboChartOfAccountIdForDepreciation.DataSource = tbl;
            cboChartOfAccountIdForDepreciation.DataBind();
        }

        protected void cboChartOfAccountIdForDepreciation_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + @" - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        protected void cboSubledgerIdForDepreciation_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupId;
            if (cboChartOfAccountIdForDepreciation.SelectedValue == string.Empty)
            {
                groupId = 0;
            }
            else
            {
                var coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdForDepreciation.SelectedValue));
                groupId = coa.SubLedgerId ?? 0;
            }
            string searchText = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupId);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchText),
                                query.Description.Like(searchText)
                            )
                        );
            query.es.Top = 20;
            var tbl = query.LoadDataTable();
            cboSubledgerIdForDepreciation.DataSource = tbl;
            cboSubledgerIdForDepreciation.DataBind();
        }

        protected void cboSubledgerIdForDepreciation_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + @" - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        protected void cboChartOfAccountIdForDepreciation_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdForDepreciation.Items.Clear();
            cboSubledgerIdForDepreciation.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdForDepreciation.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdForDepreciation.Items.Clear();
                cboChartOfAccountIdForDepreciation.Text = string.Empty;
                return;
            }
        }
        #endregion

        #region Cost
        protected void cboChartOfAccountIdForCost_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchText),
                                query.ChartOfAccountName.Like(searchText)
                            ), query.IsDetail == true, query.IsActive == true
                        );
            query.es.Top = 20;
            var tbl = query.LoadDataTable();
            cboChartOfAccountIdForCost.DataSource = tbl;
            cboChartOfAccountIdForCost.DataBind();
        }

        protected void cboChartOfAccountIdForCost_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + @" - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        protected void cboSubledgerIdForCost_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupId;
            if (cboChartOfAccountIdForCost.SelectedValue == string.Empty)
            {
                groupId = 0;
            }
            else
            {
                var coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdForCost.SelectedValue));
                groupId = coa.SubLedgerId ?? 0;
            }
            string searchText = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupId);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchText),
                                query.Description.Like(searchText)
                            )
                        );
            query.es.Top = 20;
            var tbl = query.LoadDataTable();
            cboSubledgerIdForCost.DataSource = tbl;
            cboSubledgerIdForCost.DataBind();
        }

        protected void cboSubledgerIdForCost_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + @" - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        protected void cboChartOfAccountIdForCost_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdForCost.Items.Clear();
            cboSubledgerIdForCost.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdForCost.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdForCost.Items.Clear();
                cboChartOfAccountIdForCost.Text = string.Empty;
                return;
            }
        }
        #endregion

        #region Cost Destruction
        protected void cboChartOfAccountIdForCostDestruction_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchText),
                                query.ChartOfAccountName.Like(searchText)
                            ), query.IsDetail == true, query.IsActive == true
                        );
            query.es.Top = 20;
            var tbl = query.LoadDataTable();
            cboChartOfAccountIdForCostDestruction.DataSource = tbl;
            cboChartOfAccountIdForCostDestruction.DataBind();
        }

        protected void cboChartOfAccountIdForCostDestruction_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + @" - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        protected void cboSubledgerIdForCostDestruction_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupId;
            if (cboChartOfAccountIdForCostDestruction.SelectedValue == string.Empty)
            {
                groupId = 0;
            }
            else
            {
                var coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdForCostDestruction.SelectedValue));
                groupId = coa.SubLedgerId ?? 0;
            }
            string searchText = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupId);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchText),
                                query.Description.Like(searchText)
                            )
                        );
            query.es.Top = 20;
            var tbl = query.LoadDataTable();
            cboSubledgerIdForCostDestruction.DataSource = tbl;
            cboSubledgerIdForCostDestruction.DataBind();
        }

        protected void cboSubledgerIdForCostDestruction_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + @" - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        protected void cboChartOfAccountIdForCostDestruction_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdForCostDestruction.Items.Clear();
            cboSubledgerIdForCostDestruction.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdForCost.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdForCostDestruction.Items.Clear();
                cboChartOfAccountIdForCostDestruction.Text = string.Empty;
                return;
            }
        }
        #endregion
        #endregion
    }
}

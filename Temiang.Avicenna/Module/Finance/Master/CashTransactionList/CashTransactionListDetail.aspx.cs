using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class CashTransactionListDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "CashTransactionListSearch.aspx";
            UrlPageList = "CashTransactionListList.aspx";

            ProgramID = AppConstant.Program.CASH_TRANSACTION_LIST;

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboCashType, AppEnum.StandardReference.CashManagementType);

            //PopUp Search
            if (!IsCallback)
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
            OnPopulateEntryControl(new CashTransactionList());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //Temiang.Avicenna.Module.Finance.Master.CashTransactionList.
            CashTransactionList entity = CashTransactionList.Get(txtListId.Text);
            if (entity != null)
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
            CashTransactionList entity = new CashTransactionList();

            if (entity.LoadByPrimaryKey(txtListId.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new CashTransactionList();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            CashTransactionList entity = new CashTransactionList();
            if (entity.LoadByPrimaryKey(txtListId.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("ListID='{0}'", txtDescription.Text.Trim());
            auditLogFilter.TableName = "CashTransactionList";
        }
        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtListId.Enabled = (newVal == AppEnum.DataMode.New);

            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            CashTransactionList entity = new CashTransactionList();
            if (parameters.Length > 0)
            {
                String listId = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(listId);
            }
            else
            {
                entity.LoadByPrimaryKey(txtListId.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            CashTransactionList cs = (CashTransactionList)entity;
            txtListId.Text = cs.ListId;
            txtDescription.Text = cs.Description;
            cboCashType.SelectedValue = cs.CashType;


            if (txtListId.Text != string.Empty)
            {
                int chartOfAccountId = (cs.ChartOfAccountId.HasValue ? cs.ChartOfAccountId.Value : 0);
                int subLedgerId = (cs.SubledgerId.HasValue ? cs.SubledgerId.Value : 0);
                if (chartOfAccountId != 0)
                {
                    ChartOfAccountsQuery coaQ = new ChartOfAccountsQuery();
                    coaQ.Select(coaQ.ChartOfAccountId, coaQ.ChartOfAccountCode, coaQ.ChartOfAccountName);
                    coaQ.Where(coaQ.ChartOfAccountId == chartOfAccountId);
                    DataTable dtbCoa = coaQ.LoadDataTable();
                    cboChartOfAccountId.DataSource = dtbCoa;
                    cboChartOfAccountId.DataBind();
                    cboChartOfAccountId.SelectedValue = chartOfAccountId.ToString();
                    if (subLedgerId != 0)
                    {
                        SubLedgersQuery slQ = new SubLedgersQuery();
                        slQ.Select(slQ.SubLedgerId, slQ.SubLedgerName, slQ.Description);
                        slQ.Where(slQ.SubLedgerId == subLedgerId);
                        DataTable dtbSl = slQ.LoadDataTable();
                        cboSubledgerId.DataSource = dtbSl;
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
            else
            {
                cboChartOfAccountId.Items.Clear();
                cboSubledgerId.Items.Clear();
                cboChartOfAccountId.Text = string.Empty;
                cboSubledgerId.Text = string.Empty;
            }

            //Display Data Detail
            PopulateGridDetail();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(CashTransactionList entity)
        {
            entity.ListId = txtListId.Text;
            entity.Description = txtDescription.Text;


            int chartOfAccountId = 0;
            int subLedgerId = 0;
            int.TryParse(cboChartOfAccountId.SelectedValue, out chartOfAccountId);
            int.TryParse(cboSubledgerId.SelectedValue, out subLedgerId);
            entity.ChartOfAccountId = chartOfAccountId;
            entity.SubledgerId = subLedgerId;
            entity.CashType = cboCashType.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserId = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(CashTransactionList entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                CashTransactionListItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            CashTransactionListQuery que = new CashTransactionListQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ListId > txtListId.Text);
                que.OrderBy(que.ListId.Ascending);
            }
            else
            {
                que.Where(que.ListId < txtListId.Text);
                que.OrderBy(que.ListId.Descending);
            }
            CashTransactionList entity = new CashTransactionList();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }


        #endregion

        #region Method & Event TextChanged
        protected void cboChartOfAccountId_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerId.Items.Clear();
            cboSubledgerId.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountId.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountId.Items.Clear();
                cboChartOfAccountId.Text = string.Empty;
                return;
            }
        }
        #endregion

        #region ComboBox ChartOfAccountId
        protected void cboChartOfAccountId_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
                            ),
                            query.IsDetail == true,
                            query.IsActive == true
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountId.DataSource = dtb;
            cboChartOfAccountId.DataBind();
        }

        protected void cboChartOfAccountId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerId
        protected void cboSubledgerId_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountId.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountId.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchText = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchText),
                                query.Description.Like(searchText)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerId.DataSource = dtb;
            cboSubledgerId.DataBind();
        }

        protected void cboSubledgerId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        private void PopulateGridDetail()
        {
            //Display Data Detail
            CashTransactionListItems = null; //Reset Record Detail
            grdListItem.DataSource = CashTransactionListItems;
            grdListItem.MasterTableView.IsItemInserted = false;
            grdListItem.MasterTableView.ClearEditItems();
            grdListItem.DataBind();
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdListItem.Columns[0].Visible = isVisible;
            grdListItem.Columns[grdListItem.Columns.Count - 1].Visible = isVisible;

            if (cboCashType.SelectedValue == "02") grdListItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            else grdListItem.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read) CashTransactionListItems = null;

            //Perbaharui tampilan dan data
            grdListItem.Rebind();
       }

        private BusinessObject.CashTransactionListItemCollection CashTransactionListItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCashTransactionListItem"];
                    if (obj != null) return ((BusinessObject.CashTransactionListItemCollection)(obj));
                }

                BusinessObject.CashTransactionListItemCollection coll = new BusinessObject.CashTransactionListItemCollection();
                CashTransactionListItemQuery query = new CashTransactionListItemQuery("a");
                ChartOfAccountsQuery coa = new ChartOfAccountsQuery("b");
                SubLedgersQuery sub = new SubLedgersQuery("c");

                query.Select(query,
                    coa.ChartOfAccountCode.As("refToChartOfAccounts_ChartOfAccountCode"),
                    coa.ChartOfAccountName.As("refToChartOfAccounts_ChartOfAccountName"),
                    sub.SubLedgerName.As("refToSubLedgers_SubLedgerName"));
                query.InnerJoin(coa).On(query.ChartOfAccountId == coa.ChartOfAccountId);
                query.LeftJoin(sub).On(query.SubledgerId == sub.SubLedgerId);
                query.Where(query.ListId == txtListId.Text);
                query.OrderBy(query.ListItemId.Ascending);

                coll.Load(query);

                Session["collCashTransactionListItem"] = coll;
                return coll;
            }
            set { Session["collCashTransactionListItem"] = value; }
        }

        protected void grdListItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdListItem.DataSource = CashTransactionListItems;
        }

        protected void grdListItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][BusinessObject.CashTransactionListItemMetadata.ColumnNames.ListItemId]);
            BusinessObject.CashTransactionListItem entity = FindItemGrid(itemID);
            if (entity != null) SetEntityValue(entity, e);
        }

        private BusinessObject.CashTransactionListItem FindItemGrid(string itemID)
        {
            BusinessObject.CashTransactionListItemCollection coll = CashTransactionListItems;
            BusinessObject.CashTransactionListItem retval = null;
            foreach (BusinessObject.CashTransactionListItem rec in coll)
            {
                if (rec.ListItemId.Equals(itemID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        protected void grdListItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][BusinessObject.CashTransactionListItemMetadata.ColumnNames.ListItemId]);
            BusinessObject.CashTransactionListItem entity = FindItemGrid(itemID);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdListItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            BusinessObject.CashTransactionListItem entity = CashTransactionListItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdListItem.Rebind();
        }

        private void SetEntityValue(BusinessObject.CashTransactionListItem entity, GridCommandEventArgs e)
        {
            CashTransactionListItem userControl = (CashTransactionListItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ListItemId = CashTransactionListItems.Count;
                entity.ListId = txtListId.Text;
                entity.ChartOfAccountId = userControl.ChartOfAccountId;
                entity.ChartOfAccountCode = userControl.ChartOfAccountCode;
                entity.ChartOfAccountName = userControl.ChartOfAccountName;
                entity.SubledgerId = userControl.SubLedgerId;
                entity.SubLedgerName = userControl.SubLedgerName;
                entity.Amount = userControl.Amount;
                entity.LastUpdateByUserId = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        protected void cboCashType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboChartOfAccountId.Enabled = e.Value == "01";
            cboChartOfAccountId.Items.Clear();
            cboChartOfAccountId.SelectedValue = string.Empty;
            cboChartOfAccountId.Text = string.Empty;

            cboSubledgerId.Enabled = e.Value == "01";
            cboSubledgerId.Items.Clear();
            cboSubledgerId.SelectedValue = string.Empty;
            cboSubledgerId.Text = string.Empty;

            if (e.Value == "02") grdListItem.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
            else grdListItem.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;

            CashTransactionListItems = null;
            grdListItem.Rebind();
        }
    }
}

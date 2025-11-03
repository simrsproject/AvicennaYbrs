using System;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Linq;
using System.Web;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee
{
    public partial class ParamedicFeeAddDeducDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;

            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.FeeAddDeducNo);
            txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
            cboParamedicID.Text = string.Empty;
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ParamedicFeeAddDeducSearch.aspx";
            UrlPageList = "ParamedicFeeAddDeducList.aspx";

            ProgramID = AppConstant.Program.ParamedicFeeAddDeduc;
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRParamedicFeeAdjustType, AppEnum.StandardReference.ParamedicFeeAdjustType);
                ComboBox.PopulateWithTariffComponent(cboTariffComponentID, true, false);

                trPrice.Visible = AppSession.Parameter.GetParameterValue(AppParameter.ParameterItem.pphFeeBase).ToString() == "1";
            }
        }

        #endregion

        #region ComboBox ParamedicID

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery();
            query.es.Top = 20;
            query.Select(
                query.ParamedicID,
                (query.ParamedicName + " [" + query.ParamedicID + "]").As("ParamedicName")
                );
            query.Where
                 (
                     query.Or
                     (
                        query.ParamedicID.Like(searchTextContain),
                        query.ParamedicName.Like(searchTextContain)
                     ),
                     query.IsActive == true

                 );
            query.OrderBy(query.ParamedicID.Ascending);

            cboParamedicID.DataSource = query.LoadDataTable();
            cboParamedicID.DataBind();
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
        }

        protected override void OnMenuEditClick()
        {

        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new ParamedicFeeAddDeduc();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            (new ParamedicFeeAddDeduc()).Approv(txtTransactionNo.Text, AppSession.UserLogin.UserID, true);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var str = (new ParamedicFeeAddDeduc()).Approv(txtTransactionNo.Text, AppSession.UserLogin.UserID, false);
            if (!string.IsNullOrEmpty(str))
            {
                args.MessageText = str;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {

        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {

        }

        private bool IsApprovedOrVoid(ParamedicFeeAddDeduc entity, ValidateArgs args)
        {
            if (entity.IsApproved != null && entity.IsApproved.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ParamedicFeeAddDeduc());
            PopulateNewTransactionNo();

            cboParamedicID.Text = string.Empty;
            txtTransactionDate.SelectedDate = DateTime.Now;
            txtAmount.Value = 0;
            txtPrice.Value = 0;
            chkIsIncludeInTaxCalc.Checked = true;
            cboSRParamedicFeeAdjustType.Text = string.Empty;
            cboSRParamedicFeeAdjustType.SelectedValue = string.Empty;
            cboChartOfAccountId.Items.Clear();
            cboChartOfAccountId.Text = string.Empty;
            cboSubledgerId.Items.Clear();
            cboSubledgerId.Text = string.Empty;
            cboTariffComponentID.SelectedValue = string.Empty;
            cboTariffComponentID.Text = string.Empty;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ParamedicFeeAddDeduc();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
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
            PopulateNewTransactionNo();
            var entity = new ParamedicFeeAddDeduc();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new ParamedicFeeAddDeduc();
            entity.AddNew();
            SetEntityValue(entity);

            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ParamedicFeeAddDeduc();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "ParamedicFeeAddDeduc";
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtTransactionNo.Text != string.Empty;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return !chkIsApproved.Checked;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemParamedicFeeAddDeducCoaItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ParamedicFeeAddDeduc();
            if (parameters.Length > 0)
            {
                String transactionNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transactionNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
            }

            var par = new ParamedicQuery("a");
            par.Select(par.ParamedicID, par.ParamedicName);

            DataTable tbl = par.LoadDataTable();
            cboParamedicID.DataSource = tbl;
            cboParamedicID.DataBind();

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var ad = (ParamedicFeeAddDeduc)entity;
            txtTransactionNo.Text = ad.TransactionNo;
            txtTransactionDate.SelectedDate = ad.TransactionDate;

            if (!string.IsNullOrEmpty(ad.ParamedicID))
            {
                var par = new ParamedicQuery("a");
                par.Select(
                    par.ParamedicID,
                    par.ParamedicName
                    );
                par.Where(par.ParamedicID == ad.str.ParamedicID);
                DataTable dtbpar = par.LoadDataTable();
                cboParamedicID.DataSource = dtbpar;
                cboParamedicID.DataBind();
                cboParamedicID.SelectedValue = ad.ParamedicID;
                cboParamedicID.Text = dtbpar.Rows[0]["ParamedicName"].ToString();
            }
            else
            {
                cboParamedicID.Items.Clear();
                cboParamedicID.Text = string.Empty;
            }

            cboSRParamedicFeeAdjustType.SelectedValue = ad.SRParamedicFeeAdjustType;
            txtAmount.Value = Convert.ToDouble(ad.Amount);
            txtPrice.Value = Convert.ToDouble(ad.Price ?? 0);
            txtNotes.Text = ad.Notes;
            chkIsIncludeInTaxCalc.Checked = ad.IsIncludeInTaxCalc ?? false;
            chkIsApproved.Checked = ad.IsApproved ?? false;
            cboTariffComponentID.SelectedValue = ad.TariffComponentID;

            if ((ad.ChartOfAccountId ?? 0) != 0)
            {
                var query = new ChartOfAccountsQuery();
                query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
                query.Where(query.ChartOfAccountId == ad.ChartOfAccountId);
                DataTable dtb = query.LoadDataTable();
                cboChartOfAccountId.DataSource = dtb;
                cboChartOfAccountId.DataBind();
                cboChartOfAccountId.SelectedValue = ad.ChartOfAccountId.ToString();

                if (ad.SubledgerId != null)
                {
                    var sub = new SubLedgersQuery();
                    sub.Select(sub.SubLedgerId, sub.SubLedgerName, sub.Description);
                    sub.Where(sub.SubLedgerId == ad.SubledgerId);
                    dtb = sub.LoadDataTable();
                    if (dtb.Rows.Count > 0)
                    {
                        cboSubledgerId.DataSource = dtb;
                        cboSubledgerId.DataBind();
                        cboSubledgerId.SelectedValue = ad.SubledgerId.ToString();
                    }
                }
            }

            PopulateParamedicFeeAddDeducCoaItemGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ParamedicFeeAddDeduc entity)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.ParamedicID = cboParamedicID.SelectedValue;
            entity.Amount = Convert.ToDecimal(txtAmount.Value);
            entity.Price = trPrice.Visible ? Convert.ToDecimal(txtPrice.Value) : entity.Amount;
            entity.SRParamedicFeeAdjustType = cboSRParamedicFeeAdjustType.SelectedValue;
            entity.Notes = txtNotes.Text;
            entity.IsIncludeInTaxCalc = chkIsIncludeInTaxCalc.Checked;
            entity.TariffComponentID = cboTariffComponentID.SelectedValue;
            entity.ChartOfAccountId = cboChartOfAccountId.SelectedValue.ToInt();
            entity.SubledgerId = cboSubledgerId.SelectedValue.ToInt();

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdatedByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            foreach (var item in ParamedicFeeAddDeducCoaItems)
            {
                item.TransactionNo = txtTransactionNo.Text;
                item.LastUpdateByUserId = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(ParamedicFeeAddDeduc entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                if (DataModeCurrent == AppEnum.DataMode.New) _autoNumber.Save();

                ParamedicFeeAddDeducCoaItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ParamedicFeeAddDeducQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TransactionNo > txtTransactionNo.Text);
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < txtTransactionNo.Text);
                que.OrderBy(que.TransactionNo.Descending);
            }
            var entity = new ParamedicFeeAddDeduc();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region ComboBox ChartOfAccountId
        protected void cboChartOfAccountId_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
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
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
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

        protected void cboChartOfAccountTemplateId_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new CashTransactionListQuery();
            query.Select(query.ListId, query.Description);
            query.Where
                        (
                            query.Or
                            (
                                query.ListId.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountTemplateId.DataSource = dtb;
            cboChartOfAccountTemplateId.DataBind();
        }

        protected void cboChartOfAccountTemplateId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ListId"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ListId"].ToString();
        }

        private ParamedicFeeAddDeducCoaItemCollection ParamedicFeeAddDeducCoaItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collParamedicFeeAddDeducCoaItem"];
                    if (obj != null) return ((ParamedicFeeAddDeducCoaItemCollection)(obj));
                }

                var query = new ParamedicFeeAddDeducCoaItemQuery("x");
                var coa = new ChartOfAccountsQuery("a");
                var sub = new SubLedgersQuery("b");

                query.Select(query,
                    "<a.ChartOfAccountCode + ' - ' + a.ChartOfAccountName AS refTChartOfAccounts_ChartOfAccountName>",
                    "<b.SubLedgerName + ' - ' + b.Description AS refToSubledgers_SubLedgerName>");
                query.InnerJoin(coa).On(query.ChartOfAccountId == coa.ChartOfAccountId);
                query.LeftJoin(sub).On(query.SubledgerId == sub.SubLedgerId);
                query.Where(query.TransactionNo == txtTransactionNo.Text);

                var coll = new ParamedicFeeAddDeducCoaItemCollection();
                coll.Load(query);

                Session["collParamedicFeeAddDeducCoaItem"] = coll;
                return coll;
            }
            set
            {
                Session["collParamedicFeeAddDeducCoaItem"] = value;
            }
        }

        private void RefreshCommandItemParamedicFeeAddDeducCoaItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdVoucherEntryItem.Columns[0].Visible = isVisible;
            grdVoucherEntryItem.Columns[grdVoucherEntryItem.Columns.Count - 1].Visible = isVisible;

            grdVoucherEntryItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdVoucherEntryItem.Rebind();
        }

        private void PopulateParamedicFeeAddDeducCoaItemGrid()
        {
            //Display Data Detail
            ParamedicFeeAddDeducCoaItems = null; //Reset Record Detail
            grdVoucherEntryItem.DataSource = ParamedicFeeAddDeducCoaItems; //Requery
            grdVoucherEntryItem.MasterTableView.IsItemInserted = false;
            grdVoucherEntryItem.MasterTableView.ClearEditItems();
            grdVoucherEntryItem.DataBind();
        }

        protected void grdVoucherEntryItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdVoucherEntryItem.DataSource = ParamedicFeeAddDeducCoaItems;
        }

        protected void grdVoucherEntryItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            int id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.ListItemId]);

            var entity = FindParamedicFeeAddDeducCoaItem(id);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdVoucherEntryItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            int id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.ListItemId]);

            var entity = FindParamedicFeeAddDeducCoaItem(id);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdVoucherEntryItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ParamedicFeeAddDeducCoaItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdVoucherEntryItem.Rebind();
        }

        private ParamedicFeeAddDeducCoaItem FindParamedicFeeAddDeducCoaItem(int id)
        {
            var coll = ParamedicFeeAddDeducCoaItems;
            return coll.FirstOrDefault(rec => rec.ListItemId.Equals(id));
        }

        private void SetEntityValue(ParamedicFeeAddDeducCoaItem entity, GridCommandEventArgs e)
        {
            ParamedicFeeAddDeducItem userControl = (ParamedicFeeAddDeducItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ListItemId = userControl.ListItemId;
                entity.ChartOfAccountId = userControl.ChartOfAccountId;
                entity.ChartOfAccountName = userControl.ChartOfAccountName;
                entity.SubledgerId = userControl.SubLedgerId;
                entity.SubLedgerName = userControl.SubLedgerName;
                entity.Amount = userControl.Amount;
            }
        }

        protected void cboChartOfAccountTemplateId_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                var list = new CashTransactionList();
                list.LoadByPrimaryKey(e.Value);

                if (list.CashType == "01")
                {
                    var query = new ChartOfAccountsQuery();
                    query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
                    query.Where(query.ChartOfAccountId == list.ChartOfAccountId);
                    DataTable dtb = query.LoadDataTable();
                    cboChartOfAccountId.DataSource = dtb;
                    cboChartOfAccountId.DataBind();
                    cboChartOfAccountId.SelectedValue = list.ChartOfAccountId.ToString();

                    var sub = new SubLedgersQuery();
                    sub.Select(sub.SubLedgerId, sub.SubLedgerName, sub.Description);
                    sub.Where(sub.SubLedgerId == list.SubledgerId);
                    dtb = sub.LoadDataTable();
                    if (dtb.Rows.Count > 0)
                    {
                        cboSubledgerId.DataSource = dtb;
                        cboSubledgerId.DataBind();
                        cboSubledgerId.SelectedValue = list.SubledgerId.ToString();
                    }
                }
                else
                {
                    var items = new CashTransactionListItemCollection();
                    items.Query.Where(items.Query.ListId == e.Value);
                    items.Query.Load();

                    foreach (var item in items)
                    {
                        var entity = ParamedicFeeAddDeducCoaItems.AddNew();
                        entity.ListItemId = ParamedicFeeAddDeducCoaItems.Count + 1;
                        entity.ChartOfAccountId = item.ChartOfAccountId;
                        var coa = new ChartOfAccounts();
                        if (coa.LoadByPrimaryKey(item.ChartOfAccountId ?? 0)) entity.ChartOfAccountName = coa.ChartOfAccountCode + " - " + coa.ChartOfAccountName;
                        entity.SubledgerId = item.SubledgerId;
                        var sub = new SubLedgers();
                        if (sub.LoadByPrimaryKey(item.SubledgerId ?? 0)) entity.SubLedgerName = sub.SubLedgerName + " - " + sub.Description;
                        entity.Amount = item.Amount ?? 0;
                    }
                }

            }

            grdVoucherEntryItem.Rebind();
        }
    }
}

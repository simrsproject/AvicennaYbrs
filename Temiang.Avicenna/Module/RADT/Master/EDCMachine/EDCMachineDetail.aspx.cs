using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class EDCMachineDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "EDCMachineSearch.aspx";
            UrlPageList = "EDCMachineList.aspx";

            ProgramID = AppConstant.Program.EdcMachine;

            //StandardReference Initialize
            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSRCardProvider, AppEnum.StandardReference.CardProvider);
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
            OnPopulateEntryControl(new EDCMachine());
            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            EDCMachine entity = new EDCMachine();
            if (entity.LoadByPrimaryKey(txtEDCMachineID.Text))
            {
                EDCMachineTariffCollection detil = new EDCMachineTariffCollection();
                detil.Query.Where(detil.Query.EDCMachineID == txtEDCMachineID.Text);
                detil.LoadAll();
                detil.MarkAllAsDeleted();
                entity.MarkAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    EDCMachineTariffs.Save();
                    entity.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            EDCMachine entity = new EDCMachine();
            if (entity.LoadByPrimaryKey(txtEDCMachineID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new EDCMachine();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            EDCMachine entity = new EDCMachine();
            if (entity.LoadByPrimaryKey(txtEDCMachineID.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("EDCMachineID='{0}'", txtEDCMachineID.Text.Trim());
            auditLogFilter.TableName = "EDCMachine";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtEDCMachineID.Enabled = (newVal == AppEnum.DataMode.New);

            RefreshCommandItemEDCMachineTariff(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            EDCMachine entity = new EDCMachine();
            if (parameters.Length > 0)
            {
                String eDCMachineID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(eDCMachineID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtEDCMachineID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            EDCMachine eDCMachine = (EDCMachine)entity;
            txtEDCMachineID.Text = eDCMachine.EDCMachineID;
            cboSRCardProvider.SelectedValue = eDCMachine.SRCardProvider;
            txtEDCMachineName.Text = eDCMachine.EDCMachineName;
            chkIsActive.Checked = eDCMachine.IsActive ?? false;
            if (txtEDCMachineID.Text != string.Empty)
            {
                int chartOfAccountId = (eDCMachine.ChartOfAccountID.HasValue ? eDCMachine.ChartOfAccountID.Value : 0);
                int subLedgerId = (eDCMachine.SubledgerID.HasValue ? eDCMachine.SubledgerID.Value : 0);
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
            PopulateEDCMachineTariffGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(EDCMachine entity)
        {
            entity.EDCMachineID = txtEDCMachineID.Text;
            entity.SRCardProvider = cboSRCardProvider.SelectedValue;
            entity.EDCMachineName = txtEDCMachineName.Text;
            int chartOfAccountId = 0;
            int subLedgerId = 0;
            int.TryParse(cboChartOfAccountId.SelectedValue, out chartOfAccountId);
            int.TryParse(cboSubledgerId.SelectedValue, out subLedgerId);
            entity.ChartOfAccountID = chartOfAccountId;
            entity.SubledgerID = subLedgerId;
            entity.IsActive = chkIsActive.Checked;

            foreach (EDCMachineTariff tariff in EDCMachineTariffs)
            {
                tariff.EDCMachineID = txtEDCMachineID.Text;
                if (tariff.es.IsAdded || tariff.es.IsModified)
                {
                    tariff.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    tariff.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(EDCMachine entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                EDCMachineTariffs.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            EDCMachineQuery que = new EDCMachineQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.EDCMachineID > txtEDCMachineID.Text);
                que.OrderBy(que.EDCMachineID.Ascending);
            }
            else
            {
                que.Where(que.EDCMachineID < txtEDCMachineID.Text);
                que.OrderBy(que.EDCMachineID.Descending);
            }

            EDCMachine entity = new EDCMachine();
            if (entity.Load(que))
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
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        , query.IsDetail == true);
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

        #region Record Detail Method Function EDCMachineTariff

        private void RefreshCommandItemEDCMachineTariff(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdEDCMachineTariff.Columns[0].Visible = isVisible;
            grdEDCMachineTariff.Columns[grdEDCMachineTariff.Columns.Count - 1].Visible = isVisible;

            grdEDCMachineTariff.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdEDCMachineTariff.Rebind();
        }

        private EDCMachineTariffCollection EDCMachineTariffs
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEDCMachineTariff"];
                    if (obj != null)
                    {
                        return ((EDCMachineTariffCollection)(obj));
                    }
                }

                EDCMachineTariffCollection coll = new EDCMachineTariffCollection();
                EDCMachineTariffQuery query = new EDCMachineTariffQuery("a");
                AppStandardReferenceItemQuery qSri = new AppStandardReferenceItemQuery("b");
                ChartOfAccountsQuery coa = new ChartOfAccountsQuery("coa");
                query.InnerJoin(qSri).On(query.SRCardType == qSri.ItemID & qSri.StandardReferenceID == "CardType")
                    .LeftJoin(coa).On(query.ChartOfAccountId == coa.ChartOfAccountId)
                    .Where(query.EDCMachineID == txtEDCMachineID.Text);
                query.Select(
                    query.SelectAllExcept(), 
                    qSri.ItemName.As("refToSRItem_ItemName"),
                    coa.ChartOfAccountCode.As("refToSRChartOfAccounts_ChartOfAccountCode"),
                    coa.ChartOfAccountName.As("refToSRChartOfAccounts_ChartOfAccountName")
                );
                query.OrderBy(qSri.ItemName.Ascending);
                coll.Load(query);

                Session["collEDCMachineTariff"] = coll;
                return coll;
            }
            set { Session["collEDCMachineTariff"] = value; }
        }

        private void PopulateEDCMachineTariffGrid()
        {
            //Display Data Detail
            EDCMachineTariffs = null; //Reset Record Detail
            grdEDCMachineTariff.DataSource = EDCMachineTariffs; //Requery
            grdEDCMachineTariff.MasterTableView.IsItemInserted = false;
            grdEDCMachineTariff.MasterTableView.ClearEditItems();
            grdEDCMachineTariff.DataBind();
        }

        protected void grdEDCMachineTariff_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEDCMachineTariff.DataSource = EDCMachineTariffs;
        }

        protected void grdEDCMachineTariff_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String sRCardType = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EDCMachineTariffMetadata.ColumnNames.SRCardType]);
            EDCMachineTariff entity = FindEDCMachineTariff(sRCardType);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdEDCMachineTariff_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String sRCardType = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][EDCMachineTariffMetadata.ColumnNames.SRCardType]);
            EDCMachineTariff entity = FindEDCMachineTariff(sRCardType);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdEDCMachineTariff_InsertCommand(object source, GridCommandEventArgs e)
        {
            EDCMachineTariff entity = EDCMachineTariffs.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdEDCMachineTariff.Rebind();
        }

        private EDCMachineTariff FindEDCMachineTariff(String sRCardType)
        {
            EDCMachineTariffCollection coll = EDCMachineTariffs;
            EDCMachineTariff retEntity = null;
            foreach (EDCMachineTariff rec in coll)
            {
                if (rec.SRCardType.Equals(sRCardType))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(EDCMachineTariff entity, GridCommandEventArgs e)
        {
            EDCMachineTariffDetail userControl = (EDCMachineTariffDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRCardType = userControl.SRCardType;
                entity.CardTypeName = userControl.CardTypeName;
                entity.EDCMachineTariff = userControl.EDCMachineTariff;
                entity.AddFeeAmount = userControl.AddFeeAmount;
                entity.IsActive = userControl.IsActive;
                entity.IsChargedToPatient = userControl.IsChargedToPatient;
                entity.ChartOfAccountId = userControl.CoaId;
                entity.SubledgerID = userControl.SlId;
            }
        }

        #endregion
    }
}

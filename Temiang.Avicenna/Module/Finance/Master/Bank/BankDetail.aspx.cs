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
    public partial class BankDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "BankSearch.aspx";
            UrlPageList = "BankList.aspx";

            ProgramID = AppConstant.Program.BANK;

			//StandardReference Initialize
			if (!IsPostBack)
			{
			    this.InitCurrency();
                trIsBKU.Visible = AppSession.Parameter.IsUsingBKUModule;
			}
			
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
            OnPopulateEntryControl(new Bank());
            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Bank entity = new Bank();
            entity.LoadByPrimaryKey(txtBankID.Text);
            entity.MarkAsDeleted();

            BankAccountCollection coll = new BankAccountCollection();
            string bankID = txtBankID.Text;
            coll.Query.Where(coll.Query.BankID == bankID);
            coll.LoadAll();
            coll.MarkAllAsDeleted();

            using (esTransactionScope trans = new esTransactionScope())
            {
                coll.Save();
                entity.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            } 
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Bank entity = new Bank();
            if (entity.LoadByPrimaryKey(txtBankID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new Bank();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Bank entity = new Bank();
            if (entity.LoadByPrimaryKey(txtBankID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var bankColl = new BankCollection();
            if (bankColl.LoadAll()) {
                if (bankColl.Where(b => !(b.BankID.Equals(txtBankID.Text)) &&
                    (b.JournalCode ?? string.Empty).Equals(txtJournalCode.Text) &&
                    !((b.JournalCode ?? string.Empty).Equals(string.Empty))).Count() > 0) {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = string.Format("Journal code: {0} has exist", txtJournalCode.Text);
                }
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
            auditLogFilter.PrimaryKeyData = string.Format("BankID='{0}'", txtBankID.Text.Trim());
            auditLogFilter.TableName = "Bank";
        }
        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtBankID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Bank entity = new Bank();
            if (parameters.Length > 0)
            {
                String bankID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(bankID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtBankID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            Bank bank = (Bank)entity;
            txtBankID.Text = bank.BankID;
            txtBankName.Text = bank.BankName;
            txtNoRek.Text = bank.NoRek;
            cboSRCurrency.SelectedValue = bank.CurrencyCode;
            txtJournalCode.Text = bank.JournalCode;
            chkIsActive.Checked = bank.IsActive ?? false;
            chkIsToBeCleared.Checked = bank.IsToBeCleared ?? false;
            chkCrossRefference.Checked = bank.IsCrossRefference ?? false;
            chkIsCashierFrontOffice.Checked = bank.IsCashierFrontOffice ?? false;
            chkIsCashierFrontOfficeDpReturn.Checked = bank.IsCashierFrontOfficeDpReturn ?? false;
            chkIsArPayment.Checked = bank.IsArPayment ?? false;
            ChkIsApPayment.Checked = bank.IsApPayment ?? false;
            ChkIsFeePayment.Checked = bank.IsFeePayment ?? false;
            chkIsAssetAuctionPayment.Checked = bank.IsAssetAuctionPayment ?? false;
            ChkIsBKU.Checked = bank.IsBKU ?? false;
            if (txtBankID.Text != string.Empty)
            {
                int chartOfAccountId = (bank.ChartOfAccountId.HasValue ? bank.ChartOfAccountId.Value : 0);
                int subLedgerId = (bank.SubledgerId.HasValue ? bank.SubledgerId.Value : 0);
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
            //PopulateGridDetail();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(Bank entity)
        {
            //foreach (BankAccount account in BankAccounts)
            //{
            //    account.BankID = txtBankID.Text;
            //    //Last Update Status
            //    if (account.es.IsAdded || account.es.IsModified)
            //    {
            //        account.LastUpdateByUserID = AppSession.UserLogin.UserID;
            //        account.LastUpdateDateTime = DateTime.Now;
            //    }
            //}
            entity.BankID = txtBankID.Text;
            entity.BankName = txtBankName.Text;
            entity.NoRek = txtNoRek.Text;
            entity.CurrencyCode = cboSRCurrency.SelectedValue;

            int chartOfAccountId = 0;
            int subLedgerId = 0;
            int.TryParse(cboChartOfAccountId.SelectedValue, out chartOfAccountId);
            int.TryParse(cboSubledgerId.SelectedValue, out subLedgerId);
            entity.ChartOfAccountId = chartOfAccountId;
            entity.SubledgerId = subLedgerId;
            entity.JournalCode = txtJournalCode.Text;
            entity.IsActive = chkIsActive.Checked;
            entity.IsToBeCleared = chkIsToBeCleared.Checked;
            entity.IsCrossRefference = chkCrossRefference.Checked;
            entity.IsCashierFrontOffice = chkIsCashierFrontOffice.Checked;
            entity.IsCashierFrontOfficeDpReturn = chkIsCashierFrontOfficeDpReturn.Checked;
            entity.IsArPayment = chkIsArPayment.Checked;
            entity.IsApPayment = ChkIsApPayment.Checked;
            entity.IsFeePayment = ChkIsFeePayment.Checked;
            entity.IsAssetAuctionPayment = chkIsAssetAuctionPayment.Checked;
            entity.IsBKU = ChkIsBKU.Checked;
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(Bank entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            BankQuery que = new BankQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.BankID > txtBankID.Text);
                que.OrderBy(que.BankID.Ascending);
            }
            else
            {
                que.Where(que.BankID < txtBankID.Text);
                que.OrderBy(que.BankID.Descending);
            }
            Bank entity = new Bank();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        private void InitCurrency()
        {
            var coll = new CurrencyRateCollection();
            coll.LoadAll();

            cboSRCurrency.DataSource = coll;
            cboSRCurrency.DataTextField = CurrencyRateMetadata.ColumnNames.CurrencyName;
            cboSRCurrency.DataValueField = CurrencyRateMetadata.ColumnNames.CurrencyID;
            cboSRCurrency.DataBind();
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
            query.Where(
                query.IsDetail == true,
                query.NormalBalance == "D",
                query.Or(
                    query.ChartOfAccountCode.Like(searchText),
                    query.ChartOfAccountName.Like(searchText)
                )
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
    }
}

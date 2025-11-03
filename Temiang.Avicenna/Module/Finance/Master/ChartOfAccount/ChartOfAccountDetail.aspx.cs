using System;
using System.Collections.Generic;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ChartOfAccountDetail : BasePageDetail
    {
        private string ChartOfAccountCode;

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ChartOfAccountSearch.aspx";
            UrlPageList = "ChartOfAccountList.aspx";

            ProgramID = AppConstant.Program.CHART_OF_ACCOUNT;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.Initialize(cboSRAcctLevel, AppEnum.StandardReference.AcctLevel);
                StandardReference.Initialize(cboNormalBalance, AppEnum.StandardReference.AcctDbCr);
                InitChartOfAccountGroup();

                chkIsReconcile.Visible = !AppSession.Parameter.acc_IsJournalCashBased;
                pnlBku.Visible= AppSession.Parameter.acc_IsUsingBkuAccount;
            }

            //PopUp Search
            if (!IsCallback)
            {
                //PopUpSearch.Initialize(AppEnum.PopUpSearch.AcctSubGroup, Page, txtAccountGroup);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ChartOfAccountCode = base.Request.QueryString["id"];
            if (!this.IsPostBack)
            {
                this.InitSubLedger();
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Temiang.Avicenna.BusinessObject.ChartOfAccounts());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            ChartOfAccounts entity = ChartOfAccounts.Get(this.ChartOfAccountCode);
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
            ChartOfAccounts entity = ChartOfAccounts.Get(txtChartOfAccountCode.Text);
            if (entity != null)
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }


            entity = new ChartOfAccounts();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            ChartOfAccounts entity = ChartOfAccounts.Get(txtChartOfAccountCode.Text);
            if (entity != null)
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
            auditLogFilter.PrimaryKeyData = string.Format("ChartOfAccountCode='{0}'", txtChartOfAccountCode.Text);
            auditLogFilter.TableName = "ChartOfAccounts";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtChartOfAccountCode.ReadOnly = (newVal != AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ChartOfAccounts entity = new ChartOfAccounts();
            if (parameters.Length > 0)
            {
                int accountID = System.Convert.ToInt32(parameters[0]);

                if (!parameters[0].Equals(string.Empty))
                {
                    entity = ChartOfAccounts.GetById(accountID);
                }
            }
            else
            {
                entity = ChartOfAccounts.Get(txtChartOfAccountCode.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity e)
        {
            ChartOfAccounts entity = (ChartOfAccounts)e;
            txtChartOfAccountCode.Text = entity.ChartOfAccountCode;
            txtAccountName.Text = entity.ChartOfAccountName;
            chkIsDetail.Checked = entity.IsDetail ?? false;
            chkIsActive.Checked = entity.IsActive ?? false;
            chkIsControlDocNumber.Checked = entity.IsControlDocNumber ?? false;
            chkIsReconcile.Checked = entity.IsReconcile ?? false;

            if (entity.AccountGroup.HasValue)
                cboAccountGroup.SelectedValue = entity.AccountGroup.Value.ToString();
            if (entity.AccountLevel.HasValue)
                cboSRAcctLevel.SelectedValue = entity.AccountLevel.Value.ToString();
            txtAccountGroup.Text = entity.GeneralAccount;
            cboNormalBalance.SelectedValue = entity.NormalBalance;
            int subLedgerId = (entity.SubLedgerId.HasValue ? entity.SubLedgerId.Value : 0);
            if (subLedgerId > 0)
                cboSubLedger.SelectedValue = entity.SubLedgerId.Value.ToString();
            else
                cboSubLedger.SelectedValue = "";
            PopulateAcctSubGroupName(true);

            txtNote.Text = entity.Note;
            chkBkuAccount.Checked = entity.IsBku ?? false;
            if (entity.BkuAccountID != null)
            {
                var coa = new ChartOfAccountsQuery();
                coa.es.Top = 20;
                coa.Where(coa.ChartOfAccountId == (entity.BkuAccountID ?? 0));
                coa.OrderBy(coa.ChartOfAccountId.Ascending);
                cboBkuAccount.DataSource = coa.LoadDataTable();
                cboBkuAccount.DataBind();
                cboBkuAccount.SelectedValue = entity.BkuAccountID.ToString();
            }
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(ChartOfAccounts entity)
        {
            entity.ChartOfAccountCode = txtChartOfAccountCode.Text;
            entity.ChartOfAccountName = txtAccountName.Text;
            entity.IsDetail = chkIsDetail.Checked;
            entity.IsActive = chkIsActive.Checked;
            entity.AccountGroup = int.Parse(cboAccountGroup.SelectedValue);
            entity.AccountLevel = int.Parse(cboSRAcctLevel.SelectedValue);
            entity.GeneralAccount = txtAccountGroup.Text;
            entity.NormalBalance = cboNormalBalance.SelectedValue;
            int subLedgerId = 0;
            int.TryParse(cboSubLedger.SelectedValue, out subLedgerId);
            entity.SubLedgerId = subLedgerId;
            entity.Note = txtNote.Text;
            entity.LastUpdateDateTime = DateTime.Now;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

            entity.IsDocumenNumberEnabled = false; //not used
            entity.TreeCode = string.Empty;
            //entity.IsControlDocNumber = chkIsControlDocNumber.Checked;
            entity.IsReconcile = chkIsReconcile.Checked;
            entity.IsBku = chkBkuAccount.Checked;
            entity.str.BkuAccountID = cboBkuAccount.SelectedValue;

            if (entity.es.IsAdded)
            {
                entity.DateCreated = DateTime.Now;
                entity.CreatedBy = entity.LastUpdateByUserID;
            }
        }

        private void SaveEntity(ChartOfAccounts entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                if (entity.es.IsAdded) {
                    // generate new id
                    var ec = new ChartOfAccountsQuery();
                    ec.Select(ec.ChartOfAccountId.Max());
                    var dtb = ec.LoadDataTable();
                    if (dtb.Rows.Count == 0)
                    {
                        entity.ChartOfAccountId = 1;
                    }
                    else {
                        entity.ChartOfAccountId = System.Convert.ToInt32(dtb.Rows[0][0]) + 1;
                    }
                }
                entity.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            ChartOfAccountsQuery que = new ChartOfAccountsQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ChartOfAccountCode > txtChartOfAccountCode.Text);
                que.OrderBy(que.ChartOfAccountCode.Ascending);
            }
            else
            {
                que.Where(que.ChartOfAccountCode < txtChartOfAccountCode.Text);
                que.OrderBy(que.ChartOfAccountCode.Descending);
            }
            ChartOfAccounts entity = new ChartOfAccounts();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged
        protected void txtAccountGroup_TextChanged(object sender, EventArgs e)
        {
            PopulateAcctSubGroupName(true);
        }

        private void PopulateAcctSubGroupName(bool isResetIdIfNotExist)
        {
            if (txtAccountGroup.Text == string.Empty)
            {
                lblAcctSubGroupName.Text = string.Empty;
                return;
            }
            lblAcctSubGroupName.Text = string.Empty;
            ChartOfAccounts entity = ChartOfAccounts.Get(txtAccountGroup.Text);
            if (entity != null)
            {
                lblAcctSubGroupName.Text = entity.ChartOfAccountName;
            }
        }
        #endregion

        #region Method & Event Combo Level Changed
        private void InitSubLedger()
        {
            cboSubLedger.Items.Clear();
            cboSubLedger.Items.Add(new RadComboBoxItem("", ""));
            foreach (SubLedgerGroups e in SubLedgerGroups.Get())
                cboSubLedger.Items.Add(new RadComboBoxItem(string.Format("{0} - {1}", e.GroupCode, e.GroupName), e.SubLedgerGroupId.Value.ToString()));
        }
        private void InitChartOfAccountGroup()
        {
            AppStandardReferenceItemCollection coll = new AppStandardReferenceItemCollection();
            coll.LoadByStandardReferenceID(AppEnum.StandardReference.AcctGroup.ToString());
            List<AppStandardReferenceItem> items = new List<AppStandardReferenceItem>(coll.OrderBy<AppStandardReferenceItem, int>(delegate(AppStandardReferenceItem en)
            {
                return Convert.ToInt32(en.ItemID);
            }));
            cboAccountGroup.Items.Clear();
            foreach (AppStandardReferenceItem item in items)
                cboAccountGroup.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));   

        }
        #endregion

        protected void cboBkuAccount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboBkuAccount_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var coa = new ChartOfAccountsQuery();
            coa.es.Top = 20;
            coa.Where(coa.Or(coa.ChartOfAccountCode.Like(searchText), coa.ChartOfAccountName.Like(searchText)), coa.IsDetail == true, coa.IsActive == true);
            coa.OrderBy(coa.ChartOfAccountId.Ascending);
            cboBkuAccount.DataSource = coa.LoadDataTable();
            cboBkuAccount.DataBind();
        }
    }
}

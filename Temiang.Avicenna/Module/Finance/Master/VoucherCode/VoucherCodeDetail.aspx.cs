using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class VoucherCodeDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {

            txtNumberFormat.AutoPostBack = true;
            txtNumberFormat.TextChanged += new EventHandler(txtNumberFormat_TextChanged);

            // Url Search & List
            UrlPageSearch = "VoucherCodeSearch.aspx";
            UrlPageList = "VoucherCodeList.aspx";

            ProgramID = AppConstant.Program.VOUCHER_CODE;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                txtNumberFormat.Text = "{0:00000}";
            }

            Helper.SetupComboBox(txtBankAccount);
            txtBankAccount.ItemsRequested += txtBankAccount_ItemsRequested;
            txtBankAccount.ItemDataBound += txtBankAccount_ItemDataBound;
            txtBankAccount.TextChanged += txtBankAccount_TextChanged;

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboCashType, AppEnum.StandardReference.CashManagementType);

            //PopUp Search
            if (!IsCallback)
            {

            }
        }

        void txtBankAccount_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var box = o as RadComboBox;
            if (box == null)
                return;

            var val = e.Text;
            if (val.Length != 0)
            {
                var coll = Bank.GetLike(val);

                box.Items.Clear();
                box.DataSource = coll;
                box.DataBind();
            }
        }

        void txtBankAccount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {

        }

        void txtBankAccount_TextChanged(object sender, EventArgs e)
        {
            var code = txtBankAccount.Text;
            var entity = Bank.Get(code);
            if (entity == null)
                return;
        }

        void txtNumberFormat_TextChanged(object sender, EventArgs e)
        {
            showSample();
        }

        private void showSample()
        {
            int sample = 123;
            lblNumberFormatSample.Text = string.Empty;
            string prefix = string.Empty;
            if (!string.IsNullOrEmpty(txtJournalCode.Text))
                prefix = txtJournalCode.Text + "-";
            lblNumberFormatSample.Text = prefix + String.Format(txtNumberFormat.Text, sample);
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
            OnPopulateEntryControl(new JournalCodes());

            txtCurrentNumber.Text = "1";
            txtNumberSeed.Text = "1";
            chkIsEnabled.Checked = true;
            chkIsAutoNumber.Checked = true;
            txtNumberFormat.Text = "{0:00000}";
            txtBankAccount.Text = string.Empty;
            cboCashType.SelectedIndex = 0;
            this.showSample();
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            JournalCodes entity = JournalCodes.Get(txtJournalCode.Text);
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
            JournalCodes entity = JournalCodes.Get(txtJournalCode.Text);
            if (entity != null)
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new JournalCodes();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            JournalCodes entity = JournalCodes.Get(txtJournalCode.Text);
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
            auditLogFilter.PrimaryKeyData = string.Format("JournalCode='{0}'", txtJournalCode.Text.Trim());
            auditLogFilter.TableName = "JournalCodes";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtJournalCode.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            JournalCodes entity = new JournalCodes();
            if (parameters.Length > 0)
            {
                String id = (String)parameters[0];
                String code = (String)parameters[1];

                if (!parameters[0].Equals(string.Empty))
                    entity = JournalCodes.Get(Convert.ToInt32(id));
            }
            else
            {
                entity = JournalCodes.Get(txtJournalCode.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            JournalCodes e = (JournalCodes)entity;
            txtJournalCode.Text = e.JournalCode;
            txtDescription.Text = e.Description;
            txtCurrentNumber.Value = Convert.ToDouble(e.CurrentNumber);
            txtNumberFormat.Text = e.NumberFormat;

            txtNumberSeed.Value = Convert.ToDouble(e.NumberSeed);
            chkIsEnabled.Checked = e.IsEnabled ?? false;
            chkIsAutoNumber.Checked = e.IsAutoNumber ?? false;
            if (!string.IsNullOrEmpty(e.BankID))
            {
                var bankAccount = Bank.Get(e.BankID);

                txtBankAccount.SelectedValue = e.BankID;
                //txtBankAccount.SelectedItem.Text = string.Format("{0} - {1}", bankAccount.BankName.Trim(), bankAccount.NoRek.Trim());
                txtBankAccount.Text = string.Format("{0} - {1}", bankAccount.BankName, bankAccount.NoRek);
            }

            cboCashType.SelectedValue = e.CashType;
            chkIsBKU.Checked = e.IsBku ?? false;

            showSample();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(JournalCodes entity)
        {
            entity.JournalCode = txtJournalCode.Text;
            entity.Description = txtDescription.Text;
            entity.CurrentNumber = Convert.ToInt32(txtCurrentNumber.Text);
            entity.NumberFormat = txtNumberFormat.Text;
            entity.NumberSeed = Convert.ToInt32(txtNumberSeed.Text);
            entity.IsEnabled = chkIsEnabled.Checked;
            entity.IsAutoNumber = chkIsEnabled.Checked;
            entity.BankID = txtBankAccount.SelectedValue;
            entity.CashType = cboCashType.SelectedValue;
            entity.IsVisible = true;
            entity.IsBku = chkIsBKU.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
            }
        }

        private void SaveEntity(JournalCodes entity)
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
            JournalCodesQuery que = new JournalCodesQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.JournalCode > txtJournalCode.Text, que.IsVisible == true);
                que.OrderBy(que.JournalCode.Ascending);
            }
            else
            {
                que.Where(que.JournalCode < txtJournalCode.Text, que.IsVisible == true);
                que.OrderBy(que.JournalCode.Descending);
            }
            JournalCodes entity = new JournalCodes();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        #endregion
    }
}

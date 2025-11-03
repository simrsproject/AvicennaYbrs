using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class BankAccountDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRCurrency, AppEnum.StandardReference.Currency);
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtBankAccountNo.Text = (String)DataBinder.Eval(DataItem, BankAccountMetadata.ColumnNames.BankAccountNo);
            cboSRCurrency.SelectedValue = (String)DataBinder.Eval(DataItem, BankAccountMetadata.ColumnNames.SRCurrency);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, BankAccountMetadata.ColumnNames.Notes);
            chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, BankAccountMetadata.ColumnNames.IsActive);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                BankAccountCollection coll = (BankAccountCollection)Session["collBankAccount"];

                string bankAccountNo = txtBankAccountNo.Text;
                bool isExist = false;
                foreach (BankAccount item in coll)
                {
                    if (item.BankAccountNo.Equals(bankAccountNo))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Bank Account No: {0} has exist", bankAccountNo);
                }
            }
        }

        #region Properties for return entry value
        public String BankAccountNo
        {
            get { return txtBankAccountNo.Text; }
        }
        public String SRCurrency
        {
            get { return cboSRCurrency.SelectedValue; }
        }
        public String Notes
        {
            get { return txtNotes.Text; }
        }
        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }
        #endregion
    }
}

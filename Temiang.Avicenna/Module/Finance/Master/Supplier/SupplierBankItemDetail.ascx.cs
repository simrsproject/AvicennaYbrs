using System;
using System.Data;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class SupplierBankItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                chkIsActive.Checked = true;

                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtBankAccountNo.Text = (String)DataBinder.Eval(DataItem, SupplierBankMetadata.ColumnNames.BankAccountNo);
            txtBankName.Text = (String)DataBinder.Eval(DataItem, SupplierBankMetadata.ColumnNames.BankName);
            txtBankAccountName.Text = (String)DataBinder.Eval(DataItem, SupplierBankMetadata.ColumnNames.BankAccountName);
            chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, SupplierBankMetadata.ColumnNames.IsActive);
        }

        #region Properties for return entry value

        public String BankAccountNo
        {
            get { return txtBankAccountNo.Text; }
        }

        public String BankName
        {
            get { return txtBankName.Text; }
        }

        public String BankAccountName
        {
            get { return txtBankAccountName.Text; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        #endregion

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (SupplierBankCollection)Session["collSupplierBank"];

                bool isExist = false;
                foreach (SupplierBank item in coll)
                {
                    if (item.BankAccountNo.Equals(txtBankAccountNo.Text))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Bank Account No: {0} has exist", txtBankAccountNo.Text);
                }
            }
        }
    }
}
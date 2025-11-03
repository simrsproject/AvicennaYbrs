using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.Receivable.Customer
{
    public partial class InvoicingItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            ViewState["IsNewRecord"] = false;

            txtTransactionNo.Text = (String)DataBinder.Eval(DataItem, InvoiceCustomerItemMetadata.ColumnNames.TransactionNo);
            txtTransactionDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, InvoiceCustomerItemMetadata.ColumnNames.TransactionDate);
            txtAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.Amount));
            txtNotes.Text = (String)DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.Notes);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtAmount.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Amount Must Bigger than 0");
                return;
            }
        }


        #region Properties for return entry value
        public String TransactionNo
        {
            get { return txtTransactionNo.Text; }
        }
        public DateTime? TransactionDate
        {
            get { return txtTransactionDate.SelectedDate; }
        }
        public Decimal Amount
        {
            get { return Convert.ToDecimal(txtAmount.Value); }
        }
        public String Notes
        {
            get { return txtNotes.Text; }
        }
        #endregion

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
            InvoiceCustomerItemCollection collitem = (InvoiceCustomerItemCollection)Session["collInvoiceCustomerItem" + Request.UserHostName];
        }
    }
}
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.Receivable
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

            txtPaymentNo.Text = (String)DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.PaymentNo);
            txtPaymentDate.SelectedDate =
                    (DateTime)DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.PaymentDate);
            txtRegistrationNo.Text = (String)DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.RegistrationNo);
            txtPatientID.Text = (String)DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.PatientID);
            txtPatientName.Text = (String)DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.PatientName);
            txtAmount.Value =
                Convert.ToDouble(DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.Amount));
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
        public String PaymentNo
        {
            get { return txtPaymentNo.Text; }
        }
        public DateTime? PaymentDate
        {
            get { return txtPaymentDate.SelectedDate; }
        }
        public String RegistrationNo
        {
            get { return txtRegistrationNo.Text; }
        }
        public String PatientID
        {
            get { return txtPatientID.Text; }
        }
        public String PatientName
        {
            get { return txtPatientName.Text; }
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
            InvoicesItemCollection collitem = (InvoicesItemCollection)Session["PaymentItems" + Request.UserHostName];
        }
    }
}
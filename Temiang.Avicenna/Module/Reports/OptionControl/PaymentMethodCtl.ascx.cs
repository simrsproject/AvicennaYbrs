using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class PaymentMethodCtl : BaseOptionCtl
    {
        #region ComboBox

        protected void cboPaymentMethod_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.PaymentMethodItemRequestedByPaymentType((RadComboBox) sender, e.Text,
                                                             AppParameter.GetParameterValue(
                                                                 AppParameter.ParameterItem.PaymentTypePayment));
        }

        protected void cboPaymentMethod_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.PaymentMethodItemDataBound(e);
        }

        #endregion

        public override string ParameterCaption
        {
            get { return lblCaption.Text; }
            set { lblCaption.Text = value; }
        }

      

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_PaymentMethod", cboPaymentMethod.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}
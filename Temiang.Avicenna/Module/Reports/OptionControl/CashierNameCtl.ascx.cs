using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class CashierNameCtl : BaseOptionCtl
    {
        protected void cboEntry_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.UserCashierItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboEntry_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.UserCashierItemDataBound(e);
        }

        public override string ParameterCaption
        {
            get { return lblCaption.Text; }
            set { lblCaption.Text = value; }
        }

        public override string ReportSubTitle
        {
            get { return string.Format("Cashier : {0} [{1}]", cboEntry.Text, cboEntry.SelectedValue); }
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_UserID", cboEntry.SelectedValue);

            //Retun List
            return parameters;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {

        }

    }
}
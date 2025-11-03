using Telerik.Web.UI;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class EmployeeNameCtl : BaseOptionCtl
    {
        #region ComboBox
        protected void cboPersonID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.EmployeeNameItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.EmployeeNameItemDataBound(e);
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_PersonID", cboPersonID.SelectedValue);

            //Retun List
            return parameters;
        }

        public override string ParameterCaption
        {
            get { return lblCaption.Text; }
            set { lblCaption.Text = value; }
        }
        public override string ReportSubTitle
        {
            get
            {
                return string.Format("Employee : {0}", cboPersonID.Text);
            }
        }
    }
}
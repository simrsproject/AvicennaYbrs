using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class ReferralGroupReferralCtl : BaseOptionCtl
    {
        protected void cboReferralGroup_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested((RadComboBox)sender, "ReferralGroup", e.Text);
        }
        protected void cboReferralGroup_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }

        protected void cboReferralGroup_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboReferralID.Items.Clear();
            cboReferralID.SelectedValue = string.Empty;
            cboReferralID.Text = string.Empty;
        }

        protected void cboReferralID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ReferralItemsRequested((RadComboBox)sender, e.Text, cboReferralGroup.SelectedValue);
        }
        protected void cboReferralID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ReferralItemDataBound(e);
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_ReferralGroup", cboReferralGroup.SelectedValue);
            parameters.AddNew("p_ReferralID", cboReferralID.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}
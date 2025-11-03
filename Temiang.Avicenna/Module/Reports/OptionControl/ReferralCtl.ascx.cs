using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class ReferralCtl : BaseOptionCtl
    {
        #region ComboBox 
        protected void cboReferralID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ReferralItemsRequested((RadComboBox)sender, e.Text, string.Empty);
        }
        protected void cboReferralID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ReferralItemDataBound(e);
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_ReferralID", cboReferralID.SelectedValue);

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
                return string.Format("Referral : {0}", cboReferralID.SelectedValue);
            }
        }
    }
}
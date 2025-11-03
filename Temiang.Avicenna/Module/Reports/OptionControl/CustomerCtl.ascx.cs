using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class CustomerCtl : BaseOptionCtl
    {

        #region ComboBox 
        protected void cboCustomer_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.CustomerItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboCustomerID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.CustomerItemDataBound(e);
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_CustomerID", cboCustomerID.SelectedValue);

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
                return string.Format("Customer : {0}", cboCustomerID.SelectedValue);
            }
        }

    }
}
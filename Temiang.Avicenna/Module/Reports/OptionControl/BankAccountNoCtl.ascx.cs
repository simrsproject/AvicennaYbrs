using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;


namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class BankAccountNoCtl : BaseOptionCtl
    {
        #region ComboBox
        protected void cboAccountNo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested((RadComboBox)sender, "BankAccountNo", e.Text);
        }
        protected void cboAccountNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_AccountNo", cboAccountNo.SelectedValue);

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
                return string.Format("Account No : {0}", cboAccountNo.SelectedValue);
            }
        }
    }
}
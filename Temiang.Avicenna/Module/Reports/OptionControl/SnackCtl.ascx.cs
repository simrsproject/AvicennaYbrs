using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class SnackCtl : BaseOptionCtl
    {
        #region ComboBox
        protected void cboSnackID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.SnacksRequested((RadComboBox)sender, e.Text);
        }

        protected void cboSnackID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.SnackItemDataBound(e);
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_SnackID", cboSnackID.SelectedValue);

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
                return string.Format("Snack : {0}", cboSnackID.SelectedValue);
            }
        }
    }
}
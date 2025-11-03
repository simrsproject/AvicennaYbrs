using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class SMFCtl : BaseOptionCtl
    {

        #region ComboBox 
        protected void cboSMF_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.SMFItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboSMFID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.SMFItemDataBound(e);
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_SMFID", cboSMFID.SelectedValue);

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
                return string.Format("SMF : {0}", cboSMFID.SelectedValue);
            }
        }

    }
}
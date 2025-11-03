using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class EDCCtl : BaseOptionCtl
    {

        #region ComboBox 
        protected void cboEDCMachineID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.EDCItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboEDCMachineID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.EDCItemDataBound(e);
            
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_EDCMachineID", cboEDCMachineID.SelectedValue);

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
                return string.Format("EDCMachine : {0}", cboEDCMachineID.SelectedValue);
            }
        }

    }
}
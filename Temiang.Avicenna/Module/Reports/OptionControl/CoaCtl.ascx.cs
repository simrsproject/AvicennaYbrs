using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class CoaCtl : BaseOptionCtl
    {

        #region ComboBox 
        protected void cboChartOfAccount_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ChartOfAccountItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboChartOfAccountCode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ChartOfAccountItemDataBound(e);
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_ChartOfAccountCode", cboChartOfAccountCode.SelectedValue);

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
                return string.Format("Chart Of Account : {0}", cboChartOfAccountCode.SelectedValue);
            }
        }

    }
}
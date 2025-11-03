using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class pCodeBetween : BaseOptionCtl
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
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("pCodeBetween_Start", cboChartOfAccountCode.SelectedValue);
            parameters.AddNew("pCodeBetween_End", cboChartOfAccountCode.SelectedValue);

            //Retun List
            return parameters;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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
                return string.Format("Chart of Accounts : {0} to {1}", cboChartOfAccountCode.SelectedValue, cboChartOfAccountCode.SelectedValue);
            }
        }
    }
}
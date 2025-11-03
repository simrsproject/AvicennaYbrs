using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class CodeCtl : BaseOptionCtl
    {
        #region ComboBox
        protected void cboChartOfAccountCodeStart_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ChartOfAccountItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboChartOfAccountCodeStart_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ChartOfAccountItemDataBound(e);
        }
        protected void cboChartOfAccountCodeEnd_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ChartOfAccountItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboChartOfAccountCodeEnd_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ChartOfAccountItemDataBound(e);
        }

        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("pCodeBetween_Start", cboChartOfAccountCodeStart.SelectedValue);
            parameters.AddNew("pCodeBetween_End", cboChartOfAccountCodeEnd.SelectedValue);

            //Retun List
            return parameters;
        }


        public override string ParameterCaption
        {
            get { return lblParameterCaption.Text; }
            set { lblParameterCaption.Text = value; }
        }
        public override string ReportSubTitle
        {
            get
            {
                return string.Format("Chart of Accounts : {0} to {1}", cboChartOfAccountCodeStart.SelectedValue, cboChartOfAccountCodeEnd.SelectedValue);
            }
        }
    }
}
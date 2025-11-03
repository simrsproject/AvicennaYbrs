using System;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;


namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class MonthCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateMonthComboBox(cboMonth);

            for (int i = DateTime.Now.Year; i > DateTime.Now.Year - 10; i--)
            {
                cboYear.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
            }

            if (cboYear.Text.Trim() == "")
            {
                cboYear.SelectedValue = DateTime.Now.Year.ToString();
                cboMonth.SelectedValue = DateTime.Now.ToString("MM");
            }
        }

        private void PopulateMonthComboBox(RadComboBox cbo)
        {
            cbo.Items.Add(new RadComboBoxItem("January", "01"));
            cbo.Items.Add(new RadComboBoxItem("February", "02"));
            cbo.Items.Add(new RadComboBoxItem("March", "03"));
            cbo.Items.Add(new RadComboBoxItem("April", "04"));
            cbo.Items.Add(new RadComboBoxItem("May", "05"));
            cbo.Items.Add(new RadComboBoxItem("June", "06"));
            cbo.Items.Add(new RadComboBoxItem("July", "07"));
            cbo.Items.Add(new RadComboBoxItem("August", "08"));
            cbo.Items.Add(new RadComboBoxItem("September", "09"));
            cbo.Items.Add(new RadComboBoxItem("October", "10"));
            cbo.Items.Add(new RadComboBoxItem("November", "11"));
            cbo.Items.Add(new RadComboBoxItem("December", "12"));
        }
        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_Month", cboMonth.SelectedValue);
            parameters.AddNew("p_Year", cboYear.SelectedValue);
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
                return "";// string.Format("Period : {0} {1}", cboPeriodMonth.Text, cboPeriodYear.Text);
            }
        }
    }
}
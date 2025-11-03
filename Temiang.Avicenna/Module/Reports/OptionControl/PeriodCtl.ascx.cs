using System;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class PeriodCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cboPeriodMonth.Items.Add(new RadComboBoxItem("January", "01"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("February", "02"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("March", "03"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("April", "04"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("May", "05"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("June", "06"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("July", "07"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("August", "08"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("September", "09"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("October", "10"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("November", "11"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("December", "12"));

                for (int i = DateTime.Now.Year; i > DateTime.Now.Year - 30; i--)
                {
                    cboPeriodYear.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
                }

                cboPeriodYear.SelectedValue = DateTime.Now.Year.ToString();
                cboPeriodMonth.SelectedValue = DateTime.Now.ToString("MM");
            }
        }
        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_PeriodMonth", cboPeriodMonth.SelectedValue);
            parameters.AddNew("p_PeriodYear", cboPeriodYear.SelectedValue);
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
                return string.Format("Period : {0} {1}", cboPeriodMonth.Text, cboPeriodYear.Text);
            }
        }

    }
}
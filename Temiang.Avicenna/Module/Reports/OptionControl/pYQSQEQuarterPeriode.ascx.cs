using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class pYQSQEQuarterPeriode : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Value initialized
            if (!Page.IsPostBack)
            {
                InitYear();
            }
        }

        private void InitYear()
        {
            pYQSQEQuarterPeriode_QuarterStart.Items.Clear();
            pYQSQEQuarterPeriode_QuarterEnd.Items.Clear();
            pYQSQEQuarterPeriode_Year.Items.Clear();

            pYQSQEQuarterPeriode_QuarterStart.Items.Add(new ListItem("Q1", "01"));
            pYQSQEQuarterPeriode_QuarterStart.Items.Add(new ListItem("Q2", "02"));
            pYQSQEQuarterPeriode_QuarterStart.Items.Add(new ListItem("Q3", "03"));
            pYQSQEQuarterPeriode_QuarterStart.Items.Add(new ListItem("Q4", "04"));

            pYQSQEQuarterPeriode_QuarterEnd.Items.Add(new ListItem("Q1", "01"));
            pYQSQEQuarterPeriode_QuarterEnd.Items.Add(new ListItem("Q2", "02"));
            pYQSQEQuarterPeriode_QuarterEnd.Items.Add(new ListItem("Q3", "03"));
            pYQSQEQuarterPeriode_QuarterEnd.Items.Add(new ListItem("Q4", "04"));

            foreach (ChartOfAccountBalances balance in ChartOfAccountBalances.GetDistinctYear())
                pYQSQEQuarterPeriode_Year.Items.Add(new ListItem(balance.Year, balance.Year));

            if (pYQSQEQuarterPeriode_Year.Items.Count == 0)
                pYQSQEQuarterPeriode_Year.Items.Add(new ListItem("-- Select One --", ""));

        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();

            string year = string.Empty;
            string q1 = string.Empty;
            string q2 = string.Empty;

            if (!string.IsNullOrEmpty(pYQSQEQuarterPeriode_Year.SelectedValue))
                year = pYQSQEQuarterPeriode_Year.SelectedValue;

            if (!string.IsNullOrEmpty(pYQSQEQuarterPeriode_QuarterStart.SelectedValue))
                q1 = pYQSQEQuarterPeriode_QuarterStart.SelectedValue;

            if (!string.IsNullOrEmpty(pYQSQEQuarterPeriode_QuarterEnd.SelectedValue))
                q2 = pYQSQEQuarterPeriode_QuarterEnd.SelectedValue;

            parameters.AddNew("pYQSQEQuarterPeriode_Year", year);
            parameters.AddNew("pYQSQEQuarterPeriode_QuarterStart", q1);
            parameters.AddNew("pYQSQEQuarterPeriode_QuarterEnd", q2);

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
                return string.Format("Quarter : {0}  {1}-{2}", pYQSQEQuarterPeriode_Year.SelectedItem, pYQSQEQuarterPeriode_QuarterStart.SelectedValue, pYQSQEQuarterPeriode_QuarterEnd.SelectedValue);
            }
        }
    }
}
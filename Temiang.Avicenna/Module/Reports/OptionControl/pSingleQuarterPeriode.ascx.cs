using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class pSingleQuarterPeriode : BaseOptionCtl
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
            pSingleQuarterPeriode_Quarter.Items.Clear();
            pSingleQuarterPeriode_Year.Items.Clear();

            pSingleQuarterPeriode_Quarter.Items.Add(new ListItem("Q1", "01"));
            pSingleQuarterPeriode_Quarter.Items.Add(new ListItem("Q2", "02"));
            pSingleQuarterPeriode_Quarter.Items.Add(new ListItem("Q3", "03"));
            pSingleQuarterPeriode_Quarter.Items.Add(new ListItem("Q4", "04"));

       

            //foreach (ChartOfAccountBalances balance in ChartOfAccountBalances.GetDistinctYear())
            //    pSingleQuarterPeriode_Year.Items.Add(new ListItem(balance.Year, balance.Year));

            if (pSingleQuarterPeriode_Year.Items.Count == 0)
                pSingleQuarterPeriode_Year.Items.Add(new ListItem("-- Select One --", ""));

            for (int i = 2012; i <= DateTime.Now.Year + 1; i++)
            {
                pSingleQuarterPeriode_Year.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();

            string year = string.Empty;
            string q = string.Empty;

            if (!string.IsNullOrEmpty(pSingleQuarterPeriode_Year.SelectedValue))
                year = pSingleQuarterPeriode_Year.SelectedValue;

            if (!string.IsNullOrEmpty(pSingleQuarterPeriode_Quarter.SelectedValue))
                q = pSingleQuarterPeriode_Quarter.SelectedValue;

            parameters.AddNew("pSingleQuarterPeriode_Year", year);
            parameters.AddNew("pSingleQuarterPeriode_Quarter", q);

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
                return string.Format("Quarter : {0} - {1}", pSingleQuarterPeriode_Year.SelectedItem, pSingleQuarterPeriode_Quarter.SelectedValue);
            }
        }
    }
}
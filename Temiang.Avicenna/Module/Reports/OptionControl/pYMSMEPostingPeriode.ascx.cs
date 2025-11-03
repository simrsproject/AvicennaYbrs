using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class pYMSMEPostingPeriode : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitYear();
                InitMonth();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            pYMSMEPostingPeriode_Year.SelectedIndexChanged += new EventHandler(pYMSMEPostingPeriode_Year_SelectedIndexChanged);
        }

        void pYMSMEPostingPeriode_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitMonth();
        }

        private void InitMonth()
        {
            pYMSMEPostingPeriode_MonthStart.Items.Clear();
            pYMSMEPostingPeriode_MonthEnd.Items.Clear();

            if (string.IsNullOrEmpty(pYMSMEPostingPeriode_Year.SelectedValue))
            {
                pYMSMEPostingPeriode_MonthStart.Items.Add(new ListItem("-- Select One --", ""));
                return;
            }

            foreach (ChartOfAccountBalances balance in ChartOfAccountBalances.GetDistinctMonth(pYMSMEPostingPeriode_Year.SelectedValue))
            {
                int monthINT = 0;
                int.TryParse(balance.Month, out monthINT);

                pYMSMEPostingPeriode_MonthStart.Items.Add(new ListItem(Helper.MonthName(monthINT), string.Format("{0:00}", monthINT)));
                pYMSMEPostingPeriode_MonthEnd.Items.Add(new ListItem(Helper.MonthName(monthINT), string.Format("{0:00}", monthINT)));
            }

            if (pYMSMEPostingPeriode_MonthStart.Items.Count == 0)
                pYMSMEPostingPeriode_MonthStart.Items.Add(new ListItem("-- Select One --", ""));

            if (pYMSMEPostingPeriode_MonthEnd.Items.Count == 0)
                pYMSMEPostingPeriode_MonthEnd.Items.Add(new ListItem("-- Select One --", ""));
        }

        private void InitYear()
        {
            pYMSMEPostingPeriode_MonthStart.Items.Clear();
            pYMSMEPostingPeriode_MonthEnd.Items.Clear();
            pYMSMEPostingPeriode_Year.Items.Clear();

            foreach (ChartOfAccountBalances balance in ChartOfAccountBalances.GetDistinctYear())
                pYMSMEPostingPeriode_Year.Items.Add(new ListItem(balance.Year, balance.Year));

            if (pYMSMEPostingPeriode_Year.Items.Count == 0)
                pYMSMEPostingPeriode_Year.Items.Add(new ListItem("-- Select One --", ""));

            InitMonth();
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection prms = new PrintJobParameterCollection();

            string year = string.Empty;
            string monthStart = string.Empty;
            string monthEnd = string.Empty;

            if (!string.IsNullOrEmpty(pYMSMEPostingPeriode_Year.SelectedValue))
                year = pYMSMEPostingPeriode_Year.SelectedValue;

            if (!string.IsNullOrEmpty(pYMSMEPostingPeriode_MonthStart.SelectedValue))
                monthStart = pYMSMEPostingPeriode_MonthStart.SelectedValue;

            if (!string.IsNullOrEmpty(pYMSMEPostingPeriode_MonthEnd.SelectedValue))
                monthEnd = pYMSMEPostingPeriode_MonthEnd.SelectedValue;

            prms.AddNew("pYMSMEPostingPeriode_Year", year);
            prms.AddNew("pYMSMEPostingPeriode_MonthStart", monthStart);
            prms.AddNew("pYMSMEPostingPeriode_MonthEnd", monthEnd);

            //Retun List
            return prms;
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
                return string.Format("Periode: {0}  {1} - {2}", pYMSMEPostingPeriode_Year.SelectedValue, pYMSMEPostingPeriode_MonthStart.SelectedValue, pYMSMEPostingPeriode_MonthEnd.SelectedValue);
            }
        }
    }
}
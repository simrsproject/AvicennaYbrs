using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class pYMSMEPostingPeriode2 : BaseOptionCtl
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
            pYMSMEPostingPeriode_YearStart.SelectedIndexChanged += new EventHandler(pYMSMEPostingPeriode_YearStart_SelectedIndexChanged);
            pYMSMEPostingPeriode_YearEnd.SelectedIndexChanged += new EventHandler(pYMSMEPostingPeriode_YearEnd_SelectedIndexChanged);
        }

        void pYMSMEPostingPeriode_YearStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitMonth();
        }

        void pYMSMEPostingPeriode_YearEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitMonth();
        }

        private void InitMonth()
        {
            pYMSMEPostingPeriode_Month.Items.Clear();

            if (string.IsNullOrEmpty(pYMSMEPostingPeriode_YearStart.SelectedValue))
            {
                pYMSMEPostingPeriode_Month.Items.Add(new ListItem("-- Select One --", ""));
                return;
            }

            if (string.IsNullOrEmpty(pYMSMEPostingPeriode_YearEnd.SelectedValue))
            {
                pYMSMEPostingPeriode_Month.Items.Add(new ListItem("-- Select One --", ""));
                return;
            }

            foreach (ChartOfAccountBalances balance in ChartOfAccountBalances.GetDistinctMonth(pYMSMEPostingPeriode_YearStart.SelectedValue))
            {
                int monthINT = 0;
                int.TryParse(balance.Month, out monthINT);

                pYMSMEPostingPeriode_Month.Items.Add(new ListItem(Helper.MonthName(monthINT), string.Format("{0:00}", monthINT)));
            }


            if (pYMSMEPostingPeriode_Month.Items.Count == 0)
                pYMSMEPostingPeriode_Month.Items.Add(new ListItem("-- Select One --", ""));
        }

        private void InitYear()
        {
            pYMSMEPostingPeriode_Month.Items.Clear();
            pYMSMEPostingPeriode_YearStart.Items.Clear();
            pYMSMEPostingPeriode_YearEnd.Items.Clear();

            foreach (ChartOfAccountBalances balance in ChartOfAccountBalances.GetDistinctYear())
                pYMSMEPostingPeriode_YearStart.Items.Add(new ListItem(balance.Year, balance.Year));

            if (pYMSMEPostingPeriode_YearStart.Items.Count == 0)
                pYMSMEPostingPeriode_YearStart.Items.Add(new ListItem("-- Select One --", ""));

            foreach (ChartOfAccountBalances balance in ChartOfAccountBalances.GetDistinctYear())
                pYMSMEPostingPeriode_YearEnd.Items.Add(new ListItem(balance.Year, balance.Year));

            if (pYMSMEPostingPeriode_YearEnd.Items.Count == 0)
                pYMSMEPostingPeriode_YearEnd.Items.Add(new ListItem("-- Select One --", ""));

            InitMonth();
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection prms = new PrintJobParameterCollection();

            string yearStart = string.Empty;
            string yearEnd = string.Empty;
            string month = string.Empty;

            if (!string.IsNullOrEmpty(pYMSMEPostingPeriode_YearStart.SelectedValue))
                yearStart = pYMSMEPostingPeriode_YearStart.SelectedValue;

            if (!string.IsNullOrEmpty(pYMSMEPostingPeriode_YearEnd.SelectedValue))
                yearEnd = pYMSMEPostingPeriode_YearEnd.SelectedValue;

            if (!string.IsNullOrEmpty(pYMSMEPostingPeriode_Month.SelectedValue))
                month = pYMSMEPostingPeriode_Month.SelectedValue;

            prms.AddNew("pYMSMEPostingPeriode_YearStart", yearStart);
            prms.AddNew("pYMSMEPostingPeriode_YearEnd", yearEnd);
            prms.AddNew("pYMSMEPostingPeriode_MonthEnd", month);

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
                return string.Format("Periode: {0}  {1} - {2}", pYMSMEPostingPeriode_YearStart.SelectedValue, pYMSMEPostingPeriode_YearEnd.SelectedValue, pYMSMEPostingPeriode_Month.SelectedValue);
            }
        }
    }
}
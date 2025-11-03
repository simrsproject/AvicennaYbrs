using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class pSinglePostingPeriode : BaseOptionCtl
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
            pSinglePostingPeriode_Year.SelectedIndexChanged += new EventHandler(pSinglePostingPeriode_Year_SelectedIndexChanged);
        }

        void pSinglePostingPeriode_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitMonth();
        }

        private void InitMonth()
        {
            pSinglePostingPeriode_Month.Items.Clear();

            if (string.IsNullOrEmpty(pSinglePostingPeriode_Year.SelectedValue))
            {
                pSinglePostingPeriode_Month.Items.Add(new ListItem("-- Select One --", ""));
                return;
            }

            foreach (ChartOfAccountBalances balance in ChartOfAccountBalances.GetDistinctMonth(pSinglePostingPeriode_Year.SelectedValue))
            {
                int monthINT = 0;
                int.TryParse(balance.Month, out monthINT);
                pSinglePostingPeriode_Month.Items.Add(new ListItem(Helper.MonthName(monthINT), string.Format("{0:00}", monthINT)));
            }

            if (pSinglePostingPeriode_Month.Items.Count == 0)
                pSinglePostingPeriode_Month.Items.Add(new ListItem("-- Select One --", ""));
        }

        private void InitYear()
        {
            pSinglePostingPeriode_Month.Items.Clear();
            pSinglePostingPeriode_Year.Items.Clear();

            foreach (ChartOfAccountBalances balance in ChartOfAccountBalances.GetDistinctYear())
                pSinglePostingPeriode_Year.Items.Add(new ListItem(balance.Year, balance.Year));

            if (pSinglePostingPeriode_Year.Items.Count == 0)
                pSinglePostingPeriode_Year.Items.Add(new ListItem("-- Select One --", ""));

            InitMonth();
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection prms = new PrintJobParameterCollection();
            prms.AddNew("pSinglePostingPeriode_Year", pSinglePostingPeriode_Year.SelectedValue);
            prms.AddNew("pSinglePostingPeriode_Month", pSinglePostingPeriode_Month.SelectedValue);

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
                return string.Format("Periode: {0} / {1}", pSinglePostingPeriode_Year.SelectedValue, pSinglePostingPeriode_Month.SelectedValue);
            }
        }
    }
}
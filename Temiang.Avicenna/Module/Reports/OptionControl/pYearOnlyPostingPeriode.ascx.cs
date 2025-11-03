using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class pYearOnlyPostingPeriode : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitYear();
            }
        }

        private void InitYear()
        {
            pYearOnlyPostingPeriode_Year.Items.Clear();

            foreach (ChartOfAccountBalances balance in ChartOfAccountBalances.GetDistinctYear())
                pYearOnlyPostingPeriode_Year.Items.Add(new ListItem(balance.Year, balance.Year));

            if (pYearOnlyPostingPeriode_Year.Items.Count == 0)
                pYearOnlyPostingPeriode_Year.Items.Add(new ListItem("-- Select One --", ""));

        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection prms = new PrintJobParameterCollection();
            string year = string.Empty;

            if (!string.IsNullOrEmpty(pYearOnlyPostingPeriode_Year.SelectedValue))
                year = pYearOnlyPostingPeriode_Year.SelectedValue;

            prms.AddNew("pYearOnlyPostingPeriode_Year", year);

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
                return string.Format("Periode: {0}", pYearOnlyPostingPeriode_Year.SelectedValue);
            }
        }
    }
}
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Finance.RSSA
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for IncomeReceivePrescSummaryRpt.
    /// </summary>
    public partial class IncomeReceivePrescSummaryRpt : Report
    {
        public IncomeReceivePrescSummaryRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            String p_month = printJobParameters.FindByParameterName("p_PeriodMonth").ValueString;
            String p_year = printJobParameters.FindByParameterName("p_PeriodYear").ValueString;

            txtPeriod.Value = "Bulan : " + string.Format(Convert.ToDateTime(p_month + "/01/" + p_year).ToString("MMMM"));

        }
    }
}
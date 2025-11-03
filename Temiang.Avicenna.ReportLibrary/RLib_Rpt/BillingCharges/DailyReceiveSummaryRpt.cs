namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges
{
    using BusinessObject;
    using System.Data;
    using System;

    /// <summary>
    /// Summary description for DailyReceiveSummaryRpt.
    /// </summary>
    public partial class DailyReceiveSummaryRpt : Telerik.Reporting.Report
    {
        public DailyReceiveSummaryRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(reportHeaderSection1);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);
            table1.DataSource = dt;

            this.DataSource = dt;

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;
            textBox1.Value = string.Format("Tanggal {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);

        }
    }
}
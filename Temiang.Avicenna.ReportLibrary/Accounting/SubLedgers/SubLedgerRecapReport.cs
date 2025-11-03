namespace Temiang.Avicenna.ReportLibrary.Accounting.SubLedgers
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Temiang.Avicenna.BusinessObject.Util;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.ReportLibrary;

    /// <summary>
    /// Journal Report Document.
    /// </summary>
    public partial class SubLedgerRecapReport : Telerik.Reporting.Report
    {
        private ReportDataSource reportDataSource = new ReportDataSource();

        public SubLedgerRecapReport(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            SetupReport(printJobParameters);

            this.ctSummary.DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
        }


        private void SetupReport(PrintJobParameterCollection printJobParameters)
        {
            object year = printJobParameters.FindByParameterName("pYearOnlyPostingPeriode_Year").ValueString;

            this.txtPeriode.Value = string.Format("Periode: {0}", year);

            var healthcare = Healthcare.GetHealthcare();
            this.txtCompanyName.Value = healthcare.HealthcareName;
        }
    }
}
namespace Temiang.Avicenna.ReportLibrary.Accounting.Journals
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
    public partial class JournalListReport : Telerik.Reporting.Report
    {
        private ReportDataSource reportDataSource = new ReportDataSource();

        public JournalListReport(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            SetupReport(printJobParameters);

            this.DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
        }

        private void SetupReport(PrintJobParameterCollection printJobParameters)
        {
            DateTime? dateStart = printJobParameters.FindByParameterName("pDateBetween_Start").ValueDateTime;
            DateTime? dateEnd = printJobParameters.FindByParameterName("pDateBetween_End").ValueDateTime;

            this.txtPeriode.Value = string.Format("Periode: {0}", Utility.PeriodeTitle(dateStart.Value, dateEnd.Value));

            var healthcare = Healthcare.GetHealthcare();
            this.txtCompanyName.Value = healthcare.HealthcareName;
        }
    }
}
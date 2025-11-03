
namespace Temiang.Avicenna.ReportLibrary.Accounting.Cash
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Data;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Temiang.Avicenna.BusinessObject.Util;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.ReportLibrary;

    /// <summary>
    /// Journal Report Document.
    /// </summary>
    public partial class CashTransactionPeriodeReport : Telerik.Reporting.Report
    {
        private ReportDataSource reportDataSource = new ReportDataSource();

        public CashTransactionPeriodeReport(string programID, PrintJobParameterCollection printJobParameters)
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
            var healthcare = Healthcare.GetHealthcare();
            this.txtCompanyName.Value = healthcare.HealthcareName;

            this.txtPeriode_Value.Value = string.Format("{0} to {1}", printJobParameters[0].ValueDateTime.Value.ToShortDateString(), printJobParameters[1].ValueDateTime.Value.ToShortDateString());
            var bank = Bank.Get(printJobParameters[2].ValueString);
            if (bank != null)
            {
                this.txtBankAccount_Value.Value = string.Format("{0} - {1}", bank.BankName, bank.NoRek);
            }

            this.txtReportTitle.Value = "Cash Transaction Periode";
        }
    }
}
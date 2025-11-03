namespace Temiang.Avicenna.ReportLibrary.Charges.RSUTAMA
{
    using System;
    using Temiang.Avicenna.BusinessObject;

    /// <summary>
    /// Summary description for DailyReceiptCashRpt.
    /// </summary>
    public partial class DailyReceiptCashRpt : Telerik.Reporting.Report
    {
        public DailyReceiptCashRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            Helper.InitializeLogo(this.pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDateTime").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDateTime").ValueDateTime;

            txtPeriod.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy HH:mm} s/d {1:dd-MMMM-yyyy HH:mm}", fromDate, toDate);
        }
    }
}
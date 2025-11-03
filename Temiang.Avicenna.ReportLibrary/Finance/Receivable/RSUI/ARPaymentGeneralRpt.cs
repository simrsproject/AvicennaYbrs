namespace Temiang.Avicenna.ReportLibrary.Finance.Receivable.RSUI
{
    using System;
    using Temiang.Avicenna.BusinessObject;

    /// <summary>
    /// Summary description for ARPaymentRpt.
    /// </summary>
    public partial class ARPaymentGeneralRpt : Telerik.Reporting.Report
    {
        public ARPaymentGeneralRpt(string programID, PrintJobParameterCollection printJobParameters)
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

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            txtPeriode.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
        }
    }
}
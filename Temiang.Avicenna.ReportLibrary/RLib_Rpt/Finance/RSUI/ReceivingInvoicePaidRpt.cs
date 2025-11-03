namespace Temiang.Avicenna.ReportLibrary.Rlib_Rpt.Finance.RSUI
{
    using BusinessObject;
    using System;
    using System.Data;
    
    /// <summary>
    /// Summary description for ReceivingInvoicePaidRpt.
    /// </summary>
    public partial class ReceivingInvoicePaidRpt : Telerik.Reporting.Report
    {
        public ReceivingInvoicePaidRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            textBox24.Value = string.Format("Periode : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
        }
    }
}
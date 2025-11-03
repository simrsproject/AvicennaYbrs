namespace Temiang.Avicenna.ReportLibrary.Charges
{
    using System;
    using Telerik.Reporting;
    using Temiang.Avicenna.BusinessObject;


    /// <summary>
    /// Summary description for DirectPrescriptionReturnRpt.
    /// </summary>
    public partial class DirectPrescriptionReturnRpt : Report
    {
        public DirectPrescriptionReturnRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeDataSource(this, programID, printJobParameters);
            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;


            textBox2.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}
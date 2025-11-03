namespace Temiang.Avicenna.ReportLibrary.Finance.Payable.RSUSKY
{
    using System;
    using Temiang.Avicenna.BusinessObject;


    /// <summary>
    /// Summary description for APMutationRecapRpt.
    /// </summary>
    public partial class APMutationRecapRpt : Telerik.Reporting.Report
    {
        public APMutationRecapRpt(string programID, PrintJobParameterCollection printJobParameters)
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
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}
namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges
{
    using BusinessObject;
    using System;

    /// <summary>
    /// Summary description for RekapSalesMedicalProductByPatientNoGuar.
    /// </summary>
    public partial class RekapSalesMedicalProductByPatientNoGuar : Telerik.Reporting.Report
    {
        public RekapSalesMedicalProductByPatientNoGuar(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);
            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            textBox3.Value = string.Format("Periode : {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);
        }
    }
}
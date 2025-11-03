namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges
{
    using BusinessObject;
    using System.Data;
    using System;

    public partial class PaymentByCardRpt : Telerik.Reporting.Report
    {
        public PaymentByCardRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(pageHeaderSection1);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);
            table1.DataSource = dt;

            this.DataSource = dt;

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            txtPeriod.Value = string.Format("Periode : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
        }
    }
}
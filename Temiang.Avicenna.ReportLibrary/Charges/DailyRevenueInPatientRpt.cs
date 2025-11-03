using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.Charges
{
    /// <summary>
    /// Summary description for DailyRevenueInPatientRpt.
    /// </summary>
    public partial class DailyRevenueInPatientRpt : Report
    {
        public DailyRevenueInPatientRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            // Test Data
            //programID = "TransactionRpt";
            //printJobParameters.AddNew("p_FromDate", new System.DateTime(2010, 08, 01));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 08, 25));

            // End Test Data

            InitializeComponent();

            Helper.InitializeLogo(pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);
            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            txtPeriod.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
            TotalPeriod.Value = string.Format("Total Per Periode : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);

        }
    }
}
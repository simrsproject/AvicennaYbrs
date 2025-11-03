using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.Charges
{
    /// <summary>
    /// Summary description for DailyCashierEmergencyRpt.
    /// </summary>
    public partial class DailyCashierEmergencyRpt : Report
    {
        public DailyCashierEmergencyRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            // Test Data

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
namespace Temiang.Avicenna.ReportLibrary.Accounting.Cash
{
    using System;
    using Telerik.Reporting;
    using Temiang.Avicenna.BusinessObject;


    public partial class TreasureOfAcceptance : Telerik.Reporting.Report
    {
        public TreasureOfAcceptance(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

          //  txtPeriod.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
 
        }
    }
}
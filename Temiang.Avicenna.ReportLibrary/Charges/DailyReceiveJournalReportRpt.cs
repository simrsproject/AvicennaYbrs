using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.Charges
{
    using System;

    public partial class DailyReceiptJournalReportRpt : Telerik.Reporting.Report
    {
        public DailyReceiptJournalReportRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //programID = "DailyReceiptJournalReportRpt";
            //printJobParameters.AddNew("p_FromDateTime", new System.DateTime(2010, 7, 22, 0, 0, 0));
            //printJobParameters.AddNew("p_ToDateTime", new System.DateTime(2010, 7, 22, 0, 0, 0));
            //----------------


            InitializeComponent();

            Helper.InitializeLogo(this.pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);         

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDateTime").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDateTime").ValueDateTime;

            txtPeriod.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy HH:mm} s/d {1:dd-MMMM-yyyy HH:mm}", fromDate, toDate);

        }

    }
}
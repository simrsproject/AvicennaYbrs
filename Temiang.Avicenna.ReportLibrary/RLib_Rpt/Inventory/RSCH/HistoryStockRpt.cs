namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.RSCH
{

    using System;
    using Temiang.Avicenna.BusinessObject;

    /// <summary>
    /// Summary description for StockCardRpt.
    /// </summary>
    public partial class HistoryStockRpt : Telerik.Reporting.Report
    {
        public HistoryStockRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate", new System.DateTime(2010, 04, 29));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 06, 08));
            //----------------

            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            Helper.InitializeLogo(this.pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            textBox3.Value = string.Format("{0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
        }
    }
}
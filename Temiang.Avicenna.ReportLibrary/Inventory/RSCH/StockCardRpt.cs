namespace Temiang.Avicenna.ReportLibrary.Inventory.RSCH
{
    using System;
    using Temiang.Avicenna.BusinessObject;

    /// <summary>
    /// Summary description for StockCardRpt.
    /// </summary>
    public partial class StockCardRpt : Telerik.Reporting.Report
    {
        public StockCardRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate", new System.DateTime(2010, 04, 29));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 06, 08));
            //----------------

            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            Helper.InitializeDataSource(this, programID, printJobParameters);
        }
    }
}
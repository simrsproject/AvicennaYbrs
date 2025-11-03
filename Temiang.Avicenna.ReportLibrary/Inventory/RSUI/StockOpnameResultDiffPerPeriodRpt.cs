using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.ReportLibrary.Inventory.RSUI
{
    /// <summary>
    /// Summary description for StockOpnameResultRpt.
    /// </summary>
    public partial class StockOpnameResultDiffPerPeriodRpt : Report
    {
        public StockOpnameResultDiffPerPeriodRpt(string programID, PrintJobParameterCollection printJobParameters)
        {

            //Test Parameter
            //programID = "StockOpnResultRpt";
            //printJobParameters.AddNew("p_TransactionNo", "ST/1006-0005");
            //----------------

            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);
            

            var reportDataSource = new ReportDataSource();
        

                DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
                DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
                DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;
                textBox45.Value = string.Format("Periode : {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);
            
        }
    }
}
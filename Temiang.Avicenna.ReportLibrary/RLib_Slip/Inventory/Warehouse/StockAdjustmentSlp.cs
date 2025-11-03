using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System.Data;
using Temiang.Avicenna.Common;
using System;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Warehouse
{
    
    public partial class StockAdjustmentSlp : Telerik.Reporting.Report
    {
        public StockAdjustmentSlp(string programID, PrintJobParameterCollection printJobParameters)
        {
            
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);

            var reportDataSource = new ReportDataSource();
            DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters[0]);
            DataSource = tbl;
        }
    }
}
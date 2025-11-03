using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System.Data;
using Temiang.Avicenna.Common;
using System;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Procurement.RSMP
{
    
    public partial class PurchaseOrderWithoutPrice : Telerik.Reporting.Report
    {
        public PurchaseOrderWithoutPrice(string programID, PrintJobParameterCollection printJobParameters)
        {
            
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);
            var reportDataSource = new ReportDataSource();
            DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters[0]);
            DataSource = tbl;
        }
    }
}
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System.Data;
using System;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Procurement.PAC
{
   
    public partial class PurchaseRequest : Telerik.Reporting.Report
    {
        public PurchaseRequest(string programID, PrintJobParameterCollection printJobParameters)
        {
            
            InitializeComponent();
            var reportDataSource = new ReportDataSource();
            DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters[0]);
            DataSource = tbl;
        }
    }
}
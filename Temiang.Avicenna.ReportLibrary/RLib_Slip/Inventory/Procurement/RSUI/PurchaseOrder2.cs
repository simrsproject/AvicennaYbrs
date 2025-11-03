using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System.Data;
using Temiang.Avicenna.Common;
using System;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Procurement.RSUI
{
    
    public partial class PurchaseOrderDir : Telerik.Reporting.Report
    {
        public PurchaseOrderDir(string programID, PrintJobParameterCollection printJobParameters)
        {
            
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);

            var healthcare = Healthcare.GetHealthcare();
            
            var reportDataSource = new ReportDataSource();
            DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters[0]);
            DataSource = tbl;
        }
    }
}
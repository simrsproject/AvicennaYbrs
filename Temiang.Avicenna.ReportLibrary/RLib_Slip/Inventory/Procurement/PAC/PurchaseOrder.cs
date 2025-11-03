using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System.Data;
using System;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Procurement.PAC
{
    
    public partial class PurchaseOrder : Telerik.Reporting.Report
    {
        public PurchaseOrder(string programID, PrintJobParameterCollection printJobParameters)
        {
            
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);
            var reportDataSource = new ReportDataSource();
            DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters[0]);
            DataSource = tbl;

            var healthcare = Healthcare.GetHealthcare();
            //textBox16.Value = healthcare.FoundationName;
            textBox53.Value = healthcare.FoundationName;
            //textBox44.Value = healthcare.AddressLine1;
            //textBox45.Value = healthcare.AddressLine2 + " " + healthcare.City + " " + healthcare.ZipCode;
            
        }
    }
}
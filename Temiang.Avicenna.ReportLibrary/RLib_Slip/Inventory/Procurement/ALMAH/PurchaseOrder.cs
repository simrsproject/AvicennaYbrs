using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System.Data;
using System;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Procurement.ALMAH
{

    /// <summary>
    /// Summary description for BuktiDistribusiItem.
    /// </summary>
    public partial class PurchaseOrder : Telerik.Reporting.Report
    {
        public PurchaseOrder(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            var reportDataSource = new ReportDataSource();
            DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters[0]);
            DataSource = tbl;
            table2.DataSource = tbl;
            var healthcare = Healthcare.GetHealthcare();
            
            
            textBox32.Value = healthcare.City + ", ";
            textBox38.Value = healthcare.AddressLine1;
            textBox46.Value = "Phone " + healthcare.PhoneNo;
            textBox49.Value = Temiang.Avicenna.ReportLibrary.Properties.Resources.LogoRSALMAH;
        }
    }
}

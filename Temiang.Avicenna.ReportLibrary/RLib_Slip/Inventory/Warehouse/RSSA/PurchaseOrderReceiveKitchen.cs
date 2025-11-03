using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System.Data;
using System;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Warehouse.RSSA
{

    /// <summary>
    /// Summary description for PurchaseOrderKitchen.
    /// </summary>
    public partial class PurchaseOrderReceiveKitchen : Telerik.Reporting.Report
    {
        public PurchaseOrderReceiveKitchen(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeader);
            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
            table2.DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
        }

    }
}
       
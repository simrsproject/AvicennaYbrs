using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System.Data;
using System;

namespace Temiang.Avicenna.ReportLibrary.Inventory
{

    /// <summary>
    /// Summary description for BuktiDistribusiItem.
    /// </summary>
    public partial class PurchaseOrderSlipRpt : Telerik.Reporting.Report
    {
        public PurchaseOrderSlipRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeLogo(reportHeader);
          //  textBox53.Value = printJobParameters[1].ValueString;

            var reportDataSource = new ReportDataSource();
          //  DataSource = reportDataSource.GetDataTable(programID, printJobParameters[0]);
                   DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters[0]);
                   DataSource = tbl;

                   textBox44.Value = Convert.ToString( tbl.Rows[0]["status"]);
        }
    }
}
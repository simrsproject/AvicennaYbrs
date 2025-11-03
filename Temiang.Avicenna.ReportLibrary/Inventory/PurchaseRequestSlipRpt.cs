using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System;
using System.Data;

namespace Temiang.Avicenna.ReportLibrary.Inventory
{
    /// <summary>
    /// Summary description for BuktiDistribusiItem.
    /// </summary>
    public partial class PurchaseRequestSlipRpt : Telerik.Reporting.Report
    {
        public PurchaseRequestSlipRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);
            var reportDataSource = new ReportDataSource();
            DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters);
            DataSource = tbl;

            //textBox17.Value = Convert.ToString(tbl.Rows[0]["status"]);
        }
    }
}
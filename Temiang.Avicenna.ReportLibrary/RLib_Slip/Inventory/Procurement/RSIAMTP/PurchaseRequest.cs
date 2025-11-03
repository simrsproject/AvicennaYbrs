using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System;
using System.Data;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Procurement.RSIAMTP
{

    /// <summary>
    /// Summary description for PurchaseRequest.
    /// </summary>
    public partial class PurchaseRequest : Telerik.Reporting.Report
    {
        public PurchaseRequest(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);
            var reportDataSource = new ReportDataSource();
            DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters);
            DataSource = tbl;

        }
    }
}
using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Warehouse.RSCH
{
    /// <summary>
    /// Summary description for BuktiDistribusiItem.
    /// </summary>
    public partial class DistributionRequest : Report
    {
        public DistributionRequest(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);

            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
        }
    }
}
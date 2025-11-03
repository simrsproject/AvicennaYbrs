using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.Inventory.Warehouse.RSCH
{
    /// <summary>
    /// Summary description for Distribution.
    /// </summary>
    public partial class Distribution : Report
    {
        public Distribution(string programID, PrintJobParameterCollection printJobParameters)
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
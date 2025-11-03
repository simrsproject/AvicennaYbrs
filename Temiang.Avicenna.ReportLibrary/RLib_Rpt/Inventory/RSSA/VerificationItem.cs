using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Rlib_Rpt.Inventory.Warehouse.RSSA
{

    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    /// <summary>
    /// Summary description for VerificationItem
    /// </summary>
    public partial class VerificationItem : Telerik.Reporting.Report
    {
        public VerificationItem(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);
            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
            //table2.DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
            table1.DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            textBox19.Value = string.Format("Tanggal : {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);

        }
    }
}
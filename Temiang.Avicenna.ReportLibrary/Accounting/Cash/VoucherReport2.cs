namespace Temiang.Avicenna.ReportLibrary.Accounting.Cash
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Temiang.Avicenna.BusinessObject.Util;
    using Temiang.Avicenna.BusinessObject;

    /// <summary>
    /// Summary description for VoucherReport2.
    /// </summary>
    public partial class VoucherReport2 : Telerik.Reporting.Report
    {
        private ReportDataSource reportDataSource = new ReportDataSource();

        public VoucherReport2(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
         
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeaderSection1);
          //  SetupReport(printJobParameters);

            this.DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
        }
    }
}
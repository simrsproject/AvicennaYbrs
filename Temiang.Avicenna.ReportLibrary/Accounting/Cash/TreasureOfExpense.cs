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
    using Temiang.Avicenna.ReportLibrary;

    /// <summary>
    /// Summary description for TreasureOfExpense.
    /// </summary>
    public partial class TreasureOfExpense : Telerik.Reporting.Report
    {
        private ReportDataSource reportDataSource = new ReportDataSource();

        public TreasureOfExpense(string programID, PrintJobParameterCollection printJobParameters)
        {
         

            InitializeComponent();
            Helper.InitializeLogo(pageHeader);

            this.DataSource = reportDataSource.GetDataTable(programID, printJobParameters);

        }
    }
}
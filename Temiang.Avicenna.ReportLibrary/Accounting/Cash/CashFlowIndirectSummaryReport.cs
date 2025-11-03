using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.Accounting.Cash
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Processing = Telerik.Reporting.Processing;
    using Temiang.Avicenna.BusinessObject.Util;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.ReportLibrary;

    /// <summary>
    /// Journal Report Document.
    /// </summary>
    public partial class CashFlowIndirectSummaryReport : Telerik.Reporting.Report
    {
        private ReportDataSource reportDataSource = new ReportDataSource();

        public CashFlowIndirectSummaryReport(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            this.ghsActivity.ItemDataBound += new EventHandler(ghsActivity_ItemDataBound);
            this.ghsNormalBalance.ItemDataBound += new EventHandler(ghsNormalBalance_ItemDataBound);
            this.ghsCategory.ItemDataBound += new EventHandler(ghsCategory_ItemDataBound);

            //System.Data.DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters);
            string year = printJobParameters[0].ValueString;
            string month= printJobParameters[1].ValueString;
            decimal beginingBalance = 0;
            decimal endBalance = 0;

            AppProgram appProgram = new AppProgram();
            appProgram.LoadByPrimaryKey(programID);
            if (string.IsNullOrEmpty(appProgram.StoreProcedureName))
            {
                throw new Exception("Store procedure name not defined in program setting");
            }

            System.Data.DataTable tbl = CashTransaction.CashFlowIndirectReportDataSource(
                appProgram.StoreProcedureName, month, year, out beginingBalance, out endBalance);

            SetupReport(printJobParameters);

            txtTotalBeginingBalance.Value = beginingBalance.ToString("n2");
            txtTotalEndingBalance.Value = endBalance.ToString("n2");

            this.DataSource = tbl;
        }

         

        void ghsCategory_ItemDataBound(object sender, EventArgs e)
        {
        }

        void ghsNormalBalance_ItemDataBound(object sender, EventArgs e)
        {
        }

        void ghsActivity_ItemDataBound(object sender, EventArgs e)
        {
        }

        private void SetupReport(PrintJobParameterCollection printJobParameters)
        {
            string month  = printJobParameters.FindByParameterName("pSinglePostingPeriode_Month").ValueString;
            string year = printJobParameters.FindByParameterName("pSinglePostingPeriode_Year").ValueString; 

            this.txtPeriode.Value = string.Format("Periode: {0} - {1}", Utility.MonthName(month), year);

            var healthcare = Healthcare.GetHealthcare();
            this.txtCompanyName.Value = healthcare.HealthcareName;
        }
    }
}
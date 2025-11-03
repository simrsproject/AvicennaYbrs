namespace Temiang.Avicenna.ReportLibrary.Accounting.BalanceSheet
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
    public partial class BalanceSheetGeneralReportLvl2 : Telerik.Reporting.Report
    {
        private ReportDataSource reportDataSource = new ReportDataSource();

        public BalanceSheetGeneralReportLvl2(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            System.Data.DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters);
            tbl.Columns.Add("ReportSectionName", typeof(string));
            tbl.Columns.Add("ReportSectionINT", typeof(int));
            tbl.Columns.Add("BalanceSheetTypeINT", typeof(int));

            SetupReport(printJobParameters);

            foreach (System.Data.DataRow row in tbl.Rows)
            {
                int accountGroup = Convert.ToInt32(row["AccountGroupLVL3"]);

                row["BalanceSheetTypeINT"] = Utility.BalanceSheetReportSectionINT(accountGroup); // 1:aktiva/2:passiva
                row["ReportSectionName"] = Utility.BalanceSheetReportSectionName(accountGroup); // Aktiva/Pasiva
                row["ReportSectionINT"] = accountGroup; // aktiva lancar, aktiva tetap ...
                

                row.AcceptChanges();
            }

            this.DataSource = tbl;
        }

        private void SetupReport(PrintJobParameterCollection printJobParameters)
        {
            string month = printJobParameters.FindByParameterName("pSinglePostingPeriode_Month").ValueString;
            string year = printJobParameters.FindByParameterName("pSinglePostingPeriode_Year").ValueString; 
  

            this.txtPeriode.Value = string.Format("Periode: {0} - {1}", Utility.MonthName(month), year);

            var healthcare = Healthcare.GetHealthcare();
            this.txtCompanyName.Value = healthcare.HealthcareName;
        }
    }
}
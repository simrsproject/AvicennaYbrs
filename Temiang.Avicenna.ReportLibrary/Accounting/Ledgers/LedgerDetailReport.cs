namespace Temiang.Avicenna.ReportLibrary.Accounting.Ledgers
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Data;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Temiang.Avicenna.BusinessObject.Util;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.ReportLibrary;

    /// <summary>
    /// Journal Report Document.
    /// </summary>
    public partial class LedgerDetailReport : Telerik.Reporting.Report
    {
        private ReportDataSource reportDataSource = new ReportDataSource();

        public LedgerDetailReport(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            SetupReport(printJobParameters);

            SetDataSource(programID, printJobParameters);
        }

        private void SetDataSource(string programID, PrintJobParameterCollection printJobParameters)
        {
            DataTable tblDetail = reportDataSource.GetDataTable(programID, printJobParameters);
            DataTable tblInitBalance = reportDataSource.GetDataTableDirect("spRpt_Accounting_Ledger_LedgerGeneralByAccountCode", printJobParameters);

            foreach (DataRow row in tblInitBalance.Rows)
            {
                DataRow nr = tblDetail.NewRow();
                // these two lines are needed to make this record printed at the top.
                nr["DetailId"] = 0;
                nr["beginningBalance"] = 1;

                nr["Description"] = "INITIAL BALANCE";
                nr["ChartOfAccountId"] = row["ChartOfAccountId"];
                nr["ChartOfAccountCode"] = row["ChartOfAccountCode"];
                nr["ChartOfAccountName"] = row["ChartOfAccountName"];
                nr["Description_detail"] = "INITIAL BALANCE";
                nr["NormalBalance"] = row["NormalBalance"];

                string normalBalance = row["NormalBalance"] as string;
                if (normalBalance.ToLowerInvariant() == "d")
                {
                    nr["Debit"] = Convert.ToDouble(0);
                    nr["Credit"] = Convert.ToDouble(0);

                    nr["DebitSum"] = Convert.ToDouble(row["InitialBalance"]);
                    nr["CreditSum"] = Convert.ToDouble(0);
                }
                else
                {
                    nr["Debit"] = Convert.ToDouble(0);
                    nr["Credit"] = Convert.ToDouble(0);

                    nr["DebitSum"] = Convert.ToDouble(0);
                    nr["CreditSum"] = Convert.ToDouble(row["InitialBalance"]);
                }
                tblDetail.Rows.Add(nr);
                tblDetail.AcceptChanges();
            }

            this.DataSource = tblDetail;
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
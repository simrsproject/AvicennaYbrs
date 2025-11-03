namespace Temiang.Avicenna.ReportLibrary.Accounting.SubLedgers
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
    public partial class SubLedgerDetailReport : Telerik.Reporting.Report
    {
        private ReportDataSource reportDataSource = new ReportDataSource();

        public SubLedgerDetailReport(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            SetupReport(printJobParameters);

            DataTable tblDetail = reportDataSource.GetDataTable(programID, printJobParameters);
            DataTable tblInitBalance = reportDataSource.GetDataTableDirect("spRpt_Accounting_SubLedger_SubLedgerGeneralByAccountCode", printJobParameters);

            string month = printJobParameters.FindByParameterName("pSinglePostingPeriode_Month").ValueString;
            string year = printJobParameters.FindByParameterName("pSinglePostingPeriode_Year").ValueString;

            string tgl = month + "/1/" + year;
            foreach (DataRow row in tblInitBalance.Rows)
            {
                DataRow nr = tblDetail.NewRow();
                // these two lines are needed to make this record printed at the top.
                nr["DetailId"] = 0;
                nr["beginningBalance"] = 1;
                nr["Description"] = "INITIAL BALANCE";
                nr["TransactionDate"] = tgl;
                nr["Description_Detail"] = "INITIAL BALANCE";
                nr["ChartOfAccountId"] = row["ChartOfAccountId"];
                nr["ChartOfAccountCode"] = row["ChartOfAccountCode"];
                nr["ChartOfAccountName"] = row["ChartOfAccountName"];
                nr["NormalBalance"] = row["NormalBalance"];
                nr["SubLedgerId"] = row["SubLedgerId"];
                nr["SubLedgerName"] = row["SubLedgerName"];
                nr["subledger_description"] = row["subledger_description"];

                string normalBalance = row["NormalBalance"] as string;
                if (normalBalance.ToLowerInvariant() == "d")
                {
                    nr["Debit"] = Convert.ToDouble(row["InitialBalance"]);
                    nr["Credit"] = Convert.ToDouble(0);
                }
                else
                {
                    nr["Debit"] = Convert.ToDouble(0);
                    nr["Credit"] = Convert.ToDouble(row["InitialBalance"]);
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


            var healthcare = Healthcare.GetHealthcare();
            this.txtCompanyName.Value = healthcare.HealthcareName;
        }
    }
}
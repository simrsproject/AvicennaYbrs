namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Finance.RSUI
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
 
    /// </summary>
    public partial class LedgerDetailByDateReport : Telerik.Reporting.Report
    {
        private ReportDataSource reportDataSource = new ReportDataSource();

        public LedgerDetailByDateReport(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate", new System.DateTime(2010, 04, 29));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 06, 08));
            //----------------

            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            Helper.InitializeLogo(this.pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

           // textBox3.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
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
                nr["NormalBalance"] = row["NormalBalance"];

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

    }
}
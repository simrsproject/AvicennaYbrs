namespace Temiang.Avicenna.ReportLibrary.Finance.Receivable
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
    /// Summary description for ARHistoryRpt.
    /// </summary>
    public partial class ARHistoryRpt : Telerik.Reporting.Report
    {
        private ReportDataSource reportDataSource = new ReportDataSource();

        public ARHistoryRpt(string programID, PrintJobParameterCollection printJobParameters)
        {

            InitializeComponent();

            SetupReport(printJobParameters);

            SetDataSource(programID, printJobParameters);
        }

        private void SetDataSource(string programID, PrintJobParameterCollection printJobParameters)
        {
            DataTable tblDetail = reportDataSource.GetDataTable(programID, printJobParameters);
            //DataTable tblInitSaldo = reportDataSource.GetDataTableDirect("sprpt_ARMutationGeneral", printJobParameters);

            //foreach (DataRow row in tblInitSaldo.Rows)
            //{
            //    DataRow nr = tblDetail.NewRow();
            //    // these two lines are needed to make this record printed at the top.
            //    nr["Section"] = 0;

            //    nr["PatientName"] = "SALDO";
            //    nr["Saldo"] = row["Saldo"];
            //    nr["GuarantorName"] = row["GuarantorName"];

            //    tblDetail.Rows.Add(nr);
            //    tblDetail.AcceptChanges();
            //}

            this.DataSource = tblDetail;
        }

        private void SetupReport(PrintJobParameterCollection printJobParameters)
        {
            Helper.InitializeLogo(this.pageHeader);
            //Helper.InitializeDataSource(this, programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            textBox3.Value = "Periode : " + string.Format("{0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
        }
    }
}
namespace Temiang.Avicenna.ReportLibrary.Finance.Payable.RSUTAMA
{
    using BusinessObject;
    using System;
    using System.Data;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Temiang.Avicenna.BusinessObject.Util;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.ReportLibrary;


    /// <summary>
    /// Summary description for APHistoryRpt.
    /// </summary>
    public partial class APHistoryRpt : Telerik.Reporting.Report
    {
       private ReportDataSource reportDataSource = new ReportDataSource();

        public APHistoryRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate", new System.DateTime(2010, 04, 29));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 06, 08));
            //----------------

            InitializeComponent();

            SetupReport(printJobParameters);

            SetDataSource(programID, printJobParameters);
        }
          private void SetDataSource(string programID, PrintJobParameterCollection printJobParameters)
        {
            DataTable tblDetail = reportDataSource.GetDataTable(programID, printJobParameters);
            //DataTable tblInitSaldo = reportDataSource.GetDataTableDirect("sprpt_APMutationGeneral", printJobParameters);

            //foreach (DataRow row in tblInitSaldo.Rows)
            //{
            //    DataRow nr = tblDetail.NewRow();
            //    nr["Section"] = 0;

            //    nr["StatusAP"] = "SALDO";
            //    nr["Saldo"] = row["Saldo"];
            //    nr["SupplierName"] = row["SupplierName"];
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

            textBox2.Value = string.Format("{0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
        }
    }
        
    
}
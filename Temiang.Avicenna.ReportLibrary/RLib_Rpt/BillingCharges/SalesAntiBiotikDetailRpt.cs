using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;


namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges
{
   

    /// <summary>
    /// Summary description for SalesAntiBiotikDetailRpt.
    /// </summary>
    public partial class SalesAntiBiotikDetailRpt : Telerik.Reporting.Report
    {
        public SalesAntiBiotikDetailRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(reportHeaderSection1);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);
            //table1.DataSource = dt;
            //table2.DataSource = dt;
            table3.DataSource = dt;


            this.DataSource = dt;

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            textBox25.Value = string.Format("Tanggal : {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);
        
        }
    }
}
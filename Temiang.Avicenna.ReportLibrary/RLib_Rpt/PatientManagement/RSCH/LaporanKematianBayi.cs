using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;


namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
   


    public partial class LaporanKematianBayi : Telerik.Reporting.Report
    {
        public LaporanKematianBayi(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(reportHeaderSection1);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);
            table1.DataSource = dt;

            this.DataSource = dt;

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            textBox19.Value = string.Format("Tanggal : {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);
        
        }
    }
}
namespace Temiang.Avicenna.ReportLibrary.RADT.RSUI
{
    using System;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;
    using System.Data;
    using Telerik.Reporting;


    /// <summary>
    /// Summary description for PatientBirthRecordRpt.
    /// </summary>
    public partial class PatientBirthRecordRpt : Telerik.Reporting.Report
    {
        public PatientBirthRecordRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
           // Helper.InitializeLogo(this.pageHeaderSection1);
           // Helper.InitializeLogo(this.pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);
            var reportDataSource = new ReportDataSource();

            System.Data.DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters);


       //     textBox50.Value = string.Format("Jakarta, {0:dd MMMM yyyy} ", DateTime.Now);
         // textBox92.Value = Common.Helper.GetAgeInYear(Convert.ToDateTime(tbl.Rows[0]["FatherBirthDate"]) ?? DateTime.Now.Date).ToString();
       
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}
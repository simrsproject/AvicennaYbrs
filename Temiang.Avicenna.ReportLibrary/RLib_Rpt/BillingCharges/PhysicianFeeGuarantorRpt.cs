using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges
{
    using System;
    using BusinessObject;

    /// <summary>
    /// Summary description for PhysicianFeeGuarantorRpt.
    /// </summary>
    public partial class PhysicianFeeGuarantorRpt : Telerik.Reporting.Report
    {
        public PhysicianFeeGuarantorRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //`
            InitializeComponent();

            Helper.InitializeLogo(this.reportHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);
            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
            table1.DataSource = reportDataSource.GetDataTable(programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            textBox2.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
        }
    }
}
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Finance.PhysicianFee.RSMB
{
    using System;
    using BusinessObject;

    /// <summary>
    /// Summary description for PhysicianFeeCalculationDraft.
    /// </summary>
    public partial class PhysicianFeeCalculationDraft : Telerik.Reporting.Report
    {
        public PhysicianFeeCalculationDraft(string programID, PrintJobParameterCollection printJobParameters)
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

            DateTime? fromDate = printJobParameters.FindByParameterName("StartDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("EndDate").ValueDateTime;

            textBox2.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
        }
    }
}
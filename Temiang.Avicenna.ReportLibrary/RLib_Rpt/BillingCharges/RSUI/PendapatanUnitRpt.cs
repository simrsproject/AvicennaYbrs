using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges.RSUI
{
    using System;
    using BusinessObject;

    /// <summary>
    /// Summary description for PhysicianFeeByPysicianRpt.
    /// </summary>
    public partial class PendapatanUnitRpt : Telerik.Reporting.Report
    {
        public PendapatanUnitRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            // 
            // Required for telerik Reporting designer support
            //`
            InitializeComponent();

            Helper.InitializeLogo(this.reportHeaderSection1);
            //Helper.InitializeDataSource(this, programID, printJobParameters);
            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
            table1.DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
        }
    }
}
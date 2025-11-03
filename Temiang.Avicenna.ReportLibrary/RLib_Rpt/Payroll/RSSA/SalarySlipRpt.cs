namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Payroll.RSSA
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class SalarySlipRpt : Telerik.Reporting.Report
    {
        public SalarySlipRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

           //Helper.InitializeNoLogoAlignLeft(this.groupHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
        }
    }
}
namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Payroll.RSSA

{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class SalaryListByPeriodRpt : Telerik.Reporting.Report
    {
        public SalaryListByPeriodRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            crosstab1.DataSource = DataSource;
        }
    }
}
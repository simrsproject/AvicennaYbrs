namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Payroll.RSSA

{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class RekapSalaryListByPeriodRpt : Telerik.Reporting.Report
    {
        public RekapSalaryListByPeriodRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeNoLogoAlignLeft(this.pageHeaderSection1);
            var rptData = new ReportDataSource();

            AppParameter ap1 = new AppParameter();
            ap1.LoadByPrimaryKey("HRDHead");
            txtHRDHead.Value = ap1.ParameterValue;

            AppParameter ap2 = new AppParameter();
            ap2.LoadByPrimaryKey("FinanceHead");
            txtFinanceHead.Value = ap2.ParameterValue;

            AppParameter ap3 = new AppParameter();
            ap3.LoadByPrimaryKey("Director");
            txtDirector.Value = ap3.ParameterValue;

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            crosstab1.DataSource = DataSource;
        }

    }
}
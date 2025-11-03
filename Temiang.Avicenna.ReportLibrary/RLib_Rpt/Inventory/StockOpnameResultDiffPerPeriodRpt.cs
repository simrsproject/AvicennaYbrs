namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class StockOpnameResultDiffPerPeriodRpt : Telerik.Reporting.Report
    {
        public StockOpnameResultDiffPerPeriodRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}
namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.RSSA
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class StockOpnameAkhirTahunRpt : Telerik.Reporting.Report
    {
        public StockOpnameAkhirTahunRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}
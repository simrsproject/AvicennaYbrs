namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Warehouse
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class ProductionOfGoods : Telerik.Reporting.Report
    {
        public ProductionOfGoods(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeaderSection1);
            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
        }
    }
}
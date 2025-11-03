namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.RSUI
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class ProductionOfGoodsDetail : Telerik.Reporting.Report
    {
        public ProductionOfGoodsDetail(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}
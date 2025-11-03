namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.AssetManagement.Management
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class AssetHistoryMaintenanceRpt : Telerik.Reporting.Report
    {
        public AssetHistoryMaintenanceRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}
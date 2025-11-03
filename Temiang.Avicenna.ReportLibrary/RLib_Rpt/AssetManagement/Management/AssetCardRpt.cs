namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.AssetManagement.Management
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class AssetCardRpt : Telerik.Reporting.Report
    {
        public AssetCardRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
        }
    }
}
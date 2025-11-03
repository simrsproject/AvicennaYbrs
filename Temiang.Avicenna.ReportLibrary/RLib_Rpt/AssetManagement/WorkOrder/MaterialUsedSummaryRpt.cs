namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.AssetManagement.WorkOrder
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class MaterialUsedSummaryRpt : Telerik.Reporting.Report
    {
        public MaterialUsedSummaryRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}
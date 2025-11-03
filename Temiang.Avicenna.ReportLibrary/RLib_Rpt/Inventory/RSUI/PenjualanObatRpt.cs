namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.RSUI
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class PenjualanObatRpt : Telerik.Reporting.Report
    {
        public PenjualanObatRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}
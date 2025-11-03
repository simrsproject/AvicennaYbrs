namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges.RSCH
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class RekapitulasiKeuangaHarianFarmasiRpt : Telerik.Reporting.Report
    {
        public RekapitulasiKeuangaHarianFarmasiRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}
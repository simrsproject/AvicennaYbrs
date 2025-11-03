namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges.RSCH

{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class JumlahLembarResepRiPerTahunRpt : Telerik.Reporting.Report
    {
        public JumlahLembarResepRiPerTahunRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            crosstab1.DataSource = DataSource;
        }
    }
}
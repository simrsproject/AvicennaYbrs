namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges.RSCH

{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class KeuanganFarmasiPerTahunRpt : Telerik.Reporting.Report
    {
        public KeuanganFarmasiPerTahunRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            crosstab1.DataSource = DataSource;
        }
    }
}
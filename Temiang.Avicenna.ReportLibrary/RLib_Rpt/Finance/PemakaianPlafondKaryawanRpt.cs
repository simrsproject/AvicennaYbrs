namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Finance

{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class PemakaianPlafondKaryawanRpt : Telerik.Reporting.Report
    {
        public PemakaianPlafondKaryawanRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            crosstab1.DataSource = DataSource;
        }
    }
}
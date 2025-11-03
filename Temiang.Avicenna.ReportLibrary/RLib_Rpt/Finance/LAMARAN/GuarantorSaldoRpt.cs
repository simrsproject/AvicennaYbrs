namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Finance.LAMARAN
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class GuarantorSaldoRpt : Telerik.Reporting.Report
    {
        public GuarantorSaldoRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}
namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges.RSCH.GedungBaru
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class PenerimaanHarianRanapGDBaru : Telerik.Reporting.Report
    {
        public PenerimaanHarianRanapGDBaru(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}
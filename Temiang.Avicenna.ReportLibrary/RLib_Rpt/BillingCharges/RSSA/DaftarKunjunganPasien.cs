namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges.RSSA
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class DaftarKunjunganPasien : Telerik.Reporting.Report
    {
        public DaftarKunjunganPasien(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}
namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class UlangTahunPasienDirawatRpt : Telerik.Reporting.Report
    {
        public UlangTahunPasienDirawatRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}
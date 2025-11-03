namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSSA
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class JumlahPasienRanapJK : Telerik.Reporting.Report
    {
        public JumlahPasienRanapJK(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}
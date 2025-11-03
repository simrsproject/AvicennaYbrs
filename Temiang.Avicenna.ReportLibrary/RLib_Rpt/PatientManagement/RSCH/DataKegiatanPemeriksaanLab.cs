namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class DataKegiatanPemeriksaanLab : Telerik.Reporting.Report
    {
        public DataKegiatanPemeriksaanLab(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            var healthcare = Healthcare.GetHealthcare();
            
            textBox6.Value = "Laboratorium " + healthcare.HealthcareName;
            textBox21.Value = healthcare.AddressLine1 + ' ' + healthcare.City;
        }
    }
}
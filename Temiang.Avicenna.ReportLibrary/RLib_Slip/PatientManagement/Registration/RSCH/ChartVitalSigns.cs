namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSCH
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class ChartVitalSigns : Telerik.Reporting.Report
    {
        public ChartVitalSigns(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;

            var hc = Healthcare.GetHealthcare();
            
            txtHealtcareName.Value = hc.HealthcareName.ToUpper();
            txtHealtcareCity.Value = hc.City.ToUpper();
        }
    }
}
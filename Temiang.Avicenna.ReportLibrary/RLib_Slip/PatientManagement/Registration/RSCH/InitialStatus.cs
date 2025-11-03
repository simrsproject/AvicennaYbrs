namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSCH
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;
    using System.Data;

    public partial class InitialStatus : Telerik.Reporting.Report
    {
        public InitialStatus(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            
            DataSource = dtb;

            var hc = Healthcare.GetHealthcare();
            
            txtHealtcareName.Value = hc.HealthcareName.ToUpper();
            txtHealtcareCity.Value = hc.City.ToUpper();
        }
    }
}
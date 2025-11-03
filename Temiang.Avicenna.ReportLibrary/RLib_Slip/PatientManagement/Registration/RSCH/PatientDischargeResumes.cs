namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSCH
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;
    using System.Data;

    public partial class PatientDischargeResumes : Telerik.Reporting.Report
    {
        public PatientDischargeResumes(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            
            DataSource = dtb;

            var hc = Healthcare.GetHealthcare();
            
            txtHealtcareName.Value = hc.HealthcareName;
            txtHealtcareCity.Value = hc.City.ToUpper();
        }
    }
}
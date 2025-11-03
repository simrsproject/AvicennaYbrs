namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSCH
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;
    using System.Data;

    public partial class CauseOfDeath : Telerik.Reporting.Report
    {
        public CauseOfDeath(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeNoLogoAlignLeft(this.pageHeaderSection1);

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            
            DataSource = dtb;

            var hc = Healthcare.GetHealthcare();
            txtCity.Value = hc.City + ", ";
        }
    }
}
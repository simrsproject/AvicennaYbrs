namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSMP
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;
    using System.Data;

    public partial class FormGuarantees : Telerik.Reporting.Report
    {
        public FormGuarantees(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeaderSection1);

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            
            DataSource = dtb;

            string ketua = AppParameter.GetParameterValue(AppParameter.ParameterItem.Director);
            Txtketua.Value = "(" + ketua + ")";

            var hc = Healthcare.GetHealthcare();
            
            textBox43.Value = hc.City + ", ";
        }
    }
}
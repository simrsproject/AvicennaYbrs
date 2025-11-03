using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.AssetManagement
{

    /// <summary>
    /// Summary description for SentToThirdPartiesSlp.
    /// </summary>
    public partial class SentToThirdPartiesSlp : Telerik.Reporting.Report
    {
        public SentToThirdPartiesSlp(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeader);
            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);

            var healthCare = Healthcare.GetHealthcare();
            
            textBox31.Value = healthCare.City + ",";
        }
    }
}
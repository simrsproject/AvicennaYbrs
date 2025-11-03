using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.AssetManagement
{

    /// <summary>
    /// Summary description for AssetMovementSlp.
    /// </summary>
    public partial class AssetMovementSlp : Telerik.Reporting.Report
    {
        public AssetMovementSlp(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            Helper.InitializeLogo(this.reportHeader);
            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);

            var healthCare = Healthcare.GetHealthcare();
            
            textBox8.Value = healthCare.City + ",";
        }
    }
}
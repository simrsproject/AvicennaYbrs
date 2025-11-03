using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.AssetManagement
{

    /// <summary>
    /// Summary description for WorkOrderRealizationSlp.
    /// </summary>
    public partial class WorkOrderRealizationSlp : Telerik.Reporting.Report
    {
        public WorkOrderRealizationSlp(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            Helper.InitializeLogo(this.reportHeader);
            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);

        }
    }
}
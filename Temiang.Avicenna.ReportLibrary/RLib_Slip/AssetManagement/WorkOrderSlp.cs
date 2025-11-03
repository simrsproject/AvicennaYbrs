using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.AssetManagement
{

    /// <summary>
    /// Summary description for BuktiPemakaianBarangRpt.
    /// </summary>
    public partial class WorkOrderSlp : Telerik.Reporting.Report
    {
        public WorkOrderSlp(string programID, PrintJobParameterCollection printJobParameters)
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
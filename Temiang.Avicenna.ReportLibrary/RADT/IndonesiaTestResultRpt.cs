using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RADT
{
    /// <summary>
    /// Summary description for IndonesiaTestResultRpt.
    /// </summary>
    public partial class IndonesiaTestResultRpt : Telerik.Reporting.Report
    {
        public IndonesiaTestResultRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters[0]);
        }
    }
}
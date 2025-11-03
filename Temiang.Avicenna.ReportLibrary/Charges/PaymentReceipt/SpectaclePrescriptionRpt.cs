using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Charges
{

    /// <summary>
    /// Summary description for IndonesiaTestResultRpt.
    /// </summary>
    public partial class SpectaclePrescriptionRpt : Telerik.Reporting.Report
    {
        public SpectaclePrescriptionRpt(string programID, PrintJobParameterCollection printJobParameters)
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
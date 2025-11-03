using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Inventory.Warehouse
{

    /// <summary>
    /// Summary description for BuktiPemakaianBarangRpt.
    /// </summary>
    public partial class PurchaseReturn : Telerik.Reporting.Report
    {
        public PurchaseReturn(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);
            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
        }
    }
}
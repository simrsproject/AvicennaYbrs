using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Warehouse.RSUI
{

    /// <summary>
    /// Summary description for BuktiPemakaianBarangRpt.
    /// </summary>
    public partial class PurchaseOrderReturn : Telerik.Reporting.Report
    {
        public PurchaseOrderReturn(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);
            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
            textBox50.Value = AppParameter.GetParameterValue(AppParameter.ParameterItem.PicWarehouseHead);

        }
    }
}
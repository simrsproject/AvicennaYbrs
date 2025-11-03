using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Warehouse.RSUI
{

    /// <summary>
    /// Summary description for BuktiPemakaianBarangRpt.
    /// </summary>
    public partial class NotaRetur : Telerik.Reporting.Report
    {
        public NotaRetur(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //Helper.InitializeLogo(this.pageHeaderSection1);
            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
            string finance = AppParameter.GetParameterValue(AppParameter.ParameterItem.FinanceHead);
            txtFinance.Value = '(' + finance + ')';

        }
    }
}
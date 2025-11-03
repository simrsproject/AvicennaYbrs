using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
namespace Temiang.Avicenna.ReportLibrary.Inventory.Warehouse
{


    public partial class SalesToBranch : Telerik.Reporting.Report
    {
        public SalesToBranch(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeader);
            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
        }
    }
}
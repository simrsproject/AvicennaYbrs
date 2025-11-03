using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using Telerik.Reporting;

namespace Temiang.Avicenna.ReportLibrary.Finance.PhysicianFee
{
   public partial class PhysicianFeeSummarySlipRpt : Report
    {
        public PhysicianFeeSummarySlipRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
        }
    }
}
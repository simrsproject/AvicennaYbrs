using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Finance.PhysicianFee.RSIAMTP
{

    public partial class PhysicianFeeNoVerificationRpt : Report
    {
        public PhysicianFeeNoVerificationRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
        }
    }
}
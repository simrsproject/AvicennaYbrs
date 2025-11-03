using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges.RSCH
{
    /// <summary>
    /// Summary description for RekapPembebasanPerbagian.
    /// </summary>
    public partial class RekapPembebasanPerbagian : Report
    {
        public RekapPembebasanPerbagian(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(reportHeaderSection1);
            //string paymentNo = printJobParameters.FindByParameterName("PaymentNo").ValueString;
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
            table2.DataSource = DataSource;
            var healthcare = Healthcare.GetHealthcare();
            
        }
    }
}
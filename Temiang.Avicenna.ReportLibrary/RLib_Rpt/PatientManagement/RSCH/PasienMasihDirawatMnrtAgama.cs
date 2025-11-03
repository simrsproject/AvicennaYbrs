using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
    /// <summary>
    /// Summary description for PasienMasihDirawat.
    /// </summary>
    public partial class PasienMasihDirawatMnrtAgama : Report
    {
        public PasienMasihDirawatMnrtAgama(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(pageHeaderSection1);
            //string paymentNo = printJobParameters.FindByParameterName("PaymentNo").ValueString;
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
            table2.DataSource = DataSource;
            var healthcare = Healthcare.GetHealthcare();
            
        }
    }
}
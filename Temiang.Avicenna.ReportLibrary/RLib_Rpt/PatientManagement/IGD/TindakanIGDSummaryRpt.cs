using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.IGD
{
    /// <summary>
    /// Summary description for DataInfoEMRRpt.
    /// </summary>
    public partial class TindakanIGDSummaryRpt : Report
    {
        public TindakanIGDSummaryRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeNoLogoAlignLeft(pageHeaderSection1);
            //string paymentNo = printJobParameters.FindByParameterName("PaymentNo").ValueString;
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;

            var healthcare = Healthcare.GetHealthcare();
            
            textBox70.Value = healthcare.AddressLine2 + ", ";
        }
    }
}
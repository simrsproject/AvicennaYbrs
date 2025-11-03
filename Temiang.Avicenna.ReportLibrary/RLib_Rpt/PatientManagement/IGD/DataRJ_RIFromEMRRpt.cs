using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.IGD
{
    /// <summary>
    /// Summary description for DataRJ_RIFromEMRRpt.
    /// </summary>
    public partial class DataRJ_RIFromEMRRpt : Report
    {
        public DataRJ_RIFromEMRRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeNoLogo(pageHeaderSection1);
            //string paymentNo = printJobParameters.FindByParameterName("PaymentNo").ValueString;
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;

            var healthcare = Healthcare.GetHealthcare();
            
            textBox70.Value = healthcare.AddressLine2 + ", ";
        }
    }
}
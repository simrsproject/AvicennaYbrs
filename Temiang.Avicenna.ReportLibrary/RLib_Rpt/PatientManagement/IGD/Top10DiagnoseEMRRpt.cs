using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.IGD
{
    /// <summary>
    /// Summary description for CasesEMRRpt.
    /// </summary>
    public partial class Top10DiagnoseEMRRpt : Report
    {
        public Top10DiagnoseEMRRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeNoLogoBigFont(pageHeaderSection1);
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;

            var healthcare = Healthcare.GetHealthcare();
            
            textBox15.Value = healthcare.AddressLine2 + ", ";
        }
    }
}
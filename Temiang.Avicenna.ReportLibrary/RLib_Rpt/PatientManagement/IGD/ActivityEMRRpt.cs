using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.IGD
{
    /// <summary>
    /// Summary description for ActivityEMRRpt.
    /// </summary>
    public partial class ActivityEMRRpt : Report
    {
        public ActivityEMRRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeNoLogo(pageHeaderSection1);
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;

            var healthcare = Healthcare.GetHealthcare();
            
            textBox15.Value = healthcare.AddressLine2 + ", ";
        }
    }
}
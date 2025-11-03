using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.IGD
{
    /// <summary>
    /// Summary description for SumPatientByYearRpt.
    /// </summary>
    public partial class SumPatientByYearRpt : Report
    {
        public SumPatientByYearRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeNoLogo(pageHeaderSection1);
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            crosstab2.DataSource = DataSource;
        }
    }
}
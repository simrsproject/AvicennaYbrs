using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.Cencus
{
    public partial class DailyRecapInpatientUnitRpt : Report
    {
        public DailyRecapInpatientUnitRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            // printJobParameters.AddNew("p_PeriodYear", "2010");
            // printJobParameters.AddNew("p_PeriodMonth", "05");

            //----------------


            InitializeComponent();

            Helper.InitializeLogo(pageHeader);
            DataTable dtb = Helper.ReportDataSource(programID, printJobParameters);
            table1.DataSource = dtb;
            DataSource = dtb;
        }
    }
}
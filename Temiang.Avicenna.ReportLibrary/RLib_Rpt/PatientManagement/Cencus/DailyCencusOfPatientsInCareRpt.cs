using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.Cencus
{
    public partial class DailyCensusOfPatientsInCareRpt : Report
    {
        public DailyCensusOfPatientsInCareRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_Date", new System.DateTime(2010, 05, 19));
            //printJobParameters.AddNew("p_roomid", "334");

            //----------------


            InitializeComponent();

            Helper.InitializeLogo(pageHeader);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);
            table1.DataSource = dt;
            DataSource = dt;
        }
    }
}
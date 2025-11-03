using Temiang.Avicenna.BusinessObject;
using System.Data;
namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Finance
{
   public partial class PettyCashPerMonthRpt : Telerik.Reporting.Report
    {
       public PettyCashPerMonthRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(pageHeaderSection1);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);

            this.DataSource = dt;
            this.table1.DataSource = dt;

            string year = printJobParameters.FindByParameterName("p_PeriodYear").ValueString;
            string fromMonth = Temiang.Avicenna.Common.Helper.GetMonthName(printJobParameters.FindByParameterName("p_PeriodMonth").ValueString);

            textBox2.Value = string.Format("Periode : {0} {1}", fromMonth, year); 
        }
    }
}
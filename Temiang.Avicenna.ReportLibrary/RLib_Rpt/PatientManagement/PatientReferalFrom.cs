using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement
{
    /// <summary>
    /// Summary description for PatientReferalFrom.
    /// </summary>
    public partial class PatientReferalFrom : Report
    {
        public PatientReferalFrom(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);
            string year = printJobParameters.FindByParameterName("p_PeriodYear").ValueString;
            string fromMonth =
                Common.Helper.GetMonthName(printJobParameters.FindByParameterName("p_PeriodMonth").ValueString);

            textBox4.Value = string.Format("Periode : {0} {1}", fromMonth, year);
        }
    }
}
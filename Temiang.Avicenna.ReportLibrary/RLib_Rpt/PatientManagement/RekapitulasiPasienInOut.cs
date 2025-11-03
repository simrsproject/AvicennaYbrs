using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement
{
    /// <summary>
    /// Summary description for RekapitulasiPasienInOut.
    /// </summary>
    public partial class RekapitulasiPasienInOut : Report
    {
        public RekapitulasiPasienInOut(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);

            string year = printJobParameters.FindByParameterName("p_PeriodYear").ValueString;
            string fromMonth =
                Common.Helper.GetMonthName(printJobParameters.FindByParameterName("p_PeriodMonth").ValueString);

            textBox1.Value = string.Format("Periode : {0} {1}", fromMonth, year);
            //crosstab1.DataSource = dtb;
            crosstab2.DataSource = dtb;
        }
    }
}
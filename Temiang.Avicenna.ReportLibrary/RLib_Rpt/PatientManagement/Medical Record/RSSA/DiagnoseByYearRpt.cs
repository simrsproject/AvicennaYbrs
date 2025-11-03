using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSSA
{
    /// <summary>
    /// Summary description for DiagnoseByYearRpt.
    /// </summary>
    public partial class DiagnoseByYearRpt : Report
    {
        public DiagnoseByYearRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            var rptdata = new ReportDataSource();
            Helper.InitializeNoLogo(pageHeaderSection1);
            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            DataSource = dtb;

            table1.DataSource = dtb;

            //DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            //DateTime? toDate = printJobParameters.FindByParameterName("p_PeriodYear").ValueDateTime;
            //textBox18.Value = string.Format("Tahun {0}", toDate);
        }
    }
}
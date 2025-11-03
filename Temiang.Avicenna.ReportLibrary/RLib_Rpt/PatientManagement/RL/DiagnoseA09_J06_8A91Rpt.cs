using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RL
{
    /// <summary>
    /// Summary description for DiagnoseA09_J06.
    /// </summary>
    public partial class DiagnoseA09_J06 : Report
    {
        public DiagnoseA09_J06(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            var rptdata = new ReportDataSource();
            Helper.InitializeNoLogo(pageHeaderSection1);
            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            DataSource = dtb;

            table1.DataSource = dtb;

            //DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            //DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;
            //textBox18.Value = string.Format("Tanggal {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);
        }
    }
}
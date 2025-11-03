using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
    /// <summary>
    /// Summary description for JumlahOperasiTerbanyak.
    /// </summary>
    public partial class SepuluhKasusRIBdsrkICDRpt : Report
    {
        public SepuluhKasusRIBdsrkICDRpt(string programID,
                                         PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeNoLogoAlignLeft(pageHeaderSection1);
            var rptdata = new ReportDataSource();

            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            DataSource = dtb;

            table1.DataSource = dtb;           
        }
    }
}
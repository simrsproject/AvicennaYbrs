using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
    /// <summary>
    /// Summary description for JumlahOperasiTerbanyak.
    /// </summary>
    public partial class IGDPsikotropikaNarkotikaDanPsikodileptik : Report
    {
        public IGDPsikotropikaNarkotikaDanPsikodileptik(string programID,
                                                        PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeNoLogoAlignLeft(pageHeaderSection1);
            var rptdata = new ReportDataSource();

            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            DataSource = dtb;

            table1.DataSource = dtb;

            var rs = Healthcare.GetHealthcare();
            


            textBox14.Value = string.Format("NAMA RUMAH SAKIT: {0}", rs.HealthcareName);
            textBox15.Value = "KODE RS: " + rs.HospitalCode;
        }
    }
}
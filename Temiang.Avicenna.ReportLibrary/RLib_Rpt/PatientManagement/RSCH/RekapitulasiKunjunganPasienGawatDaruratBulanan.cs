using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
    /// <summary>
    /// Summary description for RekapitulasiKunjunganPasienGawatDaruratBulanan.
    /// </summary>
    public partial class RekapitulasiKunjunganPasienGawatDaruratBulanan : Report
    {
        public RekapitulasiKunjunganPasienGawatDaruratBulanan(string programID,
                                                              PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(pageHeaderSection1);
            var rptdata = new ReportDataSource();
            var healthcare = Healthcare.GetHealthcare();
            
            textBox120.Value = healthcare.City + ", ";
            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            DataSource = dtb;

            table1.DataSource = dtb;
        }
    }
}
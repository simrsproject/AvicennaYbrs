using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
    /// <summary>
    /// Summary description for PasienGawatDaruratMeninggalDetail.
    /// </summary>
    public partial class PasienGawatDaruratMeninggalDetail : Report
    {
        public PasienGawatDaruratMeninggalDetail(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(pageHeaderSection1);
            var rptdata = new ReportDataSource();

            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            DataSource = dtb;

            table1.DataSource = dtb;
            var healthcare = Healthcare.GetHealthcare();
            
            textBox46.Value = healthcare.AddressLine2 + ", ";
        }
    }
}
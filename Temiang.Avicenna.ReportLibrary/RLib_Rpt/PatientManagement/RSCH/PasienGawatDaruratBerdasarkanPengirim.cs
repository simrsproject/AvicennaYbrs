using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
    /// Summary description for PasienGawatDaruratBerdasarkanPengirim.
    /// </summary>
    public partial class PasienGawatDaruratBerdasarkanPengirim : Report
    {
        public PasienGawatDaruratBerdasarkanPengirim(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(pageHeaderSection1);
            var rptdata = new ReportDataSource();

            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);
            Helper.InitializeLogo(pageHeaderSection1);
            DataSource = dtb;

            table1.DataSource = dtb;
            var healthcare = Healthcare.GetHealthcare();
            
            textBox37.Value = healthcare.AddressLine2 + ", ";
        }
    }
}
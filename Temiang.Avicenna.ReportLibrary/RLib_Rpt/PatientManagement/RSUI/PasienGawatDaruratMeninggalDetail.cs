using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSUI
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
            var healthcare = new Healthcare();
            healthcare.LoadByPrimaryKey("001");
            textBox46.Value = healthcare.AddressLine2 + ", ";
        }
    }
}
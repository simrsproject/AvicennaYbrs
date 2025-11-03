using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
    /// <summary>
    /// Summary description for PasienGawatDaruratBerdasarkanAlasanBerobat.
    /// </summary>
    public partial class PasienGawatDaruratBerdasarkanAlasanBerobat : Report
    {
        public PasienGawatDaruratBerdasarkanAlasanBerobat(string programID,
                                                          PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(pageHeaderSection1);
            var rptdata = new ReportDataSource();

            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            DataSource = dtb;

            table1.DataSource = dtb;
        }
    }
}
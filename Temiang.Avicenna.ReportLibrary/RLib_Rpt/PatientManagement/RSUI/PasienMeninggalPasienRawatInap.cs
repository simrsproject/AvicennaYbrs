using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSUI
{
    /// <summary>
    /// Summary description for PasienGawatDaruratMeninggalDetail.
    /// </summary>
    public partial class PasienMeninggalPasienRawatInap : Report
    {
        public PasienMeninggalPasienRawatInap(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(pageHeaderSection1);
            var rptdata = new ReportDataSource();

            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            var h = new Healthcare();
            h.LoadByPrimaryKey("001");
            textBox13.Value = h.City + ", " + string.Format("{0:dd-MMM-yyyy}", DateTime.Now);
            textBox24.Value = "Pimpinan " + h.HealthcareName;
            DataSource = dtb;

            table1.DataSource = dtb;
        }
    }
}
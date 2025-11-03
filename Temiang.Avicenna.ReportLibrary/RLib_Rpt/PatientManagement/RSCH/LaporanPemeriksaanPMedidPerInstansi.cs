using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
    /// <summary>
    /// Summary description for LaporanPemeriksaanPMedidPerUnit.
    /// </summary>
    public partial class LaporanPemeriksaanPMedidPerInstansi : Report
    {
        public LaporanPemeriksaanPMedidPerInstansi(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            Helper.InitializeLogo(reportHeaderSection1);

            var rptdata = new ReportDataSource();
            DataSource = rptdata.GetDataTable(programID, printJobParameters);

            textBox3.Value = string.Format("Periode : {0} s/d {1}",
                                           printJobParameters.FindByParameterName("p_FromDate").ValueDateTime.Value.
                                               ToShortDateString(),
                                           printJobParameters.FindByParameterName("p_ToDate").ValueDateTime.Value.
                                               ToShortDateString());
        }
    }
}
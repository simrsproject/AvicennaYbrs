using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSMP
{
    /// <summary>
    /// Summary description for TindakanPerBagianPerbulan.
    /// </summary>
    public partial class TindakanPerBagianPerbulan : Report
    {
        public TindakanPerBagianPerbulan(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(pageHeaderSection1);
            var rptdata = new ReportDataSource();

            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            DataSource = dtb;

            table1.DataSource = dtb;

            var healthcare = Healthcare.GetHealthcare();
            textBox1.Value = healthcare.City + ", ";
        }
    }
}
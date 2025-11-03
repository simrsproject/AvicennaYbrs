using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
    public partial class InpatientFromIGDRpt : Report
    {
        public InpatientFromIGDRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            var rptdata = new ReportDataSource();
            Helper.InitializeLogo(pageHeaderSection1);
            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            DataSource = dtb;

            table1.DataSource = dtb;
        }
    }
}
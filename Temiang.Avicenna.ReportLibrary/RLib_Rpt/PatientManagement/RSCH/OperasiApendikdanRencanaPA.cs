using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
    public partial class OperasiApendikdanRencanaPA : Report
    {
        public OperasiApendikdanRencanaPA(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            var healthcare = Healthcare.GetHealthcare();
            
            textBox19.Value = healthcare.City;
        }
    }
}
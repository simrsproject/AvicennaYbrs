using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
    public partial class IndikatorSasaranMutuAnestesi : Report
    {
        public IndikatorSasaranMutuAnestesi(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
            var healthcare = Healthcare.GetHealthcare();
            
            textBox19.Value = healthcare.City;
            
        }
    }
}
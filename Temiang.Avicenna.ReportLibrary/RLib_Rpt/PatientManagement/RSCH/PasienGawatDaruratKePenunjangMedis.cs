using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
    public partial class PasienGawatDaruratKePenunjangMedis : Report
    {
        public PasienGawatDaruratKePenunjangMedis(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
            var healthcare = Healthcare.GetHealthcare();
            
            textBox53.Value = healthcare.AddressLine2 + ", ";
        }
    }
}
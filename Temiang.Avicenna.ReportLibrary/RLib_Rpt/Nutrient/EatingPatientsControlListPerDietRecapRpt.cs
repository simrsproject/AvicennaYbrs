using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Nutrient
{
    using Telerik.Reporting;
    using BusinessObject;
    
    public partial class EatingPatientsControlListPerDietRecapRpt : Report
    {
        public EatingPatientsControlListPerDietRecapRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeaderSection1);
            
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}
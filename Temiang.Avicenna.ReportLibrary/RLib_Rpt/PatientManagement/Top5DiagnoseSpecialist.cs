using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement
{
    /// <summary>
    /// Summary description for Top5DiagnoseSpecialist.
    /// </summary>
    public partial class Top5DiagnoseSpecialist : Report
    {
        public Top5DiagnoseSpecialist(string programID, PrintJobParameterCollection printJobParameters)
        {
            //printJobParameters.AddNew("p_month_start", "01"); 
            //printJobParameters.AddNew("p_month_end", "12");
            //printJobParameters.AddNew("p_year", "2010");


            InitializeComponent();

            var rptdata = new ReportDataSource();

            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            DataSource = dtb;

            table1.DataSource = dtb;
            string year = printJobParameters.FindByParameterName("p_PeriodYear").ValueString;
            string fromMonth =
                Common.Helper.GetMonthName(printJobParameters.FindByParameterName("p_PeriodMonth").ValueString);

            textBox28.Value = string.Format("Periode : {0} {1}", fromMonth, year);

            var healthcare = Healthcare.GetHealthcare();
            
            textBox27.Value = ": " + healthcare.HealthcareName;
            textBox19.Value = ": " + healthcare.AddressLine2;
            textBox20.Value = ": " + healthcare.HospitalCode;
        }
    }
}
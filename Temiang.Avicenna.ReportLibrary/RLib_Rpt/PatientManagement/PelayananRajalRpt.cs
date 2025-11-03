using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement
{
    public partial class PelayananRajalRpt : Report
    {
        public PelayananRajalRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            //Helper.InitializeNoLogoBigFont(this.pageHeaderSection1);
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            table1.DataSource = dtb;
            string year = printJobParameters.FindByParameterName("p_PeriodYear").ValueString;
            string fromMonth =
                Common.Helper.GetMonthName(printJobParameters.FindByParameterName("p_PeriodMonth").ValueString);

            textBox11.Value = string.Format("Periode : {0} {1}", fromMonth, year);

            var healthcare = Healthcare.GetHealthcare();
            
            //textBox15.Value = healthcare.AddressLine2 + ", ";
        }
    }
}
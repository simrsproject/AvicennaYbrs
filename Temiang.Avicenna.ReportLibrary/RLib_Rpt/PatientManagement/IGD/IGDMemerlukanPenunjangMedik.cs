using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.IGD
{
    public partial class IGDMemerlukanPenunjangMedik : Report
    {
        public IGDMemerlukanPenunjangMedik(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeNoLogoAlignLeft(pageHeaderSection1);

            DateTime? FromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? ToDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;
            textBox2.Value = string.Format("Period: {0:dd-MM-yyyy} s/d {1:dd-MM-yyyy}", FromDate, ToDate);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;

            var healthCare = Healthcare.GetHealthcare();
            
            textBox18.Value = string.Format("{0}, {1:dd-MM-yyyy}", healthCare.City, DateTime.Now);
        }
    }
}
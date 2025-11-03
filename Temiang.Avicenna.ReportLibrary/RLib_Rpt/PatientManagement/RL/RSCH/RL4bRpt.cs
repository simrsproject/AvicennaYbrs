using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RL.RSCH

{
    /// <summary>
    /// Summary description for RL4bRpt.
    /// </summary>
    public partial class RL4bRpt : Report
    {
        public RL4bRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            var rptdata = new ReportDataSource();

            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            DataSource = dtb;

            table1.DataSource = dtb;
            PopulateHealthcareInfo();
        }

        private void PopulateHealthcareInfo()
        {
            var healthcare = Healthcare.GetHealthcare();
            
            textBox23.Value = "Nama Rumah Sakit : " + healthcare.HealthcareName;
            textBox44.Value = healthcare.City + ", " + DateTime.Now.ToString("dd-MM-yyyy");
        }
    }
}
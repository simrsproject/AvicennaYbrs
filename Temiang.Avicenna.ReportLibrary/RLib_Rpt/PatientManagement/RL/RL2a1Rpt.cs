using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RL
{
    /// <summary>
    /// Summary description for RL2a1Rpt.
    /// </summary>
    public partial class RL2a1Rpt : Report
    {
        public RL2a1Rpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //printJobParameters.AddNew("p_FromMonth", "01"); 
            //printJobParameters.AddNew("p_ToMonth", "12");
            //printJobParameters.AddNew("p_Year", "2010");

            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            var rptdata = new ReportDataSource();

            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            DataSource = dtb;

            table1.DataSource = dtb;
            PopulateHealthcareInfo();

            String p_month_start = printJobParameters.FindByParameterName("p_FromMonth").ValueString;
            String p_month_end = printJobParameters.FindByParameterName("p_ToMonth").ValueString;
            String p_year = printJobParameters.FindByParameterName("p_Year").ValueString;

            if (p_month_start == p_month_end)

                textBox22.Value = "Bulan : " +
                                  string.Format(Convert.ToDateTime(p_month_start + "/01/" + p_year).ToString("MMMM"));
            else

                textBox22.Value = "Bulan : " +
                                  string.Format(Convert.ToDateTime(p_month_start + "/01/" + p_year).ToString("MMMM")) +
                                  " s/d " +
                                  string.Format(Convert.ToDateTime(p_month_end + "/01/" + p_year).ToString("MMMM"));

            txtPeriod.Value = "Tahun " + p_year;
        }

        private void PopulateHealthcareInfo()
        {
            var healthcare = Healthcare.GetHealthcare();
            
            TKdRS1.Value = healthcare.HospitalCode.Substring(0, 1);
            TKdRS2.Value = healthcare.HospitalCode.Substring(1, 1);
            TKdRS3.Value = healthcare.HospitalCode.Substring(2, 1);
            TKdRS4.Value = healthcare.HospitalCode.Substring(3, 1);
            TKdRS5.Value = healthcare.HospitalCode.Substring(4, 1);
            TKdRS6.Value = healthcare.HospitalCode.Substring(5, 1);
            TKdRS7.Value = healthcare.HospitalCode.Substring(6, 1);
            textBox44.Value = healthcare.City + ", " + DateTime.Now.ToString("dd-MM-yyyy");
        }
    }
}
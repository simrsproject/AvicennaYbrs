using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.Medical_Record

{
    /// <summary>
    /// Summary description for KartuIndeksWilayah.
    /// </summary>
    public partial class KartuIndeksWilayah : Report
    {
        public KartuIndeksWilayah(string programID, PrintJobParameterCollection printJobParameters)
        {
            //printJobParameters.AddNew("p_month_start", "01"); 
            //printJobParameters.AddNew("p_month_end", "12");
            //printJobParameters.AddNew("p_year", "2010");


            InitializeComponent();

            var rptdata = new ReportDataSource();

            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            DataSource = dtb;

            table1.DataSource = dtb;
            PopulateHealthcareInfo();


            String p_month = printJobParameters.FindByParameterName("p_PeriodMonth").ValueString;
            String p_year = printJobParameters.FindByParameterName("p_PeriodYear").ValueString;
            textBox22.Value = "Bulan : " + string.Format(Convert.ToDateTime(p_month + "/01/" + p_year).ToString("MMMM"));
            txtPeriod.Value = "Tahun : " + p_year;
        }

        private void PopulateHealthcareInfo()
        {
            var healthcare = Healthcare.GetHealthcare();
            
            textBox10.Value = healthcare.AddressLine1 + " " + healthcare.City;
            textBox23.Value = healthcare.HealthcareName;
        }
    }
}
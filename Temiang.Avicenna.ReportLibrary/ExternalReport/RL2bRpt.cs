using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.ExternalReport
{
    using System;
    using System.Data;
    using BusinessObject;

    /// <summary>
    /// Summary description for RL21b1Rpt.
    /// </summary>
        public partial class RL2bRpt : Telerik.Reporting.Report
    {
        public RL2bRpt(string programID, PrintJobParameterCollection printJobParameters)
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

            String p_month_start = printJobParameters.FindByParameterName("p_FromMonth").ValueString;
            String p_month_end = printJobParameters.FindByParameterName("p_ToMonth").ValueString;
            String p_year = printJobParameters.FindByParameterName("p_Year").ValueString;

            //DateTime date = Convert.ToDateTime(p_month + "/01/" + p_year);
            txtPeriod.Value = "Tahun : " + p_year;
            if (p_month_start == p_month_end)

                textBox22.Value = "Bulan : " + string.Format(Convert.ToDateTime(p_month_start + "/01/" + p_year).ToString("MMMM"));
            else
           
                textBox22.Value = "Bulan : " + string.Format(Convert.ToDateTime(p_month_start + "/01/" + p_year).ToString("MMMM")) + " s/d " + string.Format(Convert.ToDateTime(p_month_end + "/01/" + p_year).ToString("MMMM"));
            

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
            textBox23.Value = "Nama Rumah Sakit : " + healthcare.HealthcareName;
            textBox44.Value = healthcare.City + ", " + DateTime.Now.ToString("dd-MM-yyyy");
            //txtHealthcareAddressLine1.Value = healthcare.AddressLine1;
            //txtHealthcareAddressLine2.Value = healthcare.AddressLine2;
            //txtHealthcarePhoneNo.Value = healthcare.PhoneNo;
            //txtHealthcareFaxNo.Value = healthcare.FaxNo;
        }
    }

}
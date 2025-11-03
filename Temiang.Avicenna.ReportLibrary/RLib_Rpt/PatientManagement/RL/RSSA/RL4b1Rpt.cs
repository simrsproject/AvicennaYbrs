using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RL.RSSA
{
    public partial class RL4b1Rpt : Report
    {
        public RL4b1Rpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate", new System.DateTime(2010, 05, 19));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 05, 24));
            //----------------

            InitializeComponent();
            var rptdata = new ReportDataSource();

            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            DataSource = dtb;

            table1.DataSource = dtb;

            PopulateHealthcareInfo();

            //Helper.InitializeLogo(pageHeaderSection1);
            //Helper.InitializeDataSource(this, programID, printJobParameters);
            String p_month = printJobParameters.FindByParameterName("p_PeriodMonth").ValueString;
            String p_year = printJobParameters.FindByParameterName("p_PeriodYear").ValueString;
            textBox22.Value = "Bulan : " + string.Format(Convert.ToDateTime(p_month + "/01/" + p_year).ToString("MMMM"));
            textBox2.Value = "Tahun " + p_year;
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
            //txtHealthcareAddressLine1.Value = healthcare.AddressLine1;
            //txtHealthcareAddressLine2.Value = healthcare.AddressLine2;
            //txtHealthcarePhoneNo.Value = healthcare.PhoneNo;
            //txtHealthcareFaxNo.Value = healthcare.FaxNo;
        }
    }
}
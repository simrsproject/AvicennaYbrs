using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RADT
{
    /// <summary>
    /// Summary description for PoliklinikDailyRpt.
    /// </summary>
    public partial class RegistrationOutPatientRpt : Report
    {
        public RegistrationOutPatientRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate", new System.DateTime(2010, 04, 26));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 05, 03));
            //----------------


            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);

            PopulateHealthcareInfo();

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            DataSource = dtb;
            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            txtPeriod.Value = string.Format("Tanggal {0:dd/MM/yyyy} s/d {1:dd/MM/yyyy}", fromDate, toDate);
        }

        private void PopulateHealthcareInfo()
        {
            var healthcare = Healthcare.GetHealthcare();
            
            //txtHealthcareName.Value = healthcare.HealthcareName;
            //txtHealthcareAddressLine1.Value = healthcare.AddressLine1;
            //txtHealthcareAddressLine2.Value = healthcare.AddressLine2;
            //txtHealthcarePhoneNo.Value = healthcare.PhoneNo;
            //txtHealthcareFaxNo.Value = healthcare.FaxNo;
        }
    }
}
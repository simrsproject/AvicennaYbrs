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
    public partial class PoliklinikDailyRpt : Report
    {
        public PoliklinikDailyRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate",new System.DateTime(2009,11,10));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2009, 11, 12));
            //----------------

            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeaderSection1);
            PopulateHealthcareInfo();

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            DataSource = dtb;
            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            txtPeriod.Value = string.Format("Periode : {0:dd MMMM yyyy} s/d {1:dd MMMM yyyy}", fromDate, toDate);
        }

        private void PopulateHealthcareInfo()
        {
            var healthcare = Healthcare.GetHealthcare();
            
            //txtHealthcareAddressLine1.Value = healthcare.AddressLine1;
            //txtHealthcareAddressLine2.Value = healthcare.AddressLine2;
            //txtHealthcarePhoneNo.Value = healthcare.PhoneNo;
            //txtHealthcareFaxNo.Value = healthcare.FaxNo;
        }
    }
}
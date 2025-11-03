using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RADT.PAC
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for LapPeneHarianKasirRJRpt.
    /// </summary>
    public partial class DoctorFeeRpt : Telerik.Reporting.Report
    {
        public DoctorFeeRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate", new System.DateTime(2009, 11, 10));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 11, 12));
            //----------------

            InitializeComponent();

            PopulateHealthcareInfo();
            Helper.InitializeLogo(this.pageHeader);
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            DataSource = dtb;
            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            txtPeriod.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);

        }
        private void PopulateHealthcareInfo()
        {
            var healthcare = Healthcare.GetHealthcare();
            
            txtHealthcareAddressLine1.Value = healthcare.AddressLine1;
            txtHealthcareAddressLine2.Value = healthcare.AddressLine2;
            txtHealthcarePhoneNoAndFax.Value = string.Format("Telp : {0}  Fax : {1}", healthcare.PhoneNo,healthcare.FaxNo);
        }
    }
}
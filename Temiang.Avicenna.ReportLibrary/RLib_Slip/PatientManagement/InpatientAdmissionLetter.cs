using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement
{

    /// <summary>
    /// Summary description for InpatientAdmissionLetter.
    /// </summary>
    public partial class InpatientAdmissionLetter : Report
    {
        public InpatientAdmissionLetter(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogoOnlySizeModeNormal(this.pageHeader);
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            this.DataSource = dtb;

            var healthcare = Healthcare.GetHealthcare();
            
            txtHealthcareName.Value = healthcare.HealthcareName.ToUpper();
            txtAddress1.Value = healthcare.AddressLine1.ToUpper();
            txtAddress2.Value = healthcare.AddressLine2.ToUpper() + " - " + healthcare.ZipCode;
            txtPhoneNo.Value = "Telp : " + healthcare.PhoneNo;
            txtFaxNo.Value = "Fax : " + healthcare.FaxNo;
            txtWebsite.Value = "Website : " + healthcare.Website;
            txtEmail.Value = "E-mail : " + healthcare.EmailAddr;
            TxtRSU.Value = healthcare.HealthcareName;
            txtCity1.Value = healthcare.City + ",";
        }
    }
}
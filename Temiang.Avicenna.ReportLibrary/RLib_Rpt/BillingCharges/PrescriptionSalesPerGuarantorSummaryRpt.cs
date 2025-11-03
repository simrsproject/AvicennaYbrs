using System.Linq;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;


    /// <summary>
    /// Summary description for BillingSummary.
    /// </summary>
    public partial class PrescriptionSalesPerGuarantorSummaryRpt : Report
    {
        public PrescriptionSalesPerGuarantorSummaryRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            {
                /// <summary>
                /// Required for telerik Reporting designer support
                /// </summary>
                InitializeComponent();
                var rptData = new ReportDataSource();
                DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
                this.DataSource = dtb;

                var healthcare = Healthcare.GetHealthcare();
                
                //txtUserName.Value = printJobParameters.FindByParameterName("UserName").ValueString;
                TxtNameRS.Value = healthcare.HealthcareName;
                TxtCityRS.Value = healthcare.AddressLine1 + ' ' + healthcare.City;
                TxtTelp.Value = "Telp " + healthcare.PhoneNo + " Fax " + healthcare.FaxNo;

                var user = new AppUser();
                user.LoadByPrimaryKey(printJobParameters.FindByParameterName("p_UserID").ValueString);
                TxtUser.Value = user.UserName;
                //TxtUserID.Value = user.UserID;
                
            }
        }
    }
}
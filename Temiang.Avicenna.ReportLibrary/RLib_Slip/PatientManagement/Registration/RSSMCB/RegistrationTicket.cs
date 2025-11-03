using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSSMCB
{

    /// <summary>
    /// Summary description for RegistrationTicket.
    /// </summary>
    public partial class RegistrationTicket : Report
    {
        public RegistrationTicket(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeLogo(this.reportHeaderSection1);
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            this.DataSource = dtb;
            //var healthcare = Healthcare.GetHealthcare();
            //
            
            //textBox16.Value = healthcare.HealthcareName + Environment.NewLine + healthcare.AddressLine1 + Environment.NewLine + healthcare.City + Environment.NewLine + "Telp " + healthcare.PhoneNo + " Fax " + healthcare.FaxNo;
             //TxtNameRS.Value = healthcare.HealthcareName;
            //TxtCityRS.Value = healthcare.AddressLine1 + ' ' + healthcare.City;
            //TxtTelp.Value = "Telp "  + healthcare.PhoneNo + " Fax " + healthcare.FaxNo;
            //var user = new AppUser();
            //user.LoadByPrimaryKey(AppSession.UserLogin.UserID);
            //textBox22.Value = user.UserName;
        }
    }
}
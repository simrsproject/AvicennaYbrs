using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration
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
            //Helper.InitializeNoLogo(this.pageHeader);
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            this.DataSource = dtb;
            var healthcare = Healthcare.GetHealthcare();
            
            TxtRSU.Value = healthcare.HealthcareName;
            textBox16.Value = healthcare.HealthcareName + Environment.NewLine + healthcare.AddressLine1 + Environment.NewLine + healthcare.City + Environment.NewLine + "Telp " + healthcare.PhoneNo + " Fax " + healthcare.FaxNo;
             //TxtNameRS.Value = healthcare.HealthcareName;
            //TxtCityRS.Value = healthcare.AddressLine1 + ' ' + healthcare.City;
            //TxtTelp.Value = "Telp "  + healthcare.PhoneNo + " Fax " + healthcare.FaxNo;
            //var user = new AppUser();
            //user.LoadByPrimaryKey(AppSession.UserLogin.UserID);
            //TxtOperator.Value = "Print Karcis Operator : " + user.UserName + ' ' + DateTime.Now;
        }
    }
}
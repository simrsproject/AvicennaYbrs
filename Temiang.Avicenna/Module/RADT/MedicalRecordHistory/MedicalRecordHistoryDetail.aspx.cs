using System;
using System.Data;
using System.Web.UI;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class MedicalRecordHistoryDetail : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.MedicalRecordHistory;

            if (!IsPostBack)
            {
                if (Request.QueryString["menu"] != null)
                {
                    (Helper.FindControlRecursive(Page, "fw_mnuMain") as RadMenu).Visible = false;
                    (Helper.FindControlRecursive(Page, "fw_lblProgramPath") as Label).Visible = false;
                    (Helper.FindControlRecursive(Page, "ctlLoginInfo") as UserControl).Visible = false;
                }

                winHistory.NavigateUrl = "PatientInformationDetail.aspx?patientID=" + Request.QueryString["patientID"];
                winHistory.Title = rpbHistory.Items[0].Text;

                rpbHistory.Items[0].Selected = true;

                string patientID = Request.QueryString["patientID"];

                var patient = new Patient();
                patient.LoadByPrimaryKey(patientID);

                if (string.IsNullOrEmpty(patient.SRSalutation))
                    lblPatientName.Text = patient.PatientName;
                else
                {
                    var appStandardReferenceItem = new AppStandardReferenceItem();
                    appStandardReferenceItem.LoadByPrimaryKey("Salutation", patient.SRSalutation);

                    lblPatientName.Text = patient.PatientName + " (" + appStandardReferenceItem.ItemName + ")";
                }

                lblCityOfBirth.Text = patient.CityOfBirth;
                lblDateOfBirth.Text = patient.DateOfBirth.Value.ToString(AppConstant.DisplayFormat.Date);

                string ageYear = Helper.GetAgeInYear(patient.DateOfBirth.Value).ToString();
                string ageMonth = Helper.GetAgeInMonth(patient.DateOfBirth.Value).ToString();
                string ageDay = Helper.GetAgeInDay(patient.DateOfBirth.Value).ToString();

                if (ageYear == "0")
                {
                    if (ageMonth == "0")
                        lblAge.Text = "(" + ageDay + "d)";
                    else
                        lblAge.Text = "(" + ageMonth + "m)";
                }
                else
                    lblAge.Text = "(" + ageYear + "y)";


                if (patient.Sex == "M")
                    lblSex.Text = "Male";
                else
                    lblSex.Text = "Female";

                lblMedicalNo.Text = patient.MedicalNo;

                if (!AppSession.Parameter.IsRadiologyNoAutoCreate)
                {
                    lblDiagnostic.Visible = true;
                    lblDiagnosticNo.Visible = true;
                    lblDiagnosticNo.Text = patient.DiagnosticNo;
                }

                rpbHistory.Items[7].Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH";
            }
        }
    }
}
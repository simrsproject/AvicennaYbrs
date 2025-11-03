using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PatientIncidentSearch : BasePageDialog
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = FormType == "entry" ? AppConstant.Program.PatientIncident : AppConstant.Program.PatientIncidentVerification;

            if (!IsPostBack)
            {
                if (FormType == "entry" && AppSession.Parameter.HealthcareInitial != "RSCH")
                    ComboBox.PopulateWithServiceUnit(cboServiceUnitID, true);
                else
                    ComboBox.PopulateWithServiceUnit(cboServiceUnitID, false);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }

        public override bool OnButtonOkClicked()
        {
            var query = new PatientIncidentQuery("a");
            var qsu = new ServiceUnitQuery("b");
            var qreg = new RegistrationQuery("d");
            var qpat = new PatientQuery("e");
            var qrefgr = new AppStandardReferenceItemQuery("f");
            var sal = new AppStandardReferenceItemQuery("sal");

            query.InnerJoin(qsu).On(query.ServiceUnitIDInCharge == qsu.ServiceUnitID);
            query.LeftJoin(qreg).On(query.RegistrationNo == qreg.RegistrationNo);
            query.LeftJoin(qpat).On(qreg.PatientID == qpat.PatientID);
            query.LeftJoin(qrefgr).On(query.SRIncidentGroup == qrefgr.ItemID & qrefgr.StandardReferenceID == AppEnum.StandardReference.IncidentGroup.ToString());
            query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == qpat.SRSalutation);

            if (FormType == "entry")
            {
                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                    query.Where(query.InsertByUserID == AppSession.UserLogin.UserID,
                                query.Or(query.IsVerified == false, query.IsVerified.IsNull()));
                else
                {
                    var qusu = new AppUserServiceUnitQuery("c");
                    query.InnerJoin(qusu).On(query.ServiceUnitIDInCharge == qusu.ServiceUnitID);
                    query.Where(qusu.UserID == AppSession.UserLogin.UserID);
                }
            }
            query.Where(query.IsRiskManagement == false);

            query.OrderBy
                (
                    query.IncidentDateTime.Descending
                );

            query.Select(
                query.PatientIncidentNo,
                        query.IncidentDateTime,
                        query.RegistrationNo,
                        qpat.MedicalNo,
                        query.PatientName,
                        sal.ItemName.As("SalutationName"),
                        query.ServiceUnitIDInCharge.As("ServiceUnitID"),
                        qsu.ServiceUnitName,

                        query.SRIncidentType,
                        query.SRIncidentGroup,
                        query.SRClinicalImpact,
                        query.SRIncidentFollowUp,
                        query.ReportingDateTime,
                        query.ReportedByUserID,
                        qrefgr.ItemName.As("IncidentGroupName"),
                        query.IsApproved,
                        query.IsVerified
                );

            if (!string.IsNullOrEmpty(txtPatientIncidentNo.Text))
            {
                if (cboFilterPatientIncidentNo.SelectedIndex == 1)
                    query.Where(query.PatientIncidentNo == txtPatientIncidentNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPatientIncidentNo.Text);
                    query.Where(query.PatientIncidentNo.Like(searchTextContain));
                }
            }
            if (!txtIncidentDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.IncidentDateTime.Date() == txtIncidentDate.SelectedDate);
            }
            if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
            {
                if (cboFilterRegistrationNo.SelectedIndex == 1)
                    query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtRegistrationNo.Text);
                    query.Where(query.RegistrationNo.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtPatientName.Text))
            {
                var searchPatient = Helper.EscapeQuery(txtPatientName.Text);
                if (cboFilterPatientName.SelectedIndex == 1)
                    query.Where(string.Format(@"<LTRIM(RTRIM(LTRIM(a.FirstName + ' ' + a.MiddleName)) + ' ' + a.LastName) = '{0}'>", searchPatient));
                else
                    query.Where(string.Format(@"<LTRIM(RTRIM(LTRIM(a.FirstName + ' ' + a.MiddleName)) + ' ' + a.LastName) LIKE '%{0}%'>", searchPatient));
            }
            if (!string.IsNullOrEmpty(txtMedicalNo.Text))
            {
                if (AppSession.Parameter.IsMedicalNoContainStrip)
                    query.Where(string.Format("<REPLACE(e.MedicalNo, '-', '') LIKE '%{0}%'>", txtMedicalNo.Text.Replace("-", "")));
                else
                    query.Where(string.Format("<e.MedicalNo LIKE '%{0}%'>", txtMedicalNo.Text));
            }
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                query.Where(query.ServiceUnitIDInCharge == cboServiceUnitID.SelectedValue);
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}

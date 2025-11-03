using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PrintDialog : BasePageDialog
    {
        private AppAutoNumberLast _empMedicalInsuranceAutoNumber;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.SelectedDate = DateTime.Now;

                var unit = new ServiceUnitCollection();
                unit.Query.Where(unit.Query.SRRegistrationType == AppConstant.RegistrationType.OutPatient, unit.Query.IsActive == true);
                unit.Query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.LoadAll();

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                var mit = new AppStandardReferenceItemCollection();
                mit.Query.Where(mit.Query.StandardReferenceID == "MedicalInsuranceType", mit.Query.IsActive == true);
                mit.Query.OrderBy(mit.Query.ItemID.Ascending);
                mit.LoadAll();

                foreach (AppStandardReferenceItem entity in mit)
                {
                    rblMedicalInsuranceType.Items.Add(new ListItem(entity.ItemName, entity.ItemID));
                }

                _empMedicalInsuranceAutoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.EmpMedicalInsuranceNo, "", AppSession.UserLogin.UserID);

                txtGuaranteeNumber.Text = _empMedicalInsuranceAutoNumber.LastCompleteNumber;
                GetEmployeeName();
                GetFamilyName();

                if (Request.QueryString["pfId"].ToInt() == -1)
                {
                    rblStatus.SelectedIndex = 0;
                    rblStatus.Enabled = false;
                    txtPatientName.Text = txtEmployeeName.Text;
                    txtRelationship.Text = "DIRI SENDIRI";
                }
                if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
                    GetInsuranceType();
                else
                    rblMedicalInsuranceType.Enabled = true;
            }
        }

        private void GetEmployeeName()
        {
            var pi = new PersonalInfo();
            if (pi.LoadByPrimaryKey(Request.QueryString["pId"].ToInt()))
            {
                txtEmployeeNumber.Text = pi.EmployeeNumber;
                txtEmployeeName.Text = pi.EmployeeName;
                hdnGender.Value = pi.SRGenderType;
                hdnMaritalStatus.Value = pi.SRMaritalStatus;
            }
            else
            {
                txtEmployeeNumber.Text = string.Empty;
                txtEmployeeName.Text = string.Empty;
                hdnGender.Value = string.Empty;
                hdnMaritalStatus.Value = string.Empty;
            }
        }

        private void GetFamilyName()
        {
            var pf = new PersonalFamily();
            if (pf.LoadByPrimaryKey(Request.QueryString["pfId"].ToInt()))
            {
                txtPatientName.Text = pf.FamilyName;
                txtRelationship.Text = string.Empty;
                if (!string.IsNullOrEmpty(pf.SRFamilyRelation))
                {
                    var rel = new AppStandardReferenceItem();
                    if (rel.LoadByPrimaryKey("FamilyRelation", pf.SRFamilyRelation))
                        txtRelationship.Text = rel.ItemName;
                }
            }
            else
            {
                txtPatientName.Text = string.Empty;
                txtRelationship.Text = string.Empty;
            }
        }

        private void GetInsuranceType()
        {
            if (rblStatus.SelectedIndex == 0)
                rblMedicalInsuranceType.SelectedValue = "01";
            else
            {
                if (hdnGender.Value == "M" || AppSession.Parameter.EmployeeMaritalStatusForMedicalInsurance.Contains(hdnMaritalStatus.Value))
                    rblMedicalInsuranceType.SelectedValue = "01";
                else
                    rblMedicalInsuranceType.SelectedValue = "02";
            }    
        }

        protected void rblStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblStatus.SelectedIndex == 0)
            {
                txtPatientName.Text = txtEmployeeName.Text;
                txtRelationship.Text = "DIRI SENDIRI";
            }
            else
                GetFamilyName();

            if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
                GetInsuranceType();
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.print = '" + Page.Request.QueryString["id"] + "|" + AppSession.Parameter.EmployeeMedicalInsuranceFormRpt + "'";
        }

        public override bool OnButtonOkClicked()
        {
            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                ShowInformationHeader("Polyclinic is required.");
                return false;
            }

            var insNo = string.Empty;

            using (esTransactionScope trans = new esTransactionScope())
            {
                var emi = new EmployeeMedicalInsurance();
                emi.AddNew();
                _empMedicalInsuranceAutoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.EmpMedicalInsuranceNo, "", AppSession.UserLogin.UserID);

                insNo = _empMedicalInsuranceAutoNumber.LastCompleteNumber;
                _empMedicalInsuranceAutoNumber.Save();

                emi.MedicalInsuranceNo = insNo;
                emi.PersonID = Request.QueryString["pId"].ToInt();
                emi.PersonalFamilyID = rblStatus.SelectedIndex == 0 ? -1 : Request.QueryString["pfId"].ToInt();
                emi.ForTreatmentDate = txtDate.SelectedDate;
                emi.ServiceUnitID = cboServiceUnitID.SelectedValue;
                emi.SRMedicalInsuranceType = rblMedicalInsuranceType.SelectedValue;
                emi.Complaint = txtComplaint.Text;
                emi.LastUpdateDateTime = DateTime.Now;
                emi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                emi.Save();

                var pf = new PersonalFamily();
                if (pf.LoadByPrimaryKey(Request.QueryString["pfId"].ToInt()))
                {
                    pf.SRCoverageType = emi.SRMedicalInsuranceType;
                    pf.Save();
                }
                    
                trans.Complete();
            }

            if (!string.IsNullOrEmpty(insNo))
            {
                AppSession.PrintShowToolBarPrint = false;
                var jobParameters = new PrintJobParameterCollection();

                var pId = jobParameters.AddNew();
                pId.Name = "p_MedicalInsuranceNo";
                pId.ValueString = insNo;

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppSession.Parameter.EmployeeMedicalInsuranceFormRpt;

                return true;
            }

            ShowInformationHeader("Save failed.");
            return false;
        }
    }
}
using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class AppointmentChangePatientDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var btkOk = (Button)Helper.FindControlRecursive(Master, "btnOk");

            if (!IsPostBack)
            {
                
            }
           
        }

        
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind:'";
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            var patient = new Patient();
            if (patient.LoadByPrimaryKey(cboPatientID.SelectedValue))
            {
                patient.Ssn = txtSsn.Text;
                patient.GuarantorCardNo = txtGuarantorCardNo.Text;
                patient.MobilePhoneNo = txtMobilePhoneNo.Text;                
                patient.Save();
            }

            var appt = new BusinessObject.Appointment();
            if (appt.LoadByPrimaryKey(Request.QueryString["AppointmentNo"]))
            {
                switch (appt.SRAppointmentStatus) {
                    case "AppoinmentStatus-003": {
                        ShowInformationHeader("Appointment status is canceled, appointment can not be changed");
                        return false;
                        break;
                    }
                    case "AppoinmentStatus-004":
                        {
                            ShowInformationHeader("Appointment status is closed, appointment can not be changed");
                            return false;
                            break;
                        }
                    default: {
                        var pat = new Patient();
                        if (pat.LoadByPrimaryKey(cboPatientID.SelectedValue)) {
                            appt.PatientID = pat.PatientID;
                            appt.FirstName = pat.FirstName;
                            appt.MiddleName = pat.MiddleName;
                            appt.LastName = pat.LastName;
                            appt.DateOfBirth = pat.DateOfBirth;
                            appt.StreetName = pat.StreetName;
                            appt.District = pat.District;
                            appt.County = pat.County;
                            appt.City = pat.City;
                            appt.ZipCode = pat.ZipCode;
                            appt.PhoneNo = pat.PhoneNo;
                            appt.Email = pat.Email;
                            appt.Ssn = pat.Ssn;
                            appt.GuarantorCardNo = pat.GuarantorCardNo;
                            appt.MobilePhoneNo = pat.MobilePhoneNo;
                            appt.Save();
                        }
                        else
                        {
                            ShowInformationHeader("Patient not found");
                            return false;
                        }
                        break;
                    }
                }
            }
            else {
                ShowInformationHeader("Appointment not found");
                return false;
            }

            using (var trans = new esTransactionScope())
            {
                trans.Complete();
            }
            return true;
        }

        protected void cboPatientID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var pat = new Patient();
            if (pat.LoadByPrimaryKey(e.Value)) {
                txtFirstName.Text = pat.FirstName;
                txtMiddleName.Text = pat.MiddleName;
                txtLastName.Text = pat.LastName;
                txtDateOfBirth.SelectedDate = pat.DateOfBirth;
                txtAddress.Text = pat.Address;
                txtSsn.Text = pat.Ssn;
                txtGuarantorCardNo.Text = pat.GuarantorCardNo;
                txtMobilePhoneNo.Text = pat.MobilePhoneNo;                
                
            }
        }

        protected void cboPatientID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var dtbPatient = (new PatientCollection()).PatientRegisterAble(
                Helper.EscapeQuery(e.Text), string.Empty, string.Empty, string.Empty, 5);
            cboPatientID.DataSource = dtbPatient;
            cboPatientID.DataBind();
        }

        protected void cboPatientID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["MedicalNo"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
        }        
    }
}

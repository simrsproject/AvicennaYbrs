using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.CRM
{
    public partial class MembershipMemberItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadComboBox CboPatientID
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboPatientID"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                chkIsActive.Checked = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            cboPatientID.Enabled = false;

            var dtbPatient = (new PatientCollection()).PatientRegisterAble((String)DataBinder.Eval(DataItem, MembershipMemberMetadata.ColumnNames.PatientID), string.Empty, string.Empty, string.Empty, 10);
            cboPatientID.DataSource = dtbPatient;
            cboPatientID.DataBind();
            cboPatientID.SelectedValue = (String)DataBinder.Eval(DataItem, MembershipMemberMetadata.ColumnNames.PatientID);
            
            PopulatePatient(cboPatientID.SelectedValue);
            chkIsActive.Checked= Convert.ToBoolean(DataBinder.Eval(DataItem, MembershipMemberMetadata.ColumnNames.IsActive));
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                if (cboPatientID.SelectedValue == CboPatientID.SelectedValue)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Selected patients already registered");
                    return;
                }

                var coll = (MembershipMemberCollection)Session["collMembershipMember" + Request.UserHostName];

                bool isExist = false;
                foreach (MembershipMember item in coll)
                {
                    if (item.PatientID.Equals(cboPatientID.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Patient ID: {0} has exist", cboPatientID.SelectedValue);
                }
            }
        }

        private void PopulatePatient(string patientId)
        {
            var patient = new Patient();
            if (patient.LoadByPrimaryKey(patientId))
            {
                txtMedicalNo.Text = patient.MedicalNo;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName.Text = patient.PatientName;
                txtGender.Text = patient.Sex;
                txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));
                txtAgeDay.Value = Helper.GetAgeInDay(patient.DateOfBirth.Value);
                txtAgeMonth.Value = Helper.GetAgeInMonth(patient.DateOfBirth.Value);
                txtAgeYear.Value = Helper.GetAgeInYear(patient.DateOfBirth.Value);
                txtAddress.Text = patient.Address;
                txtPhoneNo.Text = patient.PhoneNo;
                txtMobilePhone.Text = patient.MobilePhoneNo;
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtSalutation.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtGender.Text = string.Empty;
                txtPlaceDOB.Text = string.Empty;
                txtAgeDay.Value = 0;
                txtAgeMonth.Value = 0;
                txtAgeYear.Value = 0;
                txtAddress.Text = string.Empty;
                txtPhoneNo.Text = string.Empty;
                txtMobilePhone.Text = string.Empty;
            }
        }

        protected void cboPatientID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Patient pat = new Patient();
            if (!pat.LoadByPrimaryKey(e.Value))
            {
                cboPatientID.Text = string.Empty;
                return;
            }
            PopulatePatient(e.Value);
        }

        protected void cboPatientID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var dtbPatient = (new PatientCollection()).PatientRegisterAble(e.Text, string.Empty, string.Empty, string.Empty, 10);
            cboPatientID.DataSource = dtbPatient;
            cboPatientID.DataBind();
        }

        protected void cboPatientID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
        }

        #region Properties for return entry value

        public String PatientID
        {
            get { return cboPatientID.SelectedValue; }
        }

        public bool IsActive
        {
            get { return chkIsActive.Checked; }
        }

        #endregion
    }
}
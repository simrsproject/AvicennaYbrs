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
    public partial class PatientInformationDetail : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.MedicalRecordHistory;

            ((Button)Helper.FindControlRecursive(Page, "btnOk")).Visible = false;
            ((Button)Helper.FindControlRecursive(Page, "btnCancel")).Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                PopulateEntryControl(Request.QueryString["patientID"]);


                //allergy data
                PatientAllergyCollection allergyCollection = new PatientAllergyCollection();
                allergyCollection.Query.Where(allergyCollection.Query.PatientID == txtPatientID.Text);
                allergyCollection.Query.OrderBy(allergyCollection.Query.AllergyGroup.Ascending);
                allergyCollection.LoadAll();

                AppStandardReferenceItemQuery query = new AppStandardReferenceItemQuery("a");

                query.Select
                    (
                        query.StandardReferenceID,
                        query.ItemID,
                        query.ItemName
                    );
                query.Where(query.StandardReferenceID.Like("%Allergen%"));

                DataTable tbl = AllergyTable(query.LoadDataTable());

                foreach (DataRow row in tbl.Rows)
                {
                    foreach (BusinessObject.PatientAllergy all in allergyCollection)
                    {
                        if (((string)row[1] == all.AllergyGroup) && ((string)row[2] == all.Allergen))
                        {
                            row[4] = all.DescAndReaction;
                            break;
                        }
                    }
                }

                tbl.AcceptChanges();

                ViewState["collPatientAllergy"] = tbl;
            }
        }

        private void PopulateEntryControl(string patientID)
        {
            //Load record
            Patient patient = new Patient();
            patient.LoadByPrimaryKey(patientID);

            //Populate Control
            txtPatientID.Text = patient.PatientID;
            txtMedicalNo.Text = patient.MedicalNo;
            txtSSN.Text = patient.Ssn;

            AppStandardReferenceItem salutation = new AppStandardReferenceItem();
            salutation.LoadByPrimaryKey("Salutation", patient.SRSalutation);
            txtSalutationName.Text = salutation.ItemName;

            txtFirstName.Text = patient.FirstName;
            txtMiddleName.Text = patient.MiddleName;
            txtLastName.Text = patient.LastName;
            txtCityOfBirth.Text = patient.CityOfBirth;
            txtDateOfBirth.SelectedDate = patient.DateOfBirth;
            //rbtSex.SelectedValue = patient.Sex;
            //rbtSex.Items[patient.Sex == "M" ? 0 : 1].Enabled = true;

            txtAgeYear.Text = patient.IsAlive == true ? Helper.GetAgeInYear(patient.DateOfBirth.Value).ToString() : Helper.GetAgeInYear(patient.DateOfBirth.Value, patient.DeceasedDateTime ?? DateTime.Now.Date).ToString();
            txtAgeMonth.Text = patient.IsAlive == true ? Helper.GetAgeInMonth(patient.DateOfBirth.Value).ToString() : Helper.GetAgeInMonth(patient.DateOfBirth.Value, patient.DeceasedDateTime ?? DateTime.Now.Date).ToString();
            txtAgeDay.Text = patient.IsAlive == true ? Helper.GetAgeInDay(patient.DateOfBirth.Value).ToString() : Helper.GetAgeInDay(patient.DateOfBirth.Value, patient.DeceasedDateTime ?? DateTime.Now.Date).ToString();

            AppStandardReferenceItem sex = new AppStandardReferenceItem();
            sex.LoadByPrimaryKey("GenderType", patient.Sex);
            txtSex.Text = sex.ItemName;

            AppStandardReferenceItem bloodtype = new AppStandardReferenceItem();
            bloodtype.LoadByPrimaryKey("BloodType", patient.SRBloodType);
            txtBloodType.Text = bloodtype.ItemName;

            txtBloodRhesus.Text = patient.BloodRhesus;

            AppStandardReferenceItem ethnic = new AppStandardReferenceItem();
            ethnic.LoadByPrimaryKey("Ethnic", patient.SREthnic);
            txtEthnic.Text = ethnic.ItemName;

            AppStandardReferenceItem education = new AppStandardReferenceItem();
            education.LoadByPrimaryKey("Education", patient.SREducation);
            txtEducation.Text = education.ItemName;

            AppStandardReferenceItem marital = new AppStandardReferenceItem();
            marital.LoadByPrimaryKey("MaritalStatus", patient.SRMaritalStatus);
            txtMaritalStatus.Text = marital.ItemName;

            AppStandardReferenceItem nationality = new AppStandardReferenceItem();
            nationality.LoadByPrimaryKey("Nationality", patient.SRNationality);
            txtNationality.Text = nationality.ItemName;

            AppStandardReferenceItem occupation = new AppStandardReferenceItem();
            occupation.LoadByPrimaryKey("Occupation", patient.SROccupation);
            txtOccupation.Text = occupation.ItemName;

            AppStandardReferenceItem patientcategory = new AppStandardReferenceItem();
            patientcategory.LoadByPrimaryKey("PatientCategory", patient.SRPatientCategory);
            txtPatientCategory.Text = patientcategory.ItemName;

            AppStandardReferenceItem religion = new AppStandardReferenceItem();
            religion.LoadByPrimaryKey("Religion", patient.SRReligion);
            txtReligion.Text = religion.ItemName;

            AppStandardReferenceItem medicalfilebin = new AppStandardReferenceItem();
            medicalfilebin.LoadByPrimaryKey("MedicalFileBin", patient.SRMedicalFileBin);
            txtMedicalFileBin.Text = medicalfilebin.ItemName;

            AppStandardReferenceItem medicalfilestatus = new AppStandardReferenceItem();
            medicalfilestatus.LoadByPrimaryKey("MedicalFileStatus", patient.SRMedicalFileStatus);
            txtMedicalFileStatus.Text = medicalfilestatus.ItemName;

            Guarantor guarantor = new Guarantor();
            guarantor.LoadByPrimaryKey(patient.GuarantorID);
            txtGuarantorName.Text = guarantor.GuarantorName;

            txtCompany.Text = patient.Company;

            //Address
            txtStreetName.Text = patient.StreetName;
            txtDistrict.Text = patient.District;
            txtCity.Text = patient.City;
            txtCounty.Text = patient.County;
            txtState.Text = patient.State;
            txtZipCode.Text = patient.ZipCode;
            txtPhoneNo.Text = patient.PhoneNo;
            txtMobilePhoneNo.Text = patient.MobilePhoneNo;
            txtEmail.Text = patient.Email;

            //Temporary Address
            txtTempAddressStreetName.Text = patient.TempAddressStreetName;
            txtTempAddressDistrict.Text = patient.TempAddressDistrict;
            txtTempAddressCity.Text = patient.TempAddressCity;
            txtTempAddressCounty.Text = patient.TempAddressCounty;
            txtTempAddressState.Text = patient.TempAddressState;
            txtTempAddressZipCode.Text = patient.TempAddressZipCode;

            txtLastVisitDate.SelectedDate = patient.LastVisitDate;
            txtNumberOfVisit.Value = Convert.ToDouble(patient.NumberOfVisit);
            txtOldMedicalNo.Text = patient.OldMedicalNo;
            txtAccountNo.Text = patient.AccountNo;
            chkIsDonor.Checked = patient.IsDonor ?? false;
            txtNumberOfDonor.Value = Convert.ToDouble(patient.NumberOfDonor);
            txtLastDonorDate.SelectedDate = patient.LastDonorDate;
            chkIsBlackList.Checked = patient.IsBlackList ?? false;
            chkIsAlive.Checked = !(patient.IsAlive ?? false);
            chkIsActive.Checked = patient.IsActive ?? false;
            txtNotes.Text = patient.Notes;
            txtDeathCertificateNo.Text = patient.DeathCertificateNo;
            txtDiagnosticNo.Text = patient.DiagnosticNo;

            PopulatePatientImage(patient.PatientID, patient.Sex);
        }

        protected void grdPatientAllergy_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPatientAllergy.DataSource = (DataTable)ViewState["collPatientAllergy"];
            grdPatientAllergy.MasterTableView.GroupsDefaultExpanded = false;
        }

        private DataTable AllergyTable(DataTable table)
        {
            DataTable tbl = new DataTable();

            tbl.Columns.Add("Group", typeof(string));
            tbl.Columns.Add("StandardReferenceID", typeof(string));
            tbl.Columns.Add("ItemID", typeof(string));
            tbl.Columns.Add("ItemName", typeof(string));
            tbl.Columns.Add("DescAndReaction", typeof(string));

            foreach (DataRow row in table.Rows)
            {
                tbl.Rows.Add(WordProcessing((string)row[0]), row[0], row[1], row[2], string.Empty);
            }

            return tbl;
        }

        private string WordProcessing(string value)
        {
            string capital = string.Empty;
            int index = 0;
            foreach (char c in value)
            {
                if (Char.IsUpper(c) && index > 0)
                {
                    capital = c.ToString();
                    break;
                }

                index++;
            }

            if (!capital.Equals(string.Empty))
                return value.Insert(index, " ");
            else
                return value;
        }

        #region PatientImage
        private void PopulatePatientImage(string patientID, string sex)
        {
            // Load from database
            var patientImg = new PatientImage();
            if (patientImg.LoadByPrimaryKey(patientID))
            {
                // Show Image
                if (patientImg.Photo != null)
                {
                    imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(patientImg.Photo));
                }
                else
                {
                    //imgPatientPhoto.ImageUrl = rbtSex.SelectedValue == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                    imgPatientPhoto.ImageUrl = sex == "M" ? "~/Images/Asset/Patient/ManVector.png" : (sex == "F" ? "~/Images/Asset/Patient/WomanVector.png": "~/Images/Asset/Patient/HumanVector.png");
                }
            }
            else
                //imgPatientPhoto.ImageUrl = rbtSex.SelectedValue == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                imgPatientPhoto.ImageUrl = sex == "M" ? "~/Images/Asset/Patient/ManVector.png" : (sex == "F" ? "~/Images/Asset/Patient/WomanVector.png" : "~/Images/Asset/Patient/HumanVector.png");

        }
        #endregion
    }
}
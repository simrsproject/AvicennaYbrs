using System;
using System.Linq;
using System.Web.UI;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalInfoDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;
        private void PopulateNewApplicantNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;

            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.ApplicantNo);
            txtEmployeeNumber.Text = _autoNumber.LastCompleteNumber;
        }

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PersonalInfoSearch.aspx?status=" + Request.QueryString["status"];
            UrlPageList = "PersonalInfoList.aspx?status=" + Request.QueryString["status"];

            if (Request.QueryString["status"] == "recruit")
            {
                ProgramID = AppConstant.Program.ApplicantPersonalInfo;
                lblEmployeeNumber.Text = "Applicant No";
                rfvEmployeeNumber.ErrorMessage = "Applicant No required.";
                rfvEmployeeNumber.Visible = !AppSession.Parameter.IsAutoCreateApplicantNo;
            }
            else
                ProgramID = AppConstant.Program.PersonalInfo; //TODO: Isi ProgramID

            txtPersonID.Text = "1";
            //StandardReference Inistialize
            if (!IsPostBack)
            {
                hdnPageId.Value = PageID;

                StandardReference.InitializeIncludeSpace(cboSRReligion, AppEnum.StandardReference.Religion);
                StandardReference.InitializeIncludeSpace(cboSREthnic, AppEnum.StandardReference.Ethnic);
                StandardReference.InitializeIncludeSpace(cboSRSalutation, AppEnum.StandardReference.Salutation);
                StandardReference.InitializeIncludeSpace(cboSRBloodType, AppEnum.StandardReference.BloodType);
                StandardReference.InitializeIncludeSpace(cboSRMaritalStatus, AppEnum.StandardReference.TaxStatus, "MARITAL");

                var cs = new ClassCollection();
                //cs.Query.Where(cs.Query.IsActive == true);
                if (cs.LoadAll())
                {
                    if (cboCoverageClass.Items.Count > 0) cboCoverageClass.Items.Clear();
                    if (cboCoverageClassBPJS.Items.Count > 0) cboCoverageClassBPJS.Items.Clear();

                    cboCoverageClass.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    cboCoverageClassBPJS.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                    foreach (var c in cs)
                    {
                        cboCoverageClass.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                        cboCoverageClassBPJS.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                    }
                }

                rfvSREthnic.Visible = AppSession.Parameter.HealthcareInitial == "RSMM";

                grdPersonalWorkExperience.Columns.FindByUniqueName("StartDate").Visible = AppSession.Parameter.IsPersonalWorkExperienceUsingDatePeriod;
                grdPersonalWorkExperience.Columns.FindByUniqueName("EndDate").Visible = AppSession.Parameter.IsPersonalWorkExperienceUsingDatePeriod;
                grdPersonalWorkExperience.Columns.FindByUniqueName("YearPeriod").Visible = !AppSession.Parameter.IsPersonalWorkExperienceUsingDatePeriod;
            }

            this.AjaxManager.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(AjaxManager_AjaxRequest);

            //PopUp Search
            if (!IsCallback)
            {
               
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        void AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdRecruitmentTest, grdRecruitmentTest);
            ajax.AddAjaxSetting(AjaxManager, grdRecruitmentTest);
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new PersonalInfo());
            cboCoverageClass.SelectedValue = string.Empty;
            cboCoverageClass.Text = string.Empty;

            if (Request.QueryString["status"] == "recruit")
            {
                txtEmployeeNumber.ReadOnly = AppSession.Parameter.IsAutoCreateApplicantNo;
                if (AppSession.Parameter.IsAutoCreateApplicantNo)
                    PopulateNewApplicantNo();
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            PersonalInfo entity = new PersonalInfo();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPersonID.Text)))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            PersonalInfo entity = new PersonalInfo();
            if (Request.QueryString["status"] != "recruit")
            {
                if (!string.IsNullOrEmpty(txtEmployeeNumber.Text))
                {
                    entity.Query.es.Top = 1;
                    entity.Query.Where(entity.Query.EmployeeNumber == txtEmployeeNumber.Text,
                        entity.Query.PersonID != Convert.ToInt32(txtPersonID.Text));
                    if (entity.Query.Load())
                    {
                        args.MessageText = "Employee No already exist";
                        args.IsCancel = true;
                        return;
                    }
                }
                entity = new PersonalInfo();
            }
            else
            {
                if (AppSession.Parameter.IsAutoCreateApplicantNo)
                {
                    PopulateNewApplicantNo();
                    _autoNumber.Save();
                }
            }

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            PersonalInfo entity = new PersonalInfo();

            if (!string.IsNullOrEmpty(txtEmployeeNumber.Text))
            {
                entity.Query.es.Top = 1;
                entity.Query.Where(entity.Query.EmployeeNumber == txtEmployeeNumber.Text,
                    entity.Query.PersonID != Convert.ToInt32(txtPersonID.Text));
                if (entity.Query.Load())
                {
                    args.MessageText = "Employee No already exist";
                    args.IsCancel = true;
                    return;
                }
            }
            entity = new PersonalInfo();


            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPersonID.Text)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("PersonID='{0}'", txtPersonID.Text.Trim());
            auditLogFilter.TableName = "PersonalInfo";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtPersonID.Enabled = (newVal == AppEnum.DataMode.New);
            if (Request.QueryString["status"] == "recruit" && AppSession.Parameter.IsAutoCreateApplicantNo)
                txtEmployeeNumber.ReadOnly = true;

            RefreshCommandItemPersonalAddress(newVal);
            RefreshCommandItemPersonalContact(newVal);
            RefreshCommandItemPersonalIdentification(newVal);
            RefreshCommandItemPersonalFamily(newVal);
            RefreshCommandItemPersonalEmergencyContact(newVal);
            RefreshCommandItemPersonalWorkExperience(newVal);
            RefreshCommandItemPersonalEducationHistory(newVal);
            RefreshCommandItemPersonalLicence(newVal);
            RefreshCommandItemPersonalOrganization(newVal);
            RefreshCommandItemPersonalPhysical(newVal);
            RefreshCommandItemRecruitmentTest(newVal);

            btnUploadImage.Visible = (newVal == AppEnum.DataMode.Read);
            ibtnPrintMedicalInsurance.Visible = (newVal == AppEnum.DataMode.Read) && AppSession.Parameter.IsVisibleEmployeeMedicalInsuranceForm;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            PersonalInfo entity = new PersonalInfo();
            if (parameters.Length > 0)
            {
                string personID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(personID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtPersonID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            PersonalInfo personalInfo = (PersonalInfo)entity;
            txtPersonID.Value = personalInfo.PersonID != null ? Convert.ToDouble(personalInfo.PersonID) : -1;

            if (Request.QueryString["status"] == "recruit")
            {
                var epq = new EmployeeEmploymentPeriodQuery();
                epq.Select(epq.EmployeeNumber);
                epq.Where(epq.PersonID == txtPersonID.Value.ToInt(), epq.SREmploymentType == "0");
                DataTable epdt = epq.LoadDataTable();
                if (epdt.Rows.Count > 0)
                    txtEmployeeNumber.Text = epdt.Rows[0]["EmployeeNumber"].ToString();
                else
                    txtEmployeeNumber.Text = personalInfo.EmployeeNumber;
            }
            else
                txtEmployeeNumber.Text = personalInfo.EmployeeNumber;
            txtFirstName.Text = personalInfo.FirstName;
            txtMiddleName.Text = personalInfo.MiddleName;
            txtLastName.Text = personalInfo.LastName;
            txtPreTitle.Text = personalInfo.PreTitle;
            txtPostTitle.Text = personalInfo.PostTitle;
            txtBirthName.Text = personalInfo.BirthName;
            txtPlaceBirth.Text = personalInfo.PlaceBirth;
            txtBirthDate.SelectedDate = personalInfo.BirthDate;
            txtAgeInYear.Text = Helper.GetAgeInYear(txtBirthDate.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
            txtAgeInMonth.Text = Helper.GetAgeInMonth(txtBirthDate.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
            txtAgeInDay.Text = Helper.GetAgeInDay(txtBirthDate.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();

            rbtSex.SelectedValue = personalInfo.SRGenderType;
            cboSRReligion.SelectedValue = personalInfo.SRReligion;
            cboSREthnic.SelectedValue = personalInfo.SREthnic;
            cboSRSalutation.SelectedValue = personalInfo.SRSalutation;
            cboSRBloodType.SelectedValue = personalInfo.SRBloodType;
            cboSRMaritalStatus.SelectedValue = personalInfo.SRMaritalStatus;

            //if (!string.IsNullOrEmpty(personalInfo.PatientID))
            {
                var patq = new PatientQuery();
                patq.Where(patq.PatientID == (personalInfo.PatientID ?? string.Empty));
                patq.Select(
                    patq.PatientID,
                    patq.MedicalNo,
                    patq.PatientName,
                    patq.Sex,
                    patq.Address,
                    patq.PhoneNo,
                    patq.MobilePhoneNo,
                    patq.DateOfBirth);
                cboPatientID.DataSource = patq.LoadDataTable();
                cboPatientID.DataBind();

                cboPatientID.SelectedValue = personalInfo.PatientID;
                if (string.IsNullOrEmpty(cboPatientID.SelectedValue)) cboPatientID.Text = string.Empty;

                PopulatePatientInformation(personalInfo.PatientID);
            }

            cboCoverageClass.SelectedValue = personalInfo.CoverageClass;
            cboCoverageClassBPJS.SelectedValue = personalInfo.CoverageClassBPJS;

            //Display Data Detail
            PopulatePersonalAddressGrid();
            PopulatePersonalContactGrid();
            PopulatePersonalIdentificationGrid();
            PopulatePersonalFamilyGrid();
            PopulatePersonalEmergencyContactGrid();
            PopulatePersonalWorkExperienceGrid();
            PopulatePersonalEducationHistoryGrid();
            PopulatePersonalLicenceGrid();
            PopulatePersonalOrganizationGrid();
            PopulatePersonalPhysicalGrid();
            PopulateRecruitmentTestlGrid();

            PopulateEmployeeImage(Convert.ToInt32(txtPersonID.Value), rbtSex.SelectedValue);
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(PersonalInfo entity)
        {
            entity.PersonID = Convert.ToInt32(txtPersonID.Value);
            if (Request.QueryString["status"] == "recruit")
            {
                var epq = new EmployeeEmploymentPeriodQuery();
                epq.Select(epq.EmployeeNumber);
                epq.Where(epq.PersonID == txtPersonID.Value.ToInt(), epq.SREmploymentType != "0");
                epq.OrderBy(epq.ValidFrom.Ascending);
                epq.es.Top = 1;
                DataTable epdt = epq.LoadDataTable();
                if (epdt.Rows.Count > 0)
                    entity.EmployeeNumber = epdt.Rows[0]["EmployeeNumber"].ToString();
                else
                    entity.EmployeeNumber = txtEmployeeNumber.Text;
            }
            else
                entity.EmployeeNumber = txtEmployeeNumber.Text;

            entity.FirstName = txtFirstName.Text;
            entity.MiddleName = txtMiddleName.Text;
            entity.LastName = txtLastName.Text;
            entity.PreTitle = txtPreTitle.Text;
            entity.PostTitle = txtPostTitle.Text;
            entity.BirthName = txtBirthName.Text;
            entity.PlaceBirth = txtPlaceBirth.Text;
            entity.BirthDate = txtBirthDate.SelectedDate;
            entity.SRGenderType = rbtSex.SelectedValue;
            entity.SRReligion = cboSRReligion.SelectedValue;
            entity.SREthnic = cboSREthnic.SelectedValue;
            entity.SRSalutation = cboSRSalutation.SelectedValue;
            entity.SRBloodType = cboSRBloodType.SelectedValue;
            entity.SRMaritalStatus = cboSRMaritalStatus.SelectedValue;
            entity.PatientID = cboPatientID.SelectedValue;
            entity.CoverageClass = cboCoverageClass.SelectedValue;
            entity.CoverageClassBPJS = cboCoverageClassBPJS.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(PersonalInfo entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                txtPersonID.Value = entity.PersonID;

                //--> Personal Address
                foreach (PersonalAddress address in PersonalAddresss)
                {
                    address.PersonID = Convert.ToInt32(txtPersonID.Text);
                    //Last Update Status
                    if (address.es.IsAdded || address.es.IsModified)
                    {
                        address.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        address.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> Personal Contact
                foreach (PersonalContact contacts in PersonalContacts)
                {
                    contacts.PersonID = Convert.ToInt32(txtPersonID.Text);
                    //Last Update Status
                    if (contacts.es.IsAdded || contacts.es.IsModified)
                    {
                        contacts.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        contacts.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> Personal Identification
                foreach (PersonalIdentification identification in PersonalIdentifications)
                {
                    identification.PersonID = Convert.ToInt32(txtPersonID.Text);
                    //Last Update Status
                    if (identification.es.IsAdded || identification.es.IsModified)
                    {
                        identification.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        identification.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> Personal family
                foreach (PersonalFamily family in PersonalFamilys)
                {
                    family.PersonID = Convert.ToInt32(txtPersonID.Text);
                    //Last Update Status
                    if (family.es.IsAdded || family.es.IsModified)
                    {
                        family.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        family.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> Personal Emergency Contact
                foreach (PersonalEmergencyContact emergency in PersonalEmergencyContacts)
                {
                    emergency.PersonID = Convert.ToInt32(txtPersonID.Text);
                    //Last Update Status
                    if (emergency.es.IsAdded || emergency.es.IsModified)
                    {
                        emergency.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        emergency.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> Personal Work Experience
                foreach (PersonalWorkExperience workExperience in PersonalWorkExperiences)
                {
                    workExperience.PersonID = Convert.ToInt32(txtPersonID.Text);
                    //Last Update Status
                    if (workExperience.es.IsAdded || workExperience.es.IsModified)
                    {
                        workExperience.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        workExperience.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> Personal Education History
                foreach (PersonalEducationHistory education in PersonalEducationHistorys)
                {
                    education.PersonID = Convert.ToInt32(txtPersonID.Text);
                    //Last Update Status
                    if (education.es.IsAdded || education.es.IsModified)
                    {
                        education.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        education.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> Personal Licence
                foreach (PersonalLicence licence in PersonalLicences)
                {
                    licence.PersonID = Convert.ToInt32(txtPersonID.Text);
                    //Last Update Status
                    if (licence.es.IsAdded || licence.es.IsModified)
                    {
                        licence.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        licence.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> Personal Organization
                foreach (PersonalOrganization organization in PersonalOrganizations)
                {
                    organization.PersonID = Convert.ToInt32(txtPersonID.Text);
                    //Last Update Status
                    if (organization.es.IsAdded || organization.es.IsModified)
                    {
                        organization.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        organization.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> Personal Physical
                foreach (PersonalPhysical physical in PersonalPhysicals)
                {
                    physical.PersonID = Convert.ToInt32(txtPersonID.Text);
                    //Last Update Status
                    if (physical.es.IsAdded || physical.es.IsModified)
                    {
                        physical.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        physical.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> Personal Test
                foreach (var physical in RecruitmentTests)
                {
                    physical.PersonID = Convert.ToInt32(txtPersonID.Text);
                    //Last Update Status
                    if (physical.es.IsAdded || physical.es.IsModified)
                    {
                        physical.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        physical.LastUpdateDateTime = DateTime.Now;
                    }
                }

                PersonalAddresss.Save();
                PersonalContacts.Save();
                PersonalIdentifications.Save();
                PersonalFamilys.Save();
                PersonalEmergencyContacts.Save();
                PersonalWorkExperiences.Save();
                PersonalEducationHistorys.Save();
                PersonalLicences.Save();
                PersonalOrganizations.Save();
                PersonalPhysicals.Save();
                RecruitmentTests.Save();

                string empType = string.Empty;
                string empStatus = string.Empty;
                var ewi = new EmployeeWorkingInfo();
                if (!ewi.LoadByPrimaryKey(Convert.ToInt32(entity.PersonID)))
                {
                    //empType = empWorkInfo.SREmployeeType;
                    //empStatus = empWorkInfo.SREmployeeStatus;
                    ewi = new EmployeeWorkingInfo();
                    ewi.PersonID = entity.PersonID;
                    ewi.JoinDate = DateTime.Now.Date;
                    ewi.SupervisorId = -1;
                    ewi.SREmployeeStatus = string.Empty;
                    ewi.SREmployeeType = string.Empty;
                    ewi.PositionGradeID = -1;
                    ewi.SRRemunerationType = string.Empty;
                    ewi.AbsenceCardNo = string.Empty;
                    ewi.Note = string.Empty;
                    ewi.LastUpdateDateTime = DateTime.Now;
                    ewi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    ewi.IsKWI = false;
                    ewi.GradeYear = 0;
                    ewi.SREducationLevel = string.Empty;
                    ewi.str.ResignDate = string.Empty;
                    ewi.SRResignReason = string.Empty;
                    ewi.Save();
                }

                var esi = new EmployeeSalaryInfo();
                if (esi.LoadByPrimaryKey(Convert.ToInt32(entity.PersonID)))
                {
                    var npwp = string.Empty;
                    var jamsostek = string.Empty;

                    var id = (PersonalIdentifications.Where(i => i.SRIdentificationType == "6" && i.ValidFrom <= DateTime.Now).OrderByDescending(i => i.ValidFrom)).Take(1).SingleOrDefault();
                    if (id != null) npwp = id.IdentificationValue;

                    id = (PersonalIdentifications.Where(i => i.SRIdentificationType == "5" && i.ValidFrom <= DateTime.Now).OrderByDescending(i => i.ValidFrom)).Take(1).SingleOrDefault();
                    if (id != null) jamsostek = id.IdentificationValue;

                    esi.Npwp = npwp;
                    esi.JamsostekNo = jamsostek;
                    esi.Save();
                }

                var info = new VwEmployeeTableQuery();
                info.Select(info.SREmployeeStatus, info.SREmploymentType);
                info.Where(info.PersonID == Convert.ToInt32(entity.PersonID));
                info.es.Top = 1;
                DataTable infoDtb = info.LoadDataTable();
                if (infoDtb.Rows.Count > 0)
                {
                    empType = infoDtb.Rows[0]["SREmploymentType"].ToString();
                    empStatus = infoDtb.Rows[0]["SREmployeeStatus"].ToString();
                }

                if (!string.IsNullOrEmpty(empType))
                {
                    var std = new AppStandardReferenceItem();
                    if (std.LoadByPrimaryKey("EmploymentType", empType))
                    {
                        string guarId = AppSession.Parameter.GuarantorEmployeeID;
                        if (!string.IsNullOrEmpty(std.ReferenceID))
                        {
                            var parArray = std.ReferenceID.Split('|');
                            guarId = Convert.ToString(parArray[0]);
                        }

                        //update table patient u/ karyawan --> PersonID, GuarantorID, SREmployeeRelationship
                        if (!string.IsNullOrEmpty(entity.PatientID))
                        {
                            var pat = new Patient();
                            if (pat.LoadByPrimaryKey(entity.PatientID))
                            {
                                pat.GuarantorID = empStatus == AppSession.Parameter.EmployeeStatusActive ? guarId : AppSession.Parameter.SelfGuarantor;
                                pat.PersonID = empStatus == AppSession.Parameter.EmployeeStatusActive ? entity.PersonID : 0;
                                pat.EmployeeNumber = empStatus == AppSession.Parameter.EmployeeStatusActive ? entity.EmployeeNumber : string.Empty;
                                pat.SREmployeeRelationship = empStatus == AppSession.Parameter.EmployeeStatusActive ? AppSession.Parameter.EmployeeRelationshipSelf : string.Empty;

                                pat.Save();
                            }
                        }

                        //update table patient u/ data keluarga --> PersonID, GuarantorID, SREmployeeRelationship
                        foreach (PersonalFamily family in PersonalFamilys.Where(family => !string.IsNullOrEmpty(family.PatientID)))
                        {
                            if (family.IsGuaranteed == true)
                            {
                                var pat = new Patient();
                                if (pat.LoadByPrimaryKey(family.PatientID))
                                {
                                    pat.GuarantorID = empStatus == AppSession.Parameter.EmployeeStatusActive ? guarId : AppSession.Parameter.SelfGuarantor;
                                    pat.PersonID = empStatus == AppSession.Parameter.EmployeeStatusActive ? entity.PersonID : 0;
                                    pat.EmployeeNumber = empStatus == AppSession.Parameter.EmployeeStatusActive ? entity.EmployeeNumber : string.Empty;

                                    std = new AppStandardReferenceItem();
                                    pat.SREmployeeRelationship = std.LoadByPrimaryKey("FamilyRelation", family.SRFamilyRelation) ? std.ReferenceID : string.Empty;

                                    if (empStatus != AppSession.Parameter.EmployeeStatusActive)
                                        pat.SREmployeeRelationship = string.Empty;

                                    pat.Save();
                                }
                            }
                            else
                            {
                                var pat = new Patient();
                                if (pat.LoadByPrimaryKey(family.PatientID))
                                {
                                    pat.GuarantorID = AppSession.Parameter.SelfGuarantor;
                                    pat.PersonID = -1;
                                    pat.EmployeeNumber = string.Empty;
                                    pat.SREmployeeRelationship = string.Empty;
                                    pat.Save();
                                }
                            }
                        }
                    }
                }

                if (Request.QueryString["status"] == "recruit")
                {
                    if (DataModeCurrent == AppEnum.DataMode.New)
                    {
                        var eep = new EmployeeEmploymentPeriod();
                        eep.AddNew();
                        eep.PersonID = entity.PersonID;
                        eep.SREmploymentType = "0";
                        eep.SREmploymentCategory = "";
                        eep.ValidFrom = DateTime.Now.Date;
                        eep.ValidTo = DateTime.Now.Date.AddYears(10);
                        eep.Note = string.Empty;
                        eep.LastUpdateDateTime = DateTime.Now;
                        eep.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        eep.RecruitmentPlanID = -1;
                        eep.EmployeeNumber = entity.EmployeeNumber;
                        eep.Save();
                    }
                    else
                    {
                        var eepq = new EmployeeEmploymentPeriodQuery();
                        eepq.Where(eepq.PersonID == entity.PersonID, eepq.SREmploymentType == "0");
                        var eep = new EmployeeEmploymentPeriod();
                        eep.Load(eepq);
                        if (eep == null)
                        {
                            eep.AddNew();
                            eep.PersonID = entity.PersonID;
                            eep.SREmploymentType = "0";
                            eep.SREmploymentCategory = "";
                            eep.ValidFrom = DateTime.Now.Date;
                            eep.ValidTo = DateTime.Now.Date.AddYears(10);
                            eep.Note = string.Empty;
                            eep.RecruitmentPlanID = -1;
                        }
                        eep.EmployeeNumber = txtEmployeeNumber.Text;
                        eep.LastUpdateDateTime = DateTime.Now;
                        eep.LastUpdateByUserID = AppSession.UserLogin.UserID;

                        eep.Save();
                    }
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            PersonalInfoQuery que = new PersonalInfoQuery("a");
            var info = new VwEmployeeTableQuery("b");
            que.InnerJoin(info).On(info.PersonID == que.PersonID);

            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PersonID > txtPersonID.Text);
                que.OrderBy(que.PersonID.Ascending);
            }
            else
            {
                que.Where(que.PersonID < txtPersonID.Text);
                que.OrderBy(que.PersonID.Descending);
            }

            if (Request.QueryString["status"] == "recruit")
                que.Where(info.SREmploymentType == "0"); // applicant
            else
                que.Where(info.SREmploymentType != "0");

            PersonalInfo entity = new PersonalInfo();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged
        //protected void Button_Click(object sender, System.EventArgs e)
        //{
        //    foreach (UploadedFile f in RadUpload1.UploadedFiles)
        //    {
        //        f.SaveAs("d:\\hrImages\\" + f.GetName(), true);
        //    }
        //}
        #endregion

        #region Record Detail Method Function PersonalAddress
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PersonalAddresss.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemPersonalAddress(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPersonalAddress.Columns[0].Visible = isVisible;
            grdPersonalAddress.Columns[grdPersonalAddress.Columns.Count - 1].Visible = isVisible;

            grdPersonalAddress.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPersonalAddress.Rebind();
        }

        private PersonalAddressCollection PersonalAddresss
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPersonalAddress" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((PersonalAddressCollection)(obj));
                    }
                }

                PersonalAddressCollection coll = new PersonalAddressCollection();
                AppStandardReferenceItemQuery city = new AppStandardReferenceItemQuery("e");
                AppStandardReferenceItemQuery state = new AppStandardReferenceItemQuery("d");
                AppStandardReferenceItemQuery address = new AppStandardReferenceItemQuery("c");
                PersonalAddressQuery query = new PersonalAddressQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");
                var zc = new ZipCodeQuery("f");

                query.Select
                    (
                       query.PersonalAddressID,
                       query.PersonID,
                       query.SRAddressType,
                       address.ItemName.As("refToAddressTypeName_PersonalAddress"),
                       query.Address,
                       query.ZipCode,
                       zc.ZipPostalCode.As("refToZipCode_ZipPostalCode"),
                       query.District,
                       query.County,
                       query.City,
                       query.SRState,
                       state.ItemName.As("refToStateName_PersonalAddress"),
                       query.SRCity,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.LeftJoin(address).On
                        (
                            query.SRAddressType == address.ItemID &
                            address.StandardReferenceID == "AddressType"
                        );
                query.LeftJoin(state).On
                        (
                            query.SRState == state.ItemID &
                            state.StandardReferenceID == "Province"
                        );
                query.LeftJoin(zc).On
                        (
                            query.ZipCode == zc.ZipCode
                        );
                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PersonalAddressID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collPersonalAddress" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collPersonalAddress" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulatePersonalAddressGrid()
        {
            //Display Data Detail
            PersonalAddresss = null; //Reset Record Detail
            grdPersonalAddress.DataSource = PersonalAddresss; //Requery
            grdPersonalAddress.MasterTableView.IsItemInserted = false;
            grdPersonalAddress.MasterTableView.ClearEditItems();
            grdPersonalAddress.DataBind();
        }

        protected void grdPersonalAddress_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalAddress.DataSource = PersonalAddresss;
        }

        protected void grdPersonalAddress_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 personalAddressID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PersonalAddressMetadata.ColumnNames.PersonalAddressID]);
            PersonalAddress entity = FindPersonalAddress(personalAddressID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPersonalAddress_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 personalAddressID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PersonalAddressMetadata.ColumnNames.PersonalAddressID]);
            PersonalAddress entity = FindPersonalAddress(personalAddressID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPersonalAddress_InsertCommand(object source, GridCommandEventArgs e)
        {
            PersonalAddress entity = PersonalAddresss.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPersonalAddress.Rebind();
        }
        private PersonalAddress FindPersonalAddress(Int32 personalAddressID)
        {
            PersonalAddressCollection coll = PersonalAddresss;
            PersonalAddress retEntity = null;
            foreach (PersonalAddress rec in coll)
            {
                if (rec.PersonalAddressID.ToString().Equals(personalAddressID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PersonalAddress entity, GridCommandEventArgs e)
        {
            PersonalAddressDetail userControl = (PersonalAddressDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.PersonalAddressID = userControl.PersonalAddressID;
                entity.SRAddressType = userControl.SRAddressType;
                entity.AddressTypeName = userControl.AddressTypeName;
                entity.Address = userControl.Address;
                entity.ZipCode = userControl.ZipCode;
                entity.ZipPostalCode = userControl.ZipPortalCode;
                entity.District = userControl.District;
                entity.County = userControl.County;
                entity.City = userControl.City;
                entity.SRState = userControl.SRState;
                entity.StateName = userControl.StateName;
                entity.SRCity = userControl.SRCity;
            }
        }

        #endregion

        #region Record Detail Method Function PersonalContact
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PersonalContacts.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemPersonalContact(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPersonalContact.Columns[0].Visible = isVisible;
            grdPersonalContact.Columns[grdPersonalContact.Columns.Count - 1].Visible = isVisible;

            grdPersonalContact.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPersonalContact.Rebind();
        }

        private PersonalContactCollection PersonalContacts
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPersonalContact" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((PersonalContactCollection)(obj));
                    }
                }

                PersonalContactCollection coll = new PersonalContactCollection();
                AppStandardReferenceItemQuery contact = new AppStandardReferenceItemQuery("b");
                PersonalContactQuery query = new PersonalContactQuery("a");

                query.Select
                    (
                       query.PersonalContactID,
                       query.PersonID,
                       query.SRContactType,
                       contact.ItemName.As("refToContactTypeName_PersonalContact"),
                       query.ContactValue,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(contact).On
                        (
                            query.SRContactType == contact.ItemID &
                            contact.StandardReferenceID == "ContactType"
                        );

                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PersonalContactID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collPersonalContact" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collPersonalContact" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulatePersonalContactGrid()
        {
            //Display Data Detail
            PersonalContacts = null; //Reset Record Detail
            grdPersonalContact.DataSource = PersonalContacts; //Requery
            grdPersonalContact.MasterTableView.IsItemInserted = false;
            grdPersonalContact.MasterTableView.ClearEditItems();
            grdPersonalContact.DataBind();
        }

        protected void grdPersonalContact_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalContact.DataSource = PersonalContacts;
        }

        protected void grdPersonalContact_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 personalContactr = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PersonalContactMetadata.ColumnNames.PersonalContactID]);
            PersonalContact entity = FindPersonalContact(personalContactr);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPersonalContact_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 personalContactr = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PersonalContactMetadata.ColumnNames.PersonalContactID]);
            PersonalContact entity = FindPersonalContact(personalContactr);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPersonalContact_InsertCommand(object source, GridCommandEventArgs e)
        {
            PersonalContact entity = PersonalContacts.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPersonalContact.Rebind();
        }

        private PersonalContact FindPersonalContact(Int32 PersonalContactID)
        {
            PersonalContactCollection coll = PersonalContacts;
            PersonalContact retEntity = null;
            foreach (PersonalContact rec in coll)
            {
                if (rec.PersonalContactID.ToString().Equals(PersonalContactID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PersonalContact entity, GridCommandEventArgs e)
        {
            PersonalContactDetail userControl = (PersonalContactDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.PersonalContactID = userControl.PersonalContactID;
                entity.SRContactType = userControl.SRContactType;
                entity.ContactTypeName = userControl.ContactTypeName;
                entity.ContactValue = userControl.ContactValue;

            }
        }

        #endregion

        #region Record Detail Method Function PersonalIdentification
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PersonalIdentifications.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemPersonalIdentification(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPersonalIdentification.Columns[0].Visible = isVisible;
            grdPersonalIdentification.Columns[grdPersonalIdentification.Columns.Count - 1].Visible = isVisible;
            grdPersonalIdentification.Columns.FindByUniqueName("DocumentUpload").Visible = !isVisible;

            grdPersonalIdentification.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;


            //Perbaharui tampilan dan data
            grdPersonalIdentification.Rebind();
        }

        private PersonalIdentificationCollection PersonalIdentifications
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPersonalIdentification" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((PersonalIdentificationCollection)(obj));
                    }
                }

                PersonalIdentificationCollection coll = new PersonalIdentificationCollection();
                AppStandardReferenceItemQuery identification = new AppStandardReferenceItemQuery("c");
                PersonalIdentificationQuery query = new PersonalIdentificationQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");

                query.Select
                    (
                       query.PersonalIdentificationID,
                       query.PersonID,
                       query.SRIdentificationType,
                       identification.ItemName.As("refToIdentificationTypeName_PersonalIdentification"),
                       query.IdentificationValue,
                       query.IdentificationName,
                       query.PlaceOfIssue,
                       query.ValidFrom,
                       query.ValidTo,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.LeftJoin(identification).On
                        (
                            query.SRIdentificationType == identification.ItemID &
                            identification.StandardReferenceID == "IdentificationType"
                        );

                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PersonalIdentificationID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collPersonalIdentification" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collPersonalIdentification" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulatePersonalIdentificationGrid()
        {
            //Display Data Detail
            PersonalIdentifications = null; //Reset Record Detail
            grdPersonalIdentification.DataSource = PersonalIdentifications; //Requery
            grdPersonalIdentification.MasterTableView.IsItemInserted = false;
            grdPersonalIdentification.MasterTableView.ClearEditItems();
            grdPersonalIdentification.DataBind();
        }

        protected void grdPersonalIdentification_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalIdentification.DataSource = PersonalIdentifications;
        }

        protected void grdPersonalIdentification_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 personalIdentificationID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PersonalIdentificationMetadata.ColumnNames.PersonalIdentificationID]);
            PersonalIdentification entity = FindPersonalIdentification(personalIdentificationID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPersonalIdentification_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 personalIdentificationID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PersonalIdentificationMetadata.ColumnNames.PersonalIdentificationID]);
            PersonalIdentification entity = FindPersonalIdentification(personalIdentificationID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPersonalIdentification_InsertCommand(object source, GridCommandEventArgs e)
        {
            PersonalIdentification entity = PersonalIdentifications.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPersonalIdentification.Rebind();
        }
        private PersonalIdentification FindPersonalIdentification(Int32 personalIdentificationID)
        {
            PersonalIdentificationCollection coll = PersonalIdentifications;
            PersonalIdentification retEntity = null;
            foreach (PersonalIdentification rec in coll)
            {
                if (rec.PersonalIdentificationID.ToString().Equals(personalIdentificationID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PersonalIdentification entity, GridCommandEventArgs e)
        {
            PersonalIdentificationDetail userControl = (PersonalIdentificationDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.PersonalIdentificationID = userControl.PersonalIdentificationID;
                entity.SRIdentificationType = userControl.SRIdentificationType;
                entity.IdentificationTypeName = userControl.IdentificationTypeName;
                entity.IdentificationValue = userControl.IdentificationValue;
                entity.IdentificationName = userControl.IdentificationName;
                entity.PlaceOfIssue = userControl.PlaceOfIssue;

                if (userControl.ValidFrom == null)
                    entity.str.ValidFrom = string.Empty;
                else
                    entity.ValidFrom = userControl.ValidFrom;

                if (userControl.ValidTo == null)
                    entity.str.ValidTo = string.Empty;
                else
                    entity.ValidTo = userControl.ValidTo;
            }
        }

        #endregion

        #region Record Detail Method Function PersonalFamily
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PersonalFamilys.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemPersonalFamily(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPersonalFamily.Columns[0].Visible = isVisible;
            grdPersonalFamily.Columns[grdPersonalFamily.Columns.Count - 1].Visible = isVisible;
            grdPersonalFamily.Columns.FindByUniqueName("PrintDialog").Visible = !isVisible && AppSession.Parameter.IsVisibleEmployeeMedicalInsuranceForm;

            grdPersonalFamily.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPersonalFamily.Rebind();
        }

        private PersonalFamilyCollection PersonalFamilys
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPersonalFamily" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((PersonalFamilyCollection)(obj));
                    }
                }

                PersonalFamilyCollection coll = new PersonalFamilyCollection();
                PatientQuery patient = new PatientQuery("f");
                AppStandardReferenceItemQuery gender = new AppStandardReferenceItemQuery("e");
                AppStandardReferenceItemQuery marital = new AppStandardReferenceItemQuery("d");
                AppStandardReferenceItemQuery relation = new AppStandardReferenceItemQuery("c");
                PersonalFamilyQuery query = new PersonalFamilyQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");
                AppStandardReferenceItemQuery coverage = new AppStandardReferenceItemQuery("g");
                var zc = new ZipCodeQuery("zc");
                var cls1 = new ClassQuery("cls1");
                var cls2 = new ClassQuery("cls2");

                query.Select
                    (
                       query.PersonalFamilyID,
                       query.PersonID,
                       query.PatientID,
                       patient.MedicalNo.As("refToPatient_Medical"),
                       query.SRFamilyRelation,
                       relation.ItemName.As("refTo_FamilyRelationName"),
                       query.FamilyName,
                       query.CityOfBirth,
                       query.DateBirth,
                       query.Address,
                       query.ZipCode,
                       zc.ZipPostalCode.As("refToZipCode_ZipPostalCode"),
                       query.Phone,
                       query.SRMaritalStatus,
                       marital.ItemName.As("refTo_MaritalStatusName"),
                       query.SRGenderType,
                       gender.ItemName.As("refTo_GenderTypeName"),
                       query.SREducationLevel,
                       query.SRState,
                       query.SRCity,
                       query.IsGuaranteed,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime,
                       query.SRCoverageType,
                       coverage.ItemName.As("refTo_CoverageTypeName"),
                       query.BPJSKesehatanNo,
                       query.WeddingDate,
                       query.SRFamilyOccupation,
                       query.District,
                       query.County,
                       query.City,
                       query.CoverageClass,
                        cls1.ClassName.As("refTo_CoverageClassName"),
                       query.CoverageClassBPJS,
                       cls2.ClassName.As("refTo_CoverageClassBPJSName")
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID & query.PersonID == txtPersonID.Text);
                query.LeftJoin(relation).On
                        (
                            query.SRFamilyRelation == relation.ItemID &
                            relation.StandardReferenceID == "FamilyRelation"
                        );
                query.LeftJoin(marital).On
                        (
                            query.SRMaritalStatus == marital.ItemID &
                            marital.StandardReferenceID == "MaritalStatus"
                        );
                query.LeftJoin(gender).On
                        (
                            query.SRGenderType == gender.ItemID &
                            gender.StandardReferenceID == "GenderType"
                        );
                query.LeftJoin(coverage).On
                        (
                            query.SRCoverageType == coverage.ItemID &
                            coverage.StandardReferenceID == "EmployeeCoverageType"
                        );
                query.LeftJoin(patient).On
                    (
                        query.PatientID == patient.PatientID
                    );
                query.LeftJoin(zc).On
                        (
                            query.ZipCode == zc.ZipCode
                        );
                query.LeftJoin(cls1).On(cls1.ClassID == query.CoverageClass);
                query.LeftJoin(cls2).On(cls2.ClassID == query.CoverageClassBPJS);

                //query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PersonalFamilyID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collPersonalFamily" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collPersonalFamily" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulatePersonalFamilyGrid()
        {
            //Display Data Detail
            PersonalFamilys = null; //Reset Record Detail
            grdPersonalFamily.DataSource = PersonalFamilys; //Requery
            grdPersonalFamily.MasterTableView.IsItemInserted = false;
            grdPersonalFamily.MasterTableView.ClearEditItems();
            grdPersonalFamily.DataBind();
        }

        protected void grdPersonalFamily_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalFamily.DataSource = PersonalFamilys;
        }

        protected void grdPersonalFamily_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 personalFamilyID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PersonalFamilyMetadata.ColumnNames.PersonalFamilyID]);
            PersonalFamily entity = FindPersonalFamily(personalFamilyID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPersonalFamily_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 personalFamilyID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PersonalFamilyMetadata.ColumnNames.PersonalFamilyID]);
            PersonalFamily entity = FindPersonalFamily(personalFamilyID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPersonalFamily_InsertCommand(object source, GridCommandEventArgs e)
        {
            PersonalFamily entity = PersonalFamilys.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPersonalFamily.Rebind();
        }
        private PersonalFamily FindPersonalFamily(Int32 personalFamilyID)
        {
            PersonalFamilyCollection coll = PersonalFamilys;
            PersonalFamily retEntity = null;
            foreach (PersonalFamily rec in coll)
            {
                if (rec.PersonalFamilyID.ToString().Equals(personalFamilyID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PersonalFamily entity, GridCommandEventArgs e)
        {
            PersonalFamilyDetail userControl = (PersonalFamilyDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.PersonalFamilyID = userControl.PersonalFamilyID;
                entity.PatientID = userControl.PatientID;
                entity.MedicalNo = userControl.MedicalNo;
                entity.SRFamilyRelation = userControl.SRFamilyRelation;
                entity.FamilyRelationName = userControl.FamilyRelationName;
                entity.FamilyName = userControl.FamilyName;
                entity.CityOfBirth = userControl.CityOfBirth;
                entity.DateBirth = userControl.DateBirth;
                entity.SRMaritalStatus = userControl.SRMaritalStatus;
                entity.MaritalStatusName = userControl.MaritalStatusName;
                entity.SRGenderType = userControl.SRGenderType;
                entity.GenderTypeName = userControl.GenderTypeName;
                entity.SREducationLevel = userControl.SREducationLevel;
                entity.Address = userControl.Address;
                entity.SRState = userControl.SRState;
                entity.SRCity = userControl.SRCity;
                entity.ZipCode = userControl.ZipCode;
                entity.ZipPostalCode = userControl.ZipPortalCode;
                entity.Phone = userControl.Phone;
                entity.IsGuaranteed = userControl.IsGuaranteed;
                entity.SRCoverageType = userControl.SRCoverageType;
                entity.CoverageTypeName = userControl.CoverageTypeName;
                entity.BPJSKesehatanNo = userControl.BpjsKesehatanNo;
                if (userControl.WeddingDate == null)
                    entity.str.WeddingDate = string.Empty;
                else
                    entity.WeddingDate = userControl.WeddingDate;
                entity.SRFamilyOccupation = userControl.SRFamilyOccupation;
                entity.District = userControl.District;
                entity.County = userControl.County;
                entity.City = userControl.City;
                entity.CoverageClass = userControl.CoverageClass;
                entity.CoverageClassName = userControl.CoverageClassName;
                entity.CoverageClassBPJS = userControl.CoverageClassBpjs;
                entity.CoverageClassBPJSName = userControl.CoverageClassBpjsName;
            }
        }

        #endregion

        #region Record Detail Method Function PersonalEmergencyContact
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PersonalEmergencyContacts.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemPersonalEmergencyContact(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPersonalEmergencyContact.Columns[0].Visible = isVisible;
            grdPersonalEmergencyContact.Columns[grdPersonalEmergencyContact.Columns.Count - 1].Visible = isVisible;

            grdPersonalEmergencyContact.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPersonalEmergencyContact.Rebind();
        }

        private PersonalEmergencyContactCollection PersonalEmergencyContacts
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPersonalEmergencyContact" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((PersonalEmergencyContactCollection)(obj));
                    }
                }

                PersonalEmergencyContactCollection coll = new PersonalEmergencyContactCollection();
                AppStandardReferenceItemQuery city = new AppStandardReferenceItemQuery("e");
                AppStandardReferenceItemQuery state = new AppStandardReferenceItemQuery("d");
                PersonalEmergencyContactQuery query = new PersonalEmergencyContactQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");
                var zc = new ZipCodeQuery("f");
                var fr = new AppStandardReferenceItemQuery("fr");

                query.Select
                    (
                       query.PersonalEmergencyContactID,
                       query.PersonID,
                       query.ContactName,
                       query.Address,
                       query.SRState,
                       state.ItemName.As("refToStateName_PersonalEmergencyContact"),
                       query.SRCity,
                       query.ZipCode,
                       zc.ZipPostalCode.As("refToZipCode_ZipPostalCode"),
                       query.Phone,
                       query.Mobile,
                       query.District,
                       query.County,
                       query.City,
                       query.SRFamilyRelation,
                       fr.ItemName.As("refTo_FamilyRelationName"),
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.LeftJoin(state).On
                        (
                            query.SRState == state.ItemID &
                            state.StandardReferenceID == "Province"
                        );
                query.LeftJoin(city).On
                        (
                            query.SRCity == city.ItemID &
                            city.StandardReferenceID == "City"
                        );
                query.LeftJoin(zc).On
                        (
                            query.ZipCode == zc.ZipCode
                        );
                query.LeftJoin(fr).On
                        (
                            query.SRFamilyRelation == fr.ItemID &
                            fr.StandardReferenceID == "FamilyRelation"
                        );
                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PersonalEmergencyContactID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collPersonalEmergencyContact" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collPersonalEmergencyContact" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulatePersonalEmergencyContactGrid()
        {
            //Display Data Detail
            PersonalEmergencyContacts = null; //Reset Record Detail
            grdPersonalEmergencyContact.DataSource = PersonalEmergencyContacts; //Requery
            grdPersonalEmergencyContact.MasterTableView.IsItemInserted = false;
            grdPersonalEmergencyContact.MasterTableView.ClearEditItems();
            grdPersonalEmergencyContact.DataBind();
        }

        protected void grdPersonalEmergencyContact_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalEmergencyContact.DataSource = PersonalEmergencyContacts;
        }

        protected void grdPersonalEmergencyContact_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 personalEmergencyContactID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PersonalEmergencyContactMetadata.ColumnNames.PersonalEmergencyContactID]);
            PersonalEmergencyContact entity = FindPersonalEmergencyContact(personalEmergencyContactID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPersonalEmergencyContact_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 personalEmergencyContactID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PersonalEmergencyContactMetadata.ColumnNames.PersonalEmergencyContactID]);
            PersonalEmergencyContact entity = FindPersonalEmergencyContact(personalEmergencyContactID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPersonalEmergencyContact_InsertCommand(object source, GridCommandEventArgs e)
        {
            PersonalEmergencyContact entity = PersonalEmergencyContacts.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPersonalEmergencyContact.Rebind();
        }
        private PersonalEmergencyContact FindPersonalEmergencyContact(Int32 personalEmergencyContactID)
        {
            PersonalEmergencyContactCollection coll = PersonalEmergencyContacts;
            PersonalEmergencyContact retEntity = null;
            foreach (PersonalEmergencyContact rec in coll)
            {
                if (rec.PersonalEmergencyContactID.ToString().Equals(personalEmergencyContactID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PersonalEmergencyContact entity, GridCommandEventArgs e)
        {
            PersonalEmergencyContactDetail userControl = (PersonalEmergencyContactDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.PersonalEmergencyContactID = userControl.PersonalEmergencyContactID;
                entity.ContactName = userControl.ContactName;
                entity.Address = userControl.Address;
                entity.SRState = userControl.SRState;
                entity.StateName = userControl.StateName;
                entity.SRCity = userControl.SRCity;
                entity.ZipCode = userControl.ZipCode;
                entity.ZipPostalCode = userControl.ZipPortalCode;
                entity.Phone = userControl.Phone;
                entity.Mobile = userControl.Mobile;
                entity.District = userControl.District;
                entity.County = userControl.County;
                entity.City = userControl.City;
                entity.SRFamilyRelation = userControl.SRFamilyRelation;
                entity.FamilyRelationName = userControl.FamilyRelationName;
            }
        }

        #endregion

        #region Record Detail Method Function PersonalWorkExperience
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PersonalWorkExperiences.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemPersonalWorkExperience(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPersonalWorkExperience.Columns[0].Visible = isVisible;
            grdPersonalWorkExperience.Columns[grdPersonalWorkExperience.Columns.Count - 1].Visible = isVisible;

            grdPersonalWorkExperience.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPersonalWorkExperience.Rebind();
        }

        private PersonalWorkExperienceCollection PersonalWorkExperiences
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPersonalWorkExperience" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((PersonalWorkExperienceCollection)(obj));
                    }
                }

                PersonalWorkExperienceCollection coll = new PersonalWorkExperienceCollection();
                AppStandardReferenceItemQuery address = new AppStandardReferenceItemQuery("c");
                PersonalWorkExperienceQuery query = new PersonalWorkExperienceQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");

                query.Select
                    (
                       query.PersonalWorkExperienceID,
                       query.PersonID,
                       query.SRLineBisnis,
                       address.ItemName.As("refToLineBusinessName_PersonalWorkExperiences"),
                       query.StartDate,
                       query.EndDate,
                       query.StartYear,
                       query.EndYear,
                       query.Company,
                       query.Division,
                       query.Location,
                       query.JobDesc,
                       query.SupervisorName,
                       query.LastSalary,
                       query.ReasonOfLeaving,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.LeftJoin(address).On
                        (
                            query.SRLineBisnis == address.ItemID &
                            address.StandardReferenceID == "LineBusiness"
                        );

                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PersonalWorkExperienceID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collPersonalWorkExperience" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collPersonalWorkExperience" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulatePersonalWorkExperienceGrid()
        {
            //Display Data Detail
            PersonalWorkExperiences = null; //Reset Record Detail
            grdPersonalWorkExperience.DataSource = PersonalWorkExperiences; //Requery
            grdPersonalWorkExperience.MasterTableView.IsItemInserted = false;
            grdPersonalWorkExperience.MasterTableView.ClearEditItems();
            grdPersonalWorkExperience.DataBind();
        }

        protected void grdPersonalWorkExperience_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalWorkExperience.DataSource = PersonalWorkExperiences;
        }

        protected void grdPersonalWorkExperience_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 personalWorkExperienceID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PersonalWorkExperienceMetadata.ColumnNames.PersonalWorkExperienceID]);
            PersonalWorkExperience entity = FindPersonalWorkExperience(personalWorkExperienceID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPersonalWorkExperience_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 personalWorkExperienceID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PersonalWorkExperienceMetadata.ColumnNames.PersonalWorkExperienceID]);
            PersonalWorkExperience entity = FindPersonalWorkExperience(personalWorkExperienceID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPersonalWorkExperience_InsertCommand(object source, GridCommandEventArgs e)
        {
            PersonalWorkExperience entity = PersonalWorkExperiences.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPersonalWorkExperience.Rebind();
        }
        private PersonalWorkExperience FindPersonalWorkExperience(Int32 personalWorkExperienceID)
        {
            PersonalWorkExperienceCollection coll = PersonalWorkExperiences;
            PersonalWorkExperience retEntity = null;
            foreach (PersonalWorkExperience rec in coll)
            {
                if (rec.PersonalWorkExperienceID.ToString().Equals(personalWorkExperienceID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PersonalWorkExperience entity, GridCommandEventArgs e)
        {
            PersonalWorkExperienceDetail userControl = (PersonalWorkExperienceDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.PersonalWorkExperienceID = userControl.PersonalWorkExperienceID;
                entity.SRLineBisnis = userControl.SRLineBisnis;
                entity.LineBisnisName = userControl.LineBisnisName;

                if (userControl.StartDate == null)
                    entity.str.StartDate = string.Empty;
                else
                    entity.StartDate = userControl.StartDate;

                if (userControl.EndDate == null)
                    entity.str.EndDate = string.Empty;
                else
                    entity.EndDate = userControl.EndDate;

                entity.StartYear = userControl.StartYear;
                entity.EndYear = userControl.EndYear;
                entity.Company = userControl.Company;
                entity.Division = userControl.Division;
                entity.Location = userControl.Location;
                entity.SupervisorName = userControl.SupervisorName;
                entity.JobDesc = userControl.JobDesc;
                entity.LastSalary = userControl.LastSalary;
                entity.ReasonOfLeaving = userControl.ReasonOfLeaving;

            }
        }

        #endregion

        #region Record Detail Method Function PersonalEducationHistory
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PersonalEducationHistorys.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemPersonalEducationHistory(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPersonalEducationHistory.Columns[0].Visible = isVisible;
            grdPersonalEducationHistory.Columns[grdPersonalEducationHistory.Columns.Count - 1].Visible = isVisible;
            grdPersonalEducationHistory.Columns.FindByUniqueName("DocumentUpload").Visible = !isVisible;

            grdPersonalEducationHistory.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPersonalEducationHistory.Rebind();
        }

        private PersonalEducationHistoryCollection PersonalEducationHistorys
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPersonalEducationHistory" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((PersonalEducationHistoryCollection)(obj));
                    }
                }

                PersonalEducationHistoryCollection coll = new PersonalEducationHistoryCollection();
                AppStandardReferenceItemQuery education = new AppStandardReferenceItemQuery("c");
                PersonalEducationHistoryQuery query = new PersonalEducationHistoryQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");

                query.Select
                    (
                       query.PersonalEducationHistoryID,
                       query.PersonID,
                       query.SREducationLevel,
                       education.ItemName.As("refToEducationLevelName_PersonalEducationHistory"),
                       query.InstitutionName,
                       query.Location,
                       query.StartYear,
                       query.EndYear,
                       query.Gpa,
                       query.Achievement,
                       query.Note,
                       query.Majors,
                       query.GraduateDate,
                       query.DiplomaNo,
                       query.DiplomaVerificationNo,
                       query.EducationalAdjustmentDate,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.InnerJoin(education).On
                        (
                            query.SREducationLevel == education.ItemID &
                            education.StandardReferenceID == AppEnum.StandardReference.EducationLevel
                        );

                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PersonalEducationHistoryID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collPersonalEducationHistory" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collPersonalEducationHistory" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulatePersonalEducationHistoryGrid()
        {
            //Display Data Detail
            PersonalEducationHistorys = null; //Reset Record Detail
            grdPersonalEducationHistory.DataSource = PersonalEducationHistorys; //Requery
            grdPersonalEducationHistory.MasterTableView.IsItemInserted = false;
            grdPersonalEducationHistory.MasterTableView.ClearEditItems();
            grdPersonalEducationHistory.DataBind();
        }

        protected void grdPersonalEducationHistory_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalEducationHistory.DataSource = PersonalEducationHistorys;
        }

        protected void grdPersonalEducationHistory_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 personalEducationHistoryID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PersonalEducationHistoryMetadata.ColumnNames.PersonalEducationHistoryID]);
            PersonalEducationHistory entity = FindPersonalEducationHistory(personalEducationHistoryID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPersonalEducationHistory_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 personalEducationHistoryID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PersonalEducationHistoryMetadata.ColumnNames.PersonalEducationHistoryID]);
            PersonalEducationHistory entity = FindPersonalEducationHistory(personalEducationHistoryID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPersonalEducationHistory_InsertCommand(object source, GridCommandEventArgs e)
        {
            PersonalEducationHistory entity = PersonalEducationHistorys.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPersonalEducationHistory.Rebind();
        }
        private PersonalEducationHistory FindPersonalEducationHistory(Int32 personalEducationHistoryID)
        {
            PersonalEducationHistoryCollection coll = PersonalEducationHistorys;
            PersonalEducationHistory retEntity = null;
            foreach (PersonalEducationHistory rec in coll)
            {
                if (rec.PersonalEducationHistoryID.ToString().Equals(personalEducationHistoryID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PersonalEducationHistory entity, GridCommandEventArgs e)
        {
            PersonalEducationHistoryDetail userControl = (PersonalEducationHistoryDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.PersonalEducationHistoryID = userControl.PersonalEducationHistoryID;
                entity.SREducationLevel = userControl.SREducationLevel;
                entity.EducationLevelName = userControl.EducationLevelName;
                entity.InstitutionName = userControl.InstitutionName;
                entity.Location = userControl.Location;
                entity.StartYear = userControl.StartYear;
                entity.EndYear = userControl.EndYear;
                entity.Gpa = userControl.Gpa;
                entity.Achievement = userControl.Achievement;
                entity.Note = userControl.Note;

                entity.Majors = userControl.Majors;
                if (userControl.GraduateDate == null)
                    entity.str.GraduateDate = string.Empty;
                else
                    entity.GraduateDate = userControl.GraduateDate;
                entity.DiplomaNo = userControl.DiplomaNo;
                entity.DiplomaVerificationNo = userControl.DiplomaVerificationNo;
                if (userControl.EducationalAdjustmentDate == null)
                    entity.str.EducationalAdjustmentDate = string.Empty;
                else
                    entity.EducationalAdjustmentDate = userControl.EducationalAdjustmentDate;
            }
        }

        #endregion

        #region Record Detail Method Function PersonalLicence
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PersonalLicences.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemPersonalLicence(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPersonalLicence.Columns[0].Visible = isVisible;
            grdPersonalLicence.Columns[grdPersonalLicence.Columns.Count - 1].Visible = isVisible;
            grdPersonalLicence.Columns.FindByUniqueName("DocumentUpload").Visible = !isVisible;

            grdPersonalLicence.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPersonalLicence.Rebind();
        }

        private PersonalLicenceCollection PersonalLicences
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPersonalLicence" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((PersonalLicenceCollection)(obj));
                    }
                }

                PersonalLicenceCollection coll = new PersonalLicenceCollection();
                AppStandardReferenceItemQuery licence = new AppStandardReferenceItemQuery("c");
                PersonalLicenceQuery query = new PersonalLicenceQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");

                query.Select
                    (
                       query.PersonalLicenceID,
                       query.PersonID,
                       query.SRLicenceType,
                       licence.ItemName.As("refToLicenceTypeName_PersonalLicence"),
                       query.ValidFrom,
                       query.ValidTo,
                       query.Note,
                       query.VerificationLetterNo,
                       query.VerificationDate,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.LeftJoin(licence).On
                        (
                            query.SRLicenceType == licence.ItemID &
                            licence.StandardReferenceID == "LicenseType"
                        );

                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.ValidFrom.Descending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collPersonalLicence" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collPersonalLicence" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulatePersonalLicenceGrid()
        {
            //Display Data Detail
            PersonalLicences = null; //Reset Record Detail
            grdPersonalLicence.DataSource = PersonalLicences; //Requery
            grdPersonalLicence.MasterTableView.IsItemInserted = false;
            grdPersonalLicence.MasterTableView.ClearEditItems();
            grdPersonalLicence.DataBind();
        }

        protected void grdPersonalLicence_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalLicence.DataSource = PersonalLicences;
        }

        protected void grdPersonalLicence_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 personalLicenceID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PersonalLicenceMetadata.ColumnNames.PersonalLicenceID]);
            PersonalLicence entity = FindPersonalLicence(personalLicenceID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPersonalLicence_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 personalLicenceID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PersonalLicenceMetadata.ColumnNames.PersonalLicenceID]);
            PersonalLicence entity = FindPersonalLicence(personalLicenceID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPersonalLicence_InsertCommand(object source, GridCommandEventArgs e)
        {
            PersonalLicence entity = PersonalLicences.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPersonalLicence.Rebind();
        }
        private PersonalLicence FindPersonalLicence(Int32 personalLicenceID)
        {
            PersonalLicenceCollection coll = PersonalLicences;
            PersonalLicence retEntity = null;
            foreach (PersonalLicence rec in coll)
            {
                if (rec.PersonalLicenceID.ToString().Equals(personalLicenceID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PersonalLicence entity, GridCommandEventArgs e)
        {
            PersonalLicenceDetail userControl = (PersonalLicenceDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.PersonalLicenceID = userControl.PersonalLicenceID;
                entity.SRLicenceType = userControl.SRLicenceType;
                entity.LicenceTypeName = userControl.LicenceTypeName;
                entity.ValidFrom = userControl.ValidFrom;
                entity.ValidTo = userControl.ValidTo;
                entity.Note = userControl.Note;
                entity.VerificationLetterNo = userControl.VerificationLetterNo;
                if (userControl.VerificationDate == null)
                    entity.str.VerificationDate = string.Empty;
                else
                    entity.VerificationDate = userControl.VerificationDate;
            }
        }

        #endregion

        #region Record Detail Method Function PersonalOrganization
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PersonalOrganizations.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemPersonalOrganization(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPersonalOrganization.Columns[0].Visible = isVisible;
            grdPersonalOrganization.Columns[grdPersonalOrganization.Columns.Count - 1].Visible = isVisible;
            grdPersonalOrganization.Columns.FindByUniqueName("DocumentUpload").Visible = !isVisible;

            grdPersonalOrganization.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPersonalOrganization.Rebind();
        }

        private PersonalOrganizationCollection PersonalOrganizations
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPersonalOrganization" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((PersonalOrganizationCollection)(obj));
                    }
                }

                PersonalOrganizationCollection coll = new PersonalOrganizationCollection();
                AppStandardReferenceItemQuery role = new AppStandardReferenceItemQuery("c");
                PersonalOrganizationQuery query = new PersonalOrganizationQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");

                query.Select
                    (
                       query.PersonalOrganizationID,
                       query.PersonID,
                       query.OrganizationName,
                       query.Location,
                       query.SROrganizationType,
                       query.SROrganizationRole,
                       role.ItemName.As("refToHR_OrganizationRoleName"),
                       query.ValidFrom,
                       query.ValidTo,
                       query.Note,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.InnerJoin(role).On
                        (
                            query.SROrganizationRole == role.ItemID &
                            role.StandardReferenceID == "OrganizationRole"
                        );

                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PersonalOrganizationID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collPersonalOrganization" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collPersonalOrganization" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulatePersonalOrganizationGrid()
        {
            //Display Data Detail
            PersonalOrganizations = null; //Reset Record Detail
            grdPersonalOrganization.DataSource = PersonalOrganizations; //Requery
            grdPersonalOrganization.MasterTableView.IsItemInserted = false;
            grdPersonalOrganization.MasterTableView.ClearEditItems();
            grdPersonalOrganization.DataBind();
        }

        protected void grdPersonalOrganization_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalOrganization.DataSource = PersonalOrganizations;
        }

        protected void grdPersonalOrganization_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 personalOrganizationID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PersonalOrganizationMetadata.ColumnNames.PersonalOrganizationID]);
            PersonalOrganization entity = FindPersonalOrganization(personalOrganizationID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPersonalOrganization_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 personalOrganizationID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PersonalOrganizationMetadata.ColumnNames.PersonalOrganizationID]);
            PersonalOrganization entity = FindPersonalOrganization(personalOrganizationID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPersonalOrganization_InsertCommand(object source, GridCommandEventArgs e)
        {
            PersonalOrganization entity = PersonalOrganizations.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPersonalOrganization.Rebind();
        }
        private PersonalOrganization FindPersonalOrganization(Int32 personalOrganizationID)
        {
            PersonalOrganizationCollection coll = PersonalOrganizations;
            PersonalOrganization retEntity = null;
            foreach (PersonalOrganization rec in coll)
            {
                if (rec.PersonalOrganizationID.ToString().Equals(personalOrganizationID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PersonalOrganization entity, GridCommandEventArgs e)
        {
            PersonalOrganizationDetail userControl = (PersonalOrganizationDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.OrganizationName = userControl.OrganizationName;
                entity.Location = userControl.Location;
                entity.SROrganizationType = userControl.SROrganizationType;
                entity.SROrganizationRole = userControl.SROrganizationRole;
                entity.OrganizationRoleName = userControl.OrganizationRoleName;

                if (userControl.ValidFrom == null)
                    entity.str.ValidFrom = string.Empty;
                else
                    entity.ValidFrom = userControl.ValidFrom;

                if (userControl.ValidTo == null)
                    entity.str.ValidTo = string.Empty;
                else
                    entity.ValidTo = userControl.ValidTo;

            }
        }

        #endregion

        #region Record Detail Method Function PersonalPhysical
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PersonalPhysicals.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemPersonalPhysical(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPersonalPhysical.Columns[0].Visible = isVisible;
            grdPersonalPhysical.Columns[grdPersonalPhysical.Columns.Count - 1].Visible = isVisible;

            grdPersonalPhysical.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPersonalPhysical.Rebind();
        }

        private PersonalPhysicalCollection PersonalPhysicals
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPersonalPhysical" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((PersonalPhysicalCollection)(obj));
                    }
                }

                PersonalPhysicalCollection coll = new PersonalPhysicalCollection();
                AppStandardReferenceItemQuery measurement = new AppStandardReferenceItemQuery("d");
                AppStandardReferenceItemQuery characteristic = new AppStandardReferenceItemQuery("c");
                PersonalPhysicalQuery query = new PersonalPhysicalQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");

                query.Select
                    (
                       query.PersonalPhysicalID,
                       query.PersonID,
                       query.SRPhysicalCharacteristic,
                       characteristic.ItemName.As("refToPhysicalCharacteristic_PersonalPhysical"),
                       query.PhysicalValue,
                       query.SRMeasurementCode,
                       measurement.ItemName.As("refToMeasurementCode_PersonalPhysical"),
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.LeftJoin(characteristic).On
                        (
                            query.SRPhysicalCharacteristic == characteristic.ItemID &
                            characteristic.StandardReferenceID == "PhysicalCharacteristic"
                        );
                query.LeftJoin(measurement).On
                        (
                            query.SRMeasurementCode == measurement.ItemID &
                            measurement.StandardReferenceID == "MeasurementCode"
                        );

                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PersonalPhysicalID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collPersonalPhysical" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collPersonalPhysical" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulatePersonalPhysicalGrid()
        {
            //Display Data Detail
            PersonalPhysicals = null; //Reset Record Detail
            grdPersonalPhysical.DataSource = PersonalPhysicals; //Requery
            grdPersonalPhysical.MasterTableView.IsItemInserted = false;
            grdPersonalPhysical.MasterTableView.ClearEditItems();
            grdPersonalPhysical.DataBind();
        }

        protected void grdPersonalPhysical_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalPhysical.DataSource = PersonalPhysicals;
        }

        protected void grdPersonalPhysical_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 personalPhysicalID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PersonalPhysicalMetadata.ColumnNames.PersonalPhysicalID]);
            PersonalPhysical entity = FindPersonalPhysical(personalPhysicalID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPersonalPhysical_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 personalPhysicalID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PersonalPhysicalMetadata.ColumnNames.PersonalPhysicalID]);
            PersonalPhysical entity = FindPersonalPhysical(personalPhysicalID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPersonalPhysical_InsertCommand(object source, GridCommandEventArgs e)
        {
            PersonalPhysical entity = PersonalPhysicals.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPersonalPhysical.Rebind();
        }
        private PersonalPhysical FindPersonalPhysical(Int32 personalPhysicalID)
        {
            PersonalPhysicalCollection coll = PersonalPhysicals;
            PersonalPhysical retEntity = null;
            foreach (PersonalPhysical rec in coll)
            {
                if (rec.PersonalPhysicalID.ToString().Equals(personalPhysicalID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PersonalPhysical entity, GridCommandEventArgs e)
        {
            PersonalPhysicalDetail userControl = (PersonalPhysicalDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.PersonalPhysicalID = userControl.PersonalPhysicalID;
                entity.SRPhysicalCharacteristic = userControl.SRPhysicalCharacteristic;
                entity.PhysicalCharacteristicName = userControl.PhysicalCharacteristicName;
                entity.PhysicalValue = userControl.PhysicalValue;
                entity.SRMeasurementCode = userControl.SRMeasurementCode;
                entity.MeasurementCodeName = userControl.MeasurementCodeName;

            }
        }

        #endregion

        #region Record Detail Method Function RecruitmentTest
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PersonalPhysicals.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemRecruitmentTest(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdRecruitmentTest.Columns[0].Visible = isVisible;
            grdRecruitmentTest.Columns[grdRecruitmentTest.Columns.Count - 1].Visible = isVisible;
            grdRecruitmentTest.Columns.FindByUniqueName("ViewRecruitmentTestUrl").Visible = !isVisible && AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP";
            grdRecruitmentTest.Columns.FindByUniqueName("DocumentUpload").Visible = !isVisible;

            grdRecruitmentTest.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdRecruitmentTest.Rebind();
        }

        private PersonalRecruitmentTestCollection RecruitmentTests
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collRecruitmentTest" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((PersonalRecruitmentTestCollection)(obj));
                    }
                }

                var coll = new PersonalRecruitmentTestCollection();
                var conclusion = new AppStandardReferenceItemQuery("d");
                var characteristic = new AppStandardReferenceItemQuery("c");
                var query = new PersonalRecruitmentTestQuery("b");
                var personal = new PersonalInfoQuery("a");

                query.Select
                    (
                       query,
                       characteristic.ItemName.As("refTo_RecruitmentTestName"),
                       characteristic.ReferenceID.As("refTo_RecruitmentTestType"),
                       conclusion.ItemName.As("refTo_RecruitmentTestConclusionName")
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.LeftJoin(characteristic).On
                        (
                            query.SRRecruitmentTest == characteristic.ItemID &
                            characteristic.StandardReferenceID == "RecruitmentTest"
                        );
                query.LeftJoin(conclusion).On
                        (
                            query.SRRecruitmentTestConclusion == conclusion.ItemID &
                            conclusion.StandardReferenceID == "RecruitmentTestConclusion"
                        );

                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.TestDate.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collRecruitmentTest" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collRecruitmentTests" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulateRecruitmentTestlGrid()
        {
            //Display Data Detail
            RecruitmentTests = null; //Reset Record Detail
            grdRecruitmentTest.DataSource = RecruitmentTests; //Requery
            grdRecruitmentTest.MasterTableView.IsItemInserted = false;
            grdRecruitmentTest.MasterTableView.ClearEditItems();
            grdRecruitmentTest.DataBind();
        }

        protected void grdRecruitmentTest_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRecruitmentTest.DataSource = RecruitmentTests;
        }

        protected void grdRecruitmentTest_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 personalPhysicalID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PersonalRecruitmentTestMetadata.ColumnNames.PersonalRecruitmentTestID]);
            PersonalRecruitmentTest entity = FindRecruitmentTest(personalPhysicalID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdRecruitmentTest_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 personalPhysicalID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PersonalRecruitmentTestMetadata.ColumnNames.PersonalRecruitmentTestID]);
            PersonalRecruitmentTest entity = FindRecruitmentTest(personalPhysicalID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdRecruitmentTest_InsertCommand(object source, GridCommandEventArgs e)
        {
            PersonalRecruitmentTest entity = RecruitmentTests.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdRecruitmentTest.Rebind();
        }
        private PersonalRecruitmentTest FindRecruitmentTest(Int32 personalPhysicalID)
        {
            PersonalRecruitmentTestCollection coll = RecruitmentTests;
            PersonalRecruitmentTest retEntity = null;
            foreach (var rec in coll)
            {
                if (rec.PersonalRecruitmentTestID.ToString().Equals(personalPhysicalID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PersonalRecruitmentTest entity, GridCommandEventArgs e)
        {
            var userControl = (RecruitmentTestDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.PersonalRecruitmentTestID = userControl.PersonalRecruitmentTestID;
                entity.TestDate = userControl.TestDate;
                entity.SRRecruitmentTest = userControl.SRTest;
                entity.RecruitmentTestName = userControl.TestName;
                entity.TestResult = userControl.TestResult;
                entity.Notes = userControl.Notes;
                entity.SRRecruitmentTestConclusion = userControl.SRRecruitmentTestConclusion;
                entity.RecruitmentTestConclusionName = userControl.RecruitmentTestConclusionName;

                var std = new AppStandardReferenceItem();
                if (std.LoadByPrimaryKey(AppEnum.StandardReference.RecruitmentTest.ToString(), entity.SRRecruitmentTest))
                    entity.RecruitmentTestType = std.ReferenceID;
                else
                    entity.RecruitmentTestType = string.Empty;
            }
        }

        #endregion

        #region ComboBox PatientID
        protected void cboPatientID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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

        protected void cboPatientID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulatePatientInformation(e.Value);
        }

        private void PopulatePatientInformation(string patientId)
        {
            if (string.IsNullOrEmpty(patientId))
            {
                txtMedicalNo.Text = string.Empty;
                return;
            }

            var patient = new Patient();
            if (patient.LoadByPrimaryKey(patientId)) txtMedicalNo.Text = patient.MedicalNo;
        }

        #endregion

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (eventArgument.Equals("AfterUpload"))
            {
                OnPopulateEntryControl(new string[] { });
            }
            else if (eventArgument == "AfterUploadRecruit")
            {
                Session["collRecruitmentTest" + Request.UserHostName + hdnPageId.Value] = null;
                grdRecruitmentTest.Rebind();
            }
        }

        protected void txtBirthDate_SelectedDateChanged(object sender, EventArgs e)
        {
            txtAgeInYear.Text = Helper.GetAgeInYear(txtBirthDate.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
            txtAgeInMonth.Text = Helper.GetAgeInMonth(txtBirthDate.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
            txtAgeInDay.Text = Helper.GetAgeInDay(txtBirthDate.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
        }

        private void PopulateEmployeeImage(int personId, string gender)
        {
            // Load from database
            var personalImg = new PersonalImage();
            if (personalImg.LoadByPrimaryKey(personId))
            {
                // Show Image
                if (personalImg.Photo != null)
                {
                    imgPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(personalImg.Photo));
                }
                else
                {
                    imgPhoto.ImageUrl = gender == "M" ? "~/Images/Asset/Patient/ManVector.png" : (gender == "F" ? "~/Images/Asset/Patient/WomanVector.png" : "~/Images/Asset/Patient/HumanVector.png");
                }
            }
            else
                imgPhoto.ImageUrl = gender == "M" ? "~/Images/Asset/Patient/ManVector.png" : (gender == "F" ? "~/Images/Asset/Patient/WomanVector.png" : "~/Images/Asset/Patient/HumanVector.png");
        }
    }
}

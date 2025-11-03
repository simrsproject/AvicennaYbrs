using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Configuration;
using Temiang.Avicenna.Module.RADT;
using DevExpress.XtraRichEdit.Layout.Export;

namespace Temiang.Avicenna.Module.HR.EmployeeHR.Logbook
{
    public partial class LogbookDetail : BasePage
    {
        private string GetPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            switch (GetPageID)
            {
                case "gen":
                    ProgramID = AppConstant.Program.EmployeeLogbook;
                    break;
                case "c01":
                    ProgramID = AppConstant.Program.EmployeeLogbookMedicalCommitte;
                    break;
                case "c02":
                    ProgramID = AppConstant.Program.EmployeeLogbookNursingCommitte;
                    break;
                case "c03":
                    ProgramID = AppConstant.Program.EmployeeLogbookKtkl;
                    break;
            }

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRReligion, AppEnum.StandardReference.Religion);
                StandardReference.InitializeIncludeSpace(cboSREthnic, AppEnum.StandardReference.Ethnic);
                StandardReference.InitializeIncludeSpace(cboSRBloodType, AppEnum.StandardReference.BloodType);
                StandardReference.InitializeIncludeSpace(cboSRMaritalStatus, AppEnum.StandardReference.TaxStatus);

                StandardReference.InitializeIncludeSpace(cboSREmployeeType, AppEnum.StandardReference.EmployeeType);
                StandardReference.InitializeIncludeSpace(cboSREmployeeShiftType, AppEnum.StandardReference.EmployeeShiftType);
                StandardReference.InitializeIncludeSpace(cboSREmployeeScheduleType, AppEnum.StandardReference.EmployeeScheduleType);
                StandardReference.InitializeIncludeSpace(cboSRProfessionType, AppEnum.StandardReference.ProfessionType);

                grdEmployeePosition.Columns.FindByUniqueName("PrintJobDescription").Visible = !string.IsNullOrEmpty(AppSession.Parameter.ProgramIdPrintJobDescription);
                grdCredentialing.Columns.FindByUniqueName("urlCredential").Visible = AppSession.Application.IsModuleCredentialActive;
                grdCredentialing.Columns.FindByUniqueName("urlCredentialv2").Visible = AppSession.Application.IsModuleCredential2Active;
                
                RadTabStrip2.Tabs[1].Visible = AppSession.Application.IsModuleCredentialActive || AppSession.Application.IsModuleCredential2Active;
                RadTabStrip2.Tabs[2].Visible = AppSession.Application.IsMenuClinicalPerformanceAppraisalActive;

                if (AppSession.Parameter.IsUsingPreceptorAsProfessionalIndirectSupervisor)
                    lblPreceptorId.Text = "Professional Indirect Supervisor";

                PopulateEntryControl();
            }
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
        }

        private void PopulateEntryControl()
        {
            var personId = Page.Request.QueryString["id"];
            if (string.IsNullOrEmpty(personId))
                return;

            var pi = new PersonalInfo();
            pi.LoadByPrimaryKey(Convert.ToInt32(personId));

            var emp = new VwEmployeeTable();
            emp.Query.Where(emp.Query.PersonID == Convert.ToInt32(personId));
            emp.Query.Load();

            PopulateEmployeeImage(Convert.ToInt32(personId), pi.SRGenderType);

            txtEmployeeNumber.Text = pi.EmployeeNumber;
            txtEmployeeName.Text = emp.EmployeeName;
            txtPlaceBirth.Text = pi.PlaceBirth;
            txtBirthDate.SelectedDate = pi.BirthDate;
            txtBirthName.Text = pi.BirthName;

            var ageInYear = Helper.GetAgeInYear(pi.BirthDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
            var ageInMonth = Helper.GetAgeInMonth(pi.BirthDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
            var ageInDay = Helper.GetAgeInDay(pi.BirthDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();

            txtAge.Text = ageInYear + "th " + ageInMonth + "bl " + ageInDay + "hr";

            rbtSex.SelectedValue = pi.SRGenderType;

            cboSRReligion.SelectedValue = pi.SRReligion;
            cboSREthnic.SelectedValue = pi.SREthnic;
            cboSRBloodType.SelectedValue = pi.SRBloodType;
            cboSRMaritalStatus.SelectedValue = pi.SRMaritalStatus;


            /*------------------------------*/
            var asri = new AppStandardReferenceItem();
            if (asri.LoadByPrimaryKey(AppEnum.StandardReference.EmployeeStatus.ToString(), emp.SREmployeeStatus))
                txtSREmployeeStatus.Text = asri.ItemName;
            else txtSREmployeeStatus.Text = string.Empty;

            txtJoinDate.SelectedDate = emp.JoinDate;
            string[] empStatusResign = AppSession.Parameter.EmployeeStatueResignReference.Split(',');

            if (empStatusResign.Any(e => e.Contains(emp.SREmployeeStatus)))
            {
                var employeeWorkingInfo = new EmployeeWorkingInfo();
                if (employeeWorkingInfo.LoadByPrimaryKey(Convert.ToInt32(personId)))
                    txtResignDate.SelectedDate = employeeWorkingInfo.ResignDate;
                else txtResignDate.SelectedDate = null;
            }
            else
                txtResignDate.SelectedDate = emp.ResignDate;

            DataTable Employee = (new EmployeeWorkingInfoCollection()).EmployeeWorkingInfoView(Convert.ToInt32(personId));
            string organizationID = "-1";
            try { organizationID = Employee.Rows[0]["OrganizationUnitID"].ToString(); }
            catch { }
            string subDivisionId = "-1";
            try { subDivisionId = Employee.Rows[0]["SubDivisonID"].ToString(); }
            catch { }
            string serviceUnitId = "-1";
            try { serviceUnitId = Employee.Rows[0]["ServiceUnitID"].ToString(); }
            catch { }
            string positionID = "-1";
            try { positionID = Employee.Rows[0]["PositionID"].ToString(); }
            catch { }

            var organizationUnit = new OrganizationUnit();
            if (organizationUnit.LoadByPrimaryKey(Convert.ToInt32(organizationID)))
            {
                var ouname = organizationUnit.OrganizationUnitName;

                if (subDivisionId != "-1")
                {
                    organizationUnit = new OrganizationUnit();
                    if (organizationUnit.LoadByPrimaryKey(Convert.ToInt32(subDivisionId)))
                    {
                        ouname += " - " + organizationUnit.OrganizationUnitName;
                    }
                }

                if (!string.IsNullOrEmpty(serviceUnitId))
                {
                    organizationUnit = new OrganizationUnit();
                    if (organizationUnit.LoadByPrimaryKey(Convert.ToInt32(serviceUnitId)))
                    {
                        ouname += " - " + organizationUnit.OrganizationUnitName;
                    }
                }

                txtOrganizationName.Text = ouname;
            }

            var position = new Position();
            position.LoadByPrimaryKey(Convert.ToInt32(positionID));
            txtPositionTitle.Text = position.PositionName;

            txtServiceYear.Value = Convert.ToDouble(emp.ServiceYear);
            txtServiceYearText.Text = emp.ServiceYearText;
            txtServiceYearPermanent.Value = Convert.ToDouble(emp.ServiceYearPermanent);
            txtServiceYearPermanentText.Text = emp.ServiceYearPermanentText;

            asri = new AppStandardReferenceItem();
            if (asri.LoadByPrimaryKey(AppEnum.StandardReference.EmploymentType.ToString(), emp.SREmploymentType))
                txtSREmploymentType.Text = asri.ItemName;
            else txtSREmploymentType.Text = string.Empty;

            if (emp.PositionGradeID != null && emp.PositionGradeID != -1)
            {
                var pg = new PositionGrade();
                if (pg.LoadByPrimaryKey(Convert.ToInt32(emp.PositionGradeID)))
                    txtPositionGradeID.Text = pg.PositionGradeName;
                else txtPositionGradeID.Text = string.Empty;
            }
            else txtPositionGradeID.Text = string.Empty;

            if (emp.GradeYear != null)
                txtGradeYear.Value = Convert.ToDouble(emp.GradeYear);

            cboSREmployeeType.SelectedValue = emp.SREmployeeType;
            cboSREmployeeShiftType.SelectedValue = emp.SREmployeeShiftType;
            cboSREmployeeScheduleType.SelectedValue = emp.SREmployeeScheduleType;
            cboSRProfessionType.SelectedValue = emp.SRProfessionType;
            txtAbsenceCardNo.Text = emp.AbsenceCardNo;
            
            asri = new AppStandardReferenceItem();
            if (asri.LoadByPrimaryKey(AppEnum.StandardReference.EducationLevel.ToString(), emp.SREducationLevel))
                txtSREducationLevel.Text = asri.ItemName;
            else txtSREducationLevel.Text = string.Empty;

            /*-----------*/
            var plQuery = new VwEmployeeTableQuery();
            plQuery.Where(plQuery.PersonID == Convert.ToInt32(emp.ManagerID));
            var dtb = plQuery.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                cboManagerID.DataSource = dtb;
                cboManagerID.DataBind();
                cboManagerID.SelectedValue = emp.ManagerID.ToString();
            }
            else
            {
                cboManagerID.DataSource = null;
                cboManagerID.DataBind();
                cboManagerID.Items.Clear();
                cboManagerID.SelectedValue = string.Empty;
                cboManagerID.Text = string.Empty;
            }

            plQuery = new VwEmployeeTableQuery();
            plQuery.Where(plQuery.PersonID == Convert.ToInt32(emp.SupervisorId));
            var dtb1 = plQuery.LoadDataTable();
            if (dtb1.Rows.Count > 0)
            {
                cboSupervisorID.DataSource = dtb1;
                cboSupervisorID.DataBind();
                cboSupervisorID.SelectedValue = emp.SupervisorId.ToString();
            }
            else
            {
                cboSupervisorID.DataSource = null;
                cboSupervisorID.DataBind();
                cboSupervisorID.Items.Clear();
                cboSupervisorID.SelectedValue = string.Empty;
                cboSupervisorID.Text = string.Empty;
            }

            plQuery = new VwEmployeeTableQuery();
            plQuery.Where(plQuery.PersonID == Convert.ToInt32(emp.PreceptorId));
            var dtb2 = plQuery.LoadDataTable();
            if (dtb2.Rows.Count > 0)
            {
                cboPreceptorId.DataSource = dtb2;
                cboPreceptorId.DataBind();
                cboPreceptorId.SelectedValue = emp.PreceptorId.ToString();
            }
            else
            {
                cboPreceptorId.DataSource = null;
                cboPreceptorId.DataBind();
                cboPreceptorId.Items.Clear();
                cboPreceptorId.SelectedValue = string.Empty;
                cboPreceptorId.Text = string.Empty;
            }

            asri = new AppStandardReferenceItem();
            if (asri.LoadByPrimaryKey(AppEnum.StandardReference.ProfessionType.ToString(), emp.SRProfessionType) & !string.IsNullOrEmpty(asri.ReferenceID))
            {
                txtSRProfessionGroup.Text = asri.ReferenceID;
                asri = new AppStandardReferenceItem();
                if (asri.LoadByPrimaryKey(AppEnum.StandardReference.ProfessionGroup.ToString(), txtSRProfessionGroup.Text))
                    txtProfessionGroupName.Text = asri.ItemName;
                else
                    txtProfessionGroupName.Text = string.Empty;
            }
            else
            {
                txtProfessionGroupName.Text = string.Empty;
                txtProfessionGroupName.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(emp.SRClinicalWorkArea))
            {
                var cwaQuery = new AppStandardReferenceItemQuery();
                cwaQuery.Where(cwaQuery.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea.ToString(), cwaQuery.ItemID == emp.SRClinicalWorkArea);
                var cwaDtb = cwaQuery.LoadDataTable();
                if (cwaDtb.Rows.Count > 0)
                {
                    cboSRClinicalWorkArea.DataSource = cwaDtb;
                    cboSRClinicalWorkArea.DataBind();
                    cboSRClinicalWorkArea.SelectedValue = emp.SRClinicalWorkArea;
                }
                else
                {
                    cboSRClinicalWorkArea.Items.Clear();
                    cboSRClinicalWorkArea.SelectedValue = string.Empty;
                    cboSRClinicalWorkArea.Text = string.Empty;
                }
            }
            else
            {
                cboSRClinicalWorkArea.Items.Clear();
                cboSRClinicalWorkArea.SelectedValue = string.Empty;
                cboSRClinicalWorkArea.Text = string.Empty;
            }

            cboSRClinicalAuthorityLevel.SelectedValue = emp.SRClinicalAuthorityLevel;

            PopulateEmployeeOrientationGrid();
            RefreshCommandItemEmployeeOrientation();
            PopulateEmployeeAppraisalQuestionGrid();
            RefreshCommandItemEmployeeAppraisalQuestion();

            grdEmployeeForm.MasterTableView.CommandItemDisplay = (AppSession.UserLogin.PersonID == personId.ToInt()) ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            if (personId != AppSession.UserLogin.PersonID.ToString())
            {
                RadTabStrip1.Visible = false;
                mpgDetail1.Visible = false;
            }
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
                    imgPhoto.ImageUrl = gender == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                }
            }
            else
                imgPhoto.ImageUrl = gender == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
        }

        #region Header
        protected void grdLicenseRecap_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdLicenseRecap.DataSource = PersonalLicenseRecaps;
        }
        private DataTable PersonalLicenseRecaps
        {
            get
            {
                var coll = new PersonalLicenceCollection();
                DataTable tbl = coll.GetPersonalLicenseRecap(Convert.ToInt32(Page.Request.QueryString["id"]));

                return tbl;
            }
        }

        protected void grdLicenseRecap_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    var dataItem = e.Item as GridDataItem;
                    //if (dataItem["Remaining"].Text.ToInt() < AppSession.Parameter.DayLimitEmployeeLicenseWarning)
                    if (dataItem["Remaining"].Text.ToInt() < dataItem["DayLimit"].Text.ToInt())
                    {
                        // Beri warna merah jika sudah mendekati due
                        dataItem.ForeColor = Color.Red;
                        dataItem.Font.Bold = true;
                    }
                }
            }
            catch
            { }
        }
        #endregion

        #region Personal Identity
        protected void grdPersonalAddress_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalAddress.DataSource = PersonalAddresss;
        }
        private DataTable PersonalAddresss
        {
            get
            {
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
                       address.ItemName.As("AddressTypeName"),
                       query.Address,
                       query.ZipCode,
                       zc.ZipPostalCode.As("ZipPostalCode"),
                       query.District,
                       query.County,
                       query.City,
                       query.SRState,
                       state.ItemName.As("StateName"),
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
                query.Where(query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"])); //TODO: Betulkan parameternya
                query.OrderBy(query.PersonalAddressID.Ascending); //TODO: Betulkan ordernya
                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        protected void grdPersonalContact_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalContact.DataSource = PersonalContacts;
        }
        private DataTable PersonalContacts
        {
            get
            {
                AppStandardReferenceItemQuery contact = new AppStandardReferenceItemQuery("b");
                PersonalContactQuery query = new PersonalContactQuery("a");

                query.Select
                    (
                       query.PersonalContactID,
                       query.PersonID,
                       query.SRContactType,
                       contact.ItemName.As("ContactTypeName"),
                       query.ContactValue,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(contact).On
                        (
                            query.SRContactType == contact.ItemID &
                            contact.StandardReferenceID == "ContactType"
                        );

                query.Where(query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"])); //TODO: Betulkan parameternya
                query.OrderBy(query.PersonalContactID.Ascending); //TODO: Betulkan ordernya


                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        protected void grdPersonalIdentification_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalIdentification.DataSource = PersonalIdentifications;
        }
        private DataTable PersonalIdentifications
        {
            get
            {
                AppStandardReferenceItemQuery identification = new AppStandardReferenceItemQuery("c");
                PersonalIdentificationQuery query = new PersonalIdentificationQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");

                query.Select
                    (
                       query.PersonalIdentificationID,
                       query.PersonID,
                       query.SRIdentificationType,
                       identification.ItemName.As("IdentificationTypeName"),
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

                query.Where(query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"]));
                query.OrderBy(query.PersonalIdentificationID.Ascending); //TODO: Betulkan ordernya
                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        protected void grdPersonalFamily_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalFamily.DataSource = PersonalFamilys;
        }

        private DataTable PersonalFamilys
        {
            get
            {
                PatientQuery patient = new PatientQuery("f");
                AppStandardReferenceItemQuery gender = new AppStandardReferenceItemQuery("e");
                AppStandardReferenceItemQuery marital = new AppStandardReferenceItemQuery("d");
                AppStandardReferenceItemQuery relation = new AppStandardReferenceItemQuery("c");
                PersonalFamilyQuery query = new PersonalFamilyQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");
                AppStandardReferenceItemQuery coverage = new AppStandardReferenceItemQuery("g");

                query.Select
                    (
                       query.PersonalFamilyID,
                       query.PersonID,
                       query.PatientID,
                       patient.MedicalNo.As("MedicalNo"),
                       query.SRFamilyRelation,
                       relation.ItemName.As("FamilyRelationName"),
                       query.FamilyName,
                       query.CityOfBirth,
                       query.DateBirth,
                       query.Address,
                       query.ZipCode,
                       query.Phone,
                       query.SRMaritalStatus,
                       marital.ItemName.As("MaritalStatusName"),
                       query.SRGenderType,
                       gender.ItemName.As("GenderTypeName"),
                       query.SREducationLevel,
                       query.SRState,
                       query.SRCity,
                       query.IsGuaranteed,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime,
                       query.SRCoverageType,
                       coverage.ItemName.As("refTo_CoverageTypeName"),
                       query.BPJSKesehatanNo
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID & query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"]));
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

                query.OrderBy(query.PersonalFamilyID.Ascending); 
                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        protected void grdPersonalEmergencyContact_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalEmergencyContact.DataSource = PersonalEmergencyContacts;
        }
        private DataTable PersonalEmergencyContacts
        {
            get
            {
                AppStandardReferenceItemQuery city = new AppStandardReferenceItemQuery("e");
                AppStandardReferenceItemQuery state = new AppStandardReferenceItemQuery("d");
                PersonalEmergencyContactQuery query = new PersonalEmergencyContactQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");
                var zc = new ZipCodeQuery("f");

                query.Select
                    (
                       query.PersonalEmergencyContactID,
                       query.PersonID,
                       query.ContactName,
                       query.Address,
                       query.SRState,
                       state.ItemName.As("StateName"),
                       query.SRCity,
                       query.ZipCode,
                       zc.ZipPostalCode.As("ZipPostalCode"),
                       query.Phone,
                       query.Mobile,
                       query.District,
                       query.County,
                       query.City,
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
                query.Where(query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"])); 
                query.OrderBy(query.PersonalEmergencyContactID.Ascending); 
                
                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        protected void grdPersonalEducationHistory_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalEducationHistory.DataSource = PersonalEducationHistorys;
        }

        private DataTable PersonalEducationHistorys
        {
            get
            {
                AppStandardReferenceItemQuery education = new AppStandardReferenceItemQuery("c");
                PersonalEducationHistoryQuery query = new PersonalEducationHistoryQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");

                query.Select
                    (
                       query.PersonalEducationHistoryID,
                       query.PersonID,
                       query.SREducationLevel,
                       education.ItemName.As("EducationLevelName"),
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

                query.Where(query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"])); //TODO: Betulkan parameternya
                query.OrderBy(query.PersonalEducationHistoryID.Ascending); //TODO: Betulkan ordernya
                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        protected void grdPersonalOrganization_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalOrganization.DataSource = PersonalOrganizations;
        }

        private DataTable PersonalOrganizations
        {
            get
            {
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
                       role.ItemName.As("OrganizationRoleName"),
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

                query.Where(query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"]));
                query.OrderBy(query.PersonalOrganizationID.Ascending);
                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        protected void grdPersonalPhysical_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalPhysical.DataSource = PersonalPhysicals;
        }

        private DataTable PersonalPhysicals
        {
            get
            {
                AppStandardReferenceItemQuery measurement = new AppStandardReferenceItemQuery("d");
                AppStandardReferenceItemQuery characteristic = new AppStandardReferenceItemQuery("c");
                PersonalPhysicalQuery query = new PersonalPhysicalQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");

                query.Select
                    (
                       query.PersonalPhysicalID,
                       query.PersonID,
                       query.SRPhysicalCharacteristic,
                       characteristic.ItemName.As("PhysicalCharacteristicName"),
                       query.PhysicalValue,
                       query.SRMeasurementCode,
                       measurement.ItemName.As("MeasurementCodeName"),
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

                query.Where(query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"])); 
                query.OrderBy(query.PersonalPhysicalID.Ascending);
                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }
        #endregion

        #region Professional Identity
        protected void cboManagerID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );
            query.Where
                (
                    query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );

            cboManagerID.DataSource = query.LoadDataTable();
            cboManagerID.DataBind();
        }

        protected void cboManagerID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboSupervisorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );
            query.Where
                (
                    query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );

            cboSupervisorID.DataSource = query.LoadDataTable();
            cboSupervisorID.DataBind();
        }

        protected void cboSupervisorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboPreceptorId_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );
            query.Where
                (
                    query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );

            cboPreceptorId.DataSource = query.LoadDataTable();
            cboPreceptorId.DataBind();
        }

        protected void cboPreceptorId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboSRClinicalWorkArea_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Where
                (
                    query.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea,
                    query.ItemName.Like(searchTextContain),
                    query.IsActive == true
                );
            if (!string.IsNullOrEmpty(txtSRProfessionGroup.Text))
                query.Where(query.ReferenceID == txtSRProfessionGroup.Text);
            query.OrderBy(query.ItemID.Ascending);

            cboSRClinicalWorkArea.DataSource = query.LoadDataTable();
            cboSRClinicalWorkArea.DataBind();
        }

        protected void cboSRClinicalWorkArea_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRClinicalAuthorityLevel_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Where
                (
                    query.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel,
                    query.ItemName.Like(searchTextContain),
                    query.IsActive == true
                );
            if (!string.IsNullOrEmpty(cboSRClinicalWorkArea.SelectedValue))
                query.Where(query.ReferenceID == cboSRClinicalWorkArea.SelectedValue);
            query.OrderBy(query.ItemID.Ascending);

            cboSRClinicalAuthorityLevel.DataSource = query.LoadDataTable();
            cboSRClinicalAuthorityLevel.DataBind();
        }

        protected void cboSRClinicalAuthorityLevel_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void grdPersonalLicense_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalLicense.DataSource = PersonalLicenses;
        }
        private DataTable PersonalLicenses
        {
            get
            {
                AppStandardReferenceItemQuery licence = new AppStandardReferenceItemQuery("c");
                PersonalLicenceQuery query = new PersonalLicenceQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");

                query.Select
                    (
                       query.PersonalLicenceID,
                       query.PersonID,
                       query.SRLicenceType,
                       licence.ItemName.As("LicenceTypeName"),
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

                query.Where(query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"])); 
                query.OrderBy(query.PersonalLicenceID.Ascending); 
                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        protected void grdCredentialing_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdCredentialing.DataSource = Credentialings;
        }
        private DataTable Credentialings
        {
            get
            {
                var query = new CredentialProcessQuery("a");
                var personal = new PersonalInfoQuery("b");
                var profession = new AppStandardReferenceItemQuery("c");
                var area = new AppStandardReferenceItemQuery("d");
                var level = new AppStandardReferenceItemQuery("e");
                
                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.InnerJoin(profession).On(profession.StandardReferenceID == AppEnum.StandardReference.ProfessionGroup.ToString() & profession.ItemID == query.SRProfessionGroup);
                query.InnerJoin(area).On(area.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea.ToString() & area.ItemID == query.SRClinicalWorkArea);
                query.InnerJoin(level).On(level.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel.ToString() & level.ItemID == query.SRClinicalAuthorityLevel);
                
                query.Select(
                    query.TransactionNo,
                    query.TransactionDate,
                    query.PersonID,
                    personal.EmployeeNumber,
                    personal.EmployeeName,
                    query.SRProfessionGroup,
                    profession.ItemName.As("ProfessionGroupName"),
                    area.ItemName.As("ClinicalWorkAreaName"),
                    level.ItemName.As("ClinicalAuthorityLevelName"),
                    query.CredentialingDate,
                    query.IsPerform,
                    query.ValidFrom,
                    query.ValidTo,
                    query.DecreeNo
                    );

                query.Where(query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"]), query.IsCredentialing == true);
                query.OrderBy(query.TransactionNo.Descending);
                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        protected void grdClinicalPerformance_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdClinicalPerformance.DataSource = ClinicalPerformances;
        }
        private DataTable ClinicalPerformances
        {
            get
            {
                var query = new ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery("a");
                var personal = new PersonalInfoQuery("b");
                var profession = new AppStandardReferenceItemQuery("c");
                var area = new AppStandardReferenceItemQuery("d");
                var level = new AppStandardReferenceItemQuery("e");

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.InnerJoin(profession).On(profession.StandardReferenceID == AppEnum.StandardReference.ProfessionGroup.ToString() & profession.ItemID == query.SRProfessionGroup);
                query.InnerJoin(area).On(area.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea.ToString() & area.ItemID == query.SRClinicalWorkArea);
                query.InnerJoin(level).On(level.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel.ToString() & level.ItemID == query.SRClinicalAuthorityLevel);

                query.Select(
                    query.ScoresheetNo,
                    query.ScoringDate,
                    query.PersonID,
                    personal.EmployeeNumber,
                    personal.EmployeeName,
                    profession.ItemName.As("ProfessionGroupName"),
                    area.ItemName.As("ClinicalWorkAreaName"),
                    level.ItemName.As("ClinicalAuthorityLevelName"),
                    query.TotalScore,
                    query.ConclusionGrade,
                    query.ConclusionGradeName,
                    query.ConclusionNotes
                    );

                query.Where(query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"]), query.IsApproved == true);
                query.OrderBy(query.ScoresheetNo.Descending);
                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        #endregion

        #region Working Information
        protected void grdEmployeeEmploymentPeriod_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeEmploymentPeriod.DataSource = EmployeeEmploymentPeriods;
        }
        private DataTable EmployeeEmploymentPeriods
        {
            get
            {
                var employment = new AppStandardReferenceItemQuery("c");
                var query = new EmployeeEmploymentPeriodQuery("b");
                var personal = new PersonalInfoQuery("a");
                var rec = new RecruitmentPlanQuery("e");
                var category = new AppStandardReferenceItemQuery("f");

                query.Select
                    (
                       query.EmployeeEmploymentPeriodID,
                       query.PersonID,
                       query.SREmploymentType,
                       employment.ItemName.As("EmploymentTypeName"),
                       query.SREmploymentCategory,
                       category.ItemName.As("EmploymentCategoryName"),
                       query.ValidFrom,
                       query.ValidTo,
                       query.Note,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime,
                       query.EmployeeEmploymentPeriodID,
                       query.RecruitmentPlanID,
                       rec.RecruitmentPlanName.As("RecruitmentPlanName")
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.LeftJoin(employment).On
                        (
                            query.SREmploymentType == employment.ItemID &
                            employment.StandardReferenceID == "EmploymentType"
                        );
                query.LeftJoin(category).On
                       (
                           query.SREmploymentCategory == category.ItemID &
                           category.StandardReferenceID == "EmploymentCategory"
                       );
                query.LeftJoin(rec).On(query.RecruitmentPlanID == rec.RecruitmentPlanID);
                query.Where(query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"])); 
                query.OrderBy(query.ValidFrom.Descending, query.ValidTo.Descending); 
                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        protected void grdEmployeeOrganization_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeOrganization.DataSource = EmployeeOrganizations;
        }
        private DataTable EmployeeOrganizations
        {
            get
            {
                OrganizationUnitQuery subOrganization = new OrganizationUnitQuery("d");
                OrganizationUnitQuery organization = new OrganizationUnitQuery("c");
                EmployeeOrganizationQuery query = new EmployeeOrganizationQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");
                OrganizationUnitQuery subDivision = new OrganizationUnitQuery("e");
                var asri = new AppStandardReferenceItemQuery("f");

                query.Select
                    (
                       query.EmployeeOrganizationID,
                       query.PersonID,
                       query.OrganizationID,
                       organization.OrganizationUnitName.As("OrganizationUnitName"),
                       query.SubOrganizationID,
                       subOrganization.OrganizationUnitName.As("SubOrganizationUnitName"),
                       query.ValidFrom,
                       query.ValidTo,
                       query.IsActive,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime,
                       query.ServiceUnitID,
                       query.SubDivisonID,
                       subDivision.OrganizationUnitName.As("SubDivisonName"),
                       asri.ItemName.As("OrganizationLevelTypeName")
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.InnerJoin(organization).On(query.OrganizationID == organization.OrganizationUnitID);
                query.LeftJoin(subOrganization).On(query.SubOrganizationID == subOrganization.OrganizationUnitID);
                query.LeftJoin(subDivision).On(query.SubDivisonID == subDivision.OrganizationUnitID);
                query.LeftJoin(asri).On(query.SROrganizationLevelType == asri.ItemID && asri.StandardReferenceID == AppEnum.StandardReference.OrganizationLevelType.ToString());

                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSA")
                {
                    var unit = new ServiceUnitQuery("x");
                    query.Select(unit.ServiceUnitName.As("ServiceUnitName"));
                    query.LeftJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                }
                else
                {
                    var org = new OrganizationUnitQuery("z");
                    query.Select(org.OrganizationUnitName.As("ServiceUnitName"));
                    query.LeftJoin(org).On(query.ServiceUnitID == org.OrganizationUnitID);
                }

                query.Where(query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"])); //TODO: Betulkan parameternya
                query.OrderBy(query.SROrganizationLevelType.Ascending, query.ValidFrom.Descending, query.ValidTo.Descending); //TODO: Betulkan ordernya
                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        protected void grdEmployeePosition_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeePosition.DataSource = EmployeePositions;
        }
        private DataTable EmployeePositions
        {
            get
            {
                PositionQuery position = new PositionQuery("c");
                EmployeePositionQuery query = new EmployeePositionQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");
                CoorporateGradeQuery cg = new CoorporateGradeQuery("cg");

                query.Select
                    (
                       query,
                       position.PositionName.As("PositionName")
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.InnerJoin(position).On(query.PositionID == position.PositionID);

                query.LeftJoin(cg).On(query.CoorporateGradeID == cg.CoorporateGradeID);

                query.Where(query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"])); 
                query.OrderBy(query.ValidFrom.Descending, query.ValidTo.Descending); 

                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        protected void grdEmployeePosition_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "PrintJobDescription")
            {
                var jobParameters = new PrintJobParameterCollection();

                var parPaymentNo = jobParameters.AddNew();
                parPaymentNo.Name = "p_EmployeePositionID";
                parPaymentNo.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppSession.Parameter.ProgramIdPrintJobDescription;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
        }

        #endregion

        #region Development
        protected void grdEmployeeTrainingHistory_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeTrainingHistory.DataSource = EmployeeTrainingHistorys;
        }
        private DataTable EmployeeTrainingHistorys
        {
            get
            {
                EmployeeTrainingHistoryQuery query = new EmployeeTrainingHistoryQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");
                var train = new EmployeeTrainingQuery("c");
                var ab = new AppStandardReferenceItemQuery("ab");
                var ac = new AppStandardReferenceItemQuery("ac");
                var ae = new AppStandardReferenceItemQuery("ae");

                query.Select
                    (
                       query.EmployeeTrainingHistoryID,
                       query.PersonID,
                       query.EventName,
                       query.TrainingLocation.Coalesce("c.TrainingLocation"),
                       query.TrainingInstitution.Coalesce("c.TrainingOrganizer"),
                       query.StartDate.Coalesce("c.StartDate"),
                       query.EndDate.Coalesce("c.EndDate"),
                       query.SREmployeeTrainingDateSeparator,
                       query.TotalHour.Coalesce("c.TotalHour"),
                       query.CreditPoint.Coalesce("c.CreditPoint"),
                       query.Fee.Coalesce("c.TrainingFee"),
                       query.PlanningCosts.Coalesce("0"),
                       query.SponsorFee,
                       query.IsInHouseTraining.Coalesce("c.IsInHouseTraining"),
                       query.IsScheduledTraining.Coalesce("c.IsScheduledTraining"),
                       query.IsAttending,
                       query.Note,

                       query.SRActivityType,
                       ab.ItemName.As("ActivityTypeName"),
                       query.SRActivitySubType,
                       ae.ItemName.As("ActivitySubTypeName"),
                       query.CertificateValidityPeriod,
                       query.IsCommitmentToWork,
                       query.LengthOfService,
                       query.StartServiceDate,
                       query.EndServiceDate,
                       query.SRTrainingFinancingSources,
                       ac.ItemName.As("refToStdRef_TrainingFinancingSourcesName"),
                       query.EvaluationDate,
                       query.EvaluationNote,
                       query.EvaluationScore,
                       query.SupervisorEvaluationNote,
                       query.Recommendation,

                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.LeftJoin(train).On(query.EmployeeTrainingID == train.EmployeeTrainingID);
                query.LeftJoin(ab).On(ab.StandardReferenceID == "ActivityType" && ab.ItemID == query.SRActivityType);
                query.LeftJoin(ac).On(ac.StandardReferenceID == "TrainingFinancingSources" && ac.ItemID == query.SRTrainingFinancingSources);
                query.LeftJoin(ae).On(ae.StandardReferenceID == "ActivitySubType" && ae.ItemID == query.SRActivitySubType);

                query.Where(query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"]), query.Or(train.IsProposal.IsNull(), train.IsProposal == false));

                if (!txtTrainingPeriodStart.IsEmpty && !txtTrainingPeriodEnd.IsEmpty)
                    query.Where(query.StartDate >= txtTrainingPeriodStart.SelectedDate, query.StartDate <= txtTrainingPeriodEnd.SelectedDate);

                query.OrderBy(query.EmployeeTrainingHistoryID.Ascending);
                DataTable tbl = query.LoadDataTable();

                decimal totCreditPoint = 0;
                foreach (DataRow row in tbl.Rows)
                {
                    totCreditPoint += Convert.ToDecimal(row["CreditPoint"]);
                }
                txtTotalCreditPoint.Value = Convert.ToDouble(totCreditPoint);

                return tbl;
            }
        }

        protected void btnFilterEmployeeTraining_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdEmployeeTrainingHistory.CurrentPageIndex = 0;
            grdEmployeeTrainingHistory.Rebind();
        }

        private EmployeeOrientationCollection EmployeeOrientations
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeOrientation" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((EmployeeOrientationCollection)(obj));
                    }
                }

                EmployeeOrientationCollection coll = new EmployeeOrientationCollection();

                var query = new EmployeeOrientationQuery("b");
                var personal = new PersonalInfoQuery("a");

                query.Select
                    (
                       query.EmployeeOrientationID,
                       query.PersonID,
                       query.IsGeneral,
                       @"<CASE WHEN b.IsGeneral = 1 THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'refTo_IsParticularOrientation'>",
                       query.StartDate,
                       query.EndDate,
                       query.DurationHour,
                       query.DurationMinutes,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);

                query.Where(query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"]));
                query.OrderBy(query.EmployeeOrientationID.Ascending);
                coll.Load(query);

                Session["collEmployeeOrientation" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collEmployeeOrientation" + Request.UserHostName] = value; }
        }

        private void PopulateEmployeeOrientationGrid()
        {
            //Display Data Detail
            EmployeeOrientations = null; //Reset Record Detail
            grdEmployeeOrientation.DataSource = EmployeeOrientations; //Requery
            grdEmployeeOrientation.MasterTableView.IsItemInserted = false;
            grdEmployeeOrientation.MasterTableView.ClearEditItems();
            grdEmployeeOrientation.DataBind();
        }

        private void RefreshCommandItemEmployeeOrientation()
        {
            //Toogle grid command
            bool isVisible;
            if (string.IsNullOrEmpty(cboSupervisorID.SelectedValue))
                isVisible = false;
            else 
                isVisible = AppSession.UserLogin.PersonID == cboSupervisorID.SelectedValue.ToInt() || AppSession.UserLogin.PersonID == cboManagerID.SelectedValue.ToInt();

            grdEmployeeOrientation.Columns[0].Visible = isVisible;
            grdEmployeeOrientation.Columns[grdEmployeeOrientation.Columns.Count - 1].Visible = isVisible;

            grdEmployeeOrientation.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdEmployeeOrientation.Rebind();
        }

        protected void grdEmployeeOrientation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeOrientation.DataSource = EmployeeOrientations;

            switch (rblFilterOrientationType.SelectedIndex)
            {
                case 0:
                    grdEmployeeOrientation.DataSource = EmployeeOrientations;
                    break;

                case 1:
                    var ds = from d in EmployeeOrientations
                             where d.IsGeneral.Equals(true)
                             select d;
                    grdEmployeeOrientation.DataSource = ds;

                    break;
                case 2:
                    var ds2 = from d in EmployeeOrientations
                              where d.IsGeneral.Equals(false)
                              select d;
                    grdEmployeeOrientation.DataSource = ds2;

                    break;
            }
        }

        protected void grdEmployeeOrientation_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeOrientationMetadata.ColumnNames.EmployeeOrientationID]);
            EmployeeOrientation entity = FindEmployeeOrientation(id);
            if (entity != null && entity.IsGeneral == false)
            {
                SetEntityValue(entity, e);
                EmployeeOrientations.Save();
            }
        }

        protected void grdEmployeeOrientation_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeOrientationMetadata.ColumnNames.EmployeeOrientationID]);
            EmployeeOrientation entity = FindEmployeeOrientation(id);
            if (entity != null && entity.IsGeneral == false)
            {
                entity.MarkAsDeleted();
                EmployeeOrientations.Save();
            }
        }

        protected void grdEmployeeOrientation_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeOrientation entity = EmployeeOrientations.AddNew();
            SetEntityValue(entity, e);
            EmployeeOrientations.Save();

            //Stay in insert mode
            e.Canceled = true;
            grdEmployeeOrientation.Rebind();
        }

        private EmployeeOrientation FindEmployeeOrientation(Int32 id)
        {
            EmployeeOrientationCollection coll = EmployeeOrientations;
            EmployeeOrientation retEntity = null;
            foreach (EmployeeOrientation rec in coll)
            {
                if (rec.EmployeeOrientationID.ToString().Equals(id.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeeOrientation entity, GridCommandEventArgs e)
        {
            EmployeeOrientationDetail userControl = (EmployeeOrientationDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.EmployeeOrientationID = userControl.EmployeeOrientationID;
                entity.PersonID = Convert.ToInt32(Page.Request.QueryString["id"]);
                entity.IsGeneral = userControl.IsGeneral;
                entity.IsParticularOrientation = !userControl.IsGeneral;
                entity.StartDate = userControl.StartDate;
                entity.EndDate = userControl.EndDate;
                entity.DurationHour = userControl.DurationHour;
                entity.DurationMinutes = userControl.DurationMinutes;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        protected void btnFilterOrientationType_Click(object sender, ImageClickEventArgs e)
        {
            grdEmployeeOrientation.CurrentPageIndex = 0;
            grdEmployeeOrientation.Rebind();
        }
        #endregion

        #region Performance Assessment
        protected void grdPerformanceAppraisal_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPerformanceAppraisal.DataSource = EmployeePerformanceAppraisals;
        }
        private DataTable EmployeePerformanceAppraisals
        {
            get
            {
                var query = new EmployeePerformanceAppraisalQuery("a");
                var quarter = new AppStandardReferenceItemQuery("b");

                query.Select
                    (
                       query,
                       quarter.ItemName.As("QuarterPeriod")
                    );

                query.LeftJoin(quarter).On(quarter.StandardReferenceID == "QuarterPeriod" && quarter.ItemID == query.SRQuarterPeriod);

                query.Where(query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"]));
                query.OrderBy(query.YearPeriod.Ascending, query.SRQuarterPeriod.Ascending);
                DataTable tbl = query.LoadDataTable();
                return tbl;
            }
        }

        protected void grdEmployeeDisciplinary_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeDisciplinary.DataSource = EmployeeDisciplinarys;
        }
        private DataTable EmployeeDisciplinarys
        {
            get
            {
                AppStandardReferenceItemQuery vType = new AppStandardReferenceItemQuery("e");
                AppStandardReferenceItemQuery vDegree = new AppStandardReferenceItemQuery("d");
                AppStandardReferenceItemQuery warning = new AppStandardReferenceItemQuery("c");
                EmployeeDisciplinaryQuery query = new EmployeeDisciplinaryQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");

                query.Select
                    (
                       query.EmployeeDisciplinaryID,
                       query.PersonID,
                       query.SRWarningLevel,
                       warning.ItemName.As("WarningLevelName"),
                       query.IncidentDate,
                       query.DateIssue,
                       query.Violation,
                       query.EffectViolation,
                       query.AdviceGiven,
                       query.SanctionGiven,
                       query.SRViolationDegree,
                       vDegree.ItemName.As("ViolationDegreeName"),
                       query.SRViolationType,
                       vType.ItemName.As("ViolationTypeName"),
                       query.Note,
                       query.EffectiveDate,
                       query.ValidUntil,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.InnerJoin(warning).On
                        (
                            query.SRWarningLevel == warning.ItemID &
                            warning.StandardReferenceID == "WarningLevel"
                        );
                query.LeftJoin(vDegree).On
                        (
                            query.SRViolationDegree == vDegree.ItemID &
                            vDegree.StandardReferenceID == "ViolationDegree"
                        );
                query.LeftJoin(vType).On
                        (
                            query.SRViolationType == vType.ItemID &
                            vType.StandardReferenceID == "ViolationType"
                        );
                query.Where(query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"]));
                query.OrderBy(query.EmployeeDisciplinaryID.Ascending);
                DataTable tbl = query.LoadDataTable();
                return tbl;
            }
        }
        #endregion

        #region Record Detail Method Function EmployeeAppraisalQuestion
        private void RefreshCommandItemEmployeeAppraisalQuestion()
        {
            //Toogle grid command
            bool isVisible;
            if (string.IsNullOrEmpty(cboSupervisorID.SelectedValue))
                isVisible = false;
            else
                isVisible = AppSession.UserLogin.PersonID == cboSupervisorID.SelectedValue.ToInt() || AppSession.UserLogin.PersonID == cboManagerID.SelectedValue.ToInt();

            grdAppraisalQuestion.Columns[0].Visible = isVisible;
            grdAppraisalQuestion.Columns[grdAppraisalQuestion.Columns.Count - 1].Visible = isVisible;

            grdAppraisalQuestion.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdAppraisalQuestion.Rebind();
        }

        private EmployeeAppraisalQuestionCollection EmployeeAppraisalQuestions
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeAppraisalQuestion" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((EmployeeAppraisalQuestionCollection)(obj));
                    }
                }

                var coll = new EmployeeAppraisalQuestionCollection();

                var query = new EmployeeAppraisalQuestionQuery("a");
                var aq = new AppraisalQuestionQuery("b");

                query.Select
                    (
                       query,
                       aq.QuestionerNo.As("refAppraisalQuestion_QuestionerNo"),
                       aq.QuestionerName.As("refAppraisalQuestion_QuestionerName")
                    );

                query.InnerJoin(aq).On(aq.QuestionerID == query.QuestionerID);
               
                query.Where(query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"]));
                query.OrderBy(aq.QuestionerNo.Ascending);
                coll.Load(query);

                Session["collEmployeeAppraisalQuestion" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collEmployeeAppraisalQuestion" + Request.UserHostName] = value; }
        }

        private void PopulateEmployeeAppraisalQuestionGrid()
        {
            //Display Data Detail
            EmployeeAppraisalQuestions = null; //Reset Record Detail
            grdAppraisalQuestion.DataSource = EmployeeAppraisalQuestions; //Requery
            grdAppraisalQuestion.MasterTableView.IsItemInserted = false;
            grdAppraisalQuestion.MasterTableView.ClearEditItems();
            grdAppraisalQuestion.DataBind();
        }

        protected void grdAppraisalQuestion_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdAppraisalQuestion.DataSource = EmployeeAppraisalQuestions;
        }

        protected void grdAppraisalQuestion_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeAppraisalQuestionMetadata.ColumnNames.EmployeeAppraisalQuestionerID]);
            EmployeeAppraisalQuestion entity = FindAppraisalQuestion(id);
            if (entity != null)
            {
                SetEntityValue(entity, e);
                EmployeeAppraisalQuestions.Save();
            }
        }

        protected void grdAppraisalQuestion_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeAppraisalQuestionMetadata.ColumnNames.EmployeeAppraisalQuestionerID]);
            EmployeeAppraisalQuestion entity = FindAppraisalQuestion(id);
            if (entity != null)
            {
                entity.MarkAsDeleted();
                entity.Save();
            }
        }

        protected void grdAppraisalQuestion_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeAppraisalQuestion entity = EmployeeAppraisalQuestions.AddNew();
            SetEntityValue(entity, e);
            EmployeeAppraisalQuestions.Save();

            //Stay in insert mode
            e.Canceled = true;
            grdAppraisalQuestion.Rebind();
        }
        private EmployeeAppraisalQuestion FindAppraisalQuestion(Int32 Id)
        {
            var coll = EmployeeAppraisalQuestions;
            EmployeeAppraisalQuestion retEntity = null;
            foreach (EmployeeAppraisalQuestion rec in coll)
            {
                if (rec.EmployeeAppraisalQuestionerID.ToString().Equals(Id.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeeAppraisalQuestion entity, GridCommandEventArgs e)
        {
            var userControl = (EmployeeAppraisalQuestionDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.EmployeeAppraisalQuestionerID = userControl.EmployeeAppraisalQuestionerID;
                entity.PersonID = Convert.ToInt32(Page.Request.QueryString["id"]);
                entity.QuestionerID = userControl.QuestionerID;
                entity.QuestionerName = userControl.QuestionerName;
                var aq = new AppraisalQuestion();
                if (aq.LoadByPrimaryKey(entity.QuestionerID.ToInt()))
                    entity.QuestionerNo = aq.QuestionerNo;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        #endregion

        #region Forms
        protected void grdEmployeeForm_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeForm.DataSource = EmployeeForms;
        }
        private DataTable EmployeeForms
        {
            get
            {
                var query = new EmployeeFormQuery("a");
                var template = new EmployeeFormTemplateQuery("b");
                query.InnerJoin(template).On(template.TemplateID == query.TemplateID);
                query.Select(
                        query.TransactionNo,
                        query.TransactionDate,
                        query.TemplateID,
                        template.TemplateName,
                        query.Notes,
                        query.Result.Substring(100).As("Result"),
                        @"<CAST(1 AS BIT) AS IsEditable>"
                        );

                query.Where(query.PersonID == Convert.ToInt32(Page.Request.QueryString["id"]));
                query.OrderBy(query.TransactionNo.Descending);
                DataTable tbl = query.LoadDataTable();
                return tbl;
            }
        }
        #endregion
    }
}
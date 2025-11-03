using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Reflection.Emit;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeeWorkingInfoDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "EmployeeWorkingInfoSearch.aspx?status=" + Request.QueryString["status"];
            UrlPageList = "EmployeeWorkingInfoList.aspx?status=" + Request.QueryString["status"];

            this.WindowSearch.Height = 400;

            if (Request.QueryString["status"] == "recruit")
            {
                ProgramID = AppConstant.Program.ApplicantWorkingInfo;
                lblEmployeeNumber.Text = "Applicant No";
                lblEmployeeName.Text = "Applicant Name";
            }
            else
                ProgramID = (Request.QueryString["status"] == "trn") ? AppConstant.Program.EmployeeOrientation : AppConstant.Program.EmployeeWorkingInfo; //TODO: Isi ProgramID

            //StandardReference Initialize
            if (!IsPostBack)
            {
                hdnPageId.Value = PageID;

                //txtPersonID.Text = Request.QueryString["id"];
                StandardReference.InitializeIncludeSpace(cboSREmployeeStatus, AppEnum.StandardReference.EmployeeStatus);
                StandardReference.InitializeIncludeSpace(cboSREmployeeType, AppEnum.StandardReference.EmployeeType);
                StandardReference.InitializeIncludeSpace(cboSRResignReason, AppEnum.StandardReference.ResignReason);
                StandardReference.InitializeIncludeSpace(cboSREmployeeShiftType, AppEnum.StandardReference.EmployeeShiftType);
                StandardReference.InitializeIncludeSpace(cboSREmployeeScheduleType, AppEnum.StandardReference.EmployeeScheduleType);
                StandardReference.InitializeIncludeSpace(cboSRProfessionType, AppEnum.StandardReference.ProfessionType);

                txtGradeYear.ReadOnly = true;
                trKWI.Visible = false;
                trEmployeeRegistrationNo.Visible = false; //AppSession.Parameter.IsUsingDoubleEmployeeNo;

                grdEmployeeOrganization.Columns[5].Visible = AppSession.Parameter.IsUsingFourLevelOrganizationUnit;
                grdPositionGrade.Columns.FindByUniqueName("SalaryScaleName").Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP";
                grdPositionGrade.Columns.FindByUniqueName("NextSalaryScaleName").Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP";
                grdPositionGrade.Columns.FindByUniqueName("Dp3Name").Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSA";

                if (AppSession.Parameter.IsUsingPreceptorAsProfessionalIndirectSupervisor)
                    lblPreceptorId.Text = "Professional Indirect Supervisor";

                if (Request.QueryString["status"] == "recruit")
                {
                    tabDetail.Tabs[2].Visible = false;
                    tabDetail.Tabs[3].Visible = false;
                    tabDetail.Tabs[4].Visible = false;
                    tabDetail.Tabs[5].Visible = false;
                    tabDetail.Tabs[6].Visible = false;
                    tabDetail.Tabs[7].Visible = false;
                    tabDetail.Tabs[8].Visible = false;
                    tabDetail.Tabs[9].Visible = false;
                    tabDetail.Tabs[10].Visible = false;
                    tabDetail.Tabs[11].Visible = false;
                    tabDetail.Tabs[12].Visible = false;
                    tabDetail.Tabs[13].Visible = false;
                    tabDetail.Tabs[15].Visible = false;
                }
                else if (Request.QueryString["status"] == "trn")
                {
                    tabDetail.Tabs[0].Visible = false;
                    tabDetail.Tabs[1].Visible = false;
                    tabDetail.Tabs[2].Visible = false;
                    tabDetail.Tabs[3].Visible = false;
                    tabDetail.Tabs[4].Visible = false;
                    tabDetail.Tabs[5].Visible = false;
                    tabDetail.Tabs[6].Visible = false;
                    tabDetail.Tabs[7].Visible = false;
                    //tabDetail.Tabs[8].Visible = false;
                    //tabDetail.Tabs[9].Visible = false;
                    //tabDetail.Tabs[10].Visible = false;
                    tabDetail.Tabs[11].Visible = false;
                    tabDetail.Tabs[12].Visible = false;
                    tabDetail.Tabs[13].Visible = false;
                    tabDetail.Tabs[15].Visible = false;

                    tabDetail.SelectedIndex = 8;
                    mpgDetail.SelectedIndex = 8;
                    
                    pnlAttendanceSheet.Visible = false;
                    pnlCoorporateGrade.Visible = false;
                    trDocumentUpload.Visible = false;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuAdd.Visible = false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuEditClick()
        {
            string[] empStatusResign = AppSession.Parameter.EmployeeStatueResignReference.Split(',');

            //if (empStatusResign.Any(e => e.Contains(cboSREmployeeStatus.SelectedValue)))
            if (empStatusResign.Any(e => e == cboSREmployeeStatus.SelectedValue))
            {
                txtResignDate.Enabled = true;
            }
            else
            {
                txtResignDate.Enabled = false;
            }
            if (Request.QueryString["status"] == "trn")
            {
                cboSREmployeeStatus.Enabled = false;
                cboSRResignReason.Enabled = false;
                cboSREmployeeType.Enabled = false;
                cboSRProfessionType.Enabled = false;
                cboManagerID.Enabled = false;
                cboSupervisorID.Enabled = false;
                cboPreceptorId.Enabled = false;
                txtJoinDate.Enabled = false;
            }
        }

        protected override void OnMenuNewClick()
        {
            if (Request.QueryString["status"] == "recruit")
                cboSREmployeeStatus.SelectedValue = "1"; //active

            OnPopulateEntryControl(new EmployeeWorkingInfo());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            EmployeeWorkingInfo entity = new EmployeeWorkingInfo();
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
            if (string.IsNullOrEmpty(Request.QueryString["status"]))
            {
                if (!string.IsNullOrEmpty(txtEmployeeRegistrationNo.Text))
                {
                    var emp = new EmployeeWorkingInfo();
                    emp.Query.es.Top = 1;
                    emp.Query.Where(emp.Query.EmployeeRegistrationNo == txtEmployeeRegistrationNo.Text && emp.Query.PersonID != Convert.ToInt32(txtPersonID.Value));
                    if (emp.Query.Load())
                    {
                        args.MessageText = "Employee Registration No is duplicated with another employee.";
                        args.IsCancel = true;
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(txtAbsenceCardNo.Text))
                {
                    var emp = new EmployeeWorkingInfo();
                    emp.Query.es.Top = 1;
                    emp.Query.Where(emp.Query.AbsenceCardNo == txtAbsenceCardNo.Text && emp.Query.PersonID != Convert.ToInt32(txtPersonID.Value));
                    if (emp.Query.Load())
                    {
                        args.MessageText = "Absense Card No is duplicated with another employee.";
                        args.IsCancel = true;
                        return;
                    }
                }
            }

            var entity = new EmployeeWorkingInfo();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(Request.QueryString["status"]))
            {
                if (!string.IsNullOrEmpty(txtEmployeeRegistrationNo.Text))
                {
                    var emp = new EmployeeWorkingInfo();
                    emp.Query.es.Top = 1;
                    emp.Query.Where(emp.Query.EmployeeRegistrationNo == txtEmployeeRegistrationNo.Text && emp.Query.PersonID != Convert.ToInt32(txtPersonID.Value));
                    if (emp.Query.Load())
                    {
                        args.MessageText = "Employee Registration No is duplicated with another employee.";
                        args.IsCancel = true;
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(txtAbsenceCardNo.Text))
                {
                    var emp = new EmployeeWorkingInfo();
                    emp.Query.es.Top = 1;
                    emp.Query.Where(emp.Query.AbsenceCardNo == txtAbsenceCardNo.Text && emp.Query.PersonID != Convert.ToInt32(txtPersonID.Value));
                    if (emp.Query.Load())
                    {
                        args.MessageText = "Absense Card No is duplicated with another employee.";
                        args.IsCancel = true;
                        return;
                    }
                }
            }

            var pInfo = new PersonalInfo();
            if (pInfo.LoadByPrimaryKey(Convert.ToInt32(txtPersonID.Text)))
            {
                var entity = new EmployeeWorkingInfo();
                if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPersonID.Text)))
                {
                    SetEntityValue(entity);
                    SaveEntity(entity);
                }
                else
                {
                    entity = new EmployeeWorkingInfo();
                    entity.AddNew();
                    SetEntityValue(entity);
                    SaveEntity(entity);
                }
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

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_personID", txtPersonID.Text);
        }


        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("PersonID='{0}'", txtPersonID.Text.Trim());
            auditLogFilter.TableName = "EmployeeWorkingInfo";
        }
        #endregion


        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            //txtPersonID.Text = Request.QueryString["id"];
            //txtPersonID.Enabled = (newVal == AppEnum.DataMode.New);

            RefreshCommandItemEmployeeEmploymentPeriod(newVal);
            RefreshCommandItemEmployeeEducationLevel(newVal);
            RefreshCommandItemEmployeeOrganization(newVal);
            RefreshCommandItemEmployeePosition(newVal);
            RefreshCommandItemEmployeeGrade(newVal);
            RefreshCommandItemEmployeePositionGrade(newVal);
            RefreshCommandItemEmployeeAchievement(newVal);
            RefreshCommandItemEmployeeDisciplinary(newVal);
            RefreshCommandItemEmployeeTrainingHistory(newVal);
            RefreshCommandItemEmployeeOrientation(newVal);
            RefreshCommandItemEmployeeEducation(newVal);
            RefreshCommandItemEmployeeAppraisalQuestion(newVal);
            RefreshCommandItemEmployeePerformanceAppraisal(newVal);
            RefreshCommandItemEmployeeMiscellaneousBenefit(newVal);
            RefreshCommandItemEmployeeLanguageProficiency(newVal);
            RefreshCommandItemEmployeeRL4(newVal);
            RefreshCommandItemEmployeeClinicalPrivilege(newVal);

            rblFilterOrientationType.Enabled = true;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            EmployeeWorkingInfo entity = new EmployeeWorkingInfo();
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
            var employeeWorkingInfo = (EmployeeWorkingInfo)entity;
            txtPersonID.Text = employeeWorkingInfo.PersonID.ToString();
            txtJoinDate.SelectedDate = employeeWorkingInfo.JoinDate;

            var personal = new PersonalInfo();
            personal.LoadByPrimaryKey(Convert.ToInt32(txtPersonID.Text));

            if (Request.QueryString["status"] == "recruit")
            {
                var epq = new EmployeeEmploymentPeriodQuery();
                epq.Select(epq.EmployeeNumber);
                epq.Where(epq.PersonID == txtPersonID.Value.ToInt(), epq.SREmploymentType == "0");
                DataTable epdt = epq.LoadDataTable();
                if (epdt.Rows.Count > 0)
                    txtEmployeeNumber.Text = epdt.Rows[0]["EmployeeNumber"].ToString();
                else
                    txtEmployeeNumber.Text = personal.EmployeeNumber;
            }
            else
                txtEmployeeNumber.Text = personal.EmployeeNumber;

            txtEmployeeName.Text = personal.EmployeeName;

            PopulateEmployeeImage(Convert.ToInt32(txtPersonID.Text), personal.SRGenderType);

            var plQuery = new VwEmployeeTableQuery();
            plQuery.Where(plQuery.PersonID == Convert.ToInt32(employeeWorkingInfo.SupervisorId));
            var dtb = plQuery.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {                
                cboSupervisorID.SelectedValue = string.Empty;
                cboSupervisorID.Text = string.Empty;

                cboSupervisorID.DataSource = dtb;
                cboSupervisorID.DataBind();
                cboSupervisorID.SelectedValue = employeeWorkingInfo.SupervisorId.ToString();
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
            plQuery.Where(plQuery.PersonID == Convert.ToInt32(employeeWorkingInfo.PreceptorId));
            var dtb2 = plQuery.LoadDataTable();
            if (dtb2.Rows.Count > 0)
            {
                cboPreceptorId.SelectedValue = string.Empty;
                cboPreceptorId.Text = string.Empty;

                cboPreceptorId.DataSource = dtb2;
                cboPreceptorId.DataBind();
                cboPreceptorId.SelectedValue = employeeWorkingInfo.PreceptorId.ToString();
            }
            else
            {
                cboPreceptorId.DataSource = null;
                cboPreceptorId.DataBind();
                cboPreceptorId.Items.Clear();
                cboPreceptorId.SelectedValue = string.Empty;
                cboPreceptorId.Text = string.Empty;
            }

            plQuery = new VwEmployeeTableQuery();
            plQuery.Where(plQuery.PersonID == Convert.ToInt32(employeeWorkingInfo.ManagerID));
            var dtb3 = plQuery.LoadDataTable();
            if (dtb3.Rows.Count > 0)
            {                
                cboManagerID.SelectedValue = string.Empty;
                cboManagerID.Text = string.Empty;

                cboManagerID.DataSource = dtb3;
                cboManagerID.DataBind();
                cboManagerID.SelectedValue = employeeWorkingInfo.ManagerID.ToString();
            }
            else
            {
                cboManagerID.DataSource = null;
                cboManagerID.DataBind();
                cboManagerID.Items.Clear();
                cboManagerID.SelectedValue = string.Empty;
                cboManagerID.Text = string.Empty;
            }

            DataTable employeeView = (new EmployeeWorkingInfoCollection()).EmployeeWorkingInfoView(Convert.ToInt32(txtPersonID.Text));
            string organizationID = "-1";
            try { organizationID = employeeView.Rows[0]["OrganizationUnitID"].ToString(); }
            catch { }
            string subDivisionId = "-1";
            try { subDivisionId = employeeView.Rows[0]["SubDivisonID"].ToString(); }
            catch { }
            string serviceUnitId = "-1";
            try { serviceUnitId = employeeView.Rows[0]["ServiceUnitID"].ToString(); }
            catch { }
            string positionID = "-1";
            try { positionID = employeeView.Rows[0]["PositionID"].ToString(); }
            catch { }

            int coorporateGradeID = -1;
            try { coorporateGradeID = System.Convert.ToInt32(employeeView.Rows[0]["CoorporateGradeID"]); }
            catch { }

            decimal coorporateGradeValue = 0;
            try { 
                coorporateGradeValue = System.Convert.ToDecimal(employeeView.Rows[0]["CoorporateGradeValue"]);
                txtCoorporateGradeValue.Text = coorporateGradeValue.ToString();
            }
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

            var cg = new CoorporateGrade();
            if (cg.LoadByPrimaryKey(coorporateGradeID)) {
                txtCoorporateGradeLevel.Text = cg.CoorporateGradeLevel.ToString();
            }

            var vqr = new VwEmployeeTableQuery();
            vqr.Where(vqr.PersonID == (employeeWorkingInfo.PersonID ?? 0));
            vqr.es.Top = 1;
            var v = new VwEmployeeTable();
            v.Load(vqr);

            txtServiceYear.Value = Convert.ToDouble(v.ServiceYear);
            txtServiceYearText.Text = v.ServiceYearText;
            txtServiceYearPermanent.Value = Convert.ToDouble(v.ServiceYearPermanent);
            txtServiceYearPermanentText.Text = v.ServiceYearPermanentText;
            txtBirthDate.SelectedDate = v.BirthDate;

            var ageInYear = Helper.GetAgeInYear(v.BirthDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
            var ageInMonth = Helper.GetAgeInMonth(v.BirthDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
            var ageInDay = Helper.GetAgeInDay(v.BirthDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();

            txtAge.Text = ageInYear + "th " + ageInMonth + "bl " + ageInDay + "hr";

            cboSREmployeeStatus.SelectedValue = employeeWorkingInfo.SREmployeeStatus;

            string[] empStatusResign = AppSession.Parameter.EmployeeStatueResignReference.Split(',');

            //if (empStatusResign.Any(e => e.Contains(cboSREmployeeStatus.SelectedValue)))
            if (empStatusResign.Any(e => e == cboSREmployeeStatus.SelectedValue))
            {
                trResign.Visible = true;
                cboSRResignReason.SelectedValue = employeeWorkingInfo.SRResignReason;
                txtResignDate.SelectedDate = employeeWorkingInfo.ResignDate;
            }
            else
            {
                trResign.Visible = false;
                cboSRResignReason.SelectedValue = string.Empty;
                txtResignDate.SelectedDate = v.ResignDate;
            }

            cboSREmployeeType.SelectedValue = employeeWorkingInfo.SREmployeeType;

            if (v.PositionGradeID != null && v.PositionGradeID != -1)
            {
                var pg = new PositionGrade();
                if (pg.LoadByPrimaryKey(Convert.ToInt32(v.PositionGradeID)))
                    txtPositionGradeID.Text = pg.PositionGradeName;
                else txtPositionGradeID.Text = string.Empty;
            }
            else txtPositionGradeID.Text = string.Empty;

            if (v.GradeYear != null)
            {
                txtGradeYear.Value = Convert.ToDouble(v.GradeYear);
                txtGradeYearString.Text = Convert.ToString(v.GradeYear);
            }
                
            cboSREmployeeShiftType.SelectedValue = employeeWorkingInfo.SREmployeeShiftType;
            cboSREmployeeScheduleType.SelectedValue = employeeWorkingInfo.SREmployeeScheduleType;
            cboSRProfessionType.SelectedValue = employeeWorkingInfo.SRProfessionType;
            txtAbsenceCardNo.Text = employeeWorkingInfo.AbsenceCardNo;
            txtEmployeeRegistrationNo.Text = employeeWorkingInfo.EmployeeRegistrationNo;
            txtNote.Text = employeeWorkingInfo.Note;
            chkKWI.Checked = employeeWorkingInfo.IsKWI ?? false;

            var asri = new AppStandardReferenceItem();
            if (asri.LoadByPrimaryKey(AppEnum.StandardReference.EmploymentType.ToString(), v.SREmploymentType))
                txtSREmploymentType.Text = asri.ItemName;
            else txtSREmploymentType.Text = string.Empty;

            asri = new AppStandardReferenceItem();
            if (asri.LoadByPrimaryKey(AppEnum.StandardReference.EducationLevel.ToString(), v.SREducationLevel))
                txtSREducationLevel.Text = asri.ItemName;
            else txtSREducationLevel.Text = string.Empty;

            //Display Data Detail
            PopulateEmployeeEmploymentPeriodGrid();
            PopulateEmployeeEducationLevelGrid();
            PopulateEmployeeOrganizationGrid();
            PopulateEmployeePositionGrid();
            PopulateEmployeeGradeGrid();
            PopulateEmployeePositionGradeGrid();
            PopulateEmployeeAchievementGrid();
            PopulateEmployeeDisciplinaryGrid();
            PopulateEmployeeTrainingHistoryGrid();
            PopulateEmployeeOrientationGrid();
            PopulateEmployeeEducationGrid();
            PopulateEmployeeAppraisalQuestionGrid();
            PopulateEmployeePerformanceAppraisalGrid();
            PopulateEmployeeMiscellaneousBenefitGrid();
            PopulateEmployeeLanguageProficiencyGrid();
            PopulateEmployeeRL4Grid();
            PopulateEmployeeClinicalPrivilegeGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(EmployeeWorkingInfo entity)
        {
            entity.PersonID = Convert.ToInt32(txtPersonID.Value);
            entity.JoinDate = txtJoinDate.SelectedDate;
            entity.SupervisorId = string.IsNullOrEmpty(cboSupervisorID.SelectedValue) ? -1 : Convert.ToInt32(cboSupervisorID.SelectedValue);
            entity.SREmployeeStatus = cboSREmployeeStatus.SelectedValue;
            entity.SREmployeeType = cboSREmployeeType.SelectedValue;
            entity.PositionGradeID = -1;
            entity.SRRemunerationType = string.Empty;
            entity.AbsenceCardNo = txtAbsenceCardNo.Text;
            entity.EmployeeRegistrationNo = txtEmployeeRegistrationNo.Text;
            entity.Note = txtNote.Text;
            entity.IsKWI = chkKWI.Checked;

            entity.GradeYear = Convert.ToInt32(txtGradeYear.Value);
            entity.SREducationLevel = "XXX";

            entity.PreceptorId = string.IsNullOrEmpty(cboPreceptorId.SelectedValue) ? -1 : Convert.ToInt32(cboPreceptorId.SelectedValue);
            entity.ManagerID = string.IsNullOrEmpty(cboManagerID.SelectedValue) ? -1 : Convert.ToInt32(cboManagerID.SelectedValue);
            entity.SREmployeeShiftType = cboSREmployeeShiftType.SelectedValue;
            entity.SREmployeeScheduleType = cboSREmployeeScheduleType.SelectedValue;
            entity.SRProfessionType = cboSRProfessionType.SelectedValue;

            string[] empStatusResign = AppSession.Parameter.EmployeeStatueResignReference.Split(',');
            //if (empStatusResign.Any(e => e.Contains(cboSREmployeeStatus.SelectedValue)))
            if (empStatusResign.Any(e => e == cboSREmployeeStatus.SelectedValue))
            {
                entity.SRResignReason = cboSRResignReason.SelectedValue;
                entity.ResignDate = txtResignDate.SelectedDate;
            }
            else
            {
                entity.SRResignReason = string.Empty;
                entity.str.ResignDate = string.Empty;
            }

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //--> Employe eEmployment Period
            foreach (EmployeeEmploymentPeriod employment in EmployeeEmploymentPeriods)
            {
                employment.PersonID = Convert.ToInt32(txtPersonID.Text);
            }

            //--> Employee Education Level
            foreach (EmployeeEducationLevel edu in EmployeeEducationLevels)
            {
                edu.PersonID = Convert.ToInt32(txtPersonID.Text);
            }

            //--> Employe Employee Organization
            foreach (EmployeeOrganization organization in EmployeeOrganizations)
            {
                organization.PersonID = Convert.ToInt32(txtPersonID.Text);
            }

            //--> Employe Position
            foreach (EmployeePosition position in EmployeePositions)
            {
                position.PersonID = Convert.ToInt32(txtPersonID.Text);
            }

            //--> Employe Position Grade
            foreach (EmployeeGrade grade in EmployeeGrades)
            {
                grade.PersonID = Convert.ToInt32(txtPersonID.Text);
            }

            //--> Employe Position Grade
            foreach (EmployeePositionGrade positionGrade in EmployeePositionGrades)
            {
                positionGrade.PersonID = Convert.ToInt32(txtPersonID.Text);
            }

            //--> Employee Achievement
            foreach (EmployeeAchievement achievement in EmployeeAchievements)
            {
                achievement.PersonID = Convert.ToInt32(txtPersonID.Text);
            }

            //--> Employee Disciplinary
            foreach (EmployeeDisciplinary disciplinary in EmployeeDisciplinarys)
            {
                disciplinary.PersonID = Convert.ToInt32(txtPersonID.Text);
            }

            //--> EmployeeTrainingHistory
            foreach (EmployeeTrainingHistory training in EmployeeTrainingHistorys)
            {
                training.PersonID = Convert.ToInt32(txtPersonID.Text);
            }

            //--> EmployeeOrientation
            foreach (EmployeeOrientation orientation in EmployeeOrientations)
            {
                orientation.PersonID = Convert.ToInt32(txtPersonID.Text);
            }

            //--> EmployeeEducation
            foreach (EmployeeEducation edu in EmployeeEducations)
            {
                edu.PersonID = Convert.ToInt32(txtPersonID.Text);
            }

            //--> EmployeeAppraisalQuestion
            foreach (EmployeeAppraisalQuestion question in EmployeeAppraisalQuestions)
            {
                question.PersonID = Convert.ToInt32(txtPersonID.Text);
            }

            //--> EmployeePerformanceAppraisal
            foreach (EmployeePerformanceAppraisal appraisal in EmployeePerformanceAppraisals)
            {
                appraisal.PersonID = Convert.ToInt32(txtPersonID.Text);
            }

            //--> Employee Miscellaneous Benefit
            foreach (EmployeeMiscellaneousBenefit benefit in EmployeeMiscellaneousBenefits)
            {
                benefit.PersonID = Convert.ToInt32(txtPersonID.Text);
            }

            //--> Employee Language Proficiency
            foreach (EmployeeLanguageProficiency language in EmployeeLanguageProficiencys)
            {
                language.PersonID = Convert.ToInt32(txtPersonID.Text);
            }

            //--> Employee RL4
            foreach (EmployeeRL4 rl4 in EmployeeRL4s)
            {
                rl4.PersonID = Convert.ToInt32(txtPersonID.Text);
            }

            foreach (EmployeeClinicalPrivilege cp in EmployeeClinicalPrivileges)
            {
                cp.PersonID = Convert.ToInt32(txtPersonID.Text);
            }
        }

        private void SaveEntity(EmployeeWorkingInfo entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                if (Request.QueryString["status"] == "trn")
                {
                    EmployeeTrainingHistorys.Save();
                    EmployeeOrientations.Save();
                    EmployeeEducations.Save();
                }
                else
                {
                    entity.Save();

                    var info = new EmployeeSalaryInfo();
                    info.Query.Where(info.Query.PersonID == entity.PersonID);
                    if (!info.Query.Load())
                    {
                        var npwp = string.Empty;
                        var jamsostek = string.Empty;
                        var iden = new PersonalIdentificationCollection();
                        iden.Query.Where(iden.Query.PersonID == txtPersonID.Text, iden.Query.SRIdentificationType.In("5", "6")); //npwp, jamsostek (bpjs ketenagakerjaan)
                        if (iden.Query.Load())
                        {
                            var id = iden.Where(i => i.SRIdentificationType == "6").SingleOrDefault();
                            if (id != null) npwp = id.IdentificationValue;
                            id = iden.Where(i => i.SRIdentificationType == "5").SingleOrDefault();
                            if (id != null) jamsostek = id.IdentificationValue;
                        }

                        info = new EmployeeSalaryInfo()
                        {
                            PersonID = entity.PersonID,
                            Npwp = npwp,
                            SRPaymentFrequency = string.Empty,
                            SRTaxStatus = string.Empty,
                            BankID = string.Empty,
                            BankAccountNo = string.Empty,
                            NoOfDependent = 0,
                            LastUpdateDateTime = DateTime.Now,
                            LastUpdateByUserID = AppSession.UserLogin.UserID,
                            JamsostekNo = jamsostek,
                            SREmployeeType = string.Empty,
                            IsSalaryManaged = true
                        };
                        info.Save();
                    }

                    EmployeeEmploymentPeriods.Save();
                    EmployeeEducationLevels.Save();
                    EmployeeOrganizations.Save();
                    EmployeePositions.Save();
                    EmployeeGrades.Save();
                    EmployeePositionGrades.Save();
                    EmployeeAchievements.Save();
                    EmployeeDisciplinarys.Save();
                    EmployeeTrainingHistorys.Save();
                    EmployeeOrientations.Save();
                    EmployeeEducations.Save();
                    EmployeeAppraisalQuestions.Save();
                    EmployeePerformanceAppraisals.Save();
                    EmployeeMiscellaneousBenefits.Save();
                    EmployeeLanguageProficiencys.Save();
                    EmployeeRL4s.Save();
                    EmployeeClinicalPrivileges.Save();

                    var std = new AppStandardReferenceItem();
                    if (std.LoadByPrimaryKey("EmployeeType", entity.SREmployeeType))
                    {
                        string guarId = AppSession.Parameter.GuarantorEmployeeID;
                        if (!string.IsNullOrEmpty(std.ReferenceID))
                        {
                            var parArray = std.ReferenceID.Split('|');
                            guarId = Convert.ToString(parArray[0]);
                        }

                        var pInfo = new PersonalInfo();
                        if (pInfo.LoadByPrimaryKey(Convert.ToInt32(entity.PersonID)))
                        {
                            if (!string.IsNullOrEmpty(pInfo.PatientID))
                            {
                                var pat = new Patient();
                                if (pat.LoadByPrimaryKey(pInfo.PatientID))
                                {
                                    pat.GuarantorID = entity.SREmployeeStatus != "0"
                                                          ? guarId
                                                          : AppSession.Parameter.SelfGuarantor;
                                    pat.PersonID = entity.SREmployeeStatus != "0"
                                                          ? entity.PersonID
                                                          : 0;
                                    pat.EmployeeNumber = entity.SREmployeeStatus != "0"
                                                          ? pInfo.EmployeeNumber
                                                          : string.Empty;
                                    pat.SREmployeeRelationship = entity.SREmployeeStatus != "0"
                                                          ? "Relationship-000"
                                                          : string.Empty;

                                    pat.Save();
                                }
                            }

                            var pFams = new PersonalFamilyCollection();
                            pFams.Query.Where(pFams.Query.PersonID == entity.PersonID);
                            pFams.LoadAll();
                            foreach (var family in pFams)
                            {
                                if (!string.IsNullOrEmpty(family.PatientID))
                                {
                                    if (family.IsGuaranteed == true)
                                    {
                                        var pat = new Patient();
                                        if (pat.LoadByPrimaryKey(family.PatientID))
                                        {
                                            pat.GuarantorID = entity.SREmployeeStatus != "0"
                                                          ? guarId
                                                          : AppSession.Parameter.SelfGuarantor;
                                            pat.PersonID = entity.SREmployeeStatus != "0"
                                                                  ? entity.PersonID
                                                                  : 0;
                                            pat.EmployeeNumber = entity.SREmployeeStatus != "0"
                                                                  ? pInfo.EmployeeNumber
                                                                  : string.Empty;

                                            std = new AppStandardReferenceItem();
                                            pat.SREmployeeRelationship = std.LoadByPrimaryKey("FamilyRelation", family.SRFamilyRelation) ? std.ReferenceID : string.Empty;

                                            if (entity.SREmployeeStatus == "0")
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
                                            pat.PersonID = 0;
                                            pat.EmployeeNumber = string.Empty;
                                            pat.SREmployeeRelationship = string.Empty;
                                            pat.Save();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (Request.QueryString["status"] == "recruit")
                    {
                        if (entity.SREmployeeStatus == "1")
                        {
                            var employeeNo = txtEmployeeNumber.Text;

                            try
                            {
                                var coll = (EmployeeEmploymentPeriods.Where(b => b.SREmploymentType != "0")
                                                    .OrderBy(b => b.ValidFrom)).Take(1).Single();
                                if (coll != null)
                                {
                                    employeeNo = coll.EmployeeNumber;
                                }
                            }
                            catch
                            {
                            }

                            var pInfo = new PersonalInfo();
                            if (pInfo.LoadByPrimaryKey(entity.PersonID.ToInt()))
                            {
                                pInfo.EmployeeNumber = employeeNo;
                                pInfo.Save();
                            }
                        }
                    }
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new EmployeeWorkingInfoQuery("a");
            var info = new VwEmployeeTableQuery("b");
            que.InnerJoin(info).On(info.PersonID == que.PersonID);

            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PersonID > txtPersonID.Text.ToInt());
                que.OrderBy(que.PersonID.Ascending);
            }
            else
            {
                que.Where(que.PersonID < txtPersonID.Text.ToInt());
                que.OrderBy(que.PersonID.Descending);
            }

            if (Request.QueryString["status"] == "recruit")
                que.Where(info.SREmploymentType == "0"); // applicant
            else
                que.Where(info.SREmploymentType != "0");

            var entity = new EmployeeWorkingInfo();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged
        protected void cboSREmployeeStatus_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string[] empStatusResign = AppSession.Parameter.EmployeeStatueResignReference.Split(',');

            //if (empStatusResign.Any(emp => emp.Contains(cboSREmployeeStatus.SelectedValue)))
            if (empStatusResign.Any(emp => emp == cboSREmployeeStatus.SelectedValue))
            {
                trResign.Visible = true;
                //lblResignDate.Visible = true;
                txtResignDate.Enabled = true;
            }
            else
            {
                trResign.Visible = false;
                cboSRResignReason.SelectedValue = string.Empty;
                //lblResignDate.Visible = false;
                txtResignDate.Enabled = false;
                txtResignDate.SelectedDate = null;
            }
        }
       
        #endregion

        #region ComboBox Function

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

        #endregion ComboBox Function

        #region Record Detail Method Function EmployeeEmploymentPeriod
        private void RefreshCommandItemEmployeeEmploymentPeriod(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdEmployeeEmploymentPeriod.Columns[0].Visible = isVisible;
            grdEmployeeEmploymentPeriod.Columns[grdEmployeeEmploymentPeriod.Columns.Count - 1].Visible = isVisible;

            grdEmployeeEmploymentPeriod.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdEmployeeEmploymentPeriod.Rebind();
        }

        private EmployeeEmploymentPeriodCollection EmployeeEmploymentPeriods
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeEmploymentPeriod" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((EmployeeEmploymentPeriodCollection)(obj));
                    }
                }

                var coll = new EmployeeEmploymentPeriodCollection();
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
                       employment.ItemName.As("refToEmploymentTypeName_EmployeeEmploymentPeriod"),
                       query.SREmploymentCategory,
                       category.ItemName.As("refToEmploymentCategoryName_EmployeeEmploymentPeriod"),
                       query.ValidFrom,
                       query.ValidTo,
                       query.Note,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime,
                       query.EmployeeEmploymentPeriodID,
                       query.RecruitmentPlanID,
                       rec.RecruitmentPlanName.As("refToRecruitmentPlan_RecruitmentPlanName"),
                       query.EmployeeNumber, 
                       query.LastUpdateByUserID, 
                       query.LastUpdateDateTime
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
                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.ValidFrom.Descending, query.ValidTo.Descending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collEmployeeEmploymentPeriod" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collEmployeeEmploymentPeriod" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulateEmployeeEmploymentPeriodGrid()
        {
            //Display Data Detail
            EmployeeEmploymentPeriods = null; //Reset Record Detail
            grdEmployeeEmploymentPeriod.DataSource = EmployeeEmploymentPeriods; //Requery
            grdEmployeeEmploymentPeriod.MasterTableView.IsItemInserted = false;
            grdEmployeeEmploymentPeriod.MasterTableView.ClearEditItems();
            grdEmployeeEmploymentPeriod.DataBind();
        }

        protected void grdEmployeeEmploymentPeriod_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeEmploymentPeriod.DataSource = EmployeeEmploymentPeriods;
        }

        protected void grdEmployeeEmploymentPeriod_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 employeeEmploymentPeriodID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeEmploymentPeriodMetadata.ColumnNames.EmployeeEmploymentPeriodID]);
            EmployeeEmploymentPeriod entity = FindEmployeeEmploymentPeriod(employeeEmploymentPeriodID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdEmployeeEmploymentPeriod_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 employeeEmploymentPeriodID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeEmploymentPeriodMetadata.ColumnNames.EmployeeEmploymentPeriodID]);
            EmployeeEmploymentPeriod entity = FindEmployeeEmploymentPeriod(employeeEmploymentPeriodID);
            if (entity != null && entity.SREmploymentType != "0")
                entity.MarkAsDeleted();
        }

        protected void grdEmployeeEmploymentPeriod_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeEmploymentPeriod entity = EmployeeEmploymentPeriods.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdEmployeeEmploymentPeriod.Rebind();
        }
        private EmployeeEmploymentPeriod FindEmployeeEmploymentPeriod(Int32 employeeEmploymentPeriodID)
        {
            EmployeeEmploymentPeriodCollection coll = EmployeeEmploymentPeriods;
            EmployeeEmploymentPeriod retEntity = null;
            foreach (EmployeeEmploymentPeriod rec in coll)
            {
                if (rec.EmployeeEmploymentPeriodID.ToString().Equals(employeeEmploymentPeriodID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeeEmploymentPeriod entity, GridCommandEventArgs e)
        {
            EmployeeEmploymentPeriodDetail userControl = (EmployeeEmploymentPeriodDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.EmployeeEmploymentPeriodID = userControl.EmployeeEmploymentPeriodID;
                entity.SREmploymentType = userControl.SREmploymentType;
                entity.EmploymentTypeName = userControl.EmploymentTypeName;
                entity.SREmploymentCategory = userControl.SREmploymentCategory;
                entity.EmploymentCategoryName = userControl.EmploymentCategoryName;
                entity.ValidFrom = userControl.ValidFrom;
                entity.ValidTo = userControl.ValidTo;
                entity.RecruitmentPlanID = userControl.RecruitmentPlanID;
                entity.RecruitmentPlanName = userControl.RecruitmentPlanName;
                entity.EmployeeNumber = userControl.EmployeeNumber;
                entity.Note = userControl.Note;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeeEducationLevel
        private void RefreshCommandItemEmployeeEducationLevel(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdEmployeeEducationLevel.Columns[0].Visible = isVisible;
            grdEmployeeEducationLevel.Columns[grdEmployeeEducationLevel.Columns.Count - 1].Visible = isVisible;

            grdEmployeeEducationLevel.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdEmployeeEducationLevel.Rebind();
        }

        private EmployeeEducationLevelCollection EmployeeEducationLevels
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeEducationLevel" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((EmployeeEducationLevelCollection)(obj));
                    }
                }

                var coll = new EmployeeEducationLevelCollection();
                var query = new EmployeeEducationLevelQuery("a");
                var personal = new PersonalInfoQuery("b");
                var edu = new AppStandardReferenceItemQuery("c");
                var labor = new AppStandardReferenceItemQuery("d");
                var edugr = new AppStandardReferenceItemQuery("e");

                query.Select
                    (
                       query.EmployeeEducationLevelID,
                       query.PersonID,
                       query.SREducationLevel,
                       edu.ItemName.As("refToEducationLevelName_EmployeeEducationLevel"),
                       edugr.ItemName.As("refToEducationGroupName_EmployeeEducationLevel"),
                       labor.ItemName.As("refToTypeOfLaborName_EmployeeEducationLevel"),
                       query.ValidFrom,
                       query.ValidTo,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.LeftJoin(edu).On
                        (
                            query.SREducationLevel == edu.ItemID &
                            edu.StandardReferenceID == "EducationLevel"
                        );
                query.LeftJoin(labor).On
                       (
                           edu.Note == labor.ItemID &
                           labor.StandardReferenceID == AppEnum.StandardReference.FieldLabor.ToString()
                       );
                query.LeftJoin(edugr).On
                       (
                           edu.CustomField == edugr.ItemID &
                           edugr.StandardReferenceID == AppEnum.StandardReference.EducationGroup.ToString()
                       );
                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.ValidFrom.Descending, query.ValidTo.Descending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collEmployeeEducationLevel" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collEmployeeEducationLevel" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulateEmployeeEducationLevelGrid()
        {
            //Display Data Detail
            EmployeeEducationLevels = null; //Reset Record Detail
            grdEmployeeEducationLevel.DataSource = EmployeeEducationLevels; //Requery
            grdEmployeeEducationLevel.MasterTableView.IsItemInserted = false;
            grdEmployeeEducationLevel.MasterTableView.ClearEditItems();
            grdEmployeeEducationLevel.DataBind();
        }

        protected void grdEmployeeEducationLevel_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeEducationLevel.DataSource = EmployeeEducationLevels;
        }

        protected void grdEmployeeEducationLevel_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeEducationLevelMetadata.ColumnNames.EmployeeEducationLevelID]);
            EmployeeEducationLevel entity = FindEmployeeEducationLevel(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdEmployeeEducationLevel_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeEducationLevelMetadata.ColumnNames.EmployeeEducationLevelID]);
            EmployeeEducationLevel entity = FindEmployeeEducationLevel(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdEmployeeEducationLevel_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeEducationLevel entity = EmployeeEducationLevels.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdEmployeeEducationLevel.Rebind();
        }
        private EmployeeEducationLevel FindEmployeeEducationLevel(Int32 id)
        {
            EmployeeEducationLevelCollection coll = EmployeeEducationLevels;
            EmployeeEducationLevel retEntity = null;
            foreach (EmployeeEducationLevel rec in coll)
            {
                if (rec.EmployeeEducationLevelID.ToString().Equals(id.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeeEducationLevel entity, GridCommandEventArgs e)
        {
            EmployeeEducationLevelDetail userControl = (EmployeeEducationLevelDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.EmployeeEducationLevelID = userControl.EmployeeEducationLevelID;
                entity.SREducationLevel = userControl.SREducationLevel;
                entity.EducationLevelName = userControl.EducationLevelName;

                var std = new AppStandardReferenceItem();
                if (std.LoadByPrimaryKey(AppEnum.StandardReference.EducationLevel.ToString(), entity.SREducationLevel))
                {
                    var id = std.Note;
                    var eduGroup = std.CustomField;
                    std = new AppStandardReferenceItem();
                    if (std.LoadByPrimaryKey(AppEnum.StandardReference.FieldLabor.ToString(), id))
                        entity.TypeOfLaborName = std.ItemName;
                    else entity.TypeOfLaborName = string.Empty;

                    std = new AppStandardReferenceItem();
                    if (!string.IsNullOrEmpty(eduGroup)  && std.LoadByPrimaryKey(AppEnum.StandardReference.EducationGroup.ToString(), eduGroup))
                        entity.EducationGroupName = std.ItemName;
                    else entity.EducationGroupName = string.Empty;
                }
                else
                {
                    entity.TypeOfLaborName = string.Empty;
                    entity.EducationGroupName = string.Empty;
                }

                entity.ValidFrom = userControl.ValidFrom;
                entity.ValidTo = userControl.ValidTo;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeeOrganization
        private void RefreshCommandItemEmployeeOrganization(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdEmployeeOrganization.Columns[0].Visible = isVisible;
            grdEmployeeOrganization.Columns[grdEmployeeOrganization.Columns.Count - 1].Visible = isVisible;
            grdEmployeeOrganization.Columns.FindByUniqueName("DocumentUpload").Visible = !isVisible;

            grdEmployeeOrganization.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdEmployeeOrganization.Rebind();
        }

        private EmployeeOrganizationCollection EmployeeOrganizations
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeOrganization" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((EmployeeOrganizationCollection)(obj));
                    }
                }

                EmployeeOrganizationCollection coll = new EmployeeOrganizationCollection();
                OrganizationUnitQuery subOrganization = new OrganizationUnitQuery("d");
                OrganizationUnitQuery organization = new OrganizationUnitQuery("c");
                EmployeeOrganizationQuery query = new EmployeeOrganizationQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");
                OrganizationUnitQuery subDivision = new OrganizationUnitQuery("e");
                var asri = new AppStandardReferenceItemQuery("f");

                query.Select
                    (
                       query,
                       organization.OrganizationUnitName.As("refToEmployeeOrganization_OrganizationUnitName"),
                       subOrganization.OrganizationUnitName.As("refToEmployeeOrganization_SubOrganizationUnitName"),
                       subDivision.OrganizationUnitName.As("refToEmployeeOrganization_SubDivisonName"),
                       asri.ItemName.As("refToAppStandardReferenceItem_ItemName")
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.InnerJoin(organization).On(query.OrganizationID == organization.OrganizationUnitID);
                query.LeftJoin(subOrganization).On(query.SubOrganizationID == subOrganization.OrganizationUnitID);
                query.LeftJoin(subDivision).On(query.SubDivisonID == subDivision.OrganizationUnitID);
                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSA")
                {
                    var unit = new ServiceUnitQuery("x");
                    query.Select(unit.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"));
                    query.LeftJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                }
                else
                {
                    var org = new OrganizationUnitQuery("z");
                    query.Select(org.OrganizationUnitName.As("refToServiceUnit_ServiceUnitName"));
                    query.LeftJoin(org).On(query.ServiceUnitID == org.OrganizationUnitID);
                }
                query.LeftJoin(asri).On(query.SROrganizationLevelType == asri.ItemID && asri.StandardReferenceID == AppEnum.StandardReference.OrganizationLevelType.ToString());

                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.SROrganizationLevelType.Ascending, query.ValidFrom.Descending, query.ValidTo.Descending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collEmployeeOrganization" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collEmployeeOrganization" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulateEmployeeOrganizationGrid()
        {
            //Display Data Detail
            EmployeeOrganizations = null; //Reset Record Detail
            grdEmployeeOrganization.DataSource = EmployeeOrganizations; //Requery
            grdEmployeeOrganization.MasterTableView.IsItemInserted = false;
            grdEmployeeOrganization.MasterTableView.ClearEditItems();
            grdEmployeeOrganization.DataBind();
        }

        protected void grdEmployeeOrganization_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeOrganization.DataSource = EmployeeOrganizations;
        }

        protected void grdEmployeeOrganization_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 employeeOrganizationID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeOrganizationMetadata.ColumnNames.EmployeeOrganizationID]);
            EmployeeOrganization entity = FindEmployeeOrganization(employeeOrganizationID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdEmployeeOrganization_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 employeeOrganizationID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeOrganizationMetadata.ColumnNames.EmployeeOrganizationID]);
            EmployeeOrganization entity = FindEmployeeOrganization(employeeOrganizationID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdEmployeeOrganization_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeOrganization entity = EmployeeOrganizations.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdEmployeeOrganization.Rebind();
        }
        private EmployeeOrganization FindEmployeeOrganization(Int32 employeeOrganizationID)
        {
            EmployeeOrganizationCollection coll = EmployeeOrganizations;
            EmployeeOrganization retEntity = null;
            foreach (EmployeeOrganization rec in coll)
            {
                if (rec.EmployeeOrganizationID.ToString().Equals(employeeOrganizationID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeeOrganization entity, GridCommandEventArgs e)
        {
            EmployeeOrganizationDetail userControl = (EmployeeOrganizationDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.EmployeeOrganizationID = entity.EmployeeOrganizationID;
                entity.OrganizationID = userControl.OrganizationID;
                entity.OrganizationUnitName = userControl.OrganizationName;
                entity.SubOrganizationID = userControl.SubOrganizationID;
                entity.SubOrganizationUnitName = userControl.SubOrganizationName;
                entity.ValidFrom = userControl.ValidFrom;
                entity.ValidTo = userControl.ValidTo;
                entity.IsActive = userControl.IsActive;
                entity.ServiceUnitID = userControl.ServiceUnitID;
                entity.ServiceUnitName = userControl.ServiceUnitName;
                entity.SubDivisonID = userControl.SubDivisonID;
                entity.SubDivisonName = userControl.SubDivisionName;
                entity.SROrganizationLevelType = userControl.SROrganizationLevelTypeID;
                entity.OrganizationLevelTypeName = userControl.SROrganizationLevelTypeName;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeePosition
        private void RefreshCommandItemEmployeePosition(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdEmployeePosition.Columns[0].Visible = isVisible;
            grdEmployeePosition.Columns[grdEmployeePosition.Columns.Count - 1].Visible = isVisible;
            grdEmployeePosition.Columns.FindByUniqueName("DocumentUpload").Visible = !isVisible;
            grdEmployeePosition.Columns.FindByUniqueName("PrintJobDescription").Visible = !string.IsNullOrEmpty(AppSession.Parameter.ProgramIdPrintJobDescription);

            grdEmployeePosition.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdEmployeePosition.Rebind();
        }

        private EmployeePositionCollection EmployeePositions
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeePosition" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((EmployeePositionCollection)(obj));
                    }
                }

                EmployeePositionCollection coll = new EmployeePositionCollection();
                PositionQuery position = new PositionQuery("c");
                EmployeePositionQuery query = new EmployeePositionQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");
                CoorporateGradeQuery cg = new CoorporateGradeQuery("cg");

                query.Select
                    (
                       query,
                       position.PositionName.As("refToPosition_PositionName"),
                       cg.CoorporateGradeLevel.As("refToCoorporateGrade_Level")
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.InnerJoin(position).On(query.PositionID == position.PositionID);

                query.LeftJoin(cg).On(query.CoorporateGradeID == cg.CoorporateGradeID);

                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.ValidFrom.Descending, query.ValidTo.Descending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collEmployeePosition" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collEmployeePosition" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulateEmployeePositionGrid()
        {
            //Display Data Detail
            EmployeePositions = null; //Reset Record Detail
            grdEmployeePosition.DataSource = EmployeePositions; //Requery
            grdEmployeePosition.MasterTableView.IsItemInserted = false;
            grdEmployeePosition.MasterTableView.ClearEditItems();
            grdEmployeePosition.DataBind();
        }

        protected void grdEmployeePosition_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeePosition.DataSource = EmployeePositions;
        }

        protected void grdEmployeePosition_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 employeePositionID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeePositionMetadata.ColumnNames.EmployeePositionID]);
            EmployeePosition entity = FindEmployeePosition(employeePositionID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdEmployeePosition_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 employeePositionID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeePositionMetadata.ColumnNames.EmployeePositionID]);
            EmployeePosition entity = FindEmployeePosition(employeePositionID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdEmployeePosition_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeePosition entity = EmployeePositions.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdEmployeePosition.Rebind();
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
        private EmployeePosition FindEmployeePosition(Int32 employeePositionID)
        {
            EmployeePositionCollection coll = EmployeePositions;
            EmployeePosition retEntity = null;
            foreach (EmployeePosition rec in coll)
            {
                if (rec.EmployeePositionID.ToString().Equals(employeePositionID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeePosition entity, GridCommandEventArgs e)
        {
            EmployeePositionDetail userControl = (EmployeePositionDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.EmployeePositionID = userControl.EmployeePositionID;
                entity.PositionID = userControl.PositionID;
                entity.PositionName = userControl.PositionName;
                entity.IsPrimaryPosition = userControl.IsPrimaryPosition;
                entity.ValidFrom = userControl.ValidFrom;
                entity.ValidTo = userControl.ValidTo;
                entity.AssignmentNo = userControl.AssignmentNo;
                entity.ResignmentNo = userControl.ResignmentNo;
                entity.PositionDescription = userControl.PositionDescription;
                entity.CoorporateGradeID = userControl.CoorporateGradeID;
                entity.CoorporateGradeLevel = userControl.CoorporateGradeLevel;
                entity.CoorporateGradeValue = userControl.CoorporateGradeValue;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeeGrade
        private void RefreshCommandItemEmployeeGrade(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdEmployeeGrade.Columns[0].Visible = isVisible;
            grdEmployeeGrade.Columns[grdEmployeeGrade.Columns.Count - 1].Visible = isVisible;

            grdEmployeeGrade.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdEmployeeGrade.Rebind();
        }

        private EmployeeGradeCollection EmployeeGrades
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeGrade" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((EmployeeGradeCollection)(obj));
                    }
                }

                EmployeeGradeCollection coll = new EmployeeGradeCollection();
                EmployeeGradeMasterQuery grade = new EmployeeGradeMasterQuery("c");
                EmployeeGradeQuery query = new EmployeeGradeQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");

                query.Select
                    (
                       query.EmployeeGradeID,
                       query.PersonID,
                       query.EmployeeGradeMasterID,
                       grade.EmployeeGradeName.As("refToEmployeeGradeMaster_EmployeeGraderName"),
                       query.SalaryTableNumber,
                       query.ValidFrom,
                       query.ValidTo,
                       query.IsActive,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.InnerJoin(grade).On(query.EmployeeGradeMasterID == grade.EmployeeGradeMasterID);

                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.ValidFrom.Descending, query.ValidTo.Descending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collEmployeeGrade" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collEmployeeGrade" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulateEmployeeGradeGrid()
        {
            //Display Data Detail
            EmployeeGrades = null; //Reset Record Detail
            grdEmployeeGrade.DataSource = EmployeeGrades; //Requery
            grdEmployeeGrade.MasterTableView.IsItemInserted = false;
            grdEmployeeGrade.MasterTableView.ClearEditItems();
            grdEmployeeGrade.DataBind();
        }

        protected void grdEmployeeGrade_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeGrade.DataSource = EmployeeGrades;
        }

        protected void grdEmployeeGrade_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 employeeGradeID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeGradeMetadata.ColumnNames.EmployeeGradeID]);
            EmployeeGrade entity = FindEmployeeGrade(employeeGradeID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdEmployeeGrade_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 employeeGradeID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeGradeMetadata.ColumnNames.EmployeeGradeID]);
            EmployeeGrade entity = FindEmployeeGrade(employeeGradeID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdEmployeeGrade_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeGrade entity = EmployeeGrades.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdEmployeeGrade.Rebind();
        }
        private EmployeeGrade FindEmployeeGrade(Int32 employeeGradeID)
        {
            EmployeeGradeCollection coll = EmployeeGrades;
            EmployeeGrade retEntity = null;
            foreach (EmployeeGrade rec in coll)
            {
                if (rec.EmployeeGradeID.ToString().Equals(employeeGradeID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeeGrade entity, GridCommandEventArgs e)
        {
            EmployeeGradeDetail userControl = (EmployeeGradeDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.EmployeeGradeID = userControl.EmployeeGradeID;
                entity.EmployeeGradeMasterID = userControl.EmployeeGradeMasterID;
                entity.EmployeeGradeName = userControl.EmployeeGradeName;
                entity.SalaryTableNumber = userControl.SalaryTableNumber;
                entity.IsActive = userControl.IsActive;
                entity.ValidFrom = userControl.ValidFrom;
                entity.ValidTo = userControl.ValidTo;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeePositionGrade
        private void RefreshCommandItemEmployeePositionGrade(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPositionGrade.Columns[0].Visible = isVisible;
            grdPositionGrade.Columns[grdPositionGrade.Columns.Count - 1].Visible = isVisible;
            grdPositionGrade.Columns.FindByUniqueName("DocumentUpload").Visible = !isVisible;

            grdPositionGrade.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPositionGrade.Rebind();
        }

        private EmployeePositionGradeCollection EmployeePositionGrades
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeePositionGrade" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((EmployeePositionGradeCollection)(obj));
                    }
                }

                var coll = new EmployeePositionGradeCollection();
                var query = new EmployeePositionGradeQuery("a");
                var pg = new PositionGradeQuery("b");
                var dt = new AppStandardReferenceItemQuery("c");
                var el = new AppStandardReferenceItemQuery("d");
                var npg = new PositionGradeQuery("e");
                var ndt = new AppStandardReferenceItemQuery("f");
                var dp3 = new AppStandardReferenceItemQuery("g");

                var ss = new SalaryScaleQuery("ss");
                var nss = new SalaryScaleQuery("nss");

                query.Select
                    (
                       query.EmployeePositionGradeID,
                       query.PersonID,
                       query.SREducationLevel,
                       el.ItemName.As("refToAppStandardReferenceItem_EducationLevelName"),
                       query.ValidFrom,
                       query.PositionGradeID,
                       @"<b.PositionGradeName + ' (' + b.RankName + ')' AS 'refToPositionGrade_PositionGradeName'>",
                       //pg.PositionGradeName.As("refToPositionGrade_PositionGradeName"),
                       query.GradeYear,
                       query.PositionName,
                       query.SRDecreeType,
                       dt.ItemName.As("refToAppStandardReferenceItem_DecreeTypeName"),
                       query.DecreeNo,
                       query.NextProposalDate,
                       query.NextPositionGradeID,
                       @"<e.PositionGradeName + ' (' + e.RankName + ')' AS 'refToPositionGrade_NextPositionGradeName'>",
                       //npg.PositionGradeName.As("refToPositionGrade_NextPositionGradeName"),
                       query.NextGradeYear,
                       query.SRDecreeTypeNext,
                       query.NextPositionName,
                       ndt.ItemName.As("refToAppStandardReferenceItem_NextDecreeTypeName"),
                       query.SRDp3,
                       dp3.ItemName.As("refToAppStandardReferenceItem_Dp3ItemName"),
                       query.Notes,

                       query.SalaryScaleID,
                       query.NextSalaryScaleID,
                       ss.SalaryScaleName.As("refToSalaryScale_SalaryScaleName"),
                       nss.SalaryScaleName.As("refToSalaryScale_NextSalaryScaleName"),

                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.LeftJoin(el).On(query.SREducationLevel == el.ItemID &&
                                       el.StandardReferenceID == AppEnum.StandardReference.EducationLevel);
                query.InnerJoin(pg).On(query.PositionGradeID == pg.PositionGradeID);
                query.LeftJoin(dt).On(query.SRDecreeType == dt.ItemID &&
                                       dt.StandardReferenceID == AppEnum.StandardReference.DecreeType);
                query.LeftJoin(npg).On(query.NextPositionGradeID == npg.PositionGradeID);
                query.LeftJoin(ndt).On(query.SRDecreeTypeNext == ndt.ItemID &&
                                       ndt.StandardReferenceID == AppEnum.StandardReference.DecreeType);
                query.LeftJoin(dp3).On(query.SRDp3 == dp3.ItemID &&
                                       dp3.StandardReferenceID == AppEnum.StandardReference.Dp3);

                query.LeftJoin(ss).On(ss.SalaryScaleID == query.SalaryScaleID);
                query.LeftJoin(nss).On(nss.SalaryScaleID == query.NextSalaryScaleID);

                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.ValidFrom.Descending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collEmployeePositionGrade" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collEmployeePositionGrade" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulateEmployeePositionGradeGrid()
        {
            //Display Data Detail
            EmployeePositionGrades = null; //Reset Record Detail
            grdPositionGrade.DataSource = EmployeePositionGrades; //Requery
            grdPositionGrade.MasterTableView.IsItemInserted = false;
            grdPositionGrade.MasterTableView.ClearEditItems();
            grdPositionGrade.DataBind();
        }

        protected void grdPositionGrade_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPositionGrade.DataSource = EmployeePositionGrades;
        }

        protected void grdPositionGrade_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 employeePositionGradeID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeePositionGradeMetadata.ColumnNames.EmployeePositionGradeID]);
            EmployeePositionGrade entity = FindEmployeePositionGrade(employeePositionGradeID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPositionGrade_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 employeePositionGradeID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeePositionGradeMetadata.ColumnNames.EmployeePositionGradeID]);
            EmployeePositionGrade entity = FindEmployeePositionGrade(employeePositionGradeID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPositionGrade_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeePositionGrade entity = EmployeePositionGrades.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPositionGrade.Rebind();
        }
        private EmployeePositionGrade FindEmployeePositionGrade(Int32 employeePositionGradeID)
        {
            EmployeePositionGradeCollection coll = EmployeePositionGrades;
            EmployeePositionGrade retEntity = null;
            foreach (EmployeePositionGrade rec in coll)
            {
                if (rec.EmployeePositionGradeID.ToString().Equals(employeePositionGradeID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeePositionGrade entity, GridCommandEventArgs e)
        {
            var userControl = (EmployeePositionGradeDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.EmployeePositionGradeID = userControl.EmployeePositionGradeID;
                entity.SREducationLevel = userControl.SREducationLevel;
                entity.EducationLevelName = userControl.EducationLevelName;
                entity.ValidFrom = userControl.ValidFrom;
                entity.PositionGradeID = userControl.PositionGradeID;
                entity.PositionGradeName = userControl.PositionGradeName;
                entity.GradeYear = userControl.GradeYear;
                entity.PositionName = userControl.PositionName;
                entity.SRDecreeType = userControl.SRDecreeType;
                entity.DecreeTypeName = userControl.DecreeTypeName;
                entity.DecreeNo = userControl.DecreeNo;
                if (userControl.NextProposalDate == null)
                    entity.str.NextProposalDate = string.Empty;
                else
                    entity.NextProposalDate = userControl.NextProposalDate;
                entity.NextProposalDate = userControl.NextProposalDate;
                entity.NextPositionGradeID = userControl.NextPositionGradeID;
                entity.NextPositionGradeName = userControl.NextPositionGradeName;
                entity.NextGradeYear = userControl.NextGradeYear;
                entity.NextPositionName = userControl.NextPositionName;
                entity.SRDecreeTypeNext = userControl.SRDecreeTypeNext;
                entity.NextDecreeTypeName = userControl.DecreeTypeNameNext;
                entity.SRDp3 = userControl.SRDp3;
                entity.Dp3Name = userControl.Dp3Name;
                entity.Notes = userControl.Notes;

                entity.SalaryScaleID = userControl.SalaryScaleID;
                entity.SalaryScaleName = userControl.SalaryScaleName;
                entity.NextSalaryScaleID = userControl.NextSalaryScaleID;
                entity.NextSalaryScaleName = userControl.NextSalaryScaleName;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeeAchievement
        private void RefreshCommandItemEmployeeAchievement(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdEmployeeAchievement.Columns[0].Visible = isVisible;
            grdEmployeeAchievement.Columns[grdEmployeeAchievement.Columns.Count - 1].Visible = isVisible;

            grdEmployeeAchievement.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdEmployeeAchievement.Rebind();
        }

        private EmployeeAchievementCollection EmployeeAchievements
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeAchievement" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((EmployeeAchievementCollection)(obj));
                    }
                }

                EmployeeAchievementCollection coll = new EmployeeAchievementCollection();
                AwardQuery award = new AwardQuery("c");
                EmployeeAchievementQuery query = new EmployeeAchievementQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");

                query.Select
                    (
                       query.EmployeeAchievementID,
                       query.PersonID,
                       query.AwardID,
                       award.AwardName.As("refToAward_AwardName"),
                       award.AwardPrize.As("refToAward_AwardPrize"),
                       query.AwardDate,
                       query.Achievement,
                       query.FinancialValue,
                       query.Note,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.InnerJoin(award).On(query.AwardID == award.AwardID);

                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.EmployeeAchievementID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collEmployeeAchievement" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collEmployeeAchievement" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulateEmployeeAchievementGrid()
        {
            //Display Data Detail
            EmployeeAchievements = null; //Reset Record Detail
            grdEmployeeAchievement.DataSource = EmployeeAchievements; //Requery
            grdEmployeeAchievement.MasterTableView.IsItemInserted = false;
            grdEmployeeAchievement.MasterTableView.ClearEditItems();
            grdEmployeeAchievement.DataBind();
        }

        protected void grdEmployeeAchievement_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeAchievement.DataSource = EmployeeAchievements;
        }

        protected void grdEmployeeAchievement_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 employeeAchievementID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeAchievementMetadata.ColumnNames.EmployeeAchievementID]);
            EmployeeAchievement entity = FindEmployeeAchievement(employeeAchievementID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdEmployeeAchievement_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 employeeAchievementID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeAchievementMetadata.ColumnNames.EmployeeAchievementID]);
            EmployeeAchievement entity = FindEmployeeAchievement(employeeAchievementID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdEmployeeAchievement_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeAchievement entity = EmployeeAchievements.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdEmployeeAchievement.Rebind();
        }
        private EmployeeAchievement FindEmployeeAchievement(Int32 employeeAchievementID)
        {
            EmployeeAchievementCollection coll = EmployeeAchievements;
            EmployeeAchievement retEntity = null;
            foreach (EmployeeAchievement rec in coll)
            {
                if (rec.EmployeeAchievementID.ToString().Equals(employeeAchievementID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeeAchievement entity, GridCommandEventArgs e)
        {
            EmployeeAchievementDetail userControl = (EmployeeAchievementDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.EmployeeAchievementID = userControl.EmployeeAchievementID;
                entity.AwardID = userControl.AwardID;
                entity.AwardName = userControl.AwardName;
                entity.AwardDate = userControl.AwardDate;
                entity.Achievement = userControl.Achievement;
                entity.FinancialValue = userControl.FinancialValue;
                entity.Note = userControl.Note;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeeDisciplinary
        private void RefreshCommandItemEmployeeDisciplinary(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdEmployeeDisciplinary.Columns[0].Visible = isVisible;
            grdEmployeeDisciplinary.Columns[grdEmployeeDisciplinary.Columns.Count - 1].Visible = isVisible;
            grdEmployeeDisciplinary.Columns.FindByUniqueName("DocumentUpload").Visible = !isVisible;

            grdEmployeeDisciplinary.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdEmployeeDisciplinary.Rebind();
        }

        private EmployeeDisciplinaryCollection EmployeeDisciplinarys
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeDisciplinary" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((EmployeeDisciplinaryCollection)(obj));
                    }
                }

                EmployeeDisciplinaryCollection coll = new EmployeeDisciplinaryCollection();
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
                       warning.ItemName.As("refToWarningLevelName_EmployeeDisciplinary"),
                       query.IncidentDate,
                       query.DateIssue,
                       query.Violation,
                       query.EffectViolation,
                       query.AdviceGiven,
                       query.SanctionGiven,
                       query.SRViolationDegree,
                       vDegree.ItemName.As("refToViolationDegreeName_EmployeeDisciplinary"),
                       query.SRViolationType,
                       vType.ItemName.As("refToViolationTypeName_EmployeeDisciplinary"),
                       query.Note,
                       query.EffectiveDate,
                       query.ValidUntil,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime,
                       query.CreatedDateTime,
                       query.CreatedByUserID
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
                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.EmployeeDisciplinaryID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collEmployeeDisciplinary" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collEmployeeDisciplinary" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulateEmployeeDisciplinaryGrid()
        {
            //Display Data Detail
            EmployeeDisciplinarys = null; //Reset Record Detail
            grdEmployeeDisciplinary.DataSource = EmployeeDisciplinarys; //Requery
            grdEmployeeDisciplinary.MasterTableView.IsItemInserted = false;
            grdEmployeeDisciplinary.MasterTableView.ClearEditItems();
            grdEmployeeDisciplinary.DataBind();
        }

        protected void grdEmployeeDisciplinary_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeDisciplinary.DataSource = EmployeeDisciplinarys;
        }

        protected void grdEmployeeDisciplinary_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 employeeDisciplinaryID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeDisciplinaryMetadata.ColumnNames.EmployeeDisciplinaryID]);
            EmployeeDisciplinary entity = FindEmployeeDisciplinary(employeeDisciplinaryID);
            if (entity != null)
                SetEntityValue(entity, e, false);
        }

        protected void grdEmployeeDisciplinary_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 employeeDisciplinaryID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeDisciplinaryMetadata.ColumnNames.EmployeeDisciplinaryID]);
            EmployeeDisciplinary entity = FindEmployeeDisciplinary(employeeDisciplinaryID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdEmployeeDisciplinary_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeDisciplinary entity = EmployeeDisciplinarys.AddNew();
            SetEntityValue(entity, e, true);

            //Stay in insert mode
            e.Canceled = true;
            grdEmployeeDisciplinary.Rebind();
        }
        private EmployeeDisciplinary FindEmployeeDisciplinary(Int32 employeeDisciplinaryID)
        {
            EmployeeDisciplinaryCollection coll = EmployeeDisciplinarys;
            EmployeeDisciplinary retEntity = null;
            foreach (EmployeeDisciplinary rec in coll)
            {
                if (rec.EmployeeDisciplinaryID.ToString().Equals(employeeDisciplinaryID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeeDisciplinary entity, GridCommandEventArgs e, bool isNew)
        {
            EmployeeDisciplinaryDetail userControl = (EmployeeDisciplinaryDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.EmployeeDisciplinaryID = userControl.EmployeeDisciplinaryID;
                entity.SRWarningLevel = userControl.SRWarningLevel;
                entity.WarningLevelName = userControl.WarningLevelName;
                entity.IncidentDate = userControl.IncidentDate;
                entity.DateIssue = userControl.DateIssue;
                entity.Violation = userControl.Violation;
                entity.EffectViolation = userControl.EffectViolation;
                entity.SRViolationDegree = userControl.SRViolationDegree;
                entity.ViolationDegreeName = userControl.ViolationDegreeName;
                entity.SRViolationType = userControl.SRViolationType;
                entity.ViolationTypeName = userControl.ViolationTypeName;
                entity.Note = userControl.Note;
                entity.EffectiveDate = userControl.EffectiveDate;
                entity.ValidUntil = userControl.ValidUntil;
                if (isNew)
                {
                    entity.CreatedDateTime = DateTime.Now;
                    entity.CreatedByUserID = AppSession.UserLogin.UserID;
                }
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeeTrainingHistory
        private void RefreshCommandItemEmployeeTrainingHistory(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdEmployeeTrainingHistory.Columns[0].Visible = isVisible;
            grdEmployeeTrainingHistory.Columns[grdEmployeeTrainingHistory.Columns.Count - 1].Visible = isVisible;
            grdEmployeeTrainingHistory.Columns.FindByUniqueName("TrainingEvaluation").Visible = !isVisible;
            grdEmployeeTrainingHistory.Columns.FindByUniqueName("DocumentUpload").Visible = !isVisible;

            grdEmployeeTrainingHistory.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdEmployeeTrainingHistory.Rebind();
        }

        private EmployeeTrainingHistoryCollection EmployeeTrainingHistorys
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeTrainingHistory" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((EmployeeTrainingHistoryCollection)(obj));
                    }
                }

                EmployeeTrainingHistoryCollection coll = new EmployeeTrainingHistoryCollection();

                EmployeeTrainingHistoryQuery query = new EmployeeTrainingHistoryQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");
                var train = new EmployeeTrainingQuery("c");
                var ab = new AppStandardReferenceItemQuery("ab");
                var ac = new AppStandardReferenceItemQuery("ac");
                var ad = new AppStandardReferenceItemQuery("ad");
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
                       query.TotalHour.Coalesce("c.TotalHour"),
                       query.CreditPoint.Coalesce("c.CreditPoint"),
                       query.PlanningCosts.Coalesce("c.PlanningCosts").Coalesce("0"),
                       query.Fee.Coalesce("c.TrainingFee"),
                       query.SponsorFee,
                       query.IsInHouseTraining.Coalesce("c.IsInHouseTraining"),
                       query.IsScheduledTraining.Coalesce("c.IsScheduledTraining"),
                       query.IsAttending,
                       query.Note,

                       query.SRActivityType,
                       ab.ItemName.As("refToStdRef_ActivityTypeName"),
                       query.SRActivitySubType,
                       ae.ItemName.As("refToStdRef_ActivitySubTypeName"),
                       query.CertificateValidityPeriod,
                       query.IsCommitmentToWork,
                       query.LengthOfService,
                       query.StartServiceDate,
                       query.EndServiceDate,
                       query.SRTrainingFinancingSources,
                       ac.ItemName.As("refToStdRef_TrainingFinancingSourcesName"),
                       query.EvaluationDate,
                       query.EvaluationNote,
                       query.EvaluationNoteDateTime,
                       query.SupervisorEvaluationNote,
                       query.SupervisorEvaluationDateTime,
                       query.SupervisorEvaluationNoteByUserID,
                       query.EvaluationScore,
                       query.Recommendation,

                       query.DurationHour,
                       query.DurationMinutes,
                       query.SREmployeeTrainingPointType,
                       query.SREmployeeTrainingDateSeparator,
                       query.SREmployeeTrainingRole,
                       ad.ItemName.As("refToStdRef_EmployeeTrainingRole"),

                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.LeftJoin(train).On(query.EmployeeTrainingID == train.EmployeeTrainingID);
                query.LeftJoin(ab).On(ab.StandardReferenceID == "ActivityType" && ab.ItemID == query.SRActivityType);
                query.LeftJoin(ac).On(ac.StandardReferenceID == "TrainingFinancingSources" && ac.ItemID == query.SRTrainingFinancingSources);
                query.LeftJoin(ad).On(ad.StandardReferenceID == "EmployeeTrainingRole" && ad.ItemID == query.SREmployeeTrainingRole);
                query.LeftJoin(ae).On(ae.StandardReferenceID == "ActivitySubType" && ae.ItemID == query.SRActivitySubType);

                query.Where(query.PersonID == txtPersonID.Text, query.Or(train.IsProposal.IsNull(), train.IsProposal == false));
                //query.OrderBy(query.EmployeeTrainingHistoryID.Ascending);
                query.OrderBy(query.StartDate.Descending);
                coll.Load(query);

                Session["collEmployeeTrainingHistory" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collEmployeeTrainingHistory" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulateEmployeeTrainingHistoryGrid()
        {
            //Display Data Detail
            EmployeeTrainingHistorys = null; //Reset Record Detail
            grdEmployeeTrainingHistory.DataSource = EmployeeTrainingHistorys; //Requery
            grdEmployeeTrainingHistory.MasterTableView.IsItemInserted = false;
            grdEmployeeTrainingHistory.MasterTableView.ClearEditItems();
            grdEmployeeTrainingHistory.DataBind();
        }

        protected void grdEmployeeTrainingHistory_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeTrainingHistory.DataSource = EmployeeTrainingHistorys;
        }

        protected void grdEmployeeTrainingHistory_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 employeeTrainingHistoryID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeTrainingHistoryMetadata.ColumnNames.EmployeeTrainingHistoryID]);
            EmployeeTrainingHistory entity = FindEmployeeTrainingHistory(employeeTrainingHistoryID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdEmployeeTrainingHistory_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 employeeTrainingHistoryID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeTrainingHistoryMetadata.ColumnNames.EmployeeTrainingHistoryID]);
            EmployeeTrainingHistory entity = FindEmployeeTrainingHistory(employeeTrainingHistoryID);
            if (entity != null && string.IsNullOrEmpty(entity.SupervisorEvaluationNoteByUserID))
                entity.MarkAsDeleted();
        }

        protected void grdEmployeeTrainingHistory_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeTrainingHistory entity = EmployeeTrainingHistorys.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdEmployeeTrainingHistory.Rebind();
        }
        private EmployeeTrainingHistory FindEmployeeTrainingHistory(Int32 employeeTrainingHistoryID)
        {
            EmployeeTrainingHistoryCollection coll = EmployeeTrainingHistorys;
            EmployeeTrainingHistory retEntity = null;
            foreach (EmployeeTrainingHistory rec in coll)
            {
                if (rec.EmployeeTrainingHistoryID.ToString().Equals(employeeTrainingHistoryID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeeTrainingHistory entity, GridCommandEventArgs e)
        {
            EmployeeTrainingHistoryDetail userControl = (EmployeeTrainingHistoryDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.EmployeeTrainingHistoryID = userControl.EmployeeTrainingHistoryID;
                entity.IsAttending = userControl.IsAttending;
                entity.EventName = userControl.EventName;
                entity.TrainingLocation = userControl.TrainingLocation;
                entity.TrainingInstitution = userControl.TrainingInstitution;
                entity.StartDate = userControl.StartDate;
                entity.EndDate = userControl.EndDate;
                entity.TotalHour = userControl.TotalHour;
                entity.CreditPoint = userControl.CreditPoint;
                entity.PlanningCosts = userControl.PlanningCosts;
                entity.Fee = userControl.Fee;
                entity.SponsorFee = userControl.SponsorFee;
                entity.Note = userControl.Note;
                entity.IsInHouseTraining = userControl.IsInHouseTraining;
                entity.IsScheduledTraining = userControl.IsScheduledTraining;
                entity.IsProposal = false;

                entity.SRActivityType = userControl.SRActivityType;
                entity.SRActivitySubType = userControl.SRActivitySubType;
                entity.ActivitySubTypeName = userControl.ActivitySubTypeName;
                entity.ActivityTypeName = userControl.ActivityTypeName;
                if (userControl.CertificateValidityPeriod == null)
                    entity.str.CertificateValidityPeriod = string.Empty;
                else
                    entity.CertificateValidityPeriod = userControl.CertificateValidityPeriod;

                entity.IsCommitmentToWork = userControl.IsCommitmentToWork;
                entity.LengthOfService = userControl.LengthOfService;

                if (userControl.StartServiceDate == null)
                    entity.str.StartServiceDate = string.Empty;
                else
                    entity.StartServiceDate = userControl.StartServiceDate;

                if (userControl.EndServiceDate == null)
                    entity.str.EndServiceDate = string.Empty;
                else
                    entity.EndServiceDate = userControl.EndServiceDate;

                entity.SRTrainingFinancingSources = userControl.SRTrainingFinancingSources;
                entity.TrainingFinancingSourcesName = userControl.TrainingFinancingSourcesName;

                if (userControl.EvaluationDate == null)
                    entity.str.EvaluationDate = string.Empty;
                else
                    entity.EvaluationDate = userControl.EvaluationDate;

                entity.SREmployeeTrainingPointType = userControl.SREmployeeTrainingPointType;
                entity.DurationHour = userControl.DurationHour;
                entity.DurationMinutes = userControl.DurationMinutes;
                entity.SREmployeeTrainingDateSeparator = userControl.SREmployeeTrainingDateSeparator;
                entity.SREmployeeTrainingRole = userControl.SREmployeeTrainingRole;
                entity.EmployeeTrainingRoleName = userControl.EmployeeTrainingRoleName;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeeOrientation
        private void RefreshCommandItemEmployeeOrientation(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdEmployeeOrientation.Columns[0].Visible = isVisible;
            grdEmployeeOrientation.Columns[grdEmployeeOrientation.Columns.Count - 1].Visible = isVisible;
            grdEmployeeTrainingHistory.Columns.FindByUniqueName("DocumentUpload").Visible = !isVisible;

            grdEmployeeOrientation.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdEmployeeOrientation.Rebind();
        }

        private EmployeeOrientationCollection EmployeeOrientations
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeOrientation" + Request.UserHostName + hdnPageId.Value];
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

                query.Where(query.PersonID == txtPersonID.Text);
                query.OrderBy(query.EmployeeOrientationID.Ascending);
                coll.Load(query);

                Session["collEmployeeOrientation" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collEmployeeOrientation" + Request.UserHostName + hdnPageId.Value] = value; }
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
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdEmployeeOrientation_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeOrientationMetadata.ColumnNames.EmployeeOrientationID]);
            EmployeeOrientation entity = FindEmployeeOrientation(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdEmployeeOrientation_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeOrientation entity = EmployeeOrientations.AddNew();
            SetEntityValue(entity, e);

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

        #endregion

        #region Record Detail Method Function EmployeeEducation
        private void RefreshCommandItemEmployeeEducation(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdEmployeeEducation.Columns[0].Visible = isVisible;
            grdEmployeeEducation.Columns[grdEmployeeEducation.Columns.Count - 1].Visible = isVisible;

            grdEmployeeEducation.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdEmployeeEducation.Rebind();
        }

        private EmployeeEducationCollection EmployeeEducations
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeEducation" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((EmployeeEducationCollection)(obj));
                    }
                }

                EmployeeEducationCollection coll = new EmployeeEducationCollection();

                EmployeeEducationQuery query = new EmployeeEducationQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");
                var ab = new AppStandardReferenceItemQuery("ab");
                var ac = new AppStandardReferenceItemQuery("ac");
                var ad = new AppStandardReferenceItemQuery("ad");

                query.Select
                    (
                       query.EmployeeEducationID,
                       query.PersonID,
                       query.SREducationStatus,
                       ab.ItemName.As("refToStdRef_EducationStatusName"),
                       query.SREducationFinancingSources,
                       ac.ItemName.As("refToStdRef_EducationFinancingSourcesName"),
                       query.IsTuitionAssistance,
                       query.AssistanceAmount,
                       query.InstitutionName,
                       query.StudyProgram,
                       query.StartYearPeriod,
                       query.EndYearPeriod,
                       query.SRStudyPeriodStatus,
                       ad.ItemName.As("refToStdRef_StudyPeriodStatusName"),
                       query.IsCommitmentToWork,
                       query.DurationOfService,
                       query.StartServiceDate,
                       query.EndServiceDate,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.LeftJoin(ab).On(ab.StandardReferenceID == "EducationStatus" && ab.ItemID == query.SREducationStatus);
                query.LeftJoin(ac).On(ac.StandardReferenceID == "EducationFinancingSources" && ac.ItemID == query.SREducationFinancingSources);
                query.LeftJoin(ad).On(ad.StandardReferenceID == "StudyPeriodStatus" && ad.ItemID == query.SRStudyPeriodStatus);

                query.Where(query.PersonID == txtPersonID.Text);
                query.OrderBy(query.EmployeeEducationID.Ascending);
                coll.Load(query);

                Session["collEmployeeEducation" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collEmployeeEducation" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulateEmployeeEducationGrid()
        {
            //Display Data Detail
            EmployeeEducations = null; //Reset Record Detail
            grdEmployeeEducation.DataSource = EmployeeEducations; //Requery
            grdEmployeeEducation.MasterTableView.IsItemInserted = false;
            grdEmployeeEducation.MasterTableView.ClearEditItems();
            grdEmployeeEducation.DataBind();
        }

        protected void grdEmployeeEducation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeEducation.DataSource = EmployeeEducations;
        }

        protected void grdEmployeeEducation_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeEducationMetadata.ColumnNames.EmployeeEducationID]);
            EmployeeEducation entity = FindEmployeeEducation(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdEmployeeEducation_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeEducationMetadata.ColumnNames.EmployeeEducationID]);
            EmployeeEducation entity = FindEmployeeEducation(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdEmployeeEducation_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeEducation entity = EmployeeEducations.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdEmployeeEducation.Rebind();
        }
        private EmployeeEducation FindEmployeeEducation(Int32 id)
        {
            EmployeeEducationCollection coll = EmployeeEducations;
            EmployeeEducation retEntity = null;
            foreach (EmployeeEducation rec in coll)
            {
                if (rec.EmployeeEducationID.ToString().Equals(id.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeeEducation entity, GridCommandEventArgs e)
        {
            EmployeeEducationDetail userControl = (EmployeeEducationDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.EmployeeEducationID = userControl.EmployeeEducationID;
                entity.SREducationStatus = userControl.SREducationStatus;
                entity.EducationStatusName = userControl.EducationStatusName;
                entity.SREducationFinancingSources = userControl.SREducationFinancingSources;
                entity.EducationFinancingSourcesName = userControl.EducationFinancingSourcesName;
                entity.IsTuitionAssistance = userControl.IsTuitionAssistance;
                entity.AssistanceAmount = userControl.AssistanceAmount;
                entity.InstitutionName = userControl.InstitutionName;
                entity.StudyProgram = userControl.StudyProgram;
                entity.StartYearPeriod = userControl.StartYearPeriod;
                entity.EndYearPeriod = userControl.EndYearPeriod;
                entity.SRStudyPeriodStatus = userControl.SRStudyPeriodStatus;
                entity.StudyPeriodStatusName = userControl.StudyPeriodStatusName;
                entity.IsCommitmentToWork = userControl.IsCommitmentToWork;
                entity.DurationOfService = userControl.DurationOfService;
                if (userControl.StartServiceDate == null)
                    entity.str.StartServiceDate = string.Empty;
                else
                    entity.StartServiceDate = userControl.StartServiceDate;

                if (userControl.EndServiceDate == null)
                    entity.str.EndServiceDate = string.Empty;
                else
                    entity.EndServiceDate = userControl.EndServiceDate;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeeAppraisalQuestion
        private void RefreshCommandItemEmployeeAppraisalQuestion(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
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
                    object obj = Session["collEmployeeAppraisalQuestion" + Request.UserHostName + hdnPageId.Value];
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

                query.Where(query.PersonID == txtPersonID.Text);
                query.OrderBy(aq.QuestionerNo.Ascending);
                coll.Load(query);

                Session["collEmployeeAppraisalQuestion" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collEmployeeAppraisalQuestion" + Request.UserHostName + hdnPageId.Value] = value; }
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
                SetEntityValue(entity, e);
        }

        protected void grdAppraisalQuestion_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeAppraisalQuestionMetadata.ColumnNames.EmployeeAppraisalQuestionerID]);
            EmployeeAppraisalQuestion entity = FindAppraisalQuestion(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdAppraisalQuestion_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeAppraisalQuestion entity = EmployeeAppraisalQuestions.AddNew();
            SetEntityValue(entity, e);

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

        #region Record Detail Method Function EmployeePerformanceAppraisal
        private void RefreshCommandItemEmployeePerformanceAppraisal(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPerformanceAppraisal.Columns[0].Visible = isVisible;
            grdPerformanceAppraisal.Columns[grdPerformanceAppraisal.Columns.Count - 1].Visible = isVisible;

            grdPerformanceAppraisal.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPerformanceAppraisal.Rebind();
        }

        private EmployeePerformanceAppraisalCollection EmployeePerformanceAppraisals
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeePerformanceAppraisal" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((EmployeePerformanceAppraisalCollection)(obj));
                    }
                }

                var coll = new EmployeePerformanceAppraisalCollection();

                var query = new EmployeePerformanceAppraisalQuery("a");
                var quarter = new AppStandardReferenceItemQuery("b");

                query.Select
                    (
                       query,
                       quarter.ItemName.As("refToAppStd_QuarterPeriod")
                    );

                query.LeftJoin(quarter).On(quarter.StandardReferenceID == "QuarterPeriod" && quarter.ItemID == query.SRQuarterPeriod);

                query.Where(query.PersonID == txtPersonID.Text);
                query.OrderBy(query.YearPeriod.Ascending, query.SRQuarterPeriod.Ascending);
                coll.Load(query);

                Session["collEmployeePerformanceAppraisal" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collEmployeePerformanceAppraisal" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulateEmployeePerformanceAppraisalGrid()
        {
            //Display Data Detail
            EmployeePerformanceAppraisals = null; //Reset Record Detail
            grdPerformanceAppraisal.DataSource = EmployeePerformanceAppraisals; //Requery
            grdPerformanceAppraisal.MasterTableView.IsItemInserted = false;
            grdPerformanceAppraisal.MasterTableView.ClearEditItems();
            grdPerformanceAppraisal.DataBind();
        }

        protected void grdPerformanceAppraisal_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPerformanceAppraisal.DataSource = EmployeePerformanceAppraisals;
        }

        protected void grdPerformanceAppraisal_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeePerformanceAppraisalMetadata.ColumnNames.PerformanceAppraisalID]);
            EmployeePerformanceAppraisal entity = FindPerformanceAppraisal(id);
            if (entity != null && entity.ParticipantItemID != -1)
                SetEntityValue(entity, e);
        }

        protected void grdPerformanceAppraisal_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeePerformanceAppraisalMetadata.ColumnNames.PerformanceAppraisalID]);
            EmployeePerformanceAppraisal entity = FindPerformanceAppraisal(id);
            if (entity != null && entity.ParticipantItemID != -1)
                entity.MarkAsDeleted();
        }

        protected void grdPerformanceAppraisal_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeePerformanceAppraisal entity = EmployeePerformanceAppraisals.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPerformanceAppraisal.Rebind();
        }
        private EmployeePerformanceAppraisal FindPerformanceAppraisal(Int32 Id)
        {
            var coll = EmployeePerformanceAppraisals;
            EmployeePerformanceAppraisal retEntity = null;
            foreach (EmployeePerformanceAppraisal rec in coll)
            {
                if (rec.PerformanceAppraisalID.ToString().Equals(Id.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeePerformanceAppraisal entity, GridCommandEventArgs e)
        {
            var userControl = (EmployeePerformanceAppraisalDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.PerformanceAppraisalID = userControl.PerformanceAppraisalID;
                entity.ParticipantItemID = userControl.ParticipantItemID;
                entity.YearPeriod = userControl.YearPeriod;
                entity.SRQuarterPeriod = userControl.SRQuarterPeriod;
                entity.QuarterPeriodName = userControl.QuarterPeriodName;
                entity.Score = userControl.Score;
                entity.ScoreText = userControl.ScoreText;
                entity.Notes = userControl.Notes;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeeMiscellaneousBenefit
        private void RefreshCommandItemEmployeeMiscellaneousBenefit(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdEmployeeMiscellaneousBenefit.Columns[0].Visible = isVisible;
            grdEmployeeMiscellaneousBenefit.Columns[grdEmployeeMiscellaneousBenefit.Columns.Count - 1].Visible = isVisible;

            grdEmployeeMiscellaneousBenefit.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdEmployeeMiscellaneousBenefit.Rebind();
        }

        private EmployeeMiscellaneousBenefitCollection EmployeeMiscellaneousBenefits
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeMiscellaneousBenefit" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((EmployeeMiscellaneousBenefitCollection)(obj));
                    }
                }

                EmployeeMiscellaneousBenefitCollection coll = new EmployeeMiscellaneousBenefitCollection();
                AppStandardReferenceItemQuery benefit = new AppStandardReferenceItemQuery("e");
                EmployeeMiscellaneousBenefitQuery query = new EmployeeMiscellaneousBenefitQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");

                query.Select
                    (
                       query.EmployeeMiscellaneousBenefitID,
                       query.PersonID,
                       query.SRMiscellaneousBenefit,
                       benefit.ItemName.As("refToMiscellaneousBenefit_EmployeeMiscellaneousBenefit"),
                       query.ValidFrom,
                       query.ValidTo,
                       query.Note,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.LeftJoin(benefit).On
                        (
                            query.SRMiscellaneousBenefit == benefit.ItemID &
                            benefit.StandardReferenceID == "MiscellaneousBenefit"
                        );

                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.EmployeeMiscellaneousBenefitID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collEmployeeMiscellaneousBenefit" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collEmployeeMiscellaneousBenefit" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulateEmployeeMiscellaneousBenefitGrid()
        {
            //Display Data Detail
            EmployeeMiscellaneousBenefits = null; //Reset Record Detail
            grdEmployeeMiscellaneousBenefit.DataSource = EmployeeMiscellaneousBenefits; //Requery
            grdEmployeeMiscellaneousBenefit.MasterTableView.IsItemInserted = false;
            grdEmployeeMiscellaneousBenefit.MasterTableView.ClearEditItems();
            grdEmployeeMiscellaneousBenefit.DataBind();
        }

        protected void grdEmployeeMiscellaneousBenefit_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeMiscellaneousBenefit.DataSource = EmployeeMiscellaneousBenefits;
        }

        protected void grdEmployeeMiscellaneousBenefit_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 employeeMiscellaneousBenefitID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeMiscellaneousBenefitMetadata.ColumnNames.EmployeeMiscellaneousBenefitID]);
            EmployeeMiscellaneousBenefit entity = FindEmployeeMiscellaneousBenefit(employeeMiscellaneousBenefitID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdEmployeeMiscellaneousBenefit_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 employeeMiscellaneousBenefitID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeMiscellaneousBenefitMetadata.ColumnNames.EmployeeMiscellaneousBenefitID]);
            EmployeeMiscellaneousBenefit entity = FindEmployeeMiscellaneousBenefit(employeeMiscellaneousBenefitID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdEmployeeMiscellaneousBenefit_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeMiscellaneousBenefit entity = EmployeeMiscellaneousBenefits.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdEmployeeMiscellaneousBenefit.Rebind();
        }
        private EmployeeMiscellaneousBenefit FindEmployeeMiscellaneousBenefit(Int32 employeeMiscellaneousBenefitID)
        {
            EmployeeMiscellaneousBenefitCollection coll = EmployeeMiscellaneousBenefits;
            EmployeeMiscellaneousBenefit retEntity = null;
            foreach (EmployeeMiscellaneousBenefit rec in coll)
            {
                if (rec.EmployeeMiscellaneousBenefitID.ToString().Equals(employeeMiscellaneousBenefitID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeeMiscellaneousBenefit entity, GridCommandEventArgs e)
        {
            EmployeeMiscellaneousBenefitDetail userControl = (EmployeeMiscellaneousBenefitDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.EmployeeMiscellaneousBenefitID = userControl.EmployeeMiscellaneousBenefitID;
                entity.SRMiscellaneousBenefit = userControl.SRMiscellaneousBenefit;
                entity.MiscellaneousBenefitName = userControl.MiscellaneousBenefitName;
                entity.ValidFrom = userControl.ValidFrom;
                entity.ValidTo = userControl.ValidTo;
                entity.Note = userControl.Note;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeeLanguageProficiency
        private void RefreshCommandItemEmployeeLanguageProficiency(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdEmployeeLanguageProficiency.Columns[0].Visible = isVisible;
            grdEmployeeLanguageProficiency.Columns[grdEmployeeLanguageProficiency.Columns.Count - 1].Visible = isVisible;
            grdEmployeeLanguageProficiency.Columns.FindByUniqueName("DocumentUpload").Visible = !isVisible;

            grdEmployeeLanguageProficiency.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdEmployeeLanguageProficiency.Rebind();
        }

        private EmployeeLanguageProficiencyCollection EmployeeLanguageProficiencys
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeLanguageProficiency" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((EmployeeLanguageProficiencyCollection)(obj));
                    }
                }

                EmployeeLanguageProficiencyCollection coll = new EmployeeLanguageProficiencyCollection();
                AppStandardReferenceItemQuery translation = new AppStandardReferenceItemQuery("e");
                AppStandardReferenceItemQuery conversation = new AppStandardReferenceItemQuery("d");
                AppStandardReferenceItemQuery language = new AppStandardReferenceItemQuery("c");
                EmployeeLanguageProficiencyQuery query = new EmployeeLanguageProficiencyQuery("b");
                PersonalInfoQuery personal = new PersonalInfoQuery("a");

                query.Select
                    (
                       query.EmployeeLanguageProficiencyID,
                       query.PersonID,
                       query.EvaluationDate,
                       query.SRLanguage,
                       language.ItemName.As("refToLanguage"),
                       query.SRConversation,
                       conversation.ItemName.As("refToConversation"),
                       query.SRTranslation,
                       conversation.ItemName.As("refToTranslation"),
                       query.Notes,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.LeftJoin(language).On
                        (
                            query.SRLanguage == language.ItemID &
                            language.StandardReferenceID == "LanguageProficiency"
                        );
                query.LeftJoin(conversation).On
                        (
                            query.SRConversation == conversation.ItemID &
                            conversation.StandardReferenceID == "LanguageCapable"
                        );
                query.LeftJoin(translation).On
                        (
                            query.SRTranslation == translation.ItemID &
                            translation.StandardReferenceID == "LanguageCapable"
                        );

                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.EvaluationDate.Ascending, query.EmployeeLanguageProficiencyID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collEmployeeLanguageProficiency" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collEmployeeLanguageProficiency" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulateEmployeeLanguageProficiencyGrid()
        {
            //Display Data Detail
            EmployeeLanguageProficiencys = null; //Reset Record Detail
            grdEmployeeLanguageProficiency.DataSource = EmployeeLanguageProficiencys; //Requery
            grdEmployeeLanguageProficiency.MasterTableView.IsItemInserted = false;
            grdEmployeeLanguageProficiency.MasterTableView.ClearEditItems();
            grdEmployeeLanguageProficiency.DataBind();
        }

        protected void grdEmployeeLanguageProficiency_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeLanguageProficiency.DataSource = EmployeeLanguageProficiencys;
        }

        protected void grdEmployeeLanguageProficiency_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 employeeLanguageProficiencyID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeLanguageProficiencyMetadata.ColumnNames.EmployeeLanguageProficiencyID]);
            EmployeeLanguageProficiency entity = FindEmployeeLanguageProficiency(employeeLanguageProficiencyID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdEmployeeLanguageProficiency_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 employeeLanguageProficiencyID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeLanguageProficiencyMetadata.ColumnNames.EmployeeLanguageProficiencyID]);
            EmployeeLanguageProficiency entity = FindEmployeeLanguageProficiency(employeeLanguageProficiencyID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdEmployeeLanguageProficiency_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeLanguageProficiency entity = EmployeeLanguageProficiencys.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdEmployeeLanguageProficiency.Rebind();
        }
        private EmployeeLanguageProficiency FindEmployeeLanguageProficiency(Int32 employeeLanguageProficiencyID)
        {
            EmployeeLanguageProficiencyCollection coll = EmployeeLanguageProficiencys;
            EmployeeLanguageProficiency retEntity = null;
            foreach (EmployeeLanguageProficiency rec in coll)
            {
                if (rec.EmployeeLanguageProficiencyID.ToString().Equals(employeeLanguageProficiencyID.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeeLanguageProficiency entity, GridCommandEventArgs e)
        {
            EmployeeLanguageProficiencyDetail userControl = (EmployeeLanguageProficiencyDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.EmployeeLanguageProficiencyID = userControl.EmployeeLanguageProficiencyID;
                entity.EvaluationDate = userControl.EvaluationDate;
                entity.SRLanguage = userControl.SRLanguage;
                entity.LanguageName = userControl.LanguageName;
                entity.SRConversation = userControl.SRConversation;
                entity.ConversationName = userControl.ConversationName;
                entity.SRTranslation = userControl.SRTranslation;
                entity.TranslationName = userControl.TranslationName;
                entity.Notes = userControl.Notes;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeeRL4
        private void RefreshCommandItemEmployeeRL4(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdRL4.Columns[0].Visible = isVisible;
            grdRL4.Columns[grdRL4.Columns.Count - 1].Visible = isVisible;

            grdRL4.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdRL4.Rebind();
        }

        private EmployeeRL4Collection EmployeeRL4s
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeRL4" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((EmployeeRL4Collection)(obj));
                    }
                }

                var coll = new EmployeeRL4Collection();
                var query = new EmployeeRL4Query("a");
                var status = new AppStandardReferenceItemQuery("b");
                var type = new AppStandardReferenceItemQuery("c");
                var prof = new AppStandardReferenceItemQuery("d");
                var edulvl = new AppStandardReferenceItemQuery("e");
                var edumjr = new AppStandardReferenceItemQuery("f");

                var rl4edu = new RL4EducationQuery("g");
                var eduLevel = new AppStandardReferenceItemQuery("h");
                var medis = new AppStandardReferenceItemQuery("i");
                var work = new CompanyFieldOfWorkProfileQuery("j");
                var edu = new CompanyEducationProfileQuery("k");

                query.Select
                    (
                       query,
                       status.ItemName.As("refToAppStdItem_RL4Status"),
                       type.ItemName.As("refToAppStdItem_RL4Type"),
                       prof.ItemName.As("refToAppStdItem_RL4ProfessionType"),
                       edulvl.ItemName.As("refToAppStdItem_RL4EducationLevel"),
                       edumjr.ItemName.As("refToAppStdItem_RL4EducationMajor"),

                       edu.CompanyEducationProfileName.As("refToCompanyEducationProfileName"),
                       work.CompanyFieldOfWorkProfileName.As("refToCompanyFieldOfWorkProfileName"),
                       medis.ItemName.As("refToRL4MedisTypeName"),
                       eduLevel.ItemName.As("refToRL4EducationName")
                    );

                query.LeftJoin(status).On(status.StandardReferenceID == AppEnum.StandardReference.RL4Status.ToString() && status.ItemID == query.SRRL4Status);
                query.LeftJoin(type).On(type.StandardReferenceID == AppEnum.StandardReference.RL4Type.ToString() && type.ItemID == query.SRRL4Type);
                query.LeftJoin(prof).On(prof.StandardReferenceID == AppEnum.StandardReference.RL4ProfessionType.ToString() && prof.ItemID == query.SRRL4ProfessionType);
                query.LeftJoin(edulvl).On(edulvl.StandardReferenceID == AppEnum.StandardReference.RL4EducationLevel.ToString() && edulvl.ItemID == query.SRRL4EducationLevel);
                query.LeftJoin(edumjr).On(edumjr.StandardReferenceID == AppEnum.StandardReference.RL4EducationMajor.ToString() && edumjr.ItemID == query.SRRL4EducationMajor);

                query.LeftJoin(edu).On(query.CompanyEducationProfileID == edu.CompanyEducationProfileID);
                query.LeftJoin(work).On(query.CompanyFieldOfWorkProfileID == work.CompanyFieldOfWorkProfileID);
                query.LeftJoin(medis).On(medis.StandardReferenceID == "RL4MedisType" && medis.ItemID == query.SRMedisType);
                query.LeftJoin(eduLevel).On(eduLevel.StandardReferenceID == "EducationLevel" && eduLevel.ItemID  == query.SREducationLevel);
                query.LeftJoin(rl4edu).On(query.RL4EducationID == rl4edu.RL4EducationID);

                query.Where(query.PersonID == txtPersonID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.ValidFrom.Descending, query.ValidTo.Descending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collEmployeeRL4" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collEmployeeRL4" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulateEmployeeRL4Grid()
        {
            //Display Data Detail
            EmployeeRL4s = null; //Reset Record Detail
            grdRL4.DataSource = EmployeeRL4s; //Requery
            grdRL4.MasterTableView.IsItemInserted = false;
            grdRL4.MasterTableView.ClearEditItems();
            grdRL4.DataBind();
        }

        protected void grdRL4_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRL4.DataSource = EmployeeRL4s;
        }

        protected void grdRL4_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeRL4Metadata.ColumnNames.EmployeeRL4ID]);
            EmployeeRL4 entity = FindEmployeeRL4(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdRL4_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeRL4Metadata.ColumnNames.EmployeeRL4ID]);
            EmployeeRL4 entity = FindEmployeeRL4(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdRL4_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeRL4 entity = EmployeeRL4s.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdRL4.Rebind();
        }
        private EmployeeRL4 FindEmployeeRL4(Int32 id)
        {
            EmployeeRL4Collection coll = EmployeeRL4s;
            EmployeeRL4 retEntity = null;
            foreach (EmployeeRL4 rec in coll)
            {
                if (rec.EmployeeRL4ID.ToString().Equals(id.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeeRL4 entity, GridCommandEventArgs e)
        {
            var userControl = (EmployeeRL4Detail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.EmployeeRL4ID = userControl.EmployeeRL4ID;
                entity.CompanyEducationProfileID = userControl.CompanyEducationProfileID;
                entity.CompanyFieldOfWorkProfileID = userControl.CompanyFieldOfWorkProfileID;
                entity.SRMedisType = userControl.SRMedisType;
                entity.SREducationLevel = userControl.SREducationLevel;
                entity.RL4EducationID = userControl.RL4EducationID;

                entity.SRRL4Status = userControl.SRRL4Status;
                entity.RL4StatusName = userControl.RL4StatusName;
                entity.SRRL4Type = userControl.SRRL4Type;
                entity.RL4TypeName = userControl.RL4TypeName;
                entity.SRRL4ProfessionType = userControl.SRRL4ProfessionType;
                entity.RL4ProfessionTypeName = userControl.RL4ProfessionTypeName;
                entity.SRRL4EducationLevel = userControl.SRRL4EducationLevel;
                entity.RL4EducationLevelName = userControl.RL4EducationLevelName;
                entity.SRRL4EducationMajor = userControl.SRRL4EducationMajor;
                entity.RL4EducationMajorName = userControl.RL4EducationMajorName;

                entity.ValidFrom = userControl.ValidFrom;
                entity.ValidTo = userControl.ValidTo;
                entity.IsActive = userControl.IsActive;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeeClinicalPrivilege
        private void RefreshCommandItemEmployeeClinicalPrivilege(AppEnum.DataMode newVal)
        {
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdEmployeeClinicalPrivilege.Columns[0].Visible = isVisible;
            grdEmployeeOrganization.Columns.FindByUniqueName("DocumentUpload").Visible = !isVisible;
            grdEmployeeClinicalPrivilege.Columns[grdEmployeeClinicalPrivilege.Columns.Count - 1].Visible = isVisible;

            grdEmployeeClinicalPrivilege.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            grdEmployeeClinicalPrivilege.Rebind();
        }

        private EmployeeClinicalPrivilegeCollection EmployeeClinicalPrivileges
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeClinicalPrivilege"];
                    if (obj != null)
                    {
                        return ((EmployeeClinicalPrivilegeCollection)(obj));
                    }
                }

                var coll = new EmployeeClinicalPrivilegeCollection();
                var query = new EmployeeClinicalPrivilegeQuery("a");
                var group = new AppStandardReferenceItemQuery("b");
                var area = new AppStandardReferenceItemQuery("c");
                var level = new AppStandardReferenceItemQuery("d");

                query.Select
                    (
                       query,
                       group.ItemName.As("refToStdRef_ProfessionGroup"),
                       area.ItemName.As("refToStdRef_ClinicalWorkArea"),
                       level.ItemName.As("refToStdRef_ClinicalAuthorityLevel")
                    );

                query.InnerJoin(group).On(group.StandardReferenceID == AppEnum.StandardReference.ProfessionGroup.ToString() && group.ItemID == query.SRProfessionGroup);
                query.InnerJoin(area).On(area.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea.ToString() && area.ItemID == query.SRClinicalWorkArea);
                query.LeftJoin(level).On(level.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel.ToString() && level.ItemID == query.SRClinicalAuthorityLevel);

                query.Where(query.PersonID == txtPersonID.Text);
                query.OrderBy(query.ValidFrom.Ascending);
                coll.Load(query);

                Session["collEmployeeClinicalPrivilege"] = coll;
                return coll;
            }
            set { Session["collEmployeeClinicalPrivilege"] = value; }
        }

        private void PopulateEmployeeClinicalPrivilegeGrid()
        {
            EmployeeClinicalPrivileges = null;
            grdEmployeeClinicalPrivilege.DataSource = EmployeeClinicalPrivileges;
            grdEmployeeClinicalPrivilege.MasterTableView.IsItemInserted = false;
            grdEmployeeClinicalPrivilege.MasterTableView.ClearEditItems();
            grdEmployeeClinicalPrivilege.DataBind();
        }

        protected void grdEmployeeClinicalPrivilege_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeClinicalPrivilege.DataSource = EmployeeClinicalPrivileges;
        }

        protected void grdEmployeeClinicalPrivilege_UpdateCommand(object source, GridCommandEventArgs e)
        {

            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeClinicalPrivilegeMetadata.ColumnNames.EmployeeClinicalPrivilegeID]);
            EmployeeClinicalPrivilege entity = FindEmployeeClinicalPrivilege(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdEmployeeClinicalPrivilege_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeClinicalPrivilegeMetadata.ColumnNames.EmployeeClinicalPrivilegeID]);
            EmployeeClinicalPrivilege entity = FindEmployeeClinicalPrivilege(id);
            if (entity != null && string.IsNullOrEmpty(entity.TransactionNo))
                entity.MarkAsDeleted();
        }

        protected void grdEmployeeClinicalPrivilege_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeClinicalPrivilege entity = EmployeeClinicalPrivileges.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdEmployeeClinicalPrivilege.Rebind();
        }
        private EmployeeClinicalPrivilege FindEmployeeClinicalPrivilege(Int32 id)
        {
            EmployeeClinicalPrivilegeCollection coll = EmployeeClinicalPrivileges;
            EmployeeClinicalPrivilege retEntity = null;
            foreach (EmployeeClinicalPrivilege rec in coll)
            {
                if (rec.EmployeeClinicalPrivilegeID.ToString().Equals(id.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeeClinicalPrivilege entity, GridCommandEventArgs e)
        {
            var userControl = (EmployeeClinicalPrivilegeDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.PersonID = txtPersonID.Value.ToInt();
                entity.EmployeeClinicalPrivilegeID = userControl.EmployeeClinicalPrivilegeID;
                entity.SRProfessionGroup = userControl.SRProfessionGroup;
                entity.ProfessionGroupName = userControl.ProfessionGroupName;
                entity.SRClinicalWorkArea = userControl.SRClinicalWorkArea;
                entity.ClinicalWorkAreaName = userControl.ClinicalWorkAreaName;
                entity.SRClinicalAuthorityLevel = userControl.SRClinicalAuthorityLevel;
                entity.ClinicalAuthorityLevelName = userControl.ClinicalAuthorityLevelName;
                entity.ValidFrom = userControl.ValidFrom;
                entity.ValidTo = userControl.ValidTo;
                entity.DecreeNo = userControl.DecreeNo;
                entity.Notes = userControl.Notes;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        #endregion

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

        protected void btnFilterOrientationType_Click(object sender, ImageClickEventArgs e)
        {
            grdEmployeeOrientation.CurrentPageIndex = 0;
            grdEmployeeOrientation.Rebind();
        }
    }
}

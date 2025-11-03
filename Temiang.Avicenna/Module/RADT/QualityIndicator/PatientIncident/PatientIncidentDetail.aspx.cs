using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PatientIncidentDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        #region Page Event & Initialize
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 300;
            // Url Search & List
            if (FormType == "entry")
            {
                UrlPageSearch = "PatientIncidentSearch.aspx?type=entry";
                UrlPageList = "PatientIncidentList.aspx?type=entry";

                ProgramID = AppConstant.Program.PatientIncident;
            }
            else
            {
                //UrlPageSearch = "PatientIncidentSearch.aspx?type=verif";
                //UrlPageList = "PatientIncidentList.aspx?type=verif";
                UrlPageSearch = "##";
                UrlPageList = "PatientIncidentVerificationList.aspx?type=verif";

                ProgramID = AppConstant.Program.PatientIncidentVerification;
            }

            //StandardReference Initialize
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnit(cboServiceUnitID, false);
                ComboBox.PopulateWithServiceUnit(cboServiceUnitIDInCharge, FormType == "entry");
                ComboBox.PopulateWithServiceUnit(cboServiceUnitIncidentLocationID, false);

                StandardReference.InitializeIncludeSpace(cboSRIncidentGroup, AppEnum.StandardReference.IncidentGroup);
                StandardReference.InitializeIncludeSpace(cboSRClinicalImpact, AppEnum.StandardReference.ClinicalImpact);
                StandardReference.InitializeIncludeSpace(cboSRProbabilityFrequency, AppEnum.StandardReference.IncidentProbabilityFrequency);
                StandardReference.InitializeIncludeSpace(cboSRHandledBy, AppEnum.StandardReference.IncidentHandledBy);
                StandardReference.InitializeIncludeSpace(cboSRIncidentFollowUp, AppEnum.StandardReference.IncidentFollowUp);

                StandardReference.InitializeIncludeSpace(cboSRIncidentOccurredOn, AppEnum.StandardReference.IncidentOccurredOn);
                StandardReference.InitializeIncludeSpace(cboSRIncidentOccurredInPatientsWith, AppEnum.StandardReference.IncidentOccurredInPatientsWith);

                if (FormType == "verif")
                {
                    tabStrip.Tabs[4].Visible = true;
                    tabStrip.Tabs[5].Visible = true;
                }

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSUI" || AppSession.Parameter.HealthcareInitialAppsVersion == "RSPM")
                {
                    txtReportedBy.ReadOnly = false;
                    txtReportingDate.Enabled = false;
                    txtReportingTime.Enabled = false;
                }

                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB")
                {
                    txtReportingDate.Enabled = false;
                    txtReportingTime.Enabled = false;
                }

                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                {
                    trServiceUnitIncidentLocationID.Visible = true;
                    lblIncidentLocation.Text = "Other Incident Location";
                    rfvIncidentLocation.Visible = false;

                    rfvServiceUnitID.Visible = true;
                    rfvReportedBy.Visible = true;
                    rfvServiceUnitIncidentLocationID.Visible = true;
                    rfvSRIncidentOccurredInPatientsWith.Visible = true;
                }
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdIncidentComponentType, grdIncidentComponentType);
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new PatientIncident());
            PopulateNewNo();
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var line = new PatientIncidentCauseAnalysisCollection();
            line.Query.Where(line.Query.PatientIncidentNo == txtPatientIncidentNo.Text);
            line.LoadAll();
            line.MarkAllAsDeleted();
            line.Save();

            var entity = new PatientIncident();
            if (entity.LoadByPrimaryKey(txtPatientIncidentNo.Text))
            {
                if (!IsApproved(entity, args))
                    return;

                entity.MarkAsDeleted();
                PatientIncidentRelatedUnits.MarkAllAsDeleted();
                PatientIncidentKtds.MarkAllAsDeleted();
                PatientIncidentComponentTypes.MarkAllAsDeleted();
                PatientIncidentSafetyGoalss.MarkAllAsDeleted();

                entity.Save();
                PatientIncidentRelatedUnits.Save();
                PatientIncidentKtds.Save();
                PatientIncidentComponentTypes.Save();
                PatientIncidentSafetyGoalss.Save();
                //SaveEntity(entity);
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (chkNonPatient.Checked)
            {
                if (txtFirstName.Text == string.Empty)
                {
                    args.MessageText = "First Name required.";
                    args.IsCancel = true;
                    return;
                }
                if (string.IsNullOrEmpty(rbtSexNonPat.SelectedValue))
                {
                    args.MessageText = "Gender required.";
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(cboRegistrationNo.SelectedValue))
                {
                    args.MessageText = "Registration No required.";
                    args.IsCancel = true;
                    return;
                }
            }

            if (cboSRIncidentFollowUp.SelectedValue == AppSession.Parameter.IncidentFollowUpInvestigation && PatientIncidentRelatedUnits.Count == 0)
            {
                args.MessageText = "Investigation units required.";
                args.IsCancel = true;
                return;
            }

            var entity = new PatientIncident();
            entity.AddNew();
            PopulateNewNo();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (chkNonPatient.Checked)
            {
                if (txtFirstName.Text == string.Empty)
                {
                    args.MessageText = "First Name required.";
                    args.IsCancel = true;
                    return;
                }
                if (string.IsNullOrEmpty(rbtSexNonPat.SelectedValue))
                {
                    args.MessageText = "Gender required.";
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(cboRegistrationNo.SelectedValue))
                {
                    args.MessageText = "Registration No required.";
                    args.IsCancel = true;
                    return;
                }
            }

            if (cboSRIncidentFollowUp.SelectedValue == AppSession.Parameter.IncidentFollowUpInvestigation && PatientIncidentRelatedUnits.Count == 0)
            {
                args.MessageText = "Investigation units required.";
                args.IsCancel = true;
                return;
            }

            var entity = new PatientIncident();
            if (entity.LoadByPrimaryKey(txtPatientIncidentNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
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
            auditLogFilter.PrimaryKeyData = string.Format("PatientIncidentNo='{0}'", txtPatientIncidentNo.Text.Trim());
            auditLogFilter.TableName = "PatientIncident";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("transNo", txtPatientIncidentNo.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboSRIncidentOccurredOn.SelectedValue))
            {
                args.MessageText = "Occurred On required.";
                args.IsCancel = true;
                return;
            }

            if (FormType == "verif" && PatientIncidentComponentTypes.Count == 0)
            {
                args.MessageText = "Incident Type & Component required.";
                args.IsCancel = true;
                return;
            }

            using (var trans = new esTransactionScope())
            {
                var entity = new PatientIncident();
                entity.LoadByPrimaryKey(txtPatientIncidentNo.Text);

                entity.IsApproved = true;
                entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;

                if (FormType == "verif")
                {
                    entity.IsVerified = true;
                    entity.VerifiedDateTime = (new DateTime()).NowAtSqlServer();
                    entity.VerifiedByUserID = AppSession.UserLogin.UserID;
                }
                else
                {
                    entity.SRIncidentGroupPrev = entity.SRIncidentGroup;
                    entity.SRClinicalImpactPrev = entity.SRClinicalImpact;
                    entity.SRIncidentProbabilityFrequencyPrev = entity.SRIncidentProbabilityFrequency;
                    entity.SRIncidentFollowUpPrev = entity.SRIncidentFollowUp;
                }

                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new PatientIncident();
            entity.LoadByPrimaryKey(txtPatientIncidentNo.Text);

            if (FormType == "entry")
            {
                if (entity.IsVerified == true)
                {
                    args.MessageText = "Reporting data is verified.";
                    args.IsCancel = true;
                    return;
                }
            }

            var investigation = new PatientIncidentInvestigationCollection();
            investigation.Query.Where(investigation.Query.PatientIncidentNo == entity.PatientIncidentNo);
            investigation.LoadAll();
            if (investigation.Count > 0)
            {
                args.MessageText = "Reporting data has been investigated.";
                args.IsCancel = true;
                return;
            }

            using (var trans = new esTransactionScope())
            {
                if (FormType == "verif")
                {
                    entity.IsVerified = false;
                    entity.VerifiedDateTime = null;
                    entity.VerifiedByUserID = null;
                }
                else
                {
                    entity.IsApproved = false;
                    entity.ApprovedDateTime = null;
                    entity.ApprovedByUserID = null;
                }

                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new PatientIncident();
            if (entity.LoadByPrimaryKey(txtPatientIncidentNo.Text))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        private bool IsApprovedOrVoid(PatientIncident entity, ValidateArgs args)
        {
            if (FormType == "verif")
            {
                if (entity.IsVerified ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }
            else
            {
                if (entity.IsApproved ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }

            return true;
        }

        private bool IsApproved(PatientIncident entity, ValidateArgs args)
        {
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (FormType == "verif")
            {
                ToolBarMenuSearch.Enabled = false;
                ToolBarMenuAdd.Enabled = false;
            }
            //else
                //ToolBarMenuDelete.Enabled = !chkIsApproved.Checked;

        }

        public override bool OnGetStatusMenuEdit()
        {
            return txtPatientIncidentNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return !chkIsApproved.Checked;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            chkNonPatient.Enabled = (newVal == AppEnum.DataMode.New);

            RefreshCommandItemRelatedUnit(newVal);
            RefreshCommandItemCompType(newVal);
            RefreshCommandItemIncidentCauseAnalysis(newVal);
            RefreshCommandItemSafetyGoals(newVal);
            RefreshCommandItemKtd(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            PatientIncident entity = new PatientIncident();
            if (parameters.Length > 0)
            {
                String patientIncidentNo = (String)parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(patientIncidentNo);
            }
            else
                entity.LoadByPrimaryKey(txtPatientIncidentNo.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var pi = (PatientIncident)entity;
            txtPatientIncidentNo.Text = pi.PatientIncidentNo;
            chkNonPatient.Checked = pi.NonPatient ?? false;
            TogglePnlNonPat(pi.NonPatient ?? false);
            if (pi.NonPatient ?? false)
            {
                txtFirstName.Text = pi.FirstName;
                txtMiddleName.Text = pi.MiddleName;
                txtLastName.Text = pi.LastName;
                if (pi.DateOfBirth.HasValue) txtDateOfBirth.SelectedDate = pi.DateOfBirth.Value;
                rbtSexNonPat.SelectedValue = pi.Sex;
                HitungUmur(pi.DateOfBirth, pi.IncidentDateTime, txtAgeNonPat);

                cboParamedicID.Items.Clear();
                cboParamedicID.Text = string.Empty;
            }
            else
            {
                if (!string.IsNullOrEmpty(pi.RegistrationNo))
                    GetRegistration(pi.RegistrationNo, false);
                else
                {
                    cboRegistrationNo.Items.Clear();
                    cboRegistrationNo.Text = string.Empty;

                    txtAge.Text = string.Empty;
                    //txtMedicalNo.Text = string.Empty;
                    cboMedicalNo.Text = string.Empty;
                    txtPatientName.Text = string.Empty;
                    txtSex.Text = string.Empty;

                    cboServiceUnitID.SelectedValue = string.Empty;
                    cboServiceUnitID.Text = string.Empty;
                    cboRoomID.SelectedValue = string.Empty;
                    cboRoomID.Text = string.Empty;
                    cboBedID.SelectedValue = string.Empty;
                    cboBedID.Text = string.Empty;

                    if (!string.IsNullOrEmpty(pi.PatientID))
                        GetPatient(pi.PatientID, string.Empty);
                }

                HitungUmur(pi.DateOfBirth, pi.IncidentDateTime, txtAge);
            }

            if (!string.IsNullOrEmpty(pi.ParamedicID))
            {
                var p = new Paramedic();
                if (p.LoadByPrimaryKey(pi.ParamedicID))
                {
                    cboParamedicID.SelectedValue = p.ParamedicID;
                    cboParamedicID.Text = p.ParamedicName;
                }
                else
                {
                    cboParamedicID.Items.Clear();
                    cboParamedicID.Text = string.Empty;
                }
            }
            else
            {
                cboParamedicID.Items.Clear();
                cboParamedicID.Text = string.Empty;
            }

            txtInitialName.Text = pi.InitialName;
            txtReportedBy.Text = pi.ReportedByUserID ?? AppSession.UserLogin.UserID;
            if (!string.IsNullOrEmpty(pi.ServiceUnitIDInCharge))
                cboServiceUnitIDInCharge.SelectedValue = pi.ServiceUnitIDInCharge;
            else
            {
                cboServiceUnitIDInCharge.SelectedValue = string.Empty;
                cboServiceUnitIDInCharge.Text = string.Empty;
            }
            if (!string.IsNullOrEmpty(pi.ServiceUnitIncidentLocationID))
                cboServiceUnitIncidentLocationID.SelectedValue = pi.ServiceUnitIncidentLocationID;
            else
            {
                cboServiceUnitIncidentLocationID.SelectedValue = string.Empty;
                cboServiceUnitIncidentLocationID.Text = string.Empty;
            }
            txtIncidentLocation.Text = pi.IncidentLocation;

            txtIncidentDate.SelectedDate = pi.IncidentDateTime.HasValue ? pi.IncidentDateTime.Value : (new DateTime()).NowAtSqlServer();
            txtIncidentTime.SelectedDate = pi.IncidentDateTime.HasValue ? pi.IncidentDateTime.Value : (new DateTime()).NowAtSqlServer();
            txtReportingDate.SelectedDate = pi.ReportingDateTime.HasValue ? pi.ReportingDateTime.Value : (new DateTime()).NowAtSqlServer();
            txtReportingTime.SelectedDate = pi.ReportingDateTime.HasValue ? pi.ReportingDateTime.Value : (new DateTime()).NowAtSqlServer();
            txtIncidentName.Text = pi.IncidentName;
            txtChronology.Text = pi.Chronology;

            cboSRIncidentGroup.SelectedValue = pi.SRIncidentGroup;
            cboSRClinicalImpact.SelectedValue = pi.SRClinicalImpact;
            cboSRProbabilityFrequency.SelectedValue = pi.SRIncidentProbabilityFrequency;
            cboSRIncidentFollowUp.SelectedValue = pi.SRIncidentFollowUp;

            var rgm = new RiskGradingMtx();
            if (string.IsNullOrEmpty(pi.SRClinicalImpact) || string.IsNullOrEmpty(pi.SRIncidentProbabilityFrequency))
                lblRiskGradingName.Text = string.Empty;
            else
            {
                if (rgm.LoadByPrimaryKey(pi.SRClinicalImpact, pi.SRIncidentProbabilityFrequency))
                    GetRiskGrading(rgm.RiskGradingID);
                else
                    lblRiskGradingName.Text = string.Empty;
            }

            txtHandling.Text = pi.Handling;
            cboSRHandledBy.SelectedValue = pi.SRIncidentHandledBy;

            cboSRIncidentOccurredOn.SelectedValue = pi.SRIncidentOccurredOn;
            txtIncidentOccurredOnName.Text = pi.IncidentOccurredOnName;
            cboSRIncidentOccurredInPatientsWith.SelectedValue = pi.SRIncidentOccurredInPatientsWith;
            txtIncidentOccurredInPatientsWithName.Text = pi.IncidentOccurredInPatientsWithName;
            chkIsOccurInOtherUnits.Checked = pi.IsOccurInOtherUnits ?? false;
            txtOccurInOtherUnitsNotes.Text = pi.OccurInOtherUnitsNotes;

            if (!string.IsNullOrEmpty(pi.SRIncidentOccurredOn))
            {
                var std = new AppStandardReferenceItem();
                if (std.LoadByPrimaryKey(AppEnum.StandardReference.IncidentOccurredOn.ToString(), pi.SRIncidentOccurredOn))
                    txtIncidentOccurredOnName.Enabled = std.Note.Length > 0;
            }

            if (!string.IsNullOrEmpty(pi.SRIncidentOccurredInPatientsWith))
            {
                var std = new AppStandardReferenceItem();
                if (std.LoadByPrimaryKey(AppEnum.StandardReference.IncidentOccurredInPatientsWith.ToString(), pi.SRIncidentOccurredInPatientsWith))
                    txtIncidentOccurredInPatientsWithName.Enabled = std.Note.Length > 0;
            }

            txtOccurInOtherUnitsNotes.Enabled = chkIsOccurInOtherUnits.Checked;
            if (FormType == "verif")
                chkIsApproved.Checked = pi.IsVerified ?? false;
            else
                chkIsApproved.Checked = pi.IsApproved ?? false;

            PopulateRelatedUnitGrid();
            PopulateIncidentCompTypeGrid();
            PopulateIncidentCauseAnalysisGrid();
            PopulategrdSafetyGoalsGrid();
            PopulateKtdGrid();
        }

        private void HitungUmur(DateTime? DateOfBirth, DateTime? dNow, RadTextBox txt)
        {
            if (!DateOfBirth.HasValue) return;
            if (!dNow.HasValue) return;
            var y = Helper.GetAgeInYear(DateOfBirth.Value.Date, dNow.Value.Date).ToString();
            var m = Helper.GetAgeInMonth(DateOfBirth.Value.Date, dNow.Value.Date).ToString();
            var d = Helper.GetAgeInDay(DateOfBirth.Value.Date, dNow.Value.Date).ToString();

            txt.Text = y + " yr " + m + " mth " + d + " dy ";
        }
        #endregion

        #region Private Method Standard
        private void SetEntityValue(PatientIncident entity)
        {
            entity.PatientIncidentNo = txtPatientIncidentNo.Text;
            entity.RegistrationNo = cboRegistrationNo.SelectedValue;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.RoomID = cboRoomID.SelectedValue;
            entity.BedID = cboBedID.SelectedValue;
            entity.ParamedicID = cboParamedicID.SelectedValue;
            entity.ServiceUnitIncidentLocationID = cboServiceUnitIncidentLocationID.SelectedValue;
            entity.IncidentLocation = txtIncidentLocation.Text;
            entity.ServiceUnitIDInCharge = cboServiceUnitIDInCharge.SelectedValue;
            entity.IncidentDateTime = DateTime.Parse(txtIncidentDate.SelectedDate.Value.ToShortDateString() + " " +
                               txtIncidentTime.SelectedDate.Value.ToShortTimeString());
            entity.ReportingDateTime = DateTime.Parse(txtReportingDate.SelectedDate.Value.ToShortDateString() + " " +
                               txtReportingTime.SelectedDate.Value.ToShortTimeString());
            entity.IncidentName = txtIncidentName.Text;
            entity.Chronology = txtChronology.Text;
            entity.SRIncidentType = string.Empty;
            entity.SRIncidentGroup = cboSRIncidentGroup.SelectedValue;
            entity.SRClinicalImpact = cboSRClinicalImpact.SelectedValue;
            entity.SRIncidentProbabilityFrequency = cboSRProbabilityFrequency.SelectedValue;
            entity.Handling = txtHandling.Text;
            entity.SRIncidentHandledBy = cboSRHandledBy.SelectedValue;
            entity.SRIncidentFollowUp = cboSRIncidentFollowUp.SelectedValue;
            //entity.FollowUpDate = ??;
            entity.ReportedByUserID = txtReportedBy.Text;
            entity.SRIncidentOccurredOn = cboSRIncidentOccurredOn.SelectedValue;
            entity.IncidentOccurredOnName = txtIncidentOccurredOnName.Text;
            entity.SRIncidentOccurredInPatientsWith = cboSRIncidentOccurredInPatientsWith.SelectedValue;
            entity.IncidentOccurredInPatientsWithName = txtIncidentOccurredInPatientsWithName.Text;
            entity.IsOccurInOtherUnits = chkIsOccurInOtherUnits.Checked;
            entity.OccurInOtherUnitsNotes = txtOccurInOtherUnitsNotes.Text;
            entity.InitialName = txtInitialName.Text;
            entity.NonPatient = chkNonPatient.Checked;
            entity.IsRiskManagement = false;

            if (!(entity.NonPatient ?? false))
            {
                var p = GetPatient(string.Empty, cboMedicalNo.Text);
                entity.PatientID = p.PatientID;
                entity.FirstName = p.FirstName;
                entity.MiddleName = p.MiddleName;
                entity.LastName = p.LastName;
                entity.Sex = p.Sex;
                entity.DateOfBirth = p.DateOfBirth;
                entity.Address = string.Empty;
            }
            else
            {
                entity.FirstName = txtFirstName.Text;
                entity.MiddleName = txtMiddleName.Text;
                entity.LastName = txtLastName.Text;
                entity.Sex = rbtSexNonPat.SelectedValue;
                if (!txtDateOfBirth.IsEmpty)
                    entity.DateOfBirth = txtDateOfBirth.SelectedDate.Value;
                else
                    entity.str.DateOfBirth = string.Empty;
                entity.Address = string.Empty;
            }

            //Last Update Status
            if (entity.es.IsAdded)
            {
                entity.InsertByUserID = AppSession.UserLogin.UserID;
                entity.InsertDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
            else if (entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in PatientIncidentRelatedUnits)
            {
                item.PatientIncidentNo = txtPatientIncidentNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in PatientIncidentComponentTypes)
            {
                item.PatientIncidentNo = txtPatientIncidentNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in PatientIncidentSafetyGoalss)
            {
                item.PatientIncidentNo = txtPatientIncidentNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in PatientIncidentKtds)
            {
                item.PatientIncidentNo = txtPatientIncidentNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(PatientIncident entity)
        {
            var PatientIncidentCauseAnalysiss = new PatientIncidentCauseAnalysisCollection();
            PatientIncidentCauseAnalysiss.Query.Where(PatientIncidentCauseAnalysiss.Query.PatientIncidentNo == entity.PatientIncidentNo);
            PatientIncidentCauseAnalysiss.LoadAll();

            foreach (GridDataItem dataItem in grdCauseAnalysis.MasterTableView.Items)
            {
                string srIncidentCauseAnalysis = dataItem.GetDataKeyValue("SRIncidentCauseAnalysis").ToString();
                bool isSelect = ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("chkIsSelect")).Checked;
                string notes = ((RadTextBox)dataItem.FindControl("txtNotes")).Text;

                bool isExist = false;
                foreach (PatientIncidentCauseAnalysis row in PatientIncidentCauseAnalysiss)
                {
                    if (row.SRIncidentCauseAnalysis.Equals(srIncidentCauseAnalysis))
                    {
                        isExist = true;
                        row.Notes = notes;

                        if (!isSelect)
                            row.MarkAsDeleted();
                        break;
                    }
                }
                //Add
                if (!isExist && isSelect)
                {
                    PatientIncidentCauseAnalysis row = PatientIncidentCauseAnalysiss.AddNew();
                    row.PatientIncidentNo = entity.PatientIncidentNo;
                    row.SRIncidentCauseAnalysis = srIncidentCauseAnalysis;
                    row.Notes = notes;
                    row.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    row.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                PatientIncidentRelatedUnits.Save();
                PatientIncidentComponentTypes.Save();
                PatientIncidentCauseAnalysiss.Save();
                PatientIncidentSafetyGoalss.Save();
                PatientIncidentKtds.Save();

                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new PatientIncidentQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PatientIncidentNo > txtPatientIncidentNo.Text, que.IsRiskManagement == false);
                que.OrderBy(que.PatientIncidentNo.Ascending);
            }
            else
            {
                que.Where(que.PatientIncidentNo < txtPatientIncidentNo.Text, que.IsRiskManagement == false);
                que.OrderBy(que.PatientIncidentNo.Descending);
            }
            var entity = new PatientIncident();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Override Method & Function

        private void PopulateNewNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New)
                return;

            _autoNumber = Helper.GetNewAutoNumber(txtIncidentDate.SelectedDate.Value, AppEnum.AutoNumber.PatientIncidentNo);
            txtPatientIncidentNo.Text = _autoNumber.LastCompleteNumber;
        }

        private void GetRegistration(string registrationNo, bool isNew)
        {
            var r = new Registration();

            if (string.IsNullOrEmpty(registrationNo))
            {
                //
                txtAge.Text = string.Empty;
                //txtMedicalNo.Text = string.Empty;
                cboMedicalNo.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtSex.Text = string.Empty;

                cboServiceUnitID.SelectedValue = string.Empty;
                cboServiceUnitID.Text = string.Empty;
                cboRoomID.SelectedValue = string.Empty;
                cboRoomID.Text = string.Empty;
                cboBedID.SelectedValue = string.Empty;
                cboBedID.Text = string.Empty;

                if (isNew)
                {
                    cboParamedicID.Items.Clear();
                    cboParamedicID.Text = string.Empty;
                }
            }
            else
            {
                if (r.LoadByPrimaryKey(registrationNo))
                {
                    //var args = new RadComboBoxItemsRequestedEventArgs();
                    //args.Text = r.RegistrationNo;
                    //cboRegistrationNo_ItemsRequested(cboRegistrationNo, args);
                    //cboRegistrationNo.SelectedValue = r.RegistrationNo;

                    cboRegistrationNo.SelectedValue = r.RegistrationNo;
                    cboRegistrationNo.Text = r.RegistrationNo;

                    txtAge.Text = r.AgeInYear.ToString() + " yr " + r.AgeInMonth + " mth " + r.AgeInDay + "dy";

                    GetPatient(r.PatientID, string.Empty);

                    ComboBox.PopulateWithServiceUnit(cboServiceUnitID, false);
                    cboServiceUnitID.SelectedValue = r.ServiceUnitID;
                    ComboBox.PopulateWithRoom(cboRoomID, r.ServiceUnitID ?? "");
                    cboRoomID.SelectedValue = r.RoomID;
                    ComboBox.PopulateWithBed(cboBedID, r.RoomID ?? "");
                    cboBedID.SelectedValue = r.BedID;

                    if (isNew)
                        ComboBox.PopulateWithOneParamedic(cboParamedicID, r.ParamedicID);
                }
            }
        }

        private Patient GetPatient(string patientId, string noRM)
        {
            var p = new Patient();
            var res = (string.IsNullOrEmpty(patientId) ?
                p.LoadByMedicalNo(noRM) : p.LoadByPrimaryKey(patientId));
            if (res)
            {
                //txtMedicalNo.Text = p.MedicalNo;
                cboMedicalNo.Text = p.MedicalNo;
                txtPatientName.Text = p.PatientName;
                txtSex.Text = p.Sex;

                HitungUmur(p.DateOfBirth, (new DateTime()).NowAtSqlServer(), txtAge);
                return p;
            }
            return null;
        }

        #endregion

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid) || string.IsNullOrEmpty(eventArgument))
                return;

            switch (((RadGrid)sourceControl).ID)
            {
                case "grdIncidentComponentType":
                    grdIncidentComponentType.Rebind();
                    break;
            }
        }

        #region Selected Changed
        private void TogglePnlNonPat(bool ShowNotPat)
        {
            pnlPatient.Visible = !ShowNotPat;
            pnlNonPatient.Visible = ShowNotPat;
        }
        protected void chkNonPatient_OnCheckedChanged(Object sender, EventArgs args)
        {
            TogglePnlNonPat(chkNonPatient.Checked);
        }

        protected void cboRegistrationNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                GetRegistration(e.Value, true);
            }
        }

        protected void cboRegistrationNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var reg = new RegistrationQuery("a");
            var pat = new PatientQuery("b");
            var unit = new ServiceUnitQuery("c");
            var room = new ServiceRoomQuery("d");

            reg.es.Top = 5;
            reg.Select(
                reg.RegistrationNo,
                reg.BedID,
                pat.PatientID,
                pat.MedicalNo,
                pat.PatientName,
                unit.ServiceUnitName,
                room.RoomName
                );
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            reg.LeftJoin(room).On(reg.RoomID == room.RoomID);
            reg.Where(
                reg.IsVoid == false,
                reg.IsDirectPrescriptionReturn == false,
                reg.IsNonPatient == false
                );
            if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSUI" && AppSession.Parameter.HealthcareInitialAppsVersion != "RSPM")
            {
                /*untuk rsui dibuka semua regnya. kalau ada rs lain begini juga maka nanti harus buat parameter*/
                reg.Where(reg.IsClosed == false);
            }

            if (e.Text.Trim().Contains(" "))
            {
                var searchs = e.Text.Trim().Split(' ');
                foreach (var search in searchs)
                {
                    var searchLike = "%" + search + "%";
                    reg.Where(
                        reg.Or(
                            reg.RegistrationNo.Like(searchLike),
                            //pat.PatientID.Like(searchLike),
                            pat.FirstName.Like(searchLike),
                            pat.LastName.Like(searchLike),
                            pat.MiddleName.Like(searchLike),
                            pat.MedicalNo.Like(searchLike)
                        )
                    );
                }
            }
            else
            {
                if (e.Text.Contains("REG"))
                {
                    string searchTextContain = string.Format("{0}%", e.Text);
                    reg.Where(reg.RegistrationNo.Like(searchTextContain));
                }
                else if (e.Text.Contains("-"))
                {
                    string searchTextContain = string.Format("{0}%", e.Text);
                    reg.Where(pat.MedicalNo.Like(searchTextContain));
                }
                else
                {
                    string searchTextContain = string.Format("%{0}%", e.Text);
                    reg.Where(
                        reg.Or(
                            //reg.RegistrationNo.Like(searchTextContain),
                            //pat.PatientID.Like(searchTextContain),
                            //pat.MedicalNo.Like(searchTextContain),
                            pat.FirstName.Like(searchTextContain),
                            pat.MiddleName.Like(searchTextContain),
                            pat.LastName.Like(searchTextContain)
                        )
                    );
                }
            }
            reg.OrderBy(reg.RegistrationDate.Descending);

            cboRegistrationNo.DataSource = reg.LoadDataTable();
            cboRegistrationNo.DataBind();
        }

        protected void cboRegistrationNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString();
        }

        protected void cboMedicalNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                GetPatient(string.Empty, e.Value);
            }
        }

        protected void cboMedicalNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var pat = new PatientQuery("b");

            pat.es.Top = 5;
            pat.Select(
                pat.PatientID,
                pat.MedicalNo,
                pat.PatientName
            );
            var searchs = e.Text.Trim().Split(' ');
            foreach (var search in searchs)
            {
                var searchLike = "%" + search + "%";
                pat.Where(
                    pat.Or(
                        pat.PatientID.Like(searchLike),
                        pat.FirstName.Like(searchLike),
                        pat.LastName.Like(searchLike),
                        pat.MiddleName.Like(searchLike),
                        pat.MedicalNo.Like(searchLike)
                    )
                );
            }
            pat.OrderBy(pat.FirstName.Ascending);

            cboMedicalNo.DataSource = pat.LoadDataTable();
            cboMedicalNo.DataBind();
        }

        protected void cboMedicalNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["MedicalNo"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["MedicalNo"].ToString();
        }

        protected void txtIncidentDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            PopulateNewNo();
        }

        protected void txtDateOfBirth_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            HitungUmur(txtDateOfBirth.SelectedDate.Value, (new DateTime()).NowAtSqlServer(), txtAgeNonPat);
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                ComboBox.PopulateWithRoom(cboRoomID, e.Value);
            }
        }

        protected void cboRoomID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                ComboBox.PopulateWithBed(cboBedID, e.Value);
            }
        }

        protected void cboSRClinicalImpact_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                GetFollowUp(e.Value, cboSRProbabilityFrequency.SelectedValue);
            }
            else
            {
                cboSRIncidentFollowUp.SelectedValue = string.Empty;
                cboSRIncidentFollowUp.Text = string.Empty;
            }
        }

        protected void cboSRProbabilityFrequency_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                GetFollowUp(cboSRClinicalImpact.SelectedValue, e.Value);
            }
            else
            {
                cboSRIncidentFollowUp.SelectedValue = string.Empty;
                cboSRIncidentFollowUp.Text = string.Empty;
                lblRiskGradingName.Text = string.Empty;
            }
        }

        private void GetFollowUp(string srClinicalImpact, string srIncidentProbabilityFrequency)
        {
            var rgm = new RiskGradingMtx();

            if (string.IsNullOrEmpty(srClinicalImpact) || string.IsNullOrEmpty(srIncidentProbabilityFrequency))
            {
                cboSRIncidentFollowUp.SelectedValue = string.Empty;
                cboSRIncidentFollowUp.Text = string.Empty;
                lblRiskGradingName.Text = string.Empty;
            }
            else
            {
                if (rgm.LoadByPrimaryKey(srClinicalImpact, srIncidentProbabilityFrequency))
                {
                    cboSRIncidentFollowUp.SelectedValue = rgm.SRIncidentFollowUp;
                    GetRiskGrading(rgm.RiskGradingID);
                }
                else
                {
                    cboSRIncidentFollowUp.SelectedValue = string.Empty;
                    cboSRIncidentFollowUp.Text = string.Empty;
                    lblRiskGradingName.Text = string.Empty;
                }
            }
        }

        private void GetRiskGrading(string riskGradingId)
        {
            var rg = new RiskGrading();
            lblRiskGradingName.Text = rg.LoadByPrimaryKey(riskGradingId)
                                          ? "** " + rg.RiskGradingName
                                          : string.Empty;
        }

        protected void cboSRIncidentOccurredOn_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                var std = new AppStandardReferenceItem();
                if (std.LoadByPrimaryKey(AppEnum.StandardReference.IncidentOccurredOn.ToString(), e.Value))
                {
                    txtIncidentOccurredOnName.Enabled = std.Note.Length > 0;
                    if (txtIncidentOccurredOnName.Enabled)
                        txtIncidentOccurredOnName.Focus();
                }

            }
            else
                txtIncidentOccurredOnName.Enabled = false;

            txtIncidentOccurredOnName.Text = string.Empty;
        }

        protected void cboSRIncidentOccurredInPatientsWith_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                var std = new AppStandardReferenceItem();
                if (std.LoadByPrimaryKey(AppEnum.StandardReference.IncidentOccurredInPatientsWith.ToString(), e.Value))
                {
                    txtIncidentOccurredInPatientsWithName.Enabled = std.Note.Length > 0;
                    if (txtIncidentOccurredInPatientsWithName.Enabled)
                        txtIncidentOccurredInPatientsWithName.Focus();
                }

            }
            else
                txtIncidentOccurredInPatientsWithName.Enabled = false;

            txtIncidentOccurredInPatientsWithName.Text = string.Empty;
        }

        protected void chkIsOccurInOtherUnits_CheckedChanged(object sender, EventArgs e)
        {
            txtOccurInOtherUnitsNotes.Enabled = chkIsOccurInOtherUnits.Checked;
            if (txtOccurInOtherUnitsNotes.Enabled)
                txtOccurInOtherUnitsNotes.Focus();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var par = new ParamedicQuery("b");

            par.es.Top = 10;
            par.Select(
                par.ParamedicID,
                par.ParamedicName
            );
            var searchs = e.Text.Trim().Split(' ');
            foreach (var search in searchs)
            {
                var searchLike = "%" + search + "%";
                par.Where(
                    par.IsActive == true,
                    par.Or(
                        par.ParamedicID.Like(searchLike),
                        par.ParamedicName.Like(searchLike)
                    )
                );
            }
            par.OrderBy(par.ParamedicName.Ascending);

            cboParamedicID.DataSource = par.LoadDataTable();
            cboParamedicID.DataBind();
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        #endregion

        #region Record Detail Method Function of Patient Incident Related Unit

        private PatientIncidentRelatedUnitCollection PatientIncidentRelatedUnits
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPatientIncidentRelatedUnit"];
                    if (obj != null)
                    {
                        return ((PatientIncidentRelatedUnitCollection)(obj));
                    }
                }

                var coll = new PatientIncidentRelatedUnitCollection();
                var query = new PatientIncidentRelatedUnitQuery("a");
                var su = new ServiceUnitQuery("b");

                query.Select
                    (
                        query,
                        su.ServiceUnitName.As("refToServiceUnit_ServiceUnitName")

                    );
                query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);
                query.Where(query.PatientIncidentNo == txtPatientIncidentNo.Text);
                coll.Load(query);

                Session["collPatientIncidentRelatedUnit"] = coll;
                return coll;
            }
            set
            {
                Session["collPatientIncidentRelatedUnit"] = value;
            }
        }

        private void RefreshCommandItemRelatedUnit(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdRelatedUnit.Columns[0].Visible = isVisible;
            grdRelatedUnit.Columns[grdRelatedUnit.Columns.Count - 1].Visible = isVisible;

            grdRelatedUnit.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdRelatedUnit.Rebind();
        }

        private void PopulateRelatedUnitGrid()
        {
            //Display Data Detail
            PatientIncidentRelatedUnits = null; //Reset Record Detail
            grdRelatedUnit.DataSource = PatientIncidentRelatedUnits; //Requery
            grdRelatedUnit.MasterTableView.IsItemInserted = false;
            grdRelatedUnit.MasterTableView.ClearEditItems();
            grdRelatedUnit.DataBind();
        }

        private PatientIncidentRelatedUnit FindRelatedUnit(String unitId)
        {
            PatientIncidentRelatedUnitCollection coll = PatientIncidentRelatedUnits;
            PatientIncidentRelatedUnit retEntity = null;
            foreach (PatientIncidentRelatedUnit rec in coll)
            {
                if (rec.ServiceUnitID.Equals(unitId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdRelatedUnit_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRelatedUnit.DataSource = PatientIncidentRelatedUnits;
        }

        protected void grdRelatedUnit_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String unitId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PatientIncidentRelatedUnitMetadata.ColumnNames.ServiceUnitID]);
            PatientIncidentRelatedUnit entity = FindRelatedUnit(unitId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdRelatedUnit_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String unitId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][PatientIncidentRelatedUnitMetadata.ColumnNames.ServiceUnitID]);
            PatientIncidentRelatedUnit entity = FindRelatedUnit(unitId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdRelatedUnit_InsertCommand(object source, GridCommandEventArgs e)
        {
            PatientIncidentRelatedUnit entity = PatientIncidentRelatedUnits.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdRelatedUnit.Rebind();
        }

        private void SetEntityValue(PatientIncidentRelatedUnit entity, GridCommandEventArgs e)
        {
            var userControl = (PatientIncidentRelatedUnitItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ServiceUnitID = userControl.ServiceUnitID;
                entity.ServiceUnitName = userControl.ServiceUnitName;
            }
        }

        #endregion

        #region Record Detail Method Function of Patient Incident Component Type

        private PatientIncidentComponentTypeCollection PatientIncidentComponentTypes
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPatientIncidentComponentType" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((PatientIncidentComponentTypeCollection)(obj));
                    }
                }

                var coll = new PatientIncidentComponentTypeCollection();
                var query = new PatientIncidentComponentTypeQuery("a");
                var itype = new AppStandardReferenceItemQuery("b");
                var comp = new IncidentTypeQuery("c");
                var subComp = new IncidentTypeItemQuery("d");

                query.Select
                    (
                        query,
                        itype.ItemName.As("refToAppStandardReferenceItem_ItemName"),
                        comp.ComponentName.As("refToIncidentType_ComponentName"),
                        subComp.SubComponentName.As("refToIncidentTypeItem_SubComponentName"),
                        subComp.IsAllowEdit.As("refToIncidentTypeItem_IsAllowEdit")
                    );
                query.InnerJoin(itype).On(query.SRIncidentType == itype.ItemID &&
                                          itype.StandardReferenceID == AppEnum.StandardReference.IncidentType);
                query.InnerJoin(comp).On(query.SRIncidentType == comp.SRIncidentType && query.ComponentID == comp.ComponentID);
                query.InnerJoin(subComp).On(query.SRIncidentType == subComp.SRIncidentType &&
                                            query.ComponentID == subComp.ComponentID &&
                                            query.SubComponentID == subComp.SubComponentID);
                query.Where(query.PatientIncidentNo == txtPatientIncidentNo.Text);
                query.OrderBy(query.SRIncidentType.Ascending, query.ComponentID.Ascending,
                              query.SubComponentID.Ascending);
                coll.Load(query);

                Session["collPatientIncidentComponentType" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collPatientIncidentComponentType" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItemCompType(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdIncidentComponentType.Columns[0].Visible = isVisible;
            grdIncidentComponentType.Columns[grdIncidentComponentType.Columns.Count - 1].Visible = isVisible;

            grdIncidentComponentType.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdIncidentComponentType.Rebind();
        }

        private void PopulateIncidentCompTypeGrid()
        {
            //Display Data Detail
            PatientIncidentComponentTypes = null; //Reset Record Detail
            grdIncidentComponentType.DataSource = PatientIncidentComponentTypes; //Requery
            grdIncidentComponentType.MasterTableView.IsItemInserted = false;
            grdIncidentComponentType.MasterTableView.ClearEditItems();
            grdIncidentComponentType.DataBind();
        }

        private PatientIncidentComponentType FindCompType(String itype, String compId, String subCompId)
        {
            PatientIncidentComponentTypeCollection coll = PatientIncidentComponentTypes;
            PatientIncidentComponentType retEntity = null;
            foreach (PatientIncidentComponentType rec in coll)
            {
                if (rec.SRIncidentType.Equals(itype) && rec.ComponentID.Equals(compId) && rec.SubComponentID.Equals(subCompId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdIncidentComponentType_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdIncidentComponentType.DataSource = PatientIncidentComponentTypes;
        }

        protected void grdIncidentComponentType_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itype =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PatientIncidentComponentTypeMetadata.ColumnNames.SRIncidentType]);
            String compId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PatientIncidentComponentTypeMetadata.ColumnNames.ComponentID]);
            String subCompId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PatientIncidentComponentTypeMetadata.ColumnNames.SubComponentID]);
            PatientIncidentComponentType entity = FindCompType(itype, compId, subCompId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdIncidentComponentType_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String itype =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][PatientIncidentComponentTypeMetadata.ColumnNames.SRIncidentType]);
            String compId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][PatientIncidentComponentTypeMetadata.ColumnNames.ComponentID]);
            String subCompId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][PatientIncidentComponentTypeMetadata.ColumnNames.SubComponentID]);
            PatientIncidentComponentType entity = FindCompType(itype, compId, subCompId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdIncidentComponentType_InsertCommand(object source, GridCommandEventArgs e)
        {
            PatientIncidentComponentType entity = PatientIncidentComponentTypes.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdIncidentComponentType.Rebind();
        }

        private void SetEntityValue(PatientIncidentComponentType entity, GridCommandEventArgs e)
        {
            var userControl = (PatientIncidentComponentTypeItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRIncidentType = userControl.SRIncidentType;
                entity.IncidentType = userControl.IncidentType;
                entity.ComponentID = userControl.ComponentID;
                entity.ComponentName = userControl.ComponentName;
                entity.SubComponentID = userControl.SubComponentID;
                entity.SubComponent = userControl.SubComponent;
                entity.SubComponentName = userControl.SubComponentName;
                entity.Modus = userControl.Modus;
                entity.IsAllowEdit = userControl.IsAllowEdit;
            }
        }

        protected void lbDeleteAll_OnClick(object sender, EventArgs e)
        {
            PatientIncidentComponentTypes.MarkAllAsDeleted();
            grdIncidentComponentType.Rebind();
        }

        #endregion

        #region Record Detail Method Function Patient Incident Cause Analysis

        private void PopulateIncidentCauseAnalysisGrid()
        {
            //Display Data Detail
            grdCauseAnalysis.DataSource = GetIncidentCauseAnalysis();
            grdCauseAnalysis.DataBind();
        }

        protected void grdCauseAnalysis_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdCauseAnalysis.DataSource = GetIncidentCauseAnalysis();
        }

        private DataTable GetIncidentCauseAnalysis()
        {
            var query = new PatientIncidentCauseAnalysisQuery("a");
            var qrRef = new AppStandardReferenceItemQuery("b");
            if (this.DataModeCurrent == AppEnum.DataMode.Read)
            {
                query.InnerJoin(qrRef).On(query.SRIncidentCauseAnalysis == qrRef.ItemID);
                query.Where(query.PatientIncidentNo == txtPatientIncidentNo.Text);
            }
            else
            {
                query.RightJoin(qrRef).On(query.SRIncidentCauseAnalysis == qrRef.ItemID & query.PatientIncidentNo == txtPatientIncidentNo.Text);
            }
            query.Where(qrRef.StandardReferenceID == "IncidentCauseAnalysis");
            query.OrderBy(qrRef.ItemID.Ascending);
            query.Select
                (
                    "<CONVERT(Bit,CASE WHEN COALESCE(a.SRIncidentCauseAnalysis,'')='' THEN 0 ELSE 1 END) as IsSelect>",
                    qrRef.ItemID.As("SRIncidentCauseAnalysis"),
                    qrRef.ItemName.As("IncidentCauseAnalysis"),
                    query.Notes
                );
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        private void RefreshCommandItemIncidentCauseAnalysis(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdCauseAnalysis.Columns[0].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdCauseAnalysis.Rebind();
        }
        #endregion

        #region Record Detail Method Function of Safety Goals

        private PatientIncidentSafetyGoalsCollection PatientIncidentSafetyGoalss
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPatientIncidentSafetyGoals"];
                    if (obj != null)
                    {
                        return ((PatientIncidentSafetyGoalsCollection)(obj));
                    }
                }

                var coll = new PatientIncidentSafetyGoalsCollection();
                var query = new PatientIncidentSafetyGoalsQuery("a");
                var ref1 = new AppStandardReferenceItemQuery("b");

                query.Select
                    (
                        query,
                        ref1.ItemName.As("refToAppStandardReferenceItem_SafetyGoals")

                    );
                query.InnerJoin(ref1).On(query.SRSafetyGoals == ref1.ItemID &&
                                         ref1.StandardReferenceID ==
                                         AppEnum.StandardReference.SafetyGoals);
                query.Where(query.PatientIncidentNo == txtPatientIncidentNo.Text);
                coll.Load(query);

                Session["collPatientIncidentSafetyGoals"] = coll;
                return coll;
            }
            set
            {
                Session["collPatientIncidentSafetyGoals"] = value;
            }
        }

        private void RefreshCommandItemSafetyGoals(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdSafetyGoals.Columns[0].Visible = isVisible;
            grdSafetyGoals.Columns[grdSafetyGoals.Columns.Count - 1].Visible = isVisible;

            grdSafetyGoals.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdSafetyGoals.Rebind();
        }

        private void PopulategrdSafetyGoalsGrid()
        {
            //Display Data Detail
            PatientIncidentSafetyGoalss = null; //Reset Record Detail
            grdSafetyGoals.DataSource = PatientIncidentSafetyGoalss; //Requery
            grdSafetyGoals.MasterTableView.IsItemInserted = false;
            grdSafetyGoals.MasterTableView.ClearEditItems();
            grdSafetyGoals.DataBind();
        }

        private PatientIncidentSafetyGoals FindSafetyGoals(String id)
        {
            PatientIncidentSafetyGoalsCollection coll = PatientIncidentSafetyGoalss;
            PatientIncidentSafetyGoals retEntity = null;
            foreach (PatientIncidentSafetyGoals rec in coll)
            {
                if (rec.SRSafetyGoals.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdSafetyGoals_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSafetyGoals.DataSource = PatientIncidentSafetyGoalss;
        }

        protected void grdSafetyGoals_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String id =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PatientIncidentSafetyGoalsMetadata.ColumnNames.SRSafetyGoals]);
            PatientIncidentSafetyGoals entity = FindSafetyGoals(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdSafetyGoals_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String id =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][PatientIncidentSafetyGoalsMetadata.ColumnNames.SRSafetyGoals]);
            PatientIncidentSafetyGoals entity = FindSafetyGoals(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdSafetyGoals_InsertCommand(object source, GridCommandEventArgs e)
        {
            PatientIncidentSafetyGoals entity = PatientIncidentSafetyGoalss.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdSafetyGoals.Rebind();
        }

        private void SetEntityValue(PatientIncidentSafetyGoals entity, GridCommandEventArgs e)
        {
            var userControl = (PatientIncidentSafetyGoalsItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRSafetyGoals = userControl.SRSafetyGoals;
                entity.SafetyGoals = userControl.SafetyGoals;
                entity.Recommendation = userControl.Recommendation;
            }
        }

        #endregion

        #region Record Detail Method Function of Patient Incident KTD

        private PatientIncidentKTDCollection PatientIncidentKtds
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPatientIncidentKTD"];
                    if (obj != null)
                    {
                        return ((PatientIncidentKTDCollection)(obj));
                    }
                }

                var coll = new PatientIncidentKTDCollection();
                var query = new PatientIncidentKTDQuery("a");
                var ref1 = new AppStandardReferenceItemQuery("b");

                query.Select
                    (
                        query,
                        ref1.ItemName.As("refToAppStandardReferenceItem_IncidentKtd")

                    );
                query.InnerJoin(ref1).On(query.SRIncidentKTD == ref1.ItemID &&
                                         ref1.StandardReferenceID ==
                                         AppEnum.StandardReference.IncidentProbabilityFrequency);
                query.Where(query.PatientIncidentNo == txtPatientIncidentNo.Text);
                coll.Load(query);

                Session["collPatientIncidentKTD"] = coll;
                return coll;
            }
            set
            {
                Session["collPatientIncidentKTD"] = value;
            }
        }

        private void RefreshCommandItemKtd(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdKtd.Columns[0].Visible = isVisible;
            grdKtd.Columns[grdKtd.Columns.Count - 1].Visible = isVisible;

            grdKtd.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdKtd.Rebind();
        }

        private void PopulateKtdGrid()
        {
            //Display Data Detail
            PatientIncidentKtds = null; //Reset Record Detail
            grdKtd.DataSource = PatientIncidentKtds; //Requery
            grdKtd.MasterTableView.IsItemInserted = false;
            grdKtd.MasterTableView.ClearEditItems();
            grdKtd.DataBind();
        }

        private PatientIncidentKTD FindKtd(String ktdId)
        {
            PatientIncidentKTDCollection coll = PatientIncidentKtds;
            PatientIncidentKTD retEntity = null;
            foreach (PatientIncidentKTD rec in coll)
            {
                if (rec.SRIncidentKTD.Equals(ktdId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdKtd_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdKtd.DataSource = PatientIncidentKtds;
        }

        protected void grdKtd_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String ktdId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PatientIncidentKTDMetadata.ColumnNames.SRIncidentKTD]);
            PatientIncidentKTD entity = FindKtd(ktdId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdKtd_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String ktdId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][PatientIncidentKTDMetadata.ColumnNames.SRIncidentKTD]);
            PatientIncidentKTD entity = FindKtd(ktdId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdKtd_InsertCommand(object source, GridCommandEventArgs e)
        {
            PatientIncidentKTD entity = PatientIncidentKtds.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdKtd.Rebind();
        }

        private void SetEntityValue(PatientIncidentKTD entity, GridCommandEventArgs e)
        {
            var userControl = (PatientIncidentKtdItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRIncidentKTD = userControl.SRIncidentKTD;
                entity.IncidentKTD = userControl.IncidentKTD;
                entity.IncidentKTDName = string.Empty;
            }
        }

        #endregion
    }
}

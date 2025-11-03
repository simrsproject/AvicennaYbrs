using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class RiskManagementDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 300;
            // Url Search & List
            UrlPageSearch = "RiskManagementSearch.aspx";
            UrlPageList = "RiskManagementList.aspx";

            ProgramID = AppConstant.Program.RiskManagement;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnit(cboServiceUnitIDInCharge, true);

                StandardReference.InitializeIncludeSpace(cboSRIncidentGroup, AppEnum.StandardReference.IncidentGroup);
                StandardReference.InitializeIncludeSpace(cboSRClinicalImpact, AppEnum.StandardReference.ClinicalImpact);
                StandardReference.InitializeIncludeSpace(cboSRProbabilityFrequency, AppEnum.StandardReference.IncidentProbabilityFrequency);
                StandardReference.InitializeIncludeSpace(cboSRHandledBy, AppEnum.StandardReference.IncidentHandledBy);
                StandardReference.InitializeIncludeSpace(cboSRIncidentFollowUp, AppEnum.StandardReference.IncidentFollowUp);
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {

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
            var entity = new PatientIncident();
            if (entity.LoadByPrimaryKey(txtPatientIncidentNo.Text))
            {
                if (!IsApproved(entity, args))
                    return;

                entity.MarkAsDeleted();

                SaveEntity(entity);
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new PatientIncident();
            entity.AddNew();
            PopulateNewNo();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
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
            using (var trans = new esTransactionScope())
            {
                var entity = new PatientIncident();
                entity.LoadByPrimaryKey(txtPatientIncidentNo.Text);

                entity.IsApproved = true;
                entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;

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

            using (var trans = new esTransactionScope())
            {
                entity.IsApproved = false;
                entity.ApprovedDateTime = null;
                entity.ApprovedByUserID = null;

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
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
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

            ToolBarMenuDelete.Enabled = !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuEdit()
        {
            return txtPatientIncidentNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new PatientIncident();
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
            
            txtReportedBy.Text = pi.ReportedByUserID ?? AppSession.UserLogin.UserID;
            if (!string.IsNullOrEmpty(pi.ServiceUnitIDInCharge))
                cboServiceUnitIDInCharge.SelectedValue = pi.ServiceUnitIDInCharge;
            else
            {
                cboServiceUnitIDInCharge.SelectedValue = string.Empty;
                cboServiceUnitIDInCharge.Text = string.Empty;
            }
            
            txtIncidentDate.SelectedDate = pi.IncidentDateTime.HasValue ? pi.IncidentDateTime.Value : (new DateTime()).NowAtSqlServer();
            txtIncidentTime.SelectedDate = pi.IncidentDateTime.HasValue ? pi.IncidentDateTime.Value : (new DateTime()).NowAtSqlServer();
            txtReportingDate.SelectedDate = pi.ReportingDateTime.HasValue ? pi.ReportingDateTime.Value : (new DateTime()).NowAtSqlServer();
            txtReportingTime.SelectedDate = pi.ReportingDateTime.HasValue ? pi.ReportingDateTime.Value : (new DateTime()).NowAtSqlServer();
            txtIncidentName.Text = pi.IncidentName;

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

            chkIsApproved.Checked = pi.IsApproved ?? false;
        }
        #endregion

        #region Private Method Standard
        private void SetEntityValue(PatientIncident entity)
        {
            entity.PatientIncidentNo = txtPatientIncidentNo.Text;
            entity.RegistrationNo = string.Empty;
            entity.ServiceUnitID = string.Empty;
            entity.RoomID = string.Empty;
            entity.BedID = string.Empty;
            entity.ParamedicID = string.Empty;
            entity.ServiceUnitIncidentLocationID = string.Empty;
            entity.IncidentLocation = string.Empty;
            entity.ServiceUnitIDInCharge = cboServiceUnitIDInCharge.SelectedValue;
            entity.IncidentDateTime = DateTime.Parse(txtIncidentDate.SelectedDate.Value.ToShortDateString() + " " +
                               txtIncidentTime.SelectedDate.Value.ToShortTimeString());
            entity.ReportingDateTime = DateTime.Parse(txtReportingDate.SelectedDate.Value.ToShortDateString() + " " +
                               txtReportingTime.SelectedDate.Value.ToShortTimeString());
            entity.IncidentName = txtIncidentName.Text;
            entity.Chronology = string.Empty;
            entity.SRIncidentType = string.Empty;
            entity.SRIncidentGroup = cboSRIncidentGroup.SelectedValue;
            entity.SRClinicalImpact = cboSRClinicalImpact.SelectedValue;
            entity.SRIncidentProbabilityFrequency = cboSRProbabilityFrequency.SelectedValue;
            entity.Handling = txtHandling.Text;
            entity.SRIncidentHandledBy = cboSRHandledBy.SelectedValue;
            entity.SRIncidentFollowUp = cboSRIncidentFollowUp.SelectedValue;
            //entity.FollowUpDate = ??;
            entity.ReportedByUserID = txtReportedBy.Text;
            entity.SRIncidentOccurredOn = string.Empty;
            entity.IncidentOccurredOnName = string.Empty;
            entity.SRIncidentOccurredInPatientsWith = string.Empty;
            entity.IncidentOccurredInPatientsWithName = string.Empty;
            entity.IsOccurInOtherUnits = false;
            entity.OccurInOtherUnitsNotes = string.Empty;

            entity.NonPatient = true;
            entity.IsRiskManagement = true;

            entity.FirstName = string.Empty;
            entity.MiddleName = string.Empty;
            entity.LastName = string.Empty;
            entity.Sex = string.Empty;
            entity.DateOfBirth = (new DateTime()).NowAtSqlServer();
            entity.Address = string.Empty;

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
        }

        private void SaveEntity(PatientIncident entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

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
                que.Where(que.PatientIncidentNo > txtPatientIncidentNo.Text, que.IsRiskManagement == true);
                que.OrderBy(que.PatientIncidentNo.Ascending);
            }
            else
            {
                que.Where(que.PatientIncidentNo < txtPatientIncidentNo.Text, que.IsRiskManagement == true);
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

            _autoNumber = Helper.GetNewAutoNumber(txtIncidentDate.SelectedDate.Value, AppEnum.AutoNumber.RiskManagement);
            txtPatientIncidentNo.Text = _autoNumber.LastCompleteNumber;
        }
        #endregion

        #region Selected Changed

        protected void txtIncidentDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            PopulateNewNo();
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

        #endregion
    }
}

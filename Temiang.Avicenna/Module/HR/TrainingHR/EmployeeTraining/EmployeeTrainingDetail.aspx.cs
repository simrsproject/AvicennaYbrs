using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.TrainingHR
{
    public partial class EmployeeTrainingDetail : BasePageDetail
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        private bool isUsingProposal;

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = (FormType == string.Empty || FormType == "point") ? "##" : "EmployeeTrainingSearch.aspx?type=" + FormType;
            UrlPageList = (FormType == string.Empty || FormType == "point") ? "EmployeeTrainingHistoryList.aspx?type=" + FormType : "EmployeeTrainingList.aspx?type=" + FormType;

            this.WindowSearch.Height = 400;

            ProgramID = FormType == string.Empty ? AppConstant.Program.EmployeeTraining : (FormType == "point" ? AppConstant.Program.EmployeeTrainingPoint : (FormType == "pps" ? AppConstant.Program.EmployeeTrainingProposal : AppConstant.Program.EmployeeTrainingProposal2)) ;

            if (IsPostBack) return;

            var proposal = new AppProgram();
            if (proposal.LoadByPrimaryKey(AppConstant.Program.EmployeeTrainingProposal.ToString()) && proposal.IsVisible == true && proposal.IsDiscontinue == false)
                isUsingProposal = true;
            else
                isUsingProposal = false;

            trReferenceID.Visible = (FormType == string.Empty || FormType == "point") && isUsingProposal;
            trTrainingPoint.Visible = FormType == "point";

            StandardReference.InitializeIncludeSpace(cboSRActivityType, AppEnum.StandardReference.ActivityType);
            StandardReference.InitializeIncludeSpace(cboSRTrainingFinancingSources, AppEnum.StandardReference.TrainingFinancingSources);
            StandardReference.InitializeIncludeSpace(cboSREmployeeTrainingPointType, AppEnum.StandardReference.EmployeeTrainingPointType);
            StandardReference.InitializeIncludeSpace(cboSREmployeeTrainingDateSeparator, AppEnum.StandardReference.EmployeeTrainingDateSeparator);
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            if ((FormType == string.Empty || FormType == "point") && isUsingProposal)
            {
                ajax.AddAjaxSetting(cboReferenceID, cboReferenceID);
                ajax.AddAjaxSetting(cboReferenceID, grdEmployeeTrainingHistory);
                ajax.AddAjaxSetting(cboReferenceID, txtTrainingName);
                ajax.AddAjaxSetting(cboReferenceID, txtTrainingLocation);
                ajax.AddAjaxSetting(cboReferenceID, txtTrainingOrganizer);
                ajax.AddAjaxSetting(cboReferenceID, txtDateStart);
                ajax.AddAjaxSetting(cboReferenceID, txtDateEnd);
                ajax.AddAjaxSetting(cboReferenceID, txtTimeStart);
                ajax.AddAjaxSetting(cboReferenceID, txtTimeEnd);
                ajax.AddAjaxSetting(cboReferenceID, txtTotalHour);
                ajax.AddAjaxSetting(cboReferenceID, txtPlanningCosts);
                ajax.AddAjaxSetting(cboReferenceID, txtCreditPoint);
                ajax.AddAjaxSetting(cboReferenceID, txtTotalFeeAmount);
                ajax.AddAjaxSetting(cboReferenceID, txtSponsorFee);
                ajax.AddAjaxSetting(cboReferenceID, txtTargetAttendance);
                ajax.AddAjaxSetting(cboReferenceID, txtNotes);
                ajax.AddAjaxSetting(cboReferenceID, chkIsInHouseTraining);
                ajax.AddAjaxSetting(cboReferenceID, chkIsScheduledTraining);

                ajax.AddAjaxSetting(cboReferenceID, cboSRActivityType);
                ajax.AddAjaxSetting(cboReferenceID, cboSRActivitySubType);
                ajax.AddAjaxSetting(cboReferenceID, txtCertificateValidityPeriod);
                ajax.AddAjaxSetting(cboReferenceID, chkIsCommitmentToWork);
                ajax.AddAjaxSetting(cboReferenceID, txtLengthOfService);
                ajax.AddAjaxSetting(cboReferenceID, txtStartServiceDate);
                ajax.AddAjaxSetting(cboReferenceID, txtEndServiceDate);
                ajax.AddAjaxSetting(cboReferenceID, cboSRTrainingFinancingSources);
                ajax.AddAjaxSetting(cboReferenceID, txtEvaluationDate);
                ajax.AddAjaxSetting(cboReferenceID, txtDurationHour);
                ajax.AddAjaxSetting(cboReferenceID, txtDurationMinutes);
                ajax.AddAjaxSetting(cboReferenceID, cboSREmployeeTrainingPointType);

                ajax.AddAjaxSetting(AjaxManager, cboReferenceID);
                ajax.AddAjaxSetting(AjaxManager, grdEmployeeTrainingHistory);
                ajax.AddAjaxSetting(AjaxManager, txtTrainingName);
                ajax.AddAjaxSetting(AjaxManager, txtTrainingLocation);
                ajax.AddAjaxSetting(AjaxManager, txtTrainingOrganizer);
                ajax.AddAjaxSetting(AjaxManager, txtDateStart);
                ajax.AddAjaxSetting(AjaxManager, txtDateEnd);
                ajax.AddAjaxSetting(AjaxManager, txtTimeStart);
                ajax.AddAjaxSetting(AjaxManager, txtTimeEnd);
                ajax.AddAjaxSetting(AjaxManager, txtTotalHour);
                ajax.AddAjaxSetting(AjaxManager, txtPlanningCosts);
                ajax.AddAjaxSetting(AjaxManager, txtCreditPoint);
                ajax.AddAjaxSetting(AjaxManager, txtTotalFeeAmount);
                ajax.AddAjaxSetting(AjaxManager, txtSponsorFee);
                ajax.AddAjaxSetting(AjaxManager, txtTargetAttendance);
                ajax.AddAjaxSetting(AjaxManager, txtNotes);
                ajax.AddAjaxSetting(AjaxManager, chkIsInHouseTraining);
                ajax.AddAjaxSetting(AjaxManager, chkIsScheduledTraining);

                ajax.AddAjaxSetting(AjaxManager, cboSRActivityType);
                ajax.AddAjaxSetting(AjaxManager, cboSRActivitySubType);
                ajax.AddAjaxSetting(AjaxManager, txtCertificateValidityPeriod);
                ajax.AddAjaxSetting(AjaxManager, chkIsCommitmentToWork);
                ajax.AddAjaxSetting(AjaxManager, txtLengthOfService);
                ajax.AddAjaxSetting(AjaxManager, txtStartServiceDate);
                ajax.AddAjaxSetting(AjaxManager, txtEndServiceDate);
                ajax.AddAjaxSetting(AjaxManager, cboSRTrainingFinancingSources);
                ajax.AddAjaxSetting(AjaxManager, txtEvaluationDate);
                ajax.AddAjaxSetting(AjaxManager, txtDurationHour);
                ajax.AddAjaxSetting(AjaxManager, txtDurationMinutes);
                ajax.AddAjaxSetting(AjaxManager, cboSREmployeeTrainingPointType);

                ajax.AddAjaxSetting(txtDateStart, txtEvaluationDate);
                ajax.AddAjaxSetting(cboSRActivityType, cboSRActivitySubType);
            }
        }

        private void AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            PopulateFromProposal();
        }

        private void PopulateFromProposal()
        {

            var pps = new EmployeeTraining();
            if (pps.LoadByPrimaryKey(Request.QueryString["refid"].ToInt()))
            {
                var query = new EmployeeTrainingQuery();
                query.Where(query.EmployeeTrainingID == pps.EmployeeTrainingID);
                cboReferenceID.DataSource = query.LoadDataTable();
                cboReferenceID.DataBind();
                cboReferenceID.SelectedValue = pps.EmployeeTrainingID.ToString();

                cboReferenceID.Enabled = false;

                txtTrainingName.Text = pps.EmployeeTrainingName;
                txtTrainingLocation.Text = pps.TrainingLocation;
                txtTrainingOrganizer.Text = pps.TrainingOrganizer;
                txtDateStart.SelectedDate = pps.StartDate ?? DateTime.Now.Date;
                txtDateEnd.SelectedDate = pps.EndDate ?? DateTime.Now.Date;
                var start = new DateTime(2000, 1, 1,
                    pps.StartTime.HasValue ? pps.StartTime.Value.Hours : DateTime.Now.Hour,
                    pps.StartTime.HasValue ? pps.StartTime.Value.Minutes : DateTime.Now.Minute,
                    pps.StartTime.HasValue ? pps.StartTime.Value.Seconds : 0);
                txtTimeStart.SelectedDate = start;
                var end = new DateTime(2000, 1, 1,
                    pps.EndTime.HasValue ? pps.EndTime.Value.Hours : DateTime.Now.Hour,
                    pps.EndTime.HasValue ? pps.EndTime.Value.Minutes : DateTime.Now.Minute,
                    pps.EndTime.HasValue ? pps.EndTime.Value.Seconds : 0);
                txtTimeEnd.SelectedDate = end;
                txtTotalHour.Value = pps.TotalHour ?? 0;
                txtCreditPoint.Value = Convert.ToDouble(pps.CreditPoint ?? 0);
                txtPlanningCosts.Value = Convert.ToDouble(pps.PlanningCosts ?? 0);
                txtTotalFeeAmount.Value = Convert.ToDouble(pps.TrainingFee ?? 0);
                txtSponsorFee.Value = Convert.ToDouble(pps.SponsorFee ?? 0);
                txtTargetAttendance.Value = pps.TargetAttendance ?? 0;
                txtNotes.Text = pps.Note;
                chkIsInHouseTraining.Checked = pps.IsInHouseTraining ?? false;
                chkIsScheduledTraining.Checked = pps.IsScheduledTraining ?? false;

                cboSRActivityType.SelectedValue = pps.SRActivityType;
                if (!string.IsNullOrEmpty(pps.SRActivitySubType))
                {
                    var subtype = new AppStandardReferenceItemQuery();
                    subtype.Where(subtype.StandardReferenceID == AppEnum.StandardReference.ActivitySubType.ToString(), subtype.ItemID == pps.SRActivitySubType);
                    cboSRActivitySubType.DataSource = subtype.LoadDataTable();
                    cboSRActivitySubType.DataBind();
                    cboSRActivitySubType.SelectedValue = pps.SRActivitySubType;
                }
                else
                {
                    cboSRActivitySubType.Items.Clear();
                    cboSRActivityType.SelectedValue = string.Empty;
                    cboSRActivityType.Text = string.Empty;
                }

                if (pps.CertificateValidityPeriod != null)
                    txtCertificateValidityPeriod.SelectedDate = pps.CertificateValidityPeriod;
                else
                    txtCertificateValidityPeriod.Clear();

                chkIsCommitmentToWork.Checked = pps.IsCommitmentToWork ?? false;
                txtLengthOfService.Value = Convert.ToDouble(pps.LengthOfService);

                if (pps.StartServiceDate != null)
                    txtStartServiceDate.SelectedDate = pps.StartServiceDate;
                else
                    txtStartServiceDate.Clear();

                if (pps.EndServiceDate != null)
                    txtEndServiceDate.SelectedDate = pps.EndServiceDate;
                else
                    txtEndServiceDate.Clear();

                cboSRTrainingFinancingSources.SelectedValue = pps.SRTrainingFinancingSources;

                if (pps.EvaluationDate != null)
                    txtEvaluationDate.SelectedDate = pps.EvaluationDate;
                else
                    txtEvaluationDate.Clear();

                cboSREmployeeTrainingPointType.SelectedValue = pps.SREmployeeTrainingPointType;
                txtDurationHour.Value = Convert.ToDouble(pps.DurationHour);
                txtDurationMinutes.Value = Convert.ToDouble(pps.DurationMinutes);

                var ppsi = new EmployeeTrainingHistoryCollection();
                ppsi.Query.Where(ppsi.Query.EmployeeTrainingID == pps.EmployeeTrainingID);
                ppsi.LoadAll();

                foreach (var i in ppsi)
                {
                    var eth = EmployeeTrainingHistorys.AddNew();
                    eth.PersonID = i.PersonID;
                    eth.EventName = i.EventName;
                    eth.EmployeeTrainingID = Convert.ToInt32(txtTrainingID.Value);

                    var p = new PersonalInfo();
                    if (p.LoadByPrimaryKey(eth.PersonID.ToInt()))
                    {
                        eth.EmployeeName = p.EmployeeName;
                    }

                    eth.IsAttending = i.IsAttending;
                    eth.Note = i.Note;

                    eth.StartDate = pps.StartDate;
                    eth.EndDate = pps.EndDate;
                    eth.TrainingLocation = pps.TrainingLocation;
                    eth.TrainingInstitution = pps.TrainingOrganizer;
                    eth.Fee = pps.TrainingFee;
                    eth.SponsorFee = pps.SponsorFee;
                    eth.PlanningCosts = pps.PlanningCosts;

                    eth.TotalHour = pps.TotalHour;
                    eth.CreditPoint = pps.CreditPoint;
                    eth.IsInHouseTraining = pps.IsInHouseTraining;
                    eth.IsScheduledTraining = pps.IsScheduledTraining;

                    eth.SRActivityType = pps.SRActivityType;
                    eth.SRActivitySubType = pps.SRActivitySubType;
                    eth.CertificateValidityPeriod = pps.CertificateValidityPeriod;
                    eth.IsCommitmentToWork = pps.IsCommitmentToWork;
                    eth.LengthOfService = pps.LengthOfService;
                    eth.StartServiceDate = pps.StartServiceDate;
                    eth.EndServiceDate = pps.EndServiceDate;
                    eth.SRTrainingFinancingSources = pps.SRTrainingFinancingSources;
                    eth.EvaluationDate = pps.EvaluationDate;

                    eth.SREmployeeTrainingPointType = pps.SREmployeeTrainingPointType;
                    eth.DurationHour = pps.DurationHour;
                    eth.DurationMinutes = pps.DurationMinutes;

                    eth.SREmployeeTrainingDateSeparator = pps.SREmployeeTrainingDateSeparator;
                    eth.SREmployeeTrainingRole = i.SREmployeeTrainingRole;

                    if (!string.IsNullOrEmpty(i.SREmployeeTrainingRole))
                    {
                        var role = new AppStandardReferenceItem();
                        if (role.LoadByPrimaryKey(AppEnum.StandardReference.EmployeeTrainingRole.ToString(), i.SREmployeeTrainingRole))
                            eth.EmployeeTrainingRoleName = role.ItemName;
                        else
                            eth.EmployeeTrainingRoleName = string.Empty;
                    }
                    else
                        eth.EmployeeTrainingRoleName = string.Empty;
                }

                grdEmployeeTrainingHistory.DataSource = EmployeeTrainingHistorys;
                grdEmployeeTrainingHistory.DataBind();

                var trainers = new EmployeeTrainingExternalTrainerCollection();
                trainers.Query.Where(trainers.Query.EmployeeTrainingID == pps.EmployeeTrainingID);
                trainers.LoadAll();

                foreach (var t in trainers)
                {
                    var etet = EmployeeTrainingExternalTrainers.AddNew();
                    etet.EmployeeTrainingID = Convert.ToInt32(txtTrainingID.Value);
                    etet.ExternalTrainerSeqNo = t.ExternalTrainerSeqNo;
                    etet.ExternalTrainerName = t.ExternalTrainerName;
                    etet.PositionAs = t.PositionAs;
                    etet.Notes = t.Notes;
                }

                grdExternalTrainer.DataSource = EmployeeTrainingExternalTrainers;
                grdExternalTrainer.DataBind();

                var eti = new EmployeeTrainingItemCollection();
                eti.Query.Where(eti.Query.EmployeeTrainingID == pps.EmployeeTrainingID);
                eti.LoadAll();

                foreach (var t in eti)
                {
                    var etii = EmployeeTrainingItems.AddNew();
                    etii.EmployeeTrainingID = txtTrainingID.Value == null ? -2 : Convert.ToInt32(txtTrainingID.Value);
                    etii.PersonID = t.PersonID;
                    etii.SRComponentID = t.SRComponentID;

                    var cm = new AppStandardReferenceItem();
                    cm.LoadByPrimaryKey(AppEnum.StandardReference.EmployeeTrainingComponent.ToString(), t.SRComponentID);

                    etii.ComponentName = cm.ItemName; 
                    etii.Price = t.Price;
    
                }
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (FormType == string.Empty || FormType == "point")
            {
                ToolBarMenuSearch.Enabled = false;
                //ToolBarMenuAdd.Enabled = false;
            }
        }

        protected override void OnMenuEditClick()
        {
            if (FormType == string.Empty || FormType == "point")
            {
                cboReferenceID.Enabled = false;
            }
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new EmployeeTraining());

            txtTrainingID.Value = -2;
            chkIsActive.Checked = true;

            if (!(FormType == string.Empty  && !string.IsNullOrEmpty(Request.QueryString["refid"])))
            {
                txtDateStart.SelectedDate = DateTime.Now.Date;
            txtDateEnd.SelectedDate = DateTime.Now.Date;
                txtTotalHour.Value = 0;
                txtCreditPoint.Value = 0;
                txtTotalFeeAmount.Value = 0;
                txtSponsorFee.Value = 0;
                txtTargetAttendance.Value = 0;

                txtLengthOfService.Value = 0;
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            if (FormType != string.Empty && FormType != "point")
            {
                var pps = new EmployeeTrainingCollection();
                pps.Query.Where(pps.Query.ReferenceID == Convert.ToInt32(txtTrainingID.Text));
                pps.LoadAll();
                if (pps.Count > 0)
                {
                    args.MessageText = AppConstant.Message.RecordHasUsed;
                    args.IsCancel = true;
                    return;
                }
            }
            if (FormType != string.Empty && !string.IsNullOrEmpty(cboSREmployeeTrainingPointType.SelectedValue))
            {
                args.MessageText = "There is already a point determination for this training.";
                args.IsCancel = true;
                return;
            }

            var entity = new EmployeeTraining();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtTrainingID.Text)))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new EmployeeTraining();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new EmployeeTraining();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtTrainingID.Text)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
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
            auditLogFilter.PrimaryKeyData = string.Format("EmployeeTrainingID='{0}'", txtTrainingID.Text.Trim());
            auditLogFilter.TableName = "EmployeeTraining";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_EmployeeTrainingID", txtTrainingID.Text);
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            RefreshCommandItemEmployeeTrainingHistory(newVal);
            RefreshCommandItemEmployeeTrainingExternalTrainer(newVal);

            if (FormType == "point")
            {
                cboReferenceID.Enabled = false;
                txtTrainingName.ReadOnly = true;
                cboSRActivityType.Enabled = false;
                txtTrainingLocation.ReadOnly = true;
                txtTrainingOrganizer.ReadOnly = true;
                txtDateStart.Enabled = false;
                txtDateEnd.Enabled = false;
                txtTimeStart.Enabled = false;
                txtTimeEnd.Enabled = false;
                txtTotalHour.ReadOnly = true;
                txtDurationHour.ReadOnly = true;
                txtDurationMinutes.ReadOnly = true;
                txtCreditPoint.ReadOnly = true;
                cboSRTrainingFinancingSources.Enabled = false;
                txtTotalFeeAmount.ReadOnly = true;
                txtSponsorFee.ReadOnly = true;
                txtTargetAttendance.ReadOnly = true;
                txtNotes.ReadOnly = true;
                chkIsInHouseTraining.Enabled = false;
                chkIsScheduledTraining.Enabled = false;
                txtCertificateValidityPeriod.Enabled = false;
                chkIsCommitmentToWork.Enabled = false;
                txtLengthOfService.ReadOnly = true;
                txtStartServiceDate.Enabled = false;
                txtEndServiceDate.Enabled = false;
                txtEvaluationDate.Enabled = false;
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new EmployeeTraining();
            if (parameters.Length > 0)
            {
                string positionID = (string)parameters[0];
                if (!parameters[0].Equals(string.Empty)) entity.LoadByPrimaryKey(Convert.ToInt32(positionID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtTrainingID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var training = (EmployeeTraining)entity;
            txtTrainingID.Value = training.EmployeeTrainingID;
            txtTrainingName.Text = training.EmployeeTrainingName;
            txtTrainingLocation.Text = training.TrainingLocation;
            txtTrainingOrganizer.Text = training.TrainingOrganizer;
            txtDateStart.SelectedDate = training.StartDate ?? DateTime.Now.Date;
            txtDateEnd.SelectedDate = training.EndDate ?? DateTime.Now.Date;
            var start = new DateTime(2000, 1, 1,
                training.StartTime.HasValue ? training.StartTime.Value.Hours : DateTime.Now.Hour,
                training.StartTime.HasValue ? training.StartTime.Value.Minutes : DateTime.Now.Minute,
                training.StartTime.HasValue ? training.StartTime.Value.Seconds : 0);
            txtTimeStart.SelectedDate = start;
            var end = new DateTime(2000, 1, 1,
                training.EndTime.HasValue ? training.EndTime.Value.Hours : DateTime.Now.Hour,
                training.EndTime.HasValue ? training.EndTime.Value.Minutes : DateTime.Now.Minute,
                training.EndTime.HasValue ? training.EndTime.Value.Seconds : 0);
            txtTimeEnd.SelectedDate = end;
            txtTotalHour.Value = training.TotalHour ?? 0;
            txtCreditPoint.Value = Convert.ToDouble(training.CreditPoint ?? 0);
            txtPlanningCosts.Value = Convert.ToDouble(training.PlanningCosts ?? 0);
            txtTotalFeeAmount.Value = Convert.ToDouble(training.TrainingFee ?? 0);
            txtSponsorFee.Value = Convert.ToDouble(training.SponsorFee ?? 0);
            txtTargetAttendance.Value = training.TargetAttendance ?? 0;
            txtNotes.Text = training.Note;
            chkIsInHouseTraining.Checked = training.IsInHouseTraining ?? false;
            chkIsScheduledTraining.Checked = training.IsScheduledTraining ?? false;
            chkIsActive.Checked = training.IsActive ?? true;

            cboSRActivityType.SelectedValue = training.SRActivityType;
            if (!string.IsNullOrEmpty(training.SRActivitySubType))
            {
                var subtype = new AppStandardReferenceItemQuery();
                subtype.Where(subtype.StandardReferenceID == AppEnum.StandardReference.ActivitySubType.ToString(), subtype.ItemID == training.SRActivitySubType);
                cboSRActivitySubType.DataSource = subtype.LoadDataTable();
                cboSRActivitySubType.DataBind();
                cboSRActivitySubType.SelectedValue = training.SRActivitySubType;
            }
            else
            {
                cboSRActivitySubType.Items.Clear();
                cboSRActivitySubType.SelectedValue = string.Empty;
                cboSRActivitySubType.Text = string.Empty;
            }

            if (training.CertificateValidityPeriod != null)
                txtCertificateValidityPeriod.SelectedDate = training.CertificateValidityPeriod;
            else
                txtCertificateValidityPeriod.Clear();

            chkIsCommitmentToWork.Checked = training.IsCommitmentToWork ?? false;
            txtLengthOfService.Value = Convert.ToDouble(training.LengthOfService);

            if (training.StartServiceDate != null)
                txtStartServiceDate.SelectedDate = training.StartServiceDate;
            else
                txtStartServiceDate.Clear();

            if (training.EndServiceDate != null)
                txtEndServiceDate.SelectedDate = training.EndServiceDate;
            else
                txtEndServiceDate.Clear();

            cboSRTrainingFinancingSources.SelectedValue = training.SRTrainingFinancingSources;

            if (training.EvaluationDate != null)
                txtEvaluationDate.SelectedDate = training.EvaluationDate;
            else
                txtEvaluationDate.Clear();

            cboSREmployeeTrainingPointType.SelectedValue = training.SREmployeeTrainingPointType;
            txtDurationHour.Value = Convert.ToDouble(training.DurationHour);
            txtDurationMinutes.Value = Convert.ToDouble(training.DurationMinutes);
            cboSREmployeeTrainingDateSeparator.SelectedValue = string.IsNullOrEmpty(training.SREmployeeTrainingDateSeparator) ? "-" : training.SREmployeeTrainingDateSeparator;

            if ((FormType == string.Empty || FormType == "point") && training.ReferenceID != null && training.ReferenceID != -1)
            {
                var query = new EmployeeTrainingQuery();
                query.Where(query.EmployeeTrainingID == training.ReferenceID);
                cboReferenceID.DataSource = query.LoadDataTable();
                cboReferenceID.DataBind();
                cboReferenceID.SelectedValue = training.ReferenceID.ToString();
            }
            else
            {
                cboReferenceID.Items.Clear();
                cboReferenceID.Text = string.Empty;
                cboReferenceID.SelectedValue = string.Empty;
            }

            if ((FormType == string.Empty || FormType == "point") && !string.IsNullOrEmpty(Request.QueryString["refid"]) && DataModeCurrent == AppEnum.DataMode.New)
            {
                EmployeeTrainingHistorys = null;
                EmployeeTrainingExternalTrainers = null;
                EmployeeTrainingItems = null;
                PopulateFromProposal();
            }
            else
            {
                PopulateEmployeeTrainingHistoryGrid();
                PopulateEmployeeTrainingExternalTrainerGrid();

                EmployeeTrainingItems = null;
                var eti = EmployeeTrainingItems;
            }
        }

        private void SetEntityValue(EmployeeTraining training)
        {
            training.EmployeeTrainingID = Convert.ToInt32(txtTrainingID.Value);
            training.EmployeeTrainingName = txtTrainingName.Text;
            training.TrainingLocation = txtTrainingLocation.Text;
            training.TrainingOrganizer = txtTrainingOrganizer.Text;
            training.StartDate = txtDateStart.SelectedDate;
            training.EndDate = txtDateEnd.SelectedDate;
            training.StartTime = TimeSpan.Parse(txtTimeStart.SelectedDate.Value.ToString("HH:mm"));
            training.EndTime = TimeSpan.Parse(txtTimeEnd.SelectedDate.Value.ToString("HH:mm"));
            training.TotalHour = Convert.ToInt32(txtTotalHour.Value);
            training.CreditPoint = Convert.ToDecimal(txtCreditPoint.Value);
            training.PlanningCosts = Convert.ToDecimal(txtPlanningCosts.Value);
            training.TrainingFee = Convert.ToDecimal(txtTotalFeeAmount.Value);
            training.SponsorFee = Convert.ToDecimal(txtSponsorFee.Value);
            training.TargetAttendance = Convert.ToInt32(txtTargetAttendance.Value);
            training.Note = txtNotes.Text;
            training.IsInHouseTraining = chkIsInHouseTraining.Checked;
            training.IsScheduledTraining = chkIsScheduledTraining.Checked;
            training.IsActive = chkIsActive.Checked;
            training.ReferenceID = string.IsNullOrEmpty(cboReferenceID.SelectedValue) ? -1 : cboReferenceID.SelectedValue.ToInt();
            training.IsProposal = FormType.Contains("pps");

            training.SRActivityType = cboSRActivityType.SelectedValue;
            training.SRActivitySubType = cboSRActivitySubType.SelectedValue;
            if (!txtCertificateValidityPeriod.IsEmpty)
                training.CertificateValidityPeriod = txtCertificateValidityPeriod.SelectedDate;
            else
                training.str.CertificateValidityPeriod = string.Empty;

            training.IsCommitmentToWork = chkIsCommitmentToWork.Checked;
            training.LengthOfService = Convert.ToInt16(txtLengthOfService.Value);
            
            if (!txtStartServiceDate.IsEmpty)
                training.StartServiceDate = txtStartServiceDate.SelectedDate;
            else
                training.str.StartServiceDate = string.Empty;
            
            if (!txtEndServiceDate.IsEmpty)
                training.EndServiceDate = txtEndServiceDate.SelectedDate;
            else
                training.str.EndServiceDate = string.Empty;

            training.SRTrainingFinancingSources = cboSRTrainingFinancingSources.SelectedValue;

            if (!txtEvaluationDate.IsEmpty)
                training.EvaluationDate = txtEvaluationDate.SelectedDate;
            else
                training.str.EvaluationDate = string.Empty;

            training.SREmployeeTrainingPointType = cboSREmployeeTrainingPointType.SelectedValue;
            training.DurationHour = Convert.ToDecimal(txtDurationHour.Value);
            training.DurationMinutes = Convert.ToDecimal(txtDurationMinutes.Value);
            training.SREmployeeTrainingDateSeparator = cboSREmployeeTrainingDateSeparator.SelectedValue;

            //Last Update Status
            if (training.es.IsAdded || training.es.IsModified)
            {
                training.LastUpdateByUserID = AppSession.UserLogin.UserID;
                training.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(EmployeeTraining entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                foreach (var trn in EmployeeTrainingHistorys)
                {
                    trn.EmployeeTrainingID = entity.EmployeeTrainingID;
                    trn.EventName = entity.EmployeeTrainingName;
                    trn.StartDate = entity.StartDate;
                    trn.EndDate = entity.EndDate;
                    trn.TrainingLocation = entity.TrainingLocation;
                    trn.TrainingInstitution = entity.TrainingOrganizer;
                    trn.PlanningCosts = Convert.ToDecimal(txtPlanningCosts.Value);
                    trn.Fee = Convert.ToDecimal(txtTotalFeeAmount.Value);
                    trn.SponsorFee = Convert.ToDecimal(txtSponsorFee.Value);
                    trn.TotalHour = entity.TotalHour;
                    trn.CreditPoint = entity.CreditPoint;
                    trn.IsInHouseTraining = entity.IsInHouseTraining;
                    trn.IsScheduledTraining = entity.IsScheduledTraining;

                    trn.SRActivityType = entity.SRActivityType;
                    trn.SRActivitySubType = entity.SRActivitySubType;
                    trn.CertificateValidityPeriod = entity.CertificateValidityPeriod;
                    trn.IsCommitmentToWork = entity.IsCommitmentToWork;
                    trn.LengthOfService = entity.LengthOfService;
                    trn.StartServiceDate = entity.StartServiceDate;
                    trn.EndServiceDate = entity.EndServiceDate;
                    trn.SRTrainingFinancingSources = entity.SRTrainingFinancingSources;
                    trn.EvaluationDate = entity.EvaluationDate;

                    trn.SREmployeeTrainingPointType = entity.SREmployeeTrainingPointType;
                    trn.DurationHour = entity.DurationHour;
                    trn.DurationMinutes = entity.DurationMinutes;
                    trn.SREmployeeTrainingDateSeparator = entity.SREmployeeTrainingDateSeparator;

                    trn.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    trn.LastUpdateDateTime = DateTime.Now;
                }

                EmployeeTrainingHistorys.Save();

                foreach (var trainer in EmployeeTrainingExternalTrainers)
                {
                    trainer.EmployeeTrainingID = entity.EmployeeTrainingID;
                    trainer.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    trainer.LastUpdateDateTime = DateTime.Now;
                }

                EmployeeTrainingExternalTrainers.Save();

                foreach (var item in EmployeeTrainingItems)
                {
                    item.EmployeeTrainingID = entity.EmployeeTrainingID;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
                EmployeeTrainingItems.Save();
                
                //Commit if success, Rollback if failed
                trans.Complete();

                txtTrainingID.Value = entity.EmployeeTrainingID;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new EmployeeTrainingQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.EmployeeTrainingID > txtTrainingID.Value);
                que.OrderBy(que.EmployeeTrainingID.Ascending);
            }
            else
            {
                que.Where(que.EmployeeTrainingID < txtTrainingID.Text);
                que.OrderBy(que.EmployeeTrainingID.Descending);
            }
            if (FormType == string.Empty || FormType == "point")
                que.Where(que.IsProposal == false);
            else
                que.Where(que.IsProposal == true, que.LastUpdateByUserID == AppSession.UserLogin.UserID);

            var entity = new EmployeeTraining();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region EmployeeTrainingHistory
        private void RefreshCommandItemEmployeeTrainingHistory(AppEnum.DataMode newVal)
        {
            ////Toogle grid command

            bool isVisible = (newVal != AppEnum.DataMode.Read) && (FormType != "point") ;
            grdEmployeeTrainingHistory.Columns[0].Visible = isVisible;
            grdEmployeeTrainingHistory.Columns.FindByUniqueName("listDetailEdit").Visible = isVisible;
            grdEmployeeTrainingHistory.Columns.FindByUniqueName("listDetailView").Visible = !isVisible;


            if ((grdEmployeeTrainingHistory.Columns.FindByUniqueName("listDetailEdit").Visible = !isVisible) )
            {

                grdEmployeeTrainingHistory.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
                grdEmployeeTrainingHistory.Columns.FindByUniqueName("listDetailView").Visible = false;
            }
            else
            {
                //grdEmployeeTrainingHistory.Columns[grdEmployeeTrainingHistory.Columns.Count - 1].Visible = isVisible;
                grdEmployeeTrainingHistory.Columns.FindByUniqueName("listDetailView").Visible = isVisible;
                grdEmployeeTrainingHistory.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            }



            ////Perbaharui tampilan dan data
            grdEmployeeTrainingHistory.Rebind();
        }

        private EmployeeTrainingHistoryCollection EmployeeTrainingHistorys
        {
            get
            {
                object obj = Session["collEmployeeTrainingHistory" + Request.UserHostName];
                if (obj != null)
                    return ((EmployeeTrainingHistoryCollection)(obj));

                var coll = new EmployeeTrainingHistoryCollection();

                var query = new EmployeeTrainingHistoryQuery("a");
                var personal = new PersonalInfoQuery("b");
                var hd = new EmployeeTrainingQuery("c");
                var role = new AppStandardReferenceItemQuery("d");

                query.Select
                    (
                       query,
                       personal.EmployeeName.As("refToPersonalInfo_EmployeeName"),
                       role.ItemName.As("refToStdRef_EmployeeTrainingRole")
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.LeftJoin(hd).On(hd.EmployeeTrainingID == query.EmployeeTrainingID);
                query.LeftJoin(role).On(role.StandardReferenceID == AppEnum.StandardReference.EmployeeTrainingRole.ToString() && role.ItemID == query.SREmployeeTrainingRole);

                query.Where(query.EmployeeTrainingID == Convert.ToInt32(txtTrainingID.Value == null ? -2 : txtTrainingID.Value)); //TODO: Betulkan parameternya
                query.OrderBy(query.EmployeeTrainingHistoryID.Ascending); //TODO: Betulkan ordernya

                coll.Load(query);

                Session["collEmployeeTrainingHistory" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collEmployeeTrainingHistory" + Request.UserHostName] = value; }
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
            var ds = from d in EmployeeTrainingHistorys
                     where d.IsProposal.Equals(false)
                     select d;
            grdEmployeeTrainingHistory.DataSource = ds;
        }

        protected void grdEmployeeTrainingHistory_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 personId = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeTrainingHistoryMetadata.ColumnNames.PersonID]);

            EmployeeTrainingHistory entity = FindEmployeeTrainingHistory(personId);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdEmployeeTrainingHistory_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 personId = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeTrainingHistoryMetadata.ColumnNames.PersonID]);

            EmployeeTrainingHistory entity = FindEmployeeTrainingHistory(personId);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdEmployeeTrainingHistory_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeTrainingHistory entity = EmployeeTrainingHistorys.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdEmployeeTrainingHistory.Rebind();
        }

        private EmployeeTrainingHistory FindEmployeeTrainingHistory(Int32 personId)
        {
            EmployeeTrainingHistoryCollection coll = EmployeeTrainingHistorys;
            EmployeeTrainingHistory retEntity = null;
            foreach (EmployeeTrainingHistory rec in coll)
            {
                if (rec.PersonID.Equals(personId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(EmployeeTrainingHistory entity, GridCommandEventArgs e)
        {
            var userControl = (EmployeeTrainingAttendanceDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.EmployeeTrainingHistoryID = userControl.EmployeeTrainingHistoryID;
                entity.PersonID = userControl.PersonID;
                entity.EventName = txtTrainingName.Text;
                entity.EmployeeTrainingID = txtTrainingID.Text.ToInt();
                entity.EmployeeName = userControl.EmployeeName;
                entity.IsAttending = userControl.IsAttending;
                entity.Note = userControl.Note;
                entity.SREmployeeTrainingRole = userControl.SREmployeeTrainingRole;
                entity.EmployeeTrainingRoleName = userControl.EmployeeTrainingRoleName;

                entity.StartDate = txtDateStart.SelectedDate;
                entity.EndDate = txtDateEnd.SelectedDate;
                entity.TrainingLocation = txtTrainingLocation.Text;
                entity.TrainingInstitution = txtTrainingOrganizer.Text;
                entity.Fee = Convert.ToDecimal(txtTotalFeeAmount.Value);
                entity.SponsorFee = Convert.ToDecimal(txtSponsorFee.Value);
                entity.TotalHour = Convert.ToInt32(txtTotalHour.Value);
                entity.CreditPoint = Convert.ToDecimal(txtCreditPoint.Value);
                entity.IsInHouseTraining = chkIsInHouseTraining.Checked;
                entity.IsScheduledTraining = chkIsScheduledTraining.Checked;

                entity.SRActivityType = cboSRActivityType.SelectedValue;
                if (txtCertificateValidityPeriod.IsEmpty)
                    entity.str.CertificateValidityPeriod = string.Empty;
                else
                    entity.CertificateValidityPeriod = txtCertificateValidityPeriod.SelectedDate;
                entity.IsCommitmentToWork = chkIsCommitmentToWork.Checked;
                entity.LengthOfService = Convert.ToInt16(txtLengthOfService.Value);

                if (txtStartServiceDate.IsEmpty)
                    entity.str.StartServiceDate = string.Empty;
                else
                    entity.StartServiceDate = txtStartServiceDate.SelectedDate;

                if (txtEndServiceDate.IsEmpty)
                    entity.str.EndServiceDate = string.Empty;
                else
                    entity.EndServiceDate = txtEndServiceDate.SelectedDate;

                entity.SRTrainingFinancingSources = cboSRTrainingFinancingSources.SelectedValue;

                if (txtEvaluationDate.IsEmpty)
                    entity.str.EvaluationDate = string.Empty;
                else
                    entity.EvaluationDate = txtEvaluationDate.SelectedDate;

                entity.SREmployeeTrainingPointType = cboSREmployeeTrainingPointType.SelectedValue;
                entity.DurationHour = Convert.ToDecimal(txtDurationHour.Value);
                entity.DurationMinutes = Convert.ToDecimal(txtDurationMinutes.Value);
                entity.SREmployeeTrainingDateSeparator = cboSREmployeeTrainingDateSeparator.SelectedValue;
            }
        }
        #endregion

        #region EmployeeTrainingExternalTrainer
        private void RefreshCommandItemEmployeeTrainingExternalTrainer(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read) && (FormType != "point");
            grdExternalTrainer.Columns[0].Visible = isVisible;
            grdExternalTrainer.Columns[grdExternalTrainer.Columns.Count - 1].Visible = isVisible;

            grdExternalTrainer.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdExternalTrainer.Rebind();
        }

        private EmployeeTrainingExternalTrainerCollection EmployeeTrainingExternalTrainers
        {
            get
            {
                object obj = Session["collEmployeeTrainingExternalTrainer" + Request.UserHostName];
                if (obj != null)
                    return ((EmployeeTrainingExternalTrainerCollection)(obj));

                var coll = new EmployeeTrainingExternalTrainerCollection();

                var query = new EmployeeTrainingExternalTrainerQuery("a");
                query.Select
                    (
                       query
                    );

                query.Where(query.EmployeeTrainingID == Convert.ToInt32(txtTrainingID.Value == null ? -2 : txtTrainingID.Value)); //TODO: Betulkan parameternya
                query.OrderBy(query.ExternalTrainerSeqNo.Ascending); //TODO: Betulkan ordernya

                coll.Load(query);

                Session["collEmployeeTrainingExternalTrainer" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collEmployeeTrainingExternalTrainer" + Request.UserHostName] = value; }
        }

        private void PopulateEmployeeTrainingExternalTrainerGrid()
        {
            //Display Data Detail
            EmployeeTrainingExternalTrainers = null; //Reset Record Detail
            grdExternalTrainer.DataSource = EmployeeTrainingExternalTrainers; //Requery
            grdExternalTrainer.MasterTableView.IsItemInserted = false;
            grdExternalTrainer.MasterTableView.ClearEditItems();
            grdExternalTrainer.DataBind();
        }

        protected void grdExternalTrainer_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdExternalTrainer.DataSource = EmployeeTrainingExternalTrainers;
        }

        protected void grdExternalTrainer_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            string seqNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeTrainingExternalTrainerMetadata.ColumnNames.ExternalTrainerSeqNo]);

            EmployeeTrainingExternalTrainer entity = FindEmployeeTrainingExternalTrainer(seqNo);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdExternalTrainer_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            string seqNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeTrainingExternalTrainerMetadata.ColumnNames.ExternalTrainerSeqNo]);

            EmployeeTrainingExternalTrainer entity = FindEmployeeTrainingExternalTrainer(seqNo);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdExternalTrainer_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeTrainingExternalTrainer entity = EmployeeTrainingExternalTrainers.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdExternalTrainer.Rebind();
        }

        private EmployeeTrainingExternalTrainer FindEmployeeTrainingExternalTrainer(string seqNo)
        {
            EmployeeTrainingExternalTrainerCollection coll = EmployeeTrainingExternalTrainers;
            EmployeeTrainingExternalTrainer retEntity = null;
            foreach (EmployeeTrainingExternalTrainer rec in coll)
            {
                if (rec.ExternalTrainerSeqNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(EmployeeTrainingExternalTrainer entity, GridCommandEventArgs e)
        {
            var userControl = (EmployeeTrainingExternalTrainerDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.EmployeeTrainingID = txtTrainingID.Text.ToInt();
                entity.ExternalTrainerSeqNo = userControl.ExternalTrainerSeqNo;
                entity.ExternalTrainerName = userControl.ExternalTrainerName;
                entity.PositionAs = userControl.PositionAs;
                entity.Notes = userControl.Notes;
            }
        }
        #endregion

        #region EmployeeTrainingItems
        private EmployeeTrainingItemCollection EmployeeTrainingItems
        {
            get
            {
                //if (IsPostBack)
                {
                    var obj = Session["collEmployeeTrainingItem" + Request.UserHostName];
                    if (obj != null)
                        return ((EmployeeTrainingItemCollection)(obj));
                }
                var coll = new EmployeeTrainingItemCollection();

                var query = new EmployeeTrainingItemQuery("a");
                var rtype = new AppStandardReferenceItemQuery("i");

                query.Select(
                    query,
                     rtype.ItemName.As("refToEmployeeTrainingItem_ComponentName")


                    );

                query.InnerJoin(rtype).On(query.SRComponentID == rtype.ItemID &&
                                          rtype.StandardReferenceID == AppEnum.StandardReference.EmployeeTrainingComponent);

                query.Where(query.EmployeeTrainingID == Convert.ToInt32(txtTrainingID.Value == null ? -2 : txtTrainingID.Value));

                coll.Load(query);

                Session["collEmployeeTrainingItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collEmployeeTrainingItem" + Request.UserHostName] = value;
            }
        }
        #endregion


        protected void cboReferenceID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new EmployeeTrainingQuery("a");
            var rlz = new EmployeeTrainingQuery("b");
            query.LeftJoin(rlz).On(rlz.ReferenceID == query.EmployeeTrainingID);
            query.es.Top = 20;
            query.Select
                (
                    query.EmployeeTrainingID,
                    query.EmployeeTrainingName,
                    query.TrainingLocation,
                    query.TrainingOrganizer
                );

            query.Where
                (
                    query.IsActive == true, query.IsProposal == true, rlz.EmployeeTrainingID.IsNull(),
                    query.Or
                        (
                            query.EmployeeTrainingName.Like(searchTextContain),
                            query.TrainingLocation.Like(searchTextContain),
                            query.TrainingOrganizer.Like(searchTextContain)
                        )
                );

            cboReferenceID.DataSource = query.LoadDataTable();
            cboReferenceID.DataBind();
        }

        protected void cboReferenceID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeTrainingName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["TrainingLocation"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["EmployeeTrainingID"].ToString();
        }

        protected void cboReferenceID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var pps = new EmployeeTraining();
            if (pps.LoadByPrimaryKey(cboReferenceID.SelectedValue.ToInt()))
            {
                txtTrainingName.Text = pps.EmployeeTrainingName;
                txtTrainingLocation.Text = pps.TrainingLocation;
                txtTrainingOrganizer.Text = pps.TrainingOrganizer;
                txtDateStart.SelectedDate = pps.StartDate ?? DateTime.Now.Date;
                txtDateEnd.SelectedDate = pps.EndDate ?? DateTime.Now.Date;
                var start = new DateTime(2000, 1, 1,
                    pps.StartTime.HasValue ? pps.StartTime.Value.Hours : DateTime.Now.Hour,
                    pps.StartTime.HasValue ? pps.StartTime.Value.Minutes : DateTime.Now.Minute,
                    pps.StartTime.HasValue ? pps.StartTime.Value.Seconds : 0);
                txtTimeStart.SelectedDate = start;
                var end = new DateTime(2000, 1, 1,
                    pps.EndTime.HasValue ? pps.EndTime.Value.Hours : DateTime.Now.Hour,
                    pps.EndTime.HasValue ? pps.EndTime.Value.Minutes : DateTime.Now.Minute,
                    pps.EndTime.HasValue ? pps.EndTime.Value.Seconds : 0);
                txtTimeEnd.SelectedDate = end;
                txtTotalHour.Value = pps.TotalHour ?? 0;
                txtCreditPoint.Value = Convert.ToDouble(pps.CreditPoint ?? 0);
                txtPlanningCosts.Value = Convert.ToDouble(pps.PlanningCosts ?? 0);
                txtTotalFeeAmount.Value = Convert.ToDouble(pps.TrainingFee ?? 0);
                txtSponsorFee.Value = Convert.ToDouble(pps.SponsorFee ?? 0);
                txtTargetAttendance.Value = pps.TargetAttendance ?? 0;
                txtNotes.Text = pps.Note;
                chkIsInHouseTraining.Checked = pps.IsInHouseTraining ?? false;
                chkIsScheduledTraining.Checked = pps.IsScheduledTraining ?? false;
                cboSRActivityType.SelectedValue = pps.SRActivityType;
                if (!string.IsNullOrEmpty(pps.SRActivitySubType))
                {
                    var subtype = new AppStandardReferenceItemQuery();
                    subtype.Where(subtype.StandardReferenceID == AppEnum.StandardReference.ActivitySubType.ToString(), subtype.ItemID == pps.SRActivitySubType);
                    cboSRActivitySubType.DataSource = subtype.LoadDataTable();
                    cboSRActivitySubType.DataBind();
                    cboSRActivitySubType.SelectedValue = pps.SRActivitySubType;
                }
                else
                {
                    cboSRActivitySubType.Items.Clear();
                    cboSRActivitySubType.SelectedValue = string.Empty;
                    cboSRActivitySubType.Text = string.Empty;
                }
                if (pps.CertificateValidityPeriod != null)
                    txtCertificateValidityPeriod.SelectedDate = pps.CertificateValidityPeriod;
                else
                    txtCertificateValidityPeriod.Clear();

                chkIsCommitmentToWork.Checked = pps.IsCommitmentToWork ?? false;
                txtLengthOfService.Value = Convert.ToDouble(pps.LengthOfService);

                if (pps.StartServiceDate != null)
                    txtStartServiceDate.SelectedDate = pps.StartServiceDate;
                else
                    txtStartServiceDate.Clear();

                if (pps.EndServiceDate != null)
                    txtEndServiceDate.SelectedDate = pps.EndServiceDate;
                else
                    txtEndServiceDate.Clear();

                cboSRTrainingFinancingSources.SelectedValue = pps.SRTrainingFinancingSources;

                if (pps.EvaluationDate != null)
                    txtEvaluationDate.SelectedDate = pps.EvaluationDate;
                else
                    txtEvaluationDate.Clear();

                cboSREmployeeTrainingPointType.SelectedValue = pps.SREmployeeTrainingPointType;
                txtDurationHour.Value = Convert.ToDouble(pps.DurationHour);
                txtDurationMinutes.Value = Convert.ToDouble(pps.DurationMinutes);

                var ppsi = new EmployeeTrainingHistoryCollection();
                ppsi.Query.Where(ppsi.Query.EmployeeTrainingID == pps.EmployeeTrainingID);
                ppsi.LoadAll();

                foreach (var i in ppsi)
                {
                    var eth = EmployeeTrainingHistorys.AddNew();
                    eth.PersonID = i.PersonID;
                    eth.EventName = i.EventName;
                    eth.EmployeeTrainingID = Convert.ToInt32(txtTrainingID.Value);

                    var p = new PersonalInfo();
                    if (p.LoadByPrimaryKey(eth.PersonID.ToInt()))
                    {
                        eth.EmployeeName = p.EmployeeName;
                    }

                    eth.IsAttending = i.IsAttending;
                    eth.Note = i.Note;

                    eth.StartDate = pps.StartDate;
                    eth.EndDate = pps.EndDate;
                    eth.TrainingLocation = pps.TrainingLocation;
                    eth.TrainingInstitution = pps.TrainingOrganizer;
                    eth.PlanningCosts = pps.PlanningCosts;
                    eth.Fee = pps.TrainingFee;
                    eth.SponsorFee = pps.SponsorFee;

                    eth.TotalHour = pps.TotalHour;
                    eth.CreditPoint = pps.CreditPoint;
                    eth.IsInHouseTraining = pps.IsInHouseTraining;
                    eth.IsScheduledTraining = pps.IsScheduledTraining;

                    eth.SRActivityType = pps.SRActivityType;
                    eth.SRActivitySubType = pps.SRActivitySubType;
                    eth.CertificateValidityPeriod = pps.CertificateValidityPeriod;
                    eth.IsCommitmentToWork = pps.IsCommitmentToWork;
                    eth.LengthOfService = pps.LengthOfService;
                    eth.StartServiceDate = pps.StartServiceDate;
                    eth.EndServiceDate = pps.EndServiceDate;
                    eth.SRTrainingFinancingSources = pps.SRTrainingFinancingSources;
                    eth.EvaluationDate = pps.EvaluationDate;

                    eth.SREmployeeTrainingPointType = pps.SREmployeeTrainingPointType;
                    eth.DurationHour = pps.DurationHour;
                    eth.DurationMinutes = pps.DurationMinutes;
                    eth.SREmployeeTrainingDateSeparator = pps.SREmployeeTrainingDateSeparator;
                    eth.SREmployeeTrainingRole = i.SREmployeeTrainingRole;

                    if (!string.IsNullOrEmpty(i.SREmployeeTrainingRole))
                    {
                        var role = new AppStandardReferenceItem();
                        if (role.LoadByPrimaryKey(AppEnum.StandardReference.EmployeeTrainingRole.ToString(), i.SREmployeeTrainingRole))
                            eth.EmployeeTrainingRoleName = role.ItemName;
                        else
                            eth.EmployeeTrainingRoleName = string.Empty;
                    }
                    else
                        eth.EmployeeTrainingRoleName = string.Empty;
                }

                var ppseti = new EmployeeTrainingItemCollection();
                ppseti.Query.Where(ppseti.Query.EmployeeTrainingID == pps.EmployeeTrainingID);
                ppseti.LoadAll();

                foreach (var ii in ppseti)
                {
                    var eti = EmployeeTrainingItems.AddNew();
                    eti.EmployeeTrainingID = Convert.ToInt32(txtTrainingID.Value);
                    eti.PersonID = ii.PersonID;
                    eti.SRComponentID = ii.SRComponentID;
                    eti.Price = ii.Price;

                    
                }

                grdEmployeeTrainingHistory.Rebind();
            }
        }

        protected void txtDateStart_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            txtEvaluationDate.SelectedDate = txtDateStart.SelectedDate.Value.AddMonths(AppSession.Parameter.IntervalTrainingEvaluationSchedule);
        }

        protected void cboSRActivityType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRActivitySubType.Items.Clear();
            cboSRActivitySubType.SelectedValue = string.Empty;
            cboSRActivitySubType.Text = string.Empty;
        }

        protected void cboSRActivitySubType_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );

            query.Where
                (
                    query.StandardReferenceID == AppEnum.StandardReference.ActivitySubType.ToString(),
                    query.ItemName.Like(searchTextContain),
                    query.IsActive == true,
                    query.ReferenceID == cboSRActivityType.SelectedValue
                );

            cboSRActivitySubType.DataSource = query.LoadDataTable();
            cboSRActivitySubType.DataBind();
        }

        protected void cboSRActivitySubType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }
    }
}

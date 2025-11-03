using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PpiNeedlePuncturedDetail : BasePageDetail
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 300;
            // Url Search & List
            UrlPageSearch = "PpiNeedlePuncturedSearch.aspx?type=" + FormType;
            UrlPageList = "PpiNeedlePuncturedList.aspx?type=" + FormType;
            ProgramID = FormType == ""
                            ? AppConstant.Program.PpiNeedlePuncturedSurveillance
                            : AppConstant.Program.PpiNeedlePuncturedSurveillanceVerified;


            if (!IsPostBack)
            {
                if (FormType == "")
                {
                    rfvFollowUpDate.Visible = false;
                    rfvFollowUp.Visible = false;
                    rfvFollowUpBy.Visible = false;
                }
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new PpiNeedlePunctured());

            txtTransactionNo.Text = GetNewTransactionNo();
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            if (FormType == "")
            {
                txtFollowUp.ReadOnly = true;
                txtFollowUpDate.Enabled = false;
                txtFollowUpBy.ReadOnly = true;
            }
        }

        protected override void OnMenuEditClick()
        {
            if (FormType == "verif")
            {
                txtOfficerName.ReadOnly = true;
                txtDatePunctured.Enabled = false;
                txtPuncturedAreas.ReadOnly = true;
                txtCausePunctured.ReadOnly = true;
                chkIsBlood.Enabled = false;
                chkIsFluidSperm.Enabled = false;
                chkIsVaginalSecretions.Enabled = false;
                chkIsCerebrospinal.Enabled = false;
                chkIsUrine.Enabled = false;
                chkIsFaeces.Enabled = false;
                chkIsOfficerHiv.Enabled = false;
                chkIsOfficerHbv.Enabled = false;
                chkIsOfficerHcv.Enabled = false;
                txtOfficerImunizationHistory.ReadOnly = true;
                txtChronology.ReadOnly = true;
                txtPatientName.ReadOnly = true;
                txtMedicalNo.ReadOnly = true;
                txtDiagnose.ReadOnly = true;
                chkIsPatientHiv.Enabled = false;
                chkIsPatientHbv.Enabled = false;
                chkIsPatientHcv.Enabled = false;
                txtPatientImunizationHistory.ReadOnly = true;
                txtKnownBy.ReadOnly = true;
            }
            else
            {
                txtFollowUp.ReadOnly = true;
                txtFollowUpDate.Enabled = false;
                txtFollowUpBy.ReadOnly = true;
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new PpiNeedlePunctured();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (!IsApproved(entity, args))
                    return;

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
            if (!chkIsBlood.Checked && !chkIsFluidSperm.Checked && !chkIsVaginalSecretions.Checked && !chkIsCerebrospinal.Checked && !chkIsUrine.Checked && !chkIsFaeces.Checked)
            {
                args.MessageText = "Source Of Exposure required.";
                args.IsCancel = true;
                return;
            }

            var entity = new PpiNeedlePunctured();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new PpiNeedlePunctured();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text);
            auditLogFilter.TableName = "PpiNeedlePunctured";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            //printJobParameters.AddNew("TransactionNo", txtTransactionNo.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            if (FormType == "verif")
            {
                if (txtFollowUpDate.IsEmpty)
                {
                    args.MessageText = "Follow Up Date required.";
                    args.IsCancel = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtFollowUp.Text))
                {
                    args.MessageText = "Follow Up required.";
                    args.IsCancel = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtFollowUpBy.Text))
                {
                    args.MessageText = "Follow Up By required.";
                    args.IsCancel = true;
                    return;
                }
            }

            using (var trans = new esTransactionScope())
            {
                var entity = new PpiNeedlePunctured();
                entity.LoadByPrimaryKey(txtTransactionNo.Text);

                entity.IsApproved = true;
                entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;

                if (FormType == "verif")
                {
                    entity.IsVerified = true;
                    entity.VerifiedDateTime = (new DateTime()).NowAtSqlServer();
                    entity.VerifiedByUserID = AppSession.UserLogin.UserID;
                }

                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new PpiNeedlePunctured();
            entity.LoadByPrimaryKey(txtTransactionNo.Text);

            if (FormType == "")
            {
                if (entity.IsVerified == true)
                {
                    args.MessageText = "Data already verified.";
                    args.IsCancel = true;
                    return;
                }
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
            var entity = new PpiNeedlePunctured();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
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

        private bool IsApprovedOrVoid(PpiNeedlePunctured entity, ValidateArgs args)
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

        private bool IsApproved(PpiNeedlePunctured entity, ValidateArgs args)
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
                ToolBarMenuDelete.Visible = false;
            else
                ToolBarMenuDelete.Enabled = !chkIsApproved.Checked;
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
            var entity = new PpiNeedlePunctured();
            if (parameters.Length > 0)
            {
                string transNo = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transNo);

                txtTransactionNo.Text = entity.TransactionNo;
            }
            else
            {
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var ppi = (PpiNeedlePunctured)entity;

            txtTransactionNo.Text = ppi.TransactionNo;
            txtTransactionDate.SelectedDate = ppi.TransactionDate;

            txtOfficerName.Text = ppi.OfficerName;
            txtDatePunctured.SelectedDate = ppi.DatePunctured;
            txtPuncturedAreas.Text = ppi.PuncturedAreas;
            txtCausePunctured.Text = ppi.CausePunctured;
            chkIsBlood.Checked = ppi.IsBlood ?? false;
            chkIsFluidSperm.Checked = ppi.IsFluidSperm ?? false;
            chkIsVaginalSecretions.Checked = ppi.IsVaginalSecretions ?? false;
            chkIsCerebrospinal.Checked = ppi.IsCerebrospinal ?? false;
            chkIsUrine.Checked = ppi.IsUrine ?? false;
            chkIsFaeces.Checked = ppi.IsFaeces ?? false;
            chkIsOfficerHiv.Checked = ppi.IsOfficerHiv ?? false;
            chkIsOfficerHbv.Checked = ppi.IsOfficerHbv ?? false;
            chkIsOfficerHcv.Checked = ppi.IsOfficerHcv ?? false;
            txtOfficerImunizationHistory.Text = ppi.OfficerImunizationHistory;
            txtChronology.Text = ppi.Chronology;

            txtPatientName.Text = ppi.PatientName;
            txtMedicalNo.Text = ppi.MedicalNo;
            txtDiagnose.Text = ppi.Diagnose;
            chkIsPatientHiv.Checked = ppi.IsPatientHiv ?? false;
            chkIsPatientHbv.Checked = ppi.IsPatientHbv ?? false;
            chkIsPatientHcv.Checked = ppi.IsPatientHcv ?? false;
            txtPatientImunizationHistory.Text = ppi.PatientImunizationHistory;
            txtKnownBy.Text = ppi.KnownBy;

            txtFollowUp.Text = ppi.FollowUpBy;
            txtFollowUpDate.SelectedDate = ppi.FollowUpDate;
            txtFollowUpBy.Text = ppi.FollowUpBy;

            if (FormType == "verif")
                chkIsApproved.Checked = ppi.IsVerified ?? false;
            else
                chkIsApproved.Checked = ppi.IsApproved ?? false;
            chkIsVoid.Checked = ppi.IsVoid ?? false;
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(PpiNeedlePunctured entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewTransactionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;

            entity.OfficerName = txtOfficerName.Text;
            entity.DatePunctured = txtDatePunctured.SelectedDate;
            entity.PuncturedAreas = txtPuncturedAreas.Text;
            entity.CausePunctured = txtCausePunctured.Text;
            entity.IsBlood = chkIsBlood.Checked;
            entity.IsFluidSperm = chkIsFluidSperm.Checked;
            entity.IsVaginalSecretions = chkIsVaginalSecretions.Checked;
            entity.IsCerebrospinal = chkIsCerebrospinal.Checked;
            entity.IsUrine = chkIsUrine.Checked;
            entity.IsFaeces = chkIsFaeces.Checked;
            entity.IsOfficerHiv = chkIsOfficerHiv.Checked;
            entity.IsOfficerHbv = chkIsOfficerHbv.Checked;
            entity.IsOfficerHcv = chkIsOfficerHcv.Checked;
            entity.OfficerImunizationHistory = txtOfficerImunizationHistory.Text;
            entity.Chronology = txtChronology.Text;

            entity.PatientName = txtPatientName.Text;
            entity.MedicalNo = txtMedicalNo.Text;
            entity.Diagnose = txtDiagnose.Text;
            entity.IsPatientHiv = chkIsPatientHiv.Checked;
            entity.IsPatientHbv = chkIsPatientHbv.Checked;
            entity.IsPatientHcv = chkIsPatientHcv.Checked;
            entity.PatientImunizationHistory = txtPatientImunizationHistory.Text;
            entity.KnownBy = txtKnownBy.Text;

            entity.FollowUpBy = txtFollowUp.Text;
            if (!txtFollowUpDate.IsEmpty)
                entity.FollowUpDate = txtFollowUpDate.SelectedDate;
            else entity.str.FollowUpDate = string.Empty;
            entity.FollowUpBy = txtFollowUpBy.Text;

            if (entity.es.IsAdded)
            {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
            else if (entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(PpiNeedlePunctured entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new PpiNeedlePuncturedQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TransactionNo > txtTransactionNo.Text);
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < txtTransactionNo.Text);
                que.OrderBy(que.TransactionNo.Descending);
            }
            if (FormType == "")
                que.Where(que.CreatedByUserID == AppSession.UserLogin.UserID);
            else 
                que.Where(que.IsApproved == true);

            var entity = new PpiNeedlePunctured();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }
        #endregion

        #region Override Method & Function
        #endregion

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PpiNo);

            return _autoNumber.LastCompleteNumber;
        }
    }
}

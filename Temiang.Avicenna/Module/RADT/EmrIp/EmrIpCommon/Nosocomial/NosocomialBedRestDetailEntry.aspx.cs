using System;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class NosocomialBedRestDetailEntry : BasePageDialogEntry
    {
        public int MonitoringNo
        {
            get
            {
                return Request.QueryString["monno"].ToInt();
            }
        }
        public int SequenceNo
        {
            get
            {
                return Request.QueryString["seqno"].ToInt();
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            IsSingleRecordMode = true; //Save then close
            IsMedicalRecordEntry = true; //Activate daedline edit & add
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;

            ToolBar.EditVisible = false;
            ToolBar.AddVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Bed Rest Monitoring of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            // Hanya tuk single entry
            var ent = new NosocomialMonitoringBedRest();
            if (ent.LoadByPrimaryKey(RegistrationNo, MonitoringNo, SequenceNo))
            {

                txtMonitoringDateTime.SelectedDate = ent.MonitoringDateTime;
                chkIsInjuryCare.Checked = ent.IsInjuryCare ?? false;
                //chkIsMobilization.Checked = ent.IsMobilization ?? false;
                txtMobilization.Text = ent.Mobilization;
                txtFisiotherapi.Text = ent.Fisiotherapi;
                txtNote.Text = ent.Note;

                txtInjuryCondition.Text = ent.InjuryCondition;
                chkIsInjuryBlister.Checked = ent.IsInjuryBlister ?? false;
                chkIsInjuryOpen.Checked = ent.IsInjuryOpen ?? false;
                chkIsInjuryToFat.Checked = ent.IsInjuryToFat ?? false;
                chkIsInjuryNekrosis.Checked = ent.IsInjuryNekrosis ?? false;
                chkIsInjuryToBone.Checked = ent.IsInjuryToBone ?? false;

                txtSkinCondition.Text = ent.SkinCondition;
                chkIsSkinComplete.Checked = ent.IsSkinComplete ?? false;
                chkIsSkinRed.Checked = ent.IsSkinRed ?? false;
                chkIsSkinNoBlister.Checked = ent.IsSkinNoBlister ?? false;
                chkIsSkinWarm.Checked = ent.IsSkinWarm ?? false;
                chkIsSkinHard.Checked = ent.IsSkinHard ?? false;
                chkIsSkinItchy.Checked = ent.IsSkinItchy ?? false;

                chkIsCulture.Checked = ent.IsCulture ?? false;
                chkIsDxDekubitus.Checked = ent.IsDxDekubitus ?? false;
                txtMonitoringByName.Text = AppUser.GetUserName(ent.MonitoringByUserID);

                optIsRelease.SelectedValue = ent.IsStop ?? false ? "1" : "0";
            }

            var nm = new NosocomialMonitoring();
            if (nm.LoadByPrimaryKey(RegistrationNo, MonitoringNo))
            {
                txtReleaseDateTime.SelectedDate = nm.ReleaseDateTime;
            }

        }


        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {
            var timeNow = (new DateTime()).NowAtSqlServer();
            txtMonitoringDateTime.SelectedDate = timeNow;
            txtMonitoringByName.Text = AppSession.UserLogin.UserName;

        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save(true);
        }

        private void Save(bool isNewRecord)
        {
            var ent = new NosocomialMonitoringBedRest();
            if (isNewRecord || !ent.LoadByPrimaryKey(RegistrationNo, MonitoringNo, SequenceNo))
            {
                ent.RegistrationNo = RegistrationNo;
                ent.MonitoringNo = MonitoringNo;
                ent.SequenceNo = NewSequenceNo();
            }
            ent.MonitoringDateTime = txtMonitoringDateTime.SelectedDate;
            //ent.IsMobilization = chkIsMobilization.Checked;
            ent.Mobilization = txtMobilization.Text;
            ent.IsInjuryCare = chkIsInjuryCare.Checked;
            ent.Fisiotherapi = txtFisiotherapi.Text;

            ent.InjuryCondition = txtInjuryCondition.Text;
            ent.IsInjuryBlister = chkIsInjuryBlister.Checked;
            ent.IsInjuryOpen = chkIsInjuryOpen.Checked;
            ent.IsInjuryToFat = chkIsInjuryToFat.Checked;
            ent.IsInjuryNekrosis = chkIsInjuryNekrosis.Checked;
            ent.IsInjuryToBone = chkIsInjuryToBone.Checked;

            ent.SkinCondition = txtSkinCondition.Text;
            ent.IsSkinComplete = chkIsSkinComplete.Checked;
            ent.IsSkinRed = chkIsSkinRed.Checked;
            ent.IsSkinNoBlister = chkIsSkinNoBlister.Checked;
            ent.IsSkinWarm = chkIsSkinWarm.Checked;
            ent.IsSkinHard = chkIsSkinHard.Checked;
            ent.IsSkinItchy = chkIsSkinItchy.Checked;

            ent.IsCulture = chkIsCulture.Checked;
            ent.IsDxDekubitus = chkIsDxDekubitus.Checked;
            ent.Note = txtNote.Text;
            if (isNewRecord)
                ent.MonitoringByUserID = AppSession.UserLogin.UserID;

            ent.IsStop = optIsRelease.SelectedValue == "1";
            
            ent.Save();

            var nm = new NosocomialMonitoring();
            if (nm.LoadByPrimaryKey(RegistrationNo, MonitoringNo))
            {
                if (!txtReleaseDateTime.IsEmpty)
                {
                    nm.ReleaseDateTime = txtReleaseDateTime.SelectedDate;
                    nm.ReleaseByUserID = AppSession.UserLogin.UserID;
                }
                else
                {
                    nm.str.ReleaseDateTime = string.Empty;
                    nm.str.ReleaseByUserID = string.Empty;

                }
                nm.Save();

            }
        }


        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Save(false);
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {

        }
        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        protected override void OnMenuRejournalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }
        public override string OnGetScriptToolBarSaveClicking()
        {
            return string.Empty;
        }
        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return true;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        private int NewSequenceNo()
        {
            var qr = new NosocomialMonitoringBedRestQuery("a");
            var fb = new NosocomialMonitoringBedRest();
            qr.Where(qr.RegistrationNo == RegistrationNo, qr.MonitoringNo == MonitoringNo);
            qr.es.Top = 1;
            qr.OrderBy(qr.SequenceNo.Descending);

            if (fb.Load(qr))
            {
                return fb.SequenceNo.ToInt() + 1;
            }
            return 1;
        }

        protected void optIsRelease_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (optIsRelease.SelectedValue == "1")
            {
                txtReleaseDateTime.Enabled = true;
                txtReleaseDateTime.DatePopupButton.Enabled = true;
                txtReleaseDateTime.SelectedDate = txtMonitoringDateTime.SelectedDate;
            }
            else
            {
                txtReleaseDateTime.Enabled = false;
                txtReleaseDateTime.DatePopupButton.Enabled = false;
                txtReleaseDateTime.Clear();
            }
        }
    }
}

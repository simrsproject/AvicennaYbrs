using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data.SqlTypes;
using System.Text;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class NosocomialHapDetailEntry : BasePageDialogEntry
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
                    this.Title = "HAP Monitoring of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
                StandardReference.InitializeIncludeSpace(cboSREttType, AppEnum.StandardReference.EttType);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            // Hanya tuk single entry
            var ent = new NosocomialMonitoringHap();
            if (ent.LoadByPrimaryKey(RegistrationNo, MonitoringNo, SequenceNo))
            {
                
                txtMonitoringDateTime.SelectedDate = ent.MonitoringDateTime;
                ComboBox.SelectedValue(cboSREttType,ent.SREttType);
                chkIsTempAbove38.Checked = ent.IsTempAbove38 ?? false;
                chkIsBradikardi.Checked = ent.IsBradikardi ?? false;
                chkIsDispenea.Checked = ent.IsDispenea ?? false;
                chkIsSpO2LessThan94.Checked = ent.IsSpO2LessThan94 ?? false;
                //chkIsLeukopenia.Checked = ent.IsLeukopenia ?? false;
                chkIsLeukositosis.Checked = ent.IsLeukositosis ?? false;
                chkIsSputum.Checked = ent.IsSputum ?? false;
                chkIsCough.Checked = ent.IsCough ?? false;
                chkIsDipsnoe.Checked = ent.IsDipsnoe ?? false;
                chkIsWetRonchi.Checked = ent.IsWetRonchi ?? false;
                chkIsDesaturasi.Checked = ent.IsDesaturasi ?? false;
                chkIsCulture.Checked = ent.IsCulture ?? false;
                chkIsRadiology.Checked = ent.IsRadiology ?? false;

                txtNote.Text = ent.Note;
                chkIsElbowConnector.Checked = ent.IsElbowConnectorRepl ?? false;
                chkIsHumidification.Checked = ent.IsHumidificationRepl ?? false;
                chkIsGuedele.Checked = ent.IsGuedeleRepl ?? false;
                chkIsTidalVol.Checked = ent.IsTidalVolChange ?? false;
                chkIsRr.Checked = ent.IsRrChange ?? false;
                chkIsModeVent.Checked = ent.IsModeVentChange ?? false;
                txtSputumColor.Text = ent.SputumColor;
                //txtLeukosit.Text = ent.Leukosit;
                txtThorax.Text = ent.Thorax;
                chkIsVapDiagnose.Checked = ent.IsVapDiagnose ?? false;
                optIsRelease.SelectedValue = ent.IsRelease ?? false ? "1" : "0";
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
            var ent = new NosocomialMonitoringHap();
            if (isNewRecord || !ent.LoadByPrimaryKey(RegistrationNo, MonitoringNo, SequenceNo))
            {
                ent.RegistrationNo = RegistrationNo;
                ent.MonitoringNo = MonitoringNo;
                ent.SequenceNo = NewSequenceNo();
            }
            ent.MonitoringDateTime = txtMonitoringDateTime.SelectedDate;

            ent.MonitoringDateTime = txtMonitoringDateTime.SelectedDate;
            ent.SREttType = cboSREttType.SelectedValue;
            ent.IsTempAbove38 = chkIsTempAbove38.Checked;
            ent.IsBradikardi = chkIsBradikardi.Checked;
            ent.IsDispenea = chkIsDispenea.Checked;
            ent.IsSpO2LessThan94 = chkIsSpO2LessThan94.Checked;
            //ent.IsLeukopenia = chkIsLeukopenia.Checked;
            ent.IsLeukositosis = chkIsLeukositosis.Checked;
            ent.IsSputum = chkIsSputum.Checked;
            ent.IsCough = chkIsCough.Checked;
            ent.IsDipsnoe = chkIsDipsnoe.Checked;
            ent.IsWetRonchi = chkIsWetRonchi.Checked;
            ent.IsDesaturasi = chkIsDesaturasi.Checked;
            ent.IsCulture = chkIsCulture.Checked;
            ent.IsRadiology = chkIsRadiology.Checked;

            ent.MonitoringByUserID = AppSession.UserLogin.UserID;
            ent.IsRelease = optIsRelease.SelectedValue == "1";
            ent.Note = txtNote.Text;
            ent.IsElbowConnectorRepl = chkIsElbowConnector.Checked;
            ent.IsHumidificationRepl = chkIsHumidification.Checked;
            ent.IsGuedeleRepl = chkIsGuedele.Checked;
            ent.IsTidalVolChange = chkIsTidalVol.Checked;
            ent.IsRrChange = chkIsRr.Checked;
            ent.IsModeVentChange = chkIsModeVent.Checked;
            ent.SputumColor = txtSputumColor.Text;
            //ent.Leukosit = txtLeukosit.Text;
            ent.Thorax = txtThorax.Text;
            ent.IsVapDiagnose = chkIsVapDiagnose.Checked;
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
            var qr = new NosocomialMonitoringHapQuery("a");
            var fb = new NosocomialMonitoringHap();
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

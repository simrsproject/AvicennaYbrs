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
    public partial class NosocomialNgtDetailEntry : BasePageDialogEntry
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
                    this.Title = "NGT Monitoring of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
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
            var ent = new NosocomialMonitoringNgt();
            if (ent.LoadByPrimaryKey(RegistrationNo, MonitoringNo, SequenceNo))
            {
                
                txtMonitoringDateTime.SelectedDate = ent.MonitoringDateTime;
                txtReplacement.Text = ent.Replacement;
                chkIsTempAbove38.Checked = ent.IsTempAbove38 ?? false;
                chkIsPus.Checked = ent.IsPus ?? false;
                chkIsHeadache.Checked = ent.IsHeadache ?? false;
                chkIsNoseClogged.Checked = ent.IsNoseClogged ?? false;
                chkIsPainswallow.Checked = ent.IsPainSwallow ?? false;
                chkIsPharingRedness.Checked = ent.IsPharingRedness ?? false;
                chkIsCough.Checked = ent.IsCough ?? false;
                chkIsTransillumination.Checked = ent.IsTransillumination ?? false;
                chkIsCulture.Checked = ent.IsCulture ?? false;
                txtNote.Text = ent.Note;
                txtMonitoringByName.Text = AppUser.GetUserName(ent.MonitoringByUserID);
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
            var ent = new NosocomialMonitoringNgt();
            if (isNewRecord || !ent.LoadByPrimaryKey(RegistrationNo, MonitoringNo, SequenceNo))
            {
                ent.RegistrationNo = RegistrationNo;
                ent.MonitoringNo = MonitoringNo;
                ent.SequenceNo = NewSequenceNo();
            }
            ent.MonitoringDateTime = txtMonitoringDateTime.SelectedDate;
            ent.Replacement = txtReplacement.Text;
            ent.IsTempAbove38 = chkIsTempAbove38.Checked;
            ent.IsPus = chkIsPus.Checked;
            ent.IsHeadache = chkIsHeadache.Checked;
            ent.IsNoseClogged = chkIsNoseClogged.Checked;
            ent.IsPainSwallow = chkIsPainswallow.Checked;
            ent.IsPharingRedness = chkIsPharingRedness.Checked;
            ent.IsCough = chkIsCough.Checked;
            ent.IsTransillumination = chkIsTransillumination.Checked;
            ent.IsCulture = chkIsCulture.Checked;

            if (isNewRecord)
                ent.MonitoringByUserID = AppSession.UserLogin.UserID;

            ent.IsRelease = optIsRelease.SelectedValue == "1";
            ent.Note = txtNote.Text;
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
            var qr = new NosocomialMonitoringNgtQuery("a");
            var fb = new NosocomialMonitoringNgt();
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

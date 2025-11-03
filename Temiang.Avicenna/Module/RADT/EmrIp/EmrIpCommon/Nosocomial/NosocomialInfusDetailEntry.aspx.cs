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
    public partial class NosocomialInfusDetailEntry : BasePageDialogEntry
    {
        public int MonitoringNo
        {
            get { return Request.QueryString["monno"].ToInt(); }
        }

        public int SequenceNo
        {
            get { return Request.QueryString["seqno"].ToInt(); }
        }

        private NosocomialMonitoring _nosocomialMonitoringCurrent;

        private NosocomialMonitoring NosocomialMonitoringCurrent
        {
            get
            {
                if (_nosocomialMonitoringCurrent == null)
                {
                    _nosocomialMonitoringCurrent = new NosocomialMonitoring();
                    _nosocomialMonitoringCurrent.LoadByPrimaryKey(RegistrationNo, MonitoringNo);
                }

                return _nosocomialMonitoringCurrent;
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
                    this.Title = (this.Request.QueryString["tp"] == "INC" ? "Central (Infus) of " : "Vena Perifier of ") + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }

            AutoCompleteBox.Initialized(acbInfusLocation, NosocomialMonitoringCurrent.Location);
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region override method

        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            // Hanya tuk single entry
            var ent = new NosocomialMonitoringInfus();
            if (ent.LoadByPrimaryKey(RegistrationNo, MonitoringNo, SequenceNo))
            {

                txtMonitoringDateTime.SelectedDate = ent.MonitoringDateTime;
                ComboBox.PopulateWithOneStandardReference(cboSRIVChateter, AppEnum.StandardReference.IVChateter.ToString(), ent.SRIVCatheter);
                ComboBox.PopulateWithOneStandardReference(cboSRInfusSet, AppEnum.StandardReference.InfusSet.ToString(), ent.SRInfusSet);
                chkIsSetBlood.Checked = ent.IsSetBlood ?? false;

                AutoCompleteBox.SetValue(acbInfusLocation, ent.InfusLocation);

                chkIsTempAbove38.Checked = ent.IsTempAbove38 ?? false;
                chkIsApneu.Checked = ent.IsApneu ?? false;
                chkIsPain.Checked = ent.IsPain ?? false;
                chkIsRedness.Checked = ent.IsRedness ?? false;
                chkIsFeelingHot.Checked = ent.IsFeelingHot ?? false;
                chkVeinHarden.Checked = ent.IsVeinHarden ?? false;
                chkIsSwollen.Checked = ent.IsSwollen ?? false;
                chkIsPus.Checked = ent.IsPus ?? false;
                chkIsKanulaCulture.Checked = ent.IsKanulaCulture ?? false;
                txtMedicationMethod.Text = ent.MedicationMethod;
                txtMedicineAndLiquid.Text = ent.MedicineAndLiquid;
                txtMonitoringByName.Text = AppUser.GetUserName(ent.MonitoringByUserID);
                txtNotes.Text = ent.Notes;
                chkIsDirty.Checked = ent.IsDirty ?? false;
                txtLiquidType.Text = ent.LiquidType;
                chkIsShivers.Checked = ent.IsShivers ?? false;
                chkIsInfected.Checked = ent.IsInfected ?? false;

                txtReleaseDateTime.SelectedDate = ent.ReleaseDateTime;
                optIsRelease.SelectedValue = txtReleaseDateTime.IsEmpty ? "0" : "1";

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

            StandardReference.InitializeIncludeSpace(cboSRIVChateter, AppEnum.StandardReference.IVChateter);
            StandardReference.InitializeIncludeSpace(cboSRInfusSet, AppEnum.StandardReference.InfusSet);

            AutoCompleteBox.SetValue(acbInfusLocation, NosocomialMonitoringCurrent.Location);

        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save(true);
        }

        private void Save(bool isNewRecord)
        {
            var ent = new NosocomialMonitoringInfus();
            if (isNewRecord || !ent.LoadByPrimaryKey(RegistrationNo, MonitoringNo, SequenceNo))
            {
                ent.RegistrationNo = RegistrationNo;
                ent.MonitoringNo = MonitoringNo;
                ent.SequenceNo = NewSequenceNo();
            }

            ent.MonitoringDateTime = txtMonitoringDateTime.SelectedDate;
            ent.SRIVCatheter = cboSRIVChateter.SelectedValue;
            ent.SRInfusSet = cboSRInfusSet.SelectedValue;
            ent.IsSetBlood = chkIsSetBlood.Checked;
            ent.InfusLocation = acbInfusLocation.Text;
            ent.IsTempAbove38 = chkIsTempAbove38.Checked;
            ent.IsApneu = chkIsApneu.Checked;
            ent.IsPain = chkIsPain.Checked;
            ent.IsRedness = chkIsRedness.Checked;
            ent.IsFeelingHot = chkIsFeelingHot.Checked;
            ent.IsVeinHarden = chkVeinHarden.Checked;
            ent.IsSwollen = chkIsSwollen.Checked;
            ent.IsPus = chkIsPus.Checked;
            ent.IsKanulaCulture = chkIsKanulaCulture.Checked;
            ent.MonitoringByUserID = AppSession.UserLogin.UserID;
            ent.MedicationMethod = txtMedicationMethod.Text;
            ent.MedicineAndLiquid = txtMedicineAndLiquid.Text;
            ent.Notes = txtNotes.Text;
            if (txtReleaseDateTime.IsEmpty)
                ent.str.ReleaseDateTime = string.Empty;
            else if (txtReleaseDateTime.SelectedDate != null)
                ent.ReleaseDateTime = txtReleaseDateTime.SelectedDate.Value;
            ent.IsDirty = chkIsDirty.Checked;
            ent.LiquidType = txtLiquidType.Text;
            ent.IsShivers = chkIsShivers.Checked;
            ent.IsInfected = chkIsInfected.Checked;
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

        protected override void OnMenuPrintClick(ValidateArgs args, string programID,
            PrintJobParameterCollection printJobParameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
            var val = cboSRIVChateter.SelectedValue;
            StandardReference.InitializeIncludeSpace(cboSRIVChateter, AppEnum.StandardReference.IVChateter);
            ComboBox.SelectedValue(cboSRIVChateter, val);

            val = cboSRInfusSet.SelectedValue;
            StandardReference.InitializeIncludeSpace(cboSRInfusSet, AppEnum.StandardReference.InfusSet);
            ComboBox.SelectedValue(cboSRInfusSet, val);

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
            var qr = new NosocomialMonitoringInfusQuery("a");
            var fb = new NosocomialMonitoringInfus();
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

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
    public partial class NosocomialInfusEntry : BasePageDialogEntry
    {
        public int MonitoringNo
        {
            get
            {
                return Request.QueryString["monno"].ToInt();
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
                    this.Title = "HAIs Monitoring " + (this.Request.QueryString["tp"] == "INC" ? "Central (Infus) of " : "Vena Perifier of ") + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }

                StandardReference.InitializeIncludeSpace(cboSRIVChateter, AppEnum.StandardReference.IVChateter);
                StandardReference.InitializeIncludeSpace(cboSRInfusSet, AppEnum.StandardReference.InfusSet);
            }

            AutoCompleteBox.Initialized(acbInfusLocation, AppEnum.StandardReference.InfusLocation);
            AutoCompleteBox.Initialized(acbAntibiotic, AppEnum.AutoCompleteBox.Antibiotic);
            AutoCompleteBox.Initialized(acbOtherDrug, AppEnum.AutoCompleteBox.OtherDrug);
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            // Hanya tuk single entry
            var ent = new NosocomialMonitoring();
            ent.LoadByPrimaryKey(RegistrationNo, MonitoringNo);
            txtInstallationDateTime.SelectedDate = ent.InstallationDateTime;
            ComboBox.PopulateWithOneRoom(cboRoomID, ent.RoomID);

            AutoCompleteBox.SetValue(acbInfusLocation, AppEnum.StandardReference.InfusLocation, ent.Location);
            AutoCompleteBox.SetValue(acbAntibiotic, ent.Antibiotic);
            AutoCompleteBox.SetValue(acbOtherDrug, ent.OtherDrugs);

            txtTypeOfInfus.Text = ent.TypeOfInfus;
            txtProblem.Text = ent.Problem;
            txtMonitoring.Text = ent.Monitoring;

            txtInstallationByName.Text = AppUser.GetUserName(ent.InstallationByUserID);

            ComboBox.SelectedValue(cboSRIVChateter, ent.SRIVCatheter);
            ComboBox.SelectedValue(cboSRInfusSet, ent.SRInfusSet);
            chkIsSetBlood.Checked = ent.IsSetBlood ?? false;
        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {
            var timeNow = (new DateTime()).NowAtSqlServer();
            txtInstallationDateTime.SelectedDate = timeNow;
            txtInstallationByName.Text = AppSession.UserLogin.UserName;

            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);
            ComboBox.PopulateWithOneRoom(cboRoomID, reg.RoomID);
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save(true);
        }

        private void Save(bool isNewRecord)
        {
            var ent = new NosocomialMonitoring();
            if (isNewRecord || !ent.LoadByPrimaryKey(RegistrationNo, MonitoringNo))
            {
                ent.RegistrationNo = RegistrationNo;
                ent.MonitoringNo = NewMonitoringNo();
                ent.MonitoringType = this.Request.QueryString["tp"];
            }
            ent.RoomID = cboRoomID.SelectedValue;
            ent.Location = acbInfusLocation.Text; // txtLocation.Text;

            ent.TypeOfInfus = txtTypeOfInfus.Text;
            ent.Antibiotic = acbAntibiotic.Text;
            ent.OtherDrugs = acbOtherDrug.Text;
            ent.Problem = txtProblem.Text;
            ent.Monitoring = txtMonitoring.Text;

            ent.SRIVCatheter = cboSRIVChateter.SelectedValue;
            ent.SRInfusSet = cboSRInfusSet.SelectedValue;
            ent.IsSetBlood = chkIsSetBlood.Checked;

            if (isNewRecord)
            {
                ent.InstallationByUserID = AppSession.UserLogin.UserID;
                ent.InstallationDateTime = txtInstallationDateTime.SelectedDate.Value;
            }
            ent.str.ServiceUnitID = string.Empty;
            if (!string.IsNullOrWhiteSpace(ent.RoomID))
            {
                var sr = new ServiceRoom();
                if (sr.LoadByPrimaryKey(ent.RoomID))
                    ent.ServiceUnitID = sr.ServiceUnitID;
            }
            ent.Save();

            // Info for resfresh parent page
            hdnEditRegistrationNo.Value = ent.RegistrationNo;
            hdnEditMonitoringNo.Value = ent.MonitoringNo.ToString();
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            var script = string.Format(@"oArg.editRegNo = document.getElementById('{0}').value;
            oArg.editMonNo = document.getElementById('{1}').value;", hdnEditRegistrationNo.ClientID,
                hdnEditMonitoringNo.ClientID);
            return script;
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

        private int NewMonitoringNo()
        {
            var qr = new NosocomialMonitoringQuery("a");
            var fb = new NosocomialMonitoring();
            qr.Where(qr.RegistrationNo == RegistrationNo);
            qr.es.Top = 1;
            qr.OrderBy(qr.MonitoringNo.Descending);

            if (fb.Load(qr))
            {
                return fb.MonitoringNo.ToInt() + 1;
            }
            return 1;
        }
    }
}

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

namespace Temiang.Avicenna.Module.RADT.Medication
{
    public partial class MedicationReceiveUsedEntry : BasePageDialogEntry
    {
        private int MedicationReceiveNo
        {
            get
            {
                return Request.QueryString["medrecno"].ToInt();
            }
        }
        private int SequenceNo
        {
            get
            {
                return Request.QueryString["seqno"].ToInt();
            }
        }
        private int ScheduleNo
        {
            get
            {
                return Request.QueryString["scno"].ToInt();
            }
        }
        private string TimeSchedule
        {
            get
            {
                return Request.QueryString["time"];
            }
        }
        private int DayNo
        {
            get
            {
                return Request.QueryString["dayno"].ToInt();
            }
        }

        private bool IsAdditionalSchedule
        {
            get
            {
                return Request.QueryString["isAdditional"] == "1";
            }
        }

        private string MedicationStep
        {
            get
            {
                return Request.QueryString["stat"];
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (AppConstant.Program.PharmaceuticalCare.Equals(Request.QueryString["prgid"]))
                ProgramID = AppConstant.Program.PharmaceuticalCare;
            else
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            trIsNotConsume.Visible = false;
            trReSchedule.Visible = false;
            trVoidSchedule.Visible = false;
            trHandovers.Visible = false;
            switch (MedicationStep)
            {
                case "S":
                    this.Title = "Medication Setup Entry";
                    trVoidSchedule.Visible = true;
                    trIsNotConsume.Visible = true;
                    break;
                case "H":
                    this.Title = "Medication Handovers";
                    trHandovers.Visible = true;
                    trReason.Visible = false;
                    break;
                case "V":
                    this.Title = "Medication Verification Entry";
                    trVoidSchedule.Visible = true;
                    break;
                case "R":
                    this.Title = "Medication Realization Entry";
                    trIsNotConsume.Visible = true;
                    trReSchedule.Visible = true;
                    break;
            }

            // Program Fiture
            IsSingleRecordMode = true; //Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;

            ToolBar.EditVisible = false;
            ToolBar.AddVisible = false;
            // -------------------

        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region override method

        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            PopulateEntryControl();
        }

        private MedicationReceive PopulateEntryControl()
        {
            // Hanya tuk single entry
            var med = new MedicationReceive();
            med.LoadByPrimaryKey(MedicationReceiveNo);
            //lblItemDescription.Text = med.ItemDescription;
            lblItemDescription.Text = MedicationReceive.PrescriptionItemDescription(med.RefTransactionNo, med.RefSequenceNo, med.ItemDescription);
            txtItemUnit.Text = med.SRConsumeUnit;
            txtBalanceQty.Value = Convert.ToDouble(med.BalanceQty);
            txtBalanceRealQty.Value = Convert.ToDouble(med.BalanceRealQty);

            var reg = new Registration();
            reg.LoadByPrimaryKey(med.RegistrationNo);

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);

            if (!string.IsNullOrWhiteSpace(pat.SRSalutation))
            {
                var salname = StandardReference.GetItemName(AppEnum.StandardReference.Salutation, pat.SRSalutation);
                lblPatientName.Text = string.Format("{0} {1}  ({2}Thn/{3}Bln)", salname, pat.PatientName, reg.AgeInYear, reg.AgeInMonth);
            }
            else
                lblPatientName.Text = string.Format("{0}  ({1}Thn/{2}Bln)", pat.PatientName, reg.AgeInYear, reg.AgeInMonth);

            var su = new ServiceUnit();
            su.LoadByPrimaryKey(reg.ServiceUnitID);
            lblServiceUnitName.Text = su.ServiceUnitName;

            var room = new ServiceRoom();
            room.LoadByPrimaryKey(reg.RoomID);
            lblRoomAndBed.Text = string.Format("{0} / {1}", room.RoomName, reg.BedID);

            var ent = new MedicationReceiveUsed();
            if (ent.LoadByPrimaryKey(MedicationReceiveNo, SequenceNo))
            {
                var user = new AppUser();
                if (ent.ScheduleDateTime != null)
                {
                    txtScheduleDate.SelectedDate = ent.ScheduleDateTime.Value.Date;
                    txtScheduleTime.SelectedTime = ent.ScheduleDateTime.Value.TimeOfDay;
                }

                if (ent.SetupDateTime != null)
                {
                    txtSetupTime.SelectedDate = ent.SetupDateTime.Value;

                    if (user.LoadByPrimaryKey(ent.SetupByUserID))
                        txtSetupBy.Text = user.UserName;
                }

                if (ent.HandoversDateTime != null)
                {
                    txtHandoversTime.SelectedDate = ent.HandoversDateTime.Value;
                    user = new AppUser();
                    if (user.LoadByPrimaryKey(ent.HandoversByUserID))
                        txtHandoversBy.Text = user.UserName;

                    if (!string.IsNullOrWhiteSpace(ent.HandoversToUserID))
                    {
                        user = new AppUser();
                        if (user.LoadByPrimaryKey(ent.HandoversToUserID))
                            txtHandoversTo.Text = user.UserName;

                        if (trHandovers.Visible)
                            ComboBox.PopulateWithOneRow(cboHandoversToUserID, ent.HandoversToUserID, Enums.EntityClassName.AppUser, "UserID", "UserName");
                    }
                }

                if (ent.VerificationDateTime != null)
                {
                    txtVerificationTime.SelectedDate = ent.VerificationDateTime.Value;
                    user = new AppUser();
                    if (user.LoadByPrimaryKey(ent.VerificationByUserID))
                        txtVerificationBy.Text = user.UserName;
                }

                if (ent.RealizedDateTime != null)
                {
                    txtRealizedTime.SelectedDate = ent.RealizedDateTime.Value;
                    user = new AppUser();
                    if (user.LoadByPrimaryKey(ent.RealizedByUserID))
                        txtRealizedBy.Text = user.UserName;
                }

                txtQty.Value = Convert.ToDouble(ent.Qty);
                txtNote.Text = ent.Note;
                ComboBox.SelectedValue(cboSRMedicationReason, ent.SRMedicationReason);
                chkIsNotConsume.Checked = ent.IsNotConsume ?? false;
                chkIsReSchedule.Checked = ent.IsReSchedule ?? false;
                chkIsVoidSchedule.Checked = ent.IsVoidSchedule ?? false;
            }

            return med;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            if (newVal != AppEnum.DataMode.Read)
            {
                StandardReference.InitializeIncludeSpace(cboSRMedicationReason, AppEnum.StandardReference.MedicationReason);
                txtScheduleTime.Enabled = ("S".Equals(MedicationStep));
                txtSetupTime.Enabled = ("S".Equals(MedicationStep));
                txtHandoversTime.Enabled = ("H".Equals(MedicationStep));
                txtVerificationTime.Enabled = ("V".Equals(MedicationStep));
                txtRealizedTime.Enabled = ("R".Equals(MedicationStep));

                txtQty.Enabled = ("S".Equals(MedicationStep));
            }
        }
        protected override void OnMenuNewClick()
        {
            // Populate
            var med = PopulateEntryControl();

            // Overwrite
            var scheduleDateTime = (new DateTime()).NowAtSqlServer().AddDays(DayNo);
            txtNote.Text = string.Empty;
            if (!string.IsNullOrEmpty(TimeSchedule))
            {
                txtScheduleDate.SelectedDate = scheduleDateTime.Date;
                var timeSchedules = TimeSchedule.Split(':');
                // Check status
                txtScheduleTime.SelectedDate = new DateTime(scheduleDateTime.Year, scheduleDateTime.Month, scheduleDateTime.Day, timeSchedules[0].ToInt(), timeSchedules[1].ToInt(), 0);
            }
            else
            {
                var startDateTime = Convert.ToDateTime(med.StartDateTime);
                txtScheduleDate.SelectedDate = startDateTime.Date <= scheduleDateTime.Date ? scheduleDateTime.Date : startDateTime.Date;

                if (txtScheduleDate.SelectedDate == startDateTime.Date || txtScheduleDate.SelectedDate < startDateTime.Date)
                {
                    // MinDate diterapkan hanya untuk tgl awal
                    txtScheduleTime.MinDate = Convert.ToDateTime(med.StartDateTime);
                }

                txtScheduleTime.SelectedDate = startDateTime <= scheduleDateTime ? scheduleDateTime : startDateTime;
            }

            // Hanya setup yg bisa dirubah schedule time nya
            // Karena ada kasus hari pertama obat bisa saja harus segera diminum tanpa melihat jam schedulenya
            // Dan di ICU schedule hari pertama masuk jamnya bisa tidak ikut schedule
            txtScheduleTime.Enabled = MedicationStep == "S";
            txtQty.Value = Convert.ToDouble(med.ConsumeQty);

            switch (MedicationStep)
            {
                case "S": //Setup
                    txtSetupTime.SelectedDate = scheduleDateTime.AddDays(0 - DayNo);
                    txtSetupBy.Text = AppSession.UserLogin.UserName;

                    // TODO: Copy qty dari custom Schedule
                    var qrSchedule = new MedicationScheduleQuery();
                    qrSchedule.Where(qrSchedule.MedicationReceiveNo == MedicationReceiveNo, qrSchedule.ScheduleStartDate <= scheduleDateTime, qrSchedule.ScheduleNo == ScheduleNo);
                    qrSchedule.OrderBy(qrSchedule.ScheduleStartDate.Descending);
                    qrSchedule.es.Top = 1;

                    var histSch = new MedicationSchedule();
                    if (histSch.Load(qrSchedule))
                        txtQty.Value = Convert.ToDouble(histSch.Qty);
                    break;
                case "H": // Handovers
                    txtHandoversTime.SelectedDate = scheduleDateTime.AddDays(0 - DayNo);
                    txtHandoversBy.Text = AppSession.UserLogin.UserName;
                    break;
                case "V": // Verification
                    txtVerificationTime.SelectedDate = scheduleDateTime.AddDays(0 - DayNo);
                    txtVerificationBy.Text = AppSession.UserLogin.UserName;
                    break;
                case "R": // Realization
                    txtRealizedTime.SelectedDate = scheduleDateTime;
                    txtRealizedBy.Text = AppSession.UserLogin.UserName;
                    break;
            }
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save(args);
        }

        private void Save(ValidateArgs args)
        {
            using (var tr = new esTransactionScope())
            {
                var ent = new MedicationReceiveUsed();
                if (!ent.LoadByPrimaryKey(MedicationReceiveNo, SequenceNo))
                {
                    ent.MedicationReceiveNo = MedicationReceiveNo;
                    ent.SequenceNo = NewSequenceNo();
                    ent.IsAdditionalSchedule = IsAdditionalSchedule;
                }

                ent.IsVoidSchedule = false;
                ent.IsReSchedule = false;
                ent.IsNotConsume = chkIsReSchedule.Checked || chkIsVoidSchedule.Checked || chkIsNotConsume.Checked;

                var time = new DateTime();
                var scheduleDate = txtScheduleDate.SelectedDate.Value;
                switch (MedicationStep)
                {
                    case "S": //Setup
                        time = txtScheduleTime.SelectedDate.Value;
                        ent.ScheduleDateTime = new DateTime(scheduleDate.Year, scheduleDate.Month, scheduleDate.Day, time.Hour, time.Minute, 0);

                        time = txtSetupTime.SelectedDate.Value;
                        ent.SetupDateTime = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);
                        ent.SetupByUserID = AppSession.UserLogin.UserID;
                        ent.Qty = Convert.ToDecimal(txtQty.Value);
                        ent.IsVoidSchedule = chkIsVoidSchedule.Checked;

                        // Jika Not COnsume pada saat setup berarti dianggap langsung mengurangi stok dan tidak usah ke tahap berikutnya
                        if (ent.IsNotConsume == true)
                        {
                            ent.VerificationDateTime = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);
                            ent.VerificationByUserID = AppSession.UserLogin.UserID;

                            ent.RealizedDateTime = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);
                            ent.RealizedByUserID = AppSession.UserLogin.UserID;
                        }

                        break;
                    case "H": // Handovers
                        time = txtHandoversTime.SelectedDate.Value;
                        ent.HandoversDateTime = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);
                        ent.HandoversByUserID = AppSession.UserLogin.UserID;
                        ent.HandoversToUserID = cboHandoversToUserID.SelectedValue;
                        break;
                    case "V": // Verification
                        time = txtVerificationTime.SelectedDate.Value;
                        ent.VerificationDateTime = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);
                        ent.VerificationByUserID = AppSession.UserLogin.UserID;
                        ent.IsVoidSchedule = chkIsVoidSchedule.Checked;

                        // VoidSchedule tidak usah ke tahap berikutnya
                        if (chkIsVoidSchedule.Checked)
                        {
                            ent.RealizedDateTime = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);
                            ent.RealizedByUserID = AppSession.UserLogin.UserID;
                        }
                        break;
                    case "R": // Realization
                        time = txtRealizedTime.SelectedDate.Value;
                        ent.RealizedDateTime = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);
                        ent.RealizedByUserID = AppSession.UserLogin.UserID;
                        ent.IsReSchedule = chkIsReSchedule.Checked;
                        break;
                }

                ent.Note = txtNote.Text;
                ent.SRMedicationReason = cboSRMedicationReason.SelectedValue;

                ent.Save();
                tr.Complete();
            }
        }


        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Save(args);
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
            var timeNow = (new DateTime()).NowAtSqlServer();
            switch (MedicationStep)
            {
                case "S": //Setup
                    if (txtSetupTime.IsEmpty)
                    {
                        txtSetupTime.SelectedDate = timeNow;
                        txtSetupBy.Text = AppSession.UserLogin.UserName;
                    }
                    break;
                case "H": //Handovers
                    if (txtHandoversTime.IsEmpty)
                    {
                        txtHandoversTime.SelectedDate = timeNow;
                        txtHandoversBy.Text = AppSession.UserLogin.UserName;
                    }
                    break;
                case "V": // Verification
                    if (txtVerificationTime.IsEmpty)
                    {
                        txtVerificationTime.SelectedDate = timeNow;
                        txtVerificationBy.Text = AppSession.UserLogin.UserName;
                    }
                    break;
                case "R": // Realization
                    if (txtRealizedTime.IsEmpty)
                    {
                        txtRealizedTime.SelectedDate = timeNow.AddDays(DayNo);
                        txtRealizedBy.Text = AppSession.UserLogin.UserName;
                    }

                    break;
            }
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
            var qr = new MedicationReceiveUsedQuery("a");
            var fb = new MedicationReceiveUsed();
            qr.es.Top = 1;
            qr.Where(qr.MedicationReceiveNo == MedicationReceiveNo);
            qr.OrderBy(qr.SequenceNo.Descending);

            if (fb.Load(qr))
            {
                return fb.SequenceNo.ToInt() + 1;
            }
            return 1;
        }


        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQty.Value == 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = @"Qty must >0";
                return;
            }

            if (MedicationStep.Equals("H") && string.IsNullOrWhiteSpace(cboHandoversToUserID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Please select Handovers Receive By";
                return;
            }
            //if (string.IsNullOrEmpty(cboParamedicID.SelectedValue))
            //{
            //    args.IsValid = false;
            //    ((CustomValidator)source).ErrorMessage = @"Physician is not selected properly";
            //}
        }
    }
}

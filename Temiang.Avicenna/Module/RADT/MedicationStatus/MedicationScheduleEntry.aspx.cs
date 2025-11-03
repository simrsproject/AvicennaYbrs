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
    public partial class MedicationScheduleEntry : BasePageDialogEntry
    {
        private int MedicationReceiveNo
        {
            get
            {
                return Request.QueryString["medrecno"].ToInt();
            }
        }
        private DateTime? ScheduleDate
        {
            get
            {
                var scDate = Request.QueryString["scdate"];
                if (string.IsNullOrWhiteSpace(scDate)) return null;
                return Convert.ToDateTime(scDate);
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
        protected void Page_Init(object sender, EventArgs e)
        {
            if (AppConstant.Program.PharmaceuticalCare.Equals(Request.QueryString["prgid"]))
                ProgramID = AppConstant.Program.PharmaceuticalCare;
            else
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            this.Title = "Medication Schedule Setup Entry";

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
            lblScheduleTime.Text = String.Format("Schedule Time ({0})", ScheduleNo);
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

            var scheduleDate = ScheduleDate ?? DateTime.Today;
            var qrLastSche = new MedicationScheduleQuery();
            qrLastSche.es.Top = 1;
            qrLastSche.Where(qrLastSche.MedicationReceiveNo == MedicationReceiveNo, qrLastSche.ScheduleStartDate <= scheduleDate, qrLastSche.ScheduleNo == ScheduleNo);
            qrLastSche.OrderBy(qrLastSche.ScheduleStartDate.Descending);
            var ent = new MedicationSchedule();
            if (ent.Load(qrLastSche))
            {
                if (ent.ScheduleTime != null)
                {
                    txtScheduleDate.SelectedDate = ent.ScheduleTime.Value.Date;
                    txtScheduleTime.SelectedTime = ent.ScheduleTime.Value.TimeOfDay;
                }

                txtQty.Value = Convert.ToDouble(ent.Qty);
            }

            return med;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {

        }
        protected override void OnMenuNewClick()
        {
            // Populate
            var med = PopulateEntryControl();

            // Overwrite
            if (txtQty.Value == null || txtQty.Value==0)
                txtQty.Value = Convert.ToDouble(med.ConsumeQty);

            var scheduleDate = ScheduleDate ?? DateTime.Today;
            if (!string.IsNullOrEmpty(TimeSchedule) && txtScheduleTime.IsEmpty)
            {
                txtScheduleDate.SelectedDate = ScheduleDate;
                var timeSchedules = TimeSchedule.Split(':');
                txtScheduleTime.SelectedDate = new DateTime(scheduleDate.Year, scheduleDate.Month, scheduleDate.Day, timeSchedules[0].ToInt(), timeSchedules[1].ToInt(), 0);
            }
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save();
        }

        private void Save()
        {
            using (var tr = new esTransactionScope())
            {
                var scheduleStartDate = txtScheduleDate.SelectedDate.Value.Date;
                var ent = new MedicationSchedule();
                if (!ent.LoadByPrimaryKey(MedicationReceiveNo, scheduleStartDate, ScheduleNo))
                {
                    ent.MedicationReceiveNo = MedicationReceiveNo;
                    ent.ScheduleStartDate = scheduleStartDate;
                    ent.ScheduleNo = ScheduleNo;
                }

                var time = txtScheduleTime.SelectedDate.Value;
                ent.ScheduleTime = new DateTime(scheduleStartDate.Year, scheduleStartDate.Month, scheduleStartDate.Day, time.Hour, time.Minute, 0);

                ent.Qty = Convert.ToDecimal(txtQty.Value);
                ent.Save();
                tr.Complete();
            }
        }


        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Save();
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

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQty.Value == 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = @"Qty must >0";
            }
        }
    }
}

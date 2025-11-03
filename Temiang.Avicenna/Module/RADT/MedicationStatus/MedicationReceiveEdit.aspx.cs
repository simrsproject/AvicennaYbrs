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
using Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Medication
{
    public partial class MedicationReceiveEdit : BasePageDialogEntry
    {
        public int MedicationReceiveNo
        {
            get
            {
                return Request.QueryString["medrecno"].ToInt();
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

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

            this.Title = "Medication Receive Edit";

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRMedicationConsume, AppEnum.StandardReference.MedicationConsume);
                StandardReference.InitializeIncludeSpace(cboSRMedicationRoute, AppEnum.StandardReference.Route);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region override method

        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            PopulateEntryControl();
        }

        private void PopulateEntryControl()
        {
            // Hanya tuk single entry
            var med = new MedicationReceive();
            med.LoadByPrimaryKey(MedicationReceiveNo);
            lblItemDescription.Text = med.ItemDescription;
            txtBalanceRealQty.Value = (double)med.BalanceRealQty;
            txtItemUnit.Text = med.SRConsumeUnit;
            txtItemUnit2.Text = med.SRConsumeUnit;

            var cm = new ConsumeMethod();
            if (cm.LoadByPrimaryKey(med.SRConsumeMethod))
                txtConsumeMethod.Text = string.Format("{0} ({1}) @{2} {3}", cm.SygnaText, cm.SRConsumeMethodName, med.ConsumeQty, med.SRConsumeUnit);

            txtStartDateTime.SelectedDate = med.StartDateTime;
            txtReceiveQty.Value = Convert.ToDouble(med.ReceiveQty ?? 0);
            ComboBox.SelectedValue(cboSRMedicationConsume, med.SRMedicationConsume);
            ComboBox.SelectedValue(cboSRMedicationRoute, med.SRMedicationRoute);

            var reg = new Registration();
            reg.LoadByPrimaryKey(med.RegistrationNo);
            txtBed.Text = reg.BedID;

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);
            txtPatientName.Text = pat.PatientName;

            var su = new ServiceUnit();
            su.LoadByPrimaryKey(reg.ServiceUnitID);
            txtServiceUnitName.Text = su.ServiceUnitName;

            var room = new ServiceRoom();
            room.LoadByPrimaryKey(reg.RoomID);
            txtRoomName.Text = room.RoomName;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }

        protected override void OnMenuNewClick()
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save();
        }

        private void Save()
        {
            // Update Balance
            var med = new MedicationReceive();
            med.LoadByPrimaryKey(MedicationReceiveNo);
            med.StartDateTime = txtStartDateTime.SelectedDate;
            med.SRMedicationConsume = cboSRMedicationConsume.SelectedValue;
            med.SRMedicationRoute = cboSRMedicationRoute.SelectedValue;
            if (txtBalanceRealQty.Value != txtReceiveQty.Value) // Receive diedit
            {
                var recQtyUpd = Convert.ToDecimal(txtReceiveQty.Value ?? 0);
                var recQty = med.ReceiveQty;

                med.ReceiveQty = recQtyUpd;
                med.BalanceRealQty = med.BalanceRealQty + (recQtyUpd - recQty);
                med.BalanceQty = med.BalanceQty + (recQtyUpd - recQty);
            }

            med.Save();
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
            // Receive Qty tidak bisa diedit jika sudah ada history setupnya (ReciveQty != BalaneQty
            txtReceiveQty.Enabled = (txtReceiveQty.Value == txtBalanceRealQty.Value);
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
            if (!(txtReceiveQty.Value > 0))
            {
                args.IsValid = false;
                customValidator.ErrorMessage = "Receive Qty must > 0";
                return;
            }
        }
    }
}

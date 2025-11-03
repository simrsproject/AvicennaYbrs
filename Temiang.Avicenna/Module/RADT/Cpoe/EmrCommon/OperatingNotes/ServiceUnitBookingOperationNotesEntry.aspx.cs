using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ServiceUnitBookingOperationNotesEntry : BasePageDialogEntry
    {
        protected string BookingNo
        {
            get
            {
                return Request.QueryString["bno"];
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ServiceUnitBookingOperationNotes;

            // Program Fiture
            IsSingleRecordMode = true; //Save then close
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;
            ToolBar.NavigationVisible = false;
            ToolBar.EditVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Operating Notes of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }

            }
        }

        #region override method

        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var booking = new ServiceUnitBooking();
            booking.LoadByPrimaryKey(BookingNo);
            txtRegistrationNo.Text = booking.RegistrationNo;

            txtPatientID.Text = booking.PatientID;

            var p = new Patient();
            if (p.LoadByPrimaryKey(txtPatientID.Text))
            {
                txtMedicalNo.Text = p.MedicalNo;
                txtPatientName.Text = p.PatientName;
                txtAddress.Text = p.Address;
                txtPhoneNo.Text = p.MobilePhoneNo;
            }

            txtBookingNo.Text = booking.BookingNo;

            var prm = new Paramedic();
            if (prm.LoadByPrimaryKey(booking.str.ParamedicID))
            {
                txtPhysician1.Text = prm.ParamedicName;
            }

            rbMale.Checked = p.Sex == "M";
            rbMale.Enabled = p.Sex == "M";
            rbFemale.Checked = p.Sex == "F";
            rbFemale.Enabled = p.Sex == "F";
            txtYear.Text = Helper.GetAgeInYear(p.DateOfBirth.Value).ToString();
            txtMonth.Text = Helper.GetAgeInMonth(p.DateOfBirth.Value).ToString();
            txtDay.Text = Helper.GetAgeInDay(p.DateOfBirth.Value).ToString();

            txtOperatingNotes.Text = booking.OperatingNotes;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {
            txtOperatingNotes.Text = string.Empty;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (!Page.IsValid)
            {
                args.IsCancel = true;
                return;
            }

            var entity = new ServiceUnitBooking();
            using (var trans = new esTransactionScope())
            {
                entity.OperatingNotes = txtOperatingNotes.Text;
                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (!Page.IsValid)
            {
                args.IsCancel = true;
                return;
            }

            var entity = new ServiceUnitBooking();
            if (!entity.LoadByPrimaryKey(BookingNo))
            {
                args.IsCancel = true;
                args.MessageText = "Current record has not exist";
                return;
            }

            using (var trans = new esTransactionScope())
            {
                entity.OperatingNotes = txtOperatingNotes.Text;
                entity.Save();

                trans.Complete();
            }
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

        protected object ScriptCopyTemplate()
        {
            var retval = string.Empty;
            retval =
                "<a href='#' onclick=\"javascript:openOperatingNotesTemplate(); return false;\"><img src=\"../../../../../Images/Toolbar/ordering16.png\" alt=\"a\" />&nbsp; Copy from template</a>";

            return retval;
        }
        protected object ScriptNewTemplate()
        {
            var retval = string.Empty;
            retval =
                "<a href='#' onclick=\"javascript:openOperatingNotesTemplateNew(); return false;\"><img src=\"../../../../../Images/Toolbar/copy16.png\" alt=\"b\" />&nbsp;Save to Template</a>";

            return retval;
        }
    }
}

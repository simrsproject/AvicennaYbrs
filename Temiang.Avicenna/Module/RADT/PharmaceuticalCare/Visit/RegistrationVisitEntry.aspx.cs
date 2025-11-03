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
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.PharmaceuticalCare
{
    public partial class RegistrationVisitEntry : BasePageDialogEntry
    {
        public override string RegistrationNo => Request.QueryString["regno"];
        public override string PatientID => Request.QueryString["patid"];
        private int VisitNo => Request.QueryString["vn"].ToInt();

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PharmaceuticalCare;

            // Program Fiture
            IsSingleRecordMode = true; //Save then close
            IsMedicalRecordEntry = true; //Activate deadline edit & add
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
                    this.Title = "Visit Note of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
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
            var ent = new RegistrationVisit();
            ent.LoadByPrimaryKey(RegistrationNo, VisitNo);
            txtVisitDateTime.SelectedDate = ent.VisitDateTime;
            txtVisitNotes.Text = ent.VisitNotes;

        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {
            var timeNow = (new DateTime()).NowAtSqlServer();
            txtVisitDateTime.SelectedDate = timeNow;
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save(true);
        }

        private void Save(bool isNewRecord)
        {
            var ent = new RegistrationVisit();
            if (isNewRecord || !ent.LoadByPrimaryKey(RegistrationNo, VisitNo))
            {
                ent.RegistrationNo = RegistrationNo;
                ent.VisitNo = NewVisitNo();

                // Save Unit History krn di reg di tgl berikutnya bisa berubah
                SetServiceUnitHistory(ent, isNewRecord);
            }
            ent.VisitDateTime = txtVisitDateTime.SelectedDate;
            ent.VisitNotes = txtVisitNotes.Text;
            ent.Save();
        }

        private void SetServiceUnitHistory(RegistrationVisit ent, bool isNewRecord)
        {
            // Set history Service Unit
            var isNotSet = true;
            if (!isNewRecord)
            {
                var pt = PatientTransfer.LoadLastTransfer(RegistrationNo, txtVisitDateTime.SelectedDate.Value);
                if (pt != null)
                {
                    ent.ServiceUnitID = pt.ToServiceUnitID;
                    ent.RoomID = pt.ToRoomID;
                    ent.BedID = pt.ToBedID;

                    isNotSet = false;
                }
            }

            if (isNotSet)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(RegistrationNo);
                ent.ServiceUnitID = reg.ServiceUnitID;
                ent.RoomID = reg.RoomID;
                ent.BedID = reg.BedID;
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

        private int NewVisitNo()
        {
            var qr = new RegistrationVisitQuery("a");
            var fb = new RegistrationVisit();
            qr.Where(qr.RegistrationNo == RegistrationNo);
            qr.es.Top = 1;
            qr.OrderBy(qr.VisitNo.Descending);

            if (fb.Load(qr))
            {
                return fb.VisitNo.ToInt() + 1;
            }
            return 1;
        }
    }
}

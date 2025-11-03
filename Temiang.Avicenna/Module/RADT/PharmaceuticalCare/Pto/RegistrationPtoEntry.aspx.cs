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
    public partial class RegistrationPtoEntry : BasePageDialogEntry
    {
        public override string RegistrationNo => Request.QueryString["regno"];
        public override string PatientID => Request.QueryString["patid"];

        private RegistrationPto _current;
        private RegistrationPto RegistrationPtoCurrent
        {
            get
            {
                if (_current == null)
                {
                    var ent = new RegistrationPto();
                    if (!IsPostBack)
                        ent.LoadByPrimaryKey(RegistrationNo, Request.QueryString["cn"].ToInt());
                    else
                        ent.LoadByPrimaryKey(RegistrationNo, txtPtoNo.Text.ToInt());

                    _current = ent;
                }

                return _current;
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PharmaceuticalCare;

            // Program Fiture
            //IsSingleRecordMode = true; //Save then close

            IsMedicalRecordEntry = true; //Activate deadline edit & add
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = true;

            ToolBar.EditVisible = true;
            ToolBar.AddVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Drug Therapy Monitoring of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var ent = RegistrationPtoCurrent;
            txtPtoNo.Text = string.Format("{0:00000}", ent.PtoNo);
            txtPtoDateTime.SelectedDate = ent.PtoDateTime;
            txtPtoS.Text = ent.PtoS;
            txtPtoO.Text = ent.PtoO;
            txtPtoA.Text = ent.PtoA;
            txtPtoP.Text = ent.PtoP;
        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            var isEntryMode = newVal != AppEnum.DataMode.Read;
            lbtnPickA.Visible = isEntryMode;
            lbtnPickP.Visible = isEntryMode;
        }
        protected override void OnMenuNewClick()
        {
            txtPtoNo.Text = string.Format("{0:00000}", NewPtoNo());
            var timeNow = (new DateTime()).NowAtSqlServer();
            txtPtoDateTime.SelectedDate = timeNow;
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save(args, true);
        }

        private bool Save(ValidateArgs args, bool isNewRecord)
        {
            using (var trans = new esTransactionScope())
            {
                var ent = new RegistrationPto();
                if (isNewRecord || !ent.LoadByPrimaryKey(RegistrationNo, txtPtoNo.Text.ToInt()))
                {
                    ent.RegistrationNo = RegistrationNo;
                    ent.PtoNo = NewPtoNo();

                    // Save Unit History krn di reg di tgl berikutnya bisa berubah
                    SetServiceUnitHistory(ent, isNewRecord);
                }
                ent.PtoDateTime = txtPtoDateTime.SelectedDate;
                ent.PtoS = txtPtoS.Text;
                ent.PtoO = txtPtoO.Text;
                ent.PtoA = txtPtoA.Text;
                ent.PtoP = txtPtoP.Text;
                ent.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return true;
        }

        private void SetServiceUnitHistory(RegistrationPto ent, bool isNewRecord)
        {
            // Set history Service Unit
            var isNotSet = true;
            if (!isNewRecord)
            {
                var pt = PatientTransfer.LoadLastTransfer(RegistrationNo, txtPtoDateTime.SelectedDate.Value);
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
            Save(args, false);
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
            var ent = RegistrationPtoCurrent;
            ent.IsDeleted = true;
            ent.Save();
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
            // Bisa klik tombol Edit
            return (RegistrationPtoCurrent.IsDeleted ?? false) == false && RegistrationPtoCurrent.CreatedByUserID == AppSession.UserLogin.UserID;
        }

        public override bool OnGetStatusMenuDelete()
        {
            // Bisa klik tombol Delete
            return (RegistrationPtoCurrent.IsDeleted ?? false) == false && RegistrationPtoCurrent.CreatedByUserID == AppSession.UserLogin.UserID;
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

        private int NewPtoNo()
        {
            var qr = new RegistrationPtoQuery("a");
            var fb = new RegistrationPto();
            qr.Where(qr.RegistrationNo == RegistrationNo);
            qr.es.Top = 1;
            qr.OrderBy(qr.PtoNo.Descending);

            if (fb.Load(qr))
            {
                return fb.PtoNo.ToInt() + 1;
            }
            return 1;
        }
    }
}

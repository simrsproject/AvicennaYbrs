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
using System.Text;
using System.Web.Services;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class EpisodeDiagnoseEntry : BasePageDialogEntry
    {
        private string SequenceNo
        {
            get { return Request.QueryString["seqno"]; }
        }

        private CpoeTypeEnum CpoeType
        {
            get
            {
                switch (this.Request.QueryString["rt"])
                {
                    case "IPR":
                        return CpoeTypeEnum.InPatient;
                    //case "EMR":
                    //    return CpoeTypeEnum.Emergency;
                    default:
                        return CpoeTypeEnum.Outpatient;
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            switch (CpoeType)
            {
                case CpoeTypeEnum.InPatient:
                    ProgramID = AppConstant.Program.CpoeInPatient;
                    break;
                case CpoeTypeEnum.Emergency:
                    ProgramID = AppConstant.Program.CpoeEmergency;
                    break;
                case CpoeTypeEnum.Outpatient:
                    ProgramID = AppConstant.Program.CpoeOutPatient;
                    break;
            }
            // Program Fiture
            IsSingleRecordMode = true; //Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Diagnose for : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }
            StandardReference.Initialize(cboSRDiagnoseType, AppEnum.StandardReference.DiagnoseType);
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }
 
        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            // Hanya tuk single entry
            var ent = new EpisodeDiagnose();
            ent.LoadByPrimaryKey(RegistrationNo, SequenceNo);
            ComboBox.PopulateWithOneDiagnose(cboDiagnoseID,ent.DiagnoseID);
            chkIsOldCase.Checked = ent.IsOldCase??false;
            ComboBox.SelectedValue(cboSRDiagnoseType,ent.SRDiagnoseType);
            txtDiagnosisText.Text = ent.DiagnosisText;
            txtDiagnosisNotes.Text = ent.Notes;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {
            cboDiagnoseID.SelectedValue = string.Empty;
            cboDiagnoseID.Text = string.Empty;
            chkIsOldCase.Checked = false;
            cboSRDiagnoseType.SelectedIndex = 0;
            txtDiagnosisText.Text = string.Empty;
            txtDiagnosisNotes.Text = string.Empty;
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            SaveDiagnosis();
        }


        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            SaveDiagnosis();
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

        protected void cboDiagnoseID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateDiagnoseName();
        }

        private void PopulateDiagnoseName()
        {
            if (cboDiagnoseID.SelectedValue == string.Empty)
            {
                chkIsOldCase.Checked = false;
                txtDiagnosisText.Text = string.Empty;
                return;
            }

            var ed = new EpisodeDiagnoseQuery("a");
            var reg = new RegistrationQuery("c");

            ed.es.Top = 1;
            ed.InnerJoin(reg).On(ed.RegistrationNo == reg.RegistrationNo);
            ed.Where(
                ed.DiagnoseID == cboDiagnoseID.SelectedValue,
                reg.PatientID == PatientID
                );

            var eds = new EpisodeDiagnoseCollection();
            eds.Load(ed);

            chkIsOldCase.Checked = eds.Any();

            var entity = new Diagnose();
            if (entity.LoadByPrimaryKey(cboDiagnoseID.SelectedValue)) txtDiagnosisText.Text = entity.DiagnoseName;
        }

        private void SaveDiagnosis()
        {
            var ed = new EpisodeDiagnose();
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                var eds = new EpisodeDiagnoseCollection();
                eds.Query.Where(eds.Query.RegistrationNo == RegistrationNo);
                eds.LoadAll();

                var seqNo = (eds.OrderByDescending(j => j.SequenceNo)).Take(1).SingleOrDefault();
                ed.SequenceNo = (seqNo == null || string.IsNullOrEmpty(seqNo.SequenceNo))
                    ? "001"
                    : string.Format("{0:000}", int.Parse(seqNo.SequenceNo) + 1);
                ed.RegistrationNo = RegistrationNo;
            }
            else
            {
                ed.LoadByPrimaryKey(RegistrationNo, SequenceNo);
            }

            ed.DiagnoseID = cboDiagnoseID.SelectedValue;
            ed.SRDiagnoseType = cboSRDiagnoseType.SelectedValue;
            ed.DiagnosisText = txtDiagnosisText.Text;
            ed.MorphologyID = string.Empty;
            ed.ParamedicID = ParamedicID;
            ed.IsAcuteDisease = false;
            ed.IsChronicDisease = false;
            ed.IsOldCase = chkIsOldCase.Checked;
            ed.IsConfirmed = false;
            ed.IsVoid = false;
            ed.Notes = txtDiagnosisNotes.Text;

            ed.Save();
        }

    }
}

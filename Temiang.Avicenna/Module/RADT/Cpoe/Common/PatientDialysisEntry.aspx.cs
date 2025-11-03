using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class PatientDialysisEntry : BasePageDialogEntry
    {
        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var entity = new PatientDialysis();
            if (entity.LoadByPrimaryKey(PatientID))
            {
                txtInitialDiagnosis.Text = entity.InitialDiagnosis;
                txtRefferingHospital.Text = entity.RefferingHospital;
                txtRefferingPhysician.Text = entity.RefferingPhysician;
                txtHb.Text = entity.Hb;
                txtHbDate.SelectedDate = entity.HbDate;
                txtUrea.Text = entity.Urea;
                txtUreaDate.SelectedDate = entity.UreaDate;
                txtCreatinine.Text = entity.Creatinine;
                txtCreatinineDate.SelectedDate = entity.CreatinineDate;
                txtHBsAg.Text = entity.HBsAg;
                txtHBsAgDate.SelectedDate = entity.HBsAgDate;
                txtAntiHCV.Text = entity.AntiHCV;
                txtAntiHCVDate.SelectedDate = entity.AntiHCVDate;
                txtAntiHIV.Text = entity.AntiHIV;
                txtAntiHIVDate.SelectedDate = entity.AntiHIVDate;
                txtKidneyUltrasound.Text = entity.KidneyUltrasound;
                txtKidneyUltrasoundDate.SelectedDate = entity.KidneyUltrasoundDate;
                txtECHO.Text = entity.ECHO;
                txtECHODate.SelectedDate = entity.ECHODate;
                txtFirstHemodialysisDate.SelectedDate = entity.FirstHDDate;
                txtTransferHDDate.SelectedDate = entity.TransferHDDate;
                txtFirstPeritonealDialysisDate.SelectedDate = entity.FirstPDDate;
                txtTransferPDDate.SelectedDate = entity.TransferPDDate;
                txtKidneytransplantDate.SelectedDate = entity.KidneyTransplantDate;
            }
        }

        protected void OnPopulateEntryControl(esEntity entity)
        {
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                var pd = new PatientDialysisCollection();
                pd.Query.Where(pd.Query.PatientID == PatientID);
                pd.LoadAll();
                pd.MarkAllAsDeleted();
                pd.Save();

                var entity = new PatientDialysis();
                entity.AddNew();

                entity.PatientID = PatientID;
                entity.InitialDiagnosis = txtInitialDiagnosis.Text;
                entity.RefferingHospital = txtRefferingHospital.Text;
                entity.RefferingPhysician = txtRefferingPhysician.Text;
                entity.Hb = txtHb.Text;
                entity.HbDate = txtHbDate.SelectedDate;
                entity.Urea = txtUrea.Text;
                entity.UreaDate = txtUreaDate.SelectedDate;
                entity.Creatinine = txtCreatinine.Text;
                entity.CreatinineDate = txtCreatinineDate.SelectedDate;
                entity.HBsAg = txtHBsAg.Text;
                entity.HBsAgDate = txtHBsAgDate.SelectedDate;
                entity.AntiHCV = txtAntiHCV.Text;
                entity.AntiHCVDate = txtAntiHCVDate.SelectedDate;
                entity.AntiHIV = txtAntiHIV.Text;
                entity.AntiHIVDate = txtAntiHIVDate.SelectedDate;
                entity.KidneyUltrasound = txtKidneyUltrasound.Text;
                entity.KidneyUltrasoundDate = txtKidneyUltrasoundDate.SelectedDate;
                entity.ECHO = txtECHO.Text;
                entity.ECHODate = txtECHODate.SelectedDate;
                entity.FirstHDDate = txtFirstHemodialysisDate.SelectedDate;
                entity.TransferHDDate = txtTransferHDDate.SelectedDate;
                entity.FirstPDDate = txtFirstPeritonealDialysisDate.SelectedDate;
                entity.TransferPDDate = txtTransferPDDate.SelectedDate;
                entity.KidneyTransplantDate = txtKidneytransplantDate.SelectedDate;

                if (entity.es.IsAdded)
                {
                    entity.CreatedByUserID = AppSession.UserLogin.UserID;
                    entity.CreatedDateTime = DateTime.Now;
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = DateTime.Now;
                }
                else if (entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

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

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            IsSingleRecordMode = true; //Save then close

            // Program Fiture berguna jika non SingleRecordMode
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;
            // ------------------------------

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Dialysis List for : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }

        }
    }
}
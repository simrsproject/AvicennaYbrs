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
    public partial class SickLetterEntry : BasePageDialogEntry
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicHealthRecord;

            // Program Fiture
            IsSingleRecordMode = true; //Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;

            // Hanya bisa Add
            ToolBar.EditVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Sick Letter of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
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
            var entity = new SickLetter();
            if (entity.LoadByPrimaryKey(RegistrationNo, ParamedicID,"SL"))
            {
                txtSickLetterStartDate.SelectedDate = entity.StartDate;
                txtSickLetterEndDate.SelectedDate = entity.EndDate;
                txtSickLetterNotes.Text = entity.Notes;
            }
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {
            txtSickLetterStartDate.SelectedDate = DateTime.Now.Date;
            txtSickLetterEndDate.SelectedDate = DateTime.Now.Date.AddDays(2);
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save();
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

        private void Save()
        {
            var entity = new SickLetter();
            if (!entity.LoadByPrimaryKey(RegistrationNo, ParamedicID,"SL"))
                entity.AddNew();

            entity.RegistrationNo = RegistrationNo;
            entity.ParamedicID = ParamedicID;
            entity.SRLetterType = "SL";
            entity.StartDate = txtSickLetterStartDate.SelectedDate;
            entity.EndDate = txtSickLetterEndDate.SelectedDate;
            entity.Notes = txtSickLetterNotes.Text;
            entity.Save();

        }

        //private void Print()
        //{
        //    var sl = new SickLetter();
        //    if (sl.LoadByPrimaryKey(RegistrationNo, ParamedicID))
        //    {
        //        PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();
        //        PrintJobParameter jobParameter;
        //        PrintJobParameter jobParameter2;

        //        jobParameter = jobParameters.AddNew();
        //        jobParameter.Name = "RegistrationNo";
        //        jobParameter.ValueString = RegistrationNo;

        //        jobParameter2 = jobParameters.AddNew();
        //        jobParameter2.Name = "ParamedicID";
        //        jobParameter2.ValueString = Request.QueryString["pid"];

        //        AppSession.PrintJobParameters = jobParameters;
        //        AppSession.PrintJobReportID = AppConstant.Report.SickLetter;

        //        string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
        //        "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
        //        "oWnd.Show();" +
        //        "oWnd.Maximize();";
        //        //RadAjaxPanel1.ResponseScripts.Add(script);
        //        ScriptManager.RegisterStartupScript(this, GetType(), "printsickletter", script, true);

        //    }
        //}



    }
}

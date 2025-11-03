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
    public partial class AssessmentDefaultEntry : BasePageDialogEntry
    {
        public string ServiceUnitID
        {
            get
            {
                return Request.QueryString["unit"];
            }
        }
        private bool IsInPatient
        {
            get { return Page.Request.QueryString["rt"] == "IPR"; }
        }
        public string RegistrationInfoMedicID
        {
            get
            {
                if (!IsPostBack)
                {
                    hdnRegistrationInfoMedicID.Value = Request.QueryString["rimid"];
                }
                return hdnRegistrationInfoMedicID.Value;
            }
            set
            {
                hdnRegistrationInfoMedicID.Value = value;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            //List<string> keys = Request.Form.AllKeys.Where(key => key.Contains("referAnswerCtl")).ToList();
            //foreach (string key in keys)
            //{
            //    AddReferAnswerCtl();
            //    break; // hanya buat 1 ctl
            //}
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!IsPostBack)
            {
                // Jawab consul harus oleh dokter bersangkutan jadi amannya pakai AppSession.UserLogin.ParamedicID
                var consult =
                    ParamedicConsultRefer.LastConsultReferTo(MergeRegistrations, AppSession.UserLogin.ParamedicID);
                if (consult != null)
                {
                    AddReferAnswerCtl();
                }
            }
            else
            {
                List<string> keys = Request.Form.AllKeys.Where(key => key.Contains("referAnswerCtl")).ToList();
                foreach (string key in keys)
                {
                    AddReferAnswerCtl();
                    break; // hanya buat 1 ctl
                }
            }
        }

        private void AddReferAnswerCtl()
        {
            //var referAnswerCtl =
            //    (ReferAnswerCtl)
            //        Page.LoadControl("~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/ReferAnswerCtl.ascx");
            var referAnswerCtl = (ConsultReferAnswerCtl)Page.LoadControl(
                "~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/ConsultReferAnswerCtl.ascx");

            referAnswerCtl.Width = "100%";
            referAnswerCtl.ID = "referAnswerCtl";
            pnlReferAnswer.Controls.Add(referAnswerCtl);
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

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Assessment of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(lblChronicDisease.Text))
                lblChronicDisease.Text = Patient.ChronicDisease(PatientID);

            if (Request.QueryString["rt"] != AppConstant.RegistrationType.InPatient)
                trPpaInstruction.Style.Add("display", "none");
        }

        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            PopulateRefer();
            // Hanya tuk single entry
            var infMedic = new RegistrationInfoMedic();
            if (infMedic.LoadByPrimaryKey(RegistrationInfoMedicID))
            {
                hdnServiceUnitID.Value = infMedic.ServiceUnitID;

                if (!string.IsNullOrEmpty(infMedic.ParamedicID))
                {
                    var par = new Paramedic();
                    par.LoadByPrimaryKey(infMedic.ParamedicID);
                    txtSoapParamedicName.Text = par.ParamedicName;
                }
                txtDateSOAP.SelectedDate = infMedic.DateTimeInfo;
                txtTimeSOAP.SelectedDate = Convert.ToDateTime(infMedic.DateTimeInfo);
                txtSubjective.Text = infMedic.Info1Entry;
                txtObjective.Text = infMedic.Info2;
                txtAssesment.Text = infMedic.Info3Entry;
                txtPlanning.Text = infMedic.Info4;
                txtAttendingNotes.Text = infMedic.AttendingNotes;
                txtPpaInstruction.Text = infMedic.PpaInstruction;
                chkIsInformConcern.Checked = infMedic.IsInformConcern ?? false;
            }
            lvLocalistStatus.Rebind();
        }

        private void PopulateRefer()
        {
            if (pnlReferAnswer.Controls.Count > 0)
            {
                foreach (var ctl in pnlReferAnswer.Controls)
                {
                    if (ctl is ConsultReferAnswerCtl)
                    {
                        var referAnswerCtl = (ConsultReferAnswerCtl)ctl;
                        referAnswerCtl.PopulateEntryControl(null, null);
                    }
                }
            }
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            epDiagCtl.Rebind(newVal != AppEnum.DataMode.Read);
        }
        protected override void OnMenuNewClick()
        {
            PopulateRefer();

            var par = new Paramedic();
            par.LoadByPrimaryKey(ParamedicID);

            //hdnSoapRegistrationNo.Value = RegistrationNo;
            //hdnSoapSequenNo.Value = string.Empty;
            //hdnRegistrationInfoMedicID.Value = string.Empty;
            //hdnServiceUnitID.Value = ServiceUnitID;
            //hdnIsFromEpisodeSoape.Value = "true";

            txtSoapParamedicName.Text = par.ParamedicName;
            var timeNow = (new DateTime()).NowAtSqlServer();
            txtDateSOAP.SelectedDate = timeNow;
            txtTimeSOAP.SelectedDate = timeNow;
            txtSubjective.Text = string.Empty;
            txtObjective.Text = string.Empty;
            txtAssesment.Text = string.Empty;
            txtPlanning.Text = string.Empty;
            txtAttendingNotes.Text = string.Empty;
            chkIsInformConcern.Checked = false;

            lvLocalistStatus.Rebind();
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            SaveSoapAndBodyImage(true, args);
        }


        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            SaveSoapAndBodyImage(false, args);
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

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Format("oArg.rimid='{0}'", RegistrationInfoMedicID);
        }

        #endregion

        private void SaveSoapAndBodyImage(bool isNewRecord, ValidateArgs args)
        {
            // Save EpisodeDiagnose
            epDiagCtl.Save(args);
            if (args.IsCancel)
                return;

            // Diagnose (A)
            var diagSummary = EpisodeDiagnose.DiagnoseSummary(RegistrationNo);

            // Cek New or Edit
            RegistrationInfoMedicID = isNewRecord ? SaveNewSoap(diagSummary) : SaveEditSoap(diagSummary);

            // Save Localist / Body Image 
            if (Session["rimBodyImage"] != null)
            {
                var dtbSession = (DataTable)Session["rimBodyImage"];
                foreach (DataRow row in dtbSession.Rows)
                {
                    if (true.Equals(row["IsModified"]))
                    {
                        SaveLocalistStatus(RegistrationInfoMedicID, row["BodyID"].ToString(),
                            (byte[])row["BodyImage"]);
                    }
                }
            }

            Session.Remove("rimBodyImage");
            Session.Remove("rimBodyImage_id");

            // Save ConsultReferAnswerCtl
            if (pnlReferAnswer.Controls.Count > 0)
            {
                foreach (var ctl in pnlReferAnswer.Controls)
                {
                    if (ctl is ConsultReferAnswerCtl)
                    {
                        var referAnswerCtl = (ConsultReferAnswerCtl)ctl;
                        referAnswerCtl.SetEntityValue(null, null, null);
                    }
                }
            }
        }

        private void SaveLocalistStatus(string regInfoMedicID, string bodyId, byte[] bodyImage)
        {
            var es = new RegistrationInfoMedicBodyDiagram();
            if (!es.LoadByPrimaryKey(regInfoMedicID, bodyId))
            {
                es = new RegistrationInfoMedicBodyDiagram
                {
                    RegistrationInfoMedicID = regInfoMedicID,
                    IsDeleted = false,
                    ServiceUnitID = Request.QueryString["unit"],
                    ParamedicID = ParamedicID,
                    BodyID = bodyId,
                    CreatedDateTime = DateTime.Now,
                    CreatedByUserID = AppSession.UserLogin.UserID
                };
            }

            es.BodyImage = bodyImage;
            es.Save();
        }
        private string SaveNewSoap(string diagSummary)
        {
            var ent = new RegistrationInfoMedic();
            SaveRegistrationInfoMedic(ent, diagSummary);
            return ent.RegistrationInfoMedicID;
        }

        private string SaveEditSoap(string diagSummary)
        {
            var ent = new RegistrationInfoMedic();
            ent.LoadByPrimaryKey(RegistrationInfoMedicID);
            SaveRegistrationInfoMedic(ent, diagSummary);
            return ent.RegistrationInfoMedicID;
        }


        private void SaveRegistrationInfoMedic(RegistrationInfoMedic ent, string diagSummary)
        {
            if (string.IsNullOrEmpty(ent.RegistrationInfoMedicID))
            {
                var autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.RegInfoMedicNo);
                ent.RegistrationInfoMedicID = autoNumber.LastCompleteNumber;
                autoNumber.Save();

                ent.RegistrationNo = RegistrationNo;

                ent.SRMedicalNotesInputType = "SOAP"; //Hardcode
                ent.ServiceUnitID = ServiceUnitID;
                ent.ParamedicID = ParamedicID;
            }


            ent.Info1 = txtSubjective.Text;
            ent.Info1Entry = txtSubjective.Text;
            ent.Info2 = txtObjective.Text;
            ent.Info3Entry = txtAssesment.Text;
            //Assessment / Diagnose
            ent.Info3 = string.IsNullOrEmpty(ent.Info3) ? string.Concat(txtAssesment.Text, Environment.NewLine, diagSummary) : txtAssesment.Text;

            ent.Info4 = txtPlanning.Text;
            var date = Convert.ToDateTime(txtDateSOAP.SelectedDate);
            var time = Convert.ToDateTime(txtTimeSOAP.SelectedDate);
            ent.DateTimeInfo = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);

            ent.AttendingNotes = txtAttendingNotes.Text;
            ent.IsInformConcern = chkIsInformConcern.Checked;
            ent.PpaInstruction = txtPpaInstruction.Text;
            ent.PopulatePrescriptionCurrentDay();

            ent.Save();
        }

        protected void lvLocalistStatus_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            if (!IsPostBack || !RegistrationInfoMedicID.Equals(Session["rimBodyImage_id"]))
            {
                PopulateBodyImageSession();
            }

            var dtbSession = (DataTable)Session["rimBodyImage"];
            lvLocalistStatus.DataSource = dtbSession;
        }

        private void PopulateBodyImageSession()
        {
            var qr = new RegistrationInfoMedicBodyDiagramQuery("rim");
            var qrSubd = new BodyDiagramServiceUnitQuery("bsu");

            if (string.IsNullOrEmpty(RegistrationInfoMedicID))
            {
                qr.RightJoin(qrSubd).On(qr.BodyID == qrSubd.BodyID && qr.RegistrationInfoMedicID == "_");

            }
            else
            {
                qr.RightJoin(qrSubd).On(qr.BodyID == qrSubd.BodyID && qr.RegistrationInfoMedicID == RegistrationInfoMedicID);
            }
            var qrBody = new BodyDiagramQuery("bd");
            qr.InnerJoin(qrBody).On(qrSubd.BodyID == qrBody.BodyID);

            qr.Select(qr.RegistrationInfoMedicID,
                "<CASE WHEN rim.RegistrationInfoMedicID IS NULL THEN bd.BodyImage ELSE rim.BodyImage END as BodyImage>",
                qr.LastUpdateByUserID, qr.CreatedDateTime, qr.LastUpdateDateTime,
                "<CASE WHEN rim.RegistrationInfoMedicID IS NULL THEN 'new' ELSE 'edit' END as EntryMode>",
                qrBody.BodyID, qrBody.BodyName, "<CONVERT(BIT,0) IsModified>");

            qr.Where(qrSubd.ServiceUnitID == ServiceUnitID);

            var dtb = qr.LoadDataTable();

            // Jangan rubah session kecuali dirubah juga pada page /Module/RADT/Cpoe/Common/LocalistStatus/LocalistStatusEntry.aspx
            Session["rimBodyImage_id"] = RegistrationInfoMedicID;
            Session["rimBodyImage"] = dtb;
        }


        #region Vital Sign
        protected void grdVitalSign_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            // Vitalsign from last PHR in regno
            var lastPhr = new PatientHealthRecord();
            var qrLastPhr = new PatientHealthRecordQuery("a");
            qrLastPhr.Where(qrLastPhr.RegistrationNo == RegistrationNo);
            qrLastPhr.es.Top = 1;
            qrLastPhr.OrderBy(qrLastPhr.TransactionNo.Descending);

            if (lastPhr.Load(qrLastPhr))
            {
                var phrl = new PatientHealthRecordLineQuery("phrl");
                var quest = new QuestionQuery("q");
                phrl.InnerJoin(quest).On(phrl.QuestionID == quest.QuestionID);

                var vital = new VitalSignQuery("v");
                phrl.InnerJoin(vital).On(quest.VitalSignID == vital.VitalSignID);

                phrl.Where(phrl.TransactionNo == lastPhr.TransactionNo);

                phrl.Select(vital.VitalSignID, vital.VitalSignName, phrl.QuestionAnswerPrefix, phrl.QuestionAnswerSuffix,
                    phrl.QuestionAnswerText, phrl.QuestionAnswerNum, phrl.QuestionAnswerText2);
                phrl.OrderBy(vital.SRVitalSignGroup.Ascending, vital.RowIndexInGroup.Ascending);
                var dtb = phrl.LoadDataTable();
                dtb.Columns.Add("QuestionAnswerFormatted", typeof(System.String));

                foreach (System.Data.DataRow r in dtb.Rows)
                {
                    r["QuestionAnswerFormatted"] = string.Format("{0} {1}",
                        string.IsNullOrEmpty(r["QuestionAnswerText"].ToString())
                            ? Helper.RemoveZeroDigits(
                                Convert.ToDecimal(r["QuestionAnswerNum"] == DBNull.Value ? -1 : r["QuestionAnswerNum"]))
                            : (r["QuestionAnswerText"].ToString()),
                        string.IsNullOrEmpty(r["QuestionAnswerSuffix"].ToString())
                            ? string.Empty
                            : r["QuestionAnswerSuffix"].ToString()
                        );
                }
                dtb.AcceptChanges();

                grdVitalSign.DataSource = dtb;
            }
        }
        #endregion

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Linq;

namespace Temiang.Avicenna.Module.HR.Credential.Process
{
    public partial class CredentialingDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.CredentialingNo);
            return _autoNumber.LastCompleteNumber;
        }

        private string QuestionFormID
        {
            get { return AppSession.Parameter.QuestionFormCredentialing; }
        }

        private string FormType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        private string Role
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["role"]) ? "usr" : Request.QueryString["role"];
            }
        }

        private string EvalRole
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["eval"]) ? "1" : Request.QueryString["eval"];
            }
        }

        private string personId
        {
            get { return string.IsNullOrEmpty(Request.QueryString["pid"]) ? string.Empty : Request.QueryString["pid"]; }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            switch (FormType)
            {
                case "caa":
                    ProgramID = Role == "usr" ? AppConstant.Program.CredentialCompetencyAssessmentApplication : (Role == "eva" ? AppConstant.Program.CredentialCompetencyAssessmentEvaluator : AppConstant.Program.CredentialCompetencyAssessmentProcess);
                    break;
                case "apl":
                    ProgramID = AppConstant.Program.CredentialApplication;
                    break;
                case "rec":
                    ProgramID = Role == AppSession.Parameter.EmployeeProfessionGroupMedical ? AppConstant.Program.CredentialProcessMedicalCommittee : (Role == AppSession.Parameter.EmployeeProfessionGroupNursing ? AppConstant.Program.CredentialProcessNursingCommittee : AppConstant.Program.CredentialProcessKtkl);
                    break;
                case "ltr":
                    ProgramID = Role == AppSession.Parameter.EmployeeProfessionGroupMedical ? AppConstant.Program.RecommendationLetterMedicalCommittee : (Role == AppSession.Parameter.EmployeeProfessionGroupNursing ? AppConstant.Program.RecommendationLetterNursingCommittee : AppConstant.Program.RecommendationLetterKtkl);
                    break;
                case "cal":
                    ProgramID = AppConstant.Program.ClinicalAssignmentLetter;
                    break;
                case "cal2":
                    ProgramID = (Role == AppSession.Parameter.EmployeeProfessionGroupNursing ? AppConstant.Program.ClinicalAssignmentLetter_Komkep : AppConstant.Program.ClinicalAssignmentLetter_Ktkl);
                    break;

                case "gen":
                    ProgramID = AppConstant.Program.EmployeeLogbook;
                    break;
                case "c01":
                    ProgramID = AppConstant.Program.EmployeeLogbookMedicalCommitte;
                    break;
                case "c02":
                    ProgramID = AppConstant.Program.EmployeeLogbookNursingCommitte;
                    break;
                case "c03":
                    ProgramID = AppConstant.Program.EmployeeLogbookKtkl;
                    break;

            }
            if (FormType != "gen" & FormType != "c01" & FormType != "c02" & FormType != "c03")
            {
                if (FormType == "cal2")
                    UrlPageList = "../ClinicalAssignmentLetter/ClinicalAssignmentLetterList.aspx?pg="+ Role;
                else
                    UrlPageList = (FormType == "caa" && Role == "usr") ? "CredentialingList.aspx?type=caa" : "CredentialingAssessmentList.aspx?type=" + FormType + "&role=" + Role;
                UrlPageSearch = (FormType == "caa" && Role == "usr") ? "CredentialingSearch.aspx?role=" + Role : "##";
            }
            else
            {
                UrlPageList = "../../EmployeeHR/Logbook/LogbookDetail.aspx?id=" + personId + "&type=" + FormType + "&role=" + Role;
                UrlPageSearch = "##";
            }

            WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSREmploymentType, AppEnum.StandardReference.EmploymentType);
                StandardReference.InitializeIncludeSpace(cboSRProfessionGroup, AppEnum.StandardReference.ProfessionGroup);

                StandardReference.InitializeIncludeSpace(cboSREducationLevel, AppEnum.StandardReference.EducationLevel);
                StandardReference.InitializeIncludeSpace(cboSRCredentialingStatus, AppEnum.StandardReference.CredentialingStatus);
                StandardReference.InitializeIncludeSpace(cboSRRecredentialReason, AppEnum.StandardReference.RecredentialReason);

                pnlCompetencyAssessmentEvaluator.Visible = !(FormType == "caa" && Role == "usr"); //&& !AppSession.Parameter.IsCompetencyAssessmentUsingSingleEvaluator;

                trCompetencyAssessmentVerificationDate.Visible = !(FormType == "caa" && Role != "svr");
                rfvCompetencyAssessmentVerificationDate.Visible = (FormType == "caa" && Role == "svr" && EvalRole == "1");
                trCompetencyAssessmentVerificationDate2.Visible = !(FormType == "caa" && Role != "svr"); //&& !AppSession.Parameter.IsCompetencyAssessmentUsingSingleEvaluator;
                rfvCompetencyAssessmentVerificationDate2.Visible = (FormType == "caa" && Role == "svr" && EvalRole == "2");

                pnlCredentialingProposed.Visible = (FormType != "caa");
                rfvCredentialApplicationDate.Visible = (pnlCredentialingProposed.Visible && FormType == "apl");
                rfvSRCredentialingStatus.Visible = (FormType == "apl");

                trCredentialingDate.Visible = (FormType != "caa");
                rfvCredentialingDate.Visible = (FormType == "rec");
                trCertificateVerification.Visible = (FormType != "caa");
                rfvIsCertificateVerification.Visible = (FormType == "rec");
                rfvIsPerform.Visible = (FormType == "rec");

                rfvRecommendationLetterDate.Visible = (FormType == "ltr");
                rfvRecommendationLetterNo.Visible = (FormType == "ltr");
                rfvValidFrom.Visible = (FormType == "ltr");
                rfvValidTo.Visible = (FormType == "ltr");

                rfvClinicalAssignmentLetterDate.Visible = (FormType == "cal" || FormType == "cal2");
                rfvDecreeNo.Visible = (FormType == "cal" || FormType == "cal2");
                rfvValidFrom2.Visible = (FormType == "cal" || FormType == "cal2");
                rfvValidTo2.Visible = (FormType == "cal" || FormType == "cal2");

                switch (FormType)
                {
                    case "caa":
                        tabStrip.Tabs[2].Visible = false;
                        tabStrip.Tabs[3].Visible = false;
                        tabStrip.Tabs[4].Visible = false;
                        tabStrip.Tabs[5].Visible = false;
                        if (Role == "svr")
                        {
                            tabStrip.SelectedIndex = 1;
                            multiPage.SelectedIndex = 1;
                        }
                        break;
                    case "apl":
                        tabStrip.Tabs[2].Visible = false;
                        tabStrip.Tabs[3].Visible = false;
                        tabStrip.Tabs[4].Visible = false;
                        tabStrip.Tabs[5].Visible = false;
                        break;
                    case "rec":
                        tabStrip.Tabs[4].Visible = false;
                        tabStrip.Tabs[5].Visible = false;
                        tabStrip.SelectedIndex = 1;
                        multiPage.SelectedIndex = 1;
                        break;
                    case "ltr":
                        tabStrip.Tabs[5].Visible = false;
                        tabStrip.SelectedIndex = 4;
                        multiPage.SelectedIndex = 4;
                        break;
                    case "cal":
                    case "cal2":
                        tabStrip.SelectedIndex = 5;
                        multiPage.SelectedIndex = 5;
                        break;
                }

                if (!((FormType == "caa" && Role == "usr") || FormType == "apl"))
                {
                    lblDocumentUpload.Text = "Document View";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }

            InitializedQuestion(QuestionFormID);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = (FormType == "caa" && Role == "usr");
            ToolBarMenuAdd.Enabled = (FormType == "caa" && Role == "usr");
            ToolBarMenuMoveNext.Enabled = false;
            ToolBarMenuMovePrev.Enabled = false;

            cboPersonID.Enabled = false;

            if (!(FormType == "caa" && Role == "usr"))
            {
                txtTransactionDate.Enabled = false;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboSRProfessionGroup, cboSRProfessionGroup);
            ajax.AddAjaxSetting(cboSRProfessionGroup, cboSRClinicalWorkArea);
            ajax.AddAjaxSetting(cboSRProfessionGroup, cboSRClinicalAuthorityLevel);
            ajax.AddAjaxSetting(cboSRProfessionGroup, cboQuestionnaireID);
            ajax.AddAjaxSetting(cboSRProfessionGroup, grdSheet);
            ajax.AddAjaxSetting(cboSRProfessionGroup, txtInfo1);
            ajax.AddAjaxSetting(cboSRProfessionGroup, txtInfo2);
            ajax.AddAjaxSetting(cboSRProfessionGroup, txtInfo3);
            ajax.AddAjaxSetting(cboSRProfessionGroup, txtInfo4);

            ajax.AddAjaxSetting(cboSRClinicalWorkArea, cboSRClinicalWorkArea);
            ajax.AddAjaxSetting(cboSRClinicalWorkArea, cboSRClinicalAuthorityLevel);
            ajax.AddAjaxSetting(cboSRClinicalWorkArea, cboQuestionnaireID);
            ajax.AddAjaxSetting(cboSRClinicalWorkArea, grdSheet);
            ajax.AddAjaxSetting(cboSRClinicalWorkArea, txtInfo1);
            ajax.AddAjaxSetting(cboSRClinicalWorkArea, txtInfo2);
            ajax.AddAjaxSetting(cboSRClinicalWorkArea, txtInfo3);
            ajax.AddAjaxSetting(cboSRClinicalWorkArea, txtInfo4);

            ajax.AddAjaxSetting(cboSRClinicalAuthorityLevel, cboSRClinicalAuthorityLevel);
            ajax.AddAjaxSetting(cboSRClinicalAuthorityLevel, cboQuestionnaireID);
            ajax.AddAjaxSetting(cboSRClinicalAuthorityLevel, grdSheet);
            ajax.AddAjaxSetting(cboSRClinicalAuthorityLevel, txtInfo1);
            ajax.AddAjaxSetting(cboSRClinicalAuthorityLevel, txtInfo2);
            ajax.AddAjaxSetting(cboSRClinicalAuthorityLevel, txtInfo3);
            ajax.AddAjaxSetting(cboSRClinicalAuthorityLevel, txtInfo4);

            ajax.AddAjaxSetting(cboQuestionnaireID, cboQuestionnaireID);
            ajax.AddAjaxSetting(cboQuestionnaireID, grdSheet);
            ajax.AddAjaxSetting(cboQuestionnaireID, txtInfo1);
            ajax.AddAjaxSetting(cboQuestionnaireID, txtInfo2);
            ajax.AddAjaxSetting(cboQuestionnaireID, txtInfo3);
            ajax.AddAjaxSetting(cboQuestionnaireID, txtInfo4);

            ajax.AddAjaxSetting(txtValidFrom, txtValidFrom);
            ajax.AddAjaxSetting(txtValidFrom, txtValidTo);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new CredentialProcess());

            txtTransactionNo.Text = GetNewTransactionNo();

            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            var form = new QuestionForm();
            form.LoadByPrimaryKey(QuestionFormID);
            txtFormName.Text = form.QuestionFormName;

            if (FormType == "caa" & Role == "usr")
            {
                var personal = new VwEmployeeTableQuery();
                personal.Where(personal.PersonID == Convert.ToInt32(AppSession.UserLogin.PersonID));
                var dtb = personal.LoadDataTable();
                cboPersonID.DataSource = dtb;
                cboPersonID.DataBind();
                if (dtb.Rows.Count > 0)
                {
                    cboPersonID.SelectedValue = AppSession.UserLogin.PersonID.ToString();
                    cboPersonID.Text = dtb.Rows[0]["EmployeeNumber"].ToString() + " - " + dtb.Rows[0]["EmployeeName"].ToString();
                }

                cboPersonID_SelectedIndexChanged(cboPersonID,
                            new RadComboBoxSelectedIndexChangedEventArgs(
                                cboPersonID.Text, string.Empty,
                                cboPersonID.SelectedValue, string.Empty));

                grdSheet.DataSource = CredentialProcessSheetItems; //Requery
                grdSheet.DataBind();
            }
            else
            {
                cboSRProfessionGroup.SelectedValue = string.Empty;
                cboSRProfessionGroup.Text = string.Empty;
            }

            cboSRCredentialingStatus.SelectedValue = string.Empty;
            cboSRCredentialingStatus.Text = string.Empty;

            cboSRRecredentialReason.SelectedValue = string.Empty;
            cboSRRecredentialReason.Text = string.Empty;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new CredentialProcess();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (!IsApproved(entity, args))
                    return;

                var collValue = new CredentialProcessQuestionCollection();
                collValue.Query.Where(collValue.Query.TransactionNo == txtTransactionNo.Text);
                collValue.LoadAll();
                collValue.MarkAllAsDeleted();

                var docs = new CredentialProcessDocumentCollection();
                docs.Query.Where(docs.Query.TransactionNo == txtTransactionNo.Text);
                docs.LoadAll();
                docs.MarkAllAsDeleted();

                var licenses = new CredentialProcessLicenseCollection();
                licenses.Query.Where(licenses.Query.TransactionNo == txtTransactionNo.Text);
                licenses.LoadAll();
                licenses.MarkAllAsDeleted();

                var sheets = new CredentialProcessSheetCollection();
                sheets.Query.Where(sheets.Query.TransactionNo == txtTransactionNo.Text);
                sheets.LoadAll();
                sheets.MarkAllAsDeleted();

                var teams = new CredentialProcessTeamCollection();
                teams.Query.Where(teams.Query.TransactionNo == txtTransactionNo.Text);
                teams.LoadAll();
                teams.MarkAllAsDeleted();

                var works = new CredentialProcessWorkExperienceCollection();
                works.Query.Where(teams.Query.TransactionNo == txtTransactionNo.Text);
                works.LoadAll();
                works.MarkAllAsDeleted();

                var cpds = new CredentialProcessCpdCollection();
                cpds.Query.Where(teams.Query.TransactionNo == txtTransactionNo.Text);
                cpds.LoadAll();
                cpds.MarkAllAsDeleted();

                var recs = new CredentialProcessRecommendationResultCollection();
                recs.Query.Where(recs.Query.TransactionNo == txtTransactionNo.Text);
                recs.LoadAll();
                recs.MarkAllAsDeleted();

                var evals = new CredentialCompetencyAssessmentEvaluatorCollection();
                evals.Query.Where(recs.Query.TransactionNo == txtTransactionNo.Text);
                evals.LoadAll();
                evals.MarkAllAsDeleted();

                entity.MarkAsDeleted();

                using (var trans = new esTransactionScope())
                {
                    collValue.Save();
                    docs.Save();
                    licenses.Save();
                    sheets.Save();
                    teams.Save();
                    works.Save();
                    cpds.Save();
                    recs.Save();
                    evals.Save();

                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (cboSRCredentialingStatus.SelectedValue == "02" & string.IsNullOrEmpty(cboSRRecredentialReason.SelectedValue))
            {
                args.MessageText = "Recredential Reason required.";
                args.IsCancel = true;
                return;
            }

            var collValue = new CredentialProcessQuestionCollection();
            var entity = new CredentialProcess();
            entity.AddNew();

            SetEntityValue(entity, collValue);
            SaveEntity(entity, collValue);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            //if (FormType == "caa" && Role == "eva" && !AppSession.Parameter.IsCompetencyAssessmentUsingSingleEvaluator && CredentialCompetencyAssessmentEvaluators.Count != 2)
            //{
            //    args.MessageText = "Competency Assesment Evaluator incomplete.";
            //    args.IsCancel = true;
            //    return;
            //}

            if (FormType == "caa" && Role == "eva")
            {
                if (CredentialCompetencyAssessmentEvaluators.Count == 0)
                {
                    args.MessageText = "Competency Assesment Evaluator required.";
                    args.IsCancel = true;
                    return;
                }
                if (CredentialCompetencyAssessmentEvaluators.Count > 2)
                {
                    args.MessageText = "Competency Assessment Evaluators should not be more than 2.";
                    args.IsCancel = true;
                    return;
                }
            }

            if (cboSRCredentialingStatus.SelectedValue == "02" & string.IsNullOrEmpty(cboSRRecredentialReason.SelectedValue))
            {
                args.MessageText = "Recredential Reason required.";
                args.IsCancel = true;
                return;
            }

            if (FormType == "rec" && CredentialProcessTeams.Count == 0)
            {
                args.MessageText = "Credentialing Teams required.";
                args.IsCancel = true;
                return;
            }

            var entity = new CredentialProcess();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                var collValue = new CredentialProcessQuestionCollection();
                collValue.Query.Where(collValue.Query.TransactionNo == txtTransactionNo.Text);
                collValue.LoadAll();

                SetEntityValue(entity, collValue);
                SaveEntity(entity, collValue);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "CredentialProcess";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new CredentialProcess();
            entity.LoadByPrimaryKey(txtTransactionNo.Text);

            if (FormType == "caa" && Role == "svr")
            {
                if (entity.IsApproved == null || entity.IsApproved == false)
                {
                    args.MessageText = "Competency Assessment Application Approved Status required.";
                    args.IsCancel = true;
                    return;
                }
            }

            if (FormType == "apl")
            {
                var evals = new CredentialCompetencyAssessmentEvaluatorCollection();
                evals.Query.Where(evals.Query.TransactionNo == txtTransactionNo.Text);
                evals.Query.OrderBy(evals.Query.SRCompetencyAssessmentEvalRole.Ascending);
                evals.LoadAll();
                var msg = string.Empty;
                foreach (var e in evals)
                {
                    if (!(e.IsEvaluated.HasValue && e.IsEvaluated == true))
                    {
                        if (msg == string.Empty)
                            msg = "Eval #" + e.SRCompetencyAssessmentEvalRole;
                        else msg += ", #" + e.SRCompetencyAssessmentEvalRole;
                    }
                }
                if (msg != string.Empty)
                {
                    args.MessageText = string.Format("Competency Assessment Process Approved Status from {0} required.", msg);
                    args.IsCancel = true;
                    return;
                }
            }

            if (FormType == "rec")
            {
                if (entity.IsCredentialApplication == null || entity.IsCredentialApplication == false)
                {
                    args.MessageText = "Credential / Re-Credential Application Approved Status required.";
                    args.IsCancel = true;
                    return;
                }
            }

            if (FormType == "ltr")
            {
                if (entity.IsCredentialing == null || entity.IsCredentialing == false)
                {
                    args.MessageText = "Credential / Re-Credential Process Approved Status required.";
                    args.IsCancel = true;
                    return;
                }
            }

            if (FormType == "cal" || FormType == "cal2")
            {
                if (entity.IsRecommendationLetter == null || entity.IsRecommendationLetter == false)
                {
                    args.MessageText = "Recommendation Letter Approved Status required.";
                    args.IsCancel = true;
                    return;
                }

                if (string.IsNullOrEmpty(txtDecreeNo.Text))
                {
                    args.MessageText = "Decree No required.";
                    args.IsCancel = true;
                    return;
                }
            }

            using (var trans = new esTransactionScope())
            {
                if (FormType == "caa")
                {
                    if (Role == "usr")
                    {
                        entity.IsApproved = true;
                        entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                        entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                    }
                    else
                    {
                        bool isComplete = true;
                        if (EvalRole == "1")
                        {
                            entity.IsVerified = true;
                            entity.VerifiedDateTime = (new DateTime()).NowAtSqlServer();
                            entity.VerifiedByUserID = AppSession.UserLogin.UserID;
                        }
                        else
                        {
                            entity.IsVerified2 = true;
                            entity.VerifiedDateTime2 = (new DateTime()).NowAtSqlServer();
                            entity.VerifiedByUserID2 = AppSession.UserLogin.UserID;
                        }
                        var evals = new CredentialCompetencyAssessmentEvaluatorCollection();
                        evals.Query.Where(evals.Query.TransactionNo == entity.TransactionNo);
                        evals.LoadAll();
                        if (evals.Count == 1)
                        {
                            foreach (var e in evals)
                            {
                                e.IsEvaluated = true;
                            }
                        }
                        else
                        {
                            foreach (var e in evals)
                            {
                                if (e.EvaluatorID == AppSession.UserLogin.PersonID.ToInt())
                                    e.IsEvaluated = true;
                                else
                                    isComplete = e.IsEvaluated ?? false;
                            }
                        }
                        evals.Save();

                        entity.IsCompletelyVerified = isComplete;
                    }
                }
                else if (FormType == "apl")
                {
                    entity.IsCredentialApplication = true;
                    entity.LastCredentialApplicationDateTime = (new DateTime()).NowAtSqlServer();
                    entity.LastCredentialApplicationByUserID = AppSession.UserLogin.UserID;
                }
                else if (FormType == "rec")
                {
                    entity.IsCredentialing = true;
                    entity.LastCredentialingDateTime = (new DateTime()).NowAtSqlServer();
                    entity.LastCredentialingByUserID = AppSession.UserLogin.UserID;
                }
                else if (FormType == "ltr")
                {
                    entity.IsRecommendationLetter = true;
                    entity.LastRecommendationLetterDateTime= (new DateTime()).NowAtSqlServer();
                    entity.LastRecommendationLetterByUserID = AppSession.UserLogin.UserID;
                }
                else if (FormType == "cal" || FormType == "cal2")
                {
                    entity.IsClinicalAssignmentLetter = true;
                    entity.LastClinicalAssignmentLetterDateTime = (new DateTime()).NowAtSqlServer();
                    entity.LastCredentialingByUserID = AppSession.UserLogin.UserID;

                    var ecp = new EmployeeClinicalPrivilege();
                    var ecpQ = new EmployeeClinicalPrivilegeQuery();
                    ecpQ.Where(ecpQ.PersonID == entity.PersonID, ecpQ.TransactionNo == entity.TransactionNo);
                    if (ecpQ.LoadDataTable().Rows.Count > 0)
                        ecp.Load(ecpQ);
                    else
                        ecp.AddNew();
                    ecp.PersonID = entity.PersonID;
                    ecp.SRProfessionGroup = entity.SRProfessionGroup;
                    ecp.SRClinicalWorkArea = entity.SRClinicalWorkArea;
                    ecp.SRClinicalAuthorityLevel = entity.SRClinicalAuthorityLevel;

                    ecp.ValidFrom = entity.ValidFrom;
                    ecp.ValidTo = entity.ValidTo;
                    ecp.DecreeNo = entity.DecreeNo;
                    ecp.TransactionNo = entity.TransactionNo;
                    ecp.Notes = string.Empty;
                    ecp.LastUpdateDateTime = DateTime.Now;
                    ecp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    ecp.Save();

                    var license = new PersonalLicence();
                    var licenseQ = new PersonalLicenceQuery();
                    licenseQ.Where(licenseQ.SRLicenceType == AppSession.Parameter.PersonalLicenseTypeSPK.ToString(), licenseQ.Note == entity.DecreeNo);
                    licenseQ.es.Top = 1;
                    if (licenseQ.LoadDataTable().Rows.Count > 0)
                        license.Load(licenseQ);
                    else
                        license.AddNew();
                    license.PersonID = entity.PersonID;
                    license.SRLicenceType = AppSession.Parameter.PersonalLicenseTypeSPK;
                    license.ValidFrom = entity.ValidFrom;
                    license.ValidTo = entity.ValidTo;
                    license.Note = entity.DecreeNo;
                    license.LastUpdateDateTime = DateTime.Now;
                    license.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    license.Save();
                }

                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new CredentialProcess();
            entity.LoadByPrimaryKey(txtTransactionNo.Text);

            if (FormType == "caa")
            {
                if (Role == "usr")
                {
                    if (CredentialCompetencyAssessmentEvaluators.Count > 0)
                    {
                        args.MessageText = "This data has entered the next process (Competency Assessment Evaluator).";
                        args.IsCancel = true;
                        return;
                    }
                    if ((entity.IsVerified.HasValue && entity.IsVerified == true) || (entity.IsVerified2.HasValue && entity.IsVerified2 == true))
                    {
                        args.MessageText = "This data has entered the next process (Competency Assessment Process).";
                        args.IsCancel = true;
                        return;
                    }
                }
                else
                {
                    if (entity.IsCredentialApplication.HasValue && entity.IsCredentialApplication == true)
                    {
                        args.MessageText = "This data has entered the next process (Credential / Re-Credential Application).";
                        args.IsCancel = true;
                        return;
                    }
                }
            }
            else if (FormType == "apl")
            {
                if (entity.IsCredentialing.HasValue && entity.IsCredentialing == true)
                {
                    args.MessageText = "This data has entered the next process (Credential / Re-Credential Process).";
                    args.IsCancel = true;
                    return;
                }
            }
            else if (FormType == "rec")
            {
                if (entity.IsRecommendationLetter.HasValue && entity.IsRecommendationLetter == true)
                {
                    args.MessageText = "This data has entered the next process (Recommendation Letter).";
                    args.IsCancel = true;
                    return;
                }
            }
            else if (FormType == "ltr")
            {
                if (entity.IsClinicalAssignmentLetter.HasValue && entity.IsClinicalAssignmentLetter == true)
                {
                    args.MessageText = "This data has entered the next process (Clinical Assignment Letter).";
                    args.IsCancel = true;
                    return;
                }
            }

            using (var trans = new esTransactionScope())
            {
                if (FormType == "caa")
                {
                    if (Role == "usr")
                    {
                        entity.IsApproved = false;
                        entity.ApprovedDateTime = null;
                        entity.ApprovedByUserID = null;
                    }
                    else
                    {
                        if (EvalRole == "1")
                        {
                            entity.IsVerified = false;
                            entity.VerifiedDateTime = null;
                            entity.VerifiedByUserID = null;
                        }
                        else
                        {
                            entity.IsVerified2 = false;
                            entity.VerifiedDateTime2 = null;
                            entity.VerifiedByUserID2 = null;
                        }
                        entity.IsCompletelyVerified = false;
                        var eval = new CredentialCompetencyAssessmentEvaluator();
                        if (eval.LoadByPrimaryKey(entity.TransactionNo, AppSession.UserLogin.PersonID.ToInt()))
                        {
                            eval.IsEvaluated = false;
                            eval.Save();
                        }
                    }
                }
                else if (FormType == "apl")
                {
                    entity.IsCredentialApplication = false;
                    entity.LastCredentialApplicationDateTime = null;
                    entity.LastCredentialApplicationByUserID = null;
                }
                else if (FormType == "rec")
                {
                    entity.IsCredentialing = false;
                    entity.LastCredentialingDateTime = null;
                    entity.LastCredentialingByUserID = null;
                }
                else if (FormType == "ltr")
                {
                    entity.IsRecommendationLetter = false;
                    entity.LastRecommendationLetterDateTime = null;
                    entity.LastRecommendationLetterByUserID = null;
                }

                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new CredentialProcess();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            if (!IsApproved(entity, args))
                return;

            SetVoid(entity, true);
        }

        private void SetVoid(CredentialProcess entity, bool isVoid)
        {
            using (var trans = new esTransactionScope())
            {
                //header
                entity.IsVoid = isVoid;
                if (isVoid)
                {
                    entity.VoidByUserID = AppSession.UserLogin.UserID;
                    entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
                }
                else
                {
                    entity.VoidByUserID = null;
                    entity.VoidDateTime = null;
                }
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.Save();

                trans.Complete();
            }
        }

        #endregion

        #region ToolBar Menu Support

        private bool IsApprovedOrVoid(CredentialProcess entity, ValidateArgs args)
        {
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }
            if (FormType == "caa")
            {
                if (Role == "usr")
                {
                    if (entity.IsApproved ?? false)
                    {
                        args.MessageText = AppConstant.Message.RecordHasApproved;
                        args.IsCancel = true;
                        return false;
                    }
                }
                else if (Role == "eva")
                {
                    if (entity.IsVerified.HasValue || entity.IsVerified2.HasValue)
                    {

                        args.MessageText = "This data has entered the next process (Competency Assessment Process).";
                        args.IsCancel = true;
                        return false;
                    }
                }
                else 
                {
                    if (EvalRole == "1")
                    {
                        if (entity.IsVerified ?? false)
                        {
                            args.MessageText = AppConstant.Message.RecordHasApproved;
                            args.IsCancel = true;
                            return false;
                        }
                    }
                    else
                    {
                        if (entity.IsVerified2 ?? false)
                        {
                            args.MessageText = AppConstant.Message.RecordHasApproved;
                            args.IsCancel = true;
                            return false;
                        }
                    }
                }
            }
            else if (FormType == "apl")
            {
                if (entity.IsCredentialApplication ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }
            else if (FormType == "rec")
            {
                if (entity.IsCredentialing ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }
            else if (FormType == "ltr")
            {
                if (entity.IsRecommendationLetter ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }

            return true;
        }

        private bool IsApproved(CredentialProcess entity, ValidateArgs args)
        {
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !chkIsVoid.Checked;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemWorkExperience(oldVal, newVal);
            RefreshCommandItemCpd(oldVal, newVal);
            RefreshCommandItemLicense(oldVal, newVal);
            RefreshCommandItemTeam(oldVal, newVal);
            RefreshCommandItemDocument(newVal);
            RefreshCommandItemRecommendationResult(newVal);
            RefreshCommandItemEval(oldVal, newVal);

            var dtbQuestion = QuestionDataTable(QuestionFormID);

            foreach (DataRow rowQuestion in dtbQuestion.Rows)
            {
                // Tips: Don't use entity.es.IsModified, krn belum tentu record sudah diedit waktu save
                if (FormType == "apl")
                    SetReadOnlyCredentialProcessQuestion((newVal == AppEnum.DataMode.Read), rowQuestion, rowQuestion["QuestionGroupID"].ToString());
                else
                    SetReadOnlyCredentialProcessQuestion(true, rowQuestion, rowQuestion["QuestionGroupID"].ToString());
            }

            bool isVisible = (newVal != AppEnum.DataMode.Read);
            trDocumentUpload.Visible = !isVisible && !(FormType == "caa" && Role == "eva");
            trClinicalAssigmnetLetterUpload.Visible= (!isVisible && FormType == "cal");

            cboPersonID.Enabled = false;

            if (FormType == "caa")
            {
                if (Role == "usr")
                {
                    grdSheet.Columns.FindByUniqueName("cboSRCurrentAbility").Visible = isVisible;
                    grdSheet.Columns.FindByUniqueName("txtSelfAssessmentNotes").Visible = isVisible;
                    grdSheet.Columns.FindByUniqueName("CurrentAbility").Visible = !isVisible;
                    grdSheet.Columns.FindByUniqueName("SelfAssessmentNotes").Visible = !isVisible;

                    grdSheet.Columns.FindByUniqueName("cboSRCurrentAbilitySupervisor").Visible = false;
                    grdSheet.Columns.FindByUniqueName("cboSRCurrentAbilitySupervisor2").Visible = false;
                    grdSheet.Columns.FindByUniqueName("CurrentAbilitySupervisor").Visible = false;
                    grdSheet.Columns.FindByUniqueName("CurrentAbilitySupervisor2").Visible = false;

                    txtCompetencyAssessmentVerificationDate.Enabled = false;
                    txtCompetencyAssessmentVerificationDate2.Enabled = false;
                }
                else if (Role == "eva")
                {
                    cboSRProfessionGroup.Enabled = false;
                    cboSRClinicalWorkArea.Enabled = false;
                    cboSRClinicalAuthorityLevel.Enabled = false;
                    cboQuestionnaireID.Enabled = false;

                    cboSREducationLevel.Enabled = false;
                    txtInstitutionName.ReadOnly = true;
                    txtDiplomaNumber.ReadOnly = true;
                    txtDiplomaDate.Enabled = false;
                    txtCompetencyCertificateNo.ReadOnly = true;
                    txtCompetencyCertificateDateOfIssue.Enabled = true;

                    grdSheet.Columns.FindByUniqueName("cboSRCurrentAbility").Visible = false;
                    grdSheet.Columns.FindByUniqueName("txtSelfAssessmentNotes").Visible = false;
                    grdSheet.Columns.FindByUniqueName("CurrentAbility").Visible = true;
                    grdSheet.Columns.FindByUniqueName("SelfAssessmentNotes").Visible = true;

                    grdSheet.Columns.FindByUniqueName("cboSRCurrentAbilitySupervisor").Visible = false;
                    grdSheet.Columns.FindByUniqueName("cboSRCurrentAbilitySupervisor2").Visible = false;
                    grdSheet.Columns.FindByUniqueName("CurrentAbilitySupervisor").Visible = false;
                    grdSheet.Columns.FindByUniqueName("CurrentAbilitySupervisor2").Visible = false;

                    txtCompetencyAssessmentVerificationDate.Enabled = false;
                    txtCompetencyAssessmentVerificationDate2.Enabled = false;

                }
                else if (Role == "svr")
                {
                    cboSRProfessionGroup.Enabled = false;
                    cboSRClinicalWorkArea.Enabled = false;
                    cboSRClinicalAuthorityLevel.Enabled = false;
                    cboQuestionnaireID.Enabled = false;

                    cboSREducationLevel.Enabled = false;
                    txtInstitutionName.ReadOnly = true;
                    txtDiplomaNumber.ReadOnly = true;
                    txtDiplomaDate.Enabled = false;
                    txtCompetencyCertificateNo.ReadOnly = true;
                    txtCompetencyCertificateDateOfIssue.Enabled = true;

                    grdSheet.Columns.FindByUniqueName("cboSRCurrentAbility").Visible = false;
                    grdSheet.Columns.FindByUniqueName("txtSelfAssessmentNotes").Visible = false;
                    grdSheet.Columns.FindByUniqueName("CurrentAbility").Visible = true;
                    grdSheet.Columns.FindByUniqueName("SelfAssessmentNotes").Visible = true;

                    if (EvalRole == "1")
                    {
                        txtCompetencyAssessmentVerificationDate2.Enabled = false;
                        grdSheet.Columns.FindByUniqueName("cboSRCurrentAbilitySupervisor").Visible = isVisible;
                        grdSheet.Columns.FindByUniqueName("CurrentAbilitySupervisor").Visible = !isVisible;
                        grdSheet.Columns.FindByUniqueName("cboSRCurrentAbilitySupervisor2").Visible = false;
                        grdSheet.Columns.FindByUniqueName("CurrentAbilitySupervisor2").Visible = false;
                    }
                    else
                    {
                        txtCompetencyAssessmentVerificationDate.Enabled = false;
                        grdSheet.Columns.FindByUniqueName("cboSRCurrentAbilitySupervisor").Visible = false;
                        grdSheet.Columns.FindByUniqueName("CurrentAbilitySupervisor").Visible = false;
                        grdSheet.Columns.FindByUniqueName("cboSRCurrentAbilitySupervisor2").Visible = isVisible;
                        grdSheet.Columns.FindByUniqueName("CurrentAbilitySupervisor2").Visible = !isVisible;
                    }
                }

                grdSheet.Columns.FindByUniqueName("cboSRReview").Visible = false;
                grdSheet.Columns.FindByUniqueName("cboSRRecomendation").Visible = false;
                grdSheet.Columns.FindByUniqueName("cboSRConclusion").Visible = false;
                grdSheet.Columns.FindByUniqueName("txtNotes").Visible = false;

                grdSheet.Columns.FindByUniqueName("CurrentAbilityX").Visible = false;
                grdSheet.Columns.FindByUniqueName("SelfAssessmentNotesX").Visible = false;
                grdSheet.Columns.FindByUniqueName("CurrentAbilitySupervisorX").Visible = false;
                grdSheet.Columns.FindByUniqueName("CurrentAbilitySupervisor2X").Visible = false;
                grdSheet.Columns.FindByUniqueName("Review").Visible = false;
                grdSheet.Columns.FindByUniqueName("Recomendation").Visible = false;
                grdSheet.Columns.FindByUniqueName("Conclusion").Visible = false;
                grdSheet.Columns.FindByUniqueName("Notes").Visible = false;

                cboSRCredentialingStatus.Enabled = false;
                cboSRRecredentialReason.Enabled = false;

                rbtIsCertificateVerification.Enabled = false;
                rbtIsPerform.Enabled = false;
                txtRecommendationNotes.Enabled = false;
            }
            else
            {
                cboSRProfessionGroup.Enabled = false;
                cboSRClinicalWorkArea.Enabled = false;
                cboSRClinicalAuthorityLevel.Enabled = false;
                cboQuestionnaireID.Enabled = false;
                txtCompetencyAssessmentVerificationDate.Enabled = false;

                cboSREducationLevel.Enabled = false;
                txtInstitutionName.ReadOnly = true;
                txtDiplomaNumber.ReadOnly = true;
                txtDiplomaDate.Enabled = false;
                txtCompetencyCertificateNo.ReadOnly = true;
                txtCompetencyCertificateDateOfIssue.Enabled = true;

                txtCompetencyAssessmentVerificationDate.Enabled = false;
                txtCompetencyAssessmentVerificationDate2.Enabled = false;

                if (FormType == "apl")
                {
                    txtCredentialingDate.Enabled = false;
                    rbtIsCertificateVerification.Enabled = false;
                    rbtIsPerform.Enabled = false;
                    txtRecommendationNotes.Enabled = false;

                    grdSheet.Columns.FindByUniqueName("cboSRCurrentAbility").Visible = false;
                    grdSheet.Columns.FindByUniqueName("txtSelfAssessmentNotes").Visible = false;
                    grdSheet.Columns.FindByUniqueName("cboSRCurrentAbilitySupervisor").Visible = false;
                    grdSheet.Columns.FindByUniqueName("cboSRCurrentAbilitySupervisor2").Visible = false;
                    grdSheet.Columns.FindByUniqueName("cboSRReview").Visible = false;
                    grdSheet.Columns.FindByUniqueName("cboSRRecomendation").Visible = false;
                    grdSheet.Columns.FindByUniqueName("cboSRConclusion").Visible = false;
                    grdSheet.Columns.FindByUniqueName("txtNotes").Visible = false;

                    grdSheet.Columns.FindByUniqueName("CurrentAbility").Visible = false;
                    grdSheet.Columns.FindByUniqueName("SelfAssessmentNotes").Visible = false;
                    grdSheet.Columns.FindByUniqueName("CurrentAbilitySupervisor").Visible = false;
                    grdSheet.Columns.FindByUniqueName("CurrentAbilitySupervisor2").Visible = false;
                    grdSheet.Columns.FindByUniqueName("CurrentAbilityX").Visible = true;
                    grdSheet.Columns.FindByUniqueName("SelfAssessmentNotesX").Visible = true;
                    grdSheet.Columns.FindByUniqueName("CurrentAbilitySupervisorX").Visible = true;
                    grdSheet.Columns.FindByUniqueName("CurrentAbilitySupervisor2X").Visible = true;//!AppSession.Parameter.IsCompetencyAssessmentUsingSingleEvaluator;
                    grdSheet.Columns.FindByUniqueName("Review").Visible = false;
                    grdSheet.Columns.FindByUniqueName("Recomendation").Visible = false;
                    grdSheet.Columns.FindByUniqueName("Conclusion").Visible = false;
                    grdSheet.Columns.FindByUniqueName("Notes").Visible = false;
                }
                else
                {
                    txtCredentialApplicationDate.Enabled = false;
                    cboSRCredentialingStatus.Enabled = false;
                    cboSRRecredentialReason.Enabled = false;

                    if (FormType != "rec")
                    {
                        rbtIsPerform.Enabled = false;
                        txtRecommendationNotes.Enabled = false;

                        if (FormType == "cal" || FormType == "cal2")
                        {
                            txtRecommendationLetterDate.Enabled = false;
                            txtRecommendationLetterNo.Enabled = false;
                            txtRecommendationNotes.Enabled = false;
                        }
                    }

                    grdSheet.Columns.FindByUniqueName("cboSRCurrentAbility").Visible = false;
                    grdSheet.Columns.FindByUniqueName("txtSelfAssessmentNotes").Visible = false;
                    grdSheet.Columns.FindByUniqueName("cboSRCurrentAbilitySupervisor").Visible = false;
                    grdSheet.Columns.FindByUniqueName("cboSRCurrentAbilitySupervisor2").Visible = false;
                    grdSheet.Columns.FindByUniqueName("cboSRReview").Visible = isVisible;
                    grdSheet.Columns.FindByUniqueName("cboSRRecomendation").Visible = isVisible;
                    grdSheet.Columns.FindByUniqueName("cboSRConclusion").Visible = isVisible;
                    grdSheet.Columns.FindByUniqueName("txtNotes").Visible = false; //isVisible;

                    grdSheet.Columns.FindByUniqueName("CurrentAbility").Visible = false;
                    grdSheet.Columns.FindByUniqueName("SelfAssessmentNotes").Visible = false;
                    grdSheet.Columns.FindByUniqueName("CurrentAbilitySupervisor").Visible = false;
                    grdSheet.Columns.FindByUniqueName("CurrentAbilitySupervisor2").Visible = false;

                    grdSheet.Columns.FindByUniqueName("CurrentAbilityX").Visible = true;
                    grdSheet.Columns.FindByUniqueName("SelfAssessmentNotesX").Visible = true;
                    grdSheet.Columns.FindByUniqueName("CurrentAbilitySupervisorX").Visible = true;
                    grdSheet.Columns.FindByUniqueName("CurrentAbilitySupervisor2X").Visible = true; //!AppSession.Parameter.IsCompetencyAssessmentUsingSingleEvaluator;
                    grdSheet.Columns.FindByUniqueName("Review").Visible = !isVisible;
                    grdSheet.Columns.FindByUniqueName("Recomendation").Visible = !isVisible;
                    grdSheet.Columns.FindByUniqueName("Conclusion").Visible = !isVisible;
                    grdSheet.Columns.FindByUniqueName("Notes").Visible = false; //!isVisible;
                }
            }

            RefreshGridSheetItems();
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new CredentialProcess();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuEditClick()
        {
            var entity = new CredentialProcess();
            entity.LoadByPrimaryKey(txtTransactionNo.Text);

            OnPopulateEntryControl(entity);

            var form = new QuestionForm();
            form.LoadByPrimaryKey(QuestionFormID);
            txtFormName.Text = form.QuestionFormName;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new CredentialProcess();

            entity.LoadByPrimaryKey(string.IsNullOrEmpty(txtTransactionNo.Text) ? Request.QueryString["id"] : txtTransactionNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var cp = (CredentialProcess)entity;

            txtTransactionNo.Text = cp.TransactionNo;
            if (cp.TransactionDate.HasValue)
                txtTransactionDate.SelectedDate = cp.TransactionDate;

            if (!string.IsNullOrEmpty(cp.QuestionFormID))
            {
                var form = new QuestionForm();
                form.LoadByPrimaryKey(cp.QuestionFormID);
                txtFormName.Text = form.QuestionFormName;
            }

            if (cp.PersonID.HasValue && cp.PersonID.ToInt() > -1)
            {
                var query = new VwEmployeeTableQuery();
                query.Select
                    (
                        query.PersonID,
                        query.EmployeeNumber,
                        query.EmployeeName
                    );

                query.Where(query.PersonID == cp.PersonID.ToInt());

                cboPersonID.DataSource = query.LoadDataTable();
                cboPersonID.DataBind();

                cboPersonID.SelectedValue = cp.PersonID.ToString();

                var employeeInfo = new VwEmployeeTable();
                var employeeInfoQ = new VwEmployeeTableQuery();
                employeeInfoQ.es.Top = 1;
                employeeInfoQ.Where(employeeInfoQ.PersonID == cp.PersonID.ToInt());
                employeeInfo.Load(employeeInfoQ);
                if (employeeInfo != null)
                {
                    txtEmployeeNo.Text = employeeInfo.EmployeeNumber;
                    txtPlaceDOB.Text = string.Format("{0}, {1}", employeeInfo.PlaceBirth, Convert.ToDateTime(employeeInfo.BirthDate).ToString("dd-MMM-yyyy"));

                    if (!string.IsNullOrEmpty(cp.SREmploymentType))
                        cboSREmploymentType.SelectedValue = cp.SREmploymentType;
                    else
                        cboSREmploymentType.SelectedValue = employeeInfo.SREmploymentType;
                    
                    if (cboSREmploymentType.SelectedValue == AppSession.Parameter.EmploymentTypePermanent)
                        txtREmploymentPermanentDate.SelectedDate = employeeInfo.TglDiangkat;
                    else
                        txtREmploymentPermanentDate.Clear();
                }
                else
                {
                    txtEmployeeNo.Text = string.Empty;
                    txtPlaceDOB.Text = string.Empty;
                    cboSREmploymentType.SelectedValue = string.Empty;
                    cboSREmploymentType.Text = string.Empty;
                    txtREmploymentPermanentDate.Clear();
                }
            }
            else
            {
                cboPersonID.Items.Clear();
                cboPersonID.SelectedValue = string.Empty;
                cboPersonID.Text = string.Empty;

                txtEmployeeNo.Text = string.Empty;
                txtPlaceDOB.Text = string.Empty;

                cboSREmploymentType.SelectedValue = string.Empty;
                cboSREmploymentType.Text = string.Empty;
                txtREmploymentPermanentDate.Clear();
            }

            if (!string.IsNullOrEmpty(cp.ServiceUnitID))
            {
                var org = new OrganizationUnitQuery();
                org.Where(org.OrganizationUnitID == cp.ServiceUnitID.ToInt());
                var orgDtb = org.LoadDataTable();
                if (orgDtb.Rows.Count > 0)
                {
                    cboServiceUnitID.DataSource = orgDtb;
                    cboServiceUnitID.DataBind();
                    cboServiceUnitID.SelectedValue = cp.ServiceUnitID;
                }
                else
                {
                    cboServiceUnitID.Items.Clear();
                    cboServiceUnitID.SelectedValue = string.Empty;
                    cboServiceUnitID.Text = string.Empty;
                }
            }
            else
            {
                cboServiceUnitID.Items.Clear();
                cboServiceUnitID.SelectedValue = string.Empty;
                cboServiceUnitID.Text = string.Empty;
            }

            if (cp.PositionID.HasValue)
            {
                var pos = new PositionQuery();
                pos.Where(pos.PositionID == cp.PositionID.ToInt());
                var posDtb = pos.LoadDataTable();
                if (posDtb.Rows.Count > 0)
                {
                    cboPositionID.DataSource = posDtb;
                    cboPositionID.DataBind();
                    cboPositionID.SelectedValue = cp.PositionID.ToString();
                }
                else
                {
                    cboPositionID.Items.Clear();
                    cboPositionID.SelectedValue = string.Empty;
                    cboPositionID.Text = string.Empty;
                }
            }
            else
            {
                cboPositionID.Items.Clear();
                cboPositionID.SelectedValue = string.Empty;
                cboPositionID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(cp.SRProfessionGroup))
                cboSRProfessionGroup.SelectedValue = cp.SRProfessionGroup;
            else
            {
                cboSRProfessionGroup.SelectedValue = string.Empty;
                cboSRProfessionGroup.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(cp.SRClinicalWorkArea))
            {
                var query = new AppStandardReferenceItemQuery("a");
                query.Where
                    (
                        query.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea,
                        query.ItemID == cp.SRClinicalWorkArea
                    );
                cboSRClinicalWorkArea.DataSource = query.LoadDataTable();
                cboSRClinicalWorkArea.DataBind();
                cboSRClinicalWorkArea.SelectedValue = cp.SRClinicalWorkArea;
            }
            else
            {
                cboSRClinicalWorkArea.Items.Clear();
                cboSRClinicalWorkArea.SelectedValue = string.Empty;
                cboSRClinicalWorkArea.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(cp.SRClinicalAuthorityLevel))
            {
                var query = new AppStandardReferenceItemQuery("a");
                query.Where
                    (
                        query.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel,
                        query.ItemID == cp.SRClinicalAuthorityLevel
                    );
                cboSRClinicalAuthorityLevel.DataSource = query.LoadDataTable();
                cboSRClinicalAuthorityLevel.DataBind();
                cboSRClinicalAuthorityLevel.SelectedValue = cp.SRClinicalAuthorityLevel;
            }
            else
            {
                cboSRClinicalAuthorityLevel.Items.Clear();
                cboSRClinicalAuthorityLevel.SelectedValue = string.Empty;
                cboSRClinicalAuthorityLevel.Text = string.Empty;
            }

            cboSREducationLevel.SelectedValue = cp.SREducationLevel;
            txtInstitutionName.Text = cp.InstitutionName;
            txtDiplomaNumber.Text = cp.DiplomaNumber;

            if (cp.DiplomaDate.HasValue)
                txtDiplomaDate.SelectedDate = cp.DiplomaDate;
            else 
                txtDiplomaDate.Clear();

            if (cp.QuestionnaireID.HasValue)
            {
                var cqq = new CredentialQuestionnaireQuery();
                cqq.Where(cqq.QuestionnaireID == cp.QuestionnaireID);
                cboQuestionnaireID.DataSource = cqq.LoadDataTable();
                cboQuestionnaireID.DataBind();
                cboQuestionnaireID.SelectedValue = cp.QuestionnaireID.ToString();
                var cq = new CredentialQuestionnaire();
                cq.Load(cqq);
                txtInfo1.Text = cq.Info1;
                txtInfo2.Text = cq.Info2;
                txtInfo3.Text = cq.Info3;
                txtInfo4.Text = cq.Info4;
            }
            else
            {
                cboQuestionnaireID.Items.Clear();
                cboQuestionnaireID.SelectedValue = string.Empty;
                cboQuestionnaireID.Text = string.Empty;
                txtInfo1.Text = string.Empty;
                txtInfo2.Text = string.Empty;
                txtInfo3.Text = string.Empty;
                txtInfo4.Text = string.Empty;
            }

            if (cp.CompetencyAssessmentVerificationDate.HasValue)
                txtCompetencyAssessmentVerificationDate.SelectedDate = cp.CompetencyAssessmentVerificationDate;
            else
            {
                if (FormType == "caa" && Role == "svr" && EvalRole == "1")
                    txtCompetencyAssessmentVerificationDate.SelectedDate = DateTime.Now;
            }

            if (cp.CompetencyAssessmentVerificationDate2.HasValue)
                txtCompetencyAssessmentVerificationDate2.SelectedDate = cp.CompetencyAssessmentVerificationDate2;
            else
            {
                if (FormType == "caa" && Role == "svr" && EvalRole == "2")
                    txtCompetencyAssessmentVerificationDate2.SelectedDate = DateTime.Now;
            }

            if (cp.CredentialApplicationDate.HasValue)
                txtCredentialApplicationDate.SelectedDate = cp.CredentialApplicationDate;
            else
            {
                if (FormType == "apl")
                    txtCredentialApplicationDate.SelectedDate = DateTime.Now;
            }

            if (!string.IsNullOrEmpty(cp.SRCredentialingStatus))
                cboSRCredentialingStatus.SelectedValue = cp.SRCredentialingStatus;
            else
            {
                cboSRCredentialingStatus.SelectedValue = string.Empty;
                cboSRCredentialingStatus.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(cp.SRRecredentialReason))
                cboSRRecredentialReason.SelectedValue = cp.SRRecredentialReason;
            else
            {
                cboSRRecredentialReason.SelectedValue = string.Empty;
                cboSRRecredentialReason.Text = string.Empty;
            }

            if (cp.CredentialingDate.HasValue)
                txtCredentialingDate.SelectedDate = cp.CredentialingDate;
            else
            {
                if (FormType == "rec")
                    txtCredentialingDate.SelectedDate = DateTime.Now;
            }

            if (cp.IsCertificateVerification != null)
                rbtIsCertificateVerification.SelectedValue = (cp.IsCertificateVerification ?? false) == true ? "1" : "0";

            if (cp.IsPerform != null)
                rbtIsPerform.SelectedValue = (cp.IsPerform ?? false) == true ? "1" : "0";

            txtRecommendationNotes.Text = cp.RecommendationNotes;

            if (cp.RecommendationLetterDate.HasValue)
                txtRecommendationLetterDate.SelectedDate = cp.RecommendationLetterDate;
            else
            {
                if (FormType == "ltr")
                    txtRecommendationLetterDate.SelectedDate = DateTime.Now;
                else
                    txtRecommendationLetterDate.Clear();
            }

            txtRecommendationLetterNo.Text = cp.RecommendationLetterNo;

            if (cp.ClinicalAssignmentLetterDate.HasValue)
                txtClinicalAssignmentLetterDate.SelectedDate = cp.ClinicalAssignmentLetterDate;
            else
            {
                if (FormType == "cal" || FormType == "cal2")
                    txtClinicalAssignmentLetterDate.SelectedDate = DateTime.Now;
                else
                    txtClinicalAssignmentLetterDate.Clear();
            }

            txtDecreeNo.Text = cp.DecreeNo;

            if (cp.ValidFrom.HasValue)
            {
                txtValidFrom.SelectedDate = cp.ValidFrom;
                txtValidFrom2.SelectedDate = cp.ValidFrom;
            }
            else
            {
                txtValidFrom.Clear();
                txtValidFrom2.Clear();
            }

            if (cp.ValidTo.HasValue)
            {
                txtValidTo.SelectedDate = cp.ValidTo;
                txtValidTo2.SelectedDate = cp.ValidTo;
            }
            else
            {
                txtValidTo.Clear();
                txtValidTo2.Clear();
            }

            if (FormType == "caa")
            {
                if (Role == "usr")
                    chkIsApproved.Checked = cp.IsApproved ?? false;
                else if (Role == "svr")
                {
                    if (EvalRole == "1")
                        chkIsApproved.Checked = cp.IsVerified ?? false;
                    else
                        chkIsApproved.Checked = cp.IsVerified2 ?? false;
                }
            }
            else if (FormType == "apl")
                chkIsApproved.Checked = cp.IsCredentialApplication ?? false;
            else if (FormType == "rec")
                chkIsApproved.Checked = cp.IsCredentialing ?? false;
            else if (FormType == "ltr")
                chkIsApproved.Checked = cp.IsRecommendationLetter ?? false;
            else if (FormType == "cal" || FormType == "cal2")
                chkIsApproved.Checked = cp.IsClinicalAssignmentLetter ?? false;
            else chkIsApproved.Checked = false;

            chkIsVoid.Checked = cp.IsVoid ?? false;

            PopulateWorkExperienceGrid();
            PopulateCpdGrid();
            PopulateLicenseGrid();
            PopulateDocumentGrid();
            PopulateQuestionValue();
            PopulateTeamGrid();
            PopulateRecommendationResultGrid();
            PopulateEvalGrid();

            if (IsPostBack)
            {
                RefreshGridSheetItems();
            }
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(CredentialProcess entity, CredentialProcessQuestionCollection collValue)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewTransactionNo();
                _autoNumber.Save();
            }

            entity.TransactionNo = txtTransactionNo.Text;
            entity.QuestionFormID = QuestionFormID;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.PersonID = cboPersonID.SelectedValue.ToInt();
            entity.SREmploymentType = cboSREmploymentType.SelectedValue;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.PositionID = cboPositionID.SelectedValue == string.Empty ? -1 : cboPositionID.SelectedValue.ToInt();
            entity.SRProfessionGroup = cboSRProfessionGroup.SelectedValue;
            entity.SRClinicalWorkArea = cboSRClinicalWorkArea.SelectedValue;
            entity.SRClinicalAuthorityLevel = cboSRClinicalAuthorityLevel.SelectedValue;
            entity.QuestionnaireID = cboQuestionnaireID.SelectedValue.ToInt();
            entity.SREducationLevel = cboSREducationLevel.SelectedValue;
            entity.InstitutionName = txtInstitutionName.Text;
            entity.DiplomaNumber = txtDiplomaNumber.Text;
            
            if (!txtDiplomaDate.IsEmpty)
                entity.DiplomaDate = txtDiplomaDate.SelectedDate;
            else
                entity.str.DiplomaDate = string.Empty;

            if (!txtCompetencyAssessmentVerificationDate.IsEmpty)
                entity.CompetencyAssessmentVerificationDate = txtCompetencyAssessmentVerificationDate.SelectedDate;
            else
                entity.str.CompetencyAssessmentVerificationDate = string.Empty;

            if (!txtCompetencyAssessmentVerificationDate2.IsEmpty)
                entity.CompetencyAssessmentVerificationDate2 = txtCompetencyAssessmentVerificationDate2.SelectedDate;
            else
                entity.str.CompetencyAssessmentVerificationDate2 = string.Empty;

            if (!txtCredentialApplicationDate.IsEmpty)
                entity.CredentialApplicationDate = txtCredentialApplicationDate.SelectedDate;
            else
                entity.str.CredentialApplicationDate = string.Empty;

            entity.SRCredentialingStatus = cboSRCredentialingStatus.SelectedValue;
            entity.SRRecredentialReason = cboSRRecredentialReason.SelectedValue;

            if (!txtCredentialingDate.IsEmpty)
                entity.CredentialingDate = txtCredentialingDate.SelectedDate;
            else
                entity.str.CredentialingDate = string.Empty;

            if (FormType == "rec")
                entity.IsCertificateVerification = rbtIsCertificateVerification.SelectedValue == "1" ? true : false;
            
            entity.RecommendationNotes = txtRecommendationNotes.Text;
            
            if (FormType == "rec")
                entity.IsPerform = rbtIsPerform.SelectedValue == "1" ? true : false;

            if (!txtRecommendationLetterDate.IsEmpty)
                entity.RecommendationLetterDate = txtRecommendationLetterDate.SelectedDate;
            else
                entity.str.RecommendationLetterDate = string.Empty;

            entity.RecommendationLetterNo = txtRecommendationLetterNo.Text;

            if (!txtClinicalAssignmentLetterDate.IsEmpty)
                entity.ClinicalAssignmentLetterDate = txtClinicalAssignmentLetterDate.SelectedDate;
            else
                entity.str.ClinicalAssignmentLetterDate = string.Empty;

            entity.DecreeNo = txtDecreeNo.Text;

            if (!txtValidFrom.IsEmpty)
                entity.ValidFrom = txtValidFrom.SelectedDate;
            else
                entity.str.ValidFrom = string.Empty;

            if (!txtValidTo.IsEmpty)
                entity.ValidTo = txtValidTo.SelectedDate;
            else
                entity.str.ValidTo = string.Empty;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
            if (entity.es.IsAdded)
            {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = DateTime.Now;
            }

            if (FormType == "caa")
            {
                if (Role == "usr")
                {
                    entity.IsVoid = chkIsVoid.Checked;
                    entity.IsApproved = chkIsApproved.Checked;

                    foreach (var item in CredentialProcessLicenses)
                    {
                        item.TransactionNo = txtTransactionNo.Text;
                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        item.LastUpdateDateTime = DateTime.Now;
                    }

                    foreach (var item in CredentialProcessWorkExperiences)
                    {
                        item.TransactionNo = txtTransactionNo.Text;
                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        item.LastUpdateDateTime = DateTime.Now;
                    }
                }
                else if (Role == "eva")
                {
                    foreach (var item in CredentialCompetencyAssessmentEvaluators)
                    {
                        item.TransactionNo = txtTransactionNo.Text;
                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        item.LastUpdateDateTime = DateTime.Now;
                    }
                }
                else
                {
                    if (EvalRole == "1")
                        entity.IsVerified = chkIsApproved.Checked;
                    else
                        entity.IsVerified2 = chkIsApproved.Checked;
                }
            }

            if (FormType == "apl")
            {
                entity.IsCredentialApplication = chkIsApproved.Checked;

                foreach (var item in CredentialProcessCpds)
                {
                    item.TransactionNo = txtTransactionNo.Text;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }

            if (FormType == "rec")
            {
                entity.IsCredentialing = chkIsApproved.Checked;

                foreach (var item in CredentialProcessTeams)
                {
                    item.TransactionNo = txtTransactionNo.Text;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }

            if (FormType == "ltr")
            {
                entity.IsRecommendationLetter = chkIsApproved.Checked;
            }

            if (FormType == "cal" || FormType == "cal2")
            {
                entity.IsClinicalAssignmentLetter = chkIsApproved.Checked;
            }

            var dtbQuestion = QuestionDataTable(QuestionFormID);

            foreach (DataRow rowQuestion in dtbQuestion.Rows)
            {
                // Tips: Don't use entity.es.IsModified, krn belum tentu record sudah diedit waktu save
                SetCredentialProcessQuestion(entity.es.IsAdded, entity.TransactionNo, collValue, rowQuestion, rowQuestion["QuestionGroupID"].ToString());

                var quest = new QuestionQuery();
                quest.Where(quest.ParentQuestionID == rowQuestion["QuestionID"] && quest.SRAnswerType != string.Empty);
                var dtbSubQuestion = quest.LoadDataTable();

                foreach (DataRow rowSubQuestion in dtbSubQuestion.Rows)
                {
                    SetCredentialProcessQuestion(entity.es.IsAdded, entity.TransactionNo, collValue, rowSubQuestion, rowQuestion["QuestionGroupID"].ToString());
                }
            }
        }

        private void SetCredentialProcessQuestion(bool isNewRecord, string transactionNo, CredentialProcessQuestionCollection collValue, DataRow rowQuestion, string questionGroupID)
        {

            CredentialProcessQuestion hrLine;
            string questionID = rowQuestion[CredentialProcessQuestionMetadata.ColumnNames.QuestionID].ToString();
            //if (isNewRecord)
            //    hrLine = collValue.AddNew();
            //else
            hrLine = collValue.FindByPrimaryKey(txtTransactionNo.Text, QuestionFormID, questionGroupID, questionID) ?? collValue.AddNew();

            hrLine.TransactionNo = transactionNo;
            hrLine.QuestionFormID = QuestionFormID;
            hrLine.QuestionGroupID = questionGroupID;
            hrLine.QuestionID = questionID;
            hrLine.QuestionAnswerPrefix = rowQuestion["AnswerPrefix"].ToStringDefaultEmpty();
            hrLine.QuestionAnswerSuffix = rowQuestion["AnswerSuffix"].ToStringDefaultEmpty();

            string controlID = QuestionControlID(rowQuestion["QuestionID"].ToString());
            string answerType = rowQuestion[QuestionMetadata.ColumnNames.SRAnswerType].ToString();
            object obj = null;

            if (answerType != "DNT") //Dental Control
            {
                if (string.IsNullOrEmpty(rowQuestion[QuestionMetadata.ColumnNames.ReferenceQuestionID].ToString()))
                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID);
                else
                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, QuestionControlID(rowQuestion[QuestionMetadata.ColumnNames.ReferenceQuestionID].ToString()));

                if (obj == null)
                {
                    hrLine.MarkAsDeleted();
                    return;
                }
            }

            switch (answerType)
            {
                case "MSK":
                    var mskAnswerValue = (obj as RadMaskedTextBox);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(mskAnswerValue.TextWithLiterals);
                    break;
                case "DAT":
                    var dat = (obj as RadDatePicker);
                    hrLine.QuestionAnswerText = (dat.SelectedDate ?? DateTime.Now).ToShortDateString();
                    break;
                case "TIM":
                    var tim = (obj as RadTimePicker);
                    hrLine.QuestionAnswerText = (tim.SelectedDate ?? DateTime.Now).ToString("HH:mm");
                    break;
                case "DTM":
                    var dattim = (obj as RadDateTimePicker);
                    hrLine.QuestionAnswerText = (dattim.SelectedDate ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm");
                    break;
                case "NUM":
                    var numAnswerValue = (obj as RadNumericTextBox);
                    hrLine.QuestionAnswerNum = Convert.ToDecimal(numAnswerValue.Value);
                    break;
                case "MEM":
                    var memAnswerValue = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(memAnswerValue.Text);
                    break;
                case "TXT":
                    var txtAnswerValue = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(txtAnswerValue.Text);
                    break;
                case "CBO":
                    var cboAnswerValue = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cboAnswerValue.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cboAnswerValue.Text);
                    break;
                case "CHK":
                    var chk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = chk != null && chk.Checked ? "1" : "0";
                    break;
                case "CTX":
                    var ctxChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = ctxChk != null && ctxChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    var ctxTxt = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(ctxTxt.Text));
                    break;
                case "CDO":
                    var cdoChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = cdoChk != null && cdoChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    var cdoCbo = (obj as RadComboBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cdoCbo.Text));
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cdoCbo.SelectedValue);
                    break;
                case "CTM":
                    var ctmChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = ctmChk != null && ctmChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    var ctmTxt = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(ctmTxt.Text));
                    break;
                case "CNM":
                    var cnmChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = cnmChk != null && cnmChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    var cnmNum = (obj as RadNumericTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cnmNum.Text));
                    break;
                case "CDT":
                    var cdtChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = cdtChk != null && cdtChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    var cdtDattim = (obj as RadDateTimePicker);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate((cdtDattim.SelectedDate ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm")));
                    break;
                case "CBT":
                    var cbtCbo = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbtCbo.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbtCbo.Text);

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    var cbtTxt = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbtTxt.Text));
                    break;
                case "CBN":
                    var cbnCbo = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbnCbo.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbnCbo.Text);

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    var cbnNum = (obj as RadNumericTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbnNum.Text));
                    break;
                case "CBM":
                    var cbtCbm = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbtCbm.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbtCbm.Text);

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    var cbtTxm = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbtTxm.Text));
                    break;
                case "CB2":
                    var cbo1 = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbo1.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbo1.Text);

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    var cbo2 = (obj as RadComboBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbo2.Text));

                    hrLine.QuestionAnswerSelectionLineID = string.Format("{0}|{1}", HtmlTagHelper.Validate(cbo1.SelectedValue), HtmlTagHelper.Validate(cbo2.SelectedValue));
                    break;
                case "TTX":
                    var txt1 = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(txt1.Text);

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    var txt2 = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(txt2.Text));
                    break;
                case "TBL":
                    var tbl = (obj as HtmlTable);
                    // get row length and col length
                    var rCount = tbl.Rows.Count;
                    string ansText = string.Empty;
                    if (rCount > 0)
                    {
                        var cCount = tbl.Rows[0].Cells.Count;
                        for (var r = 1; r < rCount; r++)
                        {
                            for (var c = 0; c < cCount; c++)
                            {
                                var objCell = Helper.FindControlRecursive(
                                    pnlCredentialingQuestion,
                                    controlID + "_" + r.ToString() + "_" + c.ToString());
                                var objCellText = (objCell as RadTextBox);
                                ansText += (ansText.Equals(string.Empty) ? "" : "|") + HtmlTagHelper.Validate(objCellText.Text);
                            }
                        }
                    }
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(ansText);
                    break;
            }
            if (hrLine.es.IsAdded || hrLine.es.IsModified)
            {
                //hrLine.LastUpdateByUserID = AppSession.UserLogin.UserID;
                //hrLine.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(CredentialProcess entity, CredentialProcessQuestionCollection collValue)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                if (FormType == "caa")
                {
                    if (Role == "usr")
                    {
                        CredentialProcessWorkExperiences.Save();
                        CredentialProcessLicenses.Save();
                    }
                    else if (Role == "eva")
                    {
                        CredentialCompetencyAssessmentEvaluators.Save();
                    }
                    else
                    {
                        var eval = new CredentialCompetencyAssessmentEvaluator();
                        if (eval.LoadByPrimaryKey(entity.TransactionNo, AppSession.UserLogin.PersonID.ToInt()))
                        {
                            eval.IsEvaluated = false;
                            eval.Save();
                        }
                    }
                }

                if (FormType == "apl")
                {
                    collValue.Save();
                    CredentialProcessLicenses.Save();
                    CredentialProcessCpds.Save();

                    var supportingDocs = new CredentialProcessDocumentCollection();
                    supportingDocs.Query.Where(supportingDocs.Query.TransactionNo == entity.TransactionNo);
                    supportingDocs.LoadAll();

                    foreach (GridDataItem dataItem in grdDocument.MasterTableView.Items)
                    {
                        string id = dataItem.GetDataKeyValue("DocumentItemID").ToString();
                        bool isSelect = ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("chkIsSelect")).Checked;
                        string notes = ((RadTextBox)dataItem.FindControl("txtNotes")).Text;

                        bool isExist = false;
                        foreach (CredentialProcessDocument row in supportingDocs)
                        {
                            if (row.DocumentItemID.Equals(id))
                            {
                                isExist = true;
                                row.Notes = notes;

                                if (!isSelect)
                                    row.MarkAsDeleted();
                                break;
                            }
                        }
                        //Add
                        if (!isExist && isSelect)
                        {
                            CredentialProcessDocument row = supportingDocs.AddNew();
                            row.TransactionNo = entity.TransactionNo;
                            row.DocumentItemID = id;
                            row.Notes = notes;
                            row.IsVerified = false;
                            row.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            row.LastUpdateDateTime = DateTime.Now;
                        }
                    }

                    supportingDocs.Save();
                }

                if (FormType == "rec")
                {
                    CredentialProcessTeams.Save();

                    var supportingDocs = new CredentialProcessDocumentCollection();
                    supportingDocs.Query.Where(supportingDocs.Query.TransactionNo == entity.TransactionNo);
                    supportingDocs.LoadAll();

                    foreach (GridDataItem dataItem in grdDocument.MasterTableView.Items)
                    {
                        string id = dataItem.GetDataKeyValue("DocumentItemID").ToString();
                        bool isVerified = ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("chkIsVerified")).Checked;
                       
                        foreach (CredentialProcessDocument row in supportingDocs)
                        {
                            if (row.DocumentItemID.Equals(id))
                            {
                                row.IsVerified = isVerified;

                                break;
                            }
                        }
                    }

                    supportingDocs.Save();

                    var recomendationResults = new CredentialProcessRecommendationResultCollection();
                    recomendationResults.Query.Where(recomendationResults.Query.TransactionNo == entity.TransactionNo);
                    recomendationResults.LoadAll();

                    foreach (GridDataItem dataItem in grdRecommendationResult.MasterTableView.Items)
                    {
                        string id = dataItem.GetDataKeyValue("SRRecommendationResult").ToString();
                        bool isSelect = ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("chkIsSelect")).Checked;
                        string notes = ((RadTextBox)dataItem.FindControl("txtNotes")).Text;

                        bool isExist = false;
                        foreach (CredentialProcessRecommendationResult row in recomendationResults)
                        {
                            if (row.SRRecommendationResult.Equals(id))
                            {
                                isExist = true;
                                row.Notes = notes;

                                if (!isSelect)
                                    row.MarkAsDeleted();
                                break;
                            }
                        }
                        //Add
                        if (!isExist && isSelect)
                        {
                            CredentialProcessRecommendationResult row = recomendationResults.AddNew();
                            row.TransactionNo = entity.TransactionNo;
                            row.SRRecommendationResult = id;
                            row.Notes = notes;
                            row.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            row.LastUpdateDateTime = DateTime.Now;
                        }
                    }

                    recomendationResults.Save();
                }

                if (FormType != "apl")
                {
                    foreach (GridDataItem dataItem in grdSheet.MasterTableView.Items)
                    {
                        string questionnaireItemId = dataItem.GetDataKeyValue("QuestionnaireItemID").ToString();

                        var item = new CredentialProcessSheet();
                        item.Query.Where(item.Query.TransactionNo == entity.TransactionNo, item.Query.QuestionnaireItemID == questionnaireItemId);
                        if (!item.Query.Load())
                        {
                            item = new CredentialProcessSheet();
                            item.AddNew();
                        }

                        item.TransactionNo = entity.TransactionNo;
                        item.QuestionnaireItemID = questionnaireItemId.ToInt();

                        string currentAbility = ((RadComboBox)dataItem.FindControl("cboSRCurrentAbility")).SelectedValue;
                        string selfAssessmentNotes = ((RadTextBox)dataItem.FindControl("txtSelfAssessmentNotes")).Text;
                        string currentAbilitySvr = ((RadComboBox)dataItem.FindControl("cboSRCurrentAbilitySupervisor")).SelectedValue;
                        string currentAbilitySvr2 = ((RadComboBox)dataItem.FindControl("cboSRCurrentAbilitySupervisor2")).SelectedValue;
                        string review = ((RadComboBox)dataItem.FindControl("cboSRReview")).SelectedValue;
                        string recomendation = ((RadComboBox)dataItem.FindControl("cboSRRecomendation")).SelectedValue;
                        string conclusion = ((RadComboBox)dataItem.FindControl("cboSRConclusion")).SelectedValue;
                        string notes = ((RadTextBox)dataItem.FindControl("txtNotes")).Text;

                        if (FormType == "caa")
                        {
                            if (Role == "usr")
                            {
                                item.SRCurrentAbility = currentAbility;
                                item.SelfAssessmentNotes = selfAssessmentNotes;
                            }
                            else if (Role == "svr")
                            {
                                if (EvalRole == "1")
                                    item.SRCurrentAbilitySupervisor = currentAbilitySvr;
                                else
                                    item.SRCurrentAbilitySupervisor2 = currentAbilitySvr2;
                            }
                        }
                        else if (FormType == "rec")
                        {
                            item.SRReview = review;
                            item.SRRecomendation = recomendation;
                            item.SRConclusion = conclusion;
                            item.Notes = notes;
                        }

                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        item.LastUpdateDateTime = DateTime.Now;

                        item.Save();
                    }
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new CredentialProcessQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TransactionNo > txtTransactionNo.Text, que.PersonID == AppSession.UserLogin.PersonID.ToInt());
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < this.txtTransactionNo.Text, que.PersonID == AppSession.UserLogin.PersonID.ToInt());
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new CredentialProcess();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        private void SetReadOnlyCredentialProcessQuestion(bool isReadOnly, DataRow rowQuestion, string questionGroupID)
        {
            string questionID = rowQuestion[CredentialProcessQuestionMetadata.ColumnNames.QuestionID].ToString();
            string controlID = QuestionControlID(rowQuestion["QuestionID"].ToString());
            string answerType = rowQuestion[QuestionMetadata.ColumnNames.SRAnswerType].ToString();
            object obj;

            if (string.IsNullOrEmpty(rowQuestion[QuestionMetadata.ColumnNames.ReferenceQuestionID].ToString()))
                obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID);
            else
                obj = Helper.FindControlRecursive(pnlCredentialingQuestion, QuestionControlID(rowQuestion[QuestionMetadata.ColumnNames.ReferenceQuestionID].ToString()));

            switch (answerType)
            {
                case "MSK":
                    (obj as RadMaskedTextBox).ReadOnly = isReadOnly;
                    break;
                case "DAT":
                    (obj as RadDatePicker).DatePopupButton.Enabled = !isReadOnly;
                    (obj as RadDatePicker).DateInput.ReadOnly = isReadOnly;
                    break;
                case "TIM":
                    (obj as RadTimePicker).DatePopupButton.Enabled = !isReadOnly;
                    (obj as RadTimePicker).DateInput.ReadOnly = isReadOnly;
                    break;
                case "DTM":
                    (obj as RadDateTimePicker).DatePopupButton.Enabled = !isReadOnly;
                    (obj as RadDateTimePicker).TimePopupButton.Enabled = !isReadOnly;
                    (obj as RadDateTimePicker).DateInput.ReadOnly = isReadOnly;
                    break;
                case "NUM":
                    (obj as RadNumericTextBox).ReadOnly = isReadOnly;
                    break;
                case "MEM":
                    (obj as RadTextBox).ReadOnly = isReadOnly;
                    break;
                case "TXT":
                    (obj as RadTextBox).ReadOnly = isReadOnly;
                    break;
                case "CBO":
                    (obj as RadComboBox).Enabled = !isReadOnly;
                    break;
                case "CHK":
                    (obj as CheckBox).Enabled = !isReadOnly;
                    break;
                case "CTX":
                    (obj as CheckBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    (obj as RadTextBox).ReadOnly = isReadOnly;
                    break;
                case "CDO":
                    (obj as CheckBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    (obj as RadComboBox).Enabled = !isReadOnly;
                    break;
                case "CTM":
                    (obj as CheckBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    (obj as RadTextBox).ReadOnly = isReadOnly;
                    break;
                case "CNM":
                    (obj as CheckBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    (obj as RadNumericTextBox).ReadOnly = isReadOnly;
                    break;
                case "CDT":
                    (obj as CheckBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    (obj as RadDateTimePicker).DatePopupButton.Enabled = !isReadOnly;
                    (obj as RadDateTimePicker).TimePopupButton.Enabled = !isReadOnly;
                    (obj as RadDateTimePicker).DateInput.ReadOnly = isReadOnly;
                    break;
                case "CBT":
                    (obj as RadComboBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    (obj as RadTextBox).ReadOnly = isReadOnly;
                    break;
                case "CBN":
                    (obj as RadComboBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    (obj as RadNumericTextBox).ReadOnly = isReadOnly;
                    break;
                case "CBM":
                    (obj as RadComboBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    (obj as RadTextBox).ReadOnly = isReadOnly;
                    break;
                case "CB2":
                    (obj as RadComboBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    (obj as RadComboBox).Enabled = !isReadOnly;
                    break;
                case "TTX":
                    (obj as RadTextBox).ReadOnly = isReadOnly;

                    obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                    (obj as RadTextBox).ReadOnly = isReadOnly;
                    break;
                case "TBL":
                    var tbl = (obj as HtmlTable);
                    // get row length and col length
                    var rCount = tbl.Rows.Count;
                    string ansText = string.Empty;
                    if (rCount > 0)
                    {
                        var cCount = tbl.Rows[0].Cells.Count;
                        for (var r = 1; r < rCount; r++)
                        {
                            for (var c = 0; c < cCount; c++)
                            {
                                var objCell = Helper.FindControlRecursive(
                                    pnlCredentialingQuestion,
                                    controlID + "_" + r.ToString() + "_" + c.ToString());
                                (objCell as RadTextBox).ReadOnly = isReadOnly;
                            }
                        }
                    }
                    break;
            }
        }

        #endregion

        #region ComboBox
        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );

            query.Where(
                query.SREmployeeStatus == AppSession.Parameter.EmployeeStatusActive,
                query.Or
                        (
                            query.EmployeeNumber.Like(searchText),
                            query.EmployeeName.Like(searchText)
                        )
                );

            cboPersonID.DataSource = query.LoadDataTable();
            cboPersonID.DataBind();
        }

        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboPersonID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var employeeInfo = new VwEmployeeTable();
            var employeeInfoQ = new VwEmployeeTableQuery();
            employeeInfoQ.es.Top = 1;
            employeeInfoQ.Where(employeeInfoQ.PersonID == e.Value.ToInt());
            employeeInfo.Load(employeeInfoQ);
            if (employeeInfo != null)
            {
                txtEmployeeNo.Text = employeeInfo.EmployeeNumber;
                txtPlaceDOB.Text = string.Format("{0}, {1}", employeeInfo.PlaceBirth, Convert.ToDateTime(employeeInfo.BirthDate).ToString("dd-MMM-yyyy"));
                cboSREmploymentType.SelectedValue = employeeInfo.SREmploymentType;
                if (cboSREmploymentType.SelectedValue == AppSession.Parameter.EmploymentTypePermanent)
                    txtREmploymentPermanentDate.SelectedDate = employeeInfo.TglDiangkat;
                else
                    txtREmploymentPermanentDate.Clear();

                var org = new OrganizationUnitQuery();
                org.Where(org.OrganizationUnitID == employeeInfo.ServiceUnitID.ToInt());
                var orgDtb = org.LoadDataTable();
                if (orgDtb.Rows.Count > 0)
                {
                    cboServiceUnitID.DataSource = orgDtb;
                    cboServiceUnitID.DataBind();
                    cboServiceUnitID.SelectedValue = employeeInfo.ServiceUnitID;
                }
                else
                {
                    cboServiceUnitID.Items.Clear();
                    cboServiceUnitID.SelectedValue = string.Empty;
                    cboServiceUnitID.Text = string.Empty;
                }

                var pos = new PositionQuery();
                pos.Where(pos.PositionID == employeeInfo.PositionID.ToInt());
                var posDtb = pos.LoadDataTable();
                if (posDtb.Rows.Count > 0)
                {
                    cboPositionID.DataSource = posDtb;
                    cboPositionID.DataBind();
                    cboPositionID.SelectedValue = employeeInfo.PositionID.ToString();
                }
                else
                {
                    cboPositionID.Items.Clear();
                    cboPositionID.SelectedValue = string.Empty;
                    cboPositionID.Text = string.Empty;
                }
                cboSREducationLevel.SelectedValue = employeeInfo.SREducationLevel;

                var eduHist = new PersonalEducationHistory();
                var eduHistQ = new PersonalEducationHistoryQuery();
                eduHistQ.es.Top = 1;
                eduHistQ.Where(eduHistQ.PersonID == e.Value.ToInt(), eduHistQ.SREducationLevel == employeeInfo.SREducationLevel);
                eduHistQ.OrderBy(eduHistQ.GraduateDate.Descending, eduHistQ.EndYear.Descending, eduHistQ.StartYear.Descending);
                eduHist.Load(eduHistQ);
                if (eduHist != null)
                {
                    txtInstitutionName.Text = eduHist.InstitutionName;
                    txtDiplomaNumber.Text = eduHist.DiplomaNo;
                    if (eduHist.GraduateDate.HasValue)
                        txtDiplomaDate.SelectedDate = eduHist.GraduateDate;
                    else
                        txtDiplomaDate.Clear();
                }
            }
            else
            {
                txtEmployeeNo.Text = string.Empty;
                txtPlaceDOB.Text = string.Empty;
                cboSREmploymentType.SelectedValue = string.Empty;
                cboSREmploymentType.Text = string.Empty;
                txtREmploymentPermanentDate.Clear();

                cboServiceUnitID.Items.Clear();
                cboServiceUnitID.SelectedValue = string.Empty;
                cboServiceUnitID.Text = string.Empty;

                cboPositionID.Items.Clear();
                cboPositionID.SelectedValue = string.Empty;
                cboPositionID.Text = string.Empty;

                cboSREducationLevel.SelectedValue = string.Empty;
                cboSREducationLevel.Text = string.Empty;
            }

            var ecpq = new EmployeeClinicalPrivilegeQuery();
            ecpq.Where(ecpq.PersonID == e.Value.ToInt(), ecpq.ValidFrom <= DateTime.Now.Date);
            ecpq.OrderBy(ecpq.ValidFrom.Descending);
            ecpq.es.Top = 1;
            DataTable ecpdt = ecpq.LoadDataTable();
            if (ecpdt.Rows.Count > 0)
            {
                cboSRProfessionGroup.SelectedValue = ecpdt.Rows[0]["SRProfessionGroup"].ToString();

                var workArea = ecpdt.Rows[0]["SRClinicalWorkArea"].ToString();
                if (!string.IsNullOrEmpty(workArea))
                {
                    var cwaQuery = new AppStandardReferenceItemQuery();
                    cwaQuery.Where(cwaQuery.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea.ToString(), cwaQuery.ItemID == workArea);
                    var cwaDtb = cwaQuery.LoadDataTable();
                    if (cwaDtb.Rows.Count > 0)
                    {
                        cboSRClinicalWorkArea.DataSource = cwaDtb;
                        cboSRClinicalWorkArea.DataBind();
                        cboSRClinicalWorkArea.SelectedValue = workArea;
                    }
                    else
                    {
                        cboSRClinicalWorkArea.Items.Clear();
                        cboSRClinicalWorkArea.SelectedValue = string.Empty;
                        cboSRClinicalWorkArea.Text = string.Empty;
                    }
                }
                else
                {
                    cboSRClinicalWorkArea.Items.Clear();
                    cboSRClinicalWorkArea.SelectedValue = string.Empty;
                    cboSRClinicalWorkArea.Text = string.Empty;
                }

                cboSRClinicalAuthorityLevel.SelectedValue = ecpdt.Rows[0]["SRClinicalAuthorityLevel"].ToString();

                GetQuestionnaireID();

                grdLicense.DataSource = CredentialProcessLicenses;
                grdLicense.DataBind();
            }
            else
            {
                cboSRProfessionGroup.SelectedValue = string.Empty;
                cboSRProfessionGroup.Text = string.Empty;

                cboSRClinicalWorkArea.Items.Clear();
                cboSRClinicalWorkArea.SelectedValue = string.Empty;
                cboSRClinicalWorkArea.Text = string.Empty;

                cboSRClinicalAuthorityLevel.SelectedValue = string.Empty;
                cboSRClinicalAuthorityLevel.Text = string.Empty;

                cboQuestionnaireID.Items.Clear();
                cboQuestionnaireID.SelectedValue = string.Empty;
                cboQuestionnaireID.Text = string.Empty;

                txtInfo1.Text = string.Empty;
                txtInfo2.Text = string.Empty;
                txtInfo3.Text = string.Empty;
                txtInfo4.Text = string.Empty;
            }

            RefreshGridSheetItems();
        }

        protected void cboServiceUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new OrganizationUnitQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.OrganizationUnitID,
                    query.OrganizationUnitName
                );

            query.Where(
                query.SROrganizationLevel == "0",
                query.OrganizationUnitName.Like(searchText)
                );

            cboServiceUnitID.DataSource = query.LoadDataTable();
            cboServiceUnitID.DataBind();
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }

        protected void cboPositionID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new PositionQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.PositionID,
                    query.PositionName
                );

            query.Where(
                query.PositionName.Like(searchText)
                );

            cboPositionID.DataSource = query.LoadDataTable();
            cboPositionID.DataBind();
        }

        protected void cboPositionID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionID"].ToString();
        }

        protected void cboSRProfessionGroup_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRClinicalWorkArea.Items.Clear();
            cboSRClinicalWorkArea.SelectedValue = string.Empty;
            cboSRClinicalWorkArea.Text = string.Empty;

            cboSRClinicalAuthorityLevel.Items.Clear();
            cboSRClinicalAuthorityLevel.SelectedValue = string.Empty;
            cboSRClinicalAuthorityLevel.Text = string.Empty;

            cboQuestionnaireID.Items.Clear();
            cboQuestionnaireID.SelectedValue = string.Empty;
            cboQuestionnaireID.Text = string.Empty;

            txtInfo1.Text = string.Empty;
            txtInfo2.Text = string.Empty;
            txtInfo3.Text = string.Empty;
            txtInfo4.Text = string.Empty;
            RefreshGridSheetItems();
        }

        protected void cboSRClinicalWorkArea_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRClinicalWorkArea_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Where
                (
                    query.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea,
                    query.ItemName.Like(searchText),
                    query.IsActive == true
                );
            query.Where(query.ReferenceID == cboSRProfessionGroup.SelectedValue);
            query.OrderBy(query.ItemID.Ascending);

            cboSRClinicalWorkArea.DataSource = query.LoadDataTable();
            cboSRClinicalWorkArea.DataBind();
        }

        protected void cboSRClinicalWorkArea_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRClinicalAuthorityLevel.Items.Clear();
            cboSRClinicalAuthorityLevel.SelectedValue = string.Empty;
            cboSRClinicalAuthorityLevel.Text = string.Empty;

            cboQuestionnaireID.Items.Clear();
            cboQuestionnaireID.SelectedValue = string.Empty;
            cboQuestionnaireID.Text = string.Empty;

            GetQuestionnaireID();
            RefreshGridSheetItems();
        }

        protected void cboSRClinicalAuthorityLevel_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRClinicalAuthorityLevel_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Where
                (
                    query.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel,
                    query.ItemName.Like(searchText),
                    query.IsActive == true
                );
            query.Where(query.ReferenceID == cboSRClinicalWorkArea.SelectedValue);
            query.OrderBy(query.ItemID.Ascending);

            cboSRClinicalAuthorityLevel.DataSource = query.LoadDataTable();
            cboSRClinicalAuthorityLevel.DataBind();
        }

        protected void cboSRClinicalAuthorityLevel_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboQuestionnaireID.Items.Clear();
            cboQuestionnaireID.SelectedValue = string.Empty;
            cboQuestionnaireID.Text = string.Empty;

            GetQuestionnaireID();
            RefreshGridSheetItems();
        }

        protected void cboQuestionnaireID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["QuestionnaireName"].ToString() + " [" + ((DataRowView)e.Item.DataItem)["QuestionnaireCode"].ToString() + "]";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["QuestionnaireID"].ToString();
        }

        protected void cboQuestionnaireID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new CredentialQuestionnaireQuery("a");
            query.Where
                (
                    query.QuestionnaireName.Like(searchText),
                    query.IsActive == true
                );
            query.Where(query.SRProfessionGroup == cboSRProfessionGroup.SelectedValue,
                query.SRClinicalWorkArea == cboSRClinicalWorkArea.SelectedValue,
                query.SRClinicalAuthorityLevel ==
                cboSRClinicalAuthorityLevel.SelectedValue,
                query.IsActive == true);
            query.OrderBy(query.QuestionnaireCode.Ascending);

            cboQuestionnaireID.DataSource = query.LoadDataTable();
            cboQuestionnaireID.DataBind();
        }

        protected void cboQuestionnaireID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var cq = new CredentialQuestionnaire();
            if (cq.LoadByPrimaryKey(e.Value.ToInt()))
            {
                txtInfo1.Text = cq.Info1;
                txtInfo2.Text = cq.Info2;
                txtInfo3.Text = cq.Info3;
                txtInfo4.Text = cq.Info4;
            }
            else
            {
                txtInfo1.Text = string.Empty;
                txtInfo2.Text = string.Empty;
                txtInfo3.Text = string.Empty;
                txtInfo4.Text = string.Empty;
            }
            RefreshGridSheetItems();
        }

        private void GetQuestionnaireID()
        {
            var questionnaire = new CredentialQuestionnaire();
            var questionnaireQ = new CredentialQuestionnaireQuery();
            questionnaireQ.Where(questionnaireQ.SRProfessionGroup == cboSRProfessionGroup.SelectedValue,
                questionnaireQ.SRClinicalWorkArea == cboSRClinicalWorkArea.SelectedValue,
                questionnaireQ.SRClinicalAuthorityLevel == cboSRClinicalAuthorityLevel.SelectedValue,
                questionnaireQ.IsActive == true);
            DataTable dtb = questionnaireQ.LoadDataTable();
            if (dtb.Rows.Count == 1)
            {
                cboQuestionnaireID.DataSource = dtb;
                cboQuestionnaireID.DataBind();

                questionnaire.Load(questionnaireQ);
                cboQuestionnaireID.SelectedValue = questionnaire.QuestionnaireID.ToString();
                txtInfo1.Text = questionnaire.Info1;
                txtInfo2.Text = questionnaire.Info2;
                txtInfo3.Text = questionnaire.Info3;
                txtInfo4.Text = questionnaire.Info4;
            }
            else
            {
                cboQuestionnaireID.Items.Clear();
                cboQuestionnaireID.SelectedValue = string.Empty;
                cboQuestionnaireID.Text = string.Empty;
                txtInfo1.Text = string.Empty;
                txtInfo2.Text = string.Empty;
                txtInfo3.Text = string.Empty;
                txtInfo4.Text = string.Empty;
            }
        }

        #endregion

        #region Record Detail Method Function CredentialProcessLicense

        private CredentialProcessLicenseCollection CredentialProcessLicenses
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCredentialProcessLicense" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((CredentialProcessLicenseCollection)(obj));
                    }
                }
                CredentialProcessLicenseCollection coll;

                var x = new CredentialProcessLicenseCollection();
                x.Query.Where(x.Query.TransactionNo == txtTransactionNo.Text);
                x.LoadAll();

                if (DataModeCurrent == AppEnum.DataMode.New)
                    coll = LoadFromPersonalPersonalLicence();
                else
                    coll = LoadFromCredentialHist();

                Session["collCredentialProcessLicense" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collCredentialProcessLicense" + Request.UserHostName] = value; }
        }

        private CredentialProcessLicenseCollection LoadFromPersonalPersonalLicence()
        {
            var coll = new CredentialProcessLicenseCollection();
            var query = new CredentialProcessLicenseQuery("a");
            query.Select
                (
                query,
                "<'' AS refToAppStdField_LicenseType>"
                );
            query.Where(query.TransactionNo == "1");
            coll.Load(query);

            var pId = cboPersonID.SelectedValue;

            EmployeeWorkingInfoCollection ewiColl = new EmployeeWorkingInfoCollection();
            DataTable dtb = ewiColl.GetActiveLicense(pId);

            foreach (DataRow row in dtb.Rows)
            {
                var c = coll.AddNew();
                c.TransactionNo = txtTransactionNo.Text;
                c.SRLicenseType = row["SRLicenceType"].ToString();
                c.LicenseTypeName = row["LicenseTypeName"].ToString();
                c.LicenseNo = row["Note"].ToString();
                c.DateOfIssue = Convert.ToDateTime(row["ValidFrom"]);
                c.ValidUntil = Convert.ToDateTime(row["ValidTo"]);
            }

            return coll;
        }

        private CredentialProcessLicenseCollection LoadFromCredentialHist()
        {
            var coll = new CredentialProcessLicenseCollection();
            var query = new CredentialProcessLicenseQuery("a");
            var license = new AppStandardReferenceItemQuery("c");

            query.Select
                (
                query,
                license.ItemName.As("refToAppStdField_LicenseType")
                );

            query.LeftJoin(license).On
               (
                   license.StandardReferenceID == AppEnum.StandardReference.LicenseType.ToString() &
                   license.ItemID == query.SRLicenseType
               );

            query.Where(query.TransactionNo == txtTransactionNo.Text);
            query.OrderBy(query.SRLicenseType.Ascending);

            coll.Load(query);

            return coll;
        }


        private void RefreshCommandItemLicense(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read) && FormType == "apl";
            grdLicense.Columns[0].Visible = isVisible;
            grdLicense.Columns[grdLicense.Columns.Count - 1].Visible = isVisible;

            grdLicense.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdLicense.Rebind();
        }

        private void PopulateLicenseGrid()
        {
            //Display Data Detail
            CredentialProcessLicenses = null; //Reset Record Detail
            grdLicense.DataSource = CredentialProcessLicenses; //Requery
            grdLicense.MasterTableView.IsItemInserted = false;
            grdLicense.MasterTableView.ClearEditItems();
            grdLicense.DataBind();
        }

        protected void grdLicense_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdLicense.DataSource = CredentialProcessLicenses;
        }

        protected void grdLicense_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            CredentialProcessLicense entity = FindLicenseItem(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                                                  [CredentialProcessLicenseMetadata.ColumnNames.SRLicenseType].ToString());

            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdLicense_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            string id = item.OwnerTableView.DataKeyValues[item.ItemIndex][CredentialProcessLicenseMetadata.ColumnNames.SRLicenseType].ToString();
            CredentialProcessLicense entity = FindLicenseItem(id);

            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdLicense_InsertCommand(object source, GridCommandEventArgs e)
        {
            CredentialProcessLicense entity = CredentialProcessLicenses.AddNew();

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdLicense.Rebind();
        }

        private CredentialProcessLicense FindLicenseItem(string id)
        {
            CredentialProcessLicenseCollection coll = CredentialProcessLicenses;
            CredentialProcessLicense retEntity = null;
            foreach (CredentialProcessLicense rec in coll)
            {
                if (rec.SRLicenseType.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(CredentialProcessLicense entity, GridCommandEventArgs e)
        {
            var userControl = (CredentialingLicenseItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = txtTransactionNo.Text;
                entity.SRLicenseType = userControl.SRLicenseType;
                entity.LicenseTypeName = userControl.LicenseTypeName;
                entity.LicenseNo = userControl.LicenseNo;
                entity.DateOfIssue = userControl.DateOfIssue;
                entity.ValidUntil = userControl.ValidUntil;
            }
        }

        #endregion

        #region Record Detail Method Function CredentialProcessDocument

        private void PopulateDocumentGrid()
        {
            //Display Data Detail
            grdDocument.DataSource = GetSupportingDocuments();
            grdDocument.DataBind();
        }

        protected void grdDocument_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDocument.DataSource = GetSupportingDocuments();
        }

        private DataTable GetSupportingDocuments()
        {
            var query = new CredentialProcessDocumentQuery("a");
            var qrRef = new AppStandardReferenceItemQuery("b");
            if (this.DataModeCurrent == AppEnum.DataMode.Read || FormType != "apl")
            {
                query.InnerJoin(qrRef).On(query.DocumentItemID == qrRef.ItemID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);
            }
            else
            {
                query.RightJoin(qrRef).On(query.DocumentItemID == qrRef.ItemID & query.TransactionNo == txtTransactionNo.Text);
            }
            query.Where(qrRef.StandardReferenceID == AppEnum.StandardReference.CredentialSupportingDocuments.ToString(), qrRef.IsActive == true);
            query.OrderBy(qrRef.ItemName.Ascending);
            query.Select
                (
                    "<CONVERT(BIT,CASE WHEN COALESCE(a.DocumentItemID,'')='' THEN 0 ELSE 1 END) as IsSelect>",
                    qrRef.ItemID.As("DocumentItemID"),
                    qrRef.ItemName.As("DocumentName"),
                    "<ISNULL(a.Notes, '') AS Notes>",
                    @"<ISNULL(a.IsVerified, 0) AS IsVerified>"
                );
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        private void RefreshCommandItemDocument(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdDocument.Columns[0].Visible = isVisible && (FormType != "rec");
            grdDocument.Columns.FindByUniqueName("IsSelect").Visible = isVisible && (FormType == "rec");
            grdDocument.Columns.FindByUniqueName("chkIsVerified").Visible = isVisible && (FormType == "rec");
            grdDocument.Columns.FindByUniqueName("IsVerified").Visible = !isVisible || !(FormType == "rec");

            //Perbaharui tampilan dan data
            grdDocument.Rebind();
        }
        #endregion

        #region CredentialingQuestionRecordLine

        private string QuestionControlID(string questionID)
        {
            return "quest" + questionID.Replace('.', '_');
        }

        private string RfvControlID(string questionID)
        {
            return "rfv" + questionID.Replace('.', '_');
        }

        private void PopulateQuestionValue()
        {
            var query = new CredentialProcessQuestionQuery("a");
            var qQuest = new QuestionQuery("b");

            query.InnerJoin(qQuest).On(query.QuestionID == qQuest.QuestionID);

            query.Select
                (
                    query.QuestionID,
                    query.QuestionAnswerSelectionLineID,
                    query.QuestionAnswerNum,
                    "<CASE WHEN b.SRAnswerType = 'DNT' THEN a.QuestionAnswerText2 ELSE a.QuestionAnswerText END AS QuestionAnswerText>",
                    //query.QuestionAnswerText,
                    qQuest.SRAnswerType
                );
            query.Where
                (
                    query.TransactionNo == txtTransactionNo.Text,
                    query.QuestionFormID == QuestionFormID,
                    qQuest.SRAnswerType != "LBL"
                );
            query.OrderBy(qQuest.IndexNo.Ascending);

            DataTable dtbValue = query.LoadDataTable();

            foreach (DataRow dataRow in dtbValue.Rows)
            {
                string controlID = QuestionControlID(dataRow["QuestionID"].ToString());
                string answerType = dataRow[QuestionMetadata.ColumnNames.SRAnswerType].ToString();
                object obj;


                obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID);
                if (obj == null)
                    if (answerType != "DNT") continue;

                switch (answerType)
                {
                    case "MSK":
                        var mskAnswerValue = (obj as RadMaskedTextBox);
                        if (mskAnswerValue != null)
                            mskAnswerValue.Text = HtmlTagHelper.Devalidate(dataRow["QuestionAnswerText"].ToStringDefaultEmpty());
                        break;
                    case "DAT":
                        var dtAnswerValue = (obj as RadDatePicker);
                        if (dtAnswerValue != null)
                            dtAnswerValue.SelectedDate = Convert.ToDateTime(dataRow["QuestionAnswerText"]);
                        break;
                    case "TIM":
                        var tpAnswerValue = (obj as RadTimePicker);
                        if (tpAnswerValue != null)
                            tpAnswerValue.SelectedDate = Convert.ToDateTime(dataRow["QuestionAnswerText"]);
                        break;
                    case "DTM":
                        var dtmAnswerValue = (obj as RadDateTimePicker);
                        if (dtmAnswerValue != null)
                            dtmAnswerValue.SelectedDate = Convert.ToDateTime(dataRow["QuestionAnswerText"]);
                        break;
                    case "NUM":
                        var numAnswerValue = (obj as RadNumericTextBox);
                        if (numAnswerValue != null)
                            if (!dataRow["QuestionAnswerNum"].Equals("&nbsp;") &&
                                dataRow["QuestionAnswerNum"] != DBNull.Value)
                                numAnswerValue.Value = Convert.ToDouble(dataRow["QuestionAnswerNum"]);
                        break;
                    case "TXT":
                        var txtAnswerValue = (obj as RadTextBox);
                        if (txtAnswerValue != null)
                            txtAnswerValue.Text = HtmlTagHelper.Devalidate(dataRow["QuestionAnswerText"].ToStringDefaultEmpty());
                        break;
                    case "MEM":
                        var memAnswerValue = (obj as RadTextBox);
                        if (memAnswerValue != null)
                            memAnswerValue.Text = HtmlTagHelper.Devalidate(dataRow["QuestionAnswerText"].ToStringDefaultEmpty());
                        break;
                    case "CBO":
                        var cboAnswerValue = (obj as RadComboBox);
                        if (cboAnswerValue != null)
                            cboAnswerValue.SelectedValue = HtmlTagHelper.Devalidate(dataRow["QuestionAnswerSelectionLineID"].ToStringDefaultEmpty());
                        break;
                    case "CHK":
                        var chk = (obj as CheckBox);
                        if (chk != null)
                            chk.Checked = "1".Equals(dataRow["QuestionAnswerText"]);
                        break;
                    case "CTX":
                        var ctxValue = dataRow["QuestionAnswerText"];
                        if (ctxValue != DBNull.Value)
                        {
                            var ctxValues = ctxValue.ToString().Split('|');
                            if (ctxValues.Length > 0 && ctxValues[0] != null)
                            {
                                var ctxChk = (obj as CheckBox);
                                if (ctxChk != null)
                                    ctxChk.Checked = "1".Equals(ctxValues[0]);
                            }
                            if (ctxValues.Length > 1 && ctxValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                                var ctxTxt = (obj as RadTextBox);
                                if (ctxTxt != null)
                                    ctxTxt.Text = HtmlTagHelper.Devalidate(ctxValues[1]);
                            }
                        }
                        break;
                    case "CDO":
                        var cdoValue = dataRow["QuestionAnswerText"];
                        if (cdoValue != DBNull.Value)
                        {
                            var cdoValues = cdoValue.ToString().Split('|');
                            if (cdoValues.Length > 0 && cdoValues[0] != null)
                            {
                                var cdoChk = (obj as CheckBox);
                                if (cdoChk != null)
                                    cdoChk.Checked = "1".Equals(cdoValues[0]);
                            }

                            var cdo2Value = dataRow["QuestionAnswerSelectionLineID"];
                            if (cdo2Value != DBNull.Value)
                            {
                                obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                                var cbo2 = (obj as RadComboBox);
                                if (cbo2 != null)
                                    cbo2.SelectedValue = HtmlTagHelper.Devalidate(dataRow["QuestionAnswerSelectionLineID"].ToStringDefaultEmpty());
                            }
                        }

                        break;
                    case "CTM":
                        var ctmValue = dataRow["QuestionAnswerText"];
                        if (ctmValue != DBNull.Value)
                        {
                            var ctmValues = ctmValue.ToString().Split('|');
                            if (ctmValues.Length > 0 && ctmValues[0] != null)
                            {
                                var ctxChk = (obj as CheckBox);
                                if (ctxChk != null)
                                    ctxChk.Checked = "1".Equals(ctmValues[0]);
                            }
                            if (ctmValues.Length > 1 && ctmValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                                var ctxTxt = (obj as RadTextBox);
                                if (ctxTxt != null)
                                    ctxTxt.Text = HtmlTagHelper.Devalidate(ctmValues[1]);
                            }
                        }
                        break;
                    case "CNM":
                        var cnmValue = dataRow["QuestionAnswerText"];
                        if (cnmValue != DBNull.Value)
                        {
                            var cnmValues = cnmValue.ToString().Split('|');
                            if (cnmValues.Length > 0 && cnmValues[0] != null)
                            {
                                var ctxChk = (obj as CheckBox);
                                if (ctxChk != null)
                                    ctxChk.Checked = "1".Equals(cnmValues[0]);
                            }
                            if (cnmValues.Length > 1 && cnmValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                                var cnmNum = (obj as RadNumericTextBox);
                                if (cnmNum != null)
                                    if (!string.IsNullOrEmpty(cnmValues[1]) && !cnmValues[1].Equals("&nbsp;"))
                                        cnmNum.Value = Convert.ToDouble(cnmValues[1]);
                            }
                        }
                        break;
                    case "CDT":
                        var cdtValue = dataRow["QuestionAnswerText"];
                        if (cdtValue != DBNull.Value)
                        {
                            var cdtValues = cdtValue.ToString().Split('|');
                            if (cdtValues.Length > 0 && cdtValues[0] != null)
                            {
                                var ctxChk = (obj as CheckBox);
                                if (ctxChk != null)
                                    ctxChk.Checked = "1".Equals(cdtValues[0]);
                            }
                            if (cdtValues.Length > 1 && cdtValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                                var cdtDattim = (obj as RadDateTimePicker);
                                if (cdtDattim != null)
                                    if (!string.IsNullOrEmpty(cdtValues[1]) && !cdtValues[1].Equals("&nbsp;"))
                                        cdtDattim.SelectedDate = Convert.ToDateTime(cdtValues[1]);
                            }
                        }
                        break;
                    case "CBT":
                        var cbtValue = dataRow["QuestionAnswerText"];
                        if (cbtValue != DBNull.Value)
                        {
                            var cbtValues = cbtValue.ToString().Split('|');
                            if (cbtValues.Length > 0 && cbtValues[0] != null)
                            {
                                var cbtCbo = (obj as RadComboBox);
                                if (cbtCbo != null)
                                    cbtCbo.SelectedValue = HtmlTagHelper.Devalidate(dataRow["QuestionAnswerSelectionLineID"].ToStringDefaultEmpty());
                            }
                            if (cbtValues.Length > 1 && cbtValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                                var cbtTxt = (obj as RadTextBox);
                                if (cbtTxt != null)
                                    cbtTxt.Text = HtmlTagHelper.Devalidate(cbtValues[1]);
                            }
                        }
                        break;
                    case "CBN":
                        var cbnValue = dataRow["QuestionAnswerText"];
                        if (cbnValue != DBNull.Value)
                        {
                            var cbnValues = cbnValue.ToString().Split('|');
                            if (cbnValues.Length != null && cbnValues[0] != null)
                            {
                                var cbnCbo = (obj as RadComboBox);
                                if (cbnCbo != null)
                                    cbnCbo.SelectedValue = HtmlTagHelper.Devalidate(dataRow["QuestionAnswerSelectionLineID"].ToStringDefaultEmpty());
                            }
                            if (cbnValues.Length > 1 && cbnValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                                var cbnNum = (obj as RadNumericTextBox);
                                if (cbnNum != null)
                                    if (!string.IsNullOrEmpty(cbnValues[1]) && !cbnValues[1].Equals("&nbsp;"))
                                        cbnNum.Value = Convert.ToDouble(cbnValues[1]);
                            }
                        }
                        break;
                    case "CBM":
                        var cbmValue = dataRow["QuestionAnswerText"];
                        if (cbmValue != DBNull.Value)
                        {
                            var cbmValues = cbmValue.ToString().Split('|');
                            if (cbmValues.Length > 0 && cbmValues[0] != null)
                            {
                                var cbtCbo = (obj as RadComboBox);
                                if (cbtCbo != null)
                                    cbtCbo.SelectedValue = HtmlTagHelper.Devalidate(dataRow["QuestionAnswerSelectionLineID"].ToStringDefaultEmpty());
                            }
                            if (cbmValues.Length > 1 && cbmValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                                var cbtTxt = (obj as RadTextBox);
                                if (cbtTxt != null)
                                    cbtTxt.Text = HtmlTagHelper.Devalidate(cbmValues[1]);
                            }
                        }
                        break;
                    case "CB2":
                        var cb2Value = dataRow["QuestionAnswerSelectionLineID"];
                        if (cb2Value != DBNull.Value)
                        {
                            var cb2Values = cb2Value.ToStringDefaultEmpty().Split('|');
                            if (cb2Values.Length > 0 && cb2Values[0] != null)
                            {
                                var cbo1 = (obj as RadComboBox);
                                if (cbo1 != null)
                                    cbo1.SelectedValue = HtmlTagHelper.Devalidate(cb2Values[0]); ;
                            }
                            if (cb2Values.Length > 1 && cb2Values[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                                var cbo2 = (obj as RadComboBox);
                                if (cbo2 != null)
                                    cbo2.SelectedValue = HtmlTagHelper.Devalidate(cb2Values[1]);
                            }
                        }
                        break;
                    case "TTX":
                        var ttxValue = dataRow["QuestionAnswerText"];
                        if (ttxValue != DBNull.Value)
                        {
                            var ttxValues = ttxValue.ToString().Split('|');
                            if (ttxValues.Length > 0 && ttxValues[0] != null)
                            {
                                var txt = (obj as RadTextBox);
                                if (txt != null)
                                    txt.Text = HtmlTagHelper.Devalidate(ttxValues[0]);
                            }
                            if (ttxValues.Length > 1 && ttxValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlCredentialingQuestion, controlID + "_2");
                                var txt2 = (obj as RadTextBox);
                                if (txt2 != null)
                                    txt2.Text = HtmlTagHelper.Devalidate(ttxValues[1]);
                            }
                        }
                        break;
                    case "TBL":
                        var tblValue = dataRow["QuestionAnswerText"];
                        if (tblValue != DBNull.Value)
                        {
                            var tblValues = tblValue.ToString().Split('|');

                            // find table
                            var tbl = (HtmlTable)Helper.FindControlRecursive(
                                            pnlCredentialingQuestion,
                                            controlID);
                            // get row length and col length
                            var rCount = tbl.Rows.Count;
                            string ansText = string.Empty;
                            if (rCount > 0)
                            {
                                var cCount = tbl.Rows[0].Cells.Count;
                                for (var r = 1; r < rCount; r++)
                                {
                                    for (var c = 0; c < cCount; c++)
                                    {
                                        var objCell = Helper.FindControlRecursive(
                                            pnlCredentialingQuestion,
                                            controlID + "_" + r.ToString() + "_" + c.ToString());
                                        var objCellText = (objCell as RadTextBox);
                                        var cellValIndex = c % cCount;
                                        if (cellValIndex + (cCount * (r - 1)) < tblValues.Length)
                                        {
                                            objCellText.Text = HtmlTagHelper.Devalidate(tblValues[cellValIndex + (cCount * (r - 1))]);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                }

            }
        }

        private IEnumerable<QuestionGroup> LoadQuestionGroup(string formID)
        {
            if (ViewState["questionGroup"] != null)
                return ViewState["questionGroup"] as QuestionGroupCollection;
            else
            {
                var query = new QuestionGroupQuery("a");
                var qrQGroupInForm = new QuestionGroupInFormQuery("d");
                query.InnerJoin(qrQGroupInForm).On(query.QuestionGroupID == qrQGroupInForm.QuestionGroupID);
                query.Where(qrQGroupInForm.QuestionFormID == formID);
                query.SelectAll();
                query.OrderBy(qrQGroupInForm.RowIndex.Ascending);

                var coll = new QuestionGroupCollection();
                coll.Load(query);

                ViewState["questionGroup"] = coll;

                return coll;
            }
        }

        private DataTable QuestionDataTable(string formID)
        {
            if (ViewState["questionDataTable"] != null)
                return ViewState["questionDataTable"] as DataTable;
            else
            {
                var questionQuery = new QuestionQuery("a");
                var qrQInGroup = new QuestionInGroupQuery("c");
                var qrQGInForm = new QuestionGroupInFormQuery("d");
                questionQuery.InnerJoin(qrQInGroup).On(questionQuery.QuestionID == qrQInGroup.QuestionID);
                questionQuery.InnerJoin(qrQGInForm).On(qrQInGroup.QuestionGroupID == qrQGInForm.QuestionGroupID);
                questionQuery.OrderBy(qrQGInForm.RowIndex.Ascending, qrQInGroup.QuestionGroupID.Ascending, qrQInGroup.RowIndex.Ascending);
                questionQuery.Where(qrQGInForm.QuestionFormID == formID, questionQuery.IsActive == true);
                questionQuery.Select
                    (
                        //questionQuery,
                        questionQuery.QuestionID, questionQuery.ParentQuestionID, questionQuery.IndexNo,
                        "<ISNULL(c.QuestionLevel,a.QuestionLevel) QuestionLevel>",
                        questionQuery.QuestionText, questionQuery.QuestionShortText,
                        questionQuery.SRAnswerType, questionQuery.AnswerDecimalDigit, questionQuery.AnswerPrefix,
                        questionQuery.AnswerSuffix, questionQuery.IsActive, questionQuery.AnswerWidth,
                        questionQuery.AnswerWidth2, questionQuery.QuestionAnswerSelectionID,
                        questionQuery.QuestionAnswerDefaultSelectionID, questionQuery.QuestionAnswerSelectionID2,
                        questionQuery.QuestionAnswerDefaultSelectionID2, questionQuery.Formula, questionQuery.IsAlwaysPrint,
                        questionQuery.LastUpdateDateTime, questionQuery.LastUpdateByUserID, questionQuery.IsMandatory,
                        "<ISNULL(a.IsEmptyDefault,0) IsEmptyDefault>",
                        questionQuery.ReferenceQuestionID,
                        qrQInGroup.QuestionGroupID,
                        qrQInGroup.RowIndex
                    );

                var dtb = questionQuery.LoadDataTable();
                ViewState["questionDataTable"] = dtb;
                return dtb;
            }
        }

        private void InitializedQuestion(string formID)
        {
            //  Get List Question Group
            IEnumerable<QuestionGroup> questionGroups = LoadQuestionGroup(formID);

            //  Get List Question
            var dtbQuestion = QuestionDataTable(formID);
            //  Generate Question Entry
            pnlCredentialingQuestion.Controls.Clear();
            int rowNo = 0;
            var formulas = new Hashtable(); // Untuk menampung jawaban jenis formula
            foreach (QuestionGroup questionGroup in questionGroups)
            {
                rowNo++;
                var groupTable = new Table { Width = Unit.Percentage(100) };
                // Add Group Label
                var row = new TableRow();
                groupTable.Rows.Add(row);
                var cell = new TableCell
                {
                    HorizontalAlign = HorizontalAlign.Left,
                    Text = string.Format("{0}. {1}", rowNo, questionGroup.QuestionGroupName)
                };
                cell.Font.Bold = true;
                cell.Style["color"] = "white";
                cell.Style["background-color"] = "#758DA6";
                cell.ColumnSpan = 3;
                row.Cells.Add(cell);

                groupTable.Rows.Add(row);
                DataRow[] questionRows = dtbQuestion.Select(string.Format("QuestionGroupID='{0}'", questionGroup.QuestionGroupID), "RowIndex");

                InitializedQuestion(questionRows, groupTable, row, formulas);
                pnlCredentialingQuestion.Controls.Add(groupTable);
            }

            //Generate Formula Script
            if (!IsPostBack)
            {
                var script = new StringBuilder();
                script.AppendLine("<script type='text/javascript' language='javascript'>");
                script.AppendLine("function fillFormulaField(){");

                foreach (DictionaryEntry dictionaryEntry in formulas)
                {

                    string id = dictionaryEntry.Key.ToString();

                    // [200.020]/(([200.030]/100)*([200.030]/100))
                    string formula = dictionaryEntry.Value.ToString();
                    formula = formula.Replace("/.", "d0t").Replace('.', '_').Replace("d0t", ".");
                    formula = formula.Replace("[", "$find('ctl00_ContentPlaceHolder1_quest");
                    formula = formula.Replace("]", "').get_value()");

                    // combobox
                    if (formula.Length > 3)
                    {
                        if (formula.Substring(0, 3) == "CBO")
                        {
                            formula = formula.Substring(3);
                            script.AppendFormat("var dd{0} = $find('ctl00_ContentPlaceHolder1_quest{0}');", id);
                            script.AppendLine();
                            script.AppendFormat("var value{0}={1};", id, formula);
                            script.AppendLine();
                            script.AppendFormat("var item{0} = dd{0}.findItemByValue(value{0});", id);
                            script.AppendLine();
                            script.AppendFormat("item{0}.select();", id);
                            continue;
                        }
                    }

                    script.AppendFormat("var value{0}={1};", id, formula);
                    script.AppendLine();
                    script.AppendFormat("$find('ctl00_ContentPlaceHolder1_quest{0}').set_value(value{0});", id);
                }
                script.AppendLine();
                script.AppendLine("}</script>");
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "formula", script.ToString());
            }
        }

        private void InitializedQuestion(DataRow[] questionRows, Table groupTable, TableRow row, Hashtable formulas)
        {
            foreach (DataRow rowChild in questionRows)
            {
                //-----------------------
                // diperlukan untuk form askep multi hirarki
                var ctlID = QuestionControlID(rowChild["QuestionID"].ToString());
                var ctlAlreadyCreated = Helper.FindControlRecursive(groupTable, ctlID);
                if (ctlAlreadyCreated != null) continue;
                //-----------------------

                if (!rowChild["Formula"].ToString().Equals(string.Empty))
                {
                    formulas.Add(rowChild["QuestionID"].ToString().Replace('.', '_'), rowChild["Formula"].ToString());
                }
                if (!string.IsNullOrEmpty(rowChild["SRAnswerType"].ToString()))
                {
                    row = InitializedRowQuestion(rowChild);
                    groupTable.Rows.Add(row);
                }
                var quest = new QuestionQuery();
                quest.Where(quest.ParentQuestionID == rowChild["QuestionID"], quest.SRAnswerType != string.Empty);
                quest.OrderBy(quest.IndexNo.Ascending);
                var dtbSubQuestion = quest.LoadDataTable();

                if (dtbSubQuestion.Rows.Count > 0)
                {
                    InitializedQuestion(dtbSubQuestion.Select(), groupTable, row, formulas);
                }
            }
        }

        private void InitializedQuestion_DEACTIVATED(string formID)
        {
            //  Get List Question Group
            IEnumerable<QuestionGroup> questionGroups = LoadQuestionGroup(formID);

            //  Get List Question
            var dtbQuestion = QuestionDataTable(formID);
            //  Generate Question Entry
            pnlCredentialingQuestion.Controls.Clear();
            int rowNo = 0;
            var formulas = new Hashtable(); // Untuk menampung jawaban jenis formula
            foreach (QuestionGroup questionGroup in questionGroups)
            {
                rowNo++;
                var groupTable = new Table { Width = Unit.Percentage(100) };
                // Add Group Label
                var row = new TableRow();
                groupTable.Rows.Add(row);
                var cell = new TableCell
                {
                    HorizontalAlign = HorizontalAlign.Left,
                    Text = string.Format("{0}. {1}", rowNo, questionGroup.QuestionGroupName)
                };
                cell.Font.Bold = true;
                cell.Style["color"] = "white";
                cell.Style["background-color"] = "#758DA6";
                cell.ColumnSpan = 3;
                row.Cells.Add(cell);

                groupTable.Rows.Add(row);
                DataRow[] questionRows = dtbQuestion.Select(string.Format("QuestionGroupID='{0}'", questionGroup.QuestionGroupID), "RowIndex");

                //var questionRows = dtbQuestion.AsEnumerable().Where(d => ! string.IsNullOrEmpty(d.Field<string>("ParentQuestionID")));

                foreach (DataRow rowChild in questionRows)
                {
                    //-----------------------
                    // diperlukan untuk form askep multi hirarki
                    var ctlID = QuestionControlID(rowChild["QuestionID"].ToString());
                    var ctlAlreadyCreated = Helper.FindControlRecursive(groupTable, ctlID);
                    if (ctlAlreadyCreated != null) continue;
                    //-----------------------

                    if (!rowChild["Formula"].ToString().Equals(string.Empty))
                    {
                        formulas.Add(rowChild["QuestionID"].ToString().Replace('.', '_'), rowChild["Formula"].ToString());
                    }
                    if (!string.IsNullOrEmpty(rowChild["SRAnswerType"].ToString()))
                    {
                        row = InitializedRowQuestion(rowChild);
                        groupTable.Rows.Add(row);
                    }
                    var quest = new QuestionQuery();
                    quest.Where(quest.ParentQuestionID == rowChild["QuestionID"], quest.SRAnswerType != string.Empty);
                    quest.OrderBy(quest.IndexNo.Ascending);
                    var dtbSubQuestion = quest.LoadDataTable();

                    foreach (DataRow rowSubQuestion in dtbSubQuestion.Rows)
                    {
                        //-----------------------
                        // diperlukan untuk form askep multi hirarki
                        ctlID = QuestionControlID(rowSubQuestion["QuestionID"].ToString());
                        var ctlAlreadyCreated2 = Helper.FindControlRecursive(groupTable, ctlID);
                        if (ctlAlreadyCreated2 != null) continue;
                        //-----------------------

                        if (!rowSubQuestion["Formula"].ToString().Equals(string.Empty))
                        {
                            formulas.Add(rowSubQuestion["QuestionID"].ToString().Replace('.', '_'),
                                         rowSubQuestion["Formula"].ToString());
                        }
                        row = InitializedRowQuestion(rowSubQuestion);
                        groupTable.Rows.Add(row);
                    }
                }
                pnlCredentialingQuestion.Controls.Add(groupTable);
            }

            //Generate Formula Script
            if (!IsPostBack)
            {
                var script = new StringBuilder();
                script.AppendLine("<script type='text/javascript' language='javascript'>");
                script.AppendLine("function fillFormulaField(){");

                foreach (DictionaryEntry dictionaryEntry in formulas)
                {

                    string id = dictionaryEntry.Key.ToString();

                    // [200.020]/(([200.030]/100)*([200.030]/100))
                    string formula = dictionaryEntry.Value.ToString();
                    formula = formula.Replace("/.", "d0t").Replace('.', '_').Replace("d0t", ".");
                    formula = formula.Replace("[", "$find('ctl00_ContentPlaceHolder1_quest");
                    formula = formula.Replace("]", "').get_value()");

                    // combobox
                    if (formula.Length > 3)
                    {
                        if (formula.Substring(0, 3) == "CBO")
                        {
                            formula = formula.Substring(3);
                            script.AppendFormat("var dd{0} = $find('ctl00_ContentPlaceHolder1_quest{0}')", id);
                            script.AppendLine();
                            script.AppendFormat("var value{0}={1};", id, formula);
                            script.AppendLine();
                            script.AppendFormat("var item{0} = dd{0}.findItemByValue(value{0});", id);
                            script.AppendLine();
                            script.AppendFormat("item{0}.select();", id);
                            continue;
                        }
                    }

                    script.AppendFormat("var value{0}={1};", id, formula);
                    script.AppendLine();
                    script.AppendFormat("$find('ctl00_ContentPlaceHolder1_quest{0}').set_value(value{0});", id);
                }
                script.AppendLine();
                script.AppendLine("}</script>");
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "formula", script.ToString());
            }
        }

        private void CreateValidationControl(string ctlToValidate, string unique, DataRow rowQuestion, TableCell tc)
        {
            //if (true)
            if ((bool)rowQuestion["IsMandatory"])
            {
                var rfv = new RequiredFieldValidator();
                rfv.ValidationGroup = "entry";
                rfv.ID = RfvControlID(rowQuestion["QuestionID"].ToString() + unique);
                rfv.ControlToValidate = ctlToValidate;
                rfv.ErrorMessage = string.Format("Field {0} Required!", rowQuestion["QuestionText"]);
                rfv.SetFocusOnError = true;
                //rfv.ForeColor = System.Drawing.Color.Red;
                //rfv.Display = ValidatorDisplay.Dynamic;
                //rfv.IsValid = false;
                //rfv.EnableViewState = true;
                System.Web.UI.WebControls.Image myImg = new System.Web.UI.WebControls.Image();
                myImg.Visible = true;
                myImg.SkinID = "rfvImage";
                rfv.Controls.Add(myImg);

                tc.Controls.Add(rfv);
                //Page.Controls.Add(rfv);
            }
        }

        private TableRow InitializedRowQuestion(DataRow rowQuestion)
        {
            var tblRow = new TableRow();
            string answerType = rowQuestion["SRAnswerType"].ToString();
            string controlID = QuestionControlID(rowQuestion["QuestionID"].ToString());

            //Create 2 Cell
            tblRow.Cells.Add(new TableCell());
            tblRow.Cells.Add(new TableCell());
            //validation
            //tblRow.Cells.Add(new TableCell());
            //tblRow.Cells.Add(new TableCell());

            tblRow.Cells[0].Attributes["class"] = "label";
            //tblRow.Cells[0].VerticalAlign = VerticalAlign.Top;

            var litSep = new Literal();
            switch (answerType)
            {
                case "MSK":
                    tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);
                    var msk = MaskedTextBoxControl(controlID, rowQuestion["AnswerWidth"].ToInt(), rowQuestion["QuestionAnswerSelectionID"].ToStringDefaultEmpty());
                    tblRow.Cells[1].Controls.Add(msk);
                    CreateValidationControl(msk.ID, "", rowQuestion, tblRow.Cells[1]);
                    break;
                case "DAT":
                    tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);

                    var dat = DatePickerControl(controlID, rowQuestion["AnswerWidth"].ToInt(), Convert.ToBoolean(rowQuestion["IsEmptyDefault"]));
                    tblRow.Cells[1].Controls.Add(dat);
                    CreateValidationControl(dat.ID, "", rowQuestion, tblRow.Cells[1]);
                    break;
                case "TIM":
                    tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);

                    var tim = TimePickerControl(controlID, rowQuestion["AnswerWidth"].ToInt(), Convert.ToBoolean(rowQuestion["IsEmptyDefault"]));
                    tblRow.Cells[1].Controls.Add(tim);
                    CreateValidationControl(tim.ID, "", rowQuestion, tblRow.Cells[1]);
                    break;
                case "DTM":
                    tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);

                    var dattim = DateTimePickerControl(controlID, rowQuestion["AnswerWidth"].ToInt(), Convert.ToBoolean(rowQuestion["IsEmptyDefault"]));
                    tblRow.Cells[1].Controls.Add(dattim);
                    CreateValidationControl(dattim.ID, "", rowQuestion, tblRow.Cells[1]);
                    break;
                case "LBL":
                    //AddRowLabel(tblRow, rowQuestion);
                    AddLabel(controlID, tblRow, rowQuestion);
                    break;
                case "NUM":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var num = RadNumericTextBoxControl(controlID, int.Parse(rowQuestion["AnswerDecimalDigit"].ToString()),
                                             rowQuestion["AnswerSuffix"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                                             rowQuestion["Formula"].ToString(), rowQuestion["QuestionAnswerDefaultSelectionID"].ToString());
                    tblRow.Cells[1].Controls.Add(num);
                    CreateValidationControl(num.ID, "", rowQuestion, tblRow.Cells[1]);
                    break;
                case "MEM":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var mem = MemoControl(controlID, rowQuestion["AnswerWidth"].ToInt(), rowQuestion["QuestionAnswerDefaultSelectionID"].ToString());
                    tblRow.Cells[1].Controls.Add(mem);
                    CreateValidationControl(mem.ID, "", rowQuestion, tblRow.Cells[1]);

                    break;
                case "TXT":
                    tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);
                    var txt = TextBoxControl(controlID, rowQuestion["AnswerWidth"].ToInt(), rowQuestion["Formula"].ToString(), rowQuestion["QuestionAnswerDefaultSelectionID"].ToString());
                    //tblRow.Cells[1].Controls.Add(txt);

                    if (!string.IsNullOrEmpty(rowQuestion["AnswerSuffix"].ToString()))
                    {
                        litSep = new Literal();
                        litSep.Text = "&nbsp;&nbsp;" + rowQuestion["AnswerSuffix"].ToString();
                        tblRow.Cells[1].Controls.Add(litSep);

                        var tab = new HtmlTable() { ID = "tab_" + controlID, CellSpacing = 0, CellPadding = 0 };

                        var row = new HtmlTableRow();
                        row.Cells.Add(new HtmlTableCell());
                        row.Cells.Add(new HtmlTableCell());

                        row.Cells[0].Controls.Add(txt);
                        row.Cells[1].Controls.Add(litSep);

                        tab.Rows.Add(row);

                        tblRow.Cells[1].Controls.Add(tab);
                    }
                    else
                        tblRow.Cells[1].Controls.Add(txt);

                    CreateValidationControl(txt.ID, "", rowQuestion, tblRow.Cells[1]);

                    break;
                case "CBO":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var cbo = ComboBoxControl(controlID, rowQuestion["QuestionAnswerSelectionID"].ToString(),
                                    rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                                    rowQuestion["Formula"].ToString());
                    tblRow.Cells[1].Controls.Add(cbo);
                    CreateValidationControl(cbo.ID, "", rowQuestion, tblRow.Cells[1]);
                    break;
                case "CHK":
                    var chk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(chk);
                    break;
                case "TTX":
                    tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);
                    var txt1 = TextBoxControl(controlID, rowQuestion["AnswerWidth"].ToInt(), string.Empty, rowQuestion["QuestionAnswerDefaultSelectionID"].ToString());
                    tblRow.Cells[1].Controls.Add(txt1);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var txt2 = TextBoxControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt(), string.Empty, rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
                    tblRow.Cells[1].Controls.Add(txt2);
                    CreateValidationControl(txt1.ID, "1", rowQuestion, tblRow.Cells[1]);
                    CreateValidationControl(txt2.ID, "2", rowQuestion, tblRow.Cells[1]);
                    break;
                case "CTX":
                    var ctxChk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(ctxChk);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var ctxTxt = TextBoxControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt(), string.Empty, rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
                    tblRow.Cells[1].Controls.Add(ctxTxt);
                    break;
                case "CDO":
                    var cdoChk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(cdoChk);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var cdoCbo = ComboBoxControl(controlID + "_2", rowQuestion["QuestionAnswerSelectionID"].ToString(),
                                    rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                                    string.Empty);
                    tblRow.Cells[1].Controls.Add(cdoCbo);
                    break;
                case "CTM":
                    var ctmChk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(ctmChk);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var ctmTxt = MemoControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt(), rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
                    tblRow.Cells[1].Controls.Add(ctmTxt);
                    break;
                case "CNM":
                    var cnmChk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(cnmChk);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var cnmNum = RadNumericTextBoxControl(controlID + "_2", int.Parse(rowQuestion["AnswerDecimalDigit"].ToString()),
                                             rowQuestion["AnswerSuffix"].ToString(), rowQuestion["AnswerWidth2"].ToInt(),
                                             string.Empty, rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
                    tblRow.Cells[1].Controls.Add(cnmNum);
                    break;
                case "CDT":
                    var cdtChk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(cdtChk);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var cdtDattim = DateTimePickerControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt(), Convert.ToBoolean(rowQuestion["IsEmptyDefault"]));
                    tblRow.Cells[1].Controls.Add(cdtDattim);
                    break;
                case "CB2":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var cbo1 = ComboBoxControl(controlID, rowQuestion["QuestionAnswerSelectionID"].ToString(),
                                    rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                                    string.Empty);
                    tblRow.Cells[1].Controls.Add(cbo1);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var cbo2 = ComboBoxControl(controlID + "_2", rowQuestion["QuestionAnswerSelectionID2"].ToString(),
                                    rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString(), rowQuestion["AnswerWidth2"].ToInt(),
                                    string.Empty);
                    tblRow.Cells[1].Controls.Add(cbo2);
                    CreateValidationControl(cbo1.ID, "1", rowQuestion, tblRow.Cells[1]);
                    CreateValidationControl(cbo2.ID, "2", rowQuestion, tblRow.Cells[1]);
                    break;
                case "CBT":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var cbCbo = ComboBoxControl(controlID, rowQuestion["QuestionAnswerSelectionID"].ToString(),
                                    rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                                    string.Empty);
                    tblRow.Cells[1].Controls.Add(cbCbo);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var cbTxt = TextBoxControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt(), string.Empty, rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
                    tblRow.Cells[1].Controls.Add(cbTxt);
                    CreateValidationControl(cbCbo.ID, "1", rowQuestion, tblRow.Cells[1]);
                    CreateValidationControl(cbTxt.ID, "2", rowQuestion, tblRow.Cells[1]);
                    break;
                case "CBN":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var cbCbn = ComboBoxControl(controlID, rowQuestion["QuestionAnswerSelectionID"].ToString(),
                                    rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                                    string.Empty);
                    tblRow.Cells[1].Controls.Add(cbCbn);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var cbnNum = RadNumericTextBoxControl(controlID + "_2", int.Parse(rowQuestion["AnswerDecimalDigit"].ToString()),
                                             rowQuestion["AnswerSuffix"].ToString(), rowQuestion["AnswerWidth2"].ToInt(),
                                             string.Empty, rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
                    tblRow.Cells[1].Controls.Add(cbnNum);
                    break;
                case "CBM":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var cbCbm = ComboBoxControl(controlID, rowQuestion["QuestionAnswerSelectionID"].ToString(),
                                    rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                                    string.Empty);
                    tblRow.Cells[1].Controls.Add(cbCbm);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var cbTxm = MemoControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt(), rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
                    tblRow.Cells[1].Controls.Add(cbTxm);
                    CreateValidationControl(cbCbm.ID, "1", rowQuestion, tblRow.Cells[1]);
                    CreateValidationControl(cbTxm.ID, "2", rowQuestion, tblRow.Cells[1]);
                    break;
                case "TBL":
                    AddCaptionLabel(tblRow, rowQuestion);
                    tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);

                    var tbl = new HtmlTable() { ID = controlID, CellSpacing = 0, CellPadding = 0 };
                    // ambil answer width sebagai jumlah kolom
                    // ambil answer width 2 sebagai jumalh row
                    var cList = rowQuestion["QuestionAnswerSelectionID"].ToString().Split('|');
                    var defaultAnsList = rowQuestion["QuestionAnswerDefaultSelectionID"].ToString().Split('|');
                    var cCount = cList.Length;
                    var rCount = rowQuestion["AnswerWidth"].ToInt();
                    //create header table
                    for (int iR = 0; iR <= rCount; iR++)
                    { // row
                        var rowTbl = new HtmlTableRow();
                        for (int iC = 0; iC < cCount; iC++)
                        {
                            var rHeader = cList[iC].Split(':');
                            rowTbl.Cells.Add(new HtmlTableCell());
                            if (iR == 0)
                            {
                                // table header
                                rowTbl.Cells[iC].InnerText = rHeader[0]; // print header
                                rowTbl.Cells[iC].Width = rHeader.Length == 1 ? "50" : rHeader[1];
                                rowTbl.Align = "Center";
                            }
                            else
                            {
                                // table content input
                                var txtCell = TextBoxControl(
                                    controlID + "_" + iR.ToString() + "_" + iC.ToString(),
                                    System.Convert.ToInt32((rHeader.Length == 1 ? "50" : rHeader[1])),
                                    string.Empty, string.Empty);
                                // set defaul value if any
                                if ((iC % cCount) + (iR - 1) * cCount < defaultAnsList.Length)
                                {
                                    txtCell.Text = defaultAnsList[(iC % cCount) + (iR - 1) * cCount];
                                }
                                rowTbl.Cells[iC].Controls.Add(txtCell);
                            }
                        }
                        tbl.Rows.Add(rowTbl);
                    }
                    tblRow.Cells[1].Controls.Add(tbl);
                    break;

            }
            return tblRow;
        }
        private HtmlTableCell AddTableCell(string controlID)
        {
            var txt = new RadTextBox() { ID = controlID, Width = Unit.Pixel(35), MaxLength = 2 };
            txt.Style["text-align"] = "center";
            txt.Style["font-weight"] = "bold";

            var cell = new HtmlTableCell();
            cell.Controls.Add(txt);
            return cell;
        }
        private HtmlTableCell AddTableCellStandard(string text)
        {
            var cell = new HtmlTableCell();
            cell.Style["border-bottom-style"] = "solid";
            cell.InnerText = text;
            return cell;
        }
        private void AddLabel(string id, TableRow tblRow, DataRow rowQuestion)
        {
            var lbl = new Label();
            lbl.ID = id;
            lbl.Text = string.Format("<b>{0}{1}</b>", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);

            tblRow.Cells[0].ColumnSpan = 3;
            tblRow.Cells[0].Controls.Add(lbl);
        }
        private void AddRowLabel(TableRow tblRow, DataRow rowQuestion)
        {
            //if (rowQuestion["QuestionLevel"].ToInt() == 0)
            //{
            tblRow.Cells[0].ColumnSpan = 3;
            tblRow.Cells[0].Text = string.Format("<b>{0}{1}</b>", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);
            //}
            //else
            //    tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);
        }
        private void AddCaptionLabel(TableRow tblRow, DataRow rowQuestion)
        {
            if (rowQuestion["QuestionLevel"].ToInt() == 0)
            {
                tblRow.Cells[0].ColumnSpan = 3;
                tblRow.Cells[0].Text = string.Format("<b>{0}{1}</b>", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);
            }
            else
                tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);
        }
        private string Spacer(int questionLevel)
        {
            var retval = string.Empty;
            for (int i = 0; i < questionLevel; i++)
            {
                retval = string.Concat(retval, "&nbsp;&nbsp;");
            }
            return retval;
        }

        private RadDatePicker DatePickerControl(string id, int width, bool isEmptyDefault)
        {
            var obj = new RadDatePicker();
            obj.ID = id;
            if (!isEmptyDefault)
                obj.SelectedDate = DateTime.Now;
            obj.Width = Unit.Pixel(width == 0 ? 100 : width);
            return obj;
        }
        private RadTimePicker TimePickerControl(string id, int width, bool isEmptyDefault)
        {
            var obj = new RadTimePicker();
            obj.ID = id;
            if (!isEmptyDefault)
                obj.SelectedDate = DateTime.Now;
            obj.Width = Unit.Pixel(width == 0 ? 100 : width);
            return obj;
        }
        private RadDateTimePicker DateTimePickerControl(string id, int width, bool isEmptyDefault)
        {
            var obj = new RadDateTimePicker();
            obj.ID = id;
            if (!isEmptyDefault)
                obj.SelectedDate = DateTime.Now;
            obj.Width = Unit.Pixel(width == 0 ? 170 : width);
            //obj.TimeView.TimeFormat = "HH:mm tt";
            return obj;
        }
        private CheckBox CheckBoxControl(string id, string text, int width)
        {
            var chk = new CheckBox();
            chk.ID = id;
            chk.Width = Unit.Pixel(width == 0 ? 300 : width);
            chk.Text = text;
            return chk;
        }
        private RadTextBox TextBoxControl(string id, int width, string formula, string defaultValue)
        {
            var textBox = new RadTextBox();
            textBox.ID = id;
            textBox.Width = Unit.Pixel(width == 0 ? 300 : width);
            if (!string.IsNullOrEmpty(formula))
            {
                textBox.ShowButton = true;
                textBox.ClientEvents.OnValueChanged = "fillFormulaField";
                textBox.ClientEvents.OnButtonClick = "fillFormulaField";
            }
            // default value
            textBox.Text = defaultValue;
            return textBox;
        }
        private RadTextBox MemoControl(string id, int width, string defaultValue)
        {
            var textBox = new RadTextBox();
            textBox.ID = id;
            textBox.Width = Unit.Pixel(width == 0 ? 300 : width);
            textBox.Height = Unit.Pixel(100); //sebelumnya di remark
            textBox.TextMode = InputMode.MultiLine;
            textBox.Text = defaultValue;
            return textBox;
        }
        private RadNumericTextBox RadNumericTextBoxControl(string id, int decimalDigit, string suffix, int width, string formula, string defaultVal)
        {
            var textBox = new RadNumericTextBox();
            textBox.ID = id;
            textBox.Width = Unit.Pixel(width == 0 ? 100 : width);
            textBox.NumberFormat.DecimalDigits = decimalDigit;
            textBox.NumberFormat.PositivePattern = suffix.Equals("&nbsp;")
                                                       ? string.Empty
                                                       : string.Format("n {0}", suffix);
            defaultVal = defaultVal.Trim();
            if (Helper.IsNumeric(defaultVal))
            {
                textBox.Value = System.Convert.ToDouble(defaultVal);
            }
            else
            {
                textBox.Value = 0;
            }
            if (!string.IsNullOrEmpty(formula))
            {
                textBox.ShowButton = true;
                textBox.ClientEvents.OnButtonClick = "fillFormulaField";
            }
            return textBox;
        }
        private RadMaskedTextBox MaskedTextBoxControl(string id, int width, string mask)
        {
            var textBox = new RadMaskedTextBox();
            textBox.ID = id;
            textBox.Mask = mask;
            textBox.Width = Unit.Pixel(width == 0 ? 300 : width);
            return textBox;
        }

        private RadComboBox ComboBoxControl(string id, string selectionID, string defaultSelectionID, int width, string formula)
        {
            var comboBox = new RadComboBox();
            //comboBox.AllowCustomText = true;
            //comboBox.Filter = RadComboBoxFilter.Contains;
            comboBox.ID = id;
            comboBox.Width = Unit.Pixel(width == 0 ? 304 : width);
            var query = new QuestionAnswerSelectionLineQuery();
            query.Where(query.QuestionAnswerSelectionID == selectionID);
            query.Select(query.QuestionAnswerSelectionLineID, query.QuestionAnswerSelectionLineText);
            DataTable dtb = query.LoadDataTable();
            comboBox.Items.Clear();
            comboBox.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                comboBox.Items.Add(new RadComboBoxItem(row["QuestionAnswerSelectionLineText"].ToString(),
                                                       row["QuestionAnswerSelectionLineID"].ToString()));
            }

            if (!string.IsNullOrEmpty(formula))
            {
                //comboBox.ShowButton = true;
                //comboBox.ClientEvents.OnValueChanged = "fillFormulaField";
                //comboBox.ClientEvents.OnButtonClick = "fillFormulaField";
            }

            if (!string.IsNullOrEmpty(defaultSelectionID))
                comboBox.SelectedValue = defaultSelectionID;

            return comboBox;
        }


        private void AddSpacerCell(TableCellCollection cells)
        {
            var cell = new TableCell { Text = "&nbsp;&nbsp;", Wrap = false };
            cells.Add(cell);
        }

        #endregion

        #region CredentialProcessSheet
        protected void grdSheet_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSheet.DataSource = CredentialProcessSheetItems;
        }

        private DataTable CredentialProcessSheetItems
        {
            get
            {
                object obj = this.Session["CredentialProcessSheetItems" + Request.UserHostName];
                if (obj != null)
                    return ((DataTable)(obj));

                var questionnaireId = string.IsNullOrEmpty(cboQuestionnaireID.SelectedValue) ? "-1" : cboQuestionnaireID.SelectedValue;
                var transactionNo = txtTransactionNo.Text;

                CredentialProcessSheetCollection coll = new CredentialProcessSheetCollection();
                DataTable dtb = coll.GetJoin(questionnaireId, transactionNo);

                Session["CredentialProcessSheetItems" + Request.UserHostName] = dtb;
                return dtb;
            }
        }

        protected void grdSheet_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    var dataItem = e.Item as GridDataItem;
                    if (dataItem["IsDetail"].Text == "False")
                    {
                        //dataItem.ForeColor = Color.DarkBlue;
                        dataItem.Font.Bold = true;
                        dataItem.Font.Italic = true;
                    }
                }
            }
            catch
            { }
        }

        protected void grdSheet_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += grdSheet_ItemPreRender;
        }

        private void grdSheet_ItemPreRender(object sender, EventArgs e)
        {
            var dataItem = sender as GridDataItem;
            if (dataItem == null)
                return;

            var srCurrentAbility = dataItem["SRCurrentAbility"].Text;
            var srCurrentAbilitySvr = dataItem["SRCurrentAbilitySupervisor"].Text;
            var srCurrentAbilitySvr2 = dataItem["SRCurrentAbilitySupervisor2"].Text;
            var srReview = dataItem["SRReview"].Text;
            var srRecomendation = dataItem["SRRecomendation"].Text;
            var srConclusion = dataItem["SRConclusion"].Text;

            if (!string.IsNullOrEmpty(srCurrentAbility) && srCurrentAbility != "&nbsp;" && srCurrentAbility != "-1")
            {
                var result = (dataItem["QuestionnaireItemID"].FindControl("cboSRCurrentAbility") as RadComboBox);

                DataView dv = PopulateCurrentAbility().DefaultView;
                dv.RowFilter = "ItemID = '" + srCurrentAbility + "'";

                result.DataSource = dv.ToTable();
                result.DataValueField = "ItemID";
                result.DataTextField = "Note";
                result.DataBind();
                ComboBox.SelectedValue(result, srCurrentAbility);
            }

            if (!string.IsNullOrEmpty(srCurrentAbilitySvr) && srCurrentAbilitySvr != "&nbsp;" && srCurrentAbilitySvr != "-1")
            {
                var result = (dataItem["QuestionnaireItemID"].FindControl("cboSRCurrentAbilitySupervisor") as RadComboBox);

                DataView dv = PopulateCurrentAbility().DefaultView;
                dv.RowFilter = "ItemID = '" + srCurrentAbilitySvr + "'";

                result.DataSource = dv.ToTable();
                result.DataValueField = "ItemID";
                result.DataTextField = "Note";
                result.DataBind();
                ComboBox.SelectedValue(result, srCurrentAbilitySvr);
            }

            if (!string.IsNullOrEmpty(srCurrentAbilitySvr2) && srCurrentAbilitySvr2 != "&nbsp;" && srCurrentAbilitySvr2 != "-1")
            {
                var result = (dataItem["QuestionnaireItemID"].FindControl("cboSRCurrentAbilitySupervisor2") as RadComboBox);

                DataView dv = PopulateCurrentAbility().DefaultView;
                dv.RowFilter = "ItemID = '" + srCurrentAbilitySvr2 + "'";

                result.DataSource = dv.ToTable();
                result.DataValueField = "ItemID";
                result.DataTextField = "Note";
                result.DataBind();
                ComboBox.SelectedValue(result, srCurrentAbilitySvr2);
            }

            if (!string.IsNullOrEmpty(srReview) && srReview != "&nbsp;" && srReview != "-1")
            {
                var result = (dataItem["QuestionnaireItemID"].FindControl("cboSRReview") as RadComboBox);

                DataView dv = PopulateReview().DefaultView;
                dv.RowFilter = "ItemID = '" + srReview + "'";

                result.DataSource = dv.ToTable();
                result.DataValueField = "ItemID";
                result.DataTextField = "Note";
                result.DataBind();
                ComboBox.SelectedValue(result, srReview);
            }

            if (!string.IsNullOrEmpty(srRecomendation) && srRecomendation != "&nbsp;" && srRecomendation != "-1")
            {
                var result = (dataItem["QuestionnaireItemID"].FindControl("cboSRRecomendation") as RadComboBox);

                DataView dv = PopulateRecomendation().DefaultView;
                dv.RowFilter = "ItemID = '" + srRecomendation + "'";

                result.DataSource = dv.ToTable();
                result.DataValueField = "ItemID";
                result.DataTextField = "Note";
                result.DataBind();
                ComboBox.SelectedValue(result, srRecomendation);
            }

            if (!string.IsNullOrEmpty(srConclusion) && srConclusion != "&nbsp;" && srConclusion != "-1")
            {
                var result = (dataItem["QuestionnaireItemID"].FindControl("cboSRConclusion") as RadComboBox);

                DataView dv = PopulateConclusion().DefaultView;
                dv.RowFilter = "ItemID = '" + srConclusion + "'";

                result.DataSource = dv.ToTable();
                result.DataValueField = "ItemID";
                result.DataTextField = "ItemName";
                result.DataBind();
                ComboBox.SelectedValue(result, srConclusion);
            }
        }

        private DataTable PopulateCurrentAbility()
        {
            if (ViewState["currentability"] != null)
                return ViewState["currentability"] as DataTable;

            var query = new AppStandardReferenceItemQuery("a");
            query.Where(
                query.StandardReferenceID == "CredentialResultCurrentAbility", query.ReferenceID == cboSRProfessionGroup.SelectedValue);
            query.Select(query.ItemID, query.Note);

            ViewState["currentability"] = query.LoadDataTable();
            return ViewState["currentability"] as DataTable;
        }

        private DataTable PopulateReview()
        {
            if (ViewState["review"] != null)
                return ViewState["review"] as DataTable;

            var query = new AppStandardReferenceItemQuery("a");
            query.Where(
                query.StandardReferenceID == "CredentialResultReview", query.ReferenceID == cboSRProfessionGroup.SelectedValue);
            query.Select(query.ItemID, query.Note);

            ViewState["review"] = query.LoadDataTable();
            return ViewState["review"] as DataTable;
        }

        private DataTable PopulateRecomendation()
        {
            if (ViewState["recomendation"] != null)
                return ViewState["recomendation"] as DataTable;

            var query = new AppStandardReferenceItemQuery("a");
            query.Where(
                query.StandardReferenceID == "CredentialResultRecomendation", query.ReferenceID == cboSRProfessionGroup.SelectedValue);
            query.Select(query.ItemID, query.Note);

            ViewState["recomendation"] = query.LoadDataTable();
            return ViewState["recomendation"] as DataTable;
        }

        private DataTable PopulateConclusion()
        {
            if (ViewState["conclusion"] != null)
                return ViewState["conclusion"] as DataTable;

            var query = new AppStandardReferenceItemQuery("a");
            query.Where(
                query.StandardReferenceID == "CredentialResultConclusion", query.ReferenceID == cboSRProfessionGroup.SelectedValue);
            query.Select(query.ItemID, query.ItemName, query.Note);

            ViewState["conclusion"] = query.LoadDataTable();
            return ViewState["conclusion"] as DataTable;
        }

        private void RefreshGridSheetItems()
        {
            Session["CredentialProcessSheetItems" + Request.UserHostName] = null;
            grdSheet.Rebind();
        }

        protected string GetQuestionName(object srCredentialQuestionLevel, object questionName)
        {
            if (srCredentialQuestionLevel.ToString().Equals(string.Empty) || srCredentialQuestionLevel.ToInt().Equals(0))
                return questionName.ToString();

            if (srCredentialQuestionLevel.ToInt().Equals(1))
                return "&nbsp;&nbsp;" + questionName.ToString();

            if (srCredentialQuestionLevel.ToInt().Equals(2))
                return "&nbsp;&nbsp;&nbsp;&nbsp;" + questionName.ToString();

            if (srCredentialQuestionLevel.ToInt().Equals(3))
                return "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + questionName.ToString();

            if (srCredentialQuestionLevel.ToInt().Equals(4))
                return "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + questionName.ToString();

            if (srCredentialQuestionLevel.ToInt().Equals(5))
                return "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + questionName.ToString();

            return "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + questionName.ToString();
        }

        protected void SRCurrentAbility_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["Note"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRCurrentAbility_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new AppStandardReferenceItemQuery("a");
            query.Where(
                query.StandardReferenceID == "CredentialResultCurrentAbility",
                query.ReferenceID == cboSRProfessionGroup.SelectedValue);
            query.Select(query.ItemID, query.Note);
            var combo = o as RadComboBox;

            combo.DataSource = query.LoadDataTable();
            combo.DataBind();
        }

        protected void cboSRReview_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["Note"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRReview_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new AppStandardReferenceItemQuery("a");
            query.Where(
                query.StandardReferenceID == "CredentialResultReview",
                query.ReferenceID == cboSRProfessionGroup.SelectedValue);
            query.Select(query.ItemID, query.Note);
            var combo = o as RadComboBox;

            combo.DataSource = query.LoadDataTable();
            combo.DataBind();
        }

        protected void cboSRRecomendation_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["Note"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRRecomendation_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string noteContain = string.Format("%{0}%", cboSRProfessionGroup.SelectedValue);
            var query = new AppStandardReferenceItemQuery("a");
            query.Where(
                query.StandardReferenceID == "CredentialResultRecomendation",
                query.ReferenceID == cboSRProfessionGroup.SelectedValue);
            query.Select(query.ItemID, query.ItemName, query.Note);
            var combo = o as RadComboBox;

            combo.DataSource = query.LoadDataTable();
            combo.DataBind();
        }

        protected void cboSRConclusion_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRConclusion_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new AppStandardReferenceItemQuery("a");
            query.Where(
                query.StandardReferenceID == "CredentialResultConclusion",
                query.ReferenceID == cboSRProfessionGroup.SelectedValue);
            query.Select(query.ItemID, query.ItemName, query.Note);
            var combo = o as RadComboBox;

            combo.DataSource = query.LoadDataTable();
            combo.DataBind();
        }
        #endregion

        #region Record Detail Method Function CredentialCompetencyAssessmentEvaluator

        private CredentialCompetencyAssessmentEvaluatorCollection CredentialCompetencyAssessmentEvaluators
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCredentialCompetencyAssessmentEvaluator" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((CredentialCompetencyAssessmentEvaluatorCollection)(obj));
                    }
                }

                var coll = new CredentialCompetencyAssessmentEvaluatorCollection();
                var query = new CredentialCompetencyAssessmentEvaluatorQuery("a");
                var pinfo = new PersonalInfoQuery("b");
                var role = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                    query,
                    role.ItemName.As("refToAppStdField_CompetencyAssessmentEvalRole"),
                    pinfo.EmployeeName.As("refToPersonalInfo_EmployeeName")
                    );
                query.InnerJoin(pinfo).On(pinfo.PersonID == query.EvaluatorID);
                query.InnerJoin(role).On
                   (
                       role.StandardReferenceID == AppEnum.StandardReference.CompetencyAssessmentEvalRole.ToString() &
                       role.ItemID == query.SRCompetencyAssessmentEvalRole
                   );

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.SRCompetencyAssessmentEvalRole.Ascending);

                coll.Load(query);

                Session["collCredentialCompetencyAssessmentEvaluator" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collCredentialCompetencyAssessmentEvaluator" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemEval(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read) && (FormType == "caa" && Role == "eva");
            grdCompetencyAssessmentEvaluator.Columns[0].Visible = isVisible;
            grdCompetencyAssessmentEvaluator.Columns[grdCompetencyAssessmentEvaluator.Columns.Count - 1].Visible = isVisible;

            grdCompetencyAssessmentEvaluator.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdCompetencyAssessmentEvaluator.Rebind();
        }

        private void PopulateEvalGrid()
        {
            //Display Data Detail
            CredentialCompetencyAssessmentEvaluators = null; //Reset Record Detail
            grdCompetencyAssessmentEvaluator.DataSource = CredentialCompetencyAssessmentEvaluators; //Requery
            grdCompetencyAssessmentEvaluator.MasterTableView.IsItemInserted = false;
            grdCompetencyAssessmentEvaluator.MasterTableView.ClearEditItems();
            grdCompetencyAssessmentEvaluator.DataBind();
        }

        protected void grdCompetencyAssessmentEvaluator_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdCompetencyAssessmentEvaluator.DataSource = CredentialCompetencyAssessmentEvaluators;
        }

        protected void grdCompetencyAssessmentEvaluator_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            CredentialCompetencyAssessmentEvaluator entity = FindEvalItem(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                                                  [CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.EvaluatorID].ToInt());

            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdCompetencyAssessmentEvaluator_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            Int32 id = item.OwnerTableView.DataKeyValues[item.ItemIndex][CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.EvaluatorID].ToInt();
            CredentialCompetencyAssessmentEvaluator entity = FindEvalItem(id);

            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdCompetencyAssessmentEvaluator_InsertCommand(object source, GridCommandEventArgs e)
        {
            CredentialCompetencyAssessmentEvaluator entity = CredentialCompetencyAssessmentEvaluators.AddNew();

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdCompetencyAssessmentEvaluator.Rebind();
        }

        private CredentialCompetencyAssessmentEvaluator FindEvalItem(Int32 id)
        {
            CredentialCompetencyAssessmentEvaluatorCollection coll = CredentialCompetencyAssessmentEvaluators;
            CredentialCompetencyAssessmentEvaluator retEntity = null;
            foreach (CredentialCompetencyAssessmentEvaluator rec in coll)
            {
                if (rec.EvaluatorID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(CredentialCompetencyAssessmentEvaluator entity, GridCommandEventArgs e)
        {
            var userControl = (CredentialingCompetencyAssessmentEvaluatorItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = txtTransactionNo.Text;
                entity.EvaluatorID = userControl.EvaluatorID;
                entity.EvaluatorName = userControl.EvaluatorName;
                entity.SRCompetencyAssessmentEvalRole = userControl.SRCompetencyAssessmentEvalRole;
                entity.CompetencyAssessmentEvalRoleName = userControl.CompetencyAssessmentEvalRoleName;
            }
        }

        #endregion

        #region Record Detail Method Function CredentialProcessTeam

        private CredentialProcessTeamCollection CredentialProcessTeams
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCredentialProcessTeam" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((CredentialProcessTeamCollection)(obj));
                    }
                }

                var coll = new CredentialProcessTeamCollection();
                var query = new CredentialProcessTeamQuery("a");
                var pinfo = new PersonalInfoQuery("b");
                var position = new PositionQuery("c");
                var status = new AppStandardReferenceItemQuery("d");

                query.Select
                    (
                    query,
                    pinfo.EmployeeName.As("refToPersonalInfo_EmployeeName"),
                    position.PositionName.As("refToPosition_PositionName"),
                    status.ItemName.As("refToAppStdRefItem_CredentialingTeamPosition")
                    );
                query.InnerJoin(pinfo).On(pinfo.PersonID == query.PersonID);
                query.LeftJoin(position).On(position.PositionID == query.PositionID);
                query.LeftJoin(status).On
                   (
                       status.StandardReferenceID == AppEnum.StandardReference.CredentialingTeamPosition.ToString() &
                       status.ItemID == query.SRCredentialingTeamPosition
                   );

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.SRCredentialingTeamPosition.Ascending);

                coll.Load(query);

                Session["collCredentialProcessTeam" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collCredentialProcessTeam" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemTeam(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read) && (FormType == "rec");
            grdCredentialTeam.Columns[0].Visible = isVisible;
            grdCredentialTeam.Columns[grdCredentialTeam.Columns.Count - 1].Visible = isVisible;

            grdCredentialTeam.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdCredentialTeam.Rebind();
        }

        private void PopulateTeamGrid()
        {
            //Display Data Detail
            CredentialProcessTeams = null; //Reset Record Detail
            grdCredentialTeam.DataSource = CredentialProcessTeams; //Requery
            grdCredentialTeam.MasterTableView.IsItemInserted = false;
            grdCredentialTeam.MasterTableView.ClearEditItems();
            grdCredentialTeam.DataBind();
        }

        protected void grdCredentialTeam_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdCredentialTeam.DataSource = CredentialProcessTeams;
        }

        protected void grdCredentialTeam_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            CredentialProcessTeam entity = FindTeamItem(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                                                  [CredentialProcessTeamMetadata.ColumnNames.PersonID].ToInt());

            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdCredentialTeam_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            Int32 id = item.OwnerTableView.DataKeyValues[item.ItemIndex][CredentialProcessTeamMetadata.ColumnNames.PersonID].ToInt();
            CredentialProcessTeam entity = FindTeamItem(id);

            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdCredentialTeam_InsertCommand(object source, GridCommandEventArgs e)
        {
            CredentialProcessTeam entity = CredentialProcessTeams.AddNew();

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdCredentialTeam.Rebind();
        }

        private CredentialProcessTeam FindTeamItem(Int32 id)
        {
            CredentialProcessTeamCollection coll = CredentialProcessTeams;
            CredentialProcessTeam retEntity = null;
            foreach (CredentialProcessTeam rec in coll)
            {
                if (rec.PersonID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(CredentialProcessTeam entity, GridCommandEventArgs e)
        {
            var userControl = (CredentialingTeam)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = txtTransactionNo.Text;
                entity.PersonID = userControl.PersonID;
                entity.TeamMemberName = userControl.TeamMemberName;
                entity.PositionID = userControl.PositionID;
                entity.PositionName = userControl.PositionName;
                entity.SRCredentialingTeamPosition = userControl.SRCredentialingTeam;
                entity.CredentialingTeamPositionName = userControl.CredentialingTeamName;
                entity.AreasOfExpertise = userControl.AreasOfExpertise;
            }
        }

        #endregion

        #region Record Detail Method Function CredentialProcessWorkExperience

        private CredentialProcessWorkExperienceCollection CredentialProcessWorkExperiences
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCredentialProcessWorkExperience" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((CredentialProcessWorkExperienceCollection)(obj));
                    }
                }

                var coll = new CredentialProcessWorkExperienceCollection();
                var query = new CredentialProcessWorkExperienceQuery("a");

                query.Select(query);

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.WorkExperienceNo.Ascending);

                coll.Load(query);

                Session["collCredentialProcessWorkExperience" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collCredentialProcessWorkExperience" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemWorkExperience(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read) && FormType == "caa" && Role == "usr";
            grdWorkExperience.Columns[0].Visible = isVisible;
            grdWorkExperience.Columns[grdWorkExperience.Columns.Count - 1].Visible = isVisible;

            grdWorkExperience.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdWorkExperience.Rebind();
        }

        private void PopulateWorkExperienceGrid()
        {
            //Display Data Detail
            CredentialProcessWorkExperiences = null; //Reset Record Detail
            grdWorkExperience.DataSource = CredentialProcessWorkExperiences; //Requery
            grdWorkExperience.MasterTableView.IsItemInserted = false;
            grdWorkExperience.MasterTableView.ClearEditItems();
            grdWorkExperience.DataBind();
        }

        protected void grdWorkExperience_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdWorkExperience.DataSource = CredentialProcessWorkExperiences;
        }

        protected void grdWorkExperience_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            CredentialProcessWorkExperience entity = FindWorkExperienceItem(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                                                  [CredentialProcessWorkExperienceMetadata.ColumnNames.WorkExperienceNo].ToString());

            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdWorkExperience_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            string id = item.OwnerTableView.DataKeyValues[item.ItemIndex][CredentialProcessWorkExperienceMetadata.ColumnNames.WorkExperienceNo].ToString();
            CredentialProcessWorkExperience entity = FindWorkExperienceItem(id);

            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdWorkExperience_InsertCommand(object source, GridCommandEventArgs e)
        {
            CredentialProcessWorkExperience entity = CredentialProcessWorkExperiences.AddNew();

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdWorkExperience.Rebind();
        }

        private CredentialProcessWorkExperience FindWorkExperienceItem(string id)
        {
            CredentialProcessWorkExperienceCollection coll = CredentialProcessWorkExperiences;
            CredentialProcessWorkExperience retEntity = null;
            foreach (CredentialProcessWorkExperience rec in coll)
            {
                if (rec.WorkExperienceNo.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(CredentialProcessWorkExperience entity, GridCommandEventArgs e)
        {
            var userControl = (CredentialingWorkExperienceItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = txtTransactionNo.Text;
                entity.WorkExperienceNo = userControl.WorkExperienceNo;
                entity.InstitutionName = userControl.InstitutionName;
                entity.StartPeriod = userControl.StartPeriod;
                entity.EndPeriod = userControl.EndPeriod;
                entity.PositionName = userControl.PositionName;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        #endregion

        #region Record Detail Method Function CredentialProcessCpd

        private CredentialProcessCpdCollection CredentialProcessCpds
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCredentialProcessCpd" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((CredentialProcessCpdCollection)(obj));
                    }
                }

                var coll = new CredentialProcessCpdCollection();
                var query = new CredentialProcessCpdQuery("a");

                query.Select(query);

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.CpdNo.Ascending);

                coll.Load(query);

                Session["collCredentialProcessCpd" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collCredentialProcessCpd" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemCpd(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read) && FormType == "apl";
            grdCPD.Columns[0].Visible = isVisible;
            grdCPD.Columns[grdCPD.Columns.Count - 1].Visible = isVisible;

            grdCPD.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdCPD.Rebind();
        }

        private void PopulateCpdGrid()
        {
            //Display Data Detail
            CredentialProcessCpds = null; //Reset Record Detail
            grdCPD.DataSource = CredentialProcessCpds; //Requery
            grdCPD.MasterTableView.IsItemInserted = false;
            grdCPD.MasterTableView.ClearEditItems();
            grdCPD.DataBind();
        }

        protected void grdCPD_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdCPD.DataSource = CredentialProcessCpds;
        }

        protected void grdCPD_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            CredentialProcessCpd entity = FindCpdItem(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                                                  [CredentialProcessCpdMetadata.ColumnNames.CpdNo].ToString());

            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdCPD_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            string id = item.OwnerTableView.DataKeyValues[item.ItemIndex][CredentialProcessWorkExperienceMetadata.ColumnNames.WorkExperienceNo].ToString();
            CredentialProcessCpd entity = FindCpdItem(id);

            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdCPD_InsertCommand(object source, GridCommandEventArgs e)
        {
            CredentialProcessCpd entity = CredentialProcessCpds.AddNew();

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdCPD.Rebind();
        }

        private CredentialProcessCpd FindCpdItem(string id)
        {
            CredentialProcessCpdCollection coll = CredentialProcessCpds;
            CredentialProcessCpd retEntity = null;
            foreach (CredentialProcessCpd rec in coll)
            {
                if (rec.CpdNo.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(CredentialProcessCpd entity, GridCommandEventArgs e)
        {
            var userControl = (CredentialingCpdItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = txtTransactionNo.Text;
                entity.CpdNo = userControl.CpdNo;
                entity.CpdName = userControl.CpdName;
                entity.InstitutionName = userControl.InstitutionName;
                entity.TimeAndHours = userControl.TimeAndHours;
                entity.Skp = userControl.Skp;
                entity.AchievedCompetence = userControl.AchievedCompetence;
                entity.PhysicalEvidence = userControl.PhysicalEvidence;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        #endregion

        #region Record Detail Method Function CredentialProcessRecomendationResult

        private void PopulateRecommendationResultGrid()
        {
            //Display Data Detail
            grdRecommendationResult.DataSource = GetRecommendationResults();
            grdRecommendationResult.DataBind();
        }

        protected void grdRecommendationResult_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRecommendationResult.DataSource = GetRecommendationResults();
        }

        private DataTable GetRecommendationResults()
        {
            var query = new CredentialProcessRecommendationResultQuery("a");
            var qrRef = new AppStandardReferenceItemQuery("b");
            if (this.DataModeCurrent == AppEnum.DataMode.Read || FormType != "rec")
            {
                query.InnerJoin(qrRef).On(query.SRRecommendationResult == qrRef.ItemID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);
            }
            else
            {
                query.RightJoin(qrRef).On(query.SRRecommendationResult == qrRef.ItemID & query.TransactionNo == txtTransactionNo.Text);
            }
            query.Where(qrRef.StandardReferenceID == "CredentialRecommendationResult", qrRef.IsActive == true);
            query.OrderBy(qrRef.ItemID.Ascending);
            query.Select
                (
                    "<CONVERT(BIT,CASE WHEN COALESCE(a.SRRecommendationResult,'')='' THEN 0 ELSE 1 END) as IsSelect>",
                    qrRef.ItemID.As("SRRecommendationResult"),
                    qrRef.ItemName.As("RecommendationResultName"),
                    "<ISNULL(a.Notes, '') AS Notes>"
                );
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        private void RefreshCommandItemRecommendationResult(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read) && FormType == "rec";
            grdRecommendationResult.Columns[0].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdRecommendationResult.Rebind();
        }
        #endregion

        protected void txtValidFrom_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            txtValidTo.SelectedDate = txtValidFrom.SelectedDate.Value.AddYears(3).AddDays(-1);

            var plQ = new PersonalLicenceQuery();
            plQ.Where(plQ.PersonID == cboPersonID.SelectedValue.ToInt(), plQ.SRLicenceType == AppSession.Parameter.PersonalLicenseTypeSTR,
                plQ.ValidTo > DateTime.Now);
            plQ.OrderBy(plQ.ValidTo.Ascending);
            plQ.es.Top = 1;
            DataTable dtb = plQ.LoadDataTable();

            if (dtb.Rows.Count > 0)
            {
                var pl = new PersonalLicence();
                pl.Load(plQ);
                if (txtValidTo.SelectedDate > pl.ValidTo)
                    txtValidTo.SelectedDate = pl.ValidTo;
            }
        }
    }
}
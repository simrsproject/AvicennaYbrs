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

namespace Temiang.Avicenna.Module.HR.Credential.Process.Medic
{
    public partial class CredentialingDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.CredentialingNo);
            return _autoNumber.LastCompleteNumber;
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

        private string personId
        {
            get { return string.IsNullOrEmpty(Request.QueryString["pid"]) ? string.Empty : Request.QueryString["pid"]; }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            switch (FormType)
            {
                case "mc0":
                    ProgramID = Role == "usr" ? AppConstant.Program.MedicCredentialSelfAssessment : AppConstant.Program.MedicCredentialSelfAssessmentAdmin;
                    break;
                case "mc1":
                    ProgramID = AppConstant.Program.MedicCredentialApprovalBySupervisor;
                    break;
                case "asc":
                    ProgramID = AppConstant.Program.MedicCredentialApprovalBySubCommittee;
                    break;
                case "amc":
                    ProgramID = AppConstant.Program.MedicCredentialApprovalByMedicalCommittee;
                    break;
                case "mc2":
                    ProgramID = AppConstant.Program.MedicCredentialApprovalByDirector;
                    break;
                case "cal":
                    ProgramID = AppConstant.Program.ClinicalAssignmentLetter;
                    break;
                case "cal2":
                    ProgramID = AppConstant.Program.ClinicalAssignmentLetter_Komed;
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
                    UrlPageList = "../../Process/ClinicalAssignmentLetter/ClinicalAssignmentLetterList.aspx?pg=01";
                else
                    UrlPageList = FormType == "mc0" ? "CredentialingList.aspx?type=" + FormType + "&role=" + Role : (FormType == "cal" ? "../../Process/CredentialingAssessmentList.aspx?type=" + FormType + "&role=" + Role : "CredentialingApprovalList.aspx?type=" + FormType + "&role=" + Role);

                UrlPageSearch = FormType == "mc0" ? "CredentialingSearch.aspx?role=" + Role : "##";
            }
            else
            {
                UrlPageList = "../../../EmployeeHR/Logbook/LogbookDetail.aspx?id=" + personId + "&type=" + FormType + "&role=" + Role;
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

                rfvCredentialApplicationDate.Visible = (FormType == "mc0");
                rfvSRCredentialingStatus.Visible = (FormType == "mc0");

                trVerifiedDateTime.Visible = (FormType != "mc0");

                trCredentialingDate.Visible = (FormType != "mc0" && FormType != "mc1");
                rfvCredentialingDate.Visible = (FormType == "asc");

                trRecommendationLetterDate.Visible = (FormType == "amc" || FormType == "mc2" || FormType == "cal");
                rfvRecommendationLetterDate.Visible = (FormType == "amc");

                trVerifiedDateTime2.Visible = (FormType == "mc2" || FormType == "cal" || FormType == "cal2");

                rfvClinicalAssignmentLetterDate.Visible = (FormType == "cal" || FormType == "cal2");
                rfvDecreeNo.Visible = (FormType == "cal" || FormType == "cal2");
                rfvValidFrom.Visible = (FormType == "cal" || FormType == "cal2");
                rfvValidTo.Visible = (FormType == "cal" || FormType == "cal2");

                switch (FormType)
                {
                    case "mc0":
                        if (Role == "usr")
                        {
                            tabStrip.Tabs[2].Visible = false;
                            tabStrip.Tabs[3].Visible = false;
                        }
                        break;
                    case "mc1":
                        tabStrip.Tabs[2].Visible = false;
                        tabStrip.Tabs[3].Visible = false;
                        tabStrip.SelectedIndex = 1;
                        multiPage.SelectedIndex = 1;
                        break;
                    case "asc":
                    case "amc":
                    case "mc2":
                        tabStrip.Tabs[3].Visible = false;
                        tabStrip.SelectedIndex = 1;
                        multiPage.SelectedIndex = 1;
                        break;
                    case "cal":
                    case "cal2":
                        tabStrip.SelectedIndex = 3;
                        multiPage.SelectedIndex = 3;
                        break;
                }

                if (!(FormType == "mc0"))
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
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = (FormType == "mc0");
            ToolBarMenuAdd.Enabled = (FormType == "mc0" && Role == "usr");
            ToolBarMenuMoveNext.Enabled = false;
            ToolBarMenuMovePrev.Enabled = false;

            cboPersonID.Enabled = false;

            if (!(FormType == "mc0" && Role == "usr"))
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

            if (FormType == "mc0" & Role == "usr")
            {
                cboSRProfessionGroup.SelectedValue = "01";

                if (AppSession.UserLogin.PersonID != -1)
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


                var licenses = new CredentialProcessLicenseCollection();
                licenses.Query.Where(licenses.Query.TransactionNo == txtTransactionNo.Text);
                licenses.LoadAll();
                licenses.MarkAllAsDeleted();

                var sheets = new CredentialProcessSheetCollection();
                sheets.Query.Where(sheets.Query.TransactionNo == txtTransactionNo.Text);
                sheets.LoadAll();
                sheets.MarkAllAsDeleted();

                var recs = new CredentialProcessRecommendationResultCollection();
                recs.Query.Where(recs.Query.TransactionNo == txtTransactionNo.Text);
                recs.LoadAll();
                recs.MarkAllAsDeleted();

                entity.MarkAsDeleted();

                using (var trans = new esTransactionScope())
                {
                    licenses.Save();
                    sheets.Save();
                    recs.Save();

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
            var entity = new CredentialProcess();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            if (cboSRCredentialingStatus.SelectedValue == "02" & string.IsNullOrEmpty(cboSRRecredentialReason.SelectedValue))
            {
                args.MessageText = "Recredential Reason required.";
                args.IsCancel = true;
                return;
            }

            entity = new CredentialProcess();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (cboSRCredentialingStatus.SelectedValue == "02" & string.IsNullOrEmpty(cboSRRecredentialReason.SelectedValue))
            {
                args.MessageText = "Recredential Reason required.";
                args.IsCancel = true;
                return;
            }

            var entity = new CredentialProcess();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
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

            if (FormType == "mc0")
            {
                var cps = new CredentialProcessSheetQuery("a");
                var cqi = new CredentialQuestionnaireItemQuery("b");
                cps.InnerJoin(cqi).On(cqi.QuestionnaireItemID == cps.QuestionnaireItemID && cqi.IsDetail == true);
                cps.Where(cps.TransactionNo == entity.TransactionNo, cps.SRCurrentAbility == "");
                DataTable dtb = cps.LoadDataTable();
                if (dtb.Rows.Count > 0)
                {
                    args.MessageText = "Incomplete Self Assessment.";
                    args.IsCancel = true;
                    return;
                }
            }
            else if (FormType == "mc1")
            {
                if (entity.IsApproved == null || entity.IsApproved == false)
                {
                    args.MessageText = "Self Assessment Approved Status required.";
                    args.IsCancel = true;
                    return;
                }
            }
            else if (FormType == "asc") // sub kom
            {
                if (entity.IsVerified == null || entity.IsVerified == false)
                {
                    args.MessageText = "Supervisor Assessment Approved Status required.";
                    args.IsCancel = true;
                    return;
                }
            }
            else if (FormType == "amc") // komed
            {
                if (entity.IsCredentialing == null || entity.IsCredentialing == false)
                {
                    args.MessageText = "Approved By Credentialing Sub Committee Status required.";
                    args.IsCancel = true;
                    return;
                }
            }
            else if (FormType == "mc2") // director
            {
                if (entity.IsRecommendationLetter == null || entity.IsRecommendationLetter == false)
                {
                    args.MessageText = "Approved By Medical Committee Status required.";
                    args.IsCancel = true;
                    return;
                }
            }
            else if (FormType == "cal")
            {
                if (entity.IsVerified2 == null || entity.IsVerified2 == false)
                {
                    args.MessageText = "Approved By Director Status required.";
                    args.IsCancel = true;
                    return;
                }

                if (string.IsNullOrEmpty(txtDecreeNo.Text))
                {
                    args.MessageText = "Decree No required.";
                    args.IsCancel = true;
                    return;
                }

                if (txtValidFrom.IsEmpty)
                {
                    args.MessageText = "Valid Decree No From required.";
                    args.IsCancel = true;
                    return;
                }

                if (txtValidTo.IsEmpty)
                {
                    args.MessageText = "Valid Decree No To required.";
                    args.IsCancel = true;
                    return;
                }
            }

            using (var trans = new esTransactionScope())
            {
                if (FormType == "mc0")
                {
                    if (Role == "usr")
                    {
                        entity.IsApproved = true;
                        entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                        entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                        entity.IsCredentialApplication = true;
                        entity.LastCredentialApplicationDateTime = (new DateTime()).NowAtSqlServer();
                        entity.LastCredentialApplicationByUserID = AppSession.UserLogin.UserID;
                    }
                }
                else if (FormType == "mc1")
                {
                    entity.IsVerified = true;
                    entity.VerifiedDateTime = txtVerifiedDateTime.IsEmpty ? (new DateTime()).NowAtSqlServer().Date : txtVerifiedDateTime.SelectedDate;
                    entity.VerifiedByUserID = AppSession.UserLogin.UserID;
                }
                else if (FormType == "asc")
                {
                    entity.IsCredentialing = true;
                    entity.CredentialingDate = txtCredentialingDate.IsEmpty ? (new DateTime()).NowAtSqlServer().Date : txtCredentialingDate.SelectedDate;
                    entity.LastCredentialingDateTime = (new DateTime()).NowAtSqlServer();
                    entity.LastCredentialingByUserID = AppSession.UserLogin.UserID;
                    entity.IsCompletelyVerified = true;
                }
                else if (FormType == "amc")
                {
                    entity.IsRecommendationLetter = true;
                    entity.RecommendationLetterDate = txtRecommendationLetterDate.IsEmpty ? (new DateTime()).NowAtSqlServer().Date : txtRecommendationLetterDate.SelectedDate;
                    entity.LastRecommendationLetterDateTime = (new DateTime()).NowAtSqlServer();
                    entity.LastRecommendationLetterByUserID = AppSession.UserLogin.UserID;
                }
                else if (FormType == "mc2")
                {
                    entity.IsVerified2 = true;
                    entity.IsCompletelyVerified = true;
                    entity.VerifiedDateTime2 = txtVerifiedDateTime2.IsEmpty ? (new DateTime()).NowAtSqlServer().Date : txtVerifiedDateTime2.SelectedDate;
                    entity.VerifiedByUserID2 = AppSession.UserLogin.UserID;
                }
                else if (FormType == "cal")
                {
                    entity.IsClinicalAssignmentLetter = true;
                    entity.LastClinicalAssignmentLetterDateTime = (new DateTime()).NowAtSqlServer();
                    entity.LastClinicalAssignmentLetterByUserID = AppSession.UserLogin.UserID;

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

            if (FormType == "mc0")
            {
                if (entity.IsCredentialing.HasValue)
                {
                    args.MessageText = "This data has entered the next process (Assessment By Supervisor).";
                    args.IsCancel = true;
                    return;
                }
            }
            else if (FormType == "mc1")
            {
                if (entity.IsRecommendationLetter.HasValue)
                {
                    args.MessageText = "This data has entered the next process (Approval By Credentialing Sub Committee).";
                    args.IsCancel = true;
                    return;
                }
            }
            else if (FormType == "asc")
            {
                if (entity.IsRecommendationLetter.HasValue)
                {
                    args.MessageText = "This data has entered the next process (Approval By Medical Committee).";
                    args.IsCancel = true;
                    return;
                }
            }
            else if (FormType == "amc")
            {
                if (entity.IsVerified2.HasValue)
                {
                    args.MessageText = "This data has entered the next process (Approval By Director).";
                    args.IsCancel = true;
                    return;
                }
            }
            else if (FormType == "mc2")
            {
                if (entity.IsClinicalAssignmentLetter.HasValue)
                {
                    args.MessageText = "This data has entered the next process (Clinical Assignment Letter).";
                    args.IsCancel = true;
                    return;
                }
            }

            using (var trans = new esTransactionScope())
            {
                if (FormType == "mc0")
                {
                    entity.IsApproved = false;
                    entity.ApprovedDateTime = null;
                    entity.ApprovedByUserID = null;
                    entity.IsCredentialApplication = false;
                    entity.LastCredentialApplicationDateTime = null;
                    entity.LastCredentialApplicationByUserID = null;
                }
                else if (FormType == "mc1")
                {
                    entity.IsVerified = false;
                    entity.VerifiedDateTime = null;
                    entity.VerifiedByUserID = null;
                }
                else if (FormType == "asc")
                {
                    entity.IsCredentialing = false;
                    entity.LastCredentialingDateTime = null;
                    entity.LastCredentialingByUserID = null;
                    entity.IsCompletelyVerified = false;
                }
                else if (FormType == "amc")
                {
                    entity.IsRecommendationLetter = false;
                    entity.LastRecommendationLetterDateTime = null;
                    entity.LastRecommendationLetterByUserID = null;
                }
                else if (FormType == "mc2")
                {
                    entity.IsVerified2 = false;
                    entity.IsCompletelyVerified = false;
                    entity.VerifiedDateTime2 = null;
                    entity.VerifiedByUserID2 = null;
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
            if (FormType == "mc0")
            {
                if (entity.IsApproved ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }
            else if (FormType == "mc1") // assessment by suvervisor
            {
                if (entity.IsVerified ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }
            else if (FormType == "mc2") // assessment by director
            {
                if (entity.IsVerified2 ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }
            else if (FormType == "asc") // approved by sub komite kredensial
            {
                if (entity.IsCredentialing ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }
            else if (FormType == "amc") // approved by komite medis
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
            RefreshCommandItemLicense(oldVal, newVal);

            bool isVisible = (newVal != AppEnum.DataMode.Read);
            trDocumentUpload.Visible = !isVisible;
            trClinicalAssigmnetLetterUpload.Visible = !isVisible && FormType == "cal";

            cboPersonID.Enabled = false;

            if (FormType == "mc0")
            {
                if (Role == "usr")
                {
                    grdSheet.Columns.FindByUniqueName("cboSRCurrentAbility").Visible = isVisible;
                    grdSheet.Columns.FindByUniqueName("txtSelfAssessmentNotes").Visible = false;//isVisible;
                    grdSheet.Columns.FindByUniqueName("CurrentAbility").Visible = !isVisible;
                    grdSheet.Columns.FindByUniqueName("SelfAssessmentNotes").Visible = false;//!isVisible;
                }

                grdSheet.Columns.FindByUniqueName("cboSRReview").Visible = false;
                grdSheet.Columns.FindByUniqueName("cboSRRecomendation").Visible = false;
                grdSheet.Columns.FindByUniqueName("cboSRConclusion").Visible = false;
                grdSheet.Columns.FindByUniqueName("txtNotes").Visible = false;

                grdSheet.Columns.FindByUniqueName("Review").Visible = false;
                grdSheet.Columns.FindByUniqueName("Recomendation").Visible = false;
                grdSheet.Columns.FindByUniqueName("Conclusion").Visible = false;
                grdSheet.Columns.FindByUniqueName("Notes").Visible = false;

                txtCredentialingDate.Enabled = false;
                txtRecommendationLetterDate.Enabled = false;
            }
            else
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

                txtCredentialApplicationDate.Enabled = false;
                cboSRCredentialingStatus.Enabled = false;
                cboSRRecredentialReason.Enabled = false;

                grdSheet.Columns.FindByUniqueName("cboSRCurrentAbility").Visible = false;
                grdSheet.Columns.FindByUniqueName("CurrentAbility").Visible = true;
                grdSheet.Columns.FindByUniqueName("txtSelfAssessmentNotes").Visible = false;
                grdSheet.Columns.FindByUniqueName("SelfAssessmentNotes").Visible = false;

                if (FormType == "mc1")
                {
                    txtRecommendationLetterDate.Enabled = false;

                    grdSheet.Columns.FindByUniqueName("cboSRReview").Visible = isVisible;
                    grdSheet.Columns.FindByUniqueName("Review").Visible = !isVisible;

                    grdSheet.Columns.FindByUniqueName("cboSRRecomendation").Visible = false;
                    grdSheet.Columns.FindByUniqueName("Recomendation").Visible = false;
                }
                else
                {
                    txtCredentialingDate.Enabled = false;

                    grdSheet.Columns.FindByUniqueName("cboSRReview").Visible = false;
                    grdSheet.Columns.FindByUniqueName("Review").Visible = true;

                    if (FormType == "asc" || FormType == "amc")
                    {
                        grdSheet.Columns.FindByUniqueName("cboSRRecomendation").Visible = false;
                        grdSheet.Columns.FindByUniqueName("Recomendation").Visible = false;
                    }
                    else if (FormType == "mc2")
                    {
                        grdSheet.Columns.FindByUniqueName("cboSRRecomendation").Visible = isVisible;
                        grdSheet.Columns.FindByUniqueName("Recomendation").Visible = !isVisible;
                    }
                    else if (FormType == "cal" || FormType == "cal2")
                    {
                        txtRecommendationLetterDate.Enabled = false;

                        grdSheet.Columns.FindByUniqueName("cboSRRecomendation").Visible = false;
                        grdSheet.Columns.FindByUniqueName("Recomendation").Visible = true;
                    }
                }

                grdSheet.Columns.FindByUniqueName("cboSRConclusion").Visible = false;
                grdSheet.Columns.FindByUniqueName("Conclusion").Visible = false;

                grdSheet.Columns.FindByUniqueName("txtNotes").Visible = false;
                grdSheet.Columns.FindByUniqueName("Notes").Visible = false;

                if (FormType == "asc")
                {
                    txtRecommendationNotes.ReadOnly = !isVisible;
                    txtRecommendationResultNotes.ReadOnly = true;
                    pnlCommitteeNote.Visible = false;
                }
                else if (FormType == "amc")
                {
                    txtRecommendationNotes.ReadOnly = true;
                    txtRecommendationResultNotes.ReadOnly = !isVisible;
                }
                else
                {
                    txtRecommendationNotes.ReadOnly = true;
                    txtRecommendationResultNotes.ReadOnly = true;
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

            if (cp.CredentialApplicationDate.HasValue)
                txtCredentialApplicationDate.SelectedDate = cp.CredentialApplicationDate;
            else
            {
                if (FormType == "mc0")
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

            if (cp.VerifiedDateTime.HasValue)
                txtVerifiedDateTime.SelectedDate = cp.VerifiedDateTime;

            if (cp.CredentialingDate.HasValue)
                txtCredentialingDate.SelectedDate = cp.CredentialingDate;
            else
            {
                if (FormType == "asc")
                    txtCredentialingDate.SelectedDate = DateTime.Now;
            }

            if (cp.RecommendationLetterDate.HasValue)
                txtRecommendationLetterDate.SelectedDate = cp.RecommendationLetterDate;
            else
            {
                if (FormType == "amc")
                    txtRecommendationLetterDate.SelectedDate = DateTime.Now;
                //else
                //    txtRecommendationLetterDate.Clear();
            }
            if (cp.VerifiedDateTime2.HasValue)
                txtVerifiedDateTime2.SelectedDate = cp.VerifiedDateTime2;

            txtRecommendationNotes.Text = cp.RecommendationNotes;
            txtRecommendationResultNotes.Text = cp.RecommendationResultNotes;

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
            }
            else
            {
                txtValidFrom.Clear();
            }

            if (cp.ValidTo.HasValue)
            {
                txtValidTo.SelectedDate = cp.ValidTo;
            }
            else
            {
                txtValidTo.Clear();
            }

            if (FormType == "mc0")
            {
                if (Role == "usr")
                    chkIsApproved.Checked = cp.IsApproved ?? false;
            }
            else if (FormType == "mc1")
                chkIsApproved.Checked = cp.IsVerified ?? false;
            else if (FormType == "mc2")
                chkIsApproved.Checked = cp.IsVerified2 ?? false;
            else if (FormType == "asc")
                chkIsApproved.Checked = cp.IsCredentialing ?? false;
            else if (FormType == "amc")
                chkIsApproved.Checked = cp.IsRecommendationLetter ?? false;
            else if (FormType == "cal" || FormType == "cal2")
                chkIsApproved.Checked = cp.IsClinicalAssignmentLetter ?? false;
            else chkIsApproved.Checked = false;

            chkIsVoid.Checked = cp.IsVoid ?? false;

            PopulateLicenseGrid();

            if (IsPostBack)
            {
                RefreshGridSheetItems();
            }
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(CredentialProcess entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewTransactionNo();
                _autoNumber.Save();
            }

            entity.TransactionNo = txtTransactionNo.Text;
            entity.QuestionFormID = string.Empty;
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

            entity.str.CompetencyAssessmentVerificationDate = string.Empty;
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

            entity.RecommendationNotes = txtRecommendationNotes.Text;
            entity.RecommendationResultNotes = txtRecommendationResultNotes.Text;

            if (FormType == "mc1")
                entity.IsPerform = true;

            if (!txtRecommendationLetterDate.IsEmpty)
                entity.RecommendationLetterDate = txtRecommendationLetterDate.SelectedDate;
            else
                entity.str.RecommendationLetterDate = string.Empty;

            entity.RecommendationLetterNo = string.Empty;

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

            if (FormType == "mc0")
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
                }
            }

            if (FormType == "mc1")
            {
                entity.IsVerified = chkIsApproved.Checked;
            }

            if (FormType == "mc2")
            {
                entity.IsVerified2 = chkIsApproved.Checked;
            }

            if (FormType == "asc")
            {
                entity.IsCredentialing = chkIsApproved.Checked;
            }

            if (FormType == "amc")
            {
                entity.IsRecommendationLetter = chkIsApproved.Checked;
            }

            if (FormType == "cal" || FormType == "cal2")
            {
                entity.IsClinicalAssignmentLetter = chkIsApproved.Checked;
            }
        }

        private void SaveEntity(CredentialProcess entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                if (FormType == "mc0")
                {
                    if (Role == "usr")
                    {
                        CredentialProcessLicenses.Save();
                    }
                }

                if (FormType == "mc0" || FormType == "mc1" || FormType == "mc2")
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
                        string review = ((RadComboBox)dataItem.FindControl("cboSRReview")).SelectedValue;
                        string recomendation = ((RadComboBox)dataItem.FindControl("cboSRRecomendation")).SelectedValue;
                        string conclusion = ((RadComboBox)dataItem.FindControl("cboSRConclusion")).SelectedValue;
                        string notes = ((RadTextBox)dataItem.FindControl("txtNotes")).Text;

                        if (FormType == "mc0")
                        {
                            if (Role == "usr")
                            {
                                item.SRCurrentAbility = currentAbility;
                                item.SelfAssessmentNotes = selfAssessmentNotes;
                            }
                        }
                        else if (FormType == "mc1")
                        {
                            item.SRReview = review;
                        }
                        else if (FormType == "mc2")
                        {
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
                if (ecpdt.Rows[0]["SRProfessionGroup"].ToString() == "01")
                {
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
                }

                GetQuestionnaireID();

                grdLicense.DataSource = CredentialProcessLicenses;
                grdLicense.DataBind();
            }
            else
            {
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

            var calq = new AppStandardReferenceItemQuery();
            calq.Where
                (
                    calq.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel,
                    calq.IsActive == true,
                    calq.ReferenceID == cboSRClinicalWorkArea.SelectedValue
                );
            DataTable caldtb = calq.LoadDataTable();
            if (caldtb.Rows.Count == 1)
            {
                cboSRClinicalAuthorityLevel.DataSource = caldtb;
                cboSRClinicalAuthorityLevel.DataBind();
                cboSRClinicalAuthorityLevel.SelectedValue = caldtb.Rows[0]["ItemID"].ToString();
            }

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
            bool isVisible = (newVal != AppEnum.DataMode.Read) && FormType == "mc0";
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
                DataTable dtb = coll.GetJoinMedicWithDefaultValue(questionnaireId, transactionNo);

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
                        dataItem.Font.Bold = true;
                        dataItem.Font.Italic = true;
                    }
                    else if (FormType != "mc0")
                    {
                        var srCurrentAbility = dataItem["SRCurrentAbility"].Text;
                        var srReview = dataItem["SRReview"].Text;

                        if (string.IsNullOrEmpty(srCurrentAbility) || srCurrentAbility == "&nbsp;" || srCurrentAbility == "-1")
                            srCurrentAbility = "";
                        if (string.IsNullOrEmpty(srReview) || srReview == "&nbsp;" || srReview == "-1")
                            srReview = "";

                        if (srCurrentAbility != srReview)
                        {
                            dataItem.ForeColor = Color.Red;
                        }
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
                result.DataTextField = "ItemName";
                result.DataBind();
                ComboBox.SelectedValue(result, srCurrentAbility);
            }

            if (!string.IsNullOrEmpty(srReview) && srReview != "&nbsp;" && srReview != "-1")
            {
                var result = (dataItem["QuestionnaireItemID"].FindControl("cboSRReview") as RadComboBox);

                DataView dv = PopulateReview().DefaultView;
                dv.RowFilter = "ItemID = '" + srReview + "'";

                result.DataSource = dv.ToTable();
                result.DataValueField = "ItemID";
                result.DataTextField = "ItemName";
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
                result.DataTextField = "ItemName";
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
            query.Select(query.ItemID, query.ItemName);

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
            query.Select(query.ItemID, query.ItemName);

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
            query.Select(query.ItemID, query.ItemName);

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
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRCurrentAbility_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new AppStandardReferenceItemQuery("a");
            query.Where(
                query.StandardReferenceID == "CredentialResultCurrentAbility",
                query.ReferenceID == cboSRProfessionGroup.SelectedValue);
            query.Select(query.ItemID, query.ItemName);
            var combo = o as RadComboBox;

            combo.DataSource = query.LoadDataTable();
            combo.DataBind();
        }

        protected void cboSRReview_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRReview_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new AppStandardReferenceItemQuery("a");
            query.Where(
                query.StandardReferenceID == "CredentialResultReview",
                query.ReferenceID == cboSRProfessionGroup.SelectedValue);
            query.Select(query.ItemID, query.ItemName);
            var combo = o as RadComboBox;

            combo.DataSource = query.LoadDataTable();
            combo.DataBind();
        }

        protected void cboSRRecomendation_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
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
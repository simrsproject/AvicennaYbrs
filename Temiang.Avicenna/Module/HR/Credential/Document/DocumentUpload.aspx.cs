using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.Module.HR.Credential.Document
{
    public partial class DocumentUpload : BasePageDialogEntry
    {
        protected long DocumentID
        {
            get
            {
                if (!IsPostBack)
                    hdnPdId.Value = Request.QueryString["pdid"];
                if (string.IsNullOrEmpty(hdnPdId.Value))
                    hdnPdId.Value = "0";
                return Convert.ToInt32(hdnPdId.Value);
            }
            set { hdnPdId.Value = value.ToString(); }
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

        private string ProfessionGroup
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["pg"]) ? "" : Request.QueryString["pg"];
            }
        }

        private bool IsClinicalAssignmentLetterDoc
        {
            get
            {
                var note = string.IsNullOrEmpty(Request.QueryString["note"]) ? "" : Request.QueryString["note"];
                if (note == "cal")
                    return true;
                return false;
            }
        }

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

                case "cst1":
                    ProgramID = ProfessionGroup == "01" ? AppConstant.Program.CredentialingStatus_Komed : (ProfessionGroup == "02" ? AppConstant.Program.CredentialingStatus_Komkep : (ProfessionGroup == "03" ? AppConstant.Program.CredentialingStatus_Ktkl : AppConstant.Program.CredentialingStatusIndividual));
                    break;
                case "cst2":
                    ProgramID = ProfessionGroup == "01" ? AppConstant.Program.CredentialingStatus_Komed : (ProfessionGroup == "02" ? AppConstant.Program.CredentialingStatus_Komkep : (ProfessionGroup == "03" ? AppConstant.Program.CredentialingStatus_Ktkl : AppConstant.Program.CredentialingStatusIndividualMedic));
                    break;

                case "apl2":
                    if (Role == "chk")
                        ProgramID = ProfessionGroup == "01" ? AppConstant.Program.CredentialDocumentChecking_Komed : (ProfessionGroup == "02" ? AppConstant.Program.CredentialDocumentChecking_Komkep : (ProfessionGroup == "03" ? AppConstant.Program.CredentialDocumentChecking_Ktkl : AppConstant.Program.CredentialDocumentChecking));
                    else
                        ProgramID = Role == "usr" ? AppConstant.Program.CredentialApplication2 : (Role == "doc" ? AppConstant.Program.CredentialUpdateDocument : AppConstant.Program.CredentialApplication2Admin);
                    break;
                case "rec2":
                    ProgramID = ProfessionGroup == "01" ? AppConstant.Program.CredentialRecomendation_Komed : (ProfessionGroup == "02" ? AppConstant.Program.CredentialRecomendation_Komkep : (ProfessionGroup == "03" ? AppConstant.Program.CredentialRecomendation_Ktkl : (ProfessionGroup == "ci" ? AppConstant.Program.CredentialRecomendation_Ci : AppConstant.Program.CredentialRecomendation)));
                    break;
                case "con1":
                    ProgramID = AppConstant.Program.CredentialApprovalBySubCommitte;
                    break;
                case "con2":
                    ProgramID = AppConstant.Program.CredentialApprovalByCommitte;
                    break;
                case "dir":
                    ProgramID = AppConstant.Program.CredentialApprovalByDirector;
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
                case "cst":
                    ProgramID = ProfessionGroup == "01" ? AppConstant.Program.CredentialingStatus_Komed : (ProfessionGroup == "02" ? AppConstant.Program.CredentialingStatus_Komkep : (ProfessionGroup == "03" ? AppConstant.Program.CredentialingStatus_Ktkl : AppConstant.Program.CredentialingStatus));
                    break;
            }

            // Menu hardcode
            IsSingleRecordMode = true;

            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;

            if (!IsPostBack)
            {
                var cp = new CredentialProcess();
                if (cp.LoadByPrimaryKey(Request.QueryString["pid"].ToString()))
                {
                    var emp = new PersonalInfo();
                    if (emp.LoadByPrimaryKey(cp.PersonID.ToInt()))
                        this.Title = "File Attachment of : " + cp.TransactionNo + "[" + emp.EmployeeName + "]";
                    else
                        this.Title = "File Attachment of : " + cp.TransactionNo + " [...]";
                }
            }
        }


        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var pd = new CredentialProcessDocumentUpload();
            pd.LoadByPrimaryKey(DocumentID);
            PopulateEntryControl(pd);
        }
        protected void PopulateEntryControl(esEntity entity)
        {
            var pd = (CredentialProcessDocumentUpload)entity;
            txtDocumentName.Text = pd.DocumentName;
            txtFileAttachName.Text = pd.FileAttachName;
            txtDocumentDate.SelectedDate = pd.DocumentDate;
            txtNotes.Text = pd.Notes;
            DocumentID = pd.DocumentID ?? 0;
        }
        protected override void OnMenuNewClick()
        {
            DocumentID = 0;

            var ent = new CredentialProcessDocumentUpload();
            ent.TransactionNo = Request.QueryString["pid"].ToString();

            PopulateEntryControl(ent);

            txtDocumentDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            if (!string.IsNullOrEmpty(Request.QueryString["note"]))
            {
                if (Request.QueryString["note"] == "cal")
                    txtDocumentName.Text = "Clinical Assignment Letter";
                else
                    txtDocumentName.Text = Request.QueryString["note"].ToString();
            }
                
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new CredentialProcessDocumentUpload();
            SetEntityValue(entity);
            var newID = SaveEntity(entity);
            DocumentID = newID;

            // Upload file
            var fullPathFileName = UploadFile(entity);

            // Save thumbnail
            if (!string.IsNullOrWhiteSpace(fullPathFileName))
            {
                if (entity.OriFileName.Contains(".jpg") || entity.OriFileName.Contains(".jpeg") || entity.OriFileName.Contains(".png"))
                {
                    var imgHelper = new ImageHelper();
                    var imgArr = imgHelper.LoadImageToArray(fullPathFileName);
                    var smallImg = imgHelper.ResizeImage(imgArr, new Size(100, 100), true, InterpolationMode.Low);

                    if (entity.OriFileName.Contains(".jpg") || entity.OriFileName.Contains(".jpeg"))
                        entity.SmallImage = imgHelper.ConvertImageToByteArray(smallImg, ImageFormat.Jpeg);
                    else if (entity.OriFileName.Contains(".png"))
                        entity.SmallImage = imgHelper.ConvertImageToByteArray(smallImg, ImageFormat.Png);
                }
                entity.Save();
            }
            else
            {
                // Fail Upload
                // Delete 
                entity.MarkAsDeleted();
                entity.Save();
            }

        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            rowUploadFile.Visible = newVal == (AppEnum.DataMode.New);
        }
        protected override void OnMenuEditClick()
        {
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            // Edit hanya untuk update keterangan saja, jika edit filenya harus lewat delete lalu tambah
            var entity = new CredentialProcessDocumentUpload();
            if (entity.LoadByPrimaryKey(DocumentID))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        private void SetEntityValue(CredentialProcessDocumentUpload entity)
        {
            entity.TransactionNo = Request.QueryString["pid"].ToString();
            entity.DocumentName = txtDocumentName.Text;
            entity.DocumentDate = txtDocumentDate.SelectedDate;
            entity.Notes = txtNotes.Text;
            entity.DocumentCode = IsClinicalAssignmentLetterDoc ? "ca" : "";

            if (entity.es.IsAdded)
            {
                entity.IsUpload = true;
                entity.FileAttachName = string.Empty;
            }
        }

        private long SaveEntity(CredentialProcessDocumentUpload entity)
        {
            entity.Save();
            return entity.DocumentID ?? 0;
        }

        private string UploadFile(CredentialProcessDocumentUpload entity)
        {
            if (uplFileTemplate.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile validFile in uplFileTemplate.UploadedFiles)
                {
                    var fileName = string.Format("{0:000000000000000}_{1}", entity.DocumentID, validFile.GetName());
                    entity.FileAttachName = fileName;
                    entity.OriFileName = validFile.GetName();

                    var targetFolder = Path.Combine(AppSession.Parameter.EmployeeDocumentFolder, "CredentialingDocument", entity.TransactionNo.Trim()); if (!System.IO.Directory.Exists(targetFolder))
                        System.IO.Directory.CreateDirectory(targetFolder);

                    var pathFile = Path.Combine(targetFolder, fileName);
                    validFile.SaveAs(pathFile, true);
                    return pathFile;
                }
            }

            return string.Empty;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var ent = new CredentialProcessDocumentUpload();
            if (ent.LoadByPrimaryKey(DocumentID))
            {
                // Rename File
                var filePath = (ent.IsUpload ?? false) ?
                System.IO.Path.Combine(AppSession.Parameter.EmployeeDocumentFolder, "CredentialingDocument", ent.TransactionNo.ToString(), ent.FileAttachName)
                : ent.OriPath;

                if (System.IO.File.Exists(filePath))
                {
                    var newFilePath = Path.Combine(System.IO.Path.GetDirectoryName(filePath), "DEL_" + System.IO.Path.GetFileName(filePath));

                    File.Move(filePath, newFilePath);
                }

                ent.IsDeleted = true;
                ent.Save();
                DocumentID = 0;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }
    }
}
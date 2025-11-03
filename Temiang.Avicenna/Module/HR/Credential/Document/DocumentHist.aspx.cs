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

namespace Temiang.Avicenna.Module.HR.Credential.Document
{
    public partial class DocumentHist : BasePageDialog
    {
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

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if (source == grdDocument)
            {
                if (eventArgument.Equals("quicksearch") || eventArgument.Equals("refresh"))
                {
                    grdDocument.CurrentPageIndex = 0;
                    grdDocument.Rebind();
                }
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

                case "ewi":
                    ProgramID = AppConstant.Program.EmployeeWorkingInfo;
                    break;
            }

            if (!IsPostBack)
            {
                var isApproved = false;
                var cp = new CredentialProcess();
                if (cp.LoadByPrimaryKey(Request.QueryString["pid"].ToString()))
                {
                    var emp = new PersonalInfo();
                    if (emp.LoadByPrimaryKey(cp.PersonID.ToInt()))
                        this.Title = cp.TransactionNo + " [" + emp.EmployeeName + "]";
                    else
                        this.Title = cp.TransactionNo + " [...]";

                    if (AppSession.Application.IsModuleCredential2Active)
                    {
                        grdDocument.Columns.FindByUniqueName("DeleteColumn").Visible = (cp.IsRecommendation ?? false) != true;
                        isApproved = Role == "doc" ? (cp.IsDocumentComplete ?? false) : (cp.IsApproved ?? false);
                        if (FormType == "cst")
                            isApproved = cp.SRClinicalAppoinmentStatus != "02";
                    }
                    else
                    {
                        isApproved = IsClinicalAssignmentLetterDoc ? (cp.IsClinicalAssignmentLetter ?? false) : (cp.IsApproved ?? false);
                        grdDocument.Columns.FindByUniqueName("DeleteColumn").Visible = !isApproved;
                    }
                }

                Footer.Visible = false;

                if ((FormType == "apl2" && Role != "chk") || (FormType == "caa" && Role == "usr") || (FormType == "apl") || (FormType == "mc0"))
                {
                    RadToolBar tb = (RadToolBar)Common.Helper.FindControlRecursive(Page, "tbarMain");
                    if (tb != null)
                    {
                        tb.Items[0].Visible = this.IsUserAddAble && !isApproved;
                    }
                    grdDocument.Columns.FindByUniqueName("DeleteColumn").Visible = !isApproved;
                    grdDocument.Columns.FindByUniqueName("colMenu").Visible = !isApproved;
                }
                else if (FormType == "cst" && ProfessionGroup == "")
                {
                    RadToolBar tb = (RadToolBar)Common.Helper.FindControlRecursive(Page, "tbarMain");
                    if (tb != null)
                    {
                        tb.Items[0].Visible = !isApproved;
                    }

                    grdDocument.Columns.FindByUniqueName("DeleteColumn").Visible = false;
                    grdDocument.Columns.FindByUniqueName("colMenu").Visible = !isApproved;
                }
                else if (FormType == "cal" && IsClinicalAssignmentLetterDoc)
                {
                    RadToolBar tb = (RadToolBar)Common.Helper.FindControlRecursive(Page, "tbarMain");
                    if (tb != null)
                    {
                        tb.Items[0].Visible = this.IsUserAddAble && isApproved;
                    }
                    grdDocument.Columns.FindByUniqueName("DeleteColumn").Visible = isApproved;
                    grdDocument.Columns.FindByUniqueName("colMenu").Visible = isApproved;
                }
                else
                {
                    RadToolBar tb = (RadToolBar)Common.Helper.FindControlRecursive(Page, "tbarMain");
                    if (tb != null)
                    {
                        tb.Items[0].Visible = false;
                    }

                    grdDocument.Columns.FindByUniqueName("DeleteColumn").Visible = false;
                    grdDocument.Columns.FindByUniqueName("colMenu").Visible = false;
                }
            }
        }

        private DataTable DocumentDataTable()
        {
            var query = new CredentialProcessDocumentUploadQuery("a");
            query.Where(query.TransactionNo == Request.QueryString["pid"].ToString(), query.Or(query.IsDeleted.IsNull(), query.IsDeleted == false));
            if (IsClinicalAssignmentLetterDoc)
                query.Where(query.DocumentCode.IsNotNull(), query.DocumentCode == "ca");
            else
                query.Where(query.Or(query.DocumentCode.IsNull(), query.DocumentCode == ""));

            var searchName = ((RadTextBox)tbarMain.Items[2].FindControl("txtQuickSearch")).Text;
            if (!string.IsNullOrWhiteSpace(searchName))
                query.Where(query.DocumentName.Like("%" + searchName.Trim() + "%"));

            query.OrderBy(query.DocumentDate.Descending, query.DocumentID.Descending);

            var dtb = query.LoadDataTable();

            // Replace empty smallimage
            var imgHelper = new ImageHelper();
            var pdfImage = imgHelper.LoadImageToArray(string.Format("{0}\\Images\\pdf100.png", Server.MapPath("~")));
            var dicomImage = imgHelper.LoadImageToArray(string.Format("{0}\\Images\\dicom.png", Server.MapPath("~")));
            foreach (DataRow row in dtb.Rows)
            {
                var fileName = row["FileAttachName"].ToString().ToLower();

                var oriPath = string.Empty;
                if (row["OriPath"] != null)
                {
                    oriPath = row["OriPath"].ToString();
                }

                if (fileName.Contains(".pdf"))
                {
                    row["SmallImage"] = pdfImage;
                }
                else if (fileName.Contains(".dcm"))
                {
                    row["SmallImage"] = dicomImage;
                }
                else if (row["SmallImage"] == DBNull.Value && !string.IsNullOrWhiteSpace(oriPath) && (oriPath.ToLower().Contains(".jpg") || oriPath.ToLower().Contains(".jpeg") || oriPath.ToLower().Contains(".png") || oriPath.ToLower().Contains(".bmp")))
                {
                    // Update thumbnail
                    if (FileHelper.FileExists(oriPath, 1000))
                    {
                        try
                        {
                            var entity = new CredentialProcessDocumentUpload();
                            if (entity.LoadByPrimaryKey(row["DocumentID"].ToInt()))
                            {
                                var imgArr = imgHelper.LoadImageToArray(oriPath);
                                row["SmallImage"] = imgArr;

                                var smallImg = imgHelper.ResizeImage(imgArr, new Size(100, 100), true, InterpolationMode.Low);
                                oriPath = oriPath.ToLower();
                                if (oriPath.Contains(".jpg") || oriPath.Contains(".jpeg"))
                                    entity.SmallImage = imgHelper.ConvertImageToByteArray(smallImg, ImageFormat.Jpeg);
                                else if (oriPath.Contains(".png"))
                                    entity.SmallImage = imgHelper.ConvertImageToByteArray(smallImg, ImageFormat.Png);
                                else if (oriPath.Contains(".bmp"))
                                    entity.SmallImage = imgHelper.ConvertImageToByteArray(smallImg, ImageFormat.Bmp);

                                entity.Save();
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }

            return dtb;
        }

        protected void grdDocument_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdDocument.DataSource = DocumentDataTable();
        }

        protected void grdDocument_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;
            var pdid = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["DocumentID"]);

            var ent = new CredentialProcessDocumentUpload();
            if (ent.LoadByPrimaryKey(pdid))
            {
                // Rename File
                var filePath = (ent.IsUpload ?? false) ?
                System.IO.Path.Combine(AppSession.Parameter.EmployeeDocumentFolder, "CredentialingDocument", ent.TransactionNo.ToString().Trim(), ent.FileAttachName)
                : ent.OriPath;

                if (System.IO.File.Exists(filePath))
                {
                    var newFilePath = Path.Combine(System.IO.Path.GetDirectoryName(filePath), "DEL_" + System.IO.Path.GetFileName(filePath));

                    File.Move(filePath, newFilePath);
                }

                ent.IsDeleted = true;
                ent.Save();
            }
        }
    }
}
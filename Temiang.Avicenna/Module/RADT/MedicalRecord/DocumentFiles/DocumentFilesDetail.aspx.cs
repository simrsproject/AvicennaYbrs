using System;
using System.IO;
using Telerik.Web.UI.Upload;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class DocumentFilesDetail : BasePageDetail
    {
        private string ForDocumentChecklist
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["dc"]))
                    return "0";
                return Request.QueryString["dc"];
            }
        }

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "DocumentFilesSearch.aspx?dc=" + ForDocumentChecklist;
            UrlPageList = "DocumentFilesList.aspx?dc=" + ForDocumentChecklist;
			
            ProgramID = ForDocumentChecklist == "0"
                            ? AppConstant.Program.DocumentFiles
                            : AppConstant.Program.GuarantorDocumentFiles;

			//StandardReference Initialize
			if (!IsPostBack)
			{
                //trProgramID.Visible = ForDocumentChecklist != "0";
                pnlMedicalRecordDoc.Visible = ForDocumentChecklist == "0";
                trDocumentType.Visible = ForDocumentChecklist == "0";
                StandardReference.InitializeIncludeSpace(cboSRDocumentFileType, AppEnum.StandardReference.DocumentFileType);
                StandardReference.InitializeIncludeSpace(cboSRAssessmentType, AppEnum.StandardReference.AssessmentType);
                StandardReference.InitializeIncludeSpace(cboSRHaisMonitoring, AppEnum.StandardReference.HaisMonitoring);
            }
			
			//PopUp Search
			if (!IsCallback)
			{
				
			}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////Do not display SelectedFilesCount progress indicator.
                //radProgressArea.ProgressIndicators &= ~ProgressIndicators.SelectedFilesCount;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new DocumentFiles());
            chkIsActive.Checked = true;
            chkIsUsedForAnalysis.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            DocumentFiles entity = new DocumentFiles();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtDocumentFilesID.Text)))
            {
                entity.MarkAsDeleted();
                using (esTransactionScope trans = new esTransactionScope())
                {
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
            DocumentFiles entity = new DocumentFiles();
            entity = new DocumentFiles();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);

        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            DocumentFiles entity = new DocumentFiles();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtDocumentFilesID.Text)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
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
            auditLogFilter.PrimaryKeyData = string.Format("DocumentFilesID='{0}'", txtDocumentFilesID.Text.Trim());
            auditLogFilter.TableName = "DocumentFiles";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtDocumentFilesID.Enabled = (newVal == AppEnum.DataMode.New);
            btnUpload.Enabled = (!string.IsNullOrEmpty(txtDocumentFilesID.Text) && newVal == AppEnum.DataMode.Read && UserAccess.IsEditAble); 
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            DocumentFiles entity = new DocumentFiles();
            if (parameters.Length > 0)
            {
                string documentFilesID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(documentFilesID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtDocumentFilesID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            DocumentFiles documentFiles = (DocumentFiles)entity;
            txtDocumentFilesID.Value = Convert.ToDouble(documentFiles.DocumentFilesID);
            txtDocumentName.Text = documentFiles.DocumentName;
            txtDocumentInitial.Text = documentFiles.DocumentInitial;
            txtDocumentNumber.Text = documentFiles.DocumentNumber;
            txtFileTemplateName.Text = documentFiles.FileTemplateName;
            chkIsQuality.Checked = documentFiles.IsQuality ?? false;
            chkIsLegible.Checked = documentFiles.IsLegible ?? false;
            chkIsSign.Checked = documentFiles.IsSign ?? false;
            chkIsActive.Checked = documentFiles.IsActive ?? false;
            chkIsUsedForAnalysis.Checked = documentFiles.IsUsedForAnalysis ?? false;
            cboSRDocumentFileType.SelectedValue = documentFiles.SRDocumentFileType;
            cboSRAssessmentType.SelectedValue = documentFiles.SRAssessmentType;
            cboSRHaisMonitoring.SelectedValue = documentFiles.SRHaisMonitoring;
            trQuestionFormID.Visible = cboSRDocumentFileType.SelectedValue == "PHR";
            trAssessmentType.Visible = cboSRDocumentFileType.SelectedValue == "Assessment";
            trHaisMonitoring.Visible = cboSRDocumentFileType.SelectedValue == "Hais";
            List<string> usedProgid = new List<string> { "Report", "Careplan", "Education", "UDDRecon" };
            trProgramID.Visible = usedProgid.Contains(cboSRDocumentFileType.SelectedValue);

            if (!string.IsNullOrEmpty(documentFiles.ProgramID))
            {
                var pq = new AppProgramQuery();
                pq.Select(
                    pq.ProgramID,
                    pq.ProgramName
                    );
                pq.Where(pq.ProgramID == documentFiles.ProgramID);
                pq.OrderBy(pq.ProgramName.Ascending);

                cboProgramID.DataSource = pq.LoadDataTable();
                cboProgramID.DataBind();

                cboProgramID.SelectedValue = documentFiles.ProgramID;
            }
            else
            {
                cboProgramID.Items.Clear();
                cboProgramID.SelectedValue = string.Empty;
                cboProgramID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(documentFiles.QuestionFormID))
            {
                var pq = new QuestionFormQuery();
                pq.Select(
                    pq.QuestionFormID,
                    pq.QuestionFormName
                    );
                pq.Where(pq.QuestionFormID == documentFiles.QuestionFormID);

                cboQuestionFormID.DataSource = pq.LoadDataTable();
                cboQuestionFormID.DataBind();

                cboQuestionFormID.SelectedValue = documentFiles.QuestionFormID;
            }
            else
            {
                cboQuestionFormID.Items.Clear();
                cboQuestionFormID.SelectedValue = string.Empty;
                cboQuestionFormID.Text = string.Empty;
            }
            //Display Data Detail
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(DocumentFiles entity)
        {
            entity.DocumentFilesID = Convert.ToInt32(txtDocumentFilesID.Value);
            entity.DocumentName = txtDocumentName.Text;
            entity.DocumentInitial = txtDocumentInitial.Text;
            entity.DocumentNumber = txtDocumentNumber.Text;
            entity.IsQuality = chkIsQuality.Checked;
            entity.IsLegible = chkIsLegible.Checked;
            entity.IsSign = chkIsSign.Checked;
            entity.IsActive = chkIsActive.Checked;
            entity.IsUsedForAnalysis = chkIsUsedForAnalysis.Checked;
            entity.IsUsedForGuarantorChecklist = ForDocumentChecklist == "1";
            entity.ProgramID = cboProgramID.SelectedValue;
            entity.QuestionFormID = cboQuestionFormID.SelectedValue;
            entity.SRDocumentFileType = cboSRDocumentFileType.SelectedValue;
            entity.SRAssessmentType = cboSRAssessmentType.SelectedValue;
            entity.SRHaisMonitoring = cboSRHaisMonitoring.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(DocumentFiles entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                //Commit if success, Rollback if failed

                txtDocumentFilesID.Text = entity.DocumentFilesID.ToString();

                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            DocumentFilesQuery que = new DocumentFilesQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.DocumentFilesID > txtDocumentFilesID.Text);
                que.OrderBy(que.DocumentFilesID.Ascending);
            }
            else
            {
                que.Where(que.DocumentFilesID < txtDocumentFilesID.Text);
                que.OrderBy(que.DocumentFilesID.Descending);
            }
            if (ForDocumentChecklist == "0")
                que.Where(que.IsUsedForGuarantorChecklist == false);
            else que.Where(que.IsUsedForGuarantorChecklist == true);

            DocumentFiles entity = new DocumentFiles();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        #endregion

        //protected void uplFileTemplateName_FileExists(object sender, Telerik.Web.UI.Upload.UploadedFileEventArgs e)
        //{
        //    int counter = 1;

        //    UploadedFile file = e.UploadedFile;

        //    string targetFolder = Server.MapPath(uplFileTemplateName.TargetFolder);

        //    string targetFileName = Path.Combine(targetFolder,
        //        file.GetNameWithoutExtension() + counter.ToString() + file.GetExtension());

        //    while (System.IO.File.Exists(targetFileName))
        //    {
        //        counter++;
        //        targetFileName = Path.Combine(targetFolder,
        //            file.GetNameWithoutExtension() + counter.ToString() + file.GetExtension());
        //    }

        //    file.SaveAs(targetFileName);
        //}   

        private void ShowProgress(UploadedFile file)
        {
            const int total = 100;

            RadProgressContext progress = RadProgressContext.Current;

            for (int i = 0; i < total; i++)
            {
                progress.PrimaryTotal = 1;
                progress.PrimaryValue = 1;
                progress.PrimaryPercent = 100;

                progress.SecondaryTotal = total;
                progress.SecondaryValue = i;
                progress.SecondaryPercent = i;

                progress.CurrentOperationText = file.GetName() + " is being processed...";

                if (!Response.IsClientConnected)
                {
                    //Cancel button was clicked or the browser was closed, so stop processing
                    break;
                }
            }
        }

        protected void cboProgramID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ProgramID"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ProgramName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ProgramID"].ToString();
        }

        protected void cboProgramID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var pq = new AppProgramQuery();

            pq.es.Top = 20;
            pq.es.Distinct = true;
            pq.Select(
                pq.ProgramID,
                pq.ProgramName
                );
            pq.Where(
                pq.ProgramType.In("RPT", "XML", "RSLIP"),
                pq.Or(
                    pq.ProgramID.Like("%" + e.Text + "%"),
                    pq.ProgramName.Like("%" + e.Text + "%")
                    )
                );
            pq.OrderBy(pq.ProgramName.Ascending);

            (o as RadComboBox).DataSource = pq.LoadDataTable();
            (o as RadComboBox).DataBind();
        }

        protected void cboQuestionFormID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["QuestionFormID"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["QuestionFormName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["QuestionFormID"].ToString();
        }

        protected void cboQuestionFormID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var pq = new QuestionFormQuery();

            pq.es.Top = 20;
            pq.es.Distinct = true;
            pq.Select(
                pq.QuestionFormID,
                pq.QuestionFormName
                );
            pq.Where(
                pq.Or(
                    pq.QuestionFormID.Like("%" + e.Text + "%"),
                    pq.QuestionFormName.Like("%" + e.Text + "%")
                    ), 
                pq.IsActive == true
                );
            pq.OrderBy(pq.QuestionFormName.Ascending);

            (o as RadComboBox).DataSource = pq.LoadDataTable();
            (o as RadComboBox).DataBind();
        }

        protected void cboSRDocumentFileType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            trAssessmentType.Visible = false;
            trHaisMonitoring.Visible = false;
            trQuestionFormID.Visible = false;
            trProgramID.Visible = false;

            cboSRAssessmentType.SelectedValue = string.Empty;
            cboSRHaisMonitoring.SelectedValue = string.Empty;
            cboQuestionFormID.SelectedValue = string.Empty;
            cboProgramID.SelectedValue = string.Empty;
            cboSRAssessmentType.Text = string.Empty;
            cboSRHaisMonitoring.Text = string.Empty;
            cboQuestionFormID.Text = string.Empty;
            cboProgramID.Text = string.Empty;

            switch (e.Value)
            {
                case "Assessment":
                    trAssessmentType.Visible = true;
                    break;
                case "Hais":
                    trHaisMonitoring.Visible = true;
                    break;
                case "PHR":
                    trQuestionFormID.Visible = true;
                    break;
                case "Report":
                case "Careplan":
                case "Education":
                case "UDDRecon":
                    trProgramID.Visible = true;
                    break;
            }
        }
    }
}

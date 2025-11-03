using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.KEPK
{
    public partial class ResearchLetterDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ResearchLetterSearch.aspx";
            UrlPageList = "ResearchLetterList.aspx";

            ProgramID = AppConstant.Program.KEPK_ResearchLetter;

            WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRResearchDecision, AppEnum.StandardReference.ResearchDecision);
                StandardReference.InitializeIncludeSpace(cboSRResearchInstitution, AppEnum.StandardReference.ResearchInstitution);
                StandardReference.InitializeIncludeSpace(cboSRResearchFaculty, AppEnum.StandardReference.ResearchFaculty);
                StandardReference.InitializeIncludeSpace(cboSRResearchMajors, AppEnum.StandardReference.ResearchMajors);
                StandardReference.InitializeIncludeSpace(cboSREducationDegree, AppEnum.StandardReference.EducationDegree);
                StandardReference.InitializeIncludeSpace(cboSRResearchReviewerName, AppEnum.StandardReference.ResearchReviewerName);
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ResearchLetter());
            txtLetterID.Text = "0";
            txtLetterDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            cboSRResearchDecision.SelectedValue = string.Empty;
            cboSRResearchDecision.Text = string.Empty;
            cboSRResearchInstitution.SelectedValue = string.Empty;
            cboSRResearchInstitution.Text = string.Empty;
            cboSRResearchFaculty.SelectedValue = string.Empty;
            cboSRResearchFaculty.Text = string.Empty;
            cboSRResearchMajors.SelectedValue = string.Empty;
            cboSRResearchMajors.Text = string.Empty;
            cboSREducationDegree.SelectedValue = string.Empty;
            cboSREducationDegree.Text = string.Empty;
            cboSRResearchReviewerName.SelectedValue = string.Empty;
            cboSRResearchReviewerName.Text = string.Empty;
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ResearchLetter();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtLetterID.Text)))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new ResearchLetter();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ResearchLetter();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtLetterID.Text)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
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

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("LetterID='{0}'", txtLetterID.Text);
            auditLogFilter.TableName = "ResearchLetter";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_LetterID", txtLetterID.Text);
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !chkIsVoid.Checked;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            trUploadDocument.Visible = !isVisible && !chkIsVoid.Checked;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ResearchLetter();
            if (parameters.Length > 0)
            {
                string id = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(id));

                txtLetterID.Text = entity.LetterID.ToString();
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtLetterID.Text));
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var letter = (ResearchLetter)entity;

            txtLetterID.Text = letter.LetterID.ToString();
            txtLetterNo.Text = letter.LetterNo;
            txtLetterDate.SelectedDate = letter.LetterDate;
            txtResearcherName.Text = letter.ResearcherName;
            txtSubject.Text = letter.Subject;
            cboSRResearchDecision.SelectedValue = letter.SRResearchDecision;
            txtAttachment.Text = letter.Attachment;
            cboSRResearchInstitution.SelectedValue = letter.SRResearchInstitution;
            cboSRResearchFaculty.SelectedValue = letter.SRResearchFaculty;
            cboSRResearchMajors.SelectedValue = letter.SRResearchMajors;
            cboSREducationDegree.SelectedValue = letter.SREducationDegree;
            chkIsUpload.Checked = letter.IsUpload ?? false;
            txtReviewTime.Value = Convert.ToDouble(letter.ReviewTime);
            cboSRResearchReviewerName.SelectedValue = letter.SRResearchReviewerName;
            chkIsVoid.Checked = letter.IsVoid ?? false;
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new ResearchLetter();
            if (!entity.LoadByPrimaryKey(Convert.ToInt64(txtLetterID.Text)))
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

            SetVoid(entity, true);
        }

        private void SetVoid(ResearchLetter entity, bool isVoid)
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
        }

        private void SetEntityValue(ResearchLetter entity)
        {
            if (entity.es.IsModified)
                entity.LetterID = Convert.ToInt32(txtLetterID.Text);
            entity.LetterNo = txtLetterNo.Text;
            entity.LetterDate = txtLetterDate.SelectedDate;
            entity.ResearcherName = txtResearcherName.Text;
            entity.Subject = txtSubject.Text;
            entity.SRResearchDecision = cboSRResearchDecision.SelectedValue;
            entity.Attachment = txtAttachment.Text;
            entity.SRResearchInstitution = cboSRResearchInstitution.SelectedValue;
            entity.SRResearchFaculty = cboSRResearchFaculty.SelectedValue;
            entity.SRResearchMajors = cboSRResearchMajors.SelectedValue;
            entity.SREducationDegree = cboSREducationDegree.SelectedValue;
            entity.IsUpload = chkIsUpload.Checked;
            entity.ReviewTime = Convert.ToInt16(txtReviewTime.Value);
            entity.SRResearchReviewerName = cboSRResearchReviewerName.SelectedValue;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        }

        private void SaveEntity(ResearchLetter entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                try
                {
                    txtLetterID.Text = entity.LetterID.ToString();
                }
                catch
                { }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ResearchLetterQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.LetterID > Convert.ToInt32(txtLetterID.Text));
                que.OrderBy(que.LetterID.Ascending);
            }
            else
            {
                que.Where(que.LetterID < Convert.ToInt32(txtLetterID.Text));
                que.OrderBy(que.LetterID.Descending);
            }
            var entity = new ResearchLetter();
            entity.Load(que);

            txtLetterID.Text = entity.LetterID.ToString();

            OnPopulateEntryControl(entity);
        }
    }
}
using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using System.Linq;

namespace Temiang.Avicenna.Module.HR.Appraisal.Conclusion
{
    public partial class ConclusionDetail : BasePageDetail
    {
        private void SetEntityValue(AppraisalConclusion entity)
        {
            entity.ConclusionName = txtConclusionName.Text;
            entity.MinValue = Convert.ToDecimal(txtMinValue.Value);
            entity.MaxValue = Convert.ToDecimal(txtMaxValue.Value);

            //Last Update Status
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AppraisalConclusionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ConclusionID > txtConclusionID.Value.ToInt());
                que.OrderBy(que.ConclusionID.Ascending);
            }
            else
            {
                que.Where(que.ConclusionID < txtConclusionID.Value.ToInt());
                que.OrderBy(que.ConclusionID.Descending);
            }
            var entity = new AppraisalConclusion();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AppraisalConclusion();
            if (parameters.Length > 0)
            {
                String id = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id.ToInt());
            }
            else
            {
                entity.LoadByPrimaryKey(txtConclusionID.Value.ToInt());
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            AppraisalConclusion conc = (AppraisalConclusion)entity;
            txtConclusionID.Value = Convert.ToDouble(conc.ConclusionID);
            txtConclusionName.Text = conc.ConclusionName;
            txtMinValue.Value = Convert.ToDouble(conc.MinValue);
            txtMaxValue.Value = Convert.ToDouble(conc.MaxValue);
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AppraisalConclusion());
            txtConclusionID.Text = "0";
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
            auditLogFilter.PrimaryKeyData = string.Format("ConclusionID='{0}'", txtConclusionID.Text.Trim());
            auditLogFilter.TableName = "AppraisalConclusion";
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "##";
            UrlPageList = "ConclusionList.aspx";

            ProgramID = AppConstant.Program.AppraisalConclusion;

            txtConclusionID.Text = "0";
            //StandardReference Initialize
            if (!IsPostBack)
            {
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            AppraisalConclusion entity = new AppraisalConclusion();
            entity.LoadByPrimaryKey(txtConclusionID.Value.ToInt());
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new AppraisalConclusion();
            if (entity.LoadByPrimaryKey(txtConclusionID.Value.ToInt()))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new AppraisalConclusion();

            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(AppraisalConclusion entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                
                //Commit if success, Rollback if failed
                trans.Complete();
         
                txtConclusionID.Text = entity.ConclusionID.ToString();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            AppraisalConclusion entity = new AppraisalConclusion();
            if (entity.LoadByPrimaryKey(txtConclusionID.Value.ToInt()))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion

    }
}
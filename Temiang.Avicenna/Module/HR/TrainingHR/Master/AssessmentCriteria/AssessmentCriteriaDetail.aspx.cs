using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.HR.TrainingHR.Master.AssessmentCriteria
{
    public partial class AssessmentCriteriaDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "AssessmentCriteriaSearch.aspx";
            UrlPageList = "AssessmentCriteriaList.aspx";

            ProgramID = AppConstant.Program.EmployeeTrainingAssessmentCriteria; //TODO: Isi ProgramID

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new EmployeeTrainingAssessmentCriteria());
            txtAssessmentCriteriaID.Value = 0;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new EmployeeTrainingAssessmentCriteria();
            if (entity.LoadByPrimaryKey(txtAssessmentCriteriaID.Text.ToInt()))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new EmployeeTrainingAssessmentCriteria();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new EmployeeTrainingAssessmentCriteria();
            if (entity.LoadByPrimaryKey(txtAssessmentCriteriaID.Text.ToInt()))
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

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            //auditLogFilter.PrimaryKeyData = string.Format("AssessmentCriteriaID='{0}'", txtAssessmentCriteriaID.Text.Trim());
            //auditLogFilter.TableName = "EmployeeTrainingAssessmentCriteria";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new EmployeeTrainingAssessmentCriteria();
            if (parameters.Length > 0)
            {
                var itemId = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(itemId));
            }
            else
                entity.LoadByPrimaryKey(Convert.ToInt32(txtAssessmentCriteriaID.Text));

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var criteria = (EmployeeTrainingAssessmentCriteria)entity;

            txtAssessmentCriteriaID.Value = criteria.AssessmentCriteriaID;
            txtAssessmentCriteriaName.Text = criteria.AssessmentCriteriaName;
            txtMinValue.Value = Convert.ToDouble(criteria.MinValue);
            txtMaxValue.Value = Convert.ToDouble(criteria.MaxValue);
            txtRecommendation.Text = criteria.Recommendation;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
        }
        #endregion

        #region Private Method Standard

        private void SetEntityValue(EmployeeTrainingAssessmentCriteria entity)
        {
            entity.AssessmentCriteriaID = Convert.ToInt32(txtAssessmentCriteriaID.Value);
            entity.AssessmentCriteriaName = txtAssessmentCriteriaName.Text;
            entity.MinValue = Convert.ToDecimal(txtMinValue.Value);
            entity.MaxValue = Convert.ToDecimal(txtMaxValue.Value);
            entity.Recommendation = txtRecommendation.Text;

            if (entity.es.IsAdded)
            {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
            }

            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(EmployeeTrainingAssessmentCriteria entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();

                txtAssessmentCriteriaID.Value = entity.AssessmentCriteriaID;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new EmployeeTrainingAssessmentCriteriaQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.AssessmentCriteriaID > txtAssessmentCriteriaID.Value
                    );
                que.OrderBy(que.AssessmentCriteriaID.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.AssessmentCriteriaID < txtAssessmentCriteriaID.Value
                    );
                que.OrderBy(que.AssessmentCriteriaID.Descending);
            }

            var entity = new EmployeeTrainingAssessmentCriteria();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion
    }
}
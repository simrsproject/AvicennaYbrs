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

namespace Temiang.Avicenna.Module.HR.TrainingHR.Master.AssessmentAspect
{
    public partial class AssessmentAspectDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "AssessmentAspectSearch.aspx";
            UrlPageList = "AssessmentAspectList.aspx";

            ProgramID = AppConstant.Program.EmployeeTrainingAssessmentAspect; //TODO: Isi ProgramID

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
            OnPopulateEntryControl(new EmployeeTrainingAssessmentAspect());

            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new EmployeeTrainingAssessmentAspect();
            if (entity.LoadByPrimaryKey(txtAssessmentAspectID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new EmployeeTrainingAssessmentAspect();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new EmployeeTrainingAssessmentAspect();
            if (entity.LoadByPrimaryKey(txtAssessmentAspectID.Text))
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
            //auditLogFilter.PrimaryKeyData = string.Format("ItemID='{0}'", txtTypeOfLaborID.Text.Trim());
            //auditLogFilter.TableName = "AppStandardReferenceItem";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtAssessmentAspectID.ReadOnly = !(newVal == AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new EmployeeTrainingAssessmentAspect();
            if (parameters.Length > 0)
            {
                var itemId = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(itemId);
            }
            else
                entity.LoadByPrimaryKey(txtAssessmentAspectID.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var aspect = (EmployeeTrainingAssessmentAspect)entity;

            txtAssessmentAspectID.Text = aspect.AssessmentAspectID;
            txtAssessmentAspectName.Text = aspect.AssessmentAspectName;
            txtMinValue.Value = Convert.ToDouble(aspect.MinValue);
            txtMaxValue.Value = Convert.ToDouble(aspect.MaxValue);
            chkIsActive.Checked = aspect.IsActive ?? false;
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

        private void SetEntityValue(EmployeeTrainingAssessmentAspect entity)
        {
            entity.AssessmentAspectID = txtAssessmentAspectID.Text;
            entity.AssessmentAspectName = txtAssessmentAspectName.Text;
            entity.MinValue = Convert.ToDecimal(txtMinValue.Value);
            entity.MaxValue = Convert.ToDecimal(txtMaxValue.Value);
            entity.IsActive = chkIsActive.Checked;

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

        private void SaveEntity(EmployeeTrainingAssessmentAspect entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new EmployeeTrainingAssessmentAspectQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.AssessmentAspectID > txtAssessmentAspectID.Text
                    );
                que.OrderBy(que.AssessmentAspectID.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.AssessmentAspectID < txtAssessmentAspectID.Text
                    );
                que.OrderBy(que.AssessmentAspectID.Descending);
            }

            var entity = new EmployeeTrainingAssessmentAspect();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion
    }
}
using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class CoorporateGradeDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "CoorporateGradeSearch.aspx";
            UrlPageList = "CoorporateGradeList.aspx";

            ProgramID = AppConstant.Program.CoorporateGrade; //TODO: Isi ProgramID
            txtCoorporateGradeID.Text = "1";
            
            if (!IsPostBack)
            {
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new CoorporateGrade());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            CoorporateGrade entity = new CoorporateGrade();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtCoorporateGradeID.Text)))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new CoorporateGrade();
            var entitys = new CoorporateGradeQuery();
            entitys.Where(entitys.CoorporateGradeLevel == txtCoorporateGradeLevel.Value, entitys.CoorporateGradeID != txtCoorporateGradeID.Value);
            if (entity.Load(entitys))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                return;
            }

            entity = new CoorporateGrade();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new CoorporateGrade();
            var entitys = new CoorporateGradeQuery();
            entitys.Where(entitys.CoorporateGradeLevel == txtCoorporateGradeLevel.Value, entitys.CoorporateGradeID != txtCoorporateGradeID.Value);
            if (entity.Load(entitys))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                return;
            }

            entity = new CoorporateGrade();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtCoorporateGradeID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("CoorporateGradeID='{0}'", txtCoorporateGradeID.Text.Trim());
            auditLogFilter.TableName = "CoorporateGrade";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtCoorporateGradeID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            CoorporateGrade entity = new CoorporateGrade();
            if (parameters.Length > 0)
            {
                string id = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(id));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtCoorporateGradeID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            CoorporateGrade grade = (CoorporateGrade)entity;
            txtCoorporateGradeID.Value = Convert.ToDouble(grade.CoorporateGradeID);
            txtCoorporateGradeLevel.Value = Convert.ToDouble(grade.CoorporateGradeLevel);
            txtCoorporateGradeMin.Value = Convert.ToDouble(grade.CoorporateGradeMin);
            txtCoorporateGradeMax.Value = Convert.ToDouble(grade.CoorporateGradeMax);
            txtCoorporateGradeInterval.Value = Convert.ToDouble(grade.CoorporateGradeInterval);
            txtCoorporateGradeCoefficient.Value = Convert.ToDouble(grade.CoorporateGradeCoefficient ?? 0);
        }
        #endregion

        #region Private Method Standard
        private void SetEntityValue(CoorporateGrade entity)
        {
            entity.CoorporateGradeID = Convert.ToInt32(txtCoorporateGradeID.Value);
            entity.CoorporateGradeLevel = Convert.ToInt16(txtCoorporateGradeLevel.Value);
            entity.CoorporateGradeMin = Convert.ToDecimal(txtCoorporateGradeMin.Value);
            entity.CoorporateGradeMax = Convert.ToDecimal(txtCoorporateGradeMax.Value);
            entity.CoorporateGradeInterval = Convert.ToDecimal(txtCoorporateGradeInterval.Value);
            entity.CoorporateGradeCoefficient = Convert.ToDecimal(txtCoorporateGradeCoefficient.Value);

            if (entity.es.IsAdded)
            {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = DateTime.Now;
            }

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(CoorporateGrade entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                //Commit if success, Rollback if failed
                trans.Complete();

                txtCoorporateGradeID.Value = entity.CoorporateGradeID;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new CoorporateGradeQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.CoorporateGradeID > txtCoorporateGradeID.Text);
                que.OrderBy(que.CoorporateGradeID.Ascending);
            }
            else
            {
                que.Where(que.CoorporateGradeID < txtCoorporateGradeID.Text);
                que.OrderBy(que.CoorporateGradeID.Descending);
            }
            CoorporateGrade entity = new CoorporateGrade();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion
    }
}
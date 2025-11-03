using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionGradeDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PositionGradeSearch.aspx";
            UrlPageList = "PositionGradeList.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.PositionGrade; //TODO: Isi ProgramID

			//StandardReference Initialize
			if (!IsPostBack)
            {
                trInterval.Visible = false;
                StandardReference.InitializeIncludeSpace(cboSREmploymentType, AppEnum.StandardReference.EmploymentType);
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
            OnPopulateEntryControl(new PositionGrade());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            PositionGrade entity = new PositionGrade();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPositionGradeID.Text)))
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
            PositionGrade entity = new PositionGrade();
            
            entity = new PositionGrade();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            PositionGrade entity = new PositionGrade();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPositionGradeID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("PositionGradeID='{0}'", txtPositionGradeID.Text.Trim());
            auditLogFilter.TableName = "PositionGrade";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            PositionGrade entity = new PositionGrade();
            if (parameters.Length > 0)
            {
                string positionGradeID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(positionGradeID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtPositionGradeID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            PositionGrade positionGrade = (PositionGrade)entity;
            txtPositionGradeID.Value = Convert.ToDouble(positionGrade.PositionGradeID);
            txtPositionGradeCode.Text = positionGrade.PositionGradeCode;
            txtPositionGradeName.Text = positionGrade.PositionGradeName;
            txtInterval.Text = positionGrade.Interval;
            txtRanking.Value = Convert.ToDouble(positionGrade.Ranking);
            txtRankName.Text = positionGrade.RankName;
            cboSREmploymentType.SelectedValue = positionGrade.SREmploymentType;
            txtLowerLimit.Value = Convert.ToDouble(positionGrade.LowerLimit);
            txtUpperLimit.Value = Convert.ToDouble(positionGrade.UpperLimit);
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(PositionGrade entity)
        {
            entity.PositionGradeCode = txtPositionGradeCode.Text;
            entity.PositionGradeName = txtPositionGradeName.Text;
            entity.Interval = txtInterval.Text;
            entity.Ranking = Convert.ToInt16(txtRanking.Value);
            entity.RankName = txtRankName.Text;
            entity.SREmploymentType = cboSREmploymentType.SelectedValue;
            entity.LowerLimit = Convert.ToDecimal(txtLowerLimit.Value);
            entity.UpperLimit = Convert.ToDecimal(txtUpperLimit.Value);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(PositionGrade entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            PositionGradeQuery que = new PositionGradeQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PositionGradeID > txtPositionGradeID.Text);
                que.OrderBy(que.PositionGradeID.Ascending);
            }
            else
            {
                que.Where(que.PositionGradeID < txtPositionGradeID.Text);
                que.OrderBy(que.PositionGradeID.Descending);
            }
            PositionGrade entity = new PositionGrade();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged
        

        #endregion
    }
}

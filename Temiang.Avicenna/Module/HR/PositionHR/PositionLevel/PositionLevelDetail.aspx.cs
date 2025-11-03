using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionLevelDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PositionLevelSearch.aspx";
            UrlPageList = "PositionLevelList.aspx";

            ProgramID = AppConstant.Program.PositionLevel; //TODO: Isi ProgramID

			//StandardReference Initialize
			if (!IsPostBack)
            {
            }
			
			//PopUp Search
			if (!IsCallback)
			{
	            //PopUpSearch.Initialize(AppEnum.PopUpSearch.LastUpdateByUser, Page, txtLastUpdateByUserID);
				
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
            OnPopulateEntryControl(new PositionLevel());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            PositionLevel entity = new PositionLevel();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPositionLevelID.Text)))
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
            PositionLevel entity = new PositionLevel();
            
            entity = new PositionLevel();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            PositionLevel entity = new PositionLevel();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPositionLevelID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("PositionLevelID='{0}'", txtPositionLevelID.Text.Trim());
            auditLogFilter.TableName = "PositionLevel";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtPositionLevelID.Enabled = (newVal == AppEnum.DataMode.New);

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            PositionLevel entity = new PositionLevel();
            if (parameters.Length > 0)
            {
                string positionLevelID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(positionLevelID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtPositionLevelID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            PositionLevel positionLevel = (PositionLevel)entity;
            txtPositionLevelID.Value = Convert.ToDouble(positionLevel.PositionLevelID);
            txtPositionLevelCode.Text = positionLevel.PositionLevelCode;
            txtPositionLevelName.Text = positionLevel.PositionLevelName;
            txtRanking.Value = Convert.ToDouble(positionLevel.Ranking);

        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(PositionLevel entity)
        {
            entity.PositionLevelID = Convert.ToInt32(txtPositionLevelID.Value);
            entity.PositionLevelCode = txtPositionLevelCode.Text;
            entity.PositionLevelName = txtPositionLevelName.Text;
            entity.Ranking = Convert.ToInt16(txtRanking.Value);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(PositionLevel entity)
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
            PositionLevelQuery que = new PositionLevelQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PositionLevelID > txtPositionLevelID.Text);
                que.OrderBy(que.PositionLevelID.Ascending);
            }
            else
            {
                que.Where(que.PositionLevelID < txtPositionLevelID.Text);
                que.OrderBy(que.PositionLevelID.Descending);
            }
            PositionLevel entity = new PositionLevel();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged
        
        #endregion
    }
}

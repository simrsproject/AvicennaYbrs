using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Master
{
    public partial class AwardDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "AwardSearch.aspx";
            UrlPageList = "AwardList.aspx";

            ProgramID = AppConstant.Program.Award; //TODO: Isi ProgramID     


			//StandardReference Initialize
			if (!IsPostBack)
            {
				StandardReference.InitializeIncludeSpace(cboSRAwardCriteria, AppEnum.StandardReference.AwardCriteria);	
				StandardReference.InitializeIncludeSpace(cboSRAwardType, AppEnum.StandardReference.AwardType);	
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
            OnPopulateEntryControl(new Award());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Award entity = new Award();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtAwardID.Text)))
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
            Award entity = new Award();            
            entity = new Award();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Award entity = new Award();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtAwardID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("AwardID='{0}'", txtAwardID.Text.Trim());
            auditLogFilter.TableName = "Award";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtAwardID.Enabled = (newVal == AppEnum.DataMode.New);

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Award entity = new Award();
            if (parameters.Length > 0)
            {
                string awardID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(awardID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtAwardID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            Award award = (Award)entity;
            txtAwardID.Value = Convert.ToDouble(award.AwardID);
            txtAwardCode.Text = award.AwardCode;
            txtAwardName.Text = award.AwardName;
            cboSRAwardCriteria.SelectedValue = award.SRAwardCriteria;
            cboSRAwardType.SelectedValue = award.SRAwardType;
            txtValidFrom.SelectedDate = award.ValidFrom;
            txtValidTo.SelectedDate = award.ValidTo;
            txtAwardPrize.Text = award.AwardPrize;
            txtNote.Text = award.Note;

        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(Award entity)
        {
            entity.AwardID = Convert.ToInt32(txtAwardID.Value);
            entity.AwardCode = txtAwardCode.Text;
            entity.AwardName = txtAwardName.Text;
            entity.SRAwardCriteria = cboSRAwardCriteria.SelectedValue;
            entity.SRAwardType = cboSRAwardType.SelectedValue;
            entity.ValidFrom = txtValidFrom.SelectedDate;
            entity.ValidTo = txtValidTo.SelectedDate;
            entity.AwardPrize = txtAwardPrize.Text;
            entity.Note = txtNote.Text;


            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(Award entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                //Commit if success, Rollback if failed
                trans.Complete();

                txtAwardID.Value = entity.AwardID;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            AwardQuery que = new AwardQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.AwardID > txtAwardID.Text);
                que.OrderBy(que.AwardID.Ascending);
            }
            else
            {
                que.Where(que.AwardID < txtAwardID.Text);
                que.OrderBy(que.AwardID.Descending);
            }
            Award entity = new Award();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        #endregion
    }
}

using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ControlPanel.Setting
{
    public partial class TransactionCodeDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "TransactionCodeSearch.aspx";
            UrlPageList = "TransactionCodeList.aspx";

            ProgramID = AppConstant.Program.TransactionCodeNumbering;

			//StandardReference Initialize
			if (!IsPostBack)
            {
				StandardReference.InitializeIncludeSpace(cboSRTransactionCode, AppEnum.StandardReference.TransactionCode);	
				StandardReference.InitializeIncludeSpace(cboSRAutoNumber, AppEnum.StandardReference.AutoNumber);	
            }
			
			//PopUp Search
			if (!IsCallback)
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
            OnPopulateEntryControl(new AppAutoNumberTransactionCode());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            AppAutoNumberTransactionCode entity = new AppAutoNumberTransactionCode();
            entity.LoadByPrimaryKey(cboSRTransactionCode.SelectedValue);
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            AppAutoNumberTransactionCode entity = new AppAutoNumberTransactionCode();
            if (entity.LoadByPrimaryKey(cboSRTransactionCode.SelectedValue))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new AppAutoNumberTransactionCode();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            AppAutoNumberTransactionCode entity = new AppAutoNumberTransactionCode();
            if (entity.LoadByPrimaryKey(cboSRTransactionCode.SelectedValue))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
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
            auditLogFilter.PrimaryKeyData = string.Format("SRTransactionCode='{0}'", cboSRTransactionCode.SelectedValue.Trim());
            auditLogFilter.TableName = "AppAutoNumberTransactionCode";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            cboSRTransactionCode.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            AppAutoNumberTransactionCode entity = new AppAutoNumberTransactionCode();
            if (parameters.Length > 0)
            {
                String sRTransactionCode = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(sRTransactionCode);
            }
            else
            {
                entity.LoadByPrimaryKey(cboSRTransactionCode.SelectedValue);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            AppAutoNumberTransactionCode appAutoNumberTransactionCode = (AppAutoNumberTransactionCode)entity;
            cboSRTransactionCode.SelectedValue = appAutoNumberTransactionCode.SRTransactionCode;
            cboSRAutoNumber.SelectedValue = appAutoNumberTransactionCode.SRAutoNumber;
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(AppAutoNumberTransactionCode entity)
        {
            entity.SRTransactionCode = cboSRTransactionCode.SelectedValue;
            entity.SRAutoNumber = cboSRAutoNumber.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(AppAutoNumberTransactionCode entity)
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
            AppAutoNumberTransactionCodeQuery que = new AppAutoNumberTransactionCodeQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.SRTransactionCode > cboSRTransactionCode.SelectedValue);
                que.OrderBy(que.SRTransactionCode.Ascending);
            }
            else
            {
                que.Where(que.SRTransactionCode < cboSRTransactionCode.SelectedValue);
                que.OrderBy(que.SRTransactionCode.Descending);
            }
            AppAutoNumberTransactionCode entity = new AppAutoNumberTransactionCode();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion
    }
}
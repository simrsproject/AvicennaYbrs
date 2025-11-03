using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ControlPanel.Setting
{
    public partial class ParameterDetail : BasePageDetail
    {
        private void SetEntityValue(AppParameter entity)
        {
            entity.ParameterID = txtParameterID.Text;
            entity.ParameterName = txtParameterName.Text;
            entity.ParameterValue = txtParameterValue.Text;
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            AppParameterQuery que = new AppParameterQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ParameterID > txtParameterID.Text);
                que.OrderBy(que.ParameterID.Ascending);
            }
            else
            {
                que.Where(que.ParameterID < txtParameterID.Text);
                que.OrderBy(que.ParameterID.Descending);
            }
            AppParameter entity = new AppParameter();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            AppParameter entity = new AppParameter();
            if (parameters.Length > 0)
            {
                String parameterID = (String)parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(parameterID);
            }
            else
                entity.LoadByPrimaryKey(txtParameterID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            AppParameter appParameter = (AppParameter)entity;
            txtParameterID.Text = appParameter.ParameterID;
            txtParameterName.Text = appParameter.ParameterName;
            txtParameterValue.Text = appParameter.ParameterValue;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }


        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AppParameter());
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
            auditLogFilter.PrimaryKeyData = "ParameterID='" + txtParameterID.Text.Trim() + "'";
            auditLogFilter.TableName = "AppParameter";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtParameterID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "ParameterSearch.aspx";
            UrlPageList = "ParameterList.aspx";

            ProgramID = AppConstant.Program.ParameterSetting;
        }
        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            AppParameter entity = new AppParameter();
            entity.LoadByPrimaryKey(txtParameterID.Text);
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            AppParameter entity = new AppParameter();
            if (entity.LoadByPrimaryKey(txtParameterID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new AppParameter();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(AppParameter entity)
        {
            bool isModified = false;
            if (entity.es.IsModified)
            {
                isModified = true;
            }
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            //Update Chace parameter
            if (isModified)
                AppSession.Parameter.SetParameterValue(entity.ParameterID, entity.ParameterValue);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            AppParameter entity = new AppParameter();
            if (entity.LoadByPrimaryKey(txtParameterID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion
    }
}
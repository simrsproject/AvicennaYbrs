using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.ControlPanel
{
    public partial class AuditLogSettingDetail : BasePageDetail
    {
        private void SetEntityValue(AuditLogSetting entity)
        {
            entity.TableName = txtTableName.Text;
            entity.TableDescription = txtTableDescription.Text;
            entity.IsAuditLog = chkIsAuditLog.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            AuditLogSettingQuery que = new AuditLogSettingQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TableName > txtTableName.Text);
                que.OrderBy(que.TableName.Ascending);
            }
            else
            {
                que.Where(que.TableName < txtTableName.Text);
                que.OrderBy(que.TableName.Descending);
            }
            AuditLogSetting entity = new AuditLogSetting();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            AuditLogSetting entity = new AuditLogSetting();
            if (parameters.Length > 0)
            {
                String tableName = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(tableName);
            }
            else
                entity.LoadByPrimaryKey(txtTableName.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            AuditLogSetting auditLogSetting = (AuditLogSetting)entity;
            txtTableName.Text = auditLogSetting.TableName;
            txtTableDescription.Text = auditLogSetting.TableDescription;
            chkIsAuditLog.Checked = auditLogSetting.IsAuditLog ?? false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AuditLogSetting());
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
            auditLogFilter.PrimaryKeyData = "TableName='" + txtTableName.Text.Trim() + "'";
            auditLogFilter.TableName = "AuditLogSetting";
        }

        protected override void OnDataModeChanged(Temiang.Avicenna.Common.AppEnum.DataMode oldVal, Temiang.Avicenna.Common.AppEnum.DataMode newVal)
        {
            txtTableName.Enabled = (newVal == Temiang.Avicenna.Common.AppEnum.DataMode.New);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "AuditLogSettingSearch.aspx";
            UrlPageList = "AuditLogSettingList.aspx";

            ProgramID = AppConstant.Program.AuditLogSetting;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            AuditLogSetting entity = new AuditLogSetting();
            entity.LoadByPrimaryKey(txtTableName.Text);
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            AuditLogSetting entity = new AuditLogSetting();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(AuditLogSetting entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            AuditLogSetting entity = new AuditLogSetting();
            if (entity.LoadByPrimaryKey(txtTableName.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion
    }
}

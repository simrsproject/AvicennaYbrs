using System;
using System.Collections.Generic;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class IndicationDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "IndicationSearch.aspx";
            UrlPageList = "IndicationList.aspx";

            ProgramID = AppConstant.Program.Indication;
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
            OnPopulateEntryControl(new Indication());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Indication entity = new Indication();
            if (entity.LoadByPrimaryKey(txtIndicationID.Text))
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
            Indication entity = new Indication();
            if (entity.LoadByPrimaryKey(txtIndicationID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new Indication();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Indication entity = new Indication();
            if (entity.LoadByPrimaryKey(txtIndicationID.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("IndicationID='{0}'", txtIndicationID.Text.Trim());
            auditLogFilter.TableName = "Indication";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtIndicationID.Enabled = (newVal == AppEnum.DataMode.New);

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Indication entity = new Indication();
            if (parameters.Length > 0)
            {
                String IndicationID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(IndicationID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtIndicationID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            Indication Indication = (Indication)entity;
            txtIndicationID.Text = Indication.IndicationID;
            txtIndicationName.Text = Indication.IndicationName;
            chkIsActive.Checked = Indication.IsActive.HasValue ? Indication.IsActive.Value : true;
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(Indication entity)
        {
            entity.IndicationID = txtIndicationID.Text;
            entity.IndicationName = txtIndicationName.Text;
            entity.IsActive = chkIsActive.Checked;

            if (entity.es.IsAdded)
            {
                entity.InsertByUserID = AppSession.UserLogin.UserID;
                entity.InsertDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }else if (entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(Indication entity)
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
            IndicationQuery que = new IndicationQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.IndicationID > txtIndicationID.Text);
                que.OrderBy(que.IndicationID.Ascending);
            }
            else
            {
                que.Where(que.IndicationID < txtIndicationID.Text);
                que.OrderBy(que.IndicationID.Descending);
            }

            Indication entity = new Indication();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion
    }
}

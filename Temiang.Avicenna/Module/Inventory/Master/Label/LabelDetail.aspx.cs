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
    public partial class LabelDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "LabelSearch.aspx";
            UrlPageList = "LabelList.aspx";

            ProgramID = AppConstant.Program.Label;
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
            OnPopulateEntryControl(new Labell());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Labell entity = new Labell();
            if (entity.LoadByPrimaryKey(txtLabelID.Text))
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
            Labell entity = new Labell();
            if (entity.LoadByPrimaryKey(txtLabelID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new Labell();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Labell entity = new Labell();
            if (entity.LoadByPrimaryKey(txtLabelID.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("LabelID='{0}'", txtLabelID.Text.Trim());
            auditLogFilter.TableName = "Label";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtLabelID.Enabled = (newVal == AppEnum.DataMode.New);

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Labell entity = new Labell();
            if (parameters.Length > 0)
            {
                String LabelID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(LabelID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtLabelID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            Labell Label = (Labell)entity;
            txtLabelID.Text = Label.LabelID;
            txtLabelName.Text = Label.LabelName;
            chkIsActive.Checked = Label.IsActive.HasValue ? Label.IsActive.Value : true;
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(Labell entity)
        {
            entity.LabelID = txtLabelID.Text;
            entity.LabelName = txtLabelName.Text;
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

        private void SaveEntity(Labell entity)
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
            LabellQuery que = new LabellQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.LabelID > txtLabelID.Text);
                que.OrderBy(que.LabelID.Ascending);
            }
            else
            {
                que.Where(que.LabelID < txtLabelID.Text);
                que.OrderBy(que.LabelID.Descending);
            }

            Labell entity = new Labell();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion
    }
}

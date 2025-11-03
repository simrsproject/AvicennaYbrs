using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class SnackDetail : BasePageDetail
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "SnackSearch.aspx";
            UrlPageList = "SnackList.aspx";

            ProgramID = AppConstant.Program.Snack;
        }

        private void SetEntityValue(Snack entity)
        {
            entity.SnackID = txtSnackID.Text;
            entity.SnackName = txtSnackName.Text;
            entity.IsActive = chkIsActive.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new SnackQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.SnackID > txtSnackID.Text);
                que.OrderBy(que.SnackID.Ascending);
            }
            else
            {
                que.Where(que.SnackID < txtSnackID.Text);
                que.OrderBy(que.SnackID.Descending);
            }
            var entity = new Snack();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Snack();
            if (parameters.Length > 0)
            {
                String snackId = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(snackId);
            }
            else
                entity.LoadByPrimaryKey(txtSnackID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var snack = (Snack)entity;
            txtSnackID.Text = snack.SnackID;
            txtSnackName.Text = snack.SnackName;
            chkIsActive.Checked = snack.IsActive ?? false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Snack());
            chkIsActive.Checked = true;
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
            auditLogFilter.PrimaryKeyData = "SnackID='" + txtSnackID.Text.Trim() + "'";
            auditLogFilter.TableName = "Snack";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtSnackID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new Snack();
            if (entity.LoadByPrimaryKey(txtSnackID.Text))
            {
                entity.MarkAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new Snack();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(Snack entity)
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
            var entity = new Snack();
            if (entity.LoadByPrimaryKey(txtSnackID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion
    }
}

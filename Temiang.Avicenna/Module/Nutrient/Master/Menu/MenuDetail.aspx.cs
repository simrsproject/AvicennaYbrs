using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;


namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class MenuDetail : BasePageDetail
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "MenuSearch.aspx";
            UrlPageList = "MenuList.aspx";

            ProgramID = AppConstant.Program.Menu;
        }

        private void SetEntityValue(Menu entity)
        {
            entity.MenuID = txtMenuID.Text;
            entity.MenuName = txtMenuName.Text;
            entity.IsExtra = chkIsExtra.Checked;
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
            var que = new MenuQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.MenuID > txtMenuID.Text);
                que.OrderBy(que.MenuID.Ascending);
            }
            else
            {
                que.Where(que.MenuID < txtMenuID.Text);
                que.OrderBy(que.MenuID.Descending);
            }
            var entity = new Menu();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Menu();
            if (parameters.Length > 0)
            {
                String MenuId = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(MenuId);
            }
            else
                entity.LoadByPrimaryKey(txtMenuID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var menu = (Menu)entity;
            txtMenuID.Text = menu.MenuID;
            txtMenuName.Text = menu.MenuName;
            chkIsExtra.Checked = menu.IsExtra ?? false;
            chkIsActive.Checked = menu.IsActive ?? false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Menu());
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
            auditLogFilter.PrimaryKeyData = "MenuID='" + txtMenuID.Text.Trim() + "'";
            auditLogFilter.TableName = "Menu";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtMenuID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new Menu();
            if (entity.LoadByPrimaryKey(txtMenuID.Text))
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
            var entity = new Menu();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(Menu entity)
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
            var entity = new Menu();
            if (entity.LoadByPrimaryKey(txtMenuID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion
    }
}

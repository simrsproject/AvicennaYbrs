using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class MenuVersionDetail : BasePageDetail
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "MenuVersionSearch.aspx?ext=" + Request.QueryString["ext"];
            UrlPageList = "MenuVersionList.aspx?ext=" + Request.QueryString["ext"];

            ProgramID = Request.QueryString["ext"] == "0" ? AppConstant.Program.MenuVersion : AppConstant.Program.MenuExtraVersion;
        }

        private void SetEntityValue(MenuVersion entity)
        {
            entity.VersionID = txtVersionID.Text;
            entity.VersionName = txtVersionName.Text;
            entity.Cycle = Convert.ToInt16(txtCycle.Value);
            entity.IsPlusOneRule = chkIsPlusOneRule.Checked;
            entity.IsActive = chkIsActive.Checked;
            entity.IsExtra = Request.QueryString["ext"] != "0";

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new MenuVersionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.VersionID > txtVersionID.Text);
                que.OrderBy(que.VersionID.Ascending);
            }
            else
            {
                que.Where(que.VersionID < txtVersionID.Text);
                que.OrderBy(que.VersionID.Descending);
            }

            if (Request.QueryString["ext"] == "0")
                que.Where(que.IsExtra == false);
            else
                que.Where(que.IsExtra == true);


            var entity = new MenuVersion();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new MenuVersion();
            if (parameters.Length > 0)
            {
                String versionId = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(versionId);
            }
            else
                entity.LoadByPrimaryKey(txtVersionID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var version = (MenuVersion)entity;
            txtVersionID.Text = version.VersionID;
            txtVersionName.Text = version.VersionName;
            txtCycle.Value = Convert.ToDouble(version.Cycle);
            chkIsPlusOneRule.Checked = version.IsPlusOneRule ?? false;
            chkIsActive.Checked = version.IsActive ?? false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new MenuVersion());
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
            auditLogFilter.PrimaryKeyData = "VersionID='" + txtVersionID.Text.Trim() + "'";
            auditLogFilter.TableName = "MenuVersion";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtVersionID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new MenuVersion();
            if (entity.LoadByPrimaryKey(txtVersionID.Text))
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
            var entity = new MenuVersion();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(MenuVersion entity)
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
            var entity = new MenuVersion();
            if (entity.LoadByPrimaryKey(txtVersionID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion
    }
}

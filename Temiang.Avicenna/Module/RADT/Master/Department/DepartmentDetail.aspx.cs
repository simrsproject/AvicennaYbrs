using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class DepartmentDetail : BasePageDetail
    {
        private void SetEntityValue(Department entity)
        {
            entity.DepartmentID = txtDepartmentID.Text;
            entity.DepartmentName = txtDepartmentName.Text;
            entity.ShortName = txtShortName.Text;
            entity.Initial = txtInitial.Text;
            entity.DepartmentManager = txtDepartmentManager.Text;
            entity.IsHasRegistration = chkIsHasRegistration.Checked;
            entity.IsAllowBackDateRegistration = chkIsAllowBackDateRegistration.Checked;
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
            DepartmentQuery que = new DepartmentQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.DepartmentID > txtDepartmentID.Text);
                que.OrderBy(que.DepartmentID.Ascending);
            }
            else
            {
                que.Where(que.DepartmentID < txtDepartmentID.Text);
                que.OrderBy(que.DepartmentID.Descending);
            }
            Department entity = new Department();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Department entity = new Department();
            if (parameters.Length > 0)
            {
                String departmentID = (String)parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(departmentID);
            }
            else
                entity.LoadByPrimaryKey(txtDepartmentID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            Department department = (Department)entity;
            txtDepartmentID.Text = department.DepartmentID;
            txtDepartmentName.Text = department.DepartmentName;
            txtShortName.Text = department.ShortName;
            txtInitial.Text = department.Initial;
            txtDepartmentManager.Text = department.DepartmentManager;
            chkIsHasRegistration.Checked = department.IsHasRegistration ?? false;
            chkIsAllowBackDateRegistration.Checked = department.IsAllowBackDateRegistration ?? false;
            chkIsActive.Checked = department.IsActive ?? false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Department());
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
            auditLogFilter.PrimaryKeyData = "DepartmentID='" + txtDepartmentID.Text.Trim() + "'";
            auditLogFilter.TableName = "Department";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtDepartmentID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "DepartmentSearch.aspx";
            UrlPageList = "DepartmentList.aspx";

            ProgramID = AppConstant.Program.Department;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Department entity = new Department();
            entity.LoadByPrimaryKey(txtDepartmentID.Text);
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Department entity = new Department();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(Department entity)
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
            Department entity = new Department();
            if (entity.LoadByPrimaryKey(txtDepartmentID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion
    }
}

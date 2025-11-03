using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class SalaryTemplateDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "SalaryTemplateSearch.aspx";
            UrlPageList = "SalaryTemplateList.aspx";

            ProgramID = AppConstant.Program.SalaryTemplate; //TODO: Isi ProgramID

            //StandardReference Initialize
            if (!IsPostBack)
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
            OnPopulateEntryControl(new SalaryTemplate());
            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            SalaryTemplateItemCollection collDetail = new SalaryTemplateItemCollection();
            SalaryTemplate entity = new SalaryTemplate();
            if (entity.LoadByPrimaryKey(txtSalaryTemplateID.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity, collDetail);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            SalaryTemplate entity = new SalaryTemplate();
            if (entity.LoadByPrimaryKey(txtSalaryTemplateID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            SalaryTemplateItemCollection collDetail = new SalaryTemplateItemCollection();
            entity = new SalaryTemplate();
            entity.AddNew();
            SetEntityValue(entity, collDetail);
            SaveEntity(entity, collDetail);

        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            SalaryTemplateItemCollection collDetail = new SalaryTemplateItemCollection();
            SalaryTemplate entity = new SalaryTemplate();
            if (entity.LoadByPrimaryKey(txtSalaryTemplateID.Text))
            {
                SetEntityValue(entity, collDetail);
                SaveEntity(entity, collDetail);
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
            auditLogFilter.PrimaryKeyData = string.Format("SalaryTemplateID='{0}'", txtSalaryTemplateID.Text.Trim());
            auditLogFilter.TableName = "SalaryTemplate";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtSalaryTemplateID.Enabled = (newVal == AppEnum.DataMode.New);

            grdSalaryTemplateItem.Columns[0].Visible = (newVal != AppEnum.DataMode.Read);
            RefreshDataSourceGridDetail();

            //Refresh Selection Check
            switch (newVal)
            {
                case AppEnum.DataMode.New:
                    foreach (GridDataItem dataItem in grdSalaryTemplateItem.MasterTableView.Items)
                        dataItem.Selected = false;

                    break;
                case AppEnum.DataMode.Edit:
                    foreach (GridDataItem dataItem in grdSalaryTemplateItem.MasterTableView.Items)
                        dataItem.Selected = (bool)dataItem.GetDataKeyValue("IsSelect");

                    break;
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            SalaryTemplate entity = new SalaryTemplate();
            if (parameters.Length > 0)
            {
                string salaryTemplateID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(salaryTemplateID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtSalaryTemplateID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            SalaryTemplate salaryTemplate = (SalaryTemplate)entity;
            txtSalaryTemplateID.Text = salaryTemplate.SalaryTemplateID;
            txtSalaryTemplateName.Text = salaryTemplate.SalaryTemplateName;
            chkIsActive.Checked = salaryTemplate.IsActive ?? false;

            if (IsPostBack)
            {
                //Display Data Detail
                RefreshDataSourceGridDetail();
            }
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(SalaryTemplate entity, SalaryTemplateItemCollection collDetail)
        {
            entity.SalaryTemplateID = txtSalaryTemplateID.Text;
            entity.SalaryTemplateName = txtSalaryTemplateName.Text;
            entity.IsActive = chkIsActive.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Salary Template Item
            collDetail.Query.Where(collDetail.Query.SalaryTemplateID == txtSalaryTemplateID.Text);
            collDetail.LoadAll();

            foreach (GridDataItem dataItem in grdSalaryTemplateItem.MasterTableView.Items)
            {
                SalaryTemplateItem ugItem;
                Int32 salaryComponentID = Convert.ToInt32(dataItem.GetDataKeyValue("SalaryComponentID"));
                ugItem = collDetail.FindByPrimaryKey(txtSalaryTemplateID.Text, salaryComponentID);
                if (dataItem.Selected)
                {
                    if (ugItem == null)
                    {
                        ugItem = collDetail.AddNew();
                        ugItem.SalaryTemplateID = txtSalaryTemplateID.Text;
                        ugItem.SalaryComponentID = salaryComponentID;
                    }

                    if (ugItem.es.IsAdded || ugItem.es.IsModified)
                    {
                        ugItem.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        ugItem.LastUpdateDateTime = DateTime.Now;
                    }
                }
                else if (ugItem != null)
                    ugItem.MarkAsDeleted();
            }
        }

        private void SaveEntity(SalaryTemplate entity, SalaryTemplateItemCollection collDetail)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                //Save Detail
                collDetail.Save();

                entity.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            SalaryTemplateQuery que = new SalaryTemplateQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.SalaryTemplateID > txtSalaryTemplateID.Text);
                que.OrderBy(que.SalaryTemplateID.Ascending);
            }
            else
            {
                que.Where(que.SalaryTemplateID < txtSalaryTemplateID.Text);
                que.OrderBy(que.SalaryTemplateID.Descending);
            }
            SalaryTemplate entity = new SalaryTemplate();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        #endregion

        #region Record Detail Method Function SalaryTemplateItem
        protected void grdSalaryTemplateItem_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdSalaryTemplateItem.DataSource = SalaryTemplateItems;
        }

        private DataTable SalaryTemplateItems
        {
            get
            {
                object obj = this.Session["SalaryComponentSelections"];
                if (obj != null)
                    return ((DataTable)(obj));

                var coll = new SalaryTemplateItemCollection();
                DataTable dtb = DataModeCurrent == AppEnum.DataMode.Read
                                    ? coll.GetInnerJoinWithSalaryComponent(txtSalaryTemplateID.Text)
                                    : coll.GetFullJoinWithSalaryComponent(txtSalaryTemplateID.Text);

                Session["SalaryComponentSelections"] = dtb;
                return dtb;
            }
        }

        private void RefreshDataSourceGridDetail()
        {
            Session["SalaryComponentSelections"] = null;
            grdSalaryTemplateItem.Rebind();
        }
        #endregion
    }
}

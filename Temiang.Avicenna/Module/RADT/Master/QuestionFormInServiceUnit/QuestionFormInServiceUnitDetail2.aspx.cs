using System;
using System.Data;
using System.IO;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class QuestionFormInServiceUnitDetail2 : BasePageDetail
    {
        private void SetEntityValue(QuestionFormInServiceUnitCollection questionFormInServiceUnits)
        {
            //QuestionFormInServiceUnit
            questionFormInServiceUnits.Query.Where(questionFormInServiceUnits.Query.ServiceUnitID == txtServiceUnitID.Text);
            questionFormInServiceUnits.LoadAll();

            foreach (GridDataItem dataItem in grdQuestionForm.MasterTableView.Items)
            {
                QuestionFormInServiceUnit item;
                string questionFormId = dataItem.GetDataKeyValue("QuestionFormID").ToString();
                item = questionFormInServiceUnits.FindByPrimaryKey(txtServiceUnitID.Text, questionFormId);
                if (dataItem.Selected)
                {
                    if (item == null)
                    {
                        item = questionFormInServiceUnits.AddNew();
                        item.ServiceUnitID = txtServiceUnitID.Text;
                        item.QuestionFormID = questionFormId;
                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        item.LastUpdateDateTime = DateTime.Now;
                    }
                }
                else
                    if (item != null)
                    item.MarkAsDeleted();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ServiceUnitQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ServiceUnitID > txtServiceUnitID.Text);
                que.Where(que.SRRegistrationType != string.Empty, que.IsActive == true);
                que.OrderBy(que.ServiceUnitID.Ascending);
            }
            else
            {
                que.Where(que.ServiceUnitID < txtServiceUnitID.Text);
                que.Where(que.SRRegistrationType != string.Empty, que.IsActive == true);
                que.OrderBy(que.ServiceUnitID.Descending);
            }
            var entity = new ServiceUnit();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ServiceUnit();
            if (parameters.Length > 0)
            {
                String unitId = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(unitId);
            }
            else
                entity.LoadByPrimaryKey(txtServiceUnitID.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var unit = (ServiceUnit)entity;
            txtServiceUnitID.Text = unit.ServiceUnitID;
            txtServiceUnitName.Text = unit.ServiceUnitName;

            //Refresh Detail
            if (IsPostBack)
            {
                RefreshGridQuestionForm();
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
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
            //auditLogFilter.PrimaryKeyData = "ServiceUnitID='" + txtServiceUnitID.Text.Trim() + "'";
            //auditLogFilter.TableName = "ServiceUnit";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Refresh Grid Detail
            grdQuestionForm.Columns[0].Visible = (newVal != AppEnum.DataMode.Read);
            RefreshGridQuestionForm();

            //Refresh Selection Check
            switch (newVal)
            {
                case AppEnum.DataMode.New:
                    foreach (GridDataItem dataItem in grdQuestionForm.MasterTableView.Items)
                        dataItem.Selected = false;

                    break;
                case AppEnum.DataMode.Edit:
                    foreach (GridDataItem dataItem in grdQuestionForm.MasterTableView.Items)
                        dataItem.Selected = (bool)dataItem.GetDataKeyValue("IsSelect");

                    break;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "QuestionFormInServiceUnitSearch.aspx";
            UrlPageList = "QuestionFormInServiceUnitList.aspx";

            ProgramID = AppConstant.Program.QuestionFormInServiceUnit;

            if (!IsPostBack)
            {
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {

        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var questionForms = new QuestionFormInServiceUnitCollection();
            SetEntityValue(questionForms);
            SaveEntity(questionForms);
        }

        private void SaveEntity(QuestionFormInServiceUnitCollection questionForms)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                // Save Detail
                questionForms.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var questionForms = new QuestionFormInServiceUnitCollection();
            SetEntityValue(questionForms);
            SaveEntity(questionForms);
        }


        #endregion

        #region UserUserGroup

        protected void grdQuestionForm_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdQuestionForm.DataSource = DetailQuestionForm;
        }

        private DataTable DetailQuestionForm
        {
            get
            {
                object obj = this.Session["QuestionFormInServiceUnitColl"];
                if (obj != null)
                    return ((DataTable)(obj));

                var coll = new QuestionFormInServiceUnitCollection();
                DataTable dtb = DataModeCurrent == AppEnum.DataMode.Read
                                    ? coll.GetInnerJoinWQuestionForm(txtServiceUnitID.Text)
                                    : coll.GetFullJoinWQuestionForm(txtServiceUnitID.Text);

                Session["QuestionFormInServiceUnitColl"] = dtb;
                return dtb;
            }
        }

        private void RefreshGridQuestionForm()
        {
            Session["QuestionFormInServiceUnitColl"] = null;
            grdQuestionForm.Rebind();
        }

        #endregion
    }
}
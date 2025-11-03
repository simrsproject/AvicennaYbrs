using System;
using System.Data;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.HR.K3RS
{
    public partial class TemplateDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "TemplateSearch.aspx";
            UrlPageList = "TemplateList.aspx";

            ProgramID = AppConstant.Program.K3RS_FormTemplate;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new K3rsFormTemplate());

            txtTemplateID.Text = "0";
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var forms = new K3rsFormCollection();
            forms.Query.Where(forms.Query.TemplateID == Convert.ToInt32(txtTemplateID.Text));
            forms.LoadAll();
            if (forms.Count > 0)
            {
                args.MessageText = AppConstant.Message.RecordHasUsed;
                args.IsCancel = true;
                return;
            }

            var entity = new K3rsFormTemplate();
            entity.LoadByPrimaryKey(Convert.ToInt32(txtTemplateID.Text));
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new K3rsFormTemplate();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new K3rsFormTemplate();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtTemplateID.Text)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
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
            auditLogFilter.PrimaryKeyData = string.Format("TemplateID='{0}'", txtTemplateID.Text.ToInt());
            auditLogFilter.TableName = "K3rsFormTemplate";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtTemplateID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new K3rsFormTemplate();
            if (parameters.Length > 0)
            {
                Int32 id = Convert.ToInt32(parameters[0]);
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id);
            }
            else
                entity.LoadByPrimaryKey(Convert.ToInt32(txtTemplateID.Text));
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var template = (K3rsFormTemplate)entity;
            if (template != null) txtTemplateID.Text = Convert.ToString(template.TemplateID);
            else txtTemplateID.Text = "0";
            txtTemplateName.Text = template.TemplateName;
            txtResult.Content = template.Result;
        }
        #endregion

        #region Private Method Standard
        private void SetEntityValue(K3rsFormTemplate entity)
        {
            entity.TemplateID = Convert.ToInt32(txtTemplateID.Text);
            entity.TemplateName = txtTemplateName.Text;
            entity.Result = txtResult.Content;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(K3rsFormTemplate entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                txtTemplateID.Text = entity.TemplateID.ToString();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new K3rsFormTemplateQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TemplateID > txtTemplateID.Text);
                que.OrderBy(que.TemplateID.Ascending);
            }
            else
            {
                que.Where(que.TemplateID < txtTemplateID.Text);
                que.OrderBy(que.TemplateID.Descending);
            }
            var entity = new K3rsFormTemplate();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion
    }
}
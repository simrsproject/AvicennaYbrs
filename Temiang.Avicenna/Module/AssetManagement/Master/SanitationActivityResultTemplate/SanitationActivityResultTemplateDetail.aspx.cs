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

namespace Temiang.Avicenna.Module.AssetManagement.Master
{
    public partial class SanitationActivityResultTemplateDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "SanitationActivityResultTemplateSearch.aspx";
            UrlPageList = "SanitationActivityResultTemplateList.aspx";

            ProgramID = AppConstant.Program.SanitationActivityResultTemplate;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                var sr = new AppStandardReferenceItemCollection();
                sr.Query.Where(sr.Query.StandardReferenceID == "WorkTradeItem", sr.Query.ReferenceID == AppSession.Parameter.WorkTradeSanitation);
                sr.Query.Where(sr.Query.IsActive == true);
                sr.LoadAll();

                cboSRWorkTradeItem.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (AppStandardReferenceItem entity in sr)
                {
                    cboSRWorkTradeItem.Items.Add(new RadComboBoxItem(entity.ItemName, entity.ItemID));
                }
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
            OnPopulateEntryControl(new SanitationActivityResultTemplate());

            txtSanitationActivityResultID.Text = "0";
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new SanitationActivityResultTemplate();
            entity.LoadByPrimaryKey(Convert.ToInt32(txtSanitationActivityResultID.Text));
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboSRWorkTradeItem.SelectedValue))
            {
                args.MessageText = "Selected Work Trade Item is not valid, please select exist item.";
                args.IsCancel = true;
                return;
            }

            var entity = new SanitationActivityResultTemplate();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboSRWorkTradeItem.SelectedValue))
            {
                args.MessageText = "Selected Work Trade Item is not valid, please select exist item.";
                args.IsCancel = true;
                return;
            }

            var entity = new SanitationActivityResultTemplate();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtSanitationActivityResultID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("SanitationActivityResultID='{0}'", txtSanitationActivityResultID.Text.ToInt());
            auditLogFilter.TableName = "SanitationActivityResultTemplate";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtSanitationActivityResultID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new SanitationActivityResultTemplate();
            if (parameters.Length > 0)
            {
                Int32 id = Convert.ToInt32(parameters[0]);
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id);
            }
            else
                entity.LoadByPrimaryKey(Convert.ToInt32(txtSanitationActivityResultID.Text));
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var template = (SanitationActivityResultTemplate)entity;
            if (template != null) txtSanitationActivityResultID.Text = Convert.ToString(template.SanitationActivityResultID);
            else txtSanitationActivityResultID.Text = "0";
            cboSRWorkTradeItem.SelectedValue = template.SRWorkTradeItem;
            txtResultTemplateName.Text = template.ResultTemplateName;
            txtResult.Content = template.Result;
        }
        #endregion

        #region Private Method Standard
        private void SetEntityValue(SanitationActivityResultTemplate entity)
        {
            entity.SanitationActivityResultID = Convert.ToInt32(txtSanitationActivityResultID.Text);
            entity.SRWorkTradeItem = cboSRWorkTradeItem.SelectedValue;
            entity.ResultTemplateName = txtResultTemplateName.Text;
            entity.Result = txtResult.Content;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(SanitationActivityResultTemplate entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                txtSanitationActivityResultID.Text = entity.SanitationActivityResultID.ToString();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new SanitationActivityResultTemplateQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.SanitationActivityResultID > txtSanitationActivityResultID.Text);
                que.OrderBy(que.SanitationActivityResultID.Ascending);
            }
            else
            {
                que.Where(que.SanitationActivityResultID < txtSanitationActivityResultID.Text);
                que.OrderBy(que.SanitationActivityResultID.Descending);
            }
            var entity = new SanitationActivityResultTemplate();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion
    }
}
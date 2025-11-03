using System;
using System.IO;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Telerik.Web.UI.ImageEditor;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ImageTemplateDetail : BasePageDetail
    {
        private void SetEntityValue(ImageTemplate entity)
        {
            entity.SRImageTemplateType = cboSRImageTemplateType.SelectedValue;
            entity.ImageTemplateID = txtImageTemplateID.Text;
            entity.ImageTemplateName = txtImageTemplateName.Text;
            entity.Description = txtDescription.Text;
            if (Context.Cache[Session.SessionID + "_ImageTemplate"] != null)
                entity.Image = (byte[])Context.Cache[Session.SessionID + "_ImageTemplate"];

        }

        private void MoveRecord(bool isNextRecord)
        {
            ImageTemplateQuery que = new ImageTemplateQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ImageTemplateID > txtImageTemplateID.Text);
                que.OrderBy(que.ImageTemplateID.Ascending);
            }
            else
            {
                que.Where(que.ImageTemplateID < txtImageTemplateID.Text);
                que.OrderBy(que.ImageTemplateID.Descending);
            }
            ImageTemplate entity = new ImageTemplate();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ImageTemplate entity = new ImageTemplate();
            if (parameters.Length > 0)
            {
                String procedureID = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(procedureID);
            }
            else
                entity.LoadByPrimaryKey(txtImageTemplateID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            ImageTemplate ent = (ImageTemplate)entity;
            ComboBox.SelectedValue(cboSRImageTemplateType, ent.SRImageTemplateType);
            txtImageTemplateID.Text = ent.ImageTemplateID;
            txtImageTemplateName.Text = ent.ImageTemplateName;
            imgImageTemplate.DataValue = ent.Image;
            
            Context.Cache.Remove(Session.SessionID + "_ImageTemplate");
            if (ent.Image != null)
                Context.Cache.Insert(Session.SessionID + "_ImageTemplate", ent.Image, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
            else
                imgImageTemplate.ImageUrl = ""; // Untuk menghilangkan image
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(AjaxManager, uplImageTemplate);
            ajax.AddAjaxSetting(AjaxManager, imgImageTemplate);
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ImageTemplate());
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
            auditLogFilter.PrimaryKeyData = "ImageTemplateID='" + txtImageTemplateID.Text.Trim() + "'";
            auditLogFilter.TableName = "ImageTemplate";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtImageTemplateID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "ImageTemplateSearch.aspx";
            UrlPageList = "ImageTemplateList.aspx";

            ProgramID = AppConstant.Program.ImageTemplate;

            StandardReference.InitializeIncludeSpace(cboSRImageTemplateType,
                AppEnum.StandardReference.ImageTemplateType);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            ImageTemplate entity = new ImageTemplate();
            entity.LoadByPrimaryKey(txtImageTemplateID.Text);
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            ImageTemplate entity = new ImageTemplate();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(ImageTemplate entity)
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
            ImageTemplate entity = new ImageTemplate();
            if (entity.LoadByPrimaryKey(txtImageTemplateID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion

        protected void uplImageTemplate_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            using (Stream stream = e.File.InputStream)
            {
                byte[] imgData = new byte[stream.Length];
                stream.Read(imgData, 0, imgData.Length);
                imgImageTemplate.DataValue = imgData;

                Context.Cache.Remove(Session.SessionID + "_ImageTemplate");
                Context.Cache.Insert(Session.SessionID + "_ImageTemplate", imgData, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);

            }

        }
    }
}
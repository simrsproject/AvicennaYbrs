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
    public partial class BodyDiagramDetail : BasePageDetail
    {
        private void SetEntityValue(BodyDiagram entity)
        {
            entity.BodyID = txtBodyID.Text;
            entity.BodyName = txtBodyName.Text;
            entity.Description = txtDescription.Text;
            if (Context.Cache[Session.SessionID + "_MasterBodyImage"] != null)
                entity.BodyImage =(byte[]) Context.Cache[Session.SessionID + "_MasterBodyImage"];

        }

        private void MoveRecord(bool isNextRecord)
        {
            BodyDiagramQuery que = new BodyDiagramQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.BodyID > txtBodyID.Text);
                que.OrderBy(que.BodyID.Ascending);
            }
            else
            {
                que.Where(que.BodyID < txtBodyID.Text);
                que.OrderBy(que.BodyID.Descending);
            }
            BodyDiagram entity = new BodyDiagram();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            BodyDiagram entity = new BodyDiagram();
            if (parameters.Length > 0)
            {
                String procedureID = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(procedureID);
            }
            else
                entity.LoadByPrimaryKey(txtBodyID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            BodyDiagram ent = (BodyDiagram)entity;
            txtBodyID.Text = ent.BodyID;
            txtBodyName.Text = ent.BodyName;
            imgBodyImage.DataValue= ent.BodyImage;

            Context.Cache.Remove(Session.SessionID + "_MasterBodyImage");
            if (ent.BodyImage!=null)
                Context.Cache.Insert(Session.SessionID + "_MasterBodyImage", ent.BodyImage, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
            else
                imgBodyImage.ImageUrl = ""; // Untuk menghilangkan image

        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(AjaxManager, uplBodyImage);
            ajax.AddAjaxSetting(AjaxManager, imgBodyImage);
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new BodyDiagram());
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
            auditLogFilter.PrimaryKeyData = "BodyID='" + txtBodyID.Text.Trim() + "'";
            auditLogFilter.TableName = "Body";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtBodyID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "BodyDiagramSearch.aspx";
            UrlPageList = "BodyDiagramList.aspx";

            ProgramID = AppConstant.Program.BodyDiagram;


        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            BodyDiagram entity = new BodyDiagram();
            entity.LoadByPrimaryKey(txtBodyID.Text);
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            BodyDiagram entity = new BodyDiagram();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(BodyDiagram entity)
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
            BodyDiagram entity = new BodyDiagram();
            if (entity.LoadByPrimaryKey(txtBodyID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion

        protected void uplBodyImage_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            using (Stream stream = e.File.InputStream)
            {
                byte[] imgData = new byte[stream.Length];
                stream.Read(imgData, 0, imgData.Length);
                imgBodyImage.DataValue = imgData;

                Context.Cache.Remove(Session.SessionID + "_MasterBodyImage");
                Context.Cache.Insert(Session.SessionID + "_MasterBodyImage", imgData, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
            }

        }

    }
}
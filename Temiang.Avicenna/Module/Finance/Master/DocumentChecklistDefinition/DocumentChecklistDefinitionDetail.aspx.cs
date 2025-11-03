using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class DocumentChecklistDefinitionDetail : BasePageDetail
    {
        public object DataItem { get; set; }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "DocumentChecklistDefinitionSearch.aspx";
            UrlPageList = "DocumentChecklistDefinitionList.aspx";

            ProgramID = AppConstant.Program.GuarantorDocumentChecklistDefinition; //TODO: Isi ProgramID

            //StandardReference Initialize
            if (!IsPostBack)
            {
            }

            //PopUp Search
            if (!IsCallback)
            {
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AppStandardReferenceItem());
            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new AppStandardReferenceItem();
            entity.LoadByPrimaryKey(AppEnum.StandardReference.DocumentChecklist.ToString(), txtItemID.Text);
            entity.MarkAsDeleted();

            //UserGroupProgram
            var collDetail = new DocumentChecklistDefinitionCollection();
            collDetail.Query.Where(collDetail.Query.SRDocumentChecklist == txtItemID.Text);
            collDetail.LoadAll();
            collDetail.MarkAllAsDeleted();

            //Save
            using (var trans = new esTransactionScope())
            {
                //Save Detail
                collDetail.Save();

                //Save Header
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.DocumentChecklist.ToString(), txtItemID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            var detail = new DocumentChecklistDefinitionCollection();
            entity = new AppStandardReferenceItem();
            entity.AddNew();
            SetEntityValue(entity, detail);
            SaveEntity(entity, detail);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.DocumentChecklist.ToString(), txtItemID.Text))
            {
                var detail = new DocumentChecklistDefinitionCollection();
                SetEntityValue(entity, detail);
                SaveEntity(entity, detail);
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
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtItemID.Enabled = (newVal == AppEnum.DataMode.New);
            
            grdDocumentFiles.Columns[0].Visible = (newVal != AppEnum.DataMode.Read);
            RefreshGridgrdDocumentFiles();

            //Refresh Selection Check
            switch (newVal)
            {
                case AppEnum.DataMode.New:
                    foreach (GridDataItem dataItem in grdDocumentFiles.MasterTableView.Items)
                        dataItem.Selected = false;

                    break;
                case AppEnum.DataMode.Edit:
                    foreach (GridDataItem dataItem in grdDocumentFiles.MasterTableView.Items)
                        dataItem.Selected = (bool)dataItem.GetDataKeyValue("IsSelect");

                    break;
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AppStandardReferenceItem();
            if (parameters.Length > 0)
            {
                string itemId = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(AppEnum.StandardReference.DocumentChecklist.ToString(), itemId);
            }
            else
            {
                entity.LoadByPrimaryKey(AppEnum.StandardReference.DocumentChecklist.ToString(), txtItemID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var doc = (AppStandardReferenceItem)entity;
            txtItemID.Text = doc.ItemID;
            txtItemName.Text = doc.ItemName;
            chkIsActive.Checked = doc.IsActive ?? false;

            if (IsPostBack)
                RefreshGridgrdDocumentFiles();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(AppStandardReferenceItem entity, DocumentChecklistDefinitionCollection detail)
        {
            entity.StandardReferenceID = AppEnum.StandardReference.DocumentChecklist.ToString();
            entity.ItemID = txtItemID.Text;
            entity.ItemName = txtItemName.Text;
            entity.IsActive = chkIsActive.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //User ServiceUnit
            detail.Query.Where(detail.Query.SRDocumentChecklist == txtItemID.Text);
            detail.LoadAll();

            var lineCount = 0;

            foreach (GridDataItem dataItem in grdDocumentFiles.MasterTableView.Items)
            {
                DocumentChecklistDefinition item;
                string documentFilesId = dataItem.GetDataKeyValue("DocumentFilesID").ToString();
                item = detail.FindByPrimaryKey(txtItemID.Text, documentFilesId.ToInt());
                if (dataItem.Selected)
                {
                    lineCount += 1;
                    if (item == null)
                    {
                        item = detail.AddNew();
                        item.SRDocumentChecklist = txtItemID.Text;
                        item.DocumentFilesID = documentFilesId.ToInt();
                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        item.LastUpdateDateTime = DateTime.Now;
                    }
                }
                else
                    if (item != null)
                        item.MarkAsDeleted();
            }
            entity.LineNumber = lineCount;
        }

        private void SaveEntity(AppStandardReferenceItem entity, DocumentChecklistDefinitionCollection detail)
        {
            using (var trans = new esTransactionScope())
            {
                //Save Header
                entity.Save();

                // Save Detail
                detail.Save();
                //ctlMatrix.SaveMatrix();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AppStandardReferenceItemQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.StandardReferenceID == AppEnum.StandardReference.DocumentChecklist.ToString(), que.ItemID > txtItemID.Text);
                que.OrderBy(que.ItemID.Ascending);
            }
            else
            {
                que.Where(que.StandardReferenceID == AppEnum.StandardReference.DocumentChecklist.ToString(), que.ItemID < txtItemID.Text);
                que.OrderBy(que.ItemID.Descending);
            }
            var entity = new AppStandardReferenceItem();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region DocumentChecklist

        protected void grdDocumentFiles_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdDocumentFiles.DataSource = DetailDocumentFiles;
        }

        private DataTable DetailDocumentFiles
        {
            get
            {
                object obj = this.Session["DetailDocumentChecklistDefinition"];
                if (obj != null)
                    return ((DataTable)(obj));

                var coll = new DocumentChecklistDefinitionCollection();
                DataTable dtb = DataModeCurrent == AppEnum.DataMode.Read
                                    ? coll.GetInnerJoinWDocumentDefinition(txtItemID.Text)
                                    : coll.GetFullJoinWDocumentDefinition(txtItemID.Text);

                Session["DetailDocumentChecklistDefinition"] = dtb;
                return dtb;
            }
        }

        private void RefreshGridgrdDocumentFiles()
        {
            Session["DetailDocumentChecklistDefinition"] = null;
            grdDocumentFiles.Rebind();
        }

        #endregion
    }
}
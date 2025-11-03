using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class DocumentDefinitionDetail : BasePageDetail
    {
        public object DataItem { get; set; }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "DocumentDefinitionSearch.aspx";
            UrlPageList = "DocumentDefinitionList.aspx";

            ProgramID = AppConstant.Program.DocumentDefinition; //TODO: Isi ProgramID

            //StandardReference Initialize
            if (!IsPostBack)
            {
                var unit = new ServiceUnitQuery("b");
                unit.Select(unit.DepartmentID);
                unit.Where(unit.SRRegistrationType != "");

                var query = new DepartmentQuery("a");
                query.Where(query.DepartmentID.In(unit));
                var coll = new DepartmentCollection();
                coll.Load(query);

                cboDepartmentID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Department d in coll)
                {
                    cboDepartmentID.Items.Add(new RadComboBoxItem(d.DepartmentName, d.DepartmentID));
                }

                StandardReference.InitializeIncludeSpace(cboSRFilesAnalysis, AppEnum.StandardReference.FilesAnalysis);
            }

            //PopUp Search
            if (!IsCallback)
            {
            }
        }

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    // Set Layout matrix Control
        //    ctlMatrix.SetColumnCaption("Number", "Document Name");
        //    ctlMatrix.SetFirstColumnWidth(120);
        //}

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new DocumentDefinition());
            chkIsActive.Checked = true;
            // Reset List in matrix
            //ctlMatrix.ResetDataSource();
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new DocumentDefinition();
            entity.LoadByPrimaryKey(Convert.ToInt32(txtDocumentDefinitionID.Text));
            entity.MarkAsDeleted();

            var collDetail = new DocumentDefinitionItemCollection();
            collDetail.Query.Where(collDetail.Query.DocumentDefinitionID == Convert.ToInt32(txtDocumentDefinitionID.Text));
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
            //var entity = new DocumentDefinition();
            //if (entity.LoadByPrimaryKey(Convert.ToInt32(txtDocumentDefinitionID.Text)))
            //{
            //    args.MessageText = AppConstant.Message.DuplicateKey;
            //    args.IsCancel = true;
            //    return;
            //}
            var coll = new DocumentDefinitionCollection();
            coll.Query.Where(coll.Query.DepartmentID == cboDepartmentID.SelectedValue,
                             coll.Query.SRFilesAnalysis == cboSRFilesAnalysis.SelectedValue);
            coll.LoadAll();
            if (coll.Count > 0)
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            var documentDefinitionItems = new DocumentDefinitionItemCollection();
            var entity = new DocumentDefinition();
            entity.AddNew();
            SetEntityValue(entity, documentDefinitionItems);
            SaveEntity(entity, documentDefinitionItems);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new DocumentDefinition();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtDocumentDefinitionID.Text)))
            {
                var documentDefinitionItems = new DocumentDefinitionItemCollection();
                SetEntityValue(entity, documentDefinitionItems);
                SaveEntity(entity, documentDefinitionItems);
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
            auditLogFilter.PrimaryKeyData = string.Format("DocumentDefinitionID='{0}'",
                                                          txtDocumentDefinitionID.Text.Trim());
            auditLogFilter.TableName = "DocumentDefinition";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtDocumentDefinitionID.Enabled = (newVal == AppEnum.DataMode.New);
            cboDepartmentID.Enabled = (newVal == AppEnum.DataMode.New);
            cboSRFilesAnalysis.Enabled = (newVal == AppEnum.DataMode.New);

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
            var entity = new DocumentDefinition();
            if (parameters.Length > 0)
            {
                string documentDefinitionID = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(documentDefinitionID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtDocumentDefinitionID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var documentDefinition = (DocumentDefinition) entity;
            if (documentDefinition.DocumentDefinitionID != null)
                txtDocumentDefinitionID.Value = Convert.ToDouble(documentDefinition.DocumentDefinitionID);
            else txtDocumentDefinitionID.Value = Convert.ToDouble(-1);

            cboDepartmentID.SelectedValue = documentDefinition.DepartmentID;
            cboSRFilesAnalysis.SelectedValue = documentDefinition.SRFilesAnalysis;
            chkIsActive.Checked = documentDefinition.IsActive ?? false;

            if (IsPostBack)
                RefreshGridgrdDocumentFiles();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(DocumentDefinition entity, DocumentDefinitionItemCollection documentDefinitionItems)
        {
            entity.DocumentDefinitionID = Convert.ToInt32(txtDocumentDefinitionID.Value);
            entity.DepartmentID = cboDepartmentID.SelectedValue;
            entity.SRFilesAnalysis = cboSRFilesAnalysis.SelectedValue;
            entity.IsActive = chkIsActive.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //User ServiceUnit
            documentDefinitionItems.Query.Where(documentDefinitionItems.Query.DocumentDefinitionID == txtDocumentDefinitionID.Value.ToInt());
            documentDefinitionItems.LoadAll();

            foreach (GridDataItem dataItem in grdDocumentFiles.MasterTableView.Items)
            {
                DocumentDefinitionItem item;
                string documentFilesId = dataItem.GetDataKeyValue("DocumentFilesID").ToString();
                item = documentDefinitionItems.FindByPrimaryKey(txtDocumentDefinitionID.Value.ToInt(), documentFilesId.ToInt());
                if (dataItem.Selected)
                {
                    if (item == null)
                    {
                        item = documentDefinitionItems.AddNew();
                        item.DocumentDefinitionID = txtDocumentDefinitionID.Value.ToInt();
                        item.DocumentFilesID = documentFilesId.ToInt();
                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        item.LastUpdateDateTime = DateTime.Now;
                    }
                }
                else
                    if (item != null)
                        item.MarkAsDeleted();
            }
        }

        private void SaveEntity(DocumentDefinition entity, DocumentDefinitionItemCollection documentDefinitionItems)
        {
            using (var trans = new esTransactionScope())
            {
                //Save Header
                entity.Save();

                // Required for new record, get new Identity DocumentDefinitionID
                txtDocumentDefinitionID.Text = entity.DocumentDefinitionID.ToString();

                foreach (var item in documentDefinitionItems)
                {
                    item.DocumentDefinitionID = txtDocumentDefinitionID.Text.ToInt();
                }

                // Save Detail
                documentDefinitionItems.Save();
                //ctlMatrix.SaveMatrix();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new DocumentDefinitionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.DocumentDefinitionID > txtDocumentDefinitionID.Text);
                que.OrderBy(que.DocumentDefinitionID.Ascending);
            }
            else
            {
                que.Where(que.DocumentDefinitionID < txtDocumentDefinitionID.Text);
                que.OrderBy(que.DocumentDefinitionID.Descending);
            }
            var entity = new DocumentDefinition();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged

        #endregion

        #region ComboBox Function

        protected void cboDepartmentID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateboDepartmentID((RadComboBox) sender, e.Text);
        }

        private void PopulateboDepartmentID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new DepartmentQuery("a");
            var unit = new ServiceUnitQuery("b");
            unit.Select(unit.DepartmentID);
            unit.Where
                (
                    unit.SRRegistrationType > ""
                );

            query.Where(query.DepartmentID.In(unit) &
                        query.DepartmentName.Like(searchTextContain));

            query.Select(query.DepartmentID, query.DepartmentName);

            query.es.Top = 10;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            //if (dtb.Rows.Count > 0)
            //{
            //    comboBox.SelectedValue = dtb.Rows[0]["DepartmentID"].ToString();
            //}
        }

        protected void cboDepartmentID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView) e.Item.DataItem)["DepartmentName"].ToString();
            e.Item.Value = ((DataRowView) e.Item.DataItem)["DepartmentID"].ToString();
        }

        #endregion ComboBox Function

        #region DocumentFiles

        protected void grdDocumentFiles_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdDocumentFiles.DataSource = DetailDocumentFiles;
        }

        private DataTable DetailDocumentFiles
        {
            get
            {
                object obj = this.Session["DetailDocumentFiles"];
                if (obj != null)
                    return ((DataTable)(obj));

                var coll = new DocumentDefinitionItemCollection();
                DataTable dtb = DataModeCurrent == Temiang.Avicenna.Common.AppEnum.DataMode.Read
                                    ? coll.GetInnerJoinWDocumentDefinition(txtDocumentDefinitionID.Value.ToInt())
                                    : coll.GetFullJoinWDocumentDefinition(txtDocumentDefinitionID.Value.ToInt());

                Session["DetailDocumentFiles"] = dtb;
                return dtb;
            }
        }

        private void RefreshGridgrdDocumentFiles()
        {
            Session["DetailDocumentFiles"] = null;
            grdDocumentFiles.Rebind();
        }

        #endregion
    }
}
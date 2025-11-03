using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.Module.RADT.Master;
using System.Data;
using System.Web;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class DiagnosisDetail : BasePageDetail
    {
        private void SetEntityValue(Diagnose entity)
        {
            entity.DiagnoseID = txtDiagnoseID.Text;
            entity.DiagnoseName = txtDiagnoseName.Text;
            entity.Synonym = txtSynonym.Text;
            entity.DtdNo = cboDtdNo.SelectedValue;
            entity.IsChronicDisease = chkIsChronicDisease.Checked;
            entity.IsDisease = chkIsDisease.Checked;
            entity.IsActive = chkIsActive.Checked;
            entity.IM = chkIsIM.Checked;
            entity.ValidCode = chkIsValidCode.Checked;
            entity.Asterisk = chkIsAsterisk.Checked;
            entity.Accpdx = chkIsAccpdx.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            foreach (var item in DiagnoseSynonyms)
            {
                item.DiagnoseID = txtDiagnoseID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new DiagnoseQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.DiagnoseID > txtDiagnoseID.Text);
                que.OrderBy(que.DiagnoseID.Ascending);
            }
            else
            {
                que.Where(que.DiagnoseID < txtDiagnoseID.Text);
                que.OrderBy(que.DiagnoseID.Descending);
            }
            var entity = new Diagnose();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Diagnose();
            if (parameters.Length > 0)
            {
                String diagId = parameters[0];
                diagId = diagId.Replace("s", " ").Replace("p", "+").Replace("c", ",");
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(diagId);
            }
            else
                entity.LoadByPrimaryKey(txtDiagnoseID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var diag = (Diagnose)entity;
            txtDiagnoseID.Text = diag.DiagnoseID;
            txtDiagnoseName.Text = diag.DiagnoseName;
            txtSynonym.Text = diag.Synonym;
            if (!string.IsNullOrEmpty(diag.DtdNo))
            {
                var dtdq = new DtdQuery();
                dtdq.Where(dtdq.DtdNo == diag.DtdNo);
                cboDtdNo.DataSource = dtdq.LoadDataTable();
                cboDtdNo.DataBind();
                cboDtdNo.SelectedValue = diag.DtdNo;
            }
            else
            {
                cboDtdNo.Items.Clear();
                cboDtdNo.Text = string.Empty;
            }
            chkIsChronicDisease.Checked = diag.IsChronicDisease ?? false;
            chkIsDisease.Checked = diag.IsDisease ?? false;
            chkIsActive.Checked = diag.IsActive ?? false;
            chkIsIM.Checked = diag.IM ?? false;
            chkIsValidCode.Checked = diag.ValidCode ?? false;
            chkIsAsterisk.Checked = diag.Asterisk ?? false;
            chkIsAccpdx.Checked = diag.Accpdx ?? false;

            PopulateDiagnoseSynonymGrid();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Diagnose());
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
            auditLogFilter.PrimaryKeyData = "DiagnoseNo='" + txtDiagnoseID.Text.Trim() + "'";
            auditLogFilter.TableName = "Diagnose";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtDiagnoseID.ReadOnly = (newVal != AppEnum.DataMode.New);
            RefreshCommandItemDiagnoseSynonym(newVal);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "DiagnosisSearch.aspx";
            UrlPageList = "DiagnosisList.aspx";

            ProgramID = AppConstant.Program.Diagnosis;

            if (!AppSession.Parameter.IsMultipleSynonymValueForDiagnoseAndProcedure)
                tableSynonym.Visible = false;
            else
                trSynonym.Visible = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new Diagnose();
            if (entity.LoadByPrimaryKey(txtDiagnoseID.Text))
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
            var entity = new Diagnose();
            if (entity.LoadByPrimaryKey(txtDiagnoseID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new Diagnose();

            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(Diagnose entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                DiagnoseSynonyms.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new Diagnose();
            if (entity.LoadByPrimaryKey(txtDiagnoseID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion

        #region Dtd
        protected void cboDtdNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["DtdName"] + " (" + ((DataRowView)e.Item.DataItem)["DtdNo"] + ")";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["DtdNo"].ToString();
        }

        protected void cboDtdNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new DtdQuery();
            query.es.Top = 15;
            query.Select
                (
                    query.DtdNo,
                    query.DtdName
                );
            query.Where
                (
                    query.Or
                        (
                            query.DtdName.Like(searchTextContain),
                            query.DtdNo.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );
            query.OrderBy(query.DtdNo.Ascending);
            cboDtdNo.DataSource = query.LoadDataTable();
            cboDtdNo.DataBind();
        }
        #endregion

        #region Record Detail Method Function Synonym
        private DiagnoseSynonymCollection DiagnoseSynonyms
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collDiagnoseSynonym"];
                    if (obj != null)
                    {
                        return ((DiagnoseSynonymCollection)(obj));
                    }
                }

                var coll = new DiagnoseSynonymCollection();
                var query = new DiagnoseSynonymQuery("a");

                query.SelectAll();
                query.Where(query.DiagnoseID == txtDiagnoseID.Text);
                coll.Load(query);

                Session["collDiagnoseSynonym"] = coll;
                return coll;
            }
            set
            {
                Session["collDiagnoseSynonym"] = value;
            }
        }

        private void RefreshCommandItemDiagnoseSynonym(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);

            grdDiagnoseSynonym.Columns[0].Visible = isVisible;

            grdDiagnoseSynonym.Columns[grdDiagnoseSynonym.Columns.Count - 2].Visible = isVisible;

            grdDiagnoseSynonym.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdDiagnoseSynonym.Rebind();
        }

        private void PopulateDiagnoseSynonymGrid()
        {
            //Display Data Detail
            DiagnoseSynonyms = null; //Reset Record Detail
            grdDiagnoseSynonym.DataSource = DiagnoseSynonyms; //Requery
            grdDiagnoseSynonym.MasterTableView.IsItemInserted = false;
            grdDiagnoseSynonym.MasterTableView.ClearEditItems();
            grdDiagnoseSynonym.DataBind();
        }

        private DiagnoseSynonym FindDiagnoseSynonym(String seqNo)
        {
            DiagnoseSynonymCollection coll = DiagnoseSynonyms;
            DiagnoseSynonym retEntity = null;
            foreach (DiagnoseSynonym rec in coll)
            {
                if (rec.SequenceNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdDiagnoseSynonym_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String seqNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][DiagnoseSynonymMetadata.ColumnNames.SequenceNo]);

            var entity = FindDiagnoseSynonym(seqNo);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdDiagnoseSynonym_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][DiagnoseSynonymMetadata.ColumnNames.SequenceNo]);

            DiagnoseSynonym entity = FindDiagnoseSynonym(seqNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdDiagnoseSynonym_InsertCommand(object source, GridCommandEventArgs e)
        {
            DiagnoseSynonym entity = DiagnoseSynonyms.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdDiagnoseSynonym.Rebind();
        }

        protected void grdDiagnoseSynonym_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDiagnoseSynonym.DataSource = DiagnoseSynonyms;
        }

        private void SetEntityValue(DiagnoseSynonym entity, GridCommandEventArgs e)
        {
            var userControl = (DiagnoseSynonymDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SequenceNo = userControl.SequenceNo;
                entity.SynonymText = userControl.SynonymText;
            }
        }
        #endregion
    }
}

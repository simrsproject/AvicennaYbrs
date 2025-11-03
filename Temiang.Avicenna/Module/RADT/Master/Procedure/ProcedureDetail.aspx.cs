using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ProcedureDetail : BasePageDetail
    {
        private void SetEntityValue(Procedure entity)
        {
            entity.ProcedureID = txtProcedureID.Text;
            entity.ProcedureName = txtProcedureName.Text;
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

            foreach (var item in ProcedureSynonyms)
            {
                item.ProcedureID = txtProcedureID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            ProcedureQuery que = new ProcedureQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ProcedureID > txtProcedureID.Text);
                que.OrderBy(que.ProcedureID.Ascending);
            }
            else
            {
                que.Where(que.ProcedureID < txtProcedureID.Text);
                que.OrderBy(que.ProcedureID.Descending);
            }
            Procedure entity = new Procedure();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Procedure entity = new Procedure();
            if (parameters.Length > 0)
            {
                String procedureID = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(procedureID);
            }
            else
                entity.LoadByPrimaryKey(txtProcedureID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            Procedure procedure = (Procedure) entity;
            txtProcedureID.Text = procedure.ProcedureID;
            txtProcedureName.Text = procedure.ProcedureName;
            chkIsIM.Checked = procedure.IM ?? false;
            chkIsValidCode.Checked = procedure.ValidCode ?? false;
            chkIsAsterisk.Checked = procedure.Asterisk ?? false;
            chkIsAccpdx.Checked = procedure.Accpdx ?? false;

            PopulateProcedureSynonymGrid();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Procedure());
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
            auditLogFilter.PrimaryKeyData = "ProcedureID='" + txtProcedureID.Text.Trim() + "'";
            auditLogFilter.TableName = "Procedure";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtProcedureID.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItemProcedureSynonym(newVal);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "ProcedureSearch.aspx";
            UrlPageList = "ProcedureList.aspx";

            ProgramID = AppConstant.Program.Procedure;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Procedure entity = new Procedure();
            entity.LoadByPrimaryKey(txtProcedureID.Text);
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Procedure entity = new Procedure();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(Procedure entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ProcedureSynonyms.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Procedure entity = new Procedure();
            if (entity.LoadByPrimaryKey(txtProcedureID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion


        #region Record Detail Method Function Synonym
        private ProcedureSynonymCollection ProcedureSynonyms
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collProcedureSynonym"];
                    if (obj != null)
                    {
                        return ((ProcedureSynonymCollection)(obj));
                    }
                }

                var coll = new ProcedureSynonymCollection();
                var query = new ProcedureSynonymQuery("a");

                query.SelectAll();
                query.Where(query.ProcedureID == txtProcedureID.Text);
                coll.Load(query);

                Session["collProcedureSynonym"] = coll;
                return coll;
            }
            set
            {
                Session["collProcedureSynonym"] = value;
            }
        }

        private void RefreshCommandItemProcedureSynonym(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);

            grdProcedureSynonym.Columns[0].Visible = isVisible;

            grdProcedureSynonym.Columns[grdProcedureSynonym.Columns.Count - 2].Visible = isVisible;

            grdProcedureSynonym.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdProcedureSynonym.Rebind();
        }

        private void PopulateProcedureSynonymGrid()
        {
            //Display Data Detail
            ProcedureSynonyms = null; //Reset Record Detail
            grdProcedureSynonym.DataSource = ProcedureSynonyms; //Requery
            grdProcedureSynonym.MasterTableView.IsItemInserted = false;
            grdProcedureSynonym.MasterTableView.ClearEditItems();
            grdProcedureSynonym.DataBind();
        }

        private ProcedureSynonym FindProcedureSynonym(String seqNo)
        {
            ProcedureSynonymCollection coll = ProcedureSynonyms;
            ProcedureSynonym retEntity = null;
            foreach (ProcedureSynonym rec in coll)
            {
                if (rec.SequenceNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdProcedureSynonym_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String seqNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ProcedureSynonymMetadata.ColumnNames.SequenceNo]);

            var entity = FindProcedureSynonym(seqNo);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdProcedureSynonym_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ProcedureSynonymMetadata.ColumnNames.SequenceNo]);

            ProcedureSynonym entity = FindProcedureSynonym(seqNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdProcedureSynonym_InsertCommand(object source, GridCommandEventArgs e)
        {
            ProcedureSynonym entity = ProcedureSynonyms.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdProcedureSynonym.Rebind();
        }

        protected void grdProcedureSynonym_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdProcedureSynonym.DataSource = ProcedureSynonyms;
        }

        private void SetEntityValue(ProcedureSynonym entity, GridCommandEventArgs e)
        {
            var userControl = (ProcedureSynonymDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SequenceNo = userControl.SequenceNo;
                entity.SynonymText = userControl.SynonymText;
            }
        }
        #endregion
    }
}
using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.Module.RADT.Master;

namespace Temiang.Avicenna.Module.Master
{
    public partial class DtdDetail : BasePageDetail
    {
        private void SetEntityValue(Dtd entity)
        {
            entity.DtdNo = txtDtdNo.Text;
            entity.DtdName = txtDtdName.Text;
            entity.DtdLabel = txtDtdLabel.Text;
            entity.IsActive = chkIsActive.Checked;
            entity.SRChronicDisease = cboSRChronicDisease.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Update Key
            DiagnoseCollection coll = Diagnoses;
            foreach (Diagnose item in coll)
            {
                item.DtdNo = txtDtdNo.Text;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            DtdQuery que = new DtdQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.DtdNo > txtDtdNo.Text);
                que.OrderBy(que.DtdNo.Ascending);
            }
            else
            {
                que.Where(que.DtdNo < txtDtdNo.Text);
                que.OrderBy(que.DtdNo.Descending);
            }
            Dtd entity = new Dtd();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Dtd entity = new Dtd();
            if (parameters.Length > 0)
            {
                String dtdNo = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(dtdNo);
            }
            else
                entity.LoadByPrimaryKey(txtDtdNo.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            Dtd dtd = (Dtd)entity;
            txtDtdNo.Text = dtd.DtdNo;
            txtDtdName.Text = dtd.DtdName;
            txtDtdLabel.Text = dtd.DtdLabel;
            chkIsActive.Checked = dtd.IsActive ?? false;
            StandardReference.InitializeWithOneRow(cboSRChronicDisease, AppEnum.StandardReference.ChronicDisease,dtd.SRChronicDisease??string.Empty);

            //Display Data Detail
            PopulateGridDetail();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Dtd());
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
            auditLogFilter.PrimaryKeyData = "DtdNo='" + txtDtdNo.Text.Trim() + "'";
            auditLogFilter.TableName = "Dtd";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtDtdNo.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "DtdSearch.aspx";
            UrlPageList = "DtdList.aspx";

            ProgramID = AppConstant.Program.DTD;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.Diagnose, this.Page);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new Dtd();
            if (entity.LoadByPrimaryKey(txtDtdNo.Text))
            {
                entity.MarkAsDeleted();

                var diagnoses = new DiagnoseCollection();
                diagnoses.Query.Where(diagnoses.Query.DtdNo == txtDtdNo.Text);
                diagnoses.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    diagnoses.Save();
                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new Dtd();
            if (entity.LoadByPrimaryKey(txtDtdNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new Dtd();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(Dtd entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                Diagnoses.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Dtd entity = new Dtd();
            if (entity.LoadByPrimaryKey(txtDtdNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion

        #region Record Detail Method Function

        private DiagnoseCollection Diagnoses
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collDiagnose"];
                    if (obj != null)
                        return ((DiagnoseCollection)(obj));
                }

                DiagnoseCollection coll = new DiagnoseCollection();
                DiagnoseQuery query = new DiagnoseQuery();

                string dtdNo = txtDtdNo.Text;
                query.Where(query.DtdNo == dtdNo);
                query.OrderBy(query.DiagnoseID.Ascending);
                coll.Load(query);

                Session["collDiagnose"] = coll;
                return coll;
            }
            set { Session["collDiagnose"] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdDiagnose.Columns[0].Visible = isVisible;
            grdDiagnose.Columns[grdDiagnose.Columns.Count - 1].Visible = isVisible;

            grdDiagnose.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                Diagnoses = null;

            //Perbaharui tampilan dan data
            grdDiagnose.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            Diagnoses = null; //Reset Record Detail
            grdDiagnose.DataSource = Diagnoses;
            grdDiagnose.DataBind();
        }

        protected void grdDiagnose_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDiagnose.DataSource = Diagnoses;
        }

        protected void grdDiagnose_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            String diagnoseID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][DiagnoseMetadata.ColumnNames.DiagnoseID]);
            Diagnose entity = FindItemGrid(diagnoseID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdDiagnose_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String diagnoseID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][DiagnoseMetadata.ColumnNames.DiagnoseID]);
            Diagnose entity = FindItemGrid(diagnoseID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdDiagnose_InsertCommand(object source, GridCommandEventArgs e)
        {
            Diagnose entity = Diagnoses.AddNew();
            SetEntityValue(entity, e);
        }

        private void SetEntityValue(Diagnose entity, GridCommandEventArgs e)
        {
            DiagnoseDetail userControl = (DiagnoseDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.DtdNo = txtDtdNo.Text;
                entity.DiagnoseID = userControl.DiagnoseID;
                entity.DiagnoseName = userControl.DiagnoseName;
                entity.IsChronicDisease = userControl.IsChronicDisease;
                entity.IsDisease = userControl.IsDisease;
                entity.IsActive = userControl.IsActive;
            }
        }

        private Diagnose FindItemGrid(string diagnoseID)
        {
            DiagnoseCollection coll = Diagnoses;
            Diagnose retval = null;
            foreach (Diagnose rec in coll)
            {
                if (rec.DiagnoseID.Equals(diagnoseID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        #endregion

        protected void cboSRChronicDisease_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested((RadComboBox)sender, "ChronicDisease", e.Text);
        }

        protected void cboSRChronicDisease_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }
    }
}
using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.TaxRegulation
{
    public partial class PtkpDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PtkpSearch.aspx";
            UrlPageList = "PtkpList.aspx";

            ProgramID = AppConstant.Program.Ptkp; //TODO: Isi ProgramID

            //StandardReference Initialize
            if (!IsPostBack)
            {
                txtPtkpID.Value = -1;

                StandardReference.InitializeIncludeSpace(cboSRTaxStatus, AppEnum.StandardReference.TaxStatus);
            }

            //PopUp Search
            if (!IsCallback)
            {

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            txtPtkpID.Value = -1;

            OnPopulateEntryControl(new Ptkp());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Ptkp entity = new Ptkp();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPtkpID.Text)))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Ptkp entity = new Ptkp();
            entity = new Ptkp();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Ptkp entity = new Ptkp();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPtkpID.Text)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
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
            auditLogFilter.PrimaryKeyData = string.Format("PtkpID='{0}'", txtPtkpID.Text.Trim());
            auditLogFilter.TableName = "Ptkp";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtPtkpID.Enabled = (newVal == AppEnum.DataMode.New);

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Ptkp entity = new Ptkp();
            if (parameters.Length > 0)
            {
                string ptkpID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(ptkpID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtPtkpID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            Ptkp ptkp = (Ptkp)entity;
            txtPtkpID.Value = Convert.ToDouble(ptkp.PtkpID);
            txtValidFrom.SelectedDate = ptkp.ValidFrom;
            txtValidTo.SelectedDate = ptkp.ValidTo;
            cboSRTaxStatus.SelectedValue = ptkp.SRTaxStatus;
            txtAmount.Value = Convert.ToDouble(ptkp.Amount);

            //Display Data Detail
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(Ptkp entity)
        {
            entity.PtkpID = Convert.ToInt32(txtPtkpID.Value);
            entity.ValidFrom = txtValidFrom.SelectedDate;
            entity.ValidTo = txtValidTo.SelectedDate;
            entity.SRTaxStatus = cboSRTaxStatus.SelectedValue;
            entity.Amount = Convert.ToDecimal(txtAmount.Value);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(Ptkp entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                //Commit if success, Rollback if failed
                trans.Complete();

                if (DataModeCurrent == AppEnum.DataMode.New)
                    txtPtkpID.Value = entity.PtkpID;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            PtkpQuery que = new PtkpQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PtkpID > txtPtkpID.Text);
                que.OrderBy(que.PtkpID.Ascending);
            }
            else
            {
                que.Where(que.PtkpID < txtPtkpID.Text);
                que.OrderBy(que.PtkpID.Descending);
            }
            Ptkp entity = new Ptkp();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        #endregion
    }
}

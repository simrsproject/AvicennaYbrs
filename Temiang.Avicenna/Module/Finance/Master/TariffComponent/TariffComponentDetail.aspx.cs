using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class TariffComponentDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "TariffComponentSearch.aspx";
            UrlPageList = "TariffComponentList.aspx";

            WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.TariffComponent;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRTariffComponentType, AppEnum.StandardReference.TariffComponentType);
                StandardReference.InitializeIncludeSpace(cboPphType, AppEnum.StandardReference.PphType);
            }
        }
        
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new TariffComponent());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new TariffComponent();
            entity.LoadByPrimaryKey(txtTariffComponentID.Text);
            entity.MarkAsDeleted();

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new TariffComponent();
            if (entity.LoadByPrimaryKey(txtTariffComponentID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new TariffComponent();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new TariffComponent();
            if (entity.LoadByPrimaryKey(txtTariffComponentID.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("TariffComponentID='{0}'", txtTariffComponentID.Text.Trim());
            auditLogFilter.TableName = "TariffComponent";
        }
        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtTariffComponentID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new TariffComponent();
            if (parameters.Length > 0)
            {
                String tariffComponentId = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(tariffComponentId);
            }
            else
            {
                entity.LoadByPrimaryKey(txtTariffComponentID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var tariffComponent = (TariffComponent)entity;
            txtTariffComponentID.Text = tariffComponent.TariffComponentID;
            txtTariffComponentName.Text = tariffComponent.TariffComponentName;
            cboSRTariffComponentType.SelectedValue = tariffComponent.SRTariffComponentType;
            txtNotes.Text = tariffComponent.Notes;
            chkIsTariffParamedic.Checked = tariffComponent.IsTariffParamedic ?? false;
            chkIsIncludeInTaxCalc.Checked = tariffComponent.IsIncludeInTaxCalc ?? false;
            chkIsPrintParamedicInSlip.Checked = tariffComponent.IsPrintParamedicInSlip ?? false;
            cboPphType.SelectedValue = tariffComponent.SRPphType;
            chkIsAutoChecklist.Checked = tariffComponent.IsAutoChecklistCorrectedFeeVerification ?? false;
            chkFeeVerificationDefaultSelected.Checked = tariffComponent.IsFeeVerificationDefaultSelected ?? true;
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(TariffComponent entity)
        {
            entity.TariffComponentID = txtTariffComponentID.Text;
            entity.TariffComponentName = txtTariffComponentName.Text;
            entity.SRTariffComponentType = cboSRTariffComponentType.SelectedValue;
            entity.Notes = txtNotes.Text;
            entity.IsTariffParamedic = chkIsTariffParamedic.Checked;
            entity.IsIncludeInTaxCalc = chkIsIncludeInTaxCalc.Checked;
            entity.IsPrintParamedicInSlip = chkIsPrintParamedicInSlip.Checked;
            entity.SRPphType = cboPphType.SelectedValue;
            entity.IsAutoChecklistCorrectedFeeVerification = chkIsAutoChecklist.Checked;
            entity.IsFeeVerificationDefaultSelected = chkFeeVerificationDefaultSelected.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(TariffComponent entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new TariffComponentQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TariffComponentID > txtTariffComponentID.Text);
                que.OrderBy(que.TariffComponentID.Ascending);
            }
            else
            {
                que.Where(que.TariffComponentID < txtTariffComponentID.Text);
                que.OrderBy(que.TariffComponentID.Descending);
            }
            var entity = new TariffComponent();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

    }
}

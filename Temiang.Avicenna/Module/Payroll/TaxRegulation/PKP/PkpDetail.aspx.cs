using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.TaxRegulation
{
    public partial class PkpDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "";
            UrlPageList = "PkpList.aspx";
			
			ProgramID = AppConstant.Program.Pkp ; //TODO: Isi ProgramID
            txtPkpID.Text="1";
			//StandardReference Initialize
			if (!IsPostBack)
            {
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
            OnPopulateEntryControl(new Pkp());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Pkp entity = new Pkp();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPkpID.Text)))
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
            Pkp entity = new Pkp();
            entity = new Pkp();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Pkp entity = new Pkp();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPkpID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("PkpID='{0}'", txtPkpID.Text.Trim());
            auditLogFilter.TableName = "Pkp";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtPkpID.Enabled = (newVal == AppEnum.DataMode.New);

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Pkp entity = new Pkp();
            if (parameters.Length > 0)
            {
                string pkpID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(pkpID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtPkpID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            Pkp pkp = (Pkp)entity;
            txtPkpID.Value = Convert.ToDouble(pkp.PkpID);
            txtValidFrom.SelectedDate = pkp.ValidFrom;
            chkIsNPWP.Checked = pkp.IsNPWP ?? false;
            txtLowerLimit.Value = Convert.ToDouble(pkp.LowerLimit);
            txtUpperLimit.Value = Convert.ToDouble(pkp.UpperLimit);
            txtTaxRate.Value = Convert.ToDouble(pkp.TaxRate);
            txtTaxAmount.Value = Convert.ToDouble(pkp.TaxAmount);
            txtAmountOfDeduction.Value = Convert.ToDouble(pkp.AmountOfDeduction);

            //Display Data Detail
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(Pkp entity)
        {
            entity.PkpID = Convert.ToInt32(txtPkpID.Value);
            entity.ValidFrom = txtValidFrom.SelectedDate;
            entity.IsNPWP = chkIsNPWP.Checked;		
            entity.LowerLimit = Convert.ToDecimal(txtLowerLimit.Value);
            entity.UpperLimit = Convert.ToDecimal(txtUpperLimit.Value);
            entity.TaxRate = Convert.ToDecimal(txtTaxRate.Value);
            entity.TaxAmount = Convert.ToDecimal(txtTaxAmount.Value);
            entity.AmountOfDeduction = Convert.ToDecimal(txtAmountOfDeduction.Value);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(Pkp entity)
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
            PkpQuery que = new PkpQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PkpID > txtPkpID.Text);
                que.OrderBy(que.PkpID.Ascending);
            }
            else
            {
                que.Where(que.PkpID < txtPkpID.Text);
                que.OrderBy(que.PkpID.Descending);
            }
            Pkp entity = new Pkp();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        #endregion
    }
}

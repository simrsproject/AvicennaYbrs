using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.TaxRegulation
{
    public partial class SeveranceTaxDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "SeveranceTaxSearch.aspx";
            UrlPageList = "SeveranceTaxList.aspx";
			
			ProgramID = AppConstant.Program.SeveranceTax; //TODO: Isi ProgramID
            txtSeveranceTaxID.Text = "1";
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
            OnPopulateEntryControl(new SeveranceTax());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            SeveranceTax entity = new SeveranceTax();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtSeveranceTaxID.Text)))
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
            SeveranceTax entity = new SeveranceTax();
            entity = new SeveranceTax();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            SeveranceTax entity = new SeveranceTax();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtSeveranceTaxID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("SeveranceTaxID='{0}'", txtSeveranceTaxID.Text.Trim());
            auditLogFilter.TableName = "SeveranceTax";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtSeveranceTaxID.Enabled = (newVal == AppEnum.DataMode.New);

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            SeveranceTax entity = new SeveranceTax();
            if (parameters.Length > 0)
            {
                string severanceTaxID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(severanceTaxID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtSeveranceTaxID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            SeveranceTax severanceTax = (SeveranceTax)entity;
            txtSeveranceTaxID.Value = Convert.ToDouble(severanceTax.SeveranceTaxID);
            txtValidFrom.SelectedDate = severanceTax.ValidFrom;
            chkIsNPWP.Checked = severanceTax.IsNPWP ?? false;
            txtLowerLimit.Value = Convert.ToDouble(severanceTax.LowerLimit);
            txtUpperLimit.Value = Convert.ToDouble(severanceTax.UpperLimit);
            txtTaxRate.Value = Convert.ToDouble(severanceTax.TaxRate);
            txtTaxAmount.Value = Convert.ToDouble(severanceTax.TaxAmount);
            txtAmountOfDeduction.Value = Convert.ToDouble(severanceTax.AmountOfDeduction);

        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(SeveranceTax entity)
        {
            entity.SeveranceTaxID = Convert.ToInt32(txtSeveranceTaxID.Value);
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

        private void SaveEntity(SeveranceTax entity)
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
            SeveranceTaxQuery que = new SeveranceTaxQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.SeveranceTaxID > txtSeveranceTaxID.Text);
                que.OrderBy(que.SeveranceTaxID.Ascending);
            }
            else
            {
                que.Where(que.SeveranceTaxID < txtSeveranceTaxID.Text);
                que.OrderBy(que.SeveranceTaxID.Descending);
            }
            SeveranceTax entity = new SeveranceTax();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        #endregion
    }
}

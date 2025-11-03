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
    public partial class CurrencyDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "CurrencySearch.aspx";
            UrlPageList = "CurrencyList.aspx";

            ProgramID = AppConstant.Program.CURRENCY;
        }

        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new CurrencyRate());
            chkIsActive.Checked = true;
        }


        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            CurrencyRate entity = new CurrencyRate();
            entity.LoadByPrimaryKey(txtCurrencyID.Text);
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
            CurrencyRate entity = new CurrencyRate();
            if (entity.LoadByPrimaryKey(txtCurrencyID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new CurrencyRate();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            CurrencyRate entity = new CurrencyRate();
            if (entity.LoadByPrimaryKey(txtCurrencyID.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("CurrencyID='{0}'", txtCurrencyID.Text.Trim());
            auditLogFilter.TableName = "CurrencyRate";
        }
        #endregion

        #region Private Method Standard

        private void SetEntityValue(CurrencyRate entity)
        {

            entity.CurrencyID = txtCurrencyID.Text;
            entity.CurrencyName = txtCurrencyName.Text;
            entity.CurrencyRate = (decimal)txtCurrencyRate.Value;
            entity.IsActive = chkIsActive.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(CurrencyRate entity)
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
            CurrencyRateQuery que = new CurrencyRateQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.CurrencyID > txtCurrencyID.Text);
                que.OrderBy(que.CurrencyID.Ascending);
            }
            else
            {
                que.Where(que.CurrencyID < txtCurrencyID.Text);
                que.OrderBy(que.CurrencyID.Descending);
            }

            CurrencyRate entity = new CurrencyRate();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtCurrencyID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            CurrencyRate entity = new CurrencyRate();
            if (parameters.Length > 0)
            {
                String CurrencyID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(CurrencyID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtCurrencyID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            CurrencyRate bank = (CurrencyRate)entity;
            txtCurrencyID.Text = bank.CurrencyID;
            txtCurrencyName.Text = bank.CurrencyName;
            txtCurrencyRate.Value = Convert.ToDouble(bank.CurrencyRate);
            chkIsActive.Checked = bank.IsActive ?? false;

        }
        #endregion
    }
}

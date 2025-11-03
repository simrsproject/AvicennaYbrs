using System;
using System.Data;

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Remuneration.Base
{
    public partial class RemunerationBaseDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "RemunerationBaseSearch.aspx";
            UrlPageList = "RemunerationBaseList.aspx";

            ProgramID = AppConstant.Program.EmployeeRemunerationBase;
            this.WindowSearch.Height = 400;

            txtWageBaseID.Text = "-1";
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
            OnPopulateEntryControl(new WageBase());

            txtWageBaseID.Text = "-1";
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new WageBase();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtWageBaseID.Text)))
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
            var entity = new WageBase();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new WageBase();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtWageBaseID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("WageBaseID='{0}'", txtWageBaseID.Text.Trim());
            auditLogFilter.TableName = "WageBase";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtWageBaseID.Enabled = (newVal == AppEnum.DataMode.New);
            txtValidFrom.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new WageBase();
            if (parameters.Length > 0)
            {
                string id = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(id));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtWageBaseID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var wb = (WageBase)entity;
            txtWageBaseID.Value = Convert.ToDouble(wb.WageBaseID);
            txtValidFrom.SelectedDate = wb.ValidFrom;
            txtNominal.Value = Convert.ToDouble(wb.Nominal);
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(WageBase entity)
        {
            entity.ValidFrom = txtValidFrom.SelectedDate;
            entity.Nominal = Convert.ToDecimal(txtNominal.Value);

            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(WageBase entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                trans.Complete();

                txtWageBaseID.Text = entity.WageBaseID.ToString();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new WageBaseQuery();
            que.es.Top = 1;
            if (isNextRecord)
            {
                que.Where(que.ValidFrom > txtValidFrom.SelectedDate);
                que.OrderBy(que.ValidFrom.Ascending);
            }
            else
            {
                que.Where(que.ValidFrom < txtValidFrom.SelectedDate);
                que.OrderBy(que.ValidFrom.Descending);
            }
            var entity = new WageBase();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion
    }
}
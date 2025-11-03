using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class SalaryComponentRoundingDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "SalaryComponentRoundingSearch.aspx";
            UrlPageList = "SalaryComponentRoundingList.aspx";

            ProgramID = AppConstant.Program.SalaryComponentRounding; //TODO: Isi ProgramID
            txtSalaryComponentRoundingID.Text = "1";
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
            OnPopulateEntryControl(new SalaryComponentRounding());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            SalaryComponentRounding entity = new SalaryComponentRounding();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtSalaryComponentRoundingID.Text)))
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
            SalaryComponentRounding entity = new SalaryComponentRounding();
            entity = new SalaryComponentRounding();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            SalaryComponentRounding entity = new SalaryComponentRounding();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtSalaryComponentRoundingID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("SalaryComponentRoundingID='{0}'", txtSalaryComponentRoundingID.Text.Trim());
            auditLogFilter.TableName = "SalaryComponentRounding";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtSalaryComponentRoundingID.Enabled = (newVal == AppEnum.DataMode.New);

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            SalaryComponentRounding entity = new SalaryComponentRounding();
            if (parameters.Length > 0)
            {
                string salaryComponentRoundingID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(salaryComponentRoundingID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtSalaryComponentRoundingID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            SalaryComponentRounding salaryComponentRounding = (SalaryComponentRounding)entity;
            txtSalaryComponentRoundingID.Value = Convert.ToDouble(salaryComponentRounding.SalaryComponentRoundingID);
            txtSalaryComponentRoundingName.Text = salaryComponentRounding.SalaryComponentRoundingName;
            txtNominalValue.Value = Convert.ToDouble(salaryComponentRounding.NominalValue);
            txtNearestValue.Value = Convert.ToDouble(salaryComponentRounding.NearestValue);

            //Display Data Detail
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(SalaryComponentRounding entity)
        {
            entity.SalaryComponentRoundingID = Convert.ToInt32(txtSalaryComponentRoundingID.Value);
            entity.SalaryComponentRoundingName = txtSalaryComponentRoundingName.Text;
            entity.NominalValue = Convert.ToDecimal(txtNominalValue.Value);
            entity.NearestValue = Convert.ToDecimal(txtNearestValue.Value);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(SalaryComponentRounding entity)
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
            SalaryComponentRoundingQuery que = new SalaryComponentRoundingQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.SalaryComponentRoundingID > txtSalaryComponentRoundingID.Text);
                que.OrderBy(que.SalaryComponentRoundingID.Ascending);
            }
            else
            {
                que.Where(que.SalaryComponentRoundingID < txtSalaryComponentRoundingID.Text);
                que.OrderBy(que.SalaryComponentRoundingID.Descending);
            }
            SalaryComponentRounding entity = new SalaryComponentRounding();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        #endregion
    }
}

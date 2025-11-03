using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Globalization;
using Telerik.Web.UI.Calendar;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class DisciplinarySanctionsDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "DisciplinarySanctionsSearch.aspx";
            UrlPageList = "DisciplinarySanctionsList.aspx";

            ProgramID = AppConstant.Program.DisciplinarySanctions; //TODO: Isi ProgramID
            txtDisciplinarySanctionsID.Text = "1";

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSREmploymentType, AppEnum.StandardReference.EmploymentType);
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
            OnPopulateEntryControl(new DisciplinarySanctions());
            cboSREmploymentType.SelectedValue = string.Empty;
            cboSREmploymentType.Text = string.Empty;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new DisciplinarySanctions();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtDisciplinarySanctionsID.Text)))
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
            var entity = new DisciplinarySanctions();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new DisciplinarySanctions();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtDisciplinarySanctionsID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("DisciplinarySanctionsID='{0}'", txtDisciplinarySanctionsID.Text.Trim());
            auditLogFilter.TableName = "DisciplinarySanctions";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtDisciplinarySanctionsID.Enabled = (newVal == AppEnum.DataMode.New);

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new DisciplinarySanctions();
            if (parameters.Length > 0)
            {
                string id = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(id));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtDisciplinarySanctionsID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            DisciplinarySanctions sanction = (DisciplinarySanctions)entity;
            txtDisciplinarySanctionsID.Value = Convert.ToDouble(sanction.DisciplinarySanctionsID);
            cboSREmploymentType.SelectedValue = sanction.SREmploymentType;
            txtStartValue.Value = Convert.ToDouble(sanction.StartValue);
            txtEndValue.Value = Convert.ToDouble(sanction.EndValue);
            txtCutPercentage.Value = Convert.ToDouble(sanction.CutPercentage);
            txtValidFromDate.SelectedDate = sanction.ValidFromDate;
            
            //Display Data Detail
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(DisciplinarySanctions entity)
        {
            entity.DisciplinarySanctionsID = Convert.ToInt32(txtDisciplinarySanctionsID.Value);
            entity.SREmploymentType = cboSREmploymentType.SelectedValue;
            entity.StartValue = Convert.ToInt16(txtStartValue.Value);
            entity.EndValue = Convert.ToInt16(txtEndValue.Value);
            entity.CutPercentage = Convert.ToDecimal(txtCutPercentage.Value);
            entity.ValidFromDate = txtValidFromDate.SelectedDate;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(DisciplinarySanctions entity)
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
            var que = new DisciplinarySanctionsQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.DisciplinarySanctionsID > txtDisciplinarySanctionsID.Text);
                que.OrderBy(que.DisciplinarySanctionsID.Ascending);
            }
            else
            {
                que.Where(que.DisciplinarySanctionsID < txtDisciplinarySanctionsID.Text);
                que.OrderBy(que.DisciplinarySanctionsID.Descending);
            }
            var entity = new DisciplinarySanctions();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion
    }
}
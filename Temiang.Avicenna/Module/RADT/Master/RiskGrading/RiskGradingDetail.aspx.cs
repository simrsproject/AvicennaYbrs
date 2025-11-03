using System;
using System.Collections.Generic;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Linq;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class RiskGradingDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "RiskGradingSearch.aspx";
            UrlPageList = "RiskGradingList.aspx";

            ProgramID = AppConstant.Program.RiskGrading;

            if (!IsPostBack)
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
            OnPopulateEntryControl(new RiskGrading());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new RiskGrading();
            if (entity.LoadByPrimaryKey(txtRiskGradingID.Text))
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
            var entity = new RiskGrading();
            if (entity.LoadByPrimaryKey(txtRiskGradingID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new RiskGrading();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new RiskGrading();
            if (entity.LoadByPrimaryKey(txtRiskGradingID.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("RiskGradingID='{0}'", txtRiskGradingID.Text.Trim());
            auditLogFilter.TableName = "RiskGrading";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtRiskGradingID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new RiskGrading();
            if (parameters.Length > 0)
            {
                String id = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id);
            }
            else
            {
                entity.LoadByPrimaryKey(txtRiskGradingID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var rg = (RiskGrading)entity;
            txtRiskGradingID.Text = rg.RiskGradingID;
            txtRiskGradingName.Text = rg.RiskGradingName;

            switch (rg.RiskGradingColor)
            {
                case "Blue":
                    rblRiskGradingColor.SelectedIndex = 0;
                    break;
                case "Green":
                    rblRiskGradingColor.SelectedIndex = 1;
                    break;
                case "Yellow":
                    rblRiskGradingColor.SelectedIndex = 2;
                    break;
                case "Red":
                    rblRiskGradingColor.SelectedIndex = 3;
                    break;
                default:
                    rblRiskGradingColor.SelectedIndex = 0;
                    break;
            }
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(RiskGrading entity)
        {
            entity.RiskGradingID = txtRiskGradingID.Text;
            entity.RiskGradingName = txtRiskGradingName.Text;
            switch (rblRiskGradingColor.SelectedIndex)
            {
                case 0:
                    entity.RiskGradingColor = "Blue";
                    break;
                case 1:
                    entity.RiskGradingColor = "Green";
                    break;
                case 2:
                    entity.RiskGradingColor = "Yellow";
                    break;
                case 3:
                    entity.RiskGradingColor = "Red";
                    break;
            }

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(RiskGrading entity)
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
            var que = new RiskGradingQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.RiskGradingID > txtRiskGradingID.Text);
                que.OrderBy(que.RiskGradingID.Ascending);
            }
            else
            {
                que.Where(que.RiskGradingID < txtRiskGradingID.Text);
                que.OrderBy(que.RiskGradingID.Descending);
            }

            var entity = new RiskGrading();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion
    }
}

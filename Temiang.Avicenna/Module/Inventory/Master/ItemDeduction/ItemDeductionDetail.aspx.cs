using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ItemDeductionDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "##";
            UrlPageList = "ItemDeductionList.aspx";

            ProgramID = AppConstant.Program.ItemProductDeductionDetail;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuSearch.Visible = false;
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ItemProductDeductionDetail());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ItemProductDeductionDetail();
            entity.LoadByPrimaryKey(txtDeductionID.Text);
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
            var entity = new ItemProductDeductionDetail();
            if (entity.LoadByPrimaryKey(txtDeductionID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new ItemProductDeductionDetail();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ItemProductDeductionDetail();
            if (entity.LoadByPrimaryKey(txtDeductionID.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("DeductionID='{0}'", txtDeductionID.Text.Trim());
            auditLogFilter.TableName = "ItemProductDeductionDetail";
        }
        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtDeductionID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ItemProductDeductionDetail();
            if (parameters.Length > 0)
            {
                String id = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id);
            }
            else
            {
                entity.LoadByPrimaryKey(txtDeductionID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var id = (ItemProductDeductionDetail)entity;
            txtDeductionID.Text = id.DeductionID;
            txtMinAmount.Value = Convert.ToDouble(id.MinAmount);
            txtMaxAmount.Value = Convert.ToDouble(id.MaxAmount);
            txtDeductionAmount.Value = Convert.ToDouble(id.DeductionAmount);
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(ItemProductDeductionDetail entity)
        {
            entity.DeductionID = txtDeductionID.Text;
            entity.MinAmount = Convert.ToDecimal(txtMinAmount.Value);
            entity.MaxAmount = Convert.ToDecimal(txtMaxAmount.Value);
            entity.DeductionAmount = Convert.ToDecimal(txtDeductionAmount.Value);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(ItemProductDeductionDetail entity)
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
            var que = new ItemProductDeductionDetailQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.DeductionID > txtDeductionID.Text);
                que.OrderBy(que.DeductionID.Ascending);
            }
            else
            {
                que.Where(que.DeductionID < txtDeductionID.Text);
                que.OrderBy(que.DeductionID.Descending);
            }
            var entity = new ItemProductDeductionDetail();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion
    }
}

using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class DosageUnitDetail : BasePageDetail
    {
        private void SetEntityValue(AppStandardReferenceItem entity)
        {
            entity.StandardReferenceID = AppEnum.StandardReference.DosageUnit.ToString();
            entity.ItemID = txtItemID.Text;
            entity.ItemName = txtItemName.Text;
            entity.Note = string.Empty;
            entity.IsUsedBySystem = true;
            entity.IsActive = chkIsActive.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AppStandardReferenceItemQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ItemID > txtItemID.Text, que.StandardReferenceID == AppEnum.StandardReference.DosageUnit.ToString());
                que.OrderBy(que.ItemID.Ascending);
            }
            else
            {
                que.Where(que.ItemID < txtItemID.Text, que.StandardReferenceID == AppEnum.StandardReference.DosageUnit.ToString());
                que.OrderBy(que.ItemID.Descending);
            }
            var entity = new AppStandardReferenceItem();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AppStandardReferenceItem();
            if (parameters.Length > 0)
            {
                String itemId = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(AppEnum.StandardReference.DosageUnit.ToString(), itemId);
            }
            else
                entity.LoadByPrimaryKey(AppEnum.StandardReference.DosageUnit.ToString(), txtItemID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var d = (AppStandardReferenceItem)entity;
            txtItemID.Text = d.ItemID;
            txtItemName.Text = d.ItemName;
            chkIsActive.Checked = d.IsActive ?? false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AppStandardReferenceItem());
            chkIsActive.Checked = true;
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
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtItemID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "DosageUnitSearch.aspx";
            UrlPageList = "DosageUnitList.aspx";

            ProgramID = AppConstant.Program.DosageUnit;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new AppStandardReferenceItem();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(AppStandardReferenceItem entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.DosageUnit.ToString(), txtItemID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion
    }
}

using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class VisitTypeDetail : BasePageDetail
    {
        private void SetEntityValue(VisitType entity)
        {
            entity.VisitTypeID = txtVisitTypeID.Text;
            entity.VisitTypeName = txtVisitTypeName.Text;
            entity.Notes = txtNotes.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            VisitTypeQuery que = new VisitTypeQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.VisitTypeID > txtVisitTypeID.Text);
                que.OrderBy(que.VisitTypeID.Ascending);
            }
            else
            {
                que.Where(que.VisitTypeID < txtVisitTypeID.Text);
                que.OrderBy(que.VisitTypeID.Descending);
            }
            VisitType entity = new VisitType();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            VisitType entity = new VisitType();
            if (parameters.Length > 0)
            {
                String visitTypeID = (String)parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(visitTypeID);
            }
            else
                entity.LoadByPrimaryKey(txtVisitTypeID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            VisitType visitType = (VisitType)entity;
            txtVisitTypeID.Text = visitType.VisitTypeID;
            txtVisitTypeName.Text = visitType.VisitTypeName;
            txtNotes.Text = visitType.Notes;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new VisitType());
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
            auditLogFilter.PrimaryKeyData = string.Format("VisitTypeID='{0}'", txtVisitTypeID.Text.Trim());
            auditLogFilter.TableName = "VisitType";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtVisitTypeID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "VisitTypeSearch.aspx";
            UrlPageList = "VisitTypeList.aspx";

            ProgramID = AppConstant.Program.VisitType;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            VisitType entity = new VisitType();
            entity.LoadByPrimaryKey(txtVisitTypeID.Text);
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            VisitType entity = new VisitType();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(VisitType entity)
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
            VisitType entity = new VisitType();
            if (entity.LoadByPrimaryKey(txtVisitTypeID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion
    }
}

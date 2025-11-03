using System;
using System.Collections.Generic;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class TherapyDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "TherapySearch.aspx";
            UrlPageList = "TherapyList.aspx";

            ProgramID = AppConstant.Program.Therapy;

            //StandardReference Initialize
            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSRTherapyGroup, AppEnum.StandardReference.TherapyGroup);
                
            

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
            OnPopulateEntryControl(new Therapy());
            cboSRTherapyGroup.SelectedValue = string.Empty;
            cboSRTherapyGroup.Text = string.Empty;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Therapy entity = new Therapy();
            if (entity.LoadByPrimaryKey(txtTherapyID.Text))
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
            Therapy entity = new Therapy();
            if (entity.LoadByPrimaryKey(txtTherapyID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new Therapy();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Therapy entity = new Therapy();
            if (entity.LoadByPrimaryKey(txtTherapyID.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("TherapyID='{0}'", txtTherapyID.Text.Trim());
            auditLogFilter.TableName = "Therapy";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtTherapyID.Enabled = (newVal == AppEnum.DataMode.New);

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Therapy entity = new Therapy();
            if (parameters.Length > 0)
            {
                String TherapyID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(TherapyID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtTherapyID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            Therapy therapy = (Therapy)entity;
            txtTherapyID.Text = therapy.TherapyID;
            txtTherapyName.Text = therapy.TherapyName;
            cboSRTherapyGroup.SelectedValue = therapy.SRTherapyGroup;
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(Therapy entity)
        {
            entity.TherapyID = txtTherapyID.Text;
            entity.TherapyName = txtTherapyName.Text;
            entity.SRTherapyGroup = cboSRTherapyGroup.SelectedValue;
            
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(Therapy entity)
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
            TherapyQuery que = new TherapyQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TherapyID > txtTherapyID.Text);
                que.OrderBy(que.TherapyID.Ascending);
            }
            else
            {
                que.Where(que.TherapyID < txtTherapyID.Text);
                que.OrderBy(que.TherapyID.Descending);
            }

            Therapy entity = new Therapy();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion
    }
}

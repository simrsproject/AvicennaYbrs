using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class MenuInitializationDetail : BasePageDetail
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "MenuInitializationSearch.aspx?ext=" + Request.QueryString["ext"];
            UrlPageList = "MenuInitializationList.aspx?ext=" + Request.QueryString["ext"];

            ProgramID = Request.QueryString["ext"] == "0" ? AppConstant.Program.MenuInitialization : AppConstant.Program.MenuExtraInitialization;

            if (!IsPostBack)
            {
                var mvcoll = new MenuVersionCollection();
                mvcoll.Query.Where(mvcoll.Query.IsActive == true);
                if (Request.QueryString["ext"] == "0")
                    mvcoll.Query.Where(mvcoll.Query.IsExtra == false);
                else
                    mvcoll.Query.Where(mvcoll.Query.IsExtra == true);
                mvcoll.Query.OrderBy(mvcoll.Query.VersionID.Descending);
                mvcoll.LoadAll();

                cboVersionID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var item in mvcoll)
                {
                    cboVersionID.Items.Add(new RadComboBoxItem(item.VersionName, item.VersionID));
                }
            }
        }

        private void SetEntityValue(MenuSetting entity)
        {
            entity.StartingDate = txtStartingDate.SelectedDate;
            entity.VersionID = cboVersionID.SelectedValue;
            entity.SeqNo = cboSeqNo.SelectedValue;
            entity.IsExtra = Request.QueryString["ext"] != "0";
            
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new MenuSettingQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.StartingDate > txtStartingDate.SelectedDate);
                que.OrderBy(que.StartingDate.Ascending);
            }
            else
            {
                que.Where(que.StartingDate < txtStartingDate.SelectedDate);
                que.OrderBy(que.StartingDate.Descending);
            }
            if (Request.QueryString["ext"] == "0")
                que.Where(que.IsExtra == false);
            else
                que.Where(que.IsExtra == true);

            var entity = new MenuSetting();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new MenuSetting();
            if (parameters.Length > 0)
            {
                DateTime startDate = Convert.ToDateTime(parameters[0]);
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(startDate);
            }
            else
                entity.LoadByPrimaryKey(txtStartingDate.SelectedDate.Value.Date);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var menuInit = (MenuSetting)entity;
            txtStartingDate.SelectedDate = menuInit.StartingDate;
            cboVersionID.SelectedValue = menuInit.VersionID;
            if (!string.IsNullOrEmpty(menuInit.VersionID))
            {
                ComboBox.PopulateMenuVersionSeqNoList(cboSeqNo, menuInit.VersionID);
                cboSeqNo.SelectedValue = menuInit.SeqNo;
            }
            else
            {
                cboSeqNo.Items.Clear();
                cboSeqNo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboSeqNo.SelectedValue = string.Empty;
                cboSeqNo.Text = string.Empty;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboVersionID, cboVersionID);
            ajax.AddAjaxSetting(cboVersionID, cboSeqNo);
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new MenuSetting());
            txtStartingDate.SelectedDate = DateTime.Now;
            cboVersionID.SelectedValue = string.Empty;
            cboVersionID.Text = string.Empty;
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
            auditLogFilter.PrimaryKeyData = "StartingDate='" + txtStartingDate.SelectedDate.Value.Date + "'";
            auditLogFilter.TableName = "MenuSetting";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtStartingDate.Enabled = (newVal == AppEnum.DataMode.New);
            cboVersionID.Enabled = (newVal == AppEnum.DataMode.New);
            cboSeqNo.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new MenuSetting();
            if (entity.LoadByPrimaryKey(txtStartingDate.SelectedDate.Value.Date))
            {
                entity.MarkAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var e = new MenuSetting();
            if (e.LoadByPrimaryKey(txtStartingDate.SelectedDate.Value.Date))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            var mv = new MenuVersion();
            mv.LoadByPrimaryKey(cboVersionID.SelectedValue);
            int cycle = Convert.ToInt32(mv.Cycle) + ((mv.IsPlusOneRule ?? false) ? 1 : 0);
            if (Convert.ToInt32(cboSeqNo.SelectedValue) > cycle)
            {
                args.MessageText = "Seq No can't be greater than " + cycle.ToString();
                args.IsCancel = true;
                return;
            }

            var entity = new MenuSetting();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new MenuSetting();
            if (entity.LoadByPrimaryKey(txtStartingDate.SelectedDate.Value.Date))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        private void SaveEntity(MenuSetting entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        #endregion

        protected void cboVersionID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComboBox.PopulateMenuVersionSeqNoList(cboSeqNo, e.Value);
        }
    }
}

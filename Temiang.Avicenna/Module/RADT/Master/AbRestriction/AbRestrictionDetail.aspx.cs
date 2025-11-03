using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.Module.RADT.Master;

namespace Temiang.Avicenna.Module.Master
{
    public partial class AbRestrictionDetail : BasePageDetail
    {
        private void SetEntityValue(AbRestriction entity)
        {
            entity.AbRestrictionID = txtAbRestrictionID.Text;
            entity.AbRestrictionName = txtAbRestrictionName.Text;
            entity.SRAbRestrictionType = cboSRAbRestrictionType.SelectedValue;
            entity.IsNotRestricted = chkIsNotRestricted.Checked;
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Update Key
            AbRestrictionItemCollection coll = AbRestrictionItems;
            foreach (AbRestrictionItem item in coll)
            {
                item.AbRestrictionID = txtAbRestrictionID.Text;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            AbRestrictionQuery que = new AbRestrictionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.AbRestrictionID > txtAbRestrictionID.Text);
                que.OrderBy(que.AbRestrictionID.Ascending);
            }
            else
            {
                que.Where(que.AbRestrictionID < txtAbRestrictionID.Text);
                que.OrderBy(que.AbRestrictionID.Descending);
            }
            AbRestriction entity = new AbRestriction();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            AbRestriction entity = new AbRestriction();
            if (parameters.Length > 0)
            {
                String AbRestrictionID = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(AbRestrictionID);
            }
            else
                entity.LoadByPrimaryKey(txtAbRestrictionID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            AbRestriction abRestriction = (AbRestriction)entity;
            txtAbRestrictionID.Text = abRestriction.AbRestrictionID;
            txtAbRestrictionName.Text = abRestriction.AbRestrictionName;
            cboSRAbRestrictionType.SelectedValue = abRestriction.SRAbRestrictionType;
            //StandardReference.InitializeWithOneRow(cboSRAbRestrictionType, AppEnum.StandardReference.AbRestrictionType, abRestriction.SRAbRestrictionType ?? string.Empty);
            chkIsNotRestricted.Checked = abRestriction.IsNotRestricted ?? false;
            //Display Data Detail
            PopulateGridDetail();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AbRestriction());
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
            auditLogFilter.PrimaryKeyData = "AbRestrictionID='" + txtAbRestrictionID.Text.Trim() + "'";
            auditLogFilter.TableName = "AbRestriction";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtAbRestrictionID.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItemGrid(oldVal, newVal);

            var val = cboSRAbRestrictionType.SelectedValue;
            StandardReference.Initialize(cboSRAbRestrictionType, AppEnum.StandardReference.AbRestrictionType);
            if (!string.IsNullOrWhiteSpace(val))
                cboSRAbRestrictionType.SelectedValue = val;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "AbRestrictionSearch.aspx";
            UrlPageList = "AbRestrictionList.aspx";

            ProgramID = AppConstant.Program.AbRestriction;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRAbRestrictionType, AppEnum.StandardReference.AbRestrictionType);
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new AbRestriction();
            if (entity.LoadByPrimaryKey(txtAbRestrictionID.Text))
            {
                entity.MarkAsDeleted();

                var AbRestrictionItems = new AbRestrictionItemCollection();
                AbRestrictionItems.Query.Where(AbRestrictionItems.Query.AbRestrictionID == txtAbRestrictionID.Text);
                AbRestrictionItems.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    AbRestrictionItems.Save();
                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new AbRestriction();
            if (entity.LoadByPrimaryKey(txtAbRestrictionID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new AbRestriction();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(AbRestriction entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                AbRestrictionItems.Save();
                AbRestrictionSuggestions.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            AbRestriction entity = new AbRestriction();
            if (entity.LoadByPrimaryKey(txtAbRestrictionID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion

        #region Record Detail Method Function

        private AbRestrictionItemCollection AbRestrictionItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collAbRestrictionItem"];
                    if (obj != null)
                        return ((AbRestrictionItemCollection)(obj));
                }

                AbRestrictionItemCollection coll = new AbRestrictionItemCollection();
                var query = new AbRestrictionItemQuery("abi");
                var item = new ZatActiveQuery("i");
                query.InnerJoin(item).On(query.ZatActiveID == item.ZatActiveID);


                string restrictionID = txtAbRestrictionID.Text;
                query.Where(query.AbRestrictionID == restrictionID);
                query.Select(query);
                query.Select(item.ZatActiveName.As("refToZatActive_ZatActiveName"));
                query.OrderBy(query.AbLevel.Ascending, item.ZatActiveName.Ascending);
                coll.Load(query);

                Session["collAbRestrictionItem"] = coll;
                return coll;
            }
            set { Session["collAbRestrictionItem"] = value; }
        }

        private AbRestrictionSuggestionCollection AbRestrictionSuggestions
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collAbRestrictionSuggestion"];
                    if (obj != null)
                        return ((AbRestrictionSuggestionCollection)(obj));
                }

                var coll = new AbRestrictionSuggestionCollection();
                var query = new AbRestrictionSuggestionQuery("abi");

                string restrictionID = txtAbRestrictionID.Text;
                query.Where(query.AbRestrictionID == restrictionID);
                query.OrderBy(query.AbLevel.Ascending);
                coll.Load(query);

                Session["collAbRestrictionSuggestion"] = coll;
                return coll;
            }
            set { Session["collAbRestrictionSuggestion"] = value; }
        }


        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdAbRestrictionItem.Columns[0].Visible = isVisible;
            grdAbRestrictionItem.Columns[grdAbRestrictionItem.Columns.Count - 1].Visible = isVisible;

            grdAbRestrictionItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                AbRestrictionItems = null;

            //Perbaharui tampilan dan data
            grdAbRestrictionItem.Rebind();


            grdAbRestrictionSuggestion.Columns[0].Visible = isVisible;
            grdAbRestrictionSuggestion.Columns[grdAbRestrictionSuggestion.Columns.Count - 1].Visible = isVisible;

            grdAbRestrictionSuggestion.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                AbRestrictionSuggestions = null;

            //Perbaharui tampilan dan data
            grdAbRestrictionSuggestion.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            AbRestrictionItems = null; //Reset Record Detail
            grdAbRestrictionItem.DataSource = AbRestrictionItems;
            grdAbRestrictionItem.DataBind();

            AbRestrictionSuggestions = null; //Reset Record Detail
            grdAbRestrictionSuggestion.DataSource = AbRestrictionSuggestions;
            grdAbRestrictionSuggestion.DataBind();
        }

        protected void grdAbRestrictionItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdAbRestrictionItem.DataSource = AbRestrictionItems;
        }

        protected void grdAbRestrictionItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            var abLevel = (editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][AbRestrictionItemMetadata.ColumnNames.AbLevel]).ToInt();
            var zatActiveID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][AbRestrictionItemMetadata.ColumnNames.ZatActiveID]);
            AbRestrictionItem entity = FindItemGrid(abLevel, zatActiveID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdAbRestrictionItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            var abLevel = (item.OwnerTableView.DataKeyValues[item.ItemIndex][AbRestrictionItemMetadata.ColumnNames.AbLevel]).ToInt();
            var zatActiveID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][AbRestrictionItemMetadata.ColumnNames.ZatActiveID]);
            AbRestrictionItem entity = FindItemGrid(abLevel, zatActiveID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdAbRestrictionItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            AbRestrictionItem entity = AbRestrictionItems.AddNew();
            SetEntityValue(entity, e);
        }

        private void SetEntityValue(AbRestrictionItem entity, GridCommandEventArgs e)
        {
            AbRestrictionItemDetail userControl = (AbRestrictionItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.AbRestrictionID = txtAbRestrictionID.Text;
                entity.ZatActiveID = userControl.ZatActiveID;
                entity.ZatActiveName = userControl.ZatActiveName;
                entity.AbLevel = userControl.AbLevel;
                entity.Notes = userControl.Notes;
            }
        }

        private AbRestrictionItem FindItemGrid(int abLevel, string zatActiveID)
        {
            AbRestrictionItemCollection coll = AbRestrictionItems;
            AbRestrictionItem retval = null;
            foreach (AbRestrictionItem rec in coll)
            {
                if (rec.AbLevel == abLevel && rec.ZatActiveID.Equals(zatActiveID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }


        protected void grdAbRestrictionSuggestion_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdAbRestrictionSuggestion.DataSource = AbRestrictionSuggestions;
        }

        protected void grdAbRestrictionSuggestion_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            var abLevel = (editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][AbRestrictionSuggestionMetadata.ColumnNames.AbLevel]).ToInt();
            AbRestrictionSuggestion entity = FindAbRestrictionSuggestion(abLevel);
            if (entity != null)
                SetAbRestrictionSuggestion(entity, e);
        }

        protected void grdAbRestrictionSuggestion_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            var abLevel = (item.OwnerTableView.DataKeyValues[item.ItemIndex][AbRestrictionSuggestionMetadata.ColumnNames.AbLevel]).ToInt();
            AbRestrictionSuggestion entity = FindAbRestrictionSuggestion(abLevel);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdAbRestrictionSuggestion_InsertCommand(object source, GridCommandEventArgs e)
        {
            AbRestrictionSuggestion entity = AbRestrictionSuggestions.AddNew();
            SetAbRestrictionSuggestion(entity, e);
        }

        private void SetAbRestrictionSuggestion(AbRestrictionSuggestion entity, GridCommandEventArgs e)
        {
            AbRestrictionSuggestionDetail userControl = (AbRestrictionSuggestionDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.AbRestrictionID = txtAbRestrictionID.Text;
                entity.AbLevel = userControl.AbLevel;
                entity.SuggestionNote = userControl.SuggestionNote;
            }
        }

        private AbRestrictionSuggestion FindAbRestrictionSuggestion(int abLevel)
        {
            AbRestrictionSuggestionCollection coll = AbRestrictionSuggestions;
            AbRestrictionSuggestion retval = null;
            foreach (AbRestrictionSuggestion rec in coll)
            {
                if (rec.AbLevel == abLevel)
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }
        #endregion

    }
}
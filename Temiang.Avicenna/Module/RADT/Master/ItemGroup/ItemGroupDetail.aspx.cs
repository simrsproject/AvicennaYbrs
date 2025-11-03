//TODO: Add setting Account
using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemGroupDetail : BasePageDetail
    {
        private string FormType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ItemGroupSearch.aspx?type=" + FormType;
            UrlPageList = "ItemGroupList.aspx?type=" + FormType;

            switch (FormType)
            {
                case "":
                case "service":
                    ProgramID = AppConstant.Program.GroupItem;
                    break;

                case "product":
                    ProgramID = AppConstant.Program.GroupItemProduct;
                    break;
            }

			//StandardReference Initialize
			if (!IsPostBack)
            {
                switch (FormType)
                {
                    case "":
                        StandardReference.InitializeIncludeSpace(cboSRItemType, AppEnum.StandardReference.ItemType);
                        pnlItemSubGroup.Visible = AppSession.Parameter.IsUsingItemSubGroup;
                        break;
                    case "service":
                        StandardReference.InitializeIncludeSpace(cboSRItemType, AppEnum.StandardReference.ItemType, "Service");
                        trRestrictionUserType.Visible = false;
                        pnlItemSubGroup.Visible = AppSession.Parameter.IsUsingItemSubGroup;
                        break;
                    case "product":
                        StandardReference.InitializeIncludeSpace(cboSRItemType, AppEnum.StandardReference.ItemType, "Product");
                        pnlCito.Visible = false;
                        pnlItemSubGroup.Visible = false;
                        break;
                }
            }
			
			//PopUp Search
            //if (!IsCallback)
            //{
            //    PopUpSearch.Initialize(AppEnum.PopUpSearch.Accounts, Page, txtAccountID);	
            //}

            AutoCompleteBox.Initialized(acbRestrictionUserType, AppEnum.StandardReference.UserType, false, false, "+");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            if (FormType == "" && AppSession.Parameter.IsUsingItemSubGroup)
            {
                ajax.AddAjaxSetting(cboSRItemType, pnlItemSubGroup);
                ajax.AddAjaxSetting(cboSRItemType, cboSRItemType);
            }
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ItemGroup());
            chkIsActive.Checked = true;
            cboSRItemType.SelectedValue = string.Empty;
            cboSRItemType.Text = string.Empty;
            pnlItemSubGroup.Visible = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var item = new ItemCollection();
            item.Query.Where(item.Query.ItemGroupID == txtItemGroupID.Text);
            item.LoadAll();
            if (item.Count > 0)
            {
                args.MessageText = AppConstant.Message.RecordUsedByOther;
                args.IsCancel = true;
                return;
            }

            ItemGroup entity = new ItemGroup();
            if (entity.LoadByPrimaryKey(txtItemGroupID.Text))
            {
                entity.MarkAsDeleted();

                var coll = new AppStandardReferenceItemCollection();
                coll.Query.Where(coll.Query.StandardReferenceID == AppEnum.StandardReference.ItemSubGroup, coll.Query.ReferenceID == txtItemGroupID.Text);
                coll.LoadAll();
                coll.MarkAllAsDeleted();
                
                SaveEntity(entity, coll);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            switch (cboSRItemType.SelectedValue)
            {
                case "11":
                case "21":
                case "81":
                    if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdProductAutomaticUseGroupInitial) && string.IsNullOrEmpty(txtInitial.Text))
                    {
                        args.MessageText = "Initial is required.";
                        args.IsCancel = true;
                        return;
                    }

                    break;
                default:
                    if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdServiceAutomaticUseGroupInitial) && string.IsNullOrEmpty(txtInitial.Text))
                    {
                        args.MessageText = "Initial is required.";
                        args.IsCancel = true;
                        return;
                    }
                    break;
            }

            ItemGroup entity = new ItemGroup();
            if (entity.LoadByPrimaryKey(txtItemGroupID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new ItemGroup();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity, ItemSubGroups);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            switch (cboSRItemType.SelectedValue)
            {
                case "11":
                case "21":
                case "81":
                    if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdProductAutomaticUseGroupInitial) && string.IsNullOrEmpty(txtInitial.Text))
                    {
                        args.MessageText = "Initial is required.";
                        args.IsCancel = true;
                        return;
                    }

                    break;
                default:
                    if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdServiceAutomaticUseGroupInitial) && string.IsNullOrEmpty(txtInitial.Text))
                    {
                        args.MessageText = "Initial is required.";
                        args.IsCancel = true;
                        return;
                    }
                    break;
            }

            if (!chkIsActive.Checked)
            {
                var items = new ItemCollection();
                items.Query.Where(items.Query.ItemGroupID == txtItemGroupID.Text);
                items.LoadAll();
                if (items.Count > 0)
                {
                    args.MessageText = "Item Group ID is still used in Master Item.";
                    args.IsCancel = true;
                    return;
                }
            }

            ItemGroup entity = new ItemGroup();
            if (entity.LoadByPrimaryKey(txtItemGroupID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity, ItemSubGroups);
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
            auditLogFilter.PrimaryKeyData = string.Format("ItemGroupID='{0}'", txtItemGroupID.Text.Trim());
            auditLogFilter.TableName = "ItemGroup";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtItemGroupID.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ItemGroup entity = new ItemGroup();
            if (parameters.Length > 0)
            {
                String itemGroupID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(itemGroupID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtItemGroupID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            ItemGroup itemGroup = (ItemGroup)entity;
            txtItemGroupID.Text = itemGroup.ItemGroupID;
            txtItemGroupName.Text = itemGroup.ItemGroupName;
            txtInitial.Text = itemGroup.Initial;
            cboSRItemType.SelectedValue = itemGroup.SRItemType;
            txtCitoValue.Value = Convert.ToDouble(itemGroup.CitoValue);
            chkIsCitoInPercent.Checked = itemGroup.IsCitoInPercent ?? false;
            //txtAccountID.Text = itemGroup.AccountID;
            //PopulateAccountName(false);
            txtCssClass.Text = itemGroup.CssClass;
            chkIsActive.Checked = itemGroup.IsActive ?? false;
            AutoCompleteBox.SetValue(acbRestrictionUserType, itemGroup.RestrictionUserType, acbRestrictionUserType.Delimiter.ToCharArray()[0]);

            PopulateGrid();

            if (!string.IsNullOrEmpty(itemGroup.SRItemType))
            {
                switch (itemGroup.SRItemType)
                {
                    case "01":
                    case "31":
                    case "41":
                        pnlItemSubGroup.Visible = AppSession.Parameter.IsUsingItemSubGroup;
                        break;

                    default:
                        pnlItemSubGroup.Visible = false;
                        break;
                }
            }
            else
                pnlItemSubGroup.Visible = false;
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ItemGroup entity)
        {
            entity.ItemGroupID = txtItemGroupID.Text;
            entity.ItemGroupName = txtItemGroupName.Text;
            entity.Initial = txtInitial.Text;
            entity.SRItemType = cboSRItemType.SelectedValue;
            entity.CitoValue = Convert.ToDecimal(txtCitoValue.Value);
            entity.IsCitoInPercent = chkIsCitoInPercent.Checked;
            entity.AccountID = ""; // txtAccountID.Text;
            entity.IsActive = chkIsActive.Checked;
            entity.RestrictionUserType = acbRestrictionUserType.Text;
            entity.CssClass = txtCssClass.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            AppStandardReferenceItemCollection coll = ItemSubGroups;
            foreach (AppStandardReferenceItem item in coll)
            {
                item.ReferenceID = txtItemGroupID.Text;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(ItemGroup entity, AppStandardReferenceItemCollection itemSubGroups)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                var isNewRecord = entity.es.IsAdded;

                entity.Save();
                itemSubGroups.Save();

                // Save ItemGroupUserType
                if (!isNewRecord)
                {
                    var igts = new ItemGroupUserTypeCollection();
                    igts.Query.Where(igts.Query.ItemGroupID == entity.ItemGroupID);
                    igts.LoadAll();
                    igts.MarkAllAsDeleted();
                    igts.Save();
                }

                char delimiter = acbRestrictionUserType.Delimiter.ToCharArray()[0];
                var value = acbRestrictionUserType.Text;
                if (!value.Contains(delimiter.ToString()))
                    value = value + delimiter;

                var stdiColl = new AppStandardReferenceItemCollection();
                stdiColl.Query.Where(stdiColl.Query.StandardReferenceID == AppEnum.StandardReference.UserType.ToString());
                stdiColl.LoadAll();

                var vals = value.Split(delimiter);
                foreach (var val in vals)
                {
                    if (string.IsNullOrWhiteSpace(val)) continue;

                    var itemID = string.Empty;
                    foreach (var item in stdiColl)
                    {
                        if (item.ItemName == val)
                        {
                            itemID = item.ItemID;
                            break;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(itemID)) continue;
                    var igt = new ItemGroupUserType();
                    igt.ItemGroupID = entity.ItemGroupID;
                    igt.SRUserType = itemID;
                    igt.Save();
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            ItemGroupQuery que = new ItemGroupQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (FormType == "service")
                que.Where(que.SRItemType.In("01", "31", "41", "61"));
            else if (FormType == "product")
                que.Where(que.SRItemType.In("11", "21", "81"));

            if (isNextRecord)
            {
                que.Where(que.ItemGroupID > txtItemGroupID.Text);
                que.OrderBy(que.ItemGroupID.Ascending);
            }
            else
            {
                que.Where(que.ItemGroupID < txtItemGroupID.Text);
                que.OrderBy(que.ItemGroupID.Descending);
            }
            ItemGroup entity = new ItemGroup();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged

        //protected void txtAccountID_TextChanged(object sender, EventArgs e)
        //{
        //    PopulateAccountName(true);
        //}

        //private void PopulateAccountName(bool isResetIdIfNotExist)
        //{
        //    if (txtAccountID.Text == string.Empty)
        //    {
        //        lblAccountName.Text = string.Empty;
        //        return;
        //    }
        //    ChartOfAccounts entity = new ChartOfAccounts();
        //    if (entity.LoadByPrimaryKey(txtAccountID.Text))
        //        lblAccountName.Text = entity.ChartOfAccountName;
        //    else
        //    {
        //        lblAccountName.Text = string.Empty;
        //        if (isResetIdIfNotExist)
        //            txtAccountID.Text = string.Empty;
        //    }
        //}

        #endregion

        #region Record Detail Method Function ItemSubGroup

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemSubGroup.Columns[0].Visible = isVisible;
            grdItemSubGroup.Columns[grdItemSubGroup.Columns.Count - 1].Visible = isVisible;

            grdItemSubGroup.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItemSubGroup.Rebind();
        }

        private AppStandardReferenceItemCollection ItemSubGroups
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collAppStandardReferenceItem_ItemSubGroup"];
                    if (obj != null)
                    {
                        return ((AppStandardReferenceItemCollection)(obj));
                    }
                }

                var coll = new AppStandardReferenceItemCollection();
                var query = new AppStandardReferenceItemQuery("a");
                query.Where(query.StandardReferenceID == AppEnum.StandardReference.ItemSubGroup, query.ReferenceID == txtItemGroupID.Text);
                query.Select(query);
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collAppStandardReferenceItem_ItemSubGroup"] = coll;
                return coll;
            }
            set { Session["collAppStandardReferenceItem_ItemSubGroup"] = value; }
        }

        private void PopulateGrid()
        {
            //Display Data Detail
            ItemSubGroups = null; //Reset Record Detail
            grdItemSubGroup.DataSource = ItemSubGroups; //Requery
            grdItemSubGroup.MasterTableView.IsItemInserted = false;
            grdItemSubGroup.MasterTableView.ClearEditItems();
            grdItemSubGroup.DataBind();
        }

        protected void grdItemSubGroup_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemSubGroup.DataSource = ItemSubGroups;
        }

        protected void grdItemSubGroup_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][AppStandardReferenceItemMetadata.ColumnNames.ItemID]);
            AppStandardReferenceItem entity = FindItem(itemId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItemSubGroup_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][AppStandardReferenceItemMetadata.ColumnNames.ItemID]);
            AppStandardReferenceItem entity = FindItem(itemId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItemSubGroup_InsertCommand(object source, GridCommandEventArgs e)
        {
            AppStandardReferenceItem entity = ItemSubGroups.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemSubGroup.Rebind();
        }

        private AppStandardReferenceItem FindItem(String itemId)
        {
            AppStandardReferenceItemCollection coll = ItemSubGroups;
            AppStandardReferenceItem retEntity = null;
            foreach (AppStandardReferenceItem rec in coll)
            {
                if (rec.ItemID.Equals(itemId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(AppStandardReferenceItem entity, GridCommandEventArgs e)
        {
            var ctl = (ItemSubGroupDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (ctl != null)
            {
                entity.StandardReferenceID = AppEnum.StandardReference.ItemSubGroup.ToString();
                entity.ItemID = ctl.ItemID;
                entity.ItemName = ctl.ItemName;
                entity.Note = string.Empty;
                entity.ReferenceID = txtItemGroupID.Text;
                entity.IsUsedBySystem = false;
                entity.IsActive = ctl.IsActive;
                entity.coaID = 0;
                entity.subledgerID = 0;
                entity.NumericValue = 0;
            }
        }

        #endregion

        protected void cboSRItemType_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (e.Value)
            {
                case "01":
                case "31":
                case "41":
                    pnlItemSubGroup.Visible = AppSession.Parameter.IsUsingItemSubGroup;
                    break;

                default:
                    pnlItemSubGroup.Visible = false;
                    break;
            }
        }
    }
}
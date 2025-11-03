using System;
using System.Collections.Generic;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Linq;

namespace Temiang.Avicenna.Module.ControlPanel.Setting
{
    public partial class StandardReferencePerGroupDetail : BasePageDetail
    {
        private string StdGroup
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["gr"]) ? string.Empty : Request.QueryString["gr"];
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            switch (StdGroup)
            {
                case "PINCIDENT":
                    ProgramID = AppConstant.Program.IncidentOtherMaster;
                    break;
                case "BLOODBANK":
                    ProgramID = AppConstant.Program.BloodBankStandardReference;
                    break;
                case "CSSD":
                    ProgramID = AppConstant.Program.CssdStandardReference;
                    break;
                case "PMKP":
                    ProgramID = AppConstant.Program.PmkpStandardReference;
                    break;
                case "ASSET":
                    ProgramID = AppConstant.Program.AssetStandardReference;
                    break;
                case "KEPK":
                    ProgramID = AppConstant.Program.KEPK_StandardReference;
                    break;
                case "LAUNDRY":
                    ProgramID = AppConstant.Program.LaundryStandardReference;
                    break;
                case "INV":
                    ProgramID = AppConstant.Program.InventoryStandardReference;
                    break;
                default:
                    ProgramID = AppConstant.Program.StandardReference;
                    break;
            }

            // Url Search & List
            UrlPageSearch = "StandardReferencePerGroupSearch.aspx?gr=" + StdGroup;
            UrlPageList = "StandardReferencePerGroupList.aspx?gr=" + StdGroup;

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
            OnPopulateEntryControl(new AppStandardReference());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new AppStandardReference();
            if (entity.LoadByPrimaryKey(txtStandardReferenceID.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity();
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new AppStandardReference();
            if (entity.LoadByPrimaryKey(txtStandardReferenceID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new AppStandardReference();
            entity.AddNew();
            SetEntityValue();
            SaveEntity();
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new AppStandardReference();
            if (entity.LoadByPrimaryKey(txtStandardReferenceID.Text))
            {
                SetEntityValue();
                SaveEntity();
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
            //auditLogFilter.PrimaryKeyData = string.Format("StandardReferenceID='{0}'", txtStandardReferenceID.Text.Trim());
            //auditLogFilter.TableName = "AppStandardReference";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AppStandardReference();
            if (parameters.Length > 0)
            {
                String itemId = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(itemId);
            }
            else
            {
                entity.LoadByPrimaryKey(txtStandardReferenceID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var std = (AppStandardReference)entity;
            txtStandardReferenceID.Text = std.StandardReferenceID;
            txtStandardReferenceName.Text = std.StandardReferenceName;
            chkIsHasCOA.Checked = std.HasCOA ?? false;
            chkIsNumericValue.Checked = std.IsNumericValue ?? false;

            PopulateItemGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue()
        {
            foreach (var item in AppStandardReferenceItems)
            {
                item.StandardReferenceID = txtStandardReferenceID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity()
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                AppStandardReferenceItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AppStandardReferenceQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.StandardReferenceID > txtStandardReferenceID.Text,
                          que.StandardReferenceGroup == StdGroup);
                que.OrderBy(que.StandardReferenceID.Ascending);
            }
            else
            {
                que.Where(que.StandardReferenceID < txtStandardReferenceID.Text,
                          que.StandardReferenceGroup == StdGroup);
                que.OrderBy(que.StandardReferenceID.Descending);
            }

            var entity = new AppStandardReference();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of Incident Type Component
        private AppStandardReferenceItemCollection AppStandardReferenceItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collAppStandardReferenceItem"];
                    if (obj != null)
                    {
                        return ((AppStandardReferenceItemCollection)(obj));
                    }
                }

                var coll = new AppStandardReferenceItemCollection();
                var query = new AppStandardReferenceItemQuery("a");
                ChartOfAccountsQuery coa = new ChartOfAccountsQuery("b");
                query.LeftJoin(coa).On(query.coaID == coa.ChartOfAccountId);
                query.Select
                    (
                        query, 
                        @"<CASE WHEN a.CustomField IS NULL THEN CAST(0 AS BIT) ELSE CASE WHEN a.CustomField = '1' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END END AS 'refTo_CustomField_IsNeedCrossMatchingProcess'>",
                        @"<CASE WHEN a.CustomField2 IS NULL THEN CAST(0 AS BIT) ELSE CASE WHEN a.CustomField2 = '1' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END END AS 'refTo_CustomField2_IsReturnable'>",
                        coa.ChartOfAccountCode.As("refTo_CoaCode"),
                        coa.ChartOfAccountName.As("refTo_CoaName")
                    );
                query.Where(query.StandardReferenceID == txtStandardReferenceID.Text);
                coll.Load(query);

                Session["collAppStandardReferenceItem"] = coll;
                return coll;
            }
            set
            {
                Session["collAppStandardReferenceItem"] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 2].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            grdItem.Columns.FindByUniqueName("NumericValue").Visible = chkIsNumericValue.Checked;
            grdItem.Columns.FindByUniqueName("IsNeedCrossMatchingProcess").Visible = txtStandardReferenceID.Text == "BloodGroup";
            grdItem.Columns.FindByUniqueName("IsReturnable").Visible = txtStandardReferenceID.Text == "BloodGroup";
            grdItem.Columns.FindByUniqueName("BackColorTemplateColumn").Visible = txtStandardReferenceID.Text == "RiskManagementBands";
            grdItem.Columns.FindByUniqueName("CoaCode").Visible = chkIsHasCOA.Checked;
            grdItem.Columns.FindByUniqueName("CoaName").Visible = chkIsHasCOA.Checked;
            grdItem.Columns.FindByUniqueName("openWorkTradeItem").Visible = txtStandardReferenceID.Text == "WorkTrade";
            grdItem.Columns.FindByUniqueName("ReferenceID").Visible = txtStandardReferenceID.Text != "RiskManagementBands";

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            AppStandardReferenceItems = null; //Reset Record Detail
            grdItem.DataSource = AppStandardReferenceItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private AppStandardReferenceItem FindItem(String itemId)
        {
            AppStandardReferenceItemCollection coll = AppStandardReferenceItems;
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

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = AppStandardReferenceItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][AppStandardReferenceItemMetadata.ColumnNames.ItemID]);
            AppStandardReferenceItem entity = FindItem(itemId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String itemId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][AppStandardReferenceItemMetadata.ColumnNames.ItemID]);
            AppStandardReferenceItem entity = FindItem(itemId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            AppStandardReferenceItem entity = AppStandardReferenceItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(AppStandardReferenceItem entity, GridCommandEventArgs e)
        {
            var userControl = (StandardReferencePerGroupItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.NumericValue = userControl.NumericValue;
                entity.Note = userControl.Note;
                entity.ReferenceID = userControl.ReferenceID;
                entity.IsActive = userControl.IsActive;
                entity.IsUsedBySystem = userControl.IsUsedBySystem;
                if (txtStandardReferenceID.Text == "BloodGroup")
                {
                    entity.CustomField = userControl.CustomField;
                    entity.IsNeedCrossMatchingProcess = entity.CustomField == "1";
                    entity.CustomField2 = userControl.CustomField2;
                    entity.IsReturnable = entity.CustomField2 == "1";
                }
                else
                {
                    entity.IsNeedCrossMatchingProcess = false;
                    entity.IsReturnable = false;
                }

                int Coa = 0;
                int SubLedger = 0;
                int.TryParse(userControl.CoaId, out Coa);
                int.TryParse(userControl.SubLedgerId, out SubLedger);

                entity.coaID = Coa;
                entity.subledgerID = SubLedger;

                var c = new ChartOfAccounts();
                if (c.LoadByPrimaryKey(Coa))
                {
                    entity.CoaCode = c.ChartOfAccountCode;
                    entity.CoaName = c.ChartOfAccountName;
                }
            }
        }
        #endregion
    }
}

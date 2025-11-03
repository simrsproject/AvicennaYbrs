using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.ControlPanel.Setting
{
    public partial class StandardReferenceDetail : BasePageDetail
    {
        private void SetEntityValue(AppStandardReference entity)
        {
            entity.StandardReferenceID = txtStandardReferenceID.Text;
            entity.StandardReferenceName = txtStandardReferenceName.Text;
            entity.StandardReferenceGroup = txtStandardReferenceGroup.Text;
            entity.ItemLength = Convert.ToInt32(txtItemLength.Value);
            entity.Note = txtNotes.Text;
            entity.IsUsedBySystem = chkIsUsedBySystem.Checked;
            entity.IsActive = chkIsActive.Checked;
            entity.HasCOA = chkIsHasCOA.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Item
            AppStandardReferenceItemCollection coll = AppStandardReferenceItems;
            foreach (AppStandardReferenceItem item in coll)
            {
                item.StandardReferenceID = txtStandardReferenceID.Text;

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
            AppStandardReferenceQuery que = new AppStandardReferenceQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.StandardReferenceID > txtStandardReferenceID.Text);
                que.OrderBy(que.StandardReferenceID.Ascending);
            }
            else
            {
                que.Where(que.StandardReferenceID < txtStandardReferenceID.Text);
                que.OrderBy(que.StandardReferenceID.Descending);
            }
            AppStandardReference entity = new AppStandardReference();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            AppStandardReference entity = new AppStandardReference();
            if (parameters.Length > 0)
            {
                String standardReferenceID = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(standardReferenceID);
            }
            else
                entity.LoadByPrimaryKey(txtStandardReferenceID.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            AppStandardReference appStandardReference = (AppStandardReference)entity;
            txtStandardReferenceID.Text = appStandardReference.StandardReferenceID;
            txtStandardReferenceName.Text = appStandardReference.StandardReferenceName;
            txtStandardReferenceGroup.Text = appStandardReference.StandardReferenceGroup;
            txtItemLength.Value = Convert.ToDouble(appStandardReference.ItemLength);
            txtNotes.Text = appStandardReference.Note;
            chkIsUsedBySystem.Checked = appStandardReference.IsUsedBySystem ?? false;
            chkIsActive.Checked = appStandardReference.IsActive ?? false;
            chkIsHasCOA.Checked = appStandardReference.HasCOA ?? false;

            //Display Data Detail
            PopulateGridDetail(appStandardReference);
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdStandardReferenceItem, grdStandardReferenceItem);
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AppStandardReference());
            chkIsUsedBySystem.Checked = false;
            chkIsActive.Checked = true;
        }

        protected override void OnMenuEditClick()
        {
            chkIsUsedBySystem.Enabled = false;
            if (chkIsUsedBySystem.Checked)
            {
                txtStandardReferenceName.ReadOnly = true;
                txtStandardReferenceGroup.ReadOnly = true;
                txtItemLength.ReadOnly = true;
                txtNotes.ReadOnly = true;
                chkIsHasCOA.Enabled = false;
                chkIsActive.Enabled = false;
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
            auditLogFilter.PrimaryKeyData = "StandardReferenceID='" + txtStandardReferenceID.Text.Trim() + "'";
            auditLogFilter.TableName = "AppStandardReference";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtStandardReferenceID.ReadOnly = (newVal != AppEnum.DataMode.New);
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "StandardReferenceSearch.aspx";
            UrlPageList = "StandardReferenceList.aspx";

            ProgramID = AppConstant.Program.StandardReference;

            WindowSearch.Height = 400;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            AppStandardReference entity = new AppStandardReference();
            if (entity.LoadByPrimaryKey(txtStandardReferenceID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(AppStandardReference entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                AppStandardReferenceItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            AppStandardReference entity = new AppStandardReference();
            if (entity.LoadByPrimaryKey(txtStandardReferenceID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            AppStandardReference entity = new AppStandardReference();
            if (entity.LoadByPrimaryKey(txtStandardReferenceID.Text))
            {
                if (entity.IsUsedBySystem ?? false)
                {
                    args.MessageText = "Standard Reference used in the system. Data cannot be deleted.";
                    args.IsCancel = true;
                    return;
                }
                else
                {
                    entity.MarkAsDeleted();
                    AppStandardReferenceItemCollection itemDetail = new AppStandardReferenceItemCollection();
                    itemDetail.LoadByStandardReferenceID(txtStandardReferenceID.Text);
                    itemDetail.MarkAllAsDeleted();
                    using (esTransactionScope trans = new esTransactionScope())
                    {
                        AppStandardReferenceItems.Save();

                        entity.Save();

                        //Commit if success, Rollback if failed
                        trans.Complete();
                    }
                }
            }
        }

        #endregion

        #region Record Detail Method Function

        private AppStandardReferenceItemCollection AppStandardReferenceItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collAppStandardReferenceItem"];
                    if (obj != null)
                        return ((AppStandardReferenceItemCollection)(obj));
                }

                AppStandardReferenceItemCollection coll = new AppStandardReferenceItemCollection();
                AppStandardReferenceItemQuery itemQuery = new AppStandardReferenceItemQuery("a");
                ChartOfAccountsQuery coa = new ChartOfAccountsQuery("b");
                itemQuery.LeftJoin(coa).On(itemQuery.coaID == coa.ChartOfAccountId)
                    .Where(itemQuery.StandardReferenceID == txtStandardReferenceID.Text)
                    .Select(
                        itemQuery, 
                        coa.ChartOfAccountCode.As("refTo_CoaCode"),
                        coa.ChartOfAccountName.As("refTo_CoaName")
                    );
                itemQuery.OrderBy(itemQuery.ItemID.Ascending);
                coll.Load(itemQuery);


                //Bila nama session dirubah, rubah jug yg di StandardReferenceItemDetail.ascx.cs
                Session["collAppStandardReferenceItem"] = coll;
                return coll;
            }
            set 
            { 
                Session["collAppStandardReferenceItem"] = value; 
            }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdStandardReferenceItem.Columns[0].Visible = isVisible;
            grdStandardReferenceItem.Columns[grdStandardReferenceItem.Columns.Count - 1].Visible = isVisible;

            grdStandardReferenceItem.MasterTableView.CommandItemDisplay = isVisible
                ? GridCommandItemDisplay.Top
                : GridCommandItemDisplay.None;

            grdStandardReferenceItem.Rebind();
        }

        private void PopulateGridDetail(AppStandardReference appStandardReference)
        {
            //Reset Record Detail
            AppStandardReferenceItems = null; 

            //Requery Record Detail
            grdStandardReferenceItem.DataSource = AppStandardReferenceItems;
            grdStandardReferenceItem.MasterTableView.IsItemInserted = false;
            grdStandardReferenceItem.MasterTableView.ClearEditItems();
            grdStandardReferenceItem.DataBind();

            grdStandardReferenceItem.Columns.FindByUniqueName("CoaCode").Visible = appStandardReference.HasCOA ?? false;
            grdStandardReferenceItem.Columns.FindByUniqueName("CoaName").Visible = appStandardReference.HasCOA ?? false;
            grdStandardReferenceItem.Columns.FindByUniqueName("ReferenceID").Visible = (string.IsNullOrEmpty(appStandardReference.StandardReferenceID) || appStandardReference.StandardReferenceID != "PrescriptionCategory");
            grdStandardReferenceItem.Columns.FindByUniqueName("BackColorTemplateColumn").Visible = (!string.IsNullOrEmpty(appStandardReference.StandardReferenceID) && appStandardReference.StandardReferenceID == "PrescriptionCategory");
        }

        protected void grdStandardReferenceItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdStandardReferenceItem.DataSource = AppStandardReferenceItems;
        }

        protected void grdStandardReferenceItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            string itemID = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ItemID"].ToString();

            AppStandardReferenceItem item = AppStandardReferenceItems.FindByPrimaryKey(txtStandardReferenceID.Text,
                itemID);

            SetEntityValue(item, e);
        }

        private void SetEntityValue(AppStandardReferenceItem item, GridCommandEventArgs e)
        {
            StandardReferenceItemDetail ctl = (StandardReferenceItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (ctl != null)
            {
                item.ItemID = ctl.ItemID;
                item.ItemName = ctl.ItemName;
                item.Note = ctl.Note;
                item.ReferenceID = ctl.ReferenceID;
                item.IsUsedBySystem = ctl.IsUsedBySystem;
                item.IsActive = ctl.IsActive;
                item.CustomField = ctl.CustomField;
                item.CustomField2 = ctl.CustomField2;

                int Coa = 0;
                int SubLedger = 0;
                int.TryParse(ctl.CoaId, out Coa);
                int.TryParse(ctl.SubLedgerId, out SubLedger);
                item.coaID = Coa;
                item.subledgerID = SubLedger;

                var c = new ChartOfAccounts();
                if (c.LoadByPrimaryKey(Coa))
                {
                    item.CoaCode = c.ChartOfAccountCode;
                    item.CoaName = c.ChartOfAccountName;
                }

                item.NumericValue = ctl.NumericValue;
                item.LineNumber = ctl.LineNumber;
            }
        }

        protected void grdStandardReferenceItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            string itemID = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ItemID"].ToString();

            AppStandardReferenceItemCollection coll = AppStandardReferenceItems;
            foreach (AppStandardReferenceItem item in coll)
            {
                if (item.ItemID.Equals(itemID) & item.IsUsedBySystem.Equals(false))
                {
                    item.MarkAsDeleted();
                    break;
                }

            }
        }

        protected void grdStandardReferenceItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            AppStandardReferenceItem item = AppStandardReferenceItems.AddNew();
            SetEntityValue(item, e);
            //Stay in insert mode
            e.Canceled = true;
            grdStandardReferenceItem.Rebind();
        }

        #endregion
    }
}
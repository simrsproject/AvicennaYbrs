using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.Cssd.Master
{
    public partial class ItemDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ItemSearch.aspx";
            UrlPageList = "ItemList.aspx";

            this.WindowSearch.Height = 300;
            ProgramID = AppConstant.Program.CssdItem;

            if(AppSession.Parameter.IsCentralizedCssd)
            {
                tabDetail.Tabs[1].Visible = true;
            }
            else
            {
                tabDetail.Tabs[1].Visible = false;
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
            OnPopulateEntryControl(new Item());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            if (CssdItemDetails.Count == 0)
            {
                args.MessageText = "Item detail is not defined.";
                args.IsCancel = true;
                return;
            }

            if (CssdItemBalances.Count > 0)
            {
                args.MessageText = "Detail items cannot be deleted.";
                args.IsCancel = true;
                return;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {

            if (CssdItemDetails.Count == 0)
            {
                args.MessageText = "Item detail is not defined.";
                args.IsCancel = true;
                return;
            }


            var entity = new Item();
            if (entity.LoadByPrimaryKey(txtItemID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
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
            //auditLogFilter.PrimaryKeyData = string.Format("ItemID='{0}'", txtItemID.Text.Trim());
            //auditLogFilter.TableName = "Item";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtItemID.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItemDetail(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Item();
            if (parameters.Length > 0)
            {
                String id = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id);
            }
            else
            {
                entity.LoadByPrimaryKey(txtItemID.Text);
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var i = (Item)entity;
            txtItemID.Text = i.ItemID;
            txtItemName.Text = i.ItemName;

            if (!string.IsNullOrEmpty(i.ItemGroupID))
            {
                var gQuery = new ItemGroupQuery();
                gQuery.Where(gQuery.ItemGroupID == i.str.ItemGroupID);
                cboItemGroupID.DataSource = gQuery.LoadDataTable();
                cboItemGroupID.DataBind();
                cboItemGroupID.SelectedValue = i.ItemGroupID;
            }
            else
            {
                cboItemGroupID.Items.Clear();
                cboItemGroupID.Text = string.Empty;
            }

            var srItemUnit = string.Empty;
            switch (i.SRItemType)
            {
                case ItemType.Medical:
                    var ipm = new ItemProductMedic();
                    ipm.LoadByPrimaryKey(i.ItemID);
                    srItemUnit = ipm.SRItemUnit;
                    break;
                case ItemType.NonMedical:
                    var ipnm = new ItemProductNonMedic();
                    ipnm.LoadByPrimaryKey(i.ItemID);
                    srItemUnit = ipnm.SRItemUnit;
                    break;
                case ItemType.Kitchen:
                    var k = new ItemKitchen();
                    k.LoadByPrimaryKey(i.ItemID);
                    srItemUnit = k.SRItemUnit;
                    break;
            }

            if (!string.IsNullOrEmpty(srItemUnit))
            {
                var q = new AppStandardReferenceItemQuery();
                q.Where(q.StandardReferenceID == AppEnum.StandardReference.ItemUnit, q.ItemID == srItemUnit);
                cboSRItemUnit.DataSource = q.LoadDataTable();
                cboSRItemUnit.DataBind();
                cboSRItemUnit.SelectedValue = srItemUnit;
            }
            else
            {
                cboSRItemUnit.Items.Clear();
                cboSRItemUnit.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(i.SRCssdItemGroup))
            {
                var q = new AppStandardReferenceItemQuery();
                q.Where(q.StandardReferenceID == AppEnum.StandardReference.CssdItemGroup, q.ItemID == i.SRCssdItemGroup);
                cboSRCssdItemGroup.DataSource = q.LoadDataTable();
                cboSRCssdItemGroup.DataBind();
                cboSRCssdItemGroup.SelectedValue = i.SRCssdItemGroup;
            }
            else
            {
                cboSRCssdItemGroup.Items.Clear();
                cboSRCssdItemGroup.Text = string.Empty;
            }
            chkIsActive.Checked = i.IsActive ?? false;
            chkIsItemProduction.Checked = i.IsItemProduction ?? false;
            txtCssdPackagingCostAmount.Value = Convert.ToDouble(i.CssdPackagingCostAmount);

            PopulateGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(Item entity)
        {
            entity.SRCssdItemGroup = cboSRCssdItemGroup.SelectedValue;
            entity.CssdPackagingCostAmount = Convert.ToDecimal(txtCssdPackagingCostAmount.Value);
            
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            foreach (var item in CssdItemDetails)
            {
                item.ItemID = txtItemID.Text;
                item.LastUpdateDateTime = DateTime.Now;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }

            ///*Table CssdItemBalance*/
            CssdItemBalanceCollection collUnit = CssdItemBalances;
            foreach (CssdItemBalance unit in collUnit)
            {

                unit.ItemID = txtItemID.Text;

                //Last Update Status
                if (unit.es.IsAdded || unit.es.IsModified)
                {
                    unit.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    unit.LastUpdateDateTime = DateTime.Now;
                }
            }

        }

        private void SaveEntity(Item entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                CssdItemDetails.Save();
                CssdItemBalances.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ItemQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ItemID > txtItemID.Text, que.IsNeedToBeSterilized == true);
                que.OrderBy(que.ItemID.Ascending);
            }
            else
            {
                que.Where(que.ItemID < txtItemID.Text, que.IsNeedToBeSterilized == true);
                que.OrderBy(que.ItemID.Descending);
            }
            var entity = new Item();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged
        #endregion

        #region Record Detail Method Item Detail

        private void RefreshCommandItemDetail(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdCssdItemDetail.Columns[0].Visible = isVisible;
            grdCssdItemDetail.Columns[grdCssdItemDetail.Columns.Count - 1].Visible = isVisible;

            grdCssdItemDetail.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdCssdItemDetail.Rebind();


            grdServiceUnit.Columns[0].Visible = isVisible;
            grdServiceUnit.Columns[grdServiceUnit.Columns.Count - 1].Visible = isVisible;

            grdServiceUnit.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdServiceUnit.Rebind();


        }

        private CssdItemDetailCollection CssdItemDetails
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCssdItemDetail"];
                    if (obj != null)
                    {
                        return ((CssdItemDetailCollection)(obj));
                    }
                }

                var coll = new CssdItemDetailCollection();
                var query = new CssdItemDetailQuery("a");
                var itemq = new ItemQuery("b");
                var itemDetailq = new VwItemProductMedicNonMedicQuery("c");

                query.Select
                    (
                        query,
                        itemq.ItemName.As("refToItem_ItemName"),
                        itemDetailq.SRItemUnit.As("refToVwItem_SRItemUnit")
                    );
                query.InnerJoin(itemq).On(query.ItemDetailID == itemq.ItemID);
                query.InnerJoin(itemDetailq).On(query.ItemDetailID == itemDetailq.ItemID);
                query.Where(query.ItemID == txtItemID.Text);
                query.OrderBy
                    (
                        query.ItemDetailID.Ascending
                    );
                coll.Load(query);

                Session["collCssdItemDetail"] = coll;
                return coll;
            }
            set { Session["collCssdItemDetail"] = value; }
        }

        private void PopulateGrid()
        {
            //Display Data Detail
            CssdItemDetails = null; //Reset Record Detail
            grdCssdItemDetail.DataSource = CssdItemDetails; //Requery
            grdCssdItemDetail.MasterTableView.IsItemInserted = false;
            grdCssdItemDetail.MasterTableView.ClearEditItems();
            grdCssdItemDetail.DataBind();
        }

        protected void grdCssdItemDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdCssdItemDetail.DataSource = CssdItemDetails;
        }


        protected void grdCssdItemDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CssdItemDetailMetadata.ColumnNames.ItemDetailID]);

            CssdItemDetail entity = FindItem(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }
        protected void grdCssdItemDetail_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CssdItemDetailMetadata.ColumnNames.ItemDetailID]);

            CssdItemDetail entity = FindItem(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }
        protected void grdCssdItemDetail_InsertCommand(object source, GridCommandEventArgs e)
        {
            CssdItemDetail entity = CssdItemDetails.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdCssdItemDetail.Rebind();
        }

        private CssdItemDetail FindItem(String id)
        {
            CssdItemDetailCollection coll = CssdItemDetails;
            return coll.FirstOrDefault(rec => rec.ItemDetailID.Equals(id));
        }

        private void SetEntityValue(CssdItemDetail entity, GridCommandEventArgs e)
        {
            var userControl = (ItemDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemDetailID = userControl.ItemDetailID;
                entity.ItemDetailName = userControl.ItemDetailName;
                entity.Qty = userControl.Qty;
                entity.SRItemUnit = userControl.SrItemUnit;
            }
        }

        #endregion

        #region ComboBox Function

        protected void cboItemGroupID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemGroupName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemGroupID"].ToString();
        }

        protected void cboSRCssdItemGroup_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.Where
                (
                    query.Or
                        (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                        ),
                        query.StandardReferenceID == AppEnum.StandardReference.CssdItemGroup.ToString(),
                        query.IsActive == true
                );

            cboSRCssdItemGroup.DataSource = query.LoadDataTable();
            cboSRCssdItemGroup.DataBind();
        }

        protected void cboStandardReferenceItem_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        #endregion


        #region ServiceUnit
        private CssdItemBalanceCollection CssdItemBalances
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCssdItemBalance"];
                    if (obj != null)
                    {
                        return ((CssdItemBalanceCollection)(obj));
                    }
                }

                var coll = new CssdItemBalanceCollection();
                var query = new CssdItemBalanceQuery("a");
                var unit = new ServiceUnitQuery("b");

                string itemId = txtItemID.Text;
                query.Where(query.ItemID == itemId);
                query.Select(query.SelectAllExcept(), unit.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"));
                query.LeftJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                coll.Load(query);

                Session["collCssdItemBalance"] = coll;
                return coll;
            }
            set { Session["collCssdItemBalance"] = value; }
        }

        private void PopulateServiceUnitGrid()
        {
            //Display Data Detail
            CssdItemBalances = null; //Reset Record Detail
            grdServiceUnit.DataSource = CssdItemBalances; //Requery
            grdServiceUnit.MasterTableView.IsItemInserted = false;
            grdServiceUnit.MasterTableView.ClearEditItems();
            grdServiceUnit.DataBind();
        }

        protected void grdServiceUnit_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdServiceUnit.DataSource = CssdItemBalances;
        }

        protected void grdServiceUnit_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String unitID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CssdItemBalanceMetadata.ColumnNames.ServiceUnitID]);
            CssdItemBalance entity = FindItemServiceUnit(unitID);
            if (entity.Balance > 0)
                entity.MarkAsDeleted();
        }

        protected void grdServiceUnit_InsertCommand(object source, GridCommandEventArgs e)
        {
            CssdItemBalance entity = CssdItemBalances.AddNew();
            SetEntityValue2(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdServiceUnit.Rebind();
        }

        protected void grdServiceUnit_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String unitID =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        CssdItemBalanceMetadata.ColumnNames.ServiceUnitID]);
            CssdItemBalance entity = FindItemServiceUnit(unitID);
            if (entity != null)
                SetEntityValue2(entity, e);
        }

        private CssdItemBalance FindItemServiceUnit(String unitId)
        {
            CssdItemBalanceCollection coll = CssdItemBalances;
            CssdItemBalance retEntity = null;
            foreach (CssdItemBalance rec in coll)
            {
                if (rec.ServiceUnitID.Equals(unitId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }


        private void SetEntityValue2(CssdItemBalance entity, GridCommandEventArgs e)
        {
            var userControl = (ItemServiceUnitDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ServiceUnitID = userControl.ServiceUnitID;
                entity.ServiceUnitName = userControl.ServiceUnitName;

            }
        }
        #endregion

    }
}

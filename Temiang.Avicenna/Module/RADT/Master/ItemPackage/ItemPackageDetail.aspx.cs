using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemPackageDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ItemPackageSearch.aspx";
            UrlPageList = "ItemPackageList.aspx";

            ProgramID = AppConstant.Program.PackageItem;

            if (!IsPostBack)
            {
                var groups = new ItemGroupCollection();
                groups.Query.Where(groups.Query.IsActive == true, groups.Query.SRItemType == ItemType.Package);
                groups.LoadAll();

                cboItemGroupID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var group in groups)
                {
                    var itemGroupName = string.IsNullOrEmpty(group.Initial) ? group.ItemGroupName : group.ItemGroupName + " [" + group.Initial + "]";
                    cboItemGroupID.Items.Add(new RadComboBoxItem(itemGroupName, group.ItemGroupID));
                }

                StandardReference.InitializeIncludeSpace(cboBillingGroup, AppEnum.StandardReference.BillingGroup);
                StandardReference.InitializeIncludeSpace(cboSRBpjsItemGroup, AppEnum.StandardReference.BpjsItemGroup);
                StandardReference.InitializeIncludeSpace(cboSREklaimGroup, AppEnum.StandardReference.EklaimTariffGroup);

                grdItemPackage.MasterTableView.Columns[6].Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH";
                grdItemPackage.MasterTableView.Columns[7].Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH";
                grdItemPackage.MasterTableView.Columns[9].Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH";

                grdItemPackage.Columns.FindByUniqueName("IsAutoApprove").Visible = AppSession.Parameter.IsAutoApprovePackage;

                trBpjsItemGroup.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB";
            }

            //PopUp Search
            if (!IsCallback)
            {
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.Item, this.Page);
                //PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ItemServices, this.Page);
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ServiceUnit, Page);
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItemPackage, grdItemPackage);
            ajax.AddAjaxSetting(grdPriceHistory, grdPriceHistory);
            ajax.AddAjaxSetting(grdServiceUnit, grdServiceUnit);
        }

        protected override void OnMenuNewClick()
        {
            cboGuarantorID.DataSource = null;
            cboGuarantorID.DataBind();
            cboGuarantorID.SelectedValue = string.Empty;
            cboGuarantorID.Text = string.Empty;

            OnPopulateEntryControl(new Item());
            if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdServiceAutomaticUseGroupInitial))
                txtItemID.ReadOnly = true;

            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new Item();
            if (entity.LoadByPrimaryKey(txtItemID.Text))
            {
                // cek apakah master item sudah ada transaksi
                var cek = new VwItemsAlreadyUsedCollection();
                cek.Query.Where(cek.Query.ItemID == txtItemID.Text);
                cek.LoadAll();
                if (cek.Count > 0)
                {
                    args.MessageText = "Item already used in transaction.";
                    args.IsCancel = true;
                    return;
                }

                entity.MarkAsDeleted();

                var coll = new ItemPackageCollection();
                coll.Query.Where(coll.Query.ItemID == txtItemID.Text);
                coll.LoadAll();
                coll.MarkAllAsDeleted();

                string itemID = txtItemID.Text;
                var unitColl = new ServiceUnitItemServiceCollection();
                unitColl.Query.Where(unitColl.Query.ItemID == itemID);
                unitColl.LoadAll();
                unitColl.MarkAllAsDeleted();

                using (var trans = new esTransactionScope())
                {
                    coll.Save();
                    unitColl.Save();

                    entity.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboItemGroupID.SelectedValue) && AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdServiceAutomaticUseGroupInitial))
            {
                args.MessageText = "Item Group must selected first";
                args.IsCancel = true;
                return;
            }

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdServiceAutomaticUseGroupInitial))
            {
                txtItemID.Text = Helper.GetItemProductIDUseGroupInitial(cboItemGroupID.SelectedValue);
            }
            else if (string.IsNullOrEmpty(txtItemID.Text))
            {
                args.MessageText = "Item ID required.";
                args.IsCancel = true;
                return;
            }

            var entity = new Item();
            if (entity.LoadByPrimaryKey(txtItemID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new Item();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
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
            auditLogFilter.PrimaryKeyData = string.Format("ItemID='{0}'", txtItemID.Text.Trim());
            auditLogFilter.TableName = "Item";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtItemID.ReadOnly = (newVal != AppEnum.DataMode.New);
            if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdServiceAutomaticUseGroupInitial))
            {
                cboItemGroupID.Enabled = (newVal == AppEnum.DataMode.New);
            }
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Item();
            if (parameters.Length > 0)
            {
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(parameters[0]);
            }
            else
                entity.LoadByPrimaryKey(txtItemID.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var item = (Item)entity;
            txtItemID.Text = item.ItemID;
            cboItemGroupID.SelectedValue = item.ItemGroupID;
            txtItemName.Text = item.ItemName;
            chkIsActive.Checked = item.IsActive ?? false;
            chkIsDelegationToNurse.Checked = item.IsDelegationToNurse ?? false;
            txtNotes.Text = item.Notes;
            cboBillingGroup.SelectedValue = item.SRBillingGroup;
            cboSRBpjsItemGroup.SelectedValue = item.SRBpjsItemGroup;
            if (!string.IsNullOrEmpty(item.GuarantorID))
            {
                var guar = new GuarantorQuery("a");
                guar.Select(
                    guar.GuarantorID,
                    guar.GuarantorName
                    );
                guar.Where(guar.GuarantorID == item.str.GuarantorID);
                DataTable dtb = guar.LoadDataTable();
                cboGuarantorID.DataSource = dtb;
                cboGuarantorID.DataBind();
                cboGuarantorID.SelectedValue = item.GuarantorID;
                cboGuarantorID.Text = dtb.Rows[0]["GuarantorName"].ToString();
            }
            else
            {
                cboGuarantorID.Items.Clear();
                cboGuarantorID.Text = string.Empty;
            }

            cboSREklaimGroup.SelectedValue = item.SREklaimTariffGroup;
            if (item.ValidityPeriodFrom != null)
                txtValidityPeriodFrom.SelectedDate = item.ValidityPeriodFrom;
            else
                txtValidityPeriodFrom.Clear();

            if (item.ValidityPeriodTo != null)
                txtValidityPeriodTo.SelectedDate = item.ValidityPeriodTo;
            else
                txtValidityPeriodTo.Clear();

            //Display Data Detail
            PopulateItemPackageGrid();

            grdPriceHistory.DataSource = GetItemTariffHistory();
            grdPriceHistory.MasterTableView.IsItemInserted = false;
            grdPriceHistory.MasterTableView.ClearEditItems();
            grdPriceHistory.DataBind();

            PopulateServiceUnitItemServiceGrid();
        }

        private void SetEntityValue(esItem entity)
        {
            entity.ItemID = txtItemID.Text;
            entity.ItemGroupID = cboItemGroupID.SelectedValue;
            entity.SRItemType = BusinessObject.Reference.ItemType.Package;
            entity.ItemName = txtItemName.Text;
            entity.IsActive = chkIsActive.Checked;
            entity.IsDelegationToNurse = chkIsDelegationToNurse.Checked;
            entity.Notes = txtNotes.Text;
            entity.GuarantorID = cboGuarantorID.SelectedValue;
            entity.IsNeedToBeSterilized = false;
            entity.SRBillingGroup = cboBillingGroup.SelectedValue;
            entity.SRBpjsItemGroup = cboSRBpjsItemGroup.SelectedValue;
            entity.SREklaimTariffGroup = cboSREklaimGroup.SelectedValue;
            if (txtValidityPeriodFrom.IsEmpty)
                entity.str.ValidityPeriodFrom = string.Empty;
            else
                entity.ValidityPeriodFrom = txtValidityPeriodFrom.SelectedDate;

            if (txtValidityPeriodTo.IsEmpty)
                entity.str.ValidityPeriodTo = string.Empty;
            else
                entity.ValidityPeriodTo = txtValidityPeriodTo.SelectedDate;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Created
            if (entity.es.IsAdded)
            {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = DateTime.Now;
            }

            //Detail
            foreach (ItemPackage package in ItemPackages)
            {
                package.ItemID = txtItemID.Text;

                //Last Update Status
                if (package.es.IsAdded || package.es.IsModified)
                {
                    package.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    package.LastUpdateDateTime = DateTime.Now;
                }

                if (!ItemPackageTariffComponents.Any(i => i.DetailItemID == package.DetailItemID))
                {
                    var coll = Helper.Tariff.GetItemTariffComponentCollection(DateTime.Now.Date, AppSession.Parameter.DefaultTariffType,
                        AppSession.Parameter.DefaultTariffClass, package.DetailItemID);
                    foreach (var c in coll)
                    {
                        var comp = ItemPackageTariffComponents.AddNew();
                        comp.ItemID = package.ItemID;
                        comp.DetailItemID = package.DetailItemID;
                        comp.TariffComponentID = c.TariffComponentID;
                        comp.Price = c.Price;
                        if (package.IsDiscountInPercent == true)
                            comp.Discount = comp.Price * package.DiscountValue / 100;
                        else
                            comp.Discount = 0;
                        comp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        comp.LastUpdateDateTime = DateTime.Now;
                    }
                }
                else
                {
                    foreach (var comp in ItemPackageTariffComponents.Where(i => i.DetailItemID == package.DetailItemID))
                    {
                        comp.ItemID = txtItemID.Text;

                        if (package.IsDiscountInPercent == true)
                            comp.Discount = comp.Price * package.DiscountValue / 100;

                        //Last Update Status
                        if (comp.es.IsAdded || comp.es.IsModified)
                        {
                            comp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            comp.LastUpdateDateTime = DateTime.Now;
                        }
                    }
                }

                //untuk memunculkan diskon di grid detail : tambahan
                package.IsDiscountInPercent = false;
                package.DiscountValue = ItemPackageTariffComponents.Where(i => i.DetailItemID == package.DetailItemID).Sum(i => (i.Discount ?? 0));
            }

            ServiceUnitItemServiceCollection collUnit = ServiceUnitItemServices;
            foreach (ServiceUnitItemService unit in collUnit)
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

        private void SaveEntity(esEntity entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                ItemPackages.Save();
                ItemPackageTariffComponents.Save();
                ServiceUnitItemServices.Save();
                //ServiceUnitItemServiceCompMappings.Save();
                //ServiceUnitItemServiceClasses.Save();

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
                que.Where(que.ItemID > txtItemID.Text, que.SRItemType == BusinessObject.Reference.ItemType.Package);
                que.OrderBy(que.ItemID.Ascending);
            }
            else
            {
                que.Where(que.ItemID < txtItemID.Text, que.SRItemType == BusinessObject.Reference.ItemType.Package);
                que.OrderBy(que.ItemID.Descending);
            }

            var entity = new Item();
            entity.Load(que);
            OnPopulateEntryControl(entity);

            //grdPriceHistory.DataSource = GetItemTariffHistory();
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemPackage.Columns[0].Visible = isVisible;
            grdItemPackage.Columns[grdItemPackage.Columns.Count - 1].Visible = isVisible;
            grdItemPackage.Columns[grdItemPackage.Columns.Count - 2].Visible = isVisible;

            grdItemPackage.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdItemPackage.Rebind();

            grdServiceUnit.Columns[0].Visible = isVisible;
            grdServiceUnit.Columns[grdServiceUnit.Columns.Count - 1].Visible = isVisible;

            grdServiceUnit.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdServiceUnit.Rebind();
        }

        private ItemPackageCollection ItemPackages
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemPackage"];
                    if (obj != null)
                        return ((ItemPackageCollection)(obj));
                }

                var coll = new ItemPackageCollection();
                var query = new ItemPackageQuery("a");
                var item = new ItemQuery("b");
                var su = new ServiceUnitQuery("c");

                query.Select(
                    query,
                    item.ItemName.As("refToItem_ItemName"),
                    su.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"),
                    "<0 AS refToItemPackage_Price>",
                    "<0 AS refToItemPackage_Discount>",
                    "<0 AS refToItemPackage_Total>"
                    );
                query.InnerJoin(item).On(query.DetailItemID == item.ItemID);
                query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);
                query.Where(query.ItemID == txtItemID.Text);
                query.OrderBy(query.DetailItemID.Ascending);

                coll.Load(query);

                foreach (var a in coll)
                {
                    a.Total = (a.Price - a.Discount) * (a.Quantity ?? 0);
                }

                Session["collItemPackage"] = coll;
                return coll;
            }
            set { Session["collItemPackage"] = value; }
        }

        private ItemPackageTariffComponentCollection ItemPackageTariffComponents
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemPackageTariffComponent"];
                    if (obj != null)
                        return ((ItemPackageTariffComponentCollection)(obj));
                }

                var coll = new ItemPackageTariffComponentCollection();

                var query = new ItemPackageTariffComponentQuery("a");
                var comp = new TariffComponentQuery("b");
                var item = new ItemQuery("c");

                query.Select(
                    query,
                    comp.TariffComponentName.As("refToTariffComponent_TariffComponentName")
                    );
                query.LeftJoin(comp).On(query.TariffComponentID == comp.TariffComponentID);
                query.InnerJoin(item).On(query.DetailItemID == item.ItemID);
                query.Where(query.ItemID == txtItemID.Text);
                string[] ItemType = { "11", "21", "81" };
                query.Where(query.Or(comp.TariffComponentID.IsNotNull(), item.SRItemType.In(ItemType)));

                coll.Load(query);

                Session["collItemPackageTariffComponent"] = coll;
                return coll;
            }
            set
            { Session["collItemPackageTariffComponent"] = value; }
        }

        private void PopulateItemPackageGrid()
        {
            //Display Data Detail
            ItemPackageTariffComponents = null;
            var comps = ItemPackageTariffComponents;

            ItemPackages = null; //Reset Record Detail

            var packages = ItemPackages;

            foreach (var c in packages)
            {
                var price = ItemPackageTariffComponents.Where(i => i.DetailItemID == c.DetailItemID).Select(i => i.Price).Sum() ?? 0;
                var discount = ItemPackageTariffComponents.Where(i => i.DetailItemID == c.DetailItemID).Select(i => i.Discount).Sum() ?? 0;
                var tariffComps = Helper.Tariff.GetItemTariffComponentCollection(DateTime.Now.Date, AppSession.Parameter.DefaultTariffType,
                                                                            AppSession.Parameter.DefaultTariffClass, c.DetailItemID);
                var defaultPriceTariffComp = tariffComps.Sum(t => t.Price ?? 0);

                // ambil perubahan harga dari ItemPackageTariffComponent jika harga pada ItemPackageTariffComponent berbeda dengan harga pada ItemTariffComponent (Fajri - 2023/11/09)
                if (price != defaultPriceTariffComp)
                {
                    c.Price = price;
                    c.Discount = discount;
                    c.Total = (c.Price - c.Discount) * c.Quantity.Value;
                }
                else
                {
                    if (price > 0)
                    {
                        c.Price = price;
                        c.Discount = discount;
                        c.Total = (c.Price - c.Discount) * c.Quantity.Value;
                    }
                    else
                    {
                        if (tariffComps.Count() > 0) 
                        { 
                            c.Price = defaultPriceTariffComp;
                            c.Discount = 0;
                            c.Total = c.Price * c.Quantity.Value;
                        }
                    }
                }
            }

            var table = (DataTable)ViewState["tblItemPackage"];
            var trueColumns = ViewState["listTrueColumns"] as List<string> ?? new List<string>();

            if (table == null)
            {
                table = new DataTable();

                foreach (esColumnMetadata column in packages.es.Meta.Columns)
                {
                    table.Columns.Add(new DataColumn(column.Name, column.Type));
                    trueColumns.Add(column.Name);
                }

                table.Columns.Add(new DataColumn("refToItem_ItemName", typeof(string)));
                table.Columns.Add(new DataColumn("refToServiceUnit_ServiceUnitName", typeof(string)));
                table.Columns.Add(new DataColumn("refToItemPackage_Price", typeof(decimal)));
                table.Columns.Add(new DataColumn("refToItemPackage_Discount", typeof(decimal)));
                table.Columns.Add(new DataColumn("refToItemPackage_Total", typeof(decimal)));

                trueColumns.Add("refToItem_ItemName");
                trueColumns.Add("refToServiceUnit_ServiceUnitName");
                trueColumns.Add("refToItemPackage_Price");
                trueColumns.Add("refToItemPackage_Discount");
                trueColumns.Add("refToItemPackage_Total");

                ViewState["tblItemPackage"] = table;
                ViewState["listTrueColumns"] = trueColumns;
            }
            else
                table.Rows.Clear();

            foreach (var package in packages)
            {
                var row = table.NewRow();

                foreach (var column in trueColumns)
                {
                    row[column] = package.GetColumn(column) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }
            ///

            grdItemPackage.DataSource = table; //Requery
            grdItemPackage.MasterTableView.IsItemInserted = false;
            grdItemPackage.MasterTableView.ClearEditItems();
            grdItemPackage.DataBind();
        }

        protected void grdItemPackage_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var packages = ItemPackages;

            var table = (DataTable)ViewState["tblItemPackage"];
            var trueColumns = ViewState["listTrueColumns"] as List<string> ?? new List<string>();

            if (table == null)
            {
                table = new DataTable();

                foreach (esColumnMetadata column in packages.es.Meta.Columns)
                {
                    table.Columns.Add(new DataColumn(column.Name, column.Type));
                    trueColumns.Add(column.Name);
                }

                table.Columns.Add(new DataColumn("refToItem_ItemName", typeof(string)));
                table.Columns.Add(new DataColumn("refToServiceUnit_ServiceUnitName", typeof(string)));
                table.Columns.Add(new DataColumn("refToItemPackage_Price", typeof(decimal)));
                table.Columns.Add(new DataColumn("refToItemPackage_Discount", typeof(decimal)));
                table.Columns.Add(new DataColumn("refToItemPackage_Total", typeof(decimal)));

                trueColumns.Add("refToItem_ItemName");
                trueColumns.Add("refToServiceUnit_ServiceUnitName");
                trueColumns.Add("refToItemPackage_Price");
                trueColumns.Add("refToItemPackage_Discount");
                trueColumns.Add("refToItemPackage_Total");

                ViewState["tblItemPackage"] = table;
                ViewState["listTrueColumns"] = trueColumns;
            }
            else
                table.Rows.Clear();

            foreach (var package in packages)
            {
                var row = table.NewRow();

                foreach (var column in trueColumns)
                {
                    row[column] = package.GetColumn(column) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }

            grdItemPackage.DataSource = table;
        }

        protected void grdItemPackage_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            var itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemPackageMetadata.ColumnNames.DetailItemID]);
            var entity = FindItemPackage(itemID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItemPackage_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            var itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemPackageMetadata.ColumnNames.DetailItemID]);


            var entity = FindItemPackage(itemID);
            if (entity != null)
            {
                //var ipColl = new ItemPackageCollection();
                //ipColl.Query.Where(ipColl.Query.ServiceUnitID == entity.ServiceUnitID,
                //               ipColl.Query.DetailItemID == itemID,
                //               !ipColl.Query.ItemID.Equal(entity.ItemID));
                //ipColl.LoadAll();

                //var ItemServColl = new ServiceUnitItemServiceCollection();
                //ItemServColl.Query.Where(ItemServColl.Query.ServiceUnitID == entity.ServiceUnitID,
                //                         ItemServColl.Query.ItemID == itemID);
                //ItemServColl.LoadAll();


                //kl item ini tidak dipakai di itempackage lain & di ServiceUnitItemService baru boleh dihapus
                //if (ipColl.Count == 0 && ItemServColl.Count == 0)
                //{

                //ServiceUnitItemServiceCompMappingCollection compColl = ServiceUnitItemServiceCompMappings;
                //foreach (ServiceUnitItemServiceCompMapping comp in compColl.Where(comp => comp.ItemID.Equals(itemID) &&
                //                                                                 comp.ServiceUnitID.Equals(entity.ServiceUnitID)))
                //{
                //    comp.MarkAsDeleted();
                //}

                //ServiceUnitItemServiceClassCollection ClassCompColl = ServiceUnitItemServiceClasses;
                //foreach (BusinessObject.ServiceUnitItemServiceClass ClassComp in ClassCompColl.Where(ClassComp => ClassComp.ItemID.Equals(itemID) &&
                //                                                                    ClassComp.ServiceUnitID.Equals(entity.ServiceUnitID)))
                //{
                //    ClassComp.MarkAsDeleted();
                //}
                //}

                // harus taruh disini karena kl ditaruh diatas kedelete duluan sehingga gak bisa ambil nilai entity.ServiceUnitID
                entity.MarkAsDeleted();

                foreach (var comp in ItemPackageTariffComponents.Where(rec => rec.DetailItemID.Equals(itemID)))
                {
                    comp.MarkAsDeleted();
                }
            }
        }

        protected void grdItemPackage_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ItemPackages.AddNew();
            SetEntityValue(entity, e);

            SetEntityValueForItemComp(e);
            SetEntityValueForClassComp(e);

            e.Canceled = true;
            grdItemPackage.Rebind();
        }

        private ItemPackage FindItemPackage(String itemID)
        {
            var coll = ItemPackages;
            return coll.FirstOrDefault(rec => rec.DetailItemID.Equals(itemID));
        }

        private static void SetEntityValue(ItemPackage entity, GridCommandEventArgs e)
        {
            var userControl = (ItemPackageItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.DetailItemName = userControl.DetailItemName;
                entity.DetailItemID = userControl.DetailItemID;
                entity.ServiceUnitName = userControl.ServiceUnitName;
                entity.ServiceUnitID = userControl.ServiceUnitID;
                entity.Quantity = userControl.Quantity;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.IsStockControl = userControl.IsStockControl;
                entity.IsExtraItem = userControl.IsExtraItem;
                entity.IsActive = userControl.IsActive;
                entity.IsAutoApprove = userControl.IsAutoApprove;
                entity.DiscountValue = userControl.DiscountValue;
                entity.IsDiscountInPercent = userControl.IsDiscountInPercent;

                var comps = Helper.Tariff.GetItemTariffComponentCollection(DateTime.Now.Date,
                                                                          AppSession.Parameter.DefaultTariffType,
                                                                          AppSession.Parameter.DefaultTariffClass,
                                                                          entity.DetailItemID);
                entity.Price = 0;
                if (comps.Count() > 0)
                    entity.Price = comps.Sum(t => t.Price ?? 0);
                entity.Discount = (entity.Price) * (entity.DiscountValue ?? 0) / 100;
                entity.Total = (entity.Quantity ?? 0) * (entity.Price - entity.Discount);
            }
        }

        protected void grdPriceHistory_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdPriceHistory.DataSource = GetItemTariffHistory();
        }

        private DataTable GetItemTariffHistory()
        {
            var itemTariff = new ItemTariff();
            return itemTariff.GetHistory(txtItemID.Text);
        }

        protected void grdPriceHistory_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var dataItem = e.DetailTableView.ParentItem;
            var tariffType = dataItem.GetDataKeyValue("SRTariffType").ToString();
            var itemID = dataItem.GetDataKeyValue("ItemID").ToString();
            var classID = dataItem.GetDataKeyValue("ClassID").ToString();
            var startingDate = Convert.ToDateTime(dataItem.GetDataKeyValue("StartingDate"));

            DataTable dtb = (new ItemTariff()).GetItemTariffComponent(tariffType, itemID, classID, startingDate);
            e.DetailTableView.DataSource = dtb;
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind")
            {
                foreach (var c in ItemPackages)
                {
                    var price = ItemPackageTariffComponents.Where(i => i.DetailItemID == c.DetailItemID)
                                                           .Select(i => i.Price).Sum() ?? 0;
                    var discount = ItemPackageTariffComponents.Where(i => i.DetailItemID == c.DetailItemID)
                                                           .Select(i => i.Discount).Sum() ?? 0;
                    c.Price = price;
                    c.Discount = discount;
                    c.Total = (c.Price - c.Discount) * (c.Quantity ?? 0);
                }

                grdItemPackage.Rebind();
            }
            else if (eventArgument == "clear")
            {
                ItemPackages.MarkAllAsDeleted();
                ItemPackageTariffComponents.MarkAllAsDeleted();

                grdItemPackage.Rebind();
            }
            else if (eventArgument.Split('|')[0] == "copy")
            {
                var pacs = new ItemPackageCollection();
                pacs.Query.Where(pacs.Query.ItemID == eventArgument.Split('|')[1]);
                pacs.LoadAll();

                foreach (var entity in pacs.Select(pac => pacs.DetachEntity(pac)))
                {
                    entity.MarkAllColumnsAsDirty(DataRowState.Added);

                    ItemPackages.AttachEntity(entity);
                }

                foreach (var pac in ItemPackages)
                {
                    var item = new Item();
                    if (item.LoadByPrimaryKey(pac.DetailItemID))
                        pac.DetailItemName = item.ItemName;

                    var unit = new ServiceUnit();
                    if (unit.LoadByPrimaryKey(pac.ServiceUnitID))
                        pac.ServiceUnitName = unit.ServiceUnitName;
                }

                var tariffs = new ItemPackageTariffComponentCollection();
                tariffs.Query.Where(tariffs.Query.ItemID == eventArgument.Split('|')[1]);
                tariffs.LoadAll();

                foreach (var entity in tariffs.Select(tariff => tariffs.DetachEntity(tariff)))
                {
                    entity.MarkAllColumnsAsDirty(DataRowState.Added);

                    ItemPackageTariffComponents.AttachEntity(entity);
                }

                grdItemPackage.Rebind();
            }
        }

        protected void cboTemplate_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboTemplate_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var item = new ItemQuery("a");

            item.es.Top = 20;
            item.Select(
                item.ItemID,
                item.ItemName
                );

            item.Where(
                item.Or
                    (
                        item.ItemName.Like(searchTextContain),
                        item.ItemID.Like(searchTextContain)
                    ),
                item.SRItemType == ItemType.Package,
                item.IsActive == true
                );

            (o as RadComboBox).DataSource = item.LoadDataTable();
            (o as RadComboBox).DataBind();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void cboGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery();
            query.es.Top = 30;
            query.Where
                (
                    query.GuarantorName.Like(searchTextContain),
                    query.IsActive == true
                );
            query.OrderBy(query.GuarantorName.Ascending);

            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        #region ServiceUnitItemServices
        private ServiceUnitItemServiceCollection ServiceUnitItemServices
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collServiceUnitItemService"];
                    if (obj != null)
                    {
                        return ((ServiceUnitItemServiceCollection)(obj));
                    }
                }

                var coll = new ServiceUnitItemServiceCollection();
                var query = new ServiceUnitItemServiceQuery("a");
                var unit = new ServiceUnitQuery("b");

                string itemId = txtItemID.Text;
                query.Where(query.ItemID == itemId);
                query.Select(query.SelectAllExcept(), unit.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"));
                query.LeftJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                coll.Load(query);

                Session["collServiceUnitItemService"] = coll;
                return coll;
            }
            set { Session["collServiceUnitItemService"] = value; }
        }

        private void PopulateServiceUnitItemServiceGrid()
        {
            //Display Data Detail
            ServiceUnitItemServices = null; //Reset Record Detail
            grdServiceUnit.DataSource = ServiceUnitItemServices; //Requery
            grdServiceUnit.MasterTableView.IsItemInserted = false;
            grdServiceUnit.MasterTableView.ClearEditItems();
            grdServiceUnit.DataBind();
        }

        protected void grdServiceUnit_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdServiceUnit.DataSource = ServiceUnitItemServices;
        }

        protected void grdServiceUnit_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String unitID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ServiceUnitItemServiceMetadata.ColumnNames.ServiceUnitID]);
            ServiceUnitItemService entity = FindItemServiceUnit(unitID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdServiceUnit_InsertCommand(object source, GridCommandEventArgs e)
        {
            ServiceUnitItemService entity = ServiceUnitItemServices.AddNew();
            SetEntityValue(entity, e);

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
                        ServiceUnitItemServiceMetadata.ColumnNames.ServiceUnitID]);
            ServiceUnitItemService entity = FindItemServiceUnit(unitID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        private ServiceUnitItemService FindItemServiceUnit(String unitId)
        {
            ServiceUnitItemServiceCollection coll = ServiceUnitItemServices;
            ServiceUnitItemService retEntity = null;
            foreach (ServiceUnitItemService rec in coll)
            {
                if (rec.ServiceUnitID.Equals(unitId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(ServiceUnitItemService entity, GridCommandEventArgs e)
        {
            var userControl = (ItemServiceUnitDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ServiceUnitID = userControl.ServiceUnitID;
                entity.ServiceUnitName = userControl.ServiceUnitName;
                entity.ChartOfAccountId = 0;
                entity.SubledgerId = 0;
                entity.IsAllowEditByUserVerificated = userControl.IsAllowEditByUserVerificated;
                entity.IsVisible = userControl.IsVisible;
            }
        }
        #endregion

        #region Record Detail Method Function ServiceUnitItemServiceCompMapping

        private ServiceUnitItemServiceCompMappingCollection ServiceUnitItemServiceCompMappings
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["ServiceUnitItemServiceCompMappingCollection"];
                    if (obj != null)
                        return ((ServiceUnitItemServiceCompMappingCollection)(obj));
                }
                var coll = new ServiceUnitItemServiceCompMappingCollection();

                var query = new ServiceUnitItemServiceCompMappingQuery("a");
                var c = new TariffComponentQuery("b");

                var rev = new ChartOfAccountsQuery("c");
                var srev = new SubLedgersQuery("d");

                var disc = new ChartOfAccountsQuery("e");
                var sdisc = new SubLedgersQuery("f");

                var cost = new ChartOfAccountsQuery("g");
                var scost = new SubLedgersQuery("h");

                query.Select
                    (
                        query,

                        c.TariffComponentName.As("refToTariffComponent_TariffComponentName"),

                        rev.ChartOfAccountName.As("refToChartOfAccounts_COARevenueName"),
                        srev.SubLedgerName.As("refToSubledgers_SubledgerRevenueName"),

                        disc.ChartOfAccountName.As("refToChartOfAccounts_COADiscountName"),
                        sdisc.SubLedgerName.As("refToSubledgers_SubledgerDiscountName"),

                        cost.ChartOfAccountName.As("refToChartOfAccounts_COACostName"),
                        scost.SubLedgerName.As("refToSubledgers_SubledgerCostName")
                    );
                query.InnerJoin(c).On(query.TariffComponentID == c.TariffComponentID);

                query.LeftJoin(rev).On(query.ChartOfAccountIdIncome == rev.ChartOfAccountId);
                query.LeftJoin(srev).On(query.SubledgerIdIncome == srev.SubLedgerId);

                query.LeftJoin(disc).On(query.ChartOfAccountIdDiscount == disc.ChartOfAccountId);
                query.LeftJoin(sdisc).On(query.SubledgerIdDiscount == sdisc.SubLedgerId);

                query.LeftJoin(cost).On(query.ChartOfAccountIdCost == cost.ChartOfAccountId);
                query.LeftJoin(scost).On(query.SubledgerIdCost == scost.SubLedgerId);

                coll.Load(query);

                Session["ServiceUnitItemServiceCompMappingCollection"] = coll;
                return coll;
            }
            set
            {
                Session["ServiceUnitItemServiceCompMappingCollection"] = value;
            }
        }

        #endregion

        #region Record Detail Method Function ServiceUnitItemServiceClass

        private ServiceUnitItemServiceClassCollection ServiceUnitItemServiceClasses
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["ServiceUnitItemServiceClassCollection"];
                    if (obj != null)
                        return ((ServiceUnitItemServiceClassCollection)(obj));
                }
                var coll = new ServiceUnitItemServiceClassCollection();

                var query = new ServiceUnitItemServiceClassQuery("a");
                var c = new TariffComponentQuery("b");

                var rev = new ChartOfAccountsQuery("c");
                var srev = new SubLedgersQuery("d");

                var disc = new ChartOfAccountsQuery("e");
                var sdisc = new SubLedgersQuery("f");

                var cost = new ChartOfAccountsQuery("g");
                var scost = new SubLedgersQuery("h");

                var cls = new ClassQuery("i");

                query.Select
                    (
                        query,

                        c.TariffComponentName.As("refToTariffComponent_TariffComponentName"),

                        rev.ChartOfAccountName.As("refToChartOfAccounts_COARevenueName"),
                        srev.SubLedgerName.As("refToSubledgers_SubledgerRevenueName"),

                        disc.ChartOfAccountName.As("refToChartOfAccounts_COADiscountName"),
                        sdisc.SubLedgerName.As("refToSubledgers_SubledgerDiscountName"),

                        cost.ChartOfAccountName.As("refToChartOfAccounts_COACostName"),
                        scost.SubLedgerName.As("refToSubledgers_SubledgerCostName"),

                        cls.ClassName.As("refToClass_ClassName")
                    );
                query.InnerJoin(c).On(query.TariffComponentID == c.TariffComponentID);

                query.LeftJoin(rev).On(query.ChartOfAccountIdIncome == rev.ChartOfAccountId);
                query.LeftJoin(srev).On(query.SubledgerIdIncome == srev.SubLedgerId);

                query.LeftJoin(disc).On(query.ChartOfAccountIdDiscount == disc.ChartOfAccountId);
                query.LeftJoin(sdisc).On(query.SubledgerIdDiscount == sdisc.SubLedgerId);

                query.LeftJoin(cost).On(query.ChartOfAccountIdCost == cost.ChartOfAccountId);
                query.LeftJoin(scost).On(query.SubledgerIdCost == scost.SubLedgerId);

                query.InnerJoin(cls).On(query.ClassID == cls.ClassID);


                coll.Load(query);

                Session["ServiceUnitItemServiceClassCollection"] = coll;
                return coll;
            }
            set
            {
                ViewState["ServiceUnitItemServiceClassCollection"] = value;
            }
        }

        #endregion

        private void SetEntityValueForItemComp(GridCommandEventArgs e)
        {
            var userControl = (ItemPackageItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                var suiscm = new ServiceUnitItemServiceCompMappingCollection();
                suiscm.Query.Where(suiscm.Query.ServiceUnitID == userControl.ServiceUnitID,
                    suiscm.Query.ItemID == userControl.DetailItemID);
                suiscm.LoadAll();
                if (suiscm.Count > 0)
                    return;

                TariffComponentCollection coll = new TariffComponentCollection();
                coll.LoadAll();

                foreach (TariffComponent rec in coll)
                {
                    var entity = ServiceUnitItemServiceCompMappings.AddNew();
                    entity.ServiceUnitID = userControl.ServiceUnitID;
                    entity.ItemID = userControl.DetailItemID;
                    entity.TariffComponentID = rec.TariffComponentID;
                    entity.TariffComponentName = rec.TariffComponentName;

                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(userControl.ServiceUnitID);
                    entity.SRRegistrationType = unit.SRRegistrationType;
                }
            }
        }

        private void SetEntityValueForClassComp(GridCommandEventArgs e)
        {
            var userControl = (ItemPackageItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                var suisc = new ServiceUnitItemServiceClassCollection();
                suisc.Query.Where(suisc.Query.ServiceUnitID == userControl.ServiceUnitID,
                    suisc.Query.ItemID == userControl.DetailItemID);
                suisc.LoadAll();
                if (suisc.Count > 0)
                    return;

                TariffComponentCollection coll = new TariffComponentCollection();
                coll.LoadAll();

                ClassCollection coll2 = new ClassCollection();
                coll2.LoadAll();

                foreach (TariffComponent rec in coll)
                {
                    foreach (Class rec2 in coll2)
                    {
                        var entity = ServiceUnitItemServiceClasses.AddNew();
                        entity.ServiceUnitID = userControl.ServiceUnitID;
                        entity.ItemID = userControl.DetailItemID;
                        entity.ClassID = rec2.ClassID;
                        entity.ClassName = rec2.ClassName;
                        entity.TariffComponentID = rec.TariffComponentID;
                        entity.TariffComponentName = rec.TariffComponentName;
                    }
                }
            }
        }
    }
}
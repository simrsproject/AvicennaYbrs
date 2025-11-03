using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ItemProductNonMedicalDetail : BasePageDetail
    {
        private void SetEntityValue(Item entity, ItemProductNonMedic detail)
        {
            entity.ItemID = txtItemID.Text;
            entity.ItemGroupID = cboItemGroupID.SelectedValue;
            entity.SRItemType = BusinessObject.Reference.ItemType.NonMedical;
            entity.SRBillingGroup = cboBillingGroup.SelectedValue;
            entity.SRItemCategory = cboSRItemCategory.SelectedValue;
            entity.SRBpjsItemGroup = cboSRBpjsItemGroup.SelectedValue;
            entity.ProductAccountID = cboProductAccount.SelectedValue;
            entity.ItemName = AppSession.Parameter.IsItemInventoryNameUsingUpperCase ? txtItemName.Text.ToUpper() : txtItemName.Text;
            entity.IsActive = chkIsActive.Checked;
            entity.Notes = txtNotes.Text;
            entity.ItemIDExternal = txtItemIDExternal.Text;
            entity.IsItemProduction = chkIsItemProduction.Checked;
            entity.IsNeedToBeSterilized = chkIsNeedToBeSterilized.Checked;
            entity.Barcode = txtBarcode.Text;
            entity.SREklaimTariffGroup = cboSREklaimGroup.SelectedValue;
            entity.Photo = ItemImageInBytes();
            entity.IsAsset = chkIsAsset.Checked;
            entity.AssetGroupID = cboAssetGroupID.SelectedValue;
            entity.IsNewUpload = false;
            entity.EconomicLifeInYear = Convert.ToInt32(txtEconomicLifeInYear.Value);

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

            detail.ItemID = entity.ItemID;
            detail.MarginID = cboMarginID.SelectedValue;
            detail.SRProductType = cboSRProductType.SelectedValue;
            detail.ABCClass = txtABCClass.Text;
            detail.BrandName = txtBrandName.Text;
            detail.SRItemUnit = cboSRItemUnit.SelectedValue;
            detail.SRPurchaseUnit = cboSRPurchaseUnit.SelectedValue;
            detail.ConversionFactor = Convert.ToDecimal(txtConversionFactor.Value);
            detail.Dosage = Convert.ToDecimal(0);
            detail.SRDosageUnit = string.Empty;
            detail.IsFormularium = chkIsFormularium.Checked;
            detail.IsInventoryItem = chkIsInventoryItem.Checked;
            detail.IsControlExpired = chkIsControlExpired.Checked;
            detail.str.FabricID = cboFabricID.SelectedValue;
            detail.SalesFixedPrice = Convert.ToDecimal(txtSalesFixedPrice.Value);
            detail.MarginPercentage = Convert.ToDecimal(txtMarginPercentage.Value);
            detail.SalesDiscount = Convert.ToDecimal(txtSalesDiscount.Value);
            detail.PurchaseDiscount1 = Convert.ToDecimal(txtPurchaseDiscount1.Value);
            detail.PurchaseDiscount2 = Convert.ToDecimal(txtPurchaseDiscount2.Value);
            detail.SafetyStock = Convert.ToDecimal(txtSafetyStock.Value);
            detail.SafetyTime = Convert.ToByte(txtSafetyTime.Value);
            detail.LeadTime = Convert.ToByte(txtLeadTime.Value);
            detail.TolerancePercentage = Convert.ToDecimal(txtTolerancePercentage.Value);
            detail.Barcode = txtBarcode.Text;
            detail.SRItemBin = cboSRItemBin.SelectedValue;
            detail.IsConsignment = chkIsConsignment.Checked;
            detail.IsSalesAvailable = chkIsSalesAvailable.Checked;
            detail.IsSharePurchaseDiscToPatient = chkIsSharePurchaseDiscToPatient.Checked;
            detail.IsNeedToBeLaundered = chkIsNeedToBeLaundered.Checked;
            detail.SRPurchaseCategorization = cboSRPurchaseCategorization.SelectedValue;

            if (detail.es.IsAdded)
            {
                detail.PriceInPurchaseUnit = 0;
                detail.PriceInBaseUnit = 0;
                detail.PriceInBasedUnitWVat = 0;
                detail.HighestPriceInBasedUnit = 0;
                detail.CostPrice = 0;
                detail.LastPriceInBaseUnit = 0;
            }

            //Last Update Status
            if (detail.es.IsAdded || detail.es.IsModified)
            {
                detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                detail.LastUpdateDateTime = DateTime.Now;
            }

            //matrix item supplier
            foreach (SupplierItem l in SupplierItems)
            {
                l.ItemID = txtItemID.Text;
                if (l.es.IsAdded)
                {
                    l.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    l.LastUpdateDateTime = DateTime.Now;
                }
                else if (l.es.IsModified)
                {
                    l.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    l.LastUpdateDateTime = DateTime.Now;
                }
            }

            // item balance
            foreach (ItemBalance item in ItemBalances)
            {
                if (item.es.IsAdded)
                {
                    item.ItemID = txtItemID.Text;
                }
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }

            foreach (ItemBalanceDetail item in ItemBalanceDetails)
            {
                if (item.es.IsAdded)
                {
                    item.ItemID = txtItemID.Text;
                }
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }

            foreach (ItemProductFabric item in ItemProductFabrics)
            {
                if (item.es.IsAdded)
                {
                    item.ItemID = txtItemID.Text;
                }
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
            ItemQuery que = new ItemQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ItemID > txtItemID.Text, que.SRItemType == BusinessObject.Reference.ItemType.NonMedical);
                que.OrderBy(que.ItemID.Ascending);
            }
            else
            {
                que.Where(que.ItemID < txtItemID.Text, que.SRItemType == BusinessObject.Reference.ItemType.NonMedical);
                que.OrderBy(que.ItemID.Descending);
            }
            Item entity = new Item();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Item entity = new Item();
            if (parameters.Length > 0)
            {
                String itemID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(itemID);
            }
            else
                entity.LoadByPrimaryKey(txtItemID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var item = (Item)entity;
            txtItemID.Text = item.ItemID;

            if (item != null && item.Photo != null) PopulateItemImage(item.Photo);

            cboBillingGroup.SelectedValue = item.SRBillingGroup;
            cboSRItemCategory.SelectedValue = item.SRItemCategory;
            cboSRBpjsItemGroup.SelectedValue = item.SRBpjsItemGroup;

            if (!string.IsNullOrEmpty(item.ProductAccountID))
            {
                var pQuery = new ProductAccountQuery();
                pQuery.Where(pQuery.ProductAccountID == item.str.ProductAccountID);
                cboProductAccount.DataSource = pQuery.LoadDataTable();
                cboProductAccount.DataBind();
                cboProductAccount.SelectedValue = item.ProductAccountID;
            }
            else
            {
                cboProductAccount.Items.Clear();
                cboProductAccount.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(item.ItemGroupID))
            {
                var gQuery = new ItemGroupQuery();
                gQuery.Where(gQuery.ItemGroupID == item.str.ItemGroupID);
                cboItemGroupID.DataSource = gQuery.LoadDataTable();
                cboItemGroupID.DataBind();
                cboItemGroupID.SelectedValue = item.ItemGroupID;
            }
            else
            {
                cboItemGroupID.Items.Clear();
                cboItemGroupID.Text = string.Empty;
            }

            txtItemName.Text = item.ItemName;
            chkIsActive.Checked = item.IsActive ?? false;
            txtNotes.Text = item.Notes;
            chkIsItemProduction.Checked = item.IsItemProduction ?? false;

            var itemProductNonMedic = new ItemProductNonMedic();
            if (item.ItemID != null)
                itemProductNonMedic.LoadByPrimaryKey(item.ItemID);

            if (!string.IsNullOrEmpty(itemProductNonMedic.MarginID))
            {
                var mQuery = new ItemProductMarginQuery();
                mQuery.Where(mQuery.MarginID == itemProductNonMedic.str.MarginID);
                cboMarginID.DataSource = mQuery.LoadDataTable();
                cboMarginID.DataBind();
                cboMarginID.SelectedValue = itemProductNonMedic.MarginID;
            }
            else
            {
                cboMarginID.Items.Clear();
                cboMarginID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(itemProductNonMedic.SRItemUnit))
            {
                var itemUnitQ = new AppStandardReferenceItemQuery();
                itemUnitQ.Where(itemUnitQ.StandardReferenceID == AppEnum.StandardReference.ItemUnit, itemUnitQ.ItemID == itemProductNonMedic.str.SRItemUnit);
                cboSRItemUnit.DataSource = itemUnitQ.LoadDataTable();
                cboSRItemUnit.DataBind();
                cboSRItemUnit.SelectedValue = itemProductNonMedic.SRItemUnit;
            }
            else
            {
                cboSRItemUnit.Items.Clear();
                cboSRItemUnit.Text = string.Empty;
            }

            cboSRProductType.SelectedValue = itemProductNonMedic.SRProductType;
            txtABCClass.Text = itemProductNonMedic.ABCClass;
            txtBrandName.Text = itemProductNonMedic.BrandName;

            if (!string.IsNullOrEmpty(itemProductNonMedic.SRPurchaseUnit))
            {
                var purchaseUnitQ = new AppStandardReferenceItemQuery();
                purchaseUnitQ.Where(purchaseUnitQ.StandardReferenceID == AppEnum.StandardReference.ItemUnit, purchaseUnitQ.ItemID == itemProductNonMedic.str.SRPurchaseUnit);
                cboSRPurchaseUnit.DataSource = purchaseUnitQ.LoadDataTable();
                cboSRPurchaseUnit.DataBind();
                cboSRPurchaseUnit.SelectedValue = itemProductNonMedic.SRPurchaseUnit;
            }
            else
            {
                cboSRPurchaseUnit.Items.Clear();
                cboSRPurchaseUnit.Text = string.Empty;
            }

            txtConversionFactor.Value = Convert.ToDouble(itemProductNonMedic.ConversionFactor);
            chkIsFormularium.Checked = itemProductNonMedic.IsFormularium ?? false;
            chkIsInventoryItem.Checked = itemProductNonMedic.IsInventoryItem ?? false;
            chkIsControlExpired.Checked = itemProductNonMedic.IsControlExpired ?? false;

            if (!string.IsNullOrEmpty(itemProductNonMedic.FabricID))
            {
                var fabricQ = new FabricQuery();
                fabricQ.Where(fabricQ.FabricID == itemProductNonMedic.str.FabricID);
                cboFabricID.DataSource = fabricQ.LoadDataTable();
                cboFabricID.DataBind();
                cboFabricID.SelectedValue = itemProductNonMedic.FabricID;
            }
            else
            {
                cboFabricID.Items.Clear();
                cboFabricID.Text = string.Empty;
            }

            txtSalesFixedPrice.Value = Convert.ToDouble(itemProductNonMedic.SalesFixedPrice);
            txtMarginPercentage.Value = Convert.ToDouble(itemProductNonMedic.MarginPercentage);
            txtSalesDiscount.Value = Convert.ToDouble(itemProductNonMedic.SalesDiscount);
            txtPriceInPurchaseUnit.Value = Convert.ToDouble(itemProductNonMedic.PriceInPurchaseUnit);
            txtPriceInBaseUnit.Value = Convert.ToDouble(itemProductNonMedic.PriceInBaseUnit);
            txtPriceInBasedUnitWVat.Value = Convert.ToDouble(itemProductNonMedic.PriceInBasedUnitWVat);
            txtHighestPriceInBasedUnit.Value = Convert.ToDouble(itemProductNonMedic.HighestPriceInBasedUnit);
            txtCostPrice.Value = Convert.ToDouble(itemProductNonMedic.CostPrice);
            txtPurchaseDiscount1.Value = Convert.ToDouble(itemProductNonMedic.PurchaseDiscount1);
            txtPurchaseDiscount2.Value = Convert.ToDouble(itemProductNonMedic.PurchaseDiscount2);
            txtSafetyStock.Value = Convert.ToDouble(itemProductNonMedic.SafetyStock);
            txtSafetyTime.Value = Convert.ToDouble(itemProductNonMedic.SafetyTime);
            txtLeadTime.Value = Convert.ToDouble(itemProductNonMedic.LeadTime);
            txtTolerancePercentage.Value = Convert.ToDouble(itemProductNonMedic.TolerancePercentage);
            txtBarcode.Text = itemProductNonMedic.Barcode;
            cboSRItemBin.SelectedValue = itemProductNonMedic.SRItemBin;
            chkIsConsignment.Checked = itemProductNonMedic.IsConsignment ?? false;
            txtItemIDExternal.Text = item.ItemIDExternal;
            chkIsSalesAvailable.Checked = itemProductNonMedic.IsSalesAvailable ?? false;
            chkIsNeedToBeSterilized.Checked = item.IsNeedToBeSterilized ?? false;
            chkIsSharePurchaseDiscToPatient.Checked = itemProductNonMedic.IsSharePurchaseDiscToPatient ?? false;
            chkIsNeedToBeLaundered.Checked = itemProductNonMedic.IsNeedToBeLaundered ?? false;
            cboSRPurchaseCategorization.SelectedValue = itemProductNonMedic.SRPurchaseCategorization;

            cboSREklaimGroup.SelectedValue = item.SREklaimTariffGroup;
            chkIsAsset.Checked = item.IsAsset ?? false;
            txtEconomicLifeInYear.Value = Convert.ToDouble(txtEconomicLifeInYear.Value);
            if (!string.IsNullOrEmpty(item.AssetGroupID))
            {
                var ag = new AssetGroupQuery();
                ag.Where(ag.AssetGroupId == item.AssetGroupID);
                cboAssetGroupID.DataSource = ag.LoadDataTable();
                cboAssetGroupID.DataBind();
                cboAssetGroupID.SelectedValue = item.AssetGroupID;
            }
            else
            {
                cboAssetGroupID.Items.Clear();
                cboAssetGroupID.SelectedValue = string.Empty;
                cboAssetGroupID.Text = string.Empty;
            }

            grdPriceHistory.Rebind();
            PopulateSupplierItemGrid();
            PopulateItemProductFabricGrid();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {

        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Item());

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateItemIdProductAutomatic) == "Yes")
                txtItemID.ReadOnly = true;

            chkIsActive.Checked = true;
            chkIsInventoryItem.Checked = true;
            cboItemGroupID.Text = string.Empty;
            cboSRProductType.Text = string.Empty;
            cboSRItemBin.Text = string.Empty;
            cboSRItemUnit.Text = string.Empty;
            cboSRPurchaseUnit.Text = string.Empty;
            cboFabricID.Text = string.Empty;
            cboMarginID.Text = string.Empty;
            cboBillingGroup.Text = string.Empty;
            cboSRItemCategory.Text = string.Empty;
            cboSRBpjsItemGroup.Text = string.Empty;
            cboProductAccount.Text = string.Empty;
            chkIsSalesAvailable.Checked = true;

            if (AppSession.Parameter.IsEklaimGroupUsingDefaultValue)
                cboSREklaimGroup.SelectedValue = "14";
            else
            {
                cboSREklaimGroup.SelectedValue = string.Empty;
                cboSREklaimGroup.Text = string.Empty;
            }
        }
        
        protected override void OnMenuEditClick()
        {
            //-db (6/6/2023): dipindah ke function CboSrItemDisable()
            //chkIsInventoryItem.Enabled = !AppSession.Parameter.IsDisableInventoryStatusOnEditItemProduct;
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

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_ItemID", txtItemID.Text);
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtItemID.ReadOnly = (newVal != AppEnum.DataMode.New);
            //if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdProductAutomatic) && AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdProductAutomaticUseGroupInitial))
            //{
            //    cboItemGroupID.Enabled = (newVal == AppEnum.DataMode.New);
            //}
            CboSrItemDisable();

            RefreshCommandItemGridSupplierItem(oldVal, newVal);
            RefreshCommandItemGridLocation(oldVal, newVal);
            RefreshCommandItemProductFabric(newVal);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "ItemProductNonMedicalSearch.aspx";
            UrlPageList = "ItemProductNonMedicalList.aspx";

            ProgramID = AppConstant.Program.ItemProductNonMedical;

            WindowSearch.Height = 400;

            //StandardReference Initialize
            if (!IsCallback)
            {

            }
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRProductType, AppEnum.StandardReference.ProductType);
                StandardReference.InitializeIncludeSpace(cboSRItemBin, AppEnum.StandardReference.ItemBin);
                StandardReference.InitializeIncludeSpace(cboBillingGroup, AppEnum.StandardReference.BillingGroup);
                StandardReference.InitializeIncludeSpace(cboSRItemCategory, AppEnum.StandardReference.ItemCategory);
                StandardReference.InitializeIncludeSpace(cboSRBpjsItemGroup, AppEnum.StandardReference.BpjsItemGroup);
                StandardReference.InitializeIncludeSpace(cboSREklaimGroup, AppEnum.StandardReference.EklaimTariffGroup);
                StandardReference.InitializeIncludeSpace(cboSRPurchaseCategorization, AppEnum.StandardReference.PurchaseCategorization);

                trBpjsItemGroup.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB";
                rfvSRPurchaseCategorization.Visible = AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsSeparationOfItemPurchaseCategorization);
                
                grdLocation.Columns.FindByUniqueName("ItemSubBin").Visible = AppSession.Parameter.IsUsingItemSubBin;

                var mdl = new AppProgram();
                bool isCssd = (mdl.LoadByPrimaryKey("14") && mdl.IsVisible == true);

                chkIsNeedToBeSterilized.Visible = isCssd;
                chkIsSharePurchaseDiscToPatient.Visible = !AppSession.Parameter.IsSharePurchaseDiscToPatient;

                chkIsNeedToBeLaundered.Visible = AppSession.Application.IsModuleLaundryActive;
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Item entity = new Item();
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

                ItemProductNonMedic detail = new ItemProductNonMedic();
                if (detail.LoadByPrimaryKey(txtItemID.Text))
                    detail.MarkAsDeleted();
                else
                    detail = null;

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (detail != null)
                        detail.Save();

                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboSRItemUnit.SelectedValue))
            {
                args.MessageText = "Item Unit required";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboSRPurchaseUnit.SelectedValue))
            {
                args.MessageText = "Purchase Unit required";
                args.IsCancel = true;
                return;
            }

            if ((cboSRItemUnit.SelectedValue == cboSRPurchaseUnit.SelectedValue) && (txtConversionFactor.Value != 1))
            {
                args.MessageText = "Item Unit and Purchase Unit has same value, Conversion Factor value must be 1";
                args.IsCancel = true;
                return;
            }

            if ((cboSRItemUnit.SelectedValue != cboSRPurchaseUnit.SelectedValue) && (txtConversionFactor.Value <= 1))
            {
                args.MessageText = "Item Unit and Purchase Unit has different value, Conversion Factor value must be greater than 1";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
            {
                args.MessageText = "Item Group required.";
                args.IsCancel = true;
                return;
            }

            if (Helper.IsInacbgIntegration)
            {
                if (string.IsNullOrEmpty(cboSREklaimGroup.SelectedValue) && chkIsSalesAvailable.Checked)
                {
                    args.MessageText = "Eklaim group required.";
                    args.IsCancel = true;
                    return;
                }
            }

            if (string.IsNullOrEmpty(cboProductAccount.SelectedValue) && AppSession.Parameter.IsValidateProductAccountOnItem)
            {
                args.MessageText = "Product Account required.";
                args.IsCancel = true;
                return;
            }

            if (chkIsAsset.Checked)
            {
                //if (string.IsNullOrEmpty(cboAssetGroupID.SelectedValue))
                //{
                //    args.MessageText = "Asset Group required.";
                //    args.IsCancel = true;
                //    return;
                //}
                if (chkIsInventoryItem.Checked)
                {
                    args.MessageText = "Asset status only for Item Non Stock (Inventory Item can't be checked).";
                    args.IsCancel = true;
                    return;
                }
            }

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateItemIdProductAutomatic) == "Yes")
            {
                if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdProductAutomaticUseGroupInitial))
                    txtItemID.Text = Helper.GetItemProductIDUseGroupInitial(cboItemGroupID.SelectedValue);
                else
                    txtItemID.Text = Helper.GetItemProductID(txtItemName.Text.ToUpper(), ItemType.NonMedical);
            }
            else if (string.IsNullOrEmpty(txtItemID.Text))
            {
                args.MessageText = "Item ID required.";
                args.IsCancel = true;
                return;
            }
            if (IsBarcodeUsedByOtherItem(args, txtItemID.Text, txtBarcode.Text))
            {
                return;
            }
            var itemProd = new ItemProductNonMedic();
            if (itemProd.LoadByPrimaryKey(txtItemID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            var entity = new Item();
            entity.AddNew();

            itemProd = new ItemProductNonMedic();
            itemProd.AddNew();

            SetEntityValue(entity, itemProd);
            SaveEntity(entity, itemProd);

        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboSRItemUnit.SelectedValue))
            {
                args.MessageText = "Item Unit required";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboSRPurchaseUnit.SelectedValue))
            {
                args.MessageText = "Purchase Unit required";
                args.IsCancel = true;
                return;
            }

            if ((cboSRItemUnit.SelectedValue == cboSRPurchaseUnit.SelectedValue) && (txtConversionFactor.Value != 1))
            {
                args.MessageText = "Item Unit and Purchase Unit has same value, Conversion Factor value must be 1";
                args.IsCancel = true;
                return;
            }

            if ((cboSRItemUnit.SelectedValue != cboSRPurchaseUnit.SelectedValue) && (txtConversionFactor.Value <= 1))
            {
                args.MessageText = "Item Unit and Purchase Unit has different value, Conversion Factor value must be greater than 1";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
            {
                args.MessageText = "Item Group required.";
                args.IsCancel = true;
                return;
            }

            if (Helper.IsInacbgIntegration)
            {
                if (string.IsNullOrEmpty(cboSREklaimGroup.SelectedValue) && chkIsSalesAvailable.Checked)
                {
                    args.MessageText = "Eklaim group required.";
                    args.IsCancel = true;
                    return;
                }
            }

            if (string.IsNullOrEmpty(cboProductAccount.SelectedValue) && AppSession.Parameter.IsValidateProductAccountOnItem)
            {
                args.MessageText = "Product Account required.";
                args.IsCancel = true;
                return;
            }

            if (chkIsAsset.Checked)
            {
                //if (string.IsNullOrEmpty(cboAssetGroupID.SelectedValue))
                //{
                //    args.MessageText = "Asset Group required.";
                //    args.IsCancel = true;
                //    return;
                //}
                if (chkIsInventoryItem.Checked)
                {
                    args.MessageText = "Asset status only for Item Non Stock (Inventory Item can't be checked).";
                    args.IsCancel = true;
                    return;
                }
            }

            if (IsBarcodeUsedByOtherItem(args, txtItemID.Text, txtBarcode.Text))
            {
                return;
            }
            Item entity = new Item();
            ItemProductNonMedic detail = new ItemProductNonMedic();

            if (entity.LoadByPrimaryKey(txtItemID.Text))
            {
                if (!detail.LoadByPrimaryKey(txtItemID.Text))
                {
                    detail = new ItemProductNonMedic();
                    detail.AddNew();
                }
                SetEntityValue(entity, detail);
                SaveEntity(entity, detail);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
        }

        private void SaveEntity(Item entity, ItemProductNonMedic detail)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                detail.Save();
                SupplierItems.Save();
                ItemProductFabrics.Save();
                if (entity.IsActive == false && AppSession.Parameter.IsAutoDeleteBalanceOnInActiveItem)
                {
                    ItemBalances.MarkAllAsDeleted();
                    ItemBalanceDetails.MarkAllAsDeleted();
                }
                ItemBalances.Save();
                ItemBalanceDetails.Save();

                if (entity.IsNeedToBeSterilized == true)
                {
                    if (entity.IsItemProduction == false)
                    {
                        var iCssdColl = new CssdItemDetailCollection();
                        iCssdColl.Query.Where(iCssdColl.Query.ItemID == entity.ItemID, iCssdColl.Query.ItemDetailID == entity.ItemID);
                        iCssdColl.LoadAll();
                        iCssdColl.MarkAllAsDeleted();
                        iCssdColl.Save();

                        var iCssd = new CssdItemDetail();
                        if (!iCssd.LoadByPrimaryKey(entity.ItemID, entity.ItemID))
                        {
                            iCssd.AddNew();
                            iCssd.ItemID = entity.ItemID;
                            iCssd.ItemDetailID = entity.ItemID;
                            iCssd.Qty = 1;
                            iCssd.LastUpdateDateTime = DateTime.Now;
                            iCssd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            iCssd.Save();
                        }
                    }
                }
                else
                {
                    var iCssdColl = new CssdItemDetailCollection();
                    iCssdColl.Query.Where(iCssdColl.Query.ItemID == entity.ItemID);
                    iCssdColl.LoadAll();
                    iCssdColl.MarkAllAsDeleted();
                    iCssdColl.Save();
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void DeleteEntity(Item entity, ItemProductNonMedic detail)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                detail.Save();
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }
        private bool IsBarcodeUsedByOtherItem(ValidateArgs args, string itemID, string bc)
        {
            args.IsCancel = false;
            var barcodeUsedBy = BarcodeUsed(itemID, bc);
            if (barcodeUsedBy != null)
            {
                args.MessageText = string.Format("Barcode has used by item: {0} {1}", barcodeUsedBy.ItemID, barcodeUsedBy.ItemName);
                args.IsCancel = true;
            }
            return args.IsCancel;
        }
        private Item BarcodeUsed(string itemID, string bc)
        {
            if (string.IsNullOrEmpty(bc))
                return null;

            var item = new Item();
            if (item.LoadByBarcode(bc) && item.ItemID != itemID)
            {
                return item;
            }
            return null;
        }
        
        #endregion

        private void ValidateUnit(object sender)
        {

            AppStandardReferenceItem unit = new AppStandardReferenceItem();
            if (unit.LoadByPrimaryKey(AppEnum.StandardReference.ItemUnit.ToString(), ((RadTextBox)sender).Text))
            {
                ((RadTextBox)sender).Text = unit.ItemID;
            }
            else
            {
                ((RadTextBox)sender).Text = string.Empty;
            }
        }

        protected void CboSrItemDisable()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]) && DataModeCurrent == AppEnum.DataMode.Edit)
            {
                var bal = new ItemBalanceCollection();
                bal.Query.Where(bal.Query.ItemID == Request.QueryString["id"] && bal.Query.Balance > 0);
                bal.LoadAll();

                if (bal.Count() > 0)
                {
                    cboSRItemUnit.Enabled = false;
                    cboSRPurchaseUnit.Enabled = false;
                    txtConversionFactor.Enabled = false;
                }
                else
                {
                    cboSRItemUnit.Enabled = true;
                    cboSRPurchaseUnit.Enabled = true;
                    txtConversionFactor.Enabled = true;
                }

                var i = new Item();
                if (i.LoadByPrimaryKey(Request.QueryString["id"]))
                {
                    if (i.IsActive == true && bal.Count() > 0)
                        chkIsActive.Enabled = false;
                    else
                    {
                        if (AppSession.Application.IsMenuMasterItemProductExportAble(ProgramID))
                            chkIsActive.Enabled = this.IsPowerUser;
                        else
                            chkIsActive.Enabled = true;
                    }
                }

                var ip = new ItemProductNonMedic();
                if (ip.LoadByPrimaryKey(Request.QueryString["id"]))
                {
                    if (ip.IsConsignment == true)
                    {
                        var balconsq = new ItemBalanceQuery("a");
                        var locq = new LocationQuery("b");
                        balconsq.InnerJoin(locq).On(locq.LocationID == balconsq.LocationID && locq.IsActive == true && locq.IsConsignment == true);
                        balconsq.Where(balconsq.ItemID == Request.QueryString["id"] && balconsq.Balance > 0);
                        var balcons = new ItemBalanceCollection();
                        balcons.Load(balconsq);
                        chkIsConsignment.Enabled = balcons.Count() == 0;
                    }
                    if (ip.IsInventoryItem == true)
                        chkIsInventoryItem.Enabled = (bal.Count() == 0 && !AppSession.Parameter.IsDisableInventoryStatusOnEditItemProduct);
                    else
                        chkIsInventoryItem.Enabled = !AppSession.Parameter.IsDisableInventoryStatusOnEditItemProduct;
                }
            }

            //if (DataModeCurrent == AppEnum.DataMode.Edit)
            //{
            //    chkIsSharePurchaseDiscToPatient.Enabled = !AppSession.Parameter.IsSharePurchaseDiscToPatient;
            //}

            // kalau ada po / pr outstanding tidak boleh ubah satuan
            if (!string.IsNullOrEmpty(txtItemID.Text))
            {
                var itiq = new ItemTransactionItemQuery("itiq");
                var itq = new ItemTransactionQuery("itq");

                itiq.InnerJoin(itq).On(itiq.TransactionNo == itq.TransactionNo)
                    .Where(itq.IsApproved == true, itq.IsVoid == false,
                        itq.IsClosed == false, itiq.ItemID == txtItemID.Text,
                        itq.TransactionCode.In("034", "037"))
                    .Select(itq.TransactionNo);
                itiq.es.Distinct = true;

                var tbl = itiq.LoadDataTable();
                if (tbl.Rows.Count > 0)
                {
                    cboSRItemUnit.Enabled = false;
                    cboSRPurchaseUnit.Enabled = false;
                }
            }
        }

        #region Item Tariff
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPriceHistory.DataSource = GetItemTariffHistory();
        }

        private DataTable GetItemTariffHistory()
        {
            ItemTariff itemTariff = new ItemTariff();
            return itemTariff.GetHistory(txtItemID.Text);
        }
        #endregion

        #region Item Supplier
        private void RefreshCommandItemGridSupplierItem(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdSupplierItem.Columns[0].Visible = isVisible;
            grdSupplierItem.Columns[grdSupplierItem.Columns.Count - 1].Visible = isVisible;

            grdSupplierItem.MasterTableView.CommandItemDisplay = isVisible
                                                                       ? GridCommandItemDisplay.Top
                                                                       : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                SupplierItems = null;

            //Perbaharui tampilan dan data
            grdSupplierItem.Rebind();
        }

        protected void grdSupplierItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSupplierItem.DataSource = SupplierItems;
        }

        private void PopulateSupplierItemGrid()
        {
            //Display Data Detail
            SupplierItems = null; //Reset Record Detail
            grdSupplierItem.DataSource = SupplierItems; //Requery
            grdSupplierItem.MasterTableView.IsItemInserted = false;
            grdSupplierItem.MasterTableView.ClearEditItems();
            grdSupplierItem.DataBind();
        }

        protected void grdSupplierItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            String supplierID =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][SupplierItemMetadata.ColumnNames.SupplierID]);
            var entity = FindItemGridSupplierItem(supplierID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdSupplierItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            String supplierID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][SupplierItemMetadata.ColumnNames.SupplierID]);
            var entity = FindItemGridSupplierItem(supplierID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdSupplierItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = SupplierItems.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdSupplierItem.Rebind();
        }

        private SupplierItem FindItemGridSupplierItem(string supplierID)
        {
            var coll = SupplierItems;
            SupplierItem retval = null;
            foreach (SupplierItem rec in coll)
            {
                if (rec.SupplierID.Equals(supplierID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        private SupplierItemCollection SupplierItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collSupplierItem"];
                    if (obj != null)
                        return ((SupplierItemCollection)(obj));
                }

                var coll = new SupplierItemCollection();
                var query = new SupplierItemQuery("a");

                var supQ = new SupplierQuery("b");
                query.InnerJoin(supQ).On(query.SupplierID == supQ.SupplierID);

                var prodmedQ = new ItemProductNonMedicQuery("p");
                query.InnerJoin(prodmedQ).On(query.ItemID == prodmedQ.ItemID);

                query.Where(query.ItemID == txtItemID.Text);

                query.Select
                    (
                        query.SupplierID,
                        query.ItemID,
                        supQ.SupplierName.As("refToSupplier_SupplierName"),
                        query.PurchaseDiscount1,
                        query.PurchaseDiscount2,
                        prodmedQ.SRPurchaseUnit,
                        query.PriceInPurchaseUnit,
                        query.IsActive,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID,
                        query.ConversionFactor
                    );

                query.OrderBy(query.LastUpdateDateTime.Descending);

                coll.Load(query);

                Session["collSupplierItem"] = coll;
                return coll;
            }
            set { Session["collSupplierItem"] = value; }
        }

        private void SetEntityValue(SupplierItem entity, GridCommandEventArgs e)
        {
            var userControl = (ItemProductMedicalSupplierDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = txtItemID.Text;
                entity.SupplierID = userControl.SupplierID;
                entity.SupplierName = userControl.SupplierName;
                entity.PurchaseDiscount1 = userControl.PurchaseDiscount1;
                entity.PurchaseDiscount2 = userControl.PurchaseDiscount2;
                entity.SRPurchaseUnit = userControl.SRPurchaseUnit;
                entity.ConversionFactor = userControl.ConversionFactor;
                entity.PriceInPurchaseUnit = userControl.PriceInPurchaseUnit;
                entity.IsActive = userControl.IsActive;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        #endregion

        #region ComboBox Function
        protected void cboItemGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemGroupQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.ItemGroupID,
                    query.ItemGroupName
                );
            query.Where
                (
                    query.Or
                        (
                            query.ItemGroupID.Like(searchTextContain),
                            query.ItemGroupName.Like(searchTextContain)
                        ),
                        query.IsActive == true,
                        query.SRItemType == ItemType.NonMedical
                );

            cboItemGroupID.DataSource = query.LoadDataTable();
            cboItemGroupID.DataBind();
        }

        protected void cboItemGroupID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemGroupName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemGroupID"].ToString();
        }

        protected void cboMarginID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            ItemProductMarginQuery query = new ItemProductMarginQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.MarginID,
                    query.MarginName
                );
            query.Where
                (
                    query.Or
                        (
                            query.MarginID.Like(searchTextContain),
                            query.MarginName.Like(searchTextContain)
                        ),
                        query.IsActive == true
                );

            cboMarginID.DataSource = query.LoadDataTable();
            cboMarginID.DataBind();
        }

        protected void cboMarginID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["MarginName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["MarginID"].ToString();
        }

        protected void cboSRItemUnit_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            AppStandardReferenceItemQuery query = new AppStandardReferenceItemQuery();
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
                        query.StandardReferenceID == AppEnum.StandardReference.ItemUnit.ToString(),
                        query.IsActive == true
                );

            cboSRItemUnit.DataSource = query.LoadDataTable();
            cboSRItemUnit.DataBind();
        }

        protected void cboSRPurchaseUnit_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            AppStandardReferenceItemQuery query = new AppStandardReferenceItemQuery();
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
                        query.StandardReferenceID == AppEnum.StandardReference.ItemUnit.ToString(),
                        query.IsActive == true
                );

            cboSRPurchaseUnit.DataSource = query.LoadDataTable();
            cboSRPurchaseUnit.DataBind();
        }

        protected void cboStandardReferenceItem_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboFabricID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            FabricQuery query = new FabricQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.FabricID,
                    query.FabricName
                );
            query.Where
                (
                    query.Or
                        (
                            query.FabricID.Like(searchTextContain),
                            query.FabricName.Like(searchTextContain)
                        ),
                        query.IsActive == true
                );

            cboFabricID.DataSource = query.LoadDataTable();
            cboFabricID.DataBind();
        }

        protected void cboFabricID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["FabricName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["FabricID"].ToString();
        }

        protected void cboProductAccount_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ProductAccountQuery("a");
            query.es.Top = 10;
            query.Select
                (
                    query.ProductAccountID,
                    query.ProductAccountName
                );
            query.Where
                (
                    query.Or
                        (
                            query.ProductAccountID.Like(searchTextContain),
                            query.ProductAccountName.Like(searchTextContain)
                        ),
                        query.IsActive == true
                );

            cboProductAccount.DataSource = query.LoadDataTable();
            cboProductAccount.DataBind();
        }

        protected void cboProductAccount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ProductAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ProductAccountID"].ToString();
        }

        protected void cboAssetGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AssetGroupQuery();
            query.Select(query.AssetGroupId, query.GroupName);
            query.es.Top = 20;
            query.Where
                (
                    query.GroupName.Like(searchTextContain),
                    query.IsActive == true
                );
            query.OrderBy(query.AssetGroupId.Ascending);

            cboAssetGroupID.DataSource = query.LoadDataTable();
            cboAssetGroupID.DataBind();
        }
        protected void cboAssetGroupID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["AssetGroupId"] + @" - " + ((DataRowView)e.Item.DataItem)["GroupName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["AssetGroupId"].ToString();
        }

        #endregion

        #region Location
        private ItemBalanceCollection ItemBalances
        {
            get
            {
                object obj = Session["collItemBalance"];
                if (obj != null && IsPostBack)
                {
                    return ((ItemBalanceCollection)(obj));
                }

                var coll = new ItemBalanceCollection();

                var query = new ItemBalanceQuery("a");
                var qrItemNonMed = new ItemProductNonMedicQuery("b");
                var qrRef = new AppStandardReferenceItemQuery("c");
                var qrLoc = new LocationQuery("d");
                var qrItemBin = new AppStandardReferenceItemQuery("e");

                query.InnerJoin(qrItemNonMed).On(query.ItemID == qrItemNonMed.ItemID);
                query.LeftJoin(qrRef).On(qrItemNonMed.SRItemUnit == qrRef.ItemID & qrRef.StandardReferenceID == "ItemUnit");
                query.InnerJoin(qrLoc).On(query.LocationID == qrLoc.LocationID);
                query.LeftJoin(qrItemBin).On(query.SRItemBin == qrItemBin.ItemID &
                                             qrItemBin.StandardReferenceID == "ItemBin");

                query.Where(query.ItemID == txtItemID.Text);

                query.Select(query, qrLoc.LocationName.As("refToLocation_LocationName"), qrRef.ItemName.As("refToSRI_ItemUnit"),
                             qrItemBin.ItemName.As("refToSRI_ItemBin"));
                query.OrderBy(query.LocationID.Ascending);
                coll.Load(query);

                Session["collItemBalance"] = coll;
                return coll;
            }
            set { Session["collItemBalance"] = value; }
        }

        private ItemBalanceDetailCollection ItemBalanceDetails
        {
            get
            {
                object obj = Session["collItemBalanceDetail"];
                if (obj != null && IsPostBack)
                {
                    return ((ItemBalanceDetailCollection)(obj));
                }

                var coll = new ItemBalanceDetailCollection();
                var query = new ItemBalanceDetailQuery("a");
                query.Where(query.ItemID == txtItemID.Text);

                query.Select(query);
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collItemBalanceDetail"] = coll;
                return coll;
            }
            set { Session["collItemBalanceDetail"] = value; }
        }

        private ItemBalance FindItemBalance(String locationID)
        {
            return ItemBalances.Where(x => x.LocationID.Equals(locationID)).FirstOrDefault();
        }

        private List<ItemBalanceDetail> FindItemBalanceDetail(String locationID)
        {
            return ItemBalanceDetails.Where(x => x.LocationID.Equals(locationID) && x.Balance != 0).ToList();
        }

        private void SetEntityValueItemBalance(ItemBalance entity, GridCommandEventArgs e)
        {
            ItemProductNonMedicalBalanceDetail userControl =
                (ItemProductNonMedicalBalanceDetail)e.Item.
                FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.LocationID = userControl.LocationID;
                entity.LocationName = userControl.LocationName;
                entity.SRItemUnitName = userControl.SRItemUnitName;
                entity.Minimum = userControl.Minimum;
                entity.Maximum = userControl.Maximum;
                entity.SRItemBin = userControl.SRItemBin;
                entity.SRItemBinName = userControl.SRItemBinName;
                entity.ItemSubBin = userControl.ItemSubBin;
            }
        }

        private void SetEntityValueItemBalanceDetail(ItemBalanceDetail entity, GridCommandEventArgs e)
        {
            var userControl = (ItemProductNonMedicalBalanceDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.LocationID = userControl.LocationID;
                entity.ReferenceNo = string.Empty;
                entity.TransactionCode = string.Empty;
                entity.BalanceDate = DateTime.Now;
                entity.Balance = 0;
                entity.Booking = 0;
                entity.Price = 0;
            }
        }

        protected void grdLocation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdLocation.DataSource = ItemBalances;
        }

        protected void grdLocation_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String LocationID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemBalanceMetadata.ColumnNames.LocationID]);
            ItemBalance entity = FindItemBalance(LocationID);
            if (entity != null)
                SetEntityValueItemBalance(entity, e);
        }

        protected void grdLocation_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String LocationID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemBalanceMetadata.ColumnNames.LocationID]);
            ItemBalance entity = FindItemBalance(LocationID);
            if (entity != null)
            {
                // delete is forbidden if balance > 0!!
                if (entity.Balance != 0)
                {
                    Helper.ShowMessageAfterPostback(this, "Item can not be deleted, balance is not empty");
                    e.Canceled = true;
                    return;
                }
                entity.MarkAsDeleted();
            }

            List<ItemBalanceDetail> enDetail = FindItemBalanceDetail(LocationID);
            foreach (var ibd in enDetail)
            {
                // hapus saja
                ibd.MarkAsDeleted();
            }
        }

        protected void grdLocation_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemBalance entity = ItemBalances.AddNew();
            SetEntityValueItemBalance(entity, e);

            ItemBalanceDetail enDetail = ItemBalanceDetails.AddNew();
            SetEntityValueItemBalanceDetail(enDetail, e);

            //Stay in insert mode
            e.Canceled = true;
            grdLocation.Rebind();
        }

        private void RefreshCommandItemGridLocation(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdLocation.Columns[0].Visible = isVisible;
            grdLocation.Columns[grdLocation.Columns.Count - 1].Visible = isVisible;
            grdLocation.Columns.FindByUniqueName("editED").Visible = this.IsPowerUser && AppSession.Parameter.IsEnabledStockWithEdControl && chkIsControlExpired.Checked && !isVisible;

            grdLocation.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
            {
                ItemBalances = null;
                ItemBalanceDetails = null;
            }

            //Perbaharui tampilan dan data
            grdLocation.Rebind();
        }
        #endregion

        #region Record Detail Method Function ItemProductFabric

        private void RefreshCommandItemProductFabric(AppEnum.DataMode newVal)
        {
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdFabric.Columns[grdFabric.Columns.Count - 1].Visible = isVisible;

            grdFabric.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            grdFabric.Rebind();
        }

        private ItemProductFabricCollection ItemProductFabrics
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemProductFabric"];
                    if (obj != null)
                    {
                        return ((ItemProductFabricCollection)(obj));
                    }
                }

                var coll = new ItemProductFabricCollection();
                var query = new ItemProductFabricQuery("a");
                var fq = new FabricQuery("f");
                query.InnerJoin(fq).On(fq.FabricID == query.FabricID);

                query.Where(query.ItemID == txtItemID.Text);
                query.Select(
                    query,
                    fq.FabricName.As("refToFabric_FabricName")
                    );

                coll.Load(query);

                Session["collItemProductFabric"] = coll;
                return coll;
            }
            set { Session["collItemProductFabric"] = value; }
        }

        private void PopulateItemProductFabricGrid()
        {
            ItemProductFabrics = null;
            grdFabric.DataSource = ItemProductFabrics;
            grdFabric.MasterTableView.IsItemInserted = false;
            grdFabric.MasterTableView.ClearEditItems();
            grdFabric.DataBind();
        }

        protected void grdFabric_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdFabric.DataSource = ItemProductFabrics;
        }

        protected void grdFabric_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemProductFabricMetadata.ColumnNames.FabricID]);
            var entity = FindFabric(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdFabric_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ItemProductFabrics.AddNew();
            SetEntityValueItemFabric(entity, e);

            e.Canceled = true;
            grdFabric.Rebind();
        }

        private BusinessObject.ItemProductFabric FindFabric(string id)
        {
            var coll = ItemProductFabrics;
            return coll.FirstOrDefault(rec => rec.FabricID.Equals(id));
        }

        private void SetEntityValueItemFabric(BusinessObject.ItemProductFabric entity, GridCommandEventArgs e)
        {
            var userControl = (ItemProductFabricCtl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = txtItemID.Text;
                entity.FabricID = userControl.FabricID;
                entity.FabricName = userControl.FabricName;
            }
        }

        #endregion

        #region PatientImage
        private void PopulateItemImage(byte[] photo)
        {
            // Patient Photo
            imgPatientPhoto.ImageUrl = string.Empty;
            if (!string.IsNullOrEmpty(CaptureImageFile))
            {
                // Load form webcam capture
                var capturedImageFileArgs = CaptureImageFile.Split('|');
                var capturedImageFile = capturedImageFileArgs[0];
                if (Convert.ToBoolean(capturedImageFileArgs[2]) == true)
                {
                    var imgByteArr = (new ImageHelper()).LoadImageToArray(capturedImageFile);
                    if (imgByteArr != null)
                    {
                        imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(imgByteArr));
                        return;
                    }
                }
            }

            // Show Image
            imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(photo));
        }

        private string CaptureImageFile
        {
            get
            {
                var obj = Session["capturedImageFile"];
                if (obj != null && !string.IsNullOrEmpty(obj.ToString())) return obj.ToString();
                return string.Empty;
            }
            set
            {
                Session["capturedImageFile"] = string.Empty;
            }
        }

        private byte[] ItemImageInBytes()
        {
            if (!string.IsNullOrEmpty(CaptureImageFile))
            {
                var capturedImageFileArgs = CaptureImageFile.Split('|');
                if (Convert.ToBoolean(capturedImageFileArgs[2]) == true) // Save hanya jika statusnya sudah di crop
                {
                    var imgByteArr = (new ImageHelper()).LoadImageToArray(capturedImageFileArgs[0]);
                    if (imgByteArr != null) return imgByteArr;
                }
            }
            return null;
        }
        #endregion
    }
}

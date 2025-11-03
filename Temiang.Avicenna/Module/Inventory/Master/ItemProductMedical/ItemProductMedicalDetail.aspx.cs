using System;
using System.Configuration;
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
using System.Text;
using Temiang.Avicenna.Module.RADT.Master;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ItemProductMedicalDetail : BasePageDetail
    {
        private string FormType
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["type"]))
                    return string.Empty;
                return Request.QueryString["type"];
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack)
            {
                CboSrItemDisable();
                ChkIsGenericDisable();
            }
        }

        private void SetEntityValue(Item entity, ItemProductMedic detail)
        {
            entity.ItemID = txtItemID.Text;
            entity.ItemGroupID = cboItemGroupID.SelectedValue;
            entity.SRItemType = ItemType.Medical;
            entity.SRBillingGroup = cboBillingGroup.SelectedValue;
            entity.SRBpjsItemGroup = cboSRBpjsItemGroup.SelectedValue;
            entity.ProductAccountID = cboProductAccount.SelectedValue;
            entity.ItemName = AppSession.Parameter.IsItemInventoryNameUsingUpperCase ? txtItemName.Text.ToUpper() : txtItemName.Text;
            entity.IsActive = chkIsActive.Checked;
            entity.ItemIDExternal = txtItemIDExternal.Text;
            entity.Notes = txtNotes.Text;
            entity.IsItemProduction = chkIsItemProduction.Checked;
            entity.IsNeedToBeSterilized = chkIsNeedToBeSterilized.Checked;
            entity.Barcode = txtBarcode.Text;
            entity.SREklaimTariffGroup = cboSREklaimGroup.SelectedValue;
            entity.Photo = ItemImageInBytes();
            entity.SRItemCategory = cboSRItemCategory.SelectedValue;
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
            detail.str.MarginID = cboMarginID.SelectedValue;
            detail.SRProductType = cboSRProductType.SelectedValue;
            detail.ABCClass = txtABCClass.Text;
            detail.BrandName = acbBrandName.Text;
            detail.SRItemUnit = cboSRItemUnit.SelectedValue;
            detail.SRPurchaseUnit = cboSRPurchaseUnit.SelectedValue;
            detail.ConversionFactor = Convert.ToDecimal(txtConversionFactor.Value);
            detail.Dosage = Convert.ToDecimal(txtDosage.Value);
            detail.SRDosageUnit = cboSRDosageUnit.SelectedValue;
            detail.IsFormularium = chkIsFormularium.Checked;
            detail.IsInventoryItem = chkIsInventoryItem.Checked;
            detail.IsUsingCigna = chkIsUsingCigna.Checked;
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
            detail.SRDrugLabelType = cboSRDrugLabelType.SelectedValue;
            detail.SRRoute = cboSRRoute.SelectedValue;
            detail.SRItemBin = cboSRItemBin.SelectedValue;
            detail.IsConsignment = chkIsConsignment.Checked;
            detail.SRTherapyGroup = cboSRTherapyGroupID.SelectedValue;
            detail.str.TherapyID = cboTherapyID.SelectedValue;
            detail.IsActualDeduct = chkIsActualDeduct.Checked;
            detail.PremiPharmaciesPercentage = Convert.ToDecimal(txtPremiPharmaciesPercentage.Value);
            detail.PremiPhysicianPercentage = Convert.ToDecimal(txtPremiPhysicianPercentage.Value);
            detail.HET = Convert.ToDecimal(txtHET.Value);
            detail.SRConsumeMethod = cboSRConsumeMethod.SelectedValue;
            detail.ConsumptionLimitInDay = Convert.ToInt32(txtConsumptionLimitInDay.Value);

            if (detail.es.IsAdded)
            {
                detail.PriceInPurchaseUnit = 0;
                detail.PriceInBaseUnit = 0;
                detail.PriceInBasedUnitWVat = 0;
                detail.HighestPriceInBasedUnit = 0;
                detail.LastPriceInBaseUnit = 0;
                detail.CostPrice = 0;
            }

            detail.IsPrecursor = chkIsPrecursor.Checked;
            detail.IsNarcotic = chkIsNarcotic.Checked;
            detail.IsPsychotropic = chkIsPsychotropic.Checked;
            detail.IsMorphine = chkIsMorphine.Checked;
            detail.IsGeneric = chkIsGeneric.Checked;
            detail.IsNonGeneric = chkIsNonGeneric.Checked;
            detail.IsAntibiotic = chkIsAntibiotic.Checked;
            detail.IsRegularItem = chkIsRegularItem.Checked;
            detail.IsSalesAvailable = chkIsSalesAvailable.Checked;
            detail.IsDirectPurchase = FormType == "direct";
            detail.SRKeeping = cboSRKeeping.SelectedValue;
            detail.VENClass = cboVENClass.SelectedValue;
            detail.IsHam = chkIsHam.Checked;
            detail.IsLasa = chkIsLasa.Checked;
            detail.IsOot = chkIsOot.Checked;
            detail.IsSharePurchaseDiscToPatient = chkIsSharePurchaseDiscToPatient.Checked;
            detail.IsFornas = chkIsFornas.Checked;
            detail.IsOtc = chkIsOtc.Checked;
            detail.IsHardDrug = chkIsHardDrug.Checked;
            detail.IsTraditionalMedicine = chkIsTraditionalMedicine.Checked;
            detail.IsSupplement = chkIsSupplement.Checked;
            detail.IsMedication = chkIsMedication.Checked;
            detail.IsNoPrescriptionFee = chkIsNoPrescriptionFee.Checked;
            detail.IsPethidine = chkIsPethidine.Checked;
            detail.SRAntibioticLine = cboSRAntibioticLine.SelectedValue;
            detail.IsNonGenericLimited = chkIsNonGenericLimited.Checked;
            detail.IsChronic = chkIsChronic.Checked;
            detail.SpecificInfo = txtSpecificInfo.Text;
            detail.FornasRestrictionNotes = txtFornasRestrictionNotes.Text;
            detail.BpjsMaxQtyOrderIpr = Convert.ToInt32(txtBpjsMaxQtyOrderIpr.Value);
            detail.BpjsMaxQtyOrderOpr = Convert.ToInt32(txtBpjsMaxQtyOrderOpr.Value);
            detail.BpjsMaxQtyOrderEmr = Convert.ToInt32(txtBpjsMaxQtyOrderEmr.Value);

            //Last Update Status
            if (detail.es.IsAdded || detail.es.IsModified)
            {
                detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                detail.LastUpdateDateTime = DateTime.Now;
            }

            foreach (var dosageDetail in ItemProductDosageDetails)
            {
                dosageDetail.ItemID = txtItemID.Text;
                dosageDetail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                dosageDetail.LastUpdateDateTime = DateTime.Now;
            }
            //matrix label
            foreach (ItemProductMedicLabel l in ItemProductMedicLabels)
            {
                l.ItemID = txtItemID.Text;
                if (l.es.IsAdded)
                {
                    l.InsertByUserID = AppSession.UserLogin.UserID;
                    l.InsertDateTime = DateTime.Now;
                    l.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    l.LastUpdateDateTime = DateTime.Now;
                }
                else if (l.es.IsModified)
                {
                    l.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    l.LastUpdateDateTime = DateTime.Now;
                }
            }
            //matrix zat aktif
            foreach (ItemProductMedicZatActive l in ItemProductMedicZatActives)
            {
                l.ItemID = txtItemID.Text;
                if (l.es.IsAdded)
                {
                    l.InsertByUserID = AppSession.UserLogin.UserID;
                    l.InsertDateTime = DateTime.Now;
                    l.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    l.LastUpdateDateTime = DateTime.Now;
                }
                else if (l.es.IsModified)
                {
                    l.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    l.LastUpdateDateTime = DateTime.Now;
                }
            }
            //matrix Indication
            foreach (ItemProductMedicIndication l in ItemProductMedicIndications)
            {
                l.ItemID = txtItemID.Text;
                if (l.es.IsAdded)
                {
                    l.InsertByUserID = AppSession.UserLogin.UserID;
                    l.InsertDateTime = DateTime.Now;
                    l.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    l.LastUpdateDateTime = DateTime.Now;
                }
                else if (l.es.IsModified)
                {
                    l.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    l.LastUpdateDateTime = DateTime.Now;
                }
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

            ItemBridgingCollection collBridging = ItemBridgings;
            foreach (ItemBridging unit in collBridging)
            {
                unit.ItemID = txtItemID.Text;

                if (unit.es.IsAdded || unit.es.IsModified)
                {
                    unit.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    unit.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ItemQuery("a");
            var qs = new ItemProductMedicQuery("b");
            que.InnerJoin(qs).On(que.ItemID == qs.ItemID);
            que.es.Top = 1; // SELECT TOP 1 ..
            if (FormType == "direct")
                que.Where(qs.IsDirectPurchase == true);
            else
                que.Where(qs.IsDirectPurchase == false);
            if (isNextRecord)
            {
                que.Where(que.ItemID > txtItemID.Text, que.SRItemType == ItemType.Medical);
                que.OrderBy(que.ItemID.Ascending);
            }
            else
            {
                que.Where(que.ItemID < txtItemID.Text, que.SRItemType == ItemType.Medical);
                que.OrderBy(que.ItemID.Descending);
            }
            var entity = new Item();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Item();
            if (parameters.Length > 0)
            {
                String itemID = parameters[0];

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
            cboSRBpjsItemGroup.SelectedValue = item.SRBpjsItemGroup;
            cboSRItemCategory.SelectedValue = item.SRItemCategory;

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

            ComboBox.PopulateWithOneItemGroup(cboItemGroupID, item.ItemGroupID);

            txtItemName.Text = item.ItemName;
            chkIsActive.Checked = item.IsActive ?? false;
            txtNotes.Text = item.Notes;
            chkIsItemProduction.Checked = item.IsItemProduction ?? false;

            var itemProductMedDt = new ItemProductMedic();
            if (item.ItemID != null)
                itemProductMedDt.LoadByPrimaryKey(item.ItemID);

            if (!string.IsNullOrEmpty(itemProductMedDt.MarginID))
            {
                var mQuery = new ItemProductMarginQuery();
                mQuery.Where(mQuery.MarginID == itemProductMedDt.str.MarginID);
                cboMarginID.DataSource = mQuery.LoadDataTable();
                cboMarginID.DataBind();
                cboMarginID.SelectedValue = itemProductMedDt.MarginID;
            }
            else
            {
                cboMarginID.Items.Clear();
                cboMarginID.Text = string.Empty;
            }

            cboSRProductType.SelectedValue = itemProductMedDt.SRProductType;
            txtABCClass.Text = itemProductMedDt.ABCClass;
            AutoCompleteBox.SetValue(acbBrandName, itemProductMedDt.BrandName, acbBrandName.Delimiter.ToCharArray()[0]);

            if (!string.IsNullOrEmpty(itemProductMedDt.SRItemUnit))
            {
                var itemUnitQ = new AppStandardReferenceItemQuery();
                itemUnitQ.Where(itemUnitQ.ItemID == itemProductMedDt.str.SRItemUnit,
                               itemUnitQ.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                cboSRItemUnit.DataSource = itemUnitQ.LoadDataTable();
                cboSRItemUnit.DataBind();
                cboSRItemUnit.SelectedValue = itemProductMedDt.SRItemUnit;
            }
            else
            {
                cboSRItemUnit.Items.Clear();
                cboSRItemUnit.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(itemProductMedDt.SRPurchaseUnit))
            {
                var purchaseUnitQ = new AppStandardReferenceItemQuery();
                purchaseUnitQ.Where(purchaseUnitQ.ItemID == itemProductMedDt.str.SRPurchaseUnit,
                               purchaseUnitQ.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                cboSRPurchaseUnit.DataSource = purchaseUnitQ.LoadDataTable();
                cboSRPurchaseUnit.DataBind();
                cboSRPurchaseUnit.SelectedValue = itemProductMedDt.SRPurchaseUnit;
            }
            else
            {
                cboSRPurchaseUnit.Items.Clear();
                cboSRPurchaseUnit.Text = string.Empty;
            }

            txtConversionFactor.Value = Convert.ToDouble(itemProductMedDt.ConversionFactor);
            txtDosage.Value = Convert.ToDouble(itemProductMedDt.Dosage);

            if (!string.IsNullOrEmpty(itemProductMedDt.SRDosageUnit))
            {
                var dosageUnitQ = new AppStandardReferenceItemQuery();
                dosageUnitQ.Where(dosageUnitQ.ItemID == itemProductMedDt.str.SRDosageUnit,
                                  dosageUnitQ.StandardReferenceID == AppEnum.StandardReference.DosageUnit);
                cboSRDosageUnit.DataSource = dosageUnitQ.LoadDataTable();
                cboSRDosageUnit.DataBind();
                cboSRDosageUnit.SelectedValue = itemProductMedDt.SRDosageUnit;
            }
            else
            {
                cboSRDosageUnit.Items.Clear();
                cboSRDosageUnit.Text = string.Empty;
            }

            chkIsFormularium.Checked = itemProductMedDt.IsFormularium ?? false;
            chkIsInventoryItem.Checked = itemProductMedDt.IsInventoryItem ?? false;
            chkIsUsingCigna.Checked = itemProductMedDt.IsUsingCigna ?? false;
            chkIsControlExpired.Checked = itemProductMedDt.IsControlExpired ?? false;

            if (!string.IsNullOrEmpty(itemProductMedDt.FabricID))
            {
                var fabricQ = new FabricQuery();
                fabricQ.Where(fabricQ.FabricID == itemProductMedDt.str.FabricID);
                cboFabricID.DataSource = fabricQ.LoadDataTable();
                cboFabricID.DataBind();
                cboFabricID.SelectedValue = itemProductMedDt.FabricID;
            }
            else
            {
                cboFabricID.Items.Clear();
                cboFabricID.Text = string.Empty;
            }

            txtSalesFixedPrice.Value = Convert.ToDouble(itemProductMedDt.SalesFixedPrice);
            txtMarginPercentage.Value = Convert.ToDouble(itemProductMedDt.MarginPercentage);
            txtSalesDiscount.Value = Convert.ToDouble(itemProductMedDt.SalesDiscount);
            txtPriceInPurchaseUnit.Value = Convert.ToDouble(itemProductMedDt.PriceInPurchaseUnit);
            txtPriceInBaseUnit.Value = Convert.ToDouble(itemProductMedDt.PriceInBaseUnit);
            txtPriceInBasedUnitWVat.Value = Convert.ToDouble(itemProductMedDt.PriceInBasedUnitWVat);
            txtHighestPriceInBasedUnit.Value = Convert.ToDouble(itemProductMedDt.HighestPriceInBasedUnit);
            txtCostPrice.Value = Convert.ToDouble(itemProductMedDt.CostPrice);
            txtPurchaseDiscount1.Value = Convert.ToDouble(itemProductMedDt.PurchaseDiscount1);
            txtPurchaseDiscount2.Value = Convert.ToDouble(itemProductMedDt.PurchaseDiscount2);
            txtSafetyStock.Value = Convert.ToDouble(itemProductMedDt.SafetyStock);
            txtSafetyTime.Value = Convert.ToDouble(itemProductMedDt.SafetyTime);
            txtLeadTime.Value = Convert.ToDouble(itemProductMedDt.LeadTime);
            txtTolerancePercentage.Value = Convert.ToDouble(itemProductMedDt.TolerancePercentage);
            txtBarcode.Text = itemProductMedDt.Barcode;
            cboSRDrugLabelType.SelectedValue = itemProductMedDt.SRDrugLabelType;
            cboSRRoute.SelectedValue = itemProductMedDt.SRRoute;
            cboSRItemBin.SelectedValue = itemProductMedDt.SRItemBin;
            chkIsConsignment.Checked = itemProductMedDt.IsConsignment ?? false;
            txtPremiPharmaciesPercentage.Value = Convert.ToDouble(itemProductMedDt.PremiPharmaciesPercentage);
            txtPremiPhysicianPercentage.Value = Convert.ToDouble(itemProductMedDt.PremiPhysicianPercentage);
            txtHET.Value = Convert.ToDouble(itemProductMedDt.HET);

            if (!string.IsNullOrEmpty(itemProductMedDt.SRTherapyGroup))
            {
                var stdRefQ = new AppStandardReferenceItemQuery();
                stdRefQ.Where(stdRefQ.StandardReferenceID == AppEnum.StandardReference.TherapyGroup,
                              stdRefQ.ItemID == itemProductMedDt.str.SRTherapyGroup);
                cboSRTherapyGroupID.DataSource = stdRefQ.LoadDataTable();
                cboSRTherapyGroupID.DataBind();
                cboSRTherapyGroupID.SelectedValue = itemProductMedDt.SRTherapyGroup;
            }
            else
            {
                cboSRTherapyGroupID.Items.Clear();
                cboSRTherapyGroupID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(itemProductMedDt.TherapyID))
            {
                var tQuery = new TherapyQuery();
                tQuery.Where(tQuery.SRTherapyGroup == itemProductMedDt.str.SRTherapyGroup,
                             tQuery.TherapyID == itemProductMedDt.str.TherapyID);
                cboTherapyID.DataSource = tQuery.LoadDataTable();
                cboTherapyID.DataBind();
                cboTherapyID.SelectedValue = itemProductMedDt.TherapyID;
            }
            else
            {
                cboTherapyID.Items.Clear();
                cboTherapyID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(itemProductMedDt.SRConsumeMethod))
            {
                var q = new ConsumeMethodQuery();
                q.Where(q.SRConsumeMethod == itemProductMedDt.str.SRConsumeMethod);
                cboSRConsumeMethod.DataSource = q.LoadDataTable();
                cboSRConsumeMethod.DataBind();
                cboSRConsumeMethod.SelectedValue = itemProductMedDt.SRConsumeMethod;
            }
            else
            {
                cboSRConsumeMethod.Items.Clear();
                cboSRConsumeMethod.Text = string.Empty;
            }

            chkIsActualDeduct.Checked = itemProductMedDt.IsActualDeduct ?? false;

            //chkIsAutoBill.Checked = itemProductMedDt.IsAutoBill ?? false;
            txtItemIDExternal.Text = item.ItemIDExternal;

            chkIsPrecursor.Checked = itemProductMedDt.IsPrecursor ?? false;
            chkIsNarcotic.Checked = itemProductMedDt.IsNarcotic ?? false;
            chkIsPsychotropic.Checked = itemProductMedDt.IsPsychotropic ?? false;
            chkIsMorphine.Checked = itemProductMedDt.IsMorphine ?? false;
            chkIsGeneric.Checked = itemProductMedDt.IsGeneric ?? false;
            chkIsNonGeneric.Checked = itemProductMedDt.IsNonGeneric ?? false;
            chkIsAntibiotic.Checked = itemProductMedDt.IsAntibiotic ?? false;
            chkIsRegularItem.Checked = itemProductMedDt.IsRegularItem ?? false;
            chkIsSalesAvailable.Checked = itemProductMedDt.IsSalesAvailable ?? false;
            chkIsSharePurchaseDiscToPatient.Checked = itemProductMedDt.IsSharePurchaseDiscToPatient ?? false;
            chkIsFornas.Checked = itemProductMedDt.IsFornas ?? false;
            chkIsOtc.Checked = itemProductMedDt.IsOtc ?? false;
            chkIsHardDrug.Checked = itemProductMedDt.IsHardDrug ?? false;
            chkIsTraditionalMedicine.Checked = itemProductMedDt.IsTraditionalMedicine ?? false;
            chkIsSupplement.Checked = itemProductMedDt.IsSupplement ?? false;
            chkIsMedication.Checked = itemProductMedDt.IsMedication ?? false;
            chkIsNoPrescriptionFee.Checked = itemProductMedDt.IsNoPrescriptionFee ?? false;
            chkIsPethidine.Checked = itemProductMedDt.IsPethidine ?? false;
            cboSRAntibioticLine.SelectedValue = itemProductMedDt.SRAntibioticLine;
            chkIsNonGenericLimited.Checked = itemProductMedDt.IsNonGenericLimited ?? false;
            chkIsChronic.Checked = itemProductMedDt.IsChronic ?? false;
            txtSpecificInfo.Text = itemProductMedDt.SpecificInfo;
            txtFornasRestrictionNotes.Text = itemProductMedDt.FornasRestrictionNotes;
            if (!string.IsNullOrEmpty(itemProductMedDt.SRKeeping))
            {
                var keepingQ = new AppStandardReferenceItemQuery();
                keepingQ.Where(keepingQ.ItemID == itemProductMedDt.str.SRKeeping,
                               keepingQ.StandardReferenceID == AppEnum.StandardReference.Keeping);
                cboSRKeeping.DataSource = keepingQ.LoadDataTable();
                cboSRKeeping.DataBind();
                cboSRKeeping.SelectedValue = itemProductMedDt.SRKeeping;
            }
            else
            {
                cboSRKeeping.Items.Clear();
                cboSRKeeping.Text = string.Empty;
            }
            cboVENClass.SelectedValue = itemProductMedDt.VENClass;
            chkIsHam.Checked = itemProductMedDt.IsHam ?? false;
            chkIsLasa.Checked = itemProductMedDt.IsLasa ?? false;
            chkIsOot.Checked = itemProductMedDt.IsOot ?? false;
            chkIsNeedToBeSterilized.Checked = item.IsNeedToBeSterilized ?? false;

            cboSREklaimGroup.SelectedValue = item.SREklaimTariffGroup;
            txtConsumptionLimitInDay.Value = Convert.ToDouble(itemProductMedDt.ConsumptionLimitInDay);

            chkIsAsset.Checked = item.IsAsset ?? false;
            txtEconomicLifeInYear.Value = Convert.ToDouble(item.EconomicLifeInYear);
            txtBpjsMaxQtyOrderIpr.Value = Convert.ToDouble(itemProductMedDt.BpjsMaxQtyOrderIpr);
            txtBpjsMaxQtyOrderOpr.Value = Convert.ToDouble(itemProductMedDt.BpjsMaxQtyOrderOpr);
            txtBpjsMaxQtyOrderEmr.Value = Convert.ToDouble(itemProductMedDt.BpjsMaxQtyOrderEmr);

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

            ItemProductMedicMargins = null;
            grdMarginDetail.DataSource = ItemProductMedicMargins;
            grdMarginDetail.DataBind();

            PopulateItemProductDosageDetailGrid();
            PopulateItemLabelGrid();
            PopulateItemZatActiveGrid();
            PopulateSupplierItemGrid();
            PopulateItemIndicationGrid();
            PopulateItemProductConsumeUnitMatrixGrid();
            PopulateItemProductFabricGrid();

            // Pcare Map
            pcareReference.Populate(item.ItemID);

            PopulateItemBirdgingGrid();

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
            cboSRTherapyGroupID.Text = string.Empty;
            cboTherapyID.Text = string.Empty;
            cboSRDrugLabelType.Text = string.Empty;
            cboSRRoute.Text = string.Empty;

            cboSRItemBin.Text = string.Empty;
            cboSRItemUnit.Text = string.Empty;
            cboSRPurchaseUnit.Text = string.Empty;
            cboSRDosageUnit.Text = string.Empty;
            cboFabricID.Text = string.Empty;
            cboBillingGroup.Text = string.Empty;
            cboSRBpjsItemGroup.Text = string.Empty;
            cboSRItemCategory.Text = string.Empty;
            cboProductAccount.Text = string.Empty;

            var mrg = new ItemProductMarginQuery();
            mrg.es.Top = 1;
            mrg.Select
                (
                    mrg.MarginID,
                    mrg.MarginName
                );
            mrg.Where(mrg.IsActive == true);
            mrg.OrderBy(mrg.MarginID.Ascending);

            var tbl = mrg.LoadDataTable();

            cboMarginID.DataSource = tbl;
            cboMarginID.DataBind();

            if (tbl.Rows.Count > 0)
                cboMarginID.SelectedIndex = 0;

            chkIsRegularItem.Checked = true;
            chkIsSalesAvailable.Checked = true;
            chkIsMedication.Checked = true;

            if (AppSession.Parameter.IsEklaimGroupUsingDefaultValue)
                cboSREklaimGroup.SelectedValue = "13";
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
            RefreshCommandItemGridLabel(oldVal, newVal);
            RefreshCommandItemGridZatActive(oldVal, newVal);
            RefreshCommandItemGridIndication(oldVal, newVal);
            CboSrItemDisable();
            ChkIsGenericDisable();
            RefreshCommandItemProductDosageDetail(newVal);
            RefreshCommandItemMarginDetail(newVal);

            RefreshCommandItemGridSupplierItem(oldVal, newVal);
            RefreshCommandItemGridLocation(oldVal, newVal);
            RefreshCommandItemProductConsumeUnitMatrix(newVal);
            RefreshCommandItemProductFabric(newVal);
            RefreshCommandItemItemBridging(newVal);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "ItemProductMedicalSearch.aspx?type=" + FormType;
            UrlPageList = "ItemProductMedicalList.aspx?type=" + FormType;

            ProgramID = FormType == "direct"
                            ? AppConstant.Program.ItemProductMedicalDirectPurchase
                            : AppConstant.Program.ItemProductMedical;

            WindowSearch.Height = 400;

            trPcare.Visible = pcareReference.IsPCareValidation;

            //StandardReference Initialize
            if (!IsCallback)
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ItemUnit, this);

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRProductType, AppEnum.StandardReference.ProductType);
                StandardReference.InitializeIncludeSpace(cboSRDrugLabelType, AppEnum.StandardReference.DrugLabelType);
                StandardReference.InitializeIncludeSpace(cboSRRoute, AppEnum.StandardReference.Route);

                StandardReference.InitializeIncludeSpace(cboSRItemBin, AppEnum.StandardReference.ItemBin);
                StandardReference.InitializeIncludeSpace(cboBillingGroup, AppEnum.StandardReference.BillingGroup);
                StandardReference.InitializeIncludeSpace(cboSRBpjsItemGroup, AppEnum.StandardReference.BpjsItemGroup);
                StandardReference.InitializeIncludeSpaceOrderByRefId(cboVENClass, AppEnum.StandardReference.VenClass);
                StandardReference.InitializeIncludeSpace(cboSREklaimGroup, AppEnum.StandardReference.EklaimTariffGroup);
                StandardReference.InitializeIncludeSpace(cboSRItemCategory, AppEnum.StandardReference.ItemCategory);
                StandardReference.InitializeIncludeSpace(cboSRAntibioticLine, AppEnum.StandardReference.AntibioticLine);

                trBpjsItemGroup.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB";
                grdLocation.Columns.FindByUniqueName("ItemSubBin").Visible = AppSession.Parameter.IsUsingItemSubBin;

                var mdl = new AppProgram();
                bool isCssd = (mdl.LoadByPrimaryKey("14") && mdl.IsVisible == true);

                chkIsNeedToBeSterilized.Visible = isCssd;
                chkIsSharePurchaseDiscToPatient.Visible = !AppSession.Parameter.IsSharePurchaseDiscToPatient;

                ItemProductMedicMargins = null;
            }

            var allowCustomEntry = !AppParameter.IsYes(AppParameter.ParameterItem.IsGenericMustEqualZatActive);
            AutoCompleteBox.Initialized(acbBrandName, AppEnum.AutoCompleteBox.ZatActive, allowCustomEntry, true, "+");
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

                ItemProductMedic detail = new ItemProductMedic();
                if (detail.LoadByPrimaryKey(txtItemID.Text))
                    detail.MarkAsDeleted();
                else
                    detail = null;

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (detail != null)
                        detail.Save();

                    entity.Save();

                    //PCareReferenceItemMapping
                    pcareReference.Delete(entity.ItemID);

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

            if (!string.IsNullOrEmpty(cboSRDosageUnit.SelectedValue) && txtDosage.Value == 0)
            {
                args.MessageText = "Dosage must be greater than 0";
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

            if (string.IsNullOrEmpty(cboProductAccount.SelectedValue))
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

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdProductAutomatic))
            {
                if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdProductAutomaticUseGroupInitial))
                    txtItemID.Text = Helper.GetItemProductIDUseGroupInitial(cboItemGroupID.SelectedValue);
                else
                    txtItemID.Text = Helper.GetItemProductID(txtItemName.Text.ToUpper(), ItemType.Medical);
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

            var itemProd = new ItemProductMedic();
            if (itemProd.LoadByPrimaryKey(txtItemID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            var entity = new Item();
            entity.AddNew();

            itemProd = new ItemProductMedic();
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

            if (!string.IsNullOrEmpty(cboSRDosageUnit.SelectedValue) && txtDosage.Value == 0)
            {
                args.MessageText = "Dosage must be greater than 0";
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

            if (string.IsNullOrEmpty(cboProductAccount.SelectedValue))
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
            ItemProductMedic detail = new ItemProductMedic();

            if (entity.LoadByPrimaryKey(txtItemID.Text))
            {
                if (!detail.LoadByPrimaryKey(txtItemID.Text))
                {
                    detail = new ItemProductMedic();
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
        private void SaveEntity(Item entity, ItemProductMedic detail)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                detail.Save();

                foreach (GridDataItem dataItem in grdMarginDetail.MasterTableView.Items)
                {
                    var margin = ItemProductMedicMargins.FindByPrimaryKey(dataItem["ItemID"].Text, dataItem["ClassID"].Text);
                    if (margin != null)
                    {
                        margin.AmountPercentage = Convert.ToDecimal((dataItem.FindControl("txtAmount") as RadNumericTextBox).Value);
                    }
                }

                ItemProductMedicMargins.Save();

                ItemProductDosageDetails.Save();

                //Commit if success, Rollback if failed

                ItemProductMedicLabels.Save();
                ItemProductMedicZatActives.Save();
                ItemProductMedicIndications.Save();
                SupplierItems.Save();

                if (entity.IsActive == false && AppSession.Parameter.IsAutoDeleteBalanceOnInActiveItem)
                {
                    ItemBalances.MarkAllAsDeleted();
                    ItemBalanceDetails.MarkAllAsDeleted();
                }
                ItemBalances.Save();
                ItemBalanceDetails.Save();

                ItemProductConsumeUnitMatrixs.Save();
                ItemProductFabrics.Save();

                // Import ItemProductMedicZatActives from Generic input (Brandname)
                if (AppParameter.IsYes(AppParameter.ParameterItem.IsGenericMustEqualZatActive))
                {
                    // Delete first
                    var ipzas = new ItemProductMedicZatActiveCollection();
                    ipzas.Query.Where(ipzas.Query.ItemID == txtItemID.Text);
                    ipzas.LoadAll();
                    ipzas.MarkAllAsDeleted();
                    ipzas.Save();
                }
                if (!string.IsNullOrWhiteSpace(acbBrandName.Text))
                {
                    var zaNames = acbBrandName.Text.Split('+');
                    if (zaNames.Length > 0)
                    {
                        foreach (var zaName in zaNames)
                        {
                            var za = new ZatActive();
                            za.Query.es.Top = 1;
                            za.Query.Where(za.Query.ZatActiveName == zaName.Trim());
                            if (za.Query.Load())
                            {
                                var ipza = new ItemProductMedicZatActive();
                                if (!ipza.LoadByPrimaryKey(txtItemID.Text, za.ZatActiveID))
                                {
                                    ipza = new ItemProductMedicZatActive();
                                    ipza.ItemID = txtItemID.Text;
                                    ipza.ZatActiveID = za.ZatActiveID;
                                    ipza.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    ipza.LastUpdateDateTime = DateTime.Now;
                                    ipza.Save();
                                }
                            }
                        }

                    }
                }

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

                //PCareReferenceItemMapping
                pcareReference.Save(entity.ItemID);

                ItemBridgings.Save();

                trans.Complete();
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

                var ip = new ItemProductMedic();
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

        protected void ChkIsGenericDisable()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]) && DataModeCurrent == AppEnum.DataMode.Edit)
            {
                var ipm = new ItemProductMedic();
                ipm.LoadByPrimaryKey(Request.QueryString["id"]);

                chkIsGeneric.Enabled = !(ipm.IsNonGeneric ?? false);
                chkIsNonGeneric.Enabled = !(ipm.IsGeneric ?? false);
            }
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

        #region ComboBox Function

        protected void cboItemGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            ItemGroupQuery query = new ItemGroupQuery();
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
                        query.SRItemType == ItemType.Medical
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

        protected void cboSRDosageUnit_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
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
                        query.StandardReferenceID == AppEnum.StandardReference.DosageUnit.ToString(),
                        query.IsActive == true
                );

            cboSRDosageUnit.DataSource = query.LoadDataTable();
            cboSRDosageUnit.DataBind();
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

        protected void cboSRTherapyGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            //Common.ComboBox.StandardReferenceItemsRequested(cboSRTherapyGroupID, "TherapyGroup", e.Text);
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
                        query.StandardReferenceID == AppEnum.StandardReference.TherapyGroup.ToString(),
                        query.IsActive == true
                );

            cboSRTherapyGroupID.DataSource = query.LoadDataTable();
            cboSRTherapyGroupID.DataBind();
        }

        protected void cboSRTherapyGroupID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            //Common.ComboBox.StandardReferenceItemDataBound(e);
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRTherapyGroupID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulatecboTherapyID(e.Value);

            cboTherapyID.Text = string.Empty;
            cboTherapyID.SelectedValue = string.Empty;
        }

        protected void cboTherapyID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new TherapyQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.TherapyID,
                    query.TherapyName
                );
            query.Where
                (
                    query.Or
                        (
                            query.TherapyID.Like(searchTextContain),
                            query.TherapyName.Like(searchTextContain)
                        )
                );
            query.Where(
                    query.SRTherapyGroup == cboSRTherapyGroupID.SelectedValue
                );
            query.OrderBy(query.SRTherapyGroup.Ascending, query.TherapyID.Ascending);

            cboTherapyID.DataSource = query.LoadDataTable();
            cboTherapyID.DataBind();
        }

        protected void PopulatecboTherapyID(string SRTherapyGroupID)
        {
            var query = new TherapyQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.TherapyID,
                    query.TherapyName
                );
            query.Where(
                    query.SRTherapyGroup == SRTherapyGroupID
                );
            query.OrderBy(query.SRTherapyGroup.Ascending, query.TherapyID.Ascending);

            cboTherapyID.DataSource = query.LoadDataTable();
            cboTherapyID.DataBind();
        }

        protected void cboTherapyID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["TherapyName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["TherapyID"].ToString();
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

        protected void cboSRKeeping_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
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
                        query.StandardReferenceID == AppEnum.StandardReference.Keeping,
                        query.IsActive == true
                );

            cboSRKeeping.DataSource = query.LoadDataTable();
            cboSRKeeping.DataBind();
        }

        protected void cboSRConsumeMethod_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ConsumeMethodQuery("a");
            query.es.Top = 10;
            query.Select
                (
                    query.SRConsumeMethod,
                    query.SRConsumeMethodName
                );
            query.Where
                (
                    query.Or
                        (
                            query.SRConsumeMethod.Like(searchTextContain),
                            query.SRConsumeMethodName.Like(searchTextContain)
                        )
                );

            cboSRConsumeMethod.DataSource = query.LoadDataTable();
            cboSRConsumeMethod.DataBind();
        }

        protected void cboSRConsumeMethod_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SRConsumeMethodName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SRConsumeMethod"].ToString();
        }

        protected void chkIsGeneric_CheckedChanged(object sender, EventArgs e)
        {
            chkIsNonGeneric.Enabled = !chkIsGeneric.Checked;
            chkIsNonGeneric.Checked = false;
        }

        protected void chkIsNonGeneric_CheckedChanged(object sender, EventArgs e)
        {
            chkIsGeneric.Enabled = !chkIsNonGeneric.Checked;
            chkIsGeneric.Checked = false;
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

        #region MarginDetail
        protected void grdMarginDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdMarginDetail.DataSource = ItemProductMedicMargins;
        }

        private ItemProductMedicMarginDetailCollection ItemProductMedicMargins
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collItemProductMedicMarginDetailCollection"];
                    if (obj != null)
                        return ((ItemProductMedicMarginDetailCollection)(obj));
                }

                var mrg = new ItemProductMedicMarginDetailQuery("a");
                var clsq = new ClassQuery("b");

                mrg.Select(
                    mrg,
                    clsq.ClassName.As("refToClass_ClassName")
                    );
                mrg.InnerJoin(clsq).On(mrg.ClassID == clsq.ClassID);
                mrg.Where(
                    mrg.ItemID == txtItemID.Text,
                    clsq.IsActive == true
                    );

                var margins = new ItemProductMedicMarginDetailCollection();
                margins.Load(mrg);

                if (margins.Count == 0)
                {
                    var cls = new ClassCollection();
                    cls.Query.Where(cls.Query.IsActive == true);
                    cls.LoadAll();

                    foreach (var c in cls)
                    {
                        var margin = margins.AddNew();
                        margin.ItemID = txtItemID.Text;
                        margin.ClassID = c.ClassID;
                        margin.ClassName = c.ClassName;
                        margin.AmountPercentage = 0;
                        margin.LastUpdateDateTime = DateTime.Now;
                        margin.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                }

                Session["collItemProductMedicMarginDetailCollection"] = margins;
                return margins;
            }
            set { Session["collItemProductMedicMarginDetailCollection"] = value; }
        }

        private void RefreshCommandItemMarginDetail(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdMarginDetail.Columns[grdMarginDetail.Columns.Count - 2].Visible = isVisible;
            grdMarginDetail.Columns[grdMarginDetail.Columns.Count - 1].Visible = !isVisible;

            //Perbaharui tampilan dan data
            grdMarginDetail.Rebind();
        }
        #endregion

        #region Record Detail Method Function Item Dosage Detail

        private void RefreshCommandItemProductDosageDetail(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdDosage.Columns[0].Visible = isVisible;
            grdDosage.Columns[grdDosage.Columns.Count - 1].Visible = isVisible;

            grdDosage.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdDosage.Rebind();
        }

        private ItemProductDosageDetailCollection ItemProductDosageDetails
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemProductDosageDetail"];
                    if (obj != null)
                    {
                        return ((ItemProductDosageDetailCollection)(obj));
                    }
                }

                var coll = new ItemProductDosageDetailCollection();

                var query = new ItemProductDosageDetailQuery("a");
                var dosage = new AppStandardReferenceItemQuery("b");

                query.InnerJoin(dosage).On(
                    query.SRDosageUnit == dosage.ItemID &&
                    dosage.StandardReferenceID == AppEnum.StandardReference.DosageUnit.ToString()
                    );
                query.Where(query.ItemID == txtItemID.Text);
                query.Select(
                    query,
                    dosage.ItemName.As("refToAppStandardReferenceItem_ItemName")
                    );

                coll.Load(query);

                Session["collItemProductDosageDetail"] = coll;
                return coll;
            }
            set { Session["collItemProductDosageDetail"] = value; }
        }

        private void PopulateItemProductDosageDetailGrid()
        {
            //Display Data Detail
            ItemProductDosageDetails = null; //Reset Record Detail
            grdDosage.DataSource = ItemProductDosageDetails; //Requery
            grdDosage.MasterTableView.IsItemInserted = false;
            grdDosage.MasterTableView.ClearEditItems();
            grdDosage.DataBind();
        }

        protected void grdDosage_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDosage.DataSource = ItemProductDosageDetails;
        }

        protected void grdDosage_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            var itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemProductDosageDetailMetadata.ColumnNames.SRDosageUnit]);
            var entity = FindItemMedicBalance(itemID);
            if (entity != null)
                SetEntityValueItemMedic(entity, e);
        }

        protected void grdDosage_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemProductDosageDetailMetadata.ColumnNames.SRDosageUnit]);
            var entity = FindItemMedicBalance(itemID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdDosage_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ItemProductDosageDetails.AddNew();
            SetEntityValueItemMedic(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdDosage.Rebind();
        }

        private BusinessObject.ItemProductDosageDetail FindItemMedicBalance(String itemID)
        {
            var coll = ItemProductDosageDetails;
            return coll.FirstOrDefault(rec => rec.SRDosageUnit.Equals(itemID));
        }

        private void SetEntityValueItemMedic(BusinessObject.ItemProductDosageDetail entity, GridCommandEventArgs e)
        {
            var userControl = (Inventory.Master.ItemProductDosageDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRDosageUnit = userControl.SRDosageUnit;
                entity.ItemName = userControl.SRDosageUnitName;
                entity.Dosage = userControl.Dosage;
            }
        }

        #endregion

        #region ItemLabel
        protected void grdItemLabel_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemLabel.DataSource = ItemProductMedicLabels;
        }

        private void PopulateItemLabelGrid()
        {
            //Display Data Detail
            ItemProductMedicLabels = null; //Reset Record Detail
            grdItemLabel.DataSource = ItemProductMedicLabels; //Requery
            grdItemLabel.MasterTableView.IsItemInserted = false;
            grdItemLabel.MasterTableView.ClearEditItems();
            grdItemLabel.DataBind();
        }

        protected void grdItemLabel_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            String LabelID =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        LabellMetadata.ColumnNames.LabelID]);
            ItemProductMedicLabel entity = FindItemGridLabel(LabelID);
            if (entity != null)
                SetEntityValueLabel(entity, e);
        }

        protected void grdItemLabel_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String LabelID =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][LabellMetadata.ColumnNames.LabelID]);
            ItemProductMedicLabel entity = FindItemGridLabel(LabelID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItemLabel_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemProductMedicLabel entity = ItemProductMedicLabels.AddNew();
            SetEntityValueLabel(entity, e);
        }

        private ItemProductMedicLabel FindItemGridLabel(string LabelID)
        {
            ItemProductMedicLabelCollection coll = ItemProductMedicLabels;
            ItemProductMedicLabel retval = null;
            foreach (ItemProductMedicLabel rec in coll)
            {
                if (rec.LabelID.Equals(LabelID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        private void SetEntityValueLabel(ItemProductMedicLabel entity, GridCommandEventArgs e)
        {
            ItemProductMedicalLabelDetail userControl =
                (ItemProductMedicalLabelDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.LabelID = userControl.LabelID;
                entity.LabelName = userControl.LabelName;
                entity.ItemID = ((RadTextBox)Helper.FindControlRecursive(Page, "txtItemID")).Text;
            }
        }

        private ItemProductMedicLabelCollection ItemProductMedicLabels
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collItemProductMedicLabelCollection"];
                    if (obj != null)
                        return ((ItemProductMedicLabelCollection)(obj));
                }

                var mrg = new ItemProductMedicLabelQuery("a");
                var clsq = new LabellQuery("b");

                mrg.Select(
                    mrg,
                    clsq.LabelName.As("refToLabel_LabelName")
                    );
                mrg.InnerJoin(clsq).On(mrg.LabelID == clsq.LabelID);
                mrg.Where(
                    mrg.ItemID == txtItemID.Text,
                    clsq.IsActive == true
                    );

                var margins = new ItemProductMedicLabelCollection();
                margins.Load(mrg);

                Session["collItemProductMedicLabelCollection"] = margins;
                return margins;
            }
            set { Session["collItemProductMedicLabelCollection"] = value; }
        }

        private void RefreshCommandItemGridLabel(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemLabel.Columns[0].Visible = isVisible;
            grdItemLabel.Columns[grdItemLabel.Columns.Count - 1].Visible = isVisible;

            grdItemLabel.MasterTableView.CommandItemDisplay = isVisible
                                                                       ? GridCommandItemDisplay.Top
                                                                       : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                ItemProductMedicLabels = null;

            //Perbaharui tampilan dan data
            grdItemLabel.Rebind();
        }
        #endregion

        #region ItemZatActive
        protected void grdItemZatActive_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemZatActive.DataSource = ItemProductMedicZatActives;
        }

        private void PopulateItemZatActiveGrid()
        {
            //Display Data Detail
            ItemProductMedicZatActives = null; //Reset Record Detail
            grdItemZatActive.DataSource = ItemProductMedicZatActives; //Requery
            grdItemZatActive.MasterTableView.IsItemInserted = false;
            grdItemZatActive.MasterTableView.ClearEditItems();
            grdItemZatActive.DataBind();
        }

        protected void grdItemZatActive_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            String ZatActiveID =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ZatActiveMetadata.ColumnNames.ZatActiveID]);
            ItemProductMedicZatActive entity = FindItemGridZatActive(ZatActiveID);
            if (entity != null)
                SetEntityValueZatActive(entity, e);
        }

        protected void grdItemZatActive_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String ZatActiveID =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][ZatActiveMetadata.ColumnNames.ZatActiveID]);
            ItemProductMedicZatActive entity = FindItemGridZatActive(ZatActiveID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItemZatActive_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemProductMedicZatActive entity = ItemProductMedicZatActives.AddNew();
            SetEntityValueZatActive(entity, e);
        }

        private ItemProductMedicZatActive FindItemGridZatActive(string ZatActiveID)
        {
            ItemProductMedicZatActiveCollection coll = ItemProductMedicZatActives;
            ItemProductMedicZatActive retval = null;
            foreach (ItemProductMedicZatActive rec in coll)
            {
                if (rec.ZatActiveID.Equals(ZatActiveID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        private void SetEntityValueZatActive(ItemProductMedicZatActive entity, GridCommandEventArgs e)
        {
            ItemProductMedicalZatActiveDetail userControl =
                (ItemProductMedicalZatActiveDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ZatActiveID = userControl.ZatActiveID;
                entity.ZatActiveName = userControl.ZatActiveName;
                entity.IsPrinted = userControl.IsPrinted;
                entity.ItemID = ((RadTextBox)Helper.FindControlRecursive(Page, "txtItemID")).Text;
            }
        }

        private ItemProductMedicZatActiveCollection ItemProductMedicZatActives
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collItemProductMedicZatActiveCollection"];
                    if (obj != null)
                        return ((ItemProductMedicZatActiveCollection)(obj));
                }

                var mrg = new ItemProductMedicZatActiveQuery("a");
                var clsq = new ZatActiveQuery("b");

                mrg.Select(
                    mrg,
                    clsq.ZatActiveName.As("refToZatActive_ZatActiveName")
                    );
                mrg.InnerJoin(clsq).On(mrg.ZatActiveID == clsq.ZatActiveID);
                mrg.Where(
                    mrg.ItemID == txtItemID.Text,
                    clsq.IsActive == true
                    );

                var margins = new ItemProductMedicZatActiveCollection();
                margins.Load(mrg);

                Session["collItemProductMedicZatActiveCollection"] = margins;
                return margins;
            }
            set { Session["collItemProductMedicZatActiveCollection"] = value; }
        }

        private void RefreshCommandItemGridZatActive(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = !AppParameter.IsYes(AppParameter.ParameterItem.IsGenericMustEqualZatActive) && (newVal != AppEnum.DataMode.Read);
            grdItemZatActive.Columns[0].Visible = isVisible;
            grdItemZatActive.Columns[grdItemZatActive.Columns.Count - 1].Visible = isVisible;

            grdItemZatActive.MasterTableView.CommandItemDisplay = isVisible
                                                                       ? GridCommandItemDisplay.Top
                                                                       : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                ItemProductMedicZatActives = null;

            //Perbaharui tampilan dan data
            grdItemZatActive.Rebind();
        }
        #endregion

        #region ItemIndication
        protected void grdItemIndication_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemIndication.DataSource = ItemProductMedicIndications;
        }

        private void PopulateItemIndicationGrid()
        {
            //Display Data Detail
            ItemProductMedicIndications = null; //Reset Record Detail
            grdItemIndication.DataSource = ItemProductMedicIndications; //Requery
            grdItemIndication.MasterTableView.IsItemInserted = false;
            grdItemIndication.MasterTableView.ClearEditItems();
            grdItemIndication.DataBind();
        }

        protected void grdItemIndication_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            String IndicationID =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        IndicationMetadata.ColumnNames.IndicationID]);
            ItemProductMedicIndication entity = FindItemGridIndication(IndicationID);
            if (entity != null)
                SetEntityValueIndication(entity, e);
        }

        protected void grdItemIndication_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String IndicationID =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][IndicationMetadata.ColumnNames.IndicationID]);
            ItemProductMedicIndication entity = FindItemGridIndication(IndicationID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItemIndication_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemProductMedicIndication entity = ItemProductMedicIndications.AddNew();
            SetEntityValueIndication(entity, e);
        }

        private ItemProductMedicIndication FindItemGridIndication(string IndicationID)
        {
            ItemProductMedicIndicationCollection coll = ItemProductMedicIndications;
            ItemProductMedicIndication retval = null;
            foreach (ItemProductMedicIndication rec in coll)
            {
                if (rec.IndicationID.Equals(IndicationID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        private void SetEntityValueIndication(ItemProductMedicIndication entity, GridCommandEventArgs e)
        {
            ItemProductMedicalIndicationDetail userControl =
                (ItemProductMedicalIndicationDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.IndicationID = userControl.IndicationID;
                entity.IndicationName = userControl.IndicationName;
                entity.ItemID = ((RadTextBox)Helper.FindControlRecursive(Page, "txtItemID")).Text;
            }
        }

        private ItemProductMedicIndicationCollection ItemProductMedicIndications
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collItemProductMedicIndicationCollection"];
                    if (obj != null)
                        return ((ItemProductMedicIndicationCollection)(obj));
                }

                var mrg = new ItemProductMedicIndicationQuery("a");
                var clsq = new IndicationQuery("b");

                mrg.Select(
                    mrg,
                    clsq.IndicationName.As("refToIndication_IndicationName")
                    );
                mrg.InnerJoin(clsq).On(mrg.IndicationID == clsq.IndicationID);
                mrg.Where(
                    mrg.ItemID == txtItemID.Text,
                    clsq.IsActive == true
                    );

                var margins = new ItemProductMedicIndicationCollection();
                margins.Load(mrg);

                Session["collItemProductMedicIndicationCollection"] = margins;
                return margins;
            }
            set { Session["collItemProductMedicIndicationCollection"] = value; }
        }

        private void RefreshCommandItemGridIndication(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemIndication.Columns[0].Visible = isVisible;
            grdItemIndication.Columns[grdItemIndication.Columns.Count - 1].Visible = isVisible;

            grdItemIndication.MasterTableView.CommandItemDisplay = isVisible
                                                                       ? GridCommandItemDisplay.Top
                                                                       : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                ItemProductMedicIndications = null;

            //Perbaharui tampilan dan data
            grdItemIndication.Rebind();
        }

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
        #endregion

        #region Item Supplier
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

                SupplierQuery supQ = new SupplierQuery("b");
                query.InnerJoin(supQ).On(query.SupplierID == supQ.SupplierID);

                ItemProductMedicQuery prodmedQ = new ItemProductMedicQuery("p");
                query.InnerJoin(prodmedQ).On(query.ItemID == prodmedQ.ItemID);

                query.Where(query.ItemID == txtItemID.Text);

                query.Select
                    (
                        query.SupplierID,
                        query.ItemID,
                        supQ.SupplierName.As("refToSupplier_SupplierName"),
                        query.PurchaseDiscount1,
                        query.PurchaseDiscount2,
                        //prodmedQ.SRPurchaseUnit,
                        query.SRPurchaseUnit,
                        query.PriceInPurchaseUnit,
                        query.IsActive,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID,
                        query.DrugDistributionLicenseNo,
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
                entity.DrugDistributionLicenseNo = userControl.DrugDistributionLicenseNo;
            }
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
                var qrItemMed = new ItemProductMedicQuery("b");
                var qrRef = new AppStandardReferenceItemQuery("c");
                var qrLoc = new LocationQuery("d");
                var qrItemBin = new AppStandardReferenceItemQuery("e");

                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID);
                query.LeftJoin(qrRef).On(qrItemMed.SRItemUnit == qrRef.ItemID & qrRef.StandardReferenceID == "ItemUnit");
                query.InnerJoin(qrLoc).On(query.LocationID == qrLoc.LocationID);
                query.LeftJoin(qrItemBin).On(query.SRItemBin == qrItemBin.ItemID & qrItemBin.StandardReferenceID == "ItemBin");

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
            ItemProductMedicalBalanceDetail userControl =
                (ItemProductMedicalBalanceDetail)e.Item.
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
            var userControl = (ItemProductMedicalBalanceDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
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
            grdLocation.Columns.FindByUniqueName("editED").Visible = AppSession.Parameter.IsEnabledStockWithEdControl && chkIsControlExpired.Checked && !isVisible;

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

        #region Record Detail Method Function ItemProductConsumeUnitMatrix

        private void RefreshCommandItemProductConsumeUnitMatrix(AppEnum.DataMode newVal)
        {
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdConsumeUnitMatrix.Columns[0].Visible = isVisible;
            grdConsumeUnitMatrix.Columns[grdConsumeUnitMatrix.Columns.Count - 1].Visible = isVisible;

            grdConsumeUnitMatrix.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            grdConsumeUnitMatrix.Rebind();
        }

        private ItemProductConsumeUnitMatrixCollection ItemProductConsumeUnitMatrixs
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemProductConsumeUnitMatrix"];
                    if (obj != null)
                    {
                        return ((ItemProductConsumeUnitMatrixCollection)(obj));
                    }
                }

                var coll = new ItemProductConsumeUnitMatrixCollection();
                var query = new ItemProductConsumeUnitMatrixQuery("a");
                var consumeUnit = new AppStandardReferenceItemQuery("cu");
                query.InnerJoin(consumeUnit).On(
                    query.SRConsumeUnit == consumeUnit.ItemID &&
                    consumeUnit.StandardReferenceID == AppEnum.StandardReference.DosageUnit.ToString()
                    );

                var itemUnit = new AppStandardReferenceItemQuery("iu");
                query.InnerJoin(itemUnit).On(
                    query.SRItemUnit == itemUnit.ItemID &&
                    itemUnit.StandardReferenceID == AppEnum.StandardReference.ItemUnit.ToString()
                    );

                query.Where(query.ItemID == txtItemID.Text);
                query.Select(
                    query,
                    itemUnit.ItemName.As("refToAppStandardReferenceItem_ItemUnitName"),
                    consumeUnit.ItemName.As("refToAppStandardReferenceItem_ConsumeUnitName")
                    );

                coll.Load(query);

                Session["collItemProductConsumeUnitMatrix"] = coll;
                return coll;
            }
            set { Session["collItemProductConsumeUnitMatrix"] = value; }
        }

        private void PopulateItemProductConsumeUnitMatrixGrid()
        {
            ItemProductConsumeUnitMatrixs = null;
            grdConsumeUnitMatrix.DataSource = ItemProductConsumeUnitMatrixs;
            grdConsumeUnitMatrix.MasterTableView.IsItemInserted = false;
            grdConsumeUnitMatrix.MasterTableView.ClearEditItems();
            grdConsumeUnitMatrix.DataBind();
        }

        protected void grdConsumeUnitMatrix_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdConsumeUnitMatrix.DataSource = ItemProductConsumeUnitMatrixs;
        }

        protected void grdConsumeUnitMatrix_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            var srItemUnit = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemProductConsumeUnitMatrixMetadata.ColumnNames.SRItemUnit]);
            var srConsumeUnit = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemProductConsumeUnitMatrixMetadata.ColumnNames.SRConsumeUnit]);
            var entity = FindConsumeUnitMatrix(srItemUnit, srConsumeUnit);
            if (entity != null)
                SetEntityValueItemMedic(entity, e);
        }

        protected void grdConsumeUnitMatrix_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var srItemUnit = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemProductConsumeUnitMatrixMetadata.ColumnNames.SRItemUnit]);
            var srConsumeUnit = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemProductConsumeUnitMatrixMetadata.ColumnNames.SRConsumeUnit]);
            var entity = FindConsumeUnitMatrix(srItemUnit, srConsumeUnit);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdConsumeUnitMatrix_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ItemProductConsumeUnitMatrixs.AddNew();
            SetEntityValueItemMedic(entity, e);

            e.Canceled = true;
            grdConsumeUnitMatrix.Rebind();
        }

        private BusinessObject.ItemProductConsumeUnitMatrix FindConsumeUnitMatrix(String srItemUnit, string srConsumeUnit)
        {
            var coll = ItemProductConsumeUnitMatrixs;
            return coll.FirstOrDefault(rec => rec.SRItemUnit.Equals(srItemUnit) && rec.SRConsumeUnit.Equals(srConsumeUnit));
        }

        private void SetEntityValueItemMedic(BusinessObject.ItemProductConsumeUnitMatrix entity, GridCommandEventArgs e)
        {
            var userControl = (ItemProductConsumeUnitMatrixCtl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = txtItemID.Text;
                entity.SRItemUnit = cboSRItemUnit.SelectedValue;
                entity.ItemUnitName = cboSRItemUnit.Text;
                entity.SRConsumeUnit = userControl.SRConsumeUnit;
                entity.ConsumeUnitName = userControl.SRConsumeUnitName;
                entity.ConversionFactor = userControl.ConversionFactor;
            }
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

        #region Record Detail Method Function ItemBridging

        private ItemBridgingCollection ItemBridgings
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemBridging"];
                    if (obj != null) return ((ItemBridgingCollection)(obj));
                }

                ItemBridgingCollection coll = new ItemBridgingCollection();

                ItemBridgingQuery query = new ItemBridgingQuery("a");
                AppStandardReferenceItemQuery asri = new AppStandardReferenceItemQuery("b");

                query.Select(query, asri.ItemName.As("refToAppStandardReferenceItem_ItemName"));
                query.InnerJoin(asri).On(query.SRBridgingType == asri.ItemID && asri.StandardReferenceID == AppEnum.StandardReference.BridgingType.ToString());
                query.Where(query.ItemID == txtItemID.Text);
                coll.Load(query);

                Session["collItemBridging"] = coll;
                return coll;
            }
            set
            {
                Session["collParamedicBridging"] = value;
            }
        }

        private void RefreshCommandItemItemBridging(AppEnum.DataMode newVal)
        {
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdAliasName.Columns[0].Visible = isVisible;
            grdAliasName.Columns[grdAliasName.Columns.Count - 1].Visible = isVisible;

            grdAliasName.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            grdAliasName.Rebind();
        }

        private void PopulateItemBirdgingGrid()
        {
            ItemBridgings = null;
            grdAliasName.DataSource = ItemBridgings;
            grdAliasName.MasterTableView.IsItemInserted = false;
            grdAliasName.MasterTableView.ClearEditItems();
            grdAliasName.DataBind();
        }

        protected void grdAliasName_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdAliasName.DataSource = ItemBridgings;
        }

        protected void grdAliasName_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String type = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemBridgingMetadata.ColumnNames.SRBridgingType]);
            String id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemBridgingMetadata.ColumnNames.BridgingID]);

            var entity = FindItemBridging(type, id);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdAliasName_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String type = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemBridgingMetadata.ColumnNames.SRBridgingType]);
            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemBridgingMetadata.ColumnNames.BridgingID]);

            var entity = FindItemBridging(type, id);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdAliasName_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ItemBridgings.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdAliasName.Rebind();
        }

        private ItemBridging FindItemBridging(String type, string id)
        {
            var coll = ItemBridgings;
            return coll.FirstOrDefault(rec => rec.SRBridgingType.Equals(type) && rec.BridgingID.Equals(id));
        }

        private void SetEntityValue(ItemBridging entity, GridCommandEventArgs e)
        {
            ItemAliasDetail userControl = (ItemAliasDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = txtItemID.Text;
                entity.SRBridgingType = userControl.BridgingType;
                entity.BridgingTypeName = userControl.BridgingTypeName;
                entity.BridgingID = userControl.BridgingID;
                entity.BridgingName = string.IsNullOrEmpty(userControl.BridgingName) ? txtItemName.Text : userControl.BridgingName;
                entity.ItemIdExternal = userControl.ItemIdExternal;
                entity.IsActive = userControl.IsActive;
                entity.BridgingGroupID = userControl.BridgingGroupID;
                entity.BridgingGroupName = userControl.BridgingGroupName;
            }
        }

        #endregion

    }
}
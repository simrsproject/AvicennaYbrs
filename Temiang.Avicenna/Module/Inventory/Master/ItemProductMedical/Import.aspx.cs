using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using System.Data;
using System.IO;
using System.Configuration;
using Telerik.Web.UI;
using System.Globalization;

namespace Temiang.Avicenna.Module.Inventory.Master.ItemProductMedical
{
    public partial class Import : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProgramID = Request.QueryString["id"];

            if (!IsPostBack)
            {
            }
        }

        public override bool OnButtonOkClicked()
        {
            HideInformationHeader();

            if (!fileuploadExcel.HasFile)
            {
                ShowInformationHeader("There is no file to upload.");
                return false;
            }
            //if (ConfigurationManager.AppSettings["DocumentFolder"] == null)
            //{
            //    ShowInformationHeader("Temporary document folder is not configured.");
            //    return false;
            //}

            //if (!Directory.Exists(ConfigurationManager.AppSettings["DocumentFolder"])) Directory.CreateDirectory(ConfigurationManager.AppSettings["DocumentFolder"]);
            //string path = ConfigurationManager.AppSettings["DocumentFolder"] + fileuploadExcel.PostedFile.FileName;

            string tmp_doc = AppParameter.GetParameterValue(AppParameter.ParameterItem.TmpDocumentFolder);
            if (string.IsNullOrEmpty(tmp_doc))
                tmp_doc = ConfigurationManager.AppSettings["DocumentFolder"];

            if (string.IsNullOrEmpty(tmp_doc))
            {
                ShowInformationHeader("Temporary document folder is not configured.");
                return false;
            }

            if (!Directory.Exists(tmp_doc))
                Directory.CreateDirectory(tmp_doc);
            string path = tmp_doc + fileuploadExcel.PostedFile.FileName;


            fileuploadExcel.SaveAs(path);

            try
            {
                DataTable table = Common.CreateExcelFile.LoadExcelFileToDataTable(path);
                if (table.Rows.Count > 0)
                {
                    if (ProgramID == AppConstant.Program.ItemProductMedical)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            if (!AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdProductAutomatic) && (string.IsNullOrEmpty(row["ItemID"].ToString()))) continue;
                            if (string.IsNullOrEmpty(row["ItemName"].ToString())) continue;
                            if (string.IsNullOrEmpty(row["ItemGroupID"].ToString())) continue;
                            if (string.IsNullOrEmpty(row["ItemUnitID"].ToString())) continue;
                            if (string.IsNullOrEmpty(row["PurchaseUnitID"].ToString())) continue;
                            if (Convert.ToDecimal(row["ConversionFactor"]) == 0) continue;

                            var itemid = string.IsNullOrEmpty(row["ItemID"].ToString()) ? string.Empty : row["ItemID"].ToString();
                            var itemName = row["ItemName"].ToString();
                            var itemGroupId = row["ItemGroupID"].ToString();

                            var ig = new ItemGroup();
                            if (!ig.LoadByPrimaryKey(itemGroupId)) continue;
                            if (ig.SRItemType != BusinessObject.Reference.ItemType.Medical) continue;

                            var itemUnitId = row["ItemUnitID"].ToString();
                            var purchaseUnitId = row["PurchaseUnitID"].ToString();
                            var conversionFactor = Convert.ToDecimal(row["ConversionFactor"]);
                            if (itemUnitId != purchaseUnitId && conversionFactor == 1) continue;

                            var std = new AppStandardReferenceItem();
                            if (!std.LoadByPrimaryKey("ItemUnit", itemUnitId)) continue;

                            std = new AppStandardReferenceItem();
                            if (!std.LoadByPrimaryKey("ItemUnit", purchaseUnitId)) continue;

                            var productTypeId = string.IsNullOrEmpty(row["ProductTypeID"].ToString()) ? string.Empty : row["ProductTypeID"].ToString();
                            std = new AppStandardReferenceItem();
                            if (!std.LoadByPrimaryKey("ProductType", productTypeId))
                                productTypeId = string.Empty;

                            var drugLabelId = string.IsNullOrEmpty(row["DrugLabelID"].ToString()) ? string.Empty : row["DrugLabelID"].ToString();
                            std = new AppStandardReferenceItem();
                            if (!std.LoadByPrimaryKey("DrugLabelType", drugLabelId))
                                drugLabelId = string.Empty;

                            var abcClass = string.IsNullOrEmpty(row["ABC_Class"].ToString()) ? "A" : row["ABC_Class"].ToString().ToUpper();
                            if (abcClass != "A" || abcClass != "B" || abcClass != "C")
                                abcClass = "A";

                            var groupTherapyId = string.IsNullOrEmpty(row["GroupTherapyID"].ToString()) ? string.Empty : row["GroupTherapyID"].ToString();
                            var therapyId = string.IsNullOrEmpty(row["TherapyID"].ToString()) ? string.Empty : row["TherapyID"].ToString();
                            var therapy = new Therapy();
                            if (!therapy.LoadByPrimaryKey(therapyId))
                                therapyId = string.Empty;
                            else
                                groupTherapyId = therapy.SRTherapyGroup;

                            std = new AppStandardReferenceItem();
                            if (!std.LoadByPrimaryKey("TherapyGroup", groupTherapyId))
                                groupTherapyId = string.Empty;

                            var productAccountId = string.IsNullOrEmpty(row["ProductAccountID"].ToString()) ? string.Empty : row["ProductAccountID"].ToString();
                            var prodacc = new ProductAccount();
                            if (!prodacc.LoadByPrimaryKey(productAccountId))
                                productAccountId = string.Empty;

                            var dosageUnitId = string.IsNullOrEmpty(row["DosageUnitID"].ToString()) ? string.Empty : row["DosageUnitID"].ToString();
                            std = new AppStandardReferenceItem();
                            if (!std.LoadByPrimaryKey("DosageUnit", dosageUnitId))
                                dosageUnitId = string.Empty;

                            var dosage = dosageUnitId == string.Empty ? 0 : Convert.ToDecimal(row["Dosage"]);
                            var purchasePriceInBaseUnit = Convert.ToDecimal(row["PurchasePriceInBaseUnit"]);
                            var purchaseDiscountInPercentage = Convert.ToDecimal(row["PurchaseDiscountInPercentage"]);

                            var isInventoryItem = row["IsInventoryItem"].ToInt() == 0 ? 0 : 1;
                            var isControlExpired = row["IsControlExpired"].ToInt() == 0 ? 0 : 1;
                            var isProductionItem = row["IsProductionItem"].ToInt() == 0 ? 0 : 1;
                            var isSalesAvailable = row["IsSalesAvailable"].ToInt() == 0 ? 0 : 1;
                            var isActualDeduct = row["IsActualDeduct"].ToInt() == 0 ? 0 : 1;
                            var isConsignment = row["IsConsignment"].ToInt() == 0 ? 0 : 1;
                            var isFormulary = row["IsFormulary"].ToInt() == 0 ? 0 : 1;
                            var isNationalFormulary = row["IsNationalFormulary"].ToInt() == 0 ? 0 : 1;
                            var isGeneric = row["IsGeneric"].ToInt() == 0 ? 0 : 1;
                            var isNonGeneric = row["IsNonGeneric"].ToInt() == 0 ? 0 : 1;
                            var isNonGenericLimited = row["IsNonGenericLimited"].ToInt() == 0 ? 0 : 1;
                            var isPrecursor = row["IsPrecursor"].ToInt() == 0 ? 0 : 1;
                            var isOtc = row["IsOTC"].ToInt() == 0 ? 0 : 1;
                            var isHardDrug = row["IsHardDrug"].ToInt() == 0 ? 0 : 1;
                            var isHam = row["IsHAM"].ToInt() == 0 ? 0 : 1;
                            var isOot = row["IsOOT"].ToInt() == 0 ? 0 : 1;
                            var isNarcotic = row["IsNarcotic"].ToInt() == 0 ? 0 : 1;
                            var isPsychotropic = row["IsPsychotropic"].ToInt() == 0 ? 0 : 1;
                            var isMorphine = row["IsMorphine"].ToInt() == 0 ? 0 : 1;
                            var isPethidine = row["IsPethidine"].ToInt() == 0 ? 0 : 1;
                            var isLasa = row["IsLASA"].ToInt() == 0 ? 0 : 1;
                            var isTraditionalMedicine = row["IsTraditionalMedicine"].ToInt() == 0 ? 0 : 1;
                            var isSupplement = row["IsSupplement"].ToInt() == 0 ? 0 : 1;
                            var isAntibiotic = row["IsAntibiotic"].ToInt() == 0 ? 0 : 1;
                            var isMedication = row["IsMedication"].ToInt() == 0 ? 0 : 1;
                            var isActive = row["IsActive"].ToInt() == 0 ? 0 : 1;

                            if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdProductAutomatic))
                            {
                                if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdProductAutomaticUseGroupInitial))
                                    itemid = Helper.GetItemProductIDUseGroupInitial(itemGroupId);
                                else
                                    itemid = Helper.GetItemProductID(itemName.ToUpper(), BusinessObject.Reference.ItemType.Medical);
                            }

                            var entity = new Item();
                            entity.AddNew();

                            entity.ItemID = itemid;
                            entity.ItemGroupID = itemGroupId;
                            entity.SRItemType = BusinessObject.Reference.ItemType.Medical;
                            entity.SRBillingGroup = string.Empty;
                            entity.SRBpjsItemGroup = string.Empty;
                            entity.ProductAccountID = productAccountId;
                            entity.ItemName = AppSession.Parameter.IsItemInventoryNameUsingUpperCase ? itemName.ToUpper() : itemName;
                            entity.IsActive = isActive == 1;
                            entity.ItemIDExternal = string.Empty;
                            entity.Notes = string.Empty;
                            entity.IsItemProduction = isProductionItem == 1;
                            entity.IsNeedToBeSterilized = false;
                            entity.Barcode = string.Empty;
                            entity.SREklaimTariffGroup = string.Empty;
                            entity.Photo = null;
                            entity.SRItemCategory = string.Empty;
                            entity.IsAsset = false;
                            entity.AssetGroupID = string.Empty;

                            entity.IsNewUpload = true;

                            entity.CreatedByUserID = AppSession.UserLogin.UserID;
                            entity.CreatedDateTime = DateTime.Now;
                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            entity.LastUpdateDateTime = DateTime.Now;

                            var detail = new ItemProductMedic();
                            detail.AddNew();

                            detail.ItemID = entity.ItemID;
                            detail.MarginID = string.Empty;
                            detail.SRProductType = productTypeId;
                            detail.ABCClass = abcClass;
                            detail.BrandName = string.Empty;
                            detail.SRItemUnit = itemUnitId;
                            detail.SRPurchaseUnit = purchaseUnitId;
                            detail.ConversionFactor = conversionFactor;
                            detail.Dosage = dosage;
                            detail.SRDosageUnit = dosageUnitId;
                            detail.IsFormularium = isFormulary == 1;
                            detail.IsInventoryItem = isInventoryItem == 1;
                            detail.IsUsingCigna = false;
                            detail.IsControlExpired = isControlExpired == 1;
                            detail.FabricID = string.Empty;
                            detail.SalesFixedPrice = 0;
                            detail.MarginPercentage = 0;
                            detail.SalesDiscount = 0;
                            detail.PurchaseDiscount1 = purchaseDiscountInPercentage;
                            detail.PurchaseDiscount2 = 0;
                            detail.SafetyStock = 0;
                            detail.SafetyTime = 0;
                            detail.LeadTime = 0;
                            detail.TolerancePercentage = 0;
                            detail.Barcode = string.Empty;
                            detail.SRDrugLabelType = drugLabelId;
                            detail.SRRoute = string.Empty;
                            detail.SRItemBin = string.Empty;
                            detail.IsConsignment = isConsignment == 1;
                            detail.SRTherapyGroup = groupTherapyId;
                            detail.TherapyID = therapyId;
                            detail.IsActualDeduct = isActualDeduct == 1;
                            detail.PremiPharmaciesPercentage = 0;
                            detail.PremiPhysicianPercentage = 0;
                            detail.HET = 0;
                            detail.SRConsumeMethod = string.Empty;
                            detail.ConsumptionLimitInDay = 0;

                            detail.PriceInPurchaseUnit = purchasePriceInBaseUnit * conversionFactor;
                            detail.PriceInBaseUnit = purchasePriceInBaseUnit;
                            detail.PriceInBasedUnitWVat = purchasePriceInBaseUnit + (purchasePriceInBaseUnit * Convert.ToDecimal(AppSession.Parameter.Ppn / 100.00));
                            detail.HighestPriceInBasedUnit = purchasePriceInBaseUnit;
                            detail.LastPriceInBaseUnit = purchasePriceInBaseUnit;
                            detail.CostPrice = purchasePriceInBaseUnit;

                            detail.IsPrecursor = isPrecursor == 1;
                            detail.IsNarcotic = isNarcotic == 1;
                            detail.IsPsychotropic = isPsychotropic == 1;
                            detail.IsMorphine = isMorphine == 1;
                            detail.IsGeneric = isGeneric == 1;
                            detail.IsNonGeneric = isNonGeneric == 1;
                            detail.IsAntibiotic = isAntibiotic == 1;
                            detail.IsRegularItem = true;
                            detail.IsSalesAvailable = isSalesAvailable == 1;
                            detail.IsDirectPurchase = false;
                            detail.SRKeeping = string.Empty;
                            detail.VENClass = string.Empty;
                            detail.IsHam = isHam == 1;
                            detail.IsLasa = isLasa == 1;
                            detail.IsOot = isOot == 1;
                            detail.IsSharePurchaseDiscToPatient = false;
                            detail.IsFornas = isNationalFormulary == 1;
                            detail.IsOtc = isOtc == 1;
                            detail.IsHardDrug = isHardDrug == 1;
                            detail.IsTraditionalMedicine = isTraditionalMedicine == 1;
                            detail.IsSupplement = isSupplement == 1;
                            detail.IsMedication = isMedication == 1;
                            detail.IsNoPrescriptionFee = false;
                            detail.IsPethidine = isPethidine == 1;
                            detail.SRAntibioticLine = string.Empty;
                            detail.IsNonGenericLimited = isNonGenericLimited == 1;
                            detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            detail.LastUpdateDateTime = DateTime.Now;

                            using (esTransactionScope trans = new esTransactionScope())
                            {
                                entity.Save();
                                detail.Save();

                                //Commit if success, Rollback if failed
                                trans.Complete();
                            }
                        }
                    }
                    else if (ProgramID == AppConstant.Program.ItemProductNonMedical)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            if (!AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdProductAutomatic) && (string.IsNullOrEmpty(row["ItemID"].ToString()))) continue;
                            if (string.IsNullOrEmpty(row["ItemName"].ToString())) continue;
                            if (string.IsNullOrEmpty(row["ItemGroupID"].ToString())) continue;
                            if (string.IsNullOrEmpty(row["ItemUnitID"].ToString())) continue;
                            if (string.IsNullOrEmpty(row["PurchaseUnitID"].ToString())) continue;
                            if (Convert.ToDecimal(row["ConversionFactor"]) == 0) continue;

                            var itemid = string.IsNullOrEmpty(row["ItemID"].ToString()) ? string.Empty : row["ItemID"].ToString();
                            var itemName = row["ItemName"].ToString();
                            var itemGroupId = row["ItemGroupID"].ToString();

                            var ig = new ItemGroup();
                            if (!ig.LoadByPrimaryKey(itemGroupId)) continue;
                            if (ig.SRItemType != BusinessObject.Reference.ItemType.NonMedical) continue;

                            var itemUnitId = row["ItemUnitID"].ToString();
                            var purchaseUnitId = row["PurchaseUnitID"].ToString();
                            var conversionFactor = Convert.ToDecimal(row["ConversionFactor"]);
                            if (itemUnitId != purchaseUnitId && conversionFactor == 1) continue;

                            var std = new AppStandardReferenceItem();
                            if (!std.LoadByPrimaryKey("ItemUnit", itemUnitId)) continue;

                            std = new AppStandardReferenceItem();
                            if (!std.LoadByPrimaryKey("ItemUnit", purchaseUnitId)) continue;

                            var abcClass = string.IsNullOrEmpty(row["ABC_Class"].ToString()) ? "A" : row["ABC_Class"].ToString().ToUpper();
                            if (abcClass != "A" || abcClass != "B" || abcClass != "C")
                                abcClass = "A";

                            var productAccountId = string.IsNullOrEmpty(row["ProductAccountID"].ToString()) ? string.Empty : row["ProductAccountID"].ToString();
                            var prodacc = new ProductAccount();
                            if (!prodacc.LoadByPrimaryKey(productAccountId))
                                productAccountId = string.Empty;

                            var purchasePriceInBaseUnit = Convert.ToDecimal(row["PurchasePriceInBaseUnit"]);
                            var purchaseDiscountInPercentage = Convert.ToDecimal(row["PurchaseDiscountInPercentage"]);

                            var isInventoryItem = row["IsInventoryItem"].ToInt() == 0 ? 0 : 1;
                            var isControlExpired = row["IsControlExpired"].ToInt() == 0 ? 0 : 1;
                            var isProductionItem = row["IsProductionItem"].ToInt() == 0 ? 0 : 1;
                            var isSalesAvailable = row["IsSalesAvailable"].ToInt() == 0 ? 0 : 1;
                            var isConsignment = row["IsConsignment"].ToInt() == 0 ? 0 : 1;
                            var isActive = row["IsActive"].ToInt() == 0 ? 0 : 1;

                            if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdProductAutomatic))
                            {
                                if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdProductAutomaticUseGroupInitial))
                                    itemid = Helper.GetItemProductIDUseGroupInitial(itemGroupId);
                                else
                                    itemid = Helper.GetItemProductID(itemName.ToUpper(), BusinessObject.Reference.ItemType.NonMedical);
                            }

                            var entity = new Item();
                            entity.AddNew();

                            entity.ItemID = itemid;
                            entity.ItemGroupID = itemGroupId;
                            entity.SRItemType = BusinessObject.Reference.ItemType.NonMedical;
                            entity.SRBillingGroup = string.Empty;
                            entity.SRBpjsItemGroup = string.Empty;
                            entity.ProductAccountID = productAccountId;
                            entity.ItemName = AppSession.Parameter.IsItemInventoryNameUsingUpperCase ? itemName.ToUpper() : itemName;
                            entity.IsActive = isActive == 1;
                            entity.ItemIDExternal = string.Empty;
                            entity.Notes = string.Empty;
                            entity.IsItemProduction = isProductionItem == 1;
                            entity.IsNeedToBeSterilized = false;
                            entity.Barcode = string.Empty;
                            entity.SREklaimTariffGroup = string.Empty;
                            entity.Photo = null;
                            entity.SRItemCategory = string.Empty;
                            entity.IsAsset = false;
                            entity.AssetGroupID = string.Empty;

                            entity.IsNewUpload = true;

                            entity.CreatedByUserID = AppSession.UserLogin.UserID;
                            entity.CreatedDateTime = DateTime.Now;
                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            entity.LastUpdateDateTime = DateTime.Now;

                            var detail = new ItemProductNonMedic();
                            detail.AddNew();

                            detail.ItemID = entity.ItemID;
                            detail.MarginID = string.Empty;
                            detail.SRProductType = string.Empty;
                            detail.ABCClass = abcClass;
                            detail.BrandName = string.Empty;
                            detail.SRItemUnit = itemUnitId;
                            detail.SRPurchaseUnit = purchaseUnitId;
                            detail.ConversionFactor = conversionFactor;
                            detail.Dosage = 0;
                            detail.SRDosageUnit = string.Empty;
                            detail.IsFormularium = false;
                            detail.IsInventoryItem = isInventoryItem == 1;
                            detail.IsControlExpired = isControlExpired == 1;
                            detail.FabricID = string.Empty;
                            detail.SalesFixedPrice = 0;
                            detail.MarginPercentage = 0;
                            detail.SalesDiscount = 0;
                            detail.PurchaseDiscount1 = purchaseDiscountInPercentage;
                            detail.PurchaseDiscount2 = 0;
                            detail.SafetyStock = 0;
                            detail.SafetyTime = 0;
                            detail.LeadTime = 0;
                            detail.TolerancePercentage = 0;
                            detail.Barcode = string.Empty;
                            detail.SRItemBin = string.Empty;
                            detail.IsConsignment = isConsignment == 1;
                            
                            detail.PriceInPurchaseUnit = purchasePriceInBaseUnit * conversionFactor;
                            detail.PriceInBaseUnit = purchasePriceInBaseUnit;
                            detail.PriceInBasedUnitWVat = purchasePriceInBaseUnit + (purchasePriceInBaseUnit * Convert.ToDecimal(AppSession.Parameter.Ppn / 100.00));
                            detail.HighestPriceInBasedUnit = purchasePriceInBaseUnit;
                            detail.LastPriceInBaseUnit = purchasePriceInBaseUnit;
                            detail.CostPrice = purchasePriceInBaseUnit;

                            detail.IsSalesAvailable = isSalesAvailable == 1;
                            
                            detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            detail.LastUpdateDateTime = DateTime.Now;

                            using (esTransactionScope trans = new esTransactionScope())
                            {
                                entity.Save();
                                detail.Save();

                                //Commit if success, Rollback if failed
                                trans.Complete();
                            }
                        }
                    }
                    else if (ProgramID == AppConstant.Program.ItemKitchen)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            if (!AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdProductAutomatic) && (string.IsNullOrEmpty(row["ItemID"].ToString()))) continue;
                            if (string.IsNullOrEmpty(row["ItemName"].ToString())) continue;
                            if (string.IsNullOrEmpty(row["ItemGroupID"].ToString())) continue;
                            if (string.IsNullOrEmpty(row["ItemUnitID"].ToString())) continue;
                            if (string.IsNullOrEmpty(row["PurchaseUnitID"].ToString())) continue;
                            if (Convert.ToDecimal(row["ConversionFactor"]) == 0) continue;

                            var itemid = string.IsNullOrEmpty(row["ItemID"].ToString()) ? string.Empty : row["ItemID"].ToString();
                            var itemName = row["ItemName"].ToString();
                            var itemGroupId = row["ItemGroupID"].ToString();

                            var ig = new ItemGroup();
                            if (!ig.LoadByPrimaryKey(itemGroupId)) continue;
                            if (ig.SRItemType != BusinessObject.Reference.ItemType.Kitchen) continue;

                            var itemUnitId = row["ItemUnitID"].ToString();
                            var purchaseUnitId = row["PurchaseUnitID"].ToString();
                            var conversionFactor = Convert.ToDecimal(row["ConversionFactor"]);
                            if (itemUnitId != purchaseUnitId && conversionFactor == 1) continue;

                            var std = new AppStandardReferenceItem();
                            if (!std.LoadByPrimaryKey("ItemUnit", itemUnitId)) continue;

                            std = new AppStandardReferenceItem();
                            if (!std.LoadByPrimaryKey("ItemUnit", purchaseUnitId)) continue;

                            var abcClass = string.IsNullOrEmpty(row["ABC_Class"].ToString()) ? "A" : row["ABC_Class"].ToString().ToUpper();
                            if (abcClass != "A" || abcClass != "B" || abcClass != "C")
                                abcClass = "A";

                            var productAccountId = string.IsNullOrEmpty(row["ProductAccountID"].ToString()) ? string.Empty : row["ProductAccountID"].ToString();
                            var prodacc = new ProductAccount();
                            if (!prodacc.LoadByPrimaryKey(productAccountId))
                                productAccountId = string.Empty;

                            var purchasePriceInBaseUnit = Convert.ToDecimal(row["PurchasePriceInBaseUnit"]);
                            var purchaseDiscountInPercentage = Convert.ToDecimal(row["PurchaseDiscountInPercentage"]);

                            var isInventoryItem = row["IsInventoryItem"].ToInt() == 0 ? 0 : 1;
                            var isControlExpired = row["IsControlExpired"].ToInt() == 0 ? 0 : 1;
                            var isProductionItem = row["IsProductionItem"].ToInt() == 0 ? 0 : 1;
                            var isSalesAvailable = row["IsSalesAvailable"].ToInt() == 0 ? 0 : 1;
                            var isActive = row["IsActive"].ToInt() == 0 ? 0 : 1;

                            if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdProductAutomatic))
                            {
                                if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdProductAutomaticUseGroupInitial))
                                    itemid = Helper.GetItemProductIDUseGroupInitial(itemGroupId);
                                else
                                    itemid = Helper.GetItemProductID(itemName.ToUpper(), BusinessObject.Reference.ItemType.Kitchen);
                            }

                            var entity = new Item();
                            entity.AddNew();

                            entity.ItemID = itemid;
                            entity.ItemGroupID = itemGroupId;
                            entity.SRItemType = BusinessObject.Reference.ItemType.Kitchen;
                            entity.SRBillingGroup = string.Empty;
                            entity.SRBpjsItemGroup = string.Empty;
                            entity.ProductAccountID = productAccountId;
                            entity.ItemName = AppSession.Parameter.IsItemInventoryNameUsingUpperCase ? itemName.ToUpper() : itemName;
                            entity.IsActive = isActive == 1;
                            entity.ItemIDExternal = string.Empty;
                            entity.Notes = string.Empty;
                            entity.IsItemProduction = isProductionItem == 1;
                            entity.IsNeedToBeSterilized = false;
                            entity.Barcode = string.Empty;
                            entity.SREklaimTariffGroup = string.Empty;
                            entity.Photo = null;
                            entity.SRItemCategory = string.Empty;
                            entity.IsAsset = false;
                            entity.AssetGroupID = string.Empty;

                            entity.IsNewUpload = true;

                            entity.CreatedByUserID = AppSession.UserLogin.UserID;
                            entity.CreatedDateTime = DateTime.Now;
                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            entity.LastUpdateDateTime = DateTime.Now;

                            var detail = new ItemKitchen();
                            detail.AddNew();

                            detail.ItemID = entity.ItemID;
                            detail.MarginID = string.Empty;
                            detail.ABCClass = abcClass;
                            detail.BrandName = string.Empty;
                            detail.SRItemUnit = itemUnitId;
                            detail.SRPurchaseUnit = purchaseUnitId;
                            detail.ConversionFactor = conversionFactor;
                            detail.IsInventoryItem = isInventoryItem == 1;
                            detail.IsControlExpired = isControlExpired == 1;
                            detail.SalesFixedPrice = 0;
                            detail.MarginPercentage = 0;
                            detail.PurchaseDiscount1 = purchaseDiscountInPercentage;
                            detail.PurchaseDiscount2 = 0;
                            detail.SafetyStock = 0;
                            detail.SafetyTime = 0;
                            detail.LeadTime = 0;
                            detail.TolerancePercentage = 0;
                            detail.Barcode = string.Empty;
                            
                            detail.PriceInPurchaseUnit = purchasePriceInBaseUnit * conversionFactor;
                            detail.PriceInBaseUnit = purchasePriceInBaseUnit;
                            detail.PriceInBasedUnitWVat = purchasePriceInBaseUnit + (purchasePriceInBaseUnit * Convert.ToDecimal(AppSession.Parameter.Ppn / 100.00));
                            detail.HighestPriceInBasedUnit = purchasePriceInBaseUnit;
                            detail.LastPriceInBaseUnit = purchasePriceInBaseUnit;
                            detail.CostPrice = purchasePriceInBaseUnit;

                            detail.IsSalesAvailable = isSalesAvailable == 1;

                            detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            detail.LastUpdateDateTime = DateTime.Now;

                            using (esTransactionScope trans = new esTransactionScope())
                            {
                                entity.Save();
                                detail.Save();

                                //Commit if success, Rollback if failed
                                trans.Complete();
                            }
                        }
                    }
                }
                File.Delete(path);
            }
            catch (Exception ex)
            {
                File.Delete(path);

                ShowInformationHeader(ex.Message);
                return false;

                //Logger.LogException(ex, Request.UserHostName, AppSession.UserLogin.UserName);
                //if (Page.IsCallback)
                //{
                //    string script = string.Format("document.location.href = '{0}');", "~/ErrorPage.aspx");
                //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "redirect", script, true);
                //}
                //else
                //{
                //    Response.Redirect("~/ErrorPage.aspx");
                //}
            }

            return true;
        }
    }
}
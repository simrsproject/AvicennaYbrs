using System;
using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemTariffRequest
    {
        #region Approve
        public string Approv(string tariffRequestNo, string userID)
        {
            return ApprovProcess(tariffRequestNo, userID);
        }

        private static string ApprovProcess(string tariffRequestNo, string userID)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                var entity = new ItemTariffRequest();
                if (entity.LoadByPrimaryKey(tariffRequestNo))
                {
                    entity.IsApproved = true;
                    entity.ApprovedDate = DateTime.Now.Date;
                    entity.LastUpdateByUserID = userID;
                    entity.LastUpdateDateTime = DateTime.Now;

                    var collItem = new ItemTariffRequestItemCollection();
                    collItem.Query.Where(collItem.Query.TariffRequestNo == tariffRequestNo);
                    collItem.LoadAll();

                    var collLog = new ItemProductLogCollection();
                    
                    foreach (ItemTariffRequestItem item in collItem)
                    {
                        var isNotNew = false;

                        var tariffUpdateHistory = new ItemTariffUpdateHistory();
                        var tariff = new ItemTariff();
                        if (tariff.LoadByPrimaryKey(entity.SRTariffType, item.ItemID, entity.ClassID,
                                                entity.StartingDate.Value.Date))
                        {
                            isNotNew = true;
                            
                            tariffUpdateHistory.AddNew();
                            tariffUpdateHistory.RequestNo = tariffRequestNo;
                            tariffUpdateHistory.SRTariffType = entity.SRTariffType;
                            tariffUpdateHistory.ItemID = item.ItemID.Trim();
                            tariffUpdateHistory.ClassID = entity.ClassID;
                            tariffUpdateHistory.StartingDate = entity.StartingDate;
                            tariffUpdateHistory.Price = tariff.Price;
                            tariffUpdateHistory.ToPrice = item.Price;
                            tariffUpdateHistory.DiscPercentage = tariff.DiscPercentage;
                            tariffUpdateHistory.ToDiscPercentage = item.DiscPercentage;
                            tariffUpdateHistory.LastUpdateDateTime = DateTime.Now;
                            tariffUpdateHistory.LastUpdateByUserID = userID;
                        }
                        else
                            tariff.AddNew();    
                        
                        tariff.SRTariffType = entity.SRTariffType;
                        tariff.ItemID = item.ItemID.Trim();
                        tariff.ClassID = entity.ClassID;
                        tariff.StartingDate = entity.StartingDate;
                        tariff.Price = item.Price;
                        tariff.DiscPercentage = item.DiscPercentage;
                        tariff.Ppn = Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.Ppn));
                        tariff.ReferenceNo = tariffRequestNo;
                        tariff.ReferenceTransactionCode = TransactionCode.ItemTariffRequest;
                        tariff.IsAllowDiscount = true;
                        tariff.LastUpdateByUserID = userID;
                        tariff.LastUpdateDateTime = DateTime.Now;

                        tariff.Save();
                        
                        if (isNotNew)
                            tariffUpdateHistory.Save();

                        //ItemProductLog
                        var log = collLog.AddNew();
                        log.TariffRequestNo = entity.TariffRequestNo;
                        log.ItemID = item.ItemID.Trim();

                        if (entity.SRItemType == ItemType.Medical)
                        {
                            var med = new ItemProductMedic();
                            med.LoadByPrimaryKey(item.ItemID);
                            log.PriceInPurchaseUnitOld = med.PriceInPurchaseUnit;
                            log.PriceInBaseUnitOld = med.PriceInBaseUnit;
                            log.PriceInBaseUnitWVatOld = med.PriceInBasedUnitWVat;
                            log.CostPriceOld = med.CostPrice;
                            log.SalesDiscountOld = med.SalesDiscount;
                            log.HighestPriceInBasedUnitOld = med.HighestPriceInBasedUnit;
                        }
                        else if (entity.SRItemType == ItemType.NonMedical)
                        {
                            var nonMed = new ItemProductNonMedic();
                            nonMed.LoadByPrimaryKey(item.ItemID);
                            log.PriceInPurchaseUnitOld = nonMed.PriceInPurchaseUnit;
                            log.PriceInBaseUnitOld = nonMed.PriceInBaseUnit;
                            log.PriceInBaseUnitWVatOld = nonMed.PriceInBasedUnitWVat;
                            log.CostPriceOld = nonMed.CostPrice;
                            log.SalesDiscountOld = nonMed.SalesDiscount;
                            log.HighestPriceInBasedUnitOld = nonMed.HighestPriceInBasedUnit;
                        }
                        else if (entity.SRItemType == ItemType.Kitchen)
                        {
                            var kitchen = new ItemKitchen();
                            kitchen.LoadByPrimaryKey(item.ItemID);
                            log.PriceInPurchaseUnitOld = kitchen.PriceInPurchaseUnit;
                            log.PriceInBaseUnitOld = kitchen.PriceInBaseUnit;
                            log.PriceInBaseUnitWVatOld = kitchen.PriceInBasedUnitWVat;
                            log.CostPriceOld = kitchen.CostPrice;
                            log.HighestPriceInBasedUnitOld = kitchen.HighestPriceInBasedUnit;
                        }

                        log.PriceInPurchaseUnitNew = item.PriceInPurchaseUnit;
                        log.PriceInBaseUnitNew = item.PriceInBaseUnit;
                        log.PriceInBaseUnitWVatNew = item.PriceInBaseUnitWVat;
                        log.CostPriceNew = item.CostPrice;
                        log.SaledDiscountNew = item.DiscPercentage;
                        log.HighestPriceInBasedUnitNew = item.PriceInBaseUnit * (1 - (item.DiscPercentage / 100));

                        log.LastUpdateDateTime = DateTime.Now;
                        log.LastUpdateByUserID = userID;
                    }

                    ItemProductMedicCollection itemMedics = null;
                    ItemProductNonMedicCollection itemNonMedics = null;
                    ItemKitchenCollection itemKitchens = null;

                    itemMedics = PrepareItemProductMedics(entity, userID);
                    itemNonMedics = PrepareItemProductNonMedics(entity, userID);
                    itemKitchens = PrepareItemKitchens(entity, userID);

                    SupplierItemCollection suppItems = null;
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsRequestChangeItemProductUpdatePriceSupplierItem) == "Yes")
                        suppItems = PrepareSupplierItems(entity, userID);

                    entity.Save();
                    collLog.Save();
                    if (itemMedics != null) itemMedics.Save();
                    if (itemNonMedics != null) itemNonMedics.Save();
                    if (itemKitchens != null) itemKitchens.Save();
                    if (suppItems != null) suppItems.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();

                }
                else
                {
                    return "NotExist";
                }
            }
            return string.Empty;
        }

        private static ItemProductMedicCollection PrepareItemProductMedics(ItemTariffRequest tarifRequest, string userID)
        {
            var tariffRequestItemQ = new ItemTariffRequestItemQuery("a");
            tariffRequestItemQ.Where(tariffRequestItemQ.TariffRequestNo == tarifRequest.TariffRequestNo);
            DataTable dtbItemRequest = tariffRequestItemQ.LoadDataTable();

            tariffRequestItemQ = new ItemTariffRequestItemQuery("a");
            tariffRequestItemQ.Where(tariffRequestItemQ.TariffRequestNo == tarifRequest.TariffRequestNo);
            tariffRequestItemQ.Select(tariffRequestItemQ.ItemID);

            var medQ = new ItemProductMedicQuery("b");
            medQ.Where(medQ.ItemID.In(tariffRequestItemQ));
            medQ.SelectAll();

            var medColl = new ItemProductMedicCollection();
            medColl.Load(medQ);

            foreach (DataRow row in dtbItemRequest.Rows)
            {
                ItemProductMedic med = null;
                bool isFound = false;
                foreach (ItemProductMedic findItem in medColl)
                {
                    //Jika ItemID tidak ditemukan, maka tambah row
                    if (findItem.ItemID.Equals(row["ItemID"]))
                    {
                        isFound = true;
                        med = findItem;
                        break;
                    }
                }

                if (isFound)
                {
                    med.ItemID = row["ItemID"].ToString();
                    med.PriceInPurchaseUnit = Convert.ToDecimal(row["PriceInPurchaseUnit"]);
                    med.PriceInBaseUnit = Convert.ToDecimal(row["PriceInBaseUnit"]);
                    med.PriceInBasedUnitWVat = Convert.ToDecimal(row["PriceInBaseUnitWVat"]);
                    med.CostPrice = Convert.ToDecimal(row["CostPrice"]);
                    med.HighestPriceInBasedUnit = Convert.ToDecimal(row["PriceInBaseUnit"]) * (1 - (Convert.ToDecimal(row["DiscPercentage"]) / 100));
                    med.SalesDiscount = Convert.ToDecimal(row["DiscPercentage"]);

                    med.LastUpdateByUserID = userID;
                    med.LastUpdateDateTime = DateTime.Now;
                }
            }

            return medColl;
        }

        private static ItemProductNonMedicCollection PrepareItemProductNonMedics(ItemTariffRequest tarifRequest, string userID)
        {
            var tariffRequestItemQ = new ItemTariffRequestItemQuery("a");
            tariffRequestItemQ.Where(tariffRequestItemQ.TariffRequestNo == tarifRequest.TariffRequestNo);
            DataTable dtbItemRequest = tariffRequestItemQ.LoadDataTable();

            tariffRequestItemQ = new ItemTariffRequestItemQuery("a");
            tariffRequestItemQ.Where(tariffRequestItemQ.TariffRequestNo == tarifRequest.TariffRequestNo);
            tariffRequestItemQ.Select(tariffRequestItemQ.ItemID);

            var nonMedQ = new ItemProductNonMedicQuery("b");
            nonMedQ.Where(nonMedQ.ItemID.In(tariffRequestItemQ));
            nonMedQ.SelectAll();

            var nonMedColl = new ItemProductNonMedicCollection();
            nonMedColl.Load(nonMedQ);

            foreach (DataRow row in dtbItemRequest.Rows)
            {
                ItemProductNonMedic nonMed = null;
                bool isFound = false;
                foreach (ItemProductNonMedic findItem in nonMedColl)
                {
                    //Jika ItemID tidak ditemukan, maka tambah row
                    if (findItem.ItemID.Equals(row["ItemID"]))
                    {
                        isFound = true;
                        nonMed = findItem;
                        break;
                    }
                }

                if (isFound)
                {
                    nonMed.ItemID = row["ItemID"].ToString();
                    nonMed.PriceInPurchaseUnit = Convert.ToDecimal(row["PriceInPurchaseUnit"]);
                    nonMed.PriceInBaseUnit = Convert.ToDecimal(row["PriceInBaseUnit"]);
                    nonMed.PriceInBasedUnitWVat = Convert.ToDecimal(row["PriceInBaseUnitWVat"]);
                    nonMed.CostPrice = Convert.ToDecimal(row["CostPrice"]);
                    nonMed.HighestPriceInBasedUnit = Convert.ToDecimal(row["PriceInBaseUnit"]) * (1 - (Convert.ToDecimal(row["DiscPercentage"]) / 100));
                    nonMed.SalesDiscount = Convert.ToDecimal(row["DiscPercentage"]);
                        
                    nonMed.LastUpdateByUserID = userID;
                    nonMed.LastUpdateDateTime = DateTime.Now;
                }
            }

            return nonMedColl;
        }

        private static ItemKitchenCollection PrepareItemKitchens(ItemTariffRequest tarifRequest, string userID)
        {
            var tariffRequestItemQ = new ItemTariffRequestItemQuery("a");
            tariffRequestItemQ.Where(tariffRequestItemQ.TariffRequestNo == tarifRequest.TariffRequestNo);
            DataTable dtbItemRequest = tariffRequestItemQ.LoadDataTable();

            tariffRequestItemQ = new ItemTariffRequestItemQuery("a");
            tariffRequestItemQ.Where(tariffRequestItemQ.TariffRequestNo == tarifRequest.TariffRequestNo);
            tariffRequestItemQ.Select(tariffRequestItemQ.ItemID);

            var kicthenQ = new ItemKitchenQuery("b");
            kicthenQ.Where(kicthenQ.ItemID.In(tariffRequestItemQ));
            kicthenQ.SelectAll();

            var kitchenColl = new ItemKitchenCollection();
            kitchenColl.Load(kicthenQ);

            foreach (DataRow row in dtbItemRequest.Rows)
            {
                ItemKitchen kitchen = null;
                bool isFound = false;
                foreach (ItemKitchen findItem in kitchenColl)
                {
                    //Jika ItemID tidak ditemukan, maka tambah row
                    if (findItem.ItemID.Equals(row["ItemID"]))
                    {
                        isFound = true;
                        kitchen = findItem;
                        break;
                    }
                }

                if (isFound)
                {
                    kitchen.ItemID = row["ItemID"].ToString();
                    kitchen.PriceInPurchaseUnit = Convert.ToDecimal(row["PriceInPurchaseUnit"]);
                    kitchen.PriceInBaseUnit = Convert.ToDecimal(row["PriceInBaseUnit"]);
                    kitchen.PriceInBasedUnitWVat = Convert.ToDecimal(row["PriceInBaseUnitWVat"]);
                    kitchen.CostPrice = Convert.ToDecimal(row["CostPrice"]);
                    kitchen.HighestPriceInBasedUnit = Convert.ToDecimal(row["PriceInBaseUnit"]);
                    kitchen.LastUpdateByUserID = userID;
                    kitchen.LastUpdateDateTime = DateTime.Now;
                }
            }

            return kitchenColl;
        }

        private static SupplierItemCollection PrepareSupplierItems(ItemTariffRequest tarifRequest, string userID)
        {
            var tariffRequestItemQ = new ItemTariffRequestItemQuery("a");
            tariffRequestItemQ.Where(tariffRequestItemQ.TariffRequestNo == tarifRequest.TariffRequestNo);
            DataTable dtbItemRequest = tariffRequestItemQ.LoadDataTable();

            tariffRequestItemQ = new ItemTariffRequestItemQuery("a");
            tariffRequestItemQ.Where(tariffRequestItemQ.TariffRequestNo == tarifRequest.TariffRequestNo);
            tariffRequestItemQ.Select(tariffRequestItemQ.ItemID);

            var supplierItemQ = new SupplierItemQuery("b");
            supplierItemQ.Where(supplierItemQ.ItemID.In(tariffRequestItemQ));
            supplierItemQ.SelectAll();

            var supplierItemColl = new SupplierItemCollection();
            supplierItemColl.Load(supplierItemQ);

            foreach (DataRow row in dtbItemRequest.Rows)
            {
                foreach (var item in supplierItemColl)
                {
                    if (item.ItemID == row["ItemID"].ToString())
                    {
                        item.PriceInPurchaseUnit = Convert.ToDecimal(row["PriceInPurchaseUnit"]);
                        item.LastUpdateByUserID = userID;
                        item.LastUpdateDateTime = DateTime.Now;
                    }
                }
            }

            return supplierItemColl;
        }

        #endregion
    }
}

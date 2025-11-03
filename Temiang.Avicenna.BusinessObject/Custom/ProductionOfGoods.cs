using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ProductionOfGoods
    {
        public static string IsItemMinusProcess(string productionNo, ProductionOfGoodsItemCollection coll)
        {
            var entity = new ProductionOfGoods();
            entity.LoadByPrimaryKey(productionNo);

            string itemID = GetItemBalanceMinus(coll, entity.LocationID);
            if (itemID != string.Empty)
                return "Production can't be Approved because ItemID : " + itemID + " will be minus.";
            return string.Empty;
        }

        private static string GetItemBalanceMinus(ProductionOfGoodsItemCollection coll, string locationID)
        {
            foreach (var item in coll)
            {
                var itemBalance = new ItemBalance();
                if (itemBalance.LoadByPrimaryKey(locationID, item.ItemID))
                {
                    if ((itemBalance.Balance - (item.Qty)) < 0)
                        return item.ItemID;
                }
                else
                    return item.ItemID;
                
            }

            return string.Empty;
        }

        public void Void(string prodNo, string userID)
        {
            var entity = new ProductionOfGoods();
            entity.LoadByPrimaryKey(prodNo);
            VoidProcess(entity, userID, true);
        }

        private static void VoidProcess(ProductionOfGoods entity, string userID, bool isVoid)
        {
            if (entity.IsVoid == true && isVoid)
                return;
            if (entity.IsVoid == false && !isVoid)
                return;

            entity.IsVoid = isVoid;
            entity.VoidDateTime = DateTime.Now;
            entity.VoidByUserID = userID;

            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        public string Approve(string prodNo, ProductionOfGoodsItemCollection coll, string userID, double tax, bool isEnabledStockWithEdControl)
        {
            var entity = new ProductionOfGoods();
            entity.LoadByPrimaryKey(prodNo);
            return ApproveProcess(entity, coll, userID, true, tax, isEnabledStockWithEdControl);
        }

        private static string ApproveProcess(ProductionOfGoods entity, ProductionOfGoodsItemCollection coll, string userID, bool isApproval, double tax, bool isEnabledStockWithEdControl)
        {
            if (isApproval)
            {
                if (entity.IsApproved ?? false)
                    return "Approved";
                if (entity.IsVoid ?? false)
                    return "Void";
            }
            else
            {
                if (!entity.IsApproved ?? false)
                    return "UnApproved";
            }

            entity.IsApproved = isApproval;
            entity.ApprovedDateTime = DateTime.Now;
            entity.ApprovedByUserID = userID;

            ItemBalanceCollection itemBalancesIn = null;
            var itemBalancesOut = new ItemBalanceCollection();

            ItemBalanceDetailCollection itemBalanceDetailsIn = null;
            var itemBalanceDetailsOut = new ItemBalanceDetailCollection();

            var itemBalanceDetailEdsIn = new ItemBalanceDetailEdCollection();
            var itemBalanceDetailEdsOut = new ItemBalanceDetailEdCollection();

            //ItemBalanceByPeriodCollection itemBalanceByPeriodsIn = null;
            //ItemBalanceByPeriodCollection itemBalanceByPeriodsOut = null;
            
            ItemMovementCollection itemMovementsIn = null;
            var itemMovementsOut = new ItemMovementCollection();
            
            ItemProductMedicCollection itemProductMedics = null;
            ItemProductNonMedicCollection itemProductNonMedics = null;
            ItemKitchenCollection itemKitchens = null;
            ItemTariffCollection itemTariffs = null;
            AveragePriceHistoryCollection averageMedics = null;
            AveragePriceHistoryCollection averageNonMedics = null;
            AveragePriceHistoryCollection averageKitchens = null;

            if (isApproval)
            {
                //--item out--
                string itemNoStock;
                ItemBalance.PrepareItemBalancesForProduction(entity, coll, ref itemBalancesOut,
                                                                               ref itemBalanceDetailsOut,
                                                                               ref itemBalanceDetailEdsOut,
                                                                               ref itemMovementsOut, userID, isEnabledStockWithEdControl, out itemNoStock);

                if (!string.IsNullOrEmpty(itemNoStock))
                    return "Insufficient balance of item : " + itemNoStock;


                //itemBalanceByPeriodsOut = PrepareItemBalanceByPeriods(entity, coll.Where(c => c.IsConsumables == true), userID);

                //--item in--
                itemBalancesIn = PrepareItemBalances(entity, userID);
                decimal? costPrice = 0;
                if (!isEnabledStockWithEdControl)
                {
                    itemBalanceDetailsIn = PrepareItemBalanceDetail(entity, userID);
                    itemMovementsIn = PrepareItemMovements(entity, coll, itemBalancesIn, userID, out costPrice);
                    itemBalanceDetailEdsIn = null;
                }
                else
                {
                    itemMovementsIn = new ItemMovementCollection();

                    PrepareItemBalancesIn(entity, coll, itemBalancesIn, ref itemMovementsIn, ref itemBalanceDetailEdsIn, userID, out costPrice);
                }

                //itemBalanceByPeriodsIn = PrepareItemBalanceByPeriods(entity, userID);


                // Update Master
                PrepareItemProductMedicAndAverage(entity, userID, out itemProductMedics, out averageMedics, costPrice, tax);
                PrepareItemProductNonMedicAndAverage(entity, userID, out itemProductNonMedics, out averageNonMedics, costPrice, tax);
                PrepareItemKitchenAndAverage(entity, userID, out itemKitchens, out averageKitchens, costPrice, tax);

                // jika IsProductionOfGoodUpdatingTariff == 'No' maka jangan update price
                bool IsUpdatePrice = true;
                var qparam = new AppParameter();
                if (qparam.LoadByPrimaryKey("IsProductionOfGoodUpdatingTariff"))
                {
                    if (qparam.ParameterValue == "No") IsUpdatePrice = false;
                }
                if (IsUpdatePrice)
                    itemTariffs = PrepareItemTariffs(entity, userID, tax);
            }

            using (var trans = new esTransactionScope())
            {
                entity.Save();

                if (itemBalancesIn != null)
                    itemBalancesIn.Save();
                if (itemBalancesOut != null)
                    itemBalancesOut.Save();

                if (itemBalanceDetailsIn != null)
                    itemBalanceDetailsIn.Save();
                if (itemBalanceDetailsOut != null)
                    itemBalanceDetailsOut.Save();

                if (itemBalanceDetailEdsIn != null)
                    itemBalanceDetailEdsIn.Save();
                if (itemBalanceDetailEdsOut != null)
                    itemBalanceDetailEdsOut.Save();

                //if (itemBalanceByPeriodsIn != null)
                //    itemBalanceByPeriodsIn.Save();
                //if (itemBalanceByPeriodsOut != null)
                //    itemBalanceByPeriodsOut.Save();

                if (itemMovementsIn != null)
                    itemMovementsIn.Save();
                if (itemMovementsOut != null)
                    itemMovementsOut.Save();

                if (itemProductMedics != null)
                    itemProductMedics.Save();
                if (itemProductNonMedics != null)
                    itemProductNonMedics.Save();
                if (itemKitchens != null)
                    itemKitchens.Save();
                if (itemTariffs != null)
                    itemTariffs.Save();
                if (averageMedics != null)
                    averageMedics.Save();
                if (averageNonMedics != null)
                    averageNonMedics.Save();
                if (averageKitchens != null)
                    averageKitchens.Save();

                AppParameter app = new AppParameter();
                if (app.LoadByPrimaryKey("acc_IsAutoJournalInventoryProduction"))
                {
                    if (app.ParameterValue == "Yes")
                    {
                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.ProductionDate.Value.Date);
                        if (isClosingPeriod)
                            return "Financial statements for period: " +
                                   string.Format("{0:MMMM-yyyy}", entity.ProductionDate) +
                                   " have been closed. Please contact the authorities.";

                        /* Automatic Journal Testing Start */

                        int? journalId = JournalTransactions.AddNewInventoryProductionJournal(entity, userID, 0);

                        /* Automatic Journal Testing End */
                    }
                }
                trans.Complete();
            }

            return string.Empty;
        }

        private static ItemBalanceCollection PrepareItemBalances(ProductionOfGoods entity, ProductionOfGoodsItemCollection coll, 
            string userID)
        {
            var list = coll.GroupBy(c => c.ItemID).Select(q => new
            {
                ItemID = q.Key,
                Quantity = q.Sum(p => (p.Qty))
            });

            var balances = new ItemBalanceCollection();
            balances.Query.Where(
                balances.Query.LocationID == entity.LocationID,
                balances.Query.ItemID.In(list.Select(l => l.ItemID))
                );
            balances.LoadAll();

            foreach (var item in list)
            {
                ItemBalance balance = null;
                bool isAvailable = false;
                foreach (ItemBalance findBalance in balances.Where(findBalance => findBalance.ItemID.Equals(item.ItemID)))
                {
                    isAvailable = true;
                    balance = findBalance;
                    break;
                }
                if (!isAvailable)
                {
                    balance = balances.AddNew();
                    balance.ItemID = item.ItemID;
                    balance.LocationID = entity.LocationID;
                    balance.Balance = 0 - item.Quantity;
                    balance.Minimum = 0;
                    balance.Maximum = 0;
                    balance.ReorderType = string.Empty;
                }
                else
                    balance.Balance -= item.Quantity;
                
                balance.LastUpdateByUserID = userID;
                balance.LastUpdateDateTime = DateTime.Now;
            }

            return balances;
        }

        private static ItemBalanceCollection PrepareItemBalances(ProductionOfGoods entity, string userID)
        {
            var pf = new ProductionFormula();
            pf.LoadByPrimaryKey(entity.FormulaID);

            var pfoColl = new ProductionFormulaOtherItemCollection();
            pfoColl.Query.Where(pfoColl.Query.FormulaID == entity.FormulaID);
            pfoColl.LoadAll();

            var balances = new ItemBalanceCollection();
            balances.Query.Where(
                balances.Query.LocationID == entity.LocationID);
            if (pfoColl.Count == 0)
                balances.Query.Where(balances.Query.ItemID == pf.ItemID);
            else
            {
                var items = (pfoColl.Select(i => i.ItemID)).Distinct();
                balances.Query.Where(balances.Query.Or(balances.Query.ItemID == pf.ItemID, balances.Query.ItemID.In(items)));
            }

            balances.LoadAll();

            ItemBalance balance = null;
            bool isAvailable = false;
            foreach (ItemBalance findBalance in balances.Where(findBalance => findBalance.ItemID.Equals(pf.ItemID)))
            {
                isAvailable = true;
                balance = findBalance;
                break;
            }

            if (!isAvailable)
            {
                balance = balances.AddNew();
                balance.ItemID = pf.ItemID;
                balance.LocationID = entity.LocationID;
                balance.Balance = entity.Qty * pf.Qty;
                balance.Minimum = 0;
                balance.Maximum = 0;
                balance.ReorderType = string.Empty;
            }
            else
                balance.Balance += (entity.Qty * pf.Qty);

            balance.LastUpdateByUserID = userID;
            balance.LastUpdateDateTime = DateTime.Now;

            foreach (var pfo in pfoColl)
            {
                isAvailable = false;
                foreach (ItemBalance findBalance in balances.Where(findBalance => findBalance.ItemID.Equals(pfo.ItemID)))
                {
                    isAvailable = true;
                    balance = findBalance;
                    break;
                }

                if (!isAvailable)
                {
                    balance = balances.AddNew();
                    balance.ItemID = pfo.ItemID;
                    balance.LocationID = entity.LocationID;
                    balance.Balance = entity.Qty * pfo.Qty;
                    balance.Minimum = 0;
                    balance.Maximum = 0;
                    balance.ReorderType = string.Empty;
                }
                else
                    balance.Balance += (entity.Qty * pfo.Qty);

                balance.LastUpdateByUserID = userID;
                balance.LastUpdateDateTime = DateTime.Now;
            }

            return balances;
        }

        private static ItemBalanceByPeriodCollection PrepareItemBalanceByPeriods(ProductionOfGoods entity, ProductionOfGoodsItemCollection coll,
           string userID)
        {
            var list = coll.GroupBy(c => c.ItemID).Select(q => new
            {
                ItemID = q.Key,
                Quantity = q.Sum(p => (p.Qty))
            });

            var balances = new ItemBalanceByPeriodCollection();
            balances.Query.Where(
                balances.Query.ItemID.In(list.Select(l => l.ItemID)),
                balances.Query.LocationID == entity.LocationID,
                balances.Query.PeriodYear == entity.ApprovedDateTime.Value.Year,
                balances.Query.PeriodMonth == entity.ApprovedDateTime.Value.Month
                );
            balances.LoadAll();

            foreach (var item in list)
            {
                ItemBalanceByPeriod balance = null;
                bool isAvailable = false;
                foreach (ItemBalanceByPeriod findBalance in balances.Where(findBalance => findBalance.ItemID.Equals(item.ItemID)))
                {
                    isAvailable = true;
                    balance = findBalance;
                    break;
                }
                if (!isAvailable)
                {
                    balance = balances.AddNew();
                    balance.PeriodYear = entity.ApprovedDateTime.Value.Year;
                    balance.PeriodMonth = entity.ApprovedDateTime.Value.Month;
                    balance.ItemID = item.ItemID;
                    balance.LocationID = entity.LocationID;
                    balance.BeginningBalance = 0;
                    balance.AdjustmentIn = 0;
                    balance.AdjustmentOut = 0;
                    balance.QuantityIn = 0;
                    balance.QuantityOut = Convert.ToDecimal(item.Quantity);
                }
                else
                    balance.QuantityOut += Convert.ToDecimal(item.Quantity);
                
                balance.LastUpdateByUserID = userID;
                balance.LastUpdateDateTime = DateTime.Now;
            }

            return balances;
        }

        private static ItemBalanceByPeriodCollection PrepareItemBalanceByPeriods(ProductionOfGoods entity, IEnumerable<ProductionOfGoodsItem> coll,
           string userID)
        {
            if (!coll.Any()) return null;

            var list = coll.GroupBy(c => c.ItemID).Select(q => new
            {
                ItemID = q.Key,
                Quantity = q.Sum(p => (p.Qty))
            });

            var balances = new ItemBalanceByPeriodCollection();
            balances.Query.Where(
                balances.Query.ItemID.In(list.Select(l => l.ItemID)),
                balances.Query.LocationID == entity.LocationID,
                balances.Query.PeriodYear == entity.ApprovedDateTime.Value.Year,
                balances.Query.PeriodMonth == entity.ApprovedDateTime.Value.Month
                );
            balances.LoadAll();

            foreach (var item in list)
            {
                ItemBalanceByPeriod balance = null;
                bool isAvailable = false;
                foreach (ItemBalanceByPeriod findBalance in balances.Where(findBalance => findBalance.ItemID.Equals(item.ItemID)))
                {
                    isAvailable = true;
                    balance = findBalance;
                    break;
                }
                if (!isAvailable)
                {
                    balance = balances.AddNew();
                    balance.PeriodYear = entity.ApprovedDateTime.Value.Year;
                    balance.PeriodMonth = entity.ApprovedDateTime.Value.Month;
                    balance.ItemID = item.ItemID;
                    balance.LocationID = entity.LocationID;
                    balance.BeginningBalance = 0;
                    balance.AdjustmentIn = 0;
                    balance.AdjustmentOut = 0;
                    balance.QuantityIn = 0;
                    balance.QuantityOut = Convert.ToDecimal(item.Quantity);
                }
                else
                    balance.QuantityOut += Convert.ToDecimal(item.Quantity);

                balance.LastUpdateByUserID = userID;
                balance.LastUpdateDateTime = DateTime.Now;
            }

            return balances;
        }

        private static ItemBalanceByPeriodCollection PrepareItemBalanceByPeriods(ProductionOfGoods entity, string userID)
        {
            var pf = new ProductionFormula();
            pf.LoadByPrimaryKey(entity.FormulaID);

            var pfoColl = new ProductionFormulaOtherItemCollection();
            pfoColl.Query.Where(pfoColl.Query.FormulaID == entity.FormulaID);
            pfoColl.LoadAll();

            var balances = new ItemBalanceByPeriodCollection();
            if (pfoColl.Count == 0)
            {
                balances.Query.Where(balances.Query.ItemID == pf.ItemID);
            }
            else
            {
                var items = (pfoColl.Select(i => i.ItemID)).Distinct();
                balances.Query.Where(balances.Query.Or(balances.Query.ItemID == pf.ItemID, balances.Query.ItemID.In(items)));
            }

            balances.Query.Where(
                balances.Query.LocationID == entity.LocationID,
                balances.Query.PeriodYear == entity.ApprovedDateTime.Value.Year,
                balances.Query.PeriodMonth == entity.ApprovedDateTime.Value.Month
                );

            balances.LoadAll();

            ItemBalanceByPeriod balance = null;
            bool isAvailable = false;
            foreach (ItemBalanceByPeriod findBalance in balances.Where(findBalance => findBalance.ItemID.Equals(pf.ItemID)))
            {
                isAvailable = true;
                balance = findBalance;
                break;
            }
        
            if (!isAvailable)
            {
                balance = balances.AddNew();
                balance.PeriodYear = entity.ApprovedDateTime.Value.Year;
                balance.PeriodMonth = entity.ApprovedDateTime.Value.Month;
                balance.ItemID = pf.ItemID;
                balance.LocationID = entity.LocationID;
                balance.BeginningBalance = 0;
                balance.AdjustmentIn = 0;
                balance.AdjustmentOut = 0;
                balance.QuantityIn = Convert.ToDecimal(entity.Qty * pf.Qty);
                balance.QuantityOut = 0;
            }
            else
                balance.QuantityIn += Convert.ToDecimal(entity.Qty * pf.Qty);

            balance.LastUpdateByUserID = userID;
            balance.LastUpdateDateTime = DateTime.Now;

            foreach (var pfo in pfoColl)
            {
                isAvailable = false;
                foreach (ItemBalanceByPeriod findBalance in balances.Where(findBalance => findBalance.ItemID.Equals(pfo.ItemID)))
                {
                    isAvailable = true;
                    balance = findBalance;
                    break;
                }

                if (!isAvailable)
                {
                    balance = balances.AddNew();
                    balance.PeriodYear = entity.ApprovedDateTime.Value.Year;
                    balance.PeriodMonth = entity.ApprovedDateTime.Value.Month;
                    balance.ItemID = pfo.ItemID;
                    balance.LocationID = entity.LocationID;
                    balance.BeginningBalance = 0;
                    balance.AdjustmentIn = 0;
                    balance.AdjustmentOut = 0;
                    balance.QuantityIn = Convert.ToDecimal(entity.Qty * pfo.Qty);
                    balance.QuantityOut = 0;
                }
                else
                    balance.QuantityIn += Convert.ToDecimal(entity.Qty * pfo.Qty);

                balance.LastUpdateByUserID = userID;
                balance.LastUpdateDateTime = DateTime.Now;
            }

            return balances;
        }

        private static ItemBalanceExpireCollection PrepareItemBalanceExpires(ProductionOfGoods entity, string userID)
        {
            var pf = new ProductionFormula();
            pf.LoadByPrimaryKey(entity.FormulaID);

            var balances = new ItemBalanceExpireCollection();
            balances.Query.Where(
                balances.Query.ItemID == pf.ItemID,
                balances.Query.LocationID == entity.LocationID
                );
            balances.LoadAll();

            ItemBalanceExpire balance = null;
            bool isAvailable = false;
            foreach (ItemBalanceExpire findBalance in balances.Where(findBalance => findBalance.ItemID.Equals(pf.ItemID)))
            {
                isAvailable = true;
                balance = findBalance;
                break;
            }
           
            if (!isAvailable)
            {
                balance = balances.AddNew();
                balance.LocationID = entity.LocationID;
                if (entity.ExpiredDate != null)
                    balance.ExpiredDate = entity.ExpiredDate;
                else
                    balance.str.ExpiredDate = string.Empty;
                balance.ItemID = pf.ItemID;
                balance.IsActive = true;
                balance.QuantityIn = Convert.ToDecimal(entity.Qty*pf.Qty);
                balance.QuantityOut = 0;
            }
            else
                balance.QuantityIn += Convert.ToDecimal(entity.Qty*pf.Qty);

            balance.LastUpdateByUserID = userID;
            balance.LastUpdateDateTime = DateTime.Now;

            return balances;
        }

        private static ItemMovementCollection PrepareItemMovements(ProductionOfGoods entity, ProductionOfGoodsItemCollection coll, ItemBalanceCollection balances,
            string userID, out decimal? costPrice)
        {
            var pf = new ProductionFormula();
            pf.LoadByPrimaryKey(entity.FormulaID);

            //var allBalances = new ItemBalanceCollection();
            //allBalances.Query.Select(
            //    allBalances.Query.ItemID,
            //    allBalances.Query.Balance.Sum()
            //    );
            //allBalances.Query.Where(allBalances.Query.ItemID == pf.ItemID);
            //allBalances.Query.GroupBy(allBalances.Query.ItemID);
            //allBalances.LoadAll();

            var movements = new ItemMovementCollection();

            var itemMovement = movements.AddNew();
            itemMovement.MovementDate = entity.ApprovedDateTime;
            itemMovement.ServiceUnitID = entity.ServiceUnitID;
            itemMovement.LocationID = entity.LocationID;
            itemMovement.TransactionCode = TransactionCode.ProductionOfGoods;
            itemMovement.TransactionNo = entity.ProductionNo;
            itemMovement.SequenceNo = string.Empty;
            itemMovement.ItemID = pf.ItemID;
            if (entity.ExpiredDate != null)
                itemMovement.ExpiredDate = entity.ExpiredDate;
            else
                itemMovement.str.ExpiredDate = string.Empty;

            var balance = balances.FindByPrimaryKey(itemMovement.LocationID, itemMovement.ItemID);
            itemMovement.InitialStock = balance.Balance - (entity.Qty * pf.Qty);
            itemMovement.QuantityIn = entity.Qty * pf.Qty;
            itemMovement.QuantityOut = 0;

            string baseUnit;
            decimal? total = 0;
            decimal? total2 = 0;
            foreach (ProductionOfGoodsItem item in coll)
            {
                total += (item.Qty * item.CostPrice);
                total2 += (item.Qty * item.PriceInBaseUnit);
            }
            costPrice = total / (entity.Qty * pf.Qty);
            decimal? price = total2 / (entity.Qty * pf.Qty);
            
            var i = new Item();
            i.LoadByPrimaryKey(pf.ItemID);

            if (i.SRItemType == ItemType.Medical)
            {
                var medic = new ItemProductMedic();
                medic.LoadByPrimaryKey(itemMovement.ItemID);
                baseUnit = medic.SRItemUnit;
            }
            else if (i.SRItemType == ItemType.NonMedical)
            {
                var nonMedic = new ItemProductNonMedic();
                nonMedic.LoadByPrimaryKey(itemMovement.ItemID);
                baseUnit = nonMedic.SRItemUnit;
            }
            else
            {
                var kitchen = new ItemKitchen();
                kitchen.LoadByPrimaryKey(itemMovement.ItemID);
                baseUnit = kitchen.SRItemUnit;
            }

            itemMovement.SRItemUnit = baseUnit;
            itemMovement.CostPrice = costPrice;
            itemMovement.SalesPrice = 0; //TODO: set Sales Price
            itemMovement.PurchasePrice = costPrice;
            itemMovement.LastPriceInBaseUnit = price;
            itemMovement.LastUpdateByUserID = userID;
            itemMovement.LastUpdateDateTime = DateTime.Now;

            var pfoColl = new ProductionFormulaOtherItemCollection();
            pfoColl.Query.Where(pfoColl.Query.FormulaID == entity.FormulaID);
            pfoColl.LoadAll();
            foreach (var pfo in pfoColl)
            {
                itemMovement = movements.AddNew();
                itemMovement.MovementDate = entity.ApprovedDateTime;
                itemMovement.ServiceUnitID = entity.ServiceUnitID;
                itemMovement.LocationID = entity.LocationID;
                itemMovement.TransactionCode = TransactionCode.ProductionOfGoods;
                itemMovement.TransactionNo = entity.ProductionNo;
                itemMovement.SequenceNo = string.Empty;
                itemMovement.ItemID = pfo.ItemID;
                if (entity.ExpiredDate != null)
                    itemMovement.ExpiredDate = entity.ExpiredDate;
                else
                    itemMovement.str.ExpiredDate = string.Empty;

                balance = balances.FindByPrimaryKey(itemMovement.LocationID, itemMovement.ItemID);
                itemMovement.InitialStock = balance.Balance - (entity.Qty * pfo.Qty);
                itemMovement.QuantityIn = entity.Qty * pfo.Qty;
                itemMovement.QuantityOut = 0;

                i = new Item();
                i.LoadByPrimaryKey(pfo.ItemID);

                if (i.SRItemType == ItemType.Medical)
                {
                    var medic = new ItemProductMedic();
                    medic.LoadByPrimaryKey(itemMovement.ItemID);
                    baseUnit = medic.SRItemUnit;
                    costPrice = medic.CostPrice;
                }
                else
                {
                    var nonMedic = new ItemProductNonMedic();
                    nonMedic.LoadByPrimaryKey(itemMovement.ItemID);
                    baseUnit = nonMedic.SRItemUnit;
                    costPrice = nonMedic.CostPrice;
                }

                itemMovement.SRItemUnit = baseUnit;
                itemMovement.CostPrice = 0;
                itemMovement.SalesPrice = 0; //TODO: set Sales Price
                itemMovement.PurchasePrice = costPrice;
                itemMovement.LastPriceInBaseUnit = costPrice;
                itemMovement.LastUpdateByUserID = userID;
                itemMovement.LastUpdateDateTime = DateTime.Now;
            }

            return movements;
        }

       
        private static ItemBalanceDetailCollection PrepareItemBalanceDetail(ProductionOfGoods entity, string userID)
        {
            var pf = new ProductionFormula();
            pf.LoadByPrimaryKey(entity.FormulaID);

            var balances = new ItemBalanceDetailCollection();
            var balance = balances.AddNew();
            balance.LocationID = entity.LocationID;
            balance.ItemID = pf.ItemID;
            balance.ReferenceNo = entity.ProductionNo;
            balance.TransactionCode = TransactionCode.ProductionOfGoods;
            balance.BalanceDate = entity.ProductionDate;
            balance.Balance = entity.Qty * pf.Qty;
            balance.Booking = 0;
            balance.Price = entity.Price;
            balance.LastUpdateDateTime = DateTime.Now;
            balance.LastUpdateByUserID = userID;

            var pfoColl = new ProductionFormulaOtherItemCollection();
            pfoColl.Query.Where(pfoColl.Query.FormulaID == entity.FormulaID);
            pfoColl.LoadAll();
            foreach (var pfo in pfoColl)
            {
                balance = balances.AddNew();
                balance.LocationID = entity.LocationID;
                balance.ItemID = pfo.ItemID;
                balance.ReferenceNo = entity.ProductionNo;
                balance.TransactionCode = TransactionCode.ProductionOfGoods;
                balance.BalanceDate = entity.ProductionDate;
                balance.Balance = entity.Qty * pfo.Qty;
                balance.Booking = 0;
                balance.Price = entity.Price;
                balance.LastUpdateDateTime = DateTime.Now;
                balance.LastUpdateByUserID = userID;
            }

            return balances;
        }

        public static void PrepareItemBalancesIn(ProductionOfGoods entity, ProductionOfGoodsItemCollection coll, 
            ItemBalanceCollection itemBalance, ref ItemMovementCollection itemMovements, ref ItemBalanceDetailEdCollection itemBalanceDetailEds, string userID, out decimal? costPrice)
        {
            var pf = new ProductionFormula();
            pf.LoadByPrimaryKey(entity.FormulaID);

            var pfoColl = new ProductionFormulaOtherItemCollection();
            pfoColl.Query.Where(pfoColl.Query.FormulaID == entity.FormulaID);
            pfoColl.LoadAll();

            string baseUnit;
            bool isControlExpired = false;

            var i = new Item();
            i.LoadByPrimaryKey(pf.ItemID);

            if (i.SRItemType == ItemType.Medical)
            {
                var medic = new ItemProductMedic();
                medic.LoadByPrimaryKey(i.ItemID);
                baseUnit = medic.SRItemUnit;
                isControlExpired = medic.IsControlExpired ?? false;
            }
            else if (i.SRItemType == ItemType.NonMedical)
            {
                var nonMedic = new ItemProductNonMedic();
                nonMedic.LoadByPrimaryKey(i.ItemID);
                baseUnit = nonMedic.SRItemUnit;
                isControlExpired = nonMedic.IsControlExpired ?? false;
            }
            else
            {
                var kitchen = new ItemKitchen();
                kitchen.LoadByPrimaryKey(i.ItemID);
                baseUnit = kitchen.SRItemUnit;
                isControlExpired = kitchen.IsControlExpired ?? false;
            }

            DateTime expiredDate = entity.ExpiredDate ?? Convert.ToDateTime("1/1/2999"); ;
            var batchNo = string.IsNullOrEmpty(entity.BatchNumber) ? "-N/A-" : entity.BatchNumber; ;

            decimal? total = 0;
            decimal? total2 = 0;
            foreach (ProductionOfGoodsItem item in coll)
            {
                total += (item.Qty * item.CostPrice);
                total2 += (item.Qty * item.PriceInBaseUnit);
            }
            costPrice = total / (entity.Qty * pf.Qty);
            decimal? price = total2 / (entity.Qty * pf.Qty);

            var items = (pfoColl.Select(x => x.ItemID)).Distinct();

            #region ItemBalanceDetailEd

            itemBalanceDetailEds.Query.Where
                (
                    itemBalanceDetailEds.Query.LocationID == entity.LocationID,
                    itemBalanceDetailEds.Query.ItemID == pf.ItemID
                );
            itemBalanceDetailEds.LoadAll();

            var details = (itemBalanceDetailEds.Where(im => im.ItemID == pf.ItemID && im.ExpiredDate == expiredDate && im.BatchNumber == batchNo)
                                                .OrderBy(im => im.CreatedDateTime)).Take(1).SingleOrDefault();
            if (details == null)
            {
                details = itemBalanceDetailEds.AddNew();
                details.LocationID = entity.LocationID;
                details.ItemID = pf.ItemID;
                details.ExpiredDate = expiredDate;
                details.BatchNumber = batchNo;
                details.Balance = entity.Qty * pf.Qty;
                details.IsActive = true;
                details.CreatedDateTime = DateTime.Now;
                details.CreatedByUserID = userID;
            }
            else
            {
                if (details.Balance < 0)
                    details.Balance = entity.Qty * pf.Qty;
                else
                    details.Balance += (entity.Qty * pf.Qty);
                details.IsActive = true;
            }

            details.LastUpdateDateTime = DateTime.Now;
            details.LastUpdateByUserID = userID;
            #endregion

            #region ItemMovement
            var itemMovement = itemMovements.AddNew();
            itemMovement.MovementDate = entity.ApprovedDateTime;
            itemMovement.ServiceUnitID = entity.ServiceUnitID;
            itemMovement.LocationID = entity.LocationID;
            itemMovement.TransactionCode = TransactionCode.ProductionOfGoods;
            itemMovement.TransactionNo = entity.ProductionNo;
            itemMovement.SequenceNo = string.Empty;
            itemMovement.ItemID = pf.ItemID;
            itemMovement.ExpiredDate = expiredDate;
            itemMovement.BatchNumber = batchNo;

            var balance = itemBalance.FindByPrimaryKey(itemMovement.LocationID, itemMovement.ItemID);
            itemMovement.InitialStock = balance.Balance - (entity.Qty * pf.Qty);
            itemMovement.QuantityIn = entity.Qty * pf.Qty;
            itemMovement.QuantityOut = 0;

            itemMovement.SRItemUnit = baseUnit;
            itemMovement.CostPrice = costPrice;
            itemMovement.SalesPrice = 0; //TODO: set Sales Price
            itemMovement.PurchasePrice = costPrice;
            itemMovement.LastPriceInBaseUnit = price;
            itemMovement.LastUpdateByUserID = userID;
            itemMovement.LastUpdateDateTime = DateTime.Now;
            #endregion

            foreach (var pfo in pfoColl)
            {
                #region ItemBalanceDetailEd
                details = (itemBalanceDetailEds.Where(im => im.ItemID == pfo.ItemID && im.ExpiredDate == expiredDate && im.BatchNumber == batchNo)
                                                .OrderBy(im => im.CreatedDateTime)).Take(1).SingleOrDefault();
                if (details == null)
                {
                    details = itemBalanceDetailEds.AddNew();
                    details.LocationID = entity.LocationID;
                    details.ItemID = pfo.ItemID;
                    details.ExpiredDate = expiredDate;
                    details.BatchNumber = batchNo;
                    details.Balance = entity.Qty * pf.Qty;
                    details.IsActive = true;
                    details.CreatedDateTime = DateTime.Now;
                    details.CreatedByUserID = userID;
                }
                else
                {
                    if (details.Balance < 0)
                        details.Balance = entity.Qty * pf.Qty;
                    else
                        details.Balance += (entity.Qty * pf.Qty);
                    details.IsActive = true;
                }

                details.LastUpdateDateTime = DateTime.Now;
                details.LastUpdateByUserID = userID;
                #endregion

                #region ItemMovement
                itemMovement = itemMovements.AddNew();
                itemMovement.MovementDate = entity.ApprovedDateTime;
                itemMovement.ServiceUnitID = entity.ServiceUnitID;
                itemMovement.LocationID = entity.LocationID;
                itemMovement.TransactionCode = TransactionCode.ProductionOfGoods;
                itemMovement.TransactionNo = entity.ProductionNo;
                itemMovement.SequenceNo = string.Empty;
                itemMovement.ItemID = pfo.ItemID;
                itemMovement.ExpiredDate = expiredDate;
                itemMovement.BatchNumber = batchNo;

                balance = itemBalance.FindByPrimaryKey(itemMovement.LocationID, itemMovement.ItemID);
                itemMovement.InitialStock = balance.Balance - (entity.Qty * pfo.Qty);
                itemMovement.QuantityIn = entity.Qty * pfo.Qty;
                itemMovement.QuantityOut = 0;

                i = new Item();
                i.LoadByPrimaryKey(pfo.ItemID);

                if (i.SRItemType == ItemType.Medical)
                {
                    var medic = new ItemProductMedic();
                    medic.LoadByPrimaryKey(itemMovement.ItemID);
                    baseUnit = medic.SRItemUnit;
                    costPrice = medic.CostPrice;
                }
                else
                {
                    var nonMedic = new ItemProductNonMedic();
                    nonMedic.LoadByPrimaryKey(itemMovement.ItemID);
                    baseUnit = nonMedic.SRItemUnit;
                    costPrice = nonMedic.CostPrice;
                }

                itemMovement.SRItemUnit = baseUnit;
                itemMovement.CostPrice = 0;
                itemMovement.SalesPrice = 0; //TODO: set Sales Price
                itemMovement.PurchasePrice = costPrice;
                itemMovement.LastPriceInBaseUnit = costPrice;
                itemMovement.LastUpdateByUserID = userID;
                itemMovement.LastUpdateDateTime = DateTime.Now;
                #endregion

            }
        }

        private static void PrepareItemProductMedicAndAverage(ProductionOfGoods entity, string userID, 
            out ItemProductMedicCollection itemProductMedics, out AveragePriceHistoryCollection averagePriceHistories, decimal? costPrice, double tax)
        {
            var pf = new ProductionFormula();
            pf.LoadByPrimaryKey(entity.FormulaID);

            var i = new Item();
            i.LoadByPrimaryKey(pf.ItemID);
            if (i.SRItemType != ItemType.Medical)
            {
                itemProductMedics = null;
                averagePriceHistories = null;
                return;
            }

            itemProductMedics = new ItemProductMedicCollection();
            itemProductMedics.Query.Where(itemProductMedics.Query.ItemID == pf.ItemID);
            itemProductMedics.LoadAll();

            averagePriceHistories = new AveragePriceHistoryCollection();

            decimal? priceWvat = (entity.Price);
            decimal? priceInBaseUnitNow = (entity.Price);

            ItemProductMedic medic = itemProductMedics.FindByPrimaryKey(pf.ItemID);
            if (medic != null)
            {
                if (priceInBaseUnitNow > medic.HighestPriceInBasedUnit)
                {
                    medic.HighestPriceInBasedUnit = priceInBaseUnitNow;
                    medic.PriceInBaseUnit = priceInBaseUnitNow;
                    medic.PriceInBasedUnitWVat = (priceInBaseUnitNow * Convert.ToDecimal(1 + (tax / 100.00)));
                    medic.PriceInPurchaseUnit = priceInBaseUnitNow;
                }
                medic.PurchaseDiscount1 = 0;
                medic.PurchaseDiscount2 = 0;

                var balances = new ItemBalanceCollection();
                balances.Query.Select(
                    balances.Query.ItemID,
                    balances.Query.Balance.Sum()
                    );
                balances.Query.Where(balances.Query.ItemID == pf.ItemID);
                balances.Query.GroupBy(balances.Query.ItemID);
                balances.LoadAll();

                decimal initialQty = 0;
                if (balances.Count() != 0) initialQty = Convert.ToDecimal(balances[0].Balance);

                decimal? qtyReceiveInBaseUnit = entity.Qty;
                decimal? qtyTotal = qtyReceiveInBaseUnit + initialQty;
                decimal? newCostPrice = (((medic.CostPrice ?? 0) * initialQty) + (costPrice * qtyReceiveInBaseUnit)) / qtyTotal;


                AveragePriceHistory averagePriceHis = averagePriceHistories.AddNew();
                averagePriceHis.TransactionCode = TransactionCode.ProductionOfGoods;
                averagePriceHis.TransactionNo = entity.ProductionNo;
                averagePriceHis.ItemUnit = medic.SRItemUnit;
                averagePriceHis.ItemID = pf.ItemID;
                averagePriceHis.ChangedDate = entity.ApprovedDateTime;
                averagePriceHis.OldAveragePrice = medic.CostPrice;
                averagePriceHis.NewAveragePrice = newCostPrice;
                averagePriceHis.LastUpdateByUserID = userID;
                averagePriceHis.LastUpdateDateTime = DateTime.Now;
                //ditaruh disini agar averagePriceHis.OldAveragePrice terisi dahulu dgn nilai lama
                medic.CostPrice = newCostPrice;// costPrice; 
            }
        }

        private static void PrepareItemProductNonMedicAndAverage(ProductionOfGoods entity, string userID,
            out ItemProductNonMedicCollection itemProductNonMedics, out AveragePriceHistoryCollection averagePriceHistories, decimal? costPrice, double tax)
        {
            var pf = new ProductionFormula();
            pf.LoadByPrimaryKey(entity.FormulaID);

            var i = new Item();
            i.LoadByPrimaryKey(pf.ItemID);
            if (i.SRItemType != ItemType.NonMedical)
            {
                itemProductNonMedics = null;
                averagePriceHistories = null;
                return;
            }

            itemProductNonMedics = new ItemProductNonMedicCollection();
            itemProductNonMedics.Query.Where(itemProductNonMedics.Query.ItemID == pf.ItemID);
            itemProductNonMedics.LoadAll();

            averagePriceHistories = new AveragePriceHistoryCollection();

            decimal? priceWvat = entity.Price;
            decimal? priceInBaseUnitNow = entity.Price;

            ItemProductNonMedic nonMedic = itemProductNonMedics.FindByPrimaryKey(pf.ItemID);
            if (nonMedic != null)
            {
                if (priceInBaseUnitNow > nonMedic.HighestPriceInBasedUnit)
                {
                    nonMedic.HighestPriceInBasedUnit = priceInBaseUnitNow;
                    nonMedic.PriceInBaseUnit = priceInBaseUnitNow;
                    nonMedic.PriceInBasedUnitWVat = (priceInBaseUnitNow * Convert.ToDecimal(1 + (tax / 100.00)));
                    nonMedic.PriceInPurchaseUnit = priceInBaseUnitNow;
                }

                nonMedic.PurchaseDiscount1 = 0;
                nonMedic.PurchaseDiscount2 = 0;

                var balances = new ItemBalanceCollection();
                balances.Query.Select(
                    balances.Query.ItemID,
                    balances.Query.Balance.Sum()
                    );
                balances.Query.Where(balances.Query.ItemID == pf.ItemID);
                balances.Query.GroupBy(balances.Query.ItemID);
                balances.LoadAll();

                decimal initialQty = 0;
                if (balances.Count() != 0) initialQty = Convert.ToDecimal(balances[0].Balance);

                decimal? qtyReceiveInBaseUnit = entity.Qty;
                decimal? qtyTotal = qtyReceiveInBaseUnit + initialQty;
                decimal? newCostPrice = (((nonMedic.CostPrice ?? 0) * initialQty) + (costPrice * qtyReceiveInBaseUnit)) / qtyTotal;

                AveragePriceHistory averagePriceHis = averagePriceHistories.AddNew();
                averagePriceHis.TransactionCode = TransactionCode.ProductionOfGoods;
                averagePriceHis.TransactionNo = entity.ProductionNo;
                averagePriceHis.ItemUnit = nonMedic.SRItemUnit;
                averagePriceHis.ItemID = pf.ItemID;
                averagePriceHis.ChangedDate = entity.ApprovedDateTime;
                averagePriceHis.OldAveragePrice = nonMedic.CostPrice;
                averagePriceHis.NewAveragePrice = newCostPrice;
                averagePriceHis.LastUpdateByUserID = userID;
                averagePriceHis.LastUpdateDateTime = DateTime.Now;

                nonMedic.CostPrice = newCostPrice;// costPrice;
            }
        }

        private static void PrepareItemKitchenAndAverage(ProductionOfGoods entity, string userID,
            out ItemKitchenCollection itemKitchens, out AveragePriceHistoryCollection averagePriceHistories, decimal? costPrice, double tax)
        {
            var pf = new ProductionFormula();
            pf.LoadByPrimaryKey(entity.FormulaID);

            var i = new Item();
            i.LoadByPrimaryKey(pf.ItemID);
            if (i.SRItemType != ItemType.Kitchen)
            {
                itemKitchens = null;
                averagePriceHistories = null;
                return;
            }

            itemKitchens = new ItemKitchenCollection();
            itemKitchens.Query.Where(itemKitchens.Query.ItemID == pf.ItemID);
            itemKitchens.LoadAll();

            averagePriceHistories = new AveragePriceHistoryCollection();

            decimal? priceWvat = entity.Price;
            decimal? priceInBaseUnitNow = entity.Price;

            ItemKitchen kitchen = itemKitchens.FindByPrimaryKey(pf.ItemID);
            if (kitchen != null)
            {
                if (priceInBaseUnitNow > kitchen.HighestPriceInBasedUnit)
                {
                    kitchen.HighestPriceInBasedUnit = priceInBaseUnitNow;
                    kitchen.PriceInBaseUnit = priceInBaseUnitNow;
                    kitchen.PriceInBasedUnitWVat = (priceInBaseUnitNow * Convert.ToDecimal(1 + (tax / 100.00)));
                    kitchen.PriceInPurchaseUnit = priceInBaseUnitNow;
                }

                kitchen.PurchaseDiscount1 = 0;
                kitchen.PurchaseDiscount2 = 0;

                var balances = new ItemBalanceCollection();
                balances.Query.Select(
                    balances.Query.ItemID,
                    balances.Query.Balance.Sum()
                    );
                balances.Query.Where(balances.Query.ItemID == pf.ItemID);
                balances.Query.GroupBy(balances.Query.ItemID);
                balances.LoadAll();

                decimal initialQty = 0;
                if (balances.Count() != 0) initialQty = Convert.ToDecimal(balances[0].Balance);

                decimal? qtyReceiveInBaseUnit = entity.Qty;
                decimal? qtyTotal = qtyReceiveInBaseUnit + initialQty;
                decimal? newCostPrice = (((kitchen.CostPrice ?? 0) * initialQty) + (costPrice * qtyReceiveInBaseUnit)) / qtyTotal;

                AveragePriceHistory averagePriceHis = averagePriceHistories.AddNew();
                averagePriceHis.TransactionCode = TransactionCode.ProductionOfGoods;
                averagePriceHis.TransactionNo = entity.ProductionNo;
                averagePriceHis.ItemUnit = kitchen.SRItemUnit;
                averagePriceHis.ItemID = pf.ItemID;
                averagePriceHis.ChangedDate = entity.ApprovedDateTime;
                averagePriceHis.OldAveragePrice = kitchen.CostPrice;
                averagePriceHis.NewAveragePrice = newCostPrice;
                averagePriceHis.LastUpdateByUserID = userID;
                averagePriceHis.LastUpdateDateTime = DateTime.Now;

                kitchen.CostPrice = newCostPrice;// costPrice;
            }
        }

        private static ItemTariffCollection PrepareItemTariffs(ProductionOfGoods pog, string userID, double tax)
        {
            var pf = new ProductionFormula();
            pf.LoadByPrimaryKey(pog.FormulaID);

            var i = new Item();
            i.LoadByPrimaryKey(pf.ItemID);
            var pogQuery = new ProductionOfGoodsQuery("a");
            var pfQuery = new ProductionFormulaQuery("d");
            pogQuery.InnerJoin(pfQuery).On(pogQuery.FormulaID == pfQuery.FormulaID);

            if (i.SRItemType == ItemType.Medical)
            {
                var itemQuery = new ItemProductMedicQuery("b");
                pogQuery.InnerJoin(itemQuery).On(pfQuery.ItemID == itemQuery.ItemID);
                pogQuery.Select(
                    pfQuery.ItemID,
                    pogQuery.ExpiredDate,
                    pogQuery.Qty,
                    pogQuery.Price,
                    itemQuery.MarginID,
                    itemQuery.MarginPercentage,
                    itemQuery.HighestPriceInBasedUnit
                    );
            }
            else
            {
                var itemQuery = new ItemProductNonMedicQuery("b");
                pogQuery.InnerJoin(itemQuery).On(pfQuery.ItemID == itemQuery.ItemID);
                pogQuery.Select(
                    pfQuery.ItemID,
                    pogQuery.ExpiredDate,
                    pogQuery.Qty,
                    pogQuery.Price,
                    itemQuery.MarginID,
                    itemQuery.MarginPercentage,
                    itemQuery.HighestPriceInBasedUnit
                    );
            }
            pogQuery.Where(pogQuery.ProductionNo == pog.ProductionNo);
            DataTable dtbItemTrans = pogQuery.LoadDataTable();

            var itemTariffs = new ItemTariffCollection();
            
            foreach (DataRow row in dtbItemTrans.Rows)
            {
                decimal? priceInBaseUnit = (decimal)row["Price"];
                if (priceInBaseUnit > Convert.ToDecimal(row["HighestPriceInBasedUnit"]))
                {
                    itemTariffs.Query.Where(itemTariffs.Query.ItemID == row["ItemID"].ToString() &&
                                            itemTariffs.Query.StartingDate == pog.ApprovedDateTime.Value.Date &&
                                            itemTariffs.Query.SRTariffType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffType) &&
                                            itemTariffs.Query.ClassID == AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffClass));
                    itemTariffs.LoadAll();
                    if (itemTariffs.Count == 0)
                    {
                        ItemTariff tariff = itemTariffs.AddNew();
                        tariff.SRTariffType = AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffType);
                        tariff.ItemID = row["ItemID"].ToString();
                        tariff.ClassID = AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffClass);
                        tariff.StartingDate = pog.ApprovedDateTime.Value.Date;

                        // Price yg disimpan adalah price pembeliannya, tetapi waktu ambil untuk jual
                        // harus di markup dulu, proses markupnya di pindah ke saat query untuk penjualan
                        tariff.Price = priceInBaseUnit * (Convert.ToDecimal(1 + (tax / 100.00)));
                        tariff.ReferenceNo = pog.ProductionNo;
                        tariff.ReferenceTransactionCode = TransactionCode.ProductionOfGoods;
                        tariff.LastUpdateByUserID = userID;
                        tariff.LastUpdateDateTime = DateTime.Now;
                        tariff.Ppn = Convert.ToDecimal(tax);
                    }
                    else
                    {
                        // update harga
                        foreach (var tariff in itemTariffs)
                        {
                            tariff.Price = priceInBaseUnit * (Convert.ToDecimal(1 + (tax / 100.00)));
                            tariff.ReferenceNo = pog.ProductionNo;
                            tariff.ReferenceTransactionCode = TransactionCode.ProductionOfGoods;
                            tariff.LastUpdateByUserID = userID;
                            tariff.LastUpdateDateTime = DateTime.Now;
                            tariff.Ppn = Convert.ToDecimal(tax);
                        }
                    }
                }
            }
            return itemTariffs;
        }

        public static void UpdateCostPrice(ProductionOfGoodsItemCollection coll, out string itemZeroCostPrice)
        {
            itemZeroCostPrice = string.Empty;
            foreach (var entity in coll)
            {
                if (entity.IsConsumables == true)
                {
                    var item = new Item();
                    item.LoadByPrimaryKey(entity.ItemID);
                    decimal price = 0;
                    switch (item.SRItemType)
                    {
                        case Reference.ItemType.Medical:
                            var med = new ItemProductMedic();
                            med.LoadByPrimaryKey(item.ItemID);
                            entity.CostPrice = med.CostPrice;
                            price = med.PriceInBaseUnit ?? 0;
                            if (!med.IsInventoryItem ?? false)
                                continue;

                            break;
                        case Reference.ItemType.NonMedical:
                            var nmed = new ItemProductNonMedic();
                            nmed.LoadByPrimaryKey(item.ItemID);
                            entity.CostPrice = nmed.CostPrice;
                            price = nmed.PriceInBaseUnit ?? 0;
                            if (!nmed.IsInventoryItem ?? false)
                                continue;

                            break;
                        case Reference.ItemType.Kitchen:
                            var kitc = new ItemKitchen();
                            kitc.LoadByPrimaryKey(item.ItemID);
                            entity.CostPrice = kitc.CostPrice;
                            price = kitc.PriceInBaseUnit ?? 0;
                            if (!kitc.IsInventoryItem ?? false)
                                continue;

                            break;
                        default:
                            continue;
                    }

                    if ((entity.CostPrice ?? 0) == 0 && price > 0)
                    {
                        itemZeroCostPrice = item.ItemName;
                        return;
                    }
                }
                
            }
        }
    }
}

using System;
using System.Data;
using System.Web;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemMovement
    {
        private const string _consumptionCode = "073"; // Service Unit Stock Consumption
        private const string _prescriptionCode = "091"; // Prescription
        private const string _prescriptionReturnCode = "094"; // Prescription Return
        private const string _adjustmentCode = "074"; // Stock Adjustment
        private const string _opnameCode = "075"; // Stock Adjustment
        private const string _chargesCode = "003"; // Charges Entry

        public static ItemMovementCollection PrepareItemMovements(TransChargesItemCollection collection, string serviceUnitID,
            string locationID, string userID, bool isApproval, ItemBalanceCollection balanceCollection)
        {
            var movementCollection = new ItemMovementCollection();

            foreach (var entity in collection)
            {
                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);
                if (item.SRItemType == Reference.ItemType.Medical || item.SRItemType == Reference.ItemType.NonMedical)
                {
                    var movement = movementCollection.AddNew();
                    movement.MovementDate = DateTime.Now;
                    movement.ServiceUnitID = serviceUnitID;
                    movement.LocationID = locationID;
                    movement.TransactionCode = _chargesCode;
                    movement.TransactionNo = entity.TransactionNo;
                    movement.SequenceNo = entity.SequenceNo;
                    movement.ItemID = entity.ItemID;
                    movement.ExpiredDate = DateTime.Now.Date;

                    var balance = balanceCollection.FindByPrimaryKey(locationID, entity.ItemID);
                    movement.InitialStock = (balance != null) ? (balance.Balance + entity.ChargeQuantity) : 0;

                    if (isApproval)
                    {
                        movement.QuantityOut = entity.ChargeQuantity;
                        movement.QuantityIn = 0;
                    }
                    else
                    {
                        movement.QuantityIn = entity.ChargeQuantity;
                        movement.QuantityOut = 0;
                    }

                    movement.SRItemUnit = entity.SRItemUnit;

                    decimal cost = 0, purchase = 0;

                    if (item.SRItemType == Reference.ItemType.Medical)
                    {
                        var med = new ItemProductMedic();
                        med.LoadByPrimaryKey(item.ItemID);

                        cost = med.CostPrice ?? 0;
                        purchase = med.PriceInBaseUnit ?? 0;
                    }
                    else if (item.SRItemType == Reference.ItemType.NonMedical)
                    {
                        var med = new ItemProductNonMedic();
                        med.LoadByPrimaryKey(item.ItemID);

                        cost = med.CostPrice ?? 0;
                        purchase = med.PriceInBaseUnit ?? 0;
                    }

                    movement.CostPrice = cost;
                    movement.SalesPrice = entity.Price;
                    movement.PurchasePrice = purchase;
                    movement.LastUpdateDateTime = DateTime.Now;
                    movement.LastUpdateByUserID = userID;
                }
            }

            return movementCollection;
        }

        public static ItemMovement PrepareItemMovements(TransChargesItem entity, string serviceUnitID, string locationID,
            string userID, bool isApproval, ItemBalance balance)
        {
            var item = new Item();
            item.LoadByPrimaryKey(entity.ItemID);
            if (item.SRItemType == Reference.ItemType.Medical || item.SRItemType == Reference.ItemType.NonMedical)
            {
                var movement = new ItemMovement();
                movement.MovementDate = DateTime.Now;
                movement.ServiceUnitID = serviceUnitID;
                movement.LocationID = locationID;
                movement.TransactionCode = _chargesCode;
                movement.TransactionNo = entity.TransactionNo;
                movement.SequenceNo = entity.SequenceNo;
                movement.ItemID = entity.ItemID;
                movement.ExpiredDate = DateTime.Now.Date;
                movement.InitialStock = (balance != null) ? (balance.Balance + entity.ChargeQuantity) : 0;

                if (isApproval)
                {
                    movement.QuantityOut = entity.ChargeQuantity;
                    movement.QuantityIn = 0;
                }
                else
                {
                    movement.QuantityIn = entity.ChargeQuantity;
                    movement.QuantityOut = 0;
                }

                movement.SRItemUnit = entity.SRItemUnit;

                decimal cost = 0, purchase = 0;

                if (item.SRItemType == Reference.ItemType.Medical)
                {
                    var med = new ItemProductMedic();
                    med.LoadByPrimaryKey(item.ItemID);

                    cost = med.CostPrice ?? 0;
                    purchase = med.PriceInBaseUnit ?? 0;
                }
                else if (item.SRItemType == Reference.ItemType.NonMedical)
                {
                    var med = new ItemProductNonMedic();
                    med.LoadByPrimaryKey(item.ItemID);

                    cost = med.CostPrice ?? 0;
                    purchase = med.PriceInBaseUnit ?? 0;
                }

                movement.CostPrice = cost;
                movement.SalesPrice = entity.Price;
                movement.PurchasePrice = purchase;
                movement.LastUpdateDateTime = DateTime.Now;
                movement.LastUpdateByUserID = userID;

                return movement;
            }
            else
                return null;
        }

        public static ItemMovementCollection PrepareItemMovements(string transactionNo, string serviceUnitID, string locationID,
            string userID, bool isApproval, ItemBalanceCollection balanceCollection)
        {
            var movementCollection = new ItemMovementCollection();

            var consumptionQuery = new TransChargesItemConsumptionQuery("a");
            var chargesQuery = new TransChargesItemQuery("b");
            var query = new ItemQuery("y");
            consumptionQuery.Select
                (
                    consumptionQuery.TransactionNo,
                    consumptionQuery.SequenceNo,
                    consumptionQuery.DetailItemID,
                    consumptionQuery.Qty.Sum(),
                    consumptionQuery.SRItemUnit,
                    consumptionQuery.LastUpdateDateTime
                );
            consumptionQuery.InnerJoin(chargesQuery).On
                (
                    consumptionQuery.TransactionNo == chargesQuery.TransactionNo &
                    consumptionQuery.SequenceNo == chargesQuery.SequenceNo
                );
            consumptionQuery.InnerJoin(query).On(consumptionQuery.DetailItemID == query.ItemID);
            consumptionQuery.Where
                (
                    chargesQuery.TransactionNo == transactionNo,
                    chargesQuery.IsApprove == true,
                    query.SRItemType.In(Reference.ItemType.Medical, Reference.ItemType.NonMedical)
                );
            consumptionQuery.GroupBy
                (
                    consumptionQuery.TransactionNo,
                    consumptionQuery.SequenceNo,
                    consumptionQuery.DetailItemID,
                    consumptionQuery.SRItemUnit,
                    consumptionQuery.LastUpdateDateTime
                );

            var collection = new TransChargesItemConsumptionCollection();
            collection.Load(consumptionQuery);

            foreach (var entity in collection)
            {
                var movement = movementCollection.AddNew();
                movement.MovementDate = DateTime.Now;
                movement.ServiceUnitID = serviceUnitID;
                movement.LocationID = locationID;
                movement.TransactionCode = _consumptionCode;
                movement.TransactionNo = entity.TransactionNo;
                movement.SequenceNo = entity.SequenceNo;
                movement.ItemID = entity.DetailItemID;
                movement.ExpiredDate = DateTime.Now.Date;

                var balance = balanceCollection.FindByPrimaryKey(locationID, entity.DetailItemID);
                movement.InitialStock = (balance != null) ? (balance.Balance + entity.Qty) : 0;

                if (isApproval)
                {
                    movement.QuantityIn = entity.Qty;
                    movement.QuantityOut = 0;
                }
                else
                {
                    movement.QuantityIn = 0;
                    movement.QuantityOut = entity.Qty;
                }

                movement.SRItemUnit = entity.SRItemUnit;

                decimal cost = 0, purchase = 0;

                var item = new Item();
                item.LoadByPrimaryKey(entity.DetailItemID);

                if (item.SRItemType == Reference.ItemType.Medical)
                {
                    var med = new ItemProductMedic();
                    med.LoadByPrimaryKey(entity.DetailItemID);

                    cost = med.CostPrice ?? 0;
                    purchase = med.PriceInBaseUnit ?? 0;
                }
                else if (item.SRItemType == Reference.ItemType.NonMedical)
                {
                    var med = new ItemProductNonMedic();
                    med.LoadByPrimaryKey(entity.DetailItemID);

                    cost = med.CostPrice ?? 0;
                    purchase = med.PriceInBaseUnit ?? 0;
                }

                movement.CostPrice = cost;
                movement.SalesPrice = 0;
                movement.PurchasePrice = purchase;
                movement.LastUpdateDateTime = DateTime.Now;
                movement.LastUpdateByUserID = userID;
            }

            return movementCollection;
        }

        public static ItemMovementCollection PrepareItemMovements(string prescriptionNo, string transactionCode, string serviceUnitID,
            string locationID, string userID, bool isApproval, ItemBalanceCollection balanceCollection)
        {
            var collection = new TransPrescriptionItemCollection();
            var query = new TransPrescriptionItemQuery("a");
            query.Where
                (
                    query.PrescriptionNo == prescriptionNo,
                    query.IsApprove == true
                );
            collection.Load(query);

            var movementCollection = new ItemMovementCollection();

            foreach (var entity in collection)
            {
                var movement = movementCollection.AddNew();
                movement.MovementDate = DateTime.Now;
                movement.ServiceUnitID = serviceUnitID;
                movement.LocationID = locationID;
                movement.TransactionCode = transactionCode;
                movement.TransactionNo = prescriptionNo;
                movement.SequenceNo = entity.SequenceNo;

                if (entity.ItemInterventionID == string.Empty)
                    movement.ItemID = entity.ItemID;
                else
                    movement.ItemID = entity.ItemInterventionID;

                var balance = balanceCollection.FindByPrimaryKey(locationID, entity.ItemID);

                //Pembulatan qty
                var itemMedic = new ItemProductMedic();
                decimal resultQty = entity.ResultQty ?? 0;
                if (itemMedic.LoadByPrimaryKey(movement.ItemID))
                {
                    if (!(itemMedic.IsActualDeduct == false))
                        resultQty = System.Math.Ceiling(entity.ResultQty ?? 0);
                }

                movement.InitialStock = (balance != null) ? (balance.Balance + resultQty) : 0;

                if (isApproval)
                {
                    if (transactionCode.Equals(Reference.TransactionCode.PrescriptionReturn))
                    {
                        movement.QuantityIn = 0 - resultQty;
                        movement.QuantityOut = 0;
                    }
                    else
                    {
                        movement.QuantityIn = 0;
                        movement.QuantityOut = resultQty;
                    }
                }
                else
                {
                    //TODO: Cek lagi aturan proses UnApprov
                    if (transactionCode.Equals(Reference.TransactionCode.PrescriptionReturn))
                    {
                        movement.QuantityIn = 0;
                        movement.QuantityOut = 0 - resultQty;
                    }
                    else
                    {
                        movement.QuantityIn = 0 - resultQty;
                        movement.QuantityOut = 0;
                    }
                }

                movement.SRItemUnit = entity.SRItemUnit;

                decimal cost = 0, purchase = 0;

                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);

                if (item.SRItemType == Reference.ItemType.Medical)
                {
                    var med = new ItemProductMedic();
                    med.LoadByPrimaryKey(entity.ItemID);

                    cost = med.CostPrice ?? 0;
                    purchase = med.PriceInBaseUnit ?? 0;
                }
                else if (item.SRItemType == Reference.ItemType.NonMedical)
                {
                    var med = new ItemProductNonMedic();
                    med.LoadByPrimaryKey(entity.ItemID);

                    cost = med.CostPrice ?? 0;
                    purchase = med.PriceInBaseUnit ?? 0;
                }

                movement.CostPrice = cost;
                movement.SalesPrice = entity.Price;
                movement.PurchasePrice = purchase;
                movement.LastUpdateDateTime = DateTime.Now;
                movement.LastUpdateByUserID = userID;
            }

            return movementCollection;
        }

        public static ItemMovement PrepareItemMovement(string prescriptionNo, string sequenceNo, string transactionCode,
            string serviceUnitID, string locationID, string userID, bool isApproval, ItemBalance balance)
        {
            var entity = new TransPrescriptionItem();
            entity.LoadByPrimaryKey(prescriptionNo, sequenceNo);

            var movement = new ItemMovement();
            movement.MovementDate = DateTime.Now;
            movement.ServiceUnitID = serviceUnitID;
            movement.LocationID = locationID;
            movement.TransactionCode = transactionCode;
            movement.TransactionNo = prescriptionNo;
            movement.SequenceNo = entity.SequenceNo;

            if (entity.ItemInterventionID == string.Empty)
                movement.ItemID = entity.ItemID;
            else
                movement.ItemID = entity.ItemInterventionID;

            //Pembulatan qty
            var itemMedic = new ItemProductMedic();
            decimal resultQty = entity.ResultQty ?? 0;
            if (itemMedic.LoadByPrimaryKey(movement.ItemID))
            {
                if (!(itemMedic.IsActualDeduct == false))
                    resultQty = System.Math.Ceiling(entity.ResultQty ?? 0);
            }

            movement.InitialStock = (balance != null) ? (balance.Balance + entity.ResultQty) : 0;

            if (isApproval)
            {
                if (transactionCode.Equals(Reference.TransactionCode.PrescriptionReturn))
                {
                    movement.QuantityIn = 0 - resultQty;
                    movement.QuantityOut = 0;
                }
                else
                {
                    movement.QuantityIn = 0;
                    movement.QuantityOut = resultQty;
                }
            }
            else
            {
                //TODO: Cek lagi aturan proses UnApprov
                if (transactionCode.Equals(Reference.TransactionCode.PrescriptionReturn))
                {
                    movement.QuantityIn = 0;
                    movement.QuantityOut = 0 - resultQty;
                }
                else
                {
                    movement.QuantityIn = 0 - resultQty;
                    movement.QuantityOut = 0;
                }
            }

            movement.SRItemUnit = entity.SRItemUnit;

            decimal cost = 0, purchase = 0;

            var item = new Item();
            item.LoadByPrimaryKey(entity.ItemID);

            if (item.SRItemType == Reference.ItemType.Medical)
            {
                var med = new ItemProductMedic();
                med.LoadByPrimaryKey(entity.ItemID);

                cost = med.CostPrice ?? 0;
                purchase = med.PriceInBaseUnit ?? 0;
            }
            else if (item.SRItemType == Reference.ItemType.NonMedical)
            {
                var med = new ItemProductNonMedic();
                med.LoadByPrimaryKey(entity.ItemID);

                cost = med.CostPrice ?? 0;
                purchase = med.PriceInBaseUnit ?? 0;
            }

            movement.CostPrice = cost;
            movement.SalesPrice = entity.Price;
            movement.PurchasePrice = purchase;
            movement.LastUpdateDateTime = DateTime.Now;
            movement.LastUpdateByUserID = userID;

            return movement;
        }

        public static ItemMovementCollection PrepareItemMovements(ItemTransactionItemCollection collection, string serviceUnitID,
            string locationID, string userID, bool isApproval, ItemBalanceCollection balanceCollection)
        {
            var movementCollection = new ItemMovementCollection();

            foreach (var entity in collection)
            {
                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);
                if (item.SRItemType == Reference.ItemType.Medical || item.SRItemType == Reference.ItemType.NonMedical)
                {
                    var movement = movementCollection.AddNew();
                    movement.MovementDate = DateTime.Now;
                    movement.ServiceUnitID = serviceUnitID;
                    movement.LocationID = locationID;
                    movement.TransactionCode = _adjustmentCode;
                    movement.TransactionNo = entity.TransactionNo;
                    movement.SequenceNo = entity.SequenceNo;
                    movement.ItemID = entity.ItemID;

                    var balance = balanceCollection.FindByPrimaryKey(locationID, entity.ItemID);
                    movement.InitialStock = (balance != null) ? (balance.Balance + entity.Quantity) : 0;

                    if (isApproval)
                    {
                        if (entity.Quantity >= 0)
                        {
                            movement.QuantityIn = entity.Quantity;
                            movement.QuantityOut = 0;
                        }
                        else
                        {
                            movement.QuantityIn = 0;
                            movement.QuantityOut = Math.Abs(entity.Quantity ?? 0);
                        }
                    }
                    else
                    {
                        if (entity.Quantity >= 0)
                        {
                            movement.QuantityIn = 0;
                            movement.QuantityOut = entity.Quantity;
                        }
                        else
                        {
                            movement.QuantityIn = Math.Abs(entity.Quantity ?? 0);
                            movement.QuantityOut = 0;
                        }
                    }

                    movement.SRItemUnit = entity.SRItemUnit;

                    decimal cost = 0, purchase = 0;

                    if (item.SRItemType == Reference.ItemType.Medical)
                    {
                        var med = new ItemProductMedic();
                        med.LoadByPrimaryKey(entity.ItemID);

                        cost = med.CostPrice ?? 0;
                        purchase = med.PriceInBaseUnit ?? 0;
                    }
                    else if (item.SRItemType == Reference.ItemType.NonMedical)
                    {
                        var med = new ItemProductNonMedic();
                        med.LoadByPrimaryKey(entity.ItemID);

                        cost = med.CostPrice ?? 0;
                        purchase = med.PriceInBaseUnit ?? 0;
                    }

                    movement.CostPrice = cost;
                    movement.SalesPrice = 0;
                    movement.PurchasePrice = purchase;
                    movement.LastUpdateDateTime = DateTime.Now;
                    movement.LastUpdateByUserID = userID;
                }
            }

            return movementCollection;
        }

        public static ItemMovementCollection PrepareItemMovementsForStockOpname(ItemTransactionItemCollection collection, string serviceUnitID,
            string locationID, string userID, ItemBalanceCollection balanceCollection)
        {
            var movementCollection = new ItemMovementCollection();

            foreach (var entity in collection)
            {
                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);
                if (item.SRItemType == Reference.ItemType.Medical || item.SRItemType == Reference.ItemType.NonMedical)
                {
                    var movement = movementCollection.AddNew();
                    movement.MovementDate = DateTime.Now;
                    movement.ServiceUnitID = serviceUnitID;
                    movement.LocationID = locationID;
                    movement.TransactionCode = _opnameCode;
                    movement.TransactionNo = entity.TransactionNo;
                    movement.SequenceNo = entity.SequenceNo;
                    movement.ItemID = entity.ItemID;

                    var balance = balanceCollection.FindByPrimaryKey(locationID, entity.ItemID);
                    movement.InitialStock = (balance != null) ? (balance.Balance + entity.Quantity) : 0;

                    movement.QuantityIn = 0;
                    movement.QuantityOut = 0;
                    // Bandingkan dgn ItemStockOpnamePrevBalance bila nilai stock opname lebih besar maka simpan di QtyIn
                    ItemStockOpnamePrevBalance prevBalance = new ItemStockOpnamePrevBalance();
                    if (prevBalance.LoadByPrimaryKey(entity.TransactionNo, entity.ItemID))
                    {
                        if (prevBalance.Quantity < entity.Quantity)
                        {
                            if (prevBalance.Quantity < 0)
                            {
                                // sistem -10, fisik 5 maka qty in 15
                                movement.QuantityIn = 0 - prevBalance.Quantity + entity.Quantity;
                            }
                            else
                            {
                                // sistem 5, fisik 10 maka qty in 5
                                movement.QuantityIn = entity.Quantity - prevBalance.Quantity;
                            }
                        }
                        else
                        {
                            // sistem 10, fisik 5 maka qty out 5
                            movement.QuantityOut = prevBalance.Quantity - entity.Quantity;
                        }
                    }
                    else
                    {
                        // Bila tidak ada di prev balance maka dianggap Qty Baru
                        movement.QuantityIn = entity.Quantity;
                    }

                    movement.SRItemUnit = entity.SRItemUnit;

                    decimal cost = 0, purchase = 0;

                    if (item.SRItemType == Reference.ItemType.Medical)
                    {
                        var med = new ItemProductMedic();
                        med.LoadByPrimaryKey(entity.ItemID);

                        cost = med.CostPrice ?? 0;
                        purchase = med.PriceInBaseUnit ?? 0;
                    }
                    else if (item.SRItemType == Reference.ItemType.NonMedical)
                    {
                        var med = new ItemProductNonMedic();
                        med.LoadByPrimaryKey(entity.ItemID);

                        cost = med.CostPrice ?? 0;
                        purchase = med.PriceInBaseUnit ?? 0;
                    }

                    movement.CostPrice = cost;
                    movement.SalesPrice = 0;
                    movement.PurchasePrice = purchase;
                    movement.LastUpdateDateTime = DateTime.Now;
                    movement.LastUpdateByUserID = userID;
                }
            }

            return movementCollection;
        }

        public override void Save()
        {
            using (var trans = new esTransactionScope())
            {

                var cudType = "C";
                if (this.es.IsDeleted)
                    cudType = "D";
                else if (es.IsModified)
                    cudType = "U";

                var loc = new Location();
                loc.LoadByPrimaryKey(LocationID);
                var stockGroup = loc.SRStockGroup ?? string.Empty;

                var isMinMaxItemBalanceAutoUpdate = loc.IsAutoUpdateStockMinMax ?? false;

                var startDateForCalcMinBalance = DateTime.Today;
                var startDateForCalcMaxBalance = DateTime.Today;

                if (isMinMaxItemBalanceAutoUpdate)
                {
                    var periodForMin =
                        AppParameter.GetParameterValue(AppParameter.ParameterItem.PeriodDayHistUsingForCalcMinBalance)
                            .ToInt();
                    startDateForCalcMinBalance = DateTime.Today.AddDays(0 - periodForMin);

                    var periodForMax =
                        AppParameter.GetParameterValue(AppParameter.ParameterItem.PeriodDayHistUsingForCalcMaxBalance)
                            .ToInt();
                    startDateForCalcMaxBalance = DateTime.Today.AddDays(0 - periodForMax);
                }

                var startDateForCalcMinBalPerStockGroup = DateTime.Today;
                var startDateForCalcMaxBalPerStockGroup = DateTime.Today;

                var isMinMaxItemBalanceByStockGroupAutoUpdate = AppParameter.IsYes(AppParameter.ParameterItem.IsMinMaxItemBalanceByStockGroupAutoUpdate);

                if (isMinMaxItemBalanceByStockGroupAutoUpdate)
                {
                    var periodForMinPerStockGroup =
                        AppParameter.GetParameterValue(AppParameter.ParameterItem.PeriodDayHistUsingForCalcMinBalPerStockGroup)
                            .ToInt();
                    startDateForCalcMinBalPerStockGroup = DateTime.Today.AddDays(0 - periodForMinPerStockGroup);

                    var periodForMaxPerStockGroup =
                        AppParameter.GetParameterValue(AppParameter.ParameterItem.PeriodDayHistUsingForCalcMaxBalPerStockGroup)
                            .ToInt();
                    startDateForCalcMaxBalPerStockGroup = DateTime.Today.AddDays(0 - periodForMaxPerStockGroup);
                }

                UpdateRelatedTable(cudType, this, isMinMaxItemBalanceAutoUpdate, isMinMaxItemBalanceByStockGroupAutoUpdate, startDateForCalcMinBalance, startDateForCalcMaxBalance, startDateForCalcMinBalPerStockGroup, startDateForCalcMaxBalPerStockGroup, stockGroup);

                //UpdateInitialStockAmount(cudType);

                //if (this.TransactionCode != "045" && this.TransactionCode != "047" && this.TransactionCode != "050" && this.TransactionCode != "055")
                if (this.TransactionCode != "045" && this.TransactionCode != "047")
                    UpdateInventoryBalance(cudType, this);

                base.Save();
                trans.Complete();
            }
        }

        //public void UpdateInitialStockAmount(string cudType)
        //{
        //    if (cudType == "D")
        //    {
        //        //TODO: UpdateInitialStockAmount at Delete
        //    }
        //    else if (cudType == "C")
        //    {
        //        // UpdateInitialStockAmount
        //        var prevMovement = new ItemMovement();
        //        prevMovement.Query.Where(prevMovement.Query.LocationID == LocationID, prevMovement.Query.ItemID == ItemID, prevMovement.Query.MovementDate < MovementDate);
        //        prevMovement.Query.es.Top = 1;
        //        prevMovement.Query.OrderBy(prevMovement.Query.MovementDate.Descending);
        //        if (prevMovement.Query.Load())
        //            InitialStockAmount = prevMovement.InitialStockAmount + (prevMovement.CostPrice * (prevMovement.QuantityIn - prevMovement.QuantityOut));
        //        else
        //            InitialStockAmount = 0;
        //    }
        //    else if (cudType == "U")
        //    {
        //        //TODO: UpdateInitialStockAmount at Update
        //    }
        //}

        internal static void UpdateRelatedTable(string cudType, ItemMovement itemMovement, bool isMinMaxItemBalanceAutoUpdate, bool isMinMaxItemBalanceByStockGroupAutoUpdate, DateTime startDateForCalcMinBalance, DateTime startDateForCalcMaxBalance, DateTime startDateForCalcMinBalPerStockGroup, DateTime startDateForCalcMaxBalPerStockGroup, string stockGroup)
        {
            decimal? qtyOut = 0;
            decimal? qtyIn = 0;
            decimal? qtyOutAmt = 0;
            decimal? qtyInAmt = 0;
            if (cudType == "D")
            {
                // Kurangi
                qtyOut = 0 - itemMovement.QuantityOut;
                qtyIn = 0 - itemMovement.QuantityIn;

                qtyOutAmt = 0 - (itemMovement.QuantityOut * itemMovement.CostPrice);
                qtyInAmt = 0 - (itemMovement.QuantityIn * itemMovement.CostPrice);
            }
            else if (cudType == "C" || cudType == "U")
            {
                // Tambah / Reupdate
                qtyOut = itemMovement.QuantityOut;
                qtyIn = itemMovement.QuantityIn;
                qtyOutAmt = itemMovement.QuantityOut * itemMovement.CostPrice;
                qtyInAmt = itemMovement.QuantityIn * itemMovement.CostPrice;

                if (cudType == "U")
                {
                    // Kurangi dulu jumlahnya dg qty sebelum edit 
                    qtyOut = qtyOut - itemMovement.GetOriginalColumnValue("QuantityOut").ToDecimal();
                    qtyIn = qtyIn - itemMovement.GetOriginalColumnValue("QuantityInt").ToDecimal();
                    qtyOutAmt = qtyOutAmt - (itemMovement.GetOriginalColumnValue("QuantityOut").ToDecimal() * itemMovement.GetOriginalColumnValue("CostPrice").ToDecimal());
                    qtyInAmt = qtyInAmt - (itemMovement.GetOriginalColumnValue("QuantityInt").ToDecimal() * itemMovement.GetOriginalColumnValue("CostPrice").ToDecimal());

                }
            }
            // ItemMovementPerDate
            UpateItemMovementPerDate(itemMovement, qtyIn, qtyOut);

            //// ItemBalance
            //UpdateItemBalance(itemMovement, qtyIn, qtyOut);

            //// ItemBalanceDetail
            //UpdateItemBalanceDetail(itemMovement, qtyIn, qtyOut);

            if (!string.IsNullOrEmpty(stockGroup))
            {
                // Update pengeluaran item
                if (itemMovement.TransactionCode == Reference.TransactionCode.Prescription || itemMovement.TransactionCode == Reference.TransactionCode.PrescriptionReturn || itemMovement.TransactionCode == Reference.TransactionCode.Charges)
                    UpdateItemSalesPerDate(itemMovement, qtyOut - qtyIn, stockGroup);
                else
                    UpdateItemSalesPerDate(itemMovement, qtyOut, stockGroup);

                // Update Balance by Stock Group
                UpdateItemBalanceByStockGroup(itemMovement, qtyIn, qtyOut, stockGroup);
            }

            // Update Minimum & Maximum Balance
            if (isMinMaxItemBalanceAutoUpdate)
            {
                UpdateMaxMinItemBalance(itemMovement, startDateForCalcMinBalance, startDateForCalcMaxBalance);
            }

            if (isMinMaxItemBalanceByStockGroupAutoUpdate)
            {
                UpdateMaxMinItemBalanceByStockGroup(itemMovement, startDateForCalcMinBalPerStockGroup, startDateForCalcMaxBalPerStockGroup, stockGroup);
            }
        }

        private static void UpdateMaxMinItemBalance(ItemMovement itemMovement, DateTime startDateForCalcMinBalance, DateTime startDateForCalcMaxBalance)
        {
            // 01. Max Min per lokasi
            var ib = new ItemBalance();
            if (ib.LoadByPrimaryKey(itemMovement.LocationID, itemMovement.ItemID))
            {
                var qr = new ItemMovementPerDateQuery();
                qr.Select(qr.QuantityOut.Sum().As("QuantityOut"));
                qr.Where(qr.LocationID == itemMovement.LocationID && qr.ItemID == itemMovement.ItemID &&
                         qr.MovementDate >= startDateForCalcMinBalance);
                var dtb = qr.LoadDataTable();

                if (dtb.Rows != null && dtb.Rows.Count > 0)
                    ib.Minimum = (dtb.Rows[0][0]).ToInt();


                // Update Maximum
                qr = new ItemMovementPerDateQuery();
                qr.Select(qr.QuantityOut.Sum().As("QuantityOut"));
                qr.Where(qr.LocationID == itemMovement.LocationID && qr.ItemID == itemMovement.ItemID &&
                         qr.MovementDate >= startDateForCalcMaxBalance);
                dtb = qr.LoadDataTable();

                if (dtb.Rows != null && dtb.Rows.Count > 0)
                    ib.Maximum = (dtb.Rows[0][0]).ToInt();

                ib.Save();
            }
        }

        private static void UpdateMaxMinItemBalanceByStockGroup(ItemMovement itemMovement, DateTime startDateForCalcMinBalance, DateTime startDateForCalcMaxBalance, string stockGroup)
        {
            if (!string.IsNullOrEmpty(stockGroup))
            {
                // 02. Max Min per Stock Group
                var ibsg = new ItemBalanceByStockGroup();
                if (ibsg.LoadByPrimaryKey(stockGroup, itemMovement.ItemID))
                {
                    var qr = new ItemSalesPerDateQuery();
                    qr.Select(qr.QuantityOut.Sum().As("QuantityOut"));
                    qr.Where(qr.SRStockGroup == stockGroup && qr.ItemID == itemMovement.ItemID &&
                             qr.MovementDate >= startDateForCalcMinBalance);
                    var dtb = qr.LoadDataTable();

                    if (dtb.Rows != null && dtb.Rows.Count > 0)
                        ibsg.Minimum = (dtb.Rows[0][0]).ToInt();


                    // Update Maximum
                    qr = new ItemSalesPerDateQuery();
                    qr.Select(qr.QuantityOut.Sum().As("QuantityOut"));
                    qr.Where(qr.SRStockGroup == stockGroup && qr.ItemID == itemMovement.ItemID &&
                             qr.MovementDate >= startDateForCalcMaxBalance);
                    dtb = qr.LoadDataTable();

                    if (dtb.Rows != null && dtb.Rows.Count > 0)
                        ibsg.Maximum = (dtb.Rows[0][0]).ToInt();

                    ibsg.Save();
                }
            }
        }


        private static void UpdateItemBalanceByStockGroup(ItemMovement itemMovement, decimal? qtyIn, decimal? qtyOut, string stockGroup)
        {
            var ibs = new ItemBalanceByStockGroup();
            if (ibs.LoadByPrimaryKey(stockGroup, itemMovement.ItemID))
            {
                ibs.Balance = ibs.Balance - qtyOut + qtyIn;
            }
            else
            {
                // Ambil total stock terakhir utk SRStockGroup & item bersangkutan
                var loc = new LocationQuery();
                loc.Where(loc.SRStockGroup == stockGroup);
                loc.Select(loc.LocationID);

                var dtbLoc = loc.LoadDataTable();
                decimal? stock = 0;
                foreach (DataRow row in dtbLoc.Rows)
                {
                    var lastStock = new ItemMovementQuery();
                    lastStock.Where(lastStock.LocationID == row[0], lastStock.ItemID == itemMovement.ItemID);
                    lastStock.Select(
                        (lastStock.InitialStock + lastStock.QuantityIn - lastStock.QuantityOut).As("LastStock"));
                    lastStock.es.Top = 1;
                    lastStock.OrderBy(lastStock.MovementDate.Descending);
                    var dtbLastStock = lastStock.LoadDataTable();
                    if (dtbLastStock.Rows.Count > 0)
                        stock += (dtbLastStock.Rows[0][0]).ToDecimal();
                }


                ibs = new ItemBalanceByStockGroup();
                ibs.SRStockGroup = stockGroup;
                ibs.ItemID = itemMovement.ItemID;
                ibs.Balance = stock - qtyOut + qtyIn;
            }
            ibs.Save();
        }

        private static void UpdateItemSalesPerDate(ItemMovement itemMovement, decimal? qtyOut, string stockGroup)
        {
            if (itemMovement.TransactionCode == Reference.TransactionCode.Prescription
                || itemMovement.TransactionCode == Reference.TransactionCode.PrescriptionReturn
                || itemMovement.TransactionCode == Reference.TransactionCode.Charges
                || itemMovement.TransactionCode == Reference.TransactionCode.InventoryIssueOut
                || itemMovement.TransactionCode == Reference.TransactionCode.StockAdjustment
                || itemMovement.TransactionCode == Reference.TransactionCode.StockTaking)
            {
                var sales = new ItemSalesPerDate();
                if (sales.LoadByPrimaryKey(itemMovement.MovementDate.Value.Date, stockGroup, itemMovement.ItemID,
                    itemMovement.ServiceUnitID, itemMovement.LocationID))
                {
                    sales.QuantityOut = sales.QuantityOut + qtyOut;
                }
                else
                {
                    // Insert new record
                    sales = new ItemSalesPerDate
                    {
                        MovementDate = itemMovement.MovementDate.Value.Date,
                        SRStockGroup = stockGroup,
                        ServiceUnitID = itemMovement.ServiceUnitID,
                        LocationID = itemMovement.LocationID,
                        ItemID = itemMovement.ItemID,
                        QuantityOut = qtyOut
                    };
                }
                sales.Save();
            }
        }

        private static void UpdateItemBalanceDetail(ItemMovement itemMovement, decimal? qtyIn, decimal? qtyOut)
        {
            var ibd = new ItemBalanceDetail();
            if (ibd.LoadByPrimaryKey(itemMovement.MovementDate.Value.Date, itemMovement.ItemID, itemMovement.LocationID, itemMovement.TransactionNo))
            {
                ibd.Balance = ibd.Balance + qtyIn - qtyOut;
            }
            else
            {
                ibd = new ItemBalanceDetail
                {
                    LocationID = itemMovement.LocationID,
                    ItemID = itemMovement.ItemID,
                    ReferenceNo = itemMovement.TransactionNo,
                    TransactionCode = itemMovement.TransactionCode,
                    BalanceDate = itemMovement.MovementDate,
                    Booking = 0,
                    //Price = itemMovement.Price + (AppParameter.IsYes(AppParameter.ParameterItem.IsInventoryIncludeTax)  ? (item.Price * (entity.TaxPercentage / 100)) : 0);
                    Balance = qtyIn - qtyOut
                };


            }
            ibd.Save();
        }

        private static void UpdateItemBalance(ItemMovement itemMovement, decimal? qtyIn, decimal? qtyOut)
        {
            var ib = new ItemBalance();
            if (ib.LoadByPrimaryKey(itemMovement.LocationID, itemMovement.ItemID))
            {
                ib.Balance = ib.Balance + qtyIn - qtyOut;
            }
            else
            {
                ib = new ItemBalance
                {
                    LocationID = itemMovement.LocationID,
                    ItemID = itemMovement.ItemID,
                    Balance = qtyIn - qtyOut
                };
            }
            ib.Save();
        }

        private static void UpateItemMovementPerDate(ItemMovement itemMovement, decimal? qtyIn, decimal? qtyOut)
        {
            var imp = new ItemMovementPerDate();
            if (imp.LoadByPrimaryKey(itemMovement.MovementDate.Value.Date, itemMovement.LocationID, itemMovement.ItemID))
            {
                imp.QuantityIn = imp.QuantityIn + qtyIn;
                imp.QuantityOut = imp.QuantityOut + qtyOut;
            }
            else
            {
                imp = new ItemMovementPerDate
                {
                    MovementDate = itemMovement.MovementDate.Value.Date,
                    LocationID = itemMovement.LocationID,
                    ItemID = itemMovement.ItemID,
                    QuantityIn = qtyIn,
                    QuantityOut = qtyOut
                };
            }
            imp.Save();
        }

        public static void UpdateInventoryBalance(string cudType, ItemMovement itemMovement)
        {
            if (cudType == "D")
            {
                // Kurangi jumlahnya
                var qtyOut = 0 - itemMovement.QuantityOut;
                var qtyIn = 0 - itemMovement.QuantityIn;

                UpdateCummulativeInventoryBalance(itemMovement, qtyIn, qtyOut);
            }
            else if (cudType == "C" || cudType == "U")
            {
                var qtyOut = itemMovement.QuantityOut;
                var qtyIn = itemMovement.QuantityIn;

                if (cudType == "U")
                {
                    // Kurangi dulu jumlahnya dg qty sebelum edit 
                    qtyOut = qtyOut - itemMovement.GetOriginalColumnValue("QuantityOut").ToDecimal();
                    qtyIn = qtyIn - itemMovement.GetOriginalColumnValue("QuantityInt").ToDecimal();
                }
                UpdateCummulativeInventoryBalance(itemMovement, qtyIn, qtyOut);
            }
        }

        private static void UpdateCummulativeInventoryBalance(ItemMovement itemMovement, decimal? qtyIn, decimal? qtyOut)
        {
            DateTime periodDate = new DateTime(itemMovement.MovementDate.Value.Year, itemMovement.MovementDate.Value.Month, 1);
            string itemId = itemMovement.ItemID;
            string locationId = itemMovement.LocationID;

            var ib = new InventoryBalance();
            if (!ib.LoadByPrimaryKey(periodDate, locationId, itemId))
            {
                ib.AddNew();

                ib.PeriodDate = periodDate;
                ib.LocationID = locationId;
                ib.ItemID = itemId;

                var qbef = new InventoryBalanceQuery();
                qbef.Where(qbef.PeriodDate < periodDate, qbef.LocationID == locationId, qbef.ItemID == itemId);
                qbef.OrderBy(qbef.PeriodDate.Descending);
                qbef.es.Top = 1;

                var bef = new InventoryBalance();
                if (bef.Load(qbef))
                {
                    ib.BeginningBalance = (bef.BeginningBalance ?? 0) + (bef.Balance ?? 0);
                    ib.InitialQuantity = (bef.InitialQuantity ?? 0) + (bef.QuantityIn ?? 0) - (bef.QuantityOut ?? 0);
                }
                else
                {
                    ib.BeginningBalance = 0;
                    ib.InitialQuantity = 0;
                }

                ib.QuantityIn = 0;
                ib.QuantityOut = 0;
                ib.BalanceIn = 0;
                ib.BalanceOut = 0;
                ib.Balance = 0;
            }

            ib.QuantityIn += qtyIn;
            ib.QuantityOut += qtyOut;

            decimal price = itemMovement.CostPrice ?? 0;
            //if (itemMovement.TransactionCode == "040" || itemMovement.TransactionCode == "043")
            if (itemMovement.TransactionCode == "040")
                price = itemMovement.PurchasePrice ?? 0;

            ib.BalanceIn += (qtyIn * price);
            ib.BalanceOut += (qtyOut * price);
            ib.Balance += (ib.BalanceIn - ib.BalanceOut);
            ib.CostPrice = itemMovement.CostPrice;
            ib.LastUpdateByUserID = itemMovement.LastUpdateByUserID;
            ib.LastUpdateDateTime = DateTime.Now;

            ib.Save();
        }

    }

    public partial class ItemMovementCollection
    {
        private void UpdateRelatedTable(string cudType, bool isMinMaxItemBalanceByStockGroupAutoUpdate, DateTime startDateForCalcMinBalance, DateTime startDateForCalcMaxBalance, DateTime startDateForCalcMinBalPerStockGroup, DateTime startDateForCalcMaxBalPerStockGroup)
        {
            var locationID = string.Empty;
            var stockGroup = string.Empty;
            var isMinMaxItemBalanceAutoUpdate = false;
            foreach (ItemMovement itemMovement in this)
            {
                //itemMovement.UpdateInitialStockAmount(cudType);

                if (locationID != itemMovement.LocationID)
                {
                    var loc = new Location();
                    loc.LoadByPrimaryKey(itemMovement.LocationID);

                    locationID = itemMovement.LocationID;
                    stockGroup = loc.SRStockGroup ?? string.Empty;
                    isMinMaxItemBalanceAutoUpdate = loc.IsAutoUpdateStockMinMax ?? false;
                }
                ItemMovement.UpdateRelatedTable(cudType, itemMovement, isMinMaxItemBalanceAutoUpdate, isMinMaxItemBalanceByStockGroupAutoUpdate, startDateForCalcMinBalance, startDateForCalcMaxBalance, startDateForCalcMinBalPerStockGroup, startDateForCalcMaxBalPerStockGroup, stockGroup);
            }
        }

        private void UpdateInventoryBalance(string cudType)
        {
            foreach (ItemMovement itemMovement in this)
            {
                //if (itemMovement.TransactionCode != "045" && itemMovement.TransactionCode != "047" && itemMovement.TransactionCode != "050" && itemMovement.TransactionCode != "055")
                if (itemMovement.TransactionCode != "045" && itemMovement.TransactionCode != "047")
                    ItemMovement.UpdateInventoryBalance(cudType, itemMovement);
            }
        }

        public override void Save()
        {
            using (var trans = new esTransactionScope())
            {

                var startDateForCalcMinBalance = DateTime.Today;
                var startDateForCalcMaxBalance = DateTime.Today;


                var periodForMin =
                    AppParameter.GetParameterValue(AppParameter.ParameterItem.PeriodDayHistUsingForCalcMinBalance)
                        .ToInt();
                startDateForCalcMinBalance = DateTime.Today.AddDays(0 - periodForMin);

                var periodForMax =
                    AppParameter.GetParameterValue(AppParameter.ParameterItem.PeriodDayHistUsingForCalcMaxBalance)
                        .ToInt();
                startDateForCalcMaxBalance = DateTime.Today.AddDays(0 - periodForMax);


                var startDateForCalcMinBalPerStockGroup = DateTime.Today;
                var startDateForCalcMaxBalPerStockGroup = DateTime.Today;

                var isMinMaxItemBalanceByStockGroupAutoUpdate = AppParameter.IsYes(AppParameter.ParameterItem.IsMinMaxItemBalanceByStockGroupAutoUpdate);

                if (isMinMaxItemBalanceByStockGroupAutoUpdate)
                {
                    var periodForMinPerStockGroup =
                        AppParameter.GetParameterValue(AppParameter.ParameterItem.PeriodDayHistUsingForCalcMinBalPerStockGroup)
                            .ToInt();
                    startDateForCalcMinBalPerStockGroup = DateTime.Today.AddDays(0 - periodForMinPerStockGroup);

                    var periodForMaxPerStockGroup =
                        AppParameter.GetParameterValue(AppParameter.ParameterItem.PeriodDayHistUsingForCalcMaxBalPerStockGroup)
                            .ToInt();
                    startDateForCalcMaxBalPerStockGroup = DateTime.Today.AddDays(0 - periodForMaxPerStockGroup);
                }

                DataViewRowState state = this.RowStateFilter;


                // 1) Deletes
                this.RowStateFilter = DataViewRowState.Deleted;
                UpdateRelatedTable("D", isMinMaxItemBalanceByStockGroupAutoUpdate,
                    startDateForCalcMinBalance, startDateForCalcMaxBalance, startDateForCalcMinBalPerStockGroup, startDateForCalcMaxBalPerStockGroup);
                UpdateInventoryBalance("D");


                // 2) Update
                this.RowStateFilter = DataViewRowState.ModifiedOriginal;
                UpdateRelatedTable("U", isMinMaxItemBalanceByStockGroupAutoUpdate,
    startDateForCalcMinBalance, startDateForCalcMaxBalance, startDateForCalcMinBalPerStockGroup, startDateForCalcMaxBalPerStockGroup);
                UpdateInventoryBalance("U");

                // 3) Insert
                this.RowStateFilter = DataViewRowState.Added;
                UpdateRelatedTable("C", isMinMaxItemBalanceByStockGroupAutoUpdate,
    startDateForCalcMinBalance, startDateForCalcMaxBalance, startDateForCalcMinBalPerStockGroup, startDateForCalcMaxBalPerStockGroup);
                UpdateInventoryBalance("C");


                this.RowStateFilter = state;
                base.Save();
                trans.Complete();
            }
        }
    }
}

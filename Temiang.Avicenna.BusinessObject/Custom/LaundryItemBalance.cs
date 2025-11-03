using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class LaundryItemBalance
    {
        private const string _receivedCode = "A_RC";
        private const string _processCode = "A_WS";
        private const string _sortingCode = "A_SR";
        private const string _distributionCode = "A_DT";
        private const string _returnCode = "A_RT";

        private const string _receivedReverseCode = "U_RC";
        private const string _processReverseCode = "U_WS";
        private const string _sortingReverseCode = "U_SR";
        private const string _distributionReverseCode = "U_DT";
        private const string _returnReverseCode = "U_RT";

        #region approval
        public static void PrepareBalancesForReceived(LaundryReceivedItemCollection coll, string fromUnitId, string toUnitId, string userId,
            ref LaundryItemBalanceCollection itemBalancesFrom, ref LaundryItemBalanceCollection itemBalancesTo, ref LaundryItemMovementCollection itemMovements)
        {
            var items = (coll.Select(i => i.ItemID)).Distinct();

            if (!items.Any())
                return;

            //balance from
            itemBalancesFrom.Query.Where
                (
                    itemBalancesFrom.Query.ServiceUnitID == fromUnitId,
                    itemBalancesFrom.Query.IsCleanLaundry == true,
                    itemBalancesFrom.Query.ItemID.In(items)
                );
            itemBalancesFrom.LoadAll();

            //balance to
            itemBalancesTo.Query.Where
                (
                    itemBalancesTo.Query.ServiceUnitID == toUnitId,
                    itemBalancesTo.Query.IsCleanLaundry == false,
                    itemBalancesTo.Query.ItemID.In(items)
                );
            itemBalancesTo.LoadAll();

            foreach (var entity in coll)
            {
                decimal qty = entity.Qty ?? 0;

                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);

                #region balance from
                var balanceFrom = itemBalancesFrom.FindByPrimaryKey(fromUnitId, true, entity.ItemID);

                decimal qtyDiff = 0;
                if (balanceFrom != null)
                {
                    if (balanceFrom.Balance >= qty)
                        balanceFrom.Balance -= qty;
                    else
                    {
                        qtyDiff = qty - (balanceFrom.Balance ?? 0);
                        balanceFrom.Balance = 0;
                    }

                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceFrom = itemBalancesFrom.AddNew();
                    balanceFrom.ServiceUnitID = fromUnitId;
                    balanceFrom.IsCleanLaundry = true;
                    balanceFrom.ItemID = entity.ItemID;
                    balanceFrom.Balance = 0;
                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                    qtyDiff = qty;
                }
                #endregion

                #region balance to
                var balanceTo = itemBalancesTo.FindByPrimaryKey(toUnitId, false, entity.ItemID);
                if (balanceTo != null)
                {
                    balanceTo.Balance += qty;
                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceTo = itemBalancesTo.AddNew();
                    balanceTo.ServiceUnitID = toUnitId;
                    balanceTo.IsCleanLaundry = false;
                    balanceTo.ItemID = entity.ItemID;
                    balanceTo.Balance = qty;
                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                }
                #endregion

                #region movement from
                var movement = itemMovements.AddNew();
                movement.MovementDate = Utils.NowAtSqlServer();
                movement.TransactionNo = entity.ReceivedNo;
                movement.TransactionCode = _receivedCode;
                movement.ServiceUnitID = fromUnitId;
                movement.IsCleanLaundry = true;
                movement.ItemID = entity.ItemID;
                if (qtyDiff == 0)
                    movement.InitialQty = balanceFrom.Balance + qty;
                else
                    movement.InitialQty = qty;
                movement.QtyIn = 0;
                movement.QtyOut = qty;
                movement.LastUpdateDateTime = Utils.NowAtSqlServer();
                movement.LastUpdateByUserID = userId;
                #endregion

                #region movement to
                var movementTo = itemMovements.AddNew();
                movementTo.MovementDate = Utils.NowAtSqlServer();
                movementTo.TransactionNo = entity.ReceivedNo;
                movementTo.TransactionCode = _receivedCode;
                movementTo.ServiceUnitID = toUnitId;
                movementTo.IsCleanLaundry = false;
                movementTo.ItemID = entity.ItemID;
                movementTo.InitialQty = balanceTo.Balance - qty;
                movementTo.QtyIn = qty;
                movementTo.QtyOut = 0;
                movementTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                movementTo.LastUpdateByUserID = userId;
                #endregion
            }
        }

        public static void PrepareBalancesForProcess(LaunderedProcessItemCentralizationCollection coll, string fromUnitId, string toUnitId, string userId,
            ref LaundryItemBalanceCollection itemBalancesFrom, ref LaundryItemBalanceCollection itemBalancesTo, ref LaundryItemMovementCollection itemMovements)
        {
            var items = (coll.Select(i => i.ItemID)).Distinct();

            if (!items.Any())
                return;

            //balance from
            itemBalancesFrom.Query.Where
                (
                    itemBalancesFrom.Query.ServiceUnitID == fromUnitId,
                    itemBalancesFrom.Query.IsCleanLaundry == false,
                    itemBalancesFrom.Query.ItemID.In(items)
                );
            itemBalancesFrom.LoadAll();

            //balance to
            itemBalancesTo.Query.Where
                (
                    itemBalancesTo.Query.ServiceUnitID == toUnitId,
                    itemBalancesTo.Query.IsCleanLaundry == true,
                    itemBalancesTo.Query.ItemID.In(items)
                );
            itemBalancesTo.LoadAll();

            foreach (var entity in coll)
            {
                decimal qty = entity.Qty ?? 0;

                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);

                #region balance from
                var balanceFrom = itemBalancesFrom.FindByPrimaryKey(fromUnitId, false, entity.ItemID);

                decimal qtyDiff = 0;
                if (balanceFrom != null)
                {
                    if (balanceFrom.Balance >= qty)
                        balanceFrom.Balance -= qty;
                    else
                    {
                        qtyDiff = qty - (balanceFrom.Balance ?? 0);
                        balanceFrom.Balance = 0;
                    }

                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceFrom = itemBalancesFrom.AddNew();
                    balanceFrom.ServiceUnitID = fromUnitId;
                    balanceFrom.IsCleanLaundry = false;
                    balanceFrom.ItemID = entity.ItemID;
                    balanceFrom.Balance = 0;
                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                    qtyDiff = qty;
                }
                #endregion

                #region balance to
                var balanceTo = itemBalancesTo.FindByPrimaryKey(toUnitId, true, entity.ItemID);
                if (balanceTo != null)
                {
                    balanceTo.Balance += qty;
                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceTo = itemBalancesTo.AddNew();
                    balanceTo.ServiceUnitID = toUnitId;
                    balanceTo.IsCleanLaundry = true;
                    balanceTo.ItemID = entity.ItemID;
                    balanceTo.Balance = qty;
                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                }
                #endregion

                #region movement from
                var movement = itemMovements.AddNew();
                movement.MovementDate = Utils.NowAtSqlServer();
                movement.TransactionNo = entity.ProcessNo;
                movement.TransactionCode = _processCode;
                movement.ServiceUnitID = fromUnitId;
                movement.IsCleanLaundry = false;
                movement.ItemID = entity.ItemID;
                if (qtyDiff == 0)
                    movement.InitialQty = balanceFrom.Balance + qty;
                else
                    movement.InitialQty = qty;
                movement.QtyIn = 0;
                movement.QtyOut = qty;
                movement.LastUpdateDateTime = Utils.NowAtSqlServer();
                movement.LastUpdateByUserID = userId;
                #endregion

                #region movement to
                var movementTo = itemMovements.AddNew();
                movementTo.MovementDate = Utils.NowAtSqlServer();
                movementTo.TransactionNo = entity.ProcessNo;
                movementTo.TransactionCode = _processCode;
                movementTo.ServiceUnitID = toUnitId;
                movementTo.IsCleanLaundry = true;
                movementTo.ItemID = entity.ItemID;
                movementTo.InitialQty = balanceTo.Balance - qty;
                movementTo.QtyIn = qty;
                movementTo.QtyOut = 0;
                movementTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                movementTo.LastUpdateByUserID = userId;
                #endregion
            }
        }

        public static void PrepareBalancesForSorting(LaundrySortingProcessItemCollection coll, string fromUnitId, string toUnitId, string userId,
            ref LaundryItemBalanceCollection itemBalancesFrom, ref LaundryItemBalanceCollection itemBalancesTo, ref LaundryItemMovementCollection itemMovements)
        {
            var items = (coll.Select(i => i.ItemID)).Distinct();

            if (!items.Any())
                return;

            //balance from
            itemBalancesFrom.Query.Where
                (
                    itemBalancesFrom.Query.ServiceUnitID == fromUnitId,
                    itemBalancesFrom.Query.IsCleanLaundry == true,
                    itemBalancesFrom.Query.ItemID.In(items)
                );
            itemBalancesFrom.LoadAll();

            //balance to
            itemBalancesTo.Query.Where
                (
                    itemBalancesTo.Query.ServiceUnitID == toUnitId,
                    itemBalancesTo.Query.IsCleanLaundry == true,
                    itemBalancesTo.Query.ItemID.In(items)
                );
            itemBalancesTo.LoadAll();

            foreach (var entity in coll)
            {
                decimal qty = entity.Qty ?? 0;

                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);

                #region balance from
                var balanceFrom = itemBalancesFrom.FindByPrimaryKey(fromUnitId, true, entity.ItemID);

                decimal qtyDiff = 0;
                if (balanceFrom != null)
                {
                    if (balanceFrom.Balance >= qty)
                        balanceFrom.Balance -= qty;
                    else
                    {
                        qtyDiff = qty - (balanceFrom.Balance ?? 0);
                        balanceFrom.Balance = 0;
                    }

                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceFrom = itemBalancesFrom.AddNew();
                    balanceFrom.ServiceUnitID = fromUnitId;
                    balanceFrom.IsCleanLaundry = true;
                    balanceFrom.ItemID = entity.ItemID;
                    balanceFrom.Balance = 0;
                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                    qtyDiff = qty;
                }
                #endregion

                #region balance to
                var balanceTo = itemBalancesTo.FindByPrimaryKey(toUnitId, true, entity.ItemID);
                if (balanceTo != null)
                {
                    balanceTo.Balance += qty;
                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceTo = itemBalancesTo.AddNew();
                    balanceTo.ServiceUnitID = toUnitId;
                    balanceTo.IsCleanLaundry = true;
                    balanceTo.ItemID = entity.ItemID;
                    balanceTo.Balance = qty;
                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                }
                #endregion

                #region movement from
                var movement = itemMovements.AddNew();
                movement.MovementDate = Utils.NowAtSqlServer();
                movement.TransactionNo = entity.TransactionNo;
                movement.TransactionCode = _sortingCode;
                movement.ServiceUnitID = fromUnitId;
                movement.IsCleanLaundry = true;
                movement.ItemID = entity.ItemID;
                if (qtyDiff == 0)
                    movement.InitialQty = balanceFrom.Balance + qty;
                else
                    movement.InitialQty = qty;
                movement.QtyIn = 0;
                movement.QtyOut = qty;
                movement.LastUpdateDateTime = Utils.NowAtSqlServer();
                movement.LastUpdateByUserID = userId;
                #endregion

                #region movement to
                var movementTo = itemMovements.AddNew();
                movementTo.MovementDate = Utils.NowAtSqlServer();
                movementTo.TransactionNo = entity.TransactionNo;
                movementTo.TransactionCode = _sortingCode;
                movementTo.ServiceUnitID = toUnitId;
                movementTo.IsCleanLaundry = true;
                movementTo.ItemID = entity.ItemID;
                movementTo.InitialQty = balanceTo.Balance - qty;
                movementTo.QtyIn = qty;
                movementTo.QtyOut = 0;
                movementTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                movementTo.LastUpdateByUserID = userId;
                #endregion
            }
        }

        public static void PrepareBalancesForDistribution(LaundryDistributionItemCollection coll, string fromUnitId, string toUnitId, string userId,
            ref LaundryItemBalanceCollection itemBalancesFrom, ref LaundryItemBalanceCollection itemBalancesTo, ref LaundryItemMovementCollection itemMovements)
        {
            var items = (coll.Select(i => i.ItemID)).Distinct();

            if (!items.Any())
                return;

            //balance from
            itemBalancesFrom.Query.Where
                (
                    itemBalancesFrom.Query.ServiceUnitID == fromUnitId,
                    itemBalancesFrom.Query.IsCleanLaundry == true,
                    itemBalancesFrom.Query.ItemID.In(items)
                );
            itemBalancesFrom.LoadAll();

            //balance to
            itemBalancesTo.Query.Where
                (
                    itemBalancesTo.Query.ServiceUnitID == toUnitId,
                    itemBalancesTo.Query.IsCleanLaundry == true,
                    itemBalancesTo.Query.ItemID.In(items)
                );
            itemBalancesTo.LoadAll();

            foreach (var entity in coll)
            {
                decimal qty = entity.Qty ?? 0;
                string itemUnit = string.Empty;

                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);

                #region balance from
                var balanceFrom = itemBalancesFrom.FindByPrimaryKey(fromUnitId, true, entity.ItemID);

                decimal qtyDiff = 0;
                if (balanceFrom != null)
                {
                    if (balanceFrom.Balance >= qty)
                        balanceFrom.Balance -= qty;
                    else
                    {
                        qtyDiff = qty - (balanceFrom.Balance ?? 0);
                        balanceFrom.Balance = 0;
                    }

                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceFrom = itemBalancesFrom.AddNew();
                    balanceFrom.ServiceUnitID = fromUnitId;
                    balanceFrom.IsCleanLaundry = true;
                    balanceFrom.ItemID = entity.ItemID;
                    balanceFrom.Balance = 0;
                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                    qtyDiff = qty;
                }
                #endregion

                #region balance to
                var balanceTo = itemBalancesTo.FindByPrimaryKey(toUnitId, true, entity.ItemID);
                if (balanceTo != null)
                {
                    balanceTo.Balance += qty;
                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceTo = itemBalancesTo.AddNew();
                    balanceTo.ServiceUnitID = toUnitId;
                    balanceTo.IsCleanLaundry = true;
                    balanceTo.ItemID = entity.ItemID;
                    balanceTo.Balance = qty;
                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                }
                #endregion

                #region movement from
                var movement = itemMovements.AddNew();
                movement.MovementDate = Utils.NowAtSqlServer();
                movement.TransactionNo = entity.DistributionNo;
                movement.TransactionCode = _distributionCode;
                movement.ServiceUnitID = fromUnitId;
                movement.IsCleanLaundry = true;
                movement.ItemID = entity.ItemID;
                if (qtyDiff == 0)
                    movement.InitialQty = balanceFrom.Balance + qty;
                else
                    movement.InitialQty = qty;
                movement.QtyIn = 0;
                movement.QtyOut = qty;
                movement.LastUpdateDateTime = Utils.NowAtSqlServer();
                movement.LastUpdateByUserID = userId;
                #endregion

                #region movement to
                var movementTo = itemMovements.AddNew();
                movementTo.MovementDate = Utils.NowAtSqlServer();
                movementTo.TransactionNo = entity.DistributionNo;
                movementTo.TransactionCode = _distributionCode;
                movementTo.ServiceUnitID = toUnitId;
                movementTo.IsCleanLaundry = true;
                movementTo.ItemID = entity.ItemID;
                movementTo.InitialQty = balanceTo.Balance - qty;
                movementTo.QtyIn = qty;
                movementTo.QtyOut = 0;
                movementTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                movementTo.LastUpdateByUserID = userId;
                #endregion
            }
        }

        public static void PrepareBalancesForReturn(LaundryReturnDistributionItemCollection coll, string fromUnitId, string toUnitId, string userId,
            ref LaundryItemBalanceCollection itemBalancesFrom, ref LaundryItemBalanceCollection itemBalancesTo, ref LaundryItemMovementCollection itemMovements)
        {
            var items = (coll.Select(i => i.ItemID)).Distinct();

            if (!items.Any())
                return;

            //balance from
            itemBalancesFrom.Query.Where
                (
                    itemBalancesFrom.Query.ServiceUnitID == fromUnitId,
                    itemBalancesFrom.Query.IsCleanLaundry == true,
                    itemBalancesFrom.Query.ItemID.In(items)
                );
            itemBalancesFrom.LoadAll();

            //balance to
            itemBalancesTo.Query.Where
                (
                    itemBalancesTo.Query.ServiceUnitID == toUnitId,
                    itemBalancesTo.Query.IsCleanLaundry == true,
                    itemBalancesTo.Query.ItemID.In(items)
                );
            itemBalancesTo.LoadAll();

            foreach (var entity in coll)
            {
                decimal qty = entity.Qty ?? 0;

                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);

                #region balance from
                var balanceFrom = itemBalancesFrom.FindByPrimaryKey(fromUnitId, true, entity.ItemID);

                decimal qtyDiff = 0;
                if (balanceFrom != null)
                {
                    if (balanceFrom.Balance >= qty)
                        balanceFrom.Balance -= qty;
                    else
                    {
                        qtyDiff = qty - (balanceFrom.Balance ?? 0);
                        balanceFrom.Balance = 0;
                    }

                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceFrom = itemBalancesFrom.AddNew();
                    balanceFrom.ServiceUnitID = fromUnitId;
                    balanceFrom.IsCleanLaundry = true;
                    balanceFrom.ItemID = entity.ItemID;
                    balanceFrom.Balance = 0;
                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                    qtyDiff = qty;
                }
                #endregion

                #region balance to
                var balanceTo = itemBalancesTo.FindByPrimaryKey(toUnitId, true, entity.ItemID);
                if (balanceTo != null)
                {
                    balanceTo.Balance += qty;
                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceTo = itemBalancesTo.AddNew();
                    balanceTo.ServiceUnitID = toUnitId;
                    balanceTo.IsCleanLaundry = true;
                    balanceTo.ItemID = entity.ItemID;
                    balanceTo.Balance = qty;
                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                }
                #endregion

                #region movement from
                var movement = itemMovements.AddNew();
                movement.MovementDate = Utils.NowAtSqlServer();
                movement.TransactionNo = entity.ReturnNo;
                movement.TransactionCode = _returnCode;
                movement.ServiceUnitID = fromUnitId;
                movement.IsCleanLaundry = true;
                movement.ItemID = entity.ItemID;
                if (qtyDiff == 0)
                    movement.InitialQty = balanceFrom.Balance + qty;
                else
                    movement.InitialQty = qty;
                movement.QtyIn = 0;
                movement.QtyOut = qty;
                movement.LastUpdateDateTime = Utils.NowAtSqlServer();
                movement.LastUpdateByUserID = userId;
                #endregion

                #region movement to
                var movementTo = itemMovements.AddNew();
                movementTo.MovementDate = Utils.NowAtSqlServer();
                movementTo.TransactionNo = entity.ReturnNo;
                movementTo.TransactionCode = _returnCode;
                movementTo.ServiceUnitID = toUnitId;
                movementTo.IsCleanLaundry = true;
                movementTo.ItemID = entity.ItemID;
                movementTo.InitialQty = balanceTo.Balance - qty;
                movementTo.QtyIn = qty;
                movementTo.QtyOut = 0;
                movementTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                movementTo.LastUpdateByUserID = userId;
                #endregion
            }
        }
        #endregion

        #region un-approval
        public static void PrepareBalancesForReceivedReverse(LaundryReceivedItemCollection coll, string fromUnitId, string toUnitId, string userId,
            ref LaundryItemBalanceCollection itemBalancesFrom, ref LaundryItemBalanceCollection itemBalancesTo, ref LaundryItemMovementCollection itemMovements)
        {
            var items = (coll.Select(i => i.ItemID)).Distinct();

            if (!items.Any())
                return;

            //balance from
            itemBalancesFrom.Query.Where
                (
                    itemBalancesFrom.Query.ServiceUnitID == fromUnitId,
                    itemBalancesFrom.Query.IsCleanLaundry == true,
                    itemBalancesFrom.Query.ItemID.In(items)
                );
            itemBalancesFrom.LoadAll();

            //balance to
            itemBalancesTo.Query.Where
                (
                    itemBalancesTo.Query.ServiceUnitID == toUnitId,
                    itemBalancesTo.Query.IsCleanLaundry == false,
                    itemBalancesTo.Query.ItemID.In(items)
                );
            itemBalancesTo.LoadAll();

            foreach (var entity in coll)
            {
                decimal qty = entity.Qty ?? 0;

                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);

                #region balance from
                var balanceFrom = itemBalancesFrom.FindByPrimaryKey(fromUnitId, true, entity.ItemID);
                if (balanceFrom != null)
                {
                    balanceFrom.Balance += qty;
                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceFrom = itemBalancesFrom.AddNew();
                    balanceFrom.ServiceUnitID = fromUnitId;
                    balanceFrom.IsCleanLaundry = true;
                    balanceFrom.ItemID = entity.ItemID;
                    balanceFrom.Balance = qty;
                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                }
                #endregion

                #region balance to
                decimal qtyDiff = 0;
                var balanceTo = itemBalancesTo.FindByPrimaryKey(toUnitId, false, entity.ItemID);
                if (balanceTo != null)
                {
                    if (balanceTo.Balance >= qty)
                        balanceTo.Balance -= qty;
                    else
                    {
                        qtyDiff = qty - (balanceTo.Balance ?? 0);
                        balanceTo.Balance = 0;
                    }

                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceTo = itemBalancesTo.AddNew();
                    balanceTo.ServiceUnitID = toUnitId;
                    balanceTo.IsCleanLaundry = false;
                    balanceTo.ItemID = entity.ItemID;
                    balanceTo.Balance = 0;
                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                    qtyDiff = qty;
                }
                #endregion

                #region movement from
                var movement = itemMovements.AddNew();
                movement.MovementDate = Utils.NowAtSqlServer();
                movement.TransactionNo = entity.ReceivedNo;
                movement.TransactionCode = _receivedReverseCode;
                movement.ServiceUnitID = fromUnitId;
                movement.IsCleanLaundry = true;
                movement.ItemID = entity.ItemID;
                movement.InitialQty = balanceFrom.Balance - qty;
                movement.QtyIn = qty;
                movement.QtyOut = 0;
                movement.LastUpdateDateTime = Utils.NowAtSqlServer();
                movement.LastUpdateByUserID = userId;
                #endregion

                #region movement to
                var movementTo = itemMovements.AddNew();
                movementTo.MovementDate = Utils.NowAtSqlServer();
                movementTo.TransactionNo = entity.ReceivedNo;
                movementTo.TransactionCode = _receivedReverseCode;
                movementTo.ServiceUnitID = toUnitId;
                movementTo.IsCleanLaundry = false;
                movementTo.ItemID = entity.ItemID;
                if (qtyDiff == 0)
                    movementTo.InitialQty = balanceTo.Balance + qty;
                else
                    movementTo.InitialQty = qty;
                movementTo.QtyIn = 0;
                movementTo.QtyOut = qty;
                movementTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                movementTo.LastUpdateByUserID = userId;
                #endregion
            }
        }

        public static void PrepareBalancesForProcessReverse(LaunderedProcessItemCentralizationCollection coll, string fromUnitId, string toUnitId, string userId,
            ref LaundryItemBalanceCollection itemBalancesFrom, ref LaundryItemBalanceCollection itemBalancesTo, ref LaundryItemMovementCollection itemMovements)
        {
            var items = (coll.Select(i => i.ItemID)).Distinct();

            if (!items.Any())
                return;

            //balance from
            itemBalancesFrom.Query.Where
                (
                    itemBalancesFrom.Query.ServiceUnitID == fromUnitId,
                    itemBalancesFrom.Query.IsCleanLaundry == false,
                    itemBalancesFrom.Query.ItemID.In(items)
                );
            itemBalancesFrom.LoadAll();

            //balance to
            itemBalancesTo.Query.Where
                (
                    itemBalancesTo.Query.ServiceUnitID == toUnitId,
                    itemBalancesTo.Query.IsCleanLaundry == true,
                    itemBalancesTo.Query.ItemID.In(items)
                );
            itemBalancesTo.LoadAll();

            foreach (var entity in coll)
            {
                decimal qty = entity.Qty ?? 0;

                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);

                #region balance from
                var balanceFrom = itemBalancesFrom.FindByPrimaryKey(fromUnitId, false, entity.ItemID);
                
                if (balanceFrom != null)
                {
                    balanceFrom.Balance += qty;
                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceFrom = itemBalancesTo.AddNew();
                    balanceFrom.ServiceUnitID = fromUnitId;
                    balanceFrom.IsCleanLaundry = false;
                    balanceFrom.ItemID = entity.ItemID;
                    balanceFrom.Balance = qty;
                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                }
                #endregion

                #region balance to
                var balanceTo = itemBalancesTo.FindByPrimaryKey(toUnitId, true, entity.ItemID);

                decimal qtyDiff = 0;
                if (balanceTo != null)
                {
                    if (balanceTo.Balance >= qty)
                        balanceTo.Balance -= qty;
                    else
                    {
                        balanceTo.Balance = 0;
                        qtyDiff = qty - (balanceTo.Balance ?? 0);
                    }

                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceTo = itemBalancesTo.AddNew();
                    balanceTo.ServiceUnitID = toUnitId;
                    balanceTo.IsCleanLaundry = true;
                    balanceTo.ItemID = entity.ItemID;
                    balanceTo.Balance = 0;
                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                    qtyDiff = qty;
                }
                #endregion

                #region movement from
                var movement = itemMovements.AddNew();
                movement.MovementDate = Utils.NowAtSqlServer();
                movement.TransactionNo = entity.ProcessNo;
                movement.TransactionCode = _processReverseCode;
                movement.ServiceUnitID = fromUnitId;
                movement.IsCleanLaundry = false;
                movement.ItemID = entity.ItemID;
                movement.InitialQty = balanceFrom.Balance - qty;
                movement.QtyIn = qty;
                movement.QtyOut = 0;
                movement.LastUpdateDateTime = Utils.NowAtSqlServer();
                movement.LastUpdateByUserID = userId;
                #endregion

                #region movement to
                var movementTo = itemMovements.AddNew();
                movementTo.MovementDate = Utils.NowAtSqlServer();
                movementTo.TransactionNo = entity.ProcessNo;
                movementTo.TransactionCode = _processReverseCode;
                movementTo.ServiceUnitID = toUnitId;
                movementTo.IsCleanLaundry = true;
                movementTo.ItemID = entity.ItemID;
                if (qtyDiff == 0)
                    movementTo.InitialQty = balanceTo.Balance + qty;
                else
                    movementTo.InitialQty = qty;
                movementTo.QtyIn = 0;
                movementTo.QtyOut = qty;
                movementTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                movementTo.LastUpdateByUserID = userId;
                #endregion
            }
        }

        public static void PrepareBalancesForSortingReverse(LaundrySortingProcessItemCollection coll, string fromUnitId, string toUnitId, string userId,
            ref LaundryItemBalanceCollection itemBalancesFrom, ref LaundryItemBalanceCollection itemBalancesTo, ref LaundryItemMovementCollection itemMovements)
        {
            var items = (coll.Select(i => i.ItemID)).Distinct();

            if (!items.Any())
                return;

            //balance from
            itemBalancesFrom.Query.Where
                (
                    itemBalancesFrom.Query.ServiceUnitID == fromUnitId,
                    itemBalancesFrom.Query.IsCleanLaundry == true,
                    itemBalancesFrom.Query.ItemID.In(items)
                );
            itemBalancesFrom.LoadAll();

            //balance to
            itemBalancesTo.Query.Where
                (
                    itemBalancesTo.Query.ServiceUnitID == toUnitId,
                    itemBalancesTo.Query.IsCleanLaundry == true,
                    itemBalancesTo.Query.ItemID.In(items)
                );
            itemBalancesTo.LoadAll();

            foreach (var entity in coll)
            {
                decimal qty = entity.Qty ?? 0;

                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);

                #region balance from
                var balanceFrom = itemBalancesFrom.FindByPrimaryKey(fromUnitId, true, entity.ItemID);
                if (balanceFrom != null)
                {
                    balanceFrom.Balance += qty;
                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceFrom = itemBalancesFrom.AddNew();
                    balanceFrom.ServiceUnitID = fromUnitId;
                    balanceFrom.IsCleanLaundry = true;
                    balanceFrom.ItemID = entity.ItemID;
                    balanceFrom.Balance = qty;
                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                }
                #endregion

                #region balance to
                var balanceTo = itemBalancesTo.FindByPrimaryKey(toUnitId, true, entity.ItemID);

                decimal qtyDiff = 0;
                if (balanceTo != null)
                {
                    if (balanceTo.Balance >= qty)
                        balanceTo.Balance -= qty;
                    else
                    {
                        balanceTo.Balance = 0;
                        qtyDiff = qty - (balanceTo.Balance ?? 0);
                    }

                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceTo = itemBalancesFrom.AddNew();
                    balanceTo.ServiceUnitID = toUnitId;
                    balanceTo.IsCleanLaundry = true;
                    balanceTo.ItemID = entity.ItemID;
                    balanceTo.Balance = 0;
                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                    qtyDiff = qty;
                }
                #endregion

                #region movement from
                var movement = itemMovements.AddNew();
                movement.MovementDate = Utils.NowAtSqlServer();
                movement.TransactionNo = entity.TransactionNo;
                movement.TransactionCode = _sortingReverseCode;
                movement.ServiceUnitID = fromUnitId;
                movement.IsCleanLaundry = true;
                movement.ItemID = entity.ItemID;
                movement.InitialQty = balanceFrom.Balance - qty;
                movement.QtyIn = qty;
                movement.QtyOut = 0;
                movement.LastUpdateDateTime = Utils.NowAtSqlServer();
                movement.LastUpdateByUserID = userId;
                #endregion

                #region movement to
                var movementTo = itemMovements.AddNew();
                movementTo.MovementDate = Utils.NowAtSqlServer();
                movementTo.TransactionNo = entity.TransactionNo;
                movementTo.TransactionCode = _sortingReverseCode;
                movementTo.ServiceUnitID = toUnitId;
                movementTo.IsCleanLaundry = true;
                movementTo.ItemID = entity.ItemID;
                if (qtyDiff == 0)
                    movementTo.InitialQty = balanceTo.Balance + qty;
                else
                    movementTo.InitialQty = qty;
                movementTo.QtyIn = 0;
                movementTo.QtyOut = qty;
                movementTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                movementTo.LastUpdateByUserID = userId;
                #endregion
            }
        }

        public static void PrepareBalancesForDistributionReverse(LaundryDistributionItemCollection coll, string fromUnitId, string toUnitId, string userId,
            ref LaundryItemBalanceCollection itemBalancesFrom, ref LaundryItemBalanceCollection itemBalancesTo, ref LaundryItemMovementCollection itemMovements)
        {
            var items = (coll.Select(i => i.ItemID)).Distinct();

            if (!items.Any())
                return;

            //balance from
            itemBalancesFrom.Query.Where
                (
                    itemBalancesFrom.Query.ServiceUnitID == fromUnitId,
                    itemBalancesFrom.Query.IsCleanLaundry == true,
                    itemBalancesFrom.Query.ItemID.In(items)
                );
            itemBalancesFrom.LoadAll();

            //balance to
            itemBalancesTo.Query.Where
                (
                    itemBalancesTo.Query.ServiceUnitID == toUnitId,
                    itemBalancesTo.Query.IsCleanLaundry == true,
                    itemBalancesTo.Query.ItemID.In(items)
                );
            itemBalancesTo.LoadAll();

            foreach (var entity in coll)
            {
                decimal qty = entity.Qty ?? 0;

                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);

                #region balance from
                var balanceFrom = itemBalancesFrom.FindByPrimaryKey(fromUnitId, true, entity.ItemID);
                if (balanceFrom != null)
                {
                    balanceFrom.Balance += qty;
                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceFrom = itemBalancesTo.AddNew();
                    balanceFrom.ServiceUnitID = fromUnitId;
                    balanceFrom.IsCleanLaundry = true;
                    balanceFrom.ItemID = entity.ItemID;
                    balanceFrom.Balance = qty;
                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                }
                #endregion

                #region balance to
                var balanceTo = itemBalancesTo.FindByPrimaryKey(toUnitId, true, entity.ItemID);
                decimal qtyDiff = 0;
                if (balanceTo != null)
                {
                    if (balanceTo.Balance >= qty)
                        balanceTo.Balance -= qty;
                    else
                    {
                        qtyDiff = qty - (balanceTo.Balance ?? 0);
                        balanceTo.Balance = 0;
                    }

                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceTo = itemBalancesFrom.AddNew();
                    balanceTo.ServiceUnitID = toUnitId;
                    balanceTo.IsCleanLaundry = true;
                    balanceTo.ItemID = entity.ItemID;
                    balanceTo.Balance = 0;
                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                    qtyDiff = qty;
                }
                #endregion

                #region movement from
                var movement = itemMovements.AddNew();
                movement.MovementDate = Utils.NowAtSqlServer();
                movement.TransactionNo = entity.DistributionNo;
                movement.TransactionCode = _distributionReverseCode;
                movement.ServiceUnitID = fromUnitId;
                movement.IsCleanLaundry = true;
                movement.ItemID = entity.ItemID;
                movement.InitialQty = balanceFrom.Balance - qty;
                movement.QtyIn = qty;
                movement.QtyOut = 0;
                movement.LastUpdateDateTime = Utils.NowAtSqlServer();
                movement.LastUpdateByUserID = userId;
                #endregion

                #region movement to
                var movementTo = itemMovements.AddNew();
                movementTo.MovementDate = Utils.NowAtSqlServer();
                movementTo.TransactionNo = entity.DistributionNo;
                movementTo.TransactionCode = _distributionReverseCode;
                movementTo.ServiceUnitID = toUnitId;
                movementTo.IsCleanLaundry = true;
                movementTo.ItemID = entity.ItemID;
                if (qtyDiff == 0)
                    movementTo.InitialQty = balanceTo.Balance + qty;
                else
                    movementTo.InitialQty = qty;
                movementTo.QtyIn = 0;
                movementTo.QtyOut = qty;
                movementTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                movementTo.LastUpdateByUserID = userId;
                #endregion
            }
        }

        public static void PrepareBalancesForReturnReverse(LaundryReturnDistributionItemCollection coll, string fromUnitId, string toUnitId, string userId,
            ref LaundryItemBalanceCollection itemBalancesFrom, ref LaundryItemBalanceCollection itemBalancesTo, ref LaundryItemMovementCollection itemMovements)
        {
            var items = (coll.Select(i => i.ItemID)).Distinct();

            if (!items.Any())
                return;

            //balance from
            itemBalancesFrom.Query.Where
                (
                    itemBalancesFrom.Query.ServiceUnitID == fromUnitId,
                    itemBalancesFrom.Query.IsCleanLaundry == true,
                    itemBalancesFrom.Query.ItemID.In(items)
                );
            itemBalancesFrom.LoadAll();

            //balance to
            itemBalancesTo.Query.Where
                (
                    itemBalancesTo.Query.ServiceUnitID == toUnitId,
                    itemBalancesTo.Query.IsCleanLaundry == true,
                    itemBalancesTo.Query.ItemID.In(items)
                );
            itemBalancesTo.LoadAll();

            foreach (var entity in coll)
            {
                decimal qty = entity.Qty ?? 0;

                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);

                #region balance from
                var balanceFrom = itemBalancesFrom.FindByPrimaryKey(fromUnitId, true, entity.ItemID);
                if (balanceFrom != null)
                {
                    balanceFrom.Balance += qty;
                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceFrom = itemBalancesTo.AddNew();
                    balanceFrom.ServiceUnitID = fromUnitId;
                    balanceFrom.IsCleanLaundry = true;
                    balanceFrom.ItemID = entity.ItemID;
                    balanceFrom.Balance = qty;
                    balanceFrom.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceFrom.LastUpdateByUserID = userId;
                }
                #endregion

                #region balance to
                var balanceTo = itemBalancesTo.FindByPrimaryKey(toUnitId, true, entity.ItemID);
                decimal qtyDiff = 0;
                if (balanceTo != null)
                {
                    if (balanceTo.Balance >= qty)
                        balanceTo.Balance -= qty;
                    else
                    {
                        qtyDiff = qty - (balanceTo.Balance ?? 0);
                        balanceTo.Balance = 0;
                    }

                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                }
                else
                {
                    balanceTo = itemBalancesFrom.AddNew();
                    balanceTo.ServiceUnitID = toUnitId;
                    balanceTo.IsCleanLaundry = true;
                    balanceTo.ItemID = entity.ItemID;
                    balanceTo.Balance = 0;
                    balanceTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                    balanceTo.LastUpdateByUserID = userId;
                    qtyDiff = qty;
                }
                #endregion

                #region movement from
                var movement = itemMovements.AddNew();
                movement.MovementDate = Utils.NowAtSqlServer();
                movement.TransactionNo = entity.ReturnNo;
                movement.TransactionCode = _returnReverseCode;
                movement.ServiceUnitID = fromUnitId;
                movement.IsCleanLaundry = true;
                movement.ItemID = entity.ItemID;
                movement.InitialQty = balanceFrom.Balance - qty;
                movement.QtyIn = qty;
                movement.QtyOut = 0;
                movement.LastUpdateDateTime = Utils.NowAtSqlServer();
                movement.LastUpdateByUserID = userId;
                #endregion

                #region movement to
                var movementTo = itemMovements.AddNew();
                movementTo.MovementDate = Utils.NowAtSqlServer();
                movementTo.TransactionNo = entity.ReturnNo;
                movementTo.TransactionCode = _returnReverseCode;
                movementTo.ServiceUnitID = toUnitId;
                movementTo.IsCleanLaundry = true;
                movementTo.ItemID = entity.ItemID;
                if (qtyDiff == 0)
                    movementTo.InitialQty = balanceTo.Balance + qty;
                else
                    movementTo.InitialQty = qty;
                movementTo.QtyIn = 0;
                movementTo.QtyOut = qty;
                movementTo.LastUpdateDateTime = Utils.NowAtSqlServer();
                movementTo.LastUpdateByUserID = userId;
                #endregion
            }
        }
        #endregion

    }
}

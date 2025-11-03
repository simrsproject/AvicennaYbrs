using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CssdItemBalance
    {
        public static void PrepareItemBalancesReceive(CssdSterileItemsReceivedItemCollection coll, string fromUnitId, string cssdUnitId, string userId, bool isCentralizedCssd, bool isApproved,
            ref CssdItemBalanceCollection itemBalancesFrom, ref CssdItemBalanceCollection itemBalancesTo)
        {
            var items = (coll.Select(i => i.ItemID)).Distinct();

            itemBalancesFrom.Query.Where
                (
                    itemBalancesFrom.Query.ServiceUnitID == fromUnitId,
                    itemBalancesFrom.Query.ItemID.In(items)
                );
            itemBalancesFrom.LoadAll();

            itemBalancesTo.Query.Where
                (
                    itemBalancesTo.Query.ServiceUnitID == cssdUnitId,
                    itemBalancesTo.Query.ItemID.In(items)
                );
            itemBalancesTo.LoadAll();

            if (isApproved)
            {
                foreach (var c in coll)
                {
                    //cssd unit
                    var balanceTo = itemBalancesTo.SingleOrDefault(ib => ib.ServiceUnitID == cssdUnitId && ib.ItemID == c.ItemID);
                    if (balanceTo != null)
                    {
                        balanceTo.BalanceReceived += c.Qty;
                        if (isCentralizedCssd)
                        {
                            if (balanceTo.BalanceDistribution >= c.Qty)
                                balanceTo.BalanceDistribution -= c.Qty;
                            else
                                balanceTo.BalanceDistribution = 0;
                        }
                        else
                        {
                            if (balanceTo.BalanceReturned >= c.Qty)
                                balanceTo.BalanceReturned -= c.Qty;
                            else
                                balanceTo.BalanceReturned = 0;
                        }
                        balanceTo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balanceTo.LastUpdateByUserID = userId;
                    }
                    else
                    {
                        balanceTo = itemBalancesTo.AddNew();
                        balanceTo.ServiceUnitID = cssdUnitId;
                        balanceTo.ItemID = c.ItemID;
                        balanceTo.BalanceReceived = c.Qty;
                        balanceTo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balanceTo.LastUpdateByUserID = userId;
                    }

                    //other unit
                    var balanceFrom = itemBalancesFrom.SingleOrDefault(ib => ib.ServiceUnitID == fromUnitId && ib.ItemID == c.ItemID);
                    if (balanceFrom != null)
                    {
                        if (balanceFrom.Balance >= c.Qty)
                            balanceFrom.Balance -= c.Qty;
                        else
                            balanceFrom.Balance = 0;

                        balanceFrom.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balanceFrom.LastUpdateByUserID = userId;
                    }
                }
            }
            else //isApproved = false
            {
                foreach (var c in coll)
                {
                    //cssd unit
                    var balanceTo = itemBalancesTo.SingleOrDefault(ib => ib.ServiceUnitID == cssdUnitId && ib.ItemID == c.ItemID);
                    if (balanceTo != null)
                    {
                        if (balanceTo.BalanceReceived >= c.Qty)
                            balanceTo.BalanceReceived -= c.Qty;
                        else
                            balanceTo.BalanceReceived = 0;

                        if (isCentralizedCssd)
                            balanceTo.BalanceDistribution += c.Qty;
                        else
                            balanceTo.BalanceReturned += c.Qty;

                        balanceTo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balanceTo.LastUpdateByUserID = userId;
                    }

                    //other unit
                    var balanceFrom = itemBalancesFrom.SingleOrDefault(ib => ib.ServiceUnitID == fromUnitId && ib.ItemID == c.ItemID);
                    if (balanceFrom != null)
                    {
                        balanceFrom.Balance += c.Qty;

                        balanceFrom.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balanceFrom.LastUpdateByUserID = userId;
                    }
                }
            }
        }

        public static void PrepareItemBalanceDecontamination(string transactionNo, string cssdUnitId, string userId, string phase, bool isApproved,
            ref CssdItemBalanceCollection itemBalances)
        {
            var coll = new CssdSterileItemsReceivedItemCollection();
            var query = new CssdSterileItemsReceivedItemQuery("a");
            var refq = new CssdDecontaminationItemQuery("b");
            query.InnerJoin(refq).On(refq.ReceivedNo == query.ReceivedNo && refq.ReceivedSeqNo == query.ReceivedSeqNo);
            query.Where(refq.DecontaminationNo == transactionNo);
            query.OrderBy(query.ItemID.Ascending);
            coll.Load(query);

            var items = (coll.Select(i => i.ItemID)).Distinct();

            itemBalances.Query.Where
                (
                    itemBalances.Query.ServiceUnitID == cssdUnitId,
                    itemBalances.Query.ItemID.In(items)
                );
            itemBalances.LoadAll();

            if (isApproved)
            {
                foreach (var c in coll)
                {
                    //cssd unit
                    var balance = itemBalances.SingleOrDefault(ib => ib.ServiceUnitID == cssdUnitId && ib.ItemID == c.ItemID);
                    if (balance != null)
                    {
                        if (phase == "1")
                        {
                            if (balance.BalanceReceived >= c.Qty)
                                balance.BalanceReceived -= c.Qty;
                            else
                                balance.BalanceReceived = 0;
                            balance.BalanceDeconImmersion += c.Qty;
                        }
                        else if (phase == "2")
                        {
                            if (balance.BalanceDeconImmersion >= c.Qty)
                                balance.BalanceDeconImmersion -= c.Qty;
                            else
                                balance.BalanceDeconImmersion = 0;
                            balance.BalanceDeconAbstersion += c.Qty;
                        }
                        else if (phase == "3")
                        {
                            if (balance.BalanceDeconAbstersion >= c.Qty)
                                balance.BalanceDeconAbstersion -= c.Qty;
                            else
                                balance.BalanceDeconAbstersion = 0;
                            balance.BalanceDeconDrying += c.Qty;
                        }

                        balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balance.LastUpdateByUserID = userId;
                    }
                    else
                    {
                        balance = itemBalances.AddNew();
                        balance.ServiceUnitID = cssdUnitId;
                        balance.ItemID = c.ItemID;
                        if (phase == "1")
                            balance.BalanceDeconImmersion = c.Qty;
                        else if (phase == "2")
                            balance.BalanceDeconAbstersion = c.Qty;
                        else if (phase == "3")
                            balance.BalanceDeconDrying = c.Qty;

                        balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balance.LastUpdateByUserID = userId;
                    }
                }
            }
            else //isApproved = false
            {
                foreach (var c in coll)
                {
                    //cssd unit
                    var balance = itemBalances.SingleOrDefault(ib => ib.ServiceUnitID == cssdUnitId && ib.ItemID == c.ItemID);
                    if (balance != null)
                    {
                        if (phase == "1")
                        {
                            if (balance.BalanceDeconImmersion >= c.Qty)
                                balance.BalanceDeconImmersion -= c.Qty;
                            else
                                balance.BalanceDeconImmersion = 0;
                            balance.BalanceReceived += c.Qty;
                        }
                        else if (phase == "2")
                        {
                            if (balance.BalanceDeconAbstersion >= c.Qty)
                                balance.BalanceDeconAbstersion -= c.Qty;
                            else
                                balance.BalanceDeconAbstersion = 0;
                            balance.BalanceDeconImmersion += c.Qty;
                        }
                        else if (phase == "3")
                        {
                            if (balance.BalanceDeconDrying >= c.Qty)
                                balance.BalanceDeconDrying -= c.Qty;
                            else
                                balance.BalanceDeconDrying = 0;
                            balance.BalanceDeconAbstersion += c.Qty;
                        }

                        balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balance.LastUpdateByUserID = userId;
                    }
                }
            }
        }

        public static void PrepareItemBalanceFeasibilityTest(string transactionNo, string cssdUnitId, string userId, bool isUsingDecontamination, bool isApproved,
            ref CssdItemBalanceCollection itemBalances)
        {
            var coll = new CssdSterileItemsReceivedItemCollection();
            var query = new CssdSterileItemsReceivedItemQuery("a");
            var refq = new CssdFeasibilityTestItemQuery("b");
            query.InnerJoin(refq).On(refq.ReceivedNo == query.ReceivedNo && refq.ReceivedSeqNo == query.ReceivedSeqNo);
            query.Where(refq.FeasibilityTestNo == transactionNo);
            query.OrderBy(query.ItemID.Ascending);
            coll.Load(query);

            var items = (coll.Select(i => i.ItemID)).Distinct();

            itemBalances.Query.Where
                (
                    itemBalances.Query.ServiceUnitID == cssdUnitId,
                    itemBalances.Query.ItemID.In(items)
                );
            itemBalances.LoadAll();

            if (isApproved)
            {
                foreach (var c in coll)
                {
                    //cssd unit
                    var balance = itemBalances.SingleOrDefault(ib => ib.ServiceUnitID == cssdUnitId && ib.ItemID == c.ItemID);
                    if (balance != null)
                    {
                        balance.BalanceFeasibilityTest += c.Qty;
                        if (isUsingDecontamination)
                        {
                            if (balance.BalanceDeconDrying >= c.Qty)
                                balance.BalanceDeconDrying -= c.Qty;
                            else
                                balance.BalanceDeconDrying = 0;
                        }
                        else
                        {
                            if (balance.BalanceReceived >= c.Qty)
                                balance.BalanceReceived -= c.Qty;
                            else
                                balance.BalanceReceived = 0;
                        }

                        balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balance.LastUpdateByUserID = userId;
                    }
                    else
                    {
                        balance = itemBalances.AddNew();
                        balance.ServiceUnitID = cssdUnitId;
                        balance.ItemID = c.ItemID;
                        balance.BalanceFeasibilityTest = c.Qty;

                        balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balance.LastUpdateByUserID = userId;
                    }
                }
            }
            else //isApproved = false
            {
                foreach (var c in coll)
                {
                    //cssd unit
                    var balance = itemBalances.SingleOrDefault(ib => ib.ServiceUnitID == cssdUnitId && ib.ItemID == c.ItemID);
                    if (balance != null)
                    {
                        if (balance.BalanceFeasibilityTest >= c.Qty)
                            balance.BalanceFeasibilityTest -= c.Qty;
                        else
                            balance.BalanceFeasibilityTest = 0;

                        if (isUsingDecontamination)
                            balance.BalanceDeconDrying += c.Qty;
                        else
                            balance.BalanceReceived += c.Qty;

                        balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balance.LastUpdateByUserID = userId;
                    }
                }
            }
        }

        public static void PrepareItemBalancePackaging(string transactionNo, string cssdUnitId, string userId, bool isUsingFeasibilityTest, bool isUsingDecontamination, bool isApproved,
            ref CssdItemBalanceCollection itemBalances)
        {
            var coll = new CssdSterileItemsReceivedItemCollection();
            var query = new CssdSterileItemsReceivedItemQuery("a");
            var refq = new CssdPackagingItemQuery("b");
            query.InnerJoin(refq).On(refq.ReceivedNo == query.ReceivedNo && refq.ReceivedSeqNo == query.ReceivedSeqNo);
            query.Where(refq.TransactionNo == transactionNo);
            query.OrderBy(query.ItemID.Ascending);
            coll.Load(query);

            var items = (coll.Select(i => i.ItemID)).Distinct();

            itemBalances.Query.Where
                (
                    itemBalances.Query.ServiceUnitID == cssdUnitId,
                    itemBalances.Query.ItemID.In(items)
                );
            itemBalances.LoadAll();

            if (isApproved)
            {
                foreach (var c in coll)
                {
                    //cssd unit
                    var balance = itemBalances.SingleOrDefault(ib => ib.ServiceUnitID == cssdUnitId && ib.ItemID == c.ItemID);
                    if (balance != null)
                    {
                        balance.BalancePackaging += c.Qty;
                        if (isUsingFeasibilityTest)
                        {
                            if (balance.BalanceFeasibilityTest >= c.Qty)
                                balance.BalanceFeasibilityTest -= c.Qty;
                            else
                                balance.BalanceFeasibilityTest = 0;
                        }
                        else if (isUsingDecontamination)
                        {
                            if (balance.BalanceDeconDrying >= c.Qty)
                                balance.BalanceDeconDrying -= c.Qty;
                            else
                                balance.BalanceDeconDrying = 0;
                        }
                        else
                        {
                            if (balance.BalanceReceived >= c.Qty)
                                balance.BalanceReceived -= c.Qty;
                            else
                                balance.BalanceReceived = 0;
                        }

                        balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balance.LastUpdateByUserID = userId;
                    }
                    else
                    {
                        balance = itemBalances.AddNew();
                        balance.ServiceUnitID = cssdUnitId;
                        balance.ItemID = c.ItemID;
                        balance.BalancePackaging = c.Qty;

                        balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balance.LastUpdateByUserID = userId;
                    }
                }
            }
            else //isApproved = false
            {
                foreach (var c in coll)
                {
                    //cssd unit
                    var balance = itemBalances.SingleOrDefault(ib => ib.ServiceUnitID == cssdUnitId && ib.ItemID == c.ItemID);
                    if (balance != null)
                    {
                        if (balance.BalancePackaging >= c.Qty)
                            balance.BalancePackaging -= c.Qty;
                        else
                            balance.BalancePackaging = 0;

                        if (isUsingFeasibilityTest)
                            balance.BalanceFeasibilityTest += c.Qty;
                        else if (isUsingDecontamination)
                            balance.BalanceDeconDrying += c.Qty;
                        else
                            balance.BalanceReceived += c.Qty;

                        balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balance.LastUpdateByUserID = userId;
                    }
                }
            }
        }

        public static void PrepareItemBalanceUltrasound(string transactionNo, string cssdUnitId, string userId, bool isUsingPackaging, bool isUsingFeasibilityTest, bool isUsingDecontamination, bool isApproved,
            ref CssdItemBalanceCollection itemBalances)
        {
            var coll = new CssdSterileItemsReceivedItemCollection();
            var query = new CssdSterileItemsReceivedItemQuery("a");
            var refq = new CssdSterileItemsUltrasoundItemQuery("b");
            query.InnerJoin(refq).On(refq.ReceivedNo == query.ReceivedNo && refq.ReceivedSeqNo == query.ReceivedSeqNo);
            query.Where(refq.TransactionNo == transactionNo);
            query.OrderBy(query.ItemID.Ascending);
            coll.Load(query);

            var items = (coll.Select(i => i.ItemID)).Distinct();

            itemBalances.Query.Where
                (
                    itemBalances.Query.ServiceUnitID == cssdUnitId,
                    itemBalances.Query.ItemID.In(items)
                );
            itemBalances.LoadAll();

            if (isApproved)
            {
                foreach (var c in coll)
                {
                    //cssd unit
                    var balance = itemBalances.SingleOrDefault(ib => ib.ServiceUnitID == cssdUnitId && ib.ItemID == c.ItemID);
                    if (balance != null)
                    {
                        balance.BalanceUltrasound += c.Qty;
                        if (isUsingPackaging)
                        {
                            if (balance.BalancePackaging >= c.Qty)
                                balance.BalancePackaging -= c.Qty;
                            else
                                balance.BalancePackaging = 0;
                        }
                        else if (isUsingFeasibilityTest)
                        {
                            if (balance.BalanceFeasibilityTest >= c.Qty)
                                balance.BalanceFeasibilityTest -= c.Qty;
                            else
                                balance.BalanceFeasibilityTest = 0;
                        }
                        else if (isUsingDecontamination)
                        {
                            if (balance.BalanceDeconDrying >= c.Qty)
                                balance.BalanceDeconDrying -= c.Qty;
                            else
                                balance.BalanceDeconDrying = 0;
                        }
                        else
                        {
                            if (balance.BalanceReceived >= c.Qty)
                                balance.BalanceReceived -= c.Qty;
                            else
                                balance.BalanceReceived = 0;
                        }

                        balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balance.LastUpdateByUserID = userId;
                    }
                    else
                    {
                        balance = itemBalances.AddNew();
                        balance.ServiceUnitID = cssdUnitId;
                        balance.ItemID = c.ItemID;
                        balance.BalanceUltrasound = c.Qty;

                        balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balance.LastUpdateByUserID = userId;
                    }
                }
            }
            else //isApproved = false
            {
                foreach (var c in coll)
                {
                    //cssd unit
                    var balance = itemBalances.SingleOrDefault(ib => ib.ServiceUnitID == cssdUnitId && ib.ItemID == c.ItemID);
                    if (balance != null)
                    {
                        if (balance.BalanceUltrasound >= c.Qty)
                            balance.BalanceUltrasound -= c.Qty;
                        else
                            balance.BalanceUltrasound = 0;

                        if (isUsingPackaging)
                            balance.BalancePackaging += c.Qty;
                        else if (isUsingFeasibilityTest)
                            balance.BalanceFeasibilityTest += c.Qty;
                        else if (isUsingDecontamination)
                            balance.BalanceDeconDrying += c.Qty;
                        else
                            balance.BalanceReceived += c.Qty;

                        balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balance.LastUpdateByUserID = userId;
                    }
                }
            }
        }

        public static void PrepareItemBalanceSterilization(string transactionNo, string cssdUnitId, string userId, bool isUsingPackaging, bool isUsingFeasibilityTest, bool isUsingDecontamination, bool isApproved,
            ref CssdItemBalanceCollection itemBalances)
        {
            var coll = new CssdSterilizationProcessItemCollection();
            var query = new CssdSterilizationProcessItemQuery("a");
            var refq = new CssdSterileItemsReceivedItemQuery("b");
            query.InnerJoin(refq).On(refq.ReceivedNo == query.ReceivedNo && refq.ReceivedSeqNo == query.ReceivedSeqNo);
            query.Select(query, refq.ItemID.As("refToCssdSterileItemsReceivedItem_ItemID"), @"<ISNULL(b.IsUltrasound, 0) AS 'refToCssdSterileItemsReceivedItem_IsUltrasound'>");
            query.Where(query.ProcessNo == transactionNo);
            query.OrderBy(refq.ItemID.Ascending);
            coll.Load(query);

            var items = (coll.Select(i => i.ItemID)).Distinct();

            itemBalances.Query.Where
                (
                    itemBalances.Query.ServiceUnitID == cssdUnitId,
                    itemBalances.Query.ItemID.In(items)
                );
            itemBalances.LoadAll();

            if (isApproved)
            {
                foreach (var c in coll)
                {
                    //cssd unit
                    var balance = itemBalances.SingleOrDefault(ib => ib.ServiceUnitID == cssdUnitId && ib.ItemID == c.ItemID);
                    if (balance != null)
                    {
                        balance.Balance += c.Qty;
                        balance.BalanceSterilization += c.Qty;

                        if (c.IsUltrasound ?? false)
                        {
                            if (balance.BalanceUltrasound >= c.Qty)
                                balance.BalanceUltrasound -= c.Qty;
                            else
                                balance.BalanceUltrasound = 0;
                        }
                        else if (isUsingPackaging)
                        {
                            if (balance.BalancePackaging >= c.Qty)
                                balance.BalancePackaging -= c.Qty;
                            else
                                balance.BalancePackaging = 0;
                        }
                        else if (isUsingFeasibilityTest)
                        {
                            if (balance.BalanceFeasibilityTest >= c.Qty)
                                balance.BalanceFeasibilityTest -= c.Qty;
                            else
                                balance.BalanceFeasibilityTest = 0;
                        }
                        else if (isUsingDecontamination)
                        {
                            if (balance.BalanceDeconDrying >= c.Qty)
                                balance.BalanceDeconDrying -= c.Qty;
                            else
                                balance.BalanceDeconDrying = 0;
                        }
                        else
                        {
                            if (balance.BalanceReceived >= c.Qty)
                                balance.BalanceReceived -= c.Qty;
                            else
                                balance.BalanceReceived = 0;
                        }

                        balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balance.LastUpdateByUserID = userId;
                    }
                    else
                    {
                        balance = itemBalances.AddNew();
                        balance.ServiceUnitID = cssdUnitId;
                        balance.ItemID = c.ItemID;
                        balance.Balance = c.Qty;
                        balance.BalanceSterilization = c.Qty;

                        balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balance.LastUpdateByUserID = userId;
                    }
                }
            }
            else //isApproved = false
            {
                foreach (var c in coll)
                {
                    //cssd unit
                    var balance = itemBalances.SingleOrDefault(ib => ib.ServiceUnitID == cssdUnitId && ib.ItemID == c.ItemID);
                    if (balance != null)
                    {
                        if (balance.Balance >= c.Qty)
                            balance.Balance -= c.Qty;
                        else
                            balance.Balance = 0;

                        if (balance.BalanceSterilization >= c.Qty)
                            balance.BalanceSterilization -= c.Qty;
                        else
                            balance.BalanceSterilization = 0;

                        if (c.IsUltrasound ?? false)
                            balance.BalanceUltrasound += c.Qty;
                        else if (isUsingPackaging)
                            balance.BalancePackaging += c.Qty;
                        else if (isUsingFeasibilityTest)
                            balance.BalanceFeasibilityTest += c.Qty;
                        else if (isUsingDecontamination)
                            balance.BalanceDeconDrying += c.Qty;
                        else
                            balance.BalanceReceived += c.Qty;

                        balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balance.LastUpdateByUserID = userId;
                    }
                }
            }
        }

        public static void PrepareItemBalanceDistribution(string transactionNo, string cssdUnitId, string toUnitId, string userId, bool isApproved,
            ref CssdItemBalanceCollection itemBalancesFrom, ref CssdItemBalanceCollection itemBalancesTo, out string itemNoStock)
        {
            itemNoStock = string.Empty;

            var coll = new CssdDistributionItemCollection();
            var query = new CssdDistributionItemQuery("a");
            query.Where(query.TransactionNo == transactionNo);
            query.OrderBy(query.ItemID.Ascending);
            coll.Load(query);

            var items = (coll.Select(i => i.ItemID)).Distinct();

            itemBalancesFrom.Query.Where
                (
                    itemBalancesFrom.Query.ServiceUnitID == cssdUnitId,
                    itemBalancesFrom.Query.ItemID.In(items)
                );
            itemBalancesFrom.LoadAll();

            itemBalancesTo.Query.Where
                (
                    itemBalancesTo.Query.ServiceUnitID == toUnitId,
                    itemBalancesTo.Query.ItemID.In(items)
                );
            itemBalancesTo.LoadAll();

            if (isApproved)
            {
                foreach (var c in coll)
                {
                    //other unit
                    var balanceTo = itemBalancesTo.SingleOrDefault(ib => ib.ServiceUnitID == toUnitId && ib.ItemID == c.ItemID);
                    if (balanceTo != null)
                    {
                        balanceTo.Balance += c.Qty;
                        
                        balanceTo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balanceTo.LastUpdateByUserID = userId;
                    }
                    else
                    {
                        balanceTo = itemBalancesTo.AddNew();
                        balanceTo.ServiceUnitID = toUnitId;
                        balanceTo.ItemID = c.ItemID;
                        balanceTo.Balance = c.Qty;
                        balanceTo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balanceTo.LastUpdateByUserID = userId;
                    }

                    //cssd unit
                    var balanceFrom = itemBalancesFrom.SingleOrDefault(ib => ib.ServiceUnitID == cssdUnitId && ib.ItemID == c.ItemID);
                    if (balanceFrom != null)
                    {
                        if (balanceFrom.Balance >= c.Qty)
                            balanceFrom.Balance -= c.Qty;
                        else
                        {
                            var itm = new Item();
                            itm.LoadByPrimaryKey(c.ItemID);
                            if (itemNoStock == string.Empty)
                                itemNoStock = c.ItemID + " - " + itm.ItemName;
                            else
                                itemNoStock += "; " + c.ItemID + " - " + itm.ItemName;

                            balanceFrom.Balance = 0;
                        }
                            
                        if (balanceFrom.BalanceSterilization >= c.Qty)
                            balanceFrom.BalanceSterilization -= c.Qty;
                        else
                            balanceFrom.BalanceSterilization = 0;

                        balanceFrom.BalanceDistribution += c.Qty;

                        balanceFrom.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balanceFrom.LastUpdateByUserID = userId;
                    }
                }
            }
            else //isApproved = false
            {
                foreach (var c in coll)
                {
                    //other unit
                    var balanceTo = itemBalancesTo.SingleOrDefault(ib => ib.ServiceUnitID == toUnitId && ib.ItemID == c.ItemID);
                    if (balanceTo != null)
                    {
                        if (balanceTo.Balance >= c.Qty)
                            balanceTo.Balance -= c.Qty;
                        else
                            balanceTo.Balance = 0;

                        balanceTo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balanceTo.LastUpdateByUserID = userId;
                    }
                    
                    //cssd unit
                    var balanceFrom = itemBalancesFrom.SingleOrDefault(ib => ib.ServiceUnitID == cssdUnitId && ib.ItemID == c.ItemID);
                    if (balanceFrom != null)
                    {
                        balanceFrom.Balance += c.Qty;
                        balanceFrom.BalanceSterilization += c.Qty;

                        if (balanceFrom.BalanceDistribution >= c.Qty)
                            balanceFrom.BalanceDistribution -= c.Qty;
                        else
                            balanceFrom.BalanceDistribution = 0;

                        balanceFrom.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balanceFrom.LastUpdateByUserID = userId;
                    }
                    else
                    {
                        balanceFrom = itemBalancesTo.AddNew();
                        balanceFrom.ServiceUnitID = cssdUnitId;
                        balanceFrom.ItemID = c.ItemID;
                        balanceFrom.Balance = c.Qty;
                        balanceFrom.BalanceSterilization = c.Qty;
                        balanceFrom.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balanceFrom.LastUpdateByUserID = userId;
                    }
                }
            }
        }

        public static void PrepareItemBalanceReturn(string transactionNo, string cssdUnitId, string toUnitId, string userId, bool isApproved,
            ref CssdItemBalanceCollection itemBalancesFrom, ref CssdItemBalanceCollection itemBalancesTo)
        {
            var coll = new CssdSterileItemsReturnedItemCollection();
            var query = new CssdSterileItemsReturnedItemQuery("a");
            var process = new CssdSterilizationProcessItemQuery("b");
            var receive = new CssdSterileItemsReceivedItemQuery("c");
            query.InnerJoin(process).On(process.ProcessNo == query.ProcessNo && process.ProcessSeqNo == query.ProcessSeqNo);
            query.InnerJoin(receive).On(receive.ReceivedNo == process.ReceivedNo && receive.ReceivedSeqNo == process.ReceivedSeqNo);
            query.Select(query, receive.ItemID.As("refToCssdSterileItemsReceivedItem_ItemID"));
            query.Where(query.ReturnNo == transactionNo);
            query.OrderBy(receive.ItemID.Ascending);
            coll.Load(query);

            var items = (coll.Select(i => i.ItemID)).Distinct();

            itemBalancesFrom.Query.Where
                (
                    itemBalancesFrom.Query.ServiceUnitID == cssdUnitId,
                    itemBalancesFrom.Query.ItemID.In(items)
                );
            itemBalancesFrom.LoadAll();

            itemBalancesTo.Query.Where
                (
                    itemBalancesTo.Query.ServiceUnitID == toUnitId,
                    itemBalancesTo.Query.ItemID.In(items)
                );
            itemBalancesTo.LoadAll();

            if (isApproved)
            {
                foreach (var c in coll)
                {
                    //other unit
                    var balanceTo = itemBalancesTo.SingleOrDefault(ib => ib.ServiceUnitID == toUnitId && ib.ItemID == c.ItemID);
                    if (balanceTo != null)
                    {
                        balanceTo.Balance += c.Qty;

                        balanceTo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balanceTo.LastUpdateByUserID = userId;
                    }
                    else
                    {
                        balanceTo = itemBalancesTo.AddNew();
                        balanceTo.ServiceUnitID = toUnitId;
                        balanceTo.ItemID = c.ItemID;
                        balanceTo.Balance = c.Qty;
                        balanceTo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balanceTo.LastUpdateByUserID = userId;
                    }

                    //cssd unit
                    var balanceFrom = itemBalancesFrom.SingleOrDefault(ib => ib.ServiceUnitID == cssdUnitId && ib.ItemID == c.ItemID);
                    if (balanceFrom != null)
                    {
                        if (balanceFrom.Balance >= c.Qty)
                            balanceFrom.Balance -= c.Qty;
                        else
                            balanceFrom.Balance = 0;

                        if (balanceFrom.BalanceSterilization >= c.Qty)
                            balanceFrom.BalanceSterilization -= c.Qty;
                        else
                            balanceFrom.BalanceSterilization = 0;

                        balanceFrom.BalanceReturned += c.Qty;

                        balanceFrom.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balanceFrom.LastUpdateByUserID = userId;
                    }
                }
            }
            else //isApproved = false
            {
                foreach (var c in coll)
                {
                    //other unit
                    var balanceTo = itemBalancesTo.SingleOrDefault(ib => ib.ServiceUnitID == toUnitId && ib.ItemID == c.ItemID);
                    if (balanceTo != null)
                    {
                        if (balanceTo.Balance >= c.Qty)
                            balanceTo.Balance -= c.Qty;
                        else
                            balanceTo.Balance = 0;

                        balanceTo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balanceTo.LastUpdateByUserID = userId;
                    }

                    //cssd unit
                    var balanceFrom = itemBalancesFrom.SingleOrDefault(ib => ib.ServiceUnitID == cssdUnitId && ib.ItemID == c.ItemID);
                    if (balanceFrom != null)
                    {
                        balanceFrom.Balance += c.Qty;
                        balanceFrom.BalanceSterilization += c.Qty;

                        if (balanceFrom.BalanceReturned >= c.Qty)
                            balanceFrom.BalanceReturned -= c.Qty;
                        else
                            balanceFrom.BalanceReturned = 0;

                        balanceFrom.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balanceFrom.LastUpdateByUserID = userId;
                    }
                    else
                    {
                        balanceFrom = itemBalancesTo.AddNew();
                        balanceFrom.ServiceUnitID = cssdUnitId;
                        balanceFrom.ItemID = c.ItemID;
                        balanceFrom.Balance = c.Qty;
                        balanceFrom.BalanceSterilization = c.Qty;
                        balanceFrom.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        balanceFrom.LastUpdateByUserID = userId;
                    }
                }
            }
        }

        public static void PrepareItemBalancesForStockOpname(CssdStockOpnameItemCollection cssdStockOpnameItems, string unitId, string userId, ref CssdItemBalanceCollection itemBalances)
        {
            var items = (cssdStockOpnameItems.Select(i => i.ItemID)).Distinct();

            itemBalances.Query.Where
                (
                    itemBalances.Query.ServiceUnitID == unitId,
                    itemBalances.Query.ItemID.In(items)
                );
            itemBalances.LoadAll();

            foreach (var c in cssdStockOpnameItems)
            {
                //cssd unit
                var balance = itemBalances.SingleOrDefault(ib => ib.ServiceUnitID == unitId && ib.ItemID == c.ItemID);
                if (balance != null)
                {
                    balance.Balance = c.Balance;
                    balance.BalanceReceived = c.BalanceReceived;
                    balance.BalanceDeconImmersion = c.BalanceDeconImmersion;
                    balance.BalanceDeconAbstersion = c.BalanceDeconAbstersion;
                    balance.BalanceDeconDrying = c.BalanceDeconDrying;
                    balance.BalanceFeasibilityTest = c.BalanceFeasibilityTest;
                    balance.BalancePackaging = c.BalancePackaging;
                    balance.BalanceUltrasound = c.BalanceUltrasound;
                    balance.BalanceSterilization = c.BalanceSterilization;
                    balance.BalanceDistribution = c.BalanceDistribution;
                    balance.BalanceReturned = c.BalanceReturned;

                    balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    balance.LastUpdateByUserID = userId;
                }
                else
                {
                    balance = itemBalances.AddNew();
                    balance.ServiceUnitID = unitId;
                    balance.ItemID = c.ItemID;
                    balance.Balance = c.Balance;
                    balance.BalanceReceived = c.BalanceReceived;
                    balance.BalanceDeconImmersion = c.BalanceDeconImmersion;
                    balance.BalanceDeconAbstersion = c.BalanceDeconAbstersion;
                    balance.BalanceDeconDrying = c.BalanceDeconDrying;
                    balance.BalanceFeasibilityTest = c.BalanceFeasibilityTest;
                    balance.BalancePackaging = c.BalancePackaging;
                    balance.BalanceUltrasound = c.BalanceUltrasound;
                    balance.BalanceSterilization = c.BalanceSterilization;
                    balance.BalanceDistribution = c.BalanceDistribution;
                    balance.BalanceReturned = c.BalanceReturned;

                    balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    balance.LastUpdateByUserID = userId;
                }
            }
        }


        public string ServiceUnitName
        {
            get { return GetColumn("refToServiceUnit_ServiceUnitName").ToString(); }
            set { SetColumn("refToServiceUnit_ServiceUnitName", value); }
        }
    }
}

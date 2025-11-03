/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 7/27/2011 11:29:31 PM
===============================================================================
*/

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Temiang.Dal.Interfaces;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.BusinessObject
{
	public partial class CashTransactionDetail : esCashTransactionDetail
	{
        /// <summary>
        /// Chart Of Account Code
        /// </summary>
        public string ChartOfAccountCode
        {
            get { return (string)this.GetColumn("ChartOfAccountCode"); }
        }
        /// <summary>
        /// Chart Of Account Name
        /// </summary>
        public string ChartOfAccountName
        {
            get { return (string)this.GetColumn("ChartOfAccountName"); }
        }
        /// <summary>
        /// SubLedger name
        /// </summary>
        public string SubLedgerName
        {
            get
            {
                return this.SubLedgerId == 0 ? string.Empty : string.Format("{0} - {1}", this.GetColumn("SubLedgerName"), this.GetColumn("SubLedger_Description"));
            }
        }

        public static CashTransactionDetailCollection GetAllForTransactions(int transactionId)
        {
            var h = new CashTransactionQuery("h");
            var d = new CashTransactionDetailQuery("j");
            var c = new ChartOfAccountsQuery("c");
            var s = new SubLedgersQuery("s");

            d.Select(d, c.ChartOfAccountCode, c.ChartOfAccountName, s.SubLedgerName, s.Description.As("SubLedger_Description"));
            d.InnerJoin(h).On(d.TransactionId == h.TransactionId);
            d.InnerJoin(c).On(d.ChartOfAccountId == c.ChartOfAccountId);
            d.LeftJoin(s).On(d.SubLedgerId == s.SubLedgerId);
            d.Where(h.TransactionId == transactionId);

            var coll = new CashTransactionDetailCollection();
            coll.Query.es.WithNoLock = true;
            coll.Load(d);
            return coll;
        }

        public static CashTransactionDetailCollection GetAllForTransactionWithPaging(int transactionId, int pageNumber, int pageSize)
        {
            var h = new CashTransactionQuery("h");
            var d = new CashTransactionDetailQuery("j");
            var c = new ChartOfAccountsQuery("c");
            var s = new SubLedgersQuery("s");

            d.Select(d, c.ChartOfAccountCode, c.ChartOfAccountName, s.SubLedgerName, s.Description.As("SubLedger_Description"));
            d.InnerJoin(h).On(d.TransactionId == h.TransactionId);
            d.InnerJoin(c).On(d.ChartOfAccountId == c.ChartOfAccountId);
            d.LeftJoin(s).On(d.SubLedgerId == s.SubLedgerId);
            d.Where(h.TransactionId == transactionId);
            d.OrderBy(d.DetailId.Ascending);

            d.es.PageSize = pageSize;
            d.es.PageNumber = pageNumber + 1;
            d.es.WithNoLock = true;

            var coll = new CashTransactionDetailCollection();
            coll.Load(d);
            return coll;
        }

        public static int TotalCount(int transactionId)
        {
            int retVal = 0;
            var entity = new CashTransactionDetail();

            entity.Query.es.CountAll = true;
            entity.Query.es.CountAllAlias = "Count";
            entity.Query.es.WithNoLock = true;
            entity.Query.Where(entity.Query.TransactionId == transactionId);

            if (entity.Query.Load())
                retVal = (int)entity.GetColumn("Count");

            return retVal;
        }

        public static CashTransactionDetail Get(int transactionId, int detailId)
        {
            var h = new CashTransactionQuery("h");
            var j = new CashTransactionDetailQuery("j");
            var c = new ChartOfAccountsQuery("c");
            var s = new SubLedgersQuery("s");

            j.Select(j, c.ChartOfAccountCode, c.ChartOfAccountName, s.SubLedgerName, s.Description.As("SubLedger_Description"));
            j.InnerJoin(h).On(j.TransactionId == h.TransactionId);
            j.InnerJoin(c).On(j.ChartOfAccountId == c.ChartOfAccountId);
            j.LeftJoin(s).On(j.SubLedgerId == s.SubLedgerId);
            j.Where(h.TransactionId == transactionId, j.DetailId == detailId);

            var e = new CashTransactionDetail();
            e.Query.es.WithNoLock = true;
            if (e.Load(j))
                return e;
            
            return null;
        }

        public void SetLinkToPaymentReceive(TransPaymentItem tpi)
        {
            tpi.CashTransactionReconcileId = this.DetailId;
        }

        public TransPaymentItem SetUnLinkToPaymentReceive()
        {
            var tpi = new TransPaymentItem();
            tpi.Query.Where(tpi.Query.CashTransactionReconcileId == this.DetailId);
            if (tpi.Load(tpi.Query))
            {
                tpi.CashTransactionReconcileId = null;
                return tpi;
            }
            return null;
        }

        public void SetLinkToPaymentReceiveReturn(TransPaymentItem tpi)
        {
            tpi.BackOfficeReturnTransactionId = this.DetailId;
        }

        public TransPaymentItem SetUnLinkToPaymentReceiveReturn()
        {
            var tpi = new TransPaymentItem();
            tpi.Query.Where(tpi.Query.BackOfficeReturnTransactionId == this.DetailId);
            if (tpi.Load(tpi.Query))
            {
                tpi.BackOfficeReturnTransactionId = null;
                return tpi;
            }
            return null;
        }
	}
}

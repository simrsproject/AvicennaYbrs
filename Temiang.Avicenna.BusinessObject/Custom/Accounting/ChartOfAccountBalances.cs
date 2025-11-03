/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 3/3/2010 4:56:15 PM
===============================================================================
*/

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
	public partial class ChartOfAccountBalances : esChartOfAccountBalances
	{

        /// <summary>
        /// Additional properties
        /// </summary>
        /// <returns></returns>
        protected override List<esPropertyDescriptor> GetLocalBindingProperties()
        {
            List<esPropertyDescriptor> items = new List<esPropertyDescriptor>();

            items.Add(new esPropertyDescriptor(this, "ChartOfAccountCode", typeof(string)));
            items.Add(new esPropertyDescriptor(this, "ChartOfAccountName", typeof(string)));

            return items;
        }
        /// <summary>
        /// Chart OF Account Code
        /// </summary>
        public string ChartOfAccountCode
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("ChartOfAccountCode") is System.DBNull ? string.Empty : (string)this.GetColumn("ChartOfAccountCode");
                return retVal;
            }
        }
        /// <summary>
        /// Chart of Account Name
        /// </summary>
        public string ChartOfAccountName
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("ChartOfAccountName") is System.DBNull ? string.Empty : (string)this.GetColumn("ChartOfAccountName");
                return retVal;
            }
        }

        public static ChartOfAccountBalances Get(int chartOfAccountId, string month, string year)
        {
            ChartOfAccountBalancesQuery q = new ChartOfAccountBalancesQuery("q");
            q.Select(q);
            q.Where(q.ChartOfAccountId == chartOfAccountId, q.Month == month, q.Year == year);
            q.es.WithNoLock = true;

            ChartOfAccountBalances entity = new ChartOfAccountBalances();
            if (entity.Load(q))
            {
                return entity;
            }
            return null;
        }

        public static ChartOfAccountBalancesCollection GetDistinctYear()
        {
            ChartOfAccountsQuery c = new ChartOfAccountsQuery("c");
            ChartOfAccountBalancesQuery b = new ChartOfAccountBalancesQuery("b");

            b.es.Distinct = true;
            b.Select(b.Year.Distinct());
            b.InnerJoin(c).On(c.ChartOfAccountId == b.ChartOfAccountId);
            b.OrderBy(b.Year.Ascending);
            b.es.WithNoLock = true;

            ChartOfAccountBalancesCollection coll = new ChartOfAccountBalancesCollection();
            coll.Load(b);
            return coll;
        }

        public static ChartOfAccountBalancesCollection GetDistinctMonth(string year)
        {
            ChartOfAccountsQuery c = new ChartOfAccountsQuery("c");
            ChartOfAccountBalancesQuery b = new ChartOfAccountBalancesQuery("b");

            b.es.Distinct = true;
            b.Select(b.Month.Distinct());
            b.InnerJoin(c).On(c.ChartOfAccountId == b.ChartOfAccountId);
            b.OrderBy(b.Month.Ascending);
            b.Where(b.Year == year);
            b.es.WithNoLock = true;

            ChartOfAccountBalancesCollection coll = new ChartOfAccountBalancesCollection();
            coll.Load(b);
            return coll;
        }

        public static ChartOfAccountBalancesCollection GetAllWithPaging(int pageNumber, int pageSize, string month, string year, int[] coaids)
        {
            ChartOfAccountsQuery c = new ChartOfAccountsQuery("c");
            ChartOfAccountBalancesQuery b = new ChartOfAccountBalancesQuery("b");

            b.Select(b, c.ChartOfAccountCode, c.ChartOfAccountName);
            b.InnerJoin(c).On(c.ChartOfAccountId == b.ChartOfAccountId);
            b.Where(b.Month == month, b.Year == year);
            if (coaids.Length > 0) {
                b.Where(c.ChartOfAccountId.In(coaids));
            }
            b.OrderBy(c.ChartOfAccountCode.Ascending);
            b.es.WithNoLock = true;
            b.es.PageSize = pageSize;
            b.es.PageNumber = pageNumber + 1;

            ChartOfAccountBalancesCollection coll = new ChartOfAccountBalancesCollection();
            coll.Load(b);
            return coll;
        }

        public static TotalBalance GetTotal(string month, string year)
        {
            ChartOfAccountBalancesQuery b = new ChartOfAccountBalancesQuery("b");

            b.Select(b.InitialBalance.Sum().As("TotalInitialBalance"), b.DebitAmount.Sum().As("TotalDebitAmount"),
                b.CreditAmount.Sum().As("TotalCreditAmount"), b.FinalBalance.Sum().As("TotalFinalBalance"));
            b.Where(b.Month == month, b.Year == year);
            b.es.WithNoLock = true;

            ChartOfAccountBalances entity = new ChartOfAccountBalances();
            TotalBalance ret = new TotalBalance();
            if (entity.Load(b))
            {
                ret.InitialBalance = entity.GetColumn("TotalInitialBalance") is System.DBNull ? 0 : (decimal)entity.GetColumn("TotalInitialBalance");
                ret.DebitAmount = entity.GetColumn("TotalDebitAmount") is System.DBNull ? 0 : (decimal)entity.GetColumn("TotalDebitAmount");
                ret.CreditAmount = entity.GetColumn("TotalCreditAmount") is System.DBNull ? 0 : (decimal)entity.GetColumn("TotalCreditAmount");
                ret.FinalBalance = entity.GetColumn("TotalFinalBalance") is System.DBNull ? 0 : (decimal)entity.GetColumn("TotalFinalBalance");
            }
            return ret;
        }

        public static int TotalCount(string month, string year, int[] coaids)
        {
            int retVal = 0;
            ChartOfAccountBalances entity = new ChartOfAccountBalances();
            List<esComparison> prms = new List<esComparison>();

            prms.Add(entity.Query.Month == month);
            prms.Add(entity.Query.Year == year);

            entity.Query.es.CountAll = true;
            entity.Query.es.CountAllAlias = "Count";
            entity.Query.es.WithNoLock = true;
            entity.Query.Where(prms.ToArray());
            if (coaids.Length > 0)
            {
                entity.Query.Where(entity.Query.ChartOfAccountId.In(coaids));
            }

            if (entity.Query.Load())
                retVal = (int)entity.GetColumn("Count");

            return retVal;
        }

        public class TotalBalance
        {
            public decimal InitialBalance;
            public decimal FinalBalance;
            public decimal DebitAmount;
            public decimal CreditAmount;

            public TotalBalance()
            {
                this.InitialBalance = 0;
                this.FinalBalance = 0;
                this.DebitAmount = 0;
                this.CreditAmount = 0;
            }
        }
	}
}

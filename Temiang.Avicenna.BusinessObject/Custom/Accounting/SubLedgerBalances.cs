/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 3/3/2010 4:56:16 PM
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
	public partial class SubLedgerBalances : esSubLedgerBalances
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
            items.Add(new esPropertyDescriptor(this, "SubLedgerName", typeof(string)));
            items.Add(new esPropertyDescriptor(this, "SubLedgerName_Description", typeof(string)));
            items.Add(new esPropertyDescriptor(this, "GroupName", typeof(string)));
            items.Add(new esPropertyDescriptor(this, "GroupCode", typeof(string)));
            return items;
        }
        /// <summary>
        /// Subledger Group Description
        /// </summary>
        public string GroupName
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("GroupName") is System.DBNull ? string.Empty : (string)this.GetColumn("GroupName");
                return retVal;
            }
        }
        /// <summary>
        /// Subledger Group Code
        /// </summary>
        public string GroupCode
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("GroupCode") is System.DBNull ? string.Empty : (string)this.GetColumn("GroupCode");
                return retVal;
            }
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
        /// <summary>
        /// Sub Ledger Name
        /// </summary>
        public string SubLedgerName
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("SubLedgerName") is System.DBNull ? string.Empty : (string)this.GetColumn("SubLedgerName");
                return retVal;
            }
        }
        /// <summary>
        /// Sub Ledger Description
        /// </summary>
        public string SubLedgerNameDescription
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("SubLedgerName_Description") is System.DBNull ? string.Empty : (string)this.GetColumn("SubLedgerName_Description");
                return retVal;
            }
        }

        public static int TotalCount(string month, string year, string coacode, string coaname, string slname)
        {
            //int retVal = 0;
            //SubLedgerBalances entity = new SubLedgerBalances();
            //List<esComparison> prms = new List<esComparison>();

            //prms.Add(entity.Query.Month == month);
            //prms.Add(entity.Query.Year == year);

            //entity.Query.es.CountAll = true;
            //entity.Query.es.CountAllAlias = "Count";
            //entity.Query.es.WithNoLock = true;
            //entity.Query.Where(prms.ToArray());

            //if (entity.Query.Load())
            //    retVal = (int)entity.GetColumn("Count");

            //return retVal;

            SubLedgerGroupsQuery g = new SubLedgerGroupsQuery("g");
            SubLedgerBalancesQuery b = new SubLedgerBalancesQuery("b");
            SubLedgersQuery s = new SubLedgersQuery("s");
            ChartOfAccountsQuery c = new ChartOfAccountsQuery("c");

            b.Select(b.SubLedgerBalanceId.Count());
            b.InnerJoin(s).On(b.SubLedgerId == s.SubLedgerId);
            b.InnerJoin(g).On(s.GroupId == g.SubLedgerGroupId);
            b.InnerJoin(c).On(b.ChartOfAccountId == c.ChartOfAccountId);
            b.Where(b.Month == month, b.Year == year);

            if (!string.IsNullOrEmpty(coacode))
            {
                string searchTextContain = string.Format("%{0}%", coacode);
                b.Where(c.ChartOfAccountCode.Like(searchTextContain));
            }
            if (!string.IsNullOrEmpty(coaname))
            {
                string searchTextContain = string.Format("%{0}%", coaname);
                b.Where(c.ChartOfAccountName.Like(searchTextContain));
            }
            if (!string.IsNullOrEmpty(slname))
            {
                string searchTextContain = string.Format("%{0}%", slname);
                b.Where(s.Description.Like(searchTextContain));
            }

            b.es.WithNoLock = true;

            var dtb = b.LoadDataTable();
            return System.Convert.ToInt32(dtb.Rows[0][0]);
        }

        public static SubLedgerBalancesCollection GetAllWithPaging(int pageNumber, int pageSize, string month, string year,
            string coacode, string coaname, string slname)
        {
            SubLedgerGroupsQuery g = new SubLedgerGroupsQuery("g");
            SubLedgerBalancesQuery b = new SubLedgerBalancesQuery("b");
            SubLedgersQuery s = new SubLedgersQuery("s");
            ChartOfAccountsQuery c = new ChartOfAccountsQuery("c");

            b.Select(b, g.GroupCode, g.GroupName, s.SubLedgerName, s.Description.As("SubLedgerName_Description"), c.ChartOfAccountCode, c.ChartOfAccountName);
            b.InnerJoin(s).On(b.SubLedgerId == s.SubLedgerId);
            b.InnerJoin(g).On(s.GroupId == g.SubLedgerGroupId);
            b.InnerJoin(c).On(b.ChartOfAccountId == c.ChartOfAccountId);
            b.Where(b.Month == month, b.Year == year);
            b.OrderBy(b.ChartOfAccountId.Ascending, b.SubLedgerId.Ascending, s.GroupId.Ascending);

            if (!string.IsNullOrEmpty(coacode)) 
            {
                string searchTextContain = string.Format("%{0}%", coacode);
                b.Where(c.ChartOfAccountCode.Like(searchTextContain));
            }
            if (!string.IsNullOrEmpty(coaname))
            {
                string searchTextContain = string.Format("%{0}%", coaname);
                b.Where(c.ChartOfAccountName.Like(searchTextContain));
            }
            if (!string.IsNullOrEmpty(slname))
            {
                string searchTextContain = string.Format("%{0}%", slname);
                b.Where(s.Description.Like(searchTextContain));
            }

            b.es.WithNoLock = true;
            b.es.PageSize = pageSize;
            b.es.PageNumber = pageNumber + 1;

            SubLedgerBalancesCollection coll = new SubLedgerBalancesCollection();
            coll.Load(b);
            return coll;
        }

        public static SubLedgerBalancesCollection GetDistinctYear()
        {
            ChartOfAccountsQuery c = new ChartOfAccountsQuery("c");
            SubLedgerBalancesQuery b = new SubLedgerBalancesQuery("b");

            b.es.Distinct = true;
            b.Select(b.Year.Distinct());
            b.InnerJoin(c).On(c.ChartOfAccountId == b.ChartOfAccountId);
            b.OrderBy(b.Year.Ascending);
            b.es.WithNoLock = true;

            SubLedgerBalancesCollection coll = new SubLedgerBalancesCollection();
            coll.Load(b);
            return coll;
        }

        public static TotalBalance GetTotal(string month, string year)
        {
            SubLedgerBalancesQuery b = new SubLedgerBalancesQuery("b");

            b.Select(b.InitialBalance.Sum().As("TotalInitialBalance"), b.DebitAmount.Sum().As("TotalDebitAmount"),
                b.CreditAmount.Sum().As("TotalCreditAmount"), b.FinalBalance.Sum().As("TotalFinalBalance"));
            b.Where(b.Month == month, b.Year == year);
            b.es.WithNoLock = true;

            SubLedgerBalances entity = new SubLedgerBalances();
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

        public static SubLedgerBalances Get(int chartOfAccountId, int subLegerId, string month, string year)
        {
            SubLedgerBalancesQuery q = new SubLedgerBalancesQuery("q");
            q.Select(q);
            q.Where(q.ChartOfAccountId == chartOfAccountId, q.SubLedgerId == subLegerId, q.Month == month, q.Year == year);
            q.es.WithNoLock = true;

            SubLedgerBalances entity = new SubLedgerBalances();
            if (entity.Load(q))
            {
                return entity;
            }
            return null;
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

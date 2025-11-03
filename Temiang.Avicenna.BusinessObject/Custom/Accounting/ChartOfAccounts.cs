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
    public partial class ChartOfAccounts : esChartOfAccounts
    {
        public string ChartOfAccountNameForDisplay
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                int accountLevel = this.AccountLevel.HasValue ? this.AccountLevel.Value : 0;

                //if (accountLevel > 1)
                for (int i = 0; i < accountLevel; i++)
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");

                sb.Append(this.ChartOfAccountName);
                return sb.ToString();
            }
        }
        public static ChartOfAccounts Get(int chartOfAccountId)
        {
            ChartOfAccounts entity = new ChartOfAccounts();
            entity.Query.Where(entity.Query.ChartOfAccountId == chartOfAccountId);
            if (entity.Query.Load())
                return entity;
            else
                return null;
        }
        public static ChartOfAccountsCollection Get()
        {
            ChartOfAccountsCollection coll = new ChartOfAccountsCollection();
            coll.Query.Where();
            coll.Query.Load();
            return coll;
        }

        public static int TotalCount(string chartOfAccountCode, string chartOfAccountName, string accountLevel, string generalAccount)
        {
            int retVal = 0;
            ChartOfAccounts entity = new ChartOfAccounts();
            List<esComparison> prms = new List<esComparison>();

            if (!string.IsNullOrEmpty(chartOfAccountCode))
            {
                string searchTextContain = string.Format("{0}%", chartOfAccountCode);
                prms.Add(entity.Query.ChartOfAccountCode.Like(searchTextContain));
                //prms.Add(entity.Query.ChartOfAccountCode.Like(chartOfAccountCode + "%"));
            }
            if (!string.IsNullOrEmpty(chartOfAccountName))
            {
                string searchTextContain = string.Format("%{0}%", chartOfAccountName);
                prms.Add(entity.Query.ChartOfAccountName.Like(searchTextContain));
                //prms.Add(entity.Query.ChartOfAccountName.Like("%" + chartOfAccountName + "%"));
            }
            if (!string.IsNullOrEmpty(accountLevel))
            {
                int aLevel = 0;
                if (int.TryParse(accountLevel, out aLevel))
                    prms.Add(entity.Query.AccountLevel == aLevel);
            }
            if (!string.IsNullOrEmpty(generalAccount))
            {
                string searchTextContain = string.Format("%{0}%", generalAccount);
                prms.Add(entity.Query.GeneralAccount.Like(searchTextContain));
                //prms.Add(entity.Query.GeneralAccount.Like("%" + generalAccount + "%"));
            }
                
            entity.Query.es.CountAll = true;
            entity.Query.es.CountAllAlias = "Count";
            entity.Query.es.WithNoLock = true;
            if (prms.Count > 0)
                entity.Query.Where(prms.ToArray());

            if (entity.Query.Load())
                retVal = (int)entity.GetColumn("Count");

            return retVal;
        }

        private static esOrderByItem[] safeOrderByItems(string sortString)
        {
            List<esOrderByItem> list = new List<esOrderByItem>();
            string[] fieldsName = sortString.ToLowerInvariant().Split(char.Parse(","));
            foreach (string field in fieldsName)
            {
                ChartOfAccountsQuery q = new ChartOfAccountsQuery();
                string[] tmp = field.Split(char.Parse("^"));
                bool isDesc = false;
                if (tmp.Length > 1)
                    isDesc = tmp[1].Equals("descending");

                if (tmp[0].Equals("chartofaccountcode"))
                    list.Add(isDesc ? q.ChartOfAccountCode.Descending : q.ChartOfAccountCode.Ascending);
                if (tmp[0].Equals("chartofaccountname"))
                    list.Add(isDesc ? q.ChartOfAccountName.Descending : q.ChartOfAccountName.Ascending);
            }
            return list.ToArray();
        }

        public static ChartOfAccountsCollection GetAllWithPaging(int pageNumber, int pageSize, string chartOfAccountCode, string chartOfAccountName, string accountLevel, string generalAccount, string sortString)
        {
            ChartOfAccountsCollection coll = new ChartOfAccountsCollection();
            List<esComparison> prms = new List<esComparison>();

            if (!string.IsNullOrEmpty(chartOfAccountCode))
            {
                string searchTextContain = string.Format("{0}%", chartOfAccountCode);
                prms.Add(coll.Query.ChartOfAccountCode.Like(searchTextContain));
                //prms.Add(coll.Query.ChartOfAccountCode.Like(chartOfAccountCode + "%"));
            }
            if (!string.IsNullOrEmpty(chartOfAccountName))
            {
                string searchTextContain = string.Format("%{0}%", chartOfAccountName);
                prms.Add(coll.Query.ChartOfAccountName.Like(searchTextContain));
                //prms.Add(coll.Query.ChartOfAccountName.Like("%" + chartOfAccountName + "%"));
            }
            if (!string.IsNullOrEmpty(accountLevel))
            {
                int aLevel = 0;
                if (int.TryParse(accountLevel, out aLevel))
                    prms.Add(coll.Query.AccountLevel == aLevel);
            }
            if (!string.IsNullOrEmpty(generalAccount))
            {
                string searchTextContain = string.Format("%{0}%", generalAccount);
                prms.Add(coll.Query.GeneralAccount.Like(searchTextContain));
                //prms.Add(coll.Query.GeneralAccount.Like("%" + generalAccount + "%"));
            }
                
            coll.Query.OrderBy(safeOrderByItems(sortString));
            if (prms.Count > 0)
                coll.Query.Where(prms.ToArray());

            coll.Query.es.PageSize = pageSize;
            coll.Query.es.PageNumber = pageNumber + 1;
            coll.Query.es.WithNoLock = true;
            coll.Query.Load();
            return coll;
        }

        public static ChartOfAccountsCollection GetAll(string chartOfAccountCode, string chartOfAccountName, string accountLevel, string generalAccount, string sortString)
        {
            ChartOfAccountsCollection coll = new ChartOfAccountsCollection();
            
            var coa1 = new ChartOfAccountsQuery("a");
            var coa2 = new ChartOfAccountsQuery("b");

            List<esComparison> prms = new List<esComparison>();

            if (!string.IsNullOrEmpty(chartOfAccountCode))
            {
                string searchTextContain = string.Format("{0}%", chartOfAccountCode);
                prms.Add(coa1.ChartOfAccountCode.Like(searchTextContain));
                //prms.Add(coa1.ChartOfAccountCode.Like(chartOfAccountCode + "%"));
            }
            if (!string.IsNullOrEmpty(chartOfAccountName))
            {
                string searchTextContain = string.Format("%{0}%", chartOfAccountName);
                prms.Add(coa1.ChartOfAccountName.Like(searchTextContain));
                //prms.Add(coa1.ChartOfAccountName.Like("%" + chartOfAccountName + "%"));
            }
            if (!string.IsNullOrEmpty(accountLevel))
            {
                int aLevel = 0;
                if (int.TryParse(accountLevel, out aLevel))
                    prms.Add(coa1.AccountLevel == aLevel);
            }
            if (!string.IsNullOrEmpty(generalAccount))
            {
                string searchTextContain = string.Format("%{0}%", generalAccount);
                prms.Add(coa1.GeneralAccount.Like(searchTextContain));
                //prms.Add(coa1.GeneralAccount.Like("%" + generalAccount + "%"));
            }
                
            coa1.OrderBy(safeOrderByItems(sortString));
            if (prms.Count > 0)
                coa1.Where(prms.ToArray());

            coa1.LeftJoin(coa2).On(coa1.ChartOfAccountId == coa2.BkuAccountID);
            coa1.Select(coa1, coa2.ChartOfAccountCode.As("refTo_ChartOfAccountCode"));

            //coa1.es.WithNoLock = true;
            coll.Load(coa1);
            return coll;
        }

        public static ChartOfAccountsCollection GetLike(string chartOfAccountCode, bool detailOnly, bool activeOnly)
        {
            string searchTextContain1 = string.Format("{0}%", chartOfAccountCode);
            string searchTextContain2 = string.Format("%{0}%", chartOfAccountCode);
            ChartOfAccountsCollection coll = new ChartOfAccountsCollection();
            coll.Query.Where(coll.Query.ChartOfAccountCode.Like(searchTextContain1) || coll.Query.ChartOfAccountName.Like(searchTextContain2));
            if (detailOnly)
            {
                coll.Query.Where(coll.Query.IsDetail == true/*, coll.Query.AccountLevel == 4*/);
            }
            if (activeOnly)
            {
                coll.Query.Where(coll.Query.IsActive == true);
            }
            coll.Query.OrderBy(coll.Query.ChartOfAccountCode.Ascending);
            coll.Query.es.WithNoLock = true;
            coll.Query.Load();
            return coll;

        }

        public static ChartOfAccountsCollection GetLike(string chartOfAccountCode, bool detailOnly)
        {
            return GetLike(chartOfAccountCode, detailOnly, false);
        }

        public static ChartOfAccounts Get(string chartOfAccountCode)
        {
            ChartOfAccounts e = new ChartOfAccounts();
            e.Query.Where(e.Query.ChartOfAccountCode == chartOfAccountCode);
            e.Query.es.WithNoLock = true;
            if (e.Query.Load())
                return e;
            return null;
        }

        public static ChartOfAccounts GetById(int Id)
        {
            ChartOfAccounts e = new ChartOfAccounts();
            e.Query.Where(e.Query.ChartOfAccountId == Id);
            e.Query.es.WithNoLock = true;
            if (e.Query.Load())
                return e;
            return null;
        }
    }

    public partial class ChartOfAccounts
    {
        public string BkuAccountCode
        {
            get { return GetColumn("refTo_ChartOfAccountCode").ToString(); }
            set { SetColumn("refTo_ChartOfAccountCode", value); }
        }
    }
}

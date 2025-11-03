/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 10/24/2011 11:56:42 PM
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
	public partial class AssetGroup : esAssetGroup
	{
        public string ChartOfAccountCode
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("ChartOfAccountCode") is System.DBNull ? string.Empty : (string)this.GetColumn("ChartOfAccountCode");
                return retVal;
            }
        }
        public string ChartOfAccountName
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("ChartOfAccountName") is System.DBNull ? string.Empty : (string)this.GetColumn("ChartOfAccountName");
                return retVal;
            }
        }

        public string CoaAssetDepreciationCode
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("CoaAssetDepreciationCode") is System.DBNull ? string.Empty : (string)this.GetColumn("CoaAssetDepreciationCode");
                return retVal;
            }
        }
        public string CoaAssetDepreciationName
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("CoaAssetDepreciationName") is System.DBNull ? string.Empty : (string)this.GetColumn("CoaAssetDepreciationName");
                return retVal;
            }
        }

        public string CoaCostOfDepreciationCode
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("CoaCostOfDepreciationCode") is System.DBNull ? string.Empty : (string)this.GetColumn("CoaCostOfDepreciationCode");
                return retVal;
            }
        }
        public string CoaCostOfDepreciationName
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("CoaCostOfDepreciationName") is System.DBNull ? string.Empty : (string)this.GetColumn("CoaCostOfDepreciationName");
                return retVal;
            }
        }

        public string CoaCostOfDestructionCode
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("CoaCostOfDestructionCode") is System.DBNull ? string.Empty : (string)this.GetColumn("CoaCostOfDestructionCode");
                return retVal;
            }
        }
        public string CoaCostOfDestructionName
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("CoaCostOfDestructionName") is System.DBNull ? string.Empty : (string)this.GetColumn("CoaCostOfDestructionName");
                return retVal;
            }
        }

        protected override List<esPropertyDescriptor> GetLocalBindingProperties()
        {
            List<esPropertyDescriptor> items = new List<esPropertyDescriptor>();

            items.Add(new esPropertyDescriptor(this, "ChartOfAccountCode", typeof(string)));
            items.Add(new esPropertyDescriptor(this, "ChartOfAccountName", typeof(string)));

            return items;
        }

        public static int TotalCount(string assetGroupId, string groupName)
        {
            int retVal = 0;
            var entity = new AssetGroup();
            var prms = new List<esComparison>();

            if (!string.IsNullOrEmpty(assetGroupId))
                prms.Add(entity.Query.AssetGroupId.Like(assetGroupId + "%"));
            if (!string.IsNullOrEmpty(groupName))
                prms.Add(entity.Query.GroupName.Like(groupName + "%"));

            entity.Query.es.CountAll = true;
            entity.Query.es.CountAllAlias = "Count";
            entity.Query.es.WithNoLock = true;
            if (prms.Count > 0)
                entity.Query.Where(prms.ToArray());

            if (entity.Query.Load())
                retVal = (int)entity.GetColumn("Count");

            return retVal;
        }

        public static AssetGroupCollection GetAllWithPaging(int pageNumber, int pageSize, string assetGroupId, string groupName, string sortString)
        {
            var g = new AssetGroupQuery("g");
            var c = new ChartOfAccountsQuery("c");
            var d = new ChartOfAccountsQuery("d");
            var e = new ChartOfAccountsQuery("e");
            var f = new ChartOfAccountsQuery("f");

            g.Select(
                g, 
                c.ChartOfAccountCode, c.ChartOfAccountName,
                d.ChartOfAccountCode.As("CoaAssetDepreciationCode"), d.ChartOfAccountName.As("CoaAssetDepreciationName"),
                e.ChartOfAccountCode.As("CoaCostOfDepreciationCode"), e.ChartOfAccountName.As("CoaCostOfDepreciationName"),
                f.ChartOfAccountCode.As("CoaCostOfDestructionCode"), f.ChartOfAccountName.As("CoaCostOfDestructionName")
                );
            g.LeftJoin(c).On(g.AssetAccountId == c.ChartOfAccountId);
            g.LeftJoin(d).On(g.AssetAccumulationAccountId == d.ChartOfAccountId);
            g.LeftJoin(e).On(g.AssetCostAccountId == e.ChartOfAccountId);
            g.LeftJoin(f).On(g.AssetCostDestructionAccountId == f.ChartOfAccountId);

            var prms = new List<esComparison>();

            if (!string.IsNullOrEmpty(assetGroupId))
                prms.Add(g.AssetGroupId.Like(assetGroupId + "%"));
            if (!string.IsNullOrEmpty(groupName))
                prms.Add(g.GroupName.Like(groupName + "%"));

            g.OrderBy(safeOrderByItems(sortString));
            if (prms.Count > 0)
                g.Where(prms.ToArray());
            
            g.es.PageSize = pageSize;
            g.es.PageNumber = pageNumber + 1;
            g.es.WithNoLock = true;

            var coll = new AssetGroupCollection();
            coll.Load(g);
            return coll;
        }

        private static esOrderByItem[] safeOrderByItems(string sortString)
        {
            var list = new List<esOrderByItem>();
            var fieldsName = sortString.ToLowerInvariant().Split(char.Parse(","));
            foreach (string field in fieldsName)
            {
                var q = new AssetGroupQuery();
                var tmp = field.Split(char.Parse("^"));
                bool isDesc = false;
                if (tmp.Length > 1)
                    isDesc = tmp[1].Equals("descending");

                if (tmp[0].Equals("assetgroupid"))
                    list.Add(isDesc ? q.AssetGroupId.Descending : q.AssetGroupId.Ascending);
                if (tmp[0].Equals("groupname"))
                    list.Add(isDesc ? q.GroupName.Descending : q.GroupName.Ascending);
            }
            return list.ToArray();
        }

        public static AssetGroup Get(string groupId)
        {
            if (string.IsNullOrEmpty(groupId))
                return null;

            var entity = new AssetGroup();
            entity.Query.Where(entity.Query.AssetGroupId == groupId);
            if (entity.Query.Load())
                return entity;
            else
                return null;
        }
	}
}

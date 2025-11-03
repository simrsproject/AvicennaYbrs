/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 10/24/2011 11:56:43 PM
===============================================================================
*/

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.BusinessObject
{
	public partial class AssetMaintenanceHd : esAssetMaintenanceHd
	{
        public string AssetName
        {
            get
            {
                return this.GetColumn("AssetName") is System.DBNull ? string.Empty : (string)this.GetColumn("AssetName");
            }
        }

        protected override List<esPropertyDescriptor> GetLocalBindingProperties()
        {
            var items = new List<esPropertyDescriptor>
                            {
                                new esPropertyDescriptor(this, "AssetName", typeof (string))
                            };

            return items;
        }

        public static int TotalCount(string assetId, string transactionNo)
        {
            int retVal = 0;
            var entity = new AssetMaintenanceHd();

            var prms = new List<esComparison>();

            if (!string.IsNullOrEmpty(assetId))
                prms.Add(entity.Query.AssetID.Like(assetId + "%"));
            if (!string.IsNullOrEmpty(transactionNo))
                prms.Add(entity.Query.TransactionNo.Like(transactionNo + "%"));

            if (prms.Count > 0)
                entity.Query.Where(prms.ToArray());

            entity.Query.es.CountAll = true;
            entity.Query.es.CountAllAlias = "Count";
            entity.Query.es.WithNoLock = true;

            if (entity.Query.Load())
                retVal = (int)entity.GetColumn("Count");

            return retVal;
        }

        public static AssetMaintenanceHdCollection GetAllWithPaging(int pageNumber, int pageSize, string assetId, string transactionNo, string sortString)
        {
            var q = new AssetMaintenanceHdQuery("h");
            var assetQuery = new AssetQuery("e");

            q.Select(q, assetQuery.AssetName.As("AssetName"));
            q.InnerJoin(assetQuery).On(q.AssetID == assetQuery.AssetID);

            var prms = new List<esComparison>();
            if (!string.IsNullOrEmpty(assetId))
                prms.Add(q.AssetID.Like(assetId + "%"));
            if (!string.IsNullOrEmpty(transactionNo))
                prms.Add(q.TransactionNo.Like(transactionNo + "%"));

            if (prms.Count > 0)
                q.Where(prms.ToArray());

            q.OrderBy(safeOrderByItems(sortString));

            q.es.PageSize = pageSize;
            q.es.PageNumber = pageNumber + 1;
            q.es.WithNoLock = true;

            var coll = new AssetMaintenanceHdCollection();
            coll.Load(q);
            return coll;
        }

        private static esOrderByItem[] safeOrderByItems(string sortString)
        {
            var list = new List<esOrderByItem>();
            var fieldsName = sortString.ToLowerInvariant().Split(char.Parse(","));
            foreach (string field in fieldsName)
            {
                var q = new AssetMaintenanceHdQuery();
                var tmp = field.Split(char.Parse("^"));
                bool isDesc = false;
                if (tmp.Length > 1)
                    isDesc = tmp[1].Equals("descending");

                if (tmp[0].Equals("transactionno"))
                    list.Add(isDesc ? q.TransactionNo.Descending : q.TransactionNo.Ascending);
                if (tmp[0].Equals("assetid"))
                    list.Add(isDesc ? q.AssetID.Descending : q.AssetID.Ascending);
            }
            return list.ToArray();
        }

	    public static int TotalCountByAssetId(string assetId)
        {
            if (string.IsNullOrEmpty(assetId))
                return 0;

            int retVal = 0;
            var entity = new AssetMaintenanceHd();

            entity.Query.es.CountAll = true;
            entity.Query.es.CountAllAlias = "Count";
            entity.Query.es.WithNoLock = true;
            
            entity.Query.Where(entity.Query.AssetID == assetId && entity.Query.IsPosted == true);
            if (entity.Query.Load())
                retVal = (int)entity.GetColumn("Count");

            return retVal;
        }

        public static AssetMaintenanceHdCollection GetAllWithPagingByAssetId(string assetId, int pageNumber, int pageSize)
        {
            if (string.IsNullOrEmpty(assetId))
                return new AssetMaintenanceHdCollection();

            var q = new AssetMaintenanceHdQuery("h");

            q.Select(q);
            q.Where(q.AssetID == assetId && q.IsPosted == true);
            q.OrderBy(q.TransactionNo.Descending);

            q.es.PageSize = pageSize;
            q.es.PageNumber = pageNumber + 1;
            q.es.WithNoLock = true;

            var coll = new AssetMaintenanceHdCollection();
            coll.Load(q);
            return coll;
        }

        public static AssetMaintenanceHd Get(string transactionNo)
        {
            var entity = new AssetMaintenanceHd();
            entity.Query.Where(entity.Query.TransactionNo == transactionNo);
            return entity.Query.Load() ? entity : null;
        }
	}
}

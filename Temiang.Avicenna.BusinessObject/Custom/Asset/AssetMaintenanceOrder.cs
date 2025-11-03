/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 10/24/2011 11:56:44 PM
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
	public partial class AssetMaintenanceOrder : esAssetMaintenanceOrder
	{
        public string FromServiceUnitName
        {
            get
            {
                return this.GetColumn("FromServiceUnitName") is System.DBNull ? string.Empty : (string)this.GetColumn("FromServiceUnitName");
            }
        }
        public string FromLocationName
        {
            get
            {
                return this.GetColumn("FromLocationName") is System.DBNull ? string.Empty : (string)this.GetColumn("FromLocationName");
            }
        }
        public string ToServiceUnitName
        {
            get
            {
                return this.GetColumn("ToServiceUnitName") is System.DBNull ? string.Empty : (string)this.GetColumn("ToServiceUnitName");
            }
        }
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
                                new esPropertyDescriptor(this, "FromServiceUnitName", typeof (string)),
                                new esPropertyDescriptor(this, "FromLocationName", typeof (string)),
                                new esPropertyDescriptor(this, "ToServiceUnitName", typeof (string)),
                                new esPropertyDescriptor(this, "AssetName", typeof (string))
                            };

            return items;
        }

        public static int TotalCount(string assetId, string jobOrderNo)
        {
            int retVal = 0;
            var entity = new AssetMaintenanceOrder();

            var prms = new List<esComparison>();

            if (!string.IsNullOrEmpty(assetId))
                prms.Add(entity.Query.AssetID.Like(assetId + "%"));
            if (!string.IsNullOrEmpty(jobOrderNo))
                prms.Add(entity.Query.JobOrderNo.Like(jobOrderNo + "%"));

            if (prms.Count > 0)
                entity.Query.Where(prms.ToArray());

            entity.Query.es.CountAll = true;
            entity.Query.es.CountAllAlias = "Count";
            entity.Query.es.WithNoLock = true;

            if (entity.Query.Load())
                retVal = (int)entity.GetColumn("Count");

            return retVal;
        }

        public static AssetMaintenanceOrderCollection GetAllWithPaging(int pageNumber, int pageSize, string assetId, string jobOrderNo, string sortString)
        {
            var query = new AssetMaintenanceOrderQuery("a");
            var frServiceUnitQuery = new ServiceUnitQuery("b");
            var frLocationQuery = new ServiceRoomQuery("c");
            var toServiceUnitQuery = new ServiceUnitQuery("d");
            var assetQuery = new AssetQuery("e");

            query.Select(query, frServiceUnitQuery.ServiceUnitName.As("FromServiceUnitName"), frLocationQuery.RoomName.As("FromLocationName"),
                toServiceUnitQuery.ServiceUnitName.As("ToServiceUnitName"), assetQuery.AssetName.As("AssetName"));
            query.InnerJoin(frServiceUnitQuery).On(query.FromServiceUnitID == frServiceUnitQuery.ServiceUnitID);
            query.InnerJoin(frLocationQuery).On(query.FromLocationID == frLocationQuery.RoomID);
            query.InnerJoin(toServiceUnitQuery).On(query.ToServiceUnitID == toServiceUnitQuery.ServiceUnitID);
            query.InnerJoin(assetQuery).On(query.AssetID == assetQuery.AssetID);

            var prms = new List<esComparison>();
            if (!string.IsNullOrEmpty(assetId))
                prms.Add(query.AssetID.Like(assetId + "%"));
            if (!string.IsNullOrEmpty(jobOrderNo))
                prms.Add(query.JobOrderNo.Like(jobOrderNo + "%"));

            if (prms.Count > 0)
                query.Where(prms.ToArray());

            query.OrderBy(safeOrderByItems(sortString));

            query.es.PageSize = pageSize;
            query.es.PageNumber = pageNumber + 1;
            query.es.WithNoLock = true;

            var coll = new AssetMaintenanceOrderCollection();
            coll.Load(query);
            return coll;
        }

        public static AssetMaintenanceOrder Get(string jobOrderNumber)
        {
            if (string.IsNullOrEmpty(jobOrderNumber))
                return null;

            var orderQuery = new AssetMaintenanceOrderQuery("a");
            var frServiceUnitQuery = new ServiceUnitQuery("b");
            var frLocationQuery = new ServiceRoomQuery("c");
            var toServiceUnitQuery = new ServiceUnitQuery("d");
            var assetQuery = new AssetQuery("e");

            orderQuery.Select(orderQuery, frServiceUnitQuery.ServiceUnitName.As("FromServiceUnitName"), frLocationQuery.RoomName.As("FromLocationName"),
                toServiceUnitQuery.ServiceUnitName.As("ToServiceUnitName"), assetQuery.AssetName.As("AssetName"));
            orderQuery.InnerJoin(frServiceUnitQuery).On(orderQuery.FromServiceUnitID == frServiceUnitQuery.ServiceUnitID);
            orderQuery.InnerJoin(frLocationQuery).On(orderQuery.FromLocationID == frLocationQuery.RoomID);
            orderQuery.InnerJoin(toServiceUnitQuery).On(orderQuery.ToServiceUnitID == toServiceUnitQuery.ServiceUnitID);
            orderQuery.InnerJoin(assetQuery).On(orderQuery.AssetID == assetQuery.AssetID);

            orderQuery.Where(orderQuery.JobOrderNo == jobOrderNumber);
            orderQuery.es.WithNoLock = true;

            var entity = new AssetMaintenanceOrder();
            if (entity.Load(orderQuery))
                return entity;
            else
                return null;
        }

        private static esOrderByItem[] safeOrderByItems(string sortString)
        {
            var list = new List<esOrderByItem>();
            var fieldsName = sortString.ToLowerInvariant().Split(char.Parse(","));
            foreach (string field in fieldsName)
            {
                var q = new AssetMaintenanceOrderQuery();
                var tmp = field.Split(char.Parse("^"));
                bool isDesc = false;
                if (tmp.Length > 1)
                    isDesc = tmp[1].Equals("descending");

                if (tmp[0].Equals("joborderno"))
                    list.Add(isDesc ? q.JobOrderNo.Descending : q.JobOrderNo.Ascending);
                if (tmp[0].Equals("assetid"))
                    list.Add(isDesc ? q.AssetID.Descending : q.AssetID.Ascending);
            }
            return list.ToArray();
        }
	}
}
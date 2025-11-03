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

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
	public partial class AssetMovement : esAssetMovement
	{
        public string AssetName
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("AssetName") is System.DBNull ? string.Empty : (string)this.GetColumn("AssetName");
                return retVal;
            }
        }

        public string FromServiceUnitName
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("FromServiceUnitName") is System.DBNull ? string.Empty : (string)this.GetColumn("FromServiceUnitName");
                return retVal;
            }
        }
        public string FromLocationName
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("FromLocationName") is System.DBNull ? string.Empty : (string)this.GetColumn("FromLocationName");
                return retVal;
            }
        }
        public string ToServiceUnitName
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("ToServiceUnitName") is System.DBNull ? string.Empty : (string)this.GetColumn("ToServiceUnitName");
                return retVal;
            }
        }
        public string ToLocationName
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("ToLocationName") is System.DBNull ? string.Empty : (string)this.GetColumn("ToLocationName");
                return retVal;
            }
        }

        protected override List<esPropertyDescriptor> GetLocalBindingProperties()
        {
            var items = new List<esPropertyDescriptor>
                            {
                                new esPropertyDescriptor(this, "AssetName", typeof (string)),
                                new esPropertyDescriptor(this, "FromServiceUnitName", typeof (string)),
                                new esPropertyDescriptor(this, "FromLocationName", typeof (string)),
                                new esPropertyDescriptor(this, "ToServiceUnitName", typeof (string)),
                                new esPropertyDescriptor(this, "ToLocationName", typeof (string))
                            };

            return items;
        }

        public static AssetMovementCollection GetAllWithPaging(int pageNumber, int pageSize, string assetId, string assetName, string assetMovementNo, string sortString)
        {
            var query = new AssetMovementQuery("mv");
            var asset = new AssetQuery("a");
            var frServiceUnitQuery = new ServiceUnitQuery("frs");
            var toServiceUnitQuery = new ServiceUnitQuery("tos");
            var frLocationQuery = new ServiceRoomQuery("frloc");
            var toLocationQuery = new ServiceRoomQuery("toloc");

            query.Select(query, asset.AssetName, frServiceUnitQuery.ServiceUnitName.As("FromServiceUnitName"), 
                frLocationQuery.RoomName.As("FromLocationName"),
                toServiceUnitQuery.ServiceUnitName.As("ToServiceUnitName"), 
                toLocationQuery.RoomName.As("ToLocationName")
                );

            query.InnerJoin(asset).On(query.AssetID == asset.AssetID);
            query.InnerJoin(frServiceUnitQuery).On(query.FromServiceUnitID == frServiceUnitQuery.ServiceUnitID);
            query.InnerJoin(toServiceUnitQuery).On(query.ToServiceUnitID == toServiceUnitQuery.ServiceUnitID);
            query.LeftJoin(frLocationQuery).On(query.FromAssetLocationID == frLocationQuery.RoomID);
            query.LeftJoin(toLocationQuery).On(query.ToAssetLocationID == toLocationQuery.RoomID);

            var prms = new List<esComparison>();
            prms.Add(query.IsDeleted == false);

            if (!string.IsNullOrEmpty(assetId))
            {
                string searchTextContain = string.Format("%{0}%", assetId);
                prms.Add(query.AssetID.Like(searchTextContain));
                //prms.Add(query.AssetID.Like("%" + assetId + "%"));
            }
            if (!string.IsNullOrEmpty(assetId))
            {
                string searchTextContain = string.Format("%{0}%", assetName);
                prms.Add(asset.AssetName.Like(searchTextContain));
                //prms.Add(asset.AssetName.Like("%" + assetName + "%"));
            }
            if (!string.IsNullOrEmpty(assetMovementNo))
            {
                string searchTextContain = string.Format("%{0}%", assetMovementNo);
                prms.Add(query.AssetMovementNo.Like(searchTextContain));
                //prms.Add(query.AssetMovementNo.Like("%" + assetMovementNo + "%"));
            }

            if (prms.Count > 0)
                query.Where(prms.ToArray());
            query.OrderBy(safeOrderByItems(sortString));

            query.es.PageSize = pageSize;
            query.es.PageNumber = pageNumber + 1;
            query.es.WithNoLock = true;

            var coll = new AssetMovementCollection();
            coll.Load(query);
            return coll;
        }

        private static esOrderByItem[] safeOrderByItems(string sortString)
        {
            var list = new List<esOrderByItem>();
            var fieldsName = sortString.ToLowerInvariant().Split(char.Parse(","));
            foreach (string field in fieldsName)
            {
                var q = new AssetMovementQuery();
                var tmp = field.Split(char.Parse("^"));
                bool isDesc = false;
                if (tmp.Length > 1)
                    isDesc = tmp[1].Equals("descending");

                if (tmp[0].Equals("assetmovementno"))
                    list.Add(isDesc ? q.AssetMovementNo.Descending : q.AssetMovementNo.Ascending);
                if (tmp[0].Equals("assetid"))
                    list.Add(isDesc ? q.AssetID.Descending : q.AssetID.Ascending);
            }
            return list.ToArray();
        }

        public static int TotalCount(string assetId, string assetName, string assetMovementNo)
        {
            int retVal = 0;
            
            var m = new AssetMovementQuery("m");
            var a = new AssetQuery("a");
            
            m.InnerJoin(a).On(m.AssetID == a.AssetID);
            var prms = new List<esComparison>();

            prms.Add(m.IsDeleted == false);
            if (!string.IsNullOrEmpty(assetId))
            {
                string searchTextContain = string.Format("%{0}%", assetId);
                prms.Add(m.AssetID.Like(searchTextContain));
                //prms.Add(m.AssetID.Like("%" + assetId + "%"));
            }
            if (!string.IsNullOrEmpty(assetName))
            {
                string searchTextContain = string.Format("%{0}%", assetName);
                prms.Add(a.AssetName.Like(searchTextContain));
                //prms.Add(a.AssetName.Like("%" + assetName + "%"));
            }
            if (!string.IsNullOrEmpty(assetMovementNo))
            {
                string searchTextContain = string.Format("%{0}%", assetMovementNo);
                prms.Add(m.AssetMovementNo.Like(searchTextContain));
                //prms.Add(m.AssetMovementNo.Like(assetMovementNo + "%"));
            }

            if (prms.Count > 0)
                m.Where(prms.ToArray());

            m.es.WithNoLock = true;

            DataTable dtb = m.LoadDataTable();
            if (dtb.Rows.Count > 0)
                retVal = dtb.Rows.Count;

            return retVal;
        }

        public static int TotalCountByAssetId(string assetId)
        {
            if (string.IsNullOrEmpty(assetId))
                return 0;

            int retVal = 0;
            var entity = new AssetMovement();

            entity.Query.es.CountAll = true;
            entity.Query.es.CountAllAlias = "Count";
            entity.Query.es.WithNoLock = true;

            entity.Query.Where(entity.Query.AssetID == assetId && entity.Query.IsDeleted == false && entity.Query.IsPosted == true);
            if (entity.Query.Load())
                retVal = (int)entity.GetColumn("Count");

            return retVal;
        }

        public static AssetMovementCollection GetAllWithPagingByAssetId(string assetId, int pageNumber, int pageSize)
        {
            if (string.IsNullOrEmpty(assetId))
                return new AssetMovementCollection();

            var query = new AssetMovementQuery("mv");
            var asset = new AssetQuery("a");
            var frServiceUnitQuery = new ServiceUnitQuery("frs");
            var toServiceUnitQuery = new ServiceUnitQuery("tos");
            var frLocationQuery = new ServiceRoomQuery("frloc");
            var toLocationQuery = new ServiceRoomQuery("toloc");

            query.Select(query, asset.AssetName, frServiceUnitQuery.ServiceUnitName.As("FromServiceUnitName"), frLocationQuery.RoomName.As("FromLocationName"),
                toServiceUnitQuery.ServiceUnitName.As("ToServiceUnitName"), toLocationQuery.RoomName.As("ToLocationName"));

            query.InnerJoin(asset).On(query.AssetID == asset.AssetID);
            query.InnerJoin(frServiceUnitQuery).On(query.FromServiceUnitID == frServiceUnitQuery.ServiceUnitID);
            query.InnerJoin(toServiceUnitQuery).On(query.ToServiceUnitID == toServiceUnitQuery.ServiceUnitID);
            query.LeftJoin(frLocationQuery).On(query.FromAssetLocationID == frLocationQuery.RoomID);
            query.LeftJoin(toLocationQuery).On(query.ToAssetLocationID == toLocationQuery.RoomID);
            query.Where(query.AssetID == assetId && query.IsDeleted == false && query.IsPosted == true);
            query.OrderBy(query.AssetMovementNo.Descending);

            query.es.PageSize = pageSize;
            query.es.PageNumber = pageNumber + 1;
            query.es.WithNoLock = true;

            var coll = new AssetMovementCollection();
            coll.Load(query);
            return coll;
        }

        public static AssetMovement Get(string assetMovementNo)
        {
            if (string.IsNullOrEmpty(assetMovementNo))
                return null;

            var entity = new AssetMovement();
            entity.Query.Where(entity.Query.AssetMovementNo == assetMovementNo && entity.Query.IsDeleted == false);
            if (entity.Query.Load())
                return entity;
            else
                return null;
        }
	}
}

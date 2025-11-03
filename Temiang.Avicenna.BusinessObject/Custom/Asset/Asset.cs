/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 10/24/2011 11:56:41 PM
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
	public partial class Asset : esAsset
	{
        public string GroupName
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.GetColumn("GroupName") is System.DBNull ? string.Empty : (string)this.GetColumn("GroupName");
                return retVal;
            }
        }
        protected override List<esPropertyDescriptor> GetLocalBindingProperties()
        {
            List<esPropertyDescriptor> items = new List<esPropertyDescriptor>();

            items.Add(new esPropertyDescriptor(this, "GroupName", typeof(string)));

            return items;
        }

        public static Asset Get(string assetId)
        {
            if (string.IsNullOrEmpty(assetId))
                return null;

            var entity = new Asset();
            entity.Query.Where(entity.Query.AssetID == assetId);
            if (entity.Query.Load())
                return entity;
            else
                return null;
        }

        public static int TotalCount(string assetId, string assetName, string assetGroupId, decimal usageTimeEstimation, string assetServiceUnitId, string assetMaintenanceUnitId)
        {
            int retVal = 0;
            //var entity = new Asset();
            var a = new AssetQuery("a");
            var g = new AssetGroupQuery("g");
            var u = new ServiceUnitQuery("u");
            var l = new ServiceRoomQuery("l");
            var u2 = new ServiceUnitQuery("u2");

            a.Select(a, g.GroupName);
            a.InnerJoin(g).On(a.AssetGroupID == g.AssetGroupId);
            a.LeftJoin(u).On(a.ServiceUnitID == u.ServiceUnitID);
            a.LeftJoin(l).On(a.AssetLocationID == l.RoomID);
            a.LeftJoin(u2).On(a.MaintenanceServiceUnitID == u2.ServiceUnitID);

            var prms = new List<esComparison>();

            if (!string.IsNullOrEmpty(assetId))
            {
                string searchTextContain = string.Format("{0}%", assetId);
                prms.Add(a.AssetID.Like(searchTextContain));
                //prms.Add(a.AssetID.Like(assetId + "%"));
            }
            if (!string.IsNullOrEmpty(assetName))
            {
                string searchTextContain = string.Format("%{0}%", assetName);
                prms.Add(a.AssetName.Like(searchTextContain));
                //prms.Add(a.AssetName.Like("%" + assetName + "%"));
            }
            if (!string.IsNullOrEmpty(assetGroupId))
                prms.Add(a.AssetGroupID == assetGroupId);
            if (usageTimeEstimation == 1)
                prms.Add(a.UsageTimeEstimation > 0);
            if (!string.IsNullOrEmpty(assetServiceUnitId))
                prms.Add(a.ServiceUnitID == assetServiceUnitId);
            if (!string.IsNullOrEmpty(assetMaintenanceUnitId))
                prms.Add(a.MaintenanceServiceUnitID == assetMaintenanceUnitId);
            
            //a.es.CountAll = true;
            //a.es.CountAllAlias = "Count";
            a.es.WithNoLock = true;
            if (prms.Count > 0)
                a.Where(prms.ToArray());

            DataTable dtb = a.LoadDataTable();
            if (dtb.Rows.Count > 0)
                retVal = dtb.Rows.Count;

            //if (entity.Query.Load())
            //    retVal = (int)entity.GetColumn("Count");

            return retVal;
        }

        public static AssetCollection GetAllWithPaging(int pageNumber, int pageSize, string assetId, string assetName, string assetGroupId, decimal usageTimeEstimation, string assetServiceUnitId, string assetMaintenanceUnitId, string sortString)
        {
            var a = new AssetQuery("a");
            var g = new AssetGroupQuery("g");
            var u = new ServiceUnitQuery("u");
            var l = new ServiceRoomQuery("l");
            var u2 = new ServiceUnitQuery("u2");

            a.Select(a, g.GroupName);
            a.InnerJoin(g).On(a.AssetGroupID == g.AssetGroupId);
            a.LeftJoin(u).On(a.ServiceUnitID == u.ServiceUnitID);
            a.LeftJoin(l).On(a.AssetLocationID == l.RoomID);
            a.LeftJoin(u2).On(a.MaintenanceServiceUnitID == u2.ServiceUnitID);

            var prms = new List<esComparison>();

            if (!string.IsNullOrEmpty(assetId))
            {
                string searchTextContain = string.Format("{0}%", assetId);
                prms.Add(a.AssetID.Like(searchTextContain));
                //prms.Add(a.AssetID.Like(assetId + "%"));
            }
            if (!string.IsNullOrEmpty(assetName))
            {
                string searchTextContain = string.Format("%{0}%", assetName);
                prms.Add(a.AssetName.Like(searchTextContain));
                //prms.Add(a.AssetName.Like("%" + assetName + "%"));
            }
            if (!string.IsNullOrEmpty(assetGroupId))
                prms.Add(a.AssetGroupID == assetGroupId);
            if (usageTimeEstimation == 1)
                prms.Add(a.UsageTimeEstimation > 0);
            if (!string.IsNullOrEmpty(assetServiceUnitId))
                prms.Add(a.ServiceUnitID == assetServiceUnitId);
            if (!string.IsNullOrEmpty(assetMaintenanceUnitId))
                prms.Add(a.MaintenanceServiceUnitID == assetMaintenanceUnitId);

            a.OrderBy(safeOrderByItems(sortString));
            if (prms.Count > 0)
                a.Where(prms.ToArray());

            a.es.PageSize = pageSize;
            a.es.PageNumber = pageNumber + 1;
            a.es.WithNoLock = true;

            var coll = new AssetCollection();
            coll.Load(a);
            return coll;
        }

        private static esOrderByItem[] safeOrderByItems(string sortString)
        {
            var list = new List<esOrderByItem>();
            var fieldsName = sortString.ToLowerInvariant().Split(char.Parse(","));
            foreach (string field in fieldsName)
            {
                var q = new AssetQuery();
                var tmp = field.Split(char.Parse("^"));
                bool isDesc = false;
                if (tmp.Length > 1)
                    isDesc = tmp[1].Equals("descending");

                if (tmp[0].Equals("assetid"))
                    list.Add(isDesc ? q.AssetID.Descending : q.AssetID.Ascending);
                if (tmp[0].Equals("assetname"))
                    list.Add(isDesc ? q.AssetName.Descending : q.AssetName.Ascending);
                if (tmp[0].Equals("assetgroupid"))
                    list.Add(isDesc ? q.AssetGroupID.Descending : q.AssetGroupID.Ascending);

            }
            return list.ToArray();
        }
	}
}

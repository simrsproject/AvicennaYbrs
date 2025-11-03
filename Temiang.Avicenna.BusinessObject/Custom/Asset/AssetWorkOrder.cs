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
    public partial class AssetWorkOrder : esAssetWorkOrder
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

        public static int TotalCount(string assetId, string orderNo)
        {
            int retVal = 0;
            var entity = new AssetWorkOrder();

            var prms = new List<esComparison>();

            if (!string.IsNullOrEmpty(assetId))
                prms.Add(entity.Query.AssetID.Like(assetId + "%"));
            if (!string.IsNullOrEmpty(orderNo))
                prms.Add(entity.Query.OrderNo.Like(orderNo + "%"));

            if (prms.Count > 0)
                entity.Query.Where(prms.ToArray());

            entity.Query.es.CountAll = true;
            entity.Query.es.CountAllAlias = "Count";
            entity.Query.es.WithNoLock = true;

            if (entity.Query.Load())
                retVal = (int)entity.GetColumn("Count");

            return retVal;
        }

        public static AssetWorkOrderCollection GetAllWithPaging(int pageNumber, int pageSize, string assetId, string orderNo, string sortString)
        {
            var q = new AssetWorkOrderQuery("h");
            var assetQuery = new AssetQuery("e");

            q.Select(q, assetQuery.AssetName.As("AssetName"));
            q.InnerJoin(assetQuery).On(q.AssetID == assetQuery.AssetID);

            var prms = new List<esComparison>();
            if (!string.IsNullOrEmpty(assetId))
                prms.Add(q.AssetID.Like(assetId + "%"));
            if (!string.IsNullOrEmpty(orderNo))
                prms.Add(q.OrderNo.Like(orderNo + "%"));

            if (prms.Count > 0)
                q.Where(prms.ToArray());

            q.OrderBy(safeOrderByItems(sortString));

            q.es.PageSize = pageSize;
            q.es.PageNumber = pageNumber + 1;
            q.es.WithNoLock = true;

            var coll = new AssetWorkOrderCollection();
            coll.Load(q);
            return coll;
        }

        private static esOrderByItem[] safeOrderByItems(string sortString)
        {
            var list = new List<esOrderByItem>();
            var fieldsName = sortString.ToLowerInvariant().Split(char.Parse(","));
            foreach (string field in fieldsName)
            {
                var q = new AssetWorkOrderQuery();
                var tmp = field.Split(char.Parse("^"));
                bool isDesc = false;
                if (tmp.Length > 1)
                    isDesc = tmp[1].Equals("descending");

                if (tmp[0].Equals("orderno"))
                    list.Add(isDesc ? q.OrderNo.Descending : q.OrderNo.Ascending);
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
            var entity = new AssetWorkOrder();

            entity.Query.es.CountAll = true;
            entity.Query.es.CountAllAlias = "Count";
            entity.Query.es.WithNoLock = true;

            entity.Query.Where(entity.Query.AssetID == assetId && entity.Query.IsProceed == true);
            if (entity.Query.Load())
                retVal = (int)entity.GetColumn("Count");

            return retVal;
        }

        public static AssetWorkOrderCollection GetAllWithPagingByAssetId(string assetId, int pageNumber, int pageSize)
        {
            if (string.IsNullOrEmpty(assetId))
                return new AssetWorkOrderCollection();

            var q = new AssetWorkOrderQuery("h");

            q.Select(q);
            q.Where(q.AssetID == assetId && q.IsProceed == true);
            q.OrderBy(q.OrderNo.Descending);

            q.es.PageSize = pageSize;
            q.es.PageNumber = pageNumber + 1;
            q.es.WithNoLock = true;

            var coll = new AssetWorkOrderCollection();
            coll.Load(q);
            return coll;
        }

        public static AssetWorkOrder Get(string orderNo)
        {
            var entity = new AssetWorkOrder();
            entity.Query.Where(entity.Query.OrderNo == orderNo);
            return entity.Query.Load() ? entity : null;
        }
    }
}

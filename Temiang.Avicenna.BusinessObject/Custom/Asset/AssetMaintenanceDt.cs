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

using Temiang.Dal.Interfaces;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.BusinessObject
{
	public partial class AssetMaintenanceDt : esAssetMaintenanceDt
	{
        //public string ItemID
        //{
        //    get { return this.GetColumn("ItemID") is DBNull ? string.Empty : (string) this.GetColumn("ItemID"); }
        //}

        public string ItemName
        {
            get { return this.GetColumn("ItemName") is DBNull ? string.Empty : (string)this.GetColumn("ItemName"); }
        }

        public static AssetMaintenanceDtCollection GetAllForTransactionWithPaging(string transactionNo, int pageNumber, int pageSize)
        {
            var d = new AssetMaintenanceDtQuery("j");
            var i = new ItemQuery("i");
            var n = new ItemProductNonMedicQuery("n");

            d.Select(d, i.ItemName);
            d.InnerJoin(i).On(d.ItemID == i.ItemID);
            d.InnerJoin(n).On(i.ItemID == n.ItemID);
            d.Where(d.TransactionNo == transactionNo);
            d.OrderBy(d.MaintenanceItemId.Descending);

            d.es.PageSize = pageSize;
            d.es.PageNumber = pageNumber + 1;
            d.es.WithNoLock = true;

            var coll = new AssetMaintenanceDtCollection();
            coll.Load(d);
            return coll;
        }

        public static int TotalCount(string transactionNo)
        {
            int retVal = 0;
            var entity = new AssetMaintenanceDt();

            entity.Query.es.CountAll = true;
            entity.Query.es.CountAllAlias = "Count";
            entity.Query.es.WithNoLock = true;
            entity.Query.Where(entity.Query.TransactionNo == transactionNo);

            if (entity.Query.Load())
                retVal = (int)entity.GetColumn("Count");

            return retVal;
        }

        public static AssetMaintenanceDt Get(string transactionNo, int maintenanceItemId)
        {
            var h = new AssetMaintenanceHdQuery("h");
            var d = new AssetMaintenanceDtQuery("j");
            var i = new ItemQuery("c");
            var n = new ItemProductNonMedicQuery("s");

            d.Select(d, i.ItemName);
            d.InnerJoin(h).On(d.TransactionNo == h.TransactionNo);
            d.InnerJoin(n).On(d.ItemID == n.ItemID);
            d.InnerJoin(i).On(d.ItemID == i.ItemID);
            d.Where(h.TransactionNo == transactionNo, d.MaintenanceItemId == maintenanceItemId);

            var e = new AssetMaintenanceDt();
            e.Query.es.WithNoLock = true;
            if (e.Load(d))
                return e;

            return null;
        }

	}
}

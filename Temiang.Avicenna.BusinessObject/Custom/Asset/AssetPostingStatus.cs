/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 1/21/2012 7:36:20 PM
===============================================================================
*/

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Temiang.Dal.Interfaces;
using Temiang.Dal.Core;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
	public partial class AssetPostingStatus : esAssetPostingStatus
	{
        public static AssetPostingStatusCollection Get()
        {
            AssetPostingStatusCollection coll = new AssetPostingStatusCollection();
            coll.Query.OrderBy(coll.Query.Year.Descending, coll.Query.Month.Descending);
            coll.Query.es.WithNoLock = true;
            coll.Query.Load();
            return coll;
        }

        public static AssetPostingStatus Get(int postingId)
        {
            AssetPostingStatus entity = new AssetPostingStatus();
            entity.Query.Where(entity.Query.PostingId == postingId);
            if (entity.Query.Load())
                return entity;
            else
                return null;
        }

        public static int CancelClosing(int accountId, int branchId, int postingId)
        {
            esParameters prms = new esParameters();

            prms.Add("PostingId", postingId, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            AssetPostingStatus entity = new AssetPostingStatus();
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_AssetPostingStatusCancelClosing", prms);
            return (int)prms["Return_Value"].Value;
        }

        public static bool IsPeriodeClosed(DateTime transactionDate)
        {
            string month = transactionDate.ToString("MM");
            string year = transactionDate.ToString("yyyy");

            AssetPostingStatus entity = new AssetPostingStatus();
            entity.Query.Where(entity.Query.Month == month, entity.Query.Year == year, entity.Query.IsEnabled == true);
            if (entity.Query.Load())
                return true;
            else
                return false;
        }
	}
}

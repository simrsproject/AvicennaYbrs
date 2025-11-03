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
    public partial class PostingStatus : esPostingStatus
    {
        public static PostingStatusCollection Get()
        {
            PostingStatusCollection coll = new PostingStatusCollection();

            var query = new PostingStatusQuery("a");
            var group = new JournalGroupQuery("b");

            query.es.WithNoLock = true;
            query.Select(query, group.JournalGroupName);
            query.LeftJoin(group).On(query.JournalGroupID == group.JournalGroupID);
            query.OrderBy(query.Year.Descending, query.Month.Descending);

            coll.Load(query);

            //coll.Query.OrderBy(coll.Query.Year.Descending, coll.Query.Month.Descending);
            //coll.Query.es.WithNoLock = true;
            //coll.Query.Load();
            return coll;
        }

        public static PostingStatus Get(int postingId)
        {
            PostingStatus entity = new PostingStatus();
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

            PostingStatus entity = new PostingStatus();
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_PostingStatusCancelClosing", prms);
            return (int)prms["Return_Value"].Value;
        }

        public static bool IsPeriodeClosed(DateTime transactionDate)
        {
            string month = transactionDate.ToString("MM");
            string year = transactionDate.ToString("yyyy");

            var entity = new PostingStatus();
            entity.Query.Where(entity.Query.Month == month, entity.Query.Year == year, entity.Query.IsEnabled == true);
            if (entity.Query.Load())
                return true;
            return false;
        }
        public static bool IsPeriodeClosed(DateTime transactionDateStart, DateTime transactionDateEnd)
        {
            //di-remark krn nge-looping gak selesai2
            //for (DateTime dt = transactionDateStart.AddDays(1-transactionDateStart.Day); dt <= transactionDateEnd; dt.AddMonths(1)) {
            //    var entity = new PostingStatus();
            //    entity.Query.Where(entity.Query.Month == dt.Month, entity.Query.Year == dt.Year, entity.Query.IsEnabled == true);
            //    if (entity.Query.Load())
            //        return true;
            //}

            DataTable dtDate = new DataTable();
            dtDate.Columns.Add("Month", typeof(string));
            dtDate.Columns.Add("Year", typeof(string));

            string month = "";
            string year = "";

            for (DateTime lDate = transactionDateStart; lDate <= transactionDateEnd; lDate = lDate.AddDays(1))
            {
                var sMonth = lDate.ToString("MM");
                var sYear = lDate.ToString("yyyy");
                if (!(month == sMonth && year == sYear))
                {
                    var row = dtDate.NewRow();
                    row["Month"] = sMonth;
                    row["Year"] = sYear;

                    month = sMonth;
                    year = sYear;

                    dtDate.Rows.Add(row);
                }
            }

            foreach (DataRow row in dtDate.Rows)
            {
                var entity = new PostingStatus();
                entity.Query.Where(entity.Query.Month == row["Month"], entity.Query.Year == row["Year"], entity.Query.IsEnabled == true);
                if (entity.Query.Load())
                    return true;
            }

            return false;
        }

        public static bool IsUnApproveDisabledIfPerClosed(DateTime transactionDate)
        {
            return false;
            //var parValue = AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUnApproveDisabledIfPerClosed);
            //if (parValue.ToLower().Equals("no")) return false;

            //return IsPeriodeClosed(transactionDate);
        }
    }
}

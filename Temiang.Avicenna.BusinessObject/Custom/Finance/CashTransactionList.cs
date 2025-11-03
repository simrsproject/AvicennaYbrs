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
    public partial class CashTransactionList : esCashTransactionList
    {
        public static CashTransactionList Get(string listId)
        {
            CashTransactionList entity = new CashTransactionList();
            return !entity.LoadByPrimaryKey(listId) ? null : entity;
        }

        public static CashTransactionListCollection GetLike(string description, bool? isFixed)
        {
            CashTransactionListCollection coll = new CashTransactionListCollection();
            coll.Query.Where(
                coll.Query.Or(
                    coll.Query.ListId.Like(string.Format("{0}%", description)),
                    coll.Query.Description.Like(string.Format("{0}%", description))
                )
            );
            if (isFixed != null) coll.Query.Where(coll.Query.CashType == ((isFixed ?? false) ? "01" : "02"));

            coll.Query.OrderBy(coll.Query.ListId.Ascending);
            coll.Query.es.WithNoLock = true;
            coll.Query.Load();
            return coll;
        }

        public static CashTransactionListCollection GetAllWithPaging(int pageNumber, int pageSize, string sortString)
        {
            CashTransactionListCollection coll = new CashTransactionListCollection();
            List<esComparison> prms = new List<esComparison>();

            coll.Query.OrderBy(coll.Query.ListId.Ascending);
            if (prms.Count > 0)
                coll.Query.Where(prms.ToArray());

            coll.Query.es.PageSize = pageSize;
            coll.Query.es.PageNumber = pageNumber + 1;
            coll.Query.es.WithNoLock = true;
            coll.Query.Load();
            return coll;
        }


    }
}

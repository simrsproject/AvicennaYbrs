using System;
using System.Collections.Generic;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class BankInquiry
    {

    }

    public partial class BankInquiryCollection
    {
        private static esOrderByItem[] safeOrderByItems(string sortString, BankInquiryQuery q)
        {
            List<esOrderByItem> list = new List<esOrderByItem>();
            string[] fieldsName = sortString.ToLowerInvariant().Split(char.Parse(","));
            foreach (string field in fieldsName)
            {
                string[] tmp = field.Split(char.Parse("^"));
                bool isDesc = false;
                if (tmp.Length > 1)
                    isDesc = tmp[1].Equals("descending");

                if (tmp[0].Equals("BankID"))
                    list.Add(isDesc ? q.BankID.Descending : q.BankID.Ascending);
                if (tmp[0].Equals("Debit"))
                    list.Add(isDesc ? q.Debit.Descending : q.Debit.Ascending);
                if (tmp[0].Equals("Credit"))
                    list.Add(isDesc ? q.Credit.Descending : q.Credit.Ascending);
                if (tmp[0].Equals("CreatedDateTime"))
                    list.Add(isDesc ? q.CreatedDateTime.Descending : q.CreatedDateTime.Ascending);
            }
            if (list.Count == 0) list.Add(q.InquiryID.Ascending);
            return list.ToArray();
        }

        public bool LoadByPaging(string BankID, int pageNumber, int pageSize, string sortString) {
            List<esComparison> prms = new List<esComparison>();
            var q = this.Query;

            prms.Add(q.BankID.Equal(BankID));

            if (prms.Count > 0)
                q.Where(prms.ToArray());

            q.OrderBy(safeOrderByItems(sortString, q));

            q.es.WithNoLock = true;
            q.es.PageSize = pageSize;
            q.es.PageNumber = pageNumber + 1;
            q.es2.Connection.CommandTimeout = 600;

            return this.LoadAll();
        }
        public int GetTotalCount(string BankID)
        {
            BankInquiry entity = new BankInquiry();
            List<esComparison> prms = new List<esComparison>();
            var q = entity.Query;

            prms.Add(q.BankID.Equal(BankID));

            if (prms.Count > 0)
                q.Where(prms.ToArray());

            q.es.CountAll = true;
            q.es.CountAllAlias = "Count";
            q.es2.Connection.CommandTimeout = 600;
            q.es.WithNoLock = true;
            if (prms.Count > 0)
                entity.Query.Where(prms.ToArray());

            if (entity.Query.Load())
                return (int)entity.GetColumn("Count");
            else
                return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class BankInquiryDetail
    {
        public bool LoadByBankIdByDate_Last(string BankID, DateTime dDate) {
            var bid = new BankInquiryDetailQuery("bid");
            var bi = new BankInquiryQuery("bi");

            bid.InnerJoin(bi).On(bid.InquiryID == bi.InquiryID)
                .Where(bi.BankID == BankID, bid.TransactionDateTime < dDate.Date.AddDays(1))
                .OrderBy(bid.TransactionDateTime.Descending, bid.TransactionID.Descending);
            bid.es.Top = 1;
            return Load(bid);
        }

        public bool LoadByRelatedTransactionNo(string RelatedTransNo) {
            if (string.IsNullOrEmpty(RelatedTransNo)) return false;

            var q = this.Query;
            q.Where(q.RelatedTransactionNo == RelatedTransNo);
            q.es.Top = 1;
            return q.Load();
        }

        public decimal GetBalanceBankIdByDate(string BankID, DateTime dDate) {
            if (!LoadByBankIdByDate_Last(BankID, dDate)) return 0;
            return Balance ?? 0;
        }
    }

    public partial class BankInquiryDetailCollection
    {
        public bool HasReconciledOrRelated {
            get {
                return (ReconciledCount > 0) || (RelatedCount > 0);
            }
        }
        public int ReconciledCount {
            get {
                return this.Where(a => a.ReconcileID.HasValue).Count();
            }
        }
        public int RelatedCount
        {
            get
            {
                return this.Where(a => !string.IsNullOrEmpty(a.RelatedTransactionNo)).Count();
            }
        }
        public bool LoadByInquiryID(int InquiryID)
        {
            List<esComparison> prms = new List<esComparison>();
            var q = this.Query;

            prms.Add(q.InquiryID.Equal(InquiryID));

            if (prms.Count > 0)
                q.Where(prms.ToArray());

            q.es.WithNoLock = true;
            q.es2.Connection.CommandTimeout = 600;

            return this.LoadAll();
        }

        private static esOrderByItem[] safeOrderByItems(string sortString, BankInquiryDetailQuery q)
        {
            List<esOrderByItem> list = new List<esOrderByItem>();
            string[] fieldsName = sortString.ToLowerInvariant().Split(char.Parse(","));
            foreach (string field in fieldsName)
            {
                string[] tmp = field.Split(char.Parse("^"));
                bool isDesc = false;
                if (tmp.Length > 1)
                    isDesc = tmp[1].Equals("descending");

                if (tmp[0].Equals("TransactionDateTime"))
                    list.Add(isDesc ? q.TransactionDateTime.Descending : q.TransactionDateTime.Ascending);
                if (tmp[0].Equals("Description"))
                    list.Add(isDesc ? q.Description.Descending : q.Description.Ascending);
                if (tmp[0].Equals("ReferenceNo"))
                    list.Add(isDesc ? q.ReferenceNo.Descending : q.ReferenceNo.Ascending);
                if (tmp[0].Equals("Debit"))
                    list.Add(isDesc ? q.Debit.Descending : q.Debit.Ascending);
                if (tmp[0].Equals("Credit"))
                    list.Add(isDesc ? q.Credit.Descending : q.Credit.Ascending);
                if(tmp[0].Equals("Balance"))
                    list.Add(isDesc ? q.Balance.Descending : q.Balance.Ascending);
            }
            if (list.Count == 0) list.Add(q.TransactionID.Ascending);
            return list.ToArray();
        }

        public bool LoadByInquiryByPaging(int InquiryID, int pageNumber, int pageSize, string sortString) {
            List<esComparison> prms = new List<esComparison>();
            var q = this.Query;

            prms.Add(q.InquiryID.Equal(InquiryID));

            if (prms.Count > 0)
                q.Where(prms.ToArray());

            q.OrderBy(safeOrderByItems(sortString, q));

            q.es.WithNoLock = true;
            q.es.PageSize = pageSize;
            q.es.PageNumber = pageNumber + 1;
            q.es2.Connection.CommandTimeout = 600;

            return this.LoadAll();
        }

        public int GetCountByInquiryByPaging(int InquiryID)
        {
            BankInquiryDetail entity = new BankInquiryDetail();
            List<esComparison> prms = new List<esComparison>();
            var q = entity.Query;

            prms.Add(q.InquiryID.Equal(InquiryID));

            if (prms.Count > 0)
                q.Where(prms.ToArray());

            q.es.CountAll = true;
            q.es.CountAllAlias = "Count";
            q.es2.Connection.CommandTimeout = 600;
            q.es.WithNoLock = true;

            if (entity.Query.Load())
                return (int)entity.GetColumn("Count");
            else
                return 0;
        }

        public bool LoadByBankIdByPaging(string bankId, string description, DateTime date, bool UnreconciledOnly, int pageNumber, int pageSize, string sortString)
        {
            List<esComparison> prms = new List<esComparison>();
            var bid = new BankInquiryDetailQuery("bid");
            var bi = new BankInquiryQuery("bi");

            bid.InnerJoin(bi).On(bid.InquiryID == bi.InquiryID)
                .Select(bid);

            prms.Add(bi.BankID.Equal(bankId));
            prms.Add(bid.TransactionDateTime.LessThan(date.Date.AddDays(1)));

            if (string.IsNullOrEmpty(description) == false)
            {
                bid.Where(string.Format("<REPLACE(REPLACE(REPLACE(bid.Description, CHAR(13), ''), CHAR(10), ''), ' ', '') LIKE '%' + REPLACE('{0}', ' ', '')+'%'>",description,bid.es.JoinAlias));
            }

            if (UnreconciledOnly)
                prms.Add(bid.ReconcileID.IsNull());

            if (prms.Count > 0)
                bid.Where(prms.ToArray());           

            bid.OrderBy(safeOrderByItems(sortString, bid));

            bid.es.WithNoLock = true;
            bid.es.PageSize = pageSize;
            bid.es.PageNumber = pageNumber + 1;
            bid.es2.Connection.CommandTimeout = 600;

            return this.Load(bid);
        }

        public int GetCountByBankIdByPaging(string bankId, string description, DateTime date, bool UnreconciledOnly)
        {
            BankInquiryDetail entity = new BankInquiryDetail();
            List<esComparison> prms = new List<esComparison>();
            var bid = new BankInquiryDetailQuery("bid");
            var bi = new BankInquiryQuery("bi");

            bid.InnerJoin(bi).On(bid.InquiryID == bi.InquiryID);

            prms.Add(bi.BankID.Equal(bankId));
            prms.Add(bid.TransactionDateTime.LessThan(date.Date.AddDays(1)));

            if (string.IsNullOrEmpty(description) == false)
            {
                bid.Where(string.Format("<REPLACE(REPLACE(REPLACE(bid.Description, CHAR(13), ''), CHAR(10), ''), ' ', '') LIKE '%' + REPLACE('{0}', ' ', '')+'%'>", description, bid.es.JoinAlias));
            }

            if (UnreconciledOnly)
                prms.Add(bid.ReconcileID.IsNull());

            if (prms.Count > 0)
                bid.Where(prms.ToArray());
            //bid.Where(bi.BankID.Equal(bankId));

            //if (string.IsNullOrEmpty(description) == false)
            //{
            //    bid.Where(bid.Description == description);
            //}

            bid.es.CountAll = true;
            bid.es.CountAllAlias = "Count";
            bid.es2.Connection.CommandTimeout = 600;
            bid.es.WithNoLock = true;

            if (entity.Load(bid))
                return (int)entity.GetColumn("Count");
            else
                return 0;
        }

        public bool LoadByCashCodeByUnlinkedByUnreconciledByPaging(string SRCashTransactionCode, int pageNumber, int pageSize, string sortString)
        {
            List<esComparison> prms = new List<esComparison>();
            var q = this.Query;

            prms.Add(q.SRCashTransactionCode.Equal(SRCashTransactionCode));
            prms.Add(q.ReconcileID.IsNull());
            prms.Add(q.RelatedTransactionNo.Equal(string.Empty));

            if (prms.Count > 0)
                q.Where(prms.ToArray());

            q.OrderBy(safeOrderByItems(sortString, q));

            q.es.WithNoLock = true;
            q.es.PageSize = pageSize;
            q.es.PageNumber = pageNumber + 1;
            q.es2.Connection.CommandTimeout = 600;

            return q.Load();
        }

        public int GetCountByCashCodeByBankIdByPaging(string SRCashTransactionCode)
        {
            BankInquiryDetail entity = new BankInquiryDetail();
            List<esComparison> prms = new List<esComparison>();
            var q = entity.Query;

            prms.Add(q.SRCashTransactionCode.Equal(SRCashTransactionCode));
            prms.Add(q.ReconcileID.IsNull());
            prms.Add(q.RelatedTransactionNo.Equal(string.Empty));

            if (prms.Count > 0)
                q.Where(prms.ToArray());

            q.es.CountAll = true;
            q.es.CountAllAlias = "Count";
            q.es2.Connection.CommandTimeout = 600;
            q.es.WithNoLock = true;

            if (q.Load())
                return (int)entity.GetColumn("Count");
            else
                return 0;
        }
    }
}

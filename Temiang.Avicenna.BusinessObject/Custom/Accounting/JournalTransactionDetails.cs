/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 3/3/2010 4:56:15 PM
===============================================================================
*/

using System;
using System.Web;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Temiang.Avicenna.BusinessObject.Common;

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
	public partial class JournalTransactionDetails : esJournalTransactionDetails
	{
        private bool _isRevenue;
        private string _srBillingGroup;
        private string _errMsg;
        private string _serviceUnitID;
        private string _tariffCompID;
        private string _srRegType;

        public bool IsRevenue {
            get { return _isRevenue; }
            set { _isRevenue = value; }
        }
        public string SRBillingGroup
        {
            get { return _srBillingGroup; }
            set { _srBillingGroup = value; }
        }
        public string SRRegistrationType
        {
            get { return _srRegType; }
            set { _srRegType = value; }
        }
        public string ErrorMsg
        {
            get { return _errMsg; }
            set { _errMsg = value; }
        }
        
        public string RefferenceNumber
        {
            get { return (string)this.GetColumn("RefferenceNumber"); }
        }
       
        public string ChartOfAccountCode
        {
            get { return (string)this.GetColumn("ChartOfAccountCode"); }
        }
        
        public string ChartOfAccountName
        {
            get { return (string)this.GetColumn("ChartOfAccountName"); }
        }
       
        public string SubLedgerName
        {
            get
            {
                string retVal = string.Empty;
                retVal = this.SubLedgerId == 0 ? string.Empty : string.Format("{0} - {1}", this.GetColumn("SubLedgerName"), this.GetColumn("SubLedger_Description"));
                return retVal;
            }
        }

        public DateTime TransactionDate
        {
            get
            {
                return (DateTime)this.GetColumn("TransactionDate");
            }
        }

        public string TransactionNumber
        {
            get
            {
                return string.Format("{0}-{1}", (string)this.GetColumn("JournalCode"), (string)this.GetColumn("TransactionNumber"));
            }
        }

        public string JournalType
        {
            get
            {
                return (string)this.GetColumn("JournalType");
            }
        }

        public string HeaderDescription
        {
            get
            {
                return (string)this.GetColumn("HeaderDescription");
            }
        }

        public string JournalGrouping {
            get {
                return JournalType + " - " + HeaderDescription;
            }
        }

        public override void Save() {
            CutJournalDescription();
            SetUserCreateUpdate();
            base.Save();
        }

        public void SetUserCreateUpdate() {
            string userID = null;
            DateTime? timeNow = null;
            timeNow = (new DateTime()).NowAtSqlServer();
            try {
                userID = ((UserLogin)HttpContext.Current.Session["_UserLogin"]).UserID != null ? ((UserLogin)HttpContext.Current.Session["_UserLogin"]).UserID : "";
            }
            catch { 
            
            }
            if (this.es.IsAdded) {
                if (!string.IsNullOrEmpty(userID)) {
                    this.CreatedBy = userID;
                }
                if (!this.DateCreated.HasValue) {
                    this.DateCreated = timeNow;
                }
            }
            if (this.es.IsAdded || this.es.IsModified) {
                if (!string.IsNullOrEmpty(userID))
                {
                    this.LastUpdateByUserID = userID;
                }
                this.LastUpdateDateTime = timeNow;
            }
        }

        public bool CutJournalDescription()
        {
            if (!this.es.IsDeleted)
            {
                if (this.Description.Length > 255)
                {
                    this.Description = this.Description.Substring(0, 253) + "~";
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public void SwabDK() {
            var credit = this.Credit ?? 0;
            this.Credit = this.Debit;
            this.Debit = credit;
        }

        public static JournalTransactionDetailsCollection GetAllForTransactions(int journalId)
        {
            JournalTransactionsQuery h = new JournalTransactionsQuery("h");
            JournalTransactionDetailsQuery j = new JournalTransactionDetailsQuery("j");
            ChartOfAccountsQuery c = new ChartOfAccountsQuery("c");
            SubLedgersQuery s = new SubLedgersQuery("s");

            j.Select(j, c.ChartOfAccountCode, c.ChartOfAccountName, s.SubLedgerName, s.Description.As("SubLedger_Description"));
            j.InnerJoin(h).On(j.JournalId == h.JournalId);
            j.InnerJoin(c).On(j.ChartOfAccountId == c.ChartOfAccountId);
            j.LeftJoin(s).On(j.SubLedgerId == s.SubLedgerId);
            j.Where(h.JournalId == journalId);

            JournalTransactionDetailsCollection coll = new JournalTransactionDetailsCollection();
            coll.Query.es.WithNoLock = true;
            coll.Load(j);
            return coll;
        }

        public static JournalTransactionDetailsCollection GetAllForTransactionWithPaging(int journalId, int pageNumber, int pageSize)
        {
            JournalTransactionsQuery h = new JournalTransactionsQuery("h");
            JournalTransactionDetailsQuery j = new JournalTransactionDetailsQuery("j");
            ChartOfAccountsQuery c = new ChartOfAccountsQuery("c");
            SubLedgersQuery s = new SubLedgersQuery("s");

            j.Select(j, c.ChartOfAccountCode, c.ChartOfAccountName, s.SubLedgerName, s.Description.As("SubLedger_Description"));
            j.InnerJoin(h).On(j.JournalId == h.JournalId);
            j.InnerJoin(c).On(j.ChartOfAccountId == c.ChartOfAccountId);
            j.LeftJoin(s).On(j.SubLedgerId == s.SubLedgerId);
            j.Where(j.JournalId == journalId);
            j.OrderBy(j.DetailId.Ascending);

            //if (string.IsNullOrEmpty(j.RegistrationNo) == false)
            //{
            //    j.Where(string.Format("<REPLACE(REPLACE(REPLACE(jtd.RegistrationNo, CHAR(13), ''), CHAR(10), ''), ' ', '') LIKE '%' + REPLACE('{0}', ' ', '')+'%'>", j.RegistrationNo, j.es.JoinAlias));
            //}

            j.es.PageSize = pageSize;
            j.es.PageNumber = pageNumber + 1;
            j.es.WithNoLock = true;

            JournalTransactionDetailsCollection coll = new JournalTransactionDetailsCollection();
            coll.Load(j);

            return coll;
        }

        public static void GetTotal(int journalId, out double debit, out double credit)
        {
            debit = 0;
            credit = 0;

            JournalTransactionDetailsQuery j = new JournalTransactionDetailsQuery("j");
            j.Select(j.Debit.Sum().As("Total_Debit"), j.Credit.Sum().As("Total_Credit"));
            j.Where(j.JournalId == journalId);
            j.es.WithNoLock = true;

            JournalTransactionDetails entity = new JournalTransactionDetails();
            if (entity.Load(j))
            {
                debit = Convert.ToDouble(entity.GetColumn("Total_Debit") == DBNull.Value ? 0 : entity.GetColumn("Total_Debit"));
                credit = Convert.ToDouble(entity.GetColumn("Total_Credit") == DBNull.Value ? 0 : entity.GetColumn("Total_Credit"));
            }
        }

        public static JournalTransactionDetailsCollection GetByChartofAccountId(int chartOfAccountId, int? subLedgerId, DateTime dateStart, DateTime dateEnd)
        {
            JournalTransactionsQuery h = new JournalTransactionsQuery("h");
            JournalTransactionDetailsQuery j = new JournalTransactionDetailsQuery("j");
            ChartOfAccountsQuery c = new ChartOfAccountsQuery("c");
            SubLedgersQuery s = new SubLedgersQuery("s");

            j.Select(j, h.JournalCode, h.TransactionNumber, h.TransactionDate, c.ChartOfAccountCode, c.ChartOfAccountName, s.SubLedgerName, s.Description.As("SubLedger_Description"), h.JournalType, h.RefferenceNumber.Coalesce("''").As("RefferenceNumber"));
            j.InnerJoin(h).On(j.JournalId == h.JournalId);
            j.InnerJoin(c).On(j.ChartOfAccountId == c.ChartOfAccountId);
            j.LeftJoin(s).On(j.SubLedgerId == s.SubLedgerId);
            j.Where(j.ChartOfAccountId == chartOfAccountId, h.TransactionDate.Date().Between(dateStart, dateEnd),
                h.IsVoid == false);
            if (subLedgerId.HasValue)
                j.Where(j.SubLedgerId == subLedgerId.Value);
            j.OrderBy(h.TransactionDate.Ascending, j.DetailId.Ascending);

            JournalTransactionDetailsCollection coll = new JournalTransactionDetailsCollection();
            coll.Query.es.WithNoLock = true;
            coll.Load(j);
            return coll;
        }

        public static JournalTransactionDetails Get(int journalId, int detailId)
        {
            JournalTransactionsQuery h = new JournalTransactionsQuery("h");
            JournalTransactionDetailsQuery j = new JournalTransactionDetailsQuery("j");
            ChartOfAccountsQuery c = new ChartOfAccountsQuery("c");
            SubLedgersQuery s = new SubLedgersQuery("s");

            j.Select(j, c.ChartOfAccountCode, c.ChartOfAccountName, s.SubLedgerName, s.Description.As("SubLedger_Description"));
            j.InnerJoin(h).On(j.JournalId == h.JournalId);
            j.InnerJoin(c).On(j.ChartOfAccountId == c.ChartOfAccountId);
            j.LeftJoin(s).On(j.SubLedgerId == s.SubLedgerId);
            j.Where(h.JournalId == journalId, j.DetailId == detailId);

            JournalTransactionDetails e = new JournalTransactionDetails();
            e.Query.es.WithNoLock = true;
            if (e.Load(j))
                return e;
            else
                return null;
        }

        public static int TotalCount(int journalId)
        {
            int retVal = 0;
            JournalTransactionDetails entity = new JournalTransactionDetails();

            entity.Query.es.CountAll = true;
            entity.Query.es.CountAllAlias = "Count";
            entity.Query.es.WithNoLock = true;
            entity.Query.Where(entity.Query.JournalId == journalId);

            if (entity.Query.Load())
                retVal = (int)entity.GetColumn("Count");

            return retVal;
        }
	}
}

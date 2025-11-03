/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 7/27/2011 11:29:31 PM
===============================================================================
*/

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using Temiang.Dal.Core;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CashTransaction : esCashTransaction
    {
        private string _PaymentNo;
        private string _SequenceNo;
        /// <summary>
        /// Return: "1":Ref ke Cash Transaction
        ///         "2":Ref ke payment receive
        /// </summary>
        public string RefferenceIdentification
        {
            get
            {
                if (this.DetailIdRef.HasValue) return "1";



                // cek payment pasien
                if (this.TransactionId.HasValue)
                {
                    var ctdColl = new CashTransactionDetailCollection();
                    if (ctdColl.LoadByTransactionId(this.TransactionId.Value))
                    {
                        var ids = ctdColl.Select(x => x.DetailId);

                        var payQ = new TransPaymentItemQuery();
                        payQ.Where(payQ.CashTransactionReconcileId.In(ids));
                        var payColl = new TransPaymentItemCollection();
                        if (payColl.Load(payQ))
                        {
                            //_PaymentNo = payColl.First().PaymentNo;
                            //_SequenceNo = payColl.First().SequenceNo;

                            _PaymentNo = string.Empty;
                            _SequenceNo = string.Empty;
                            foreach (var pay in payColl)
                            {
                                _PaymentNo += ((_PaymentNo.Length == 0) ? "" : "#") + pay.PaymentNo + "^" + pay.SequenceNo;
                            }
                            return "2";
                        }
                    }
                }

                // cek payment ar
                if (this.TransactionId.HasValue)
                {
                    var pay = new InvoicesQuery();
                    pay.Where(pay.CashTransactionReconcileId.Equal(this.TransactionId));
                    var payColl = new InvoicesCollection();
                    if (payColl.Load(pay))
                    {
                        _PaymentNo = payColl.First().InvoiceNo;
                        _SequenceNo = string.Empty;
                        return "3";
                    }
                }

                // cek payment ap
                if (this.TransactionId.HasValue)
                {
                    var pay = new InvoiceSupplierQuery();
                    pay.Where(pay.CashTransactionReconcileId.Equal(this.TransactionId));
                    var payColl = new InvoiceSupplierCollection();
                    if (payColl.Load(pay))
                    {
                        _PaymentNo = payColl.First().InvoiceNo;
                        _SequenceNo = string.Empty;
                        return "4";
                    }
                }

                // cek itemtrans
                if (this.TransactionId.HasValue)
                {
                    var pay = new ItemTransaction();
                    pay.Query.Where(pay.Query.Or(
                        pay.Query.CashTransactionReconcileId.Like(this.TransactionId.Value.ToString()),
                        pay.Query.CashTransactionReconcileId.Like(this.TransactionId.Value.ToString() + ",%"),
                        pay.Query.CashTransactionReconcileId.Like("%," + this.TransactionId.Value.ToString()),
                        pay.Query.CashTransactionReconcileId.Like("%," + this.TransactionId.Value.ToString() + ",%")));
                    if (pay.Query.Load())
                    {
                        _PaymentNo = pay.TransactionNo;
                        _SequenceNo = string.Empty;
                        switch (pay.TransactionCode)
                        {
                            case TransactionCode.PurchaseOrder:
                                return "5";
                            case TransactionCode.PurchaseOrderReturn:
                                return "6";
                            case TransactionCode.PurchaseOrderReceive:
                                return "7";
                        }
                    }
                }
                return "";
            }
        }

        public string PaymentNo
        {
            get
            {
                return _PaymentNo;
            }
        }
        public string SequenceNo
        {
            get
            {
                return _SequenceNo;
            }
        }

        /// <summary>
        /// Account Name
        /// </summary>
        public string AccountName
        {
            get
            {
                return this.GetColumn("AccountName") is DBNull ? string.Empty : (string)this.GetColumn("AccountName");
            }
        }
        /// <summary>
        /// Account Number
        /// </summary>
        public string AccountNumber
        {
            get
            {
                return this.GetColumn("AccountNumber") is DBNull ? string.Empty : (string)this.GetColumn("AccountNumber");
            }
        }

        public decimal Amount
        {
            get
            {
                return this.GetColumn("Amount") is DBNull ? decimal.Zero : (decimal)this.GetColumn("Amount");
            }
        }

        /// <summary>
        /// Additional properties
        /// </summary>
        /// <returns></returns>
        protected override List<esPropertyDescriptor> GetLocalBindingProperties()
        {
            var items = new List<esPropertyDescriptor>
                            {
                                new esPropertyDescriptor(this, "AccountName", typeof(string)),
                                new esPropertyDescriptor(this, "AccountNumber", typeof(string)),
                                new esPropertyDescriptor(this, "Amount", typeof(decimal))
                            };
            return items;
        }


        public void Void(string UserID)
        {
            this.IsVoid = true;
            this.VoidDate = DateTime.Now.Date;
            this.LastUpdateByUserID = UserID;
            this.LastUpdateDateTime = DateTime.Now;

            using (var scope = new esTransactionScope())
            {
                this.Save();

                //var tpiColl = new TransPaymentItemCollection();
                //CashTransaction.SetLinkToPaymentReceive(tpiColl, this, string.Empty, string.Empty);
                //tpiColl.Save();

                var tpARColl = new InvoicesCollection();
                CashTransaction.SetLinkToPaymentARReceive(tpARColl, this, string.Empty);
                tpARColl.Save();

                var tpAPColl = new InvoiceSupplierCollection();
                CashTransaction.SetLinkToPaymentAPReceive(tpAPColl, this, string.Empty);
                tpAPColl.Save();

                // save changes to database
                scope.Complete();
            }
        }


        public static int TotalCount(string journalNumber, string bankId, string moduleName, string transactionType, string documentNumber, DateTime? transactionDate, DateTime? transactionDateTo, string status, string description,
            string descriptionDetail, double? Amount, string rangeFilter)
        {
            var retVal = 0;
            var qr = new CashTransactionQuery("a");
            var prms = new List<esComparison>();


            if (!string.IsNullOrEmpty(journalNumber))
            {
                string searchTextContain = string.Format("%{0}%", journalNumber);
                prms.Add(qr.JournalNumber.Like(searchTextContain));
            }

            if (!string.IsNullOrEmpty(bankId))
            {
                string searchTextContain = string.Format("%{0}%", bankId);
                prms.Add(qr.BankId.Like(searchTextContain));
                //prms.Add(qr.BankId.Like("%" + bankId + "%"));
            }

            if (!string.IsNullOrEmpty(moduleName))
            {
                prms.Add(qr.Module.Equal(moduleName));
            }

            if (!string.IsNullOrEmpty(transactionType))
            {
                prms.Add(qr.TransactionType.Equal(transactionType));
            }

            if (!string.IsNullOrEmpty(documentNumber))
            {
                string searchTextContain = string.Format("%{0}%", documentNumber);
                prms.Add(qr.DocumentNumber.Like(searchTextContain));
                //prms.Add(qr.BankId.Like("%" + documentNumber + "%"));
            }

            if (transactionDate.HasValue && transactionDateTo.HasValue)
            {
                prms.Add(qr.TransactionDate.Between(transactionDate.Value, transactionDateTo.Value));
            }

            if (!string.IsNullOrEmpty(description))
            {
                string searchTextContain = string.Format("%{0}%", description);
                prms.Add(qr.Description.Like(searchTextContain));
                //prms.Add(qr.BankId.Like("%" + description + "%"));
            }
            if (!string.IsNullOrEmpty(descriptionDetail))
            {
                // WTF!!! bingung gw bikin sub query
                //var dd = new CashTransactionDetailQuery("dd");
                //dd.Where(dd.Description.Like("%" + descriptionDetail + "%"))
                //    .Select(dd.TransactionId).As("ddx");
                //dd.es.Distinct = true;
                //qr.InnerJoin(dd).On(qr.TransactionId == dd.TransactionId);

                // coba cara gampang
                string searchTextContain = string.Format("%{0}%", descriptionDetail);
                var dd = new CashTransactionDetailQuery("dd");
                dd.Where(dd.Description.Like(searchTextContain))
                    .Select(dd.TransactionId);
                dd.es.Distinct = true;
                dd.es.Top = 100;
                var tbl = dd.LoadDataTable();
                var TransIDs = (from t in tbl.AsEnumerable()
                                select t.Field<Int32>("TransactionId")).ToArray();
                if (TransIDs.Length > 0)
                {
                    prms.Add(qr.TransactionId.In(TransIDs));
                }
                else
                {
                    prms.Add(qr.TransactionId.Equal(0));
                }
            }
            if (Amount.HasValue && Amount != -999)
            {
                var c = new CashTransactionDetailQuery("c");
                qr.InnerJoin((c.Select(
                            c.TransactionId,
                            c.Amount.Sum().As("Amount"))
                        .GroupBy(c.TransactionId))).On(c.TransactionId == qr.TransactionId);
                c.Having(c.Amount.Sum() == Amount.Value);
                qr.Where(string.Format("<c.Amount = {0}>", Amount.Value));
            }


            var toDay = DateTime.Now.Date;
            var firstDayOfMonth = new DateTime(toDay.Year, toDay.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var oneLastMonthDay = toDay.AddMonths(-1);
            var firstDayOfYear = new DateTime(toDay.Year, 1, 1);
            var lastDayOfYear = new DateTime(toDay.Year, 12, 31);
            var oneLastYearDay = toDay.AddYears(-1);

            // this month
            if (rangeFilter == "1")
            {
                prms.Add(qr.TransactionDate.GreaterThanOrEqual(firstDayOfMonth));
                prms.Add(qr.TransactionDate.LessThan(lastDayOfMonth.AddDays(1)));
            }
            // one last month
            if (rangeFilter == "2")
            {
                prms.Add(qr.TransactionDate.GreaterThanOrEqual(oneLastMonthDay));
                prms.Add(qr.TransactionDate.LessThan(toDay.AddDays(1)));
            }
            // this year
            if (rangeFilter == "3")
            {
                prms.Add(qr.TransactionDate.GreaterThanOrEqual(firstDayOfYear));
                prms.Add(qr.TransactionDate.LessThan(lastDayOfYear.AddDays(1)));
            }
            // one last year
            if (rangeFilter == "4")
            {
                prms.Add(qr.TransactionDate.GreaterThanOrEqual(oneLastYearDay));
                prms.Add(qr.TransactionDate.LessThan(toDay.AddDays(1)));
            }

            switch (status)
            {
                case "0": // not approvaed
                    prms.Add(qr.IsPosted == false);
                    break;
                case "1":
                    prms.Add(qr.IsPosted == true);
                    break;
            }


            qr.es.CountAll = true;
            qr.es.CountAllAlias = "Count";
            qr.es.WithNoLock = true;
            if (prms.Count > 0)
                qr.Where(prms.ToArray());

            CashTransaction entity = new CashTransaction();
            if (entity.Load(qr))
                retVal = (int)entity.GetColumn("Count");

            return retVal;
        }

        private static esOrderByItem[] SafeOrderByItems(string sortString, CashTransactionQuery q)
        {
            var list = new List<esOrderByItem>();
            var fieldsName = sortString.ToLowerInvariant().Split(char.Parse(","));
            foreach (var field in fieldsName)
            {
                var tmp = field.Split(char.Parse("^"));
                var isDesc = false;
                if (tmp.Length > 1)
                    isDesc = tmp[1].Equals("desc");

                if (tmp[0].Equals("transactionid"))
                    list.Add(isDesc ? q.TransactionId.Descending : q.TransactionId.Ascending);
            }
            return list.ToArray();
        }

        public static CashTransactionCollection GetAllWithPaging(int pageNumber, int pageSize,
            string journalNumber, string bankId, string moduleName, string transactionType, string documentNumber, DateTime? transactionDate, DateTime? transactionDateTo, string status,
            string sortString, string description, string descriptionDetail, double? Amount, string rangeFilter)
        {
            var a = new CashTransactionQuery("a");
            var b = new BankQuery("b");
            var c = new CashTransactionDetailQuery("c");

            a.Select( //a, 
                a.TransactionId,
                a.JournalNumber,
                a.TransactionDate,
                a.TransactionType,
                a.Description,
                a.ChequeNumber,
                a.DueDate,
                a.LastUpdateDateTime,
                a.LastUpdateByUserID,
                a.IsPosted,
                a.IsVoid,
                b.BankName.As("AccountName"), b.NoRek.As("AccountNumber"), "<d.Amount>");
            a.From(c.Select(
                        c.TransactionId,
                        c.Amount.Sum().As("Amount"))
                    .GroupBy(c.TransactionId)).As("d");
            a.RightJoin(a).On(a.TransactionId.Equal(c.TransactionId));
            a.InnerJoin(b).On(a.BankId.Equal(b.BankID));


            // where
            var prms = new List<esComparison>();

            if (!string.IsNullOrEmpty(journalNumber))
            {
                string searchTextContain = string.Format("%{0}%", journalNumber);
                prms.Add(a.JournalNumber.Like(searchTextContain));
                //prms.Add(a.JournalNumber.Like("%" + journalNumber + "%"));
            }
            if (!string.IsNullOrEmpty(bankId))
            {
                string searchTextContain = string.Format("%{0}%", bankId);
                prms.Add(a.BankId.Like(searchTextContain));
                //prms.Add(a.BankId.Like("%" + bankId + "%"));
            }
            if (!string.IsNullOrEmpty(moduleName))
            {
                prms.Add(a.Module.Equal(moduleName));
            }
            if (!string.IsNullOrEmpty(transactionType))
            {
                prms.Add(a.TransactionType.Equal(transactionType));
            }
            if (!string.IsNullOrEmpty(documentNumber))
            {
                string searchTextContain = string.Format("%{0}%", documentNumber);
                prms.Add(a.DocumentNumber.Like(searchTextContain));
                //prms.Add(a.DocumentNumber.Like("%" + documentNumber + "%"));
            }
            if (transactionDate.HasValue && transactionDateTo.HasValue)
            {
                prms.Add(a.TransactionDate.Between(transactionDate.Value, transactionDateTo.Value));
            }
            if (!string.IsNullOrEmpty(description))
            {
                string searchTextContain = string.Format("%{0}%", description);
                prms.Add(a.Description.Like(searchTextContain));
                //prms.Add(a.Description.Like("%" + description + "%"));
            }
            if (!string.IsNullOrEmpty(descriptionDetail))
            {
                // WTF!!! bingung gw bikin sub query
                //var dd = new CashTransactionDetailQuery("dd");
                //dd.Where(dd.Description.Like("%" + descriptionDetail + "%"))
                //    .Select(dd.TransactionId).As("ddx");
                //dd.es.Distinct = true;
                //a.InnerJoin(dd).On(a.TransactionId == dd.TransactionId);

                // coba cara gampang
                string searchTextContain = string.Format("%{0}%", descriptionDetail);
                var dd = new CashTransactionDetailQuery("dd");
                dd.Where(dd.Description.Like(searchTextContain))
                    .Select(dd.TransactionId);
                dd.es.Distinct = true;
                dd.es.Top = 100;
                var tbl = dd.LoadDataTable();
                var TransIDs = (from t in tbl.AsEnumerable()
                                select t.Field<Int32>("TransactionId")).ToArray();
                if (TransIDs.Length > 0)
                {
                    prms.Add(a.TransactionId.In(TransIDs));
                }
                else
                {
                    prms.Add(a.TransactionId.Equal(0));
                }
            }
            if (Amount.HasValue && Amount != -999)
            {
                c.Having(c.Amount.Sum() == Amount.Value);
                a.Where(string.Format("<d.Amount = {0}>", Amount.Value));
            }

            var toDay = DateTime.Now.Date;
            var firstDayOfMonth = new DateTime(toDay.Year, toDay.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var oneLastMonthDay = toDay.AddMonths(-1);
            var firstDayOfYear = new DateTime(toDay.Year, 1, 1);
            var lastDayOfYear = new DateTime(toDay.Year, 12, 31);
            var oneLastYearDay = toDay.AddYears(-1);
            // this month
            if (rangeFilter == "1")
            {
                prms.Add(a.TransactionDate.GreaterThanOrEqual(firstDayOfMonth));
                prms.Add(a.TransactionDate.LessThan(lastDayOfMonth.AddDays(1)));
            }
            // one last month
            if (rangeFilter == "2")
            {
                prms.Add(a.TransactionDate.GreaterThanOrEqual(oneLastMonthDay));
                prms.Add(a.TransactionDate.LessThan(toDay.AddDays(1)));
            }
            // this year
            if (rangeFilter == "3")
            {
                prms.Add(a.TransactionDate.GreaterThanOrEqual(firstDayOfYear));
                prms.Add(a.TransactionDate.LessThan(lastDayOfYear.AddDays(1)));
            }
            // one last year
            if (rangeFilter == "4")
            {
                prms.Add(a.TransactionDate.GreaterThanOrEqual(oneLastYearDay));
                prms.Add(a.TransactionDate.LessThan(toDay.AddDays(1)));
            }

            switch (status)
            {
                case "0": // not approvaed
                    prms.Add(a.IsPosted == false);
                    break;
                case "1":
                    prms.Add(a.IsPosted == true);
                    break;
            }
            if (prms.Count > 0)
                a.Where(prms.ToArray());

            // order by
            a.OrderBy(a.TransactionId.Descending);
            //a.OrderBy(safeOrderByItems(sortString, a));

            a.es.WithNoLock = true;
            a.es.PageSize = pageSize;
            a.es.PageNumber = pageNumber + 1;

            var coll = new CashTransactionCollection();
            coll.Load(a);
            return coll;
        }

        public static CashTransaction Get(int transactionId)
        {
            var entity = new CashTransaction();
            entity.Query.Where(entity.Query.TransactionId == transactionId);
            return entity.Query.Load() ? entity : null;
        }

        public static int MarkStatusAsPosting(int transactionId, string editBy, string code)
        {
            var entity = Get(transactionId);
            var en = CashTransactionDetail.GetAllForTransactions(transactionId);
            var bankAccount = Bank.Get(entity.BankId);
            var journalAlreadyCreate = (entity.JournalId.HasValue && entity.JournalId.Value > 0);

            if (journalAlreadyCreate)
            {
                return PostingUpdateBalance(transactionId, entity.JournalId.Value);
            }

            var debitAmount = decimal.Zero;
            var creditAmount = decimal.Zero;

            decimal totalCredit = 0;
            decimal totalDebit = 0;

            foreach (var e in en)
            {
                debitAmount += e.Credit.Value;
                creditAmount += e.Debit.Value;
            }

            // --------------------------------------------------------------------------------------------------------------------------
            // Start Journal Code Checking or auto generation
            // --------------------------------------------------------------------------------------------------------------------------

            var journalCodeValid = false;
            JournalCodes journalCodeEntity = null;

            if (!string.IsNullOrEmpty(bankAccount.JournalCode))
            {
                int journalCodeId;
                var result = int.TryParse(bankAccount.JournalCode, out journalCodeId);
                if (result)
                {
                    journalCodeEntity = JournalCodes.Get(journalCodeId);
                    journalCodeValid = (journalCodeEntity != null);
                }
            }

            var journalCode = !journalCodeValid
                                  ? code
                                  : JournalCodes.GetOrCreateAutoJournalCode("CM", entity.TransactionDate.Value);
            //journalCodeEntity.JournalCode;


            //var journalCode = code;

            // --------------------------------------------------------------------------------------------------------------------------
            // END JournalCode Checking
            // --------------------------------------------------------------------------------------------------------------------------


            using (var scope = new esTransactionScope())
            {
                // --------------------------------------------------------------------------------------------------------------------------
                // Create Journal Header
                // --------------------------------------------------------------------------------------------------------------------------
                var now = DateTime.Now;
                var j = new JournalTransactions
                {
                    JournalType = JournalType.CashTransaction,
                    JournalCode = journalCode,
                    TransactionNumber = JournalCodes.GenerateAndIncrementAutoNumber(journalCode),
                    TransactionDate = entity.TransactionDate,
                    Description = entity.Description,
                    IsPosted = false,
                    DateCreated = now,
                    LastUpdateDateTime = now,
                    CreatedBy = editBy,
                    LastUpdateByUserID = editBy
                };
                j.AddNew();
                j.Save();

                // --------------------------------------------------------------------------------------------------------------------------
                // END Create Journal Header
                // --------------------------------------------------------------------------------------------------------------------------


                // --------------------------------------------------------------------------------------------------------------------------
                // Create Journal Detail
                // --------------------------------------------------------------------------------------------------------------------------
                var isDebitPos = (entity.NormalBalance.ToLowerInvariant() == "d");
                // create cash COA at the very first line for debit transaction <-- wrong
                if (isDebitPos)
                {
                    var d = new JournalTransactionDetails
                    {
                        JournalId = j.JournalId,
                        ChartOfAccountId = bankAccount.ChartOfAccountId,
                        SubLedgerId = 0,
                        Debit = (debitAmount - creditAmount) >= 0 ? (debitAmount - creditAmount) : 0,
                        Credit = (debitAmount - creditAmount) >= 0 ? 0 : ((debitAmount - creditAmount) * -1),
                        Description = entity.Description,
                        DocumentNumber = entity.DocumentNumber
                    };
                    d.AddNew();
                    d.Save();
                    totalDebit += d.Debit.Value;
                }

                // find related front office payment related to cash transaction detail
                var payDetails = new TransPaymentItemCollection();
                var ctDetailIDs = en.Select(x => x.DetailId);
                if (ctDetailIDs.Count() > 0)
                {
                    payDetails.Query.Where(payDetails.Query.CashTransactionReconcileId.In(ctDetailIDs));
                    payDetails.LoadAll();
                }

                // create every transaction found, no need to worries with the debit/credit amount. We assume its correct!
                //foreach (var d in en.Select(e => new JournalTransactionDetails
                //{
                //    JournalId = j.JournalId,
                //    ChartOfAccountId = e.ChartOfAccountId,
                //    SubLedgerId = e.SubLedgerId.HasValue ? e.SubLedgerId : 0,
                //    Debit = e.Debit,
                //    Credit = e.Credit,
                //    Description = e.Description,
                //    DocumentNumber = entity.DocumentNumber

                //}))
                //{
                //    d.AddNew();
                //    d.Save();
                //    totalCredit += d.Credit.Value;
                //    totalDebit += d.Debit.Value;
                //}

                foreach (var d in en)
                {
                    JournalTransactionDetails jtd = new JournalTransactionDetails();

                    jtd.JournalId = j.JournalId;
                    jtd.ChartOfAccountId = d.ChartOfAccountId;
                    jtd.SubLedgerId = d.SubLedgerId.HasValue ? d.SubLedgerId : 0;
                    jtd.Debit = d.Debit;
                    jtd.Credit = d.Credit;
                    jtd.Description = d.Description;
                    jtd.DocumentNumber = (payDetails.Count > 0) ?
                        (payDetails.Where(y => y.CashTransactionReconcileId == d.DetailId).Select(y => y.PaymentNo).FirstOrDefault())
                        : entity.DocumentNumber;
                    if (string.IsNullOrEmpty(jtd.DocumentNumber)) jtd.DocumentNumber = string.Empty;
                    jtd.DocumentNumberSequenceNo = (payDetails.Count > 0) ?
                        (payDetails.Where(y => y.CashTransactionReconcileId == d.DetailId).Select(y => y.SequenceNo).FirstOrDefault())
                        : null;

                    jtd.AddNew();
                    jtd.Save();
                    totalCredit += jtd.Credit.Value;
                    totalDebit += jtd.Debit.Value;
                }

                // if credit transaction we create cash coa at the last line 
                if (!isDebitPos)
                {
                    var cAmt = creditAmount - debitAmount;
                    var d = new JournalTransactionDetails
                    {
                        JournalId = j.JournalId,
                        ChartOfAccountId = bankAccount.ChartOfAccountId,
                        SubLedgerId = 0,
                        Debit = cAmt < 0 ? (-cAmt) : 0,
                        Credit = cAmt <= 0 ? 0 : cAmt,
                        Description = entity.Description,
                        DocumentNumber = entity.DocumentNumber
                    };
                    d.AddNew();
                    d.Save();
                    totalCredit += d.Credit.Value;
                }
                // --------------------------------------------------------------------------------------------------------------------------
                // END Create Journal Detail
                // --------------------------------------------------------------------------------------------------------------------------


                // --------------------------------------------------------------------------------------------------------------------------
                // Last. Attach journal ID for this cash transaction and calculate last balance
                // --------------------------------------------------------------------------------------------------------------------------
                var result = PostingUpdateBalance(transactionId, j.JournalId.Value);

                //auto approved journal
                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsAutoApprovedPaymentBackOffice) == "Yes")
                {
                    if ((totalDebit > 0 && totalCredit > 0) && (totalDebit == totalCredit))
                    {
                        j.BeginEdit();
                        j.IsPosted = true;
                        j.EndEdit();
                        j.Save();
                    }
                }

                scope.Complete();
            }

            return 0;
        }

        public static int MarkStatusAsUnPosting(int transactionId, string editBy, string code)
        {
            var entity = Get(transactionId);
            //var en = CashTransactionDetail.GetAllForTransactions(transactionId);
            //var bankAccount = Bank.Get(entity.BankId);
            var journalAlreadyCreate = (entity.JournalId.HasValue && entity.JournalId.Value > 0);

            using (var scope = new esTransactionScope())
            {
                // start void journal
                if (journalAlreadyCreate)
                {
                    JournalTransactions ju = JournalTransactions.Get(entity.JournalId.Value);
                    if (ju.IsPosted.Value)
                    {
                        // can not unpost, please unpost journal manually first
                        return -1;
                    }

                    if (!ju.IsVoid.Value)
                    {
                        ju.IsVoid = true;
                        ju.VoidDate = DateTime.Now.Date;
                        ju.LastUpdateByUserID = editBy;
                        ju.LastUpdateDateTime = DateTime.Now;
                        ju.Save();
                    }
                    // end of void journal

                    // start unpost cash transaction
                    // end of unpost cash transaction
                }

                UnPostingUpdateBalance(entity.TransactionId.Value);

                scope.Complete();
            }

            return 0;
        }

        public static int MarkStatusAsUnPostingRev2(int transactionId, string editBy, string code)
        {
            var entity = Get(transactionId);

            using (var scope = new esTransactionScope())
            {
                UnPostingUpdateBalance(entity.TransactionId.Value);

                // do reverse journal
                JournalTransactions.AddNewReverseJournal(0, entity.JournalId.Value,
                    code, editBy, DateTime.Now, "Unapproval");

                scope.Complete();
            }

            return 0;
        }

        public static int PostingUpdateBalance(int transactionId, int journalId)
        {
            var prms = new esParameters
                           {
                               {"TransactionId", transactionId, esParameterDirection.Input, DbType.Int32, 0},
                               {"JournalId", journalId, esParameterDirection.Input, DbType.Int32, 0}
                           };

            var util = new esUtility();
            var ret = util.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_CashTransactionUpdateBalance", prms);

            return 0;
        }

        public static int UnPostingUpdateBalance(int transactionId)
        {
            var prms = new esParameters
                           {
                               {"TransactionId", transactionId, esParameterDirection.Input, DbType.Int32, 0}
                           };

            var util = new esUtility();
            var ret = util.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_CashTransactionUpdateBalanceUnPosting", prms);

            return 0;
        }

        public static int Save(CashTransaction entity)
        {
            entity.Save();
            return entity.TransactionId.Value;
        }

        public static int AddNew(CashTransaction entity)
        {
            return AddNew(entity, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public static void SetLinkToPaymentReceive_(TransPaymentItemCollection newTpColl, CashTransaction entity, string PaymentNo, string SequenceNo)
        {
            if (PaymentNo == null) PaymentNo = string.Empty;
            if (SequenceNo == null) SequenceNo = string.Empty;

            newTpColl.Query.Where(newTpColl.Query.CashTransactionReconcileId.Equal(entity.TransactionId),
                "<[PaymentNo] + SequenceNo <> '" + PaymentNo + SequenceNo + "'>");
            if (newTpColl.LoadAll())
            {
                foreach (var tpR in newTpColl)
                {
                    tpR.CashTransactionReconcileId = null;
                }
            }

            if (!string.IsNullOrEmpty(PaymentNo) && !string.IsNullOrEmpty(SequenceNo))
            {
                var tp = new TransPaymentItem();
                tp.Query.Where(tp.Query.PaymentNo.Equal(PaymentNo), tp.Query.SequenceNo.Equal(SequenceNo));
                if (tp.Load(tp.Query))
                {
                    tp.CashTransactionReconcileId = entity.TransactionId.Value;
                    newTpColl.AttachEntity(tp);
                }
            }
        }

        public static void SetLinkToPaymentARReceive(InvoicesCollection newPARColl, CashTransaction entity, string InvoicePaymenNo)
        {
            if (InvoicePaymenNo == null) InvoicePaymenNo = string.Empty;

            newPARColl.Query.Where(newPARColl.Query.CashTransactionReconcileId == entity.TransactionId,
                newPARColl.Query.InvoiceNo != InvoicePaymenNo);
            if (newPARColl.LoadAll())
            {
                foreach (var tpR in newPARColl)
                {
                    tpR.CashTransactionReconcileId = null;
                }
            }

            if (!string.IsNullOrEmpty(InvoicePaymenNo))
            {
                var tp = new Invoices();
                tp.Query.Where(tp.Query.InvoiceNo.Equal(InvoicePaymenNo));
                if (tp.Load(tp.Query))
                {
                    tp.CashTransactionReconcileId = entity.TransactionId.Value;
                    newPARColl.AttachEntity(tp);
                }
            }
        }

        public static void SetLinkToPaymentAPReceive(InvoiceSupplierCollection newPAPColl, CashTransaction entity, string InvoicePaymenNo)
        {
            if (InvoicePaymenNo == null) InvoicePaymenNo = string.Empty;

            newPAPColl.Query.Where(newPAPColl.Query.CashTransactionReconcileId == entity.TransactionId,
                newPAPColl.Query.InvoiceNo != InvoicePaymenNo);
            if (newPAPColl.LoadAll())
            {
                foreach (var tpR in newPAPColl)
                {
                    tpR.CashTransactionReconcileId = null;
                }
            }

            if (!string.IsNullOrEmpty(InvoicePaymenNo))
            {
                var tp = new InvoiceSupplier();
                tp.Query.Where(tp.Query.InvoiceNo.Equal(InvoicePaymenNo));
                if (tp.Load(tp.Query))
                {
                    tp.CashTransactionReconcileId = entity.TransactionId.Value;
                    newPAPColl.AttachEntity(tp);
                }
            }
        }

        public static void SetLinkToReturnPO(CashTransaction entity, string returnNo)
        {
            if (!string.IsNullOrEmpty(returnNo))
            {
                var it = new ItemTransaction();
                if (it.LoadByPrimaryKey(returnNo))
                {
                    if (string.IsNullOrEmpty(it.CashTransactionReconcileId))
                        it.CashTransactionReconcileId = entity.TransactionId.Value.ToString();
                    else
                    {
                        string[] ids = it.CashTransactionReconcileId.Split(',');
                        if (!ids.Any(i => i == entity.TransactionId.Value.ToString())) it.CashTransactionReconcileId = it.CashTransactionReconcileId + "," + entity.TransactionId.Value.ToString();
                    }
                    it.Save();
                }
            }
        }

        public static int AddNew(CashTransaction entity, string PaymentNo, string SequenceNo,
            string InvoicePaymentAR, string InvoicePaymentAP, string ItemTransaction)
        {
            using (var scope = new esTransactionScope())
            {
                entity.AddNew();
                entity.Save();

                if (!string.IsNullOrEmpty(PaymentNo))
                {
                    //var tpiColl = new TransPaymentItemCollection();
                    //SetLinkToPaymentReceive(tpiColl, entity, PaymentNo, SequenceNo);
                    //tpiColl.Save();
                }

                else if (!string.IsNullOrEmpty(InvoicePaymentAR))
                {
                    var ivARColl = new InvoicesCollection();
                    SetLinkToPaymentARReceive(ivARColl, entity, InvoicePaymentAR);
                    ivARColl.Save();
                }

                else if (!string.IsNullOrEmpty(InvoicePaymentAP))
                {
                    var ivAPColl = new InvoiceSupplierCollection();
                    SetLinkToPaymentAPReceive(ivAPColl, entity, InvoicePaymentAP);
                    ivAPColl.Save();
                }

                else if (!string.IsNullOrEmpty(ItemTransaction))
                {
                    SetLinkToReturnPO(entity, ItemTransaction);
                }

                // save changes to database
                scope.Complete();
            }
            return entity.TransactionId.Value;
        }

        public static int AddNewAutomaticCashTransactionForParamedicFeePaymentGroup(
            Bank bank, DateTime transactionDate, string moduleName,
            string transactionType, string paymentType, string paymentMethod, string normalBalance,
            decimal currencyRate, string chequeNumber, string documentNumber,
            string description, int journalId, string createdBy,
            JournalTransactionDetailsCollection jtdColl, out int? transactionId)
        {
            transactionId = 0;

            // prevent duplicate cash entry transaction when reprocessing journal is fired
            var ceColl = new CashTransactionCollection();
            ceColl.Query.Where(ceColl.Query.DocumentNumber == documentNumber && ceColl.Query.IsVoid == false);
            ceColl.LoadAll();
            if (ceColl.Count == 0)
            {
                var header = new CashTransaction
                {
                    PostingId = 0,
                    BankId = bank.BankID,
                    ChartOfAccountId = bank.ChartOfAccountId,
                    TransactionDate = transactionDate,
                    TransactionType = transactionType,
                    PaymentType = paymentType,
                    PaymentMethod = paymentMethod,
                    NormalBalance = normalBalance,
                    Module = moduleName,
                    CurrencyCode = bank.CurrencyCode,
                    CurrencyRate = currencyRate,
                    IsPosted = false,
                    IsCleared = false,
                    IsVoid = false,
                    ChequeNumber = chequeNumber,
                    DocumentNumber = documentNumber,
                    Description = description,
                    JournalId = journalId,
                    DateCreated = DateTime.Now,
                    LastUpdateDateTime = DateTime.Now,
                    VoidDate = new DateTime(1900, 1, 1),
                    CreatedBy = createdBy,
                    LastUpdateByUserID = createdBy,
                    IsAutoCashEntry = true
                };
                header.AddNew();
                header.Save();

                decimal debitAmount = 0;
                decimal creditAmount = 0;

                foreach (var jtd in jtdColl)
                {
                    var detail = new CashTransactionDetail
                    {
                        TransactionId = header.TransactionId,
                        ChartOfAccountId = jtd.ChartOfAccountId,
                        Debit = jtd.Debit,
                        Credit = jtd.Credit,
                        Amount = (jtd.Debit - jtd.Credit),
                        Description = description,
                        SubLedgerId = jtd.SubLedgerId,
                        CostCenterId = 0
                    };

                    detail.AddNew();
                    detail.Save();
                }

                transactionId = header.TransactionId;
            }
            return 0;
        }

        public static int AddNewAutomaticCashTransaction(string bankId, int? chartOfAccountId, DateTime transactionDate, string moduleName, string transactionType,
            string paymentType, string paymentMethod, string normalBalance, string currencyCode, decimal currencyRate, string chequeNumber, string documentNumber, string description,
            int journalId, string createdBy,
            int chartOfAccountIdDet, decimal payAmt, decimal OtherAmt, int coa_discount, decimal Bank_Cost, int coa_biaya, int subLedgerId, out int? transactionId)
        {
            transactionId = 0;

            // prevent duplicate cash entry transaction when reprocessing journal is fired
            var ceColl = new CashTransactionCollection();
            ceColl.Query.Where(ceColl.Query.DocumentNumber == documentNumber && ceColl.Query.IsVoid == false);
            ceColl.LoadAll();
            if (ceColl.Count == 0)
            {
                var header = new CashTransaction
                {
                    PostingId = 0,
                    BankId = bankId,
                    ChartOfAccountId = chartOfAccountId,
                    TransactionDate = transactionDate,
                    TransactionType = transactionType,
                    PaymentType = paymentType,
                    PaymentMethod = paymentMethod,
                    NormalBalance = normalBalance,
                    Module = moduleName,
                    CurrencyCode = currencyCode,
                    CurrencyRate = currencyRate,
                    IsPosted = false,
                    IsCleared = false,
                    IsVoid = false,
                    ChequeNumber = chequeNumber,
                    DocumentNumber = documentNumber,
                    Description = description,
                    JournalId = journalId,
                    DateCreated = DateTime.Now,
                    LastUpdateDateTime = DateTime.Now,
                    VoidDate = new DateTime(1900, 1, 1),
                    CreatedBy = createdBy,
                    LastUpdateByUserID = createdBy,
                    IsAutoCashEntry = true
                };
                header.AddNew();
                header.Save();

                decimal debitAmount = 0;
                decimal creditAmount = 0;
                if (normalBalance == "K")
                    debitAmount = currencyRate * (payAmt + OtherAmt + Bank_Cost);
                else
                    creditAmount = currencyRate * (payAmt + OtherAmt + Bank_Cost);

                var detail = new CashTransactionDetail
                {
                    TransactionId = header.TransactionId,
                    ChartOfAccountId = chartOfAccountIdDet,
                    Debit = debitAmount,
                    Credit = creditAmount,
                    Amount = (payAmt + OtherAmt + Bank_Cost),
                    Description = description,
                    SubLedgerId = subLedgerId,
                    CostCenterId = 0
                };

                detail.AddNew();
                detail.Save();

                if (OtherAmt != 0)//khusus AR payment
                {
                    var detailDisc = new CashTransactionDetail
                    {
                        TransactionId = header.TransactionId,
                        ChartOfAccountId = coa_discount,
                        Debit = OtherAmt >= 0 ? OtherAmt : 0,
                        Credit = OtherAmt >= 0 ? 0 : (OtherAmt * -1),
                        Amount = OtherAmt * -1,
                        Description = description,
                        SubLedgerId = 0,
                        CostCenterId = 0

                    };
                    detailDisc.AddNew();
                    detailDisc.Save();
                }

                if (Bank_Cost > 0)//khusus AR payment
                {
                    var detailBiaya = new CashTransactionDetail
                    {
                        TransactionId = header.TransactionId,
                        ChartOfAccountId = coa_biaya,
                        Debit = Bank_Cost >= 0 ? Bank_Cost : 0,
                        Credit = Bank_Cost >= 0 ? 0 : (Bank_Cost * -1),
                        Amount = Bank_Cost * -1,
                        Description = description,
                        SubLedgerId = 0,
                        CostCenterId = 0

                    };
                    detailBiaya.AddNew();
                    detailBiaya.Save();
                }

                transactionId = header.TransactionId;
            }
            return 0;
        }

        public static int AddNewAutomaticCashTransactionDownPaymentFromJournal(
            TransPayment tp, TransPaymentItemCollection tpiColl,
            JournalTransactions jt,
            string UserID, out int? transactionId)
        {
            transactionId = 0;

            var jtColl = new JournalTransactionDetailsCollection();
            jtColl.Query.Where(jtColl.Query.JournalId.Equal(jt.JournalId));
            if (jtColl.LoadAll())
            {
                var bColl = new BankCollection();
                bColl.LoadAll();

                bool CashIn = tp.TransactionCode == "018";

                // eliminasi yang bukan cash bank
                var jtToRemove = CashIn ?
                    jtColl.Where(j => (!bColl.Select(b => b.ChartOfAccountId).Contains(j.ChartOfAccountId)) && j.Debit > 0) :
                    jtColl.Where(j => (!bColl.Select(b => b.ChartOfAccountId).Contains(j.ChartOfAccountId)) && j.Credit > 0);

                var jtdCollToDetach = new List<JournalTransactionDetails>();

                foreach (var jttr in jtToRemove)
                {
                    var jtToRemoveK = CashIn ?
                        jtColl.Where(x => x.Credit.Value == jttr.Debit.Value && !bColl.Select(b => b.ChartOfAccountId).Contains(x.ChartOfAccountId)).FirstOrDefault() :
                        jtColl.Where(x => x.Debit.Value == jttr.Credit.Value && !bColl.Select(b => b.ChartOfAccountId).Contains(x.ChartOfAccountId)).FirstOrDefault();
                    if (jtToRemoveK != null)
                    {
                        if (!jtdCollToDetach.Where(jtd => jtd.DetailId == jtToRemoveK.DetailId).Any())
                        {
                            jtdCollToDetach.Add(jtToRemoveK);// jtColl.DetachEntity(jtToRemoveK);
                        }
                        if (!jtdCollToDetach.Where(jtd => jtd.DetailId == jttr.DetailId).Any())
                        {
                            jtdCollToDetach.Add(jttr); //jtColl.DetachEntity(jttr);
                        }
                    }
                    else
                    {
                        // tidak ketemu nominal, how???
                    }
                }
                foreach (var jtdToDetach in jtdCollToDetach)
                {
                    jtColl.DetachEntity(jtdToDetach);
                }


                var jtCash = jtColl.Where(j => j.DocumentNumber == tp.PaymentNo &&
                    (bColl.Select(b => b.ChartOfAccountId).Contains(j.ChartOfAccountId)));
                /* cash transaksi hanya untuk akun yang ada bank-nya */
                if (jtCash.Any())
                {
                    // prevent duplicate cash entry transaction when reprocessing journal is fired
                    var ceColl = new CashTransactionCollection();
                    ceColl.Query.Where(ceColl.Query.DocumentNumber == tp.PaymentNo && ceColl.Query.IsVoid == false);
                    ceColl.LoadAll();
                    if (ceColl.Count == 0)
                    {
                        // nominal paling gede jadi patokan
                        var ces = CashIn ? jtCash.Where(x => x.Debit.Value > 0).OrderByDescending(x => x.Debit.Value) :
                            jtCash.Where(x => x.Credit.Value > 0).OrderByDescending(x => x.Credit.Value);
                        var ce = ces.First();

                        Bank bank = bColl.Where(x => x.ChartOfAccountId == ce.ChartOfAccountId).First();

                        var tpi = tpiColl.Where(x => x.PaymentNo == ce.DocumentNumber && x.SequenceNo == ce.DocumentNumberSequenceNo).First();

                        var header = new CashTransaction
                        {
                            PostingId = 0,
                            BankId = bank.BankID,
                            ChartOfAccountId = bank.ChartOfAccountId,
                            TransactionDate = tp.PaymentDate.Value.Date,
                            TransactionType = "ADJST",
                            PaymentType = tpi.SRPaymentType,
                            PaymentMethod = tpi.SRPaymentMethod,
                            NormalBalance = CashIn ? "D" : "K",
                            Module = "GL",
                            CurrencyCode = bank.CurrencyCode,
                            CurrencyRate = 1,
                            IsPosted = false,
                            IsCleared = false,
                            IsVoid = false,
                            ChequeNumber = string.Empty,
                            DocumentNumber = tp.PaymentNo,
                            Description = jt.Description,
                            JournalId = jt.JournalId,
                            DateCreated = DateTime.Now,
                            LastUpdateDateTime = DateTime.Now,
                            VoidDate = new DateTime(1900, 1, 1),
                            CreatedBy = UserID,
                            LastUpdateByUserID = UserID,
                            IsAutoCashEntry = true
                        };
                        header.AddNew();
                        header.Save();

                        // detail
                        if (ces.Count() > 1)
                        {
                            int iter = 0;
                            foreach (var ce2 in ces)
                            {
                                iter++;
                                if (iter == 1) continue;

                                /*jika payment lebih dari 1 type maka yang lain taruh sebagai detail tapi minus*/
                                var bank2 = bColl.Where(x => x.ChartOfAccountId == ce2.ChartOfAccountId).First();
                                var detail = new CashTransactionDetail
                                {
                                    TransactionId = header.TransactionId,
                                    ChartOfAccountId = ce2.ChartOfAccountId,
                                    Debit = ce2.Debit,
                                    Credit = ce2.Credit,
                                    Amount = CashIn ? (ce2.Credit - ce2.Debit) : (ce2.Debit - ce2.Credit),
                                    Description = ce2.Description,
                                    SubLedgerId = ce2.SubLedgerId,
                                    CostCenterId = 0
                                };

                                detail.AddNew();
                                detail.Save();
                            }
                        }
                        //var jtCash2 = jtCash.Where(y => /*y.DocumentNumber != ce.DocumentNumber &&*/ y.DocumentNumberSequenceNo != ce.DocumentNumberSequenceNo);
                        //foreach (var jt2 in jtCash2)
                        //{
                        //    /*jika payment lebih dari 1 type maka yang lain taruh sebagai detail tapi minus*/
                        //    var bank2 = bColl.Where(x => x.ChartOfAccountId == jt2.ChartOfAccountId).First();
                        //    var detail = new CashTransactionDetail
                        //    {
                        //        TransactionId = header.TransactionId,
                        //        ChartOfAccountId = jt2.ChartOfAccountId,
                        //        Debit = jt2.Debit,
                        //        Credit = jt2.Credit,
                        //        Amount = CashIn ? (jt2.Credit - jt2.Debit) : (jt2.Debit - jt2.Credit),
                        //        Description = jt2.Description,
                        //        SubLedgerId = jt2.SubLedgerId,
                        //        CostCenterId = 0
                        //    };

                        //    detail.AddNew();
                        //    detail.Save();
                        //}

                        decimal debet = 0, kredit = 0;
                        debet = jtCash.Sum(x => x.Credit ?? 0);
                        kredit = jtCash.Sum(x => x.Debit ?? 0);

                        var balance = debet - kredit;
                        // kredit taruh sebagai detail

                        var jDetail = CashIn ? jtColl.Where(x => x.Credit != 0) : jtColl.Where(x => x.Debit != 0);
                        foreach (var jd in jDetail)
                        {
                            var detailCB = new CashTransactionDetail
                            {
                                TransactionId = header.TransactionId,
                                ChartOfAccountId = jd.ChartOfAccountId,
                                Debit = jd.Debit,
                                Credit = jd.Credit,
                                Amount = CashIn ? (jd.Credit - jd.Debit) : (jd.Debit - jd.Credit),
                                Description = jd.Description,
                                SubLedgerId = 0,
                                CostCenterId = 0
                            };

                            detailCB.AddNew();
                            detailCB.Save();
                        }
                        transactionId = header.TransactionId;
                    }
                }
            }

            return 0;
        }


        public static int AddNewAutomaticCashTransactionPaymentReceiveFromJournal(
            TransPayment tp, TransPaymentItemCollection tpiColl,
            JournalTransactions jt,
            string UserID, out int? transactionId)
        {
            transactionId = 0;

            var jtColl = new JournalTransactionDetailsCollection();
            jtColl.Query.Where(jtColl.Query.JournalId.Equal(jt.JournalId));
            if (jtColl.LoadAll())
            {
                var bColl = new BankCollection();
                bColl.LoadAll();

                var jtCash = jtColl.Where(j => j.DocumentNumber == tp.PaymentNo && !string.IsNullOrEmpty(j.DocumentNumberSequenceNo) &&
                    (bColl.Select(b => b.ChartOfAccountId).Contains(j.ChartOfAccountId)));
                /* cash transaksi hanya untuk akun yang ada bank-nya */
                if (jtCash.Any())
                {
                    // prevent duplicate cash entry transaction when reprocessing journal is fired
                    var ceColl = new CashTransactionCollection();
                    ceColl.Query.Where(ceColl.Query.DocumentNumber == tp.PaymentNo && ceColl.Query.IsVoid == false);
                    ceColl.LoadAll();
                    if (ceColl.Count == 0)
                    {
                        // nominal paling gede jadi patokan
                        var ce = jtCash.OrderByDescending(x => x.Debit.Value).First();
                        Bank bank = bColl.Where(x => x.ChartOfAccountId == ce.ChartOfAccountId).First();

                        var tpi = tpiColl.Where(x => x.PaymentNo == ce.DocumentNumber && x.SequenceNo == ce.DocumentNumberSequenceNo).First();

                        var header = new CashTransaction
                        {
                            PostingId = 0,
                            BankId = bank.BankID,
                            ChartOfAccountId = bank.ChartOfAccountId,
                            TransactionDate = tp.PaymentDate.Value.Date,
                            TransactionType = "ADJST",
                            PaymentType = tpi.SRPaymentType,
                            PaymentMethod = tpi.SRPaymentMethod,
                            NormalBalance = ce.Credit.Value > 0 ? "K" : "D",
                            Module = "GL",
                            CurrencyCode = bank.CurrencyCode,
                            CurrencyRate = 1,
                            IsPosted = false,
                            IsCleared = false,
                            IsVoid = false,
                            ChequeNumber = string.Empty,
                            DocumentNumber = tp.PaymentNo,
                            Description = jt.Description,
                            JournalId = jt.JournalId,
                            DateCreated = DateTime.Now,
                            LastUpdateDateTime = DateTime.Now,
                            VoidDate = new DateTime(1900, 1, 1),
                            CreatedBy = UserID,
                            LastUpdateByUserID = UserID,
                            IsAutoCashEntry = true
                        };
                        header.AddNew();
                        header.Save();

                        // detail
                        var jtCash2 = jtCash.Where(y => /*y.DocumentNumber != ce.DocumentNumber &&*/ y.DocumentNumberSequenceNo != ce.DocumentNumberSequenceNo);
                        foreach (var jt2 in jtCash2)
                        {
                            /*jika payment lebih dari 1 type maka yang lain taruh sebagai detail tapi minus*/
                            var bank2 = bColl.Where(x => x.ChartOfAccountId == jt2.ChartOfAccountId).First();
                            var detail = new CashTransactionDetail
                            {
                                TransactionId = header.TransactionId,
                                ChartOfAccountId = jt2.ChartOfAccountId,
                                Debit = jt2.Debit,
                                Credit = jt2.Credit,
                                Amount = -(jt2.Debit - jt2.Credit),
                                Description = jt2.Description,
                                SubLedgerId = jt2.SubLedgerId,
                                CostCenterId = 0
                            };

                            detail.AddNew();
                            detail.Save();
                        }

                        decimal debet = 0, kredit = 0;
                        debet = jtCash.Sum(x => x.Credit ?? 0);
                        kredit = jtCash.Sum(x => x.Debit ?? 0);

                        var balance = debet - kredit;
                        // selisihnya hajar ke header pendapatan saja, 
                        // ambil coa pendapatan
                        var coa = new ChartOfAccountsCollection();
                        var coaPdptHeader = AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_CoaRevenueTopLevel);
                        if (!string.IsNullOrEmpty(coaPdptHeader))
                        {
                            coa.Query.Where(coa.Query.ChartOfAccountCode.Like(coaPdptHeader)).OrderBy(coa.Query.ChartOfAccountCode.Ascending);
                        }
                        else
                        {
                            coa.Query.Where(coa.Query.ChartOfAccountCode.Like("4%")).OrderBy(coa.Query.ChartOfAccountCode.Ascending);
                        }
                        if (coa.LoadAll())
                        {
                            var detailPendapatan = new CashTransactionDetail
                            {
                                TransactionId = header.TransactionId,
                                ChartOfAccountId = coa.First().ChartOfAccountId,
                                Debit = debet,
                                Credit = kredit,
                                Amount = (kredit - debet),
                                Description = "Pendapatan",
                                SubLedgerId = 0,
                                CostCenterId = 0
                            };

                            detailPendapatan.AddNew();
                            detailPendapatan.Save();
                        }

                        transactionId = header.TransactionId;
                    }
                }
            }

            return 0;
        }

        public static int AddNewAutomaticCashTransactionPaymentReturn(
            TransPayment tp, TransPaymentItemCollection tpiColl,
            int JournalId, string UserID)
        {
            // 1. cari sudah pernah ada cash transaction atau belum
            // jika sudah pernah ada maka skip insert cash entry
            var ctColl = new CashTransactionCollection();
            ctColl.Query.Where(ctColl.Query.DocumentNumber == tp.PaymentNo, ctColl.Query.IsVoid == 0);
            if (ctColl.LoadAll())
            {
                // sudah pernah ada, skip
                return ctColl.First().TransactionId.Value;
            }
            else
            {
                // cari cash transaction referensinya, kemudian do cash entry reverse
                var ctRefColl = new CashTransactionCollection();
                ctRefColl.Query.Where(ctRefColl.Query.DocumentNumber == tp.PaymentReferenceNo, ctRefColl.Query.IsVoid == 0)
                    .OrderBy(ctRefColl.Query.IsPosted.Descending);
                if (ctRefColl.LoadAll())
                {
                    return ReverseCashEntry(ctRefColl.First(), tp.PaymentDate.Value,
                        tp.PaymentNo, "Return", JournalId, UserID);
                }
            }

            return 0;
        }

        public static int AddNewAutomaticCashTransactionPaymentReceiveCashierCorrectionFromJournal(
            TransPaymentCorrection tpc, TransPaymentItemCorrectionCollection tpicColl,
            JournalTransactions jt,
            string UserID, out int? transactionId)
        {
            transactionId = 0;

            var jtColl = new JournalTransactionDetailsCollection();
            jtColl.Query.Where(jtColl.Query.JournalId.Equal(jt.JournalId));
            if (jtColl.LoadAll())
            {
                var bColl = new BankCollection();
                bColl.LoadAll();

                var jtCash = jtColl.Where(j => (tpicColl.Select(y => y.PaymentNo)).Contains(j.DocumentNumber) &&
                        (bColl.Select(b => b.ChartOfAccountId).Contains(j.ChartOfAccountId)));
                /* cash transaksi hanya untuk akun yang ada bank-nya */
                if (jtCash.Any())
                {
                    // prevent duplicate cash entry transaction when reprocessing journal is fired
                    var ceColl = new CashTransactionCollection();
                    ceColl.Query.Where(ceColl.Query.DocumentNumber == tpc.PaymentCorrectionNo && ceColl.Query.IsVoid == false);
                    ceColl.LoadAll();
                    if (ceColl.Count == 0)
                    {
                        foreach (var tpic in tpicColl)
                        {
                            // nominal paling gede jadi patokan
                            var ce = jtCash.OrderByDescending(x => x.Debit.Value).First();
                            Bank bank = bColl.Where(x => x.ChartOfAccountId == ce.ChartOfAccountId).First();

                            var tpiC = tpicColl.Where(x => x.PaymentNo == ce.DocumentNumber &&
                                x.SequenceNo == ce.DocumentNumberSequenceNo).First();

                            var header = new CashTransaction
                            {
                                PostingId = 0,
                                BankId = bank.BankID,
                                ChartOfAccountId = bank.ChartOfAccountId,
                                TransactionDate = tpc.PaymentCorrectionDate.Value.Date,
                                TransactionType = "ADJST",
                                PaymentType = "PaymentType-002",
                                PaymentMethod = "PaymentMethod-001",
                                NormalBalance = ce.Credit.Value > 0 ? "K" : "D",
                                Module = "GL",
                                CurrencyCode = bank.CurrencyCode,
                                CurrencyRate = 1,
                                IsPosted = false,
                                IsCleared = false,
                                IsVoid = false,
                                ChequeNumber = string.Empty,
                                DocumentNumber = tpc.PaymentCorrectionNo,
                                Description = jt.Description,
                                JournalId = jt.JournalId,
                                DateCreated = DateTime.Now,
                                LastUpdateDateTime = DateTime.Now,
                                VoidDate = new DateTime(1900, 1, 1),
                                CreatedBy = UserID,
                                LastUpdateByUserID = UserID,
                                IsAutoCashEntry = true
                            };
                            header.AddNew();
                            header.Save();

                            // detail
                            var jtCash2 = jtCash.Where(y => /*y.DocumentNumber != ce.DocumentNumber &&*/ y.DocumentNumberSequenceNo != ce.DocumentNumberSequenceNo);
                            foreach (var jt2 in jtCash2)
                            {
                                /*jika payment lebih dari 1 type maka yang lain taruh sebagai detail tapi minus*/
                                var bank2 = bColl.Where(x => x.ChartOfAccountId == jt2.ChartOfAccountId).First();
                                var detail = new CashTransactionDetail
                                {
                                    TransactionId = header.TransactionId,
                                    ChartOfAccountId = jt2.ChartOfAccountId,
                                    Debit = jt2.Debit,
                                    Credit = jt2.Credit,
                                    Amount = -(jt2.Debit - jt2.Credit),
                                    Description = jt2.Description,
                                    SubLedgerId = jt2.SubLedgerId,
                                    CostCenterId = 0
                                };

                                detail.AddNew();
                                detail.Save();
                            }

                            decimal debet = 0, kredit = 0;
                            debet = jtCash.Sum(x => x.Debit ?? 0);
                            kredit = jtCash.Sum(x => x.Credit ?? 0);

                            var balance = debet - kredit;
                            // selisihnya hajar ke header pendapatan saja, 
                            // ambil coa pendapatan
                            var coa = new ChartOfAccountsCollection();
                            coa.Query.Where(coa.Query.ChartOfAccountCode.Like("4%")).OrderBy(coa.Query.ChartOfAccountCode.Ascending);
                            if (coa.LoadAll())
                            {
                                var detailPendapatan = new CashTransactionDetail
                                {
                                    TransactionId = header.TransactionId,
                                    ChartOfAccountId = coa.First().ChartOfAccountId,
                                    Debit = debet,
                                    Credit = kredit,
                                    Amount = (debet - kredit),
                                    Description = "Pendapatan",
                                    SubLedgerId = 0,
                                    CostCenterId = 0
                                };

                                detailPendapatan.AddNew();
                                detailPendapatan.Save();
                            }

                            transactionId = header.TransactionId;
                        }
                    }
                }
            }

            return 0;
        }

        public static int AddNewAutomaticCashTransactionAP(string bankId, int? chartOfAccountId, DateTime transactionDate, string moduleName, string transactionType,
           string paymentType, string paymentMethod, string normalBalance, string currencyCode, decimal currencyRate, string chequeNumber, string documentNumber, string description,
           int journalId, string createdBy,
           int chartOfAccountIdAP, int subLedgerIdAP, out int? transactionId, int chartOfAccountIdAPNM, decimal HutangM, decimal HutangNm,
            int loss, int profit, decimal lossProfit)
        {
            var header = new CashTransaction
            {
                PostingId = 0,
                BankId = bankId,
                ChartOfAccountId = chartOfAccountId,
                TransactionDate = transactionDate,
                TransactionType = transactionType,
                PaymentType = paymentType,
                PaymentMethod = paymentMethod,
                NormalBalance = normalBalance,
                Module = moduleName,
                CurrencyCode = currencyCode,
                CurrencyRate = currencyRate,
                IsPosted = false,
                IsCleared = false,
                IsVoid = false,
                ChequeNumber = chequeNumber,
                DocumentNumber = documentNumber,
                Description = description,
                JournalId = journalId,
                DateCreated = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                VoidDate = new DateTime(1900, 1, 1),
                CreatedBy = createdBy,
                LastUpdateByUserID = createdBy,
                IsAutoCashEntry = true
            };
            header.AddNew();
            header.Save();

            decimal debitAmount = 0;
            decimal creditAmount = 0;
            if (HutangM != 0) {
                if (normalBalance == "K")
                    debitAmount = currencyRate * HutangM;
                else
                    creditAmount = currencyRate * HutangM;

                var detail = new CashTransactionDetail
                {
                    TransactionId = header.TransactionId,
                    ChartOfAccountId = chartOfAccountIdAP,
                    Debit = debitAmount,
                    Credit = creditAmount,
                    Amount = currencyRate * HutangM,
                    Description = description,
                    SubLedgerId = subLedgerIdAP,
                    CostCenterId = 0
                };
                detail.AddNew();
                detail.Save();
            }
            if (HutangNm != 0)
            {
                if (normalBalance == "K")
                    debitAmount = currencyRate * HutangNm;
                else
                    creditAmount = currencyRate * HutangNm;

                var detail = new CashTransactionDetail
                {
                    TransactionId = header.TransactionId,
                    ChartOfAccountId = chartOfAccountIdAPNM,
                    Debit = debitAmount,
                    Credit = creditAmount,
                    Amount = currencyRate * HutangNm,
                    Description = description,
                    SubLedgerId = subLedgerIdAP,
                    CostCenterId = 0
                };
                detail.AddNew();
                detail.Save();
            }

            if (lossProfit != 0)
            {
                int coa = 0;
                if (lossProfit > 0) coa = loss; else coa = profit;

                if (coa != 0)
                {
                    var detailLostProfit = new CashTransactionDetail
                    {

                        TransactionId = header.TransactionId,
                        ChartOfAccountId = coa,
                        Debit = lossProfit * currencyRate,
                        Credit = 0,
                        Amount = lossProfit,
                        Description = description,
                        SubLedgerId = 0,
                        CostCenterId = 0
                    };
                    detailLostProfit.AddNew();
                    detailLostProfit.Save();
                }
            }

            transactionId = header.TransactionId;

            return 0;
        }

        public static int AddNewAutomaticCashTransactionAssetAuctionFromJournal(
            AssetStatusHistory sh, JournalTransactions jt, int coa_fa,
            string UserID, out int? transactionId)
        {
            transactionId = 0;
            var documentNo = "FAA-" + sh.SeqNo.ToString();

            var jtColl = new JournalTransactionDetailsCollection();
            jtColl.Query.Where(jtColl.Query.JournalId.Equal(jt.JournalId));
            if (jtColl.LoadAll())
            {
                var bColl = new BankCollection();
                bColl.LoadAll();

                var jtCash = jtColl.Where(j => j.DocumentNumber == documentNo && !string.IsNullOrEmpty(j.DocumentNumberSequenceNo) &&
                    (bColl.Select(b => b.ChartOfAccountId).Contains(j.ChartOfAccountId)));
                /* cash transaksi hanya untuk akun yang ada bank-nya */
                if (jtCash.Any())
                {
                    // prevent duplicate cash entry transaction when reprocessing journal is fired
                    var ceColl = new CashTransactionCollection();
                    ceColl.Query.Where(ceColl.Query.DocumentNumber == documentNo && ceColl.Query.IsVoid == false);
                    ceColl.LoadAll();
                    if (ceColl.Count == 0)
                    {
                        // nominal paling gede jadi patokan
                        var ce = jtCash.OrderByDescending(x => x.Debit.Value).First();
                        Bank bank = bColl.Where(x => x.ChartOfAccountId == ce.ChartOfAccountId).First();

                        var header = new CashTransaction
                        {
                            PostingId = 0,
                            BankId = bank.BankID,
                            ChartOfAccountId = bank.ChartOfAccountId,
                            TransactionDate = sh.TransactionDate.Value.Date,
                            TransactionType = "ADJST",
                            PaymentType = sh.SRPaymentType,
                            PaymentMethod = sh.SRPaymentMethod,
                            NormalBalance = ce.Credit.Value > 0 ? "K" : "D",
                            Module = "GL",
                            CurrencyCode = bank.CurrencyCode,
                            CurrencyRate = 1,
                            IsPosted = false,
                            IsCleared = false,
                            IsVoid = false,
                            ChequeNumber = string.Empty,
                            DocumentNumber = documentNo,
                            Description = jt.Description,
                            JournalId = jt.JournalId,
                            DateCreated = DateTime.Now,
                            LastUpdateDateTime = DateTime.Now,
                            VoidDate = new DateTime(1900, 1, 1),
                            CreatedBy = UserID,
                            LastUpdateByUserID = UserID,
                            IsAutoCashEntry = true
                        };
                        header.AddNew();
                        header.Save();

                        // detail
                        var detail = new CashTransactionDetail
                        {
                            TransactionId = header.TransactionId,
                            ChartOfAccountId = coa_fa,
                            Debit = ce.Credit,
                            Credit = ce.Debit,
                            Amount = (ce.Debit - ce.Credit),
                            Description = jt.Description,
                            SubLedgerId = 0,
                            CostCenterId = 0
                        };

                        detail.AddNew();
                        detail.Save();

                        transactionId = header.TransactionId;
                    }
                }
            }

            return 0;
        }

        public static int AddNewAutomaticCashTransactionFromJournal(JournalTransactions jt,
            string moduleName, string transactionType, string PaymentType, string PaymentMethod, DateTime PaymentDate,
            string UserID, out int? transactionId)
        {
            transactionId = 0;

            var jtdColl = new JournalTransactionDetailsCollection();
            jtdColl.Query.Where(jtdColl.Query.JournalId.Equal(jt.JournalId));
            if (jtdColl.LoadAll())
            {
                var bColl = new BankCollection();
                bColl.LoadAll();

                var jtCash = jtdColl.Where(j => j.DocumentNumber == jt.RefferenceNumber && //!string.IsNullOrEmpty(j.DocumentNumberSequenceNo) &&
                    (bColl.Select(b => b.ChartOfAccountId).Contains(j.ChartOfAccountId)));
                /* cash transaksi hanya untuk akun yang ada bank-nya */
                if (jtCash.Any())
                {
                    // prevent duplicate cash entry transaction when reprocessing journal is fired
                    var ceColl = new CashTransactionCollection();
                    ceColl.Query.Where(ceColl.Query.DocumentNumber == jt.RefferenceNumber && ceColl.Query.IsVoid == false);
                    ceColl.LoadAll();
                    if (ceColl.Count == 0)
                    {
                        // nominal paling gede jadi patokan
                        var ce = jtCash.OrderByDescending(x => x.Debit.Value).First();
                        Bank bank = bColl.Where(x => x.ChartOfAccountId == ce.ChartOfAccountId).First();

                        //var tpi = tpiColl.Where(x => x.PaymentNo == ce.DocumentNumber && x.SequenceNo == ce.DocumentNumberSequenceNo).First();
                        var header = new CashTransaction
                        {
                            PostingId = 0,
                            BankId = bank.BankID,
                            ChartOfAccountId = bank.ChartOfAccountId,
                            TransactionDate = PaymentDate,
                            TransactionType = transactionType, //"ADJST",
                            PaymentType = PaymentType,
                            PaymentMethod = PaymentMethod,
                            NormalBalance = ce.Credit.Value > 0 ? "K" : "D",
                            Module = moduleName, //"GL",
                            CurrencyCode = bank.CurrencyCode,
                            CurrencyRate = 1,
                            IsPosted = false,
                            IsCleared = false,
                            IsVoid = false,
                            ChequeNumber = string.Empty,
                            DocumentNumber = jt.RefferenceNumber,
                            Description = jt.Description,
                            JournalId = jt.JournalId,
                            DateCreated = DateTime.Now,
                            LastUpdateDateTime = DateTime.Now,
                            VoidDate = new DateTime(1900, 1, 1),
                            CreatedBy = UserID,
                            LastUpdateByUserID = UserID,
                            IsAutoCashEntry = true
                        };
                        header.AddNew();
                        header.Save();

                        // detail
                        var jtds = jtdColl.Where(y => y.DetailId != ce.DetailId);
                        foreach (var jtd in jtds)
                        {
                            /*jika payment lebih dari 1 type maka yang lain taruh sebagai detail tapi minus*/
                            //var bank2 = bColl.Where(x => x.ChartOfAccountId == jt2.ChartOfAccountId).First();
                            var detail = new CashTransactionDetail
                            {
                                TransactionId = header.TransactionId,
                                ChartOfAccountId = jtd.ChartOfAccountId,
                                Debit = jtd.Debit,
                                Credit = jtd.Credit,
                                Amount = -(jtd.Debit - jtd.Credit),
                                Description = jtd.Description,
                                SubLedgerId = jtd.SubLedgerId,
                                CostCenterId = 0
                            };

                            detail.AddNew();
                            detail.Save();
                        }

                        //decimal debet = 0, kredit = 0;
                        //debet = jtCash.Sum(x => x.Credit ?? 0);
                        //kredit = jtCash.Sum(x => x.Debit ?? 0);

                        //var balance = debet - kredit;
                        //// selisihnya hajar ke header pendapatan saja, 
                        //// ambil coa pendapatan
                        //var coa = new ChartOfAccountsCollection();
                        //var coaPdptHeader = AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_CoaRevenueTopLevel);
                        //if (!string.IsNullOrEmpty(coaPdptHeader))
                        //{
                        //    coa.Query.Where(coa.Query.ChartOfAccountCode.Like(coaPdptHeader)).OrderBy(coa.Query.ChartOfAccountCode.Ascending);
                        //}
                        //else
                        //{
                        //    coa.Query.Where(coa.Query.ChartOfAccountCode.Like("4%")).OrderBy(coa.Query.ChartOfAccountCode.Ascending);
                        //}
                        //if (coa.LoadAll())
                        //{
                        //    var detailPendapatan = new CashTransactionDetail
                        //    {
                        //        TransactionId = header.TransactionId,
                        //        ChartOfAccountId = coa.First().ChartOfAccountId,
                        //        Debit = debet,
                        //        Credit = kredit,
                        //        Amount = (kredit - debet),
                        //        Description = "Pendapatan",
                        //        SubLedgerId = 0,
                        //        CostCenterId = 0
                        //    };

                        //    detailPendapatan.AddNew();
                        //    detailPendapatan.Save();
                        //}

                        transactionId = header.TransactionId;
                    }
                }
            }

            return 0;
        }

        public static DataTable CashFlowIndirectReportDataSource(string StoreProcedureName, string month, string year,
            out decimal beginingBalance, out decimal endingBalance)
        {
            beginingBalance = 0;
            endingBalance = 0;

            var prms = new esParameters
                           {
                               {"pSinglePostingPeriode_Month", month, esParameterDirection.Input, DbType.String, 2},
                               {"pSinglePostingPeriode_Year", year, esParameterDirection.Input, DbType.String, 4},
                               {"pBeginingBalance", beginingBalance, esParameterDirection.Output, DbType.Decimal, 18},
                               {"pEndingBalance", endingBalance, esParameterDirection.Output, DbType.Decimal, 18}
                           };


            prms["pBeginingBalance"].Precision = 18;
            prms["pBeginingBalance"].Scale = 2;

            prms["pEndingBalance"].Precision = 18;
            prms["pEndingBalance"].Scale = 2;

            var util = new esUtility();
            var ret = util.FillDataTable(esQueryType.StoredProcedure, StoreProcedureName, prms);

            beginingBalance = System.Convert.ToDecimal(prms["pBeginingBalance"].Value);
            endingBalance = System.Convert.ToDecimal(prms["pEndingBalance"].Value);

            return ret;
        }

        public void Reconcile(bool Reconcile, string ReconcileBy)
        {
            this.IsCleared = Reconcile;
            if (Reconcile)
            {
                this.ClearedDateTime = DateTime.Now;
                this.CreatedBy = ReconcileBy;
            }
        }

        private static int ReverseCashEntry(CashTransaction ctRef, DateTime TransactionDate,
            string DocumentNumber, string DescriptionPrefix, int JournalId,
            string UserID)
        {

            var ctdRefColl = new CashTransactionDetailCollection();
            ctdRefColl.Query.Where(ctdRefColl.Query.TransactionId == ctRef.TransactionId);
            ctdRefColl.LoadAll();

            var header = new CashTransaction
            {
                PostingId = 0,
                BankId = ctRef.BankId,
                ChartOfAccountId = ctRef.ChartOfAccountId,
                TransactionDate = TransactionDate.Date,
                TransactionType = "ADJST",
                PaymentType = ctRef.PaymentType,
                PaymentMethod = ctRef.PaymentMethod,
                NormalBalance = ctRef.NormalBalance == "K" ? "D" : "K",
                Module = ctRef.Module,
                CurrencyCode = ctRef.CurrencyCode,
                CurrencyRate = ctRef.CurrencyRate,
                IsPosted = false,
                IsCleared = false,
                IsVoid = false,
                ChequeNumber = ctRef.ChequeNumber,
                DocumentNumber = DocumentNumber,
                Description = DescriptionPrefix + " " + ctRef.Description,
                JournalId = JournalId,
                DateCreated = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                VoidDate = new DateTime(1900, 1, 1),
                CreatedBy = UserID,
                LastUpdateByUserID = UserID,
                IsAutoCashEntry = true
            };
            header.AddNew();
            header.Save();

            // detail
            var detailColl = new CashTransactionDetailCollection();
            foreach (var ctdRef in ctdRefColl)
            {
                var detail = detailColl.AddNew();
                detail.TransactionId = header.TransactionId;
                detail.ChartOfAccountId = ctdRef.ChartOfAccountId;
                detail.Debit = ctdRef.Credit;
                detail.Credit = ctdRef.Debit;
                detail.Amount = (header.NormalBalance == "D") ? (detail.Credit - detail.Debit) : (detail.Debit - detail.Credit);
                detail.Description = DescriptionPrefix + " " + ctdRef.Description;
                detail.SubLedgerId = ctdRef.SubLedgerId;
                detail.CostCenterId = ctdRef.CostCenterId;
            }
            detailColl.Save();

            return header.TransactionId.Value;
        }
    }
}

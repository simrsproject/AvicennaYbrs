using System;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class BkuJournalTransactions : esBkuJournalTransactions
    {
        public static int Rejournal(int BkuJournalID, string UserID) {
            var bku = new BkuJournalTransactions();
            if (bku.LoadByPrimaryKey(BkuJournalID)) {
                return AddBkuJournalByJournalTransactions(bku.JournalIdToCopy ?? 0, UserID);
            }
            return 0;
        }
        public static int AddBkuJournalByCashTransactions(int TransactionID, string UserID)
        {
            var ct = new CashTransaction();
            ct.Query.Where(ct.Query.TransactionId == TransactionID, ct.Query.IsPosted == true, ct.Query.IsVoid == false);
            ct.Query.es.Top = 1;
            if (!ct.Query.Load()) return 0;

            return AddBkuJournalByJournalTransactions(ct.JournalId ?? 0, UserID);
        }
        public static int AddBkuJournalByJournalTransactions(int JournalID, string UserID) {
            if (!AppParameter.IsYes(AppParameter.ParameterItem.IsUsingBKUModule)) return 0;

            var jt = new JournalTransactions();
            if (!jt.LoadByPrimaryKey(JournalID)) return 0;

            var ct = new CashTransaction();
            ct.Query.Where(ct.Query.JournalId == JournalID, ct.Query.IsPosted == true, ct.Query.IsVoid == false);
            ct.Query.es.Top = 1;
            if (!ct.Query.Load()) return 0;

            var bank = new Bank();
            if (!bank.LoadByPrimaryKey(ct.BankId)) return 0;
            if (!(bank.IsBKU ?? false)) return 0;

            var bkudColl = new BkuJournalTransactionDetailsCollection();
            var bku = new BkuJournalTransactions();
            bku.Query.Where(bku.query.JournalIdToCopy == JournalID);
            bku.Query.es.Top = 1;
            if (!bku.Query.Load()) {
                bku.AddNew();
                bku.CashTransactionId = ct.TransactionId;
                bku.JournalIdToCopy = JournalID;
                bku.CreateByUserID = UserID;
                bku.CreateDateTime = DateTime.Now;
                bku.LastUpdateByUserID = UserID;
                bku.LastUpdateDateTime = DateTime.Now;
            }
            else {
                bkudColl.Query.Where(bkudColl.Query.BkuJournalId == bku.BkuJournalId);
                bkudColl.LoadAll();
                bkudColl.MarkAllAsDeleted();
            }
            bku.Save();
            var bkuMsg = new BkuJournalMessagesCollection();
            bkuMsg.Query.Where(bkuMsg.Query.BkuJournalId == bku.BkuJournalId);
            bkuMsg.LoadAll();
            bkuMsg.MarkAllAsDeleted();

            // cari jurnal mana yang akan dicopy
            var coaColl = new ChartOfAccountsCollection();
            coaColl.Query.Where(coaColl.Query.IsDetail == true, coaColl.Query.IsActive == true);
            coaColl.LoadAll();
            var bankBkuColl = new BankCollection();
            bankBkuColl.Query.Where(bankBkuColl.Query.IsActive == true, bankBkuColl.Query.IsBKU == true);
            bankBkuColl.LoadAll();

            CopyFromJournalTraceBack(0, bku.BkuJournalId.Value, jt, bkudColl, coaColl, bankBkuColl, bkuMsg, UserID);

            if (bku.es.IsModified) {
                bku.LastUpdateByUserID = UserID;
                bku.LastUpdateDateTime = DateTime.Now;
            }

            bku.Save();
            // just in case it has no bkujournalid
            foreach (var bkud in bkudColl.Where(b => !b.BkuJournalId.HasValue)) {
                bkud.BkuJournalId = bku.BkuJournalId;
            }
            bkudColl.Save();
            bkuMsg.Save();

            return bku.BkuJournalId ?? 0;
        }

        private static void CopyFromJournalTraceBack(int CoaIDRef, int bkuJournalId, JournalTransactions jt, BkuJournalTransactionDetailsCollection bkudColl, 
            ChartOfAccountsCollection coaColl, BankCollection bankBkuColl, BkuJournalMessagesCollection bkuMsg, string UserID)
        {
            var jtdColl = new JournalTransactionDetailsCollection();
            jtdColl.Query.Where(jtdColl.Query.JournalId == jt.JournalId);
            if (jtdColl.LoadAll())
            {
                foreach (var jtd in jtdColl) {
                    if (bankBkuColl.Where(b => b.ChartOfAccountId == jtd.ChartOfAccountId).Any())
                    {
                        AddByJournalDetail(bkuJournalId, bkudColl, jtd, coaColl, bkuMsg, UserID, true);
                    }
                    else {
                        if (jtd.DocumentNumber == jt.RefferenceNumber && CoaIDRef == jtd.ChartOfAccountId.Value)
                        { /* cek berdasarkan COAID harusnya ini */
                            // bypass tidak perlu dijurnal
                        }
                        else if (CoaIDRef == jtd.ChartOfAccountId.Value) {
                            // bypass tidak perlu dijurnal
                        }
                        else if (string.IsNullOrEmpty(jtd.DocumentNumber))
                        {
                            AddByJournalDetail(bkuJournalId, bkudColl, jtd, coaColl, bkuMsg, UserID, false);
                        }
                        else
                        {
                            var pjt = new JournalTransactions();
                            pjt.Query.Where(pjt.Query.RefferenceNumber == jtd.DocumentNumber, pjt.Query.IsVoid == false, pjt.Query.JournalIdRefference.IsNull());
                            pjt.Query.es.Top = 1;
                            if (!pjt.Query.Load())
                            {
                                AddByJournalDetail(bkuJournalId, bkudColl, jtd, coaColl, bkuMsg, UserID, false);
                            }
                            else
                            {
                                if (pjt.JournalId == jt.JournalId)
                                {
                                    AddByJournalDetail(bkuJournalId, bkudColl, jtd, coaColl, bkuMsg, UserID, false);
                                }
                                else
                                {
                                    CopyFromJournalTraceBack(jtd.ChartOfAccountId ?? 0, bkuJournalId, pjt, bkudColl, coaColl, bankBkuColl, bkuMsg, UserID);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void AddByJournalDetail(int bkuJournalId, BkuJournalTransactionDetailsCollection bkudColl, JournalTransactionDetails jtd,
            ChartOfAccountsCollection coaColl, BkuJournalMessagesCollection bkuMsg, string UserID, bool UseCoaIfNoBKU)
        {
            var coa = coaColl.Where(c => c.ChartOfAccountId == jtd.ChartOfAccountId).FirstOrDefault();
            if (coa == null) return;
            if ((coa.BkuAccountID ?? 0) == 0 && (!UseCoaIfNoBKU))
            {
                var msg = bkuMsg.AddNew();
                msg.BkuJournalId = bkuJournalId;
                msg.DetailJournalId = jtd.DetailId;
                msg.Message = HelperMirror.CutText(string.Format("COA {0} - {1} has no equivalent COA of BKU", 
                    coa.ChartOfAccountCode.Trim(), coa.ChartOfAccountName.Trim()), 500);
                msg.CreateByUserID = UserID;
                msg.CreateDateTime = DateTime.Now;
            }
            else
            {
                var bkud = bkudColl.AddNew();
                bkud.BkuJournalId = bkuJournalId;
                bkud.JournalDetailIdToCopy = jtd.DetailId;
                bkud.ChartOfAccountId = (coa.BkuAccountID ?? 0) == 0 ? coa.ChartOfAccountId : coa.BkuAccountID;
                bkud.Debit = jtd.Debit;
                bkud.Credit = jtd.Credit;
                bkud.Description = jtd.Description;
                bkud.SubLedgerId = jtd.SubLedgerId;
                bkud.CreateByUserID = UserID;
                bkud.CreateDateTime = DateTime.Now;
                bkud.LastUpdateByUserID = UserID;
                bkud.LastUpdateDateTime = DateTime.Now;
            }
        }
    }
}

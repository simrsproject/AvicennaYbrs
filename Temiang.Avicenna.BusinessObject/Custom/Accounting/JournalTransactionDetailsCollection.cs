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
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
	public partial class JournalTransactionDetailsCollection : esJournalTransactionDetailsCollection
	{
        public override void Save()
        {
            foreach (var jt in this) {
                jt.CutJournalDescription();
            }

            base.Save();
        }

        public DataTable JournalNonBalance()
        {
            esParameters par = new esParameters();

//            string commandText = @"SELECT jtd.JournalId 
//                                   FROM JournalTransactionDetails AS jtd 
//                                   INNER JOIN JournalTransactions AS jt ON jt.JournalId = jtd.JournalId AND jt.IsPosted = 0
//                                   GROUP BY jtd.JournalId
//                                   HAVING SUM(jtd.Debit) - SUM(jtd.Credit) >= 1";

            string commandText = @" SELECT *
                                    FROM JournalTransactions AS jt 
                                    WHERE MONTH(jt.TransactionDate) IN (SELECT CAST(ps.[Month] AS INT) 
                                                                          FROM PostingStatus AS ps 
                                                                          WHERE ps.IsEnabled = 0)
	                                    AND YEAR(jt.TransactionDate) IN (SELECT CAST(ps.[Year] AS INT) 
                                                                         FROM PostingStatus AS ps 
                                                                         WHERE ps.IsEnabled = 0)
                                    ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public bool LoadByRefferenceNo(string RefferenceNo)
        {
            var jt = new JournalTransactionsQuery("jt");
            var jtd = new JournalTransactionDetailsQuery("jtd");
            var coa = new ChartOfAccountsQuery("coa");
            var sl = new SubLedgersQuery("sl");

            jtd.InnerJoin(jt).On(jtd.JournalId == jt.JournalId)
                .InnerJoin(coa).On(jtd.ChartOfAccountId == coa.ChartOfAccountId)
                .LeftJoin(sl).On(jtd.SubLedgerId == sl.SubLedgerId)
                .Where(jt.RefferenceNumber == RefferenceNo)
                .Select(
                    jtd,
                    coa.ChartOfAccountCode.As("ChartOfAccountCode"),
                    coa.ChartOfAccountName.As("ChartOfAccountName"),
                    sl.SubLedgerName.As("SubLedgerName"),
                    sl.Description.As("SubLedger_Description"),
                    jt.JournalType.As("JournalType"),
                    jt.Description.As("HeaderDescription"),
                    jtd.RegistrationNo.As("Registration No")
                    //"<jt.JournalType + ' - ' + jt.Description JournalGrouping>"
                );
            return this.Load(jtd);
        }

        public bool LoadByRefNoForAccrual(List<string> transNos) {
            var jadq = new JournalTransactionDetailsQuery("jad");
            var jaq = new JournalTransactionsQuery("ja");
            jadq.InnerJoin(jaq).On(jadq.JournalId == jaq.JournalId)
                .Where(jaq.RefferenceNumber.In(transNos), jaq.IsVoid == false)
                .Select(jadq);
            return this.Load(jadq);
        }
        public bool LoadByRefNoBillRecalForAccrual(string[] regNos, List<string> transNos, DateTime date)
        {
            var jbdq = new JournalTransactionDetailsQuery("jbd");
            var jbq = new JournalTransactionsQuery("jb");
            jbdq.InnerJoin(jbq).On(jbdq.JournalId == jbq.JournalId)
                .Where(jbq.RefferenceNumber.In(regNos), jbq.TransactionDate < date, jbdq.DocumentNumber.In(transNos), jbq.IsVoid == false)
                .Select(jbdq);
            return this.Load(jbdq);
        }

        public void MergeJournalRevenue(JournalTransactionDetailsCollection jadColl, JournalTransactions jt, int dAccPatRec) {
            foreach (var jad in jadColl)
            {
                JournalTransactionDetails jd = null;
                if (string.IsNullOrEmpty(jad.TariffComponentID))
                {
                    // data lama gak ada data tariff componentid, atau jurnal obat alkes gak ada tariff componentid gpp masuk sini
                    jd = this.Where(j => j.ChartOfAccountId == jad.ChartOfAccountId &&
                        j.SubLedgerId == jad.SubLedgerId &&
                        j.DocumentNumber == jad.DocumentNumber &&
                        j.DocumentNumberSequenceNo == jad.DocumentNumberSequenceNo &&
                        j.ItemID == jad.ItemID // <-- diperlukan untuk item consumption
                    ).FirstOrDefault();
                }
                else
                {
                    jd = this.Where(j => j.ChartOfAccountId == jad.ChartOfAccountId &&
                            j.SubLedgerId == jad.SubLedgerId &&
                            j.DocumentNumber == jad.DocumentNumber &&
                            j.DocumentNumberSequenceNo == jad.DocumentNumberSequenceNo &&
                            j.TariffComponentID == jad.TariffComponentID &&
                            j.ItemID == jad.ItemID // <-- diperlukan untuk item consumption
                        ).FirstOrDefault();
                }

                if (jd == null)
                {
                    jadColl.DetachEntity(jad);
                    jad.AcceptChanges();
                    jad.MarkAllColumnsAsDirty(DataRowState.Added);
                    jad.JournalId = jt.JournalId;
                    this.AttachEntity(jad);
                }
                else
                {
                    jd.Debit += jad.Debit.Value;
                    jd.Credit += jad.Credit.Value;
                }
            }

            foreach (var jd in this)
            {
                var amt = jd.Debit.Value - jd.Credit.Value;
                jd.Debit = amt > 0 ? amt : 0;
                jd.Credit = amt < 0 ? (amt * -1) : 0;

                if (jd.ChartOfAccountId.Value == dAccPatRec && jd.SubLedgerId == 0)
                {
                    this.DetachEntity(jd);
                }
                else if (jd.Debit.Value + jd.Credit.Value == 0)
                {
                    this.DetachEntity(jd);
                }
            }
        }

        public bool DeleteByJournalId(int JournalID) {
            esParameters pars = new esParameters();
            pars.Add("JournalId", JournalID);
            es.Connection.CommandTimeout = 800;
            return ExecuteNonQuery(esQueryType.Text, "delete from JournalTransactionDetails where JournalId = @JournalId", pars) > 0;
        }
    }
}

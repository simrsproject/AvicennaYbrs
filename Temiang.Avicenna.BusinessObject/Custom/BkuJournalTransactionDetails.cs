using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class BkuJournalTransactionDetailsCollection : esBkuJournalTransactionDetailsCollection
    {
        
    }

    public partial class BkuJournalTransactionDetails : esBkuJournalTransactionDetails
    {
        public static void GetTotal(int BkuJournalId, out double debit, out double credit)
        {
            debit = 0;
            credit = 0;

            BkuJournalTransactionDetailsQuery j = new BkuJournalTransactionDetailsQuery("j");
            j.Select(j.Debit.Sum().As("Total_Debit"), j.Credit.Sum().As("Total_Credit"));
            j.Where(j.BkuJournalId == BkuJournalId);
            j.es.WithNoLock = true;

            BkuJournalTransactionDetails entity = new BkuJournalTransactionDetails();
            if (entity.Load(j))
            {
                debit = Convert.ToDouble(entity.GetColumn("Total_Debit") == DBNull.Value ? 0 : entity.GetColumn("Total_Debit"));
                credit = Convert.ToDouble(entity.GetColumn("Total_Credit") == DBNull.Value ? 0 : entity.GetColumn("Total_Credit"));
            }
        }
    }
}

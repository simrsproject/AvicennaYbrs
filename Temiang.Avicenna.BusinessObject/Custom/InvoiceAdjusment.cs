using System;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class InvoiceAdjusment
    {
        public string AddReason
        {
            get { return GetColumn("refToAppStandardReference_AddReason").ToString(); }
            set { SetColumn("refToAppStandardReference_AddReason", value); }
        }

        public string PaymentApproved(string TransactionNo, string userID, string type)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                InvoiceAdjusment entity = new InvoiceAdjusment();
                if (entity.LoadByPrimaryKey(TransactionNo))
                {
                    entity.IsApproved = true;
                    entity.LastUpdateByUserID = userID;
                    entity.LastUpdateDateTime = DateTime.Now;

                    var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.TransactionDate.Value);
                    if (isClosingPeriod)
                        return "Financial statements for period: " +
                               string.Format("{0:MMMM-yyyy}", entity.TransactionDate.Value) +
                               " have been closed. Please contact the authorities.";

                    /* Automatic Journal Testing Start */
                    if (type == "AR")
                    {
                        int? journalId = JournalTransactions.AddNewARJournal(entity, userID);
                    }
                    else
                    {
                         int? journalId = JournalTransactions.AddNewAPJournal(entity, userID, 0);
                    }
               
                    /* Automatic Journal Testing End */

                    entity.Save();                  

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
                else
                {
                    return "NotExist";
                }
            }
            return string.Empty;
        }
    }
}

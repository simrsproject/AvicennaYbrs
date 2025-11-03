using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Common
{
    public partial class Helper
    {
        public class ProcessApproval
        {
            public static void SetApproval(string journalId, bool isPosted)
            {
                var entity = new JournalTransactions();
                entity.LoadByPrimaryKey(Convert.ToInt32(journalId));

                entity.IsPosted = isPosted;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;

                using (var trans = new esTransactionScope())
                {
                    entity.Save();

                    trans.Complete();
                }
            }
        }
    }
}

using System;
//using System.Data;
using Temiang.Dal.Interfaces;
//using Temiang.Dal.DynamicQuery;
//using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransPaymentReceipt
    {
        #region void unvoid
        private static void VoidProcess(string paymentReceiptNo, string userID, bool isVoid)
        {
            TransPaymentReceipt entity = new TransPaymentReceipt();
            if (entity.LoadByPrimaryKey(paymentReceiptNo))
            {
                if (entity.IsVoid == true && isVoid) return;
                if (entity.IsVoid == false && !isVoid) return;

                entity.IsVoid = isVoid;
                entity.IsApproved = !isVoid;
                entity.str.VoidDate = isVoid ? DateTime.Now.ToString() : "";
                entity.str.VoidByUserID = isVoid ? userID : "";
                
                using (esTransactionScope trans = new esTransactionScope())
                {
                    entity.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        public void Void(string paymentReceiptNo, string userID)
        {
            VoidProcess(paymentReceiptNo, userID, true);
        }
        public void UnVoid(string paymentReceiptNo, string userID)
        {
            VoidProcess(paymentReceiptNo, userID, false);
        }
        #endregion

        #region Approve UnApprove
        private static string ApprovProcess(string paymentReceiptNo, string userID, bool isApproval)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                TransPaymentReceipt entity = new TransPaymentReceipt();
                if (entity.LoadByPrimaryKey(paymentReceiptNo))
                {
                    //Check status record
                    if (isApproval)
                    {
                        if (entity.IsApproved != null && entity.IsApproved.Value)
                            return "Approved";

                        if (entity.IsVoid != null && entity.IsVoid.Value)
                            return "Voided";
                    }
                    else
                    {
                        if (entity.IsApproved != null && !entity.IsApproved.Value)
                            return "UnApproved";
                    }

                    entity.IsApproved = isApproval;
                    entity.str.ApprovedDate = isApproval ? DateTime.Now.ToString() : "";
                    entity.str.ApprovedByUserID = isApproval ? userID : "";
                    

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

        public string Approv(string paymentReceiptNo, string userID)
        {
            return ApprovProcess(paymentReceiptNo, userID, true);
        }

        public string UnApprov(string paymentReceiptNo, string userID)
        {
            return ApprovProcess(paymentReceiptNo, userID, false);
        }
        #endregion
    }
}

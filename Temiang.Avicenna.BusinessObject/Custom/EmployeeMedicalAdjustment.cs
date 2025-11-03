using System;
using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeMedicalAdjustment
    {
        #region Approve UnApprove

        public string Approv(Int32 transactionNo, string userID)
        {
            return ApprovProcess(transactionNo, userID, true);
        }

        public string UnApprov(Int32 transactionNo, string userID)
        {
            return ApprovProcess(transactionNo, userID, false);
        }

        private static string ApprovProcess(Int32 transactionNo, string userID, bool isApproval)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                EmployeeMedicalAdjustment entity = new EmployeeMedicalAdjustment();
                if (entity.LoadByPrimaryKey(Convert.ToInt32(transactionNo)))
                {
                    //Check status record
                    if (isApproval)
                    {
                        if (entity.IsApproved.Value)
                            return "Approved";
                    }                   

                    entity.IsApproved = isApproval;
                    
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

        #endregion
    }
}

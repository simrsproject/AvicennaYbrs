using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class GuarantorDepositBalance
    {
        public static void PrepareGuarantorDepositBalances(string transNo, string guarId, string transCode, string userId, decimal debet, decimal credit,
            ref GuarantorDepositBalance balance, ref GuarantorDepositMovement movement)
        {
            decimal initial;
            if (!balance.LoadByPrimaryKey(guarId))
            {
                balance.AddNew();
                initial = 0;
            }
            else
            {
                initial = balance.BalanceAmount ?? 0;
            }
            balance.GuarantorID = guarId;
            balance.BalanceAmount = initial + debet - credit;
            balance.LastUpdateDateTime = DateTime.Now;
            balance.LastUpdateByUserID = userId;

            movement.AddNew();
            movement.MovementDate = DateTime.Now;
            movement.GuarantorID = guarId;
            movement.TransactionCode = transCode;
            movement.TransactionNo = transNo;
            movement.InitialBalance = initial;
            movement.Debet = debet;
            movement.Credit = credit;
            movement.LastUpdateDateTime = DateTime.Now;
            movement.LastUpdateByUserID = userId;
        }
    }
}

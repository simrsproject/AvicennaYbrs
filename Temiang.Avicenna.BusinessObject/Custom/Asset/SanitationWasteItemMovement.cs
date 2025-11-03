using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class SanitationWasteItemMovement
    {
        public static void PrepareSanitationWasteItemMovement(SanitationWasteTrans it, IEnumerable<SanitationWasteTransItem> coll, string userId,
             ref SanitationWasteItemBalanceCollection itemBalance, ref SanitationWasteItemMovementCollection itemMovements)
        {
            var items = (coll.Select(i => i.SRWasteType)).Distinct();

            if (!items.Any())
                return;

            itemBalance.Query.Where(itemBalance.Query.SRWasteType.In(items));
            itemBalance.LoadAll();

            foreach (SanitationWasteTransItem entity in coll)
            {
                decimal? qty = entity.Qty;
                if (qty > 0)
                {
                    #region balance
                    var balance =
                        itemBalance.SingleOrDefault(
                            ib =>
                            ib.SRWasteType == entity.SRWasteType);
                    if (balance == null)
                    {
                        balance = itemBalance.AddNew();
                        balance.SRWasteType = entity.SRWasteType;
                        balance.Balance = (it.TransactionCode == "R" ? qty : (-1) * qty);
                    }
                    else
                    {
                        balance.Balance += (it.TransactionCode == "R" ? qty : (-1) * qty);
                    }
                    
                    balance.LastUpdateDateTime = Utils.NowAtSqlServer(); 
                    balance.LastUpdateByUserID = userId;
                    #endregion

                    #region movement
                    var movement = itemMovements.AddNew();
                    movement.MovementDate = Utils.NowAtSqlServer();
                    movement.TransactionCode = it.TransactionCode;
                    movement.TransactionNo = entity.TransactionNo;
                    movement.SRWasteType = entity.SRWasteType;
                    movement.InitialQty = balance.Balance - (it.TransactionCode == "R" ? qty : (-1) * qty);
                    movement.QtyIn = it.TransactionCode == "R" ? qty : 0;
                    movement.QtyOut = it.TransactionCode == "D" ? qty : 0;
                    movement.Balance = movement.InitialQty + movement.QtyIn - movement.QtyOut;
                    movement.LastUpdateDateTime = Utils.NowAtSqlServer();
                    movement.LastUpdateByUserID = userId;
                    #endregion
                }
            }
        }

    }
}

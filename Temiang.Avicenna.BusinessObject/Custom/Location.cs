using System;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class Location
    {
        public override void Save()
        {
            using (var trans = new esTransactionScope())
            {
                // History Balance hanya diupdate jika record di edit

                // 01. Max Min auto update
                var isAutoUpdateStockMinMaxChanged = false;
                if (es.IsModified)
                    isAutoUpdateStockMinMaxChanged = this.IsAutoUpdateStockMinMax == true &&
                                                     !this.GetOriginalColumnValue("IsAutoUpdateStockMinMax")
                                                         .Equals(this.GetColumn("IsAutoUpdateStockMinMax"));

                if (isAutoUpdateStockMinMaxChanged)
                {
                    var dayForMin =
                        AppParameter.GetParameterValue(AppParameter.ParameterItem.PeriodDayHistUsingForCalcMinBalance)
                            .ToInt();
                    var dayForMax =
                        AppParameter.GetParameterValue(AppParameter.ParameterItem.PeriodDayHistUsingForCalcMaxBalance)
                            .ToInt();
                    UpdateStockMinMaxPerLocation(LocationID, dayForMin, dayForMax);
                }


                //Balance, max min per stock group
                var prevStockGroup = string.Empty;
                var newStockGroup = string.Empty;
                if (es.IsModified)
                {
                    prevStockGroup = this.GetOriginalColumnValue("SRStockGroup").ToString();
                    newStockGroup = SRStockGroup;
                }

                //02. Save Location ...SRStockGroup diperlukan untuk update history balance per StockGroup
                base.Save();

                //03. Update kondisi  balance per StockGroup 
                if (!string.IsNullOrEmpty(prevStockGroup) || !string.IsNullOrEmpty(newStockGroup))
                {
                    if (prevStockGroup != newStockGroup)
                    {
                        if (!string.IsNullOrEmpty(prevStockGroup))
                            UpdateHistStockPerStockGroup(prevStockGroup);

                        if (!string.IsNullOrEmpty(newStockGroup))
                            UpdateHistStockPerStockGroup(newStockGroup);
                    }
                }

                //04.  Update Min Max ItemBalanceByStockGroup
                if (prevStockGroup != newStockGroup)
                {
                    if (AppParameter.IsYes(AppParameter.ParameterItem.IsMinMaxItemBalanceByStockGroupAutoUpdate))
                    {
                        var dayForMin =
                            AppParameter.GetParameterValue(
                                AppParameter.ParameterItem.PeriodDayHistUsingForCalcMinBalPerStockGroup).ToInt();
                        var dayForMax =
                            AppParameter.GetParameterValue(
                                AppParameter.ParameterItem.PeriodDayHistUsingForCalcMaxBalPerStockGroup).ToInt();

                        UpdateMinMaxItemBalanceByStockGroup(prevStockGroup, dayForMin, dayForMax);
                        UpdateMinMaxItemBalanceByStockGroup(newStockGroup, dayForMin, dayForMax);
                    }
                }

                trans.Complete();
            }
        }

        private void UpdateHistStockPerStockGroup(string stockGroup)
        {
            var util = new Dal.Core.esUtility();

            // Update ItemSalesPerDate
            string cmdText = string.Format(@"DELETE ItemSalesPerDate WHERE SRStockGroup='{0}'
            
            INSERT INTO ItemSalesPerDate
              (
                MovementDate,
                SRStockGroup,
                ItemID,
                ServiceUnitID,
                LocationID,
                QuantityOut
              )
            SELECT CONVERT(VARCHAR, im.MovementDate, 112),
                   l.SRStockGroup,
                   im.ItemID,
                   im.ServiceUnitID,
                   im.LocationID,
                   SUM(CASE WHEN im.TransactionCode IN ('091', '094', '003') THEN im.QuantityOut - im.QuantityIN ELSE im.QuantityOut END)
            FROM   ItemMovement AS im
                   INNER JOIN Location l
                        ON  l.LocationID = im.LocationID
            WHERE  l.SRStockGroup = '{0}'
                   AND im.TransactionCode IN ('091', '094', '003', '082','074','075')
            GROUP BY
                   CONVERT(VARCHAR, im.MovementDate, 112),
                   l.SRStockGroup,
                   im.ItemID,
                   im.ServiceUnitID,
                   im.LocationID", stockGroup);
            util.ExecuteNonQuery(esQueryType.Text, cmdText);


            // Update ItemBalanceByStockGroup
            util = new Dal.Core.esUtility();
            cmdText = string.Format(@"DELETE ItemBalanceByStockGroup WHERE SRStockGroup='{0}'
INSERT INTO ItemBalanceByStockGroup
  (
    SRStockGroup,
    ItemID,
    Minimum,
    Maximum,
    Balance
  )
SELECT q.SRStockGroup,
       q.ItemID,
       0,
       0,
       SUM(q.Balance)
FROM   (
           SELECT l.SRStockGroup,
                  ib.ItemID,
                  Balance = (
                      SELECT TOP 1 COALESCE(im.InitialStock, 0) + COALESCE(im.QuantityIn, 0) -COALESCE(im.QuantityOut, 0)
                      FROM   ItemMovement AS im
                      WHERE  im.ItemID = ib.ItemID
                             AND im.LocationID = l.LocationID
                      ORDER BY
                             im.MovementDate DESC
                  )
           FROM   Location l
                  INNER JOIN ItemBalance AS ib
                       ON  ib.LocationID = l.LocationID
           WHERE  l.SRStockGroup = '{0}'
       ) q
WHERE  q.Balance IS NOT NULL
GROUP BY
       q.SRStockGroup,
       q.ItemID", stockGroup);
            util.ExecuteNonQuery(esQueryType.Text, cmdText);

        }

        public static void UpdateMinMaxItemBalanceByStockGroup(string stockGroup, int dayForMin, int dayForMax)
        {
            var util = new Dal.Core.esUtility();

            string cmdText = string.Format(@"UPDATE ib
            SET   ib.Minimum = (
                       SELECT SUM(isd.QuantityOut)
                       FROM   ItemSalesPerDate AS isd
                       WHERE  isd.ItemID = ib.ItemID
                              AND isd.SRStockGroup = ib.SRStockGroup
                              AND isd.MovementDate >= DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0-{1})
                   ),
                   ib.Maximum = (
                       SELECT SUM(isd.QuantityOut)
                       FROM   ItemSalesPerDate AS isd
                       WHERE  isd.ItemID = ib.ItemID
                              AND isd.SRStockGroup = ib.SRStockGroup
                              AND isd.MovementDate >= DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0-{2})
                   )
            FROM ItemBalanceByStockGroup ib WHERE ib.SRStockGroup='{0}'", stockGroup, dayForMin, dayForMax);
            util.ExecuteNonQuery(esQueryType.Text, cmdText);

        }

        public static void UpdateStockMinMaxPerLocation(string locationID, int dayForMin, int dayForMax)
        {
            var util = new Dal.Core.esUtility();

            string cmdText = string.Format(@"UPDATE ib
SET ib.Minimum = COALESCE ((
           SELECT SUM(isd.QuantityOut)
           FROM   ItemSalesPerDate AS isd
           WHERE  isd.ItemID = ib.ItemID
                  AND isd.LocationID = ib.LocationID
                  AND isd.MovementDate >= DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0-{1})
       ),0),
       ib.Maximum = COALESCE ((
           SELECT SUM(isd.QuantityOut)
           FROM   ItemSalesPerDate AS isd
           WHERE  isd.ItemID = ib.ItemID
                  AND isd.LocationID = ib.LocationID
                  AND isd.MovementDate >= DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0-{2})
       ),0)
FROM ItemBalance ib WHERE ib.LocationID = '{0}'", locationID, dayForMin, dayForMax);
            util.ExecuteNonQuery(esQueryType.Text, cmdText);
        }
    }

    public partial class LocationCollection
    {
        public void LoadByServiceUnitID(string ServiceUnitID) {
            var loc = new LocationQuery("loc");
            var suloc = new ServiceUnitLocationQuery("suloc");
            loc.InnerJoin(suloc).On(loc.LocationID == suloc.LocationID)
                .Where(suloc.ServiceUnitID == ServiceUnitID);
            this.Load(loc);
        }
    }
}

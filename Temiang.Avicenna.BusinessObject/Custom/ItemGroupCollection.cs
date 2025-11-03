using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
//using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemGroupCollection
    {
        public void GetByCostCalculation(CostCalculationCollection ccColl)
        {
            var igIds = ccColl.Select(x => x.ItemGroupID).Distinct().ToArray();
            if (igIds.Count() > 0)
            {
                var igQ = new ItemGroupQuery("b");
                igQ.Where(igQ.ItemGroupID.In(igIds))
                    .Select(igQ);
                igQ.es.Distinct = true;
                this.Load(igQ);
            }
        }
    }
}
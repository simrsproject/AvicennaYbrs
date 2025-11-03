using System;
using Temiang.Dal.Interfaces; using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppAutoNumber
    {
        public void LoadByEffectiveDate(DateTime effectiveDate, string autoNumberID)
        {
            Query.es.Top = 1;
            Query.OrderBy(Query.EffectiveDate, esOrderByDirection.Descending);
            Query.Where(Query.SRAutoNumber == autoNumberID, Query.EffectiveDate <= effectiveDate);
            Load(Query);
        }
    }
}
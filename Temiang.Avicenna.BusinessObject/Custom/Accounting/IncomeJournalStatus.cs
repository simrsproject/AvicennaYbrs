using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class IncomeJournalStatus : esIncomeJournalStatus
    {
        public static IncomeJournalStatusCollection Get()
        {
            IncomeJournalStatusCollection coll = new IncomeJournalStatusCollection();
            coll.Query.OrderBy(coll.Query.Year.Descending, coll.Query.Month.Descending);
            coll.Query.es.WithNoLock = true;
            coll.Query.Load();
            return coll;
        }

        public static IncomeJournalStatus Get(int Id)
        {
            IncomeJournalStatus entity = new IncomeJournalStatus();
            entity.Query.Where(entity.Query.Id == Id);
            if (entity.Query.Load())
                return entity;
            else
                return null;
        }
    }
}

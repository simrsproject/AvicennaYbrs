using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PathologyAnatomy
    {
        public bool LoadByPathologyNo(string PathologyNo)
        {
            esPathologyAnatomyQuery query = GetDynamicQuery();
            query.es.Top = 1;
            query.Where(query.PathologyNo == PathologyNo);
            return query.Load();
        }
    }
}


using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Temiang.Dal.Interfaces; using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicFeeRemunByIdi
    {
        public bool LoadByRemunNo(string RemunNo) {
            this.Query.Where(this.Query.RemunNo == RemunNo);
            return this.Load(this.Query);
        }
    }

    public partial class ParamedicFeeRemunByIdiCollection : esParamedicFeeRemunByIdiCollection
    {

    }
}

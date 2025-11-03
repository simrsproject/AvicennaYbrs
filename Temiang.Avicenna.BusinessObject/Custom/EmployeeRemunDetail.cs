
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Temiang.Dal.Interfaces; using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeRemunDetail
    {
        public bool ToBeDelete { get; set; }
    }

    public partial class EmployeeRemunDetailCollection : esEmployeeRemunDetailCollection
    {

    }
}

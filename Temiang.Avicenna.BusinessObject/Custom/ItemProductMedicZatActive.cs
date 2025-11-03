using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemProductMedicZatActive
    {
        public string ZatActiveName
        {
            get { return GetColumn("refToZatActive_ZatActiveName").ToString(); }
            set { SetColumn("refToZatActive_ZatActiveName", value); }
        }
    }
}

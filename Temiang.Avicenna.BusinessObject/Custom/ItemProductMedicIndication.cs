using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemProductMedicIndication
    {
        public string IndicationName
        {
            get { return GetColumn("refToIndication_IndicationName").ToString(); }
            set { SetColumn("refToIndication_IndicationName", value); }
        }
    }
}

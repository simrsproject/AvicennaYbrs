using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemProductMedicLabel
    {
        public string LabelName
        {
            get { return GetColumn("refToLabel_LabelName").ToString(); }
            set { SetColumn("refToLabel_LabelName", value); }
        }
    }
}

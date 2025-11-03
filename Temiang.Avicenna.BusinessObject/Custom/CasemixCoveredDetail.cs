using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CasemixCoveredDetail
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }
    }
}

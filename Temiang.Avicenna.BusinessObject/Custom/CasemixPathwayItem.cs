using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CasemixPathwayItem
    {
        public string ItemName
        {
            get { return GetColumn("refTo_Item_ItemName").ToString(); }
            set { SetColumn("refTo_Item_ItemName", value); }
        }
        public string ItemGroupName
        {
            get { return GetColumn("refTo_ItemGroup_ItemGroupName").ToString(); }
            set { SetColumn("refTo_ItemGroup_ItemGroupName", value); }
        }
    }
}

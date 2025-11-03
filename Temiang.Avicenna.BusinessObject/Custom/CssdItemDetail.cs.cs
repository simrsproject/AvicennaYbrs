using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CssdItemDetail
    {
        public string ItemDetailName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }
        public string SRItemUnit
        {
            get { return GetColumn("refToVwItem_SRItemUnit").ToString(); }
            set { SetColumn("refToVwItem_SRItemUnit", value); }
        }
    }
}

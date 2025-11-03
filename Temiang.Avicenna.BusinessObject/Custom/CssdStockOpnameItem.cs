using System;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class CssdStockOpnameItem
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }

        public string SRItemUnit
        {
            get { return GetColumn("refToItem_SRItemUnit").ToString(); }
            set { SetColumn("refToItem_SRItemUnit", value); }
        }

        public bool IsCssdUnit
        {
            get { return Convert.ToBoolean(GetColumn("refTo_IsCssdUnit")); }
            set { SetColumn("refTo_IsCssdUnit", value); }
        }
    }
}

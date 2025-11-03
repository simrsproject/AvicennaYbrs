using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransChargesItemConsumption
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }
        public string LocationName
        {
            get { return GetColumn("refToLocation_LocationName").ToString(); }
            set { SetColumn("refToLocation_LocationName", value); }
        }

        public decimal? MaxValue
        {
            get { return Convert.ToDecimal(GetColumn("refTo_MaxQty")); }
            set { SetColumn("refTo_MaxQty", value); }
        }
    }
}

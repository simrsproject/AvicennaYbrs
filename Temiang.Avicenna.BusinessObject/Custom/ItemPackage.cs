using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemPackage
    {
        public string DetailItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }

        public string ServiceUnitName
        {
            get { return GetColumn("refToServiceUnit_ServiceUnitName").ToString(); }
            set { SetColumn("refToServiceUnit_ServiceUnitName", value); }
        }

        public decimal Price
        {
            get { return Convert.ToDecimal(GetColumn("refToItemPackage_Price").ToString()); }
            set { SetColumn("refToItemPackage_Price", value); }
        }

        public decimal Discount
        {
            get { return Convert.ToDecimal(GetColumn("refToItemPackage_Discount").ToString()); }
            set { SetColumn("refToItemPackage_Discount", value); }
        }


        public decimal Total
        {
            get { return Convert.ToDecimal(GetColumn("refToItemPackage_Total").ToString()); }
            set { SetColumn("refToItemPackage_Total", value); }
        }
    }
}
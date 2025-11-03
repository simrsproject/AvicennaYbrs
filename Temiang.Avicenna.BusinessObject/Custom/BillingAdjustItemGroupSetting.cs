using System;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class BillingAdjustItemGroupSetting
    {
        public string ItemGroupName
        {
            get { return GetColumn("refToItemGroup_Name").ToString(); }
            set { SetColumn("refToItemGroup_Name", value); }
        }
    }
}

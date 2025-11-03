using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CssdSterileItemsReceivedItemDetail
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }
        public string SRItemUnit
        {
            get { return GetColumn("refToAppStdRef_ItemUnit").ToString(); }
            set { SetColumn("refToAppStdRef_ItemUnit", value); }
        }
        public bool IsBrokenInstrumentX
        {
            get { return Convert.ToBoolean(GetColumn("refTo_IsBrokenInstrument")); }
            set { SetColumn("refTo_IsBrokenInstrument", value); }
        }
        public Decimal QtyReplacementsX
        {
            get { return Convert.ToDecimal(GetColumn("refTo_QtyReplacements")); }
            set { SetColumn("refTo_QtyReplacements", value); }
        }
    }
}

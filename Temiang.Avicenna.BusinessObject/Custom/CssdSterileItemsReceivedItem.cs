
namespace Temiang.Avicenna.BusinessObject
{
    public partial class CssdSterileItemsReceivedItem
    {
        public string ItemNo
        {
            get { return GetColumn("refTo_CssdItemNo").ToString(); }
            set { SetColumn("refTo_CssdItemNo", value); }
        }

        public string ItemName
        {
            get { return GetColumn("refToCssdItem_ItemName").ToString(); }
            set { SetColumn("refToCssdItem_ItemName", value); }
        }

        public string CssdItemUnit
        {
            get { return GetColumn("refToAppStandardReferenceItem_CssdItemUnit").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_CssdItemUnit", value); }
        }

        public bool IsItemProduction
        {
            get { return GetColumn("refToItem_IsItemProduction").ToBoolean(); }
            set { SetColumn("refToItem_IsItemProduction", value); }
        }

        public string DttDescription
        {
            get { return GetColumn("refTo_IsDttDescription").ToString(); }
            set { SetColumn("refTo_IsDttDescription", value); }
        }
    }
}

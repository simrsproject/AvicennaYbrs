namespace Temiang.Avicenna.BusinessObject
{
    public partial class CssdSterileItemsUltrasoundItem
    {
        public decimal? Qty
        {
            get { return (decimal?)GetColumn("refToCssdSterileItemsReceivedItem_Qty"); }
            set { SetColumn("refToCssdSterileItemsReceivedItem_Qty", value); }
        }

        public string CssdItemNo
        {
            get { return GetColumn("refToCssdSterileItemsReceivedItem_CssdItemNo").ToString(); }
            set { SetColumn("refToCssdSterileItemsReceivedItem_CssdItemNo", value); }
        }

        public string ItemNo
        {
            get { return GetColumn("refTo_CssdItemNo").ToString(); }
            set { SetColumn("refTo_CssdItemNo", value); }
        }

        public string ItemID
        {
            get { return GetColumn("refToCssdSterileItemsReceivedItem_ItemID").ToString(); }
            set { SetColumn("refToCssdSterileItemsReceivedItem_ItemID", value); }
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

        public string Notes
        {
            get { return GetColumn("refToCssdSterileItemsReceivedItem_Notes").ToString(); }
            set { SetColumn("refToCssdSterileItemsReceivedItem_Notes", value); }
        }
    }
}

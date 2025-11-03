namespace Temiang.Avicenna.BusinessObject
{
    public partial class CssdSterileItemsReturnedItem
    {
        public string ReceivedNo
        {
            get { return GetColumn("refToCssdSterilizationProcessItem_ReceivedNo").ToString(); }
            set { SetColumn("refToCssdSterilizationProcessItem_ReceivedNo", value); }
        }

        public string ReceivedSeqNo
        {
            get { return GetColumn("refToCssdSterilizationProcessItem_ReceivedSeqNo").ToString(); }
            set { SetColumn("refToCssdSterilizationProcessItem_ReceivedSeqNo", value); }
        }

        public decimal? Weight
        {
            get { return (decimal?)GetColumn("refToCssdSterilizationProcessItem_Weight"); }
            set { SetColumn("refToCssdSterilizationProcessItem_Weight", value); }
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

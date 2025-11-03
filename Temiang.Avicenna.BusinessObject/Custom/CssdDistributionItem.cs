namespace Temiang.Avicenna.BusinessObject
{
    public partial class CssdDistributionItem
    {
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
    }
}

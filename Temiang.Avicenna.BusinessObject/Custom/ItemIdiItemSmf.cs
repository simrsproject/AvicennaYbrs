namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemIdiItemSmf
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }
        public string SmfName
        {
            get { return GetColumn("refToSmf_SmfName").ToString(); }
            set { SetColumn("refToSmf_SmfName", value); }
        }
        public string IdiName
        {
            get { return GetColumn("refToItemIdi_IdiName").ToString(); }
            set { SetColumn("refToItemIdi_IdiName", value); }
        }
    }
}

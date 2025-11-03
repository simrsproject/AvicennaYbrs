namespace Temiang.Avicenna.BusinessObject
{
    public partial class DataRptItem
    {
        public string ItemName
        {
            get { return GetColumn("refToDataRptItem_ItemName").ToString(); }
            set { SetColumn("refToDataRptItem_ItemName", value); }
        }
    }
}

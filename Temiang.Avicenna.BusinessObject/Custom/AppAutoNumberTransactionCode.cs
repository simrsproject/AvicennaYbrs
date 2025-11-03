namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppAutoNumberTransactionCode
    {
        public string TransactionName
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemName", value); }
        }
    }
}

namespace Temiang.Avicenna.BusinessObject
{
    public partial class SnackOrderItem
    {
        public string SnackName
        {
            get { return GetColumn("refToSnack_SnackName").ToString(); }
            set { SetColumn("refToSnack_SnackName", value); }
        }
    }
}

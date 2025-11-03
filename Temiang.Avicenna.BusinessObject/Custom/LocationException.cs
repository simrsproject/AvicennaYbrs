namespace Temiang.Avicenna.BusinessObject
{
    public partial class LocationException
    {
        public string LocationExceptionName
        {
            get { return GetColumn("refToLocation_LocationExceptionName").ToString(); }
            set { SetColumn("refToLocation_LocationExceptionName", value); }
        }
    }
}

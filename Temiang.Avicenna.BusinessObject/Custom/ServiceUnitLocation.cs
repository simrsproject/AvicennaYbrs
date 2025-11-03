namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceUnitLocation
    {
        public string LocationName
        {
            get { return GetColumn("refToLocation_LocationName").ToString(); }
            set { SetColumn("refToLocation_LocationName", value); }
        }
    }
}

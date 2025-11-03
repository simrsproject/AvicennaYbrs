namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceUnitVisitType
    {
        public string VisitTypeName
        {
            get { return GetColumn("refToVisitType_VisitTypeName").ToString(); }
            set { SetColumn("refToVisitType_VisitTypeName", value); }
        }
    }
}

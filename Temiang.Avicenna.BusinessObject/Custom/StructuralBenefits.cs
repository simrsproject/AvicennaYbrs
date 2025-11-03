namespace Temiang.Avicenna.BusinessObject
{
    public partial class StructuralBenefits
    {
        public string PositionName
        {
            get { return GetColumn("refToPosition_PositionName").ToString(); }
            set { SetColumn("refToPosition_PositionName", value); }
        }
    }
}

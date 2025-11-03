namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicFeeGuarantorCategory
    {
        public string PhysicianFeeType
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemName", value); }
        }
    }
}

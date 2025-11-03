namespace Temiang.Avicenna.BusinessObject
{
    public partial class GuarantorItemPrescriptionByTherapyRule
    {
        public string TherapyGroupName
        {
            get { return GetColumn("refToAppStandardReferenceItem_TherapyGroup").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_TherapyGroup", value); }
        }

        public string GuarantorRuleTypeName
        {
            get { return GetColumn("refToAppStandardReferenceItem_GuarantorRuleType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_GuarantorRuleType", value); }
        }
    }
}

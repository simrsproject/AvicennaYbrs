namespace Temiang.Avicenna.BusinessObject
{
    public partial class TreatmentForAnimalBites
    {
        public string TreatmentForAnimalBitesName
        {
            get { return GetColumn("refToAppStandardReferenceItem_TreatmentForAnimalBites").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_TreatmentForAnimalBites", value); }
        }
    }
}

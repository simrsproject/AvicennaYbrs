namespace Temiang.Avicenna.BusinessObject
{
    public partial class GuarantorSurgicalPackageCovered
    {
        public string PackageName
        {
            get { return GetColumn("refToSurgicalPackage_PackageName").ToString(); }
            set { SetColumn("refToSurgicalPackage_PackageName", value); }
        }
    }
}

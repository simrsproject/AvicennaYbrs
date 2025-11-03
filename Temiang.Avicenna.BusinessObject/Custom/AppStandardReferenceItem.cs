namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppStandardReferenceItem
    {
        private AdjustedDisc _adjustedDisc = new AdjustedDisc();

        public string ReferenceName
        {
            get { return GetColumn("refTo_ReferenceName").ToString(); }
            set { SetColumn("refTo_ReferenceName", value); }
        }
        public string CustomFieldName
        {
            get { return GetColumn("refTo_CustomFieldName").ToString(); }
            set { SetColumn("refTo_CustomFieldName", value); }
        }
        public string CoaCode
        {
            get { return GetColumn("refTo_CoaCode").ToString(); }
            set { SetColumn("refTo_CoaCode", value); }
        }
        public string CoaName
        {
            get { return GetColumn("refTo_CoaName").ToString(); }
            set { SetColumn("refTo_CoaName", value); }
        }

        public AdjustedDisc AdjustedDisc
        {
            get { return _adjustedDisc; }
            set { _adjustedDisc = value; }
        }

        public bool IsNeedCrossMatchingProcess
        {
            get { return GetColumn("refTo_CustomField_IsNeedCrossMatchingProcess").ToBoolean(); }
            set { SetColumn("refTo_CustomField_IsNeedCrossMatchingProcess", value); }
        }

        public bool IsReturnable
        {
            get { return GetColumn("refTo_CustomField2_IsReturnable").ToBoolean(); }
            set { SetColumn("refTo_CustomField2_IsReturnable", value); }
        }
    }
}

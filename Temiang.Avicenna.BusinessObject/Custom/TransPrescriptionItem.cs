namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransPrescriptionItem
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }

        public string ItemInterventionName
        {
            get { return GetColumn("refToItem_ItemInterventionName").ToString(); }
            set { SetColumn("refToItem_ItemInterventionName", value); }
        }

        public string FornasRestrictionNotes
        {
            get { return GetColumn("refToItem_FornasRestrictionNotes").ToString(); }
            set { SetColumn("refToItem_FornasRestrictionNotes", value); }
        }

        public string CombinedNotes
        {
            get { return GetColumn("refTo_CombinedNotes").ToString(); }
            set { SetColumn("refTo_CombinedNotes", value); }
        }

        public decimal Total
        {
            get { return (decimal)GetColumn("refToTransPrescriptionItem_Total"); }
            set { SetColumn("refToTransPrescriptionItem_Total", value); }
        }

        public string SRConsumeMethodName
        {
            get { return GetColumn("refToConsumeMethod_SRConsumeMethodName").ToString(); }
            set { SetColumn("refToConsumeMethod_SRConsumeMethodName", value); }
        }

        public string EmbalaceLabel
        {
            get { return GetColumn("refToEmbalace_EmbalaceLabel").ToString(); }
            set { SetColumn("refToEmbalace_EmbalaceLabel", value); }
        }

        public bool IsHam
        {
            get { return true.Equals(GetColumn("refToIpm_IsHam")); }
            set { SetColumn("refToIpm_IsHam", value); }
        }

        public bool IsActive
        {
            get { return true.Equals(GetColumn("refToItem_IsActive")); }
            set { SetColumn("refToItem_IsActive", value); }
        }

        public bool IsFromTemplate
        {
            get { return true.Equals(GetColumn("refTo_IsFromTemplate")); }
            set { SetColumn("refTo_IsFromTemplate", value); }
        }
    }
}

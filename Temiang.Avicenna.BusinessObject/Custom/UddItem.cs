namespace Temiang.Avicenna.BusinessObject
{
    public partial class UddItem
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
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

        public bool IsOldRecord
        {
            get { return true.Equals(GetColumn("ref_IsOldRecord")); }
            set { SetColumn("ref_IsOldRecord", value); }
        }

        public bool IsAntibiotic
        {
            get { return true.Equals(GetColumn("refToIpm_IsAntibiotic")); }
            set { SetColumn("refToIpm_IsAntibiotic", value); }
        }
        public string SRRaspro
        {
            get { return GetColumn("refToRR_SRRaspro").ToString(); }
            set { SetColumn("refToRR_SRRaspro", value); }
        }

        public int ConsumeDayNo
        {
            get { return GetColumn("ref_ConsumeDayNo").ToInt(); }
            set { SetColumn("ref_ConsumeDayNo", value); }
        }
        public string ParamedicName
        {
            get { return GetColumn("refToParamedic_ParamedicName").ToString(); }
            set { SetColumn("refToParamedic_ParamedicName", value); }
        }
        public string LastUpdateByUserName
        {
            get { return GetColumn("refToAppUser_UserName").ToString(); }
            set { SetColumn("refToAppUser_UserName", value); }
        }                        
    }
}

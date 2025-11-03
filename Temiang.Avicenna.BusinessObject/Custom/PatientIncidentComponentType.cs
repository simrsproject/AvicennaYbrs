namespace Temiang.Avicenna.BusinessObject
{
    public partial class PatientIncidentComponentType
    {
        public string IncidentType
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemName", value); }
        }

        public string ComponentName
        {
            get { return GetColumn("refToIncidentType_ComponentName").ToString(); }
            set { SetColumn("refToIncidentType_ComponentName", value); }
        }

        public string SubComponent
        {
            get { return GetColumn("refToIncidentTypeItem_SubComponentName").ToString(); }
            set { SetColumn("refToIncidentTypeItem_SubComponentName", value); }
        }

        public bool? IsAllowEdit
        {
            get { return (bool?)GetColumn("refToIncidentTypeItem_IsAllowEdit"); }
            set { SetColumn("refToIncidentTypeItem_IsAllowEdit", value); }
        }
    }
}
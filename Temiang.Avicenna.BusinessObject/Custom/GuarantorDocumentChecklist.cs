namespace Temiang.Avicenna.BusinessObject
{
    public partial class GuarantorDocumentChecklist
    {
        public string RegistrationType
        {
            get { return GetColumn("refToAppStandardReference_RegistrationType").ToString(); }
            set { SetColumn("refToAppStandardReference_RegistrationType", value); }
        }

        public string DocumentChecklistName
        {
            get { return GetColumn("refToAppStandardReference_DocumentChecklist").ToString(); }
            set { SetColumn("refToAppStandardReference_DocumentChecklist", value); }
        }
    }
}

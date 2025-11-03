namespace Temiang.Avicenna.BusinessObject
{
    public partial class CssdMachineItem
    {
        public string CssdProcessType
        {
            get { return GetColumn("refToAppStandardReferenceItem_CssdProcessType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_CssdProcessType", value); }
        }
    }
}

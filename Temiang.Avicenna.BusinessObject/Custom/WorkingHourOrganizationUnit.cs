namespace Temiang.Avicenna.BusinessObject
{
    public partial class WorkingHourOrganizationUnit
    {
        public string OrganizationUnitName
        {
            get { return GetColumn("refToOrganizationUnit_OrganizationUnitName").ToString(); }
            set { SetColumn("refToOrganizationUnit_OrganizationUnitName", value); }
        }
    }
}

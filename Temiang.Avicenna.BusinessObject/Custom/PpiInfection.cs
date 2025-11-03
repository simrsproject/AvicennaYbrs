namespace Temiang.Avicenna.BusinessObject
{
    public partial class PpiInfection
    {
        public string InfectionTypeName
        {
            get { return GetColumn("refToAppStdRef_InfectionType").ToString(); }
            set { SetColumn("refToAppStdRef_InfectionType", value); }
        }
    }
}

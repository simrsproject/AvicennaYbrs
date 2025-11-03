namespace Temiang.Avicenna.BusinessObject
{
    public partial class LaundryWashingProgramType
    {
        public string LaundryProgramName
        {
            get { return GetColumn("refToAppStdRef_LaundryProgramName").ToString(); }
            set { SetColumn("refToAppStdRef_LaundryProgramName", value); }
        }

        public string LaundryTypeName
        {
            get { return GetColumn("refToAppStdRef_LaundryTypeName").ToString(); }
            set { SetColumn("refToAppStdRef_LaundryTypeName", value); }
        }
    }
}

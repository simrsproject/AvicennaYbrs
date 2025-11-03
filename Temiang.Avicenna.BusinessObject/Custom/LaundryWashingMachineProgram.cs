namespace Temiang.Avicenna.BusinessObject
{
    public partial class LaundryWashingMachineProgram
    {
        public string LaundryProgramName
        {
            get { return GetColumn("refToAppStdRef_LaundryProgramName").ToString(); }
            set { SetColumn("refToAppStdRef_LaundryProgramName", value); }
        }
    }
}

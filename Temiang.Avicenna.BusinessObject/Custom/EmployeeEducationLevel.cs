namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeEducationLevel
    {
        public string EducationLevelName
        {
            get { return GetColumn("refToEducationLevelName_EmployeeEducationLevel").ToString(); }
            set { SetColumn("refToEducationLevelName_EmployeeEducationLevel", value); }
        }

        public string TypeOfLaborName
        {
            get { return GetColumn("refToTypeOfLaborName_EmployeeEducationLevel").ToString(); }
            set { SetColumn("refToTypeOfLaborName_EmployeeEducationLevel", value); }
        }

        public string EducationGroupName
        {
            get { return GetColumn("refToEducationGroupName_EmployeeEducationLevel").ToString(); }
            set { SetColumn("refToEducationGroupName_EmployeeEducationLevel", value); }
        }
    }
}

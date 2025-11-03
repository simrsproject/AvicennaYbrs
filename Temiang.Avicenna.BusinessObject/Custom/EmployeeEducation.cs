namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeEducation
    {
        public string EducationStatusName
        {
            get { return GetColumn("refToStdRef_EducationStatusName").ToString(); }
            set { SetColumn("refToStdRef_EducationStatusName", value); }
        }

        public string EducationFinancingSourcesName
        {
            get { return GetColumn("refToStdRef_EducationFinancingSourcesName").ToString(); }
            set { SetColumn("refToStdRef_EducationFinancingSourcesName", value); }
        }

        public string StudyPeriodStatusName
        {
            get { return GetColumn("refToStdRef_StudyPeriodStatusName").ToString(); }
            set { SetColumn("refToStdRef_StudyPeriodStatusName", value); }
        }
    }
}

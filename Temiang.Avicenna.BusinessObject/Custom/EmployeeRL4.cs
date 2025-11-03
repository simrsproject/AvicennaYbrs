using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeRL4
    {
        public string CompanyEducationProfileName
        {
            get { return GetColumn("refToCompanyEducationProfileName").ToString(); }
            set { SetColumn("refToCompanyEducationProfileName", value); }
        }

        public string CompanyFieldOfWorkProfileName
        {
            get { return GetColumn("refToCompanyFieldOfWorkProfileName").ToString(); }
            set { SetColumn("refToCompanyFieldOfWorkProfileName", value); }
        }

        public string MedisTypeName
        {
            get { return GetColumn("refToRL4MedisTypeName").ToString(); }
            set { SetColumn("refToRL4MedisTypeName", value); }
        }

        //public string EducationLevelName
        //{
        //    get { return GetColumn("refToSREducationLevelName").ToString(); }
        //    set { SetColumn("refToSREducationLevelName", value); }
        //}

        public string RL4EducationName
        {
            get { return GetColumn("refToRL4EducationName").ToString(); }
            set { SetColumn("refToRL4EducationName", value); }
        }

        //---------
        public string RL4StatusName
        {
            get { return GetColumn("refToAppStdItem_RL4Status").ToString(); }
            set { SetColumn("refToAppStdItem_RL4Status", value); }
        }

        public string RL4TypeName
        {
            get { return GetColumn("refToAppStdItem_RL4Type").ToString(); }
            set { SetColumn("refToAppStdItem_RL4Type", value); }
        }

        public string RL4ProfessionTypeName
        {
            get { return GetColumn("refToAppStdItem_RL4ProfessionType").ToString(); }
            set { SetColumn("refToAppStdItem_RL4ProfessionType", value); }
        }

        public string RL4EducationLevelName
        {
            get { return GetColumn("refToAppStdItem_RL4EducationLevel").ToString(); }
            set { SetColumn("refToAppStdItem_RL4EducationLevel", value); }
        }

        public string RL4EducationMajorName
        {
            get { return GetColumn("refToAppStdItem_RL4EducationMajor").ToString(); }
            set { SetColumn("refToAppStdItem_RL4EducationMajor", value); }
        }

    }
}

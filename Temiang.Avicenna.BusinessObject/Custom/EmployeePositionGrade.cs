namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeePositionGrade
    {
        public string EducationLevelName
        {
            get { return GetColumn("refToAppStandardReferenceItem_EducationLevelName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_EducationLevelName", value); }
        }

        public string PositionGradeName
        {
            get { return GetColumn("refToPositionGrade_PositionGradeName").ToString(); }
            set { SetColumn("refToPositionGrade_PositionGradeName", value); }
        }

        public string DecreeTypeName
        {
            get { return GetColumn("refToAppStandardReferenceItem_DecreeTypeName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_DecreeTypeName", value); }
        }

        public string NextPositionGradeName
        {
            get { return GetColumn("refToPositionGrade_NextPositionGradeName").ToString(); }
            set { SetColumn("refToPositionGrade_NextPositionGradeName", value); }
        }

        public string NextDecreeTypeName
        {
            get { return GetColumn("refToAppStandardReferenceItem_NextDecreeTypeName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_NextDecreeTypeName", value); }
        }

        public string Dp3Name
        {
            get { return GetColumn("refToAppStandardReferenceItem_Dp3ItemName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_Dp3ItemName", value); }
        }

        public string SalaryScaleName
        {
            get { return GetColumn("refToSalaryScale_SalaryScaleName").ToString(); }
            set { SetColumn("refToSalaryScale_SalaryScaleName", value); }
        }

        public string NextSalaryScaleName
        {
            get { return GetColumn("refToSalaryScale_NextSalaryScaleName").ToString(); }
            set { SetColumn("refToSalaryScale_NextSalaryScaleName", value); }
        }
    }
}

using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PersonalInfo
    {
        public string EmployeeName
        {
            get
            {
                if (string.IsNullOrEmpty(FirstName))
                    return string.Empty;
                
                var name = string.Empty;
                name = name + FirstName.Trim();
                if (MiddleName.Trim() != string.Empty)
                    name = name + " " + MiddleName.Trim();
                if (LastName.Trim() != string.Empty)
                    name = name + " " + LastName.Trim();

                return name;
            }
        }

        public string EmployeeAges
        {
            get
            {
                int ageInYears = 0;
                int ageInMonths = 0;
                int ageInDays = 0;

                ageInDays = DateTime.Now.Day - Convert.ToDateTime(BirthDate).Day;
                ageInMonths = DateTime.Now.Month - Convert.ToDateTime(BirthDate).Month;
                ageInYears = DateTime.Now.Year - Convert.ToDateTime(BirthDate).Year;

                if (ageInDays < 0)
                {
                    ageInDays += DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    ageInMonths = ageInMonths--;

                    if (ageInMonths < 0)
                    {
                        ageInMonths += 12;
                        ageInYears--;
                    }
                }
                if (ageInMonths < 0)
                {
                    ageInMonths += 12;
                    ageInYears--;
                }


                return "Years: " + ageInYears + " Months: " + ageInMonths;
            }
        }
    }
}
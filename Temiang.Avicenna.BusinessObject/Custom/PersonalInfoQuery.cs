using System;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PersonalInfoQuery
    {
        public esQueryItem EmployeeName
        {
            get
            {
                //esQueryItem employeeName = ((((FirstName + " " + MiddleName).LTrim()).RTrim() + " " + LastName).LTrim()).RTrim();
                esQueryItem employeeName = (PreTitle + " ").LTrim() + (FirstName + " " + MiddleName).RTrim() +
                                           (" " + LastName).RTrim() + (" " + PostTitle).RTrim();
                employeeName = employeeName.As("EmployeeName");
                return employeeName;
            }
        }

        public esQueryItem AgeInYears
        {
            get
            {
                esQueryItem AgeInYears = DateTime.Now.Year - BirthDate.DatePart("YYYY");
                AgeInYears = AgeInYears.As("AgeInYears");                
                return AgeInYears;
            }
        }

        public esQueryItem ageInMonths
        {
            get
            {
                esQueryItem ageInMonths = DateTime.Now.Month - BirthDate.DatePart("MM");
                ageInMonths = ageInMonths.As("ageInMonths");
                return ageInMonths;
            }
        }

        public esQueryItem ageInDays
        {
            get
            {
                esQueryItem ageInDays = DateTime.Now.Day - BirthDate.DatePart("dd");
                ageInDays = ageInDays.As("ageInDays");
                return ageInDays;
            }
        }
    }
}

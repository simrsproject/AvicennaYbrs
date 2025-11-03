using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PatientAllergy
    {
        public static string DrugAndFoodAllergy(string patientID)
        {
            var allgs = new PatientAllergyCollection();
            allgs.Query.Where(allgs.Query.PatientID == patientID,
                              allgs.Query.Or(allgs.Query.Allergen.Like("Drug%"), allgs.Query.Allergen.Like("Food%")));
            var allergy = string.Empty;
            if (allgs.LoadAll() && allgs.Count > 0)
            {
                foreach (PatientAllergy allg in allgs)
                {
                    allergy = string.Concat(allergy, ", ", allg.DescAndReaction);
                }
                if (!string.IsNullOrEmpty(allergy))
                    allergy = allergy.Substring(2);
            }
            return allergy;
        }
    }
}

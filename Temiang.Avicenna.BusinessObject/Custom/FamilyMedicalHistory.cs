using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class FamilyMedicalHistory
    {
        public const string MedicalDiseaseStandardReferenceID = "FamilyMedHist";
        public static string ToString(string patientID)
        {
            var retval = string.Empty;
            var famMed = new FamilyMedicalHistoryCollection();
            famMed.Query.Where(famMed.Query.PatientID == patientID);
            if (famMed.LoadAll() && famMed.Count > 0)
            {
                var sb = new StringBuilder();
                foreach (FamilyMedicalHistory med in famMed)
                {
                    var stdi = new AppStandardReferenceItem();
                    stdi.LoadByPrimaryKey(MedicalDiseaseStandardReferenceID, med.SRMedicalDisease);
                    if (!string.IsNullOrEmpty(med.Notes))
                    {
                        sb.AppendFormat(", {0} ({1})", stdi.ItemName, med.Notes);
                    }
                    else
                    {
                        sb.AppendFormat(", {0}", stdi.ItemName);
                    }
                }
                retval = sb.ToString();
                if (!string.IsNullOrEmpty(retval))
                    retval = retval.Substring(2);
            }
            return retval;
        }
    }
}


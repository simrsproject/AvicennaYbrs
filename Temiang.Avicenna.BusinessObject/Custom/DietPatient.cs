using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class DietPatient
    {
        public static class Last
        {
            public static string DietName(string registrationNo)
            {
                var query = new DietPatientQuery();
                query.Where(query.RegistrationNo == registrationNo, query.EffectiveStartDate.Date() <= DateTime.Now.Date);
                query.es.Top = 1;
                query.OrderBy(query.EffectiveStartDate.Descending);
                var dp = new DietPatient();
                if (dp.Load(query))
                {
                    var dietName = string.Empty;

                    var dpicoll = new DietPatientItemCollection();
                    dpicoll.Query.Where(dpicoll.Query.TransactionNo == dp.TransactionNo);
                    dpicoll.LoadAll();
                    foreach (var dpi in dpicoll)
                    {
                        var d = new Diet();
                        if (d.LoadByPrimaryKey(dpi.DietID))
                        {
                            dietName = string.IsNullOrEmpty(dietName) ? d.DietName : dietName + ", " + d.DietName;
                        }
                    }

                    return dietName;
                }

                return String.Empty;
            }

            public static string FormOfFood(string registrationNo)
            {
                var query = new DietPatientQuery();
                query.Where(query.RegistrationNo == registrationNo, query.EffectiveStartDate.Date() <= DateTime.Now.Date);
                query.es.Top = 1;
                query.OrderBy(query.EffectiveStartDate.Descending);
                var dp = new DietPatient();
                if (dp.Load(query))
                {
                    var formOfFood = string.Empty;
                    var std = new AppStandardReferenceItem();
                    if (std.LoadByPrimaryKey("FormOfFood", dp.FormOfFood))
                    {
                        formOfFood = std.ItemName;
                    }

                    return formOfFood;
                }

                return String.Empty;
            }
        }
    }
}

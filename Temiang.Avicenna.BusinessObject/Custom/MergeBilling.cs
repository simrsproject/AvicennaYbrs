using System;
using System.Collections.Generic;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class MergeBilling
    {
        public static string[] GetMergeRegistration(string registrationNo)
        {
            var mrg = new BusinessObject.MergeBilling();
            mrg.LoadByPrimaryKey(registrationNo);

            if (string.IsNullOrEmpty(mrg.RegistrationNo)) { 
                return (new string[]{ registrationNo});
            }

                var mrgs = new MergeBillingCollection();
            if (!string.IsNullOrEmpty(mrg.FromRegistrationNo))
                mrgs.Query.Where(
                    mrgs.Query.Or(
                        mrgs.Query.RegistrationNo == mrg.RegistrationNo,
                        mrgs.Query.FromRegistrationNo == mrg.RegistrationNo,
                        mrgs.Query.RegistrationNo == mrg.FromRegistrationNo,
                        mrgs.Query.FromRegistrationNo == mrg.FromRegistrationNo
                        )
                    );
            else
                mrgs.Query.Where(
                    mrgs.Query.Or(
                        mrgs.Query.RegistrationNo == mrg.RegistrationNo,
                        mrgs.Query.FromRegistrationNo == mrg.RegistrationNo
                        )
                    );
            mrgs.LoadAll();

            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            if (reg.SRRegistrationType == "IPR")
                return mrgs.Select(m => m.RegistrationNo).ToArray();

            var list = string.IsNullOrEmpty((mrgs.Where(m => m.RegistrationNo == registrationNo).SingleOrDefault()).FromRegistrationNo) ?
                        mrgs.Select(m => m.RegistrationNo).ToArray() :
                        mrgs.Where(m => m.RegistrationNo == registrationNo).Select(m => m.RegistrationNo).ToArray();

            if (!list.Any())
            {
                var parValue = AppParameter.GetParameterValue(AppParameter.ParameterItem.MaxResultRecord);
                var max = System.Convert.ToInt32(string.IsNullOrEmpty(parValue.ToString()) ? "500" : parValue);
                list = new string[max];//AppSession.Parameter.MaxResultRecord];

                var depts = new DepartmentCollection();
                depts.Query.Where(depts.Query.IsActive == true);
                depts.LoadAll();

                var idx = 0;

                foreach (var r in depts.Select(dept => mrgs.Where(m => m.RegistrationNo.IndexOf(dept.Initial) != -1)
                                                           .Select(m => m.RegistrationNo).ToArray()).SelectMany(r => r))
                {
                    list.SetValue(r, idx);
                    idx++;
                }

                return list.Where(l => !string.IsNullOrEmpty(l)).ToArray();
            }
            return list;
        }

        public static string[] GetFullMergeRegistration(string registrationNo)
        {
            var mrg = new BusinessObject.MergeBilling();
            mrg.LoadByPrimaryKey(registrationNo);

            if (string.IsNullOrEmpty(mrg.RegistrationNo))
            {
                return (new string[] { registrationNo });
            }

            var mrgs = new MergeBillingCollection();
            if (!string.IsNullOrEmpty(mrg.FromRegistrationNo))
                mrgs.Query.Where(
                    mrgs.Query.Or(
                        mrgs.Query.RegistrationNo == mrg.RegistrationNo,
                        mrgs.Query.FromRegistrationNo == mrg.RegistrationNo,
                        mrgs.Query.RegistrationNo == mrg.FromRegistrationNo,
                        mrgs.Query.FromRegistrationNo == mrg.FromRegistrationNo
                        )
                    );
            else
                mrgs.Query.Where(
                    mrgs.Query.Or(
                        mrgs.Query.RegistrationNo == mrg.RegistrationNo,
                        mrgs.Query.FromRegistrationNo == mrg.RegistrationNo
                        )
                    );
            mrgs.LoadAll();

            return mrgs.Select(m => m.RegistrationNo).ToArray();
        }

        /// <summary>
        ///  Untuk keperluan display data rekammedis dalam 1 Episode
        /// </summary>
        /// <param name="registrationNo"></param>
        /// <param name="patientID"></param>
        /// <returns></returns>
        [Obsolete("Tidak bisa dipakai patokan mencari list Reg dalam 1 Episode, krn kadang FromReg nya tidak ada, ganti dgn Registration.RelatedRegistrations",true)]
        public static List<string> GetFullMergeRegistration(string registrationNo, string patientID)
        {
            var mrg = new BusinessObject.MergeBilling();
            mrg.LoadByPrimaryKey(registrationNo);

            var mrgs = new MergeBillingCollection();
            if (!string.IsNullOrEmpty(mrg.FromRegistrationNo))
                mrgs.Query.Where(
                    mrgs.Query.Or(
                        mrgs.Query.RegistrationNo == mrg.RegistrationNo,
                        mrgs.Query.FromRegistrationNo == mrg.RegistrationNo,
                        mrgs.Query.RegistrationNo == mrg.FromRegistrationNo,
                        mrgs.Query.FromRegistrationNo == mrg.FromRegistrationNo
                    )
                );
            else
                mrgs.Query.Where(
                    mrgs.Query.Or(
                        mrgs.Query.RegistrationNo == mrg.RegistrationNo,
                        mrgs.Query.FromRegistrationNo == mrg.RegistrationNo
                    )
                );
            mrgs.LoadAll();

            // Filter PatientID
            var regs = new List<string>();
            foreach (var item in mrgs)
            {
                if (item.RegistrationNo != registrationNo)
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(item.RegistrationNo);
                    if (reg.PatientID!=patientID) continue;
                }
                regs.Add(item.RegistrationNo);
            }
            return regs;
        }
        public static string GetMergeBillingFrom(string RegistrationNo)
        {
            var mb = new BusinessObject.MergeBilling();
            if (mb.LoadByPrimaryKey(RegistrationNo))
            {
                return mb.FromRegistrationNo;
            }
            return string.Empty;
        }

        public static string[] GetMergeBillingByReg(string RegistrationNo)
        {
            var mrgs = new MergeBillingCollection();
                mrgs.Query.Where(mrgs.Query.FromRegistrationNo == RegistrationNo);
            if (mrgs.LoadAll())
            {
                var ret = mrgs.Select(m => m.RegistrationNo).ToList();
                ret.Add(RegistrationNo);
                return ret.ToArray();
            }
            else {
                return (new string[] { RegistrationNo });
            }
        }
    }
}
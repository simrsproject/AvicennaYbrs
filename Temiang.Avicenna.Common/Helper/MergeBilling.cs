using System.Linq;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Common
{
    public partial class Helper
    {
        public class MergeBilling
        {
            public static string[] GetMergeRegistration(string registrationNo)
            {
                return BusinessObject.MergeBilling.GetMergeRegistration(registrationNo);
            }

            public static string[] GetFullMergeRegistration(string registrationNo)
            {
                return BusinessObject.MergeBilling.GetFullMergeRegistration(registrationNo);
            }

            public static string GetMergeBillingFrom(string RegistrationNo) {
                return BusinessObject.MergeBilling.GetMergeBillingFrom(RegistrationNo);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Common
{
    public class AppCache
    {
        public static List<string> RelatedRegistrations(bool isPostBack, string registrationNo)
        {
            var cacheName = string.Format("regnos_{0}", registrationNo.Replace("/", ""));
            if (!isPostBack || HttpContext.Current.Cache[cacheName] == null)
            {
                //HttpContext.Current.Cache.Insert(cacheName, MergeBilling.GetFullMergeRegistration(registrationNo, patientID), null,
                //    DateTime.Now.AddSeconds(30), TimeSpan.Zero);
                HttpContext.Current.Cache.Insert(cacheName, Registration.RelatedRegistrations(registrationNo), null,
                    DateTime.Now.AddSeconds(60), TimeSpan.Zero);
            }

            return (List<string>)HttpContext.Current.Cache[cacheName];
        }
    }
}

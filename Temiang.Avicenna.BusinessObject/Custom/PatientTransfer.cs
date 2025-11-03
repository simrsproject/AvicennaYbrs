using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PatientTransfer
    {
        public static PatientTransfer LoadLastTransfer(string registrationNo, DateTime transferdate)
        {
            var pt = new PatientTransfer();
            pt.Query.es.Top = 1;
            pt.Query.Where(pt.Query.RegistrationNo == registrationNo, pt.Query.TransferDate <= transferdate);
            pt.Query.OrderBy(pt.Query.TransferDate.Descending);
            if (pt.Query.Load())
                return pt;
            return null;
        }
    }
}

using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransChargesItemCompCollection
    {
        public void GetTcicByNoRegWithMergeBilling(string RegistrationNo) {
            var registrationNoList = MergeBilling.GetMergeRegistration(RegistrationNo);
            GetTcicByNoRegWithMergeBilling(registrationNoList, false);
        }

        public void GetTcicByNoRegWithMergeBilling(string[] RegistrationNoList, bool IntermbilledOnly)
        {
            var tcic = new TransChargesItemCompQuery("tcic");
            var tci = new TransChargesItemQuery("tci");
            var tc = new TransChargesQuery("tc");
            tcic.InnerJoin(tci).On(tcic.TransactionNo == tci.TransactionNo && tcic.SequenceNo == tci.SequenceNo)
                .InnerJoin(tc).On(tci.TransactionNo == tc.TransactionNo && tc.RegistrationNo.In(RegistrationNoList))
                .Select(tcic);
            if (IntermbilledOnly)
            {
                var cc = new CostCalculationQuery("cc");
                tcic.InnerJoin(cc).On(tci.TransactionNo == cc.TransactionNo && tci.SequenceNo == cc.SequenceNo);
                tcic.Where(cc.IntermBillNo.IsNotNull());
            }
            this.Load(tcic);
        }
    }
}


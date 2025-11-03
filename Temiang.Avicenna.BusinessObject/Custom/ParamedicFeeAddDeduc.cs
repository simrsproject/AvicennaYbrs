using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicFeeAddDeduc
    {
        public string ParamedicFeeAdjustType
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemName", value); }
        }

        #region Approve

        public string Approv(string transactionNo, string userID, bool Approval)
        {
            return ApprovProcess(transactionNo, userID, Approval);
        }

        private static string ApprovProcess(string transactionNo, string userID, bool isApproval)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                var entity = new ParamedicFeeAddDeduc();
                if (entity.LoadByPrimaryKey(transactionNo))
                {
                    if (isApproval)
                    {
                        if (entity.IsApproved != null && entity.IsApproved.Value)
                            return "Approved";
                    }
                    else
                    {
                        if (!(entity.IsApproved ?? false))
                            return "UnApproved";
                        if (!string.IsNullOrEmpty(entity.VerificationNo))
                            return "Transaction has been verified";
                    }
                    entity.IsApproved = isApproval;
                    entity.LastUpdateDateTime = DateTime.Now;
                    entity.LastUpdatedByUserID = userID;

                    entity.Save();
                    trans.Complete();
                }
            }
            return string.Empty;
        }

        #endregion
    }

    public partial class ParamedicFeeAddDeducCollection
    {
        public bool GetReadyToBePaid(DateTime? paymentDateFrom, DateTime? paymentDateTo,
            string paramedicID, DateTime? dischargeDateFrom, DateTime? dischargeDateTo,
            string registrationNo, string medicalNo, string patientName, string paymentGroupNoDraft)
        {
            #region "Fee Additional Deduction"
            if (string.IsNullOrEmpty(registrationNo) && string.IsNullOrEmpty(medicalNo) && string.IsNullOrEmpty(patientName))
            {
                var deduc = new ParamedicFeeAddDeducQuery("deduc");
                var par = new ParamedicQuery("par");
                var ver = new ParamedicFeeVerificationQuery("ver");

                deduc.InnerJoin(ver).On(deduc.VerificationNo == ver.VerificationNo)
                    .InnerJoin(par).On(deduc.ParamedicID == par.ParamedicID)
                    .Where(deduc.IsApproved == true, deduc.PaymentGroupNo.IsNull(), deduc.VerificationNo.IsNotNull(),
                        ver.IsApproved == true
                    ).Select(deduc);

                if (!string.IsNullOrEmpty(paramedicID))
                {
                    deduc.Where(deduc.ParamedicID == paramedicID);
                }
                if (paymentDateFrom.HasValue)
                {
                    deduc.Where(deduc.TransactionDate.Date() >= paymentDateFrom.Value.Date);
                }
                if (paymentDateTo.HasValue)
                {
                    deduc.Where(deduc.TransactionDate.Date() <= paymentDateTo.Value.Date);
                }

                this.Load(deduc);

                if (!string.IsNullOrEmpty(paymentGroupNoDraft))
                {
                    deduc = new ParamedicFeeAddDeducQuery("deduc");
                    par = new ParamedicQuery("par");
                    ver = new ParamedicFeeVerificationQuery("ver");

                    deduc.InnerJoin(ver).On(deduc.VerificationNo == ver.VerificationNo)
                        .InnerJoin(par).On(deduc.ParamedicID == par.ParamedicID)
                        .Where(deduc.IsApproved == true, deduc.PaymentGroupNo == paymentGroupNoDraft, deduc.VerificationNo.IsNotNull(),
                            ver.IsApproved == true
                        ).Select(deduc);

                    if (!string.IsNullOrEmpty(paramedicID))
                    {
                        deduc.Where(deduc.ParamedicID == paramedicID);
                    }

                    var coll = new ParamedicFeeAddDeducCollection();
                    coll.Load(deduc);

                    this.Combine(coll);
                }

                return true;
            }
            else
            {
                return false;
            }

            //var adQ = new ParamedicFeeAddDeducQuery("a");
            //var padQ = new ParamedicQuery("b");

            //adQ.Select(adQ);

            //adQ.InnerJoin(padQ).On(adQ.ParamedicID.Equal(padQ.ParamedicID))
            //    .Where(
            //    adQ.IsApproved == true
            //);
            //adQ.Where(adQ.ParamedicID == paramedicID);
            //adQ.Where(adQ.VerificationNo.IsNotNull());
            //adQ.Where(adQ.PaymentGroupNo.IsNull());

            //adQ.OrderBy(
            //    adQ.ParamedicID.Ascending
            //);

            //return this.Load(adQ);
            #endregion
        }
    }
}

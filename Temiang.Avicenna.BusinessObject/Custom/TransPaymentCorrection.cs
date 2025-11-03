using System;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransPaymentCorrection
    {
        public void Approve(string UserID) {
            this.IsApproved = true;
            this.ApprovedByUserID = UserID;
            this.DateApproved = DateTime.Now;

            this.Save();
        }

        public void UnApprove(string UserID)
        {
            this.IsApproved = false;
            this.ApprovedByUserID = UserID;
            this.DateApproved = DateTime.Now;

            this.Save();
        }

        public void Void(string UserID)
        {
            this.IsVoid = true;
            this.VoidByUserID = UserID;
            this.DateVoid = DateTime.Now;

            this.Save();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class MembershipDetail
    {
        public decimal Balance
        {
            get { return (decimal)GetColumn("refToBalance"); }
            set { SetColumn("refToBalance", value); }
        }
        public string MedicalNo
        {
            get { return GetColumn("refToMedicalNo").ToString(); }
            set { SetColumn("refToMedicalNo", value); }
        }
        public string PatientName
        {
            get { return GetColumn("refToPatientName").ToString(); }
            set { SetColumn("refToPatientName", value); }
        }
        public string GuarantorName
        {
            get { return GetColumn("refToGuarantorName").ToString(); }
            set { SetColumn("refToGuarantorName", value); }
        }
        public string ServiceUnitName
        {
            get { return GetColumn("refToServiceUnitName").ToString(); }
            set { SetColumn("refToServiceUnitName", value); }
        }

        public static bool UpdateRewardPoints(Int64 id, decimal totPaymentAmt, decimal div, bool isPayment, string userId)
        {
            var md = new MembershipDetail();
            if (md.LoadByPrimaryKey(id))
            {
                var point = (totPaymentAmt / div).ToInt();
                var mod = totPaymentAmt % div;

                if (isPayment)
                {
                    md.TotalAmount += totPaymentAmt;
                    md.ReedeemAmount += (Convert.ToDecimal(point) * div);
                    md.BalanceAmount += mod;
                    md.RewardPoint += Convert.ToDecimal(point);
                }
                else
                {
                    md.TotalAmount -= totPaymentAmt;
                    md.ReedeemAmount -= (Convert.ToDecimal(point) * div);
                    md.BalanceAmount -= mod;
                    md.RewardPoint -= Convert.ToDecimal(point);
                }

                md.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                md.LastUpdateByUserID = userId;

                md.Save();
            }

            return true;
        }

        public static bool EmployeeRefferalRewardPoints(string membershipNo, string registrationNo, DateTime registrationDate, string guarantorId, 
            string appSelf, decimal appGeneral, decimal appGuarantee, string userId, bool isNew, string guarantorId2, string fromRegistrationNo)
        {
            var md = new MembershipDetail();
            if (isNew)
            {
                md.AddNew();

                md.MembershipNo = membershipNo;
                md.RegistrationNo = registrationNo;
                md.StartDate = registrationDate.Date;
                md.EndDate = registrationDate.Date;
                md.TotalAmount = 0;
                md.ReedeemAmount = 0;
                md.BalanceAmount = 0;
                md.RewardPoint = 0;

                //-hitung point refferal-
                var guar = new Guarantor();
                guar.LoadByPrimaryKey(guarantorId);
                if (guar.SRGuarantorType == appSelf)
                    md.RewardPointRefferal = appGeneral;
                else
                    md.RewardPointRefferal = appGuarantee;
                //-end-

                md.ClaimedPoint = 0;
                md.IsClosed = false;

                md.CreateDateTime = (new DateTime()).NowAtSqlServer();
                md.CreateByUserID = userId;

                md.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                md.LastUpdateByUserID = userId;

                if (!string.IsNullOrEmpty(fromRegistrationNo))
                {
                    var md2 = new MembershipDetail();
                    var md2q = new MembershipDetailQuery();
                    md2q.Where(md2q.MembershipNo == membershipNo, md2q.RegistrationNo == registrationNo);
                    if (md2.Load(md2q))
                    {
                        md2.RewardPointRefferal = 0;

                        md.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        md.LastUpdateByUserID = userId;

                        md2.Save();
                    }
                }
            }
            else
            {
                var mdq = new MembershipDetailQuery();
                mdq.Where(mdq.MembershipNo == membershipNo, mdq.RegistrationNo == registrationNo);
                md.Load(mdq);

                if (md.RewardPointRefferal > 0)
                {
                    //-hitung point refferal from-
                    var guar = new Guarantor();
                    guar.LoadByPrimaryKey(guarantorId);
                    if (guar.SRGuarantorType == appSelf)
                        md.RewardPointRefferal -= appGeneral;
                    else
                        md.RewardPointRefferal -= appGuarantee;

                    //-hitung point refferal to-
                    if (!string.IsNullOrEmpty(guarantorId2))
                    {
                        guar = new Guarantor();
                        guar.LoadByPrimaryKey(guarantorId2);
                        if (guar.SRGuarantorType == appSelf)
                            md.RewardPointRefferal += appGeneral;
                        else
                            md.RewardPointRefferal += appGuarantee;
                    }

                    md.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    md.LastUpdateByUserID = userId;
                }
            }

            md.Save();

            return true;
        }
        public static bool UpdateEmployeeRewardPoints(string membershipNo, string registrationNo, decimal totPaymentAmt, decimal div, bool isPayment, string userId)
        {
            var md = new MembershipDetail();
            var mdq = new MembershipDetailQuery();
            mdq.Where(mdq.MembershipNo == membershipNo, mdq.RegistrationNo == registrationNo);
            if (md.Load(mdq))
            {
                //-hitung point pembayaran-
                var point = (totPaymentAmt / div).ToInt();
                var mod = totPaymentAmt % div;

                if (isPayment)
                {
                    md.TotalAmount += totPaymentAmt;
                    md.ReedeemAmount += (Convert.ToDecimal(point) * div);
                    md.BalanceAmount += mod;
                    md.RewardPoint += Convert.ToDecimal(point);
                }
                else
                {
                    md.TotalAmount -= totPaymentAmt;
                    md.ReedeemAmount -= (Convert.ToDecimal(point) * div);
                    md.BalanceAmount -= mod;
                    md.RewardPoint -= Convert.ToDecimal(point);
                }
                //-end-

                md.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                md.LastUpdateByUserID = userId;

                md.Save();
            }

            return true;
        }

        public static bool UpdateRewardPointsBalanceAmount(Int64 id, decimal div, string userId)
        {
            var md = new MembershipDetail();
            if (md.LoadByPrimaryKey(id))
            {
                var point = (md.BalanceAmount / div).ToInt();
                var mod = md.BalanceAmount % div;

                md.ReedeemAmount += (Convert.ToDecimal(point) * div);
                md.BalanceAmount = mod;
                md.RewardPoint += Convert.ToDecimal(point);

                md.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                md.LastUpdateByUserID = userId;

                md.Save();
            }

            return true;
        }
    }
}

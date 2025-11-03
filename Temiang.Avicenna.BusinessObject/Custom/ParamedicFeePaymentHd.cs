using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.Interfaces;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicFeePaymentHd
    {
        #region void unvoid
        private static void VoidProcess(string paymentNo, string userID, bool isVoid)
        {
            ParamedicFeePaymentHd entity = new ParamedicFeePaymentHd();
            if (entity.LoadByPrimaryKey(paymentNo))
            {
                if (entity.IsVoid == true && isVoid) return;
                if (entity.IsVoid == false && !isVoid) return;

                //Lanjut
                entity.IsVoid = isVoid;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = userID;

                using (esTransactionScope trans = new esTransactionScope())
                {
                    entity.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        public void Void(string paymentNo, string userID)
        {
            VoidProcess(paymentNo, userID, true);
        }
        public void UnVoid(string paymentNo, string userID)
        {
            VoidProcess(paymentNo, userID, false);
        }
        #endregion

        #region Approve UnApprove

        public string Approv(string paymentNo, string userID)
        {
            return ApprovProcess(paymentNo, userID, true);
        }

        public string UnApprov(string paymentNo, string userID)
        {
            return ApprovProcess(paymentNo, userID, false);
        }

        private static string ApprovProcess(string paymentNo, string userID, bool isApproval)
        {
            if(string.IsNullOrEmpty(paymentNo)) return "Can not approve empty payment no / payment group no.";

            bool isJournalByDischargeDateByPaymentGroup = false;
            ParamedicFeePaymentGroup payG = new ParamedicFeePaymentGroup();

            bool isJournalByDischargeDate = false;
            ParamedicFeePaymentHd entity = new ParamedicFeePaymentHd();

            bool isJournalJadulBanget = false;

            using (esTransactionScope trans = new esTransactionScope())
            {
                if (payG.LoadByPrimaryKey(paymentNo)) {
                    if (isApproval) {
                        if (payG.IsApprove != null && payG.IsApprove.Value)
                            return "Approved";

                        if (payG.IsVoid != null && payG.IsVoid.Value)
                            return "Voided";
                    }
                    else
                    {
                        if (payG.IsApprove != null && !payG.IsApprove.Value)
                            return "UnApproved";
                    }
                    payG.IsApprove = isApproval;
                    payG.LastUpdateDateTime = DateTime.Now;
                    payG.LastUpdateByUserID = userID;

                    payG.Save();

                    if ((payG.IsDetail ?? 0) > 0)
                    {
                       
                    }
                    else {
                        // update detail
                        var payColl = new ParamedicFeePaymentHdCollection();
                        payColl.Query.Where(payColl.Query.PaymentGroupNo == payG.PaymentGroupNo);
                        if (payColl.LoadAll())
                        {
                            foreach (var pay in payColl)
                            {
                                pay.IsApproved = isApproval;
                                pay.LastUpdateDateTime = DateTime.Now;
                                pay.LastUpdateByUserID = userID;
                            }
                            payColl.Save();
                        }
                        else
                        {
                            return "Missing payment header";
                        }
                    }

                    AppParameter app = new AppParameter();
                    app.LoadByPrimaryKey("acc_IsAutoJournalFeePayment");

                    if (app.ParameterValue == "Yes")
                    {
                        /* Automatic Journal Testing Start */
                        var IsJournal = false;
                        var appprg = new AppProgram();
                        if (appprg.LoadByPrimaryKey("05.07.03"))
                        {
                            if (appprg.NavigateUrl.Trim().IndexOf("ParamedicFeeVerificationByDischargeDateList.aspx") > 0)
                            {
                                var closingperiod = payG.PaymentDate.Value;
                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod);
                                if (isClosingPeriod)
                                {
                                    return "Financial statements for period: " +
                                           string.Format("{0:MMMM-yyyy}", closingperiod) +
                                           " have been closed. Please contact the authorities.";
                                }

                                isJournalByDischargeDateByPaymentGroup = true;
                            }
                        }
                        /* Automatic Journal Testing End */
                    }
                }
                else
                {
                    if (entity.LoadByPrimaryKey(paymentNo))
                    {
                        if (isApproval)
                        {
                            if (entity.IsApproved != null && entity.IsApproved.Value)
                                return "Approved";

                            if (entity.IsVoid != null && entity.IsVoid.Value)
                                return "Voided";
                        }
                        else
                        {
                            if (entity.IsApproved != null && !entity.IsApproved.Value)
                                return "UnApproved";
                        }
                        entity.IsApproved = isApproval;
                        entity.LastUpdateDateTime = DateTime.Now;
                        entity.LastUpdateByUserID = userID;

                        entity.Save();
                    }

                    AppParameter app = new AppParameter();
                    app.LoadByPrimaryKey("acc_IsAutoJournalFeePayment");

                    if (app.ParameterValue == "Yes")
                    {
                        /* Automatic Journal Testing Start */
                        var IsJournal = false;
                        var appprg = new AppProgram();
                        if (appprg.LoadByPrimaryKey("05.07.03"))
                        {
                            if (appprg.NavigateUrl.Trim().IndexOf("ParamedicFeeVerificationByDischargeDateList.aspx") > 0)
                            {
                                var closingperiod = payG.PaymentDate.Value;
                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod);
                                if (isClosingPeriod)
                                {
                                    return "Financial statements for period: " +
                                           string.Format("{0:MMMM-yyyy}", closingperiod) +
                                           " have been closed. Please contact the authorities.";
                                }

                                isJournalByDischargeDate = true;
                                IsJournal = true;
                            }
                        }
                        if (!IsJournal)
                        {
                            var closingperiod = payG.PaymentDate.Value;
                            var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod);
                            if (isClosingPeriod)
                            {
                                return "Financial statements for period: " +
                                       string.Format("{0:MMMM-yyyy}", closingperiod) +
                                       " have been closed. Please contact the authorities.";
                            }

                            isJournalJadulBanget = true;
                        }
                        /* Automatic Journal Testing End */

                    }
                }

                trans.Complete();
            }

            if (isJournalByDischargeDateByPaymentGroup) {
                // paramedic fee based on discharge date
                if (isApproval)
                {
                    int? journalId = JournalTransactions.AddNewPhysicianPaymentJournalByDischargeDateByPaymentGroup(
                        BusinessObject.JournalType.PhysicianPayment, payG, userID, 0);
                }
                else
                {
                    int? journalId = JournalTransactions.AddNewPhysicianPaymentJournalByDischargeDateByPaymentGroupUnapproval(
                        BusinessObject.JournalType.PhysicianPayment, payG, userID, 0);
                }
            }

            if (isJournalByDischargeDate) {
                // paramedic fee based on discharge date
                if (isApproval)
                {
                    int? journalId = JournalTransactions.AddNewPhysicianPaymentJournalByDischargeDate(
                        BusinessObject.JournalType.PhysicianPayment, entity, userID, 0);
                }
                else
                {
                    int? journalId = JournalTransactions.AddNewPhysicianPaymentJournalByDischargeDateUnapproval(
                        BusinessObject.JournalType.PhysicianPayment, entity, userID, 0);
                }
            }

            if (isJournalJadulBanget) {
                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSSA")
                {
                    int? journalId = JournalTransactions.AddNewPhysicianPaymentJournal2(BusinessObject.JournalType.PhysicianPayment, entity, userID, 0);
                }
                else
                {
                    int? journalId = JournalTransactions.AddNewPhysicianPaymentJournal(BusinessObject.JournalType.PhysicianPayment, entity, userID, 0);
                }
            }

            return string.Empty;
        }
       
        #endregion
    }
}

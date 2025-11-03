using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class InvoiceCustomer
    {
        public string ReceivableStatusName
        {
            get { return GetColumn("refToAppStandardReference_ReceivableStatusName").ToString(); }
            set { SetColumn("refToAppStandardReference_ReceivableStatusName", value); }
        }

        #region void unvoid

        private static void VoidProcess(string invoiceNo, string userID, bool isVoid)
        {
            InvoiceCustomer entity = new InvoiceCustomer();
            if (entity.LoadByPrimaryKey(invoiceNo))
            {
                if (entity.IsVoid == true && isVoid) return;
                if (entity.IsVoid == false && !isVoid) return;

                //Lanjut
                entity.IsVoid = isVoid;
                if (isVoid)
                {
                    entity.VoidDate = DateTime.Now;
                    entity.VoidByUserID = userID;
                }
                else
                {
                    entity.str.VoidDate = string.Empty;
                    entity.str.VoidByUserID = string.Empty;
                }


                using (esTransactionScope trans = new esTransactionScope())
                {
                    entity.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        public void Void(string invoiceNo, string userID)
        {
            VoidProcess(invoiceNo, userID, true);
        }

        public void UnVoid(string invoiceNo, string userID)
        {
            VoidProcess(invoiceNo, userID, false);
        }

        #endregion

        #region Approve UnApprove

        private static string ApprovProcess(string invoiceNo, string userID, bool isApproval, string receivableStatus)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                InvoiceCustomer entity = new InvoiceCustomer();
                if (entity.LoadByPrimaryKey(invoiceNo))
                {
                    //Check status record
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
                    entity.str.ApprovedDate = isApproval ? DateTime.Now.ToString() : "";
                    entity.str.ApprovedByUserID = isApproval ? userID : "";
                    entity.SRReceivableStatus = isApproval ? receivableStatus : "";

                    if (!isApproval)
                    {
                        entity.IsVoid = true;
                        entity.VoidDate = DateTime.Now;
                        entity.VoidByUserID = userID;
                    }

                    entity.Save();

                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsAutoJournalARInvoicing).ToLower() == "yes")
                    {
                        if (isApproval)
                        {
                            var journalId = JournalTransactions.AddNewARCustomerInvoicingJournal2(entity, userID, 0);
                        }
                        else
                        {
                            var journalId = JournalTransactions.AddNewARCustomerInvoicingJournal2Unapproval(entity, userID, 0);
                        }
                    }
                    //Commit if success, Rollback if failed
                    trans.Complete();

                }
                else
                {
                    return "NotExist";
                }
            }
            return string.Empty;
        }

        public string Approv(string invoiceNo, string userID, string receivableStatus)
        {
            return ApprovProcess(invoiceNo, userID, true, receivableStatus);
        }

        public string UnApprov(string invoiceNo, string userID, string receivableStatus)
        {
            return ApprovProcess(invoiceNo, userID, false, receivableStatus);
        }

        public string PaymentApproved(string invoiceNo, InvoiceCustomerItemCollection invoicesItems, string userID)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                InvoiceCustomer entity = new InvoiceCustomer();
                if (entity.LoadByPrimaryKey(invoiceNo))
                {
                    if (!(entity.IsInvoicePayment ?? false))
                    {
                        return "Invoice payment number is invalid";
                    }
                    if (entity.IsApproved ?? false)
                    {
                        return "Invoice payment has been approved";
                    }

                    entity.IsApproved = true;
                    entity.str.ApprovedDate = DateTime.Now.ToString();
                    entity.str.ApprovedByUserID = userID;
                    entity.IsPaymentApproved = true;
                    entity.str.PaymentApprovedDate = DateTime.Now.ToString();
                    entity.str.PaymentByUserID = userID;
                    entity.str.PaymentApprovedByUserID = userID;
                    entity.LastUpdateByUserID = userID;
                    entity.LastUpdateDateTime = DateTime.Now;


                    var invColl = new InvoiceCustomerItemCollection();
                    invColl.Query.Where(invColl.Query.InvoiceNo.In(invoicesItems.Select(x => x.InvoiceReferenceNo)));
                    invColl.LoadAll();

                    System.Collections.Generic.List<string> OverPaymentNos = new System.Collections.Generic.List<string>();
                    foreach (var i in invoicesItems)
                    {
                        var ent = (from c in invColl
                                   where c.TransactionNo == i.TransactionNo && c.InvoiceNo == i.InvoiceReferenceNo
                                   select c).SingleOrDefault();
                        if (ent != null)
                        {
                            // if (((ent.PaymentAmount ?? 0) + i.PaymentAmount + i.OtherAmount) > ent.VerifyAmount) 
                            if (Math.Abs((ent.PaymentAmount ?? 0) + (ent.OtherAmount ?? 0) + (ent.BankCost ?? 0)) >= Math.Abs(ent.VerifyAmount ?? 0))
                            {
                                // kelebihan bayar, kemungkinan double bayar. abort aja
                                OverPaymentNos.Add(ent.TransactionNo);
                                continue;
                            }
                            ent.PaymentAmount = (ent.PaymentAmount ?? 0) + i.PaymentAmount;
                            ent.OtherAmount = (ent.OtherAmount ?? 0) + i.OtherAmount;
                            ent.BankCost = (ent.BankCost ?? 0) + i.BankCost;
                        }
                    }
                    if (OverPaymentNos.Count > 0)
                    {
                        string sOpn = string.Empty;
                        foreach (var opn in OverPaymentNos)
                        {
                            if (!sOpn.Equals(string.Empty)) sOpn += ", ";
                            sOpn += opn;
                        }
                        return "Over payment detected: " + sOpn + ". Payment process is aborted!";
                    }
                    invColl.Save();

                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsAutoJournalARPayment).ToLower() == "yes")
                    {
                        /* Automatic Journal Testing Start */

                        var appprmdate = new AppParameter();
                        DateTime jDate = DateTime.Now;
                        if (appprmdate.LoadByPrimaryKey("acc_JournalARPaymentDate"))
                            jDate = appprmdate.ParameterValue.ToString().Equals("0") ?
                                entity.PaymentDate.Value.Date : entity.PaymentApprovedDate.Value.Date;

                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(jDate);
                        if (isClosingPeriod)
                            return "Financial statements for period: " +
                                   string.Format("{0:MMMM-yyyy}", jDate) +
                                   " have been closed. Please contact the authorities.";

                        var journalId = JournalTransactions.AddNewARCustomerPaymentJournal(entity, userID, 0);

                        /* Automatic Journal Testing End */
                    }

                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();

                }
                else
                {
                    return "Data is Not Exist";
                }
            }
            return string.Empty;
        }

        public string PaymentUnApproved(string invoiceNo, InvoiceCustomerItemCollection invoicesItems, string userID)
        {
            // cek cash entrynya, sudah reconcile belum? kalau sudah recon jangan bisa diunapprove ya
            var ceColl = new CashTransactionCollection();
            ceColl.Query.Where(ceColl.Query.DocumentNumber == invoiceNo,
                ceColl.Query.IsPosted == true,
                ceColl.Query.IsVoid == false,
                ceColl.Query.IsCleared == true /*sudah recon*/);
            ceColl.LoadAll();
            if (ceColl.Count > 0) return "Cash transaction has been cleared, payment can not be unapproved!";

            using (esTransactionScope trans = new esTransactionScope())
            {
                var entity = new InvoiceCustomer();
                if (entity.LoadByPrimaryKey(invoiceNo))
                {
                    if (!(entity.IsApproved ?? false))
                    {
                        return "Data is already unapproved";
                    }

                    entity.IsApproved = false;
                    entity.str.ApprovedDate = string.Empty;
                    entity.str.ApprovedByUserID = string.Empty;
                    entity.IsPaymentApproved = false;
                    entity.str.PaymentApprovedDate = string.Empty;
                    entity.str.PaymentByUserID = string.Empty;
                    entity.str.PaymentApprovedByUserID = string.Empty;
                    entity.LastUpdateByUserID = userID;
                    entity.LastUpdateDateTime = DateTime.Now;


                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsAutoJournalARPayment).ToLower() == "yes")
                    {
                        /* Automatic Journal Testing Start */

                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(DateTime.Now);
                        if (isClosingPeriod)
                            return "Financial statements for period: " +
                                   string.Format("{0:MMMM-yyyy}", DateTime.Now) +
                                   " have been closed. Please contact the authorities.";

                        var journalId = JournalTransactions.AddNewARCustomerPaymentJournalUnapproval(entity, userID, 0);

                        /* Automatic Journal Testing End */
                    }

                    entity.Save();

                    var coll = new InvoiceCustomerItemCollection();
                    coll.Query.Where(coll.Query.InvoiceNo.In(invoicesItems.Select(x => x.InvoiceReferenceNo).Distinct().ToArray()));
                    coll.LoadAll();

                    foreach (var i in invoicesItems)
                    {
                        var ent = (from c in coll
                                   where c.TransactionNo == i.TransactionNo
                                   select c).SingleOrDefault();
                        if (ent != null)
                        {
                            ent.PaymentAmount = (ent.PaymentAmount ?? 0) - i.PaymentAmount;
                            if (ent.PaymentAmount < 0)
                                ent.PaymentAmount = 0;
                            ent.OtherAmount = (ent.OtherAmount ?? 0) - i.OtherAmount;
                            if (ent.OtherAmount < 0)
                                ent.OtherAmount = 0;
                            ent.BankCost = (ent.BankCost ?? 0) - i.BankCost;
                            if (ent.BankCost < 0)
                                ent.BankCost = 0;
                        }
                    }
                    coll.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();

                }
                else
                {
                    return "NotExist";
                }
            }
            return string.Empty;
        }

        public string PaymentVoid(string invoiceNo, string userID)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                InvoiceCustomer entity = new InvoiceCustomer();
                if (entity.LoadByPrimaryKey(invoiceNo))
                {
                    entity.IsVoid = true;
                    entity.str.VoidDate = DateTime.Now.ToString();
                    entity.str.VoidByUserID = userID;
                    entity.LastUpdateByUserID = userID;
                    entity.LastUpdateDateTime = DateTime.Now;

                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
                else
                {
                    return "NotExist";
                }
            }
            return string.Empty;
        }

        public string WriteOffApproved(string invoiceNo, InvoiceCustomerItemCollection invoicesItems, string userID)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                InvoiceCustomer entity = new InvoiceCustomer();
                if (entity.LoadByPrimaryKey(invoiceNo))
                {
                    entity.IsApproved = true;
                    entity.str.ApprovedDate = DateTime.Now.ToString();
                    entity.str.ApprovedByUserID = userID;
                    entity.IsPaymentApproved = true;
                    entity.str.PaymentApprovedDate = DateTime.Now.ToString();
                    entity.str.PaymentByUserID = userID;
                    entity.str.PaymentApprovedByUserID = userID;
                    entity.LastUpdateByUserID = userID;
                    entity.LastUpdateDateTime = DateTime.Now;

                    entity.Save();

                    var coll = new InvoiceCustomerItemCollection();
                    coll.Query.Where(coll.Query.InvoiceNo == entity.InvoiceReferenceNo);
                    coll.LoadAll();

                    foreach (var i in invoicesItems)
                    {
                        var ent = (from c in coll
                                   where c.TransactionNo == i.TransactionNo
                                   select c).SingleOrDefault();
                        if (ent != null)
                        {
                            ent.PaymentAmount = (ent.PaymentAmount ?? 0) + i.PaymentAmount;
                            ent.OtherAmount = (ent.OtherAmount ?? 0) + i.OtherAmount;
                        }
                    }

                    coll.Save();

                    /* Automatic Journal Testing Start */

                    //var journalId = JournalTransactions.WriteOffARJournal(entity, userID);

                    /* Automatic Journal Testing End */

                    //Commit if success, Rollback if failed
                    trans.Complete();

                }
                else
                {
                    return "NotExist";
                }
            }
            return string.Empty;
        }


        #endregion
    }
}

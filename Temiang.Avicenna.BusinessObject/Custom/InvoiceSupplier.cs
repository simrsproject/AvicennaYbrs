using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class InvoiceSupplier
    {
        #region - GRID LIST -

        public string SupplierName
        {
            get
            {
                string retVal = "";
                retVal = this.GetColumn("SupplierName") is System.DBNull ? "" : (string)this.GetColumn("SupplierName");
                return retVal;
            }
        }

        public decimal Total
        {
            get
            {
                decimal retVal = 0;
                retVal = this.GetColumn("Total") is System.DBNull ? 0 : (decimal)this.GetColumn("Total");
                return retVal;
            }
        }
        public string PayableStatus
        {
            get
            {
                string retVal = "";
                retVal = this.GetColumn("PayableStatus") is System.DBNull ? "" : (string)this.GetColumn("PayableStatus");
                return retVal;
            }
        }

        #region - Creating Invoice -
        public static int TotalCount(string invoiceNo, DateTime? invoiceDate, DateTime? invoiceDueDate, string supplierName, string invoiceSuppNo,
            string receivedNo, string purchaseOrderNo)
        {
            int retVal = 0;

            var h = new InvoiceSupplierQuery("h");

            h.Select("<COUNT(DISTINCT h.InvoiceNo) AS Count>");

            // where 
            List<esComparison> prms = new List<esComparison>();

            if (!string.IsNullOrEmpty(invoiceNo))
            {
                string searchTextContain = string.Format("{0}%", invoiceNo);
                prms.Add(h.InvoiceNo.Like(searchTextContain));
            }
            if (invoiceDate.HasValue)
            {
                prms.Add(h.InvoiceDate.GreaterThanOrEqual(invoiceDate.Value));
                prms.Add(h.InvoiceDate.LessThan(invoiceDate.Value.AddDays(1)));
            }
            if (invoiceDueDate.HasValue)
            {
                prms.Add(h.InvoiceDueDate.GreaterThanOrEqual(invoiceDueDate.Value));
                prms.Add(h.InvoiceDueDate.LessThan(invoiceDueDate.Value.AddDays(1)));
            }
            if (!string.IsNullOrEmpty(supplierName))
            {
                var s = new SupplierQuery("g");
                h.InnerJoin(s).On(s.SupplierID == h.SupplierID);

                string searchTextContain = string.Format("%{0}%", supplierName);
                prms.Add(s.SupplierName.Like(searchTextContain));
            }
            if (!string.IsNullOrEmpty(invoiceSuppNo) || !string.IsNullOrEmpty(receivedNo) || !string.IsNullOrEmpty(purchaseOrderNo))
            {
                var dtColl = new InvoiceSupplierItemCollection();
                var dtq = new InvoiceSupplierItemQuery("dt");

                if (!string.IsNullOrEmpty(invoiceSuppNo) || !string.IsNullOrEmpty(purchaseOrderNo))
                {
                    var itq = new ItemTransactionQuery("it");
                    dtq.InnerJoin(itq).On(itq.TransactionNo == dtq.TransactionNo && itq.TransactionCode == "040");

                    if (!string.IsNullOrEmpty(invoiceSuppNo))
                    {
                        string searchTextContain = string.Format("{0}%", invoiceSuppNo);
                        dtq.Where(itq.InvoiceNo.Like(searchTextContain));
                    }

                    if (!string.IsNullOrEmpty(purchaseOrderNo))
                    {
                        string searchTextContain;
                        if (purchaseOrderNo.ToUpper().Contains("PO"))
                        {
                            searchTextContain = string.Format("{0}%", purchaseOrderNo);
                        }
                        else
                        {
                            searchTextContain = string.Format("%{0}%", purchaseOrderNo);
                        }

                        dtq.Where(itq.ReferenceNo.Like(searchTextContain));
                    }
                }

                if (!string.IsNullOrEmpty(receivedNo))
                {
                    string searchTextContain;
                    if (receivedNo.ToUpper().Contains("POR/"))
                    {
                        searchTextContain = string.Format("{0}%", receivedNo);
                    }
                    else
                    {
                        searchTextContain = string.Format("%{0}%", receivedNo);
                    }
                    dtq.Where(dtq.TransactionNo.Like(searchTextContain));
                }

                dtq.Select(dtq.InvoiceNo);
                dtq.es.Distinct = true;
                dtColl.Load(dtq);
                if (dtColl.Count > 0)
                {
                    //h.Where(h.InvoiceNo.In(dtColl.Select(dt => dt.InvoiceNo)));
                    prms.Add(h.InvoiceNo.In(dtColl.Select(dt => dt.InvoiceNo)));
                }
            }

            h.Where(h.IsInvoicePayment == false, h.IsConsignment == false);

            if (prms.Count > 0)
                h.Where(prms.ToArray());
            else
                h.Where(h.InvoiceDate == DateTime.Now.Date);

            h.es2.Connection.CommandTimeout = 600;

            var dtb = h.LoadDataTable();
            retVal = (int)dtb.Rows[0][0];
            return retVal;
        }

        public static InvoiceSupplierCollection GetAllWithPaging(int pageNumber, int pageSize, string invoiceNo, DateTime? invoiceDate, DateTime? invoiceDueDate, string supplierName, string invoiceSuppNo,
            string receivedNo, string purchaseOrderNo, string sortString)
        {
            var h = new InvoiceSupplierQuery("h");
            var s = new SupplierQuery("s");
            var st = new AppStandardReferenceItemQuery("st");
            h.InnerJoin(s).On(s.SupplierID == h.SupplierID);
            h.LeftJoin(st).On(st.StandardReferenceID == "PayableStatus" && st.ItemID == h.SRPayableStatus);

            h.Select(
                h.InvoiceNo, h.InvoiceDate, h.InvoiceDueDate, s.SupplierName, h.InvoiceSuppNo, "<CAST(0 AS DECIMAL) Total>",
                st.ItemName.As("PayableStatus"), h.IsApproved, h.IsVoid);

            // where
            List<esComparison> prms = new List<esComparison>();

            if (!string.IsNullOrEmpty(invoiceNo))
            {
                string searchTextContain = string.Format("{0}%", invoiceNo);
                prms.Add(h.InvoiceNo.Like(searchTextContain));
            }
            if (invoiceDate.HasValue)
            {
                prms.Add(h.InvoiceDate.GreaterThanOrEqual(invoiceDate.Value));
                prms.Add(h.InvoiceDate.LessThan(invoiceDate.Value.AddDays(1)));
            }
            if (invoiceDueDate.HasValue)
            {
                prms.Add(h.InvoiceDueDate.GreaterThanOrEqual(invoiceDueDate.Value));
                prms.Add(h.InvoiceDueDate.LessThan(invoiceDueDate.Value.AddDays(1)));
            }
            if (!string.IsNullOrEmpty(supplierName))
            {
                string searchTextContain = string.Format("%{0}%", supplierName);
                prms.Add(s.SupplierName.Like(searchTextContain));
            }
            if (!string.IsNullOrEmpty(invoiceSuppNo) || !string.IsNullOrEmpty(receivedNo) || !string.IsNullOrEmpty(purchaseOrderNo))
            {
                var dtColl = new InvoiceSupplierItemCollection();
                var dtq = new InvoiceSupplierItemQuery("dt");

                if (!string.IsNullOrEmpty(invoiceSuppNo) || !string.IsNullOrEmpty(purchaseOrderNo))
                {
                    var itq = new ItemTransactionQuery("it");
                    dtq.InnerJoin(itq).On(itq.TransactionNo == dtq.TransactionNo && itq.TransactionCode == "040");

                    if (!string.IsNullOrEmpty(invoiceSuppNo))
                    {
                        string searchTextContain = string.Format("{0}%", invoiceSuppNo);
                        dtq.Where(itq.InvoiceNo.Like(searchTextContain));
                    }

                    if (!string.IsNullOrEmpty(purchaseOrderNo))
                    {
                        string searchTextContain;
                        if (purchaseOrderNo.ToUpper().Contains("PO"))
                        {
                            searchTextContain = string.Format("{0}%", purchaseOrderNo);
                        }
                        else
                        {
                            searchTextContain = string.Format("%{0}%", purchaseOrderNo);
                        }

                        dtq.Where(itq.ReferenceNo.Like(searchTextContain));
                    }
                }

                if (!string.IsNullOrEmpty(receivedNo))
                {
                    string searchTextContain;
                    if (receivedNo.ToUpper().Contains("POR/"))
                    {
                        searchTextContain = string.Format("{0}%", receivedNo);
                    }
                    else
                    {
                        searchTextContain = string.Format("%{0}%", receivedNo);
                    }
                    dtq.Where(dtq.TransactionNo.Like(searchTextContain));
                }

                dtq.Select(dtq.InvoiceNo);
                dtq.es.Distinct = true;
                dtColl.Load(dtq);
                if (dtColl.Count > 0)
                {
                    //h.Where(h.InvoiceNo.In(dtColl.Select(dt => dt.InvoiceNo)));
                    prms.Add(h.InvoiceNo.In(dtColl.Select(dt => dt.InvoiceNo)));
                }
            }

            h.Where(h.IsInvoicePayment == false, h.IsConsignment == false);

            if (prms.Count > 0)
                h.Where(prms.ToArray());
            else
                h.Where(h.InvoiceDate == DateTime.Now.Date);

            h.OrderBy(safeOrderByItems(sortString, h));
            //h.OrderBy(h.InvoiceNo.Descending);

            //h.es.WithNoLock = true;
            h.es.PageSize = pageSize;
            h.es.PageNumber = pageNumber + 1;
            h.es2.Connection.CommandTimeout = 600;

            var coll = new InvoiceSupplierCollection();
            coll.Load(h);

            if (coll.Count > 0)
            {
                var dtColl = new InvoiceSupplierItemCollection();
                dtColl.Query.Where(dtColl.Query.InvoiceNo.In(coll.Select(x => x.InvoiceNo)));
                dtColl.Query.Select(
                    dtColl.Query.InvoiceNo,
                    dtColl.Query.Amount,
                    dtColl.Query.PPnAmount,
                    dtColl.Query.StampAmount,
                    dtColl.Query.DownPaymentAmount,
                    dtColl.Query.OtherDeduction,
                    dtColl.Query.PphAmount
                    );
                dtColl.LoadAll();

                foreach (var inv in coll)
                {
                    var dt = dtColl.Where(x => x.InvoiceNo == inv.InvoiceNo);
                    if (dt.Any())
                    {
                        inv.SetColumn("Total", dt.Sum(x => (x.Amount ?? 0) + (x.PPnAmount ?? 0) + (x.StampAmount ?? 0) - (x.DownPaymentAmount ?? 0) - (x.OtherDeduction ?? 0) - (x.PphAmount ?? 0)));
                    }

                    if ((inv.IsApproved ?? false) && !(inv.IsVoid ?? false))
                    {
                        var count = (new InvoiceSupplierCollection()).ItemTransactionOutstandingByInvoiceNo(inv.InvoiceNo);
                        if (count.Rows.Count == 0)
                            inv.SetColumn("PayableStatus", "Paid");
                    }
                }
            }

            return coll;
        }

        private static esOrderByItem[] safeOrderByItems(string sortString, InvoiceSupplierQuery q)
        {
            List<esOrderByItem> list = new List<esOrderByItem>();
            string[] fieldsName = sortString.ToLowerInvariant().Split(char.Parse(","));
            foreach (string field in fieldsName)
            {
                string[] tmp = field.Split(char.Parse("^"));
                bool isDesc = false;
                if (tmp.Length > 1)
                    isDesc = tmp[1].Equals("descending");

                if (tmp[0].Equals("invoiceno"))
                    list.Add(isDesc ? q.InvoiceNo.Descending : q.InvoiceNo.Ascending);
                if (tmp[0].Equals("invoicedate"))
                    list.Add(isDesc ? q.InvoiceDate.Descending : q.InvoiceDate.Ascending);
                if (tmp[0].Equals("invoiceduedate"))
                    list.Add(isDesc ? q.InvoiceDueDate.Descending : q.InvoiceDueDate.Ascending);
            }
            if (list.Count == 0) list.Add(q.InvoiceNo.Descending);
            return list.ToArray();
        }
        #endregion

        #region - Payment Receive -
        public static int TotalPaymentCount(string invoiceNo, DateTime? invoiceDate, DateTime? invoiceDateTo, string invoiceReferenceNo, string supplierName,
            string invoiceSuppNo, string status)
        {
            int retVal = 0;

            var h = new InvoiceSupplierQuery("h");

            h.Select("<COUNT(DISTINCT h.InvoiceNo) AS Count>");

            // where 
            List<esComparison> prms = new List<esComparison>();

            if (!string.IsNullOrEmpty(invoiceNo))
            {
                string searchTextContain = string.Format("{0}%", invoiceNo);
                prms.Add(h.InvoiceNo.Like(searchTextContain));
            }
            if (invoiceDate.HasValue && invoiceDateTo.HasValue)
            {
                prms.Add(h.InvoiceDate.GreaterThanOrEqual(invoiceDate.Value));
                prms.Add(h.InvoiceDate.LessThan(invoiceDateTo.Value.AddDays(1)));
            }
            if (!string.IsNullOrEmpty(invoiceReferenceNo))
            {
                string searchTextContain;

                if (invoiceReferenceNo.ToUpper().Contains("AP-"))
                {
                    searchTextContain = string.Format("{0}%", invoiceReferenceNo);
                }
                else
                {
                    searchTextContain = string.Format("%{0}%", invoiceReferenceNo);
                }

                prms.Add(h.InvoiceReferenceNo.Like(invoiceReferenceNo));
            }
            if (!string.IsNullOrEmpty(supplierName))
            {
                var s = new SupplierQuery("s");
                h.InnerJoin(s).On(s.SupplierID == h.SupplierID);

                string searchTextContain = string.Format("%{0}%", supplierName);
                prms.Add(s.SupplierName.Like(searchTextContain));
            }
            if (!string.IsNullOrEmpty(invoiceSuppNo))
            {
                var dtColl = new InvoiceSupplierItemCollection();
                var dtq = new InvoiceSupplierItemQuery("dt");
                var itq = new ItemTransactionQuery("it");
                dtq.InnerJoin(itq).On(itq.TransactionNo == dtq.TransactionNo && itq.TransactionCode == "040");

                string searchTextContain = string.Format("{0}%", invoiceSuppNo);
                dtq.Where(itq.InvoiceNo.Like(searchTextContain));

                dtq.Select(dtq.InvoiceNo);
                dtq.es.Distinct = true;
                dtColl.Load(dtq);
                if (dtColl.Count > 0)
                {
                    //h.Where(h.InvoiceNo.In(dtColl.Select(dt => dt.InvoiceNo)));
                    prms.Add(h.InvoiceNo.In(dtColl.Select(dt => dt.InvoiceNo)));
                }
            }
            switch (status)
            {
                case "0":
                    prms.Add(h.IsApproved == true);
                    break;
                case "1":
                    prms.Add(h.IsApproved != true);
                    prms.Add(h.IsVoid != true);
                    //prms.Add(h.Or(h.IsApproved.IsNull(), h.IsApproved == false), h.Or(h.IsVoid.IsNull(), h.IsVoid == false));
                    break;
                case "2":
                    prms.Add(h.IsVoid == true);
                    break;
            }

            h.Where(h.IsInvoicePayment == true, h.IsWriteOff == false, h.IsAddPayment.IsNull());

            if (prms.Count > 0)
                h.Where(prms.ToArray());
            else
                h.Where(h.InvoiceDate == DateTime.Now.Date);

            h.es2.Connection.CommandTimeout = 600;

            var dtb = h.LoadDataTable();
            retVal = (int)dtb.Rows[0][0];
            return retVal;
        }

        public static InvoiceSupplierCollection GetAllPaymentWithPaging(int pageNumber, int pageSize, string invoiceNo, DateTime? invoiceDate, DateTime? invoiceDateTo, string invoiceReferenceNo, string supplierName,
            string invoiceSuppNo, string status, string sortString)
        {
            var h = new InvoiceSupplierQuery("h");
            var s = new SupplierQuery("s");
            h.InnerJoin(s).On(s.SupplierID == h.SupplierID);

            h.Select(
                h.InvoiceNo, h.InvoiceDate, h.InvoiceReferenceNo, s.SupplierName, "<CAST(0 AS DECIMAL) Total>",
                h.IsApproved, h.IsVoid);

            // where
            List<esComparison> prms = new List<esComparison>();

            if (!string.IsNullOrEmpty(invoiceNo))
            {
                string searchTextContain = string.Format("{0}%", invoiceNo);
                prms.Add(h.InvoiceNo.Like(searchTextContain));
            }
            if (invoiceDate.HasValue && invoiceDateTo.HasValue)
            {
                prms.Add(h.InvoiceDate.GreaterThanOrEqual(invoiceDate.Value));
                prms.Add(h.InvoiceDate.LessThan(invoiceDateTo.Value.AddDays(1)));
            }
            //if (!string.IsNullOrEmpty(invoiceReferenceNo))
            //{
            //    string searchTextContain;

            //    if (invoiceReferenceNo.ToUpper().Contains("AP-"))
            //    {
            //        searchTextContain = string.Format("{0}%", invoiceReferenceNo);
            //    }
            //    else
            //    {
            //        searchTextContain = string.Format("%{0}%", invoiceReferenceNo);
            //    }

            //    prms.Add(h.InvoiceReferenceNo.Like(invoiceReferenceNo));
            //}
            if (!string.IsNullOrEmpty(invoiceReferenceNo))
            {
                var dtColl = new InvoiceSupplierItemCollection();
                var dtq = new InvoiceSupplierItemQuery("dt");

                dtq.Where(dtq.InvoiceReferenceNo == invoiceReferenceNo);

                dtq.Select(dtq.InvoiceNo);
                dtq.es.Distinct = true;
                dtColl.Load(dtq);

                if (dtColl.Count > 0)
                {
                    prms.Add(h.InvoiceNo.In(dtColl.Select(dt => dt.InvoiceNo)));
                }
            }
            if (!string.IsNullOrEmpty(supplierName))
            {
                string searchTextContain = string.Format("%{0}%", supplierName);
                prms.Add(s.SupplierName.Like(searchTextContain));
            }
            if (!string.IsNullOrEmpty(invoiceSuppNo))
            {
                var dtColl = new InvoiceSupplierItemCollection();
                var dtq = new InvoiceSupplierItemQuery("dt");
                var itq = new ItemTransactionQuery("it");
                dtq.InnerJoin(itq).On(itq.TransactionNo == dtq.TransactionNo && itq.TransactionCode == "040");

                string searchTextContain = string.Format("{0}%", invoiceSuppNo);
                dtq.Where(itq.InvoiceNo.Like(searchTextContain));

                dtq.Select(dtq.InvoiceNo);
                dtq.es.Distinct = true;
                dtColl.Load(dtq);
                if (dtColl.Count > 0)
                {
                    //h.Where(h.InvoiceNo.In(dtColl.Select(dt => dt.InvoiceNo)));
                    prms.Add(h.InvoiceNo.In(dtColl.Select(dt => dt.InvoiceNo)));
                }
            }
            switch (status)
            {
                case "0":
                    prms.Add(h.IsApproved == true);
                    break;
                case "1":
                    prms.Add(h.IsApproved != true);
                    prms.Add(h.IsVoid != true);
                    //prms.Add(h.Or(h.IsApproved.IsNull(), h.IsApproved == false), h.Or(h.IsVoid.IsNull(), h.IsVoid == false));
                    break;
                case "2":
                    prms.Add(h.IsVoid == true);
                    break;
            }

            h.Where(h.IsInvoicePayment == true, h.IsWriteOff == false, h.IsAddPayment.IsNull());

            if (prms.Count > 0)
                h.Where(prms.ToArray());
            else
                h.Where(h.InvoiceDate == DateTime.Now.Date);

            h.OrderBy(safeOrderPaymentByItems(sortString, h));
            //h.OrderBy(h.InvoiceNo.Descending);

            //h.es.WithNoLock = true;
            h.es.PageSize = pageSize;
            h.es.PageNumber = pageNumber + 1;
            h.es2.Connection.CommandTimeout = 600;

            var coll = new InvoiceSupplierCollection();
            coll.Load(h);

            if (coll.Count > 0)
            {
                var dtColl = new InvoiceSupplierItemCollection();
                dtColl.Query.Where(dtColl.Query.InvoiceNo.In(coll.Select(x => x.InvoiceNo)));
                dtColl.Query.Select(
                    dtColl.Query.InvoiceNo,
                    dtColl.Query.PaymentAmount.Sum(),
                    dtColl.Query.TransactionNo.Count()
                    );
                dtColl.Query.GroupBy(dtColl.Query.InvoiceNo);
                dtColl.LoadAll();

                foreach (var inv in coll)
                {
                    var dt = dtColl.Where(x => x.InvoiceNo == inv.InvoiceNo);
                    if (dt.Any())
                    {
                        inv.SetColumn("Total", dt.Sum(x => x.PaymentAmount));
                    }
                }
            }

            return coll;
        }

        private static esOrderByItem[] safeOrderPaymentByItems(string sortString, InvoiceSupplierQuery q)
        {
            List<esOrderByItem> list = new List<esOrderByItem>();
            string[] fieldsName = sortString.ToLowerInvariant().Split(char.Parse(","));
            foreach (string field in fieldsName)
            {
                string[] tmp = field.Split(char.Parse("^"));
                bool isDesc = false;
                if (tmp.Length > 1)
                    isDesc = tmp[1].Equals("descending");

                if (tmp[0].Equals("invoiceno"))
                    list.Add(isDesc ? q.InvoiceNo.Descending : q.InvoiceNo.Ascending);
                if (tmp[0].Equals("invoicedate"))
                    list.Add(isDesc ? q.InvoiceDate.Descending : q.InvoiceDate.Ascending);
            }
            if (list.Count == 0) list.Add(q.InvoiceNo.Descending);
            return list.ToArray();
        }
        #endregion

        #endregion

        public string PayableStatusName
        {
            get { return GetColumn("refToAppStandardReference_PayableStatusName").ToString(); }
            set { SetColumn("refToAppStandardReference_PayableStatusName", value); }
        }


        #region void unvoid
        private static void VoidProcess(string invoiceNo, string userID, bool isVoid)
        {
            InvoiceSupplier entity = new InvoiceSupplier();
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
        private static string ApprovProcess(string invoiceNo, string userID, bool isApproval, string payableStatus, bool isConsignment)
        {

            var entity = new InvoiceSupplier();
            if (entity.LoadByPrimaryKey(invoiceNo))
            {
                using (esTransactionScope trans = new esTransactionScope())
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
                    // unapproval jangan hilangin tanggal approval, cukup main di flag saja
                    if (isApproval)
                    {
                        entity.str.ApprovedDate = DateTime.Now.ToString();
                    }

                    entity.str.ApprovedByUserID = isApproval ? userID : "";
                    entity.SRPayableStatus = isApproval ? payableStatus : "";

                    entity.Save();

                    if (isConsignment)
                    {
                        var consignmentColl = new InvoiceSupplierItemConsignmentCollection();
                        consignmentColl.Query.Where(consignmentColl.Query.InvoiceNo == invoiceNo);
                        consignmentColl.LoadAll();
                        foreach (var coll in consignmentColl)
                        {
                            var tx = new ItemTransactionItem();
                            tx.LoadByPrimaryKey(coll.TransactionNo, coll.SequenceNo);
                            if (isApproval)
                                tx.QuantityFinishInBaseUnit += coll.Qty;
                            else
                                tx.QuantityFinishInBaseUnit -= coll.Qty;
                            tx.Save();
                        }
                    }

                    trans.Complete();
                }
            }
            else
            {
                return "NotExist";
            }

            // jurnal
            if (isConsignment)
            {
                AppParameter app = new AppParameter();
                app.LoadByPrimaryKey("acc_IsAutoJournalPOReceived");
                if (app.ParameterValue.ToLower() == "yes")
                {
                    var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.InvoiceDate.Value);
                    if (isClosingPeriod)
                        return "Financial statements for period: " +
                               string.Format("{0:MMMM-yyyy}", entity.InvoiceDate) +
                               " have been closed. Please contact the authorities.";

                    if (isApproval)
                    {
                        int? journalId = JournalTransactions.AddNewConsignmentInvoicingJournal(entity, userID);
                    }
                    else
                    {
                        int? journalId = JournalTransactions.AddNewConsignmentInvoicingVoidJournal(entity, userID);
                    }
                }
            }
            else
            {
                var PrmApUsingInvoicing = new AppParameter();
                if (PrmApUsingInvoicing.LoadByPrimaryKey("acc_IsJournalApUsingInvoicing"))
                {
                    if (PrmApUsingInvoicing.ParameterValue.ToLower() == "yes")
                    {
                        if (isApproval)
                        {
                            DateTime jDate = DateTime.Now;
                            PrmApUsingInvoicing = new AppParameter();
                            if (PrmApUsingInvoicing.LoadByPrimaryKey("acc_JournalAPInvoicingDate"))
                                jDate = PrmApUsingInvoicing.ParameterValue.ToString().Equals("0") ?
                                    entity.InvoiceDate.Value.Date : entity.ApprovedDate.Value.Date;

                            var isClosingPeriod = PostingStatus.IsPeriodeClosed(jDate);
                            if (isClosingPeriod)
                                return "Financial statements for period: " +
                                       string.Format("{0:MMMM-yyyy}", jDate) +
                                       " have been closed. Please contact the authorities.";

                            int? journalId = JournalTransactions.AddNewAPInvoicingJournal2(entity, userID, 0);
                        }
                        else
                        {
                            DateTime jDate = DateTime.Now;
                            var isClosingPeriod = PostingStatus.IsPeriodeClosed(jDate);
                            if (isClosingPeriod)
                                return "Financial statements for period: " +
                                       string.Format("{0:MMMM-yyyy}", jDate) +
                                       " have been closed. Please contact the authorities.";
                            // belum jadi dipakai,
                            // solusi skrng: unapproval harus tidak diperbolehkan jika journal sudah ada, jadi harus void jurnal dulu (solusi untuk RSUI)
                            int? journalId = JournalTransactions.AddNewAPInvoicingJournal2Unapproval(entity, userID, 0);
                        }
                    }
                }
                //}
            }

            return string.Empty;
        }

        public string Approv(string invoiceNo, string userID, string payableStatus, bool isConsignment)
        {
            return ApprovProcess(invoiceNo, userID, true, payableStatus, isConsignment);
        }

        public string UnApprov(string invoiceNo, string userID, string payableStatus, bool isConsignment)
        {
            return ApprovProcess(invoiceNo, userID, false, payableStatus, isConsignment);
        }

        public string PaymentApproved(string invoiceNo, InvoiceSupplierItemCollection invoicesItems, string userID)
        {
            bool createJournal = false;
            var entity = new InvoiceSupplier();
            using (esTransactionScope trans = new esTransactionScope())
            {
                if (entity.LoadByPrimaryKey(invoiceNo))
                {
                    entity.IsApproved = true;
                    entity.str.ApprovedDate = DateTime.Now.ToString();
                    entity.str.ApprovedByUserID = userID;
                    entity.IsPaymentApproved = true;
                    entity.str.PaymentApprovedDateTime = DateTime.Now.ToString();
                    entity.str.PaymentApprovedByUserID = userID;
                    entity.LastUpdateByUserID = userID;
                    entity.LastUpdateDateTime = DateTime.Now;
                    entity.Save();

                    var coll = new InvoiceSupplierItemCollection();
                    //coll.Query.Where(coll.Query.InvoiceNo == entity.InvoiceReferenceNo);
                    coll.Query.Where(coll.Query.InvoiceNo.In(invoicesItems.Select(x => x.InvoiceReferenceNo ?? string.Empty)));
                    coll.LoadAll();

                    ////
                    System.Collections.Generic.List<string> OverPaymentNos = new System.Collections.Generic.List<string>();
                    foreach (var i in invoicesItems)
                    {
                        var ent = (from c in coll
                                   where c.TransactionNo == i.TransactionNo
                                   select c).SingleOrDefault();
                        if (ent != null)
                        {
                            // if (((ent.PaymentAmount ?? 0) + i.PaymentAmount + i.OtherAmount) > ent.VerifyAmount) 
                            if (!(ent.VerifyAmount == 0 && ent.DownPaymentAmount > 0)) //dp 100%
                            {
                                if (ent.VerifyAmount >= 0)
                                {
                                    if ((ent.PaymentAmount) >= ent.VerifyAmount)
                                    {
                                        // kelebihan bayar, kemungkinan double bayar. abort aja --> POR
                                        OverPaymentNos.Add(ent.TransactionNo);
                                        continue;
                                    }
                                }
                                else
                                {
                                    if ((ent.PaymentAmount) <= ent.VerifyAmount)
                                    {
                                        // kelebihan bayar, kemungkinan double bayar. abort aja --> PO Return
                                        OverPaymentNos.Add(ent.TransactionNo);
                                        continue;
                                    }
                                }
                            }

                            ent.PaymentAmount = (ent.PaymentAmount ?? 0) + i.PaymentAmount;
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
                    ////

                    //foreach (var i in invoicesItems)
                    //{
                    //    var ent = (from c in coll
                    //               where c.TransactionNo == i.TransactionNo
                    //               select c).SingleOrDefault();
                    //    if (ent != null)
                    //    {
                    //        ent.PaymentAmount = (ent.PaymentAmount ?? 0) + i.PaymentAmount;
                    //    }
                    //}

                    coll.Save();


                    createJournal = true;

                    //Commit if success, Rollback if failed
                    trans.Complete();

                }
                else
                {
                    return "NotExist";
                }
            }

            if (createJournal)
            {
                AppParameter app = new AppParameter();
                app.LoadByPrimaryKey("acc_IsAutoJournalAPPayment");

                if (app.ParameterValue == "Yes")
                {
                    DateTime jDate = DateTime.Now;
                    app = new AppParameter();
                    if (app.LoadByPrimaryKey("acc_JournalAPPaymentDate"))
                        jDate = app.ParameterValue.ToString().Equals("0") ?
                            entity.InvoiceDate.Value.Date : entity.PaymentApprovedDateTime.Value.Date;

                    var isClosingPeriod = PostingStatus.IsPeriodeClosed(jDate);
                    if (isClosingPeriod)
                        return "Financial statements for period: " +
                               string.Format("{0:MMMM-yyyy}", jDate) +
                               " have been closed. Please contact the authorities.";


                    /* Automatic Journal Testing Start */
                    var PrmApUsingInvoicing = new AppParameter();
                    if (!PrmApUsingInvoicing.LoadByPrimaryKey("acc_IsJournalApUsingInvoicing"))
                    {
                        throw new Exception("Parameter acc_IsJournalApUsingInvoicing not yet configured!");
                    }
                    if (PrmApUsingInvoicing.ParameterValue == "Yes")
                    {
                        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSMM")
                        {
                            int? journalId = JournalTransactions.AddNewAPPaymentJournal2RSMM(entity, userID, 0);
                        }
                        else
                        {
                            int? journalId = JournalTransactions.AddNewAPPaymentJournal2(entity, userID, 0);
                        }
                    }
                    else
                    {
                        int? journalId = JournalTransactions.AddNewAPPaymentJournal(entity, userID, 0);
                    }

                    /* Automatic Journal Testing End */
                }
            }

            return string.Empty;
        }

        public string PaymentUnApproved(string invoiceNo, InvoiceSupplierItemCollection invoicesItems, string userID)
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
                var entity = new InvoiceSupplier();
                if (entity.LoadByPrimaryKey(invoiceNo))
                {
                    entity.IsApproved = false;
                    entity.str.ApprovedDate = string.Empty;
                    entity.str.ApprovedByUserID = string.Empty;
                    entity.IsPaymentApproved = false;
                    entity.str.PaymentApprovedDateTime = String.Empty;
                    entity.str.PaymentApprovedByUserID = string.Empty;
                    entity.LastUpdateByUserID = userID;
                    entity.LastUpdateDateTime = DateTime.Now;

                    /* Automatic Journal Testing Start */
                    int? journalId;
                    var PrmApUsingInvoicing = new AppParameter();
                    if (!PrmApUsingInvoicing.LoadByPrimaryKey("acc_IsJournalApUsingInvoicing"))
                    {
                        return "Parameter acc_IsJournalApUsingInvoicing not yet configured!";
                    }
                    if (PrmApUsingInvoicing.ParameterValue == "Yes")
                    {
                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(DateTime.Now);
                        if (isClosingPeriod)
                            return "Financial statements for period: " +
                                   string.Format("{0:MMMM-yyyy}", DateTime.Now) +
                                   " have been closed. Please contact the authorities.";

                        journalId = JournalTransactions.AddNewAPPaymentJournal2Unapproval(entity, userID, 0);
                    }
                    else
                    {
                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(DateTime.Now);
                        if (isClosingPeriod)
                            return "Financial statements for period: " +
                                   string.Format("{0:MMMM-yyyy}", DateTime.Now) +
                                   " have been closed. Please contact the authorities.";

                        journalId = JournalTransactions.AddNewAPPaymentJournal2Unapproval(entity, userID, 0);

                        //throw new Exception("Auto journal is not available yet");
                    }
                    /* Automatic Journal Testing End */

                    entity.Save();

                    if (invoicesItems.Any(x => !string.IsNullOrEmpty(x.InvoiceReferenceNo)))
                    {
                        var coll = new InvoiceSupplierItemCollection();
                        coll.Query.Where(coll.Query.InvoiceNo.In(invoicesItems.Where(x => !string.IsNullOrEmpty(x.InvoiceReferenceNo)).Select(x => x.InvoiceReferenceNo).Distinct().ToArray()));
                        if (coll.LoadAll())
                        {
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
                                }
                            }

                            coll.Save();
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

        public string APWriteOffApproved(string invoiceNo, InvoiceSupplierItemCollection invoicesItems, string userID)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                var entity = new InvoiceSupplier();
                if (entity.LoadByPrimaryKey(invoiceNo))
                {
                    entity.IsApproved = true;
                    entity.str.ApprovedDate = DateTime.Now.ToString();
                    entity.str.ApprovedByUserID = userID;
                    entity.IsPaymentApproved = true;
                    entity.str.PaymentApprovedDateTime = DateTime.Now.ToString();
                    entity.str.PaymentApprovedByUserID = userID;
                    entity.LastUpdateByUserID = userID;
                    entity.LastUpdateDateTime = DateTime.Now;

                    entity.Save();

                    var coll = new InvoiceSupplierItemCollection();
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
                        }
                    }

                    coll.Save();

                    AppParameter app = new AppParameter();
                    app.LoadByPrimaryKey("acc_IsAutoJournalAPPayment");

                    if (app.ParameterValue == "Yes")
                    {
                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.InvoiceDate.Value);
                        if (isClosingPeriod)
                            return "Financial statements for period: " +
                                   string.Format("{0:MMMM-yyyy}", entity.InvoiceDate.Value) +
                                   " have been closed. Please contact the authorities.";

                        /* Automatic Journal Testing Start */

                        int? journalId = JournalTransactions.AddNewWriteOffAPJournal(entity, invoicesItems, userID, 0);

                        /* Automatic Journal Testing End */
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

        public string PaymentApproved(string invoiceNo, InvoiceAdjusmentCollection invoiceAdjusments, string userID)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                var entity = new InvoiceSupplier();
                if (entity.LoadByPrimaryKey(invoiceNo))
                {
                    entity.IsApproved = true;
                    entity.str.ApprovedDate = DateTime.Now.ToString();
                    entity.str.ApprovedByUserID = userID;
                    entity.IsPaymentApproved = true;
                    entity.str.PaymentApprovedDateTime = DateTime.Now.ToString();
                    entity.str.PaymentApprovedByUserID = userID;
                    entity.LastUpdateByUserID = userID;
                    entity.LastUpdateDateTime = DateTime.Now;
                    entity.Save();

                    var app = new AppParameter();
                    app.LoadByPrimaryKey("acc_IsAutoJournalAPPayment");

                    if (app.ParameterValue == "Yes")
                    {
                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.InvoiceDate.Value);
                        if (isClosingPeriod)
                            return "Financial statements for period: " +
                                   string.Format("{0:MMMM-yyyy}", entity.InvoiceDate.Value) +
                                   " have been closed. Please contact the authorities.";

                        /* Automatic Journal Testing Start */

                        int? journalId = JournalTransactions.AddNewAPAddPaymentJournal(entity, userID);


                        /* Automatic Journal Testing End */
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

        public string PaymentUnApproved(string invoiceNo, InvoiceAdjusmentCollection invoiceAdjusments, string userID)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                var entity = new InvoiceSupplier();
                if (entity.LoadByPrimaryKey(invoiceNo))
                {
                    entity.IsApproved = false;
                    entity.str.ApprovedDate = string.Empty;
                    entity.str.ApprovedByUserID = string.Empty;
                    entity.IsPaymentApproved = false;
                    entity.str.PaymentApprovedDateTime = String.Empty;
                    entity.str.PaymentApprovedByUserID = string.Empty;
                    entity.LastUpdateByUserID = userID;
                    entity.LastUpdateDateTime = DateTime.Now;

                    /* Automatic Journal Testing Start */

                    /* Automatic Journal Testing End */

                    entity.Save();

                    var coll = new InvoiceSupplierItemCollection();
                    coll.Query.Where(coll.Query.InvoiceNo == entity.InvoiceReferenceNo);
                    coll.LoadAll();

                    foreach (var i in invoiceAdjusments)
                    {
                        var ent = (from c in coll
                                   where c.TransactionNo == i.TransactionNo
                                   select c).SingleOrDefault();
                        if (ent != null)
                        {
                            ent.PaymentAmount = (ent.PaymentAmount ?? 0) - i.PaymentAmount;
                            if (ent.PaymentAmount < 0)
                                ent.PaymentAmount = 0;
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
                var entity = new InvoiceSupplier();
                if (entity.LoadByPrimaryKey(invoiceNo))
                {
                    entity.IsVoid = true;
                    entity.str.VoidDate = DateTime.Now.ToString();
                    entity.str.VoidByUserID = userID;
                    entity.LastUpdateByUserID = userID;
                    entity.LastUpdateDateTime = DateTime.Now;

                    entity.Save();

                    var adjColl = new InvoiceAdjusmentCollection();
                    adjColl.Query.Where(adjColl.Query.InvoicePaymentNo == invoiceNo);
                    adjColl.LoadAll();
                    foreach (var adj in adjColl)
                    {
                        adj.InvoicePaymentNo = null;
                        adj.PaymentAmount = null;
                        adj.OtherCost = null;
                        adj.BankCost = null;
                        adj.LastUpdateByUserID = userID;
                        adj.LastUpdateDateTime = DateTime.Now;
                    }

                    adjColl.Save();

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

        public static decimal PphProgresif(decimal amount)
        {
            var percentPph21Base = Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.PercentPph21Base));
            var total = amount * percentPph21Base / 100;
            var pptColl = new PphProgressiveTaxCollection();
            pptColl.Query.OrderBy(pptColl.Query.MinAmount.Ascending);
            pptColl.LoadAll();

            decimal pphAmt = 0;

            foreach (var p in pptColl)
            {
                if (total > 0)
                {
                    var range = (p.MaxAmount ?? 0) - (p.MinAmount ?? 0);
                    var calculated = total > range ? range : total;

                    pphAmt += calculated * (p.Percentage ?? 0) / 100;
                    total -= calculated;
                }
            }

            return pphAmt;
        }
    }
}

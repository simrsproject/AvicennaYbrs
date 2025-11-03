using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;


using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject.Common;


namespace Temiang.Avicenna.BusinessObject
{
    public partial class Invoices
    {
        #region - GRID LIST -

        public string GuarantorName
        {
            get
            {
                string retVal = "";
                retVal = this.GetColumn("GuarantorName") is System.DBNull ? "" : (string)this.GetColumn("GuarantorName");
                return retVal;
            }
        }

        public decimal TotalTransaction
        {
            get
            {
                decimal retVal = 0;
                retVal = this.GetColumn("TotalTransaction") is System.DBNull ? 0 : (decimal)this.GetColumn("TotalTransaction");
                return retVal;
            }
        }
        public decimal TotalAmount
        {
            get
            {
                decimal retVal = 0;
                retVal = this.GetColumn("TotalAmount") is System.DBNull ? 0 : (decimal)this.GetColumn("TotalAmount");
                return retVal;
            }
        }
        public string ReceivableStatus
        {
            get
            {
                string retVal = "";
                retVal = this.GetColumn("ReceivableStatus") is System.DBNull ? "" : (string)this.GetColumn("ReceivableStatus");
                return retVal;
            }
        }
        public string AgingDateStr
        {
            get
            {
                string retVal = "";
                retVal = this.GetColumn("AgingDateStr") is System.DBNull ? "" : (string)this.GetColumn("AgingDateStr");
                return retVal;
            }
        }
        public int Aging
        {
            get
            {
                int retVal = 0;
                retVal = this.GetColumn("Aging") is System.DBNull ? 0 : (int)this.GetColumn("Aging");
                return retVal;
            }
        }

        #region - Creating Invoice -
        public static int TotalCount(string invoiceNo, DateTime? invoiceDate, DateTime? invoiceDateTo, DateTime? invoiceDueDate, string guarantorName, string paymentNo,
            string registrationNo, string medicalNo, string patientName)
        {
            int retVal = 0;

            var h = new InvoicesQuery("h");

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
            if (invoiceDueDate.HasValue)
            {
                prms.Add(h.InvoiceDueDate.GreaterThanOrEqual(invoiceDueDate.Value));
                prms.Add(h.InvoiceDueDate.LessThan(invoiceDueDate.Value.AddDays(1)));
            }
            if (!string.IsNullOrEmpty(guarantorName))
            {
                var g = new GuarantorQuery("g");
                h.InnerJoin(g).On(g.GuarantorID == h.GuarantorID);

                string searchTextContain = string.Format("%{0}%", guarantorName);
                prms.Add(g.GuarantorName.Like(searchTextContain));
            }
            if (!string.IsNullOrEmpty(paymentNo) || !string.IsNullOrEmpty(registrationNo) || !string.IsNullOrEmpty(medicalNo) || !string.IsNullOrEmpty(patientName))
            {
                var dtColl = new InvoicesItemCollection();
                var dtq = new InvoicesItemQuery("dt");

                if (!string.IsNullOrEmpty(paymentNo))
                {
                    if (paymentNo.Length == 14) // full no payment, pakai =
                    {
                        dtq.Where(dtq.PaymentNo == paymentNo);
                    }
                    else
                    {
                        string searchTextContain;

                        if (paymentNo.ToUpper().Contains("PM"))
                        {
                            searchTextContain = string.Format("{0}%", paymentNo);
                        }
                        else
                        {
                            searchTextContain = string.Format("%{0}%", paymentNo);
                        }

                        dtq.Where(dtq.PaymentNo.Like(searchTextContain));
                    }
                }

                if (!string.IsNullOrEmpty(registrationNo))
                {
                    if (registrationNo.Length >= 18) // full noreg, pakai =
                    {
                        dtq.Where(dtq.RegistrationNo == registrationNo);
                    }
                    else
                    {
                        string searchTextContain;
                        if (registrationNo.ToUpper().Contains("REG/"))
                        {
                            searchTextContain = string.Format("{0}%", registrationNo);
                        }
                        else
                        {
                            searchTextContain = string.Format("%{0}%", registrationNo);
                        }
                        dtq.Where(dtq.RegistrationNo.Like(searchTextContain));
                    }
                }

                if (!string.IsNullOrEmpty(medicalNo))
                {
                    var patq = new PatientQuery("pat");
                    dtq.InnerJoin(patq).On(patq.PatientID == dtq.PatientID);

                    if (!string.IsNullOrEmpty(medicalNo))
                    {
                        if (medicalNo.Length == 8) // full norm, pakai =
                        {
                            dtq.Where(patq.MedicalNo == medicalNo);
                        }
                        else
                        {
                            var searchTextContain = string.Format("%{0}%", registrationNo);
                            dtq.Where(patq.MedicalNo.Like(searchTextContain));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(patientName))
                {
                    if (!string.IsNullOrEmpty(patientName))
                    {
                        var searchTextContain = string.Format("%{0}%", patientName);
                        dtq.Where(dtq.PatientName.Like(searchTextContain));
                    }
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

            h.Where(h.IsInvoicePayment == false, h.InvoiceReferenceNo.IsNull(), h.Or(h.IsAdditionalInvoice.IsNull(), h.IsAdditionalInvoice == false));

            if (prms.Count > 0)
                h.Where(prms.ToArray());
            else
                h.Where(h.InvoiceDate == DateTime.Now.Date);

            h.es2.Connection.CommandTimeout = 600;

            var dtb = h.LoadDataTable();
            retVal = (int)dtb.Rows[0][0];
            return retVal;
        }

        public static InvoicesCollection GetAllWithPaging(int pageNumber, int pageSize, string invoiceNo, DateTime? invoiceDate, DateTime? invoiceDateTo, DateTime? invoiceDueDate, string guarantorName, string paymentNo,
            string registrationNo, string medicalNo, string patientName, string sortString)
        {
            var h = new InvoicesQuery("h");
            var g = new GuarantorQuery("g");
            var s = new AppStandardReferenceItemQuery("s");
            h.InnerJoin(g).On(g.GuarantorID == h.GuarantorID);
            h.LeftJoin(s).On(s.StandardReferenceID == "ReceivableStatus" && s.ItemID == h.SRReceivableStatus);

            h.Select(
                h.InvoiceNo, h.InvoiceDate, h.InvoiceDueDate, g.GuarantorName, "<CAST(0 AS DECIMAL) TotalTransaction>", @"<ISNULL(h.DiscountAmount, 0) AS DiscountAmount>", "<CAST(0 AS DECIMAL) TotalAmount>",
                s.ItemName.As("ReceivableStatus"), @"<CASE WHEN h.AgingDate IS NULL THEN '' ELSE CONVERT(VARCHAR, h.AgingDate, 103) END AS 'AgingDateStr'>",
                h.IsApproved, h.IsVoid, @"<DATEDIFF(DAY, GETDATE(), h.InvoiceDueDate) AS Aging>");

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
            if (invoiceDueDate.HasValue)
            {
                prms.Add(h.InvoiceDueDate.GreaterThanOrEqual(invoiceDueDate.Value));
                prms.Add(h.InvoiceDueDate.LessThan(invoiceDueDate.Value.AddDays(1)));
            }
            if (!string.IsNullOrEmpty(guarantorName))
            {
                string searchTextContain = string.Format("%{0}%", guarantorName);
                prms.Add(g.GuarantorName.Like(searchTextContain));
            }
            if (!string.IsNullOrEmpty(paymentNo) || !string.IsNullOrEmpty(registrationNo) || !string.IsNullOrEmpty(medicalNo) || !string.IsNullOrEmpty(patientName))
            {
                var dtColl = new InvoicesItemCollection();
                var dtq = new InvoicesItemQuery("dt");

                if (!string.IsNullOrEmpty(paymentNo))
                {
                    if (paymentNo.Length == 14) // full no payment, pakai =
                    {
                        dtq.Where(dtq.PaymentNo == paymentNo);
                    }
                    else
                    {
                        string searchTextContain;

                        if (paymentNo.ToUpper().Contains("PM"))
                        {
                            searchTextContain = string.Format("{0}%", paymentNo);
                        }
                        else
                        {
                            searchTextContain = string.Format("%{0}%", paymentNo);
                        }

                        dtq.Where(dtq.PaymentNo.Like(searchTextContain));
                    }
                }
                if (!string.IsNullOrEmpty(registrationNo))
                {
                    if (registrationNo.Length >= 18) // full noreg, pakai =
                    {
                        dtq.Where(dtq.RegistrationNo == registrationNo);
                    }
                    else
                    {
                        string searchTextContain;
                        if (registrationNo.ToUpper().Contains("REG/"))
                        {
                            searchTextContain = string.Format("{0}%", registrationNo);
                        }
                        else
                        {
                            searchTextContain = string.Format("%{0}%", registrationNo);
                        }
                        dtq.Where(dtq.RegistrationNo.Like(searchTextContain));
                    }
                }
                if (!string.IsNullOrEmpty(medicalNo))
                {
                    var patq = new PatientQuery("pat");
                    dtq.InnerJoin(patq).On(patq.PatientID == dtq.PatientID);

                    if (!string.IsNullOrEmpty(medicalNo))
                    {
                        if (medicalNo.Length == 8) // full norm, pakai =
                        {
                            dtq.Where(patq.MedicalNo == medicalNo);
                        }
                        else
                        {
                            var searchTextContain = string.Format("%{0}%", registrationNo);
                            dtq.Where(patq.MedicalNo.Like(searchTextContain));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(patientName))
                {
                    if (!string.IsNullOrEmpty(patientName))
                    {
                        var searchTextContain = string.Format("%{0}%", patientName);
                        dtq.Where(dtq.PatientName.Like(searchTextContain));
                    }
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

            h.Where(h.IsInvoicePayment == false, h.InvoiceReferenceNo.IsNull(), h.Or(h.IsAdditionalInvoice.IsNull(), h.IsAdditionalInvoice == false));

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

            var coll = new InvoicesCollection();
            coll.Load(h);

            if (coll.Count > 0)
            {
                var dtColl = new InvoicesItemCollection();
                dtColl.Query.Where(dtColl.Query.InvoiceNo.In(coll.Select(x => x.InvoiceNo)));
                dtColl.Query.Select(
                    dtColl.Query.InvoiceNo,
                    dtColl.Query.Amount.Sum(),
                    dtColl.Query.PaymentNo.Count()
                    );
                dtColl.Query.GroupBy(dtColl.Query.InvoiceNo);
                dtColl.LoadAll();

                foreach (var inv in coll)
                {
                    var dt = dtColl.Where(x => x.InvoiceNo == inv.InvoiceNo);
                    if (dt.Any())
                    {
                        inv.SetColumn("TotalTransaction", dt.Sum(x => x.Amount));
                        inv.SetColumn("TotalAmount", dt.Sum(x => x.Amount) - (inv.DiscountAmount ?? 0));
                    }
                }
            }

            return coll;
        }

        private static esOrderByItem[] safeOrderByItems(string sortString, InvoicesQuery q)
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
        public static int TotalPaymentCount(string invoiceNo, string invoiceReferenceNo, DateTime? invoiceDate, DateTime? invoiceDateTo, DateTime? paymentDate, DateTime? paymentDateTo, string guarantorName, 
            string registrationNo, string medicalNo, string patientName)
        {
            int retVal = 0;

            var h = new InvoicesQuery("h");

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
            if (paymentDate.HasValue && paymentDateTo.HasValue)
            {
                prms.Add(h.PaymentDate.GreaterThanOrEqual(paymentDate.Value));
                prms.Add(h.PaymentDate.LessThan(paymentDateTo.Value.AddDays(1)));
            }
            if (!string.IsNullOrEmpty(guarantorName))
            {
                var g = new GuarantorQuery("g");
                h.InnerJoin(g).On(g.GuarantorID == h.GuarantorID);

                string searchTextContain = string.Format("%{0}%", guarantorName);
                prms.Add(g.GuarantorName.Like(searchTextContain));
            }
            if (!string.IsNullOrEmpty(invoiceReferenceNo) || !string.IsNullOrEmpty(registrationNo) || !string.IsNullOrEmpty(medicalNo) || !string.IsNullOrEmpty(patientName))
            {
                var dtColl = new InvoicesItemCollection();
                var dtq = new InvoicesItemQuery("dt");

                if (!string.IsNullOrEmpty(invoiceReferenceNo))
                {
                    if (invoiceReferenceNo.Length == 18) // full no payment, pakai =
                    {
                        dtq.Where(dtq.InvoiceReferenceNo == invoiceReferenceNo);
                    }
                    else
                    {
                        string searchTextContain;

                        if (invoiceReferenceNo.ToUpper().Contains("AR-"))
                        {
                            searchTextContain = string.Format("{0}%", invoiceReferenceNo);
                        }
                        else
                        {
                            searchTextContain = string.Format("%{0}%", invoiceReferenceNo);
                        }

                        dtq.Where(dtq.InvoiceReferenceNo.Like(searchTextContain));
                    }
                }

                if (!string.IsNullOrEmpty(registrationNo))
                {
                    if (registrationNo.Length >= 18) // full noreg, pakai =
                    {
                        dtq.Where(dtq.RegistrationNo == registrationNo);
                    }
                    else
                    {
                        string searchTextContain;
                        if (registrationNo.ToUpper().Contains("REG/"))
                        {
                            searchTextContain = string.Format("{0}%", registrationNo);
                        }
                        else
                        {
                            searchTextContain = string.Format("%{0}%", registrationNo);
                        }
                        dtq.Where(dtq.RegistrationNo.Like(searchTextContain));
                    }
                }

                if (!string.IsNullOrEmpty(medicalNo))
                {
                    var patq = new PatientQuery("pat");
                    dtq.InnerJoin(patq).On(patq.PatientID == dtq.PatientID);

                    if (!string.IsNullOrEmpty(medicalNo))
                    {
                        if (medicalNo.Length == 8) // full norm, pakai =
                        {
                            dtq.Where(patq.MedicalNo == medicalNo);
                        }
                        else
                        {
                            var searchTextContain = string.Format("%{0}%", registrationNo);
                            dtq.Where(patq.MedicalNo.Like(searchTextContain));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(patientName))
                {
                    if (!string.IsNullOrEmpty(patientName))
                    {
                        var searchTextContain = string.Format("%{0}%", patientName);
                        dtq.Where(dtq.PatientName.Like(searchTextContain));
                    }
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

            h.Where(h.IsInvoicePayment == true, h.IsWriteOff == false, h.IsAddPayment.IsNull());

            if (prms.Count > 0)
                h.Where(prms.ToArray());
            else
                h.Where(h.PaymentDate == DateTime.Now.Date);

            h.es2.Connection.CommandTimeout = 600;

            var dtb = h.LoadDataTable();
            retVal = (int)dtb.Rows[0][0];
            return retVal;
        }

        public static InvoicesCollection GetAllPaymentWithPaging(int pageNumber, int pageSize, string invoiceNo, string invoiceReferenceNo, DateTime? invoiceDate, DateTime? invoiceDateTo, DateTime? paymentDate, DateTime? paymentDateTo, 
            string guarantorName, string registrationNo, string medicalNo, string patientName, string sortString)
        {
            var h = new InvoicesQuery("h");
            var g = new GuarantorQuery("g");
            var s = new AppStandardReferenceItemQuery("s");
            h.InnerJoin(g).On(g.GuarantorID == h.GuarantorID);
            h.LeftJoin(s).On(s.StandardReferenceID == "ReceivableStatus" && s.ItemID == h.SRReceivableStatus);

            h.Select(
                h.InvoiceNo, h.PaymentDate, h.InvoiceReferenceNo, g.GuarantorName, "<CAST(0 AS DECIMAL) TotalAmount>",
                s.ItemName.As("ReceivableStatus"), h.SRPhysicianFeeProportionalStatus, h.PhysicianFeeProportionalPercentage,
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
            if (paymentDate.HasValue && paymentDateTo.HasValue)
            {
                prms.Add(h.PaymentDate.GreaterThanOrEqual(paymentDate.Value));
                prms.Add(h.PaymentDate.LessThan(paymentDateTo.Value.AddDays(1)));
            }
            if (!string.IsNullOrEmpty(guarantorName))
            {
                string searchTextContain = string.Format("%{0}%", guarantorName);
                prms.Add(g.GuarantorName.Like(searchTextContain));
            }
            if (!string.IsNullOrEmpty(invoiceReferenceNo) || !string.IsNullOrEmpty(registrationNo) || !string.IsNullOrEmpty(medicalNo) || !string.IsNullOrEmpty(patientName))
            {
                var dtColl = new InvoicesItemCollection();
                var dtq = new InvoicesItemQuery("dt");

                if (!string.IsNullOrEmpty(invoiceReferenceNo))
                {
                    if (invoiceReferenceNo.Length == 18) // full no payment, pakai =
                    {
                        dtq.Where(dtq.InvoiceReferenceNo == invoiceReferenceNo);
                    }
                    else
                    {
                        string searchTextContain;

                        if (invoiceReferenceNo.ToUpper().Contains("AR-"))
                        {
                            searchTextContain = string.Format("{0}%", invoiceReferenceNo);
                        }
                        else
                        {
                            searchTextContain = string.Format("%{0}%", invoiceReferenceNo);
                        }

                        dtq.Where(dtq.InvoiceReferenceNo.Like(searchTextContain));
                    }
                }
                if (!string.IsNullOrEmpty(registrationNo))
                {
                    if (registrationNo.Length >= 18) // full noreg, pakai =
                    {
                        dtq.Where(dtq.RegistrationNo == registrationNo);
                    }
                    else
                    {
                        string searchTextContain;
                        if (registrationNo.ToUpper().Contains("REG/"))
                        {
                            searchTextContain = string.Format("{0}%", registrationNo);
                        }
                        else
                        {
                            searchTextContain = string.Format("%{0}%", registrationNo);
                        }
                        dtq.Where(dtq.RegistrationNo.Like(searchTextContain));
                    }
                }
                if (!string.IsNullOrEmpty(medicalNo))
                {
                    var patq = new PatientQuery("pat");
                    dtq.InnerJoin(patq).On(patq.PatientID == dtq.PatientID);

                    if (!string.IsNullOrEmpty(medicalNo))
                    {
                        if (medicalNo.Length == 8) // full norm, pakai =
                        {
                            dtq.Where(patq.MedicalNo == medicalNo);
                        }
                        else
                        {
                            var searchTextContain = string.Format("%{0}%", registrationNo);
                            dtq.Where(patq.MedicalNo.Like(searchTextContain));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(patientName))
                {
                    if (!string.IsNullOrEmpty(patientName))
                    {
                        var searchTextContain = string.Format("%{0}%", patientName);
                        dtq.Where(dtq.PatientName.Like(searchTextContain));
                    }
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

            h.Where(h.IsInvoicePayment == true, h.IsWriteOff == false, h.IsAddPayment.IsNull());

            if (prms.Count > 0)
                h.Where(prms.ToArray());
            else
                h.Where(h.PaymentDate == DateTime.Now.Date);

            h.OrderBy(safeOrderPaymentByItems(sortString, h));
            //h.OrderBy(h.InvoiceNo.Descending);

            //h.es.WithNoLock = true;
            h.es.PageSize = pageSize;
            h.es.PageNumber = pageNumber + 1;
            h.es2.Connection.CommandTimeout = 600;

            var coll = new InvoicesCollection();
            coll.Load(h);

            if (coll.Count > 0)
            {
                var dtColl = new InvoicesItemCollection();
                dtColl.Query.Where(dtColl.Query.InvoiceNo.In(coll.Select(x => x.InvoiceNo)));
                dtColl.Query.Select(
                    dtColl.Query.InvoiceNo,
                    dtColl.Query.PaymentAmount.Sum(),
                    dtColl.Query.PaymentNo.Count()
                    );
                dtColl.Query.GroupBy(dtColl.Query.InvoiceNo);
                dtColl.LoadAll();

                foreach (var inv in coll)
                {
                    var dt = dtColl.Where(x => x.InvoiceNo == inv.InvoiceNo);
                    if (dt.Any())
                    {
                        inv.SetColumn("TotalAmount", dt.Sum(x => x.PaymentAmount) + (inv.RoundingAmount ?? 0));
                    }
                }
            }

            return coll;
        }

        private static esOrderByItem[] safeOrderPaymentByItems(string sortString, InvoicesQuery q)
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
                if (tmp[0].Equals("paymentdate"))
                    list.Add(isDesc ? q.PaymentDate.Descending : q.PaymentDate.Ascending);
            }
            if (list.Count == 0) list.Add(q.InvoiceNo.Descending);
            return list.ToArray();
        }
        #endregion

        #endregion


        public string ReceivableStatusName
        {
            get { return GetColumn("refToAppStandardReference_ReceivableStatusName").ToString(); }
            set { SetColumn("refToAppStandardReference_ReceivableStatusName", value); }
        }

        #region void unvoid

        private static void VoidProcess(string invoiceNo, string userID, bool isVoid)
        {
            Invoices entity = new Invoices();
            if (entity.LoadByPrimaryKey(invoiceNo))
            {
                if (entity.IsVoid == true && isVoid) return;
                if (entity.IsVoid == false && !isVoid) return;

                if (isVoid && (entity.IsApproved ?? false)) {
                    throw new Exception("Data has been approved");
                }

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

        private static string ApprovProcess(string invoiceNo, string userID, bool isApproval, string receivableStatus, string voidReason)
        {
            var bStatus = false;
            Invoices entity = new Invoices();
            InvoicesItemCollection iviColl = new InvoicesItemCollection();
            iviColl.Query.Where(iviColl.Query.InvoiceNo == invoiceNo);
            iviColl.LoadAll();

            using (esTransactionScope trans = new esTransactionScope())
            {
                if (entity.LoadByPrimaryKey(invoiceNo))
                {
                    if (entity.IsInvoicePayment ?? false) {
                        return "Invoice number is invalid";
                    }

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
                    // untuk unapproval, approvedDate jangan di-set null karena ada kebutuhan untuk tau
                    // tgl brp invoice ini di-unapprove
                    //entity.str.ApprovedDate = isApproval ? DateTime.Now.ToString() : "";
                    //entity.str.ApprovedByUserID = isApproval ? userID : "";
                    if (isApproval)
                    {
                        entity.str.ApprovedDate = DateTime.Now.ToString();
                        entity.str.ApprovedByUserID = userID;
                    }

                    entity.SRReceivableStatus = isApproval ? receivableStatus : "";

                    if (!isApproval)
                    {
                        entity.IsVoid = true;
                        entity.VoidDate = DateTime.Now;
                        entity.VoidByUserID = userID;
                        entity.VoidReason = voidReason;
                    }

                    //var app = new AppParameter();
                    //app.LoadByPrimaryKey("IsPhysicianFeeArBasedOnPayment");
                    //var isPhysicianFeeArBasedOnPayment = app.ParameterValue;

                    //app = new AppParameter();
                    //app.LoadByPrimaryKey("IsPhysicianFeeArPaidBasedOnPayment");
                    //var isPhysicianFeeArPaidBasedOnPayment = app.ParameterValue;

                    //app = new AppParameter();
                    //app.LoadByPrimaryKey("IsPhysicianFeeArPaidBasedOnPayment");
                    //var isPhysicianFeeArCreateOnArReceipt = app.ParameterValue;

                    //if (!isApproval & isPhysicianFeeArPaidBasedOnPayment == "No")
                    //{
                    //    entity.IsVoid = true;
                    //    entity.VoidDate = DateTime.Now;
                    //    entity.VoidByUserID = userID;
                    //}

                    entity.Save();

                    //if (isPhysicianFeeArBasedOnPayment == "No" && isPhysicianFeeArCreateOnArReceipt == "No")
                    //{
                    //    if (isApproval)
                    //    {
                    //        var invoicesItems = new InvoicesItemCollection();
                    //        invoicesItems.Query.Where(invoicesItems.Query.InvoiceNo == invoiceNo);
                    //        invoicesItems.LoadAll();

                    //        int? x = ParamedicFeeTransChargesItemCompSettled.AddNewSettledInv(entity, invoicesItems, userID);
                    //    }
                    //    else
                    //    {
                    //        if (isPhysicianFeeArPaidBasedOnPayment == "No")
                    //        {
                    //            int? x = ParamedicFeeTransChargesItemCompSettled.AddReturnSettledInv(invoiceNo, userID);
                    //        }
                    //        else
                    //        {
                    //            int? x = ParamedicFeeTransChargesItemCompSettled.DeleteSettled(entity);
                    //        }
                    //    }

                    //}

                    // rekal untuk prorata ???
                    if (!(entity.IsAdditionalInvoice ?? false)) {
                        foreach (var itm in iviColl)
                        {
                            var ba = new BillingAdjustment(itm.RegistrationNo, true);
                            var msg = ba.CalculateAndSaveProrata_NoTransactionScope(userID);
                            if (!string.IsNullOrEmpty(msg))
                            {
                                return (msg);
                            }
                        }

                        //// prorata jasmed by invoice
                        //iviColl.CalculateFeeProrata();
                        //iviColl.SaveFeeProrata_NoTransScope();
                    }

                    //Commit if success, Rollback if failed
                    bStatus = true;
                    trans.Complete();

                }
                else
                {
                    return "NotExist";
                }
            }

            if (bStatus)
            {
                var app = new AppParameter();
                if (app.LoadByPrimaryKey("acc_IsAutoJournalARInvoicing"))
                {
                    if (app.ParameterValue == "Yes")
                    {
                        var PrmUsingInvoicing = new AppParameter();
                        if (!PrmUsingInvoicing.LoadByPrimaryKey("acc_IsJournalArUsingInvoicing"))
                        {
                            throw new Exception("Parameter acc_IsJournalArUsingInvoicing not yet configured!");
                        }
                        if (PrmUsingInvoicing.ParameterValue == "Yes")
                        {
                            int? journalId = 0;
                            if (isApproval)
                            {
                                DateTime jDate = DateTime.Now;
                                var appprmdate = new AppParameter();
                                if (appprmdate.LoadByPrimaryKey("acc_JournalARInvoicingDate"))
                                    jDate = appprmdate.ParameterValue.ToString().Equals("0") ?
                                        entity.InvoiceDate.Value.Date : entity.ApprovedDate.Value.Date;

                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(jDate);
                                if (isClosingPeriod)
                                    return "Financial statements for period: " +
                                           string.Format("{0:MMMM-yyyy}", jDate) +
                                           " have been closed. Please contact the authorities.";

                                journalId = JournalTransactions.AddNewARInvoicingJournal2(entity, userID, 0);
                            }
                            else
                            {
                                DateTime jDate = entity.VoidDate.Value.Date;
                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(jDate);
                                if (isClosingPeriod)
                                    return "Financial statements for period: " +
                                           string.Format("{0:MMMM-yyyy}", jDate) +
                                           " have been closed. Please contact the authorities.";

                                journalId = JournalTransactions.AddNewARInvoicingJournal2Unapproval(entity, userID, 0);
                            }

                            //if (journalId == -1)
                            //    return "Create journal failed";
                        }
                    }
                }
            }
            return string.Empty;
        }

        public string Approv(string invoiceNo, string userID, string receivableStatus)
        {
            return ApprovProcess(invoiceNo, userID, true, receivableStatus, string.Empty);
        }

        public string UnApprov(string invoiceNo, string userID, string receivableStatus, string voidReason)
        {
            return ApprovProcess(invoiceNo, userID, false, receivableStatus, voidReason);
        }

        public string PaymentApproved(string invoiceNo, InvoicesItemCollection invoicesItems, string userID)
        {
            //tambah : sp_PaymentAR_FixingDataJasaMedis
            esParameters prms = new esParameters();
            prms.Add("invoiceNo", invoiceNo, esParameterDirection.Input, DbType.String, 25);
            Invoices entity = new Invoices();
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_PaymentAR_FixingDataJasaMedis", prms);

            using (esTransactionScope trans = new esTransactionScope())
            {
                //Invoices entity = new Invoices();
                if (entity.LoadByPrimaryKey(invoiceNo))
                {
                    if (!(entity.IsInvoicePayment ?? false)) {
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

                    var InvColl = new InvoicesItemCollection();
                    //InvColl.Query.Where(InvColl.Query.InvoiceNo == entity.InvoiceReferenceNo);
                    InvColl.Query.Where(InvColl.Query.InvoiceNo.In(invoicesItems.Select(x => x.InvoiceReferenceNo)));
                    InvColl.LoadAll();


                    System.Collections.Generic.List<string> OverPaymentNos = new System.Collections.Generic.List<string>();
                    foreach (var i in invoicesItems)
                    {
                        var ent = (from c in InvColl
                                   where c.PaymentNo == i.PaymentNo && c.InvoiceNo == i.InvoiceReferenceNo
                                   select c).SingleOrDefault();
                        if (ent != null)
                        {
                            // if (((ent.PaymentAmount ?? 0) + i.PaymentAmount + i.OtherAmount) > ent.VerifyAmount) 
                            if (Math.Abs((ent.PaymentAmount ?? 0) + (ent.OtherAmount ?? 0) + (ent.BankCost ?? 0)) >= Math.Abs(ent.VerifyAmount ?? 0))
                            {
                                // kelebihan bayar, kemungkinan double bayar. abort aja
                                OverPaymentNos.Add(ent.PaymentNo);
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
                    InvColl.Save();

                    AppParameter app = new AppParameter();
                    app.LoadByPrimaryKey("acc_IsAutoJournalARPayment");

                    if (app.ParameterValue == "Yes")
                    {
                        /* Automatic Journal Testing Start */
                        int? journalId;
                        var PrmUsingInvoicing = new AppParameter();
                        if (!PrmUsingInvoicing.LoadByPrimaryKey("acc_IsJournalArUsingInvoicing"))
                        {
                            return "Parameter acc_IsJournalArUsingInvoicing not yet configured!";
                        }

                        if (PrmUsingInvoicing.ParameterValue == "Yes")
                        {
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

                            journalId = JournalTransactions.AddNewARPaymentJournal2(entity, userID, 0);
                        }
                        else
                        {
                            var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.PaymentDate.Value);
                            if (isClosingPeriod)
                                return "Financial statements for period: " +
                                       string.Format("{0:MMMM-yyyy}", entity.PaymentDate.Value) +
                                       " have been closed. Please contact the authorities.";

                            journalId = JournalTransactions.AddNewARPaymentJournal(entity, userID, 0);
                        }

                        if (journalId == 2)
                            return "Chart Of Account for Payment Method Cash is not found.";

                        /* Automatic Journal Testing End */
                    }

                    entity.Save();

                    //app = new AppParameter();
                    //app.LoadByPrimaryKey("IsPhysicianFeeArBasedOnPayment");
                    //var isPhysicianFeeArBasedOnPayment = app.ParameterValue;

                    //app = new AppParameter();
                    //app.LoadByPrimaryKey("IsPhysicianFeeArCreateOnArReceipt");
                    //var isPhysicianFeeArCreateOnArReceipt = app.ParameterValue;

                    //if (isPhysicianFeeArBasedOnPayment == "Yes" && isPhysicianFeeArCreateOnArReceipt == "No")
                    //{
                    //    int? x = ParamedicFeeTransChargesItemCompSettled.AddNewSettled(entity, invoicesItems, userID);
                    //}

                    //// update payment invoce jasmed
                    //var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    //feeColl.SetInvoicePayment(entity, invoicesItems, userID);
                    //feeColl.Save();

                    // rekal untuk prorata ???
                    //foreach (var itm in invoicesItems) {
                    //    var ba = new BillingAdjustment(itm.RegistrationNo);
                    //    var msg = ba.CalculateAndSaveProrata_NoTransactionScope(userID);
                    //    if (!string.IsNullOrEmpty(msg))
                    //    {
                    //        return (msg);
                    //    }
                    //}

                    //Commit if success, Rollback if failed
                    trans.Complete();

                    //// update payment invoce jasmed, dikeluarkan dari transaction PAR supaya tidak locking table transaksi terlalu lama
                    //var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    //feeColl.SetInvoicePayment(entity, invoicesItems, userID);
                    //feeColl.Save();
                }
                else
                {
                    return "Data is Not Exist";
                }
            }
            return string.Empty;
        }

        public string PaymentUnApproved(string invoiceNo, InvoicesItemCollection invoicesItems, string userID)
        {
            // cek cash entrynya, sudah reconcile belum? kalau sudah recon jangan bisa diunapprove ya
            var ceColl = new CashTransactionCollection();
            ceColl.Query.Where(ceColl.Query.DocumentNumber == invoiceNo,
                ceColl.Query.IsPosted == true,
                ceColl.Query.IsVoid == false,
                ceColl.Query.IsCleared == true /*sudah recon*/);
            ceColl.LoadAll();
            if (ceColl.Count > 0) return "Cash transaction has been cleared, payment can not be unapproved!";

            var ppColl = new ParamedicFeeTransPaymentCollection();
            ppColl.Query.Where(
                ppColl.Query.PaymentRefNo == invoiceNo, ppColl.Query.IsVoid == false, 
                ppColl.Query.PaymentGroupNo.IsNotNull());
            ppColl.Query.es.Top = 1;
            if (ppColl.LoadAll()) {
                return "Paramedic fee has been paid, payment can not be unapproved!";
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                Invoices entity = new Invoices();
                if (entity.LoadByPrimaryKey(invoiceNo))
                {
                    if (!(entity.IsApproved ?? false)) {
                        return "Data is already unapproved";
                    }

                    if (entity.SRPhysicianFeeProportionalStatus == "01" || entity.SRPhysicianFeeProportionalStatus == "02") {
                        return "Paramedic fee calculation is in progress";
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

                    /* Automatic Journal Testing Start */

                    int? journalId;
                    var PrmUsingInvoicing = new AppParameter();
                    if (!PrmUsingInvoicing.LoadByPrimaryKey("acc_IsJournalArUsingInvoicing"))
                    {
                        return "Parameter acc_IsJournalArUsingInvoicing not yet configured!";
                    }
                    if (PrmUsingInvoicing.ParameterValue == "Yes")
                    {
                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(DateTime.Now);
                        if (isClosingPeriod)
                            return "Financial statements for period: " +
                                   string.Format("{0:MMMM-yyyy}", DateTime.Now) +
                                   " have been closed. Please contact the authorities.";

                        journalId = JournalTransactions.AddNewARPaymentJournal2Unapproval(entity, userID, 0);
                    }
                    else
                    {
                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(DateTime.Now);
                        if (isClosingPeriod)
                            return "Financial statements for period: " +
                                   string.Format("{0:MMMM-yyyy}", DateTime.Now) +
                                   " have been closed. Please contact the authorities.";

                        journalId = JournalTransactions.AddNewARPaymentJournal2Unapproval(entity, userID, 0);

                        //throw new Exception("Auto journal is not available yet");
                    }

                    /* Automatic Journal Testing End */

                    entity.Save();

                    var coll = new InvoicesItemCollection();
                    coll.Query.Where(coll.Query.InvoiceNo.In(invoicesItems.Select(x => x.InvoiceReferenceNo).Distinct().ToArray()));
                    coll.LoadAll();

                    foreach (var i in invoicesItems)
                    {
                        var ent = (from c in coll
                                   where c.PaymentNo == i.PaymentNo
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

                    //var app = new AppParameter();
                    //app.LoadByPrimaryKey("IsPhysicianFeeArBasedOnPayment");

                    //if (app.ParameterValue == "Yes")
                    //{
                    //    int? x = ParamedicFeeTransChargesItemCompSettled.DeleteSettled(entity);
                    //}

                    // update payment invoce jasmed
                    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    feeColl.UnSetInvoicePayment(entity, invoicesItems, userID);
                    feeColl.Save();

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
                Invoices entity = new Invoices();
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

                    AppParameter app = new AppParameter();
                    app.LoadByPrimaryKey("acc_IsAutoJournalARPayment");

                    if (app.ParameterValue == "Yes")
                    {
                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.PaymentDate.Value);
                        if (isClosingPeriod)
                            return "Financial statements for period: " +
                                   string.Format("{0:MMMM-yyyy}", entity.PaymentDate.Value) +
                                   " have been closed. Please contact the authorities.";

                        /* Automatic Journal Testing Start */

                        int? journalId = JournalTransactions.AddNewARAddPaymentJournal(entity, userID);

                        if (journalId == 2)
                            return "Chart Of Account for Payment Method Cash is not found.";

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

        public string PaymentUnApproved(string invoiceNo, InvoiceAdjusmentCollection invoiceAdjusments, string userID)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                Invoices entity = new Invoices();
                if (entity.LoadByPrimaryKey(invoiceNo))
                {
                    entity.IsApproved = false;
                    entity.str.ApprovedDate = string.Empty;
                    entity.str.ApprovedByUserID = string.Empty;
                    entity.IsPaymentApproved = false;
                    entity.str.PaymentApprovedDate = string.Empty;
                    entity.str.PaymentByUserID = string.Empty;
                    entity.str.PaymentApprovedByUserID = string.Empty;
                    entity.LastUpdateByUserID = userID;
                    entity.LastUpdateDateTime = DateTime.Now;

                    /* Automatic Journal Testing Start */

                    /* Automatic Journal Testing End */

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

        public string PaymentVoid(string invoiceNo, string userID)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                Invoices entity = new Invoices();
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

                    var bid = new BankInquiryDetail();
                    if (bid.LoadByRelatedTransactionNo(entity.InvoiceNo))
                    {
                        bid.RelatedTransactionNo = string.Empty;
                        bid.Save();
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

        public string WriteOffApproved(string invoiceNo, InvoicesItemCollection invoicesItems, string userID)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                Invoices entity = new Invoices();
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

                    var coll = new InvoicesItemCollection();
                    coll.Query.Where(coll.Query.InvoiceNo == entity.InvoiceReferenceNo);
                    coll.LoadAll();

                    foreach (var i in invoicesItems)
                    {
                        var ent = (from c in coll
                                   where c.PaymentNo == i.PaymentNo
                                   select c).SingleOrDefault();
                        if (ent != null)
                        {
                            ent.PaymentAmount = (ent.PaymentAmount ?? 0) + i.PaymentAmount;
                            ent.OtherAmount = (ent.OtherAmount ?? 0) + i.OtherAmount;
                        }
                    }

                    coll.Save();

                    var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.PaymentDate.Value);
                    if (isClosingPeriod)
                        return "Financial statements for period: " +
                               string.Format("{0:MMMM-yyyy}", entity.PaymentDate.Value) +
                               " have been closed. Please contact the authorities.";

                    /* Automatic Journal Testing Start */

                    int? journalId = JournalTransactions.WriteOffARJournal(entity, userID);

                    /* Automatic Journal Testing End */

                    // update payment invoce writeoff jasmed
                    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    feeColl.SetWriteOffByInvoice(invoicesItems);
                    feeColl.Save();

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

        #region CekDataValid
        public string GetPaymentVoid(string invoiceNo)
        {
            return GetPaymentVoidProcess(invoiceNo);
        }
        private static string GetPaymentVoidProcess(string invoiceNo)
        {
            var msg = string.Empty;
            var ii = new InvoicesItemQuery("a");
            var tp = new TransPaymentQuery("b");
            ii.InnerJoin(tp).On(tp.PaymentNo == ii.PaymentNo);
            ii.Where(ii.InvoiceNo == invoiceNo, ii.Or(tp.IsApproved == false, tp.IsVoid == true));
            ii.Select(ii.PaymentNo);
            DataTable dtb = ii.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (msg == string.Empty)
                        msg = row["PaymentNo"].ToString();
                    else
                        msg+= ", " + row["PaymentNo"].ToString();
                }
            }
            
            return msg;
        }
        public string GetGetMultipleInvoicing(string invoiceNo)
        {
            return GetMultipleInvoicingProcess(invoiceNo);
        }
        private static string GetMultipleInvoicingProcess(string invoiceNo)
        {
            var msg = string.Empty;
            var ii = new InvoicesItemQuery("aa");
            ii.Where(ii.InvoiceNo == invoiceNo);
            ii.Select(ii.PaymentNo);

            DataTable dtbii = ii.LoadDataTable();
            if (dtbii.Rows.Count > 0)
            {
                var iiQ = new InvoicesItemQuery("bb");
                var iQ = new InvoicesQuery("b");
                iiQ.InnerJoin(iQ).On(iQ.InvoiceNo == iiQ.InvoiceNo && iQ.IsInvoicePayment == false);
                iiQ.Where(iiQ.InvoiceNo != invoiceNo, iiQ.Or(iQ.IsVoid.IsNull(), iQ.IsVoid == false), iiQ.PaymentNo.In(ii));
                iiQ.Select(iiQ.InvoiceNo, iiQ.PaymentNo);
                DataTable dtb = iiQ.LoadDataTable();
                if (dtb.Rows.Count > 0)
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        if (msg == string.Empty)
                            msg = row["PaymentNo"].ToString() + " (INV#: " + row["InvoiceNo"].ToString() + ")";
                        else
                            msg += ", " + row["PaymentNo"].ToString() + " (INV#: " + row["InvoiceNo"].ToString() + ")";
                    }
                }
            }

            return msg;
        }
        #endregion
    }
}
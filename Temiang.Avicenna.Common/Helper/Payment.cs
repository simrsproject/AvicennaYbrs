using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Telerik.Web.UI.PivotGrid.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Common
{
    public partial class Helper
    {
        public class Payment
        {
            public static decimal GetTotalPayment(string[] registrationNo, string[] paymentType)
            {
                var headerColl = new TransPaymentCollection();

                var headerQuery = new TransPaymentQuery("a");
                var orderQuery = new TransPaymentItemOrderQuery("b");
                var billQuery = new TransPaymentItemIntermBillQuery("c");
                headerQuery.LeftJoin(orderQuery).On(headerQuery.PaymentNo == orderQuery.PaymentNo &&
                                                    orderQuery.IsPaymentProceed == true &&
                                                    orderQuery.IsPaymentReturned == false);
                headerQuery.LeftJoin(billQuery).On(headerQuery.PaymentNo == billQuery.PaymentNo &&
                                                    billQuery.IsPaymentProceed == true &&
                                                    billQuery.IsPaymentReturned == false);
                headerQuery.Where(
                    headerQuery.TransactionCode == TransactionCode.Payment,
                    headerQuery.RegistrationNo.In(registrationNo),
                    headerQuery.IsApproved == true,
                    headerQuery.IsVoid == false,
                    orderQuery.PaymentNo.IsNull(),
                    billQuery.PaymentNo.IsNull()
                    );
                headerQuery.Select(headerQuery);
                headerQuery.es.Distinct = true;

                headerColl.Load(headerQuery);

                decimal total = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    detailColl.Query.Where(
                        detailColl.Query.PaymentNo == header.PaymentNo,
                        detailColl.Query.SRPaymentType.In(paymentType)
                        );
                    detailColl.LoadAll();

                    total += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                }

                return total;
            }

            public static decimal GetTotalPayment(string[] registrationNo)
            {
                var headerColl = new TransPaymentCollection();

                var headerQuery = new TransPaymentQuery("a");
                var orderQuery = new TransPaymentItemOrderQuery("b");
                var billQuery = new TransPaymentItemIntermBillQuery("c");
                headerQuery.LeftJoin(orderQuery).On(headerQuery.PaymentNo == orderQuery.PaymentNo &&
                                                    orderQuery.IsPaymentProceed == true &&
                                                    orderQuery.IsPaymentReturned == false);
                headerQuery.LeftJoin(billQuery).On(headerQuery.PaymentNo == billQuery.PaymentNo &&
                                                    billQuery.IsPaymentProceed == true &&
                                                    billQuery.IsPaymentReturned == false);
                headerQuery.Where(
                    headerQuery.TransactionCode == TransactionCode.Payment,
                    headerQuery.RegistrationNo.In(registrationNo),
                    headerQuery.IsApproved == true,
                    headerQuery.IsVoid == false,
                    orderQuery.PaymentNo.IsNull(),
                    billQuery.PaymentNo.IsNull()
                    );
                headerQuery.Select(headerQuery);
                headerQuery.es.Distinct = true;

                headerColl.Load(headerQuery);

                decimal total = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    detailColl.Query.Where(detailColl.Query.PaymentNo == header.PaymentNo);
                    detailColl.LoadAll();

                    total += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                }

                return total;
            }

            public static decimal GetTotalPayment(string[] registrationNo, bool isPaymentReceive, string[] paymentMethod)
            {
                var headerColl = new TransPaymentCollection();

                var headerQuery = new TransPaymentQuery("a");
                headerQuery.Where(
                    headerQuery.RegistrationNo.In(registrationNo),
                    headerQuery.IsApproved == true,
                    headerQuery.IsVoid == false
                    );
                if (isPaymentReceive)
                    headerQuery.Where(headerQuery.TransactionCode == TransactionCode.Payment);
                else
                    headerQuery.Where(headerQuery.TransactionCode == TransactionCode.PaymentReturn);

                headerQuery.Select(headerQuery);
                headerQuery.es.Distinct = true;

                headerColl.Load(headerQuery);

                decimal total = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    detailColl.Query.Where(
                        detailColl.Query.PaymentNo == header.PaymentNo,
                        detailColl.Query.SRPaymentType.In(paymentMethod)
                        );
                    detailColl.LoadAll();

                    //total += detailColl.Sum(detail => Math.Abs(detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                    total += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                }

                if (!isPaymentReceive)
                    return (total * (-1));

                return total;
            }

            public static decimal GetTotalPayment(string[] registrationNo, bool isPaymentReceive, string paymentMethod)
            {
                var headerColl = new TransPaymentCollection();

                var headerQuery = new TransPaymentQuery("a");
                headerQuery.Where(
                    headerQuery.RegistrationNo.In(registrationNo),
                    headerQuery.IsApproved == true,
                    headerQuery.IsVoid == false
                    );
                if (isPaymentReceive)
                    headerQuery.Where(headerQuery.TransactionCode == TransactionCode.Payment);
                else
                    headerQuery.Where(headerQuery.TransactionCode == TransactionCode.PaymentReturn);

                headerQuery.Select(headerQuery);
                headerQuery.es.Distinct = true;

                headerColl.Load(headerQuery);

                decimal total = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();

                    detailColl.Query.Where(
                        detailColl.Query.PaymentNo == header.PaymentNo,
                        detailColl.Query.SRPaymentType == paymentMethod
                        );
                    detailColl.LoadAll();

                    total += detailColl.Sum(detail => Math.Abs(detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                }

                if (!isPaymentReceive)
                    return (total * (-1));

                return total;
            }

            public static decimal GetTotalPayment(string[] registrationNo, bool isPaymentReceive)
            {
                var headerColl = new TransPaymentCollection();

                var headerQuery = new TransPaymentQuery("a");
                headerQuery.Where(
                    headerQuery.RegistrationNo.In(registrationNo),
                    headerQuery.IsApproved == true,
                    headerQuery.IsVoid == false
                    );
                if (isPaymentReceive)
                    headerQuery.Where(headerQuery.TransactionCode == TransactionCode.Payment);
                else
                    headerQuery.Where(headerQuery.TransactionCode == TransactionCode.PaymentReturn);

                headerQuery.Select(headerQuery);
                headerQuery.es.Distinct = true;

                headerColl.Load(headerQuery);

                decimal total = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    detailColl.Query.Where(detailColl.Query.PaymentNo == header.PaymentNo);
                    detailColl.LoadAll();

                    total += detailColl.Sum(detail => Math.Abs(detail.Amount ?? 0) - Math.Abs(detail.RoundingAmount ?? 0));
                }

                if (!isPaymentReceive)
                    return (total * (-1));

                return total;
            }

            public static decimal GetTotalPaymentDiscount(string[] registrationNo)
            {
                var headerColl = new TransPaymentCollection();
                var headerQuery = new TransPaymentQuery();

                headerQuery.Where(
                    headerQuery.TransactionCode != TransactionCode.DownPayment,
                    headerQuery.RegistrationNo.In(registrationNo),
                    headerQuery.IsVoid == false,
                    headerQuery.IsApproved == true
                    );

                headerColl.Load(headerQuery);

                decimal total = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    var detailQuery = new TransPaymentItemQuery();

                    detailQuery.Where(
                        detailQuery.PaymentNo == header.PaymentNo,
                        detailQuery.SRPaymentType == AppSession.Parameter.PaymentTypeDiscount
                        );
                    detailColl.Load(detailQuery);

                    total += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                }

                return total;
            }

            public static decimal GetPaymentDiscount(string[] registrationNo, bool isToGuarantor)
            {
                var headerColl = new TransPaymentCollection();
                var headerQuery = new TransPaymentQuery();

                headerQuery.Where(
                    headerQuery.TransactionCode != TransactionCode.DownPayment,
                    headerQuery.RegistrationNo.In(registrationNo),
                    headerQuery.IsVoid == false,
                    headerQuery.IsApproved == true,
                    headerQuery.IsToGuarantor == isToGuarantor
                    );

                headerColl.Load(headerQuery);

                decimal total = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    var detailQuery = new TransPaymentItemQuery();

                    detailQuery.Where(
                        detailQuery.PaymentNo == header.PaymentNo,
                        detailQuery.SRPaymentType == AppSession.Parameter.PaymentTypeDiscount
                        );
                    detailColl.Load(detailQuery);

                    total += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                }

                return total;
            }

            public static DataTable GetDownPaymentOutstanding(string[] RegistrationNo, TransPaymentItemOrderCollection tpios, TransPaymentItemIntermBillCollection tpiibs, string regType)
            {
                var regcoll = new RegistrationCollection();
                regcoll.Query.Where(regcoll.Query.RegistrationNo.In(RegistrationNo));
                regcoll.LoadAll();

                var patientIds = (regcoll.Select(i => i.PatientID)).Distinct();

                //registrations
                var htp = new TransPaymentQuery("a");
                var dtp = new TransPaymentItemQuery("b");
                htp.InnerJoin(dtp).On(
                    htp.PaymentNo == dtp.PaymentNo &&
                    htp.IsApproved == true &&
                    htp.TransactionCode == BusinessObject.Reference.TransactionCode.DownPaymentReturn
                  );
                htp.Where(htp.RegistrationNo.In(RegistrationNo));
                htp.Select(dtp.ReferenceNo);
                DataTable dtb = htp.LoadDataTable();

                //patients
                htp = new TransPaymentQuery("a");
                dtp = new TransPaymentItemQuery("b");
                htp.InnerJoin(dtp).On(
                    htp.PaymentNo == dtp.PaymentNo &&
                    htp.IsApproved == true &&
                    htp.TransactionCode == BusinessObject.Reference.TransactionCode.DownPaymentReturn
                  );
                htp.Where(htp.RegistrationNo == string.Empty, htp.PatientID.In(patientIds));
                htp.Select(dtp.ReferenceNo);
                DataTable dtb2 = htp.LoadDataTable();

                dtb.Merge(dtb2);

                // dp noreg
                var detail = new TransPaymentItemQuery("a");
                var header = new TransPaymentQuery("b");
                var method = new PaymentMethodQuery("c");

                detail.es.Distinct = true;

                detail.Select
                    (
                        "<'Down Payment' AS [Group]>",
                        detail.PaymentNo,
                        detail.SequenceNo,
                        @"<'' AS ItemID>",
                        header.PaymentDate,
                        header.PaymentReferenceNo,
                        method.PaymentMethodName,
                        detail.Amount,
                        header.IsApproved,
                        header.IsVoid,
                        header.IsVisiteDownPayment
                    );

                detail.InnerJoin(header).On(detail.PaymentNo == header.PaymentNo);
                detail.InnerJoin(method).On(
                    detail.SRPaymentType == method.SRPaymentTypeID &&
                    detail.SRPaymentMethod == method.SRPaymentMethodID
                    );

                detail.Where
                    (
                        header.TransactionCode == BusinessObject.Reference.TransactionCode.DownPayment,
                        header.RegistrationNo.In(RegistrationNo),
                        header.PaymentReferenceNo == string.Empty,
                        header.IsApproved == true,
                        header.IsVoid == false,
                        header.IsVisiteDownPayment == false,
                        detail.SRPaymentType == AppSession.Parameter.PaymentTypeDownPayment
                    );

                if (dtb.AsEnumerable().Any()) detail.Where(header.PaymentNo.NotIn(dtb.AsEnumerable().Select(d => d.Field<string>("ReferenceNo"))));

                var tab = detail.LoadDataTable();

                // dp patient IsVisiteDownPayment == false
                detail = new TransPaymentItemQuery("a");
                header = new TransPaymentQuery("b");
                method = new PaymentMethodQuery("c");

                detail.es.Distinct = true;

                detail.Select
                    (
                        "<'Down Payment' AS [Group]>",
                        detail.PaymentNo,
                        detail.SequenceNo,
                        @"<'' AS ItemID>",
                        header.PaymentDate,
                        header.PaymentReferenceNo,
                        method.PaymentMethodName,
                        detail.Amount,
                        header.IsApproved,
                        header.IsVoid,
                        header.IsVisiteDownPayment
                    );

                detail.InnerJoin(header).On(detail.PaymentNo == header.PaymentNo);
                detail.InnerJoin(method).On(
                    detail.SRPaymentType == method.SRPaymentTypeID &&
                    detail.SRPaymentMethod == method.SRPaymentMethodID
                    );

                detail.Where
                    (
                        header.TransactionCode == BusinessObject.Reference.TransactionCode.DownPayment,
                        header.RegistrationNo == string.Empty,
                        header.PatientID.In(patientIds),
                        header.PaymentReferenceNo == string.Empty,
                        header.IsApproved == true,
                        header.IsVoid == false,
                        header.IsVisiteDownPayment == false,
                        detail.SRPaymentType == AppSession.Parameter.PaymentTypeDownPayment
                    );

                if (dtb.AsEnumerable().Any()) detail.Where(header.PaymentNo.NotIn(dtb.AsEnumerable().Select(d => d.Field<string>("ReferenceNo"))));

                var tab2 = detail.LoadDataTable();

                tab.Merge(tab2);

                //-- IsVisiteDownPayment = true
                if (regType == "OPR" && (tpios != null || tpiibs != null))
                {
                    // dp patient IsVisiteDownPayment == true & sudah di payment
                    detail = new TransPaymentItemQuery("a");
                    header = new TransPaymentQuery("b");
                    method = new PaymentMethodQuery("c");
                    var visit = new TransPaymentItemVisiteQuery("d");

                    detail.es.Distinct = true;

                    detail.Select
                        (
                            "<'Down Payment' AS [Group]>",
                            detail.PaymentNo,
                            detail.SequenceNo,
                            visit.ItemID,
                            header.PaymentDate,
                            header.PaymentReferenceNo,
                            method.PaymentMethodName,
                            //detail.Amount,
                            @"<(d.Price - (d.Price * d.Discount / 100)) AS Amount>",
                            header.IsApproved,
                            header.IsVoid,
                            header.IsVisiteDownPayment
                        );

                    detail.InnerJoin(header).On(detail.PaymentNo == header.PaymentNo);
                    detail.InnerJoin(method).On(
                        detail.SRPaymentType == method.SRPaymentTypeID &&
                        detail.SRPaymentMethod == method.SRPaymentMethodID
                        );
                    detail.InnerJoin(visit).On(detail.PaymentNo == visit.PaymentNo);

                    detail.Where
                        (
                            header.TransactionCode == BusinessObject.Reference.TransactionCode.DownPayment,
                            header.PatientID.In(patientIds),
                            header.IsApproved == true,
                            header.IsVoid == false,
                            header.IsVisiteDownPayment == true,
                            detail.SRPaymentType == AppSession.Parameter.PaymentTypeDownPayment,
                            detail.Or(header.IsClosedVisiteDownPayment.IsNull(), header.IsClosedVisiteDownPayment == false)
                        );
                    detail.Where("<(d.VisiteQty - d.RealizationQty) > 0>");

                    if (dtb.AsEnumerable().Any()) detail.Where(header.PaymentNo.NotIn(dtb.AsEnumerable().Select(d => d.Field<string>("ReferenceNo"))));

                    var tab3 = detail.LoadDataTable();

                    if (tab3.Rows.Count > 0)
                    {
                        var items = string.Empty;
                        if (tpios != null && tpios.Count > 0)
                        {
                            foreach (var i in tpios)
                            {
                                if (items == string.Empty)
                                    items = i.ItemID;
                                else
                                    items += ("|" + i.ItemID);
                            }
                        }
                        else if (tpiibs != null && tpiibs.Count > 0)
                        {
                            foreach (var i in tpiibs)
                            {
                                var cc = new CostCalculationCollection();
                                cc.Query.Where(cc.Query.IntermBillNo == i.IntermBillNo);
                                cc.LoadAll();
                                foreach (var c in cc)
                                {
                                    if (items == string.Empty)
                                        items = c.ItemID;
                                    else
                                        items += ("|" + c.ItemID);
                                }
                            }
                        }

                        foreach (DataRow row in tab3.Rows)
                        {
                            if (!items.Contains(row["ItemID"].ToString()))
                            {
                                row.Delete();
                                break;
                            }
                        }

                        tab3.AcceptChanges();
                    }

                    tab.Merge(tab3);
                }

                return tab;
            }

            public static DataTable GetDownPaymentOutstandingByPatientID(string patientId)
            {
                var htp = new TransPaymentQuery("a");
                var dtp = new TransPaymentItemQuery("b");
                htp.InnerJoin(dtp).On(
                    htp.PaymentNo == dtp.PaymentNo &&
                    htp.PatientID == patientId &&
                    htp.IsApproved == true &&
                    htp.TransactionCode == BusinessObject.Reference.TransactionCode.DownPaymentReturn
                  );
                htp.Select(dtp.ReferenceNo);
                htp.Where(htp.RegistrationNo == string.Empty);

                DataTable dtb = htp.LoadDataTable();

                var detail = new TransPaymentItemQuery("a");
                var header = new TransPaymentQuery("b");
                var method = new PaymentMethodQuery("c");

                detail.es.Distinct = true;

                detail.Select
                    (
                        "<'Down Payment' AS [Group]>",
                        detail.PaymentNo,
                        detail.SequenceNo,
                        @"<'' AS ItemID>",
                        header.PaymentDate,
                        header.PaymentReferenceNo,
                        method.PaymentMethodName,
                        detail.Amount,
                        header.IsApproved,
                        header.IsVoid,
                        header.IsVisiteDownPayment
                    );

                detail.InnerJoin(header).On(detail.PaymentNo == header.PaymentNo);
                detail.InnerJoin(method).On(
                    detail.SRPaymentType == method.SRPaymentTypeID &&
                    detail.SRPaymentMethod == method.SRPaymentMethodID
                    );

                detail.Where
                    (
                        header.TransactionCode == BusinessObject.Reference.TransactionCode.DownPayment,
                        header.RegistrationNo == string.Empty,
                        header.PatientID == patientId,
                        header.PaymentReferenceNo == string.Empty,
                        header.IsApproved == true,
                        header.IsVoid == false,
                        detail.SRPaymentType == AppSession.Parameter.PaymentTypeDownPayment
                    );

                if (dtb.AsEnumerable().Any()) detail.Where(header.PaymentNo.NotIn(dtb.AsEnumerable().Select(d => d.Field<string>("ReferenceNo"))));
                var tab = detail.LoadDataTable();

                return tab;
            }

            public static DataTable GetVisitDownPaymentOutstandingByPatientId(string patientId)
            {
                var htp = new TransPaymentQuery("a");
                var dtp = new TransPaymentItemQuery("b");
                htp.InnerJoin(dtp).On(
                    htp.PaymentNo == dtp.PaymentNo &&
                    htp.PatientID == patientId &&
                    htp.IsApproved == true &&
                    htp.TransactionCode == BusinessObject.Reference.TransactionCode.DownPaymentReturn
                  );
                htp.Select(dtp.ReferenceNo);
                htp.Where(htp.RegistrationNo == string.Empty);

                DataTable dtb = htp.LoadDataTable();

                // IsVisiteDownPayment = true
                var header = new TransPaymentQuery("a");
                var visit = new TransPaymentItemVisiteQuery("b");

                header.Select
                    (
                        "<'Down Payment' AS [Group]>",
                        header.PaymentNo,
                        header.PaymentDate,
                        header.PaymentTime,
                        @"<SUM((b.VisiteQty - b.RealizationQty) * (b.Price - (b.Price * b.Discount / 100))) AS 'Amount'>",
                        header.IsVisiteDownPayment
                    );

                header.InnerJoin(visit).On(visit.PaymentNo == header.PaymentNo);

                header.Where
                    (
                        header.TransactionCode == BusinessObject.Reference.TransactionCode.DownPayment,
                        header.RegistrationNo == string.Empty,
                        header.PatientID == patientId,
                        header.IsApproved == true,
                        header.IsVoid == false,
                        header.IsVisiteDownPayment == true,
                        header.Or(header.IsClosedVisiteDownPayment.IsNull(), header.IsClosedVisiteDownPayment == false)
                    );
                header.GroupBy
                    (header.PaymentNo,
                        header.PaymentDate,
                        header.PaymentTime,
                        header.IsVisiteDownPayment);

                if (dtb.AsEnumerable().Any()) header.Where(header.PaymentNo.NotIn(dtb.AsEnumerable().Select(d => d.Field<string>("ReferenceNo"))));

                var tab = header.LoadDataTable();
                if (tab.Rows.Count > 0)
                {
                    foreach (DataRow row in tab.Rows)
                    {
                        if (Convert.ToDecimal(row["Amount"]) == 0)
                        {
                            row.Delete();
                            break;
                        }
                    }
                    tab.AcceptChanges();
                }

                return tab;
            }

            public static decimal GetTotalDownPayment(string[] registrationNo)
            {
                var headerColl = new TransPaymentCollection();
                var headerQuery = new TransPaymentQuery();

                headerQuery.Where(
                    headerQuery.TransactionCode == TransactionCode.DownPayment,
                    headerQuery.RegistrationNo.In(registrationNo),
                    headerQuery.IsApproved == true,
                    headerQuery.IsVoid == false,
                    headerQuery.IsVisiteDownPayment == false
                    );

                headerColl.Load(headerQuery);

                decimal total = 0, payment = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    var detailQuery = new TransPaymentItemQuery();

                    detailQuery.Where(
                        detailQuery.PaymentNo == header.PaymentNo,
                        detailQuery.SRPaymentType == AppSession.Parameter.PaymentTypeDownPayment
                        );
                    detailColl.Load(detailQuery);

                    total += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                }

                // payment based dp
                headerColl = new TransPaymentCollection();

                headerQuery = new TransPaymentQuery();
                headerQuery.Where
                    (
                        headerQuery.TransactionCode == TransactionCode.Payment,
                        headerQuery.RegistrationNo.In(registrationNo),
                        headerQuery.IsApproved == true,
                        headerQuery.IsVoid == false,
                        headerQuery.IsVisiteDownPayment == false
                    );

                headerColl.Load(headerQuery);

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    var detailQuery = new TransPaymentItemQuery();

                    detailQuery.Where
                        (
                            detailQuery.PaymentNo == header.PaymentNo,
                            detailQuery.IsFromDownPayment == true
                        );
                    detailColl.Load(detailQuery);

                    payment += detailColl.Sum(detail => detail.Amount.Value + detail.Balance.Value);
                }

                var regcoll = new RegistrationCollection();
                regcoll.Query.Where(regcoll.Query.RegistrationNo.In(registrationNo));
                regcoll.LoadAll();

                var patientIds = (regcoll.Select(i => i.PatientID)).Distinct();
                if (patientIds.Any())
                {
                    #region dp patient tanpa noreg : IsVisiteDownPayment == false
                    // dp patient tanpa noreg
                    headerColl = new TransPaymentCollection();
                    headerQuery = new TransPaymentQuery();

                    headerQuery.Where(
                        headerQuery.TransactionCode == TransactionCode.DownPayment,
                        headerQuery.RegistrationNo == string.Empty,
                        headerQuery.PatientID.In(patientIds),
                        headerQuery.IsApproved == true,
                        headerQuery.IsVoid == false,
                        headerQuery.IsVisiteDownPayment == false
                        );

                    headerColl.Load(headerQuery);

                    foreach (var header in headerColl)
                    {
                        var detailColl = new TransPaymentItemCollection();
                        var detailQuery = new TransPaymentItemQuery();

                        detailQuery.Where(
                            detailQuery.PaymentNo == header.PaymentNo,
                            detailQuery.SRPaymentType == AppSession.Parameter.PaymentTypeDownPayment
                            );
                        detailColl.Load(detailQuery);

                        total += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                    }
                    #endregion

                    #region dp patient tanpa noreg : IsVisiteDownPayment == true
                    // dp patient tanpa noreg
                    headerColl = new TransPaymentCollection();
                    headerQuery = new TransPaymentQuery();

                    headerQuery.Where(
                        headerQuery.TransactionCode == TransactionCode.DownPayment,
                        headerQuery.RegistrationNo == string.Empty,
                        headerQuery.PatientID.In(patientIds),
                        headerQuery.IsApproved == true,
                        headerQuery.IsVoid == false,
                        headerQuery.IsVisiteDownPayment == true
                        );

                    headerColl.Load(headerQuery);

                    foreach (var header in headerColl)
                    {
                        var detailColl = new TransPaymentItemCollection();
                        var detailQuery = new TransPaymentItemQuery();

                        detailQuery.Where(
                            detailQuery.PaymentNo == header.PaymentNo,
                            detailQuery.SRPaymentType == AppSession.Parameter.PaymentTypeDownPayment
                            );
                        detailColl.Load(detailQuery);

                        total += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                    }
                    #endregion

                    #region retur dp patient tanpa noreg
                    // retur dp patient tanpa noreg
                    headerColl = new TransPaymentCollection();

                    headerQuery = new TransPaymentQuery();
                    headerQuery.Where(
                        headerQuery.TransactionCode == TransactionCode.DownPaymentReturn,
                        headerQuery.RegistrationNo == string.Empty,
                        headerQuery.PatientID.In(patientIds),
                        headerQuery.IsApproved == true,
                        headerQuery.IsVoid == false
                        //, headerQuery.IsVisiteDownPayment == false
                        );

                    headerColl.Load(headerQuery);

                    foreach (var header in headerColl)
                    {
                        var detailColl = new TransPaymentItemCollection();
                        var detailQuery = new TransPaymentItemQuery();

                        detailQuery.Where
                            (
                                detailQuery.PaymentNo == header.PaymentNo,
                                detailQuery.SRPaymentType == AppSession.Parameter.PaymentTypeDownPayment
                            );
                        detailColl.Load(detailQuery);

                        payment += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                    }
                    #endregion

                    #region visit realization
                    var visiteRealizationQuery = new TransPaymentItemVisiteRealizationQuery("a");
                    var itemVisitQuery = new TransPaymentItemVisiteQuery("b");
                    var paymentRefQuery = new TransPaymentQuery("c");
                    visiteRealizationQuery.InnerJoin(itemVisitQuery).On(itemVisitQuery.PaymentNo == visiteRealizationQuery.PaymentNo &&
                        itemVisitQuery.PatientID == visiteRealizationQuery.PatientID &&
                        itemVisitQuery.ItemID == visiteRealizationQuery.ItemID);
                    visiteRealizationQuery.InnerJoin(paymentRefQuery).On(paymentRefQuery.PaymentNo == visiteRealizationQuery.PaymentReferenceNo);
                    visiteRealizationQuery.Where(visiteRealizationQuery.PatientID.In(patientIds), paymentRefQuery.RegistrationNo.NotIn(registrationNo));
                    visiteRealizationQuery.Select(visiteRealizationQuery.PatientID, @"<SUM(b.Price - (b.Price * b.Discount / 100)) AS 'Amount'>");
                    visiteRealizationQuery.GroupBy(visiteRealizationQuery.PatientID);
                    var dtbVisit = visiteRealizationQuery.LoadDataTable();
                    if (dtbVisit.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtbVisit.Rows)
                        {
                            payment += Convert.ToDecimal(row["Amount"]);
                        }
                    }
                    #endregion

                    #region closing visit dp
                    var closingHdQ = new ClosingVisiteDownPaymentQuery("a");
                    var closingDtQ = new ClosingVisiteDownPaymentItemQuery("b");
                    closingHdQ.InnerJoin(closingDtQ).On(closingDtQ.ClosingNo == closingHdQ.ClosingNo);
                    closingHdQ.Where(closingHdQ.PatientID.In(patientIds), closingHdQ.IsApproved == true);
                    closingHdQ.Select(closingHdQ.PatientID, closingDtQ.Amount.Sum());
                    closingHdQ.GroupBy(closingHdQ.PatientID);
                    var dtbClosing = closingHdQ.LoadDataTable();
                    if (dtbClosing.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtbClosing.Rows)
                        {
                            payment += Convert.ToDecimal(row["Amount"]);
                        }
                    }
                    #endregion
                }

                return (total > payment ? (total - payment) : 0);
            }

            public static decimal GetTotalDownPaymentOnly(string[] registrationNo)
            {
                var headerColl = new TransPaymentCollection();
                var headerQuery = new TransPaymentQuery();

                headerQuery.Where(
                    headerQuery.TransactionCode == TransactionCode.DownPayment,
                    headerQuery.RegistrationNo.In(registrationNo),
                    headerQuery.IsApproved == true,
                    headerQuery.IsVoid == false,
                    headerQuery.IsVisiteDownPayment == false
                    );

                headerColl.Load(headerQuery);

                decimal total = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    var detailQuery = new TransPaymentItemQuery();

                    detailQuery.Where(
                        detailQuery.PaymentNo == header.PaymentNo,
                        detailQuery.SRPaymentType == AppSession.Parameter.PaymentTypeDownPayment
                        );
                    detailColl.Load(detailQuery);

                    total += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                }

                return total;
            }

            public static decimal GetTotalDownPaymentOnly(string registrationNo)
            {
                var headerColl = new TransPaymentCollection();
                var headerQuery = new TransPaymentQuery();

                headerQuery.Where(
                    headerQuery.TransactionCode == TransactionCode.DownPayment,
                    headerQuery.RegistrationNo == registrationNo,
                    headerQuery.IsApproved == true,
                    headerQuery.IsVoid == false,
                    headerQuery.IsVisiteDownPayment == false
                    );

                headerColl.Load(headerQuery);

                decimal total = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    var detailQuery = new TransPaymentItemQuery();

                    detailQuery.Where(
                        detailQuery.PaymentNo == header.PaymentNo,
                        detailQuery.SRPaymentType == AppSession.Parameter.PaymentTypeDownPayment
                        );
                    detailColl.Load(detailQuery);

                    total += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                }

                return total;
            }

            public static decimal GetTotalDownPayment(string[] registrationNo, DateTime toDate)
            {
                var headerColl = new TransPaymentCollection();
                var headerQuery = new TransPaymentQuery();

                headerQuery.Where(
                    headerQuery.TransactionCode.In(TransactionCode.DownPayment, TransactionCode.DownPaymentReturn),
                    headerQuery.RegistrationNo.In(registrationNo),
                    headerQuery.IsApproved == true,
                    headerQuery.IsVoid == false,
                    headerQuery.IsVisiteDownPayment == false,
                    headerQuery.PaymentDate <= toDate
                    );

                headerColl.Load(headerQuery);

                decimal total = 0, payment = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    var detailQuery = new TransPaymentItemQuery();

                    detailQuery.Where(
                        detailQuery.PaymentNo == header.PaymentNo,
                        detailQuery.SRPaymentType == AppSession.Parameter.PaymentTypeDownPayment
                        );
                    detailColl.Load(detailQuery);

                    if (header.TransactionCode == TransactionCode.DownPayment)
                        total += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                    else
                        total += detailColl.Sum(detail => (-1) * (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                }

                // payment based dp
                headerColl = new TransPaymentCollection();

                headerQuery = new TransPaymentQuery();
                headerQuery.Where
                    (
                        headerQuery.TransactionCode == TransactionCode.Payment,
                        headerQuery.RegistrationNo.In(registrationNo),
                        headerQuery.IsApproved == true,
                        headerQuery.IsVoid == false,
                        headerQuery.IsVisiteDownPayment == false
                    );

                headerColl.Load(headerQuery);

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    var detailQuery = new TransPaymentItemQuery();

                    detailQuery.Where
                        (
                            detailQuery.PaymentNo == header.PaymentNo,
                            detailQuery.IsFromDownPayment == true
                        );
                    detailColl.Load(detailQuery);

                    payment += detailColl.Sum(detail => detail.Amount.Value);
                }


                return total - payment;
            }

            public static decimal GetTotalDownPayment(string registrationNo, DateTime toDate)
            {
                var headerColl = new TransPaymentCollection();
                var headerQuery = new TransPaymentQuery();

                headerQuery.Where(
                    headerQuery.TransactionCode == TransactionCode.DownPayment,
                    headerQuery.RegistrationNo == registrationNo,
                    headerQuery.IsApproved == true,
                    headerQuery.IsVoid == false,
                    headerQuery.IsVisiteDownPayment == false,
                    headerQuery.PaymentDate <= toDate
                    );
                DataTable dtb = headerQuery.LoadDataTable();

                headerColl.Load(headerQuery);

                decimal total = 0, payment = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    var detailQuery = new TransPaymentItemQuery();

                    detailQuery.Where(
                        detailQuery.PaymentNo == header.PaymentNo,
                        detailQuery.SRPaymentType == AppSession.Parameter.PaymentTypeDownPayment
                        );
                    detailColl.Load(detailQuery);

                    total += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                }

                // payment based dp
                headerColl = new TransPaymentCollection();

                headerQuery = new TransPaymentQuery();
                headerQuery.Where
                    (
                        headerQuery.TransactionCode == TransactionCode.Payment,
                        headerQuery.RegistrationNo == registrationNo,
                        headerQuery.IsApproved == true,
                        headerQuery.IsVoid == false,
                        headerQuery.IsVisiteDownPayment == false
                    );

                headerColl.Load(headerQuery);

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    var detailQuery = new TransPaymentItemQuery();

                    detailQuery.Where
                        (
                            detailQuery.PaymentNo == header.PaymentNo,
                            detailQuery.IsFromDownPayment == true
                        );
                    detailColl.Load(detailQuery);

                    payment += detailColl.Sum(detail => detail.Amount.Value);
                }


                return total - payment;
            }

            public static decimal GetTotalDownPaymentOutstanding(string[] registrationNo, DateTime toDate)
            {
                var headerColl = new TransPaymentCollection();
                var headerQuery = new TransPaymentQuery();

                headerQuery.Where(
                    headerQuery.TransactionCode == TransactionCode.DownPayment,
                    headerQuery.RegistrationNo.In(registrationNo),
                    headerQuery.IsApproved == true,
                    headerQuery.IsVoid == false,
                    headerQuery.IsVisiteDownPayment == false,
                    headerQuery.PaymentDate <= toDate,
                    headerQuery.ReferenceNo == string.Empty
                    );

                headerColl.Load(headerQuery);

                decimal total = 0, payment = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    var detailQuery = new TransPaymentItemQuery();

                    detailQuery.Where(
                        detailQuery.PaymentNo == header.PaymentNo,
                        detailQuery.SRPaymentType == AppSession.Parameter.PaymentTypeDownPayment
                        );
                    detailColl.Load(detailQuery);

                    total += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                }

                // payment based dp
                headerColl = new TransPaymentCollection();

                headerQuery = new TransPaymentQuery();
                headerQuery.Where
                    (
                        headerQuery.TransactionCode == TransactionCode.Payment,
                        headerQuery.RegistrationNo.In(registrationNo),
                        headerQuery.IsApproved == true,
                        headerQuery.IsVoid == false,
                        headerQuery.IsVisiteDownPayment == false
                    );

                headerColl.Load(headerQuery);

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    var detailQuery = new TransPaymentItemQuery();

                    detailQuery.Where
                        (
                            detailQuery.PaymentNo == header.PaymentNo,
                            detailQuery.IsFromDownPayment == true
                        );
                    detailColl.Load(detailQuery);

                    payment += detailColl.Sum(detail => detail.Amount.Value);
                }


                return total - payment;
            }

            public static decimal GetTotalDownPaymentReturn(string registrationNo)
            {
                var headerColl = new TransPaymentCollection();
                var headerQuery = new TransPaymentQuery();

                headerQuery.Where(
                    headerQuery.TransactionCode == TransactionCode.DownPaymentReturn,
                    headerQuery.RegistrationNo == registrationNo,
                    headerQuery.IsApproved == true,
                    headerQuery.IsVoid == false
                    );

                headerColl.Load(headerQuery);

                decimal total = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    var detailQuery = new TransPaymentItemQuery();

                    detailQuery.Where(
                        detailQuery.PaymentNo == header.PaymentNo,
                        detailQuery.SRPaymentType == AppSession.Parameter.PaymentTypeDownPayment
                        );
                    detailColl.Load(detailQuery);

                    total += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                }

                return total;
            }

            public static decimal GetTotalDownPaymentReturn(string[] registrationNo)
            {
                var headerColl = new TransPaymentCollection();
                var headerQuery = new TransPaymentQuery();

                headerQuery.Where(
                    headerQuery.TransactionCode == TransactionCode.DownPaymentReturn,
                    headerQuery.RegistrationNo.In(registrationNo),
                    headerQuery.IsApproved == true,
                    headerQuery.IsVoid == false
                    );

                headerColl.Load(headerQuery);

                decimal total = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    var detailQuery = new TransPaymentItemQuery();

                    detailQuery.Where(
                        detailQuery.PaymentNo == header.PaymentNo,
                        detailQuery.SRPaymentType == AppSession.Parameter.PaymentTypeDownPayment
                        );
                    detailColl.Load(detailQuery);

                    total += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                }

                return total;
            }

            public static decimal GetTotalRoundingPayment(string paymentNo)
            {
                var headerColl = new TransPaymentCollection();
                var headerQuery = new TransPaymentQuery();

                headerQuery.Where(
                    headerQuery.PaymentNo == paymentNo,
                    headerQuery.IsVoid == false//,
                                               //headerQuery.IsApproved == true
                    );

                headerQuery.Where(headerQuery.TransactionCode == TransactionCode.Payment);

                headerColl.Load(headerQuery);

                decimal rounding = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    var detailQuery = new TransPaymentItemQuery();

                    detailQuery.Where(detailQuery.PaymentNo == header.PaymentNo);
                    detailColl.Load(detailQuery);

                    rounding += detailColl.Sum(detail => detail.RoundingAmount ?? 0);
                }

                return rounding;
            }

            public static bool IsPaymentExist(string[] registrationNo)
            {
                var headerColl = new TransPaymentCollection();
                var headerQuery = new TransPaymentQuery();

                headerQuery.Where
                    (
                    headerQuery.Or
                        (
                        headerQuery.TransactionCode == TransactionCode.Payment,
                        headerQuery.TransactionCode == TransactionCode.PaymentReturn
                        ),
                    headerQuery.RegistrationNo.In(registrationNo),
                    headerQuery.IsVoid == false,
                    headerQuery.IsApproved == true
                    );
                return headerColl.Load(headerQuery);
            }

            public static bool IsPaymentZeroExist(string[] registrationNo)
            {
                var headerColl = new TransPaymentCollection();
                var headerQuery = new TransPaymentQuery("a");
                var detailQuery = new TransPaymentItemQuery("b");
                headerQuery.InnerJoin(detailQuery).On(detailQuery.PaymentNo == headerQuery.PaymentNo);
                headerQuery.Where
                    (
                    headerQuery.TransactionCode == TransactionCode.Payment,
                    headerQuery.RegistrationNo.In(registrationNo),
                    headerQuery.IsVoid == false,
                    headerQuery.IsApproved == true,
                    detailQuery.SRPaymentType == AppSession.Parameter.PaymentTypePersonalAR,
                    detailQuery.Amount == 0
                    );
                return headerColl.Load(headerQuery);
            }

            public static decimal GetTotalPaymentAll(string[] registrationNo)
            {
                var headerColl = new TransPaymentCollection();
                var headerQuery = new TransPaymentQuery();

                headerQuery.Where
                    (
                    headerQuery.Or
                        (
                        headerQuery.TransactionCode == TransactionCode.Payment,
                        headerQuery.TransactionCode == TransactionCode.PaymentReturn
                        ),
                    headerQuery.RegistrationNo.In(registrationNo),
                    headerQuery.IsVoid == false,
                    headerQuery.IsApproved == true
                    );
                headerColl.Load(headerQuery);

                decimal total = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    var detailQuery = new TransPaymentItemQuery();

                    detailQuery.Where(detailQuery.PaymentNo == header.PaymentNo);
                    detailColl.Load(detailQuery);

                    total += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                }

                return total;
            }

            public static decimal GetTotalPaymentByIntermbill(string[] IntermbillNo,
                bool isToPatient, bool isToGuarantor, decimal Plafon)
            {
                List<string> PayNos = new List<string>();

                if (IntermbillNo.Count() == 0) return 0;

                if (isToPatient)
                {
                    var tpibColl = new TransPaymentItemIntermBillCollection();
                    var tpib = new TransPaymentItemIntermBillQuery();
                    tpib.Where(tpib.IntermBillNo.In(IntermbillNo));
                    if (tpibColl.Load(tpib))
                    {
                        PayNos.AddRange(tpibColl.Select(x => x.PaymentNo));
                    }
                }
                if (isToGuarantor)
                {
                    var tpibgColl = new TransPaymentItemIntermBillGuarantorCollection();
                    var tpibg = new TransPaymentItemIntermBillGuarantorQuery();
                    tpibg.Where(tpibg.IntermBillNo.In(IntermbillNo));
                    if (tpibgColl.Load(tpibg))
                    {
                        PayNos.AddRange(tpibgColl.Select(x => x.PaymentNo));
                    }
                }

                decimal ExcessGuar = 0;

                if (Plafon > 0 && isToGuarantor == false)
                {
                    // kelebihan payment guarantor (karena diskon) jadi pengurang tagihan pasien

                    var totalARGuarPaid = GetTotalPaymentByIntermbill(IntermbillNo, false, true, Plafon);
                    if (totalARGuarPaid > Plafon) ExcessGuar = totalARGuarPaid - Plafon;
                }

                var payNos2 = PayNos.Distinct();
                if (payNos2.Count() == 0) return ExcessGuar;

                var headerColl = new TransPaymentCollection();
                var headerQuery = new TransPaymentQuery();

                headerQuery.Where
                    (
                    headerQuery.Or
                        (
                        headerQuery.TransactionCode == TransactionCode.Payment,
                        headerQuery.TransactionCode == TransactionCode.PaymentReturn
                        ),
                    headerQuery.Or(
                        headerQuery.PaymentNo.In(payNos2),
                        (headerQuery.And(
                            headerQuery.PaymentReferenceNo.In(payNos2),
                            headerQuery.TransactionCode == "017")
                        )
                    ),
                    headerQuery.IsVoid == false,
                    headerQuery.IsApproved == true
                    );
                headerColl.Load(headerQuery);

                decimal total = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    var detailQuery = new TransPaymentItemQuery();

                    detailQuery.Where(detailQuery.PaymentNo == header.PaymentNo);
                    detailColl.Load(detailQuery);

                    total += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                }

                return total + ExcessGuar;
            }

            public static decimal GetTotalPaymentByIntermbillIncludePlafonCOB(string RegistrationNo, string[] IntermbillNo,
                bool isToPatient, bool isToGuarantor, decimal Plafon)
            {
                //List<string> PayNos = new List<string>();
                if (IntermbillNo.Count() == 0) return 0;

                Dictionary<string, string> PayNoGuarIDs = new Dictionary<string, string>();
                if (isToPatient)
                {
                    //var tpibColl = new TransPaymentItemIntermBillCollection();
                    var tpib = new TransPaymentItemIntermBillQuery("tpiib");
                    var tp = new TransPaymentQuery("tp");
                    tpib.InnerJoin(tp).On(tpib.PaymentNo == tp.PaymentNo)
                        .Where(tp.IsVoid == false, tpib.IntermBillNo.In(IntermbillNo))
                        .Select(tpib.PaymentNo, tp.GuarantorID);
                    tpib.GroupBy(tpib.PaymentNo, tp.GuarantorID);
                    var dttb = tpib.LoadDataTable();
                    foreach (System.Data.DataRow row in dttb.Rows)
                    {
                        PayNoGuarIDs.Add(row["PaymentNo"].ToString(), row["GuarantorID"].ToString());
                    }
                }
                if (isToGuarantor)
                {
                    var tpibgColl = new TransPaymentItemIntermBillGuarantorCollection();
                    var tpibg = new TransPaymentItemIntermBillGuarantorQuery("tpiibg");
                    var tp = new TransPaymentQuery("tp");
                    tpibg.InnerJoin(tp).On(tpibg.PaymentNo == tp.PaymentNo)
                        .Where(tp.IsVoid == false, tpibg.IntermBillNo.In(IntermbillNo))
                        .Select(tpibg.PaymentNo, tp.GuarantorID);
                    tpibg.GroupBy(tpibg.PaymentNo, tp.GuarantorID);
                    var dttb = tpibg.LoadDataTable();
                    foreach (System.Data.DataRow row in dttb.Rows)
                    {
                        PayNoGuarIDs.Add(row["PaymentNo"].ToString(), row["GuarantorID"].ToString());
                    }
                }

                decimal ExcessGuar = 0;

                if (Plafon > 0 && isToGuarantor == false)
                {
                    // kelebihan payment guarantor (karena diskon) jadi pengurang tagihan pasien

                    var totalARGuarPaid = GetTotalPaymentByIntermbill(IntermbillNo, false, true, Plafon);
                    if (totalARGuarPaid > Plafon) ExcessGuar = totalARGuarPaid - Plafon;
                }

                // ambil plafon COB yang blm ada payment COB
                var RegGuarColl = new RegistrationGuarantorCollection();
                RegGuarColl.Query.Where(RegGuarColl.Query.RegistrationNo == RegistrationNo);
                RegGuarColl.LoadAll();

                decimal PlafonCOB = 0;
                var payNos2 = PayNoGuarIDs.Select(k => k.Key).Distinct().ToList(); //PayNos.Distinct();
                if (payNos2.Count() == 0) {
                    PlafonCOB = RegGuarColl.Sum(r => r.PlafondAmount ?? 0);
                    return ExcessGuar + PlafonCOB;
                } 

                var headerColl = new TransPaymentCollection();
                var headerQuery = new TransPaymentQuery();

                headerQuery.Where
                    (
                    headerQuery.Or
                        (
                        headerQuery.TransactionCode == TransactionCode.Payment,
                        headerQuery.TransactionCode == TransactionCode.PaymentReturn
                        ),
                    headerQuery.Or(
                        headerQuery.PaymentNo.In(payNos2),
                        (headerQuery.And(
                            headerQuery.PaymentReferenceNo.In(payNos2),
                            headerQuery.TransactionCode == "017")
                        )
                    ),
                    headerQuery.IsVoid == false,
                    headerQuery.IsApproved == true
                    );
                headerColl.Load(headerQuery);

                decimal total = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    var detailQuery = new TransPaymentItemQuery();

                    detailQuery.Where(detailQuery.PaymentNo == header.PaymentNo);
                    detailColl.Load(detailQuery);

                    total += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                }

                PlafonCOB = RegGuarColl.Where(r => headerColl.Select(h => h.GuarantorID).Contains(r.GuarantorID))
                    .Sum(r => r.PlafondAmount ?? 0);

                return total + ExcessGuar + PlafonCOB;
            }

            public static void SetApproval(TransPayment entity, TransPaymentItemCollection TransPaymentItems,
                TransPaymentItemOrderCollection TransPaymentItemOrders, TransPaymentItemIntermBillCollection TransPaymentItemIntermBills,
                TransPaymentItemIntermBillGuarantorCollection TransPaymentItemIntermBillGuarantors,
                bool isApprove, double remainingAmountPatient, double remainingAmountGuarantor, string programName)
            {
                entity.IsApproved = isApprove;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.ApproveByUserID = AppSession.UserLogin.UserID;
                entity.ApproveDate = entity.LastUpdateDateTime;

                //registration
                var reg = new Registration();
                reg.LoadByPrimaryKey(entity.RegistrationNo);

                var total = TransPaymentItems.Sum(detail => (decimal)detail.Amount);

                if (isApprove)
                    reg.RemainingAmount -= total;
                else
                    reg.RemainingAmount += total;

                if ((reg.SRRegistrationType == AppConstant.RegistrationType.InPatient && AppSession.Parameter.IsAutoClosedRegIpOnPayment) ||
                    (reg.SRRegistrationType != AppConstant.RegistrationType.InPatient && AppSession.Parameter.IsAutoClosedRegOpOnPayment))
                {
                    if (remainingAmountPatient == 0 & remainingAmountGuarantor == 0)
                    {
                        var isAutoClosedRegOnPaymentWithHoldTx = AppSession.Parameter.IsAutoClosedRegOnPaymentWithHoldTx;

                        var coll = new MergeBillingCollection();
                        coll.Query.Where(coll.Query.FromRegistrationNo == entity.RegistrationNo);
                        coll.LoadAll();

                        var regs = new string[coll.Count + 1];
                        var idx = 1;

                        regs.SetValue(entity.RegistrationNo, 0);

                        foreach (var bill in coll)
                        {
                            regs.SetValue(bill.RegistrationNo, idx);
                            idx++;
                        }

                        var regis = new RegistrationCollection();
                        regis.Query.Where(regis.Query.RegistrationNo.In(regs));
                        regis.LoadAll();

                        var historys = new RegistrationCloseOpenHistoryCollection();
                        bool isClosed = !((remainingAmountPatient + remainingAmountGuarantor) > 0) && isApprove;

                        foreach (var re in regis)
                        {
                            if (!isAutoClosedRegOnPaymentWithHoldTx)
                                re.IsClosed = !((remainingAmountPatient + remainingAmountGuarantor) > 0) && isApprove;
                            else
                            {
                                re.IsHoldTransactionEntry = !((remainingAmountPatient + remainingAmountGuarantor) > 0) && isApprove;
                                re.IsHoldTransactionEntryByUserID = AppSession.UserLogin.UserID;
                            }

                            re.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            re.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                            if (isClosed)
                            {
                                var hist = historys.AddNew();
                                hist.RegistrationNo = re.RegistrationNo;
                                hist.StatusId = !isAutoClosedRegOnPaymentWithHoldTx ? "C" : "H";
                                hist.IsTrue = true;
                                hist.Notes = programName;
                                hist.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                hist.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            }
                        }

                        var ques = new ServiceUnitQueCollection();
                        ques.Query.Where(ques.Query.RegistrationNo.In(regs));
                        ques.LoadAll();

                        foreach (var que in ques)
                        {
                            if (!isAutoClosedRegOnPaymentWithHoldTx)
                                que.IsClosed = !((remainingAmountPatient + remainingAmountGuarantor) > 0) && isApprove;

                            que.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            que.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        }

                        using (var trans = new esTransactionScope())
                        {
                            regis.Save();

                            if (historys.Count > 0)
                                historys.Save();

                            if (ques.Count > 0)
                                ques.Save();

                            trans.Complete();
                        }

                        var ques2 = new ServiceUnitQueCollection();
                        ques2.Query.Where(ques2.Query.RegistrationNo == entity.RegistrationNo);
                        ques2.LoadAll();

                        foreach (var que in ques2)
                        {
                            if (!isAutoClosedRegOnPaymentWithHoldTx)
                                que.IsClosed = !((remainingAmountPatient + remainingAmountGuarantor) > 0) && isApprove;

                            que.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            que.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        }

                        using (var trans = new esTransactionScope())
                        {
                            if (ques2.Count > 0)
                                ques2.Save();

                            trans.Complete();
                        }

                        if (!isAutoClosedRegOnPaymentWithHoldTx)
                            reg.IsClosed = !((remainingAmountPatient + remainingAmountGuarantor) > 0) && isApprove;
                        else
                        {
                            reg.IsHoldTransactionEntry = !((remainingAmountPatient + remainingAmountGuarantor) > 0) && isApprove;
                            reg.IsHoldTransactionEntryByUserID = AppSession.UserLogin.UserID;
                        }
                    }
                }

                foreach (var item in TransPaymentItemOrders)
                {
                    item.IsPaymentProceed = isApprove;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }

                foreach (var item in TransPaymentItemIntermBills)
                {
                    item.IsPaymentProceed = isApprove;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }

                foreach (var item in TransPaymentItemIntermBillGuarantors)
                {
                    item.IsPaymentProceed = isApprove;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }

                bool isAllowCheckout = TransPaymentItemIntermBills.Count > 0;

                using (var trans = new esTransactionScope())
                {
                    entity.Save();

                    if (AppSession.Parameter.IsNeedAllowCheckoutConfirmedForDischarge && isAllowCheckout)
                    {
                        reg.IsAllowPatientCheckOut = true;
                        reg.AllowPatientCheckOutByUserID = AppSession.UserLogin.UserID;
                        reg.AllowPatientCheckOutDateTime = (new DateTime()).NowAtSqlServer();
                    }

                    reg.Save();
                    TransPaymentItemOrders.Save();
                    TransPaymentItemIntermBills.Save();
                    TransPaymentItemIntermBillGuarantors.Save();

                    var package =
                        (TransPaymentItems.Where(p => p.SRPaymentMethod == AppSession.Parameter.PaymentMethodPackageBalance))
                            .SingleOrDefault();
                    if (package != null)
                    {
                        var pat = new Patient();
                        pat.LoadByPrimaryKey(reg.PatientID);

                        if (pat.PackageBalance != null)
                        {
                            if (pat.PackageBalance > 0)
                            {
                                if (isApprove)
                                {
                                    if (package.IsPackageClosed ?? false)
                                        pat.PackageBalance -= (package.Amount + package.Balance);
                                    else
                                        pat.PackageBalance -= package.Amount;
                                }
                                else
                                {
                                    if (package.IsPackageClosed ?? false)
                                        pat.PackageBalance += (package.Amount + package.Balance);
                                    else
                                        pat.PackageBalance += package.Amount;
                                }

                                pat.Save();
                            }
                        }
                    }

                    #region Guarantor Deposit Balance

                    var tpicoll = new TransPaymentItemCollection();
                    tpicoll.Query.Where(tpicoll.Query.PaymentNo == entity.PaymentNo,
                                        tpicoll.Query.SRPaymentType == AppSession.Parameter.PaymentTypeSaldoAR);
                    tpicoll.LoadAll();
                    if (tpicoll.Count > 0)
                    {
                        decimal totAmount = tpicoll.Sum(item => item.Amount ?? 0);

                        var balance = new GuarantorDepositBalance();
                        var movement = new GuarantorDepositMovement();
                        GuarantorDepositBalance.PrepareGuarantorDepositBalances(entity.PaymentNo, entity.GuarantorID,
                                                                                "A/R Process", AppSession.UserLogin.UserID,
                                                                                0,
                                                                                totAmount,
                                                                                ref balance, ref movement);
                        balance.Save();
                        movement.Save();
                    }

                    #endregion

                    #region Membership - Update Reward Point
                    var totPatientPayment = TransPaymentItems.Where(item => item.SRPaymentType == AppSession.Parameter.PaymentTypePayment).Sum(item => (item.Amount ?? 0));
                    if (reg.MembershipDetailID == -1)
                    {
                        reg.MembershipDetailID = Registration.GetMembershipDetailId(reg.PatientID, reg.RegistrationDate.Value.Date);
                        if (reg.MembershipDetailID != -1)
                            reg.Save();
                    }
                    if (reg.MembershipDetailID != -1)
                    {
                        var div = AppSession.Parameter.MultipleForRewardPoints;
                        var x = BusinessObject.MembershipDetail.UpdateRewardPoints(Convert.ToInt64(reg.MembershipDetailID), totPatientPayment, div, true, AppSession.UserLogin.UserID);
                    }
                    if (!string.IsNullOrEmpty(reg.MembershipNo))
                    {
                        var div = AppSession.Parameter.MultipleForRewardPointsForEmployee;
                        var x = BusinessObject.MembershipDetail.UpdateEmployeeRewardPoints(reg.MembershipNo, reg.RegistrationNo, totPatientPayment, div, true, AppSession.UserLogin.UserID);
                    }
                    #endregion

                    // rekal untuk prorata ???
                    var ba = new BillingAdjustment(entity.RegistrationNo, true);
                    var msg = ba.CalculateAndSaveProrata_NoTransactionScope(AppSession.UserLogin.UserID);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        //ShowInformationHeader(msg);
                        //return false;
                        throw new Exception(msg);
                    }

                    // update informasi payment jasmed
                    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    if (isApprove)
                    {
                        feeColl.RecalculateForFeeByPlafonGuarantor(entity, TransPaymentItems, AppSession.UserLogin.UserID);
                        feeColl.SetPayment(entity, 0, AppSession.UserLogin.UserID);
                    }
                    else
                    {
                        feeColl.UnSetPayment(entity, AppSession.UserLogin.UserID);
                    }
                    feeColl.Save();

                    if (AppSession.Parameter.IsJobOrderRealizationNeedConfirm)
                    {
                        if (isApprove)
                        {
                            foreach (var item in TransPaymentItemOrders)
                            {
                                var tci = new TransChargesItem();
                                if (tci.LoadByPrimaryKey(item.TransactionNo, item.SequenceNo))
                                {
                                    tci.IsPaymentConfirmed = true;
                                    tci.PaymentConfirmedBy = entity.PrintReceiptAsName;
                                    tci.PaymentConfirmedDateTime = (new DateTime()).NowAtSqlServer().Date;
                                    tci.LastPaymentConfirmedByUserID = AppSession.UserLogin.UserID;
                                    tci.LastPaymentConfirmedDateTime = (new DateTime()).NowAtSqlServer();
                                    tci.Save();
                                }
                            }
                        }
                        else
                        {
                            foreach (var item in TransPaymentItemOrders)
                            {
                                var tci = new TransChargesItem();
                                if (tci.LoadByPrimaryKey(item.TransactionNo, item.SequenceNo))
                                {
                                    tci.IsPaymentConfirmed = false;
                                    tci.PaymentConfirmedBy = string.Empty;
                                    tci.str.PaymentConfirmedDateTime = string.Empty;
                                    tci.LastPaymentConfirmedByUserID = AppSession.UserLogin.UserID;
                                    tci.LastPaymentConfirmedDateTime = (new DateTime()).NowAtSqlServer();
                                    tci.Save();
                                }
                            }
                        }
                    }

                    // update qty realization TransPaymentItemVisite
                    #region TransPaymentItemVisite
                    var visitList = (TransPaymentItems.Where(p => p.VisiteDownPaymentNotes != null & p.VisiteDownPaymentNotes != string.Empty));
                    if (visitList.Any() && visitList.Count() > 0)
                    {
                        var visiteRealizations = new TransPaymentItemVisiteRealizationCollection();

                        foreach (var v in visitList)
                        {
                            var val = v.VisiteDownPaymentNotes.Split(';');
                            var valLength = val.Length;
                            for (int i = 0; i < valLength; i++)
                            {
                                var val2 = val[i].Split('|');

                                var paymentNo = val2[0];
                                var itemId = val2[1];

                                var itemVisitQ = new TransPaymentItemVisiteQuery();
                                itemVisitQ.Where(itemVisitQ.PaymentNo == paymentNo, itemVisitQ.ItemID == itemId);

                                var itemVisit = new TransPaymentItemVisite();
                                if (itemVisit.Load(itemVisitQ) && itemVisit != null)
                                {
                                    itemVisit.RealizationQty += 1;
                                    itemVisit.Save();

                                    var visiteRealization = visiteRealizations.AddNew();
                                    visiteRealization.PaymentNo = itemVisit.PaymentNo;
                                    visiteRealization.PatientID = itemVisit.PatientID;
                                    visiteRealization.ItemID = itemVisit.ItemID;
                                    visiteRealization.PaymentReferenceNo = entity.PaymentNo;
                                    visiteRealization.RealizationQty = 1;
                                }
                            }
                        }

                        visiteRealizations.Save();
                    }
                    #endregion

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                // Create Journal Accounting
                CreateJournalAccounting(entity, TransPaymentItems, isApprove, reg);

                // checkout otomatis,
                Helper.RegistrationOpenClose.SetDischargeDate(reg);
            }

            public static void CreateJournalAccounting(TransPayment entity, TransPaymentItemCollection tpiColl,
            bool isApprove, Registration reg)
            {
                /* Automatic Journal Testing Start */
                if (isApprove)
                {
                    /* Automatic Journal Testing Start */
                    if (entity.TransactionCode == TransactionCode.PaymentReturn)
                    {
                        //function ini utk payment return dari pembelian resep
                        int? journalId =
                            JournalTransactions.AddNewPaymentReturnJournal(BusinessObject.JournalType.PaymentReturn,
                                entity, reg, tpiColl, "TP",
                                AppSession.UserLogin.UserID, 0);
                    }
                    else if (entity.TransactionCode == TransactionCode.Payment)
                    {
                        var x = (from tpi in tpiColl select tpi.SRPaymentType).Distinct();
                        string journalType = BusinessObject.JournalType.Payment;
                        if (x.Contains(AppSession.Parameter.PaymentTypeCorporateAR))
                        {
                            journalType = BusinessObject.JournalType.ARCreating;
                        }
                        else if (x.Contains(AppSession.Parameter.PaymentTypePersonalAR))
                        {
                            journalType = BusinessObject.JournalType.ARCreating;
                        }
                        else if (x.Contains(AppSession.Parameter.PaymentTypeSaldoAR))
                        {
                            //journalType = BusinessObject.JournalType.ARCreating;
                        }

                        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "Yes")
                        {
                            int? journalId = JournalTransactions.AddNewPaymentCashBasedJournal(journalType,
                                entity, reg, tpiColl,
                                "TP", entity.PaymentNo, AppSession.UserLogin.UserID, 0);
                        }
                        else
                        {
                            var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                            if (type.Contains(reg.SRRegistrationType))
                            {

                                int? journalId = JournalTransactions.AddNewPaymentCashBasedJournalTemporary(journalType,
                                    entity, reg, tpiColl,
                                    "TP", entity.PaymentNo, AppSession.UserLogin.UserID, 0);
                            }
                            else
                            {
                                int? journalId = JournalTransactions.AddNewPaymentCashBasedJournal(journalType,
                                                                                                   entity, reg,
                                                                                                   tpiColl,
                                                                                                   "TP", entity.PaymentNo,
                                                                                                   AppSession.UserLogin.
                                                                                                       UserID, 0);
                            }
                        }
                    }
                    /* Automatic Journal Testing End */

                    //if (AppSession.Parameter.IsPhysicianFeeBasedOnPayment == "Yes")
                    //{
                    //    int? x = ParamedicFeeTransChargesItemCompSettled.AddNewSettled(entity, TransPaymentItemOrders,
                    //                                                               TransPaymentItemIntermBills,
                    //                                                               AppSession.UserLogin.UserID);
                    //}

                    //if (AppSession.Parameter.IsPhysicianFeeArCreateOnArReceipt == "Yes")
                    //{
                    //    int? x = ParamedicFeeTransChargesItemCompSettled.AddNewSettled(entity, TransPaymentItemIntermBills,
                    //                                                               AppSession.UserLogin.UserID);

                    //    int? y = ParamedicFeeTransChargesItemCompSettled.AddNewSettled(entity, TransPaymentItemIntermBillGuarantors,
                    //                                                               AppSession.UserLogin.UserID);
                    //}
                }
                else
                {
                    if (entity.TransactionCode == TransactionCode.PaymentReturn)
                    {
                        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "Yes")
                        {
                            //function ini utk payment return dari pembelian resep
                            int? journalId =
                                JournalTransactions.AddNewPaymentReturnCorrectionJournal(
                                    BusinessObject.JournalType.PaymentReturn, entity, reg, tpiColl, "TP",
                                    AppSession.UserLogin.UserID, 0);
                        }
                        else
                        {
                            //var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                            //if (type.Contains(reg.SRRegistrationType))
                            //{

                            //}
                            //else
                            {
                                int? journalId =
                                    JournalTransactions.AddNewPaymentReturnCorrectionJournal(
                                        BusinessObject.JournalType.PaymentReturn, entity, reg, tpiColl, "TP",
                                        AppSession.UserLogin.UserID, 0);
                            }
                        }
                    }
                    else if (entity.TransactionCode == TransactionCode.Payment)
                    {
                        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "Yes")
                        {
                            int? journalId =
                                JournalTransactions.AddNewPaymentCorrectionJournal(BusinessObject.JournalType.Payment,
                                    entity, reg, tpiColl, "TP",
                                    AppSession.UserLogin.UserID, 0);
                        }
                        else
                        {
                            var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                            if (type.Contains(reg.SRRegistrationType))
                            {
                                int? journalId =
                                    JournalTransactions.AddNewPaymentCorrectionJournalTemporary(BusinessObject.JournalType.Payment,
                                        entity, reg, tpiColl, "TP",
                                        AppSession.UserLogin.UserID, 0);
                            }
                            else
                            {
                                int? journalId =
                                    JournalTransactions.AddNewPaymentCorrectionJournal(BusinessObject.JournalType.Payment,
                                        entity, reg, tpiColl, "TP",
                                        AppSession.UserLogin.UserID, 0);
                            }
                        }
                    }

                    //if (AppSession.Parameter.IsPhysicianFeeBasedOnPayment == "Yes" || AppSession.Parameter.IsPhysicianFeeArCreateOnArReceipt == "Yes")
                    //{
                    //    int? x = ParamedicFeeTransChargesItemCompSettled.DeleteSettled(entity, false);
                    //}
                }
            }

        }
    }
}
